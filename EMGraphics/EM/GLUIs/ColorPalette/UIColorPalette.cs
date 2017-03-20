using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EMGraphics
{
	/// <summary>
	/// color palette.
	/// 在窗口固定位置显示的色标。
	/// 本类型只圈定了一个矩形范围。
	/// </summary>
	public partial class UIColorPalette : UIRenderer
	{
		private int maxMarkerCount;

		/// <summary>
		/// </summary>
		/// <param name="anchor"></param>
		/// <param name="margin"></param>
		/// <param name="size"></param>
		/// <param name="zNear"></param>
		/// <param name="zFar"></param>
		public UIColorPalette(int maxMarkerCount,
			CodedColorArray codedColors, Color textColor,
			AnchorStyles anchor, Padding margin,
			System.Drawing.Size size, int zNear, int zFar)
			: base(anchor, margin, size, zNear, zFar)
		{
			this.StateList.Add(new ClearColorState(Color.Orange));
			{
				this.maxMarkerCount = maxMarkerCount;
			}

			//{
			//	// background gradient color.
			//	var shaderCodes = new ShaderCode[2];
			//	shaderCodes[0] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\Background.vert"), ShaderType.VertexShader);
			//	shaderCodes[1] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\Background.frag"), ShaderType.FragmentShader);
			//	var provider = new ShaderCodeArray(shaderCodes);
			//	var model = new Backgrpund(Color.LightBlue, Color.White);
			//	var map = new AttributeMap();
			//	map.Add("in_Position", Backgrpund.strPosition);
			//	map.Add("in_Color", Backgrpund.strColor);
			//	var renderer = new Renderer(model, provider, map);
			//	renderer.RotationAxis = model.RotationAxis;
			//	renderer.Scale = model.Scale;
			//	renderer.ModelSize = model.ModelSize;
			//	this.Renderer = renderer;
			//}
			{
				// this writes 'Surface current[mA/m]".
				var headLine = new UIText(AnchorStyles.Left | AnchorStyles.Top,
					new Padding(3, 3, 3, 3), new Size(140, 15), -100, 100);
				headLine.Text = "Surface current[mA/m]";
				headLine.TextColor = Color.Black;
				this.Children.Add(headLine);
			}
			{
				// color palette.
				var coloredBar = new UIColoredBar(
					codedColors,
					AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top,
					new Padding(5, marginTop, 5, marginBottom), new Size(30, 100), -100, 100);
				this.Children.Add(coloredBar);
			}
			{
				InitLabels(maxMarkerCount, size);
			}
		}

		//protected override void DoRender(RenderEventArgs arg)
		//{
		//	// this displays background gradient color only.
		//	// other components are rendered in this component's chilcren part.
		//	ICamera camera = arg.Camera;
		//	mat4 projection = this.GetOrthoProjection();
		//	//vec3 position = (camera.Position - camera.Target).normalize();
		//	mat4 view = glm.lookAt(new vec3(0, 0, 1), new vec3(0, 0, 0), new vec3(0, 1, 0));
		//	var renderer = this.Renderer as Renderer;
		//	vec3 rendererSize = renderer.ModelSize;
		//	vec3 scale = new vec3(this.Size.Width / rendererSize.x, this.Size.Height / rendererSize.y, 1);
		//	mat4 model = glm.scale(mat4.identity(), scale);
		//	renderer.SetUniform("mvp", projection * view * model);

		//	base.DoRender(arg);
		//}
	}
}