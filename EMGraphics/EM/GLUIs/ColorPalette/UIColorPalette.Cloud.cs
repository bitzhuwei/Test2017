using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EMGraphics
{
	public partial class UIColorPalette 
	{
		private static Bitmap rainbow;

		public void UpdateCloud(float maxValue, float minValue, int parts)
		{
			CodedColor[] codedColors = this.GetCodedColors(maxValue, minValue, parts);

			UpdateLabels(codedColors);

			this.ColorPalette = new CodedColorArray(codedColors);
		}

		private CodedColor[] GetCodedColors(float maxValue, float minValue, int parts)
		{
			if (maxValue <= minValue) { throw new ArgumentException("max value <= min value!"); }
			if (parts <= 0) { throw new ArgumentException("parts <= 0"); }

			if (rainbow == null)
			{
				rainbow = ManifestResourceLoader.LoadBitmap(@"EM\GLUIs\ColorPalette\rainbow.bmp");
			}

			int width = rainbow.Width;
			var codedColors = new CodedColor[parts];
			float step = (maxValue - minValue) / (parts - 1);
			for (int i = 0; i < parts; i++)
			{
				int x = (int)(width * ((float)i / (float)(parts - 1)));
				if (x >= width) { x = width - 1; }
				Color color = rainbow.GetPixel(x, 0);
				codedColors[i] = new CodedColor(color, (float)i / (float)(parts - 1), i * step + minValue);
			}

			return codedColors;
		}

		private void UpdateLabels(CodedColor[] codedColors)
		{
			int validCount = codedColors.Length;
			int length = maxMarkerCount;
			var font = new Font("Arial", 32);
			for (int i = 0; i < validCount; i++)
			{
				UIText label = this.labelList[i];
				label.Text = string.Format("{0:E}", codedColors[i].PropertyValue);
				label.Enabled = true;
			}
			for (int i = validCount; i < length; i++)
			{
				UIText label = this.labelList[i];
				label.Enabled = false;
			}
		}

	}
}