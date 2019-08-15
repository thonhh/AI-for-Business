# ======================================================================================================================
# Wrapper classes around models (including initialization, print methods, gathering solution methods, ...)
# ======================================================================================================================
from gurobipy import *
from docplex.cp.model import CpoModel
import copy
import time
import params


class ModelPrototype:
    """
    Super class of all model classes
    """

    def __init(self):
        self.model = None

    def set_timelimit(self, limit):
        raise NotImplementedError("This method needs to be implemented.")

    def model_optimize(self):
        raise NotImplementedError("This method needs to be implemented.")

    def write_model(self, path):
        raise NotImplementedError("This method needs to be implemented.")

    def write_solution(self, path):
        raise NotImplementedError("This method needs to be implemented.")

    def optimize(self, time_limit, out_path):
        """
        Optimize current model
        :param time_limit: time limit in seconds
        :param out_path: path for output files
        :type out_path: str
        :return: None
        """
        # Create dir if necessary
        if not os.path.exists(out_path):
            os.makedirs(out_path)

        self.set_timelimit(time_limit)
        self.write_model(out_path)
        self.model_optimize()
        #self.write_solution(out_path)


class ModelILP(ModelPrototype):
    def set_timelimit(self, limit):
        self.model.setParam('TimeLimit', limit)

    def model_optimize(self):
        self.model.optimize()

    def write_model(self, path):
        #self.model.write(''.join([path, '/model_master.lp']))
        pass

    def write_solution(self, path):
        #self.model.write(''.join([path, '/model_master.sol']))
        pass


class ModelCP(ModelPrototype):
    def __init__(self):
        self.model = None
        self.sol = None
        self.timelimit = float("inf")

    def set_timelimit(self, limit):
        self.timelimit = limit

    def model_optimize(self):
        if params.PRINT_SUB:
            sol = self.model.solve(TimeLimit=self.timelimit, Workers=1, RelativeOptimalityTolerance=0, OptimalityTolerance=0.0001)
        else:
            sol = self.model.solve(TimeLimit=self.timelimit, LogVerbosity="Quiet", Workers=1, RelativeOptimalityTolerance=0, OptimalityTolerance=0.0001)  # , DefaultInferenceLevel='Extended', Workers=1)

        self.sol = sol

    def write_model(self, path):
        #print("Write method for CP model has not been implemented yet.")
        return

    def write_solution(self, path):
        #print("Write method for CP model has not been implemented yet.")
        return


class MasterModel(ModelILP):
    """
    Wrapping class around master problem

    min sum c_k t_k + M*alpha
    s.t. sum a_jk t_k >= 1 (for all j)
         sum t_k <= N + alpha
         all vars >= 0
    """

    def __init__(self, patterns, costs, instance):
        """
        Initialize model
        :param patterns: initial patterns (assignment of tasks to resources)
        :param costs: costs of patterns
        :param instance: problem instance
        """
        self.J = instance.J
        self.N = instance.N
        self.patterns = patterns
        self.costs = costs

        self.model = Model("master_model")
        self.M = instance.L*instance.MAX_C + 1
        self.t_k = None  # primal variables
        self.alpha = None  # dummy variable

        self.init_model()

    def init_model(self):
        # - set logging
        if not params.PRINT_MASTER:
            self.model.Params.LogToConsole = 0
        else:
            self.model.Params.LogToConsole = 1

        # Init variables
        self.t_k = self.model.addVars(len(self.patterns), lb=0.0, vtype=GRB.CONTINUOUS, name='t_k')
        self.alpha = self.model.addVar(lb=0.0, vtype=GRB.CONTINUOUS, name="alpha")

        # Add constraints
        for j in range(self.J):
            expr = LinExpr(0.0)
            for k in range(len(self.patterns)):
                if j in self.patterns[k]:
                    expr.add(self.t_k[k])
            self.model.addConstr(expr >= 1)  # sum a_jk t_k >= 1 (all tasks are scheduled at least once)

        self.model.addConstr(self.t_k.sum("*") <= self.N + self.alpha)  # sum t_k <= N + alpha (at most N resources)

        # Add objective: min sum c_k t_k
        self.model.setObjective(quicksum(self.costs[k] * self.t_k[k] for k in range(len(self.patterns))) + self.M*self.alpha,
                                sense=GRB.MINIMIZE)

        self.model.update()

    def alpha_is_not_zero(self):
        """
        Check if variable alpha has non-zero value in the solution
        :return: True is alpha > 0
        """
        return self.alpha.X > 0.0 + 0.000001

    def solution_is_integer(self):
        """
        Check if the solution of master model is integer
        :return: True if solution is int
        """
        EPS = 0.0001
        for i in range(len(self.patterns)):
            if EPS < self.t_k[i].X < 1 - EPS:
                return False
        return True

    def get_selected_indices(self):
        """Return list of selected pattern indices"""
        EPS = 0.0001
        indices = []
        for i in range(len(self.patterns)):
            if EPS < self.t_k[i].X:
                indices.append(i)
        return indices


class DualModel(ModelILP):
    """
    Wrapping class around dual problem

    max sum_{1 <= j <= J} l_j + N l_0
        s.t.: sum_{a_jk l_j} + l_0 <= c_k (for all r_k in O)
              l_0 <= 0
              l_0 >= -M (for M see Master model)
              l_j >= 0 (for all 1 <= j <= J)
    """

    def __init__(self, patterns, costs, instance):
        """
        Initialize model
        :param patterns: initial patterns (assignment of tasks to resources)
        :param costs: costs of patterns
        :param instance: problem instance (for big M)
        """
        self.J = instance.J
        self.N = instance.N
        self.patterns = copy.deepcopy(patterns)
        self.costs = copy.deepcopy(costs)

        self.model = Model("dual_model")
        self.M = instance.L*instance.MAX_C + 1
        self.l_j = None  # dual variables
        self.l_0 = None
        self.dummy_constr = None
        self.init_model()

    def init_model(self):
        # - set logging
        if not params.PRINT_DUAL:
            self.model.Params.LogToConsole = 0
        else:
            self.model.Params.LogToConsole = 1

        # Init dual variables
        self.l_j = self.model.addVars(self.J, vtype=GRB.CONTINUOUS, name="l_j")
        self.l_0 = self.model.addVar(vtype=GRB.CONTINUOUS, name="l_0", lb=-self.M, ub=0)

        # Add constraints
        for i in range(len(self.patterns)):
            # - sum of (a_jk * l_j)
            expr = LinExpr(0.0)
            for p in self.patterns[i]:
                expr.add(self.l_j[p])
            # - add l_0
            expr.add(self.l_0)

            # -> sum (a_jk * l_j) + l_0 <= c_k (for all r_k in O)
            self.model.addConstr(expr <= self.costs[i])

        # constraints on l
        self.model.addConstrs(self.l_j[j] >= 0 for j in range(self.J))

        # Add objective: max sum(l_j) + N * l_0
        self.model.setObjective(self.l_j.sum("*") + self.N * self.l_0, sense=GRB.MAXIMIZE)

        self.model.update()

    def add_pattern(self, pattern, ck):
        """
        Add dual constraint (pattern) to the model
        :param pattern: list of tasks in pattern
        :param ck: cost of pattern
        :param idx: index of the profile
        :return: None
        """
        self.patterns.append(pattern)
        self.costs.append(ck)

        # -> sum (a_jk * l_j) + l_0 <= c_k (for all r_k in O)
        expr = LinExpr(0.0)
        for p in pattern:
            expr.add(self.l_j[p])  # - sum of (a_jk * l_j)
        expr.add(self.l_0)  # - add l_0

        self.model.reset()
        self.model.addConstr(expr <= ck)
        self.model.update()

class SubproblemModelCP(ModelCP):
    """Model for subproblem in CP formulation"""

    def __init__(self, instance, profile_idx, l_j, l_0, on_same, on_diff):
        """
        Initialize model
        :param instance: problem instance of class Instance
        :param profile_idx: index of one specific profile for this model
        :param l_j: variables from dual task (with optimal solution) (note l_0 is constant)
        :param on_same: pairs of task indices, that should be scheduled on the same resource
        :param on_diff: pairs of tasks indices, that should be scheduled on different resources
        """
        self.instance = instance
        self.profile_idx = profile_idx
        self.profile = self.instance.profiles[profile_idx]
        self.l_j = l_j
        self.l_0 = l_0
        self.on_same = on_same
        self.on_diff = on_diff
        self.trans_cost = self.instance.get_trans_cost(self.profile_idx)
        self.timelimit = float("inf")
        self.sol = None  # solution
        self.primitives = None
        self.modes_of_mach = None
        self.types = None

        self.init_model()

    def init_model(self):
        # INPUTS --------------------------------------------------
        inst = self.instance
        id_profile = self.profile_idx
        rewards = self.l_j
        equivalences = self.on_same
        mutexes = self.on_diff
        # END INPUTS ----------------------------------------------

        # MODEL --------------------------------------------------
        types = list(inst.profiles[id_profile])
        types.insert(0, inst.INIT)
        types.append(inst.TERM)
        trans_time = [[0 for i in range(inst.V)] for j in range(inst.V)]
        for i in range(len(inst.edges)):
            for j in range(len(inst.edges[i])):
                trans_time[i][inst.edges[i][j]['to']] = inst.edges[i][j]['t']
        changeover_cost = inst.get_trans_cost(id_profile)

        cp = CpoModel()
        """
        primitives = [[] for i in range(inst.J)]
        all_primitives = []
        init = cp.interval_var(start=0)
        init.set_end_max(inst.L)
        total_cost = changeover_cost + inst.vertices[inst.INIT]['C'] * cp.length_of(init, 0) - self.l_0  # subtract l_0
        modes_of_mach = [init]  # serves only for retrieving starts and ends of modes
        last_shift = init
        for i in range(1, len(types) - 1):
            v = types[i]
            shift = cp.interval_var()
            shift.set_size_min(inst.vertices[v]['t_min'])
            shift.set_size_max(inst.vertices[v]['t_max'])
            shift.set_end_max(inst.L)
            total_cost += inst.vertices[v]['C'] * cp.length_of(shift, 0)
            cp.add(cp.start_at_end(shift, last_shift, -trans_time[types[i - 1]][types[i]]))
            last_shift = shift
            modes_of_mach.append(shift)
            for t in range(inst.J):
                if inst.tasks[t]['p'][v] <= inst.L:
                    prim = cp.interval_var(size=inst.tasks[t]['p'][v])
                    total_cost -= rewards[t] * cp.presence_of(prim)
                    prim.set_optional()
                    prim.set_start_min(inst.tasks[t]['r'])
                    prim.set_end_max(inst.tasks[t]['d'])
                    primitives[t].append(prim)
                    all_primitives.append(prim)
                    cp.add(cp.start_before_start(shift, prim))
                    cp.add(cp.end_before_end(prim, shift))
        term = cp.interval_var(end=inst.L)
        total_cost += inst.vertices[inst.TERM]['C'] * cp.length_of(term, 0)
        cp.add(cp.start_at_end(term, last_shift, -trans_time[types[len(types) - 2]][inst.TERM]))
        modes_of_mach.append(term)
        cp.add(cp.no_overlap(cp.sequence_var(all_primitives)))
        for t in range(inst.J):
            cp.add(sum([cp.presence_of(p) for p in primitives[t]]) <= 1)
        for eq in equivalences:
            cp.add(sum([cp.presence_of(p) for p in primitives[eq[0]]]) == sum(
                [cp.presence_of(p) for p in primitives[eq[1]]]))
        for mut in mutexes:
            cp.add(sum([cp.presence_of(p) for p in primitives[mut[0]]]) + sum(
                [cp.presence_of(p) for p in primitives[mut[1]]]) <= 1)
        """
        #"""
        # MODEL 2
        primitives = [[] for i in range(inst.J)]
        all_primitives = []
        init = cp.interval_var(start=0)
        init.set_end_max(inst.L)
        total_cost = changeover_cost + inst.vertices[inst.INIT]['C'] * cp.length_of(init, 0) - self.l_0
        modes_of_mach = [init]  # serves only for retrieving starts and ends of modes
        last_shift = init
        for i in range(1, len(types) - 1):
            v = types[i]
            shift = cp.interval_var()
            shift.set_size_min(inst.vertices[v]['t_min'])
            shift.set_size_max(inst.vertices[v]['t_max'])
            shift.set_end_max(inst.L)
            total_cost += inst.vertices[v]['C'] * cp.length_of(shift, 0)
            cp.add(cp.start_at_end(shift, last_shift, -trans_time[types[i - 1]][types[i]]))
            last_shift = shift
            modes_of_mach.append(shift)
            sub_tasks = []
            for t in range(inst.J):
                if inst.tasks[t]['p'][v] <= inst.L:
                    prim = cp.interval_var(size=inst.tasks[t]['p'][v])
                    total_cost -= rewards[t] * cp.presence_of(prim)
                    prim.set_optional()
                    prim.set_start_min(inst.tasks[t]['r'])
                    prim.set_end_max(inst.tasks[t]['d'])
                    primitives[t].append(prim)
                    sub_tasks.append(prim)
                    cp.add(cp.start_before_start(shift, prim))
                    cp.add(cp.end_before_end(prim, shift))
            if len(sub_tasks) > 1:
                cp.add(cp.no_overlap(cp.sequence_var(sub_tasks)))
        term = cp.interval_var(end=inst.L)
        total_cost += inst.vertices[inst.TERM]['C'] * cp.length_of(term, 0)
        cp.add(cp.start_at_end(term, last_shift, -trans_time[types[len(types) - 2]][inst.TERM]))
        modes_of_mach.append(term)
        for t in range(inst.J):
            cp.add(sum([cp.presence_of(p) for p in primitives[t]]) <= 1)
        for eq in equivalences:
            cp.add(sum([cp.presence_of(p) for p in primitives[eq[0]]]) == sum(
                [cp.presence_of(p) for p in primitives[eq[1]]]))
        for mut in mutexes:
            cp.add(sum([cp.presence_of(p) for p in primitives[mut[0]]]) + sum(
                [cp.presence_of(p) for p in primitives[mut[1]]]) <= 1)
        #"""
        """
        # MOdel 3
        primitives = [[] for i in range(inst.J)]
        init = cp.interval_var(start=0)
        init.set_end_max(inst.L)
        total_cost = changeover_cost + inst.vertices[inst.INIT]['C'] * cp.length_of(init, 0) - self.l_0
        modes_of_mach = [init]  # serves only for retrieving starts and ends of modes
        last_shift = init
        for i in range(1, len(types) - 1):
            v = types[i]
            shift = cp.interval_var()
            shift.set_size_min(inst.vertices[v]['t_min'])
            shift.set_size_max(inst.vertices[v]['t_max'])
            shift.set_end_max(inst.L)
            total_cost += inst.vertices[v]['C'] * cp.length_of(shift, 0)
            cp.add(cp.start_at_end(shift, last_shift, -trans_time[types[i - 1]][types[i]]))
            last_shift = shift
            modes_of_mach.append(shift)
            for t in range(inst.J):
                if inst.tasks[t]['p'][v] <= inst.L:
                    prim = cp.interval_var(size=inst.tasks[t]['p'][v])
                    prim.set_optional()
                    prim.set_start_min(inst.tasks[t]['r'])
                    prim.set_end_max(inst.tasks[t]['d'])
                    primitives[t].append(prim)
                    cp.add(cp.start_before_start(shift, prim))
                    cp.add(cp.end_before_end(prim, shift))
        term = cp.interval_var(end=inst.L)
        total_cost += inst.vertices[inst.TERM]['C'] * cp.length_of(term, 0)
        cp.add(cp.start_at_end(term, last_shift, -trans_time[types[len(types) - 2]][inst.TERM]))
        modes_of_mach.append(term)
        masters = []
        for t in range(inst.J):
            new_master = cp.interval_var()
            new_master.set_optional()
            masters.append(new_master)
            total_cost -= rewards[t] * cp.presence_of(new_master)
            cp.add(cp.alternative(new_master, primitives[t]))
        cp.add(cp.no_overlap(cp.sequence_var(masters)))
        for eq in equivalences:
            cp.add(cp.presence_of(masters[eq[0]]) == cp.presence_of(masters[eq[1]]))
        for mut in mutexes:
            cp.add(cp.presence_of(masters[mut[0]]) + cp.presence_of(masters[mut[1]]) <= 1)
        """
        cp.add(cp.minimize(total_cost))

        # Set self.model
        self.model = cp
        self.primitives = primitives
        self.modes_of_mach = modes_of_mach
        self.types = types

    def print_sol(self):
        if self.sol is None:
            self.optimize()

        if self.sol:
            pattern = []
            tasks = []
            for t in range(self.instance.J):
                for pr in self.primitives[t]:
                    p = self.sol.get_var_solution(pr)
                    if p.is_present():
                        tasks.append({"task": t, "start": p.get_start(), "end": p.get_end()})
                        pattern.append(t)
                        break

            modes = []
            sched_cost = 0
            for t in range(len(self.modes_of_mach)):
                m = self.sol.get_var_solution(self.modes_of_mach[t])
                modes.append({"mode": self.types[t], "start": m.get_start(), "end": m.get_end()})
                sched_cost += self.instance.vertices[self.types[t]]['C'] * m.get_length()
            sched_cost += self.instance.get_trans_cost(self.profile_idx)  # add transition cost

            print("Solution status: " + self.sol.get_solve_status())
            print("Solve time: " + str(self.sol.get_solve_time()))
            print("Objective: " + str(self.sol.get_objective_values()[0]))
            print("Schedule cost: " + str(sched_cost))

            print(self.get_solution())
        else:
            print("No solution found.")

    def get_solution(self):
        s_i = {}
        s_j = {}
        p_i = {}
        p_j = {}

        modes = []  # Get all modes
        for t in range(len(self.modes_of_mach)):
            m = self.sol.get_var_solution(self.modes_of_mach[t])
            modes.append({"mode": self.types[t], "start": m.get_start(), "end": m.get_end()})

        # s_i, p_i
        for i in range(1,len(modes)-1,1):  # Skip starting and ending mode
            s_i[i-1] = modes[i]["start"]
            p_i[i-1] = modes[i]["end"] - modes[i]["start"]

        # s_j, y_ji
        for t in range(self.instance.J):
            for pr in self.primitives[t]:
                p = self.sol.get_var_solution(pr)
                if p.is_present():
                    s_j[t] = p.get_start()
                    p_j[t] = p.get_end() - p.get_start()
                    break

        return s_i, s_j, p_i, p_j, self.profile_idx

    def is_solved_opt(self):
        return self.sol is not None  # TODO: how to check if sol is optimal?

    def get_obj_val(self):
        assert self.sol is not None
        return self.sol.get_objective_values()[0]

    def get_pattern(self):
        pattern = []
        for t in range(self.instance.J):
            for pr in self.primitives[t]:
                p = self.sol.get_var_solution(pr)
                if p.is_present():
                    pattern.append(t)
                    break
        return pattern

    def get_ck(self):
        ck = 0
        for t in range(len(self.modes_of_mach)):
            m = self.sol.get_var_solution(self.modes_of_mach[t])
            ck += self.instance.vertices[self.types[t]]['C'] * m.get_length()
        ck += self.instance.get_trans_cost(self.profile_idx)  # add transition cost
        return ck


class SubproblemModel(ModelILP):
    """
    Model for subproblem (monoprocessor scheduling)
    """

    def __init__(self, instance, profile_idx, l_j, l_0, on_same, on_diff):
        """
        Initialize model
        :param instance: instance class with loaded parameters
        :param profile_idx: index of one specific profile for this model
        :param l_j: variables from dual task (with optimal solution) (note l_0 is constant)
        :param on_same: pairs of task indices, that should be scheduled on the same resource
        :param on_diff: pairs of tasks indices, that should be scheduled on different resources
        """
        self.instance = instance
        self.profile_idx = profile_idx
        self.profile = self.instance.profiles[profile_idx]
        self.l_j = l_j
        self.l_0 = l_0
        self.on_same = on_same
        self.on_diff = on_diff
        self.total_lazy = 0
        self.trans_cost = self.instance.get_trans_cost(self.profile_idx)
        self.timelimit = float("inf")
        # - variables
        self.y_ji = None  # variable (task j is assigned to interval i)
        self.s_i = None  # start time of interval i
        self.s_j = None  # start time of task j
        self.p_i = None  # processing time of interval i
        self.a_jj = None  # additional variables for precedences

        self.model = Model("subproblem-model")
        self.init_model()

    def init_model(self):

        def p_ji(j, i):
            """
            Return processing time of task j on interval i
            :param j: index of task
            :param i: index of interval
            :return:
            """
            return self.instance.tasks[j]["p"][self.profile[i]]

        # - set logging

        if params.PRINT_SUB:
            self.model.Params.LogToConsole = 1
        else:
            self.model.Params.LogToConsole = 0

        # - lazy constraints will be used
        self.model.Params.lazyConstraints = 1

        t_on = self.instance.V_distances[self.instance.INIT][self.profile[0]]["time"]
        t_off = self.instance.V_distances[self.profile[-1]][self.instance.TERM]["time"]
        I = len(self.profile)
        tasks = self.instance.tasks

        # - init variables
        self.y_ji = self.model.addVars(self.instance.J, I, vtype=GRB.BINARY, name="y_ji")

        self.s_i = self.model.addVars(I, vtype=GRB.INTEGER, lb=0.0, ub=self.instance.L, name="s_i")
        self.s_j = self.model.addVars(self.instance.J, vtype=GRB.INTEGER, lb=0.0, name="s_j")
        self.p_i = self.model.addVars(I, vtype=GRB.INTEGER, lb=0.0, name="p_i")

        #self.s_i = self.model.addVars(I, vtype=GRB.CONTINUOUS, lb=0.0, ub=self.instance.L, name="s_i")
        #self.s_j = self.model.addVars(self.instance.J, vtype=GRB.CONTINUOUS, lb=0.0, name="s_j")
        #self.p_i = self.model.addVars(I, vtype=GRB.CONTINUOUS, lb=0.0, name="p_i")


        self.a_jj = {}  # dictionary to save a_jj' variables
        for i, j in pairs_generator(self.instance.J):
            # Add variables (0,1), (0,2), ..
            self.a_jj[i, j] = self.model.addVar(vtype=GRB.BINARY, name=''.join(['a_', str(i), '-', str(j)]))

        # - add constraints
        # -> start times (modes should go one after another)
        self.model.addConstr(self.s_i[0] >= t_on)  # the first interval
        self.model.addConstr(self.s_i[I - 1] + self.p_i[I - 1] + t_off <= self.instance.L)  # the last interval
        for i in range(I - 1):  # all intervals between the first and the last
            t_trans = self.instance.V_distances[self.profile[i]][self.profile[i + 1]]["time"]
            self.model.addConstr(self.s_i[i + 1] == self.s_i[i] + self.p_i[i] + t_trans)

        # -> processing times between t_min, t_max
        for i in range(I):
            t_min = self.instance.vertices[self.profile[i]]["t_min"]
            t_max = self.instance.vertices[self.profile[i]]["t_max"]
            self.model.addConstr(t_min <= self.p_i[i] <= t_max)

        # -> release time of task
        self.model.addConstrs(tasks[j]["r"] <= self.s_j[j] for j in range(self.instance.J))
        # -> deadline of task
        M = self.instance.L * 2  # Note: this could be smaller
        self.model.addConstrs(self.s_j[j] + quicksum(self.y_ji[j, i] * p_ji(j, i) for i in range(I))
                              <= tasks[j]["d"] # + M * (1 - self.y_ji.sum(j, "*"))
                              for j in range(self.instance.J))

        # -> all tasks should be scheduled max. 1 times
        f_added = False
        for j in range(self.instance.J):
            sum_proc = LinExpr(0.0)
            sum_other = LinExpr(0.0)
            for i in range(I):
                if p_ji(j, i) < self.instance.L:
                    sum_proc.add(self.y_ji[j, i])
                else:
                    sum_other.add(self.y_ji[j, i])
                    f_added = True

            self.model.addConstr(sum_proc <= 1)  # task can be scheduled maximally once
            if f_added:  # task can not be scheduled on intervals, where it cannot be processed
                self.model.addConstr(sum_other == 0)

        # -> task starts / ends after start of the interval
        M1 = self.instance.L
        M2 = self.instance.L * 2
        for j in range(self.instance.J):
            for i in range(I):
                if p_ji(j, i) < self.instance.L:  # only for processing intervals
                    # start
                    self.model.addConstr(self.s_i[i] <= self.s_j[j] + M1 * (1 - self.y_ji[j, i]))
                    # end
                    self.model.addConstr(self.s_j[j] + p_ji(j, i)  # * self.y_ji[j, i]
                                         <= self.s_i[i] + self.p_i[i] + M2 * (1 - self.y_ji[j, i])
                                         )

        # -> pairs on_same [ sum_i y_ji == sum_i y_j'i]
        for pair in self.on_same:
            self.model.addConstr(self.y_ji.sum(pair[0], "*") == self.y_ji.sum(pair[1], "*"))

        # -> pairs on_diff
        for pair in self.on_diff:
            self.model.addConstr(self.y_ji.sum(pair[0], "*") + self.y_ji.sum(pair[1], "*") <= 1)

        # ADDITIONAL CONSTRAINTS TO SPEED UP SOLUTION
        self.model.addConstrs(quicksum(self.y_ji[j, i] * self.instance.tasks[j]["p"][self.profile[i]]
                                       for j in range(self.instance.J))
                              <= self.p_i[i] for i in range(len(self.profile)))

        instance = self.instance
        for j, j_ in pairs_generator(instance.J):
            # if release time of the j_ + minimal processing time + minimal processing time of j is greater than d(j)
            # then j must start before j_ (if they are on the same processor)
            # TODO : This does not work (probably - as subproblem seems to be unsolvable with this)
            #if instance.tasks[j_]["r"] + min(instance.tasks[j_]["p"]) + min(instance.tasks[j]["p"]) > instance.tasks[j]["d"]:
            #    self.model.addConstr(self.a_jj[j, j_] == 1)
            #if instance.tasks[j]["r"] + min(instance.tasks[j]["p"]) + min(instance.tasks[j_]["p"]) > instance.tasks[j_]["d"]:
            #    self.model.addConstr(self.a_jj[j, j_] == 0)

            # If deadline of task j is before release time of another task j_, then j < j_
            if instance.tasks[j]["d"] <= instance.tasks[j_]["r"]:
                self.model.addConstr(self.a_jj[j, j_] == 1)
            if instance.tasks[j_]["d"] <= instance.tasks[j]["r"]:
                self.model.addConstr(self.a_jj[j, j_] == 0)

        # - set objective
        self.set_objective()

    def optimize(self, time_limit, out_path):
        # Create dir if necessary
        if not os.path.exists(out_path):
            os.makedirs(out_path)

        # Set additional params
        self.model._data = self
        self.timelimit = time_limit

        self.set_timelimit(time_limit)
        self.write_model(out_path)
        self.model.optimize(subproblem_callback)
        #self.write_solution(out_path)

    def set_objective(self):
        # - add objective
        expr = LinExpr(0.0)
        expr.add(self.trans_cost)  # C_trans
        expr.add(-self.l_0)  # - l_0*

        for i in range(len(self.profile)):
            expr.add(self.p_i[i] * self.instance.vertices[self.profile[i]]["C"])  # + p_i C_i

            for j in range(self.instance.J):
                if self.instance.tasks[j]["p"][self.profile[i]] < self.instance.L:
                    # - y_ji * l_j*
                    expr.add(- self.y_ji[j, i] * self.l_j[j])

        self.model.setObjective(expr, sense=GRB.MINIMIZE)

    def reset_objective(self, l_j, l_0):
        # Change dual variables
        self.l_j = l_j
        self.l_0 = l_0
        # Reset objective
        self.set_objective()

    def is_solved_opt(self):
        return (self.model.status == GRB.Status.OPTIMAL
                or self.model.status == GRB.Status.INTERRUPTED
                or self.model.status == GRB.Status.SUBOPTIMAL)

    def get_solution(self):
        s_i = self.model.getAttr("x", self.s_i)
        s_j = self.model.getAttr("x", self.s_j)
        p_i = self.model.getAttr("x", self.p_i)
        y_ji = self.model.getAttr("x", self.y_ji)
        p_j = {}

        for j in range(self.instance.J):
            t_int = -1
            for i in range(len(self.profile)):
                if y_ji[j, i] > 0.5:
                    t_int = i
                    break
            p_j[j] = self.instance.tasks[j]["p"][self.profile[t_int]]

        return s_i, s_j, p_i, p_j, self.profile_idx



    def get_obj_val(self):
        return self.model.ObjVal

    def get_pattern(self):
        if self.model.status != GRB.Status.OPTIMAL and self.model.status != GRB.Status.SUBOPTIMAL and self.model.status != GRB.Status.INTERRUPTED:
            print("[Get pattern:] Optimal solution was not found")
            return []

        pattern = []

        for j in range(self.instance.J):
            for i in range(len(self.profile)):
                if self.y_ji[j, i].X > 0.5:
                    pattern.append(j)

        return pattern

    def get_ck(self):
        price = self.instance.get_trans_cost(self.profile_idx)
        # Add costs for processing times in intervals
        for i in range(len(self.profile)):
            price += self.p_i[i].X * self.instance.vertices[self.profile[i]]["C"]

        return price


class MonoprocessorModel(ModelILP):
    """
    Model for monoprocessor scheduling
    """

    def __init__(self, instance, profile_idx, pattern, log_to_console=False):
        """
        Initialize model
        :param instance: instance class with loaded parameters
        :param profile_idx: index of one specific profile for this model
        :param pattern: list of task indices to be scheduled
        """
        self.instance = instance
        self.profile_idx = profile_idx
        self.profile = self.instance.profiles[profile_idx]
        self.pattern = pattern
        self.total_lazy = 0
        self.log_to_console = log_to_console
        self.timelimit = float("inf")

        # - variables
        self.y_ji = None  # variable (task j is assigned to interval i)
        self.s_i = None  # start time of interval i
        self.s_j = None  # start time of task j
        self.p_i = None  # processing time of interval i
        self.a_jj = None  # additional variables for precedences

        self.model = Model("monoprocessor-model")
        self.init_model()

    def init_model(self):
        def p_ji(j, i):
            """
            Return processing time of task j on interval i
            :param j: index of task
            :param i: index of interval
            :return:
            """
            return self.instance.tasks[j]["p"][self.profile[i]]

        # - set logging
        if not self.log_to_console:
            self.model.Params.LogToConsole = 0

        # - lazy constraints will be used
        self.model.Params.lazyConstraints = 1

        t_on = self.instance.V_distances[self.instance.INIT][self.profile[0]]["time"]
        t_off = self.instance.V_distances[self.profile[-1]][self.instance.TERM]["time"]
        I = len(self.profile)
        tasks = self.instance.tasks

        # - init variables
        self.y_ji = self.model.addVars(self.instance.J, I, vtype=GRB.BINARY, name="y_ji")
        self.s_i = self.model.addVars(I, vtype=GRB.CONTINUOUS, lb=0.0, ub=self.instance.L, name="s_i")
        self.s_j = self.model.addVars(self.instance.J, vtype=GRB.CONTINUOUS, lb=0.0, name="s_j")
        self.p_i = self.model.addVars(I, vtype=GRB.CONTINUOUS, lb=0.0, name="p_i")
        self.a_jj = {}  # dictionary to save a_jj' variables
        for i, j in pairs_generator(self.instance.J):
            # Add variables (0,1), (0,2), ..
            self.a_jj[i, j] = self.model.addVar(vtype=GRB.BINARY, name=''.join(['a_', str(i), '-', str(j)]))

        # - add constraints
        # -> start times (modes should go one after another)
        self.model.addConstr(self.s_i[0] >= t_on)  # the first interval
        self.model.addConstr(self.s_i[I - 1] + self.p_i[I - 1] + t_off <= self.instance.L)  # the last interval
        for i in range(I - 1):  # all intervals between the first and the last
            t_trans = self.instance.V_distances[self.profile[i]][self.profile[i + 1]]["time"]
            self.model.addConstr(self.s_i[i + 1] == self.s_i[i] + self.p_i[i] + t_trans)

        # -> processing times between t_min, t_max
        for i in range(I):
            t_min = self.instance.vertices[self.profile[i]]["t_min"]
            t_max = self.instance.vertices[self.profile[i]]["t_max"]
            self.model.addConstr(t_min <= self.p_i[i] <= t_max)

        M = self.instance.L * 2  # Note: this could be smaller
        # -> release time of task
        self.model.addConstrs(tasks[j]["r"] <= self.s_j[j] for j in range(self.instance.J))  # Note: should there ne big M?
        # -> deadline of task
        self.model.addConstrs(self.s_j[j] + quicksum(self.y_ji[j, i] * p_ji(j, i) for i in range(I))
                              <= tasks[j]["d"]  # + M * (1 - self.y_ji.sum(j, "*"))
                              # Note : it may be possible to comment out big M here
                              for j in range(self.instance.J))

        # -> scheduling tasks
        for j in range(self.instance.J):
            f_added = False
            sum_proc = LinExpr(0.0)
            sum_other = LinExpr(0.0)
            for i in range(I):
                if p_ji(j, i) <= self.instance.L:
                    sum_proc.add(self.y_ji[j, i])
                else:
                    sum_other.add(self.y_ji[j, i])
                    f_added = True

            if j in self.pattern:
                # -> all tasks in pattern should be scheduled
                self.model.addConstr(sum_proc == 1)
                if f_added:
                    self.model.addConstr(sum_other == 0)
            else:
                # -> all other tasks should not be scheduled
                self.model.addConstr(sum_proc + sum_other == 0)

        # -> task starts / ends after start of the interval
        M1 = self.instance.L
        M2 = self.instance.L * 2
        for j in self.pattern:
            for i in range(I):
                if p_ji(j, i) < self.instance.L:  # only for processing intervals
                    # start
                    self.model.addConstr(self.s_i[i] <= self.s_j[j] + M1 * (1 - self.y_ji[j, i]))
                    # end
                    self.model.addConstr(self.s_j[j] + self.y_ji[j, i] * p_ji(j, i)
                                         <= self.s_i[i] + self.p_i[i] + M2 * (1 - self.y_ji[j, i])
                                         )

        # ADDITIONAL CONSTRAINTS TO SPEED UP SOLUTION
        self.model.addConstrs(quicksum(self.y_ji[j, i] * self.instance.tasks[j]["p"][self.profile[i]]
                                       for j in range(self.instance.J))
                              <= self.p_i[i] for i in range(len(self.profile)))

        # - set objective
        self.set_objective()

    def set_objective(self):
        # - add objective
        expr = LinExpr(0.0)
        expr.add(self.instance.get_trans_cost(self.profile_idx))  # C_trans

        for i in range(len(self.profile)):
            expr.add(self.p_i[i] * self.instance.vertices[self.profile[i]]["C"])  # + p_i C_i

        self.model.setObjective(expr, sense=GRB.MINIMIZE)

    def optimize(self, time_limit, out_path):
        # Create dir if necessary
        if not os.path.exists(out_path):
            os.makedirs(out_path)

        # Set additional params
        self.model._data = self
        self.timelimit = time_limit

        self.set_timelimit(time_limit)
        self.write_model(out_path)
        self.model.optimize(subproblem_callback)
        self.write_solution(out_path)

    def get_obj_val(self):
        return self.model.ObjVal

    def print(self):
        if self.model.status != GRB.Status.OPTIMAL:
            print("Optimal solution was not found")
            return

        print("MONOPROCESSOR MODEL SOLUTION")
        print("  PROFILE")
        print(self.profile)
        print("  PATTERN")
        print(self.pattern)
        print("  MODES")
        for i in range(len(self.profile)):
            print("  - s_i, p_i", self.s_i[i].X, self.p_i[i].X)
        print("  TASKS")
        for j in range(self.instance.J):
            print("j:", j, "  s_j =", self.s_j[j].X)
            print("    ", end="")
            for i in range(len(self.profile)):
                print(self.y_ji[j,i].X, " [", self.instance.tasks[j]["p"][self.profile[i]], "]", sep="", end=", ")
            print()


def pairs_generator(rng):
    """
    Creates Generator of ordered pairs
    :param rng : Range of the interval, e.g. 3 means pairs (0,1),(0,2),(1,2)
    :return:
    """
    return ((i, j) for i in range(rng) for j in range(rng) if i < j)


def pattern_pairs_generator(pattern):
    """
    Creates Generator of ordered pairs
    :param pattern : subset of tasks (indices)
    :return:
    """
    return ((i, j) for i in pattern for j in pattern if i < j)


def print_model_status(model, sol_path):
    print("[Optimize status:]", model.status, end=" ")
    # Print some info
    if model.status == GRB.Status.INFEASIBLE:
        print("- model was proven infeasible")
    elif model.status == GRB.Status.TIME_LIMIT:
        print("- time limit was reached.", end=" ")
        if model.SolCount > 0:
            print("- suboptimal solution was found.")
        else:
            print("")
    elif model.status == GRB.Status.OPTIMAL:
        print("- optimal solution was found")
        model.write(sol_path)
    else:
        print("")


def subproblem_callback(model, where):
    """
    My callback function to set lazy constraints
    :param model:
    :param where:
    :type model : Model
    :return:
    """
    if where == GRB.Callback.MIPSOL:
        EPS = 0.0001
        generated = 0

        m = model._data
        instance = m.instance
        I = len(m.profile)
        M = instance.L * 2

        # variables
        y_ji = m.y_ji
        s_j = m.s_j
        a_jj = m.a_jj

        # values of vars.
        y_ji_val = model.cbGetSolution(y_ji)
        s_j_val = model.cbGetSolution(s_j)
        a_jj_val = model.cbGetSolution(a_jj)

        for j, j_ in pairs_generator(instance.J):
            for i in range(I):
                mode = m.profile[i]
                # If both tasks are on the same interval
                if y_ji_val[j, i] > 0.5 and y_ji_val[j_, i] > 0.5:
                    # If tasks does not respect ordering a_jj, generate new constraints
                    # print("j,j_: %d %d, s_j=%f, p_j=%f, s_j_=%f (a_jj_: %f)" %
                    #      (j, j_, s_j_val[j], instance.tasks[j]["p"][mode], s_j_val[j_], a_jj_val[j, j_]))
                    if a_jj_val[j, j_] > 0.5 and s_j_val[j] + instance.tasks[j]["p"][mode] > s_j_val[j_] + EPS:
                        # print("I)")
                        model.cbLazy(s_j[j] + y_ji[j, i] * instance.tasks[j]["p"][mode] <= s_j[j_]
                                     + M * (1 - y_ji[j, i])
                                     + M * (1 - y_ji[j_, i])
                                     + M * (1 - a_jj[j, j_]))
                        generated += 1

                    if a_jj_val[j, j_] < 0.5 and s_j_val[j_] + instance.tasks[j_]["p"][mode] > s_j_val[j] + EPS:
                        # print("II)", s_j_val[j_], y_ji_val[j_, i], instance.tasks[j_]["p"][mode], s_j_val[j])
                        model.cbLazy(s_j[j_] + y_ji[j_, i] * instance.tasks[j_]["p"][mode] <= s_j[j]
                                     + M * (1 - y_ji[j, i])
                                     + M * (1 - y_ji[j_, i])
                                     + M * a_jj[j, j_])
                        generated += 1

        m.total_lazy += generated
        if params.PRINT_SUB:
            print("[Callback:] Generated new constraints: ", generated, " (", m.total_lazy, " total)", sep="")

        # NOTE: uncomment this if optimization should terminate after finding the first negative solution
        #if generated == 0 and model.cbGet(GRB.Callback.MIPSOL_OBJ) < 0 - EPS:
           #print("[Callback:] GENERATED NEGATIVE OBJECTIVE, TERMINATING OPTIMIZATION.")
           #model.terminate()