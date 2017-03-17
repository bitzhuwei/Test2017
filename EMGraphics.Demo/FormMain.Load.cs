﻿using System;
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
        void FormMain_Load(object sender, EventArgs e)
        {
            {
                var camera = new Camera(
                    new vec3(5, 4, 3) * 0.5f, new vec3(0, 0, 0), new vec3(0, 0, 1),
                    CameraType.Ortho, this.glCanvas1.Width, this.glCanvas1.Height);
                var rotator = new SatelliteManipulater(System.Windows.Forms.MouseButtons.Left);
                rotator.Bind(camera, this.glCanvas1);
                var screenTranslate = new ScreenTranslateManipulater(System.Windows.Forms.MouseButtons.Middle);
                screenTranslate.Bind(camera, this.glCanvas1);
                this.camera = camera;
                this.rotator = rotator;
                this.scene = new Scene(camera, this.glCanvas1);
                this.glCanvas1.Resize += this.scene.Resize;
                this.scene.RootViewPort.Children[0].Content.ClearColor = Color.White;
            }
            {
                var uiAxis = new UIAxis(AnchorStyles.Left | AnchorStyles.Bottom,
                    new Padding(3, 3, 3, 3), new Size(128, 128));
                this.scene.RootUI.Children.Add(uiAxis);
            }
			{
				var uiCodedColorBar = new UIColorPaletteRenderer(20, CodedColor.GetDefault(),
					AnchorStyles.Left | AnchorStyles.Top,
					new Padding(10, 10, 10, 10), new Size(100, 100),-1000,1000);
				//uiCodedColorBar.SetCodedColor(0, 100, 10);
				uiCodedColorBar.StateList.Add(new ClearColorState(Color.Blue));
				this.scene.RootUI.Children.Add(uiCodedColorBar);
			}
            //{
            //    var builder = new StringBuilder();
            //    builder.AppendLine("O: to select image.");
            //    builder.AppendLine("1: Scene's property grid.");
            //    builder.AppendLine("2: Canvas' property grid.");
            //    MessageBox.Show(builder.ToString());
            //}

            {
                this.btnPickTriangle_Click(this.btnPickTriangle, e);
            }
        }
    }
}
