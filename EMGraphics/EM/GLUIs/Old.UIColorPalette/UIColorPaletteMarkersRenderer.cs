﻿
namespace EMGraphics.OldColorPalette
{
    /// <summary>
    /// 若干条向下突出的白色竖线。
    /// </summary>
    internal class UIColorPaletteMarkersRenderer : UIRenderer
    {
        /// <summary>
        /// </summary>
        /// <param name="anchor"></param>
        /// <param name="margin"></param>
        /// <param name="size"></param>
        /// <param name="zNear"></param>
        /// <param name="zFar"></param>
        public UIColorPaletteMarkersRenderer(int maxMarkerCount,
            System.Windows.Forms.AnchorStyles anchor, System.Windows.Forms.Padding margin,
            System.Drawing.Size size, int zNear, int zFar)
            : base(anchor, margin, size, zNear, zFar)
        {
            var model = new LinesModel(maxMarkerCount);
            this.Renderer = LinesRenderer.Create(model);
        }

        protected override void DoRender(RenderEventArgs arg)
        {
            mat4 projection = this.GetOrthoProjection();
            mat4 view = glm.lookAt(new vec3(0, 0, 1), new vec3(0, 0, 0), new vec3(0, 1, 0));
            float length = this.Size.Height;
            mat4 model = glm.scale(mat4.identity(), new vec3(this.Size.Width - 1, this.Size.Height - 1, 1));// '-1' to make sure lines shows up.
            var renderer = this.Renderer as Renderer;
            renderer.SetUniform("mvp", projection * view * model);

            base.DoRender(arg);
        }

        public void UpdateCodedColors(double axisMin, double axisMax, double step)
        {
            var renderer = this.Renderer as LinesRenderer;
            if (renderer != null)
            {
                renderer.UpdateCodedColors(axisMin, axisMax, step);
            }
        }
    }
}