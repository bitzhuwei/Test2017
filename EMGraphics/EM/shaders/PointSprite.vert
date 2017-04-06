#version 150 core

uniform mat4 mvp;
uniform float labelSize = 50.0f;
in vec3 position;
flat out int vertexIndex;

void main(void)
{
	gl_Position = mvp * vec4(position, 1.0f);
	gl_PointSize = labelSize;
	vertexIndex = gl_VertexID;
}
