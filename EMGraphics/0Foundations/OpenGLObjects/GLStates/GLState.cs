﻿using System.ComponentModel;
using System.Drawing.Design;

namespace EMGraphics
{
    /// <summary>
    ///
    /// </summary>
    [Editor(typeof(PropertyGridEditor), typeof(UITypeEditor))]
    public abstract class GLState
    {
        private bool inUse = false;

        /// <summary>
        /// You want to use this switch?
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        ///
        /// </summary>
        public GLState()
        {
            this.Enabled = true;
        }

        /// <summary>
        ///
        /// </summary>
        public void On()
        {
            if (this.Enabled)
            {
                this.inUse = true;
                this.StateOn();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void Off()
        {
            if (this.inUse)
            {
                this.inUse = false;
                this.StateOff();
            }
        }

        /// <summary>
        ///
        /// </summary>
        protected abstract void StateOn();

        /// <summary>
        ///
        /// </summary>
        protected abstract void StateOff();
    }
}