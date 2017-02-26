﻿using System;
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
        public SelectingType CurrentSelectingType { get; set; }

        /// <summary>
        /// 当前选中的几何图形（三角形）及其上下文信息。
        /// </summary>
        public PickedGeometry CurrentPickedGeometry { get; set; }

        /// <summary>
        /// 用什么颜色表示高亮？
        /// </summary>
        public Color HighlightColor { get; set; }

        private FormDisplayText frmDisplayPickedGeometry = new FormDisplayText("Picked Geometry");

        void glCanvas1_MouseDown(object sender, MouseEventArgs e)
        {
            List<Tuple<Point, PickedGeometry>> allPickedGeometrys = this.scene.Pick(
               e.Location, PickingGeometryType.Triangle);
            PickedGeometry geometry = null;
            if (allPickedGeometrys != null && allPickedGeometrys.Count > 0)
            { geometry = allPickedGeometrys[0].Item2; }

            if (geometry != null)
            {
                // print to window.
                this.frmDisplayPickedGeometry.SetText(geometry.ToString());
                this.frmDisplayPickedGeometry.Show();

                var renderer = geometry.FromRenderer as IHighlightable;
                if (renderer != null)
                {
                    // TODO: highlight this geometry...
                    switch (this.CurrentSelectingType)
                    {
                        case SelectingType.Triangle:
                            renderer.HighlightIndex = (int)geometry.VertexIds[0];
                            break;
                        case SelectingType.Mesh:
                            renderer.HighlightIndex = -1;
                            break;
                        case SelectingType.Face:
                            throw new NotImplementedException();
                        case SelectingType.Model:
                            throw new NotImplementedException();
                        default:
                            throw new NotImplementedException();
                    }
                }
            }
            else
            {
                PickedGeometry current = this.CurrentPickedGeometry;
                if (current != null)
                {
                    // TODO: de-highlight this geometry...
                    var renderer = current.FromRenderer as IHighlightable;
                    if (renderer != null)
                    {
                        renderer.HighlightIndex = -2;
                    }
                }
            }

            this.CurrentPickedGeometry = geometry;
        }

    }
}
