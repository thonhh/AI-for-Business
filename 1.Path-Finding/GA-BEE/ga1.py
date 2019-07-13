#https://www.youtube.com/watch?v=zumC_C0C25c
import random

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
    def get_genes(self):
        return self._genes
    def get_fitness(self):
        self._fitness=0
        for i in range(self._genes.__len__()):
            ''' '''
            if self._genes[i]== TARGET_CHROMOSOME[i]:
                self._fitness += 1
            #    return self._fitness# sai vi tri lenh return
        return self._fitness

    def __str__(self):
        return  self._genes.__str__()

class Population:
    ''' '''
    def __init__(self, size):
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
        i = NUMB_OF__ELITE_CHROMOSOMES  # CÓ THỂ TRUNG I
        while i < POPULATION_SIZE:
            chromosome1 = GeneticAlgorithm._select_tornment_population(pop).get_chromosomes()[0]
            chromosome2 = GeneticAlgorithm._select_tornment_population(pop).get_chromosomes()[0]
            crossover_pop.get_chromosomes().append(GeneticAlgorithm._crossover_chromosomes(chromosome1, chromosome2))
            i += 1;

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

def _print_population(pop,gen_number):
    print("\n----------------------------------------------------")
    print("Generation #",gen_number,"|Fittness chromosome fitness:",pop.get_chromosomes()[0].get_fitness())
    print(("Target Chromosome:",TARGET_CHROMOSOME))
    print("\n----------------------------------------------------")
    i=0
    for x in pop.get_chromosomes():
        #print("Chromosome #", i," :", x, "|Fitness: ", x.get_fitness())
        print("Chromosome #", i, " :", "|Fitness: ", x.get_fitness())
        i+=1

population = Population(POPULATION_SIZE)
#print(population.get_chromosomes()[0].get_fitness())
population.get_chromosomes().sort(key=lambda x: x.get_fitness(), reverse=True) #Population : nham P va p
_print_population(population,0)

generation_number = 1
while population.get_chromosomes()[0].get_fitness()<TARGET_CHROMOSOME.__len__():
    population = GeneticAlgorithm.evolve(population)
    population.get_chromosomes().sort(key=lambda x: x.get_fitness(),reverse=True)
    _print_population(population,generation_number)
    generation_number += 1
