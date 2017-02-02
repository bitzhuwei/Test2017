﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace EMGraphics
{
    public partial class PickableRenderer
    {
        /// <summary>
        /// Position buffer.
        /// </summary>
        [Browsable(false)]
        public VertexBuffer PositionBuffer { get { return this.innerPickableRenderer.PositionBuffer; } }

        /// <summary>
        /// Move vertexes' position accroding to difference on screen.
        /// <para>根据<paramref name="differenceOnScreen"/>来修改指定索引处的顶点位置。</para>
        /// </summary>
        /// <param name="differenceOnScreen"></param>
        /// <param name="viewMatrix"></param>
        /// <param name="projectionMatrix"></param>
        /// <param name="viewport"></param>
        /// <param name="positionIndexes"></param>
        public virtual void MovePositions(Point differenceOnScreen,
            mat4 viewMatrix, mat4 projectionMatrix, vec4 viewport, IEnumerable<uint> positionIndexes)
        {
            if (positionIndexes == null) { return; }
            if (positionIndexes.Count() == 0) { return; }

            this.innerPickableRenderer.MovePositions(differenceOnScreen, viewMatrix, projectionMatrix, viewport, positionIndexes);
        }

        /// <summary>
        /// Move vertexes' position accroding to difference on screen.
        /// <para>根据<paramref name="differenceOnScreen"/>来修改指定索引处的顶点位置。</para>
        /// </summary>
        /// <param name="differenceOnScreen"></param>
        /// <param name="viewMatrix"></param>
        /// <param name="projectionMatrix"></param>
        /// <param name="viewport"></param>
        /// <param name="positionIndexes"></param>
        public virtual void MovePositions(Point differenceOnScreen,
            mat4 viewMatrix, mat4 projectionMatrix, vec4 viewport, params uint[] positionIndexes)
        {
            if (positionIndexes == null) { return; }
            if (positionIndexes.Length == 0) { return; }

            this.innerPickableRenderer.MovePositions(differenceOnScreen, viewMatrix, projectionMatrix, viewport, positionIndexes);
        }
    }
}