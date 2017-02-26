using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMGraphics
{
    /// <summary>
    /// 
    /// </summary>
    public class EMModel : IBufferable, IModelSpace
    {
        public EMModel(vec3[] vertexPositions, vec3[] vertexColors, vec3[] normals)
        {
            BoundingBox box = vertexPositions.Move2Center();
            this.vertexPositions = vertexPositions;
            this.ModelSize = box.MaxPosition - box.MinPosition;
            this.WorldPosition = box.MaxPosition / 2.0f + box.MinPosition / 2.0f;
            this.vertexColors = vertexColors;
            this.vertexNormals = normals;
            this.RotationAngleDegree = 0;
            this.RotationAxis = new vec3(0, 1, 0);
            this.Scale = new vec3(1, 1, 1);

            this.vertexCount = vertexPositions.Length;
        }

        public const string strPosition = "position";
        private VertexBuffer positionBuffer;
        private vec3[] vertexPositions;

        public const string strColor = "color";
        private VertexBuffer colorBuffer;
        private vec3[] vertexColors;

        public const string strNormal = "normal";
        private VertexBuffer normalBuffer;
        private vec3[] vertexNormals;

        //public const string strIndex = "index";
        private IndexBuffer indexBuffer = null;
        private int vertexCount;

        public VertexBuffer GetVertexAttributeBuffer(string bufferName, string varNameInShader)
        {
            if (bufferName == strPosition)
            {
                if (this.positionBuffer == null)
                {
                    this.positionBuffer = this.vertexPositions.GenVertexBuffer(VBOConfig.Vec3, varNameInShader, BufferUsage.StaticDraw);
                }
                return this.positionBuffer;
            }
            else if (bufferName == strColor)
            {
                if (this.colorBuffer == null)
                {
                    this.colorBuffer = this.vertexColors.GenVertexBuffer(VBOConfig.Vec3, varNameInShader, BufferUsage.StaticDraw);
                }
                return this.colorBuffer;
            }
            else if (bufferName == strNormal)
            {
                if (this.normalBuffer == null)
                {
                    this.normalBuffer = this.vertexNormals.GenVertexBuffer(VBOConfig.Vec3, varNameInShader, BufferUsage.StaticDraw);
                }
                return this.normalBuffer;
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
                ZeroIndexBuffer buffer = GLBuffer.Create(DrawMode.Triangles, 0, this.vertexCount);
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
