namespace Nets.Network;

public class Network
{
    public Layer[] Layers { get; }
    public readonly NetworkTopology Topology;

    public Network(Layer[] layers, NetworkTopology topology)
    {
        Layers = layers;
        Topology = topology;
    }

    public float[] Propagate(float[] input)
    {
        float[] output = input;
        foreach (Layer layer in Layers)
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
        Layers = layers.ToArray();
        Topology = topology;
    }
}