#version 150 core

in GS_FS_VERTEX
{
	float isHighlight;
    vec3 normal;
} fragment_in;

uniform vec3 ambientLight;
uniform vec3 directionalLightColor;
uniform vec3 directionalLightDirection;
uniform vec3 halfVector;
uniform float shininess;
uniform float strength;
uniform bool useLineColor;
uniform vec3 highlightColor;
uniform vec3 regularColor;

in float isHighlight;

out vec4 outColor;

void main(void)
{
	if (useLineColor)
	{
		outColor = vec4(1, 1, 1, 1);
	}
	else 
	{
		if (fragment_in.isHighlight > 0.0)
		{
			outColor = vec4(highlightColor, 1.0);
		}
		else// not highlight.
		{
			float diffuse = max(0.0, dot(fragment_in.normal, directionalLightDirection));
			
			vec3 scatteredLight = ambientLight + directionalLightColor * diffuse;
			if (strength == (0.0))
			{
				outColor = vec4(1.0);
			}
			else
			{	
				vec3 rgb = min(regularColor * scatteredLight, vec3(1.0));
				outColor = vec4(rgb, 1.0);
			}
		}
	}
}

