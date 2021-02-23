#version 330 core
layout (location = 0) in vec3 vPosition;

#define SCALE 1.5f

void main()
{
    gl_Position = vec4(vPosition * SCALE, 1);
}