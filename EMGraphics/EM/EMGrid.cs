using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMGraphics
{
    /// <summary>
    /// 
    /// </summary>
    public class EMGrid : IBufferable, IModelSpace
    {

        public string Label { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertexPositions"></param>
        /// <param name="normals"></param>
        /// <param name="triangles"></param>
        /// <param name="label"></param>
        public EMGrid(vec3[] vertexPositions, vec3[] normals, Triangle[] triangles, BoundingBox box, string label)
        {
            this.vertexPositions = vertexPositions;
            this.vertexNormals = normals;
            this.triangles = triangles;
            this.Label = label;
            this.ModelSize = box.MaxPosition - box.MinPosition;
            this.WorldPosition = box.MaxPosition / 2.0f + box.MinPosition / 2.0f;
            this.RotationAngleDegree = 0;
            this.RotationAxis = new vec3(0, 1, 0);
            this.Scale = new vec3(1, 1, 1);

            this.vertexCount = vertexPositions.Length;
        }

        public const string strPosition = "position";
        private VertexBuffer positionBuffer;
        private vec3[] vertexPositions;

        public vec3[] VertexPositions
        {
            get { return vertexPositions; }
        }

        public const string strNormal = "normal";
        private VertexBuffer normalBuffer;
        private vec3[] vertexNormals;

        public vec3[] VertexNormals
        {
            get { return vertexNormals; }
        }

        public const string strCloudColor = "cloudColor";
        private VertexBuffer cloudColorBuffer;

        //public const string strIndex = "index";
        private IndexBuffer indexBuffer = null;
        private Triangle[] triangles;

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
            else if (bufferName == strNormal)
            {
                if (this.normalBuffer == null)
                {
                    this.normalBuffer = this.vertexNormals.GenVertexBuffer(VBOConfig.Vec3, varNameInShader, BufferUsage.StaticDraw);
                }
                return this.normalBuffer;
            }
            else if (bufferName == strCloudColor)
            {
                if (this.cloudColorBuffer == null)
                {
                    this.cloudColorBuffer = this.vertexNormals.GenVertexBuffer(VBOConfig.Vec3, varNameInShader, BufferUsage.DynamicDraw);
                }
                return this.cloudColorBuffer;
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
                OneIndexBuffer buffer = GLBuffer.Create(IndexBufferElementType.UInt, this.triangles.Length * 3, DrawMode.Triangles, BufferUsage.StaticDraw);
                unsafe
                {
                    IntPtr pointer = buffer.MapBuffer(MapBufferAccess.WriteOnly);
                    var array = (uint*)pointer;
                    for (int i = 0; i < this.triangles.Length; i++)
                    {
                        array[i * 3 + 0] = (uint)this.triangles[i].Num1;
                        array[i * 3 + 1] = (uint)this.triangles[i].Num2;
                        array[i * 3 + 2] = (uint)this.triangles[i].Num3;
                    }
                    buffer.UnmapBuffer();
                }
                this.indexBuffer = buffer;
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
