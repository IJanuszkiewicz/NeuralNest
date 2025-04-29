namespace Nets.GeneticAlgorithm.SelectionMethods;

public interface ISelectionMethod
{
    public T Select<T>(T[] population) where T : IIndividual;
}