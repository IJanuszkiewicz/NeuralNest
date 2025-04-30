using Nets.Network;

namespace Nets.Simulation;

public class SimulationParameters(
    NetworkTopology networkTopology,
    uint numBirds,
    uint numFoods,
    uint worldWidth,
    uint worldHeight,
    float eyeFov,
    float eyeRange,
    float maxSpeed,
    float minSpeed,
    uint generationDuration, 
    uint numGenerations, 
    uint numReceptors,
    float gaussianMutationProbability,
    float gaussianMutationStrength)

{
    public NetworkTopology NetworkTopology = networkTopology;
    public uint NumBirds = numBirds;
    public uint NumFoods = numFoods;
    public uint WorldWidth = worldWidth;
    public uint WorldHeight = worldHeight;
    public float EyeFov = eyeFov;
    public float EyeRange = eyeRange;

    public float MaxSpeed = maxSpeed;
    public float MinSpeed = minSpeed;
    public uint GenerationDuration = generationDuration; //duration in ticks
    public uint NumGenerations = numGenerations;
    public uint NumReceptors = numReceptors;
    public float GaussianMutationProbability = gaussianMutationProbability;
    public float GaussianMutationStrength = gaussianMutationStrength;
}