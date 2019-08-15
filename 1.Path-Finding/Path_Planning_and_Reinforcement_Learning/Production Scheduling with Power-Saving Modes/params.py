########################################################################################################################
# This file contains parameters which can be changed - for simple debugging and algorithm setting purposes
########################################################################################################################

TIMEOUT = 3600  # timeout for a single test (single run of BB or full model) (in seconds)
TEST_MONOPROCESSOR = False  # if true - monoprocessor ILP problem is solved for each subproblem pattern - objective is compared (to check CP mistakes)

INSTANCE_HEURISTIC_INIT = False  # Use heuristic initialization?

BB_SUBPROBLEM_USE_CP = False  # if True use CP for subproblem modelling, else use ILP model
BB_USE_GET_PAIR_HEUR = True  # if True - use heuristics to select next pair

FULL_MODEL_I = 10  # Number of intervals for full model

# ----------------------------------------------------------------------------------------------------------------------
# PRINTS
PRINT_BB = False  # if True - information about generated patterns, time etc. will be printed to stdout
PRINT_SUB = False  # if True - print solving progress of the external solver
PRINT_MASTER = False  # if True - print solving progress of the Master model
PRINT_DUAL = False  # if True - print solving progress of the Dual model
PRINT_COMPLEX = False  # if True - print information about complex model solution

# ----------------------------------------------------------------------------------------------------------------------
# VISUALIZATION
VIZ_GANTT_WIDTH = 1200  # WIDTH of Gantt chart

# ----------------------------------------------------------------------------------------------------------------------
# DATA GENERATOR
GENERATOR_N = [1, 2, 3, 4]  # Tested numbers of resources
GENERATOR_J = [5, 10, 15, 20, 25]  # Tested numbers of tasks
GENERATOR_n_inst = [0,1,2,3,4]  # range of generated files for combination of N, J