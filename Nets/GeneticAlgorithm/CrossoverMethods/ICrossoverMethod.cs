namespace Nets.GeneticAlgorithm.CrossoverMethods;

public interface ICrossoverMethod
{
    public T Crossover<T>(T parentA, T parentB) where T : IIndividual;
}