using System;
using System.Drawing;
using System.Text;

namespace EMGraphics
{
    /// <summary>
    /// helper class.
    /// </summary>
    public static partial class FontBitmapHelper
    {
        /// <summary>
        /// bigger interval means less mix. Maybe 1 is enough for font with size of 64.
        /// </summary>
        private const int glyphInterval = 1;

        /// <summary>
        /// bigger maring means less mix. But 1 is enough.
        /// </summary>
        private const int leftMargin = 1;

        /// <summary>
        /// Gets a <see cref="FontBitmap"/>'s intance.
        /// </summary>
        /// <param name="font"></param>
        /// <param name="charSet"></param>
        /// <param name="drawBoundary"></param>
        /// <returns></returns>
        public static FontBitmap GetFontBitmap(this Font font, string charSet, bool drawBoundary = false)
        {
            var fontBitmap = new FontBitmap();
            fontBitmap.GlyphFont = font;

            Size charSize = GetSingleCharSize(charSet[0], font);

            fontBitmap.GlyphHeight = charSize.Height;

            var bitmap = new Bitmap(charSize.Width * charSet.Length, charSize.Height);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                Color clearColor = Color.FromArgb(0, 0, 0, 0);
                graphics.Clear(clearColor);
                int index = 0;
                foreach (var item in charSet)
                {
                    using (var glyphBmp = new Bitmap(charSize.Width, charSize.Height))
                    {
                        using (var glyphGraphics = Graphics.FromImage(glyphBmp))
                        {
                            glyphGraphics.DrawString(item.ToString(), font, Brushes.Red, 0, 0);
                        }
                        graphics.DrawImage(glyphBmp, index * charSize.Width, 0);
                        index++;
                    }
                }
            }
            fontBitmap.GlyphBitmap = bitmap;
            var dict = new FullDictionary<char, GlyphInfo>(GlyphInfo.Default);
            for (int i = 0; i < charSet.Length; i++)
            {
                char c = charSet[i];
                var glyphInfo = new GlyphInfo(
                    i * charSize.Width + charSize.Width / 5, 0, charSize.Width * 3 / 5, charSize.Height);
                dict.Add(c, glyphInfo);
            }
            fontBitmap.GlyphInfoDictionary = dict;

            return fontBitmap;
        }

        private static Size GetSingleCharSize(char c, Font font)
        {
            using (var bitmap = new Bitmap(1, 1))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    SizeF size = graphics.MeasureString(c.ToString(), font);
                    return new Size((int)Math.Ceiling(size.Width), (int)Math.Ceiling(size.Height));
                }
            }
        }

        private static void PrintSingleChars(Font font, string charSet)
        {
            int index = 0;
            foreach (var item in charSet)
            {
                SizeF size;
                using (var bitmap = new Bitmap(1, 1))
                {
                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        size = graphics.MeasureString(item.ToString(), font);
                    }
                }
                using (var bitmap = new Bitmap((int)Math.Ceiling(size.Width), (int)Math.Ceiling(size.Height)))
                {
                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.DrawString(item.ToString(), font, Brushes.Red, 0, 0);
                    }
                    bitmap.Save(string.Format("{2}, {0}x{1}.png", bitmap.Width, bitmap.Height, index++));
                }
            }
        }

        private static int GetGlyphHeight(string charSet, Font font)
        {
            using (var bitmap = new Bitmap(1, 1))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    SizeF size = graphics.MeasureString(charSet, font);
                    return (int)Math.Ceiling(size.Height);
                }
            }
        }

        private static string GetVerticalCharSet(string charSet)
        {
            var builder = new StringBuilder();
            builder.Append("祝");
            builder.AppendLine();
            foreach (var item in charSet)
            {
                builder.Append(item);
                builder.AppendLine();
            }
            builder.Append("威");
            builder.AppendLine();

            return builder.ToString();
        }

        private static SizeF GetStringSize(string charSet, Font font)
        {
            using (var bitmap = new Bitmap(1, 1))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    return graphics.MeasureString(charSet, font);
                }
            }
        }
        /// <summary>
        /// Gets a <see cref="FontBitmap"/>'s intance.
        /// </summary>
        /// <param name="font"></param>
        /// <param name="charSet"></param>
        /// <param name="drawBoundary"></param>
        /// <returns></returns>
        public static FontBitmap GetFontBitmap_old(this Font font, string charSet, bool drawBoundary = false)
        {
            var fontBitmap = new FontBitmap();
            fontBitmap.GlyphFont = font;

            // 以下几步，不能调换先后顺序。
            // Don't change the order in which these functions invoked.
            int singleCharWidth, singleCharHeight;
            PrepareInitialGlyphDict(fontBitmap, charSet, out singleCharWidth, out singleCharHeight);
            int width, height;
            PrepareFinalBitmapSize(fontBitmap, out width, out height);
            PrintBitmap(fontBitmap, singleCharWidth, singleCharHeight, width, height);
            if (drawBoundary)
            {
                using (var graphics = Graphics.FromImage(fontBitmap.GlyphBitmap))
                {
                    bool odd = false;
                    foreach (GlyphInfo item in fontBitmap.GlyphInfoDictionary.Values)
                    {
                        graphics.DrawRectangle(
                            odd ? Pens.Green : Pens.Blue,
                            item.ToRectangle(1, 1, -2, -1));
                        odd = !odd;
                    }
                    graphics.DrawRectangle(Pens.Red, 0, 0, fontBitmap.GlyphBitmap.Width - 1, fontBitmap.GlyphBitmap.Height - 1);
                }
            }
            return fontBitmap;
        }
    }
}