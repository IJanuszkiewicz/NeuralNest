namespace Nets.GeneticAlgorithm;

public interface IIndividual
{
    public float Fitness { get; }
    public Chromosome Chromosome { get; }
    public static abstract IIndividual FromChromosome(Chromosome chromosome);
}