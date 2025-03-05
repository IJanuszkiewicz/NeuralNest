namespace Nets.Simulation;

public class World
{
    public Bird[] Birds;
    public Food[] Foods;
    public float Width;
    public float Height;

    public World(float width, float height, Bird[] birds, Food[] foods)
    {
        Width = width;
        Height = height;
        Birds = birds;
        Foods = foods;
    }
    

}