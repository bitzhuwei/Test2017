#version 150 core

in GS_FS_VERTEX
{
	float isHighlight;
    vec3 normal;
	vec3 cloudColor;
} fragment_in;

uniform vec3 ambientLight;
uniform vec3 directionalLightColor;
uniform vec3 directionalLightDirection;
uniform bool useLineColor;
uniform vec3 highlightColor;
uniform vec3 regularColor;
uniform vec3 highlightLineColor;
uniform vec3 regularLineColor;
uniform bool enableCloudColor = false;

out vec4 outColor;

void main(void)
{
	if (useLineColor)
	{
		if (fragment_in.isHighlight > 0.0)
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
		if (enableCloudColor)
		{
			outColor = vec4(fragment_in.cloudColor, 1.0);
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
}

