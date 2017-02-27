#version 150 core

layout (triangles) in;
layout (triangle_strip, max_vertices = 27) out;

in VS_GS_VERTEX
{
	float isHighlight;
} vertex_in[];

out GS_FS_VERTEX
{
	float isHighlight;
    vec3 normal;
} vertex_out;

uniform mat4 mvpMatrix;
uniform mat4 normalMatrix;

void main(void)
{
    //int i;
	vec3[3] vertexes;
	for (int i = 0; i < 3; i++)
	{
		vec4 position = gl_in[i].gl_Position;
		vertexes[i] = vec3(position);
		gl_Position = mvpMatrix * position;
		EmitVertex();
	}

    vec3 v12 = vertexes[1] - vertexes[0];
    vec3 v23 = vertexes[2] - vertexes[1];
	vec3 normalLine = vec3(normalMatrix * vec4(cross(v12, v23), 1.0));
    vertex_out.normal = normalize(normalLine);

    vertex_out.isHighlight = vertex_in[0].isHighlight;

	EndPrimitive();
}

