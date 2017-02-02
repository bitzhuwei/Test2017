﻿namespace EMGraphics
{
    /// <summary>
    ///
    /// </summary>
    public class LineWidthState : GLState
    {
        private static float min;
        private static float max;

        static LineWidthState()
        {
            OpenGL.LineWidthRange(out min, out max);
            //OpenGL.GetFloat(GetTarget.LineWidthGranularity, lineWidthRange);//TODO: what does LineWidthGranularity mean?
        }

        /// <summary>
        ///
        /// </summary>
        public float MinLineWidth { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public float MaxLineWidth { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public float LineWidth { get; set; }

        /// <summary>
        ///
        /// </summary>
        public LineWidthState() : this(1.0f) { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="lineWidth"></param>
        public LineWidthState(float lineWidth)
        {
            this.LineWidth = lineWidth;
            this.MinLineWidth = min;
            this.MaxLineWidth = max;
        }

        private float[] original = new float[1];

        /// <summary>
        ///
        /// </summary>
        public override string ToString()
        {
            return string.Format("Line Width: {0}", LineWidth);
        }

        /// <summary>
        ///
        /// </summary>
        protected override void StateOn()
        {
            OpenGL.GetFloat(GetTarget.LineWidth, original);

            OpenGL.LineWidth(LineWidth);
        }

        /// <summary>
        ///
        /// </summary>
        protected override void StateOff()
        {
            OpenGL.LineWidth(original[0]);
        }
    }
}