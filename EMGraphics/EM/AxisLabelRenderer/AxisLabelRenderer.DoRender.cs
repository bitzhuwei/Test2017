namespace EMGraphics
{
    /// <summary>
    /// Renders a label that always faces camera in 3D space.
    /// </summary>
    public partial class AxisLabelRenderer
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

            base.DoRender(arg);
        }
    }
}