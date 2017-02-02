﻿using System.Drawing;

namespace EMGraphics
{
    /// <summary>
    /// font, texture and texture coordiante.
    /// </summary>
    public interface IFontTexture
    {
        /// <summary>
        /// Texture's id.
        /// </summary>
        Texture TextureObj { get; }

        /// <summary>
        /// glyph's height.
        /// </summary>
        float GlyphHeight { get; }

        /// <summary>
        /// texture's size.
        /// </summary>
        Size TextureSize { get; }

        /// <summary>
        /// glyph information dictionary.
        /// </summary>
        FullDictionary<char, GlyphInfo> GlyphInfoDictionary { get; }
    }
}