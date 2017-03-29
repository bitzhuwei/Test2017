using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace EMGraphics
{
    /// <summary>
    /// 
    /// </summary>
    public class AxisBodyRenderer : Renderer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static AxisBodyRenderer Create(AxisBody model)
        {
            var shaderCodes = new ShaderCode[2];
            shaderCodes[0] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\AxisBody.vert"), ShaderType.VertexShader);
            shaderCodes[1] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\AxisBody.frag"), ShaderType.FragmentShader);
            var provider = new ShaderCodeArray(shaderCodes);
            var map = new AttributeMap();
            map.Add("inPosition", AxisBody.strPosition);
            map.Add("inColor", AxisBody.strColor);
            var renderer = new AxisBodyRenderer(model, provider, map);
            renderer.ModelSize = model.ModelSize;

            return renderer;
        }

        private AxisBodyRenderer(IBufferable model, IShaderProgramProvider shaderProgramProvider,
            AttributeMap attributeMap, params GLState[] switches)
            : base(model, shaderProgramProvider, attributeMap, switches)
        {
        }

        protected override void DoRender(RenderEventArgs arg)
        {
            base.DoRender(arg);
        }

    }
}
