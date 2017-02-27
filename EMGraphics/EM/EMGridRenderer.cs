﻿using System;
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
    public partial class EMGridRenderer : PickableRenderer, IHighlightable
    {
        private const string strEMRenderer = "EMRenderer";
        /// <summary>
        /// 是否渲染模型的各个三角形面
        /// </summary>
        [Category(strEMRenderer)]
        public bool RenderFaces { get; set; }

        /// <summary>
        /// 是否渲染模型的各个三角形边
        /// </summary>
        [Category(strEMRenderer)]
        public bool RenderLines { get; set; }

        [Category(strEMRenderer)]
        public vec3 AmbientLightColor { get; set; }

        //[Category(strEMRenderer)]
        //public vec3 DirectionalLightDirection { get; set; }

        [Category(strEMRenderer)]
        public vec3 DirectionalLightColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static EMGridRenderer Create(EMGrid model)
        {
            var shaderCodes = new ShaderCode[3];
            shaderCodes[0] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\EMGridFlat.vert"), ShaderType.VertexShader);
            shaderCodes[1] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\EMGridFlat.geom"), ShaderType.GeometryShader);
            shaderCodes[2] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\EMGridFlat.frag"), ShaderType.FragmentShader);
            var map = new AttributeMap();
            map.Add("inPosition", EMGrid.strPosition);
            var renderer = new EMGridRenderer(model, shaderCodes, map, EMGrid.strPosition);
            renderer.ModelSize = model.ModelSize;
            renderer.WorldPosition = model.WorldPosition;
            renderer.RotationAngleDegree = model.RotationAngleDegree;
            renderer.RotationAxis = model.RotationAxis;
            renderer.Scale = model.Scale;

            return renderer;
        }

        private EMGridRenderer(IBufferable model, ShaderCode[] shaderCodes,
            AttributeMap attributeMap, string positionNameInIBufferable,
            params GLState[] switches)
            : base(model, shaderCodes, attributeMap, positionNameInIBufferable, switches)
        {
            this.AmbientLightColor = new vec3(0.1f);
            this.DirectionalLightColor = new vec3(1);

            this.RenderFaces = true;
            this.RenderLines = true;

            this.HighlightIndex = -2;
            this.HighlightColor = Color.Yellow;

            this.RegularColor = Color.Orange;
        }

        private PolygonModeState polygonFaceState = new PolygonModeState(PolygonMode.Fill);
        private PolygonModeState polygonLineState = new PolygonModeState(PolygonMode.Line);
        private PolygonOffsetState offsetState = new PolygonOffsetLineState();
        private LineWidthState lineWidthState = new LineWidthState(1.0f);

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
                this.polygonFaceState.On();

                this.SetUniform("useLineColor", false);
                this.SetUniform("ambientLight", this.AmbientLightColor);
                this.SetUniform("directionalLightColor", this.DirectionalLightColor);
                vec3 lightDirection = (arg.Camera.Target - arg.Camera.Position).normalize();
                this.SetUniform("directionalLightDirection", lightDirection);
                // highlight options:
                this.SetUniform("highlightIndex", this.HighlightIndex);
                this.SetUniform("highlightColor", this.HighlightColor.ToVec3());
                this.SetUniform("regularColor", this.RegularColor.ToVec3());

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

        #region IHighlightable

        /// <summary>
        /// -2：全部不高亮。-1：全部高亮。0或正整数：高亮指定的图元（此项目中的图元即三角形）。
        /// </summary>
        public int HighlightIndex { get; set; }

        /// <summary>
        /// 用什么颜色表示高亮？
        /// </summary>
        public Color HighlightColor { get; set; }

        #endregion IHighlightable

        /// <summary>
        /// 非高亮时的底色。
        /// </summary>
        public Color RegularColor { get; set; }
    }
}
