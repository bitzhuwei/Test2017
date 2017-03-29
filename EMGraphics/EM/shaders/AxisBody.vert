#version 330 core

uniform mat4 mvp;

in vec3 inPosition;
in vec3 inColor;

out vec3 passColor;

void main()
{
	gl_Position = mvp * vec4(inPosition, 1.0);

	passColor = inColor;
}