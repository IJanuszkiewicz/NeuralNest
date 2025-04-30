using Nets.Simulation;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Nets.Visualisation.Models;

public class Foods : Model
{
    private readonly World _world;
    
    public Foods(Shader shader, World world) : base(shader)
    {
        _world = world;
        
        Init();
        var attributeLocation = Shader.GetAttribLocation("pos");
        GL.EnableVertexAttribArray(attributeLocation);
        GL.VertexAttribPointer(attributeLocation, 2, VertexAttribPointerType.Float, false,
            2 * sizeof(float), 0
        );
    }
    
    public override void Draw()
    {
        GL.BindVertexArray(VertexArrayObject);
        Shader.Use();
        GL.DrawArrays(PrimitiveType.Points, 0, _world.Foods.Length * 2);
    }
    
    protected override float[] GetVertices()
    {
        var vertices = new List<float>();
        foreach (var food in _world.Foods)
        {
            vertices.Add(food.Position.X);
            vertices.Add(food.Position.Y);
        }
        return vertices.ToArray();
    }
}