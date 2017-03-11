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
    public class NASFile
    {
        private static readonly string[] separator = new string[] { " " };

        /// <summary>
        /// vertexes in this *.nas file.
        /// </summary>
        public vec3[] VertexPositions { get; private set; }

        public vec3[] VertexNormals { get; private set; }

		public BoundingBox Box { get; private set; }

        /// <summary>
        /// triangles in this *.nas file.
        /// </summary>
        public Triangle[] Triangles { get; private set; }

        public vec3[] FaceNormalPositions { get; private set; }
        public vec3[] FaceNormalDirections { get; private set; }
        public float[] FaceNormalLengths { get; private set; }

        private NASFile(vec3[] vertexPositions, Triangle[] triangles)
        {
			this.Box = vertexPositions.Move2Center();
            this.VertexPositions = vertexPositions;
            this.Triangles = triangles;
            var veretxNormals = new vec3[vertexPositions.Length];
            var facePositions = new vec3[triangles.Length];
            var faceDirections = new vec3[triangles.Length];
            var faceLengths = new float[triangles.Length];
            InitAll(vertexPositions, triangles, veretxNormals, facePositions, faceDirections, faceLengths);
            this.VertexNormals = veretxNormals;
            this.FaceNormalPositions = facePositions;
            this.FaceNormalDirections = faceDirections;
            this.FaceNormalLengths = faceLengths;
        }

        private void InitAll(vec3[] vertexPositions, Triangle[] triangles,
            vec3[] vertexNormals,
            vec3[] facePositions, vec3[] faceDirections, float[] faceLengths)
        {
            for (int i = 0; i < triangles.Length; i++)
            {
                int index1 = triangles[i].Num1;
                int index2 = triangles[i].Num2;
                int index3 = triangles[i].Num3;
                vec3 vertex1 = vertexPositions[index1];
                vec3 vertex2 = vertexPositions[index2];
                vec3 vertex3 = vertexPositions[index3];

                vec3 position = vertex1 / 3.0f + vertex2 / 3.0f + vertex3 / 3.0f;
                facePositions[i] = position;

                vec3 v12 = vertex2 - vertex1;
                vec3 v23 = vertex3 - vertex2;
                vec3 faceNormalDirection = v12.cross(v23).normalize();
                faceDirections[i] = faceNormalDirection;
                vertexNormals[index1] += faceNormalDirection;
                vertexNormals[index2] += faceNormalDirection;
                vertexNormals[index3] += faceNormalDirection;

                vec3 v31 = vertex1 - vertex3;
                float length = v12.length();
                float tmp = v23.length(); if (length < tmp) { length = tmp; }
                tmp = v31.length(); if (length < tmp) { length = tmp; }
                faceLengths[i] = length;
            }

            //normalLineModel = new NormalLineModel(faceNormalPositions, faceNormalDirections, faceNormalLengths);

            for (int i = 0; i < vertexNormals.Length; i++)
            {
                vertexNormals[i] = vertexNormals[i].normalize();
            }
        }
        public static NASFile Load(string filename)
        {
            NASFile result;

            using (var reader = new StreamReader(filename))
            {
                string[] parts;
                string line = string.Empty;
                float x = 0, y = 0, z = 0;
                int ctria1, ctria2, ctria3;
                var points = new List<vec3>();
                var triangles = new List<Triangle>();

                //points.Add(new vec3(0, 0, 0));//点坐标的数组是从1开始的,不是从0开始的,方便
                //grids.Add(new Triangle(0, 0, 0));//网格数组也是从1开始的

                int pointIndexMin = 0;//点的最小序号
                bool flagPointIndexMin = true;

                do
                {
                    line = reader.ReadLine();
                    if (reader.EndOfStream)
                    {
                        break;
                    }

                    parts = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    if (line.Length >= 5 && line.Substring(0, 5) == "GRID*")//观察导出文件的格式,当遇到*号时,开始解读点的坐标,反之就是网格的信息
                    {
                        if (flagPointIndexMin)
                        {
                            pointIndexMin = int.Parse(parts[1]);
                            flagPointIndexMin = false;
                        }

                        if (parts.Length == 5)//GRID*              26176                -8.088764491E-01 5.021697901E-02   26176
                        {
                            x = float.Parse(parts[2]);
                            y = float.Parse(parts[3]);
                        }
                        else//GRID*              26182                -6.270781884E-01-4.699668723E-02   26182
                        {
                            string xy = parts[2];
                            if (xy.Substring(0, 1) == "-")
                            {
                                x = float.Parse(xy.Substring(0, 16));
                                y = float.Parse(xy.Substring(16));
                            }
                            else
                            {
                                x = float.Parse(xy.Substring(0, 15));
                                y = float.Parse(xy.Substring(15));
                            }
                        }
                    }
                    else if (line.Length >= 1 && line.Substring(0, 1) == "*")//提取Z坐标
                    {
                        string last = parts[parts.Length - 1];
                        if (last.Length == 15)// 正数或零
                        { z = float.Parse(last); }
                        else// 负数
                        { z = float.Parse(last.Substring(last.Length - 16)); }
                        points.Add(new vec3(x, y, z));
                    }
                    else if (line.Length >= 5 && line.Substring(0, 5) == "CTRIA")
                    {
                        string label = parts[2];
                        ctria1 = int.Parse(parts[3]) - pointIndexMin;//第一个点
                        ctria2 = int.Parse(parts[4]) - pointIndexMin;//第二个点
                        ctria3 = int.Parse(parts[5]) - pointIndexMin;//第三个点
                        var triangle = new Triangle(ctria1, ctria2, ctria3, label);
                        triangle.IndexOfTriangles = triangles.Count;
                        triangles.Add(triangle);
                    }
                } while (true);

                //for (int i = 0; i < triangles.Count - 1; i++)
                //{
                //    int p = i;
                //    for (int j = i + 1; j < triangles.Count; j++)
                //    {
                //        if (triangles[p].FaceLabel.CompareTo(triangles[j].FaceLabel) > 0)
                //        {
                //            p = j;
                //        }
                //    }
                //    if (p != i)
                //    {
                //        Triangle tmp = triangles[i];
                //        triangles[i] = triangles[p];
                //        triangles[p] = tmp;
                //    }
                //}

                result = new NASFile(points.ToArray(), triangles.ToArray());
            }

            return result;
        }
    }
}
