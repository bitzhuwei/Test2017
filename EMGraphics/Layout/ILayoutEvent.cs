﻿namespace EMGraphics
{
    /// <summary>
    ///
    /// </summary>
    public interface ILayoutEvent
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        bool DoBeforeLayout();

        /// <summary>
        ///
        /// </summary>
        void DoAfterLayout();
    }
}