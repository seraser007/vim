using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace VIM
{
    public partial class LicenseListForm : Form
    {
        public string selectedFile="";
        private string[] files;
        public LicenseListForm(Icon mainIcon, Font mainFont, string[] fileList)
        {
            this.Icon = mainIcon;
            this.Font = mainFont;
            files = fileList;

            InitializeComponent();

            for (int i=0;i<fileList.Length;i++)
            {
                listBox1.Items.Add(Path.GetFileName(files[i]));
            }
        }

        private void LicenseListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult!=DialogResult.OK)
            {
                selectedFile = "";
            }
            else
            {
                selectedFile = files[listBox1.SelectedIndex];
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex>=0)
                this.DialogResult = DialogResult.OK;
        }
    }
}
