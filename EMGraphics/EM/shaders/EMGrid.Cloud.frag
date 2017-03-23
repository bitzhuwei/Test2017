#version 150 core

in float isHighlight;
in vec3 normal;
in vec3 cloudColor;

uniform vec3 ambientLight;
uniform vec3 directionalLightColor;
uniform vec3 directionalLightDirection;
uniform bool useLineColor;
uniform vec3 highlightColor;
uniform vec3 regularColor;
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

