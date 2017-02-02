﻿namespace EMGraphics
{
    /// <summary>
    /// Create, update, use and delete a renderbuffer object.
    /// </summary>
    public partial class Renderbuffer
    {
        private static OpenGL.glBindRenderbuffer glBindRenderbuffer;
        private static OpenGL.glGenRenderbuffers glGenRenderbuffers;
        private static OpenGL.glRenderbufferStorage glRenderbufferStorage;

        private uint[] renderbuffer = new uint[1];

        /// <summary>
        /// Create, update, use and delete a renderbuffer object.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="internalformat"></param>
        /// <param name="bufferType"></param>
        public Renderbuffer(int width, int height, uint internalformat, RenderbufferType bufferType)
        {
            if (glGenRenderbuffers == null)
            {
                glGenRenderbuffers = OpenGL.GetDelegateFor<OpenGL.glGenRenderbuffers>();
                glBindRenderbuffer = OpenGL.GetDelegateFor<OpenGL.glBindRenderbuffer>();
                glRenderbufferStorage = OpenGL.GetDelegateFor<OpenGL.glRenderbufferStorage>();
            }

            this.Width = width;
            this.Height = height;
            this.BufferType = bufferType;

            glGenRenderbuffers(1, renderbuffer);
            glBindRenderbuffer(OpenGL.GL_RENDERBUFFER, renderbuffer[0]);
            glRenderbufferStorage(OpenGL.GL_RENDERBUFFER,
                internalformat,// TODO: add comment about OpenGL.GL_DEPTH24_STENCIL8, OpenGL.GL_RGBA,
                width, height);
        }

        /// <summary>
        ///
        /// </summary>
        public RenderbufferType BufferType { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Framebuffer Id.
        /// </summary>
        public uint Id { get { return renderbuffer[0]; } }

        /// <summary>
        ///
        /// </summary>
        public int Width { get; set; }

        ///// <summary>
        ///// Bind a named renderbuffer object.
        ///// </summary>
        //public void Bind()
        //{
        //    glBindRenderbuffer(OpenGL.GL_RENDERBUFFER, this.Id);
        //}

        //  TODO: We should be able to just use the code below - however we
        //  get invalid dimension issues at the moment, so recreate for now.
        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="format"></param>
        ///// <param name="width"></param>
        ///// <param name="height"></param>
        //public void Resize(uint format, int width, int height, bool autoBind = true)
        //{
        //    if (autoBind)
        //    {
        //        glBindRenderbuffer(OpenGL.GL_RENDERBUFFER, this.Id);
        //    }
        //    glRenderbufferStorage(OpenGL.GL_RENDERBUFFER, format, width, height);
        //    if (autoBind)
        //    {
        //        glBindRenderbuffer(OpenGL.GL_RENDERBUFFER, 0);
        //    }
        //}

        ///// <summary>
        ///// Unbind this renderbuffer object.
        ///// </summary>
        //public void Unbind()
        //{
        //    glBindRenderbuffer(OpenGL.GL_RENDERBUFFER, 0);
        //}

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}: [w:{1}, h:{2}] {3}", this.GetType().Name, this.Width, this.Height, this.BufferType);
        }
    }
}