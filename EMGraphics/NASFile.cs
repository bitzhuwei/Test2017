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
        private List<vec3> points;

        /// <summary>
        /// vertexes in this *.nas file.
        /// </summary>
        public List<vec3> Points
        {
            get { return points; }
        }

        private NASFile(List<vec3> points)
        {
            this.points = points;
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
                var finalPoints = new List<vec3>();

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
                        ctria1 = int.Parse(parts[3]) - pointIndexMin + 1;//第一个点
                        ctria2 = int.Parse(parts[4]) - pointIndexMin + 1;//第二个点
                        ctria3 = int.Parse(parts[5]) - pointIndexMin + 1;//第三个点
                        finalPoints.Add(points[ctria1 - 1]);
                        finalPoints.Add(points[ctria2 - 1]);
                        finalPoints.Add(points[ctria3 - 1]);
                    }
                } while (true);
                result = new NASFile(finalPoints);
            }

            return result;
        }
    }
}
