﻿using System;
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
		private bool whiteClearColor = true;

		private void glCanvas1_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 'w')
			{
				if (this.whiteClearColor)
				{
					this.scene.RootViewPort.Children[0].Content.ClearColor = Color.Black;
					this.whiteClearColor = !this.whiteClearColor;
				}
				else
				{
					this.scene.RootViewPort.Children[0].Content.ClearColor = Color.White;
					this.whiteClearColor = !this.whiteClearColor;
				}
			}
			else if (e.KeyChar == 's')
			{
				(new FormProperyGrid(this.scene)).Show();
			}
		}

	}
}
