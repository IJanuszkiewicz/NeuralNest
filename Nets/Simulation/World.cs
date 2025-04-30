using System.Numerics;
using Nets.Network;

namespace Nets.Simulation;

public class World
{
    public Bird[] Birds;
    public Food[] Foods;
    public float Width;
    public float Height;

    public void Draw()
    {
     for (int y = 0; y < Height; y++)
             {
                 for (int x = 0; x < Width; x++)
                 {
                     if (Array.Exists(Foods, food => (food.Position - new Vector2(x, y)).Length() < 1))
                     {
                         Console.Write("."); // Food
                     }
                     else if (Array.Exists(Birds, bird => (bird.Position - new Vector2(x, y)).Length() < 1))
                     {
                         Console.Write("B"); // Bird
                     }
                     else
                     {
                         Console.Write(" "); // Empty space
                     }
                 }
                 Console.WriteLine();
             }
    }

    public World(float width, float height, Bird[] birds, Food[] foods)
    {
        Width = width;
        Height = height;
        Birds = birds;
        Foods = foods;
    }

    public World(SimulationParameters simulationParameters)
    {
        Width = simulationParameters.WorldWidth;
        Height = simulationParameters.WorldHeight;
        
        Birds = new Bird[simulationParameters.NumBirds];
        Foods = new Food[simulationParameters.NumFoods];
        
        var random = new Random();

        for (int i = 0; i < Birds.Length; i++)
        {
            var randSpeed = new Vector2((float)random.NextDouble(), (float)random.NextDouble());
            randSpeed = Vector2.Normalize(randSpeed) * 
                        (random.NextSingle() * (simulationParameters.MaxSpeed - simulationParameters.MinSpeed) + 
                         simulationParameters.MinSpeed);
            Birds[i] = new Bird(
                new Network.Network(simulationParameters.NetworkTopology), 
                new Vector2(random.NextSingle() * Width, random.NextSingle() * Height),
                randSpeed,
                    new Eye(simulationParameters.EyeFov, simulationParameters.EyeRange, simulationParameters.NumReceptors),
                simulationParameters.MaxSpeed,
                simulationParameters.MinSpeed
                );
        }

        for (int i = 0; i < Foods.Length; i++)
        {
            Foods[i] = new Food(new Vector2(random.NextSingle() * Width, random.NextSingle() * Height));
        }
        
    }


}