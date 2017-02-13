﻿using System;
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

        [Category(strTestRenderer)]
        public vec3 AmbientLightColor { get; set; }

        [Category(strTestRenderer)]
        public vec3 DirectionalLightDirection { get; set; }

        [Category(strTestRenderer)]
        public vec3 DirectionalLightColor { get; set; }

        //public vec3 HalfVector { get; set; }
        [Category(strTestRenderer)]
        public float Shininess { get; set; }

        [Category(strTestRenderer)]
        public float Strength { get; set; }

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
            map.Add("inNormal", TestModel.strNormal);
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
            this.AmbientLightColor = new vec3(0.5f);
            this.DirectionalLightDirection = new vec3(3, 4, 5).normalize();
            this.DirectionalLightColor = new vec3(1);
            //this.HalfVector = new vec3(1);
            this.Shininess = 10.0f;
            this.Strength = 1.0f;

            this.RenderFaces = true;
            this.RenderLines = true;
        }

        private PolygonModeState polygonFaceState = new PolygonModeState(PolygonMode.Fill);
        private PolygonModeState polygonLineState = new PolygonModeState(PolygonMode.Line);
        private PolygonOffsetState offsetState = new PolygonOffsetLineState();
        private LineWidthState lineWidthState = new LineWidthState(3.0f);

        protected override void DoRender(RenderEventArgs arg)
        {
            bool renderFaces = this.RenderFaces;
            bool renderLines = this.RenderLines;

            if (renderFaces || renderLines)
            {
                mat4 projection = arg.Camera.GetProjectionMatrix();
                mat4 view = arg.Camera.GetViewMatrix();
                mat4 model = this.GetModelMatrix().Value;
                this.SetUniform("mvpMatrix", projection * view * model);
                this.SetUniform("normalMatrix", glm.transpose(glm.inverse(model)).to_mat3());
            }

            if (renderFaces)
            {
                this.SetUniform("useLineColor", false);
                this.polygonFaceState.On();

                this.SetUniform("ambientLight", this.AmbientLightColor);
                this.SetUniform("directionalLightColor", this.DirectionalLightColor);
                this.SetUniform("directionalLightDirection", this.DirectionalLightDirection.normalize());
                this.SetUniform("halfVector", this.DirectionalLightDirection.normalize());
                //this.SetUniform("halfVector", this.HalfVector.normalize());
                this.SetUniform("shininess", this.Shininess);
                this.SetUniform("strength", this.Strength);

                base.DoRender(arg);

                this.polygonFaceState.Off();
            }

            if (renderLines)
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
