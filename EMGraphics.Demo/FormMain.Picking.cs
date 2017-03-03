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
                switch (value)
                {
                    case SelectingType.Triangle:
                        {
                            SceneObject whole = this.wholeObject;
                            if (whole != null)
                            {
                                whole.RenderingEnabled = true;
                                whole.PickingEnabled = true;
                            }
                            SceneObject notPicked = this.notPickedGroup;
                            if (notPicked != null)
                            {
                                notPicked.RenderingEnabled = false;
                                notPicked.PickingEnabled = false;
                            }
                            SceneObject picked = this.pickedGroup;
                            if (picked != null)
                            {
                                picked.RenderingEnabled = false;
                                picked.PickingEnabled = false;
                            }
                        }
                        break;
                    case SelectingType.Mesh:
                        {
                            SceneObject whole = this.wholeObject;
                            if (whole != null)
                            {
                                whole.RenderingEnabled = true;
                                whole.PickingEnabled = false;
                            }
                            SceneObject notPicked = this.notPickedGroup;
                            if (notPicked != null)
                            {
                                notPicked.RenderingEnabled = false;
                                notPicked.PickingEnabled = true;
                            }
                            SceneObject picked = this.pickedGroup;
                            if (picked != null)
                            {
                                picked.RenderingEnabled = true;
                                picked.PickingEnabled = true;
                            }
                        }
                        break;
                    case SelectingType.Model:
                        {
                            SceneObject whole = this.wholeObject;
                            if (whole != null)
                            {
                                whole.RenderingEnabled = true;
                                whole.PickingEnabled = true;
                            }
                            SceneObject notPicked = this.notPickedGroup;
                            if (notPicked != null)
                            {
                                notPicked.RenderingEnabled = false;
                                notPicked.PickingEnabled = false;
                            }
                            SceneObject picked = this.pickedGroup;
                            if (picked != null)
                            {
                                picked.RenderingEnabled = false;
                                picked.PickingEnabled = false;
                            }
                        }
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
        private SelectingType currentSelectingType;

        /// <summary>
        /// 当前选中的几何图形（三角形）及其上下文信息。
        /// </summary>
        public PickedGeometry CurrentPickedGeometry { get; set; }

        private void btnPickTriangle_Click(object sender, EventArgs e)
        {
            this.CurrentSelectingType = SelectingType.Triangle;

            this.btnPickTriangle.ForeColor = Color.Green;
            this.btnPickMesh.ForeColor = Color.Black;
        }

        private void btnPickMesh_Click(object sender, EventArgs e)
        {
            this.CurrentSelectingType = SelectingType.Mesh;

            this.btnPickTriangle.ForeColor = Color.Black;
            this.btnPickMesh.ForeColor = Color.Green;
        }

        /// <summary>
        /// 用什么颜色表示高亮？
        /// </summary>
        public Color HighlightColor { get; set; }

        private FormDisplayText frmDisplayPickedGeometry = new FormDisplayText("Picked Geometry");

        void glCanvas1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                List<Tuple<Point, PickedGeometry>> allPickedGeometrys = this.scene.Pick(
                   e.Location, PickingGeometryType.Triangle);
                PickedGeometry geometry = null;
                if (allPickedGeometrys != null && allPickedGeometrys.Count > 0)
                { geometry = allPickedGeometrys[0].Item2; }

                PickedGeometry current = this.CurrentPickedGeometry;
                if (current != null)
                {
                    DeHighlight(current);

                    var renderer = current.FromRenderer as RendererBase;
                    if (renderer != null)
                    {
                        SceneObject obj = renderer.BindingSceneObject;
                        if (obj != null)
                        {
                            ITreeNode<SceneObject> parent = obj.Parent;
                            if (parent != null)
                            {
                                ITreeNode<SceneObject> target = parent.Parent;
                                if (target == this.pickedGroup)
                                {
                                    this.pickedGroup.Children.Remove(renderer.BindingSceneObject);
                                    this.notPickedGroup.Children.Add(renderer.BindingSceneObject);
                                }
                            }
                        }
                    }
                }

                if (geometry != null)
                {
                    Highlight(geometry);

                    var renderer = geometry.FromRenderer as RendererBase;
                    if (renderer != null)
                    {
                        SceneObject obj = renderer.BindingSceneObject;
                        if (obj != null)
                        {
                            ITreeNode<SceneObject> parent = obj.Parent;
                            if (parent != null)
                            {
                                ITreeNode<SceneObject> target = parent.Parent;
                                if (target == this.notPickedGroup)
                                {
                                    this.notPickedGroup.Children.Remove(renderer.BindingSceneObject);
                                    this.pickedGroup.Children.Add(renderer.BindingSceneObject);
                                }
                            }
                        }
                    }
                }

                this.CurrentPickedGeometry = geometry;
            }
        }

        private void DeHighlight(PickedGeometry pickedGeometry)
        {
            var renderer = pickedGeometry.FromRenderer as IHighlightable;
            if (renderer != null)
            {
                renderer.HighlightIndex0 = -2;
                renderer.HighlightIndex1 = -2;
                renderer.HighlightIndex2 = -2;
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
    }
}
