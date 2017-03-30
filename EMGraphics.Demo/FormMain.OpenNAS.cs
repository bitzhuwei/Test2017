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
        private SceneObject wholeObject;
        private SceneObject boxObject;
        private SceneObject wholeNormal;
        private SceneObject notPickedGroup;
        private SceneObject pickedGroup;

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openNASFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.scene.RootObject.Children.Clear();

                NASFile file = NASFile.Load(this.openNASFile.FileName);
                IEMDataSource dataSource = file.Parse();
                BoundingBox box = dataSource.Box;
                vec3 center = box.MaxPosition / 2.0f + box.MinPosition / 2.0f;

                // 将整个nas模型作为一个单独的模型
                // get nas model as a single model
                {
                    SceneObject wholeObject = GetWholeObject(
                        dataSource.VertexPositions, dataSource.VertexNormals,
                        dataSource.Triangles, dataSource.Box, center);
                    this.wholeObject = wholeObject;
                    this.scene.RootObject.Children.Add(wholeObject);
                }
                {
                    BoundingBoxRenderer renderer = BoundingBoxRenderer.Create(
                        (box.MaxPosition - box.MinPosition) * 1.1f);
                    //renderer.WorldPosition = center;
                    renderer.BoundingBoxColor = Color.FromArgb(255, 211, 211, 211);
                    renderer.Scale = new vec3(1, 1, 0);
                    SceneObject boxObj = renderer.WrapToSceneObject(generateBoundingBox: false);
                    this.scene.RootObject.Children.Add(boxObj);
                    this.boxObject = boxObj;
                }
                {
                    SceneObject wholeNormal = GetWholeNormal(
                        dataSource.FaceNormalPositions,
                        dataSource.FaceNormalDirections,
                        dataSource.FaceNormalLengths,
                        dataSource.Triangles,
                        center);
                    this.scene.RootObject.Children.Add(wholeNormal);
                    this.wholeNormal = wholeNormal;
                }

                // 分别加载nas模型里的各个face，这是为了实现拾取face功能
                // get parted grids(faces) for fast picking 
                {
                    IList<EMGrid> gridList = dataSource.GridList;
                    SceneObject notPickedGroup = GetPartedGrids(gridList, center);
                    notPickedGroup.RenderingEnabled = false;
                    notPickedGroup.PickingEnabled = false;
                    this.scene.RootObject.Children.Add(notPickedGroup);
                    this.notPickedGroup = notPickedGroup;
                    var pickedGroup = new SceneObject(); pickedGroup.Name = string.Format("Picked Group.");
                    //pickedGroup.RenderingEnabled = true;
                    pickedGroup.PickingEnabled = false;
                    this.scene.RootObject.Children.Add(pickedGroup);
                    this.pickedGroup = pickedGroup;
                }

                // pick a mesh for the first time which runs very slow.
                {
                    this.UpdatePickingState(SelectingType.Mesh);
                    this.scene.Pick(Point.Empty, PickingGeometryType.Triangle);
                }
                {
                    this.UpdatePickingState(this.CurrentSelectingType);
                }
                {
                    vec3 size = box.MaxPosition - box.MinPosition;
                    float max = size.x;
                    if (max < size.y) { max = size.y; }
                    if (max < size.z) { max = size.z; }
                    // center axis 
                    // NOTE: this renderer must be the last one!
                    {
                        var model = new CenterAxis(max);
                        CenterAxisRenderer renderer = CenterAxisRenderer.Create(model);
                        SceneObject obj = renderer.WrapToSceneObject(generateBoundingBox: false);
                        this.scene.RootObject.Children.Add(obj);
                    }

                    const float deltaDistance = 0.05f;
                    {
                        var renderer = LabelRenderer.Create(1, 32);
                        renderer.Text = "X";
                        renderer.TextColor = Color.Blue;
                        renderer.KeepFront = true;
                        renderer.WorldPosition = new vec3(max / 2.0f + deltaDistance, 0, 0);
                        SceneObject obj = renderer.WrapToSceneObject(generateBoundingBox: false);
                        this.scene.RootObject.Children.Add(obj);
                    }
                    {
                        var renderer = LabelRenderer.Create(1, 32);
                        renderer.Text = "Y";
                        renderer.TextColor = Color.Green;
                        renderer.KeepFront = true;
                        renderer.WorldPosition = new vec3(0, max / 2.0f + deltaDistance, 0);
                        SceneObject obj = renderer.WrapToSceneObject(generateBoundingBox: false);
                        this.scene.RootObject.Children.Add(obj);
                    }
                    {
                        var renderer = LabelRenderer.Create(1, 32);
                        renderer.Text = "Z";
                        renderer.TextColor = Color.Red;
                        renderer.KeepFront = true;
                        renderer.WorldPosition = new vec3(0, 0, max / 2.0f + deltaDistance);
                        SceneObject obj = renderer.WrapToSceneObject(generateBoundingBox: false);
                        this.scene.RootObject.Children.Add(obj);
                    }

                }
                {
                    this.camera.ZoomCamera(box);
                }

                this.glCanvas1.Repaint();
            }
        }

        private SceneObject GetWholeNormal(
            vec3[] faceNormalPositions,
            vec3[] faceNormalDirections,
            float[] faceNormalLengths,
            Triangle[] triangles,
            vec3 center)
        {
            var model = new FaceNormal(faceNormalPositions, faceNormalDirections, faceNormalLengths, triangles);
            var renderer = FaceNormalRenderer.Create(model);
            renderer.WorldPosition += center;
            SceneObject obj = renderer.WrapToSceneObject("Whole Normals", generateBoundingBox: false);
            //obj.RenderingEnabled = false;
            obj.PickingEnabled = false;
            return obj;
        }

        private SceneObject GetWholeObject(
            vec3[] positions, vec3[] normals, Triangle[] triangles, BoundingBox box, vec3 center)
        {
            var grid = new EMGrid(positions, normals, triangles, box, "Whole Model");
            var renderer = EMGridRenderer.Create(grid);
            renderer.WorldPosition = center;
            //renderer.Initialize();
            SceneObject obj = renderer.WrapToSceneObject("Whole Model", generateBoundingBox: false);
            return obj;
        }

        private static SceneObject GetPartedGrids(IList<EMGrid> gridList, vec3 center)
        {
            var notPickedGroup = new SceneObject();
            notPickedGroup.Name = string.Format("Not Picked Group.");

            for (int i = 0; i < gridList.Count; i++)
            {
                EMGrid grid = gridList[i];
                EMGridRenderer renderer = EMGridRenderer.Create(grid);
                renderer.WorldPosition = center;
                //renderer.Initialize();
                SceneObject obj = renderer.WrapToSceneObject(string.Format(
                    "Mesh [{0}/{1}]", i + 1, gridList.Count), generateBoundingBox: false);
                notPickedGroup.Children.Add(obj);
            }

            return notPickedGroup;
        }

    }
}
