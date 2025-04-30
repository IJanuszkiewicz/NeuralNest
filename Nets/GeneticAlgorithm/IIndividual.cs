namespace Nets.GeneticAlgorithm;

public interface IIndividual
{
    public float Fitness { get; }
    public Genome Genome { get; }
    public IIndividual MakeChild(Genome genome);
}