﻿namespace EMGraphics
{
    /// <summary>
    /// http://www.cnblogs.com/bitzhuwei/p/polygon-offset-for-stitching-andz-fighting.html
    /// </summary>
    public class PolygonOffsetPointState : PolygonOffsetState
    {
        /// <summary>
        /// http://www.cnblogs.com/bitzhuwei/p/polygon-offset-for-stitching-andz-fighting.html
        /// </summary>
        public PolygonOffsetPointState()
            : base(PolygonOffset.Point, true)
        { }

        /// <summary>
        /// http://www.cnblogs.com/bitzhuwei/p/polygon-offset-for-stitching-andz-fighting.html
        /// </summary>
        /// <param name="pullNear"></param>
        public PolygonOffsetPointState(bool pullNear)
            : base(PolygonOffset.Point, pullNear)
        { }
    }
}