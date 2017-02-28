using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMGraphics
{
    /// <summary>
    /// 
    /// </summary>
    public class Triangle
    {
        /// <summary>
        /// 本面元的第一个点
        /// </summary>
        public int Num1 { get; set; }

        /// <summary>
        /// 本面元的第二个点
        /// </summary>
        public int Num2 { get; set; }

        /// <summary>
        /// 本面元的第三个点
        /// </summary>
        public int Num3 { get; set; }

        /// <summary>
        /// 网格平面的单位法向量
        /// </summary>
        public vec3 Normal { get; set; }

        /// <summary>
        /// 面元的重心坐标
        /// </summary>
        public vec3 GravityPoint { get; set; }

        /// <summary>
        /// 网格的面积
        /// </summary>
        public double Area { get; set; }

        /// <summary>
        /// 网格的标签
        /// </summary>
        public string FaceLabel { get; set; }

        public Triangle(int num1, int num2, int num3, string faceLabel)
        {
            this.Num1 = num1;
            this.Num2 = num2;
            this.Num3 = num3;
            this.FaceLabel = faceLabel;
        }

        internal int IndexOfTriangles { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, label: {3}", this.Num1, this.Num2, this.Num3, this.FaceLabel);
        }
    }
}
