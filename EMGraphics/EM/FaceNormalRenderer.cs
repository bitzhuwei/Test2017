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
    public partial class FaceNormalRenderer : Renderer
    {
        public MarkableStruct<Color> HeadColor { get; set; }
        private long headColorUpdatedTicks;

        public MarkableStruct<Color> TailColor { get; set; }
        private long tailColorUpdatedTicks;

		private static IShaderProgramProvider provider;

		private Dictionary<string, int> labelDict;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static FaceNormalRenderer Create(FaceNormal model)
        {
			if (provider == null)
			{
				var shaderCodes = new ShaderCode[2];
				shaderCodes[0] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\NormalLine.vert"), ShaderType.VertexShader);
				shaderCodes[1] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\NormalLine.frag"), ShaderType.FragmentShader);
				provider = new ShaderCodeArray(shaderCodes);
			}
            var map = new AttributeMap();
            map.Add("inPosition", FaceNormal.strPosition);
            var renderer = new FaceNormalRenderer(model, provider, map);
            renderer.ModelSize = model.ModelSize;
            renderer.WorldPosition = model.WorldPosition;
            renderer.RotationAngleDegree = model.RotationAngleDegree;
            renderer.RotationAxis = model.RotationAxis;
            renderer.Scale = model.Scale;
			
			// this is for rendering part of whole normals.
			renderer.labelDict = model.LabelDict;
			var indexBuffers = new ZeroIndexBuffer[model.StartIndexes.Length];
			for (int i = 0; i < indexBuffers.Length; i++)
			{
				indexBuffers[i] = new ZeroIndexBuffer(DrawMode.Lines,
					model.StartIndexes[i], model.Counts[i]);
			}
			renderer.renderSingleIndexBuffers = indexBuffers;
			
            return renderer;
        }

		
		private FaceNormalRenderer(IBufferable model, IShaderProgramProvider shaderProgramProvider,
			AttributeMap attributeMap, params GLState[] switches)
			: base(model, shaderProgramProvider, attributeMap, switches)
		{
			this.HeadColor = new MarkableStruct<Color>(Color.FromArgb(255, 100, 150, 150));
			this.TailColor = new MarkableStruct<Color>(Color.Red);
			//this.StateList.Add(new LineWidthState(1));
			//this.RenderAll = false;
			//this.RenderGroups = true;
		}

		private ZeroIndexBuffer[] renderSingleIndexBuffers;

		/// <summary>
		/// which indexes in renderSingleIndexBuffers are to be rendered?
		/// </summary>
		private List<int> visibleGroups = new List<int>();

		public void SetVisible(string label, bool visible)
		{
			int index;
			if(this.labelDict.TryGetValue(label, out index))
			{
				if(visible)
				{
					if(!this.visibleGroups.Contains(index))
					{
						this.visibleGroups.Add(index);
					}
				}
				else
				{
					if(this.visibleGroups.Contains(index))
					{
						this.visibleGroups.Remove(index);
					}
				}
			}
		}

		/// <summary>
		/// Render all normals no matter what.
		/// </summary>
		public bool RenderAll { get; set; }

		/// <summary>
		/// Render normal groups whose indexes are in <see cref="visibleGroups"/>.
		/// </summary>
		public bool RenderGroups { get; set; }

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

			if (this.RenderAll)
			{
				base.DoRender(arg);
			}
			else if (this.RenderGroups)
			{
				if (this.visibleGroups.Count > 0)
				{
					ShaderProgram program = this.Program;

					// 绑定shader
					program.Bind();
					SetUniformValues(program);

					GLState[] stateList = this.stateList.ToArray();
					StatesOn(stateList);

					foreach (var index in this.visibleGroups)
					{
						this.vertexArrayObject.Render(arg, program, 
							this.renderSingleIndexBuffers[index]);
					}

					StatesOff(stateList);

					// 解绑shader
					program.Unbind();
				}
			}
		}

    }
}
