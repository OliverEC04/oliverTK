#version 330 core
layout (location = 0) in vec2 vPosition;

out vec2 fTexCoords;

uniform vec2 uPosition;
uniform vec2 uSize;

void main()
{
    vec2 relativePos = vPosition * uSize + uPosition * 2;
    gl_Position = vec4(relativePos.x - 1, -relativePos.y + 1, 0, 1);
    fTexCoords = (vPosition + 1) / 2;
}