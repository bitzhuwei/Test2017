using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EMGraphics
{
	public partial class UIColorPalette 
	{

		private int marginBottom = 20;
		private int marginTop = 30;
		private List<UIText> labelList = new List<UIText>();

		private void InitLabels(CodedColorArray codedColors, int maxMarkerCount, Size size)
		{
			int validCount = codedColors.Items.Length;
			int length = maxMarkerCount;
			var font = new Font("Arial", 32);
			FontTexture texture = font.GetFontBitmap("0123456789.eE+-").GetFontTexture();
			for (int i = 0; i < length; i++)
			{
				var label = new UIText(
					System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom,
					new System.Windows.Forms.Padding(50, 0, 0, 0),
					new System.Drawing.Size(100, 15), zNear, zFar,
					texture, 100);
				label.TextColor = Color.Black;
				label.Initialize();
				//label.StateList.Add(new ClearColorState(Color.Green));
				//label.Text = string.Format("{0}th label.", i);
				label.BeforeLayout += label_beforeLayout;
				label.Enabled = false;
				this.Children.Add(label);
				this.labelList.Add(label);
			}
			for (int i = 0; i < validCount; i++)
			{
				UIText label = this.labelList[i];
				CodedColor cc = codedColors.Items[i];
				label.Text = string.Format("{0:E}", cc.PropertyValue);
				label.Enabled = true;
			}
		}

		private void label_beforeLayout(object sender, CancelEventArgs e)
		{
			int count = this.codedColors.Items.Length;
			var label = sender as UIText;
			int index = this.labelList.IndexOf(label);
			if (count < index + 1) { return; }
			
			float distance = marginBottom;
			distance += (float)index / (float)(count - 1) * (float)(this.Size.Height - marginBottom - marginTop);
			distance -= label.Size.Height / 2;
			System.Windows.Forms.Padding padding = label.Margin;
			padding.Bottom = (int)distance;
			label.Margin = padding;
		}
	}
}