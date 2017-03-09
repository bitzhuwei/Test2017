using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EMGraphics.Demo
{
    public partial class FormEMGridRenderer : Form
    {
        private Scene scene;
        public FormEMGridRenderer(Scene scene)
        {
            InitializeComponent();

            this.scene = scene;
        }

    }
}
