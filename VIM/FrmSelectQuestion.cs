using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace VIM
{
    public partial class FrmSelectQuestion : Form
    {
        OleDbConnection connection;
        DataSet DS;
        string selectedGUID = "";
        string selectedNumber = "";
        string selectedSequence = "";
        BindingSource bs = new BindingSource();

        public string selectedQuestionGUID
        {
            get { return selectedGUID; }
        }

        public string selectedQuestionNumber
        {
            get { return selectedNumber; }
        }

        public string sequence
        {
            get { return selectedSequence; }
        }

        public FrmSelectQuestion(string templateGUID)
        {
            connection = MainForm.connection;
            DS = MainForm.DS;
            this.Icon = MainForm.mainIcon;
            this.Font = MainForm.mainFont;

            InitializeComponent();

            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select QUESTION_GUID, QUESTION_NUMBER, QUESTION_TEXT, SEQUENCE \n" +
                "from TEMPLATE_QUESTIONS \n" +
                "where TEMPLATE_GUID like '" + templateGUID + "' \n" +
                "order by SEQUENCE";

            OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("Q_SELECT"))
                DS.Tables["Q_SELECT"].Clear();

            adpt.Fill(DS, "Q_SELECT");

            bs.DataSource = DS;
            bs.DataMember = "Q_SELECT";
            dgvQuestions.DataSource = bs;

            dgvQuestions.AutoGenerateColumns = true;

            dgvQuestions.Columns["QUESTION_GUID"].Visible = false;
            dgvQuestions.Columns["SEQUENCE"].Visible = false;

            dgvQuestions.Columns["QUESTION_NUMBER"].HeaderText = "No.";
            dgvQuestions.Columns["QUESTION_NUMBER"].FillWeight = 10;

            dgvQuestions.Columns["QUESTION_TEXT"].HeaderText = "Question";
            dgvQuestions.Columns["QUESTION_TEXT"].FillWeight = 90;

            cbQuestions.DataSource = DS.Tables["Q_SELECT"];
            cbQuestions.DisplayMember = "QUESTION_NUMBER";

            //btnSearch.Height = cbQuestions.Height;
        }

        private void FrmSelectQuestion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
                return;

            selectedGUID = dgvQuestions.CurrentRow.Cells["QUESTION_GUID"].Value.ToString();
            selectedNumber = dgvQuestions.CurrentRow.Cells["QUESTION_NUMBER"].Value.ToString();
            selectedSequence = dgvQuestions.CurrentRow.Cells["SEQUENCE"].Value.ToString();
        }

        private void dgvQuestions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LocateQuestion();
        }

        private void LocateQuestion()
        {
            String searchValue = cbQuestions.Text;

            int rowIndex = -1;

            foreach (DataGridViewRow dgRow in dgvQuestions.Rows)
            {
                if (dgRow.Cells["QUESTION_NUMBER"].Value.ToString().Equals(searchValue))
                {
                    rowIndex = dgRow.Index;
                    break;
                }
            }

            if (rowIndex >= 0)
            {
                //dataGridView1.Rows[rowIndex].Selected = true;
                dgvQuestions.CurrentCell = dgvQuestions["QUESTION_NUMBER", rowIndex];
            }
        }

        private void cbQuestions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cbQuestions.Text==dgvQuestions.CurrentRow.Cells["QUESTION_NUMBER"].Value.ToString())
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                    LocateQuestion();
            }
        }

        private void dgvQuestions_FilterStringChanged(object sender, EventArgs e)
        {
            bs.Filter = dgvQuestions.FilterString;
        }

        private void dgvQuestions_SortStringChanged(object sender, EventArgs e)
        {
            bs.Sort = dgvQuestions.SortString;
        }

    }
}
