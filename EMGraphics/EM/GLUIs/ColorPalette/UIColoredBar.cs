using System.Drawing;

namespace EMGraphics
{
    /// <summary>
    /// 彩色的色标带。
    /// </summary>
    internal class UIColoredBar : UIRenderer
    {
        /// <summary>
        /// sampler for color palette.
        /// </summary>
        public Texture Sampler { get; private set; }

        private CodedColor[] codedColors;

		/// <summary>
		/// 彩色的色标带。
		/// </summary>
		/// <param name="anchor"></param>
		/// <param name="margin"></param>
		/// <param name="size"></param>
		/// <param name="zNear"></param>
		/// <param name="zFar"></param>
		public UIColoredBar(
			CodedColor[] codedColors,
			System.Windows.Forms.AnchorStyles anchor, System.Windows.Forms.Padding margin,
			System.Drawing.Size size, int zNear, int zFar)
			: base(anchor, margin, size, zNear, zFar)
		{
			this.codedColors = codedColors;

			var model = new QuadStrip(1);
			var shaderCodes = new ShaderCode[2];
			shaderCodes[0] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\QuadStrip.vert"), ShaderType.VertexShader);
			shaderCodes[1] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\QuadStrip.frag"), ShaderType.FragmentShader);
			var provider = new ShaderCodeArray(shaderCodes);
			var map = new AttributeMap();
			map.Add("in_Position", QuadStrip.position);
			map.Add("in_TexCoord", QuadStrip.texCoord);

			this.Renderer = new Renderer(model, provider, map);
		}

        protected override void DoInitialize()
        {
            base.DoInitialize();

            Bitmap bitmap = this.codedColors.GetBitmap(1024);
            var texture = new Texture(TextureTarget.Texture1D, bitmap, new SamplerParameters());
            this.codedColors = null;
            texture.Initialize();
            this.Sampler = texture;
            bitmap.Dispose();
            var renderer = this.Renderer as Renderer;
            renderer.SetUniform("codedColorSampler", new samplerValue(TextureTarget.Texture1D,
                texture.Id, 0));
        }

        protected override void DoRender(RenderEventArgs arg)
        {
            mat4 projection = this.GetOrthoProjection();
            mat4 view = glm.lookAt(new vec3(0, 0, 1), new vec3(0, 0, 0), new vec3(0, 1, 0));
            float length = this.Size.Height;
            mat4 model = glm.scale(mat4.identity(), new vec3(this.Size.Width, this.Size.Height, 1));
            var renderer = this.Renderer as Renderer;
            renderer.SetUniform("mvp", projection * view * model);

            base.DoRender(arg);
        }

        public void UpdateTexture(Bitmap bitmap)
        {
            this.Sampler.UpdateContent(bitmap);
        }
    }
}