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
    public partial class FaceNormalRenderer
    {

		private VertexBuffer positionBuffer;

		protected override void DoInitialize()
		{
			base.DoInitialize();

			this.positionBuffer = this.DataSource.GetVertexAttributeBuffer(FaceNormal.strPosition, string.Empty);

		}
		
		/// <summary>
		/// 反转法线方向。
		/// <para>Reverse normal's direction.</para>
		/// </summary>
		/// <param name="label"></param>
		public unsafe void ReverseNormals(string label)
		{
			VertexBuffer positionBuffer = this.positionBuffer;
			IntPtr pointer = positionBuffer.MapBuffer(MapBufferAccess.ReadWrite);
			if (pointer == IntPtr.Zero) { return; }
			
			int index;
			if (this.labelDict.TryGetValue(label, out index))
			{
				ZeroIndexBuffer indexBuffer = this.renderSingleIndexBuffers[index];
				int length = indexBuffer.Length;
				vec3* array = (vec3*)pointer.ToPointer();
				for (int start = indexBuffer.FirstVertex, i = 0; i < length; i += 4)
				{
					vec3 head = array[start + i + 0];
					vec3 part = array[start + i + 1];
					vec3 tail = array[start + i + 3];
					vec3 newTail = head + head - tail;
					vec3 newPart = head + head - part;
					array[start + i + 1] = newPart;
					array[start + i + 2] = newPart;
					array[start + i + 3] = newTail;					
				}
			}

			positionBuffer.UnmapBuffer();
		}

		/// <summary>
		/// 反转法线方向。
		/// <para>Reverse normal's direction.</para>
		/// </summary>
		public unsafe void ReverseVisibleNormals()
		{
			VertexBuffer positionBuffer = this.positionBuffer;
			IntPtr pointer = positionBuffer.MapBuffer(MapBufferAccess.ReadWrite);
			if (pointer == IntPtr.Zero) { return; }

			foreach (var index in this.visibleGroups)
			{
				ZeroIndexBuffer indexBuffer = this.renderSingleIndexBuffers[index];
				int length = indexBuffer.Length;
				vec3* array = (vec3*)pointer.ToPointer();
				for (int start = indexBuffer.FirstVertex, i = 0; i < length; i += 4)
				{
					vec3 head = array[start + i + 0];
					vec3 part = array[start + i + 1];
					vec3 tail = array[start + i + 3];
					vec3 newTail = head + head - tail;
					vec3 newPart = head + head - part;
					array[start + i + 1] = newPart;
					array[start + i + 2] = newPart;
					array[start + i + 3] = newTail;
				}
			}

			positionBuffer.UnmapBuffer();
		}

		/// <summary>
		/// 反转法线方向。
		/// <para>Reverse normal's direction.</para>
		/// </summary>
		public unsafe void ReverseAllNormals()
		{
			VertexBuffer positionBuffer = this.positionBuffer;
			IntPtr pointer = positionBuffer.MapBuffer(MapBufferAccess.ReadWrite);
			if (pointer == IntPtr.Zero) { return; }

			vec3* array = (vec3*)pointer.ToPointer();
			for (int i = 0; i < positionBuffer.Length; i += 4)
			{
				vec3 head = array[i + 0];
				vec3 part = array[i + 1];
				vec3 tail = array[i + 3];
				vec3 newTail = head + head - tail;
				vec3 newPart = head + head - part;
				array[i + 1] = newPart;
				array[i + 2] = newPart;
				array[i + 3] = newTail;					
			}

			positionBuffer.UnmapBuffer();
		}
		
    }
}
