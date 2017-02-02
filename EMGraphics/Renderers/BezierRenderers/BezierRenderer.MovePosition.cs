﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace EMGraphics
{
    public partial class BezierRenderer
    {
        /// <summary>
        /// Move vertexes' position accroding to difference on screen.
        /// <para>根据<paramref name="differenceOnScreen"/>来修改指定索引处的顶点位置。</para>
        /// </summary>
        /// <param name="differenceOnScreen"></param>
        /// <param name="viewMatrix"></param>
        /// <param name="projectionMatrix"></param>
        /// <param name="viewport"></param>
        /// <param name="positionIndexes"></param>
        public override void MovePositions(Point differenceOnScreen, mat4 viewMatrix, mat4 projectionMatrix, vec4 viewport, IEnumerable<uint> positionIndexes)
        {
            base.MovePositions(differenceOnScreen, viewMatrix, projectionMatrix, viewport, positionIndexes);

            this.needsUpdating = true;
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
        public override void MovePositions(Point differenceOnScreen,
            mat4 viewMatrix, mat4 projectionMatrix, vec4 viewport, params uint[] positionIndexes)
        {
            base.MovePositions(differenceOnScreen, viewMatrix, projectionMatrix, viewport, positionIndexes);

            this.needsUpdating = true;
        }

        private void UpdateEvaluator()
        {
            IntPtr pointer = this.PositionBuffer.MapBuffer(MapBufferAccess.ReadOnly);
            if (pointer == IntPtr.Zero)// this happens when not glMakeCurrent().
            {
                //this.PositionBuffer.Unbind();
                return;
            }

            int length = this.PositionBuffer.Length;
            mat4 modelMatrix = this.GetModelMatrix().Value;
            var array = new vec3[length];
            unsafe
            {
                var bufferHeader = (vec3*)pointer.ToPointer();
                for (int i = 0; i < length; i++)
                {
                    array[i] = new vec3(modelMatrix * new vec4(bufferHeader[i], 1.0f));
                }
            }
            this.Evaluator.Setup(array);
            this.PositionBuffer.UnmapBuffer();
        }
    }
}