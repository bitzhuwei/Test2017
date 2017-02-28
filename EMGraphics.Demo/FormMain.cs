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
    public partial class FormMain : Form
    {
        private Camera camera;
        private SatelliteManipulater rotator;
        private Scene scene;

        public FormMain()
        {
            InitializeComponent();

            this.CurrentSelectingType = SelectingType.Triangle;
            this.HighlightColor = Color.Yellow;

            this.Load += this.FormMain_Load;
            this.glCanvas1.OpenGLDraw += this.glCanvas1_OpenGLDraw;
            this.glCanvas1.KeyPress += this.glCanvas1_KeyPress;
            this.glCanvas1.MouseDown += glCanvas1_MouseDown;

            Application.Idle += Application_Idle;
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            this.Text = string.Format("EMGraphics DEMO - FPS: {0}", this.glCanvas1.FPS.ToShortString());
        }

        private void glCanvas1_OpenGLDraw(object sender, PaintEventArgs e)
        {
            this.scene.Render();
        }

    }
}
