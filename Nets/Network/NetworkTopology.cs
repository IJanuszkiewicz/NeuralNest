namespace Nets.Network;

// first size is input size, last is output size
public record NetworkTopology(int[] LayerSizes)
{
    public int InputSize => LayerSizes.First();
    public int OutputSize => LayerSizes.Last();
}