﻿using System;

namespace EMGraphics
{
    internal class ZeroIndexPointInQuadSearcher : ZeroIndexPointSearcher
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="x">mouse position(Left Down is (0, 0)).</param>
        /// <param name="y">mouse position(Left Down is (0, 0)).</param>
        /// <param name="lastVertexId"></param>
        /// <param name="modernRenderer"></param>
        /// <returns></returns>
        internal override uint Search(RenderEventArgs arg,
            int x, int y,
            uint lastVertexId, ZeroIndexRenderer modernRenderer)
        {
            OneIndexBuffer buffer = GLBuffer.Create(IndexBufferElementType.UInt, 4, DrawMode.Points, BufferUsage.StaticDraw);
            unsafe
            {
                var array = (uint*)buffer.MapBuffer(MapBufferAccess.WriteOnly);
                array[0] = lastVertexId - 0;
                array[1] = lastVertexId - 1;
                array[2] = lastVertexId - 2;
                array[3] = lastVertexId - 3;
                buffer.UnmapBuffer();
            }
            modernRenderer.Render4InnerPicking(arg, buffer);
            uint id = ColorCodedPicking.ReadStageVertexId(x, y);

            buffer.Dispose();

            if (lastVertexId - 3 <= id && id <= lastVertexId - 0)
            { return id; }
            else
            { throw new Exception("This should not happen!"); }
        }
    }
}