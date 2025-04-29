namespace Nets.Network;

public class Layer
{
    public Neuron[] Neurons { get; }

    public Layer(int inputSize, int outputSize)
    {
        Neurons = new Neuron[outputSize];
        for (int i = 0; i < outputSize; i++)
        {
            Neurons[i] = new Neuron(inputSize);
        }
    }

    public Layer(Neuron[] neurons)
    {
        Neurons = neurons;
    }

    public float[] Propagate(float[] input)
    {
        var output = new float[Neurons.Length];
        for (int i = 0; i < Neurons.Length; i++)
        {
            output[i] = Neurons[i].Propagate(input);
        }
        return output;
    }
}