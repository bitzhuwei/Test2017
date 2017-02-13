using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMGraphics
{
    public static class NormalHelper
    {
        /// <summary>
        /// Calculates normal lines for specified vertex positions and faces.
        /// </summary>
        /// <param name="vertexPositions"></param>
        /// <param name="faces"></param>
        /// <returns></returns>
        public static vec3[] CalculateNormals(this vec3[] vertexPositions, Triangle[] faces)
        {
            if (vertexPositions == null || faces == null) { throw new ArgumentNullException(); }

            var faceNormals = new vec3[faces.Length];
            for (int i = 0; i < faceNormals.Length; i++)
            {
                int vertexId1 = faces[i].Num1;
                int vertexId2 = faces[i].Num2;
                int vertexId3 = faces[i].Num3;
                vec3 vertex1 = vertexPositions[vertexId1];
                vec3 vertex2 = vertexPositions[vertexId2];
                vec3 vertex3 = vertexPositions[vertexId3];
                vec3 v1 = vertex1 - vertex3;
                vec3 v2 = vertex3 - vertex2;
                faceNormals[i] = v2.cross(v1).normalize();
            }

            var normals = new vec3[vertexPositions.Length];
            for (int i = 0; i < normals.Length; i++)
            {
                var sum = new vec3();
                int shared = 0;
                for (int j = 0; j < faces.Length; j++)
                {
                    int vertexId1 = faces[j].Num1;
                    int vertexId2 = faces[j].Num2;
                    int vertexId3 = faces[j].Num3;
                    // note: vertex id starts from 1(not 0).
                    if (vertexId1 - 1 == i || vertexId2 - 1 == i || vertexId3 - 1 == i)
                    {
                        sum = sum + faceNormals[j];
                        shared++;
                    }
                }

                if (shared > 0)
                {
                    sum = (sum / shared).normalize();
                }

                normals[i] = sum;
            }

            return normals;
        }
    }
}
