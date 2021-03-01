#version 330 core
in vec2 fTexCoords;

out vec4 Color;

uniform sampler2D uTexture0;
uniform sampler2D uTexture1;

void main()
{
    Color = mix(texture(uTexture0, fTexCoords), texture(uTexture1, fTexCoords), 0.5f);
}