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
    public partial class SplashForm : Form
    {
        public SplashForm(string appName)
        {
            this.Icon = MainForm.mainIcon;

            InitializeComponent();

            label1.Text = appName;
            label1.Parent = pictureBox1;

            if (MainForm.betaString.Length==0)
                label2.Text = "Version " + Application.ProductVersion;
            else
                label2.Text = "Version " + Application.ProductVersion + " Beta " + MainForm.betaString;

            label2.Parent = pictureBox1;

            this.BringToFront();
        }
    }
}
