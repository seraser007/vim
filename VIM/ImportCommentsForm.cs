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
    public partial class ImportCommentsForm : Form
    {
        public static string originalText="";
        public ImportCommentsForm(Icon mainIcon, Font mainFont, string mainOriginalText)
        {
            this.Icon=mainIcon;
            this.Font=mainFont;
            originalText = mainOriginalText;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tbOriginal.Text = tbNew.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tbOriginal.Text = tbOriginal.Text + " \r\n" + tbNew.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tbOriginal.Undo();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tbOriginal.Text = originalText;
        }

        private void ImportCommentsForm_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = button2;
        }
    }
}
