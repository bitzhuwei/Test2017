﻿using System;

namespace EMGraphics
{
    internal class ZeroIndexLineInQuadStripSearcher : ZeroIndexLineSearcher
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
        internal override uint[] Search(RenderEventArgs arg,
            int x, int y,
            uint lastVertexId, ZeroIndexRenderer modernRenderer)
        {
            OneIndexBuffer buffer = GLBuffer.Create(IndexBufferElementType.UInt, 8, DrawMode.Lines, BufferUsage.StaticDraw);
            unsafe
            {
                var array = (uint*)buffer.MapBuffer(MapBufferAccess.WriteOnly);
                array[0] = lastVertexId - 0; array[1] = lastVertexId - 2;
                array[2] = lastVertexId - 2; array[3] = lastVertexId - 3;
                array[4] = lastVertexId - 3; array[5] = lastVertexId - 1;
                array[6] = lastVertexId - 1; array[7] = lastVertexId - 0;
                buffer.UnmapBuffer();
            }
            modernRenderer.Render4InnerPicking(arg, buffer);
            uint id = ColorCodedPicking.ReadStageVertexId(x, y);

            buffer.Dispose();
            if (id + 2 == lastVertexId)
            { return new uint[] { lastVertexId - 0, lastVertexId - 2, }; }
            else if (id + 3 == lastVertexId)
            { return new uint[] { lastVertexId - 2, lastVertexId - 3 }; }
            else if (id + 1 == lastVertexId)
            { return new uint[] { lastVertexId - 3, lastVertexId - 1, }; }
            else if (id + 0 == lastVertexId)
            { return new uint[] { lastVertexId - 1, lastVertexId - 0, }; }
            else
            { throw new Exception("This should not happen!"); }
        }
    }
}