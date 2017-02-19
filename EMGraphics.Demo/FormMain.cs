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
                this.scene.RootViewPort.Children[0].Content.ClearColor = Color.White;
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
            Color.LightYellow.ToVec3(),
            Color.Yellow.ToVec3(),
            Color.LightGoldenrodYellow.ToVec3(),
            Color.GreenYellow.ToVec3(),
            Color.LightGreen.ToVec3(),
            Color.DarkSeaGreen.ToVec3(),
            Color.DarkSeaGreen.ToVec3(),
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
            BoundingBox box = demoPositions.Move2Center();
            vec3 center = box.MaxPosition / 2.0f + box.MinPosition / 2.0f;
            vec3 size = box.MaxPosition - box.MinPosition;
            {
                EMModel model = GetEMModel(demoPositions, demoColors, demoTriangles);
                if (model != null)
                {
                    {
                        var renderer = EMGraphics.EMRenderer.Create(model);
                        renderer.WorldPosition = center;
                        renderer.ModelSize = size;
                        SceneObject obj = renderer.WrapToSceneObject(generateBoundingBox: true);
                        this.scene.RootObject.Children.Add(obj);

                        (new FormProperyGrid(renderer)).Show();
                    }
                }
            }
            {
                NormalLineModel model = GetNormalLineModel(demoPositions, demoTriangles);
                if (model != null)
                {
                    var renderer = EMGraphics.NormalLineRenderer.Create(model);
                    renderer.WorldPosition = center;
                    renderer.ModelSize = size;
                    SceneObject obj = renderer.WrapToSceneObject(generateBoundingBox: false);
                    this.scene.RootObject.Children.Add(obj);

                    (new FormProperyGrid(renderer)).Show();
                }

                this.glCanvas1.Repaint();
            }
        }

        /// <summary>
        /// 获取三角形面的法线。
        /// </summary>
        /// <param name="positions">vec3.x .y .z分别表示顶点位置的x y z坐标。</param>
        /// <param name="colors">vec3.x .y .z分别表示颜色分量的R G B值，范围为[0, 1]</param>
        /// <param name="triangles">Triangle记录了positions数组里的哪三个顶点组成1个三角形。</param>
        /// <returns></returns>
        private NormalLineModel GetNormalLineModel(vec3[] positions, Triangle[] triangles)
        {
            var normalPositions = new vec3[triangles.Length];
            var normalDirections = new vec3[triangles.Length];
            var normalLengths = new float[triangles.Length];

            for (int i = 0; i < triangles.Length; i++)
            {
                vec3 vertex1 = positions[triangles[i].Num1];
                vec3 vertex2 = positions[triangles[i].Num2];
                vec3 vertex3 = positions[triangles[i].Num3];

                vec3 position = vertex1 / 3.0f + vertex2 / 3.0f + vertex3 / 3.0f;
                normalPositions[i] = position;

                vec3 v12 = vertex2 - vertex1;
                vec3 v23 = vertex3 - vertex2;
                //normalDirections[i] = v23.cross(v12).normalize();
                normalDirections[i] = v12.cross(v23).normalize();

                float length = vertex1.length();
                float tmp = vertex2.length(); if (tmp > length) { length = tmp; }
                tmp = vertex3.length(); if (tmp > length) { length = tmp; }
                normalLengths[i] = length;
            }

            var model = new NormalLineModel(normalPositions, normalDirections, normalLengths);
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positions">vec3.x .y .z分别表示顶点位置的x y z坐标。</param>
        /// <param name="colors">vec3.x .y .z分别表示颜色分量的R G B值，范围为[0, 1]</param>
        /// <param name="triangles">Triangle记录了positions数组里的哪三个顶点组成1个三角形。</param>
        /// <returns></returns>
        private EMModel GetEMModel(vec3[] positions, vec3[] colors, Triangle[] triangles)
        {
            vec3[] normals = positions.CalculateNormals(triangles);
            var model = new EMModel(positions, colors, normals, triangles);
            return model;
        }

        private void glCanvas1_OpenGLDraw(object sender, PaintEventArgs e)
        {
            this.scene.Render();
        }
    }
}
