using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMGraphics.Demo
{
    /// <summary>
    /// 选中模式。
    /// 一个模型可包含多个“面”（即三角形网格），一个三角形网格由多个三角形组成。
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
        /// 选中一个模型
        /// </summary>
        Model,
    }
}
