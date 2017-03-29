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
        private AxisSeatRenderer seat2;
        private AxisBodyRenderer body;
        private AxisLabelRenderer labelX;
        private AxisLabelRenderer labelY;
        private AxisLabelRenderer labelZ;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IntegratedAxisRenderer Create()
        {
            var result = new IntegratedAxisRenderer();
            result.ModelSize = new vec3(1, 1, 1);

            {
                var model = new AxisSeat(0.25f, Color.Black);
                var renderer = AxisSeatRenderer.Create(model);
                renderer.ModelSize = new vec3(1, 1, 1);
                result.seat = renderer;
            }
            {
                var model = new AxisSeat(0.5f, Color.Gray);
                var renderer = AxisSeatRenderer.Create(model);
                renderer.ModelSize = new vec3(1, 1, 1);
                result.seat2 = renderer;
            }
            {
                var model = new AxisBody(0.6f);
                var renderer = AxisBodyRenderer.Create(model);
                renderer.ModelSize = new vec3(1, 1, 1);
                result.body = renderer;
            }
            const float labelDisplacement = 0.4f;
            {
                var renderer = AxisLabelRenderer.Create(1);
                renderer.Text = "X";
                renderer.TextColor = Color.Blue;
                renderer.WorldPosition = new vec3(labelDisplacement, 0, 0);
                result.labelX = renderer;
            }
            {
                var renderer = AxisLabelRenderer.Create(1);
                renderer.Text = "Y";
                renderer.TextColor = Color.Green;
                renderer.WorldPosition = new vec3(0, labelDisplacement, 0);
                result.labelY = renderer;
            }
            {
                var renderer = AxisLabelRenderer.Create(1);
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
            this.seat2.Render(arg);
            this.body.Render(arg);
            this.labelX.Render(arg);
            this.labelY.Render(arg);
            this.labelZ.Render(arg);
        }

        public void SetMVP(mat4 projection, mat4 view, mat4 model)
        {
            this.seat.SetUniform("mvp", projection * view * model);
            this.seat2.SetUniform("mvp", projection * view * model);
            this.body.SetUniform("mvp", projection * view * model);
            this.labelX.SetUniform("projection", projection);
            this.labelX.SetUniform("view", view * model);
            this.labelY.SetUniform("projection", projection);
            this.labelY.SetUniform("view", view * model);
            this.labelZ.SetUniform("projection", projection);
            this.labelZ.SetUniform("view", view * model);
        }

        protected override void DoInitialize()
        {
            this.seat.Initialize();
            this.seat2.Initialize();
            this.body.Initialize();
            this.labelX.Initialize();
            this.labelY.Initialize();
            this.labelZ.Initialize();
        }
    }
}
