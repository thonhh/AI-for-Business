# Production Scheduling with Power-Saving Modes
This repository contains the source code and benchmark instances for the production scheduling problem with power saving modes presented in *Energy-aware Production Scheduling with Power-Saving modes*.

The project has following dependencies:
- [Python3](https://www.python.org/) (>= 3.5) - main programming language
- [Gurobi](http://www.gurobi.com/) (>= 7.0) - library for Mixed Integer Linear Programming 
 
      [Install on Anaconda] (https://www.gurobi.com/documentation/8.1/quickstart_mac/installing_the_anaconda_py.html#section:Anaconda)
- [CP Optimizer](https://www.ibm.com/analytics/data-science/prescriptive-analytics/cplex-cp-optimizer) (>= 12.7) - library for Constraint Programming
- [docplex](https://ibmdecisionoptimization.github.io/docplex-doc/) (>= 2.3.44) - Python binding for CP Optimizer

The content of the repository is following
- `data_in/tests` the benchmark instances named as `{numMachines}_{numJobs}_{instanceIndex}.txt`
- `graph.svg` transition times and costs between the modes used in the benchmark instances
- `main.py` the entry-point of the solvers

To run the Branch-and-Price solvers (CP and ILP subproblems), do

    python3 main.py

## License
[MIT license](LICENSE)

## Authors
Please see file [AUTHORS](AUTHORS) for the list of authors.

## <a name="citing"></a>Citing
If you find our code or benchmark instances useful, we kindly request that you cite the following paper
```
@article{benedikt2018,
title = "Energy-Aware Production Scheduling with Power-Saving Modes",
author = "Ondřej Benedikt and Přemysl Šůcha and István Módos and Marek Vlk and Zdeněk
Hanzálek",
}
```