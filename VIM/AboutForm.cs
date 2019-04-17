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
    public partial class AboutForm : Form
    {
        public AboutForm(string appName, Icon mainIcon, Font maniFont, string licenseString, string licenseOwner, DateTime licenseExpire)
        {
            InitializeComponent();

            this.Icon = mainIcon;
            

            lblAppName.Text = appName;
            lblAppName.Parent = pictureBox1;

            if (MainForm.betaString.Length == 0)
                lblVersion.Text = "Version " + Application.ProductVersion;
            else
                lblVersion.Text = "Version " + Application.ProductVersion + " Beta " + MainForm.betaString;

            lblVersion.Parent = pictureBox1;

            lblOwner.Text = "License owner : " + licenseOwner;
            lblExpire.Text = "License valid till : " + licenseExpire.ToShortDateString();

            lblOwner.Parent = pictureBox1;
            lblExpire.Parent = pictureBox1;
            btnLicense.Parent = pictureBox1;
        }

        private void btnLicense_Click(object sender, EventArgs e)
        {
            LicenseForm lForm = new LicenseForm(this.Icon, this.Font, "", "", "");

            var rslt = lForm.ShowDialog();

            if (rslt==DialogResult.OK)
            {
                //Save license key
            }
        }
    }
}
