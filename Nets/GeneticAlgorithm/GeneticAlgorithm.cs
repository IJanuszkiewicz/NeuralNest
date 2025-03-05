namespace Nets.GeneticAlgorithm;

public class GeneticAlgorithm
{
    private ISelectionMethod _selectionMethod;
    private ICrossoverMethod _crossoverMethod;
    private IMutationMethod _mutationMethod;

    public GeneticAlgorithm(ISelectionMethod selectionMethod, ICrossoverMethod crossoverMethod,
        IMutationMethod mutationMethod)
    {
        _selectionMethod = selectionMethod;
        _crossoverMethod = crossoverMethod;
        _mutationMethod = mutationMethod;
    }

    public I[] Evolve<I>(I[] population) where I : IIndividual
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