﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace EMGraphics
{
    public partial class FontTexture
    {
        private static readonly object synObj = new object();
        private static readonly Dictionary<IntPtr, FontTexture> dict = new Dictionary<IntPtr, FontTexture>();

        /// <summary>
        /// Default FontTexture instance for this render context.
        /// </summary>
        public static FontTexture Default
        {
            get
            {
                IntPtr renderContext = Win32.wglGetCurrentContext();
                FontTexture resource;
                if (!dict.TryGetValue(renderContext, out resource))
                {
                    lock (synObj)
                    {
                        if (!dict.TryGetValue(renderContext, out resource))
                        {
                            resource = InitializeDefaultFontTexture();
                            dict.Add(renderContext, resource);
                        }
                    }
                }

                return resource;
            }
        }

        private const string defaultCharSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890.:,;'\"(!?)+-*/=_{}[]@~#\\<>|^%$£& ";

        private static FontTexture InitializeDefaultFontTexture()
        {
            Font font = new Font("新宋体", 32.0f, FontStyle.Regular, GraphicsUnit.Pixel);// SystemFonts.DefaultFont;
            FontBitmap fontBitmap = font.GetFontBitmap(defaultCharSet);
            FontTexture fontTexture = fontBitmap.GetFontTexture();
            return fontTexture;
        }
    }
}