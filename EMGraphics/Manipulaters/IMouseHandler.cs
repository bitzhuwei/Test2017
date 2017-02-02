﻿using System.Windows.Forms;

namespace EMGraphics
{
    /// <summary>
    /// Handle mouse events.
    /// </summary>
    public interface IMouseHandler
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void canvas_MouseDown(object sender, MouseEventArgs e);

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void canvas_MouseMove(object sender, MouseEventArgs e);

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void canvas_MouseUp(object sender, MouseEventArgs e);

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void canvas_MouseWheel(object sender, MouseEventArgs e);
    }
}