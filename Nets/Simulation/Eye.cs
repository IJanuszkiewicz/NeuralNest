using System.Diagnostics;
using System.Numerics;

namespace Nets.Simulation;

public class Eye
{
    private readonly float _fov;
    private readonly float _range;
    public const int NumReceptors = 5;

    public Eye(float fov, float range)
    {
        // needed for AngleBetween to work correctly
        Debug.Assert(fov < Math.PI);
        Debug.Assert(fov > 0);
        
        Debug.Assert(range > 0);
        
        _fov = fov;
        _range = range;
    }

    public float[] Process(Food[] foods, Vector2 position, Vector2 speed)
    {
        var leftVector = Vector2.Transform(speed, Matrix3x2.CreateRotation(_fov/2));
        foreach (var food in foods)
        {
            var relativePos = food.Position - position;
            
            // skipping food the eye can't see
            if (relativePos.Length() > _range) continue;
            if (AngleBetween(speed, relativePos) > _fov/2) continue;
            
            
        }
    }

    private float AngleBetween(Vector2 a, Vector2 b)
    {
        var dot = Vector2.Dot(Vector2.Normalize(a), Vector2.Normalize(b));

        return (float)Math.Acos(dot);
    }
}