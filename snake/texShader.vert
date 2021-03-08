#version 330 core
layout (location = 0) in vec2 vPosition;

out vec2 fTexCoords;

uniform vec2 uPosition;
uniform vec2 uSize;
uniform vec2 uTexRepeat;

void main()
{
    vec2 relativePos = vPosition * uSize + uPosition * 2;
    gl_Position = vec4(relativePos.x - 1, -relativePos.y + 1, 0, 1);
    fTexCoords = vec2((vPosition.x + 1) / 2 * uTexRepeat.x, (vPosition.y + 1) / 2 * uTexRepeat.y);
}