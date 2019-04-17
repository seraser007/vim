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
    public partial class LicenseForm : Form
    {
        public LicenseForm(Icon mainIcon, Font mainFont,string licenseKey, string owner, string validTill)
        {
            this.Icon = mainIcon;
            this.Font = mainFont;

            InitializeComponent();

            tbLicense.Text = licenseKey;
            tbOwner.Text = owner;
            tbValidTill.Text = validTill;
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            tbLicense.Paste();
        }
    }
}
