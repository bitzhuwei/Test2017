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

        /// <summary>
        /// 当前选中的几何图形（三角形）及其上下文信息。
        /// </summary>
        public PickedGeometry CurrentPickedGeometry { get; set; }

        private void btnPickTriangle_Click(object sender, EventArgs e)
        {
            this.CurrentSelectingType = SelectingType.Triangle;

            PickedGeometry current = this.CurrentPickedGeometry;
            if (current != null)
            {
                DeHighlight(current);
                this.CurrentPickedGeometry = null;
            }

            ResetPickedGroup();

            this.btnPickTriangle.ForeColor = Color.Green;
            this.btnPickMesh.ForeColor = Color.Black;
            this.btnPickModel.ForeColor = Color.Black;
        }

        private void btnPickMesh_Click(object sender, EventArgs e)
        {
            this.CurrentSelectingType = SelectingType.Mesh;

            PickedGeometry current = this.CurrentPickedGeometry;
            if (current != null)
            {
                DeHighlight(current);
                this.CurrentPickedGeometry = null;
            }

            ResetPickedGroup();

            this.btnPickTriangle.ForeColor = Color.Black;
            this.btnPickMesh.ForeColor = Color.Green;
            this.btnPickModel.ForeColor = Color.Black;
        }

        private void btnPickModel_Click(object sender, EventArgs e)
        {
            this.CurrentSelectingType = SelectingType.Model;

            PickedGeometry current = this.CurrentPickedGeometry;
            if (current != null)
            {
                DeHighlight(current);
                this.CurrentPickedGeometry = null;
            }

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
                    this.CurrentPickedGeometry = null;
                }

                if (geometry != null)
                {
                    Highlight(geometry);
                }

                this.CurrentPickedGeometry = geometry;
            }
        }

        private void DeHighlight(PickedGeometry pickedGeometry)
        {
            {
                var renderer = pickedGeometry.FromRenderer as IHighlightable;
                if (renderer != null)
                {
                    renderer.HighlightIndex0 = -2;
                    renderer.HighlightIndex1 = -2;
                    renderer.HighlightIndex2 = -2;
                }
            }

            {
                var renderer = pickedGeometry.FromRenderer as RendererBase;
                if (renderer != null)
                {
                    SceneObject obj = renderer.BindingSceneObject;// mesh xxx/yyy
                    if (obj != null)
                    {
                        ITreeNode<SceneObject> parent = obj.Parent;// mesh & face normal xxx/yyy
                        if (parent != null)
                        {
                            ITreeNode<SceneObject> target = parent.Parent;
                            if (target == this.pickedGroup)
                            {
                                this.pickedGroup.Children.Remove(parent);
                                this.notPickedGroup.Children.Add(parent);
                            }
                        }
                    }
                }
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
                    SceneObject obj = renderer.BindingSceneObject;// mesh xxx/yyy
                    if (obj != null)
                    {
                        ITreeNode<SceneObject> parent = obj.Parent;// mesh & face normal xxx/yyy
                        if (parent != null)
                        {
                            ITreeNode<SceneObject> target = parent.Parent;
                            if (target == this.notPickedGroup)
                            {
                                this.notPickedGroup.Children.Remove(parent);
                                this.pickedGroup.Children.Add(parent);
                            }
                        }
                    }
                }
            }
        }
    }
}
