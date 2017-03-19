using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace EMGraphics
{
    public static class CodedColorsHelper
    {
        /// <summary>
        /// Get bitmap of 1 height for coded color bar.
        /// </summary>
        /// <param name="codedColors"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static Bitmap GetBitmap(this CodedColor[] codedColors, int width)
        {
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
    }
}