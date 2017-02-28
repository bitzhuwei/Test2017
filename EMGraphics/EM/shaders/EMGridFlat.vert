#version 150 core

in vec3 inPosition;

out VS_GS_VERTEX
{
	float isHighlight;
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
}

