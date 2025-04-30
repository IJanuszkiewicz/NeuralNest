using System.Diagnostics.Contracts;
using System.Numerics;
using Nets.GeneticAlgorithm.CrossoverMethods;
using Nets.GeneticAlgorithm.MutationMethods;
using Nets.GeneticAlgorithm.SelectionMethods;

namespace Nets.Simulation;

public class Simulation
{
    public World World { get; }
    public const float BirdRange = 0.1f;
    private Random _random;
    private GeneticAlgorithm.GeneticAlgorithm _geneticAlgorithm;
    private SimulationParameters _simulationParameters;

    public Simulation(SimulationParameters parameters)
    {
        World = new World(parameters);
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
        var fitnesses  = new float[World.Birds.Length];
        for (var i = 0; i < World.Birds.Length; i++)
        {
            fitnesses[i] = World.Birds[i].Fitness;
        }

        lock (this)
        {
            World.Birds = _geneticAlgorithm.Evolve<Bird>(World.Birds);
            foreach (var bird in World.Birds)
            {
                var (pos, vel) = GetRandomPosition();
                bird.Position = pos;
                bird.Velocity = vel;
            }
        }

        return new GenerationStats(fitnesses);
    }

    public GenerationStats[] Run()
    {
        var allStats = new GenerationStats[_simulationParameters.NumGenerations];
        for (int i = 0; i < _simulationParameters.NumGenerations; i++)
        {
            for (int j = 0; j < _simulationParameters.GenerationDuration; j++)
            {
                Step();
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
                
                // Draw here
            }
            allStats[i] = NewGeneration();
            Console.WriteLine($"Generation {i} stats: {allStats[i]}");
        }
        return allStats;
    }

    private (Vector2, Vector2) GetRandomPosition()
    {
        var position = new Vector2(_random.NextSingle() * World.Width, _random.NextSingle() * World.Height);
        var randSpeed = new Vector2((float)_random.NextDouble(), (float)_random.NextDouble());
        randSpeed = Vector2.Normalize(randSpeed) * 
                    (_random.NextSingle() * (_simulationParameters.MaxSpeed - _simulationParameters.MinSpeed) + 
                     _simulationParameters.MinSpeed);
        return (position, randSpeed);
    }

    public void Step()
    {
        ProcessCollisions();
        Parallel.ForEach(World.Birds, bird =>
        {
            bird.ProcessBrain(World.Foods);
            lock (this)
            {
                bird.Move();
            }
        });
    }

    private void ProcessCollisions()
    {
        foreach (var bird in World.Birds)
        {
            // The world is a donut
            if (bird.Position.X > World.Width)
            {
                bird.Position.X -= World.Width;
            }

            if (bird.Position.X < 0)
            {
                bird.Position.X += World.Width;
            }

            if (bird.Position.Y > World.Height)
            {
                bird.Position.Y -= World.Height;
            }

            if (bird.Position.Y < 0)
            {
                bird.Position.Y += World.Height;
            }
                
            foreach (var food in World.Foods)
            {
                if ((bird.Position - food.Position).Length() < BirdRange)
                {
                    bird.Fitness += 1;
                    food.Position.X = _random.NextSingle() * World.Width;
                    food.Position.Y = _random.NextSingle() * World.Height;
                }
            }
        }
    }
}