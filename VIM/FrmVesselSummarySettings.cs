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
using System.Diagnostics;

namespace VIM
{
    public partial class FrmVesselSummarySettings : Form
    {
        public int actionCode
        {
            get { if (rbtnCreateSummary.Checked) return 0; else return 1; }
            set { if (value == 0) { rbtnCreateSummary.Checked = true; } else { rbtnSendSummary.Checked = true; }; UpdateState(); }
        }

        public string messageSubject
        {
            get { return tbMessageSubject.Text.Trim(); }
            set { tbMessageSubject.Text = value; }
        }

        public string messageText
        {
            get { return tbMessageText.Text; }
            set { tbMessageText.Text = value; }
        }

        public bool showFile
        {
            get { return chbShowFile.Checked; }
            set { chbShowFile.Checked = value; }
        }

        public string fileNameTemplate
        {
            get { return tbFileNameTemplate.Text.Trim(); }
            set { tbFileNameTemplate.Text = value; }
        }

        public string fileFormatText
        {
            get { return cbFileFormat.Text; }
            set { cbFileFormat.Text = value; }
        }

        public string ccAddresses
        {
            get { return GetCCAddressString(); }
            set { FillCCAddressList(value); }
        }

        public bool sendToFleet
        {
            get { return chbSendToFleet.Checked; }
            set { chbSendToFleet.Checked = value; }
        }

        public bool useTemplate
        {
            get { return chbUseTemplate.Checked; }
            set { chbUseTemplate.Checked = value; UpdateUseTemplate(); }
        }

        public string templateFile
        {
            get { return tbTemplate.Text.Trim(); }
            set { tbTemplate.Text = value; }
        }

        public FrmVesselSummarySettings(Font mainFont, Icon mainIcon)
        {
            this.Font = mainFont;
            this.Icon = mainIcon;

            InitializeComponent();

            int mailClientID = MainForm.GetMailClientID();

            switch (mailClientID)
            {
                case 0:
                    rbtnUseOutlook.Checked = true;
                    break;
                case 1:
                    rbtnUseMAPI.Checked = true;
                    break;
                case 2:
                    rbtnInnerClient.Checked = true;
                    break;
            }
        }

        private void UpdateState()
        {
            bool send = rbtnSendSummary.Checked;

            tbMessageSubject.ReadOnly = !send;
            tbMessageText.ReadOnly = !send;
            chbShowFile.Enabled = !send;
            btnNew.Enabled = send;
            btnEdit.Enabled = send;
            btnDelete.Enabled = send;
            lbCarbonCopy.Enabled = send;

            if (send)
                lbCarbonCopy.BackColor = SystemColors.Window;
            else
                lbCarbonCopy.BackColor = SystemColors.Control;

            rbtnUseOutlook.Enabled = send;
            rbtnUseMAPI.Enabled = send;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            string defValue = "";

            if (MainForm.InputBox("Email adderss","Please provide valid address",ref defValue)==DialogResult.OK)
            {
                lbCarbonCopy.Items.Add(defValue);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lbCarbonCopy.Items.Count == 0)
                return;

            if (lbCarbonCopy.SelectedIndex < 0)
                return;

            string defValue = lbCarbonCopy.Items[lbCarbonCopy.SelectedIndex].ToString();

            if (MainForm.InputBox("Email adderss", "Please provide valid address", ref defValue) == DialogResult.OK)
            {
                if (defValue.Trim().Length==0)
                    lbCarbonCopy.Items.RemoveAt(lbCarbonCopy.SelectedIndex);
                else
                    lbCarbonCopy.Items[lbCarbonCopy.SelectedIndex] = defValue;
            }
        }

        private string GetCCAddressString()
        {
            string line = "";

            for (int i=0; i<lbCarbonCopy.Items.Count;i++)
            {
                if (lbCarbonCopy.Items[i].ToString().Length>0)
                {
                    if (line.Length == 0)
                        line = lbCarbonCopy.Items[i].ToString();
                    else
                        line = line + "%nl" + lbCarbonCopy.Items[i].ToString();
                }
            }

            return line;
        }

        private void FillCCAddressList(string line)
        {
            lbCarbonCopy.Items.Clear();

            if (line.Length > 0)
            {
                line = line.Replace("%nl", "\r\n");

                TextBox tb = new TextBox();
                tb.Multiline = true;
                tb.Text = line;

                lbCarbonCopy.Items.AddRange(tb.Lines);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lbCarbonCopy.Items.Count == 0)
                return;

            if (lbCarbonCopy.SelectedIndex < 0)
                return;

            lbCarbonCopy.Items.RemoveAt(lbCarbonCopy.SelectedIndex);
        }

        private void FrmVesselSummarySettings_Load(object sender, EventArgs e)
        {
            //Restore form settings
            // Upgrade?
            if (Properties.Settings.Default.VesselSummarySettingsFormSize.Width == 0)
                Properties.Settings.Default.Upgrade();

            if (Properties.Settings.Default.VesselSummarySettingsFormSize.Width == 0 || Properties.Settings.Default.VesselSummarySettingsFormSize.Height == 0)
            {
                // first start
                // optional: add default values
            }
            else
            {
                this.WindowState = Properties.Settings.Default.VesselSummarySettingsFormState;

                // we don't want a minimized window at startup
                if (this.WindowState == FormWindowState.Minimized) this.WindowState = FormWindowState.Normal;

                this.Location = Properties.Settings.Default.VesselSummarySettingsFormLocation;
                this.Size = Properties.Settings.Default.VesselSummarySettingsFormSize;
            }
        }

        private void FrmVesselSummarySettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Store form settings
            Properties.Settings.Default.VesselSummarySettingsFormState = this.WindowState;

            if (this.WindowState == FormWindowState.Normal)
            {
                // save location and size if the state is normal
                Properties.Settings.Default.VesselSummarySettingsFormLocation = this.Location;
                Properties.Settings.Default.VesselSummarySettingsFormSize = this.Size;
            }
            else
            {
                // save the RestoreBounds if the form is minimized or maximized!
                Properties.Settings.Default.VesselSummarySettingsFormLocation = this.RestoreBounds.Location;
                Properties.Settings.Default.VesselSummarySettingsFormSize = this.RestoreBounds.Size;
            }

            // don't forget to save the settings
            Properties.Settings.Default.Save();

            if (this.DialogResult == DialogResult.OK)
            {
                //MainForm.SaveUseOutlook(rbtnUseOutlook.Checked);

                if (rbtnUseOutlook.Checked)
                {
                    MainForm.SetMailClientID(0);
                }
                else
                {
                    if (rbtnUseMAPI.Checked)
                    {
                        MainForm.SetMailClientID(1);
                    }
                    else
                    {
                        MainForm.SetMailClientID(2);
                    }
                }
            }
        }

        private void rbtnCreateSummary_Click(object sender, EventArgs e)
        {
            UpdateState();
        }

        private void rbtnSendSummary_Click(object sender, EventArgs e)
        {
            UpdateState();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string fileName = "";
            string templateFolder=Path.Combine(MainForm.workFolder,"Templates");

            if (tbTemplate.Text.Trim().Length > 0)
            {
                fileName = Path.Combine(templateFolder, tbTemplate.Text);

                if (!File.Exists(fileName))
                    fileName = "";
            }

            openFileDialog1.FileName = fileName;
            openFileDialog1.InitialDirectory = templateFolder;


            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;

                if (templateFolder!=Path.GetDirectoryName(fileName))
                {
                    MessageBox.Show("Selected file is not located in the application Templates folder",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    tbTemplate.Text = Path.GetFileName(fileName);
                }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            //Open file
            string fileName = "";
            string templateFolder = Path.Combine(MainForm.workFolder, "Templates");

            if (tbTemplate.Text.Trim().Length > 0)
            {
                fileName = Path.Combine(templateFolder, tbTemplate.Text);
            }

            if (File.Exists(fileName))
            {
                Process.Start(fileName);
            }
            else
            {
                MessageBox.Show("File \"" + fileName + "\" was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbUseTemplate_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUseTemplate();
        }

        private void UpdateUseTemplate()
        {
            tbTemplate.Enabled = chbUseTemplate.Checked;
            btnBrowse.Enabled = chbUseTemplate.Checked;
            btnOpen.Enabled = chbUseTemplate.Checked;
        }

    }
}
