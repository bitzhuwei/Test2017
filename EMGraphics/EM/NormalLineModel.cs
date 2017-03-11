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
        public string Label { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="normalPositions">root positions of every normal line.</param>
        /// <param name="normalDirections">directions of every normal line.</param>
        /// <param name="normalLengths">lengths of every normal line.</param>
        /// <param name="label"></param>
        public NormalLineModel(dvec3[] normalPositions, dvec3[] normalDirections, double[] normalLengths, string label)
        {
            if (normalPositions == null || normalDirections == null || normalLengths == null)
            { throw new ArgumentNullException(); }
            if (normalPositions.Length != normalDirections.Length)
            { throw new ArgumentException(); }
            if (normalPositions.Length != normalLengths.Length)
            { throw new ArgumentException(); }

            this.vertexCount = normalPositions.Length * 4;
            BoundingBoxd box = normalPositions.Move2Center();
            this.normalPositions = normalPositions;
            this.Label = label;
			this.ModelSize = new vec3(box.MaxPosition - box.MinPosition);
			this.WorldPosition = new vec3(box.MaxPosition / 2.0 + box.MinPosition / 2.0);
            this.normalDirections = normalDirections;
            this.normalLengths = normalLengths;
            this.RotationAngleDegree = 0;
            this.RotationAxis = new vec3(0, 1, 0);
            this.Scale = new vec3(1, 1, 1);
        }

        public const string strPosition = "position";
        private VertexBuffer positionBuffer;

        private dvec3[] normalPositions;
        private dvec3[] normalDirections;
        private double[] normalLengths;

        private IndexBuffer indexBuffer = null;

        private int vertexCount;

        public VertexBuffer GetVertexAttributeBuffer(string bufferName, string varNameInShader)
        {
            if (bufferName == strPosition)
            {
                if (this.positionBuffer == null)
                {
                    dvec3[] positions = new dvec3[this.normalPositions.Length * 4];
                    for (int i = 0; i < this.normalPositions.Length; i++)
                    {
                        dvec3 root = this.normalPositions[i];
                        dvec3 direction = this.normalDirections[i].normalize() * normalLengths[i];
                        positions[i * 4 + 0] = root;
                        positions[i * 4 + 1] = root + direction * 2.0f / 3.0f;
                        positions[i * 4 + 2] = root + direction * 2.0f / 3.0f;
                        positions[i * 4 + 3] = root + direction;
                    }
                    this.positionBuffer = positions.GenVertexBuffer(VBOConfig.DVec3, varNameInShader, BufferUsage.StaticDraw);
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
