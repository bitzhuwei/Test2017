using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMGraphics.Demo
{
    /// <summary>
    /// 选中模式。
    /// 一个模型可包含多个面，一个面可包含多个三角形网格，一个三角形网格可包含多个三角形。
    /// </summary>
    public enum SelectingType
    {
        /// <summary>
        /// 选中一个三角形
        /// </summary>
        Triangle,

        /// <summary>
        /// 选中一个三角形网格
        /// </summary>
        Mesh,

        /// <summary>
        /// 选中一个面
        /// </summary>
        Face,

        /// <summary>
        /// 选中一个模型
        /// </summary>
        Model,
    }
}
