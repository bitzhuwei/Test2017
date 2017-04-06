#version 150 core

uniform mat4 mvp;
uniform float labelSize = 50.0f;
in vec3 position;
flat out int vertexIndex;

void main(void)
{
	vec4 pos = mvp * vec4(position, 1.0f);
	//gl_PointSize = (1.0 - pos.z / pos.w) * factor;
	gl_PointSize = labelSize;
	gl_Position = pos;
	vertexIndex = gl_VertexID;
}
