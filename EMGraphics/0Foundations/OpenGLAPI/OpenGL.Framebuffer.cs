﻿namespace EMGraphics
{
    public static partial class OpenGL
    {
        #region GL_framebuffer_no_attachments

        ////  Delegates
        /// <summary>
        /// Set a named parameter of a framebuffer.
        /// </summary>
        /// <param name="target">The target of the operation, which must be GL_READ_FRAMEBUFFER​, GL_DRAW_FRAMEBUFFER​ or GL_FRAMEBUFFER​.</param>
        /// <param name="pname">A token indicating the parameter to be modified.</param>
        /// <param name="param">The new value for the parameter named pname​.</param>
        internal delegate void glFramebufferParameteri(uint target, uint pname, int param);

        /// <summary>
        /// Retrieve a named parameter from a framebuffer
        /// </summary>
        /// <param name="target">The target of the operation, which must be GL_READ_FRAMEBUFFER​, GL_DRAW_FRAMEBUFFER​ or GL_FRAMEBUFFER​.</param>
        /// <param name="pname">A token indicating the parameter to be retrieved.</param>
        /// <param name="parameters">The address of a variable to receive the value of the parameter named pname​.</param>
        public delegate void glGetFramebufferParameteriv(uint target, uint pname, int[] parameters);

        /// <summary>
        ///
        /// </summary>
        /// <param name="framebuffer"></param>
        /// <param name="pname"></param>
        /// <param name="param"></param>
        public delegate void glNamedFramebufferParameteri(uint framebuffer, uint pname, int param);

        /// <summary>
        ///
        /// </summary>
        /// <param name="framebuffer"></param>
        /// <param name="pname"></param>
        /// <param name="parameters"></param>
        public delegate void glGetNamedFramebufferParameteriv(uint framebuffer, uint pname, int[] parameters);

        #endregion GL_framebuffer_no_attachments

        #region GL_EXT_framebuffer_object

        //  Delegates
        /// <summary>
        ///
        /// </summary>
        /// <param name="renderbuffer"></param>
        /// <returns></returns>
        public delegate bool glIsRenderbuffer(uint renderbuffer);

        /// <summary>
        /// bind a named renderbuffer object.
        /// </summary>
        /// <param name="target">Specifies the target to which the renderbuffer object is bound. The symbolic constant must be GL_RENDERBUFFER.</param>
        /// <param name="renderbuffer">Specifies the name of a renderbuffer object.</param>
        internal delegate void glBindRenderbuffer(uint target, uint renderbuffer);

        /// <summary>
        ///
        /// </summary>
        /// <param name="n"></param>
        /// <param name="renderbuffers"></param>
        private delegate void glDeleteRenderbuffers(uint n, uint[] renderbuffers);

        /// <summary>
        ///
        /// </summary>
        /// <param name="n"></param>
        /// <param name="renderbuffers"></param>
        internal delegate void glGenRenderbuffers(uint n, uint[] renderbuffers);

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        /// <param name="internalformat"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        internal delegate void glRenderbufferStorage(uint target, uint internalformat, int width, int height);

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        /// <param name="pname"></param>
        /// <param name="parameters"></param>
        public delegate void glGetRenderbufferParameteriv(uint target, uint pname, int[] parameters);

        /// <summary>
        ///
        /// </summary>
        /// <param name="framebuffer"></param>
        /// <returns></returns>
        public delegate bool glIsFramebuffer(uint framebuffer);

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        /// <param name="framebuffer"></param>
        internal delegate void glBindFramebuffer(uint target, uint framebuffer);

        /// <summary>
        ///
        /// </summary>
        /// <param name="n"></param>
        /// <param name="framebuffers"></param>
        private delegate void glDeleteFramebuffers(uint n, uint[] framebuffers);

        /// <summary>
        ///
        /// </summary>
        /// <param name="n"></param>
        /// <param name="framebuffers"></param>
        internal delegate void glGenFramebuffers(uint n, uint[] framebuffers);

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        internal delegate uint glCheckFramebufferStatus(uint target);

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        /// <param name="attachment"></param>
        /// <param name="textarget"></param>
        /// <param name="texture"></param>
        /// <param name="level"></param>
        public delegate void glFramebufferTexture1D(uint target, uint attachment, uint textarget, uint texture, int level);

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        /// <param name="attachment"></param>
        /// <param name="textarget"></param>
        /// <param name="texture"></param>
        /// <param name="level"></param>
        internal delegate void glFramebufferTexture2D(uint target, uint attachment, uint textarget, uint texture, int level);

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        /// <param name="attachment"></param>
        /// <param name="textarget"></param>
        /// <param name="texture"></param>
        /// <param name="level"></param>
        /// <param name="zoffset"></param>
        public delegate void glFramebufferTexture3D(uint target, uint attachment, uint textarget, uint texture, int level, int zoffset);

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        /// <param name="attachment"></param>
        /// <param name="renderbuffertarget"></param>
        /// <param name="renderbuffer"></param>
        internal delegate void glFramebufferRenderbuffer(uint target, uint attachment, uint renderbuffertarget, uint renderbuffer);

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        /// <param name="attachment"></param>
        /// <param name="pname"></param>
        /// <param name="parameters"></param>
        public delegate void glGetFramebufferAttachmentParameteriv(uint target, uint attachment, uint pname, int[] parameters);

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        private delegate void glGenerateMipmap(uint target);

        private static OpenGL.glGenerateMipmap _glGenerateMipmap;

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        public static void GenerateMipmap(MipmapTarget target)
        {
            if (_glGenerateMipmap == null)
            { _glGenerateMipmap = OpenGL.GetDelegateFor<OpenGL.glGenerateMipmap>(); }
            _glGenerateMipmap((uint)target);
        }

        //  Constants
        /// <summary>
        ///
        /// </summary>
        public const uint GL_INVALID_FRAMEBUFFER_OPERATION = 0x0506;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_MAX_RENDERBUFFER_SIZE = 0x84E8;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_FRAMEBUFFER_BINDING = 0x8CA6;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_RENDERBUFFER_BINDING = 0x8CA7;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_READ_FRAMEBUFFER = 0x8CA8;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_DRAW_FRAMEBUFFER = 0x8CA9;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE = 0x8CD0;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_FRAMEBUFFER_ATTACHMENT_OBJECT_NAME = 0x8CD1;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_FRAMEBUFFER_ATTACHMENT_TEXTURE_LEVEL = 0x8CD2;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_FRAMEBUFFER_ATTACHMENT_TEXTURE_CUBE_MAP_FACE = 0x8CD3;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_FRAMEBUFFER_ATTACHMENT_TEXTURE_3D_ZOFFSET = 0x8CD4;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_FRAMEBUFFER_COMPLETE = 0x8CD5;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_FRAMEBUFFER_UNDEFINED = 0x8219;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_FRAMEBUFFER_INCOMPLETE_ATTACHMENT = 0x8CD6;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT = 0x8CD7;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_FRAMEBUFFER_INCOMPLETE_MULTISAMPLE = 0x8D56;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_FRAMEBUFFER_INCOMPLETE_DIMENSIONS = 0x8CD9;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_FRAMEBUFFER_INCOMPLETE_FORMATS_EXT = 0x8CDA;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_FRAMEBUFFER_INCOMPLETE_DRAW_BUFFER = 0x8CDB;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_FRAMEBUFFER_INCOMPLETE_READ_BUFFER = 0x8CDC;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_FRAMEBUFFER_UNSUPPORTED = 0x8CDD;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_MAX_COLOR_ATTACHMENTS = 0x8CDF;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_MAX_FRAMEBUFFER_WIDTH = 0x9315;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_MAX_FRAMEBUFFER_HEIGHT = 0x9316;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_MAX_FRAMEBUFFER_LAYERS = 0x9317;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_MAX_FRAMEBUFFER_SAMPLES = 0x9318;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_COLOR_ATTACHMENT0 = 0x8CE0;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_COLOR_ATTACHMENT1 = 0x8CE1;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_COLOR_ATTACHMENT2 = 0x8CE2;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_COLOR_ATTACHMENT3 = 0x8CE3;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_COLOR_ATTACHMENT4 = 0x8CE4;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_COLOR_ATTACHMENT5 = 0x8CE5;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_COLOR_ATTACHMENT6 = 0x8CE6;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_COLOR_ATTACHMENT7 = 0x8CE7;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_COLOR_ATTACHMENT8 = 0x8CE8;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_COLOR_ATTACHMENT9 = 0x8CE9;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_COLOR_ATTACHMENT10 = 0x8CEA;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_COLOR_ATTACHMENT11 = 0x8CEB;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_COLOR_ATTACHMENT12 = 0x8CEC;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_COLOR_ATTACHMENT13 = 0x8CED;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_COLOR_ATTACHMENT14 = 0x8CEE;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_COLOR_ATTACHMENT15 = 0x8CEF;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_DEPTH_ATTACHMENT = 0x8D00;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_STENCIL_ATTACHMENT = 0x8D20;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_DEPTH_STENCIL_ATTACHMENT = 0x821A;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_FRAMEBUFFER = 0x8D40;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_RENDERBUFFER = 0x8D41;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_RENDERBUFFER_WIDTH = 0x8D42;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_RENDERBUFFER_HEIGHT = 0x8D43;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_RENDERBUFFER_INTERNAL_FORMAT = 0x8D44;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_STENCIL_INDEX1 = 0x8D46;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_STENCIL_INDEX4 = 0x8D47;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_STENCIL_INDEX8 = 0x8D48;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_STENCIL_INDEX16 = 0x8D49;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_RENDERBUFFER_RED_SIZE = 0x8D50;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_RENDERBUFFER_GREEN_SIZE = 0x8D51;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_RENDERBUFFER_BLUE_SIZE = 0x8D52;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_RENDERBUFFER_ALPHA_SIZE = 0x8D53;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_RENDERBUFFER_DEPTH_SIZE = 0x8D54;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_RENDERBUFFER_STENCIL_SIZE = 0x8D55;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_FRAMEBUFFER_DEFAULT_WIDTH = 0x9310;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_FRAMEBUFFER_DEFAULT_HEIGHT = 0x9311;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_FRAMEBUFFER_DEFAULT_LAYERS = 0x9312;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_FRAMEBUFFER_DEFAULT_SAMPLES = 0x9313;

        /// <summary>
        ///
        /// </summary>
        public const uint GL_FRAMEBUFFER_DEFAULT_FIXED_SAMPLES_LOCATIONS = 0x9314;

        #endregion GL_EXT_framebuffer_object

        //#region GL_EXT_framebuffer_multisample

        ////  Delegates
        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="target"></param>
        ///// <param name="samples"></param>
        ///// <param name="internalformat"></param>
        ///// <param name="width"></param>
        ///// <param name="height"></param>
        //public delegate void glRenderbufferStorageMultisample(uint target, int samples, uint internalformat, int width, int height);

        ////  Constants
        ///// <summary>
        /////
        ///// </summary>
        //public const uint GL_RENDERBUFFER_SAMPLES = 0x8CAB;
        ///// <summary>
        /////
        ///// </summary>
        //public const uint GL_FRAMEBUFFER_INCOMPLETE_MULTISAMPLE = 0x8D56;
        ///// <summary>
        /////
        ///// </summary>
        //public const uint GL_MAX_SAMPLES = 0x8D57;

        //#endregion
    }
}