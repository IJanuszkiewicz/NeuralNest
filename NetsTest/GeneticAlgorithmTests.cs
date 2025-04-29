using Nets.GeneticAlgorithm;
using Nets.GeneticAlgorithm.SelectionMethods;
using MathNet.Numerics.Distributions;

namespace NetsTest;

[TestFixture]
public class GeneticAlgorithmTests
{
    private class TestIndividual(float fitness) : IIndividual
    {
        public float Fitness { get; } = fitness;
        public Chromosome Chromosome { get; } = new([]);
        public static IIndividual FromChromosome(Chromosome chromosome) { return new TestIndividual(0); }
    }
    
    [Test]
    public void TestProportionalSelection()
    {
        float[] expectedProportions = [0.1f, 0.2f, 0.3f, 0.4f];
        int sampleSize = 10_000;
        var population = new IIndividual[]
        {
            new TestIndividual(expectedProportions[0]),
            new TestIndividual(expectedProportions[1]),
            new TestIndividual(expectedProportions[2]),
            new TestIndividual(expectedProportions[3])
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

        // Compute Chi-Square statistic
        double chiSquare = 0;
        for (int i = 0; i < observed.Length; i++)
        {
            double expected = expectedProportions[i] * sampleSize;
            chiSquare += Math.Pow(observed[i] - expected, 2) / expected;
        }
        
        int degreesOfFreedom = observed.Length - 1;
        var chiDist = new ChiSquared(degreesOfFreedom);
        double pValue = 1 - chiDist.CumulativeDistribution(chiSquare);
        
        Assert.That(pValue, Is.GreaterThan(0.05));
    }
}