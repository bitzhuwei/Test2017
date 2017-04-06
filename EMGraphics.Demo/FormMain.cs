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
            this.glCanvas1.MouseMove += GlCanvas1_MouseMove;
            this.glCanvas1.MouseUp += GlCanvas1_MouseUp;
            this.glCanvas1.MouseWheel += GlCanvas1_MouseWheel;

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


        private void btnEMGridRendererProperties_Click(object sender, EventArgs e)
        {
            (new FormEMGridRenderer(this.scene)).Show();
        }

        private void reverseSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SceneObject obj = this.wholeNormal;
            if (obj != null)
            {
                var renderer = obj.Renderer as FaceNormalRenderer;
                if (renderer != null)
                {
                    foreach (PickedGeometry geometry in this.CurrentPickedGeometrys)
                    {
                        var gridRenderer = geometry.FromRenderer as EMGridRenderer;
                        if (gridRenderer != null)
                        {
                            renderer.ReverseNormals(gridRenderer.Label);
                        }
                    }
                }
            }
        }

        private void reverseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SceneObject obj = this.wholeNormal;
            if (obj != null)
            {
                var renderer = obj.Renderer as FaceNormalRenderer;
                if (renderer != null)
                {
                    renderer.ReverseAllNormals();
                }
            }

        }

    }
}
