using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace EMGraphics
{
    public interface IHighlightable
    {
        /// <summary>
        /// -2：全部不高亮。-1：全部高亮。其他：高亮其指定的图元（此项目中的图元即三角形）。
        /// </summary>
        int HighlightIndex0 { get; set; }
		/// <summary>
        /// -2：全部不高亮。-1：全部高亮。其他：高亮其指定的图元（此项目中的图元即三角形）。
        /// </summary>
        int HighlightIndex1 { get; set; }
		/// <summary>
        /// -2：全部不高亮。-1：全部高亮。其他：高亮其指定的图元（此项目中的图元即三角形）。
        /// </summary>
        int HighlightIndex2 { get; set; }

        /// <summary>
        /// 用什么颜色表示高亮？
        /// </summary>
        Color HighlightColor { get; set; }
    }
}
