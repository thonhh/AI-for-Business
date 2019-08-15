import os
import random
import params


def generate_tasks(L, J, N, max_p, t_on, rand):
    """
    Generate some random tasks (tuples of release time, deadline and processing time)
    :param L:
    :param J:
    :param max_p:
    :param t_on:
    :param t_off:
    :param rand:
    :return: list of pairs (release time, deadline)
    """
    max_trials = 100
    iter = 0
    while True:
        # Init the first interval
        intervals = []
        for i in range(N):
            intervals.append((0,L))
        # Prepare tasks
        tasks = []

        while len(intervals) > 0:
            random.shuffle(intervals)
            i_cur = intervals.pop()
            t_start = random.randint(i_cur[0], i_cur[1]-1)  # -1 excludes right boundary of the interval
            t_end = min(i_cur[1], t_start + random.randint(1,max_p))

            # Split interval
            if t_start > i_cur[0]:
                intervals.append((i_cur[0], t_start))

            if t_end < i_cur[1]:
                intervals.append((t_end, i_cur[1]))

            # Create new task
            r = max(t_start - random.randint(0, rand), 0)
            d = min(t_end + random.randint(0, rand), L)
            p = t_end - t_start
            tasks.append((r, d, p))

            # terminate if there is enough tasks
            if len(tasks) >= J:
                break

        if iter >= max_trials:
            return None

        if len(tasks) < J:
            iter += 1
            continue
        else:
            return [(x[0]+t_on, x[1]+t_on, x[2]) for x in tasks]  # Shift times by t_on


def generate_instance_files(path):
    for J in J_all:
        for N in N_all:
            for n_inst in n_instances:
                cur_path = path + str(N) + "_" + str(J) + "_" + str(n_inst) + ".txt"

                tasks = generate_tasks(L - (t_on + t_off), J, N, max_p, t_on, rand)  # Generate tasks
                assert tasks is not None, "Tasks were not found in several trials, try to adjust settings."

                f_out = open(cur_path, 'w')

                f_out.write("%d %d %d %d %d\n" % (N, J, V, E, P))
                f_out.write("%d\n" % (L))
                f_out.write("%d %d\n" % (INIT, TERM))

                for v in vertices:
                    f_out.write("%d %d %d\n" % (v[0], v[1], v[2]))

                for e in edges:
                    f_out.write("%d %d %d %d\n" % (e[0], e[1], e[2], e[3]))

                for prof in profiles:
                    f_out.write(" ".join(str(x) for x in prof) + "\n")

                # TODO: Note that tasks generation is specific for given graph (processing mode 0, plus two other modes)
                for t in tasks:
                    f_out.write(" ".join(str(x) for x in t) + " " + str(L+1) + " " + str(L+1) + "\n")

                f_out.close()

def create_folder(path):
    """
    Creates folder (if not exists)
    :param path:
    :return:
    """
    if not os.path.exists(path):
        os.makedirs(path)


if __name__ == "__main__":
    random.seed(17)  # Set random seed
    # Set parameters
    L = 1000  # Length of horizon
    J_all = params.GENERATOR_J
    N_all = params.GENERATOR_N
    max_p = 100  # Maximal processing time of a single task
    rand = max_p / 4  # Maximal shift for r / d of task
    t_on = 10  # Time from INIT mode to processing mode
    t_off = 15  # Time from processing mode to TERM mode
    n_instances = params.GENERATOR_n_inst  # Number of instances per combination
    # ---------------------------
    # Graph
    INIT, TERM = 2, 2
    vertices = [(0, L, 50), (0, L, 10), (0, L, 0)]  # (t_min, t_max, C_(for time unit))
    edges = [(2, 0, 10, 100), (0, 2, 15, 0), (0, 1, 5, 0), (1, 0, 5, 1)]  # (from, to, time, C_(for time unit))
    V = len(vertices)
    E = len(edges)
    # Profiles
    profiles = [(0,), (0, 1, 0)]
    P = len(profiles)
    # ---------------------------
    path = "data_in/tests/"
    create_folder(path)
    generate_instance_files(path)



