using System.Diagnostics.Contracts;
using System.Numerics;
using Nets.GeneticAlgorithm.CrossoverMethods;
using Nets.GeneticAlgorithm.MutationMethods;
using Nets.GeneticAlgorithm.SelectionMethods;

namespace Nets.Simulation;

public class Simulation
{
    private World _world;
    public const float BirdRange = 0.1f;
    private Random _random;
    private GeneticAlgorithm.GeneticAlgorithm _geneticAlgorithm;
    private SimulationParameters _simulationParameters;

    public Simulation(SimulationParameters parameters)
    {
        _world = new World(parameters);
        _random = new Random();
        _geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(
            new ProportionalSelection(), 
            new UniformCrossover(),
            new GaussianMutation(parameters.GaussianMutationProbability, parameters.GaussianMutationStrength)
            );
        _simulationParameters = parameters;
    }

    public GenerationStats NewGeneration()
    {
        var fitnesses  = new float[_world.Birds.Length];
        for (var i = 0; i < _world.Birds.Length; i++)
        {
            fitnesses[i] = _world.Birds[i].Fitness;
        }
        
        _world.Birds = _geneticAlgorithm.Evolve<Bird>(_world.Birds);
        foreach (var bird in _world.Birds)
        {
            var (pos, vel) = GetRandomPosition();
            bird.Position = pos;
            bird.Velocity = vel;
        }
        return new GenerationStats(fitnesses);
    }

    public void Run()
    {
        for (int i = 0; i < _simulationParameters.NumGenerations; i++)
        {
            for (int j = 0; j < _simulationParameters.GenerationDuration; j++)
            {
                Step();
                // Thread.Sleep(TimeSpan.FromMilliseconds(1000));
                
                // Draw here
            }
            var stats = NewGeneration();
            Console.WriteLine($"Generation {i} stats: {stats}");
        }
    }

    private (Vector2, Vector2) GetRandomPosition()
    {
        var position = new Vector2(_random.NextSingle() * _world.Width, _random.NextSingle() * _world.Height);
        var randSpeed = new Vector2((float)_random.NextDouble(), (float)_random.NextDouble());
        randSpeed = Vector2.Normalize(randSpeed) * 
                    (_random.NextSingle() * (_simulationParameters.MaxSpeed - _simulationParameters.MinSpeed) + 
                     _simulationParameters.MinSpeed);
        return (position, randSpeed);
    }

    public void Step()
    {
        ProcessCollisions();
        Parallel.ForEach(_world.Birds, bird =>
        {
            bird.ProcessBrain(_world.Foods);
            bird.Move();
        });
    }

    private void ProcessCollisions()
    {
        foreach (var bird in _world.Birds)
        {
            // The world is a donut
            if (bird.Position.X > _world.Width)
            {
                bird.Position.X -= _world.Width;
            }

            if (bird.Position.X < 0)
            {
                bird.Position.X += _world.Width;
            }

            if (bird.Position.Y > _world.Height)
            {
                bird.Position.Y -= _world.Height;
            }

            if (bird.Position.Y < 0)
            {
                bird.Position.Y += _world.Height;
            }
                
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