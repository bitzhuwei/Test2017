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
    public partial class EMGridCloudRenderer : PickableRenderer
    {
		private static IShaderProgramProvider provider;
		private EMGridRenderer gridRenderer;

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static EMGridCloudRenderer Create(EMGrid model, EMGridRenderer gridRenderer)
        {
			if (provider == null)
			{
				var shaderCodes = new ShaderCode[2];
				shaderCodes[0] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\EMGrid.Cloud.vert"), ShaderType.VertexShader);
				shaderCodes[1] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\EMGrid.Cloud.frag"), ShaderType.FragmentShader);
				provider = new ShaderCodeArray(shaderCodes);
			}
            var map = new AttributeMap();
            map.Add("inPosition", EMGrid.strPosition);
			map.Add("inCloudColor", EMGrid.strCloudColor);
			var renderer = new EMGridCloudRenderer(model, provider, map, EMGrid.strPosition);
            renderer.ModelSize = model.ModelSize;
            renderer.WorldPosition = model.WorldPosition;
            renderer.RotationAngleDegree = model.RotationAngleDegree;
            renderer.RotationAxis = model.RotationAxis;
            renderer.Scale = model.Scale;

			renderer.gridRenderer = gridRenderer;

			return renderer;
        }

		public unsafe void UpdateCloudColor(IList<float> propertyValues, CodedColorArray colorPalette)
		{
			VertexBuffer cloudColorBuffer = this.cloudColorBuffer;
			IntPtr pointer = cloudColorBuffer.MapBuffer(MapBufferAccess.WriteOnly);
			if (pointer == IntPtr.Zero) { return; }

			int length = cloudColorBuffer.Length;
			vec3* array = (vec3*)pointer.ToPointer();
			for (int i = 0; i < length; i++)
			{
				Color c = colorPalette.Map2Color(propertyValues[i]);
				array[i] = c.ToVec3();
			}

			cloudColorBuffer.UnmapBuffer();
		}

		private EMGridCloudRenderer(IBufferable model, IShaderProgramProvider shaderProgramProvider,
            AttributeMap attributeMap, string positionNameInIBufferable,
            params GLState[] switches)
            : base(model, shaderProgramProvider, attributeMap, positionNameInIBufferable, switches)
        {
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
		private VertexBuffer cloudColorBuffer;

		protected override void DoInitialize()
		{
			base.DoInitialize();

			this.cloudColorBuffer = this.DataSource.GetVertexAttributeBuffer(EMGrid.strCloudColor, string.Empty);
		}
		protected override void DoRender(RenderEventArgs arg)
		{
			bool renderFaces = this.gridRenderer.RenderFaces;
			bool renderLines = this.gridRenderer.RenderLines;

			if (renderFaces || renderLines)
			{
				mat4 projection = arg.Camera.GetProjectionMatrix();
				mat4 view = arg.Camera.GetViewMatrix();
				mat4 model = this.gridRenderer.GetModelMatrix().Value;// this.GetModelMatrix().Value;
				this.SetUniform("mvpMatrix", projection * view * model);
				this.SetUniform("normalMatrix", glm.transpose(glm.inverse(model)).to_mat3());
			}

			if (renderFaces)
			{
				this.polygonFaceState.On();
				this.fillOffsetState.On();

				this.SetUniform("useLineColor", false);
				this.SetUniform("ambientLight", this.gridRenderer.AmbientLightColor.ToVec3());
				this.SetUniform("directionalLightColor", this.gridRenderer.DirectionalLightColor.ToVec3());
				vec3 lightDirection = (arg.Camera.Target - arg.Camera.Position).normalize();
				this.SetUniform("directionalLightDirection", lightDirection);
				// highlight options:
				this.SetUniform("highlightIndex0", this.gridRenderer.HighlightIndex0);
				this.SetUniform("highlightIndex1", this.gridRenderer.HighlightIndex1);
				this.SetUniform("highlightIndex2", this.gridRenderer.HighlightIndex2);
				this.SetUniform("regularColor", this.gridRenderer.RegularColor.ToVec3());
				this.SetUniform("highlightColor", this.gridRenderer.HighlightColor.ToVec3());
				this.SetUniform("flatMode", this.gridRenderer.FlatMode);
				this.SetUniform("renderCloud", this.gridRenderer.RenderCloud);

				base.DoRender(arg);

				this.fillOffsetState.Off();
				this.polygonFaceState.Off();
			}

			if (renderLines)
			{
				this.SetUniform("useLineColor", true);
				this.SetUniform("regularLineColor", this.gridRenderer.RegularLineColor.ToVec3());
				this.SetUniform("highlightLineColor", this.gridRenderer.HighlightLineColor.ToVec3());
				this.lineWidthState.On();
				//this.lineOffsetState.On();
				this.polygonLineState.On();

				base.DoRender(arg);

				this.polygonLineState.Off();
				//this.lineOffsetState.Off();
				this.lineWidthState.Off();
			}
		}
	}
}
