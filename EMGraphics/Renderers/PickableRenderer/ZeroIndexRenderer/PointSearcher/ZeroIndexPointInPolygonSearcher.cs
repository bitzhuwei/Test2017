﻿using System;

namespace EMGraphics
{
    internal class ZeroIndexPointInPolygonSearcher : ZeroIndexPointSearcher
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
            ZeroIndexBuffer zeroIndexBuffer = modernRenderer.IndexBuffer;
            // when the temp index buffer could be long, it's no longer needed.
            // what a great OpenGL API design!
            ZeroIndexBuffer indexBuffer = ZeroIndexBuffer.Create(DrawMode.Points, zeroIndexBuffer.FirstVertex, zeroIndexBuffer.RenderingVertexCount, zeroIndexBuffer.PrimCount);
            modernRenderer.Render4InnerPicking(arg, indexBuffer);
            uint id = ColorCodedPicking.ReadStageVertexId(x, y);

            indexBuffer.Dispose();

            if (zeroIndexBuffer.FirstVertex <= id
                && id < zeroIndexBuffer.FirstVertex + zeroIndexBuffer.RenderingVertexCount)
            { return id; }
            else
            { throw new Exception("This should not happen!"); }
        }
    }
}