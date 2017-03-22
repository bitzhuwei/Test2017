#version 150 core

layout (triangles) in;
layout (triangle_strip, max_vertices = 3) out;

in VS_GS_VERTEX
{
	float isHighlight;
	vec3 smoothNormal;
	vec3 cloudColor;
} vertex_in[];

out GS_FS_VERTEX
{
	float isHighlight;
    vec3 normal;
	vec3 cloudColor;
} vertex_out;

uniform mat4 mvpMatrix;
uniform mat3 normalMatrix;
uniform bool flatMode = true;

void main(void)
{
	bool highlight = (vertex_in[0].isHighlight > 0)
		&& (vertex_in[1].isHighlight > 0)
		&& (vertex_in[2].isHighlight > 0);

	if (flatMode)
	{
		vec3[3] vertexes;
		for (int i = 0; i < 3; i++)
		{
			vertexes[i] = vec3(gl_in[i].gl_Position);
		}

		vec3 v12 = vertexes[1] - vertexes[0];
		vec3 v23 = vertexes[2] - vertexes[1];
		vec3 normalLine = normalize(normalMatrix * cross(v12, v23));
		
		for (int i = 0; i < 3; i++)
		{
			gl_Position = mvpMatrix * gl_in[i].gl_Position;
			vertex_out.isHighlight = highlight ? 1 : -1;
			vertex_out.normal = normalLine;
			vertex_out.cloudColor = vertex_in[i].cloudColor;
			EmitVertex();
		}
		EndPrimitive();
	}
	else
	{
		for (int i = 0; i < 3; i++)
		{
			gl_Position = mvpMatrix * gl_in[i].gl_Position;
			vertex_out.isHighlight = highlight ? 1 : -1;
			vertex_out.normal = normalize(normalMatrix * vertex_in[i].smoothNormal);
			vertex_out.cloudColor = vertex_in[i].cloudColor;
			EmitVertex();
		}
		EndPrimitive();	
	}
}

