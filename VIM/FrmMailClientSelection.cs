using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VIQManager
{
    public partial class FrmMailClientSelection : Form
    {
        public FrmMailClientSelection()
        {
            InitializeComponent();

            cbMAPIClients.Items.Clear();

            cbMAPIClients.Items.Add("");

            cbMAPIClients.Items.Add("Default MAPI client");

            for (int i = 0; i < MainForm.MAPIClients.Count; i++)
            {
                cbMAPIClients.Items.Add(MainForm.MAPIClients[i]);
            }

            bool UseOutlook;
            bool UseMAPI;
            string MAPIClient;
            string MAPIProfile;
            string MAPIPassword;

            MainForm.GetMailSettings(out UseOutlook, out  UseMAPI,out  MAPIClient,out  MAPIProfile,out  MAPIPassword);

            rbtnOutlook.Checked = UseOutlook;
            rbtnMAPI.Checked = UseMAPI;
            cbMAPIClients.Text = MAPIClient;
            tbMAPILogin.Text = MAPIProfile;
            tbMAPIPassword.Text = MAPIPassword;
        }

        private void rbtnOutlook_CheckedChanged(object sender, EventArgs e)
        {
            cbMAPIClients.Enabled = false;
            tbMAPILogin.Enabled = false;
            tbMAPIPassword.Enabled = false;
        }

        private void rbtnMAPI_CheckedChanged(object sender, EventArgs e)
        {
            cbMAPIClients.Enabled = true;
            tbMAPILogin.Enabled = true;
            tbMAPIPassword.Enabled = true;
        }

        private void FrmMailClientSelection_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Check settings
            if (this.DialogResult != DialogResult.OK)
                return;

            if (rbtnMAPI.Checked)
            {
                if (cbMAPIClients.Text.Trim().Length==0)
                {
                    MessageBox.Show("Please select MAPI mail client", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
            }
            
            //Save settings in personal ini file
            MainForm.SaveMailSettings(rbtnOutlook.Checked, rbtnMAPI.Checked, cbMAPIClients.Text, tbMAPILogin.Text, tbMAPIPassword.Text);
        }
    }
}
