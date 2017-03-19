#version 150 core

in float passTexCoord;

out vec4 out_Color;

uniform sampler1D codedColorSampler;

void main(void) 
{
	vec4 color = texture(codedColorSampler, passTexCoord);
	out_Color = color;
}
