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
    public class NormalLineRenderer : PickableRenderer
    {
        private const string strNormalLineRenderer = "NormalLineRenderer";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static NormalLineRenderer Create(NormalLineModel model)
        {
            var shaderCodes = new ShaderCode[2];
            shaderCodes[0] = new ShaderCode(File.ReadAllText(@"NormalLine.vert"), ShaderType.VertexShader);
            shaderCodes[1] = new ShaderCode(File.ReadAllText(@"NormalLine.frag"), ShaderType.FragmentShader);
            var map = new AttributeMap();
            map.Add("inPosition", NormalLineModel.strPosition);
            var renderer = new NormalLineRenderer(model, shaderCodes, map, EMModel.strPosition);
            renderer.ModelSize = model.ModelSize;
            renderer.WorldPosition = model.WorldPosition;
            renderer.RotationAngleDegree = model.RotationAngleDegree;
            renderer.RotationAxis = model.RotationAxis;
            renderer.Scale = model.Scale;

            return renderer;
        }

        private NormalLineRenderer(IBufferable model, ShaderCode[] shaderCodes,
            AttributeMap attributeMap, string positionNameInIBufferable,
            params GLState[] switches)
            : base(model, shaderCodes, attributeMap, positionNameInIBufferable, switches)
        {
            this.StateList.Add(new LineWidthState(1));
        }

        protected override void DoRender(RenderEventArgs arg)
        {
            mat4 projection = arg.Camera.GetProjectionMatrix();
            mat4 view = arg.Camera.GetViewMatrix();
            mat4 model = this.GetModelMatrix().Value;
            this.SetUniform("mvpMatrix", projection * view * model);

            base.DoRender(arg);
        }
    }
}
