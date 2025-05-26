using Nets.Visualisation.Models;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Nets.Visualisation;

public class Visualisation : GameWindow
{
    private readonly Simulation.Simulation _simulation;
    private readonly Shader _birdShader;
    private readonly Shader _foodShader;
    
    private readonly Birds _birds;
    private readonly Foods _foods;
    
    public Visualisation(Simulation.Simulation simulation) : base(GameWindowSettings.Default,
        new NativeWindowSettings
        {
            ClientSize = (Config.WindowWidth, Config.WindowHeight), Title = Config.WindowTitle, NumberOfSamples = 4
        }
    )
    {
        _simulation = simulation;
        _birdShader = new Shader("../../../Visualisation/Shaders/shader.vert", "../../../Visualisation/Shaders/shader.frag", "../../../Visualisation/Shaders/shader.geom");
        _birdShader.SetVector3("color", new Vector3(1.0f, 0f, 0f));
        _birdShader.SetFloat("worldWidth", simulation.World.Width);
        _birdShader.SetFloat("worldHeight", simulation.World.Height);
        _birdShader.SetFloat("size", 0.01f);
        
        _foodShader = new Shader("../../../Visualisation/Shaders/shader.vert", "../../../Visualisation/Shaders/shader.frag");
        _foodShader.SetVector3("color", new Vector3(1.0f, 1.0f, 1.0f));
        _foodShader.SetFloat("worldWidth", simulation.World.Width);
        _foodShader.SetFloat("worldHeight", simulation.World.Height);
        _foodShader.SetFloat("size", 5.0f);
        
        lock (_simulation)
        {
            _birds = new Birds(_birdShader, _simulation.World);
            _foods = new Foods(_foodShader, _simulation.World);
        }
    }
    
    protected override void OnLoad()
    {
        base.OnLoad();
        GL.Enable(EnableCap.Multisample);
        GL.Enable(EnableCap.ProgramPointSize);
        GL.ClearColor(0, 0, 0, 1.0f);
    }
    
    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);
        
        if (KeyboardState.IsKeyDown(Keys.Escape))
            Close();

        if (KeyboardState.IsKeyDown(Keys.Up) || KeyboardState.IsKeyDown(Keys.W))
        {
            lock (_simulation)
            {
                _simulation.StepWaitTime = Math.Max(0.0, _simulation.StepWaitTime - 5);
            }
        }
        
        if (KeyboardState.IsKeyDown(Keys.Down) || KeyboardState.IsKeyDown(Keys.S))
        {
            lock (_simulation)
            {
                _simulation.StepWaitTime += 5;
            }
        }

        if (KeyboardState.IsKeyDown(Keys.Right))
        {
            if (_simulation.StepWaitTime > 0)
            {
                lock (_simulation)
                {
                    _simulation.StepWaitTime = 0;
                }
            }
        }
        else
        {
            lock (_simulation)
            {
                _simulation.StepWaitTime = 10000;
            }
        }
    }
    
    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        
        GL.Clear(ClearBufferMask.ColorBufferBit);
        
        lock (_simulation)
        {
            _birds.UpdateVertices();
            _foods.UpdateVertices();
        }
        
        _birds.Draw();
        _foods.Draw();

        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
    {
        base.OnFramebufferResize(e);

        GL.Viewport(0, 0, e.Width, e.Height);
    }
    
    protected override void OnUnload()
    {
        base.OnUnload();
        _birdShader.Dispose();
        _foodShader.Dispose();
    }
}