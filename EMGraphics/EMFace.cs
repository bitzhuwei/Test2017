using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMGraphics
{
    public class EMFace
    {
        public List<EMGrid> GridList { get; private set; }

        public EMFace()
        {
            this.GridList = new List<EMGrid>();
        }
    }
}
