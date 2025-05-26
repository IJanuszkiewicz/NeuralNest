namespace Nets.GeneticAlgorithm.SelectionMethods;

public class ProportionalSelection : ISelectionMethod
{
    public T Select<T>(T[] population) where T : IIndividual
    {
        float sum = 0;
        foreach (var individual in population)
        {
            sum += individual.Fitness;
        }
        
        // In case the birds didn't eat anything
        if (sum == 0)
        {
            return population[Random.Shared.NextInt64(population.Length)];
        }
        
        double randomValue = Random.Shared.NextDouble();
        
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