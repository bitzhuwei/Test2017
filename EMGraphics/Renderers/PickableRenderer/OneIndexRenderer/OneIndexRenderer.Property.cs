﻿namespace EMGraphics
{
    partial class OneIndexRenderer
    {
        // TODO: 将来优化时以此作为分段操作的界限。
        //private int mapBufferRangeLength = 2 * 2 * 2 * 2 * 3 * 3 * 3 * 3 * 10;

        ///// <summary>
        ///// 如果VBO太长，就应该据此分段执行各种MapBufferRange操作。
        ///// </summary>
        //public int MapBufferRangeLength
        //{
        //    get { return mapBufferRangeLength; }
        //    set
        //    {
        //        if (value < sizeof(uint) * 4)
        //        {
        //            mapBufferRangeLength = sizeof(uint) * 4;
        //        }
        //        else
        //        {
        //            mapBufferRangeLength = value;
        //        }
        //    }
        //}

        /// <summary>
        /// 此渲染器的索引Buffer。
        /// </summary>
        public new OneIndexBuffer IndexBuffer { get { return this.indexBuffer as OneIndexBuffer; } }
    }
}