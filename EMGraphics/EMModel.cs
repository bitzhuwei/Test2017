using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMGraphics
{
    public class EMModel
    {
        public List<EMFace> FaceList { get; private set; }

        public EMModel()
        {
            this.FaceList = new List<EMFace>();
        }
    }
}
