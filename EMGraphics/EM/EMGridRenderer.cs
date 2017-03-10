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
        public Color AmbientLightColor { get; set; }

        //[Category(strEMRenderer)]
        //public vec3 DirectionalLightDirection { get; set; }

        [Category(strEMRenderer)]
        public Color DirectionalLightColor { get; set; }

		private static IShaderProgramProvider provider;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static EMGridRenderer Create(EMGrid model)
        {
			if (provider == null)
			{
				var shaderCodes = new ShaderCode[3];
				shaderCodes[0] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\EMGrid.vert"), ShaderType.VertexShader);
				shaderCodes[1] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\EMGrid.geom"), ShaderType.GeometryShader);
				shaderCodes[2] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\EMGrid.frag"), ShaderType.FragmentShader);
				provider = new ShaderCodeArray(shaderCodes);
			}
            var map = new AttributeMap();
            map.Add("inPosition", EMGrid.strPosition);
			map.Add("inNormal", EMGrid.strNormal);
			IShaderProgramProvider pro = null;
			var renderer = new EMGridRenderer(model, provider, map, EMGrid.strPosition);
            renderer.ModelSize = model.ModelSize;
            renderer.WorldPosition = model.WorldPosition;
            renderer.RotationAngleDegree = model.RotationAngleDegree;
            renderer.RotationAxis = model.RotationAxis;
            renderer.Scale = model.Scale;

            return renderer;
        }

        private EMGridRenderer(IBufferable model, IShaderProgramProvider shaderProgramProvider,
            AttributeMap attributeMap, string positionNameInIBufferable,
            params GLState[] switches)
            : base(model, shaderProgramProvider, attributeMap, positionNameInIBufferable, switches)
        {
			this.AmbientLightColor = Color.FromArgb(255, 70, 70, 70);
			this.DirectionalLightColor = Color.White;

            this.RenderFaces = true;
            this.RenderLines = true;

            this.HighlightIndex0 = -2;
            this.HighlightIndex1 = -2;
            this.HighlightIndex2 = -2;

            this.RegularColor = Color.FromArgb(255, 202, 122, 0);
            this.RegularLineColor = Color.FromArgb(255, 211, 211, 211);
            this.HighlightColor = Color.FromArgb(255, 204, 203, 0);
            this.HighlightLineColor = Color.FromArgb(255, 100, 100, 100);

            this.FlatMode = true;
        }

        private PolygonModeState polygonFaceState = new PolygonModeState(PolygonMode.Fill);
        private PolygonModeState polygonLineState = new PolygonModeState(PolygonMode.Line);
        //private PolygonOffsetState lineOffsetState = new PolygonOffsetLineState(true);
        private PolygonOffsetState fillOffsetState = new PolygonOffsetFillState(pullNear: false);

  //      public PolygonOffsetState LineOffsetState
  //      {
  //          get { return lineOffsetState; }
  //      }
		//public PolygonOffsetState FillOffsetState
		//{
		//	get { return fillOffsetState; }
		//}

        private LineWidthState lineWidthState = new LineWidthState(0.5f);

        /// <summary>
        /// 
        /// </summary>
        public LineWidthState LineWidth { get { return this.lineWidthState; } }

        /// <summary>
        /// 
        /// </summary>
        public Color HighlightLineColor { get; set; }
        public Color RegularLineColor { get; set; }

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
                this.fillOffsetState.On();

                this.SetUniform("useLineColor", false);
                this.SetUniform("ambientLight", this.AmbientLightColor.ToVec3());
                this.SetUniform("directionalLightColor", this.DirectionalLightColor.ToVec3());
                vec3 lightDirection = (arg.Camera.Target - arg.Camera.Position).normalize();
                this.SetUniform("directionalLightDirection", lightDirection);
                // highlight options:
                this.SetUniform("highlightIndex0", this.HighlightIndex0);
                this.SetUniform("highlightIndex1", this.HighlightIndex1);
                this.SetUniform("highlightIndex2", this.HighlightIndex2);
                this.SetUniform("regularColor", this.RegularColor.ToVec3());
                this.SetUniform("highlightColor", this.HighlightColor.ToVec3());
                this.SetUniform("flatMode", this.FlatMode);

                base.DoRender(arg);

                this.fillOffsetState.Off();
                this.polygonFaceState.Off();
            }

            if (renderLines)
            {
                this.SetUniform("useLineColor", true);
                this.SetUniform("regularLineColor", this.RegularLineColor.ToVec3());
                this.SetUniform("highlightLineColor", this.HighlightLineColor.ToVec3());
                this.lineWidthState.On();
                //this.lineOffsetState.On();
                this.polygonLineState.On();

                base.DoRender(arg);

                this.polygonLineState.Off();
                //this.lineOffsetState.Off();
                this.lineWidthState.Off();
            }

        }

        #region IHighlightable

        /// <summary>
        /// -2：全部不高亮。-1：全部高亮。0或正整数：高亮指定的图元（此项目中的图元即三角形）。
        /// </summary>
        public int HighlightIndex0 { get; set; }
        /// <summary>
        /// -2：全部不高亮。-1：全部高亮。0或正整数：高亮指定的图元（此项目中的图元即三角形）。
        /// </summary>
        public int HighlightIndex1 { get; set; }
        /// <summary>
        /// -2：全部不高亮。-1：全部高亮。0或正整数：高亮指定的图元（此项目中的图元即三角形）。
        /// </summary>
        public int HighlightIndex2 { get; set; }

        /// <summary>
        /// 用什么颜色表示高亮？
        /// </summary>
        public Color HighlightColor { get; set; }

        #endregion IHighlightable

        /// <summary>
        /// 非高亮时的底色。
        /// </summary>
        public Color RegularColor { get; set; }

        /// <summary>
        /// true for flat mode; false for smooth mode.
        /// </summary>
        public bool FlatMode { get; set; }
	}
}
