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
        public static void Parse(this NASFile file,
            out List<EMGrid> gridList,
            out List<NormalLineModel> normalLineModelList,
            out BoundingBox box)
        {
            if (file == null) { throw new ArgumentNullException(); }

            vec3[] allVertexPositions = file.Points;
            box = allVertexPositions.Move2Center();
            var allVertexNormals = new vec3[allVertexPositions.Length];
            Triangle[] allTriangles = file.Triangles;

            var allFaceNormalPositions = new vec3[allTriangles.Length];
            var allFaceNormalDirections = new vec3[allTriangles.Length];
            var allFaceNormalLengths = new float[allTriangles.Length];

            InitAll(
                allVertexPositions, allVertexNormals, allTriangles,
                allFaceNormalPositions, allFaceNormalDirections, allFaceNormalLengths);

            var meshGroups = from item in allTriangles
                             group item by item.FaceLabel;

            gridList = FindGridList(allVertexPositions, allVertexNormals, meshGroups);

            normalLineModelList = new List<NormalLineModel>();
            FindNormaLineModelList(
                allTriangles, normalLineModelList,
                allFaceNormalPositions, allFaceNormalDirections, allFaceNormalLengths, meshGroups);

            //gridList.Add(new EMGrid(allVertexPositions, allVertexNormals, allTriangles, "test"));
            //normalLineModelList.Add(new NormalLineModel(allFaceNormalPositions, allFaceNormalDirections, allFaceNormalLengths, "test2"));
        }

        private static void FindNormaLineModelList(Triangle[] triangles, List<NormalLineModel> normalLineModelList, vec3[] allFaceNormalPositions, vec3[] allFaceNormalDirections, float[] allFaceNormalLengths, IEnumerable<IGrouping<string, Triangle>> meshGroups)
        {
            foreach (var group in meshGroups)
            {
                var dict = new Dictionary<int, int>();// index of all -> index of this mesh
                string label = group.Key;
                int count = group.Count();

                var faceNormalPositions = new vec3[count];
                var faceNormalDirections = new vec3[count];
                var faceNormalLengths = new float[count];

                int i = 0;
                foreach (var triangle in group)
                {
                    int index = triangle.IndexOfTriangles;// triangles.IndexOf(triangle);
                    faceNormalPositions[i] = allFaceNormalPositions[index];
                    faceNormalDirections[i] = allFaceNormalDirections[index];
                    faceNormalLengths[i] = allFaceNormalLengths[index];
                    i++;
                }

                normalLineModelList.Add(new NormalLineModel(faceNormalPositions, faceNormalDirections, faceNormalLengths, label));
            }
        }

        private static List<EMGrid> FindGridList(vec3[] allVertexPositions, vec3[] allVertexNormals, IEnumerable<IGrouping<string, Triangle>> meshGroups)
        {
            var list = new List<EMGrid>();
            foreach (var group in meshGroups)
            {
                var dict = new Dictionary<int, int>();// index of all -> index of this mesh
                string label = group.Key;
                int count = group.Count();
                var positions = new List<vec3>();
                var normals = new List<vec3>();
                var triangles = new Triangle[count];
                int i = 0;
                foreach (var triangle in group)
                {
                    int index1, index2, index3;
                    if (dict.ContainsKey(triangle.Num1)) { index1 = dict[triangle.Num1]; }
                    else
                    {
                        index1 = positions.Count;
                        positions.Add(allVertexPositions[triangle.Num1]);
                        normals.Add(allVertexNormals[triangle.Num1]);
                        dict.Add(triangle.Num1, index1);
                    }

                    if (dict.ContainsKey(triangle.Num2)) { index2 = dict[triangle.Num2]; }
                    else
                    {
                        index2 = positions.Count;
                        positions.Add(allVertexPositions[triangle.Num2]);
                        normals.Add(allVertexNormals[triangle.Num2]);
                        dict.Add(triangle.Num2, index2);
                    }

                    if (dict.ContainsKey(triangle.Num3)) { index3 = dict[triangle.Num3]; }
                    else
                    {
                        index3 = positions.Count;
                        positions.Add(allVertexPositions[triangle.Num3]);
                        normals.Add(allVertexNormals[triangle.Num3]);
                        dict.Add(triangle.Num3, index3);
                    }

                    triangles[i++] = new Triangle(index1, index2, index3, label);
                }
                if (i != count) { throw new Exception(); }
                list.Add(new EMGrid(positions.ToArray(), normals.ToArray(), triangles, label));
            }

            return list;
        }

        private static void InitAll(vec3[] allVertexPositions, vec3[] allVertexNormals, Triangle[] allTriangles, vec3[] allFaceNormalPositions, vec3[] allFaceNormalDirections, float[] allFaceNormalLengths)
        {
            for (int i = 0; i < allTriangles.Length; i++)
            {
                int index1 = allTriangles[i].Num1;
                int index2 = allTriangles[i].Num2;
                int index3 = allTriangles[i].Num3;
                vec3 vertex1 = allVertexPositions[index1];
                vec3 vertex2 = allVertexPositions[index2];
                vec3 vertex3 = allVertexPositions[index3];

                vec3 position = vertex1 / 3.0f + vertex2 / 3.0f + vertex3 / 3.0f;
                allFaceNormalPositions[i] = position;

                vec3 v12 = vertex2 - vertex1;
                vec3 v23 = vertex3 - vertex2;
                vec3 faceNormalDirection = v12.cross(v23).normalize();
                allFaceNormalDirections[i] = faceNormalDirection;
                allVertexNormals[index1] += faceNormalDirection;
                allVertexNormals[index2] += faceNormalDirection;
                allVertexNormals[index3] += faceNormalDirection;

                vec3 v31 = vertex1 - vertex3;
                float length = v12.length();
                float tmp = v23.length(); if (length < tmp) { length = tmp; }
                tmp = v31.length(); if (length < tmp) { length = tmp; }
                allFaceNormalLengths[i] = length;
            }

            //normalLineModel = new NormalLineModel(faceNormalPositions, faceNormalDirections, faceNormalLengths);

            for (int i = 0; i < allVertexNormals.Length; i++)
            {
                allVertexNormals[i] = allVertexNormals[i].normalize();
            }
        }
    }
}
