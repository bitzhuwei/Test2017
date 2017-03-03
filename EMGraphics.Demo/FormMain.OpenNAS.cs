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

                List<EMGrid> gridList; List<NormalLineModel> normalLineModelList; BoundingBox box;
                NASFile file = NASFile.Load(openFileDlg.FileName);
                file.Parse(out gridList, out normalLineModelList, out box);
                vec3 center = box.MaxPosition / 2.0f + box.MinPosition / 2.0f;
                vec3 size = box.MaxPosition - box.MinPosition;

                SceneObject wholeObject = GetWholeObject(file.VertexPositions, file.VertexNormals, file.Triangles, center, size);
                this.scene.RootObject.Children.Add(wholeObject);

                {
                    SceneObject notPickedGroup = GetNotPickedGroup(gridList, normalLineModelList, center, size);
                    notPickedGroup.RenderingEnabled = false;
                    notPickedGroup.PickingEnabled = false;
                    this.scene.RootObject.Children.Add(notPickedGroup);
                    var pickedGroup = new SceneObject(); pickedGroup.Name = string.Format("Picked Grids.");
                    pickedGroup.RenderingEnabled = false;
                    pickedGroup.PickingEnabled = false;
                    this.scene.RootObject.Children.Add(pickedGroup);
                }

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

        private SceneObject GetWholeObject(vec3[] positions, vec3[] normals, Triangle[] triangles, vec3 center, vec3 size)
        {
            var grid = new EMGrid(positions, normals, triangles, "Whole Model");
            var renderer = EMGridRenderer.Create(grid);
            renderer.WorldPosition += center;
            renderer.ModelSize = size;
            SceneObject obj = renderer.WrapToSceneObject("Whole Model", generateBoundingBox: true);
            return obj;
        }

        private static SceneObject GetNotPickedGroup(List<EMGrid> gridList, List<NormalLineModel> normalLineModelList, vec3 center, vec3 size)
        {
            var gridObjects = new SceneObject[gridList.Count];
            for (int i = 0; i < gridList.Count; i++)
            {
                EMGrid grid = gridList[i];
                var renderer = EMGridRenderer.Create(grid);
                renderer.WorldPosition += center;
                renderer.ModelSize = size;
                gridObjects[i] = renderer.WrapToSceneObject(string.Format(
                    "Mesh [{0}/{1}]", i + 1, gridList.Count), generateBoundingBox: false);
            }
            // generate and display faces' normal lines.
            for (int i = 0; i < normalLineModelList.Count; i++)
            {
                NormalLineModel model = normalLineModelList[i];
                var renderer = NormalLineRenderer.Create(model);
                renderer.WorldPosition += center;
                renderer.ModelSize = size;
                SceneObject obj = renderer.WrapToSceneObject(string.Format(
                    "Face Normal Line of Mesh [{0}/{1}]", i + 1, normalLineModelList.Count), generateBoundingBox: false);
                obj.RenderingEnabled = false;
                gridObjects[i].Children.Add(obj);
            }

            var notPickedGroup = new SceneObject(); notPickedGroup.Name = string.Format("Not Picked Grids.");
            for (int i = 0; i < gridObjects.Length; i++) { notPickedGroup.Children.Add(gridObjects[i]); }
            return notPickedGroup;
        }

    }
}
