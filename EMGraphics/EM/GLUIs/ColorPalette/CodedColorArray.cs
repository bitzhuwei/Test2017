using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace EMGraphics
{
	/// <summary>
	/// 用以描述色标的颜色、位置和数值。
	/// </summary>
	public class CodedColorArray : IEnumerable<CodedColor>
	{

		public CodedColor[] Items { get; private set; }

		public static CodedColorArray GetDefault()
		{
			var array = new CodedColor[5];
			array[0] = new CodedColor(
				Color.FromArgb(255, 0, 22, 255), 0.0f, 100.0f);
			array[1] = new CodedColor(
				Color.FromArgb(255, 0, 193, 136), 0.25f, 200.0f);
			array[2] = new CodedColor(
				Color.FromArgb(255, 166, 255, 27), 0.5f, 300.0f);
			array[3] = new CodedColor(
				Color.FromArgb(255, 255, 173, 0), 0.75f, 400.0f);
			array[4] = new CodedColor(
				Color.FromArgb(255, 255, 8, 1), 1.0f, 500.0f);

			return new CodedColorArray(array);
		}

		public IEnumerator<CodedColor> GetEnumerator()
		{
			foreach (CodedColor item in this.Items)
			{
				yield return item;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public CodedColorArray(CodedColor[] array)
		{
			if (array == null) { throw new ArgumentNullException(); }

			this.Items = array;
		}

		/// <summary>
		/// Get bitmap of 1 height for coded color bar.
		/// </summary>
		/// <param name="codedColors"></param>
		/// <param name="width"></param>
		/// <returns></returns>
		public Bitmap GetBitmap(int width)
		{
			CodedColor[] codedColors = this.Items;

			const int bitmapHeight = 1;
			var format = System.Drawing.Imaging.PixelFormat.Format32bppRgb;
			var lockMode = System.Drawing.Imaging.ImageLockMode.WriteOnly;
			var bitmap = new Bitmap(width, bitmapHeight, format);
			var bitmapRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
			BitmapData bmpData = bitmap.LockBits(bitmapRect, lockMode, format);

			int byteLength = Math.Abs(bmpData.Stride) * bitmapHeight;
			byte[] bitmapBytes = new byte[byteLength];

			for (int i = 0; i < codedColors.Length - 1; i++)
			{
				int left = (int)(width * codedColors[i].Coord);
				int right = (int)(width * codedColors[i + 1].Coord);
				vec3 leftColor = codedColors[i].DisplayColor.ToVec3();
				vec3 rightColor = codedColors[i + 1].DisplayColor.ToVec3();
				for (int col = left; col < right; col++)
				{
					vec3 color = (leftColor * ((right - col) * 1.0f / (right - left)) + rightColor * ((col - left) * 1.0f / (right - left)));
					for (int row = 0; row < 1; row++)
					{
						bitmapBytes[row * bmpData.Stride + col * 4 + 0] = (byte)(color.z * 255.0f);
						bitmapBytes[row * bmpData.Stride + col * 4 + 1] = (byte)(color.y * 255.0f);
						bitmapBytes[row * bmpData.Stride + col * 4 + 2] = (byte)(color.x * 255.0f);
					}
				}
			}

			System.Runtime.InteropServices.Marshal.Copy(bitmapBytes, 0, bmpData.Scan0, byteLength);

			bitmap.UnlockBits(bmpData);

			return bitmap;
		}

		public Color Map2Color(float propertyValue)
		{
			if (propertyValue <= this.Items[0].PropertyValue)
			{
				return this.Items[0].DisplayColor;
			}
			else if (this.Items[this.Items.Length - 1].PropertyValue <= propertyValue)
			{
				return this.Items[this.Items.Length - 1].DisplayColor;
			}
			else
			{
				for (int i = 1; i < this.Items.Length - 1; i++)
				{
					if (propertyValue <= this.Items[i].PropertyValue)
					{
						Color leftColor = this.Items[i].DisplayColor;
						Color rightColor = this.Items[i + 1].DisplayColor;
						float percent = (propertyValue - this.Items[0].PropertyValue)
							/ (this.Items[this.Items.Length - 1].PropertyValue - this.Items[0].PropertyValue);
						float r = leftColor.R * percent + rightColor.R * (1 - percent);
						float g = leftColor.G * percent + rightColor.G * (1 - percent);
						float b = leftColor.B * percent + rightColor.B * (1 - percent);
						Color result = Color.FromArgb((int)r, (int)g, (int)b);
						return result;
					}
				}
			}

			return Color.Aqua;// this is when something wrong happens.
		}
	}
}