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
    public partial class frmMessageSettings : Form
    {
        public string msgSubject
        {
            set { tbMessageSubject.Text = value; }
            get { return tbMessageSubject.Text; }
        }

        public string msgText
        {
            set { tbMessageBody.Text = value; }
            get { return tbMessageBody.Text; }
        }

        public bool useSizeLimit
        {
            set { chbSizeLimit.Checked = value; }
            get { return chbSizeLimit.Checked; }
        }

        public int maxSize
        {
            set { numSize.Value = value; }
            get { return Convert.ToInt32(numSize.Value); }
        }

        public string sizeDimension
        {
            set { cbDimension.Text = value; }
            get { return cbDimension.Text; }
        }

        public bool useReportCount
        {
            set { chbSendLast.Checked = value; }
            get { return chbSendLast.Checked; }
        }

        public int reportCount
        {
            set { numCount.Value = value; }
            get { return Convert.ToInt32(numCount.Value); }
        }

        public bool sendMessage
        {
            set { if (value) { rbtnCreateSend.Checked = true; } else { rbtnCreateSave.Checked = true; }; UpdateState(); }
            get { return rbtnCreateSend.Checked; }
        }

        public bool useTempFolder
        {
            set { if (value) rbtnUseTempFolder.Checked = true; else rbtnUseSelectedFolder.Checked = true; }
            get { return rbtnUseTempFolder.Checked; }
        }

        public string userFolder
        {
            set { tbSelectedFolder.Text = value; }
            get { return tbSelectedFolder.Text; }
        }

        public bool sendToFleet
        {
            get { return chbSendToFleet.Checked; }
            set { chbSendToFleet.Checked = value; }
        }

        public string ccAddresses
        {
            get { return GetCCAddressString(); }
            set { FillCCAddressList(value); }
        }

        public bool askForVessel
        {
            get { return chbAskForVessel.Checked; }
            set { chbAskForVessel.Checked = value; }
        }

        public bool useChart
        {
            get { return chbUseChart.Checked; }
            set { chbUseChart.Checked = value; }
        }

        public frmMessageSettings(Icon mainIcon, Font mainFont)
        {
            this.Icon = mainIcon;
            this.Font = mainFont;

            InitializeComponent();

            tbMessageBody.BackColor = SystemColors.Window;

            switch (MainForm.GetMailClientID())
            {
                case 0:
                    rbtnOutlook.Checked = true;
                    break;
                case 1:
                    rbtnMAPI.Checked = true;
                    break;
                case 2:
                    rbtnApplication.Checked = true;
                    break;
                default:
                    rbtnOutlook.Checked = true;
                    break;
            }
            
        }

        private void rbtnCreateSend_Click(object sender, EventArgs e)
        {
            UpdateState();
        }

        private void rbtnCreateSave_Click(object sender, EventArgs e)
        {
            UpdateState();
        }

        private void UpdateState()
        {
            bool send = rbtnCreateSend.Checked;

            tbMessageSubject.ReadOnly = !send;
            tbMessageSubject.Enabled = send;

            tbMessageBody.Enabled = send;
            tbMessageBody.ReadOnly = !send;

            chbSendLast.Enabled = send;
            chbSizeLimit.Enabled = send;
            numCount.Enabled = send;
            numSize.Enabled = send;
            cbDimension.Enabled = send;
            rbtnOutlook.Enabled = send;
            rbtnMAPI.Enabled = send;

            rbtnUseTempFolder.Enabled = !send;
            rbtnUseSelectedFolder.Enabled = !send;
            tbSelectedFolder.Enabled = !send;
            btnBrowse.Enabled = !send;

            chbAskForVessel.Enabled = send;
            chbSendToFleet.Enabled = send;
            lbCarbonCopy.Enabled = send;
            btnNew.Enabled = send;
            btnEdit.Enabled = send;
            btnDelete.Enabled = send;
        }

        private void frmMessageSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                //MainForm.SaveUseOutlook(rbtnOutlook.Checked);

                if (rbtnOutlook.Checked)
                {
                    MainForm.SetMailClientID(0);
                }
                else
                {
                    if (rbtnMAPI.Checked)
                    {
                        MainForm.SetMailClientID(1);
                    }
                    else
                    {
                        MainForm.SetMailClientID(2);
                    }
                }

                if (rbtnCreateSave.Checked && rbtnUseSelectedFolder.Checked && tbSelectedFolder.Text.Length > 0)
                {
                    if (!Directory.Exists(tbSelectedFolder.Text))
                    {
                        MessageBox.Show("Selected folder \""+tbSelectedFolder.Text+"\" does not exist.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var rslt = folderBrowserDialog1.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                tbSelectedFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private string GetCCAddressString()
        {
            string line = "";

            for (int i = 0; i < lbCarbonCopy.Items.Count; i++)
            {
                if (lbCarbonCopy.Items[i].ToString().Length > 0)
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            string defValue = "";

            if (MainForm.InputBox("Email adderss", "Please provide valid address", ref defValue) == DialogResult.OK)
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
                if (defValue.Trim().Length == 0)
                    lbCarbonCopy.Items.RemoveAt(lbCarbonCopy.SelectedIndex);
                else
                    lbCarbonCopy.Items[lbCarbonCopy.SelectedIndex] = defValue;
            }
        }

        private void chbAskForVessel_CheckStateChanged(object sender, EventArgs e)
        {
            chbSendToFleet.Enabled = chbAskForVessel.Checked;
        }

    }
}
