namespace Nets.GeneticAlgorithm.CrossoverMethods;

public class UniformCrossover : ICrossoverMethod
{
    public T Crossover<T>(T parentA, T parentB) where T : IIndividual
    {
        float[] childGenes = new float[parentA.Genome.Genes.Length];

        for (int i = 0; i < childGenes.Length; i++)
        {
            childGenes[i] = Random.Shared.NextDouble() < 0.5 ? parentA.Genome.Genes[i] : parentB.Genome.Genes[i];
        }

        return (T)T.FromGenome(new Genome(childGenes));
    }
}