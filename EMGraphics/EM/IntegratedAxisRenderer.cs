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
        private AxisBodyRenderer body;
        private LabelRenderer labelX;
        private LabelRenderer labelY;
        private LabelRenderer labelZ;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IntegratedAxisRenderer Create()
        {
            var result = new IntegratedAxisRenderer();
            result.ModelSize = new vec3(1, 1, 1);

            {
                var model = new AxisSeat(0.4f, Color.Gray);
                var renderer = AxisSeatRenderer.Create(model);
                result.seat = renderer;
            }
            {
                var model = new AxisBody(0.6f);
                var renderer = AxisBodyRenderer.Create(model);
                result.body = renderer;
            }
            const float labelDisplacement = 0.85f;
            {
                var renderer = LabelRenderer.Create(1);
                renderer.Text = "X";
                renderer.TextColor = Color.Blue;
                renderer.WorldPosition = new vec3(labelDisplacement, 0, 0);
                result.labelX = renderer;
            }
            {
                var renderer = LabelRenderer.Create(1);
                renderer.Text = "Y";
                renderer.TextColor = Color.Green;
                renderer.WorldPosition = new vec3(0, labelDisplacement, 0);
                result.labelY = renderer;
            }
            {
                var renderer = LabelRenderer.Create(1);
                renderer.Text = "Z";
                renderer.TextColor = Color.Red;
                renderer.WorldPosition = new vec3(0, 0, labelDisplacement);
                result.labelZ = renderer;
            }

            return result;
        }

        private IntegratedAxisRenderer() { }

        protected override void DoRender(RenderEventArgs arg)
        {
            this.seat.Render(arg);
            this.body.Render(arg);
            this.labelX.Render(arg);
            this.labelY.Render(arg);
            this.labelZ.Render(arg);
        }

        public void SetMVP(mat4 mvp)
        {
            this.seat.SetUniform("mvp", mvp);
            this.body.SetUniform("mvp", mvp);
        }

        protected override void DoInitialize()
        {
            this.seat.Initialize();
            this.body.Initialize();
            this.labelX.Initialize();
            this.labelX.Initialize();
            this.labelX.Initialize();
        }
    }
}
