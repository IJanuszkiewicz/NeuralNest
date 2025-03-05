namespace NetsTest;
using Nets.Network;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void NeuronPropagate()
    {
        var neuron = new Neuron(-0.1f, [1f, 0, 0.5f, 1f]);
        
        Assert.That(neuron.Propagate([1f,1f,1f,1f]), Is.EqualTo(2.4f).Within(1e-10f));
    }

    [Test]
    public void LayerPropagate()
    {
        var neurons = new Neuron[]
        {
            new Neuron(-0.1f, [1f, 0, 0.5f, 1f]), 
            new Neuron(-1f, [0.5f,0.5f, 0.5f, 0.5f])
        };
        
        var layer = new Layer(neurons);

        var output = layer.Propagate([1f, 1f, 1f, 1f]);
        
        Assert.That(output[0], Is.EqualTo(2.4f).Within(1e-10f));
        Assert.That(output[1], Is.EqualTo(1f).Within(1e-10f));
        
    }

    [Test]
    public void NetworkPropagate()
    {
        var network = new Network(new NetworkTopology([3, 6, 2]));

        Assert.That(network.Propagate([1f, 1f, 1f]).Length, Is.EqualTo(2));
        
    }
    
    [Test]
    public void BiggerNetworkPropagate()
    {
        
        var network = new Network(new NetworkTopology([3, 6, 20, 20, 2]));

        Assert.That(network.Propagate([1f, 1f, 1f]).Length, Is.EqualTo(2));
    }
}