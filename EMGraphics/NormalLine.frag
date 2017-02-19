#version 330 core

flat in vec3 passColor;// true for blue; false for red.

out vec4 outColor;

void main()
{
	outColor = vec4(passColor, 1.0);
}