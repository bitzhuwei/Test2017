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
			var array = new CodedColor[17];
			var values = new int[] { 0, 63, 127, 191, 255 };
			array[0] = new CodedColor(
				Color.FromArgb(255, 0, 0, 255), 0.0f, 0.0f);
			array[1] = new CodedColor(
				Color.FromArgb(255, 0, 63, 255), 0.0f, 0.0f);
			array[2] = new CodedColor(
				Color.FromArgb(255, 0, 127, 255), 0.0f, 0.0f);
			array[3] = new CodedColor(
				Color.FromArgb(255, 0, 191, 255), 0.0f, 0.0f);
			array[4] = new CodedColor(
				Color.FromArgb(255, 0, 255, 255), 0.0f, 0.0f);
			array[5] = new CodedColor(
				Color.FromArgb(255, 0, 255, 191), 0.0f, 0.0f);
			array[6] = new CodedColor(
				Color.FromArgb(255, 0, 255, 127), 0.0f, 0.0f);
			array[7] = new CodedColor(
				Color.FromArgb(255, 0, 255, 63), 0.0f, 0.0f);
			array[8] = new CodedColor(
				Color.FromArgb(255, 0, 255, 0), 0.0f, 0.0f);
			array[9] = new CodedColor(
				Color.FromArgb(255, 63, 255, 0), 0.0f, 0.0f);
			array[10] = new CodedColor(
				Color.FromArgb(255, 127, 255, 0), 0.0f, 0.0f);
			array[11] = new CodedColor(
				Color.FromArgb(255, 191, 255, 0), 0.0f, 0.0f);
			array[12] = new CodedColor(
				Color.FromArgb(255, 255, 255, 0), 0.0f, 0.0f);
			array[13] = new CodedColor(
				Color.FromArgb(255, 255, 191, 0), 0.0f, 0.0f);
			array[14] = new CodedColor(
				Color.FromArgb(255, 255, 127, 0), 0.0f, 0.0f);
			array[15] = new CodedColor(
				Color.FromArgb(255, 255, 63, 0), 0.0f, 0.0f);
			array[16] = new CodedColor(
				Color.FromArgb(255, 255, 0, 0), 0.0f, 0.0f);

			for (int i = 0; i < array.Length; i++)
			{
				array[i].Coord = (float)i / (float)(array.Length - 1);
				array[i].PropertyValue = i;
			}

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

		//public Color Map2Color(float propertyValue)
		//{
		//	float min = this.Items[0].PropertyValue;
		//	float max = this.Items[this.Items.Length - 1].PropertyValue;
		//	float mid = (min + max) / 2;
		//	if (propertyValue <= mid)
		//	{
		//		Color red = Color.Red;
		//		Color green = Color.Green;
		//		float percent = (propertyValue - min) / (mid - min);
		//		float r = red.R * percent + green.R * (1 - percent);
		//		float g = red.G * percent + green.G * (1 - percent);
		//		float b = red.B * percent + green.B * (1 - percent);
		//		return Color.FromArgb(255, (int)r, (int)g, (int)b);
		//	}
		//	else
		//	{
		//		Color green = Color.Green;
		//		Color blue = Color.Blue;
		//		float percent = (propertyValue - mid) / (max - mid);
		//		float r = green.R * percent + blue.R * (1 - percent);
		//		float g = green.G * percent + blue.G * (1 - percent);
		//		float b = green.B * percent + blue.B * (1 - percent);
		//		return Color.FromArgb(255, (int)r, (int)g, (int)b);
		//	}
		//}
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
				for (int i = 1; i < this.Items.Length; i++)
				{
					if (propertyValue <= this.Items[i].PropertyValue)
					{
						Color leftColor = this.Items[i - 1].DisplayColor;
						Color rightColor = this.Items[i].DisplayColor;
						float percent = (propertyValue - this.Items[i - 1].PropertyValue)
							/ (this.Items[i].PropertyValue - this.Items[i - 1].PropertyValue);
						float r = leftColor.R * percent + rightColor.R * (1 - percent);
						float g = leftColor.G * percent + rightColor.G * (1 - percent);
						float b = leftColor.B * percent + rightColor.B * (1 - percent);
						Color result = Color.FromArgb((int)r, (int)g, (int)b);
						return result;
					}
				}
			}

			return Color.Black;// this is when something wrong happens.
		}
	}
}