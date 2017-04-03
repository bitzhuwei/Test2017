namespace EMGraphics
{
    /// <summary>
    /// Renders a label that always faces camera in 3D space.
    /// </summary>
    public partial class CenterAxisLabelRenderer
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
            {
                const float deltaDistance = 0.05f;
                mat4 projection = arg.Camera.GetProjectionMatrix();
                mat4 view = arg.Camera.GetViewMatrix();
                {
                    IOrthoViewCamera camera = arg.Camera;
                    float newBottom = (float)camera.Bottom;
                    float newTop = (float)camera.Top;
                    float newLeft = (float)camera.Left;
                    float newRight = (float)camera.Right;
                    float newWidth = newRight - newLeft;
                    float newHeight = newTop - newBottom;
                    if (newWidth >= newHeight)
                    {
                        float scale = newHeight / 2.0f;
                        this.WorldPosition = this.StandardOffset * scale;
                    }
                    else
                    {
                        float scale = newWidth / 2.0f;
                        this.WorldPosition = this.StandardOffset * scale;
                    }
                }
                mat4 model = this.GetModelMatrix().Value;
                this.SetUniform("projection", projection);
                this.SetUniform("view", view * model);
            }
            //{
            //    const float left = -1, bottom = -1, right = 1, top = 1, near = -100, far = 100;
            //    mat4 projection;
            //    mat4 view = arg.Camera.GetViewMatrix();
            //    mat4 model = this.GetModelMatrix().Value;
            //    {
            //        IOrthoViewCamera camera = arg.Camera;
            //        float newBottom = (float)camera.Bottom;
            //        float newTop = (float)camera.Top;
            //        float newLeft = (float)camera.Left;
            //        float newRight = (float)camera.Right;
            //        float newWidth = newRight - newLeft;
            //        float newHeight = newTop - newBottom;
            //        if (newWidth >= newHeight)
            //        {
            //            float w = newWidth / newHeight * (top - bottom);
            //            float leftPercent = (0 - newLeft) / newWidth;
            //            float bottomPercent = (0 - newBottom) / newHeight;
            //            projection = glm.ortho(-w * leftPercent, w * (1 - leftPercent),
            //                -(top - bottom) * bottomPercent, (top - bottom) * (1 - bottomPercent), near, far);
            //        }
            //        else
            //        {
            //            float h = newHeight / newWidth * (right - left);
            //            float leftPercent = (0 - newLeft) / newWidth;
            //            float bottomPercent = (0 - newBottom) / newHeight;
            //            projection = glm.ortho(-(right - left) * leftPercent, (right - left) * (1 - leftPercent),
            //                -h * bottomPercent, h * (1 - bottomPercent), near, far);
            //        }
            //        this.SetUniform("projection", projection);
            //        this.SetUniform("view", view * model);
            //    }
            //}

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