﻿using System;

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
            var shaderCodes = new ShaderCode[2];
            shaderCodes[0] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"Resources\Simple.vert"), ShaderType.VertexShader);
            shaderCodes[1] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"Resources\Simple.frag"), ShaderType.FragmentShader);
			var provider = new ShaderCodeArray(shaderCodes);
            var map = new AttributeMap();
            map.Add("in_Position", Axis.strPosition);
            map.Add("in_Color", Axis.strColor);
            var axis = new Axis(partCount, 0.5f);
            var renderer = new Renderer(axis, provider, map);
            renderer.ModelSize = axis.ModelSize;

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
            var renderer = this.Renderer as Renderer;
            vec3 rendererSize = renderer.ModelSize;
            vec3 scale = new vec3(length / rendererSize.x, length / rendererSize.y, length / rendererSize.z);
            mat4 model = glm.scale(mat4.identity(), scale);
            renderer.SetUniform("projectionMatrix", projection);
            renderer.SetUniform("viewMatrix", view);
            renderer.SetUniform("modelMatrix", model);

            base.DoRender(arg);
        }
    }
}