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
        var bird = new Bird(brain, Vector2.Zero, Vector2.Zero, new Eye(1, 1, 5), 1, 0.1f);

        var child = bird.MakeChild(bird.Genome);

        Assert.That(child.Genome.Genes, Is.EqualTo(bird.Genome.Genes));
    }

    [Test]
    public void EyeReceptorTest_SingleFood()
    {
        //   . - food
        //   ^ - bird looking directly at the food
        var eye = new Eye((float)Math.PI / 2, 1, 3);
        var food = new Food(new Vector2(0f, 0.5f));
        var pos = new Vector2(0f, 0f);
        var speed = new Vector2(0f, 1f);

        var signals = eye.Process([food], pos, speed);
        Assert.Multiple(() =>
        {
            Assert.That(signals[0], Is.EqualTo(0));
            Assert.That(signals[2], Is.EqualTo(0));
            Assert.That(signals[1], Is.EqualTo(0.5f));
        });
    }

    [Test]
    public void EyeReceptorTest_MoreFood()
    {
        //  .. - food
        //   ^ - bird
        var eye = new Eye((float)Math.PI * 0.6f, 1, 3);
        var food1 = new Food(new Vector2(0f, 0.5f));
        var food2 = new Food(new Vector2(-0.5f, 0.5f));
        var pos = new Vector2(0f, 0f);
        var speed = new Vector2(0f, 1f);

        var signals = eye.Process([food1, food2], pos, speed);
        Assert.Multiple(() =>
        {
            Assert.That(signals[0], Is.EqualTo(0.292893219f));
            Assert.That(signals[2], Is.EqualTo(0));
            Assert.That(signals[1], Is.EqualTo(0.5f));
        });
    }
}
