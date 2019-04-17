using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VIM
{
    public partial class CommentsForm : Form
    {
        public CommentsForm(Font mainFont)
        {
            this.Font = mainFont;

            InitializeComponent();

            tbComments.ReadOnly = !MainForm.isPowerUser;
        }

        private void CommentsForm_Shown(object sender, EventArgs e)
        {
            tbComments.SelectionLength = 0;
        }
    }
}
