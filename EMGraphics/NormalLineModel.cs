using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMGraphics
{
    /// <summary>
    /// 
    /// </summary>
    public class NormalLineModel : IBufferable, IModelSpace
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertexPositions">root positions of every normal line.</param>
        /// <param name="directions">directions of every normal line.</param>
        /// <param name="lengths">lengths of every normal line.</param>
        public NormalLineModel(vec3[] vertexPositions, vec3[] directions, float[] lengths)
        {
            if (vertexPositions == null || directions == null || lengths == null)
            { throw new ArgumentNullException(); }
            if (vertexPositions.Length != directions.Length)
            { throw new ArgumentException(); }
            if (vertexPositions.Length != lengths.Length)
            { throw new ArgumentException(); }

            this.vertexCount = vertexPositions.Length * 4;
            BoundingBox box = vertexPositions.Move2Center();
            this.vertexPositions = vertexPositions;
            this.ModelSize = box.MaxPosition - box.MinPosition;
            this.WorldPosition = box.MaxPosition / 2.0f + box.MinPosition / 2.0f;
            this.directions = directions;
            this.lengths = lengths;
            this.RotationAngleDegree = 0;
            this.RotationAxis = new vec3(0, 1, 0);
            this.Scale = new vec3(1, 1, 1);
        }

        public const string strPosition = "position";
        private VertexBuffer positionBuffer;

        private vec3[] vertexPositions;
        private vec3[] directions;
        private float[] lengths;

        private IndexBuffer indexBuffer = null;

        private int vertexCount;

        public VertexBuffer GetVertexAttributeBuffer(string bufferName, string varNameInShader)
        {
            if (bufferName == strPosition)
            {
                if (this.positionBuffer == null)
                {
                    vec3[] positions = new vec3[this.vertexPositions.Length * 4];
                    for (int i = 0; i < this.vertexPositions.Length; i++)
                    {
                        vec3 root = this.vertexPositions[i];
                        vec3 direction = this.directions[i].normalize() * lengths[i];
                        positions[i * 4 + 0] = root;
                        positions[i * 4 + 1] = root + direction * 2.0f / 3.0f;
                        positions[i * 4 + 2] = root + direction * 2.0f / 3.0f;
                        positions[i * 4 + 3] = root + direction;
                    }
                    this.positionBuffer = positions.GenVertexBuffer(VBOConfig.Vec3, varNameInShader, BufferUsage.StaticDraw);
                    // be ready to release array by GC.
                    this.vertexPositions = null;
                    this.directions = null;
                    this.lengths = null;
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
