namespace Nets.GeneticAlgorithm;

public interface ICrossoverMethod
{
    public abstract I Crossover<I>(I parent1, I parent2) where I : IIndividual;
}