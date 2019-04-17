using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations;

namespace VIM
{
    public partial class FrmFleetEmail : Form
    {

        public string fleetID
        {
            set { tbFleetID.Text = value; }
            get { return tbFleetID.Text; }
        }

        private string _email = "";

        public string fleetEmail
        {
            set { _email = value; tbFleetEmail.Text = value; }
            get { return tbFleetEmail.Text.Trim(); }
        }

        private ListBox lbEmails;

        public ListBox EMails
        {
            set { lbEmails = value; }
        }

        private bool _needSave = false;

        public bool needSave
        {
            get { return _needSave; }
        }

        public FrmFleetEmail(Font mainFont, Icon mainIcon)
        {
            this.Font = mainFont;
            this.Icon = mainIcon;

            InitializeComponent();
        }

        private void FrmFleetEmail_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
                return;

            //Check email

            if (_email.ToLower() != tbFleetEmail.Text.Trim().ToLower())
            {
                ListBox.ObjectCollection items = lbEmails.Items;

                foreach (string item in items)
                {
                    if (item.ToLower()==tbFleetEmail.Text.Trim().ToLower())
                    {
                        MessageBox.Show("Email address \"" + tbFleetEmail.Text.Trim() + "\" already in the list. \n" +
                            "Please provide another address or click Cancel button.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                        return;
                    }
                }

                _needSave = true;
            }

            string s = tbFleetEmail.Text.Trim();

            if (s.Contains("<"))
            {
                int start = s.IndexOf("<") + 1;
                int end = s.LastIndexOf(">");

                if (end<0)
                {
                    tbFleetEmail.Text = tbFleetEmail.Text.Trim() + ">";
                    tbFleetEmail.Update();
                    s = tbFleetEmail.Text.Trim();
                    end = s.LastIndexOf(">");
                }

                s = s.Substring(start, end - start);
            }

            if (s.Length > 0)
            {
                EmailAddressAttribute em = new EmailAddressAttribute();

                if (!em.IsValid(s))
                {
                    var rslt = MessageBox.Show("Email addres is not valid. Would you like to save it anyway?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (rslt != DialogResult.Yes)
                        e.Cancel = true;
                }
            }
        }
    }
}
