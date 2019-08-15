from docplex.cp.model import CpoModel

import os.path
import instance
import random
import time

def main():
    filename = os.path.join(os.path.dirname(os.path.abspath(__file__)), "data_in/test_1.txt")
    inst = instance.Instance(filename)

    # input from master problem, now random
    id_profile = 0
    random.seed(42)
    rewards = [random.randint(0, 30000) for i in range(inst.J)]
    #rewards = [random.uniform(0.0, 30000.0) for i in range(inst.J)]
    equivalences = [[0, 1], [2, 3]]
    mutexes = [[2, 1], [4, 3]]
    # input from master problem, now random

    types = inst.profiles[id_profile]
    types.insert(0, inst.INIT)
    types.append(inst.TERM)
    trans_cost = [[0 for i in range(inst.V)] for j in range(inst.V)]
    trans_time = [[0 for i in range(inst.V)] for j in range(inst.V)]
    for i in range(len(inst.edges)):
        for j in range(len(inst.edges[i])):
            trans_cost[i][inst.edges[i][j]['to']] = inst.edges[i][j]['C'] * inst.edges[i][j]['t']
            trans_time[i][inst.edges[i][j]['to']] = inst.edges[i][j]['t']
    changeover_cost = 0
    for i in range(1, len(types)):
        changeover_cost += trans_cost[types[i - 1]][types[i]]

    cp = CpoModel()
    primitives = [[] for i in range(inst.J)]
    all_primitives = []
    init = cp.interval_var(start=0)
    init.set_end_max(inst.L)
    total_cost = changeover_cost + inst.vertices[inst.INIT]['C'] * cp.length_of(init, 0)
    modes_of_mach = [init] # serves only for retrieving starts and ends of modes
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
        cp.add(sum([cp.presence_of(p) for p in primitives[eq[0]]]) == sum([cp.presence_of(p) for p in primitives[eq[1]]]))
    for mut in mutexes:
        cp.add(sum([cp.presence_of(p) for p in primitives[mut[0]]]) + sum([cp.presence_of(p) for p in primitives[mut[1]]]) <= 1)
    cp.add(cp.minimize(total_cost))

    # set TimeLimit in seconds or delete it for no limit
    # parameters DefaultInferenceLevel and Workers may be beneficial to add
    sol = cp.solve(TimeLimit=30, LogVerbosity="Quiet")#, DefaultInferenceLevel='Extended', Workers=1)
    if sol:
        tasks = []
        for t in range(inst.J):
            for pr in primitives[t]:
                p = sol.get_var_solution(pr)
                if p.is_present():
                    tasks.append({"task": t, "start": p.get_start(), "end": p.get_end()})
                    break
        modes = []
        sched_cost = 0
        for t in range(len(modes_of_mach)):
            m = sol.get_var_solution(modes_of_mach[t])
            modes.append({"mode": types[t], "start": m.get_start(), "end": m.get_end()})
            sched_cost += inst.vertices[types[t]]['C'] * m.get_length()
        print("Solution status: " + sol.get_solve_status())
        print("Solve time: " + str(sol.get_solve_time()))
        print("Objective: " + str(sol.get_objective_values()[0]))
        print("Schedule cost: " + str(sched_cost))
        print(tasks)
        print(modes)
    else:
        print("No solution found.")

if __name__ == "__main__":
    start_time = time.time()
    main()
    end_time = time.time()
    print(end_time - start_time)