using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EMGraphics;

namespace EMGraphics.Demo
{
    public partial class FormMain
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GlCanvas1_MouseWheel(object sender, MouseEventArgs e)
        {
            ICanvas canvas = sender as ICanvas;
            Point mousePosition = e.Location;
            Size size = canvas.ClientRectangle.Size;
            float leftPercent = (float)(mousePosition.X - 0) / (float)size.Width;
            float topPercent = (float)(mousePosition.Y - 0) / (float)size.Height;
            double factor = 0.001 * e.Delta;
            IOrthoViewCamera camera = this.camera;
            double left = camera.Left;
            double right = camera.Right;
            double bottom = camera.Bottom;
            double top = camera.Top;
            double width = right - left;
            double height = top - bottom;
            camera.Left = left + width * leftPercent * factor;
            camera.Right = right - width * (1 - leftPercent) * factor;
            camera.Bottom = bottom + height * (1 - topPercent) * factor;
            camera.Top = top - height * topPercent * factor;

            //ScaleObject(e);
        }

        private void ScaleObject(MouseEventArgs e)
        {
            float factor = 1000.0f;
            {
                SceneObject obj = this.wholeObject;
                if (obj != null)
                {
                    RendererBase renderer = obj.Renderer;
                    if (renderer != null)
                    {
                        renderer.Scale = renderer.Scale * (1.0f + e.Delta / factor);
                    }
                }
            }
            {
                SceneObject obj = this.boxObject;
                if (obj != null)
                {
                    RendererBase renderer = obj.Renderer;
                    if (renderer != null)
                    {
                        renderer.Scale = renderer.Scale * (1.0f + e.Delta / factor);
                    }
                }
            }

            {
                SceneObject obj = this.wholeNormal;
                if (obj != null)
                {
                    RendererBase renderer = obj.Renderer;
                    if (renderer != null)
                    {
                        renderer.Scale = renderer.Scale * (1.0f + e.Delta / factor);
                    }
                }
            }
            {
                SceneObject obj = this.pickedGroup;
                if (obj != null)
                {
                    foreach (var child in obj.Children)
                    {
                        if (child != null)
                        {
                            var renderer = child.Content.Renderer;
                            if (renderer != null)
                            {
                                renderer.Scale = renderer.Scale * (1.0f + e.Delta / factor);
                            }
                        }
                    }
                }
            }
            {
                SceneObject obj = this.notPickedGroup;
                if (obj != null)
                {
                    foreach (var child in obj.Children)
                    {
                        if (child != null)
                        {
                            var renderer = child.Content.Renderer;
                            if (renderer != null)
                            {
                                renderer.Scale = renderer.Scale * (1.0f + e.Delta / factor);
                            }
                        }
                    }
                }
            }
        }

    }
}
