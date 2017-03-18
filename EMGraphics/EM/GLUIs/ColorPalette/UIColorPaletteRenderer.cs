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
		}


	}
}