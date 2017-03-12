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
    public class NormalLineRenderer : PickableRenderer
    {
        public MarkableStruct<Color> HeadColor { get; set; }
        private long headColorUpdatedTicks;

        public MarkableStruct<Color> TailColor { get; set; }
        private long tailColorUpdatedTicks;

		private static IShaderProgramProvider provider;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static NormalLineRenderer Create(NormalLineModel model)
        {
			if (provider == null)
			{
				var shaderCodes = new ShaderCode[2];
				shaderCodes[0] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\NormalLine.vert"), ShaderType.VertexShader);
				shaderCodes[1] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\NormalLine.frag"), ShaderType.FragmentShader);
				provider = new ShaderCodeArray(shaderCodes);
			}
            var map = new AttributeMap();
            map.Add("inPosition", NormalLineModel.strPosition);
            var renderer = new NormalLineRenderer(model, provider, map, EMGrid.strPosition);
            renderer.ModelSize = model.ModelSize;
            renderer.WorldPosition = model.WorldPosition;
            renderer.RotationAngleDegree = model.RotationAngleDegree;
            renderer.RotationAxis = model.RotationAxis;
            renderer.Scale = model.Scale;

            return renderer;
        }

		private NormalLineRenderer(IBufferable model, IShaderProgramProvider shaderProgramProvider,
			AttributeMap attributeMap, string positionNameInIBufferable,
			params GLState[] switches)
			: base(model, shaderProgramProvider, attributeMap, positionNameInIBufferable, switches)
		{
			this.HeadColor = new MarkableStruct<Color>(Color.FromArgb(255, 100, 150, 150));
			this.TailColor = new MarkableStruct<Color>(Color.Red);
			this.StateList.Add(new LineWidthState(2));
		}

        protected override void DoRender(RenderEventArgs arg)
        {
            mat4 projection = arg.Camera.GetProjectionMatrix();
            mat4 view = arg.Camera.GetViewMatrix();
            mat4 model = this.GetModelMatrix().Value;
            this.SetUniform("mvpMatrix", projection * view * model);
            if (this.HeadColor.UpdateTicks != this.headColorUpdatedTicks)
            {
                this.SetUniform("headColor", this.HeadColor.Value.ToVec3());
                this.headColorUpdatedTicks = this.HeadColor.UpdateTicks;
            }
            if (this.TailColor.UpdateTicks != this.tailColorUpdatedTicks)
            {
                this.SetUniform("tailColor", this.TailColor.Value.ToVec3());
                this.tailColorUpdatedTicks = this.TailColor.UpdateTicks;
            }

            base.DoRender(arg);
        }
    }
}
