using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EMGraphics;

namespace EMGraphics.Demo
{
    public partial class FormMain
    {

		private void btnCloud_Click(object sender, EventArgs e)
		{
			if (this.wholeObject == null)
			{
				MessageBox.Show("请先加载一个nas文件！");
				return;
			}

			if (this.openCoudFile.ShowDialog()== DialogResult.OK)
			{
				CloudFile cloudFile = CloudFile.Load(this.openCoudFile.FileName);
				// update color palette.
				{
					this.uiColorPalette.UpdateCloud(cloudFile.MaxValue, cloudFile.MinValue, 5);
				}
				// update renderer's cloud.
				{
					var renderer = this.wholeObject.Renderer as EMGridRenderer;
					if (renderer == null)
					{
						MessageBox.Show("发生错误：没有找到EM渲染器！");
						return;
					}

					renderer.UpdateCloud(cloudFile.MaxValue, cloudFile.MinValue, cloudFile.PropertyValues);
				}
			}
		}
	}
}
