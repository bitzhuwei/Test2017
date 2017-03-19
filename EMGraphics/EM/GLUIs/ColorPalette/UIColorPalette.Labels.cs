using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EMGraphics
{
	public partial class UIColorPalette 
	{

		private float marginBottom = 50;
		private float marginTop = 50;
		private int currentMarkersCount;
		private List<UIText> labelList = new List<UIText>();

		private void InitLabels(int maxMarkerCount, Size size)
		{
			int length = maxMarkerCount;
			var font = new Font("Arial", 32);
			for (int i = 0; i < length; i++)
			{
				const int width = 100;
				float distance = marginBottom;
				distance += 2.0f * (float)i / (float)length * (float)(this.Size.Width - marginBottom - marginTop);
				distance -= width / 2;
				var label = new UIText(
					System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom,
					new System.Windows.Forms.Padding(50, 0, 0, 0),
					new System.Drawing.Size(width, 15), zNear, zFar,
					font.GetFontBitmap("0123456789.eE+-").GetFontTexture(), 100);
				label.Initialize();
				label.StateList.Add(new ClearColorState(Color.Green));
				label.Text = ((float)i).ToShortString();
				label.BeforeLayout += label_beforeLayout;
				this.Children.Add(label);
				this.labelList.Add(label);
			}
			this.currentMarkersCount = 2;
		}

		private void label_beforeLayout(object sender, CancelEventArgs e)
		{
			int count = currentMarkersCount - 1;
			var label = sender as UIText;
			int index = this.labelList.IndexOf(label);
			float distance = marginBottom;
			distance += (float)index / (float)count * (float)(this.Size.Height - marginBottom-marginTop);
			distance -= label.Size.Height / 2;
			System.Windows.Forms.Padding padding = label.Margin;
			padding.Bottom = (int)distance;
			label.Margin = padding;
		}

		protected override void DoInitialize()
		{
			base.DoInitialize();

			foreach (ITreeNode<UIRenderer> item in this.Children)
			{
				item.Content.Initialize();
			}

			this.SetCodedColor(-100, 100, 200);
		}
		public const int bitmapWidth = 1024;

		public void SetCodedColor(double axisMin, double axisMax, double step)
		{
			// update labels.
			{
				int labelCount = (int)((axisMax - axisMin) / step) + 1;
				// valid labels.
				for (int i = 0; i < labelCount - 1; i++)
				{
					this.labelList[i].Enabled = true;
					this.labelList[i].Text = string.Format("{0}", axisMin + step * i);
				}
				// last valid label.
				{
					int i = labelCount - 1;
					this.labelList[i].Enabled = true;
					this.labelList[i].Text = string.Format("{0}", axisMax);
				}
				// invalid labels.
				for (int i = labelCount; i < this.maxMarkerCount; i++)
				{
					this.labelList[i].Enabled = false;
				}
				this.currentMarkersCount = labelCount;
			}
		}

	}
}