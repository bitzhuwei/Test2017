#version 150 core

in vec3 passColor;

uniform bool useLineColor;

out vec4 out_Color;

void main(){
	if (useLineColor)
	{
		out_Color = vec4(1, 1, 1, 1);
	}
	else 
	{
		out_Color = vec4(passColor, 1.0f);
	}
}
