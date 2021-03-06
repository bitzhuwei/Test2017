﻿using System;
using System.Drawing;

using System.Windows.Forms;

namespace EMGraphics
{
    /// <summary>
    /// Rotates a camera on a sphere, whose center is camera's Target.
    /// <para>Just like a satellite moves around a fixed star.</para>
    /// </summary>
    public class SatelliteManipulater : Manipulater, IMouseHandler
    {
        private vec3 back;
        private Size bound = new Size();
        private ICamera camera;
        private ICanvas canvas;

        private MouseButtons lastBindingMouseButtons;
        private Point lastPosition = new Point();
        private MouseEventHandler mouseDownEvent;
        private bool mouseDownFlag = false;
        private MouseEventHandler mouseMoveEvent;
        private MouseEventHandler mouseUpEvent;
        private MouseEventHandler mouseWheelEvent;
        private vec3 right;
        private vec3 up;

		public bool MouseWheelEnabled { get; set; }

        /// <summary>
        ///
        /// </summary>
        public SatelliteManipulater(MouseButtons bindingMouseButtons = MouseButtons.Right)
        {
            this.HorizontalRotationFactor = 4;
            this.VerticalRotationFactor = 4;
            this.BindingMouseButtons = bindingMouseButtons;
            this.mouseDownEvent = new MouseEventHandler(((IMouseHandler)this).canvas_MouseDown);
            this.mouseMoveEvent = new MouseEventHandler(((IMouseHandler)this).canvas_MouseMove);
            this.mouseUpEvent = new MouseEventHandler(((IMouseHandler)this).canvas_MouseUp);
            this.mouseWheelEvent = new MouseEventHandler(((IMouseHandler)this).canvas_MouseWheel);

			this.MouseWheelEnabled = true;
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
            canvas.MouseWheel += this.mouseWheelEvent;
        }

        void IMouseHandler.canvas_MouseDown(object sender, MouseEventArgs e)
        {
            this.lastBindingMouseButtons = this.BindingMouseButtons;
            if ((e.Button & this.lastBindingMouseButtons) != MouseButtons.None)
            {
                this.lastPosition = e.Location;
                var control = sender as Control;
                this.SetBounds(control.Width, control.Height);
                this.mouseDownFlag = true;
                PrepareCamera();
            }
        }

        void IMouseHandler.canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.mouseDownFlag
                && ((e.Button & this.lastBindingMouseButtons) != MouseButtons.None)
                && (e.X != lastPosition.X || e.Y != lastPosition.Y))
            {
                IViewCamera camera = this.camera;
                if (camera == null) { return; }

                vec3 back = this.back;
                vec3 right = this.right;
                vec3 up = this.up;
                Size bound = this.bound;
                Point downPosition = this.lastPosition;
                {
                    float deltaX = -this.HorizontalRotationFactor * (e.X - downPosition.X) / bound.Width;
                    float cos = (float)Math.Cos(deltaX);
                    float sin = (float)Math.Sin(deltaX);
                    vec3 newBack = new vec3(
                        back.x * cos + right.x * sin,
                        back.y * cos + right.y * sin,
                        back.z * cos + right.z * sin);
                    back = newBack;
                    right = up.cross(back);
                    back = back.normalize();
                    right = right.normalize();
                }
                {
                    float deltaY = this.VerticalRotationFactor * (e.Y - downPosition.Y) / bound.Height;
                    float cos = (float)Math.Cos(deltaY);
                    float sin = (float)Math.Sin(deltaY);
                    vec3 newBack = new vec3(
                        back.x * cos + up.x * sin,
                        back.y * cos + up.y * sin,
                        back.z * cos + up.z * sin);
                    back = newBack;
                    up = back.cross(right);
                    back = back.normalize();
                    up = up.normalize();
                }

                camera.Position = camera.Target +
                    back * (float)((camera.Position - camera.Target).length());
                camera.UpVector = up;
                this.back = back;
                this.right = right;
                this.up = up;
                this.lastPosition = e.Location;

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
			if (!this.MouseWheelEnabled) { return; }

            this.camera.MouseWheel(e.Delta);

            if (this.canvas.RenderTrigger == RenderTrigger.Manual)
            { this.canvas.Repaint(); }
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
                this.canvas.MouseWheel -= this.mouseWheelEvent;
                this.canvas = null;
                this.camera = null;
            }
        }

        private void PrepareCamera()
        {
            var camera = this.camera;
            if (camera != null)
            {
                vec3 back = camera.Position - camera.Target;
                vec3 right = camera.UpVector.cross(back);
                vec3 up = back.cross(right);

                this.back = back.normalize();
                this.right = right.normalize();
                this.up = up.normalize();
            }
        }

        private void SetBounds(int width, int height)
        {
            this.bound.Width = width;
            this.bound.Height = height;
        }
    }
}