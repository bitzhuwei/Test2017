#version 330 core

uniform mat4 mvpMatrix;

in vec3 inPosition;

flat out vec3 passColor;// true for blue; false for red.

void main()
{
	gl_Position = mvpMatrix * vec4(inPosition, 1.0);

	if (gl_VertexID % 4 == 0 || gl_VertexID % 4 == 1)
	{
		passColor = vec3(0.0, 0.0, 1.0);
	}
	else
	{
		passColor = vec3(1.0, 0.0, 0.0);
	}
}