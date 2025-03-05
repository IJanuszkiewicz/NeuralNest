using System.Numerics;
using Nets.GeneticAlgorithm;

namespace Nets.Simulation;
using Nets.Network;

public class Bird : IIndividual
{
    private Network _brain;
    Vector2 _position;
    Vector2 _velocity;
    float _maxAcceleration;

    public Bird(Network brain, Vector2 position, Vector2 velocity, float maxAcceleration)
    {
        _brain = brain;
        _position = position;
        _velocity = velocity;
        _maxAcceleration = maxAcceleration;
    }
    
    
    public float Fitness => throw new NotImplementedException();

    // TODO: convert neural network to chromosome
    public Chromosome Chromosome => throw new NotImplementedException(); 
    
    // TODO: create bird from chromosome
    public static IIndividual FromChromosome(Chromosome chromosome)
    {
        throw new NotImplementedException();
    }
}