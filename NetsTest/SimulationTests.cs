using System.Numerics;
using Nets.GeneticAlgorithm;
using Nets.Simulation;
using Nets.Network;

namespace NetsTest;

[TestFixture]
public class SimulationTests
{
    [Test]
    public void BirdMakeChildTest()
    {
        var topology = new NetworkTopology([5, 6, 2]);
        var brain = new Network(topology);
        var bird = new Bird(brain, Vector2.Zero, Vector2.Zero, new Eye(1,1, 5), 1,0.1f);
        
        var child = bird.MakeChild(bird.Genome);
        
        Assert.That(child.Genome.Genes, Is.EqualTo(bird.Genome.Genes));
    }
}