namespace Nets.Simulation;

public class GenerationStats
{
    public float[] Fitnesses;

    public GenerationStats(float[] fitnesses)
    {
        Fitnesses = fitnesses;
    }

    public override string ToString()
    {
        var min = Fitnesses[0];
        var max = Fitnesses[0];
        float sum = 0;
        foreach (var fitness in Fitnesses)
        {
            sum += fitness;
            if(fitness < min) min = fitness;
            if(fitness > max) max = fitness;
        }
        
        return $"Max: {max}, Min: {min}, Total: {sum}, Average: {sum/Fitnesses.Length}";
    }
}