﻿namespace EMGraphics
{
    public partial class CenterAxisLabelRenderer
    {
        /// <summary>
        ///
        /// </summary>
        protected override void DoInitialize()
        {
            base.DoInitialize();

            //int[] viewport = OpenGL.GetViewport();
            //this.SetUniform("pixelScale", (float)viewport[2]);
            //this.SetUniform("fontHeight", (float)fontResource.FontHeight);
            //this.SetUniform("textColor", new vec3(1, 0, 0));
            this.SetUniform("fontTexture", this.fontTexture.TextureObj);
        }
    }
}