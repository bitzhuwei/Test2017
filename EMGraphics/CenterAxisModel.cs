using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMGraphics
{
    /// <summary>
    /// 
    /// </summary>
    public class CenterAxisModel : IBufferable, IModelSpace
    {
        /// <summary>
        /// lengths of x/y/z axis.
        /// </summary>
        /// <param name="lengths"></param>
        public CenterAxisModel(float length)
        {
            this.ModelSize = new vec3(length, length, length);
        }

        public const string strPosition = "position";
        private VertexBuffer positionBuffer;

        public const string strColor = "color";
        private VertexBuffer colorBuffer;

        //public const string strIndex = "index";
        private IndexBuffer indexBuffer = null;

        public VertexBuffer GetVertexAttributeBuffer(string bufferName, string varNameInShader)
        {
            if (bufferName == strPosition)
            {
                if (this.positionBuffer == null)
                {
                    vec3[] vertexPositions = new vec3[6];
                    //vertexPositions[0] = new vec3(0, 0, 0);
                    vertexPositions[1] = new vec3(ModelSize.x / 2, 0, 0);
                    //vertexPositions[2] = new vec3(0, 0, 0);
                    vertexPositions[3] = new vec3(0, ModelSize.y / 2, 0);
                    //vertexPositions[4] = new vec3(0, 0, 0);
                    vertexPositions[5] = new vec3(0, 0, ModelSize.z / 2);
                    this.positionBuffer = vertexPositions.GenVertexBuffer(VBOConfig.Vec3, varNameInShader, BufferUsage.StaticDraw);
                }
                return this.positionBuffer;
            }
            else if (bufferName == strColor)
            {
                if (this.colorBuffer == null)
                {
                    vec3[] vertexColors = new vec3[6];
                    vertexColors[0] = new vec3(1.0f, 0, 0);
                    vertexColors[1] = new vec3(1.0f, 0, 0);
                    vertexColors[2] = new vec3(0, 1.0f, 0);
                    vertexColors[3] = new vec3(0, 1.0f, 0);
                    vertexColors[4] = new vec3(0, 0, 1.0f);
                    vertexColors[5] = new vec3(0, 0, 1.0f);
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
                ZeroIndexBuffer buffer = GLBuffer.Create(DrawMode.Lines, 0, 6);
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
