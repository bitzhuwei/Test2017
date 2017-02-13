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

            this.Load += FormMain_Load;
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            this.Text = string.Format("{0} - FPS: {1}", this.GetType().Name, this.glCanvas1.FPS.ToShortString());
        }

        void FormMain_Load(object sender, EventArgs e)
        {
            {
                var camera = new Camera(
                    new vec3(5, 4, 3) * 0.5f, new vec3(0, 0, 0), new vec3(0, 1, 0),
                    CameraType.Ortho, this.glCanvas1.Width, this.glCanvas1.Height);
                var rotator = new SatelliteManipulater(System.Windows.Forms.MouseButtons.Left);
                rotator.Bind(camera, this.glCanvas1);
                var screenTranslate = new ScreenTranslateManipulater(System.Windows.Forms.MouseButtons.Middle);
                screenTranslate.Bind(camera, this.glCanvas1);
                this.camera = camera;
                this.rotator = rotator;
                this.scene = new Scene(camera, this.glCanvas1);
                this.glCanvas1.Resize += this.scene.Resize;
                //this.scene.RootViewPort.Children[0].Content.ClearColor = Color.White;
            }
            {
                打开OToolStripMenuItem_Click(sender, e);
            }
            {
                var uiAxis = new UIAxis(AnchorStyles.Left | AnchorStyles.Bottom,
                    new Padding(3, 3, 3, 3), new Size(128, 128));
                this.scene.RootUI.Children.Add(uiAxis);
            }
            //{
            //    var builder = new StringBuilder();
            //    builder.AppendLine("O: to select image.");
            //    builder.AppendLine("1: Scene's property grid.");
            //    builder.AppendLine("2: Canvas' property grid.");
            //    MessageBox.Show(builder.ToString());
            //}
        }

        private vec3[] demoPositions = new vec3[]
        {
            new vec3((float)1.000000000E+00,(float)0.000000000E+00,(float)0.000000000E+00),
            new vec3((float)1.000000000E+00,(float)1.000000000E+00,(float)0.000000000E+00),
            new vec3((float)1.000000000E+00,(float)1.000000000E+00,(float)1.000000000E+00),
            new vec3((float)1.000000000E+00,(float)0.000000000E+00,(float)1.000000000E+00),
            new vec3((float)0.000000000E+00,(float)0.000000000E+00,(float)0.000000000E+00),
            new vec3((float)0.000000000E+00,(float)1.000000000E+00,(float)0.000000000E+00),
            new vec3((float)0.000000000E+00,(float)1.000000000E+00,(float)1.000000000E+00),
            new vec3((float)0.000000000E+00,(float)0.000000000E+00,(float)1.000000000E+00),
        };

        private vec3[] demoColors = new vec3[]
        {
            Color.YellowGreen.ToVec3(),
            Color.YellowGreen.ToVec3(),
            Color.YellowGreen.ToVec3(),
            Color.YellowGreen.ToVec3(),
            Color.YellowGreen.ToVec3(),
            Color.YellowGreen.ToVec3(),
            Color.YellowGreen.ToVec3(),
            Color.YellowGreen.ToVec3(),
        };

        private EMGraphics.Triangle[] demoTriangles = new EMGraphics.Triangle[]
        {
            new EMGraphics.Triangle(1 - 1, 6 - 1, 2 - 1),
            new EMGraphics.Triangle(6 - 1, 1 - 1, 5 - 1),
            new EMGraphics.Triangle(2 - 1, 6 - 1, 7 - 1),
            new EMGraphics.Triangle(2 - 1, 7 - 1, 3 - 1),
            new EMGraphics.Triangle(4 - 1, 1 - 1, 2 - 1),
            new EMGraphics.Triangle(4 - 1, 2 - 1, 3 - 1),
            new EMGraphics.Triangle(4 - 1, 7 - 1, 8 - 1),
            new EMGraphics.Triangle(7 - 1, 4 - 1, 3 - 1),
            new EMGraphics.Triangle(5 - 1, 1 - 1, 4 - 1),
            new EMGraphics.Triangle(5 - 1, 4 - 1, 8 - 1),
            new EMGraphics.Triangle(6 - 1, 5 - 1, 8 - 1),
            new EMGraphics.Triangle(6 - 1, 8 - 1, 7 - 1),
        };

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestModel model = GetModel();
            if (model != null)
            {
                var renderer = EMGraphics.TestRenderer.Create(model);
                SceneObject obj = renderer.WrapToSceneObject(generateBoundingBox: true);
                this.scene.RootObject.Children.Add(obj);
                this.glCanvas1.Repaint();
            }
        }

        private TestModel GetModel()
        {
            //vec3[] positions = null;//vec3.x .y .z分别表示顶点位置的x y z坐标。
            //vec3[] colors = null;//vec3.x .y .z分别表示颜色分量的R G B值，范围为[0, 1]
            //Triangle[] triangles = null;//Triangle记录了positions数组里的哪三个顶点组成1个三角形。
            vec3[] positions = demoPositions;
            vec3[] colors = demoColors;
            Triangle[] triangles = demoTriangles;
            vec3[] normals = positions.CalculateNormals(triangles);
            var model = new TestModel(positions, colors, normals, triangles);
            return model;
        }

        private void glCanvas1_OpenGLDraw(object sender, PaintEventArgs e)
        {
            this.scene.Render();
        }
    }
}
