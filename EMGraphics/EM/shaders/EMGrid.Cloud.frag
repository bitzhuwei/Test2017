#version 150 core

in float isHighlight;
in vec3 cloudColor;

uniform bool useLineColor;
uniform vec3 highlightLineColor;
uniform vec3 regularLineColor;

out vec4 outColor;

void main(void)
{
	if (useLineColor)
	{
		if (isHighlight > 0.0)
		{
			outColor = vec4(highlightLineColor, 1.0);
		}
		else
		{
			outColor = vec4(regularLineColor, 1.0);
		}
	}
	else 
	{
		outColor = vec4(cloudColor, 1.0);
	}
}

