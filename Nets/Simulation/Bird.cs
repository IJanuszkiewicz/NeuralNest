using System.Diagnostics;
using System.Numerics;
using Nets.GeneticAlgorithm;

namespace Nets.Simulation;
using Network;

public class Bird : IIndividual
{
    private Network _brain;
    public Vector2 Position;
    public Vector2 Velocity;
    private Eye _eye;
    
    private readonly float _maxSpeed;
    private readonly float _minSpeed;

    public Bird(Network brain, Vector2 position, Vector2 velocity, Eye eye, float maxSpeed, float minSpeed)
    {
        // Brain only input is vision, so the size must match
        Debug.Assert(brain.Topology.InputSize == eye.NumReceptors);
        
        // Only output we need is rotation and acceleration
        Debug.Assert(brain.Topology.OutputSize == 2);
        
        _brain = brain;
        Position = position;
        Velocity = velocity;
        _eye = eye;
        _maxSpeed = maxSpeed;
        _minSpeed = minSpeed;
    }

    public void ProcessBrain(Food[] foods)
    {
        var vision = _eye.Process(foods, Position, Velocity);
        var thought = _brain.Propagate(vision);
        var speedChange = thought[1];
        var angleChange = thought[0] * MathF.PI ;
        // if (Random.Shared.NextDouble() < 0.0001)
        // {
            // Console.WriteLine($"Speed change: {speedChange}, Angle change: {angleChange}, Speed: {Velocity.Length()}");
        // }

        Velocity = Vector2.Normalize(Velocity) * float.Clamp(Velocity.Length() + speedChange, _minSpeed, _maxSpeed);
        Velocity = Vector2.Transform(Velocity, Matrix3x2.CreateRotation(angleChange));
    }

    public void Move()
    {
         Position += Velocity;
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
        return new Bird(childBrain, Position, Velocity, _eye, _maxSpeed, _minSpeed);
    }
}