namespace Nets.GeneticAlgorithm.CrossoverMethods;

public class UniformCrossover : ICrossoverMethod
{
    public Genome Crossover(IIndividual parentA, IIndividual parentB)
    {
        float[] childGenes = new float[parentA.Genome.Genes.Length];

        for (int i = 0; i < childGenes.Length; i++)
        {
            childGenes[i] = Random.Shared.NextDouble() < 0.5 ? parentA.Genome.Genes[i] : parentB.Genome.Genes[i];
        }

        return new Genome(childGenes);
    }
}