using System.Numerics;

namespace Nets.Simulation;
using Nets.Network;

public class Bird
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
}