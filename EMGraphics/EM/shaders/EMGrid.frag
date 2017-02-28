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
uniform vec3 lineColor = vec3(0.8, 0.8, 0.8);

out vec4 outColor;

void main(void)
{
	if (useLineColor)
	{
		outColor = vec4(lineColor, 1);
	}
	else 
	{
		if (fragment_in.isHighlight > 0.0)
		{
			outColor = vec4(highlightColor, 1.0);
		}
		else// not highlight.
		{
			float diffuse = max(0.0, dot(fragment_in.normal, -directionalLightDirection));
			
			vec3 scatteredLight = ambientLight + directionalLightColor * diffuse;
			vec3 rgb = min(regularColor * scatteredLight, vec3(1.0));
			outColor = vec4(rgb, 1.0);
		}
	}
}

