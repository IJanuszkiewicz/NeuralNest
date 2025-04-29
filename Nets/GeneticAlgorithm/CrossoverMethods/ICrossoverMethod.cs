namespace Nets.GeneticAlgorithm.CrossoverMethods;

public interface ICrossoverMethod
{
    public Genome Crossover(IIndividual parentA, IIndividual parentB);
}