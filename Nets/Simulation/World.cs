namespace Nets.Simulation;

public class World
{
    private Bird[] _birds;
    private Food[] _foods;
    private float _width;
    private float _height;

    public World(float width, float height, Bird[] birds, Food[] foods)
    {
        _width = width;
        _height = height;
        _birds = birds;
        _foods = foods;
    }
    

}