using System.Numerics;

namespace Nets.Simulation;

public class Food
{
    private Vector2 _position;
    public Vector2 Position => _position;

    public Food(Vector2 position)
    {
        _position = position;
    }
}