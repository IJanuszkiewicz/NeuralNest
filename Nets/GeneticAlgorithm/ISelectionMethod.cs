namespace Nets.GeneticAlgorithm;

public interface ISelectionMethod
{
    public abstract I Select<I>(I[] population) where I : IIndividual;
}