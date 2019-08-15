class MonoprocessorSolution:
    """
    Store solution of the pricing proglem
     which consists of
      s_i
      s_j
      p_i
      p_j
      profile_idx
    """
    def __init__(self):
        """
        Initialize solution variables
        """
        self.s_i = None
        self.s_j = None
        self.p_i = None
        self.p_j = None
        self.profile_idx = None

    def init_directly(self, s_i, s_j, p_i, p_j, profile_idx):
        """
        Initialize this monoprocessor scheduling solution directly
        :param s_i:
        :param s_j:
        :param p_i:
        :param y_ij:
        :param profile_idx:
        :return:
        """
        self.s_i = s_i
        self.s_j = s_j
        self.p_i = p_i
        self.p_j = p_j
        self.profile_idx = profile_idx
        return self

    def init_from_heuristics(self, inst, s_i, s_j, p_i, y_ji, profile_idx):
        """
        Initialize this monoprocessor scheduling solution directly
        :param s_i:
        :param s_j:
        :param p_i:
        :param y_ij:
        :param profile_idx:
        :return:
        """
        self.s_i = s_i
        self.s_j = s_j
        self.p_i = p_i
        self.profile_idx = profile_idx

        p_j = {}
        for j in range(inst.J):
            t_int = -1
            for i in range(len(inst.profiles[profile_idx])):
                if y_ji[j,i] > 0.5:
                    t_int = i
                    break
            p_j[j] = inst.tasks[j]["p"][inst.profiles[profile_idx][t_int]]
        self.p_j = p_j

        return self

    def get_s_i(self, i):
        return self.s_i[i]

    def get_s_j(self, j):
        return self.s_j[j]

    def get_p_i(self, i):
        return self.p_i[i]

    def get_p_j(self, j):
        return self.p_j[j]

    def print(self):
        print("s_i:", self.s_i)
        print("p_i:", self.p_i)
        print("s_j:", self.s_j)
        print("p_j:", self.p_j)


class PatternWrapper:
    """
    Container for solutions of the subproblems
    containing:
        patterns (subsets of tasks)
        c_k      (pattern costs)
        solutions (subproblem model solutions -- values of important variables (not including a_jj_))
    """
    def __init__(self):
        """
        Initialize this solution
        """
        self.patterns = []
        self.c_k = []
        self.solutions = []
        self.reused = False

    def init_given_same(self, pair, prev_sol):
        """
        Initialize the new pattern wrapper from previous one, given pair of tasks which should be in each pattern
        :param pair: pair of task indices which should be in the pattern
        :param prev_sol: pattern wrapper from the previous iteration
        :type prev_sol: PatternWrapper
        :return:
        """
        for i in range(len(prev_sol.patterns)):
            if set(pair).issubset(prev_sol.patterns[i]):  # check if both elements of pair are in patterns[i]
                self.add_pattern_again(prev_sol.patterns[i], prev_sol.c_k[i], prev_sol.solutions[i])
        if prev_sol.reused:
            self.reused = True

    def init_given_diff(self, pair, prev_sol):
        """
        Initialize the new pattern wrapper frim the previous one, given pair of tasks which should not be together
        :param pair: pair of tasks which can not be in the same pattern
        :param prev_sol: pattern wrapper from the previous iteration
        :type prev_sol: PatternWrapper
        :return:
        """
        for i in range(len(prev_sol.patterns)):
            if not set(pair).issubset(prev_sol.patterns[i]):  # check if both elements of pair are not in patterns[i]
                self.add_pattern_again(prev_sol.patterns[i], prev_sol.c_k[i], prev_sol.solutions[i])
        if prev_sol.reused:
            self.reused = True

    def add_pattern(self, subproblem_model):
        """
        Add pattern given by the solution of subproblem_model
        :param subproblem_model:
        :type subproblem_model: SubproblemModel
        :return: None
        """
        self.patterns.append(tuple(subproblem_model.get_pattern()))
        self.c_k.append(subproblem_model.get_ck())
        self.solutions.append(MonoprocessorSolution().init_from_subproblem(subproblem_model))

    def add_pattern_again(self, pattern, c_k, solution):
        """
        Add pattern (plus cost and solution) directly
        (used for reinitialization after branching)
        :param pattern:
        :param c_k:
        :param solution:
        :return:
        """
        self.patterns.append(tuple(pattern))
        self.c_k.append(c_k)
        self.solutions.append(solution)

    def add_pattern_without(self, pattern, c_k, task_id, sol):
        """
        Add pattern directly but remove task_id
        :param pattern:
        :param c_k:
        :param task_id:
        :param sol: solution for pattern
        :return:
        """
        new_pattern = [i for i in pattern if i != task_id]
        if len(new_pattern) > 0:
            self.patterns.append(tuple(new_pattern))
            self.c_k.append(c_k)  # This cost is overestimated, should the pattern be in the optimal solution
                                  # - it will be generated again with the optimal cost
            # TODO : is this correct?
            #self.solutions.append(None)  # This pattern is dummy (should never be used in the opt. solution -> see c_k
            self.solutions.append(sol)
            self.reused = True



    def sum_of_costs(self):
        """ Return sum of all c_k in this wrapper."""
        return sum(self.c_k)

    def contains_better(self,p,ck):
        """
        Check if this pattern wrapper contains the same pattern with better cost
        :param p: pattern
        :param ck:
        :return:
        :type p: tuple
        """
        for i in range(len(self.patterns)):
            if self.patterns[i] == p and self.c_k[i] <= ck:
                return True
        return False

    def print(self):
        """
        Print patterns
        :return: None
        """
        for i in range(len(self.patterns)):
            if self.reused:
                print(i, ":", " pattern ", self.patterns[i],
                      " c_k ", round(self.c_k[i], 2),
                      " profile_idx [solution reused]", sep="")
            else:
                print(i, ":", " pattern ", self.patterns[i],
                      " c_k ", round(self.c_k[i],2),
                      " profile_idx ", self.solutions[i].profile_idx, sep="")
                self.solutions[i].print()