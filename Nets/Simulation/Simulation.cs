using System.Diagnostics.Contracts;

namespace Nets.Simulation;

public class Simulation
{
    private World _world;
    public const float BirdRange = 0.1f;
    private Random _random;

    public Simulation(World world)
    {
        _world = world;
        _random = new Random();
    }

    public void Step()
    {
        ProcessCollisions();
        foreach (var bird in _world.Birds)
        {
            bird.ProcessBrain(_world.Foods);
            bird.Move();
        }
    }

    private void ProcessCollisions()
    {
        foreach (var bird in _world.Birds)
        {
            foreach (var food in _world.Foods)
            {
                if ((bird.Position - food.Position).Length() < BirdRange)
                {
                    bird.Fitness += 1;
                    food.Position.X = _random.NextSingle() * _world.Width;
                    food.Position.Y = _random.NextSingle() * _world.Height;
                }
            }
        }
    }
}