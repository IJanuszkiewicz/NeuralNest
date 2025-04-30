using Nets.Simulation;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Nets.Visualisation.Models;

public class Birds : Model
{
    private readonly World _world;
    
    public Birds(Shader shader, World world) : base(shader)
    {
        _world = world;
        
        Init();
        var attributeLocation = Shader.GetAttribLocation("pos");
        GL.EnableVertexAttribArray(attributeLocation);
        GL.VertexAttribPointer(attributeLocation, 2, VertexAttribPointerType.Float, false,
            4 * sizeof(float), 0
        );
        
        attributeLocation = Shader.GetAttribLocation("vel");
        GL.EnableVertexAttribArray(attributeLocation);
        GL.VertexAttribPointer(attributeLocation, 2, VertexAttribPointerType.Float, false,
            4 * sizeof(float), 2 * sizeof(float)
        );
    }
    
    public override void Draw()
    {
        GL.BindVertexArray(VertexArrayObject);
        Shader.Use();
        GL.DrawArrays(PrimitiveType.Points, 0, _world.Birds.Length * 4);
    }
    
    protected override float[] GetVertices()
    {
        var vertices = new List<float>();
        foreach (var bird in _world.Birds)
        {
            vertices.Add(bird.Position.X);
            vertices.Add(bird.Position.Y);
            vertices.Add(bird.Velocity.X);
            vertices.Add(bird.Velocity.Y);
        }
        return vertices.ToArray();
    }
}