using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace EMGraphics
{
	/// <summary>
	/// color palette.
	/// 在窗口固定位置显示的色标。
	/// 本类型只圈定了一个矩形范围。
	/// </summary>
	public class UIColorPaletteRenderer : UIRenderer
	{

		/// <summary>
		/// </summary>
		/// <param name="anchor"></param>
		/// <param name="margin"></param>
		/// <param name="size"></param>
		/// <param name="zNear"></param>
		/// <param name="zFar"></param>
		public UIColorPaletteRenderer(int maxMarkerCount,
			CodedColor[] codedColors, Color textColor,
			System.Windows.Forms.AnchorStyles anchor, System.Windows.Forms.Padding margin,
			System.Drawing.Size size, int zNear, int zFar)
			: base(anchor, margin, size, zNear, zFar)
		{
			{
				var shaderCodes = new ShaderCode[2];
				shaderCodes[0] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\Background.vert"), ShaderType.VertexShader);
				shaderCodes[1] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\Background.frag"), ShaderType.FragmentShader);
				var provider = new ShaderCodeArray(shaderCodes);
				var model = new Backgrpund(Color.Blue, Color.White);
				var map = new AttributeMap();
				map.Add("in_Position", Backgrpund.strPosition);
				map.Add("in_Color", Backgrpund.strColor);
				var renderer = new Renderer(model, provider, map);
				renderer.RotationAxis = model.RotationAxis;
				renderer.Scale = model.Scale;
				renderer.ModelSize = model.ModelSize;
				this.Renderer = renderer;
			}
		}

		protected override void DoRender(RenderEventArgs arg)
		{
			ICamera camera = arg.Camera;
			mat4 projection = this.GetOrthoProjection();
			//vec3 position = (camera.Position - camera.Target).normalize();
			mat4 view = glm.lookAt(new vec3(0, 0, 1), new vec3(0, 0, 0), new vec3(0, 1, 0));
			var renderer = this.Renderer as Renderer;
			vec3 rendererSize = renderer.ModelSize;
			vec3 scale = new vec3(this.Size.Width / rendererSize.x, this.Size.Height / rendererSize.y, 1);
			mat4 model = glm.scale(mat4.identity(), scale);
			renderer.SetUniform("mvp", projection * view * model);

			base.DoRender(arg);
		}
	}
}