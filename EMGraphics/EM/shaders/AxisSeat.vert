﻿#version 330 core

uniform mat4 mvp;

in vec3 inPosition;

void main()
{
	gl_Position = mvp * vec4(inPosition, 1.0);
}