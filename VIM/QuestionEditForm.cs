using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VIM
{
    public partial class QuestionEditForm : Form
    {
        public string localString;

        public QuestionEditForm(Font mainFont, Icon mainIcon)
        {
            this.Font = mainFont;
            this.Icon = mainIcon;
            
            InitializeComponent();

            ProtectFields(MainForm.isPowerUser);
        }

        private void ProtectFields(bool status)
        {
            tbQuestionGUID.ReadOnly = !status;
            tbQuestionnaireGUID.ReadOnly = !status;
            tbQuestionNumber.ReadOnly = !status;
            tbQuestionSequence.ReadOnly = !status;
            tbQuestionText.ReadOnly = !status;

            cbChapterName.Enabled = status;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
