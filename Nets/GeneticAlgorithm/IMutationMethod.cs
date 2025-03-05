namespace Nets.GeneticAlgorithm;

public interface IMutationMethod
{
    public abstract void Mutate(Chromosome chromosome);
}