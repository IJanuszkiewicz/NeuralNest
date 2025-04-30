namespace Nets.GeneticAlgorithm.MutationMethods;

public class GaussianMutation(float mutationProbability, float mutationStrength) : IMutationMethod
{
    public void Mutate(Genome genome)
    {
        for (int i = 0; i < genome.Genes.Length; i++)
        {
            if ((float)Random.Shared.NextDouble() <= mutationProbability)
            {
                float sign  = Random.Shared.NextDouble() < 0.5 ? -1 : 1;
                genome.Genes[i] += sign*(float)Random.Shared.NextDouble() * mutationStrength;
            }
        }
    }
}