﻿namespace EMGraphics
{
    /// <summary>
    /// set and reset viewport using glViewport();
    /// </summary>
    public class ViewportState : GLState
    {
        private int[] original = new int[4];

        /// <summary>
        /// set and reset viewport using glViewport();
        /// </summary>
        public ViewportState()
        {
            int[] viewport = OpenGL.GetViewport();

            this.Init(viewport[0], viewport[1], viewport[2], viewport[3]);
        }

        /// <summary>
        /// set and reset viewport using glViewport();
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public ViewportState(int x, int y, int width, int height)
        {
            this.Init(x, y, width, height);
        }

        /// <summary>
        /// set and reset viewport using glViewport();
        /// </summary>
        /// <param name="viewport"></param>
        public ViewportState(int[] viewport)
        {
            this.Init(viewport[0], viewport[1], viewport[2], viewport[3]);
        }

        private void Init(int x, int y, int width, int height)
        {
            this.X = x; this.Y = y; this.Width = width; this.Height = height;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("glViewport({0}, {1}, {2}, {3});",
                X, Y, Width, Height);
        }

        /// <summary>
        ///
        /// </summary>
        protected override void StateOn()
        {
            OpenGL.GetInteger(GetTarget.Viewport, original);

            OpenGL.Viewport(X, Y, Width, Height);
        }

        /// <summary>
        ///
        /// </summary>
        protected override void StateOff()
        {
            OpenGL.Viewport(original[0], original[1], original[2], original[3]);
        }

        /// <summary>
        ///
        /// </summary>
        public int X { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int Height { get; set; }
    }
}