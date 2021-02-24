#version 330 core
layout (location = 0) in vec3 vPosition;
layout (location = 1) in vec3 vColor;

out vec3 fColor;

uniform vec3 uColor = vec3(1.0f);
uniform float uScale = 1;

void main()
{
    gl_Position = vec4(vPosition * uScale, 1);
    fColor = vColor * uColor;
}