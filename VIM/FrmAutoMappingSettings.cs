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
    public partial class FrmAutoMappingSettings : Form
    {
        private string section = "";

        public FrmAutoMappingSettings()
        {
            this.Icon = MainForm.mainIcon;
            this.Font = MainForm.mainFont;

            InitializeComponent();

            section = "AutoMappingSettings";
            
            chbIgnore.Checked = MainForm.StrToBool(MainForm.ReadIniValue(MainForm.iniPersonalFile, section, "IgnoreSigns", "False"));

            chbClearMapping.Checked = MainForm.StrToBool(MainForm.ReadIniValue(MainForm.iniPersonalFile, section, "ClearMapping", "False"));

            tbSigns.Text = MainForm.ReadIniValue(MainForm.iniPersonalFile, section, "Signs");

        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            MainForm.WriteIniValue(MainForm.iniPersonalFile, section, "IgnoreSigns", MainForm.BoolToStr(chbIgnore.Checked));

            MainForm.WriteIniValue(MainForm.iniPersonalFile, section, "ClearMapping", MainForm.BoolToStr(chbClearMapping.Checked));

            MainForm.WriteIniValue(MainForm.iniPersonalFile, section, "Signs", tbSigns.Text);
        }


    }
}
