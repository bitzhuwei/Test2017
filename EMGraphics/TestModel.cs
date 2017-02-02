﻿using CSharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMGraphics
{
    /// <summary>
    /// 
    /// </summary>
    public class TestModel : IBufferable, IModelSpace
    {
        public TestModel(vec3[] vertexPositions, vec3[] vertexColors, Triangle[] triangles)
        {
            BoundingBox box = vertexPositions.Move2Center();
            this.vertexPositions = vertexPositions;
            this.ModelSize = box.MaxPosition - box.MinPosition;
            this.WorldPosition = box.MaxPosition / 2.0f + box.MinPosition / 2.0f;
            this.vertexColors = vertexColors;
            this.triangles = triangles;
            this.RotationAngleDegree = 0;
            this.RotationAxis = new vec3(0, 1, 0);
            this.Scale = new vec3(1, 1, 1);
        }

        public const string strPosition = "position";
        private VertexBuffer positionBuffer;
        private vec3[] vertexPositions;

        public const string strColor = "color";
        private VertexBuffer colorBuffer;
        private vec3[] vertexColors;

        //public const string strIndex = "index";
        private IndexBuffer indexBuffer = null;
        private Triangle[] triangles;

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
