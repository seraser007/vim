using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VIM
{
    public partial class FormProgress : Form
    {
        private int maxValue = 0;
        private int aValue = 0;
        private bool stopProcess = false;

        public int position
        {
            set 
            { 
                aValue = value; 
                progressBar1.Value = aValue; 
                lblProgress.Text = aValue.ToString() + " of " + maxValue.ToString();
                Application.DoEvents();
            }

            get { return aValue; }
        }

        public bool terminate
        {
            get { return stopProcess; }
        }

        public FormProgress(int MaxValue)
        {
            InitializeComponent();

            maxValue = MaxValue;
            progressBar1.Maximum = MaxValue;
        }

        private void btnTerminate_Click(object sender, EventArgs e)
        {
            stopProcess = true;
        }
    }
}
