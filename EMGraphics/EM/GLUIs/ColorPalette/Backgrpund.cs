using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace EMGraphics
{
	// (top)
	// 0                  1
	// -------------------
	// |                 |
	// |                 |
	// |                 |
	// |                 |
	// |                 |
	// |                 |
	// -------------------
	// 3                 2
	// (bottom)
	/// <summary>
	/// </summary>
	public class Backgrpund : IBufferable, IModelSpace
	{
		private static readonly vec3[] positions = new vec3[]
		{
			new vec3(-1,  1, 0) * 0.5f,
			new vec3( 1,  1, 0) * 0.5f,
			new vec3( 1, -1, 0) * 0.5f,
			new vec3(-1, -1, 0) * 0.5f,
		};

		private IndexBuffer indexBuffer;
		public const string strPosition = "position";
		private VertexBuffer positionBuffer;
		public const string strColor = "color";
		private VertexBuffer colorBuffer;
		private vec3[] colors;

		public Backgrpund(Color topColor, Color bottomColor)
		{
			var colors = new vec3[4];
			colors[0] = topColor.ToVec3();
			colors[1] = colors[0];
			colors[2] = bottomColor.ToVec3();
			colors[3] = colors[2];

			this.colors = colors;

			this.RotationAxis = new vec3(0, 0, 1);
			this.Scale = new vec3(1, 1, 1);
			this.ModelSize = new vec3(1, 1, 1);
		}

		public vec3 WorldPosition { get; set; }
		public float RotationAngleDegree { get; set; }
		public vec3 RotationAxis { get; set; }
		public vec3 Scale { get; set; }
		public vec3 ModelSize { get; set; }

		public IndexBuffer GetIndexBuffer()
		{
			if (this.indexBuffer == null)
			{
				this.indexBuffer = ZeroIndexBuffer.Create(DrawMode.Quads, 0, 4);
			}

			return this.indexBuffer;
		}

		public VertexBuffer GetVertexAttributeBuffer(string bufferName, string varNameInShader)
		{
			if (bufferName == strPosition)
			{
				if (this.positionBuffer == null)
				{
					this.positionBuffer = positions.GenVertexBuffer(VBOConfig.Vec3, varNameInShader, BufferUsage.StaticDraw);
				}

				return this.positionBuffer;
			}
			else if (bufferName == strColor)
			{
				if (this.colorBuffer == null)
				{
					this.colorBuffer = this.colors.GenVertexBuffer(VBOConfig.Vec3, varNameInShader, BufferUsage.StaticDraw);
				}

				return this.colorBuffer;
			}
			else
			{
				throw new NotImplementedException();

			}
		}

		public bool UsesZeroIndexBuffer()
		{
			return true;
		}
	}
}