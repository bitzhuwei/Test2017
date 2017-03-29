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
    public class AxisSeat : IBufferable, IModelSpace
    {
        public Color SeatColor { get; set; }

        private int detail;

        private float displacement;

        /// <summary>
        /// lengths of x/y/z axis.
        /// </summary>
        /// <param name="length"></param>
        /// <param name="color"></param>
        /// <param name="detail"></param>
        /// <param name="displacement"></param>
        public AxisSeat(float length, Color color, int detail = 10, float displacement = 0.05f)
        {
            this.ModelSize = new vec3(length, length, length);
            this.SeatColor = color;
            this.length = length;
            this.detail = detail;
            this.displacement = displacement;
        }

        public const string strPosition = "position";
        private VertexBuffer positionBuffer;

        //public const string strIndex = "index";
        private IndexBuffer indexBuffer = null;
        private float length;

        public VertexBuffer GetVertexAttributeBuffer(string bufferName, string varNameInShader)
        {
            if (bufferName == strPosition)
            {
                if (this.positionBuffer == null)
                {
                    var vertexPositions = new vec3[1 + detail * 3 + 1];
                    vertexPositions[0] = new vec3(0, 0, 0);
                    for (int i = 0; i < detail; i++)
                    {
                        vertexPositions[1 + i] = new vec3(
                            (float)Math.Cos(Math.PI / 2.0 * (double)i / (double)detail),
                            (float)Math.Sin(Math.PI / 2.0 * (double)i / (double)detail),
                            0);
                    }
                    for (int i = 0; i < detail; i++)
                    {
                        vertexPositions[1 + detail + i] = new vec3(
                            0,
                            (float)Math.Cos(Math.PI / 2.0 * (double)i / (double)detail),
                            (float)Math.Sin(Math.PI / 2.0 * (double)i / (double)detail));
                    }
                    for (int i = 0; i < detail; i++)
                    {
                        vertexPositions[1 + detail + detail + i] = new vec3(
                            (float)Math.Sin(Math.PI / 2.0 * (double)i / (double)detail),
                            0,
                            (float)Math.Cos(Math.PI / 2.0 * (double)i / (double)detail));
                    }
                    vertexPositions[1 + detail * 3] = new vec3(1, 0, 0);

                    vec3 displace = new vec3(this.displacement, this.displacement, this.displacement) * 2;
                    for (int i = 0; i < vertexPositions.Length; i++)
                    {
                        vertexPositions[i] = (vertexPositions[i] - displace) * this.length / 2.0f;
                    }

                    this.positionBuffer = vertexPositions.GenVertexBuffer(VBOConfig.Vec3, varNameInShader, BufferUsage.StaticDraw);
                }
                return this.positionBuffer;
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
                ZeroIndexBuffer buffer = GLBuffer.Create(DrawMode.TriangleFan, 0, 1 + this.detail * 3 + 1);
                this.indexBuffer = buffer;
            }

            return indexBuffer;
        }

        public bool UsesZeroIndexBuffer()
        {
            return true;
        }

        public vec3 ModelSize { get; set; }

        public float RotationAngleDegree { get; set; }

        public vec3 RotationAxis { get; set; }

        public vec3 Scale { get; set; }

        public vec3 WorldPosition { get; set; }


    }
}
