#version 330 core
in vec2 fTexCoords;

out vec4 Color;

uniform sampler2D uTexture;

void main()
{
    Color = texture(uTexture, fTexCoords);
//    Color = vec4(fTexCoords, 0, 1);
}