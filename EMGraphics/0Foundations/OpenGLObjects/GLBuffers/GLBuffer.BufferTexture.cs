﻿namespace EMGraphics
{
    public abstract partial class GLBuffer
    {
        /// <summary>
        /// Dump a <see cref="Texture"/> filled with this <see cref="GLBuffer"/>.
        /// </summary>
        /// <param name="internalFormat"></param>
        /// <param name="autoDispose">Dispose this buffer when disposing returned texture.</param>
        /// <returns></returns>
        public Texture DumpBufferTexture(uint internalFormat, bool autoDispose)
        {
            var texture = new Texture(TextureTarget.TextureBuffer,
                new TexBufferImageFiller(internalFormat, this, autoDispose),
                new NullSampler());
            texture.Initialize();
            return texture;
        }
    }
}