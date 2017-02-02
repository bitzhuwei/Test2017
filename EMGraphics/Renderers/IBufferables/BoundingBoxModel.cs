﻿using System;

namespace EMGraphics
{
    //
    //        2-------------------3
    //      / .                  /|
    //     /  .                 / |
    //    /   .                /  |
    //   /    .               /   |
    //  /     .              /    |
    // 6--------------------7     |
    // |      .             |     |
    // |      0 . . . . . . |. . .1
    // |     .              |    /
    // |    .               |   /
    // |   .                |  /
    // |  .                 | /
    // | .                  |/
    // 4 -------------------5
    //
    /// <summary>
    /// bounding box's model.
    /// </summary>
    public partial class BoundingBoxModel : IBufferable
    {
        /// <summary>
        /// eight vertexes.
        /// </summary>
        private static readonly vec3[] positions = new vec3[]
        {
            new vec3(-1, -1, -1),// 0
            new vec3(-1, -1, +1),// 1
            new vec3(-1, +1, -1),// 2
            new vec3(-1, +1, +1),// 3
            new vec3(+1, -1, -1),// 4
            new vec3(+1, -1, +1),// 5
            new vec3(+1, +1, -1),// 6
            new vec3(+1, +1, +1),// 7
        };

        /// <summary>
        /// render in GL_QUADS.
        /// </summary>
        private static readonly byte[] indexes = new byte[24]
        {
            1, 3, 7, 5, 0, 4, 6, 2,
            2, 6, 7, 3, 0, 1, 5, 4,
            4, 5, 7, 6, 0, 2, 3, 1,
        };

        /// <summary>
        /// position
        /// </summary>
        public const string strPosition = "position";

        private VertexBuffer positionBuffer = null;
        private IndexBuffer indexBuffer = null;
        private vec3 lengths;

        /// <summary>
        /// bounding box.
        /// </summary>
        /// <param name="lengths">bounding box's length at x, y, z axis.</param>
        public BoundingBoxModel(vec3 lengths)
        {
            this.lengths = lengths;
        }

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
                    var array = new vec3[positions.Length];
                    for (int i = 0; i < positions.Length; i++)
                    {
                        array[i] = positions[i] / 2 * this.lengths;
                    }
                    VertexBuffer buffer = array.GenVertexBuffer(VBOConfig.Vec3, varNameInShader, BufferUsage.StaticDraw);
                    this.positionBuffer = buffer;
                }
                return this.positionBuffer;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IndexBuffer GetIndexBuffer()
        {
            if (this.indexBuffer == null)
            {
                //int length = indexes.Length;
                //OneIndexBuffer buffer = Buffer.Create(IndexElementType.UByte, length, DrawMode.Quads, BufferUsage.StaticDraw);
                //unsafe
                //{
                //    IntPtr pointer = buffer.MapBuffer(MapBufferAccess.WriteOnly);
                //    var array = (byte*)pointer;
                //    for (int i = 0; i < indexes.Length; i++)
                //    {
                //        array[i] = indexes[i];
                //    }
                //    buffer.UnmapBuffer();
                //}
                //this.indexBuffer = buffer;
                // another way to do this:
                this.indexBuffer = indexes.GenIndexBuffer(DrawMode.Quads, BufferUsage.StaticDraw);
            }

            return this.indexBuffer;
        }

        /// <summary>
        /// Uses <see cref="ZeroIndexBuffer"/> or <see cref="OneIndexBuffer"/>.
        /// </summary>
        /// <returns></returns>
        public bool UsesZeroIndexBuffer() { return false; }
    }
}