#version 150 core

in vec3 inPosition;
in vec3 inCloudColor;

out float isHighlight;
out vec3 cloudColor;

uniform mat4 mvpMatrix;
uniform int highlightIndex0;
uniform int highlightIndex1;
uniform int highlightIndex2;

void main(void)
{
    gl_Position = mvpMatrix * vec4(inPosition, 1.0f);

	if (highlightIndex0 == -2)
	{
		isHighlight = -1.0;
	}
	else if (highlightIndex0 == -1 // all highlight.
		|| gl_VertexID == highlightIndex0 // specified highlight.
		|| gl_VertexID == highlightIndex1 // specified highlight.
		|| gl_VertexID == highlightIndex2)// specified highlight.
	{
		isHighlight = 1.0;
	}
	else// not highlight.
	{
		isHighlight = -1.0;
	}

	cloudColor = inCloudColor;
}

