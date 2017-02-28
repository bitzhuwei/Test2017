using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EMGraphics
{
    /// <summary>
    /// information(vertex positions, triangles etc) laoded from a *.nas file.
    /// </summary>
    public static class NASParser
    {
        public static void Parse(this NASFile file, out EMGrid grid, out NormalLineModel normalLineModel, out BoundingBox box)
        {
            if (file == null) { throw new ArgumentNullException(); }

            vec3[] vertexPositions = file.Points.ToArray();
            box = vertexPositions.Move2Center();
            var vertexNormals = new vec3[vertexPositions.Length];
            Triangle[] triangles = file.Triangles.ToArray();

            var faceNormalPositions = new vec3[triangles.Length];
            var faceNormalDirections = new vec3[triangles.Length];
            var faceNormalLengths = new float[triangles.Length];

            for (int i = 0; i < triangles.Length; i++)
            {
                int index1 = triangles[i].Num1;
                int index2 = triangles[i].Num2;
                int index3 = triangles[i].Num3;
                vec3 vertex1 = vertexPositions[index1];
                vec3 vertex2 = vertexPositions[index2];
                vec3 vertex3 = vertexPositions[index3];

                vec3 position = vertex1 / 3.0f + vertex2 / 3.0f + vertex3 / 3.0f;
                faceNormalPositions[i] = position;

                vec3 v12 = vertex2 - vertex1;
                vec3 v23 = vertex3 - vertex2;
                vec3 faceNormalDirection = v12.cross(v23).normalize();
                faceNormalDirections[i] = faceNormalDirection;
                vertexNormals[index1] += faceNormalDirection;
                vertexNormals[index2] += faceNormalDirection;
                vertexNormals[index3] += faceNormalDirection;

                vec3 v31 = vertex1 - vertex3;
                float length = v12.length();
                float tmp = v23.length(); if (length < tmp) { length = tmp; }
                tmp = v31.length(); if (length < tmp) { length = tmp; }
                faceNormalLengths[i] = length;
            }

            normalLineModel = new NormalLineModel(faceNormalPositions, faceNormalDirections, faceNormalLengths);

            for (int i = 0; i < vertexNormals.Length; i++)
            {
                vertexNormals[i] = -vertexNormals[i].normalize();
            }

            grid = new EMGrid(vertexPositions, vertexNormals, triangles);
        }
    }
}
