namespace Nets.GeneticAlgorithm.MutationMethods;

public class GaussianMutation(float mutationProbability, float mutationStrength) : IMutationMethod
{
    public void Mutate(Genome genome)
    {
        for (int i = 0; i < genome.Genes.Length; i++)
        {
            if ((float)Random.Shared.NextDouble() <= mutationProbability)
            {
                genome.Genes[i] += (float)Random.Shared.NextDouble() * mutationStrength;
            }
        }
    }
}