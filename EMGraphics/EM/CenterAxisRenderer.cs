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
    public class CenterAxisRenderer : Renderer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static CenterAxisRenderer Create(CenterAxis model)
        {
            var shaderCodes = new ShaderCode[2];
            shaderCodes[0] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\CenterAxis.vert"), ShaderType.VertexShader);
            shaderCodes[1] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\CenterAxis.frag"), ShaderType.FragmentShader);
			var provider = new ShaderCodeArray(shaderCodes);
            var map = new AttributeMap();
            map.Add("inPosition", CenterAxis.strPosition);
            map.Add("inColor", CenterAxis.strColor);
            var renderer = new CenterAxisRenderer(model, provider, map);
			renderer.ModelSize = model.ModelSize;

            return renderer;
        }

        private CenterAxisRenderer(IBufferable model, IShaderProgramProvider shaderProgramProvider,
            AttributeMap attributeMap, params GLState[] switches)
            : base(model, shaderProgramProvider, attributeMap, switches)
        {
        }

        LineStippleState lineStippleState = new LineStippleState();

        protected override void DoRender(RenderEventArgs arg)
        {
            mat4 projection = arg.Camera.GetProjectionMatrix();
            mat4 view = arg.Camera.GetViewMatrix();
            mat4 model = this.GetModelMatrix().Value;
            this.SetUniform("mvpMatrix", projection * view * model);

            base.DoRender(arg);

            lineStippleState.On();
            // 把所有在此之前渲染的内容都推到最远。
            // Push all rendered stuff to farest position.
            OpenGL.Clear(OpenGL.GL_DEPTH_BUFFER_BIT);
            base.DoRender(arg);
            lineStippleState.Off();
        }
    }
}
