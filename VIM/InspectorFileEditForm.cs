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
    public partial class InspectorFileEditForm : Form
    {
        public string fileName
        {
            get { return tbFileName.Text; }
            set { tbFileName.Text = value; }
        }
        private string initialFolder = "";

        public InspectorFileEditForm(Icon mainIcon, Font mainFont)
        {
            this.Icon = mainIcon;
            this.Font = mainFont;

            InitializeComponent();

            initialFolder = Directory.GetCurrentDirectory() + "\\Related";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //Browse for file
            string iniDir = "";

            if (tbFileName.Text.Trim().Length == 0)
                iniDir = initialFolder;
            else
                iniDir = Path.GetDirectoryName(tbFileName.Text);

            openFileDialog1.InitialDirectory = iniDir;
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.FileName = "";
            openFileDialog1.Multiselect = false;
            

            var rslt = openFileDialog1.ShowDialog();

            if (rslt==DialogResult.OK)
            {
                string newFile = openFileDialog1.FileName;

                if (newFile.StartsWith(initialFolder))
                    tbFileName.Text = openFileDialog1.FileName;
                else
                {
                    MessageBox.Show("All files should be located in the \"" + initialFolder + "\" folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
