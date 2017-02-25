#version 330 core

uniform vec3 ambientLight;
uniform vec3 directionalLightColor;
uniform vec3 directionalLightDirection;
uniform vec3 halfVector;
uniform float shininess;
uniform float strength;
uniform bool useLineColor;

in vec3 passNormal;
in vec3 passColor;

out vec4 outColor;

void main()
{
	if (useLineColor)
	{
		outColor = vec4(1, 1, 1, 1);
	}
	else 
	{
		float diffuse = max(0.0, dot(passNormal, directionalLightDirection));
		
		vec3 scatteredLight = ambientLight + directionalLightColor * diffuse;
		if (strength == (0.0))
		{
			outColor = vec4(1.0);
		}
		else
		{	
			vec3 rgb = min(passColor * scatteredLight, vec3(1.0));
			outColor = vec4(rgb, 1.0);
		}
	}
}