namespace Nets.GeneticAlgorithm.MutationMethods;

public interface IMutationMethod
{
    public abstract void Mutate(Chromosome chromosome);
}