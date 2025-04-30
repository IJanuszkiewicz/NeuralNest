using System.Diagnostics;
using System.Numerics;

namespace Nets.Simulation;

public class Eye
{
    private readonly float _fov;
    private readonly float _range;
    public readonly uint NumReceptors;

    public Eye(float fov, float range, uint numReceptors)
    {
        // needed for AngleBetween to work correctly
        Debug.Assert(fov < Math.PI);
        Debug.Assert(fov > 0);
        
        Debug.Assert(range > 0);
        
        _fov = fov;
        _range = range;
        NumReceptors = numReceptors;
    }

    public float[] Process(Food[] foods, Vector2 position, Vector2 speed)
    {
        var signals = new float[NumReceptors];
        
        var leftVector = Vector2.Transform(speed, Matrix3x2.CreateRotation(_fov/2));
        foreach (var food in foods)
        {
            var relativePos = food.Position - position;
            
            // skipping food the eye can't see
            if (relativePos.Length() > _range) continue;
            if (AngleBetween(speed, relativePos) > _fov/2) continue;
            
            int receptor = (int)Math.Min(Math.Floor(AngleBetween(leftVector, relativePos)/_fov  * NumReceptors), NumReceptors - 1);
            float energy = (_range - relativePos.Length())/_range;
            
            signals[receptor] += energy;
        }
        return signals;
    }

    private float AngleBetween(Vector2 a, Vector2 b)
    {
        var dot = Vector2.Dot(Vector2.Normalize(a), Vector2.Normalize(b));

        return (float)Math.Acos(dot);
    }
}