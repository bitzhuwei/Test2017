using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMGraphics
{
    /// <summary>
    /// 
    /// </summary>
    public class FaceNormalLineModel : IBufferable, IModelSpace
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="normalPositions">root positions of every normal line.</param>
        /// <param name="normalDirections">directions of every normal line.</param>
        /// <param name="normalLengths">lengths of every normal line.</param>
        public FaceNormalLineModel(vec3[] normalPositions, vec3[] normalDirections, float[] normalLengths)
        {
            if (normalPositions == null || normalDirections == null || normalLengths == null)
            { throw new ArgumentNullException(); }
            if (normalPositions.Length != normalDirections.Length)
            { throw new ArgumentException(); }
            if (normalPositions.Length != normalLengths.Length)
            { throw new ArgumentException(); }

            this.vertexCount = normalPositions.Length * 4;
            BoundingBox box = normalPositions.Move2Center();
            this.normalPositions = normalPositions;
            this.ModelSize = box.MaxPosition - box.MinPosition;
            this.WorldPosition = box.MaxPosition / 2.0f + box.MinPosition / 2.0f;
            this.normalDirections = normalDirections;
            this.normalLengths = normalLengths;
            this.RotationAngleDegree = 0;
            this.RotationAxis = new vec3(0, 1, 0);
            this.Scale = new vec3(1, 1, 1);
        }

        public const string strPosition = "position";
        private VertexBuffer positionBuffer;

        private vec3[] normalPositions;
        private vec3[] normalDirections;
        private float[] normalLengths;

        private IndexBuffer indexBuffer = null;

        private int vertexCount;

        public VertexBuffer GetVertexAttributeBuffer(string bufferName, string varNameInShader)
        {
            if (bufferName == strPosition)
            {
                if (this.positionBuffer == null)
                {
                    vec3[] positions = new vec3[this.normalPositions.Length * 4];
                    for (int i = 0; i < this.normalPositions.Length; i++)
                    {
                        vec3 root = this.normalPositions[i];
                        vec3 direction = this.normalDirections[i].normalize() * normalLengths[i];
                        positions[i * 4 + 0] = root;
                        positions[i * 4 + 1] = root + direction * 2.0f / 3.0f;
                        positions[i * 4 + 2] = root + direction * 2.0f / 3.0f;
                        positions[i * 4 + 3] = root + direction;
                    }
                    this.positionBuffer = positions.GenVertexBuffer(VBOConfig.Vec3, varNameInShader, BufferUsage.StaticDraw);
                    // be ready to release array by GC.
                    this.normalPositions = null;
                    this.normalDirections = null;
                    this.normalLengths = null;
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
                this.indexBuffer = ZeroIndexBuffer.Create(DrawMode.Lines, 0, vertexCount);
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
