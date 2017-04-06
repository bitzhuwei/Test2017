#version 150 core

uniform sampler2D spriteTexture0;
uniform sampler2D spriteTexture1;
uniform sampler2D spriteTexture2;
flat in int vertexIndex;

out vec4 outColor;

void main(void)
{
	//vec4 top         = texture(spriteTexture, vec2(gl_PointCoord.x, gl_PointCoord.y + 1.0 / 200.0));
	//vec4 bottom      = texture(spriteTexture, vec2(gl_PointCoord.x, gl_PointCoord.y - 1.0 / 200.0));
	//vec4 left        = texture(spriteTexture, vec2(gl_PointCoord.x - 1.0 / 300.0, gl_PointCoord.y));
	//vec4 right       = texture(spriteTexture, vec2(gl_PointCoord.x + 1.0 / 300.0, gl_PointCoord.y));
	//vec4 topLeft     = texture(spriteTexture, vec2(gl_PointCoord.x - 1.0 / 300.0, gl_PointCoord.y + 1.0 / 200.0));
	//vec4 topRight    = texture(spriteTexture, vec2(gl_PointCoord.x + 1.0 / 300.0, gl_PointCoord.y + 1.0 / 200.0));
	//vec4 bottomLeft  = texture(spriteTexture, vec2(gl_PointCoord.x - 1.0 / 300.0, gl_PointCoord.y - 1.0 / 200.0));
	//vec4 bottomRight = texture(spriteTexture, vec2(gl_PointCoord.x + 1.0 / 300.0, gl_PointCoord.y - 1.0 / 200.0));
	//vec4 sx = -topLeft - 2 * left - bottomLeft + topRight   + 2 * right  + bottomRight;
	//vec4 sy = -topLeft - 2 * top  - topRight   + bottomLeft + 2 * bottom + bottomRight;
	//outColor = sqrt(sx * sx + sy * sy);
	vec4 textureColor;
	if (vertexIndex == 0)
	{
		textureColor = texture(spriteTexture0, gl_PointCoord);
	}
	else if (vertexIndex == 1)
	{
		textureColor = texture(spriteTexture1, gl_PointCoord);
	}
	else if (vertexIndex == 2)
	{
		textureColor = texture(spriteTexture2, gl_PointCoord);
	}
	else
	{
		textureColor = vec4(0.5, 0.5, 0,1);
	}	

	if(textureColor.a <= 0.01f) { discard; }

	outColor = textureColor;
}
