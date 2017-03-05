#version 150 core

in GS_FS_VERTEX
{
	float isHighlight;
    vec3 normal;
} fragment_in;

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
		outColor = vec4(regularLineColor, 1);
	}
	else 
	{
		float diffuse = max(0.0, dot(fragment_in.normal, -directionalLightDirection));
		vec3 scatteredLight = ambientLight + directionalLightColor * diffuse;
		vec3 rgb;
		if (fragment_in.isHighlight > 0.0)
		{
			rgb = min(highlightColor * scatteredLight, vec3(1.0));
		}
		else
		{
			rgb = min(regularColor * scatteredLight, vec3(1.0));
		}

		outColor = vec4(rgb, 1.0);
	}
}

