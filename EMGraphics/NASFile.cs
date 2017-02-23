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
            set { points = value; }
        }
        private List<Triangle> grids;

        /// <summary>
        /// triangles that refer to <see cref="Points"/> in this *.nas file.
        /// </summary>
        public List<Triangle> Grids
        {
            get { return grids; }
            set { grids = value; }
        }

        private NASFile(List<vec3> points, List<Triangle> grids)
        {
            this.points = points;
            this.grids = grids;
        }

        public static NASFile Load(string filename)
        {
            NASFile result;

            using (var reader = new StreamReader(filename))
            {
                string[] str22;
                string sss = string.Empty;
                float x = 0, y = 0, z = 0;
                int ctria1, ctria2, ctria3;
                var points = new List<vec3>();
                var grids = new List<Triangle>();
                points.Add(new vec3(0, 0, 0));//点坐标的数组是从1开始的,不是从0开始的,方便
                grids.Add(new Triangle(1, 2, 3));//网格数组也是从1开始的

                int pointIndexMin = 0;//点的最小序号
                bool flagPointIndexMin = true;

                do
                {
                    sss = reader.ReadLine();
                    if (reader.EndOfStream)
                    {
                        break;
                    }

                    str22 = sss.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    if (sss.Length >= 5 && sss.Substring(0, 5) == "GRID*")//观察导出文件的格式,当遇到*号时,开始解读点的坐标,反之就是网格的信息
                    {
                        if (flagPointIndexMin)
                        {
                            pointIndexMin = int.Parse(str22[1]);
                            flagPointIndexMin = false;
                        }

                        if (str22.Length == 5)//GRID*              26176                -8.088764491E-01 5.021697901E-02   26176
                        {
                            x = float.Parse(str22[2]);
                            y = float.Parse(str22[3]);
                        }
                        else//GRID*              26182                -6.270781884E-01-4.699668723E-02   26182
                        {
                            if (str22[2].Substring(0, 1) == "-")
                            {
                                x = float.Parse(str22[2].Substring(0, 16));
                            }
                            else
                            {
                                x = float.Parse(str22[2].Substring(0, 15));
                            }

                            y = float.Parse(str22[2].Substring(str22[2].Length - 16));
                        }
                    }
                    else if (sss.Length >= 1 && sss.Substring(0, 1) == "*")//提取Z坐标
                    {
                        z = float.Parse(str22[str22.Length - 1].Substring(str22[str22.Length - 1].Length - 15));
                        points.Add(new vec3(x, y, z));
                    }
                    else if (sss.Length >= 5 && sss.Substring(0, 5) == "CTRIA")
                    {
                        ctria1 = int.Parse(str22[3]) - pointIndexMin + 1;//第一个点
                        ctria2 = int.Parse(str22[4]) - pointIndexMin + 1;//第二个点
                        ctria3 = int.Parse(str22[5]) - pointIndexMin + 1;//第三个点
                        grids.Add(new Triangle(ctria1, ctria2, ctria3));
                    }
                } while (true);
                result = new NASFile(points, grids);
            }

            return result;
        }
    }
}
