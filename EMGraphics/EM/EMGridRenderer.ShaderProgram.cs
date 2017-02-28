using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace EMGraphics
{
    /// <summary>
    /// 
    /// </summary>
    public partial class EMGridRenderer
    {
        private ShaderProgram flatShaderProgram;
        private ShaderProgram smoothShaderProgram;

        protected override void DoInitialize()
        {
            base.DoInitialize();

            this.flatShaderProgram = this.Program;

            // init smooth shader program.
            {
                var shaderCodes = new ShaderCode[2];
                shaderCodes[0] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\EMGridSmooth.vert"), ShaderType.VertexShader);
                shaderCodes[1] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\EMGridSmooth.frag"), ShaderType.FragmentShader);
                this.smoothShaderProgram = shaderCodes.CreateProgram();
            }
        }

		public EMRenderMode RenderMode {
			get
			{
				if (this.Program == this.flatShaderProgram)
				{ return EMRenderMode.Flat; }
				else if (this.Program == this.smoothShaderProgram)
				{ return EMRenderMode.Smooth; }
				else 
				{ throw new NotImplementedException(); }
			}
			set
			{
				switch (value)
				{
					case EMRenderMode.Flat:
						this.Program = this.flatShaderProgram;
						break;
					case EMRenderMode.Smooth:
						this.Program = this.smoothShaderProgram;
						break;
					default:
						throw new NotImplementedException();
				}
			}			
		}
    }

    public enum EMRenderMode
    {
        Flat,
        Smooth,
    }
}