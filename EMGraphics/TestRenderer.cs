using CSharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMGraphics
{
    /// <summary>
    /// 
    /// </summary>
    public class TestRenderer : PickableRenderer
    {
        private const string strTestRenderer = "TestRenderer";
        /// <summary>
        /// 是否渲染模型的各个三角形面
        /// </summary>
        [Category(strTestRenderer)]
        public bool RenderFaces { get; set; }

        /// <summary>
        /// 是否渲染模型的各个三角形边
        /// </summary>
        [Category(strTestRenderer)]
        public bool RenderLines { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static TestRenderer Create(TestModel model)
        {
            var shaderCodes = new ShaderCode[2];
            shaderCodes[0] = new ShaderCode(File.ReadAllText(@"Test.vert"), ShaderType.VertexShader);
            shaderCodes[1] = new ShaderCode(File.ReadAllText(@"Test.frag"), ShaderType.FragmentShader);
            var map = new AttributeMap();
            map.Add("inPosition", TestModel.strPosition);
            map.Add("inColor", TestModel.strColor);
            var renderer = new TestRenderer(model, shaderCodes, map, TestModel.strPosition);
            renderer.ModelSize = model.ModelSize;
            renderer.WorldPosition = model.WorldPosition;
            renderer.RotationAngleDegree = model.RotationAngleDegree;
            renderer.RotationAxis = model.RotationAxis;
            renderer.Scale = model.Scale;

            return renderer;
        }

        private TestRenderer(IBufferable model, ShaderCode[] shaderCodes,
            AttributeMap attributeMap, string positionNameInIBufferable,
            params GLState[] switches)
            : base(model, shaderCodes, attributeMap, positionNameInIBufferable, switches)
        {
            this.RenderFaces = true;
            this.RenderLines = true;
        }

        private PolygonModeState polygonFaceState = new PolygonModeState(PolygonMode.Fill);
        private PolygonModeState polygonLineState = new PolygonModeState(PolygonMode.Line);
        private PolygonOffsetState offsetState = new PolygonOffsetLineState();
        private LineWidthState lineWidthState = new LineWidthState(3.0f);

        protected override void DoRender(RenderEventArgs arg)
        {
            mat4 projection = arg.Camera.GetProjectionMatrix();
            mat4 view = arg.Camera.GetViewMatrix();
            mat4 model = this.GetModelMatrix().Value;
            this.SetUniform("projection", projection);
            this.SetUniform("view", view);
            this.SetUniform("model", model);

            if (this.RenderFaces)
            {
                this.SetUniform("useLineColor", false);
                this.polygonFaceState.On();
                base.DoRender(arg);
                this.polygonFaceState.Off();
            }

            if (this.RenderLines)
            {
                this.SetUniform("useLineColor", true);
                this.lineWidthState.On();
                this.offsetState.On();
                this.polygonLineState.On();
                base.DoRender(arg);
                this.polygonLineState.Off();
                this.offsetState.Off();
                this.lineWidthState.Off();
            }
        }
    }
}
