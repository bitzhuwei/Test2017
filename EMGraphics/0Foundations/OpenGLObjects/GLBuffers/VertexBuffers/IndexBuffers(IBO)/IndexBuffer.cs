﻿using System.ComponentModel;
using System.Drawing.Design;

namespace EMGraphics
{
    /// <summary>
    /// 索引buffer渲染器的基类。
    /// <para>Base type for Vertex Buffer Object' pointer storing vertex' index.</para>
    /// </summary>
    [Browsable(true)]
    [Editor(typeof(IndexBufferEditor), typeof(UITypeEditor))]
    public abstract class IndexBuffer : GLBuffer
    {
        /// <summary>
        /// 用哪种方式渲染各个顶点？（OpenGL.GL_TRIANGLES etc.）
        /// </summary>
        public DrawMode Mode { get; set; }

        /// <summary>
        /// 索引buffer渲染器的基类。
        /// <para>Base type for Vertex Buffer Object' pointer storing vertex' index.</para>
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="bufferId"></param>
        /// <param name="length">此VBO含有多个个元素？<para>How many elements?</para></param>
        /// <param name="byteLength">此VBO中的数据在内存中占用多少个字节？<para>How many bytes in this buffer?</para></param>
        /// <param name="primCount">primCount in instanced rendering.</param>
        internal IndexBuffer(DrawMode mode, uint bufferId, int length, int byteLength, int primCount)
            : base(bufferId, length, byteLength)
        {
            this.Mode = mode;
            this.PrimCount = primCount;
        }

        /// <summary>
        /// 执行此VBO的渲染操作。
        /// <para>Render using this VBO.</para>
        /// </summary>
        /// <param name="arg"></param>
        public abstract void Render(RenderEventArgs arg);

        /// <summary>
        /// primCount in instanced rendering.
        /// </summary>
        public int PrimCount { get; private set; }
    }
}