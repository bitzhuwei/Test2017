﻿using System;
namespace EMGraphics
{
    /// <summary>
    /// Teapot.
    /// <para>Uses <see cref="OneIndexBuffer"/></para>
    /// </summary>
    public class Teapot : IBufferable
    {
        /// <summary>
        ///
        /// </summary>
        public Teapot()
        {
            this.model = new TeapotModel();
        }

        /// <summary>
        ///
        /// </summary>
        public const string strPosition = "position";

        /// <summary>
        ///
        /// </summary>
        public const string strColor = "color";

        /// <summary>
        ///
        /// </summary>
        public const string strNormal = "normal";

        private TeapotModel model;
        private VertexBuffer positionBuffer;
        private VertexBuffer colorBuffer;
        private VertexBuffer normalBuffer;

        /// <summary>
        ///
        /// </summary>
        /// <param name="bufferName"></param>
        /// <param name="varNameInShader"></param>
        /// <returns></returns>
        public VertexBuffer GetVertexAttributeBuffer(string bufferName, string varNameInShader)
        {
            if (bufferName == strPosition)
            {
                if (this.positionBuffer == null)
                {
                    vec3[] positions = model.GetPositions();
                    //int length = positions.Length;
                    //VertexBuffer buffer = VertexBuffer.Create(typeof(float), length, VBOConfig.Vec3, varNameInShader, BufferUsage.StaticDraw);
                    //unsafe
                    //{
                    //    IntPtr pointer = buffer.MapBuffer(MapBufferAccess.WriteOnly);
                    //    var array = (float*)pointer;
                    //    for (int i = 0; i < positions.Length; i++)
                    //    {
                    //        array[i] = positions[i];
                    //    }
                    //    buffer.UnmapBuffer();
                    //}
                    //this.positionBuffer = buffer;
                    // another way to do this:
                    this.positionBuffer = positions.GenVertexBuffer(VBOConfig.Vec3, varNameInShader, BufferUsage.StaticDraw);
                }
                return this.positionBuffer;
            }
            else if (bufferName == strColor)
            {
                if (this.colorBuffer == null)
                {
                    vec3[] normals = model.GetNormals();
                    for (int i = 0; i < normals.Length; i++)
                    {
                        if (normals[i].x < 0) { normals[i].x = -normals[i].x; }
                        if (normals[i].y < 0) { normals[i].y = -normals[i].y; }
                        if (normals[i].z < 0) { normals[i].z = -normals[i].z; }
                    }
                    //int length = normals.Length;
                    //VertexBuffer buffer = VertexBuffer.Create(typeof(float), length, VBOConfig.Vec3, varNameInShader, BufferUsage.StaticDraw);
                    //unsafe
                    //{
                    //    IntPtr pointer = buffer.MapBuffer(MapBufferAccess.WriteOnly);
                    //    var array = (float*)pointer;
                    //    for (int i = 0; i < normals.Length; i++)
                    //    {
                    //        array[i] = normals[i];
                    //    }
                    //    buffer.UnmapBuffer();
                    //}
                    //this.colorBuffer = buffer;
                    // another way to do this:
                    this.colorBuffer = normals.GenVertexBuffer(VBOConfig.Vec3, varNameInShader, BufferUsage.StaticDraw);
                }
                return this.colorBuffer;
            }
            else if (bufferName == strNormal)
            {
                if (this.normalBuffer == null)
                {
                    vec3[] normals = model.GetNormals();
                    //int length = normals.Length;
                    //VertexBuffer buffer = VertexBuffer.Create(typeof(float), length, VBOConfig.Vec3, varNameInShader, BufferUsage.StaticDraw);
                    //unsafe
                    //{
                    //    IntPtr pointer = buffer.MapBuffer(MapBufferAccess.WriteOnly);
                    //    var array = (float*)pointer;
                    //    for (int i = 0; i < normals.Length; i++)
                    //    {
                    //        array[i] = normals[i];
                    //    }
                    //    buffer.UnmapBuffer();
                    //}
                    //this.normalBuffer = buffer;
                    // another way to do this:
                    this.normalBuffer = normals.GenVertexBuffer(VBOConfig.Vec3, varNameInShader, BufferUsage.StaticDraw);
                }
                return this.normalBuffer;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IndexBuffer GetIndexBuffer()
        {
            if (indexBuffer == null)
            {
                EMGraphics.TeapotModel.Face[] faces = model.GetFaces();
                int length = faces.Length * 3;
                OneIndexBuffer buffer = GLBuffer.Create(IndexBufferElementType.UShort, length, DrawMode.Triangles, BufferUsage.StaticDraw);
                unsafe
                {
                    IntPtr pointer = buffer.MapBuffer(MapBufferAccess.WriteOnly);
                    var array = (ushort*)pointer;
                    for (int i = 0; i < faces.Length; i++)
                    {
                        array[i * 3 + 0] = (ushort)(faces[i].vertexId1 - 1);
                        array[i * 3 + 1] = (ushort)(faces[i].vertexId2 - 1);
                        array[i * 3 + 2] = (ushort)(faces[i].vertexId3 - 1);
                    }
                    buffer.UnmapBuffer();
                }
                this.indexBuffer = buffer;
            }

            return indexBuffer;
        }

        private IndexBuffer indexBuffer = null;

        /// <summary>
        /// Uses <see cref="ZeroIndexBuffer"/> or <see cref="OneIndexBuffer"/>.
        /// </summary>
        /// <returns></returns>
        public bool UsesZeroIndexBuffer() { return false; }

        /// <summary>
        ///
        /// </summary>
        public vec3 Size { get { return new vec3(6.42963028f, 3.15f, 4.0f); } }
    }
}