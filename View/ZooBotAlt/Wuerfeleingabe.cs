using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MeisterGeister.View.ZooBotAlt
{
    public partial class Wuerfeleingabe : Form
    {
        private int m_eigenschaft1 = 10;
        private int m_eigenschaft2 = 10;
        private int m_eigenschaft3 = 10;

        public Wuerfeleingabe()
        {
            InitializeComponent();
        }

        public int Eigenschaft1
        {
            get { return this.m_eigenschaft1; }
        }

        public int Eigenschaft2
        {
            get { return this.m_eigenschaft2; }
        }

        public int Eigenschaft3
        {
            get { return this.m_eigenschaft3; }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.m_eigenschaft1 = (int)this.numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            this.m_eigenschaft2 = (int)this.numericUpDown2.Value;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            this.m_eigenschaft3 = (int)this.numericUpDown3.Value;
        }
    }
}