#version 330 core

uniform vec3 seatColor;

out vec4 outColor;

void main()
{
    if (int(gl_FragCoord.x - 0.5) % 2 == 1 && int(gl_FragCoord.y - 0.5) % 2 != 1) discard;
    if (int(gl_FragCoord.x - 0.5) % 2 != 1 && int(gl_FragCoord.y - 0.5) % 2 == 1) discard;
    
    outColor = vec4(seatColor, 1.0f);
}