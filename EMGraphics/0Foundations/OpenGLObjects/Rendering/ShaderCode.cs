﻿using System;
using System.Linq;

namespace EMGraphics
{
    /// <summary>
    /// 某种类型的shader代码。
    /// <para>Some type of shader code.</para>
    /// </summary>
    public class ShaderCode: IShaderProgramProvider
	{
        /// <summary>
        /// 某种类型的shader代码。
        /// <para>Some type of shader code.</para>
        /// </summary>
        /// <param name="sourceCode"></param>
        /// <param name="shaderType"></param>
        public ShaderCode(string sourceCode, ShaderType shaderType)
        {
            this.SourceCode = sourceCode;
            this.ShaderType = shaderType;
        }

        /// <summary>
        ///
        /// </summary>
        public string SourceCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ShaderType ShaderType { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}", this.ShaderType);
        }

        /// <summary>
        /// Create OpenGL shader object.
        /// </summary>
        /// <returns></returns>
        public Shader CreateShader()
        {
            var shader = new Shader();
            shader.Create((uint)this.ShaderType, this.SourceCode);

            return shader;
        }

		public ShaderProgram GetShaderProgram()
		{
			return this.CreateProgram();
		}
	}

    /// <summary>
    ///
    /// </summary>
    public static class ShaderCodesHelper
    {
        /// <summary>
        /// Creates a shader program object by a single shader.
        /// </summary>
        /// <param name="shaderCode"></param>
        /// <returns></returns>
        public static ShaderProgram CreateProgram(this ShaderCode shaderCode)
        {
            var program = new ShaderProgram();
            Shader shader = shaderCode.CreateShader();
            program.Initialize(shader);

            shader.Dispose();

            return program;
        }

        /// <summary>
        /// Creates a shader program by multiple shaders.
        /// </summary>
        /// <param name="shaderCodes"></param>
        /// <returns></returns>
        public static ShaderProgram CreateProgram(this ShaderCode[] shaderCodes)
        {
            var program = new ShaderProgram();
            Shader[] shaders = (from item in shaderCodes select item.CreateShader()).ToArray();
            program.Initialize(shaders);

            foreach (Shader item in shaders) { item.Dispose(); }

            return program;
        }
    }
}