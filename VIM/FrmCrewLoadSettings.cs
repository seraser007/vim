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
    public partial class FrmCrewLoadSettings : Form
    {
        private bool formActive = false;

        public FrmCrewLoadSettings()
        {
            this.Icon = MainForm.mainIcon;
            this.Font = MainForm.mainFont;

            InitializeComponent();

            bool askForFile = Convert.ToBoolean(MainForm.ReadIniValue(MainForm.iniPersonalFile, "LoadCrewSettings", "AskForFile","True"));
            string fileName = MainForm.ReadIniValue(MainForm.iniPersonalFile, "LoadCrewSettings", "FileName");
            bool relativePath = Convert.ToBoolean(MainForm.ReadIniValue(MainForm.iniPersonalFile, "LoadCrewSettings", "RelativePath","False"));

            if (askForFile)
                rbAskForFile.Checked = true;
            else
                rbUsePredefinedFile.Checked = true;

            if (relativePath)
                rbRelative.Checked = true;
            else
                rbAbsolute.Checked = true;

            tbFileName.Text = fileName;

            formActive = true;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {

            if (tbFileName.Text.Trim().Length == 0)
            {
                openFileDialog1.FileName = "";
            }
            else
            {
                if (rbAbsolute.Checked)
                {
                    if (File.Exists(tbFileName.Text))
                    {
                        openFileDialog1.InitialDirectory = Path.GetDirectoryName(tbFileName.Text);
                        openFileDialog1.FileName = Path.GetFileName(tbFileName.Text);
                    }
                    else
                    {
                        string dir = Path.GetDirectoryName(tbFileName.Text);

                        if (Directory.Exists(dir))
                            openFileDialog1.InitialDirectory = dir;

                        openFileDialog1.FileName = "";
                    }
                }
                else
                {
                    string fileName = "";

                    if (tbFileName.Text.StartsWith("\\"))
                        fileName = Path.Combine(MainForm.workFolder, tbFileName.Text.Substring(1));
                    else
                        fileName=Path.Combine(MainForm.workFolder, tbFileName.Text);

                    if (File.Exists(fileName))
                    {
                        openFileDialog1.FileName = Path.GetFileName(fileName);
                        openFileDialog1.InitialDirectory = Path.GetDirectoryName(fileName);
                    }
                    else
                    {
                        openFileDialog1.FileName = "";
                    }
                }
            }

            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                if (rbRelative.Checked)
                {
                    tbFileName.Text = openFileDialog1.FileName.Substring(MainForm.workFolder.Length + 1);
                }
                else
                {
                    tbFileName.Text = openFileDialog1.FileName;
                }
            }
        }

        private void FrmCrewLoadSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult==DialogResult.OK)
            {
                MainForm.WriteIniValue(MainForm.iniPersonalFile, "LoadCrewSettings", "AskForFile", MainForm.BoolToStr(rbAskForFile.Checked));
                MainForm.WriteIniValue(MainForm.iniPersonalFile, "LoadCrewSettings", "FileName", tbFileName.Text);
                MainForm.WriteIniValue(MainForm.iniPersonalFile, "LoadCrewSettings", "RelativePath", MainForm.BoolToStr(rbRelative.Checked));
            }
        }

        private void rbUsePredefinedFile_CheckedChanged(object sender, EventArgs e)
        {
            rbAbsolute.Enabled = true;
            rbRelative.Enabled = true;
            btnBrowse.Enabled = true;
            tbFileName.Enabled = true;
        }

        private void rbAskForFile_CheckedChanged(object sender, EventArgs e)
        {
            rbAbsolute.Enabled = false;
            rbRelative.Enabled = false;
            btnBrowse.Enabled = false;
            tbFileName.Enabled = false;
        }


        private void rbRelative_Click(object sender, EventArgs e)
        {
            if (!formActive)
                return;

            string fName = tbFileName.Text;

            if (fName.Length > 0)
            {
                if (fName.StartsWith(MainForm.workFolder))
                {
                    fName = fName.Substring(MainForm.workFolder.Length);
                    tbFileName.Text = fName;
                }
                else
                {
                    MessageBox.Show("File is not located in the working folder. File name will be cleared.",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    tbFileName.Text = "";
                }
            }
        }

 
        private void rbAbsolute_Click(object sender, EventArgs e)
        {
            if (!formActive) return;

            if (tbFileName.Text.Trim().Length > 0)
            {
                string fileName = tbFileName.Text;

                if (fileName.StartsWith("\\"))
                {
                    fileName = Path.Combine(MainForm.workFolder, fileName.Substring(1));
                }
                else
                    fileName = Path.Combine(MainForm.workFolder, fileName);

                if (File.Exists(fileName))
                {
                    tbFileName.Text = fileName;
                }
                else
                {
                    MessageBox.Show("File was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbFileName.Text = "";
                }
            }

        }
    }
}
