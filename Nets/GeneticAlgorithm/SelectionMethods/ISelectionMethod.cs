namespace Nets.GeneticAlgorithm.SelectionMethods;

public interface ISelectionMethod
{
    public abstract IIndividual Select(IIndividual[] population);
}