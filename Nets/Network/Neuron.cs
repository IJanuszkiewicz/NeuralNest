namespace Nets.Network;

public class Neuron
{
    public float Bias { get; }
    public float[] Weights { get; }

    public Neuron(float bias, float[] weights)
    {
        Weights = weights;
        Bias = bias;
    }

    public Neuron(int inputSize)
    {
        var random = new Random();
        Bias = random.NextSingle()*2 - 1;
        Weights = new float[inputSize];
        for (int i = 0; i < Weights.Length; i++)
        {
            Weights[i] = random.NextSingle()*2 - 1;
        }
    }

    public float Propagate(float[] inputs)
    {
        float output = 0;
        for (int i = 0; i < inputs.Length; i++)
        {
            output += inputs[i] * Weights[i];
        }
        
        // return (float)Math.Tanh(output + Bias);
        return float.Max(output + Bias, 0f);
    }
    
}