namespace Nets.Network;

public class Layer
{
    private Neuron[] _neurons;

    public Layer(int inputSize, int outputSize)
    {
        _neurons = new Neuron[outputSize];
        for (int i = 0; i < outputSize; i++)
        {
            _neurons[i] = new Neuron(inputSize);
        }
    }

    public Layer(Neuron[] neurons)
    {
        _neurons = neurons;
    }

    public float[] Propagate(float[] input)
    {
        var output = new float[_neurons.Length];
        for (int i = 0; i < _neurons.Length; i++)
        {
            output[i] = _neurons[i].Propagate(input);
        }
        return output;
    }
}