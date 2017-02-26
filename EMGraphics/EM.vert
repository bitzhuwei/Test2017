#version 330 core

uniform mat4 mvpMatrix;
uniform mat3 normalMatrix;
uniform int highlightIndex;
uniform vec3 highlightColor;

in vec3 inPosition;
in vec3 inColor;
in vec3 inNormal;

out vec3 passNormal;
out vec3 passColor;
out float isHighlight;

void main()
{
	passNormal = normalize(normalMatrix * inNormal);

	if (highlightIndex == -2)
	{
		passColor = inColor;
		isHighlight = -1.0;
	}
	else if (highlightIndex == -1 // all highlight.
		|| gl_VertexID == highlightIndex + 0 // specified highlight.
		|| gl_VertexID == highlightIndex + 1 // specified highlight.
		|| gl_VertexID == highlightIndex + 2)// specified highlight.
	{
		passColor = highlightColor;
		isHighlight = 1.0;
	}
	else// not highlight.
	{
		passColor = inColor;
		isHighlight = -1.0;
	}

	gl_Position = mvpMatrix * vec4(inPosition, 1.0);
}