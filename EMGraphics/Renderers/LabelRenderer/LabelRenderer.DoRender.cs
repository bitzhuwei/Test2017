﻿namespace EMGraphics
{
    /// <summary>
    /// Renders a label that always faces camera in 3D space.
    /// </summary>
    public partial class LabelRenderer
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="arg"></param>
        protected override void DoRender(RenderEventArgs arg)
        {
            this.SetUniform("billboardCenter_worldspace", this.WorldPosition);

            if (labelHeightRecord.IsMarked())
            {
                this.SetUniform("labelHeight", this.LabelHeight);
                labelHeightRecord.CancelMark();
            }
            if (textRecord.IsMarked())
            {
                if (this.DataSource != null)
                {
                    (this.DataSource as TextModel).SetText(this.text, this.fontTexture);
                }
            }
            if (this.textColorRecord.IsMarked())
            {
                this.SetUniform("textColor", this.textColor.ToVec3());
                this.textColorRecord.CancelMark();
            }

            if (discardTransparencyRecord.IsMarked())
            {
                bool discard = this.DiscardTransparency;
                this.SetUniform("discardTransparency", discard);
                //this.blendState.Enabled = discard;
                discardTransparencyRecord.CancelMark();
            }
            int[] viewport = OpenGL.GetViewport();
            this.SetUniform("viewportSize", new vec2(viewport[2], viewport[3]));
            mat4 projection = arg.Camera.GetProjectionMatrix();
            mat4 view = arg.Camera.GetViewMatrix();
            this.SetUniform("projection", projection);
            this.SetUniform("view", view);

            if (this.KeepFront)
            {
                // 把所有在此之前渲染的内容都推到最远。
                // Push all rendered stuff to farest position.
                OpenGL.Clear(OpenGL.GL_DEPTH_BUFFER_BIT);
            }

            base.DoRender(arg);
        }
    }
}