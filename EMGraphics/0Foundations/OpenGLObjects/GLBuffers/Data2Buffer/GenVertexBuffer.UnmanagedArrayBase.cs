﻿using System;
using System.Runtime.InteropServices;

namespace EMGraphics
{
    public static partial class Data2Buffer
    {
        /// <summary>
        /// 生成顶点属性Buffer。描述顶点的位置或颜色或UV等各种属性。
        /// <para>每个<see cref="VertexBuffer"/>仅描述其中一个属性。</para>
        /// <para>Vertex Buffer Object that describes vertex' property(position, color, uv coordinate, etc.).</para>
        /// <para>Each <see cref="VertexBuffer"/> describes only 1 property.</para>
        /// </summary>
        /// <param name="array"></param>
        /// <param name="config">This <paramref name="config"/> decides parameters' values in glVertexAttribPointer(attributeLocation, size, type, false, 0, IntPtr.Zero);</param>
        /// <param name="varNameInVertexShader">此顶点属性VBO对应于vertex shader中的哪个in变量？<para>Mapping variable's name in vertex shader.</para></param>
        /// <param name="usage"></param>
        /// <param name="instancedDivisor">0: not instanced. 1: instanced divisor is 1.</param>
        /// <param name="patchVertexes">How many vertexes makes a patch? No patch if <paramref name="patchVertexes"/> is 0.</param>
        /// <returns></returns>
        public static VertexBuffer GenVertexBuffer(this UnmanagedArrayBase array, VBOConfig config, string varNameInVertexShader, BufferUsage usage, uint instancedDivisor = 0, int patchVertexes = 0)
        {
            if (glGenBuffers == null)
            {
                InitFunctions();
            }

            uint[] buffers = new uint[1];
            glGenBuffers(1, buffers);
            const uint target = OpenGL.GL_ARRAY_BUFFER;
            glBindBuffer(target, buffers[0]);
            glBufferData(target, array.ByteLength, array.Header, (uint)usage);
            glBindBuffer(target, 0);

            var buffer = new VertexBuffer(
                buffers[0], config, varNameInVertexShader, array.Length, array.ByteLength, instancedDivisor, patchVertexes);

            return buffer;
        }
    }
}