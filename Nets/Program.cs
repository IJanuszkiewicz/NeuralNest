using Nets.Network;
using Nets.Simulation;

namespace Nets;

class Program
{
    static void Main(string[] args)
    {
        var parameters = new SimulationParameters(
            new NetworkTopology([7, 8, 2]),
            40,
            60, 
            140, 
            30, 
            (float)Math.PI * 0.9f, 
            20, 
            0.5f, 
            0.001f,
            3000, 
            500, 
            7,
            0.1f,
            0.3f
        );
        
        var sim = new Simulation.Simulation(parameters);
        sim.Run();
    }
}