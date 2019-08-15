# ======================================================================================================================
# Multiple solving methods
# ======================================================================================================================
from model import *
from gurobipy import *
from instance import Instance
from solution import *
from complex_model import RelativeModel
from time import gmtime, strftime
import visualization
import time
import params
import json


# Global variables
path_out_glob = ""
path_log_glob = ""
EPS = 0.0001
glob_n_nodes = 0
glob_n_patterns = 0
glob_best_obj = float("inf")
glob_start_time = 0

def solve(path_in, path_out, path_log, time_limit, debug=False):
    """
    Solve instance using the branch and price  algorithm
    :param path_in: path to the datafile with instance data
    :param path_out: path where results should be saved
    :param path_log: path to the log file
    :param time_limit: maximal number of seconds limiting Gurobi
    :return: None
    """
    global path_out_glob
    global path_log_glob
    global EPS

    path_out_glob = path_out
    path_log_glob = path_log
    EPS= 0.0001

    print("[Instance:] Creating instance:")
    inst = Instance(path_in, debug)

    if debug:
        print("  - DATA")
        print(inst)

    print("[Instance:] Making graph.")
    visualization.draw_graph(inst, "".join([path_out, "/graph"]))

    print("[Instance:] Visualizing heuristic solution.")
    if inst.heuristic is not None:
        visualization.visualize_heuristics(inst, "".join([path_out,"/gantt_heur"]))  # TODO

    print("[Full model:] Solving instance.")
    solve_full_model(inst)

    print("[B&P:] Solving instance.")
    solution = branch_and_price_start(inst)

    for i in range(len(solution.patterns)):
        print("Pattern:", solution.patterns[i])
        solution.solutions[i].print()

    print("[Visualization:] Visualizing solution.")
    visualization.visualize_solution(solution, 1200, inst, path_out_glob)

    #visualization.draw_gantt_from_patterns(inst, solution, "".join([path_out,"/gantt_sol"]))

    #print("[Full model:] Solving the full model.")
    #full_model = RelativeModel(inst, path_out_glob, [0])
    #full_model.optimize(float("inf"), path_log_glob)
    #full_model.print_sol()


def solve_tests():
    def solve_all_tests(method_name, method_short, use_CP, is_bp):
        print("Solving test instances with: " + method_name)
        f_log.write("Solving test instances with: " + method_name + "\n")
        f_tex.write("Solving test instances with: " + method_name + "\n")
        params.BB_SUBPROBLEM_USE_CP = use_CP  # If CP is used in subproblem
        results = {}  # Dict with results
        path_imgs_cur = path_out_glob + method_short
        if not os.path.exists(path_imgs_cur):
            os.makedirs(path_imgs_cur)

        for J in params.GENERATOR_J:
            for N in params.GENERATOR_N:
                res_conf = []  # Results for this configuration
                for inst in params.GENERATOR_n_inst:
                    path_in = "".join(["data_in/tests/", str(N), "_", str(J), "_", str(inst), ".txt"])
                    instance = Instance(path_in)
                    # Solution
                    if is_bp:
                        t_s = time.time()
                        solution = branch_and_price_start(instance)
                        t_e = time.time()

                        if solution == "timeout":
                            log_str = ("[%s]: N = %d, J = %d, id = %d: Obj = inf, time = %f, nodes = %d, patterns = %d\n"
                                       % (method_short, N, J, inst, t_e - t_s, glob_n_nodes, glob_n_patterns))
                            print(log_str, end="")
                            f_log.write(log_str)
                            f_tex.write("%d, %d, %d, inf, %.2f, %d, %d\n"
                                        % (N, J, inst, t_e - t_s, glob_n_nodes, glob_n_patterns))

                            res_conf.append((float("inf"), t_e - t_s, glob_n_nodes, glob_n_patterns))
                        else:
                            log_str = ("[%s]: N = %d, J = %d, id = %d: Obj = %f, time = %f, nodes = %d, patterns = %d\n"
                            % (method_short, N, J, inst, solution.sum_of_costs(), t_e - t_s, glob_n_nodes, glob_n_patterns))
                            print(log_str, end="")
                            f_log.write(log_str)
                            f_tex.write("%d, %d, %d, %f, %.2f, %d, %d\n"
                                        % (N, J, inst, solution.sum_of_costs(), t_e - t_s, glob_n_nodes, glob_n_patterns))

                            res_conf.append((solution.sum_of_costs(), t_e - t_s, glob_n_nodes, glob_n_patterns))

                            # Visualize result
                            visualization.visualize_solution(solution, params.VIZ_GANTT_WIDTH, instance,
                                                             path_imgs_cur + "/%d_%d_%d.svg" % (N, J, inst))
                    else:
                        t_s = time.time()
                        obj, n_vars, n_constr, full_model = solve_full_model(instance, timeout=params.TIMEOUT)
                        t_e = time.time()

                        print("["+method_short+"]: N = %d, J = %d, id = %d: Obj = %s, time = %f, Vars = %d, Constr = %d"
                              % (N, J, inst, str(obj), t_e - t_s, n_vars, n_constr))

                        f_log.write("["+method_short+"]: N = %d, J = %d, id = %d: Obj = %s, time = %f, Vars = %d, Constr = %d\n"
                              % (N, J, inst, obj, t_e - t_s, n_vars, n_constr))
                        f_tex.write("%d, %d, %d, %f, %.2f, %d, %d\n" % (N, J, inst, obj, t_e - t_s, n_vars, n_constr))

                        res_conf.append((obj, t_e - t_s, n_vars, n_constr))

                        if obj < float("inf"):  # i.e. no timeout
                            # Visualize solution
                            visualization.visualize_complex_solution(full_model, params.VIZ_GANTT_WIDTH, instance,
                                                                     path_imgs_cur + "/%d_%d_%d.svg" % (N, J, inst))
                # End of random instances
                f_log.flush()
                f_tex.flush()

                results[str((J, N, method_short))] = res_conf
        # End of solution generation

        return results

    def display_solutions():
        f_out = open(path_out_glob+"solutions.html","w")

        f_out.write('<!DOCTYPE html>\n')
        f_out.write('<html lang="en">\n')
        f_out.write('<head><meta charset="UTF-8" /><title>Results</title></head>\n')
        f_out.write('<body>\n')
        f_out.write('<table>\n')

        f_out.write('<tr>')
        f_out.write('<th>BP - ILP</th>')
        f_out.write('<th>BP - CP</th>')
        f_out.write('<th>ILP - full</th>')
        f_out.write('</tr>\n')

        for J in params.GENERATOR_J:
            for N in params.GENERATOR_N:
                for inst in params.GENERATOR_n_inst:
                    cur_im = str(N) + '_' + str(J) + '_' + str(inst)
                    f_out.write('<tr><td>N_J_id = ' + cur_im + '</td></tr>\n')
                    f_out.write('<tr>\n')
                    f_out.write('<td><img src="./bp_ilp/'+ cur_im + '.svg" / ></td>\n')
                    f_out.write('<td><img src="./bp_cp/' + cur_im + '.svg" / ></td>\n')
                    f_out.write('<td><img src="./ilp_full/' + cur_im + '.svg" / ></td>\n')
                    f_out.write('</tr>\n')

        f_out.write('</table>')
        f_out.write('</body>')
        f_out.write('</html>')



        f_out.close()

    # SOLVE TESTS ------------------------------------------------------------------------------------------------------
    global path_out_glob, path_log_glob

    test_time = strftime("%Y_%m_%d-%H_%M_%S", gmtime())
    path_out_glob = "data_out/tests/"+test_time+"/"
    path_log_glob = "data_out/tests/"+test_time+"/log.txt"

    if not os.path.exists(path_out_glob):
        os.makedirs(path_out_glob)


    # Different solvers:
    f_log = open(path_log_glob, "w")
    f_tex = open(path_out_glob + "data.csv", "w")
    solve_all_tests("Branch and price ILP", "bp_ilp", False, True)
    solve_all_tests("Branch and price CP", "bp_cp", True, True)
    #solve_all_tests("ILP full model", "ilp_full", False, False)
    f_log.close()
    f_tex.close()
    # Display solutions
    display_solutions()


def solve_full_model(inst, timeout=float("inf")):
    """ Solves complex ILP model for given instance, returns (objective, n_vars, n_constrs)"""
    full_model = RelativeModel(inst, path_out_glob, params.FULL_MODEL_I)
    full_model.optimize(timeout, "data_out/temp/")

    if full_model.model.status == GRB.Status.TIME_LIMIT:
        return float("inf"), full_model.model.NumVars, full_model.model.NumConstrs, full_model
    else:
        return full_model.get_objective(), full_model.model.NumVars, full_model.model.NumConstrs, full_model


def branch_and_price_start(instance):
    global glob_n_nodes, glob_n_patterns, glob_best_obj, glob_start_time
    glob_n_nodes, glob_n_patterns, glob_best_obj, glob_start_time = 0, 0, float("inf"), time.time()

    t_bb_start = time.time()
    on_same = []  # pairs scheduled on the same resource
    on_diff = []  # pairs scheduled on different resources
    p_wrap = PatternWrapper()  # container of patterns and their costs

    # Add heuristic solution to pattern wrapper
    if instance.heuristic is not None:
        h_p = instance.heuristic.patterns
        h_c = instance.heuristic.patterns_costs
        h_s = instance.heuristic.patterns_solutions
        for i in range(len(h_p)):
            p_wrap.add_pattern_again(h_p[i], h_c[i], h_s[i])

    # Add dummy solutions (single task) to pattern wrapper
    for j in range(instance.J):
        dummy_m = MonoprocessorModel(instance,0,[j])   # TODO: trivial solution - no need for monoprocessor opt. model
        dummy_m.optimize(float("inf"), path_out_glob)  # Find the solution for a single task
        dummy_s = MonoprocessorSolution().init_from_heuristics(instance,
                                                               dummy_m.model.getAttr("x", dummy_m.s_i),
                                                               dummy_m.model.getAttr("x", dummy_m.s_j),
                                                               dummy_m.model.getAttr("x", dummy_m.p_i),
                                                               dummy_m.model.getAttr("x", dummy_m.y_ji),
                                                               0)  # TODO: inst.STARTING_PROFILE
        p_wrap.add_pattern_again([j],dummy_m.get_obj_val(), dummy_s)

    # Call branch and price on the root
    solution = branch_and_price(instance, on_same, on_diff, p_wrap)

    # Check if timeout was reached
    if time.time() - glob_start_time > params.TIMEOUT:
        if params.PRINT_BB:
            print("[B&P:] TIMEOUT.")
        return "timeout"

    # Print solution
    if params.PRINT_BB:
        print("[B&P:] Done. Solution:")
        solution.print()
        print("[B&P:] number of nodes: ", glob_n_nodes, ",\n       ",
              "number of generated patterns (dual): ", glob_n_patterns, ",\n       ",
              "time: ", round(time.time() - t_bb_start, 2), " s.", sep="")

    return solution


def branch_and_price(instance, on_same, on_diff, p_wrap):
    """
    Branch and price algorithm
    :param instance: problem instance
    :param on_same: pairs which should be on the same resource
    :param on_diff: pairs which should be on different resources
    :param p_wrap:
    :type p_wrap: PatternWrapper
    :return:
    """
    def solve_subproblem(l_j, l_0):
        """Solve subproblem for all profiles and return the best solution"""
        best_obj = float("inf")
        best_pattern = []
        best_ck = 0.0
        best_model = None

        for i in range(len(instance.profiles)):
            if params.BB_SUBPROBLEM_USE_CP:
                subproblem_models[i] = SubproblemModelCP(instance, i, l_j, l_0, on_same, on_diff)
                subproblem = subproblem_models[i]
            else:  # for ILP model - just reset objective with new l_0, l_j
                subproblem = subproblem_models[i]
                subproblem.reset_objective(l_j, l_0)

            # TODO: CP model seems to need some time to init, which it does not count
            subproblem.optimize(get_timeout(glob_start_time, time.time()), path_out_glob)

            # Check timeout
            if time.time()-glob_start_time > params.TIMEOUT:
                return None, None, None, None

            if subproblem.is_solved_opt():
                if subproblem.get_obj_val() < best_obj:
                    best_obj = subproblem.get_obj_val()
                    best_pattern = subproblem.get_pattern()
                    best_ck = subproblem.get_ck()
                    best_model = subproblem_models[i]

                    # COMPARE WITH MONOPROCESSOR SOLUTION
                    if params.TEST_MONOPROCESSOR:
                        mp = MonoprocessorModel(instance, i, subproblem.get_pattern())
                        mp.optimize(float("inf"), path_out_glob)

                        if abs(mp.get_obj_val() - subproblem.get_ck()) > 1:
                            print("Mismatch: i=", i, subproblem.get_ck(), " <- sub ; mono -> ", mp.get_obj_val(), "l:", l_j, l_0)

        assert best_model is not None, "Solution to the subproblem should be found, but it was not."
        return best_obj, best_pattern, best_ck, best_model.profile_idx

    def get_solution(master_model, p_wrap):
        """Extract solution from master model which solution is int.
        :type master_model: MasterModel
        :type p_wrap: PatternWrapper"""
        p_wrap_sol = PatternWrapper()  # Solution wrapper
        for i in range(len(p_wrap.patterns)):
            if master_model.t_k[i].X > 0.5:  # Solution is integral - pattern i was chosen
                p_wrap_sol.add_pattern_again(p_wrap.patterns[i], p_wrap.c_k[i], p_wrap.solutions[i])

        return p_wrap_sol

    def pair_not_inside(p, lst):
        """Check is pair is not in on_same list or on_diff list (list of pairs)"""
        for pair in lst:
            if p[0] == pair[0] and p[1] == pair[1]:
                return False
        return True

    def pair_both_in_lst(p, lst):
        """Check if both elements of pair are in the list lst"""
        return True if p[0] in lst and p[1] in lst else False

    def pair_none_in_lst(p, lst):
        """Check if none element of pair is in the list lst"""
        return True if p[0] not in lst and p[1] not in lst else False

    def get_pair(master_model, p_wrap, on_same, on_diff):
        def pair_heur(e):
            id1, id2 = e
            r1, r2 = instance.tasks[id1]["r"], instance.tasks[id2]["r"]
            d1, d2 = instance.tasks[id1]["d"], instance.tasks[id2]["d"]

            if r1 <= r2:
                return min(d2 - r2, max(d1 - r2, 0))
            else:
                return min(d1 - r1, max(d2 - r1, 0))


        """Get a new branching pair"""
        indices = master_model.get_selected_indices()
        all_tasks = set()

        for idx in indices:
            all_tasks.update(p_wrap.patterns[idx])

        all_tasks = list(all_tasks)
        pairs = ((i, j) for i in all_tasks for j in all_tasks
                 if i < j and pair_not_inside((i,j), on_same) and pair_not_inside((i,j), on_diff))  # Generator of valid

        if params.BB_USE_GET_PAIR_HEUR:
            pairs = list(pairs)
            pairs.sort(key=lambda x : pair_heur(x), reverse=True)  # Biggest overlap first

        for p in pairs:
            return p  # Return the first pair which is valid (from possibly sorted pairs)

        return None

    def generate_new_patterns(p_wrap, pair):
        p_wrap_same = PatternWrapper()
        p_wrap_diff = PatternWrapper()

        for i in range(len(p_wrap.patterns)):
            both_inside = pair_both_in_lst(pair, p_wrap.patterns[i])
            none_inside = pair_none_in_lst(pair, p_wrap.patterns[i])
            if none_inside:
                # None of two items is in the list - add to both branches
                p_wrap_same.add_pattern_again(p_wrap.patterns[i], p_wrap.c_k[i], p_wrap.solutions[i])
                p_wrap_diff.add_pattern_again(p_wrap.patterns[i], p_wrap.c_k[i], p_wrap.solutions[i])
            if both_inside:
                # Both items are in the list - add to on_same
                p_wrap_same.add_pattern_again(p_wrap.patterns[i], p_wrap.c_k[i], p_wrap.solutions[i])
                # Delete one of the two and add it to on_diff - generating new pattern with overestimated cost
                # - both were inside - if we delete one, we obtain new pattern containing single of the two only
                p_wrap_diff.add_pattern_without(p_wrap.patterns[i], p_wrap.c_k[i], pair[0], p_wrap.solutions[i])
                p_wrap_diff.add_pattern_without(p_wrap.patterns[i], p_wrap.c_k[i], pair[1], p_wrap.solutions[i])
            if not both_inside and not none_inside:
                # Only one is in the list - add to on_diff
                p_wrap_diff.add_pattern_again(p_wrap.patterns[i], p_wrap.c_k[i], p_wrap.solutions[i])
                # Generate again new patterns with overestimated cost (one was inside
                # - deleting it we can obtain pattern which does not contain any of the two items
                # - it is not needed to add it to on_diff as there is already complete pattern
                if pair[0] in p_wrap.patterns[i]:
                    p_wrap_same.add_pattern_without(p_wrap.patterns[i], p_wrap.c_k[i], pair[0], p_wrap.solutions[i])
                    # p_wrap_diff.add_pattern_without(p_wrap.patterns[i], p_wrap.c_k[i], pair[0]) # no need to add it
                if pair[1] in p_wrap.patterns[i]:
                    p_wrap_same.add_pattern_without(p_wrap.patterns[i], p_wrap.c_k[i], pair[1], p_wrap.solutions[i])
                    # p_wrap_diff.add_pattern_without(p_wrap.patterns[i], p_wrap.c_k[i], pair[1])

        return p_wrap_same, p_wrap_diff

    def get_better_sol(p_wrap1, p_wrap2):
        """
        Select the better solution of two solutions (stored in pattern wrappers
        :param p_wrap1: solution 1
        :param p_wrap2: solution 2
        :type p_wrap1: PatternWrapper
        :type p_wrap2: PatternWrapper
        :return:
        """
        if p_wrap1 is None and p_wrap2 is None:
            return None
        elif p_wrap1 is None and p_wrap2 is not None:
            return p_wrap2
        elif p_wrap1 is not None and p_wrap2 is None:
            return p_wrap1
        else:
            if p_wrap1.sum_of_costs() < p_wrap2.sum_of_costs():
                return p_wrap1
            else:
                return p_wrap2

    global glob_n_patterns, glob_n_nodes, glob_best_obj, glob_start_time

    # Init
    start = time.time()
    glob_n_nodes += 1  # +1 for current node
    n_iter = 0  # Number of iterations of pattern generation
    n_patterns = len(p_wrap.patterns)  # Starting number of patterns
    subproblem_models = []  # subproblem model for each profile
    done = False

    if params.PRINT_BB:
        print("\n[B&P:] Branching, on same = ", on_same, "; on diff = ", on_diff)

    # Initialize subproblem models
    for i in range(len(instance.profiles)):
        if params.BB_SUBPROBLEM_USE_CP:
            #subproblem_models.append(SubproblemModelCP(instance, i, [0 for j in range(instance.J)], 0, on_same, on_diff))
            subproblem_models.append(None)
        else:
            subproblem_models.append(SubproblemModel(instance, i, [0 for j in range(instance.J)], 0, on_same, on_diff))

    # Init dual model
    dual_model = DualModel(p_wrap.patterns, p_wrap.c_k, instance)

    # Generate patterns
    while not done:
        dual_model.optimize(get_timeout(glob_start_time, time.time()), path_out_glob)  # solve dual - to get dual costs

        # Check if dual model was solved optimally (i.e. check if it reached timeout or similar problems)
        if dual_model.model.status != GRB.Status.OPTIMAL:  # if not - it may be unbounded or infeasible
            if params.PRINT_BB:
                print("[B&P:] Dual model was not solved optimally.")
            return None

        # Solve subproblem -- generate a new pattern
        best_obj, best_pattern, best_ck, best_idx = solve_subproblem(dual_model.model.getAttr("x", dual_model.l_j),
                                                                     dual_model.l_0.X)

        # Check if solve_subproblem returned None(s) (= timeout reached)
        if best_obj is None and best_pattern is None and best_ck is None and best_idx is None:
            return None

        if best_obj < 0 - EPS:  # If objective of subproblem was < 0, add a new pattern to the dual model
            dual_model.add_pattern(best_pattern, best_ck)  # adding pattern to the dual model
            s_i, s_j, p_i, p_j, profile_idx = subproblem_models[best_idx].get_solution()
            best_solution = MonoprocessorSolution().init_directly(s_i, s_j, p_i, p_j, profile_idx)
            p_wrap.add_pattern_again(best_pattern, best_ck, best_solution)
            glob_n_patterns += 1

            if params.PRINT_BB:
                print("[B&P:] Pattern generated: ", best_ck, " : ", best_pattern, " : ", best_idx, best_obj)
        else:  # Objective was not negative - stop pattern generation phase
            done = True

        n_iter += 1
    # END of pattern generation phase ----------------------------------------------------------------------------------

    # Solve master model to get the optimal solution of the relaxed problem
    master_model = MasterModel(p_wrap.patterns, p_wrap.c_k, instance)
    master_model.optimize(get_timeout(glob_start_time, time.time()), path_out_glob)

    if master_model.model.status == GRB.Status.TIME_LIMIT:
        if params.PRINT_BB:
            print("[B&P:] Timeout reached while solving master model.")
        return None

    # Informative prints
    if params.PRINT_BB:
        print("All patterns:")
        p_wrap.print()
        print("[B&B:] Pattern generation took ", round(time.time()-start, 3), " seconds. [", n_iter, " iterations]", sep="")
        print("       - generated patterns " + str(n_patterns) + "-" + str(len(p_wrap.patterns) - 1) if n_patterns < len(p_wrap.patterns)
              else "       - none patterns were generated this iteration")
        print("       - objective:", master_model.model.objVal)

    # BRANCHING
    if master_model.model.status == GRB.Status.INFEASIBLE:
        if params.PRINT_BB:
            print("[B&P:] Master model is infeasible.")
        return None

    if master_model.alpha_is_not_zero():  # If alpha > 0 - there is no solution to relaxed master model without alpha
        if params.PRINT_BB:
            print("[B&P:] Master model - alpha is not zero: alpha =", master_model.alpha.X)
        return None

    if master_model.model.objVal > glob_best_obj:  # Check if master model is worse than best so far solution
        if params.PRINT_BB:
            print("[B&P:] Master model solution is worse than best so far solution.")
        return None

    if master_model.solution_is_integer():  # Check if solution is integer
        master_solution = get_solution(master_model, p_wrap)
        if params.PRINT_BB:
            print("[B&P:] Master solution is int.")
            master_solution.print()
        # Update best so far
        if master_model.model.objVal < glob_best_obj:
            glob_best_obj = master_model.model.objVal
        return master_solution
    else:
        if params.PRINT_BB:
            print("[B&P:] Master solution is not int.")

        # Create two branches and return better solution
        pair = get_pair(master_model, p_wrap, on_same, on_diff)

        if pair is None:  # There was no pair to generate
            return None

        # Generate new lists
        on_same_new = on_same.copy()
        on_diff_new = on_diff.copy()
        on_diff_new.append(pair)
        on_same_new.append(pair)

        p_wrap_same, p_wrap_diff = generate_new_patterns(p_wrap, pair)

        # - two branches
        sol_same = branch_and_price(instance, on_same_new, on_diff, p_wrap_same)
        sol_diff = branch_and_price(instance, on_same, on_diff_new, p_wrap_diff)

        return get_better_sol(sol_same, sol_diff)

def get_timeout(start, now):
    """Get timeout for some optimization, given start time and current time"""
    return max(1, params.TIMEOUT - max((now-start),0))

def test_patterns(instance, patterns, costs, profiles):
    """
    Take computed patterns and compare their costs with optimal costs solved by ILP model (for monoprocessor scheduling)
    :param instance: problem instance
    :param patterns: list of patterns
    :param costs: list of pattern costs
    :param profiles: list of pattern profiles
    :return: True if costs are OK
    """
    print("TESTING PATTERNS:")
    patterns_ok = True
    EPS = 0.001
    for i in range(len(patterns)):
        mon = MonoprocessorModel(instance, profiles[i], patterns[i])
        mon.optimize(float("inf"))
        print(i,")", "optimum:", mon.get_obj_val(), "| c_k", costs[i])
        # visualization.draw_gantt_mono(inst, mon.y_ji, mon.s_i, mon.s_j, mon.p_i, mon.profile, "".join([path_out,"/mon_", str(i)]))
        if abs(mon.get_obj_val() - costs[i]) > EPS:
            patterns_ok = False

    return patterns_ok


