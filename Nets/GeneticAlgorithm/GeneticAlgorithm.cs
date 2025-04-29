using Nets.GeneticAlgorithm.CrossoverMethods;
using Nets.GeneticAlgorithm.MutationMethods;
using Nets.GeneticAlgorithm.SelectionMethods;

namespace Nets.GeneticAlgorithm;

public class GeneticAlgorithm(
    ISelectionMethod selectionMethod,
    ICrossoverMethod crossoverMethod,
    IMutationMethod mutationMethod)
{
    private ISelectionMethod _selectionMethod = selectionMethod;
    private ICrossoverMethod _crossoverMethod = crossoverMethod;
    private IMutationMethod _mutationMethod = mutationMethod;

    public T[] Evolve<T>(T[] population) where T : IIndividual
    {
        for (int i = 0; i < population.Length; i++)
        {
            // select
            // crossover
            // mutate
        }
        throw new NotImplementedException();
    }

}