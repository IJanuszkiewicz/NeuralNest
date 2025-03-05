namespace Nets.Network;

public class Neuron
{
    private float _bias;
    private float[] _weights;

    public Neuron(float bias, float[] weights)
    {
        _weights = weights;
        _bias = bias;
    }

    public Neuron(int inputSize)
    {
        var random = new Random();
        _bias = random.NextSingle();
        _weights = new float[inputSize];
        for (int i = 0; i < _weights.Length; i++)
        {
            _weights[i] = random.NextSingle();
        }
    }

    public float Propagate(float[] inputs)
    {
        float output = 0;
        for (int i = 0; i < inputs.Length; i++)
        {
            output += inputs[i] * _weights[i];
        }
        
        return float.Max(output + _bias, 0f);
    }
    
}