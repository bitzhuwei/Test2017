﻿using System.Drawing;

namespace EMGraphics
{
    /// <summary>
    ///
    /// </summary>
    public class AccumBufferState : GLState
    {
        private vec3 clearValue = new vec3(0, 0, 0);

        /// <summary>
        ///
        /// </summary>
        public Color ClearValue
        {
            get
            {
                return Color.FromArgb(
                    (int)(clearAlphazValue * 255),
                    (int)(clearValue.x * 255),
                    (int)(clearValue.y * 255),
                    (int)(clearValue.z * 255));
            }
            set
            {
                this.clearValue.x = value.R / 255.0f;
                this.clearValue.y = value.G / 255.0f;
                this.clearValue.z = value.B / 255.0f;
            }
        }

        private float clearAlphazValue = 0.0f;

        /// <summary>
        /// Alpha value.
        /// </summary>
        public float ClearAlphaValue
        {
            get { return clearAlphazValue; }
            set { clearAlphazValue = value; }
        }

        /// <summary>
        ///
        /// </summary>
        public AccumBufferState() { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="clearColor"></param>
        /// <param name="clearAlphazValue">Ranges between [0, 1.0].</param>
        public AccumBufferState(Color clearColor, float clearAlphazValue)
        {
            this.ClearValue = clearColor;
            this.clearAlphazValue = clearAlphazValue;
        }

        private float[] original = new float[4];

        /// <summary>
        ///
        /// </summary>
        public override string ToString()
        {
            return string.Format("glClearAccum({0}, {1}, {2}, {3});",
                clearValue.x, clearValue.y, clearValue.z, clearAlphazValue);
        }

        /// <summary>
        ///
        /// </summary>
        protected override void StateOn()
        {
            OpenGL.GetFloat(GetTarget.AccumClearValue, original);

            OpenGL.ClearAccum(clearValue.x, clearValue.y, clearValue.z, clearAlphazValue);
        }

        /// <summary>
        ///
        /// </summary>
        protected override void StateOff()
        {
            OpenGL.ClearAccum(original[0], original[1], original[2], original[3]);
        }
    }
}