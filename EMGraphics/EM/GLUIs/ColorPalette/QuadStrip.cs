using System;
using System.Drawing;

namespace EMGraphics
{
	//   ^
	//  /|\ y
	//   |
	//   |
	//   |
	// (0, 0)-----------> x
	//
	// 10----------------11
	// |                  |
	// |                  |
	// |                  |
	// 8------------------9
	// |                  |
	// |                  |
	// |                  |
	// 6------------------7
	// |                  |
	// |                  |
	// |                  |
	// 4------------------5
	// |                  |
	// |                  |
	// |                  |
	// 2------------------3
	// |                  |
	// |                  |
	// |                  |
	// 0------------------1
	// 
	// side length is 1.
	//
	/// <summary>
	/// 
	/// </summary>
	internal class QuadStrip : IBufferable
	{
		private CodedColorArray codedColorArray;
		private int maxMarkerCount;

		/// <summary>
		///
		/// </summary>
		public const string position = "position";

		/// <summary>
		///
		/// </summary>
		public const string texCoord = "texCoord";

		/// <summary>
		///
		/// </summary>
		public const string color = "color";

		//private vec3[] positions;
		private VertexBuffer positionBuffer;
		//private float[] texCoords;
		private VertexBuffer texCoordBuffer;

		public QuadStrip(CodedColorArray codedColorArray, int maxMarkerCount)
		{
			if (codedColorArray == null) 
			{
				throw new ArgumentNullException();
			}
			
			this.codedColorArray = codedColorArray;

			if (maxMarkerCount < codedColorArray.Items.Length)
			{
				throw new ArgumentException();
			}

			this.maxMarkerCount = maxMarkerCount;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="bufferName"></param>
		/// <param name="varNameInShader"></param>
		/// <returns></returns>
		public VertexBuffer GetVertexAttributeBuffer(string bufferName, string varNameInShader)
		{
			if (bufferName == position)
			{
				if (this.positionBuffer == null)
				{
					CodedColor[] items = this.codedColorArray.Items;
					var positions = new vec3[this.maxMarkerCount * 2];
					for (int i = 0; i < items.Length; i++)
					{
						positions[i * 2 + 0] = new vec3(-0.5f, (float)i / (float)(items.Length - 1) - 0.5f, 0);
						positions[i * 2 + 1] = new vec3(+0.5f, (float)i / (float)(items.Length - 1) - 0.5f, 0);
					}

					this.positionBuffer = positions.GenVertexBuffer(VBOConfig.Vec3, varNameInShader, BufferUsage.StaticDraw);
				}

				return this.positionBuffer;
			}
			else if (bufferName == texCoord)
			{
				if (this.texCoordBuffer == null)
				{
					CodedColor[] items = this.codedColorArray.Items;
					var texCoords = new float[this.maxMarkerCount * 2];
					for (int i = 0; i < items.Length; i++)
					{
						float value = (float)i / (float)(items.Length - 1);
						texCoords[i * 2 + 0] = value;
						texCoords[i * 2 + 1] = value;
					}

					this.texCoordBuffer = texCoords.GenVertexBuffer(VBOConfig.Float, varNameInShader, BufferUsage.StaticDraw);
				}

				return this.texCoordBuffer;
			}
			else
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <returns></returns>
		public IndexBuffer GetIndexBuffer()
		{
			if (indexBuffer == null)
			{
				int vertexCount = this.codedColorArray.Items.Length * 2;
				ZeroIndexBuffer buffer = ZeroIndexBuffer.Create(DrawMode.QuadStrip, 0, vertexCount);
				this.indexBuffer = buffer;
			}

			return indexBuffer;
		}

		private IndexBuffer indexBuffer = null;

		/// <summary>
		/// Uses <see cref="ZeroIndexBuffer"/> or <see cref="OneIndexBuffer"/>.
		/// </summary>
		/// <returns></returns>
		public bool UsesZeroIndexBuffer() { return true; }

	}
}