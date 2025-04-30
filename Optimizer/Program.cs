
using Nets.Network;
using Nets.Simulation;

class Optimizer
{
    public static void Main(string[] args)
    {
        float[] eyeRanges = [10, 50];
        float[] eyeFovs = [(float)Math.PI/7, (float)Math.PI/2, (float)Math.PI *0.9f];
        float[] mutProps = [0.01f, 0.1f, 0.3f, 0.5f];
        float[] mutStrengths = [0.1f, 0.3f, 0.5f, 1f];
        NetworkTopology[] topologies =
        [
            new NetworkTopology([5, 2]),
            new NetworkTopology([5, 7, 2]),
            new NetworkTopology([5, 7, 7, 2]),
        ];

        (float, float, float, float, NetworkTopology, float) best = (0, 0, 0, 0, new NetworkTopology([5, 2]), 0);
        foreach (var range in eyeRanges)
        {
            foreach (var fov in eyeFovs)
            {
                foreach (var mutProp in mutProps)
                {
                    foreach (var mutStrength in mutStrengths)
                    {
                        foreach (var topology in topologies)
                        {
                            Console.WriteLine();
                            Console.Write($"Testing for: \n\tEye range: {range}\n\tfov: {fov}\n\tMutation probability: {mutProp}\n\tMutation strength: {mutStrength}\n\tTopology: ");
                            foreach (var num in topology.LayerSizes)
                            {
                                Console.Write($"{num} ");
                            }
                            Console.WriteLine();
                            var parameters = new SimulationParameters(
                                topology, 40 , 70, 100, 100, fov, range, 0.5f, 0.001f, 3000, 60, 5, mutProp, mutStrength);
                            var sim = new Simulation(parameters);
                            var stats = sim.Run();
                            if (stats.Last().Fitnesses.Sum() > best.Item6)
                            {
                                best = (range, fov, mutProp, mutStrength, topology, stats.Last().Fitnesses.Sum()); 
                            }
                        }
                    }
                }
            }
        }
        
        Console.Write($"Best parameters: \n\tEye range: {best.Item1}\n\tfov: {best.Item2}\n\tMutation probability: {best.Item3}\n\tMutation strength: {best.Item4}\n\tTopology: ");
        foreach (var num in best.Item5.LayerSizes)
        {
            Console.Write($"{num} ");
        }
        Console.WriteLine();
    }
}