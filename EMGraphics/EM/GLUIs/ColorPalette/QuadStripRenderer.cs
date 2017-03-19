using System.IO;

namespace EMGraphics
{
	//   ^
	//  /|\ y
	//   |
	//   |
	//   |
	// (0, 0)-----------> x
	//
	// 0------------------1
	// |                  |
	// |                  |
	// |                  |
	// 2------------------3
	// |                  |
	// |                  |
	// |                  |
	// 4------------------5
	// |                  |
	// |                  |
	// |                  |
	// 6------------------7
	// |                  |
	// |                  |
	// |                  |
	// 8------------------9
	// |                  |
	// |                  |
	// |                  |
	// 10----------------11
	// 
	// side length is 1.
	//
	/// <summary>
	/// </summary>
	internal class QuadStripRenderer : Renderer
    {

        public static QuadStripRenderer Create(QuadStrip model)
        {
            var shaderCodes = new ShaderCode[2];
            shaderCodes[0] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\QuadStripTexture.vert"), ShaderType.VertexShader);
            shaderCodes[1] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\QuadStripTexture.frag"), ShaderType.FragmentShader);
			var provider = new ShaderCodeArray(shaderCodes);
            var map = new AttributeMap();
			map.Add("in_Position", QuadStrip.position);
            map.Add("in_TexCoord", QuadStrip.texCoord);

            var renderer = new QuadStripRenderer(model, provider, map);
            return renderer;
        }

        private QuadStripRenderer(IBufferable model, IShaderProgramProvider shaderProgramProvider,
            AttributeMap attributeMap, params GLState[] switches)
            : base(model, shaderProgramProvider, attributeMap, switches)
        {
        }
    }
}