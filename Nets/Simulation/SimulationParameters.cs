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
    public readonly NetworkTopology NetworkTopology = networkTopology;
    public readonly uint NumBirds = numBirds;
    public readonly uint NumFoods = numFoods;
    public readonly uint WorldWidth = worldWidth;
    public readonly uint WorldHeight = worldHeight;
    public readonly float EyeFov = eyeFov;
    public readonly float EyeRange = eyeRange;

    public readonly float MaxSpeed = maxSpeed;
    public readonly float MinSpeed = minSpeed;
    public readonly uint GenerationDuration = generationDuration; //duration in ticks
    public readonly uint NumGenerations = numGenerations;
    public readonly uint NumReceptors = numReceptors;
    public readonly float GaussianMutationProbability = gaussianMutationProbability;
    public readonly float GaussianMutationStrength = gaussianMutationStrength;
}