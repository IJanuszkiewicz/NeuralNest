using Nets.GeneticAlgorithm.CrossoverMethods;
using Nets.GeneticAlgorithm.MutationMethods;
using Nets.GeneticAlgorithm.SelectionMethods;

namespace Nets.GeneticAlgorithm;

public class GeneticAlgorithm(
    ISelectionMethod selectionMethod,
    ICrossoverMethod crossoverMethod,
    IMutationMethod mutationMethod)
{
    public T[] Evolve<T>(T[] population) where T : IIndividual
    {
        var newGeneration = new T[population.Length];
        for (int i = 0; i < population.Length; i++)
        {
            var parentA = selectionMethod.Select(population);
            var parentB = selectionMethod.Select(population);
            
            var child = crossoverMethod.Crossover(parentA, parentB);
            
            mutationMethod.Mutate(child.Genome);
            newGeneration[i] = child;
        }
        return newGeneration;
    }

}