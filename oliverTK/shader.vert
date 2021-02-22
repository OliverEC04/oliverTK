#version 330 core
layout (location = 0) in vec3 vPosition;

void main()
{
    gl_Position = vec4(vPosition * 2 + 0.25f, 1);
}