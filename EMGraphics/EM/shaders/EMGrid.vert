#version 150 core

in vec3 inPosition;
in vec3 inNormal;
in vec3 cloudColor;

out VS_GS_VERTEX
{
	float isHighlight;
	vec3 smoothNormal;
	vec3 cloudColor;
} vertex_out;

uniform int highlightIndex0;
uniform int highlightIndex1;
uniform int highlightIndex2;

void main(void)
{
    gl_Position = vec4(inPosition, 1.0f);

	if (highlightIndex0 == -2)
	{
		vertex_out.isHighlight = -1.0;
	}
	else if (highlightIndex0 == -1 // all highlight.
		|| gl_VertexID == highlightIndex0 // specified highlight.
		|| gl_VertexID == highlightIndex1 // specified highlight.
		|| gl_VertexID == highlightIndex2)// specified highlight.
	{
		vertex_out.isHighlight = 1.0;
	}
	else// not highlight.
	{
		vertex_out.isHighlight = -1.0;
	}

	vertex_out.smoothNormal = inNormal;
	vertex_out.cloudColor = cloudColor;
}

