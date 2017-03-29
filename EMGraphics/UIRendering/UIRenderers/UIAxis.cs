using System;
using System.Drawing;

namespace EMGraphics
{
    /// <summary>
    /// opengl UI for Axis
    /// </summary>
    public class UIAxis : UIRenderer
    {
        /// <summary>
        /// opengl UI for Axis
        /// </summary>
        /// <param name="anchor"></param>
        /// <param name="margin"></param>
        /// <param name="size"></param>
        /// <param name="partCount">24 as default.</param>
        public UIAxis(
            System.Windows.Forms.AnchorStyles anchor, System.Windows.Forms.Padding margin,
            System.Drawing.Size size, int partCount = 24)
            : base(anchor, margin, size, -Math.Max(size.Width, size.Height), Math.Max(size.Width, size.Height))
        {
            IntegratedAxisRenderer renderer = IntegratedAxisRenderer.Create();
            this.Renderer = renderer;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="arg"></param>
        protected override void DoRender(RenderEventArgs arg)
        {
            ICamera camera = arg.Camera;
            mat4 projection = this.GetOrthoProjection();
            vec3 position = (camera.Position - camera.Target).normalize();
            mat4 view = glm.lookAt(position, new vec3(0, 0, 0), camera.UpVector);
            float length = Math.Max(this.Size.Width, this.Size.Height);
            var renderer = this.Renderer as IntegratedAxisRenderer;
            vec3 rendererSize = renderer.ModelSize;
            vec3 scale = new vec3(length / rendererSize.x, length / rendererSize.y, length / rendererSize.z);
            mat4 model = glm.scale(mat4.identity(), scale);
            renderer.SetMVP(projection * view * model);

            base.DoRender(arg);
        }
    }
}