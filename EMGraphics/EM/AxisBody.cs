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
                    var vertexPositions = new vec3[1 + detail * 3 + 1];

                    this.positionBuffer = vertexPositions.GenVertexBuffer(VBOConfig.Vec3, varNameInShader, BufferUsage.StaticDraw);
                }
                return this.positionBuffer;
            }
            else if (bufferName == strColor)
            {
                if (this.colorBuffer == null)
                {
                    var vertexColors = new vec3[1 + detail * 3 + 1];

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


        public int detail { get; set; }

        public float desplacement { get; set; }
    }
}
