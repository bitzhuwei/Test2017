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
    public partial class FormDisplayText : Form
    {
        public FormDisplayText(string title)
        {
            InitializeComponent();

            this.Text = title;
        }

        public void SetText(string text)
        {
            this.textBox1.Text = text;
        }
    }
}
