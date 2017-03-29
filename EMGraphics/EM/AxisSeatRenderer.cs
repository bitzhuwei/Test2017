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
    public class AxisSeatRenderer : Renderer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static AxisSeatRenderer Create(AxisSeat model)
        {
            var shaderCodes = new ShaderCode[2];
            shaderCodes[0] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\AxisSeat.vert"), ShaderType.VertexShader);
            shaderCodes[1] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\AxisSeat.frag"), ShaderType.FragmentShader);
            var provider = new ShaderCodeArray(shaderCodes);
            var map = new AttributeMap();
            map.Add("inPosition", AxisSeat.strPosition);
            var renderer = new AxisSeatRenderer(model, provider, map);
            renderer.ModelSize = model.ModelSize;
            renderer.SeatColor = model.SeatColor;

            return renderer;
        }

        private AxisSeatRenderer(IBufferable model, IShaderProgramProvider shaderProgramProvider,
            AttributeMap attributeMap, params GLState[] switches)
            : base(model, shaderProgramProvider, attributeMap, switches)
        {
        }

        protected override void DoRender(RenderEventArgs arg)
        {
            this.SetUniform("seatColor", this.SeatColor.ToVec3());

            base.DoRender(arg);
        }

        public System.Drawing.Color SeatColor { get; set; }
    }
}
