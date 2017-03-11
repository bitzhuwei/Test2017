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
		private SceneObject wholeNormal;
		private SceneObject notPickedGroup;
		private SceneObject pickedGroup;

		private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (openFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.scene.RootObject.Children.Clear();

				NASFile file = NASFile.Load(openFileDlg.FileName);
				IEMDataSource dataSource = file.Parse();
				BoundingBox box = dataSource.Box;
				vec3 center = box.MaxPosition / 2.0f + box.MinPosition / 2.0f;
				vec3 size = box.MaxPosition - box.MinPosition;

				// 将整个nas模型作为一个单独的模型
				// get nas model as a single model
				{
					//SceneObject wholeObject = GetWholeObject(file.VertexPositions, file.VertexNormals, file.Triangles, center, size);
					SceneObject wholeObject = GetWholeObject(
						dataSource.VertexPositions,dataSource.VertexNormals,
						dataSource.Triangles);
					this.scene.RootObject.Children.Add(wholeObject);
					this.wholeObject = wholeObject;
					//SceneObject wholeNormal = GetWholeNormal(
					//	dataSource.FaceNormalPositions,
					//	dataSource.FaceNormalDirections,
					//	dataSource.FaceNormalLengths);
					//this.scene.RootObject.Children.Add(wholeNormal);
					//this.wholeNormal = wholeNormal;
				}

				// 分别加载nas模型里的各个face，这是为了实现拾取face功能
				// get parted grids(faces) for fast picking 
				{
					IList<EMGrid> gridList = dataSource.GridList;
					IList<NormalLineModel> normalLineModelList = dataSource.NormalList;
					SceneObject notPickedGroup = GetPartedGrids(gridList, normalLineModelList);
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
					List<Tuple<Point, PickedGeometry>> allPickedGeometrys = this.scene.Pick(
						Point.Empty, PickingGeometryType.Triangle);
				}
				{
					this.UpdatePickingState(this.CurrentSelectingType);
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

		private SceneObject GetWholeNormal(
			vec3[] faceNormalPositions, 
			vec3[] faceNormalDirections, 
			float[] faceNormalLengths)
		{
			var model = new NormalLineModel(faceNormalPositions, faceNormalDirections, faceNormalLengths, "Whole Face Normal");
			var renderer = NormalLineRenderer.Create(model);
			SceneObject obj = renderer.WrapToSceneObject(generateBoundingBox: false);
			return obj;
		}

		private SceneObject GetWholeObject(
			vec3[] positions, vec3[] normals, Triangle[] triangles)
		{
			var grid = new EMGrid(positions, normals, triangles, "Whole Model");
			var renderer = EMGridRenderer.Create(grid);
			//renderer.Initialize();
			SceneObject obj = renderer.WrapToSceneObject("Whole Model", generateBoundingBox: true);
			return obj;
		}

		private static SceneObject GetPartedGrids(
			IList<EMGrid> gridList, IList<NormalLineModel> normalLineModelList)
		{
			var partedGridObjects = new SceneObject[gridList.Count];
			for (int i = 0; i < partedGridObjects.Length; i++)
			{
				var obj = new SceneObject();
				obj.Name = string.Format("Mesh & Face Normal [{0}/{1}]", i + 1, partedGridObjects.Length);
				partedGridObjects[i] = obj;
			}
			for (int i = 0; i < gridList.Count; i++)
			{
				EMGrid grid = gridList[i];
				EMGridRenderer renderer = EMGridRenderer.Create(grid);
				//renderer.Initialize();
				SceneObject obj = renderer.WrapToSceneObject(string.Format(
					"Mesh [{0}/{1}]", i + 1, gridList.Count), generateBoundingBox: false);
				partedGridObjects[i].Children.Add(obj);
			}
			// generate and display faces' normal lines.
			for (int i = 0; i < normalLineModelList.Count; i++)
			{
				NormalLineModel model = normalLineModelList[i];
				NormalLineRenderer renderer = NormalLineRenderer.Create(model);
				//renderer.Initialize();
				SceneObject obj = renderer.WrapToSceneObject(string.Format(
					"Face Normal of Mesh [{0}/{1}]", i + 1, normalLineModelList.Count), generateBoundingBox: false);
				obj.RenderingEnabled = false;
				obj.PickingEnabled = false;
				partedGridObjects[i].Children.Add(obj);
			}

			var notPickedGroup = new SceneObject(); notPickedGroup.Name = string.Format("Not Picked Group.");
			for (int i = 0; i < partedGridObjects.Length; i++)
			{
				notPickedGroup.Children.Add(partedGridObjects[i]);
			}

			return notPickedGroup;
		}

	}
}
