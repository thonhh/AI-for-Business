# ======================================================================================================================
# Script with parameters initialization calling optimization routines
# ======================================================================================================================
import solver
import os
import cProfile

def main():
    def create_folder(path):
        """
        Creates folder (if not exists)
        :param path:
        :return:
        """
        if not os.path.exists(path):
            os.makedirs(path)

    PATH_IN = "data_in/tests/1_5_0.txt"  # Path to the input data
    PATH_OUT = "".join(["data_out/", PATH_IN[PATH_IN.find("/")+1:-4]])  # Path to output data:  data_out/[path in data_in]
    PATH_LOG = "data_out/logfile.txt"  # Path to the log file, where some info produced by solver will be saved
    TIME_LIMIT = float("inf")  # Time limiting Gurobi calculations

    create_folder(PATH_OUT)
    # Call branch and price solver
    #solver.solve(PATH_IN, PATH_OUT, PATH_LOG, TIME_LIMIT, debug=True)

    #create_folder("data_out/tests/")
    solver.solve_tests()

if __name__ == "__main__":
    #pr = cProfile.Profile()
    #pr.enable()

    main()

    #pr.disable()
    #pr.print_stats(sort='time')


