namespace Nets.Network;

public class Network
{
    private Layer[] _layers;
    public readonly NetworkTopology Topology;

    public Network(Layer[] layers)
    {
        _layers = layers;
    }

    public float[] Propagate(float[] input)
    {
        float[] output = input;
        foreach (Layer layer in _layers)
        {
            output = layer.Propagate(output);
        }

        return output;
    }

    public Network(NetworkTopology topology)
    {
        var layers = new List<Layer>();
        for (int i = 1; i < topology.LayerSizes.Length; i++)
        {
            layers.Add(new Layer(topology.LayerSizes[i-1], topology.LayerSizes[i]));
        }
        _layers = layers.ToArray();
        Topology = topology;
    }
}