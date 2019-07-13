#https://www.youtube.com/watch?v=zumC_C0C25c
import random
import sys
import copy
#from Hive import Utilities
try:
    import numpy as np
except:
    raise ImportError("Numpy module not installed.")

TARGET_CHROMOSOME=[1,1,0,1,1,1,0,1,0,1]
POPULATION_SIZE=8
TOURNAMENT_SELECTION_SIZE = 4
MUTATITION_RATE = 0.25
NUMB_OF__ELITE_CHROMOSOMES = 1
class Chromosome:
    def __init__(self):
        self._genes=[]
        self._fitness=0
        i=0
        while i<TARGET_CHROMOSOME.__len__():
            if random.random()>=0.5:
                self._genes.append(1)
            else:
                self._genes.append(0)
            i+=1

# ---------------------------------------------------
    def __init__(self, lower, upper, fun, funcon=None):
        """

        Instantiates a bee object randomly.

        Parameters:
        ----------
            :param list lower  : lower bound of solution vector
            :param list upper  : upper bound of solution vector
            :param def  fun    : evaluation function
            :param def  funcon : constraints function, must return a boolean

        """

        # creates a random solution vector
        self._random(lower, upper)

        # checks if the problem constraint(s) are satisfied
        if not funcon:
            self.valid = True
        else:
            self.valid = funcon(self.vector)

        # computes fitness of solution vector
        if (fun != None):
            self.value = fun(self.vector)
        else:
            self.value = sys.float_info.max
        self._fitness()

        # initialises trial limit counter - i.e. abandonment counter
        self.counter = 0

    def _random(self, lower, upper):
        """ Initialises a solution vector randomly. """

        self.vector = []
        for i in range(len(lower)):
            self.vector.append( lower[i] + random.random() * (upper[i] - lower[i]) )

    #def _fitness(self):
    def get_fitness(self):
        """

        Evaluates the fitness of a solution vector.

        The fitness is a measure of the quality of a solution.

        """

        if (self.value >= 0):
            self._fitness = 1 / (1 + self.value)
        else:
            self._fitness = 1 + abs(self.value)


#---------------------------------------------------
    def get_genes(self):
        return self._genes
    def get_fitness1(self):
        self._fitness=0
        for i in range(self._genes.__len__()):
            ''' '''
            if self._genes[i]== TARGET_CHROMOSOME[i]:
                self._fitness += 1
            #    return self._fitness# sai vi tri lenh return
        return self._fitness

    def __str__(self):
        return  self._genes.__str__()

class BeeHive1(object):
    """

    Creates an Artificial Bee Colony (ABC) algorithm.

    The population of the hive is composed of three distinct types
    of individuals:

        1. "employees",
        2. "onlookers",
        3. "scouts".

    The employed bees and onlooker bees exploit the nectar
    sources around the hive - i.e. exploitation phase - while the
    scouts explore the solution domain - i.e. exploration phase.

    The number of nectar sources around the hive is equal to
    the number of actively employed bees and the number of employees
    is equal to the number of onlooker bees.

    """



    def __init__(self                 ,
                 lower, upper         ,
                 fun          = None  ,
                 numb_bees    =  30   ,
                 max_itrs     = 100   ,
                 max_trials   = None  ,
                 selfun       = None  ,
                 seed         = None  ,
                 verbose      = False ,
                 extra_params = None ,):
        """

        Instantiates a bee hive object.

        1. INITIALISATION PHASE.
        -----------------------

        The initial population of bees should cover the entire search space as
        much as possible by randomizing individuals within the search
        space constrained by the prescribed lower and upper bounds.

        Parameters:
        ----------

            :param list lower          : lower bound of solution vector
            :param list upper          : upper bound of solution vector
            :param def fun             : evaluation function of the optimal problem
            :param def numb_bees       : number of active bees within the hive
            :param int max_trials      : max number of trials without any improvment
            :param def selfun          : custom selection function
            :param int seed            : seed of random number generator
            :param boolean verbose     : makes computation verbose
            :param dict extra_params   : optional extra arguments for selection function selfun

        """

        # checks input
        assert (len(upper) == len(lower)), "'lower' and 'upper' must be a list of the same length."

        # generates a seed for the random number generator
        if (seed == None):
            self.seed = random.randint(0, 1000)
        else:
            self.seed = seed
        random.seed(self.seed)

        # computes the number of employees
        self.size = POPULATION_SIZE#int((numb_bees + numb_bees % 2))

        # assigns properties of algorithm
        self.dim = len(lower)
        self.max_itrs = max_itrs
        if (max_trials == None):
            self.max_trials = 0.6 * self.size * self.dim
        else:
            self.max_trials = max_trials
        self.selfun = selfun
        self.extra_params = extra_params

        # assigns properties of the optimisation problem
        self.evaluate = fun
        self.lower    = lower
        self.upper    = upper

        # initialises current best and its a solution vector
        self.best = sys.float_info.max
        self.solution = None

        # creates a bee hive = chromosome
        self.population = [ Chromosome(lower, upper, fun) for i in range(self.size) ]
        population.get_chromosomes().sort(key=lambda x: x.get_fitness(), reverse=True)  # Population : nham P va p
        self._print_population(population,0)
        # initialises best solution vector to food nectar
        self.find_best()

        # computes selection probability
        self.compute_probability()

        # verbosity of computation
        self.verbose = verbose

    def find_best(self):
        """ Finds current best bee candidate. """

        values = [ bee.value for bee in self.population ]
        index  = values.index(min(values))
        if (values[index] < self.best):
            self.best     = values[index]
            self.solution = self.population[index].vector

    def compute_probability(self):
        """

        Computes the relative chance that a given solution vector is
        chosen by an onlooker bee after the Waggle dance ceremony when
        employed bees are back within the hive.

        """

        # retrieves fitness of bees within the hive
        values = [bee.fitness for bee in self.population]
        max_values = max(values)

        # computes probalities the way Karaboga does in his classic ABC implementation
        if (self.selfun == None):
            self.probas = [0.9 * v / max_values + 0.1 for v in values]
        else:
            if (self.extra_params != None):
                self.probas = self.selfun(list(values), **self.extra_params)
            else:
                self.probas = self.selfun(values)

        # returns intervals of probabilities
        return [sum(self.probas[:i+1]) for i in range(self.size)]

    def send_employee(self, index):
        """

        2. SEND EMPLOYED BEES PHASE.
        ---------------------------

        During this 2nd phase, new candidate solutions are produced for
        each employed bee by cross-over and mutation of the employees.

        If the modified vector of the mutant bee solution is better than
        that of the original bee, the new vector is assigned to the bee.

        """

        # deepcopies current bee solution vector
        zombee = copy.deepcopy(self.population[index])

        # draws a dimension to be crossed-over and mutated
        d = random.randint(0, self.dim-1)

        # selects another bee
        bee_ix = index;
        while (bee_ix == index): bee_ix = random.randint(0, self.size-1)

        # produces a mutant based on current bee and bee's friend
        zombee.vector[d] = self._mutate(d, index, bee_ix)

        # checks boundaries
        zombee.vector = self._check(zombee.vector, dim=d)

        # computes fitness of mutant
        zombee.value = self.evaluate(zombee.vector)
        zombee._fitness()

        # deterministic crowding
        if (zombee.fitness > self.population[index].fitness):
            self.population[index] = copy.deepcopy(zombee)
            self.population[index].counter = 0
        else:
            self.population[index].counter += 1 #con nào chưa mạnh lên thì thì tăng bộ đếm

    def send_onlookers(self):
        """

        3. SEND ONLOOKERS PHASE.
        -----------------------

        We define as many onlooker bees as there are employed bees in
        the hive since onlooker bees will attempt to locally improve the
        solution path of the employed bee they have decided to follow
        after the waggle dance phase.

        If they improve it, they will communicate their findings to the bee
        they initially watched "waggle dancing".

        """

        # sends onlookers
        numb_onlookers = 0; beta = 0
        while (numb_onlookers < self.size):

            # draws a random number from U[0,1]
            phi = random.random()

            # increments roulette wheel parameter beta
            beta += phi * max(self.probas)
            beta %= max(self.probas)

            # selects a new onlooker based on waggle dance
            index = self.select(beta)

            # sends new onlooker đột biến ngẫu nhiên từng con và cập nhật lại sức mạnh (>hơn cái cũ) cho con đó
            self.send_employee(index)

            # increments number of onlookers
            numb_onlookers += 1

    def select(self, beta):
        """

        4. WAGGLE DANCE PHASE.
        ---------------------

        During this 4th phase, onlooker bees are recruited using a roulette
        wheel selection.

        This phase represents the "waggle dance" of honey bees (i.e. figure-
        eight dance). By performing this dance, successful foragers
        (i.e. "employed" bees) can share, with other members of the
        colony, information about the direction and distance to patches of
        flowers yielding nectar and pollen, to water sources, or to new
        nest-site locations.

        During the recruitment, the bee colony is re-sampled in order to mostly
        keep, within the hive, the solution vector of employed bees that have a
        good fitness as well as a small number of bees with lower fitnesses to
        enforce diversity.

        Parameter(s):
        ------------
            :param float beta : "roulette wheel selection" parameter - i.e. 0 <= beta <= max(probas)

        """

        # computes probability intervals "online" - i.e. re-computed after each onlooker
        probas = self.compute_probability()

        # selects a new potential "onlooker" bee
        for index in range(self.size):
            if (beta < probas[index]):
                return index

    def send_scout(self):
        """

        5. SEND SCOUT BEE PHASE.
        -----------------------

        Identifies bees whose abandonment counts exceed preset trials limit,
        abandons it and creates a new random bee to explore new random area
        of the domain space.

        In real life, after the depletion of a food nectar source, a bee moves
        on to other food sources.

        By this means, the employed bee which cannot improve their solution
        until the abandonment counter reaches the limit of trials becomes a
        scout bee. Therefore, scout bees in ABC algorithm prevent stagnation
        of employed bee population.

        Intuitively, this method provides an easy means to overcome any local
        optima within which a bee may have been trapped.

        """

        # retrieves the number of trials for all bees
        trials = [ self.population[i].counter for i in range(self.size) ]

        # identifies the bee with the greatest number of trials
        index = trials.index(max(trials))

        # checks if its number of trials exceeds the pre-set maximum number of trials
        #nếu số lần đột biến của 1 con ong (MÀ KO THU LỢI - VẪN YẾU) > max_trials THÌ THAY CON KHÁC
        if (trials[index] > self.max_trials):

            # creates a new scout bee randomly
            self.population[index] = Chromosome(self.lower, self.upper, self.evaluate)

            # sends scout bee to exploit its solution vector
            self.send_employee(index)

    def _mutate(self, dim, current_bee, other_bee):
        """

        Mutates a given solution vector - i.e. for continuous
        real-values.

        Parameters:
        ----------

            :param int dim         : vector's dimension to be mutated
            :param int current_bee : index of current bee
            :param int other_bee   : index of another bee to cross-over

        """

        return self.population[current_bee].vector[dim]    + \
               (random.random() - 0.5) * 2                 * \
               (self.population[current_bee].vector[dim] - self.population[other_bee].vector[dim])

    def _check(self, vector, dim=None):
        """

        Checks that a solution vector is contained within the
        pre-determined lower and upper bounds of the problem.

        """

        if (dim == None):
            range_ = range(self.dim)
        else:
            range_ = [dim]

        for i in range_:

            # checks lower bound
            if  (vector[i] < self.lower[i]):
                vector[i] = self.lower[i]

            # checks upper bound
            elif (vector[i] > self.upper[i]):
                vector[i] = self.upper[i]

        return vector

    def _verbose(self, itr, cost):
        """ Displays information about computation. """

        msg = "# Iter = {} | Best Evaluation Value = {} | Mean Evaluation Value = {} "
        print(msg.format(int(itr), cost["best"][itr], cost["mean"][itr]))

    def _print_population(pop, gen_number):
        print("\n----------------------------------------------------")
        print("Generation #", gen_number, "|Fittness chromosome fitness:", pop.get_chromosomes()[0].get_fitness())
        print(("Target Chromosome:", TARGET_CHROMOSOME))
        print("\n----------------------------------------------------")
        i = 0
        for x in pop.get_chromosomes():
            print("Chromosome #", i, " :", x, "|Fitness: ", x.get_fitness())
            i += 1

    def run(self):
        """ Runs an Artificial Bee Colony (ABC) algorithm. """

        cost = {}; cost["best"] = []; cost["mean"] = []

        #_print_population(population, 0)

        for itr in range(self.max_itrs):

            # employees phase đột biến cả bầy và cập nhật lại sức mạnh (>hơn cái cũ) cho bầy
            for index in range(self.size):
                self.send_employee(index)

            # onlookers phase - đột biến ngẫu nghiên self.size lần và cập nhật lại sức mạnh (>hơn cái cũ) cho self.size đó
            self.send_onlookers()

            # scouts phase - KIỂM TRA ONG/NGUỒN MẬT SAU MAX LẦN ĐỘT BIẾN ->NẾU KO TỐT HƠN THÌ THAY ONG/NGUỒN MẬT
            self.send_scout()

            # computes best path-TÌM ĐƯỢC CON ONG/NGUỒN MẬT TỐT NHẤT
            self.find_best()

            # stores convergence information
            cost["best"].append( self.best )
            cost["mean"].append( sum( [ bee.value for bee in self.population ] ) / self.size )

            # prints out information about computation
            if self.verbose:
                self._verbose(itr, cost)

        return cost

class Population:
    ''' '''
    def __init__(self, size, fun):
        self._chromosomes = []
        i=0
        while i<size:
            self._chromosomes.append(Chromosome())
            i+=1
    def get_chromosomes(self): return  self._chromosomes

class GeneticAlgorithm:
    ''' '''
    @staticmethod
    def evolve(pop):
        ''''''
        return GeneticAlgorithm._mutate_population(GeneticAlgorithm._crossover_population(pop))

    @staticmethod
    def _crossover_population(pop):
        crossover_pop = Population(0)
        for i in range(NUMB_OF__ELITE_CHROMOSOMES):
            crossover_pop.get_chromosomes().append(pop.get_chromosomes()[i])
            i = NUMB_OF__ELITE_CHROMOSOMES
            while i < POPULATION_SIZE:
                chromosome1 = GeneticAlgorithm._select_tornment_population(pop).get_chromosomes()[0]
                chromosome2 = GeneticAlgorithm._select_tornment_population(pop).get_chromosomes()[0]
                crossover_pop.get_chromosomes().append(GeneticAlgorithm._crossover_chromosomes(chromosome1,chromosome2))
                i +=1;
        return crossover_pop
    @staticmethod
    def _mutate_population(pop):
        for i in range(NUMB_OF__ELITE_CHROMOSOMES,POPULATION_SIZE):
            GeneticAlgorithm._mutate_chromosome(pop.get_chromosomes()[i])
        return pop

    @staticmethod
    def _crossover_chromosomes(chromosome1,chromosome2):
        crossover_chrom = Chromosome()
        for i in range(TARGET_CHROMOSOME.__len__()):
            if random.random()>=0.5:
                crossover_chrom.get_genes()[i] = chromosome1.get_genes()[i]
            else:
                crossover_chrom.get_genes()[i] = chromosome2.get_genes()[i]
        return crossover_chrom

    @staticmethod
    def _mutate_chromosome(chromosome):
        ''''''
        for i in range(TARGET_CHROMOSOME.__len__()):
            if random.random() < MUTATITION_RATE:
                if random.random() < 0.5:
                    chromosome.get_genes()[i] = 1
                else:
                    chromosome.get_genes()[i] = 0

    @staticmethod
    def _select_tornment_population(pop):
        ''''''
        tournament_pop = Population(0)
        i = 0
        while i <TOURNAMENT_SELECTION_SIZE:
            tournament_pop.get_chromosomes().append(pop.get_chromosomes()[random.randrange(0,POPULATION_SIZE)])
            i+=1
        tournament_pop.get_chromosomes().sort(key=lambda x:x.get_fitness(),reverse=True)
        return  tournament_pop



def evaluator(vector, a=1, b=100):
    """

    The Rosenbrock function is a non-convex function used as a performance test
    problem for optimization algorithms introduced by Howard H. Rosenbrock in
    1960. It is also known as Rosenbrock's valley or Rosenbrock's banana
    function.

    The function is defined by

                        f(x, y) = (a-x)^2 + b(y-x^2)^2

    It has a global minimum at (x, y) = (a, a**2), where f(x, y) = 0.

    """

    vector = np.array(vector)

    return (a - vector[0])**2 + b * (vector[1] - vector[0]**2)**2 #ham can xem lai


    # plots convergence
#Utilities.ConvergencePlot(cost)



def _print_population(pop, gen_number):
    print("\n----------------------------------------------------")
    print("Generation #", gen_number, "|Fittness chromosome fitness:", pop.get_chromosomes()[0].get_fitness1())
    print(("Target Chromosome:", TARGET_CHROMOSOME))
    print("\n----------------------------------------------------")
    i = 0
    for x in pop.get_chromosomes():
        print("Chromosome #", i, " :", x, "|Fitness: ", x.get_fitness1())
        i += 1


population = Population(POPULATION_SIZE)
#print(population.get_chromosomes()[0].get_fitness())
population.get_chromosomes().sort(key=lambda x: x.get_fitness1(), reverse=True) #Population : nham P va p
_print_population(population,0)

generation_number = 1
while population.get_chromosomes()[0].get_fitness()<TARGET_CHROMOSOME.__len__():
    population = GeneticAlgorithm.evolve(population)
    population.get_chromosomes().sort(key=lambda x: x.get_fitness1(),reverse=True)
    #_print_population(population,generation_number)
    generation_number += 1
