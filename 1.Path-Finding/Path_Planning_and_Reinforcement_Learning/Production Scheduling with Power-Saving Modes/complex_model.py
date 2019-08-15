from gurobipy import *
import params

class RelativeModel:
    """
    Wrapper around model and its variables
    """

    def __init__(self, instance, path, n_intervals):
        """
        Initialize a new relative model
        :param instance:
        :type instance : Instance
        :param path : folder where generated files (if any) will be saved
        :type path : str
        """
        # - model
        self.model = None
        self.x_nim = None
        self.y_jin = None
        self.a_jj = None
        self.s_j = None
        self.s_n = None
        self.p_nim = None
        self.bigM = None
        self.p_jin = None
        self.total_lazy = 0
        # instance and output
        self.path = path
        self.instance = instance
        self.OUTPUT_FLAG = params.PRINT_COMPLEX

        # Transform edges to vertices
        self.vert, self.edges = self.transform_graph()

        # Params
        self.I = n_intervals
        self.N = instance.N
        self.J = instance.J
        self.V = len(self.vert)

        # Generate the model
        self.generate_relative_model(instance)

    def transform_graph(self):
        """ Transforms instance graph to a new one where edges are not labelled (new vertices created)"""
        vert = []
        edges = [[] for i in range(self.instance.V)]

        # copy original vertices
        for v in range(self.instance.V):
            vert.append({"t_min": self.instance.vertices[v]["t_min"],
                         "t_max": self.instance.vertices[v]["t_max"],
                         "C": self.instance.vertices[v]["C"]})

        # transform edges into vertices
        trans_id = self.instance.V  # id of the next trans vertex

        for id_from in range(len(self.instance.edges)):
            for edge in self.instance.edges[id_from]:
                id_to, t, C = edge["to"], edge["t"], edge["C"]

                vert.append({"t_min": t, "t_max": t, "C": C})  # Create new transition vertex
                edges.append([])
                edges[id_from].append(trans_id)
                edges[trans_id].append(id_to)
                trans_id += 1

        # Add reflexive edge
        edges[self.instance.INIT].append(self.instance.INIT)

        return vert, edges

    def generate_relative_model(self, instance):
        """
        :param: instance
        :type: instance : Instance
        :return: model
        :rtype: Model
        """
        # print("- create model")
        # Create a new model
        model = Model("relative_model")

        # Set output flag to 0 if OUTPUT_FLAG = False
        if not self.OUTPUT_FLAG:
            model.setParam("OutputFlag", 0)

        # Create variables
        # BINARY
        # - x_nim : 1 iff resource n is in mode m during interval i
        x_nim = model.addVars(self.N, self.I, self.V, vtype=GRB.BINARY, name='x_nim')
        # - y_jin : 1 iff task j is processed by resource n during interval i
        y_jin = model.addVars(self.J, self.I, self.N, vtype=GRB.BINARY, name='y_jin')
        # - a_jj' : additional variable for switching between constraints
        a_jj = {}  # dictionary to save a_jj' variables
        for i, j in pairs_generator(self.J):
            # Add variables (0,1), (0,2), ..
            a_jj[i, j] = model.addVar(vtype=GRB.BINARY, name=''.join(['a_', str(i), '-', str(j)]))

        # CONTINUOUS
        # - s_j : start time of task j
        s_j = model.addVars(self.J, vtype=GRB.CONTINUOUS, lb=0, ub=instance.L, name='s_j')
        # - s_n : start time of the first (indexed 0) interval on resource n
        s_n = model.addVars(self.N, vtype=GRB.CONTINUOUS, lb=0, ub=instance.L, name='s_n')
        # - p_nim : processing time spent in mode m by resource n (length of the interval i for resource n)
        p_nim = model.addVars(self.N, self.I, self.V, vtype=GRB.CONTINUOUS, lb=0, ub=instance.L, name='p_nim')

        # BIG M
        #bigM = instance.L + instance.MAX_p_jm
        bigM = 2 * instance.L + 2

        # Processing times of task j on resource n in interval i
        p_jin = {}
        for j in range(self.J):
            for i in range(self.I):
                for n in range(self.N):
                    p_jin[j, i, n] = quicksum(x_nim[n, i, m]
                                              * (instance.tasks[j]["p"][m] if m < instance.V else (instance.L+1))
                                              for m in range(self.V))

        # Using lazy constraints
        model.Params.lazyConstraints = 1

        model.update()

        # Save variables
        self.model = model
        self.x_nim = x_nim
        self.y_jin = y_jin
        self.a_jj = a_jj
        self.s_j = s_j
        self.s_n = s_n
        self.p_nim = p_nim
        self.p_jin = p_jin
        self.bigM = bigM

        # Create CONSTRAINTS
        # - all resources need to be in a single mode in each interval
        model.addConstrs(x_nim.sum(n, i, '*') == 1
                         for n in range(self.N)
                         for i in range(self.I))
        # - resource can change its state only if edge in the graph G exists
        model.addConstrs(quicksum(x_nim[n, i + 1, m_] for m_ in self.edges[m]) >= x_nim[n, i, m]
                         for n in range(self.N)
                         for i in range(self.I - 1)
                         for m in range(self.V))
        # - the first state has to be INIT
        model.addConstrs(x_nim[n, 0, instance.INIT] == 1 for n in range(self.N))
        # - the last state has to be TERM
        model.addConstrs(x_nim[n, self.I-1, instance.TERM] == 1 for n in range(self.N))
        # - end of the last interval should be before L
        model.addConstrs(self.get_s_in(self.I-1, n) + p_nim.sum(n, self.I-1, "*") <= instance.L
                         for n in range(self.N))
        # - minimal and maximal time in each state is constrained
        model.addConstrs(x_nim[n, i, m] * self.vert[m]["t_min"] <= p_nim[n, i, m]
                         for n in range(self.N)
                         for i in range(self.I)
                         for m in range(self.V))  # minimal
        model.addConstrs(x_nim[n, i, m] * self.vert[m]["t_max"] >= p_nim[n, i, m]
                         for n in range(self.N)
                         for i in range(self.I)
                         for m in range(self.V))  # maximal
        # - processing of each task will start in a single interval only
        #model.addConstrs(y_jin.sum(j,"*","*") == 1 for j in range(self.J))
        # NOTE : destroying the symmetry (T1 -: R1; T2 -: R1, R2; T3 -: R1, R2, R3; ...)
        model.addConstrs(quicksum(y_jin[j, i, n]
                                  for i in range(self.I)
                                  for n in range(min(self.N, j + 1))
                                  ) == 1
                         for j in range(self.J))
        # - start time of task is after release time
        model.addConstrs(s_j[j] >= instance.tasks[j]["r"] for j in range(self.J))
        # - start time of task is before deadline (minus processing time)
        model.addConstrs(s_j[j] + p_jin[j, i, n] <= instance.tasks[j]["d"] + bigM * (1 - y_jin[j,i,n])
                         for n in range(self.N)
                         for i in range(self.I)
                         for j in range(self.J))
        # - start time of task and start time of interval of processor are connected
        model.addConstrs(self.get_s_in(i, n) <= s_j[j] + bigM * (1 - y_jin[j, i, n])  # TODO : bigM = instance.L + 1
                         for n in range(self.N)
                         for i in range(self.I)
                         for j in range(self.J))
        # - start time of task and ending time of interval of processor are connected
        model.addConstrs(s_j[j] + p_jin[j, i, n] <= self.get_s_in(i, n) + p_nim.sum(n, i, '*') + bigM * (1 - y_jin[j, i, n])
                         for n in range(self.N)
                         for i in range(self.I)
                         for j in range(self.J))
        # - two tasks can not overlap
        # NOTE : non-overlapping constraints are generated by mycallback in a lazy way


        # Additional constraints
        # NOTE : FOLLOWING CONSTRAINTS SHOULD ONLY TIGHTEN THE SOLUTION
        # NOTE : THEY SHOULD HAVE NO INFLUENCE ON OPTIMALITY OF THE FINAL SOLUTION

        # - constraining processing time in mode by sum of minimal processing times of tasks
        model.addConstrs(quicksum(y_jin[j, i, n] * (instance.tasks[j]["p"][m] if m < instance.V else (instance.L+1)) #* min(instance.tasks[j]["p"]) # TODO: check if correct
                                  for j in range(instance.J)
                                  ) <= p_nim[n, i, m] + (1 + (instance.L+1) * instance.J * (1 - x_nim[n, i, m]))
                         for n in range(instance.N)
                         for i in range(self.I)
                         for m in range(self.V))
        # # # NOTE : adding this constrain seems to improve time even more (in combination with the previous one)
        # model.addConstrs(quicksum(y_jin[j, i, n] * min(instance.tasks[j]["p"])
        #                           for j in range(instance.J)
        #                           ) <= p_nim.sum(n, i, '*')
        #                  for n in range(instance.N)
        #                  for i in range(self.I))

        # NOTE : CONSTRAINING PRECEDENCES
        for j, j_ in pairs_generator(instance.J):
            # If deadline of task j is before release time of another task j_, then j < j_
            if instance.tasks[j]["d"] <= instance.tasks[j_]["r"]:
                model.addConstr(a_jj[j, j_] == 1)
            if instance.tasks[j_]["d"] <= instance.tasks[j]["r"]:
                model.addConstr(a_jj[j, j_] == 0)

            #     # if release time of the j_ + minimal processing time + minimal processing time of j is greater than d(j)
            #     # then j must start before j_ (if they are on the same processor)
            #     if instance.tasks[j_]["r"] + min(instance.tasks[j_]["p"]) + min(instance.tasks[j]["p"]) > instance.tasks[j]["d"]:
            #         model.addConstr(a_jj[j, j_] == 1)
            #     if instance.tasks[j]["r"] + min(instance.tasks[j]["p"]) + min(instance.tasks[j_]["p"]) > instance.tasks[j_]["d"]:
            #         model.addConstr(a_jj[j, j_] == 0)

        # - set objective
        # print("- set objective")
        expr = LinExpr(0.0)
        for m in range(self.V):
            expr.add(self.vert[m]["C"] * p_nim.sum('*', '*', m))

        model.setObjective(expr, sense=GRB.MINIMIZE)
        model.update()

    def optimize(self, time_limit, log_path):
        """
        Optimize current model
        :param time_limit : time limit in seconds
        :param log_path : path to the log file
        :type log_path : str
        :return: None
        """
        if not os.path.exists(self.path):
            os.makedirs(self.path)

        self.model.setParam('TimeLimit', time_limit)
        self.model._data = self
        self.model.optimize(mycallback)

        # Print some info
        if params.PRINT_COMPLEX:
            if self.model.status == GRB.Status.INFEASIBLE:
                print("[Complex model:] Model was proven infeasible")
            elif self.model.status == GRB.Status.TIME_LIMIT:
                print("[Complex model:] Time limit was reached.")
                if self.model.SolCount > 0:
                    print("        Suboptimal solution was found.")
                    #self.model.write(''.join([self.path, '/model.sol']))
            elif self.model.status == GRB.Status.OPTIMAL:
                print("[Model complex:] Optimal solution was found")
                #self.model.write(''.join([self.path, '/model.sol']))

    def get_s_in(self, i, n):
        """
        Return start time (variable) of interval i on resource n
        for 0th interval - start time is 0
        for ith interval - start time is sum of all processing times on given resource over all intervals [0,1,...,i-1]
        :return:
        """
        if i == 0:
            return self.s_n[n]
        else:
            return self.s_n[n] + quicksum(self.p_nim[n, i_, m] for i_ in range(i) for m in range(self.V))

    def get_s_in_val(self, i, n):
        """
        Return start time (real value) of interval i on resource n
        :param i: Index of interval
        :param n: Index of resource
        :return: Value of start time s_in of ith interval on nth resource
        """
        if i == 0:
            return self.s_n[n].X
        else:
            return self.get_s_in(i, n).getValue()

    def get_n_solutions(self):
        """
        Return solCount param. of Gurobi model
        :return: number of solutions found for this model
        :rtype: int
        """
        return self.model.SolCount

    def get_objective(self):
        """
        Return objValue param. of Gurobi model
        :return: objective value of solved model
        """
        return self.model.ObjVal

    def get_modes(self):
        """
        Returns list containing (for each resource) list of mode indices (for each interval)
        :return: List of mode indices for each interval for each resource
        """

        def x_max_over_m(n, i):
            for m in range(self.V):
                if self.x_nim[n, i, m].X > 0.5:
                    return m
            return -1

        return [[x_max_over_m(n, i) for i in range(self.I)] for n in range(self.N)]

    def get_start_modes(self):
        """
        Returns list of lists of start times of individual intervals (for each resource)
        :return: list
        """
        return [[self.get_s_in_val(i, n) for i in range(self.I)] for n in range(self.N)]

    def get_processing_modes(self):
        """
        Returns list of lists of processing times of individual modes (for each resource)
        :return: list
        """
        modes = self.get_modes()
        return [[self.p_nim[n, i, int(modes[n][i])].X for i in range(self.I)] for n in range(self.N)]

    def get_tasks_assign(self):
        """
        Returns list of [resource, interval] assignments (for each task)
        :return: list
        """

        def x_max_over_i_n(j):
            for i in range(self.I):
                for n in range(self.N):
                    if self.y_jin[j, i, n].X > 0.5:
                        return [n, i]
            return [-1, -1]

        return [x_max_over_i_n(j) for j in range(self.J)]

    def get_tasks_process(self):
        """
        Returns list of processing times of each task
        :return: list
        """
        assign = self.get_tasks_assign()
        modes = self.get_modes()
        return [self.instance.tasks[j]["p"][modes[assign[j][0]][assign[j][1]]] for j in range(self.J)]

    def get_tasks_start(self):
        """
        Return list of tasks start times
        :return: list
        """
        return [self.s_j[j].X for j in range(self.J)]

    def print_sol(self):
        """
        Print some information about the solution to the standard output
        :return: None
        """
        if self.model.SolCount < 1:
            print("No solution found. nothing to print.")
            return
        DECIMALS = "%.4f"
        modes = self.get_modes()
        start_modes = self.get_start_modes()
        processing_modes = self.get_processing_modes()
        tasks_assign = self.get_tasks_assign()
        tasks_process = self.get_tasks_process()
        tasks_start = self.get_tasks_start()

        print("|==============================")
        print("| SOLUTION INFO")
        print("|==============================")
        print("| Modes")
        for i in range(self.instance.N):
            print("|    ", i, ": ", modes[i])
        print("|------------------------------")
        print("| Start times of modes")
        for i in range(self.instance.N):
            print("|    ", i, ": ", [DECIMALS % x for x in start_modes[i]])

        print("|------------------------------")
        print("| Processing times of modes")
        for i in range(self.instance.N):
            print("|    ", i, ": ", [DECIMALS % x for x in processing_modes[i]])
        print("|------------------------------")
        print("| Assignment of tasks to resources, start times and processing times of individual tasks")
        for j in range(self.instance.J):
            print("|    ", j, ": ", tasks_assign[j], " ", round(tasks_start[j], 4), " ", tasks_process[j])
        print("|==============================")

    def write_sol_log(self, path):
        """
        Write solution to the log file
        :param path: Path to the file
        :return: None
        """
        inst = self.instance
        f = open(path, "a+")
        f.write("INSTANCE: N = %d, J = %d, V = %d, E = %d, I = %d, L = %d, (INIT,TERM) = (%d, %d), Time limit = %f, " %
                (inst.N, inst.J, inst.V, inst.E, inst.I, inst.L, inst.INIT, inst.TERM, self.model.params.timeLimit))

        f.write("PARAMS: ")
        if self.instance.complex_load:
            f.write("Load = Complex, ")
        else:
            f.write("Load = Simple, ")

        if self.model.status == GRB.Status.INFEASIBLE:
            f.write("SOLUTION: infeasible\n")
        elif self.model.status == GRB.Status.TIME_LIMIT:
            f.write("SOLUTION: time_limit. ")
            if self.model.SolCount > 0:
                f.write("suboptimal, objective = %f, gap = %f\n" % (self.model.ObjVal, self.model.MIPGap))
            else:
                f.write("\n")
        elif self.model.status == GRB.Status.OPTIMAL:
            f.write("SOLUTION: optimal, ")
            f.write("Objective = %f, runtime = %f\n" % (self.model.ObjVal, self.model.runtime))

        f.close()


def pairs_generator(rng):
    """
    Creates Generator of ordered pairs
    :param rng : Range of the interval, e.g. 3 means pairs (0,1),(0,2),(1,2)
    :return:
    """
    return ((i, j) for i in range(rng) for j in range(rng) if i < j)


def mycallback(model, where):
    """
    My callback function to set lazy constraints
    :param model:
    :type model : Model
    :param where:
    :return:
    """
    if where == GRB.Callback.MIPSOL:
        EPS = 0.0001
        generated = 0

        rel_model = model._data
        instance = rel_model.instance

        x = rel_model.x_nim
        y = rel_model.y_jin
        s_j = rel_model.s_j
        a_jj = rel_model.a_jj
        p_jin = rel_model.p_jin
        val_x = model.cbGetSolution(x)
        val_y = model.cbGetSolution(y)
        val_s_j = model.cbGetSolution(s_j)
        val_a_jj = model.cbGetSolution(a_jj)

        for j, j_ in pairs_generator(instance.J):
            for i in range(rel_model.I):
                for n in range(instance.N):
                    # If both tasks are on the same processor and interval
                    if val_y[j, i, n] > 0.5 and val_y[j_, i, n] > 0.5:
                        mode = -1
                        for m in range(rel_model.V):
                            if val_x[n, i, m] > 0.5:
                                mode = m
                                break
                        # If tasks does not respect ordering a_jj, generate new constraints
                        if val_a_jj[j,j_] > 0.5 and val_s_j[j] + instance.tasks[j]["p"][mode] > val_s_j[j_] + EPS:
                            #print("j,j_: %d %d, s_j=%f, p_j=%f, s_j_=%f (a_jj_: %f)" %(j,j_, val_s_j[j], instance.tasks[j][2][mode], val_s_j[j_], val_a_jj[j,j_]))
                            # Generate lazy constraint
                            model.cbLazy(s_j[j] + p_jin[j, i, n] <= s_j[j_]
                                         + rel_model.bigM * (1 - y[j, i, n])
                                         + rel_model.bigM * (1 - y[j_, i, n])
                                         + rel_model.bigM * (1 - a_jj[j, j_])
                                         )
                            generated += 1

                        if val_a_jj[j, j_] < 0.5 and val_s_j[j_] + instance.tasks[j_]["p"][mode] > val_s_j[j] + EPS:
                            #print("II) j,j_: %d %d, s_j_=%f, p_j_=%f, s_j=%f (a_jj_: %f)" %(j,j_, val_s_j[j_], instance.tasks[j_][2][mode], val_s_j[j], val_a_jj[j,j_]))

                            model.cbLazy(s_j[j_] + p_jin[j_, i, n] <= s_j[j]
                                         + rel_model.bigM * (1 - y[j, i, n])
                                         + rel_model.bigM * (1 - y[j_, i, n])
                                         + rel_model.bigM * (a_jj[j, j_])
                                         )
                            generated += 1

        rel_model.total_lazy += generated
        if model.Params.OutputFlag == 1:
            print("[Callback:] Generated new constraints: ", generated, " (", rel_model.total_lazy," total)", sep="")
