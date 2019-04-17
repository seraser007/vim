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
    public partial class FormOptions : Form
    {
        public bool allowedToAll
        {
            get { return chbAnyUser.Checked; }
            set { chbAnyUser.Checked = value; }
        }

        public bool onePowerFont
        {
            get { return chbSameFont.Checked; }
            set { chbSameFont.Checked = value; }
        }

        public FormOptions(Icon mainIcon, Font mainFont)
        {
            this.Icon = mainIcon;
            this.Font = mainFont;

            InitializeComponent();

            RefreshFontInfo();
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = this.Font;

            if (fontDialog1.ShowDialog()==DialogResult.OK)
            {
                this.Font = fontDialog1.Font;

                RefreshFontInfo();
            }
        }

        private void RefreshFontInfo()
        {
            lblFontName.Text = this.Font.Name;
            lblFontSize.Text = this.Font.Size.ToString();
            lblBold.Text = this.Font.Bold.ToString();
            lblItalic.Text = this.Font.Italic.ToString();
            lblUnderline.Text = this.Font.Underline.ToString();
        }
    }
}
