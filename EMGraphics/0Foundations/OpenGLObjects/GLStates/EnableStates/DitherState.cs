﻿namespace EMGraphics
{
    /// <summary>
    ///
    /// </summary>
    public class DitherState : EnableState
    {
        /// <summary>
        ///
        /// </summary>
        public DitherState()
            : base(OpenGL.GL_DITHER, true)
        { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="enableCapacity">true for enable, false for disable</param>
        public DitherState(bool enableCapacity)
            : base(OpenGL.GL_DITHER, enableCapacity)
        { }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this.EnableCapacity)
            { return "OpenGL.Enable(GL_DITHER);"; }
            else
            { return "OpenGL.Disable(GL_DITHER);"; }
        }
    }
}