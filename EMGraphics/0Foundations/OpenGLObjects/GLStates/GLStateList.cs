﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

namespace EMGraphics
{
    /// <summary>
    ///
    /// </summary>
    [Editor(typeof(IListEditor<GLState>), typeof(UITypeEditor))]
    public class GLStateList : List<GLState>
    {
    }
}