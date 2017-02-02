﻿using System.ComponentModel;

namespace EMGraphics
{
    public partial class SceneObject
    {
        private GLStateList groupStateList = new GLStateList();

        /// <summary>
        /// Turn on before rendering something and turn off after rendering.
        /// </summary>
        [Category(strSceneObject)]
        [Description("Turn on before rendering something and turn off after rendering.")]
        public GLStateList GroupStateList { get { return groupStateList; } }

        ///// <summary>
        ///// Occurs before this object and all of its children's rendering.
        ///// </summary>
        //public event EventHandler BeforeRendering;

        ///// <summary>
        ///// Occurs before this object and all of its children's rendering.
        ///// </summary>
        //internal void DoBeforeRendering()
        //{
        //    EventHandler handler = this.BeforeRendering;
        //    if (handler != null)
        //    {
        //        handler(this, new EventArgs());
        //    }
        //}

        /// <summary>
        ///
        /// </summary>
        /// <param name="arg"></param>
        public void Render(RenderEventArgs arg)
        {
            RendererBase renderer = this.Renderer;
            if (renderer != null)
            {
                renderer.Render(arg);
            }
        }

        ///// <summary>
        ///// Occurs after this object and all of its children's rendering.
        ///// </summary>
        //public event EventHandler AfterRendering;

        ///// <summary>
        ///// Occurs after this object and all of its children's rendering.
        ///// </summary>
        //internal void DoAfterRendering()
        //{
        //    EventHandler handler = this.AfterRendering;
        //    if (handler != null)
        //    {
        //        handler(this, new EventArgs());
        //    }
        //}
    }
}