#version 330 core

in bool passColor;// true for blue; false for red.

out vec4 outColor;

void main()
{
	if (passColor)
	{
		outColor = vec4(0.0, 0.0, 1.0, 1.0);
	}
	else 
	{
		outColor = vec4(1.0, 0.0, 0.0, 1.0);
	}
}