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
using System.Data.OleDb;

namespace VIM
{
    public partial class frmMonthlyBulletin : Form
    {
        string templatePath;
        //int _inspectionType = 0;
        DataSet DS;
        OleDbConnection connection;

        public string templateName
        {
            get { return Path.Combine(templatePath,tbFileName.Text.Trim()); }
            set { tbFileName.Text = Path.GetFileName(value); }
        }

        public string subject
        {
            get { return tbSubject.Text.Trim(); }
            set { tbSubject.Text = value; }
        }

        public DateTime dateIssue
        {
            get { return dtIssue.Value; }
            set { dtIssue.Value = value; }
        }

        public DateTime dateFrom
        {
            get { return dtFrom.Value; }
            set { dtFrom.Value = value; }
        }

        public DateTime dateTill
        {
            get { return dtTill.Value; }
            set { dtTill.Value = value; }
        }

        public bool useMonth
        {
            get { return rbtnMonth.Checked; }
            set { rbtnMonth.Checked = value; rbtnPeriod.Checked = !rbtnMonth.Checked; }
        }

        public bool includePeriod
        {
            get { return chbIncludePeriod.Checked; }
            set { chbIncludePeriod.Checked = value; }
        }

        public int selectedYear { get; set; }
        public int selectedMonth { get; set; }

        public bool useComments
        {
            get { return chbUseComments.Checked; }
            set { chbUseComments.Checked = value; }
        }

        public string commentsLabel
        {
            get { return tbLabel.Text; }
            set { tbLabel.Text = value; }
        }

        public string commentsText
        {
            get { return tbComments.Text; }
            set { tbComments.Text = value; }
        }

        public bool useCommonComments
        {
            get { return rbtnCommonComments.Checked; }
            set { rbtnCommonComments.Checked = value; rbtnOperatorComments.Checked = !rbtnCommonComments.Checked; }
        }

        public bool useSelectedColor
        {
            get { return rbtnSelectedColor.Checked; }
            set { rbtnSelectedColor.Checked = value; rbtnSelectedStyle.Checked = !rbtnSelectedColor.Checked; }
        }

        public string selectedColorRGB
        {
            get { return "RGB(" + tbTextColor.ForeColor.R.ToString() + "," + tbTextColor.ForeColor.G.ToString() + "," + tbTextColor.ForeColor.B.ToString() + ")"; }
            set { tbTextColor.ForeColor = ParseColor(value); }
        }

        public string selectedStyle
        {
            get { return tbSelectedStyle.Text.Trim(); }
            set { tbSelectedStyle.Text = value; }
        }

        public Color selectedColor
        {
            get { return tbTextColor.ForeColor; }
            set { tbTextColor.ForeColor = value; }
        }
        
        public int inspectionType
        {
            get { return Convert.ToInt32(cbInspectionType.SelectedValue); }
            set {
                if (InspectionTypeExists(value))
                    cbInspectionType.SelectedValue = value;
                else
                    cbInspectionType.SelectedValue = 1;
                }
        }

        public frmMonthlyBulletin()
        {
            this.Font = MainForm.mainFont;
            this.Icon = MainForm.mainIcon;

            DS = MainForm.DS;
            connection = MainForm.connection;

            templatePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\Templates";
            
            InitializeComponent();

            FillInspectionTypes();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = templatePath;

            var rslt = openFileDialog1.ShowDialog();

            if (rslt==DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;

                string filePath = Path.GetDirectoryName(fileName);

                if (templatePath.Contains(filePath))
                {
                    if (File.Exists(fileName))
                    {
                        tbFileName.Text = Path.GetFileName(fileName);
                    }
                    else
                    {
                        MessageBox.Show("File \"" + fileName + "\" was not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Template file should be located in the folder \"" + templatePath + "\".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FillInspectionTypes()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select INSPECTION_TYPE_ID, INSPECTION_TYPE \n" +
                "from INSPECTION_TYPES \n" +
                "union \n" +
                "select TOP 1 0 as INSPECTION_TYPE_ID, 'All' as INSPECTION_TYPE \n" +
                "from INSPECTION_TYPES \n" +
                "order by INSPECTION_TYPE";

            if (DS.Tables.Contains("INSPECTION_TYPE_LIST"))
                DS.Tables["INSPECTION_TYPE_LIST"].Clear();

            OleDbDataAdapter da = new OleDbDataAdapter(cmd);

            da.Fill(DS, "INSPECTION_TYPE_LIST");

            cbInspectionType.DataSource = DS.Tables["INSPECTION_TYPE_LIST"];
            cbInspectionType.DisplayMember = "INSPECTION_TYPE";
            cbInspectionType.ValueMember = "INSPECTION_TYPE_ID";
        }

        private void frmMonthlyBulletin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {

                //Check all parameters

                if (cbInspectionType.SelectedValue==null || cbInspectionType.SelectedValue==System.DBNull.Value)
                {
                    MessageBox.Show("Please select inspection type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }

                if (tbFileName.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Please provide template file name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }

                string templateFile = Path.Combine(templatePath, tbFileName.Text.Trim());

                if (!File.Exists(templateFile))
                {
                    MessageBox.Show("File \"" + templateFile + "\" does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }

                if (DateTime.Compare(dtFrom.Value, dtTill.Value) > 0)
                {
                    MessageBox.Show("Start date should be less or equal till date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }

                selectedMonth = monthSlector.selectedMonth;
                selectedYear = monthSlector.selectedYear;
            }

            Properties.Settings.Default.MonthlyBulletinFormState = this.WindowState;

            if (this.WindowState == FormWindowState.Normal)
            {
                // save location and size if the state is normal
                Properties.Settings.Default.MonthlyBulletinFormLocation = this.Location;
                Properties.Settings.Default.MonthlyBulletinFormSize = this.Size;
            }
            else
            {
                // save the RestoreBounds if the form is minimized or maximized!
                Properties.Settings.Default.MonthlyBulletinFormLocation = this.RestoreBounds.Location;
                Properties.Settings.Default.MonthlyBulletinFormSize = this.RestoreBounds.Size;
            }

            // don't forget to save the settings
            Properties.Settings.Default.Save();

        }

        private void dtFrom_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dtTill_ValueChanged(object sender, EventArgs e)
        {

        }

        private void frmMonthlyBulletin_Shown(object sender, EventArgs e)
        {
            if (useMonth)
                rbtnMonth.Checked = true;
            else
                rbtnPeriod.Checked = true;

            monthSlector.selectedYear = selectedYear;
            monthSlector.selectedMonth = selectedMonth;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Save defaults
            if (cbInspectionType.SelectedValue==null || cbInspectionType.SelectedValue==System.DBNull.Value)
            {
                MessageBox.Show("Please select inspection type","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            try
            {
                string iniFileName = Path.Combine(MainForm.serviceFolder, "Settings_Common.ini");
                
                //Use global section for bulletin settings
                string section = "MonthlyBulletin";

                IniFile iniFile = new IniFile(iniFileName);

                iniFile.Write(section, "UseMonth", rbtnMonth.Checked);
                iniFile.Write(section, "TemplateFile", tbFileName.Text.Trim());
                iniFile.Write(section, "Subject", tbSubject.Text.Trim());
                iniFile.Write(section, "IncludePeriod", chbIncludePeriod.Checked);
                iniFile.Write(section, "UseComments", chbUseComments.Checked);
                iniFile.Write(section, "CommentsLabel", tbLabel.Text);
                iniFile.Write(section, "CommentsText", tbComments.Text);
                iniFile.Write(section, "UseCommonComments", rbtnCommonComments.Checked);
                iniFile.Write(section, "UseSelectedColor", rbtnSelectedColor.Checked);
                iniFile.Write(section, "SelectedColor", GetRGBString(tbTextColor.ForeColor));
                iniFile.Write(section, "SelectedStyle", tbSelectedStyle.Text.Trim());
                iniFile.Write(section, "InspectionType", Convert.ToInt32(cbInspectionType.SelectedValue));

                MessageBox.Show("Monthly bulletin setting saved successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmMonthlyBulletin_Load(object sender, EventArgs e)
        {
            // Upgrade?
            if (Properties.Settings.Default.MonthlyBulletinFormSize.Width == 0) Properties.Settings.Default.Upgrade();

            if (Properties.Settings.Default.MonthlyBulletinFormSize.Width == 0 || Properties.Settings.Default.MonthlyBulletinFormSize.Height == 0)
            {
                // first start
                // optional: add default values
            }
            else
            {
                this.WindowState = Properties.Settings.Default.MonthlyBulletinFormState;

                // we don't want a minimized window at startup
                if (this.WindowState == FormWindowState.Minimized) this.WindowState = FormWindowState.Normal;

                this.Location = Properties.Settings.Default.MonthlyBulletinFormLocation;
                this.Size = Properties.Settings.Default.MonthlyBulletinFormSize;
            }

        }

        private void btnTextColor_Click_1(object sender, EventArgs e)
        {
            colorDialog1.Color = tbTextColor.ForeColor;
            var rslt = colorDialog1.ShowDialog();

            if (rslt==DialogResult.OK)
            {
                tbTextColor.ForeColor = colorDialog1.Color;
            }
        }

        private Color ParseColor(string rgbColor)
        {
            if (rgbColor==null || rgbColor.Length==0 || !rgbColor.StartsWith("RGB("))
                return SystemColors.WindowText;

            string s = rgbColor.Substring(4).Trim();
            s = s.Substring(0, s.Length - 1);

            int index = 0;
            string x = "";
            int R = 0;
            int G = 0;
            int B = 0;

            for (int i=0;i<s.Length;i++)
            {
                if (s[i] != ',' && i<s.Length)
                    x = x + s[i];
                else
                {
                    switch (index)
                    {
                        case 0:
                            R = Convert.ToInt16(x);

                            if (R < 0) R = 0;

                            if (R > 255) R = 255;

                            x = "";
                            index++;
                            break;
                        case 1:
                            G = Convert.ToInt16(x);

                            if (G < 0) G = 0;
                            if (G > 255) G = 255;

                            x = "";
                            index++;
                            break;
                        case 2:
                            B = Convert.ToInt16(x);

                            if (B < 0) B = 0;
                            if (B > 255) B = 255;

                            x = "";
                            index++;
                            break;
                    }
                }
            }

            if (x.Length>0 && index==2)
            {
                B = Convert.ToInt16(x);

                if (B < 0) B = 0;
                if (B > 255) B = 255;
            }

            return Color.FromArgb(R, G, B);
        }

        private string GetRGBString(Color color)
        {
            string s = "RGB(";

            s = s + color.R.ToString();
            s = s + "," + color.G.ToString();
            s = s + "," + color.B.ToString() + ")";

            return s;
        }

        private bool InspectionTypeExists(int aType)
        {
            if (aType < 0)
                return false;

            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select Count(INSPECTION_TYPE_ID) as RecCount \n" +
                "from INSPECTION_TYPES \n" +
                "where INSPECTION_TYPE_ID=" + aType.ToString();

            object rslt = cmd.ExecuteScalar();

            if (rslt == System.DBNull.Value)
                return false;
            else
            {
                int i = Convert.ToInt32(rslt);

                if (i == 0)
                    return false;
                else
                    return true;
            }
        }
    }
}
