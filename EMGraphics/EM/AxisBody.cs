using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace EMGraphics
{
    /// <summary>
    /// 
    /// </summary>
    public class AxisBody : IBufferable, IModelSpace
    {
        private int detail;

        /// <summary>
        /// lengths of x/y/z axis.
        /// </summary>
        /// <param name="length"></param>
        /// <param name="color"></param>
        /// <param name="detail"></param>
        /// <param name="displacement"></param>
        public AxisBody(float length, int detail = 10)
        {
            this.ModelSize = new vec3(length, length, length);
            this.length = length;
            this.detail = detail;
        }

        public const string strPosition = "position";
        private VertexBuffer positionBuffer;

        public const string strColor = "color";
        private VertexBuffer colorBuffer;

        //public const string strIndex = "index";
        private IndexBuffer indexBuffer = null;
        private float length;

        public VertexBuffer GetVertexAttributeBuffer(string bufferName, string varNameInShader)
        {
            if (bufferName == strPosition)
            {
                if (this.positionBuffer == null)
                {
                    const double radius = 0.05;
                    int singleBody = (1 + detail) * 2;
                    var vertexPositions = new vec3[singleBody * 3];
                    for (int i = 0; i < (1 + detail); i++)
                    {
                        vertexPositions[i * 2 + 0] = new vec3(
                            0,
                            (float)(radius * Math.Sin(Math.PI * 2 * (double)i / (double)detail)),
                            (float)(radius * Math.Cos(Math.PI * 2 * (double)i / (double)detail)));
                        vertexPositions[i * 2 + 1] = new vec3(
                            1,
                            (float)(radius * Math.Sin(Math.PI * 2 * (double)i / (double)detail)),
                            (float)(radius * Math.Cos(Math.PI * 2 * (double)i / (double)detail)));
                    }
                    for (int i = 0; i < (1 + detail); i++)
                    {
                        vertexPositions[singleBody + i * 2 + 0] = new vec3(
                            (float)(radius * Math.Sin(Math.PI * 2 * (double)i / (double)detail)),
                            0,
                            (float)(radius * Math.Cos(Math.PI * 2 * (double)i / (double)detail)));
                        vertexPositions[singleBody + i * 2 + 1] = new vec3(
                            (float)(radius * Math.Sin(Math.PI * 2 * (double)i / (double)detail)),
                            1,
                            (float)(radius * Math.Cos(Math.PI * 2 * (double)i / (double)detail)));
                    }
                    for (int i = 0; i < (1 + detail); i++)
                    {
                        vertexPositions[singleBody + singleBody + i * 2 + 0] = new vec3(
                            (float)(radius * Math.Sin(Math.PI * 2 * (double)i / (double)detail)),
                            (float)(radius * Math.Cos(Math.PI * 2 * (double)i / (double)detail)),
                            0);
                        vertexPositions[singleBody + singleBody + i * 2 + 1] = new vec3(
                            (float)(radius * Math.Sin(Math.PI * 2 * (double)i / (double)detail)),
                            (float)(radius * Math.Cos(Math.PI * 2 * (double)i / (double)detail)),
                            1);
                    }

                    this.positionBuffer = vertexPositions.GenVertexBuffer(VBOConfig.Vec3, varNameInShader, BufferUsage.StaticDraw);
                }
                return this.positionBuffer;
            }
            else if (bufferName == strColor)
            {
                if (this.colorBuffer == null)
                {
                    var red = new vec3(1, 0, 0);
                    var green = new vec3(0, 1, 0);
                    var blue = new vec3(0, 0, 1);
                    int singleBody = (1 + detail) * 2;
                    var vertexColors = new vec3[singleBody * 3];
                    for (int i = 0; i < singleBody; i++)
                    {
                        vertexColors[i] = red;
                    }
                    for (int i = 0; i < singleBody; i++)
                    {
                        vertexColors[singleBody + i] = green;
                    }
                    for (int i = 0; i < singleBody; i++)
                    {
                        vertexColors[singleBody + singleBody + i] = blue;
                    }

                    this.colorBuffer = vertexColors.GenVertexBuffer(VBOConfig.Vec3, varNameInShader, BufferUsage.StaticDraw);
                }
                return this.colorBuffer;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public IndexBuffer GetIndexBuffer()
        {
            if (this.indexBuffer == null)
            {
                int singleBody = (1 + detail) * 2;
                var indexes = new uint[singleBody + 1 + singleBody + 1 + singleBody];
                uint count = 0;
                int index = 0;
                for (int i = 0; i < singleBody; i++)
                {
                    indexes[index++] = count++;
                }
                indexes[index++] = uint.MaxValue;//separator.
                for (int i = 0; i < singleBody; i++)
                {
                    indexes[index++] = count++;
                }
                indexes[index++] = uint.MaxValue;//separator.
                for (int i = 0; i < singleBody; i++)
                {
                    indexes[index++] = count++;
                }

                this.indexBuffer = indexes.GenIndexBuffer(DrawMode.TriangleStrip, BufferUsage.StaticDraw);
            }

            return indexBuffer;
        }

        public bool UsesZeroIndexBuffer()
        {
            return false;
        }

        public vec3 ModelSize { get; set; }

        public float RotationAngleDegree { get; set; }

        public vec3 RotationAxis { get; set; }

        public vec3 Scale { get; set; }

        public vec3 WorldPosition { get; set; }


    }
}
