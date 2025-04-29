using Nets.GeneticAlgorithm;
using Nets.GeneticAlgorithm.SelectionMethods;
using MathNet.Numerics.Distributions;
using Nets.GeneticAlgorithm.CrossoverMethods;
using Nets.GeneticAlgorithm.MutationMethods;

namespace NetsTest;

[TestFixture]
public class GeneticAlgorithmTests
{
    private const double SignificanceLevel = 0.01;
    
    private class TestIndividual(float? fitness, Genome? genome = null) : IIndividual
    {
        public float Fitness { get; } = fitness ?? 0;
        public Genome Genome { get; } = genome ?? new Genome([]);
        public static IIndividual FromGenome(Genome genome) { return new TestIndividual(0, genome); }
    }
    
    private double CalculateChiSquarePValue(int[] observed, float[] expectedProportions, int sampleSize)
    {
        double chiSquare = 0;
        for (int i = 0; i < observed.Length; i++)
        {
            double expected = expectedProportions[i] * sampleSize;
            chiSquare += Math.Pow(observed[i] - expected, 2) / expected;
        }
        int degreesOfFreedom = observed.Length - 1;
        var chiDist = new ChiSquared(degreesOfFreedom);
        return 1 - chiDist.CumulativeDistribution(chiSquare);
    }
    
    [Test]
    public void TestProportionalSelection()
    {
        float[] expectedProportions = [0.1f, 0.2f, 0.3f, 0.4f];
        const int sampleSize = 100_000;
        var population = new TestIndividual[]
        {
            new(expectedProportions[0]),
            new(expectedProportions[1]),
            new(expectedProportions[2]),
            new(expectedProportions[3])
        };
        int[] observed = [0, 0, 0, 0];
        var selectionMethod = new ProportionalSelection();
        
        for (int i = 0; i < sampleSize; i++)
        {
            var selectedIndividual = selectionMethod.Select(population);
            if (selectedIndividual == population[0])
                observed[0]++;
            else if (selectedIndividual == population[1])
                observed[1]++;
            else if (selectedIndividual == population[2])
                observed[2]++;
            else if (selectedIndividual == population[3])
                observed[3]++;
        }
        
        double pValue = CalculateChiSquarePValue(observed, expectedProportions, sampleSize);

        Assert.That(observed.Length, Is.EqualTo(expectedProportions.Length));
        Assert.That(pValue, Is.GreaterThan(SignificanceLevel));
    }

    [Test]
    public void TestUniformCrossover()
    {
        const int sampleSize = 100_000;
        float[] genomeA = new float[sampleSize], genomeB = new float[sampleSize];
        for (int i = 0; i < sampleSize; i++)
        {
            genomeA[i] = 1;
            genomeB[i] = 0;
        }
        var parentA = new TestIndividual(null, new Genome(genomeA));
        var parentB = new TestIndividual(null, new Genome(genomeB));
        var crossoverMethod = new UniformCrossover();
        
        var child = crossoverMethod.Crossover(parentA, parentB);

        int numOfParentAGenes = (int)child.Genome.Genes.Sum();
        int[] observed = [numOfParentAGenes, sampleSize - numOfParentAGenes];
        float[] expectedProportions = [0.5f, 0.5f];
        double pValue = CalculateChiSquarePValue(observed, expectedProportions, sampleSize);
        
        Assert.That(child.Genome.Genes.Length, Is.EqualTo(sampleSize));
        Assert.That(pValue, Is.GreaterThan(SignificanceLevel));
    }
    
    [Test]
    public void TestGaussianMutation_NoMutation()
    {
        const int sampleSize = 100_000;
        float[] genome = new float[sampleSize];
        for (int i = 0; i < sampleSize; i++)
        {
            genome[i] = 0.5f;
        }
        var individual = new TestIndividual(null, new Genome(genome));
        var mutationMethod = new GaussianMutation(0f, 100f);
        
        mutationMethod.Mutate(individual.Genome);

        int numOfMutatedGenes = individual.Genome.Genes.Count(g => g != 0.5f);  // Intentional float comparison
        Assert.That(numOfMutatedGenes, Is.EqualTo(0));
    }
    
    [Test]
    public void TestGaussianMutation_HalfMutated()
    {
        const int sampleSize = 100_000;
        float[] genome = new float[sampleSize];
        for (int i = 0; i < sampleSize; i++)
        {
            genome[i] = 0.5f;
        }
        var individual = new TestIndividual(null, new Genome(genome));
        var mutationMethod = new GaussianMutation(0.5f, 100f);
        
        mutationMethod.Mutate(individual.Genome);

        int numOfMutatedGenes = individual.Genome.Genes.Count(g => g != 0.5f);  // Intentional float comparison
        int[] observed = [numOfMutatedGenes, sampleSize - numOfMutatedGenes];
        float[] expectedProportions = [0.5f, 0.5f];
        double pValue = CalculateChiSquarePValue(observed, expectedProportions, sampleSize);
        
        Assert.That(pValue, Is.GreaterThan(SignificanceLevel));
    }
    
    [Test]
    public void TestGaussianMutation_AllMutated()
    {
        const int sampleSize = 100_000;
        float[] genome = new float[sampleSize];
        for (int i = 0; i < sampleSize; i++)
        {
            genome[i] = 0.5f;
        }
        var individual = new TestIndividual(null, new Genome(genome));
        var mutationMethod = new GaussianMutation(1f, 100f);
        
        mutationMethod.Mutate(individual.Genome);

        int numOfMutatedGenes = individual.Genome.Genes.Count(g => g != 0.5f);  // Intentional float comparison
        Assert.That(numOfMutatedGenes, Is.EqualTo(sampleSize));
    }
}