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
    public partial class FrmSMTPSettings : Form
    {
        public string sender
        {
            get { return tbSender.Text.Trim(); }
            set { tbSender.Text = value; }
        }

        public string address
        {
            get { return tbAddress.Text.Trim(); }
            set { tbAddress.Text = value; }
        }

        public string server
        {
            get { return tbSMTPServer.Text.Trim(); }
            set { tbSMTPServer.Text = value; }
        }

        public int port
        {
            get { return Convert.ToInt32(nePort.Value); }
            set { nePort.Value = value; }
        }

        public string user
        {
            get { return tbUserName.Text.Trim(); }
            set { tbUserName.Text = value; }
        }

        public string security
        {
            get { return cbConnectionSecurity.Text; }
            set { cbConnectionSecurity.Text = value; }
        }

        public string authentication
        {
            get { return cbAuthenticationMethod.Text; }
            set { cbAuthenticationMethod.Text = value; }
        }

        public string password
        {
            get { return tbPassword.Text; }
            set { tbPassword.Text = value; }
        }

        public string serverType
        {
            set
            {
                if (value == "SMTP")
                    rbtnSMTP.Checked = true;
                else
                    rbtnExchange.Checked = true;

                ServerSelection();
            }
            get
            {
                if (rbtnSMTP.Checked)
                    return "SMTP";
                else
                    return "Exchange";
            }
        }

        public string exUser
        {
            get { return tbExUser.Text.Trim(); }
            set { tbExUser.Text = value; }
        }

        public string exPassword
        {
            get { return tbExPassword.Text; }
            set { tbExPassword.Text = value; }
        }

        public string exServer
        {
            get { return tbServerURL.Text; }
            set { tbServerURL.Text = value; }
        }

        public string exDomain
        {
            get { return tbDomain.Text; }
            set { tbDomain.Text = value; }
        }

        public string exEmail
        {
            get { return tbEmail.Text; }
            set { tbEmail.Text = value; }
        }

        public FrmSMTPSettings()
        {
            InitializeComponent();
        }

        private void FrmSMTPSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Check correct field filling
            if (this.DialogResult != DialogResult.OK)
                return;

            if (rbtnSMTP.Checked)
            {
                if (tbAddress.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Please provide sender address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }

                if (tbSMTPServer.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Please provide SMTP server name or IP address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }

                if (nePort.Value == 0 )
                {
                    MessageBox.Show("Please provide port number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                if (tbExUser.Text.Trim().Length==0)
                {
                    MessageBox.Show("Please provide user name for Exchange server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }

                if (tbServerURL.Text.Trim().Length==0 && tbEmail.Text.Trim().Length==0)
                {
                    MessageBox.Show("Please provide server URL or email address for Exchange server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void ServerSelection()
        {
            if (rbtnSMTP.Checked)
            {
                tabSMTP.Parent = tabControl1;
                tabExchange.Parent = null;
            }
            else
            {
                tabSMTP.Parent = null;
                tabExchange.Parent = tabControl1;
            }
        }

        private void rbtnExchange_CheckedChanged(object sender, EventArgs e)
        {
            ServerSelection();
        }

        private void rbtnSMTP_CheckedChanged(object sender, EventArgs e)
        {
            ServerSelection();
        }
    }
}
