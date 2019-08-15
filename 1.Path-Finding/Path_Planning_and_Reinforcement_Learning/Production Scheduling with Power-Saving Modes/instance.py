# ======================================================================================================================
# Wrapper class around instance (including init from file, print methods, heuristic solution, ...)
# ======================================================================================================================
import bisect
import model
import params
from gurobipy import *
from solution import MonoprocessorSolution


class Instance:
    """
    Wrapper class around instance parameters
    """

    def __init__(self, path, debug=False):
        """
        Initialize this instance from text file
        :param path: path to the instance file, if path is set to None, instance will not be loaded from file
        :param debug: True if some debugging info should be printed to standard output
        :type path: str
        :type debug: bool
        :return: None
        """
        # Declare attributes
        # - instance parameters
        self.N = None  # Number of parallel resources
        self.J = None  # Number of tasks to be assigned to the resources
        self.V = None  # Number of vertices in input graph
        self.E = None  # Number of edges
        self.P = None  # Number of profiles
        self.L = None  # Length of the scheduling period
        self.INIT = None  # Initial state
        self.TERM = None  # Terminal state
        self.vertices = None  # List of vertices (additional vertices after transformation are appended to this list)
        self.edges = None  # List of edges
        self.tasks = None  # List of tasks
        self.profiles = None  # List of production profiles
        self.profiles_tree = None  # Tree of all profiles
        # - transformed graph
        self.V_complete = None  # Number of vertices in transformed graph (non-zero edges transformed to vertices)
        self.E_complete = None  # Number of edges after transformation
        self.edges_complete = None  # List of edges after transformation
        self.V_distances = None  # Distances between pairs of verticees
        # Edge (in the transformed graph) between 2 original nodes is mapped onto new vertex
        # index of the new vertex is saved into trans_nodes dict
        self.trans_nodes = {}
        # - additional instance params
        self.MAX_p_jm = 0  # Maximal processing time (excluding those states, where the task can not be processed)
        self.MIN_p_jm = float("inf")
        self.MAX_C = 0  # Maximal energy consumption over all states of the original graph
        self.min_price = 0.0  # Minimal necessary price for tasks processing
        self.min_time = 0.0  # Minimal necessary time for tasks processing
        self.debug = debug
        self.loaded = False
        # - heuristic solution
        self.heuristic = None

        if path is not None:
            # Load data
            self.dprint("[Instance:] Loading data.")
            self.read_data(path)
            if len(self.profiles[0]) > 1:
                print("Warning: the first profile should be <simple> - single mode..")

            # Transform graph and compute distances between all pairs
            self.dprint("[Instance:] Transforming graph.")
            self.transform_graph()

            # Add profiles to the tree
            self.dprint("[Instance:] Creating tree of profiles.")

            # Try to compute heuristic solution
            if params.INSTANCE_HEURISTIC_INIT:
                self.dprint("[Instance:] Computing heuristic solution.")
                self.heuristic = Instance.Heuristic(self)
                self.heuristic.dprint()

            # Set flag that instance was loaded
            self.loaded = True

    def read_data(self, path):
        """
        Read data from file and set appropriate attributes
        :param path: Path to the file with instance data
        :type path: str
        :return: None
        """

        def line_to_int_list(f):
            """
            Read a single line from file to the list of ints
            :param f: input file
            :return: list of ints (read from a single line of f)
            :rtype: list
            """
            return list(map(int, f.readline().strip().split(' ')))

        def max_up_to_x(seq, x):
            """
            Return maximal value of seq, which is lower than x
            :param seq: list of numbers
            :param x: some number
            :type seq: list
            :return: maximum of seq lower than x (or None)
            """
            seq_sorted = sorted(seq)
            index = bisect.bisect(seq_sorted, x)
            return seq_sorted[index - 1]

        # Open the data file
        f_in = open(path, 'r')

        # Read the first line: |N| |J| |V| |E| |I| |P|
        self.N, self.J, self.V, self.E, self.P = line_to_int_list(f_in)

        # Read the second line: L
        self.L = line_to_int_list(f_in)[0]

        # Read the third line: INIT TERM
        self.INIT, self.TERM = line_to_int_list(f_in)

        # Read vertices (each line: t_min t_max C)
        self.vertices = list()
        for i in range(self.V):
            line = line_to_int_list(f_in)
            vertex = {"t_min": line[0],
                      "t_max": line[1],
                      "C": line[2]
                      }
            self.vertices.append(vertex)

            # Check for max C
            if vertex["C"] > self.MAX_C:
                self.MAX_C = vertex["C"]

        # Read edges (each line: from to time price); save them as outgoing edges for each vertex
        self.edges = [[] for i in range(self.V)]
        for i in range(self.E):
            numbers = line_to_int_list(f_in)
            edge = {"to": numbers[1],  # index of terminal vertex
                    "t": numbers[2],  # time
                    "C": numbers[3]  # price (for time unit)
                    }
            self.edges[numbers[0]].append(edge)

        # Read profiles
        self.profiles = list()
        for p in range(self.P):
            self.profiles.append(tuple(line_to_int_list(f_in)))

        # Read tasks
        self.tasks = list()
        for i in range(self.J):
            numbers = line_to_int_list(f_in)
            task = {"r": numbers[0],  # release time
                    "d": numbers[1],  # deadline
                    "p": numbers[2:]  # processing times (list, one processing time for each vertex)
                    }
            self.tasks.append(task)

            max_processing = max_up_to_x(task["p"], self.L)
            min_processing = min(task["p"])

            # Save max processing time
            if max_processing > self.MAX_p_jm:
                self.MAX_p_jm = max_processing

            if min_processing < self.MIN_p_jm:
                self.MIN_p_jm = min_processing

            # Add minimal price
            self.min_price += min_processing * self.vertices[task["p"].index(min_processing)]["C"]  # time * price
            self.min_time += min_processing

        # Sort tasks according to deadlines
        self.tasks.sort(key=lambda x: x["d"])

        f_in.close()

    def transform_graph(self):
        """
        Make a complete graph (V_complete, E_complete, edges_complete)
        - transforms edges of the input graph into vertices
        - uses Floyd-Warshall algorithm to compute distances
        :return: None
        """
        matr = self.floyd_warshall()
        n_trans_nodes = 0  # number of new transition nodes
        n_edges = 0  # Number of edges in the transformed graph
        self.edges_complete = [[] for i in range(self.V)]

        # Add new vertices and edges, count edges
        for i in range(self.V):
            for j in range(self.V):
                # Init index of transition vertex
                self.trans_nodes[i, j] = None

                if i != j and matr[i][j]["time"] <= self.L:
                    if matr[i][j]["time"] > 0:
                        # Price from FW was for whole time, but algorithm uses price for time unit - it must be divided
                        vertex = {"t_min": matr[i][j]["time"],  # t_min
                                  "t_max": matr[i][j]["time"],  # t_max (= t_min for transition vertex)
                                  "C": matr[i][j]["price"]/matr[i][j]["time"]  # price for time unit
                                  }
                        self.vertices.append(vertex)  # Add transition vertex

                        # Save index of transition vertex
                        self.trans_nodes[i, j] = len(self.vertices)-1

                        # Check for max C
                        if matr[i][j]["price"]/matr[i][j]["time"] > self.MAX_C:
                            self.MAX_C = matr[i][j]["price"]/matr[i][j]["time"]

                        new_idx = len(self.edges_complete)
                        self.edges_complete.append([])  # New vertex created - add new placeholder for edges
                        self.edges_complete[i].append(new_idx)
                        self.edges_complete[new_idx].append(j)

                        n_edges += 2
                        n_trans_nodes += 1
                    else:  # Time on the edge is 0 - just add it without creating new vertex
                        # Save index of transition vertex
                        self.trans_nodes[i, j] = -1  # There is no transition vertex, just edge

                        self.edges_complete[i].append(j)
                        n_edges += 1

        # Add reflexive edge
        self.edges_complete[self.INIT].append(self.INIT)
        n_edges += 1

        self.V_complete = self.V + n_trans_nodes
        self.E_complete = n_edges

    def floyd_warshall(self):
        """
        Compute distances between each pair of vertices
            In the matrix there are dicts containing "time" and "price", where "price" is a total cost of transition
            (NOT for time unit)
        :return:
        """
        def d(i, j):
            return matr[i][j]["price"]

        # INITIALIZATION OF FW matrix
        new_V = self.V*2  # Divide each vertex to 2 (v_in, v_out) connected by edge t_min, C
        matr = [[{"time": self.L+1, "price": (self.L+1) * self.MAX_C} for j in range(new_V)] for i in range(new_V)]

        # Zeros on diagonal
        for i in range(new_V):
            for j in range(new_V):
                if i == j:
                    matr[i][j]["time"] = 0
                    matr[i][j]["price"] = 0

        # Initialize lengths
        # - splitting vertices
        for v in range(self.V):
            matr[v][v + self.V]["time"] = self.vertices[v]["t_min"]
            matr[v][v + self.V]["price"] = self.vertices[v]["t_min"] * self.vertices[v]["C"]
        # - adding edges
        for v in range(self.V):
            for e in self.edges[v]:
                matr[v + self.V][e["to"]]["time"] = e["t"]
                matr[v + self.V][e["to"]]["price"] = e["t"] * e["C"]

        # MAIN PART OF FW ALG
        # Compute distances
        for k in range(new_V):
            for i in range(new_V):
                for j in range(new_V):
                    if d(i, j) > d(i, k) + d(k, j):
                        matr[i][j]["time"] = matr[i][k]["time"] + matr[k][j]["time"]
                        matr[i][j]["price"] = matr[i][k]["price"] + matr[k][j]["price"]

        self.V_distances = matr
        return matr

    def add_profiles(self):
        """
        Create new root Node of profiles_tree and add each profile to this root
        :return: None
        """
        self.profiles_tree = Instance.Node(self.INIT)

        for p in range(len(self.profiles)):
            self.profiles_tree.add_profile(self.profiles[p], p)

    def get_trans_cost(self, profile_idx):
        """
        Get transition costs of profile with index [profile_idx]
        :param profile_idx: index of profile which transition costs should be returned
        :return: sum of transition costs of profile
        """
        profile = self.profiles[profile_idx]
        cost = 0.0

        for i in range(len(profile)):
            if i == 0:
                # - transition to initial state
                cost += self.V_distances[self.INIT][profile[i]]["price"]
            else:
                # - transition between states
                cost += self.V_distances[profile[i-1]][profile[i]]["price"]

        # - transition to terminal state
        cost += self.V_distances[profile[-1]][self.TERM]["price"]

        return cost


    def get_modes_leading_to_TERM(self):
        """ For backward compatibility"""
        return [i for i in range(self.V)]


    def dprint(self, message):
        """
        Print message if this instance was loaded in adebug mode
        :param message: Text to be printed
        :type message: str
        :return: None
        """
        if self.debug:
            print(message)

    def __str__(self):
        if not self.loaded:
            return "Instance has not been loaded yet."

        str_build = list()
        str_build.append("|==============================\n")
        str_build.append("| INSTANCE:\n")
        str_build.append("|    |N| = %d\n" % self.N)
        str_build.append("|    |J| = %d\n" % self.J)
        str_build.append("|    |V| = %d\n" % self.V)
        str_build.append("|    |E| = %d\n" % self.E)
        str_build.append("|    |P| = %d\n" % self.P)
        str_build.append("|    |L| = %d\n" % self.L)
        str_build.append("|    Initial  state: %d\n" % self.INIT)
        str_build.append("|    Terminal state: %d\n" % self.TERM)
        str_build.append("|------------------------------\n")
        str_build.append("| Max C    = %d\n" % self.MAX_C)
        str_build.append("| Max p_jm = %d\n" % self.MAX_p_jm)
        str_build.append("| Min p_jm = %d\n" % self.MIN_p_jm)
        str_build.append("| Min process time = %d\n" % self.min_time)
        str_build.append("| Min process price = %d\n" % self.min_price)
        str_build.append("|------------------------------\n")

        str_build.append("| Vertices: [t_min, t_max, C]\n")
        for v in range(self.V):
            str_build.append("|    [%d, %d, %.2f]\n" % (self.vertices[v]["t_min"],
                                                        self.vertices[v]["t_max"],
                                                        self.vertices[v]["C"]))
        str_build.append("|------------------------------\n")

        str_build.append("| Edges: from -> [[to, time, price], ...]\n")
        for e in range(self.V):
            str_build.append("|    %d -> %s\n" % (e, self.edges[e]))
        str_build.append("|------------------------------\n")

        str_build.append("| Profiles\n")
        for p in self.profiles:
            str_build.append("| ")
            str_build.append(str(p))
            str_build.append("\n")
        str_build.append("|------------------------------\n")

        str_build.append("| Distances\n")
        for i in range(self.V):
            str_build.append("| ")
            for j in range(self.V):
                str_build.append("(%d %.2f) " % (self.V_distances[i][j]["time"], self.V_distances[i][j]["price"]))
            str_build.append("\n")
        str_build.append("|------------------------------\n")

        str_build.append("| Tasks: id: [release, deadline, processing]\n")
        for j in range(len(self.tasks)):
            str_build.append("|    %d: %s\n" % (j, self.tasks[j]))
        str_build.append("|------------------------------\n")

        str_build.append("| TRANSFORMED GRAPH:\n")
        str_build.append("| Vertices: [t_min, t_max, C]\n")
        for v in self.vertices:
            str_build.append("|    [%d, %d, %.2f]\n" % (v["t_min"], v["t_max"], v["C"]))
        str_build.append("|------------------------------\n")

        str_build.append("| Edges: from -> [to, ...]\n")
        for e in range(self.V_complete):
            str_build.append("|    %d -> %s\n" % (e, self.edges_complete[e]))
        str_build.append("|------------------------------\n")
        str_build.append("|==============================\n")
        return "".join(str_build)

    # INNER CLASSES ----------------------------------------------------------------------------------------------------
    class Node:
        """
        Single node of profiles tree
        """
        def __init__(self, mode):
            self.mode = mode  # Current mode in this node
            self.next = list()  # List of Node
            self.term = False  # True if this state is the last state of some profile (excluding TERM state)
            self.term_idx = -1  # Index of profile which state is terminal

        def add_profile(self, profile, profile_idx):
            """
            Add htis profile to the tree
            :param profile: List of modes indices
            :param profile_idx: Index of [profile]
            :type profile: list Node
            :return: None
            """
            # Profile is empty
            if len(profile) == 0:
                return

            fst_mode = profile[0]
            # Find next node or create it
            if Instance.Node.in_list(fst_mode, self.next):
                cur_node = self.next[Instance.Node.in_list_pos(fst_mode, self.next)]
            else:
                cur_node = Instance.Node(fst_mode)
                self.next.append(cur_node)
                if len(profile) == 1:
                    cur_node.term = True
                    cur_node.term_idx = profile_idx

            # Add rest of the profile to the next node
            cur_node.add_profile(profile[1:], profile_idx)

        @staticmethod
        def in_list(mode, lst):
            """
            Check if Node with mode [mode] is inside list [lst]
            :param mode: is [mode] in the list [lst]
            :param lst: list of Nodes
            :return: True if node with mode [mode] is inside list [lst]
            """
            for node in lst:
                if node.mode == mode:
                    return True
            return False

        @staticmethod
        def in_list_pos(mode, lst):
            """
            Return index of Node with mode [mode] in the list [lst] or -1 if there is no such Node
            :param mode:
            :param lst:
            :return: Index or -1
            """
            for i in range(len(lst)):
                if lst[i].mode == mode:
                    return i
            return -1

        def print_tree(self, depth):
            """
            Print whole subtree starting from this node
            :param depth: starting depth
            :return: None
            """
            self.print_node(depth)

            for node in self.next:
                node.print_tree(depth+1)

        def print_node(self, depth):
            """
            Print this current node (format: [depth spaces] mode [(T) if this node is terminal of some profile]\n)
            :param depth: Depth of this node in the tree
            :return: None
            """
            print(depth * " " + str(self.mode) + " " +  str(self.term_idx))
            # print(depth * " " + str(self.mode) + "(T) %d" % self.term_idx if self.term else "")

    class Heuristic:
        """
        Wrapper around heuristic solution
        """
        def __init__(self, inst):
            """
            Initialize this heuristic solution
            :param inst: problem instance
            :type inst: Instance
            """
            self.instance = inst
            self.r_time = [0 for n in range(inst.N)]  # current time on resources
            self.r_profile = [0 for n in range(inst.N)]  # assignment resource -> profile
            self.r_start = [[0] for n in range(inst.N)]  # start times of intervals
            self.r_mode = [inst.INIT for n in range(inst.N)]  # Current mode for each resource
            self.t_start = [-1 for j in range(inst.J)]  # start times of tasks
            self.t_assign = [-1 for j in range(inst.J)]  # assignment task -> resource
            self.f_complete = True  # flag (True if tasks were completely assigned
            self.patterns = []  # Initial patterns found by heuristic
            self.patterns_costs = []  # Costs of initial patterns
            self.patterns_solutions = []  # optimal profiles for each pattern

            # Find heuristic solution
            self.find()
            self.patterns = self.get_patterns()
            self.patterns_costs, self.patterns_solutions = self.get_costs_and_sols()

            #print(self.patterns_costs)

        def find(self):
            """
            Find simplistic heuristic solution for Skoda dataset
            (all resources are switched into working state and tasks (sorted EDF) are assigned to minimize completition)
            :return: None
            """
            # Turn resources to working state
            t_off_on = self.instance.V_distances[2][0]["time"]
            t_on_off = self.instance.V_distances[0][2]["time"]

            for n in range(self.instance.N):
                self.r_time[n] = t_off_on
                self.r_start[n] = [0, t_off_on]
                self.r_mode[n] = 0

            # Try to assign each task
            for j in range(self.instance.J):
                best_compl = float("inf")
                best_start = -1
                best_res = -1
                for n in range(self.instance.N):
                    r = self.instance.tasks[j]["r"]  # release time
                    d = self.instance.tasks[j]["d"]  # deadline
                    p = self.instance.tasks[j]["p"][self.r_mode[n]] # processing time
                    t = self.r_time[n]  # time on resource [n]

                    c = t + max(0, r-t) + p  # completion time
                    # check if deadline is OK and resource has time to turn off
                    if c <= d and c < self.instance.L - t_on_off:
                        if c < best_compl:
                            best_compl = c
                            best_res = n
                            best_start = t + max(0, r-t)

                if best_res != -1:
                    self.t_start[j] = best_start
                    self.t_assign[j] = best_res
                    self.r_time[best_res] = best_compl
                else:
                    self.f_complete = False

            # Add transition to Term state
            for n in range(self.instance.N):
                self.r_time[n] += t_on_off  # Note: should there be +1 to start mode after finishing previous task?
                self.r_start[n].append(self.r_time[n])
                self.r_mode[n] = 2

        def get_patterns(self):
            """
            Find patterns from heuristic solution
            :return: List containing patterns (lists of tasks indices)
            """
            if not self.f_complete:
                return []

            patterns = []
            for n in range(self.instance.N):
                p_n = []
                for j in range(self.instance.J):
                    if self.t_assign[j] == n:
                        p_n.append(j)
                patterns.append(p_n.copy())

            return patterns

        def get_costs_and_sols(self):
            """
            Find the cost of each pattern -> for each pattern, ILP (subproblem model) is run (cost = ILP.objective)
                     solution (containing s_i, s_j, p_i, y_ji, prof_idx)
            :return: List of costs
            """
            #print("  - Evaluating heuristic patterns:")
            patterns_costs = []
            patterns_solutions = []

            for i in range(len(self.patterns)):
                # Find optimal cost for each pattern
                best_cost = float("inf")
                best_solution = None

                for p_idx in range(len(self.instance.profiles)):
                    m = model.MonoprocessorModel(self.instance, p_idx, self.patterns[i])
                    m.optimize(float("inf"))

                    if m.model.status == GRB.Status.OPTIMAL:
                        if m.get_obj_val() < best_cost:
                            best_cost = m.get_obj_val()
                            best_solution = MonoprocessorSolution().init_from_heuristics(self.instance,
                                                                                         m.model.getAttr("x", m.s_i),
                                                                                         m.model.getAttr("x", m.s_j),
                                                                                         m.model.getAttr("x", m.p_i),
                                                                                         m.model.getAttr("x", m.y_ji),
                                                                                         p_idx)

                patterns_costs.append(best_cost)
                patterns_solutions.append(best_solution)

            return patterns_costs, patterns_solutions

        def dprint(self):
            """
            Print heuristic solution if debug mode is set
            :return: None
            """
            if self.instance.debug:
                print("[Heuristic:] Heuristic is complete." if self.f_complete else "[Heuristic:] Heuristic is not complete.")
                print("[ - tasks] id: assign, start")
                for j in range(self.instance.J):
                    print("  %d: %d %.2f" % (j, self.t_assign[j], self.t_start[j]))
                print("[ - patterns:]")
                for pattern in self.patterns:
                    print(pattern)
                print("[ - pattenrs costs:]")
                print(self.patterns_costs)
                print("[ - profiles]")
                print(self.r_profile)
                print("[ - start times]")
                for start in self.r_start:
                    print(start)