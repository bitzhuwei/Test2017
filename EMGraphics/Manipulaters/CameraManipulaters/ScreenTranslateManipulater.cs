using System;
using System.Drawing;

using System.Windows.Forms;

namespace EMGraphics
{
    /// <summary>
    /// Rotates a camera on a sphere, whose center is camera's Target.
    /// <para>Just like a satellite moves around a fixed star.</para>
    /// </summary>
    public class ScreenTranslateManipulater : Manipulater, IMouseHandler
    {
        private vec3 back;
        private ICamera camera;
        private ICanvas canvas;

        private MouseButtons lastBindingMouseButtons;
        private Point lastPosition = new Point();
        private bool mouseDownFlag = false;
        private MouseEventHandler mouseDownEvent;
        private MouseEventHandler mouseMoveEvent;
        private MouseEventHandler mouseUpEvent;
        private vec3 right;
        private vec3 up;

        /// <summary>
        ///
        /// </summary>
        public ScreenTranslateManipulater(MouseButtons bindingMouseButtons = MouseButtons.Middle)
        {
            this.HorizontalTranslateFactor = 0.001f;
            this.VerticalTranslateFactor = 0.001f;
            this.BindingMouseButtons = bindingMouseButtons;
            this.mouseDownEvent = new MouseEventHandler(((IMouseHandler)this).canvas_MouseDown);
            this.mouseMoveEvent = new MouseEventHandler(((IMouseHandler)this).canvas_MouseMove);
            this.mouseUpEvent = new MouseEventHandler(((IMouseHandler)this).canvas_MouseUp);
        }

        /// <summary>
        ///
        /// </summary>
        public MouseButtons BindingMouseButtons { get; set; }

        /// <summary>
        ///
        /// </summary>
        public float HorizontalTranslateFactor { get; set; }

        /// <summary>
        ///
        /// </summary>
        public float VerticalTranslateFactor { get; set; }

        /// <summary>
        ///
        /// </summary>
        public override void Bind(ICamera camera, ICanvas canvas)
        {
            if (camera == null || canvas == null) { throw new ArgumentNullException(); }

            this.camera = camera;
            this.canvas = canvas;

            canvas.MouseDown += this.mouseDownEvent;
            canvas.MouseMove += this.mouseMoveEvent;
            canvas.MouseUp += this.mouseUpEvent;
        }

        void IMouseHandler.canvas_MouseDown(object sender, MouseEventArgs e)
        {
            this.lastBindingMouseButtons = this.BindingMouseButtons;
            if ((e.Button & this.lastBindingMouseButtons) != MouseButtons.None)
            {
                this.lastPosition = e.Location;
                this.mouseDownFlag = true;
            }
        }

        void IMouseHandler.canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.mouseDownFlag
                && ((e.Button & this.lastBindingMouseButtons) != MouseButtons.None))
            {
                IOrthoCamera camera = this.camera;
                if (camera == null) { return; }

                Point newPosition = e.Location;
                int diffX = newPosition.X - this.lastPosition.X;
                int diffY = newPosition.Y - this.lastPosition.Y;
                int canvasWidth = this.canvas.ClientRectangle.Width;
                int canvasHeight = this.canvas.ClientRectangle.Height;
                double widthDiffPercent = (double)diffX / (double)canvasWidth;
                double heightDiffPercent = (double)diffY / (double)canvasHeight;
                double cameraWidth = camera.Right - camera.Left;
                double cameraHeibht = camera.Top - camera.Bottom;
                double moveX = cameraWidth * widthDiffPercent;
                double moveY = cameraHeibht * heightDiffPercent;
                camera.Left -= moveX; camera.Right -= moveX;
                camera.Bottom += moveY; camera.Top += moveY;

                this.lastPosition = newPosition;

                if (this.canvas.RenderTrigger == RenderTrigger.Manual)
                { this.canvas.Repaint(); }
            }
        }

        void IMouseHandler.canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button & this.lastBindingMouseButtons) != MouseButtons.None)
            {
                this.mouseDownFlag = false;
            }
        }

        void IMouseHandler.canvas_MouseWheel(object sender, MouseEventArgs e)
        {
            // nothing to do.
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("back:{0}|{3:0.00},up:{1}|{4:0.00},right:{2}|{5:0.00}",
                back, up, right, back.length(), up.length(), right.length());
        }

        /// <summary>
        ///
        /// </summary>
        public override void Unbind()
        {
            if (this.canvas != null && (!this.canvas.IsDisposed))
            {
                this.canvas.MouseDown -= this.mouseDownEvent;
                this.canvas.MouseMove -= this.mouseMoveEvent;
                this.canvas.MouseUp -= this.mouseUpEvent;
                this.canvas = null;
                this.camera = null;
            }
        }

    }
}