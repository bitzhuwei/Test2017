﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

namespace EMGraphics
{
    /// <summary>
    /// 用在IList&lt;<see cref="UniformVariable"/>&gt;类型的属性上。
    /// </summary>
    internal class UniformVariableListEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            //打开属性编辑器修改数据
            var editor = new FormUniformVariableListEditor(context, provider, value as List<UniformVariable>);
            editor.ShowDialog();

            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            //指定为模式窗体属性编辑器类型
            return UITypeEditorEditStyle.Modal;
        }
    }
}