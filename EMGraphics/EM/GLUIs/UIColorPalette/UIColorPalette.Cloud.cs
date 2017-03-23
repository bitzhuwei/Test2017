using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EMGraphics
{
	public partial class UIColorPalette 
	{

		public void UpdateCloud(float maxValue, float minValue)
		{
			CodedColorArray array = this.GetCodedColors(maxValue, minValue);

			UpdateLabels(array.Items);

			this.ColorPalette = array;
		}

		private CodedColorArray GetCodedColors(float maxValue, float minValue)
		{
			if (maxValue <= minValue) { throw new ArgumentException("max value <= min value!"); }

			CodedColorArray array = CodedColorArray.GetDefault();

			float step = (maxValue - minValue) / (array.Items.Length - 1);
			for (int i = 0; i < array.Items.Length; i++)
			{
				array.Items[i].PropertyValue = minValue + i * step;
			}

			return array;
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