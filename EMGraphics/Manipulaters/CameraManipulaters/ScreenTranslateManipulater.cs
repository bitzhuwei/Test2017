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
        private Size bound = new Size();
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
            this.HorizontalRotationFactor = 0.001f;
            this.VerticalRotationFactor = 0.001f;
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
        public float HorizontalRotationFactor { get; set; }

        /// <summary>
        ///
        /// </summary>
        public float VerticalRotationFactor { get; set; }

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
                IViewCamera camera = this.camera;
                if (camera == null) { return; }

                Point newPosition = e.Location;
                vec3 right = this.camera.GetRight();
                camera.Position -= right * (newPosition.X - this.lastPosition.X) * this.HorizontalRotationFactor;
                camera.Target -= right * (newPosition.X - this.lastPosition.X) * this.HorizontalRotationFactor;
                vec3 down = this.camera.GetDown();
                camera.Position -= down * (newPosition.Y - this.lastPosition.Y) * this.VerticalRotationFactor;
                camera.Target -= down * (newPosition.Y - this.lastPosition.Y) * this.VerticalRotationFactor;
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