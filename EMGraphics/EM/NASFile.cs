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
        public dvec3[] VertexPositions { get; private set; }

        public dvec3[] VertexNormals { get; private set; }

        /// <summary>
        /// triangles in this *.nas file.
        /// </summary>
        public Triangle[] Triangles { get; private set; }

        public dvec3[] FaceNormalPositions { get; private set; }
        public dvec3[] FaceNormalDirections { get; private set; }
        public double[] FaceNormalLengths { get; private set; }

        private NASFile(dvec3[] vertexPositions, Triangle[] triangles)
        {
            this.VertexPositions = vertexPositions;
            this.Triangles = triangles;
            var vertexNormals = new dvec3[vertexPositions.Length];
            var facePositions = new dvec3[triangles.Length];
            var faceDirections = new dvec3[triangles.Length];
            var faceLengths = new double[triangles.Length];
            InitAll(vertexPositions, vertexNormals, triangles, facePositions, faceDirections, faceLengths);
            this.VertexNormals = vertexNormals;
            this.FaceNormalPositions = facePositions;
            this.FaceNormalDirections = faceDirections;
            this.FaceNormalLengths = faceLengths;
        }

        private void InitAll(dvec3[] vertexPositions, 
			dvec3[] vertexNormals, Triangle[] triangles,
			dvec3[] facePositions, dvec3[] faceDirections, double[] faceLengths)
        {
            for (int i = 0; i < triangles.Length; i++)
            {
                int index1 = triangles[i].Num1;
                int index2 = triangles[i].Num2;
                int index3 = triangles[i].Num3;
				dvec3 vertex1 = vertexPositions[index1];
				dvec3 vertex2 = vertexPositions[index2];
				dvec3 vertex3 = vertexPositions[index3];

				dvec3 position = vertex1 / 3.0 + vertex2 / 3.0 + vertex3 / 3.0;
                facePositions[i] = position;

				dvec3 v12 = vertex2 - vertex1;
				dvec3 v23 = vertex3 - vertex2;
				dvec3 faceNormalDirection = v12.cross(v23).normalize();
                faceDirections[i] = faceNormalDirection;
                vertexNormals[index1] += faceNormalDirection;
                vertexNormals[index2] += faceNormalDirection;
                vertexNormals[index3] += faceNormalDirection;

				dvec3 v31 = vertex1 - vertex3;
                double length = v12.length();
				double tmp = v23.length(); if (length < tmp) { length = tmp; }
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
                double x = 0, y = 0, z = 0;
                int ctria1, ctria2, ctria3;
                var points = new List<dvec3>();
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
                            x = double.Parse(parts[2]);
                            y = double.Parse(parts[3]);
                        }
                        else//GRID*              26182                -6.270781884E-01-4.699668723E-02   26182
                        {
                            string xy = parts[2];
                            if (xy.Substring(0, 1) == "-")
                            {
                                x = double.Parse(xy.Substring(0, 16));
                                y = double.Parse(xy.Substring(16));
                            }
                            else
                            {
                                x = double.Parse(xy.Substring(0, 15));
                                y = double.Parse(xy.Substring(15));
                            }
                        }
                    }
                    else if (line.Length >= 1 && line.Substring(0, 1) == "*")//提取Z坐标
                    {
                        string last = parts[parts.Length - 1];
                        if (last.Length == 15)// 正数或零
                        { z = double.Parse(last); }
                        else// 负数
                        { z = double.Parse(last.Substring(last.Length - 16)); }
                        points.Add(new dvec3(x, y, z));
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
