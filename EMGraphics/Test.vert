#version 150 core

in vec3 inPosition;
in vec3 inColor;

out vec3 passColor;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;

void main()
{
	gl_Position = projection * view * model * vec4(inPosition, 1.0f);

	passColor = inColor;
}
