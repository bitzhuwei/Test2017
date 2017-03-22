using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace EMGraphics
{
    /// <summary>
    /// 
    /// </summary>
    public partial class EMGridRenderer 
    {

		private VertexBuffer cloudColorBuffer;

		protected override void DoInitialize()
		{
			base.DoInitialize();

			this.cloudColorBuffer = this.DataSource.GetVertexAttributeBuffer(EMGrid.strCloudColor, string.Empty);
		}

		public unsafe void UpdateCloud(IList<float> propertyValues, CodedColorArray colorPalette)
		{
			VertexBuffer cloudColorBuffer = this.cloudColorBuffer;
			IntPtr pointer = cloudColorBuffer.MapBuffer(MapBufferAccess.WriteOnly);
			if (pointer == IntPtr.Zero) { return; }

			int length = cloudColorBuffer.Length;
			vec3* array = (vec3*)pointer.ToPointer();
			for (int i = 0; i < length; i++)
			{
				Color c = colorPalette.Map2Color(propertyValues[i]);
				array[i] = c.ToVec3();
			}

			cloudColorBuffer.UnmapBuffer();
		}
	}
}
