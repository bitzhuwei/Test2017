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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlCanvas1_MouseWheel(object sender, MouseEventArgs e)
		{
			float factor = 1000.0f;
			{
				SceneObject obj = this.wholeObject;
				if (obj != null)
				{
					RendererBase renderer = obj.Renderer;
					if (renderer != null)
					{
						renderer.Scale = renderer.Scale * (1.0f + e.Delta / factor);
					}
				}
			}
			{
				SceneObject obj = this.boxObject;
				if (obj != null)
				{
					RendererBase renderer = obj.Renderer;
					if (renderer != null)
					{
						renderer.Scale = renderer.Scale * (1.0f + e.Delta / factor);
					}
				}
			}

			{
				SceneObject obj = this.wholeNormal;
				if (obj != null)
				{
					RendererBase renderer = obj.Renderer;
					if (renderer != null)
					{
						renderer.Scale = renderer.Scale * (1.0f + e.Delta / factor);
					}
				}
			}
			{
				SceneObject obj = this.pickedGroup;
				if (obj != null)
				{
					RendererBase renderer = obj.Renderer;
					if (renderer != null)
					{
						renderer.Scale = renderer.Scale * (1.0f + e.Delta / factor);
					}
				}
			}
			{
				SceneObject obj = this.notPickedGroup;
				if (obj != null)
				{
					RendererBase renderer = obj.Renderer;
					if (renderer != null)
					{
						renderer.Scale = renderer.Scale * (1.0f + e.Delta / factor);
					}
				}
			}
		}

	}
}
