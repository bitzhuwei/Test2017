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
        public static NASDataSource Parse(this NASFile file)
        {
            if (file == null) { throw new ArgumentNullException(); }

            var gridGroups = from item in file.Triangles
                             group item by item.FaceLabel;

            IList<EMGrid> gridList = FindGridList(file.VertexPositions, file.VertexNormals, file.Box, gridGroups);

            return new NASDataSource(
                file.VertexPositions, file.VertexNormals, file.Triangles,
                file.FaceNormalPositions, file.FaceNormalDirections, file.FaceNormalLengths,
                gridList, file.Box);
        }

        private static List<EMGrid> FindGridList(
            vec3[] allVertexPositions, vec3[] allVertexNormals, BoundingBox box,
            IEnumerable<IGrouping<string, Triangle>> gridGroups)
        {
            var list = new List<EMGrid>();

            foreach (var group in gridGroups)
            {
                var dict = new Dictionary<int, int>();// index of all -> index of this grid
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
                list.Add(new EMGrid(positions.ToArray(), normals.ToArray(), triangles, box, label));
            }

            return list;
        }


    }
}
