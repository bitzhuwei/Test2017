#version 150 core

in vec3 inPosition;

out VS_GS_VERTEX
{
	float isHighlight;
} vertex_out;

uniform int highlightIndex;

void main(void)
{
    gl_Position = vec4(inPosition, 1.0f);

	if (highlightIndex == -2)
	{
		vertex_out.isHighlight = -1.0;
	}
	else if (highlightIndex == -1 // all highlight.
		|| gl_VertexID == highlightIndex + 0 // specified highlight.
		|| gl_VertexID == highlightIndex + 1 // specified highlight.
		|| gl_VertexID == highlightIndex + 2)// specified highlight.
	{
		vertex_out.isHighlight = 1.0;
	}
	else// not highlight.
	{
		vertex_out.isHighlight = -1.0;
	}
}

