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
    public partial class FormObsView : Form
    {
        public string txtObservation
        {
            set { rtbObservation.Text = value; }
        }

        public string txtComments
        {
            set { rtbComments.Text = value; }
        }
        
        private List<string> fonts = new List<string>();

        public string questionText
        {
            set { tbQuestion.Text = value; }
        }

        public FormObsView(Icon mainIcon, Font mainFont)
        {
            InitializeComponent();

            this.Icon = mainIcon;
            this.Font = mainFont;
            
            if (!MainForm.isPowerUser)
            {
                toolStrip1.Visible = false;
                toolStrip2.Visible = false;
            }

            foreach (FontFamily font in System.Drawing.FontFamily.Families)
            {
                tcbFontsObs.Items.Add(font.Name);
                tcbFontsComm.Items.Add(font.Name);
            }

            LoadSettings();

            rtbComments.BackColor = SystemColors.Window;
            rtbObservation.BackColor = SystemColors.Window;

            tcbFontsObs.Text = rtbObservation.Font.Name;
            tcbSizeObs.Text = rtbObservation.Font.Size.ToString();

            tbtnColorObs.ForeColor = rtbObservation.ForeColor;

            tbtnBoldObs.Checked = rtbObservation.Font.Bold;
            tbtnItalicObs.Checked = rtbObservation.Font.Italic;
            tbtnUnderObs.Checked = rtbObservation.Font.Underline;
            tbtnStrikeoutObs.Checked = rtbObservation.Font.Strikeout;

            tcbFontsComm.Text = rtbComments.Font.Name;
            tcbSizeComm.Text = rtbComments.Font.Size.ToString();

            tbtnColorComm.ForeColor = rtbComments.ForeColor;

            tbtnBoldComm.Checked = rtbComments.Font.Bold;
            tbtnItalicComm.Checked = rtbComments.Font.Italic;
            tbtnUnderComm.Checked = rtbComments.Font.Underline;
            tbtnStrikeoutComm.Checked = rtbComments.Font.Strikeout;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = rtbComments.Font;

            var rslt = fontDialog1.ShowDialog();

            if (rslt==DialogResult.OK)
            {
                rtbComments.Font = fontDialog1.Font;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = rtbObservation.ForeColor;

            if (colorDialog1.ShowDialog()==DialogResult.OK)
            {
                tbtnColorObs.ForeColor = colorDialog1.Color;
                rtbObservation.ForeColor = colorDialog1.Color;
            }
        }

        private void tcbSizeObs_TextChanged(object sender, EventArgs e)
        {
            float newSize = (float)Convert.ToDecimal(tcbSizeObs.Text);

            if (newSize > 0)
                rtbObservation.Font = new Font(rtbObservation.Font.Name, newSize, rtbObservation.Font.Style);
        }

        private void tcbFontsObs_TextChanged(object sender, EventArgs e)
        {
            if (rtbObservation.Font.Name!=tcbFontsObs.Text)
            {
                if (tcbFontsObs.Text.Length>0)
                    rtbObservation.Font = new Font(tcbFontsObs.Text, rtbObservation.Font.Size, rtbObservation.Font.Style);
            }
        }

        private void tbtnBoldObs_Click(object sender, EventArgs e)
        {
            if (rtbObservation.Font.Bold)
                rtbObservation.Font = new Font(rtbObservation.Font.Name, rtbObservation.Font.Size, rtbObservation.Font.Style & ~FontStyle.Bold);
            else
                rtbObservation.Font = new Font(rtbObservation.Font.Name, rtbObservation.Font.Size, rtbObservation.Font.Style | FontStyle.Bold);

            tbtnBoldObs.Checked = rtbObservation.Font.Bold;
        }

        private void tbtnItalicObs_Click(object sender, EventArgs e)
        {
            if (rtbObservation.Font.Italic)
                rtbObservation.Font = new Font(rtbObservation.Font.Name, rtbObservation.Font.Size, rtbObservation.Font.Style & ~FontStyle.Italic);
            else
                rtbObservation.Font = new Font(rtbObservation.Font.Name, rtbObservation.Font.Size, rtbObservation.Font.Style | FontStyle.Italic);

            tbtnItalicObs.Checked = rtbObservation.Font.Italic;
        }

        private void tbtnUnderObs_Click(object sender, EventArgs e)
        {
            if (rtbObservation.Font.Underline)
                rtbObservation.Font = new Font(rtbObservation.Font.Name, rtbObservation.Font.Size, rtbObservation.Font.Style & ~FontStyle.Underline);
            else
                rtbObservation.Font = new Font(rtbObservation.Font.Name, rtbObservation.Font.Size, rtbObservation.Font.Style | FontStyle.Underline);

            tbtnUnderObs.Checked = rtbObservation.Font.Underline;
        }

        private void tbtnColorComm_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = rtbComments.ForeColor;

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                tbtnColorComm.ForeColor = colorDialog1.Color;
                rtbComments.ForeColor = colorDialog1.Color;
            }
        }

        private void tbtnBoldComm_Click(object sender, EventArgs e)
        {
            if (rtbComments.Font.Bold)
                rtbComments.Font = new Font(rtbComments.Font.Name, rtbComments.Font.Size, rtbComments.Font.Style & ~FontStyle.Bold);
            else
                rtbComments.Font = new Font(rtbComments.Font.Name, rtbComments.Font.Size, rtbComments.Font.Style | FontStyle.Bold);

            tbtnBoldComm.Checked = rtbComments.Font.Bold;
        }

        private void tbtnItalicComm_Click(object sender, EventArgs e)
        {
            if (rtbComments.Font.Italic)
                rtbComments.Font = new Font(rtbComments.Font.Name, rtbComments.Font.Size, rtbComments.Font.Style & ~FontStyle.Italic);
            else
                rtbComments.Font = new Font(rtbComments.Font.Name, rtbComments.Font.Size, rtbComments.Font.Style | FontStyle.Italic);

            tbtnItalicComm.Checked = rtbComments.Font.Italic;
        }

        private void tbtnUnderComm_Click(object sender, EventArgs e)
        {
            if (rtbComments.Font.Underline)
                rtbComments.Font = new Font(rtbComments.Font.Name, rtbComments.Font.Size, rtbComments.Font.Style & ~FontStyle.Underline);
            else
                rtbComments.Font = new Font(rtbComments.Font.Name, rtbComments.Font.Size, rtbComments.Font.Style | FontStyle.Underline);

            tbtnUnderComm.Checked = rtbComments.Font.Underline;
        }

        private void SaveSettings()
        {
            if (MainForm.isPowerUser || MainForm.anyUserMayChangeFont)
            {
                try
                {
                    string fileName = "";

                    if ((!MainForm.isPowerUser && MainForm.anyUserMayChangeFont) || (MainForm.isPowerUser && !MainForm.oneFontForPowerUsers))
                    {
                        /*
                        if (MainForm.appDataFolder.Length == 0 || !Directory.Exists(MainForm.appDataFolder))
                        {
                            MainForm.SetAppDataFolder();

                            if (MainForm.appDataFolder.Length == 0 || !Directory.Exists(MainForm.appDataFolder))
                            {
                                MessageBox.Show("Unable to locate data folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        */

                        fileName = Path.Combine(MainForm.serviceFolder, "Settings_" + MainForm.userName + ".ini");
                    }
                    else
                    {
                        fileName = Path.Combine(MainForm.serviceFolder, "Settings_Common.ini");
                    }

                    IniFile iniFile = new IniFile(fileName);

                    string section = "Observation viewer";
                    string iniKey = "";

                    iniKey = "Observation";

                    iniFile.Write(section, iniKey + ".FontName", rtbObservation.Font.Name);
                    iniFile.Write(section, iniKey + ".FontSize", rtbObservation.Font.Size.ToString());
                    iniFile.Write(section, iniKey + ".FontColor", ColorToHex(rtbObservation.ForeColor));
                    iniFile.Write(section, iniKey + ".Bold", rtbObservation.Font.Bold);
                    iniFile.Write(section, iniKey + ".Italic", rtbObservation.Font.Italic);
                    iniFile.Write(section, iniKey + ".Underline", rtbObservation.Font.Underline);
                    iniFile.Write(section, iniKey + ".Strikeout", rtbObservation.Font.Strikeout);

                    iniKey = "Comments";
                    iniFile.Write(section, iniKey + ".FontName", rtbComments.Font.Name);
                    iniFile.Write(section, iniKey + ".FontSize", rtbComments.Font.Size.ToString());
                    iniFile.Write(section, iniKey + ".FontColor", ColorToHex(rtbComments.ForeColor));
                    iniFile.Write(section, iniKey + ".Bold", rtbComments.Font.Bold);
                    iniFile.Write(section, iniKey + ".Italic", rtbComments.Font.Italic);
                    iniFile.Write(section, iniKey + ".Underline", rtbComments.Font.Underline);
                    iniFile.Write(section, iniKey + ".Strikeout", rtbComments.Font.Strikeout);

                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadSettings()
        {
            string obsFontName = "Calibri";
            float obsFontSize = 12;
            bool obsFontBold = false;
            bool obsFontItalic = false;
            bool obsFontUnderline = false;
            bool obsFontStrikeout = false;
            Color obsColor = Color.Red;
            FontStyle obsFontStyle = FontStyle.Regular;

            string commFontName = "Calibri";
            float commFontSize = 12;
            bool commFontBold = false;
            bool commFontItalic = false;
            bool commFontUnderline = false;
            bool commFontStrikeout = false;
            Color commColor = Color.Blue;
            FontStyle commFontStyle = FontStyle.Regular;

            string fileName="";

            if ((!MainForm.isPowerUser && MainForm.anyUserMayChangeFont) || (MainForm.isPowerUser && !MainForm.oneFontForPowerUsers))
            {
                /*
                if (MainForm.appDataFolder.Length == 0 || !Directory.Exists(MainForm.appDataFolder))
                {
                    MainForm.SetAppDataFolder();

                    if (MainForm.appDataFolder.Length == 0 || !Directory.Exists(MainForm.appDataFolder))
                    {
                        MessageBox.Show("Unable to locate data folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                */

                fileName = Path.Combine(MainForm.serviceFolder, "Settings_" + MainForm.userName + ".ini");
            }
            else
            {
                fileName=Path.Combine(MainForm.serviceFolder,"Settings_Common.ini"); 
            }

            IniFile iniFile = new IniFile(fileName);

            string section = "Observation viewer";
            string iniKey = "Observation";

            obsFontName = iniFile.ReadString(section, iniKey + ".FontName", obsFontName);
            obsFontSize = Convert.ToSingle(iniFile.ReadString(section, iniKey + ".FontSize", obsFontSize.ToString()));
            obsColor = HexToColor(iniFile.ReadString(section, iniKey + ".FontColor", ColorToHex(obsColor)));
            obsFontBold = iniFile.ReadBoolean(section, iniKey + ".Bold", obsFontBold);
            obsFontItalic = iniFile.ReadBoolean(section, iniKey + ".Italic", obsFontItalic);
            obsFontUnderline = iniFile.ReadBoolean(section, iniKey + ".Underline", obsFontUnderline);
            obsFontStrikeout = iniFile.ReadBoolean(section, iniKey + ".Strikeout", obsFontStrikeout);

            if (obsFontBold)
                obsFontStyle = obsFontStyle | FontStyle.Bold;

            if (obsFontItalic)
                obsFontStyle = obsFontStyle | FontStyle.Italic;

            if (obsFontUnderline)
                obsFontStyle = obsFontStyle | FontStyle.Underline;

            if (obsFontStrikeout)
                obsFontStyle = obsFontStyle | FontStyle.Strikeout;

            rtbObservation.Font = new Font(obsFontName, obsFontSize, obsFontStyle);
            rtbObservation.ForeColor = obsColor;

            iniKey = "Comments";

            commFontName = iniFile.ReadString(section, iniKey + ".FontName", commFontName);
            commFontSize = Convert.ToSingle(iniFile.ReadString(section, iniKey + ".FontSize", commFontSize.ToString()));
            commColor = HexToColor(iniFile.ReadString(section, iniKey + ".FontColor", ColorToHex(commColor)));
            commFontBold = iniFile.ReadBoolean(section, iniKey + ".Bold", commFontBold);
            commFontItalic = iniFile.ReadBoolean(section, iniKey + ".Italic", commFontItalic);
            commFontUnderline = iniFile.ReadBoolean(section, iniKey + ".Underline", commFontUnderline);
            commFontStrikeout = iniFile.ReadBoolean(section, iniKey + ".Strikeout", commFontStrikeout);

            if (commFontBold)
                commFontStyle = commFontStyle | FontStyle.Bold;

            if (commFontItalic)
                commFontStyle = commFontStyle | FontStyle.Italic;

            if (commFontUnderline)
                commFontStyle = commFontStyle | FontStyle.Underline;

            if (commFontStrikeout)
                commFontStyle = commFontStyle | FontStyle.Strikeout;

            rtbComments.Font = new Font(commFontName, commFontSize, commFontStyle);
            rtbComments.ForeColor = commColor;
        }

        private static String ColorToHex(System.Drawing.Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        private static Color HexToColor(string hex)
        {
            if (hex.StartsWith("#"))
                return ColorTranslator.FromHtml(hex);
            else
                return Color.Black;
        }

        private void FormObsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            //SaveSettings();

            Properties.Settings.Default.FormObsViewState = this.WindowState;
            if (this.WindowState == FormWindowState.Normal)
            {
                // save location and size if the state is normal
                Properties.Settings.Default.FormObsViewLocation = this.Location;
                Properties.Settings.Default.FormObsViewSize = this.Size;
            }
            else
            {
                // save the RestoreBounds if the form is minimized or maximized!
                Properties.Settings.Default.FormObsViewLocation = this.RestoreBounds.Location;
                Properties.Settings.Default.FormObsViewSize = this.RestoreBounds.Size;
            }

            Properties.Settings.Default.FormObsViewSplitterDistance = splitContainer1.SplitterDistance;

            // don't forget to save the settings
            Properties.Settings.Default.Save();
        }

        private void tcbSizeComm_TextChanged(object sender, EventArgs e)
        {
            float newSize = (float)Convert.ToDecimal(tcbSizeComm.Text);

            if (newSize > 0)
                rtbComments.Font = new Font(rtbComments.Font.Name, newSize, rtbComments.Font.Style);
        }

        private void tcbFontsObs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rtbObservation.Font.Name != tcbFontsObs.Text)
            {
                if (tcbFontsObs.Text.Length > 0)
                    rtbObservation.Font = new Font(tcbFontsObs.Text, rtbObservation.Font.Size, rtbObservation.Font.Style);
            }
        }

        private void tcbFontsComm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rtbComments.Font.Name != tcbFontsComm.Text)
            {
                if (tcbFontsComm.Text.Length > 0)
                    rtbComments.Font = new Font(tcbFontsComm.Text, rtbComments.Font.Size, rtbComments.Font.Style);
            }

        }

        private void FormObsView_Load(object sender, EventArgs e)
        {
            // Upgrade?
            if (Properties.Settings.Default.FormObsViewSize.Width == 0) Properties.Settings.Default.Upgrade();

            if (Properties.Settings.Default.FormObsViewSize.Width == 0 || Properties.Settings.Default.FormObsViewSize.Height == 0)
            {
                // first start
                // optional: add default values
            }
            else
            {
                this.WindowState = Properties.Settings.Default.FormObsViewState;

                // we don't want a minimized window at startup
                if (this.WindowState == FormWindowState.Minimized) this.WindowState = FormWindowState.Normal;

                this.Location = Properties.Settings.Default.FormObsViewLocation;
                this.Size = Properties.Settings.Default.FormObsViewSize;
                splitContainer1.SplitterDistance = Properties.Settings.Default.FormObsViewSplitterDistance;
            }
        }

        private void tbtnSaveObs_Click(object sender, EventArgs e)
        {
            SaveObsFont();
        }

        private void SaveObsFont()
        {
            try
            {
                string fileName = "";

                if ((!MainForm.isPowerUser && MainForm.anyUserMayChangeFont) || (MainForm.isPowerUser && !MainForm.oneFontForPowerUsers))
                {
                    /*
                    if (MainForm.appDataFolder.Length == 0 || !Directory.Exists(MainForm.appDataFolder))
                    {
                        MainForm.SetAppDataFolder();

                        if (MainForm.appDataFolder.Length == 0 || !Directory.Exists(MainForm.appDataFolder))
                        {
                            MessageBox.Show("Unable to locate data folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    */

                    fileName = Path.Combine(MainForm.serviceFolder, "Settings_" + MainForm.userName + ".ini");
                }
                else
                {
                    fileName = Path.Combine(MainForm.serviceFolder, "Settings_Common.ini");
                }

                IniFile iniFile = new IniFile(fileName);

                string section = "Observation viewer";
                string iniKey = "";

                iniKey = "Observation";

                iniFile.Write(section, iniKey + ".FontName", rtbObservation.Font.Name);
                iniFile.Write(section, iniKey + ".FontSize", rtbObservation.Font.Size.ToString());
                iniFile.Write(section, iniKey + ".FontColor", ColorToHex(rtbObservation.ForeColor));
                iniFile.Write(section, iniKey + ".Bold", rtbObservation.Font.Bold);
                iniFile.Write(section, iniKey + ".Italic", rtbObservation.Font.Italic);
                iniFile.Write(section, iniKey + ".Underline", rtbObservation.Font.Underline);
                iniFile.Write(section, iniKey + ".Strikeout", rtbObservation.Font.Strikeout);

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbtnSaveComm_Click(object sender, EventArgs e)
        {
            SaveCommFont();
        }

        private void SaveCommFont()
        {
            try
            {
                string fileName = "";

                if ((!MainForm.isPowerUser && MainForm.anyUserMayChangeFont) || (MainForm.isPowerUser && !MainForm.oneFontForPowerUsers))
                {
                    /*
                    if (MainForm.appDataFolder.Length == 0 || !Directory.Exists(MainForm.appDataFolder))
                    {
                        MainForm.SetAppDataFolder();

                        if (MainForm.appDataFolder.Length == 0 || !Directory.Exists(MainForm.appDataFolder))
                        {
                            MessageBox.Show("Unable to locate data folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    */

                    fileName = Path.Combine(MainForm.serviceFolder, "Settings_" + MainForm.userName + ".ini");
                }
                else
                {
                    fileName = Path.Combine(MainForm.serviceFolder, "Settings_Common.ini");
                }

                IniFile iniFile = new IniFile(fileName);

                string section = "Observation viewer";
                string iniKey = "";

                iniKey = "Comments";
                iniFile.Write(section, iniKey + ".FontName", rtbComments.Font.Name);
                iniFile.Write(section, iniKey + ".FontSize", rtbComments.Font.Size.ToString());
                iniFile.Write(section, iniKey + ".FontColor", ColorToHex(rtbComments.ForeColor));
                iniFile.Write(section, iniKey + ".Bold", rtbComments.Font.Bold);
                iniFile.Write(section, iniKey + ".Italic", rtbComments.Font.Italic);
                iniFile.Write(section, iniKey + ".Underline", rtbComments.Font.Underline);
                iniFile.Write(section, iniKey + ".Strikeout", rtbComments.Font.Strikeout);

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbtnStrikeoutObs_Click(object sender, EventArgs e)
        {
            if (rtbObservation.Font.Strikeout)
                rtbObservation.Font = new Font(rtbObservation.Font.Name, rtbObservation.Font.Size, rtbObservation.Font.Style & ~FontStyle.Strikeout);
            else
                rtbObservation.Font = new Font(rtbObservation.Font.Name, rtbObservation.Font.Size, rtbObservation.Font.Style | FontStyle.Strikeout);

            tbtnStrikeoutObs.Checked = rtbObservation.Font.Strikeout;

        }

        private void tbtnStrikeoutComm_Click(object sender, EventArgs e)
        {
            if (rtbComments.Font.Strikeout)
                rtbComments.Font = new Font(rtbComments.Font.Name, rtbComments.Font.Size, rtbComments.Font.Style & ~FontStyle.Strikeout);
            else
                rtbComments.Font = new Font(rtbComments.Font.Name, rtbComments.Font.Size, rtbComments.Font.Style | FontStyle.Strikeout);

            tbtnStrikeoutComm.Checked = rtbComments.Font.Strikeout;

        }
    }
}
