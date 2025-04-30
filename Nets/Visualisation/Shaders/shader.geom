#version 460 core

layout(points) in;
layout(triangle_strip, max_vertices = 3) out;

in vec2 vVel[];

uniform float size;

void main()
{
    vec4 center = gl_in[0].gl_Position;
    float size = size;
    
    vec2 velocity = vVel[0];
    float angle = atan(velocity.x, velocity.y);
    
    mat2 rot = mat2(
    cos(angle), -sin(angle),
    sin(angle),  cos(angle)
    );
    
    vec2 p0 = rot * vec2(0.0,  size*1.5);
    vec2 p1 = rot * vec2(-size, -size);
    vec2 p2 = rot * vec2( size, -size);

    gl_Position = center + vec4(p0, 0.0, 0.0);
    EmitVertex();

    gl_Position = center + vec4(p1, 0.0, 0.0);
    EmitVertex();

    gl_Position = center + vec4(p2, 0.0, 0.0);
    EmitVertex();

    EndPrimitive();
}