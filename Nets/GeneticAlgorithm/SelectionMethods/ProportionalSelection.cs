namespace Nets.GeneticAlgorithm.SelectionMethods;

public class ProportionalSelection : ISelectionMethod
{
    public IIndividual Select(IIndividual[] population)
    {
        float sum = 0;
        foreach (var individual in population)
        {
            sum += individual.Fitness;
        }
        
        double randomValue  = Random.Shared.NextDouble();
        
        double cumSum = 0;
        foreach (var individual in population)
        {
            cumSum += individual.Fitness / sum;
            if (randomValue <= cumSum)
            {
                return individual;
            }
        }
        
        throw new Exception("Selection failed, no individual selected.");
    }
}