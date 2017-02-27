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

                NASFile file = NASFile.Load(openFileDlg.FileName);
                vec3[] positions = file.Points.ToArray();
                vec3[] colors = new vec3[positions.Length];
                // temp solution for color:
                for (int i = 0; i < colors.Length; i++)
                {
                    colors[i] = Color.Orange.ToVec3();
                }
                BoundingBox box = positions.Move2Center();
                vec3 center = box.MaxPosition / 2.0f + box.MinPosition / 2.0f;
                vec3 size = box.MaxPosition - box.MinPosition;
                {
                    vec3[] normals = CalculateNormals(positions);
                    var model = new EMModel(positions, colors, normals);
                    var renderer = EMGraphics.EMRenderer.Create(model);
                    renderer.WorldPosition = center;
                    renderer.ModelSize = size;
                    SceneObject obj = renderer.WrapToSceneObject(generateBoundingBox: true);
                    this.scene.RootObject.Children.Add(obj);

                    (new FormProperyGrid(renderer)).Show();
                }
                {
                    // generate and display faces' normal lines.
                    NormalLineModel model = GetFaceNormalLineModel(positions);
                    var renderer = EMGraphics.NormalLineRenderer.Create(model);
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
                    renderer.WorldPosition = center;
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

        /// <summary>
        /// 获取三角形面的法线。
        /// </summary>
        /// <param name="positions">vec3.x .y .z分别表示顶点位置的x y z坐标。</param>
        /// <param name="colors">vec3.x .y .z分别表示颜色分量的R G B值，范围为[0, 1]</param>
        /// <param name="triangles">Triangle记录了positions数组里的哪三个顶点组成1个三角形。</param>
        /// <returns></returns>
        private NormalLineModel GetFaceNormalLineModel(vec3[] positions)
        {
            var normalPositions = new vec3[positions.Length];
            var normalDirections = new vec3[positions.Length];
            var normalLengths = new float[positions.Length];

            for (int i = 0; i < positions.Length / 3; i++)
            {
                vec3 vertex1 = positions[i * 3 + 0];
                vec3 vertex2 = positions[i * 3 + 1];
                vec3 vertex3 = positions[i * 3 + 2];

                vec3 position = vertex1 / 3.0f + vertex2 / 3.0f + vertex3 / 3.0f;
                normalPositions[i] = position;

                vec3 v12 = vertex2 - vertex1;
                vec3 v23 = vertex3 - vertex2;
                //normalDirections[i] = v23.cross(v12).normalize();
                normalDirections[i] = v12.cross(v23).normalize();

                vec3 v31 = vertex1 - vertex3;
                float length = v12.length();
                float tmp = v23.length(); if (length < tmp) { length = tmp; }
                tmp = v31.length(); if (length < tmp) { length = tmp; }
                normalLengths[i * 3 + 0] = length;
                normalLengths[i * 3 + 1] = length;
                normalLengths[i * 3 + 2] = length;
            }

            var model = new NormalLineModel(normalPositions, normalDirections, normalLengths);
            return model;
        }

        /// <summary>
        /// 为(positions.Length / 3)个三角形计算各自顶点的法线。
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        private vec3[] CalculateNormals(vec3[] positions)
        {
            var normals = new vec3[positions.Length];
            for (int i = 0; i < normals.Length / 3; i++)
            {
                vec3 v10 = positions[i * 3 + 1] - positions[i * 3 + 0];
                vec3 v21 = positions[i * 3 + 2] - positions[i * 3 + 1];
                vec3 normal = v21.cross(v10).normalize();
                normals[i * 3 + 0] = normal;
                normals[i * 3 + 1] = normal;
                normals[i * 3 + 2] = normal;
            }

            return normals;
        }
    }
}
