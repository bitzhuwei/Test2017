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
    public class IntegratedAxisRenderer : RendererBase
    {
        private AxisSeatRenderer seat;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IntegratedAxisRenderer Create()
        {
            var result = new IntegratedAxisRenderer();
            result.ModelSize = new vec3(1, 1, 1);
            {
                var model = new AxisSeat(0.5f, Color.Gray);
                var renderer = AxisSeatRenderer.Create(model);
                result.seat = renderer;
            }

            return result;
        }

        private IntegratedAxisRenderer() { }

        protected override void DoRender(RenderEventArgs arg)
        {
            this.seat.Render(arg);
        }

        public void SetMVP(mat4 mvp)
        {
            this.seat.SetUniform("mvp", mvp);
        }

        protected override void DoInitialize()
        {
            this.seat.Initialize();
        }
    }
}
