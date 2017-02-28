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
        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.scene.RootObject.Children.Clear();

                EMGrid grid; NormalLineModel normalLineModel; BoundingBox box;
                NASFile file = NASFile.Load(openFileDlg.FileName);
                file.Parse(out grid, out normalLineModel, out box);
                vec3 center = box.MaxPosition / 2.0f + box.MinPosition / 2.0f;
                vec3 size = box.MaxPosition - box.MinPosition;
                {
                    var renderer = EMGraphics.EMGridRenderer.Create(grid);
                    renderer.WorldPosition = center;
                    renderer.ModelSize = size;
                    SceneObject obj = renderer.WrapToSceneObject(generateBoundingBox: true);
                    this.scene.RootObject.Children.Add(obj);

                    (new FormProperyGrid(renderer)).Show();
                }
                {
                    // generate and display faces' normal lines.
                    var renderer = EMGraphics.NormalLineRenderer.Create(normalLineModel);
                    renderer.WorldPosition = center;
                    renderer.ModelSize = size;
                    SceneObject obj = renderer.WrapToSceneObject(generateBoundingBox: false);
                    this.scene.RootObject.Children.Add(obj);

                    renderer.Enabled = false;

                    (new FormProperyGrid(renderer)).Show();
                }
                //{
                //    // generate and display vertexes' normal lines.
                //    vec3[] normals = positions.CalculateNormals(triangles);
                //    float[] lengths = new float[positions.Length];
                //    for (int i = 0; i < lengths.Length; i++)
                //    {
                //        lengths[i] = 0.5f;
                //    }
                //    var model = new NormalLineModel(positions, normals, lengths);
                //    var renderer = EMGraphics.NormalLineRenderer.Create(model);
                //    renderer.HeadColor.Value = Color.Blue;
                //    renderer.TailColor.Value = Color.Purple;
                //    renderer.WorldPosition = center;
                //    renderer.ModelSize = size;
                //    SceneObject obj = renderer.WrapToSceneObject(generateBoundingBox: false);
                //    this.scene.RootObject.Children.Add(obj);

                //    (new FormProperyGrid(renderer)).Show();
                //}
                {
                    // center axis 
                    // NOTE: this renderer must be the last one!
                    float max = size.x;
                    if (max < size.y) { max = size.y; }
                    if (max < size.z) { max = size.z; }
                    var model = new CenterAxisModel(max);
                    CenterAxisRenderer renderer = CenterAxisRenderer.Create(model);
                    //renderer.WorldPosition = center;
                    renderer.ModelSize = size;
                    SceneObject obj = renderer.WrapToSceneObject(generateBoundingBox: false);
                    this.scene.RootObject.Children.Add(obj);
                }
                {
                    this.camera.ZoomCamera(box);
                }

                this.glCanvas1.Repaint();
            }
        }

    }
}
