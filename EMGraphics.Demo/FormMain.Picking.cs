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
		/// <summary>
		/// 选中模式：三角形、网格、面、模型？
		/// </summary>
		public SelectingType CurrentSelectingType
		{
			get { return this.currentSelectingType; }
			set
			{
				this.currentSelectingType = value;
				UpdatePickingState(value);
			}
		}

		private void UpdatePickingState(SelectingType value)
		{
			switch (value)
			{
				case SelectingType.Triangle:
					{
						SceneObject whole = this.wholeObject;
						if (whole != null)
						{
							whole.PickingEnabled = true;
						}
						SceneObject notPicked = this.notPickedGroup;
						if (notPicked != null)
						{
							notPicked.PickingEnabled = false;
						}
						SceneObject picked = this.pickedGroup;
						if (picked != null)
						{
							picked.PickingEnabled = false;
						}
					}
					break;
				case SelectingType.Mesh:
					{
						SceneObject whole = this.wholeObject;
						if (whole != null)
						{
							whole.PickingEnabled = false;
						}
						SceneObject notPicked = this.notPickedGroup;
						if (notPicked != null)
						{
							notPicked.PickingEnabled = true;
						}
						SceneObject picked = this.pickedGroup;
						if (picked != null)
						{
							picked.PickingEnabled = true;
						}
					}
					break;
				case SelectingType.Model:
					{
						SceneObject whole = this.wholeObject;
						if (whole != null)
						{
							whole.PickingEnabled = true;
						}
						SceneObject notPicked = this.notPickedGroup;
						if (notPicked != null)
						{
							notPicked.PickingEnabled = false;
						}
						SceneObject picked = this.pickedGroup;
						if (picked != null)
						{
							picked.PickingEnabled = false;
						}
					}
					break;
				default:
					throw new NotImplementedException();
			}
		}
		private SelectingType currentSelectingType;

		private List<PickedGeometry> currentPickedGeometrys = new List<PickedGeometry>();
		/// <summary>
		/// 当前选中的几何图形（三角形）及其上下文信息。
		/// </summary>
		public List<PickedGeometry> CurrentPickedGeometrys { get { return this.currentPickedGeometrys; } }

		private void btnPickTriangle_Click(object sender, EventArgs e)
		{
			this.CurrentSelectingType = SelectingType.Triangle;

			DeHighlight(this.CurrentPickedGeometrys);
			this.CurrentPickedGeometrys.Clear();

			ResetPickedGroup();

			this.btnPickTriangle.ForeColor = Color.Green;
			this.btnPickMesh.ForeColor = Color.Black;
			this.btnPickModel.ForeColor = Color.Black;
		}

		private void btnPickMesh_Click(object sender, EventArgs e)
		{
			this.CurrentSelectingType = SelectingType.Mesh;

			DeHighlight(this.CurrentPickedGeometrys);
			this.CurrentPickedGeometrys.Clear();

			ResetPickedGroup();

			this.btnPickTriangle.ForeColor = Color.Black;
			this.btnPickMesh.ForeColor = Color.Green;
			this.btnPickModel.ForeColor = Color.Black;
		}

		private void btnPickModel_Click(object sender, EventArgs e)
		{
			this.CurrentSelectingType = SelectingType.Model;

			DeHighlight(this.CurrentPickedGeometrys);
			this.CurrentPickedGeometrys.Clear();

			ResetPickedGroup();

			this.btnPickTriangle.ForeColor = Color.Black;
			this.btnPickMesh.ForeColor = Color.Black;
			this.btnPickModel.ForeColor = Color.Green;
		}

		private void ResetPickedGroup()
		{
			SceneObject picked = this.pickedGroup;
			SceneObject notPicked = this.notPickedGroup;
			if (picked != null && notPicked != null)
			{
				foreach (var item in picked.Children)
				{
					notPickedGroup.Children.Add(item);
				}
				picked.Children.Clear();
			}
		}

		/// <summary>
		/// 用什么颜色表示高亮？
		/// </summary>
		public Color HighlightColor { get; set; }

		private FormDisplayText frmDisplayPickedGeometry = new FormDisplayText("Picked Geometry");

		private Point leftMouseDownPosition;

		void glCanvas1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.leftMouseDownPosition = e.Location;
			}
		}

		private void GlCanvas1_MouseMove(object sender, MouseEventArgs e)
		{
		}

		private void GlCanvas1_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (e.Location == this.leftMouseDownPosition)
				{
					TryPicking(sender, e);
				}
			}
		}

		private void TryPicking(object sender, MouseEventArgs e)
		{
			List<Tuple<Point, PickedGeometry>> allPickedGeometrys = this.scene.Pick(
				   e.Location, PickingGeometryType.Triangle);
			PickedGeometry geometry = null;
			if (allPickedGeometrys != null && allPickedGeometrys.Count > 0)
			{ geometry = allPickedGeometrys[0].Item2; }


			if ((Control.ModifierKeys & Keys.Control) == Keys.Control)// when CTRL key is down, we do multiple selection.
			{
				PickedGeometry alreadySelected = Match(geometry);

				if (alreadySelected != null)// already selected, so dedelect it.
				{
					DeHighlight(alreadySelected);
					this.currentPickedGeometrys.Remove(alreadySelected);
				}
				else// otherwise, select it.
				{
					if (geometry != null)
					{
						Highlight(geometry);
						this.CurrentPickedGeometrys.Add(geometry);
					}
					else// nothing to do.
					{
					}
				}
			}
			else// when CTRL key is not down, we do single selection.
			{
				DeHighlight(this.CurrentPickedGeometrys);
				this.CurrentPickedGeometrys.Clear();
				if (geometry != null)
				{
					Highlight(geometry);
					this.CurrentPickedGeometrys.Add(geometry);
				}
			}
		}

		private PickedGeometry Match(PickedGeometry geometry)
		{
			PickedGeometry alreadySelected = null;

			if (geometry != null)
			{
				foreach (var item in this.CurrentPickedGeometrys)
				{
					switch (this.CurrentSelectingType)
					{
						case SelectingType.Triangle:
							if (item.FromRenderer == geometry.FromRenderer
								&& item.StageVertexId == geometry.StageVertexId)
							{
								alreadySelected = item;
							}
							break;
						case SelectingType.Mesh:
							if (item.FromRenderer == geometry.FromRenderer)
							{
								alreadySelected = item;
							}
							break;
						case SelectingType.Model:
							if (item.FromRenderer == geometry.FromRenderer)
							{
								alreadySelected = item;
							}
							break;
						default:
							break;
					}

					if (alreadySelected != null) { break; }
				}
			}

			return alreadySelected;
		}

		private void DeHighlight(PickedGeometry geometry)
		{
			{
				var renderer = geometry.FromRenderer as IHighlightable;
				if (renderer != null)
				{
					renderer.HighlightIndex0 = -2;
					renderer.HighlightIndex1 = -2;
					renderer.HighlightIndex2 = -2;
				}
			}

			{
				var renderer = geometry.FromRenderer as RendererBase;
				if (renderer != null)
				{
					SceneObject obj = renderer.BindingSceneObject;// mesh [xxx/yyy]
					if (obj != null)
					{
						this.pickedGroup.Children.Remove(obj);
						this.notPickedGroup.Children.Add(obj);
					}
				}
			}

			{
				var renderer = geometry.FromRenderer as EMGridRenderer;
				var wholeNormal = this.wholeNormal.Renderer as FaceNormalRenderer;
				if (renderer != null && wholeNormal != null)
				{
					wholeNormal.SetVisible(renderer.Label, false);
				}
			}
		}

		private void DeHighlight(List<PickedGeometry> geometrys)
		{
			foreach (var geometry in geometrys)
			{
				DeHighlight(geometry);
			}
		}

		private void Highlight(List<PickedGeometry> geometrys)
		{
			foreach (var geometry in geometrys)
			{
				Highlight(geometry);
			}
		}

		private void Highlight(PickedGeometry geometry)
		{
			// print to window.
			if (this.frmDisplayPickedGeometry.IsDisposed)
			{
				this.frmDisplayPickedGeometry = new FormDisplayText("Picked Geometry");
			}

			this.frmDisplayPickedGeometry.SetText(geometry.ToString());
			this.frmDisplayPickedGeometry.Show();

			{
				var renderer = geometry.FromRenderer as IHighlightable;
				if (renderer != null)
				{
					// TODO: highlight this geometry.
					switch (this.CurrentSelectingType)
					{
						case SelectingType.Triangle:
							renderer.HighlightIndex0 = (int)geometry.VertexIds[0];
							renderer.HighlightIndex1 = (int)geometry.VertexIds[1];
							renderer.HighlightIndex2 = (int)geometry.VertexIds[2];
							break;
						case SelectingType.Mesh:
							renderer.HighlightIndex0 = -1;
							renderer.HighlightIndex1 = -1;
							renderer.HighlightIndex2 = -1;
							break;
						case SelectingType.Model:
							renderer.HighlightIndex0 = -1;
							renderer.HighlightIndex1 = -1;
							renderer.HighlightIndex2 = -1;
							break;
						default:
							throw new NotImplementedException();
					}
				}
			}
			{
				var renderer = geometry.FromRenderer as RendererBase;
				if (renderer != null)
				{
					SceneObject obj = renderer.BindingSceneObject;// mesh [xxx/yyy]
					if (obj != null)
					{
						this.notPickedGroup.Children.Remove(obj);
						this.pickedGroup.Children.Add(obj);
					}
				}
			}
			{
				var renderer = geometry.FromRenderer as EMGridRenderer;
				var wholeNormal = this.wholeNormal.Renderer as FaceNormalRenderer;
				if (renderer != null && wholeNormal != null)
				{
					wholeNormal.SetVisible(renderer.Label, true);
				}
			}
		}
	}
}
