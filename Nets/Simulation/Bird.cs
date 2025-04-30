using System.Diagnostics;
using System.Numerics;
using Nets.GeneticAlgorithm;

namespace Nets.Simulation;
using Network;

public class Bird : IIndividual
{
    private Network _brain;
    private Vector2 _position;
    private Vector2 _velocity;
    private Eye _eye;
    
    public Vector2 Position => _position;

    public const float MaxSpeed = 1f;
    
    // U need speed to fly ʕ•ᴥ•ʔ
    public const float MinSpeed = 0.01f;

    public Bird(Network brain, Vector2 position, Vector2 velocity, Eye eye)
    {
        // Brain only input is vision, so the size must match
        Debug.Assert(brain.Topology.InputSize == Eye.NumReceptors);
        
        // Only output we need is rotation and acceleration
        Debug.Assert(brain.Topology.OutputSize == 2);
        
        _brain = brain;
        _position = position;
        _velocity = velocity;
        _eye = eye;
    }

    public void ProcessBrain(Food[] foods)
    {
        var vision = _eye.Process(foods, _position, _velocity);
        var thought = _brain.Propagate(vision);
        var speedChange = thought[1];
        var angleChange = thought[0];

        _velocity = Vector2.Normalize(_velocity) * float.Clamp(_velocity.Length() + speedChange, MinSpeed, MaxSpeed);
        _velocity = Vector2.Transform(_velocity, Matrix3x2.CreateRotation(angleChange));
    }

    public void Move()
    {
         _position += _velocity;
    }


    public float Fitness { get; set; }
    
    public Genome Genome
    {
        get
        {
            var genomeList = new List<float>();
            foreach (var layer in _brain.Layers)
            {
                foreach (var neuron in layer.Neurons)
                {
                    genomeList.Add(neuron.Bias);
                    foreach (var weight in neuron.Weights)
                    {
                        genomeList.Add(weight);
                    }
                }
            }
            return new Genome(genomeList.ToArray());
        }
    }
    
    public IIndividual MakeChild(Genome genome)
    {
        var layers = new List<Layer>();
        int geneIndex = 0;
        foreach (var layer in _brain.Layers)
        {
            var neurons = new List<Neuron>();
            foreach (var neuron in layer.Neurons)
            {
                float bias = genome.Genes[geneIndex++];
                float[] weights = new float[neuron.Weights.Length];
                for (int k = 0; k < weights.Length; k++)
                {
                    weights[k] = genome.Genes[geneIndex++];
                }
                neurons.Add(new Neuron(bias, weights));
            }
            layers.Add(new Layer(neurons.ToArray()));
        }
        var childBrain = new Network(layers.ToArray(), _brain.Topology);
        return new Bird(childBrain, _position, _velocity, _eye);
    }
}