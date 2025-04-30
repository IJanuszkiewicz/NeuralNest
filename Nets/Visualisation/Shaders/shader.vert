#version 460 core
in vec2 pos;
in vec2 vel;

uniform float worldWidth;
uniform float worldHeight;
uniform float size;

out vec2 vVel;

void main()
{
    vec2 adjustedPosition = pos * vec2(1.0/(worldWidth/2), 1.0/(worldHeight/2)) - vec2(1.0, 1.0);
    gl_Position = vec4(adjustedPosition, 0.0, 1.0);
    gl_PointSize = size;
    vVel = vel;
}