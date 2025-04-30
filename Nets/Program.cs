using Nets.Network;
using Nets.Simulation;

namespace Nets;

class Program
{
    static void Main(string[] args)
    {
        var parameters = new SimulationParameters(
            new NetworkTopology([5, 7, 2]),
            40,
            70, 
            100, 
            100, 
            (float)Math.PI /7, 
            20, 
            0.5f, 
            0.001f,
            3000, 
            50, 
            5,
            0.5f,
            0.1f
        );
        
        var sim = new Simulation.Simulation(parameters);
        
        var simulationThread = new Thread(() => sim.Run());
        simulationThread.Start();
        
        var gui = new Visualisation.Visualisation(500, 500, "Nets", sim);
        gui.Run();
        Environment.Exit(1);
    }
}