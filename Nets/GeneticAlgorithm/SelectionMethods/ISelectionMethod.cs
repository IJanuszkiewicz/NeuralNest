namespace Nets.GeneticAlgorithm.SelectionMethods;

public interface ISelectionMethod
{
    public IIndividual Select(IIndividual[] population);
}