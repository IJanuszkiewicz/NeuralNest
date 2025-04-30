using OpenTK.Graphics.OpenGL4;

namespace Nets.Visualisation.Models;

public abstract class Model(Shader shader)
{
    protected readonly Shader Shader = shader;

    protected int VertexArrayObject;
    protected int VertexBufferObject;

    protected void Init()
    {
        VertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(VertexArrayObject);

        VertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
        var vertices = GetVertices();
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices,
            BufferUsageHint.DynamicDraw
        );
    }
    
    public void UpdateVertices()
    {
        GL.BindVertexArray(VertexArrayObject);
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
        IntPtr ptr = GL.MapBuffer(BufferTarget.ArrayBuffer, BufferAccess.WriteOnly);
        if (ptr == IntPtr.Zero)
        {
            throw new Exception("Failed to map buffer");
        }
        float[] newVertices = GetVertices();
        System.Runtime.InteropServices.Marshal.Copy(newVertices, 0, ptr, newVertices.Length);
        GL.UnmapBuffer(BufferTarget.ArrayBuffer);
    }

    public abstract void Draw();

    protected abstract float[] GetVertices();
}