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

		private void glCanvas1_DoubleClick(object sender, EventArgs e)
		{
			var form = new FormDisplayText("Double Click Dialog");
			var builder = new StringBuilder();
			foreach (var item in this.CurrentPickedGeometrys)
			{
				builder.Append(item.ToString());
				builder.AppendLine();
				builder.AppendLine("===============================");
			}

			if (this.CurrentPickedGeometrys.Count == 0)
			{
				builder.Append("Nothing picked!");
			}

			form.SetText(builder.ToString());
			form.Show();
		}
	}
}
