namespace Nets.GeneticAlgorithm.CrossoverMethods;

public interface ICrossoverMethod
{
    public abstract IIndividual Crossover(IIndividual parent1, IIndividual parent2);
}