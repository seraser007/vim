using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace VIM
{
    public partial class QuestionnaireViewForm : Form
    {
        OleDbConnection connection;
        DataSet DS;
        bool isRoVIQ;
        string tableName;
        string templateGUID;
        string version;
        string typeCode;

        public QuestionnaireViewForm(bool roVIQ, string mainTemplateGUID, string mainVersion, string mainTypeCode)
        {
            this.Font = MainForm.mainFont;
            this.Icon = MainForm.mainIcon;
            connection = MainForm.connection;
            DS = MainForm.DS;

            isRoVIQ = roVIQ;
            templateGUID = mainTemplateGUID;
            version = mainVersion;
            typeCode = mainTypeCode;

            InitializeComponent();

            tableName = "";

            if (isRoVIQ)
                tableName = "RTEMPLATE_QUESTIONS";
            else
                tableName = "TEMPLATE_QUESTIONS";

            fillQuestions();
            FillChapters();

            ProtectFields(MainForm.isPowerUser);
        }

        private void ProtectFields(bool status)
        {
            btnNew.Enabled = status;
            btnDelete.Enabled = status;

            if (status)
            {
                btnEdit.Text = "Edit";
                btnEdit.Image = VIM.Properties.Resources.edit;
            }
            else
            {
                btnEdit.Text = "View";
                btnEdit.Image = VIM.Properties.Resources.normal_view;
            }
        }

        private void fillQuestions()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                 "SELECT CHAPTER_NUMBER, QUESTION_NUMBER, QUESTION_TEXT, SEQUENCE, QUESTION_GUID, \n" +
                 "TEMPLATE_GUID, CHAPTER_NAME, SECTION_NUMBER, SECTION_NAME \n" +
                 "FROM [" + tableName + "] \n" +
                 "where TEMPLATE_GUID={" + templateGUID + "} \n" +
                 "order by SEQUENCE";

            OleDbDataAdapter tAdapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("QUESTIONNAIRE_VIEW"))
                DS.Tables["QUESTIONNAIRE_VIEW"].Dispose();

            tAdapter.Fill(DS, "QUESTIONNAIRE_VIEW");

            dgvQuestions.DataSource = DS;
            dgvQuestions.AutoGenerateColumns = true;
            dgvQuestions.DataMember = "QUESTIONNAIRE_VIEW";

            dgvQuestions.Columns["CHAPTER_NUMBER"].HeaderText = "Chapter";
            dgvQuestions.Columns["CHAPTER_NUMBER"].FillWeight = 5;

            dgvQuestions.Columns["QUESTION_NUMBER"].HeaderText = "Question number";
            dgvQuestions.Columns["QUESTION_NUMBER"].FillWeight = 10;

            dgvQuestions.Columns["QUESTION_TEXT"].HeaderText = "Question text";
            dgvQuestions.Columns["QUESTION_TEXT"].FillWeight = 100;

            dgvQuestions.Columns["SEQUENCE"].HeaderText = "Sequence";
            dgvQuestions.Columns["SEQUENCE"].FillWeight = 10;

            dgvQuestions.Columns["QUESTION_GUID"].HeaderText = "GUID";
            dgvQuestions.Columns["QUESTION_GUID"].FillWeight = 20;

            dgvQuestions.Columns["TEMPLATE_GUID"].Visible = false;
            dgvQuestions.Columns["CHAPTER_NAME"].Visible = false;
            dgvQuestions.Columns["SECTION_NUMBER"].Visible = false;
            dgvQuestions.Columns["SECTION_NAME"].Visible = false;
            //dgvQuestions.Columns["QUESTION_TEXT"].Visible = false;
            //dataGridView1.Columns[""].Visible = false;
            //dataGridView1.Columns["ID"].Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            editQuestion();
        }


        private void editQuestion()
        {
            QuestionEditForm qeForm = new QuestionEditForm(this.Font, this.Icon);

            qeForm.tbQuestionNumber.Text = dgvQuestions.CurrentRow.Cells["QUESTION_NUMBER"].Value.ToString();
            qeForm.tbQuestionGUID.Text = dgvQuestions.CurrentRow.Cells["QUESTION_GUID"].Value.ToString();
            qeForm.tbQuestionSequence.Text = dgvQuestions.CurrentRow.Cells["SEQUENCE"].Value.ToString();
            qeForm.tbQuestionText.Text = dgvQuestions.CurrentRow.Cells["QUESTION_TEXT"].Value.ToString();

            //string ID = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
            string templateGUID=dgvQuestions.CurrentRow.Cells["TEMPLATE_GUID"].Value.ToString();

            FillChapters();

            qeForm.cbChapterName.DataSource = DS.Tables["VIQ_CHAPTERS"];
            qeForm.cbChapterName.DisplayMember = "FULL_NAME";
            qeForm.cbChapterName.ValueMember = "CH_NUMBER";

            qeForm.cbChapterName.Text =
                dgvQuestions.CurrentRow.Cells["CHAPTER_NUMBER"].Value.ToString() + ". " +
                dgvQuestions.CurrentRow.Cells["CHAPTER_NAME"].Value.ToString();
            qeForm.tbQuestionnaireGUID.Text = templateGUID; 


            var rslt = qeForm.ShowDialog();

            if ((rslt == DialogResult.OK) && (MainForm.isPowerUser))
            {
                OleDbCommand cmd = new OleDbCommand("", connection);

                string chapterNumber = qeForm.cbChapterName.SelectedValue.ToString();
                string chapterName = qeForm.cbChapterName.Text.Substring(3).Trim();
                string questionGUID = qeForm.tbQuestionGUID.Text;
                string sequence = qeForm.tbQuestionSequence.Text;

                if (isRoVIQ)
                {
                    //Check for the same sequence
                    cmd.CommandText =
                        "select Count(SEQUENCE) \n" +
                        "from [" + tableName + "] \n" +
                        "where TEMPLATE_GUID like {" + templateGUID + "} \n" +
                        "and SEQUENCE='" + StrToSQLStr(sequence) + "' \n" +
                        "and QUESTION_GUID <> " + MainForm.FormatGuidString(questionGUID);

                    int recNos = (int) cmd.ExecuteScalar();

                    if (recNos==0)
                    {
                        cmd.CommandText =
                            "update [" + tableName + "] set \n" +
                            "QUESTION_NUMBER='" + StrToSQLStr(qeForm.tbQuestionNumber.Text) + "', \n" +
                            "QUESTION_GUID={" + qeForm.tbQuestionGUID.Text + "}, \n" +
                            "SEQUENCE='" + StrToSQLStr(qeForm.tbQuestionSequence.Text) + "', \n" +
                            "QUESTION_TEXT='" + StrToSQLStr(qeForm.tbQuestionText.Text) + "', \n" +
                            "CHAPTER_NUMBER='" + chapterNumber + "', \n" +
                            "CHAPTER_NAME='" + StrToSQLStr(chapterName) + "' \n" +
                            "where TEMPLATE_GUID={" + dgvQuestions.CurrentRow.Cells["TEMPLATE_GUID"].Value.ToString() + "} \n" +
                            "and QUESTION_GUID={" + dgvQuestions.CurrentRow.Cells["QUESTION_GUID"].Value.ToString() + "}";
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("There is a record with sequence \""+sequence+"\". Please provide another one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    cmd.CommandText =
                        "update [" + tableName + "] set \n" +
                        "QUESTION_NUMBER='" + StrToSQLStr(qeForm.tbQuestionNumber.Text) + "', \n" +
                        "QUESTION_GUID={" + qeForm.tbQuestionGUID.Text + "}, \n" +
                        "SEQUENCE='" + StrToSQLStr(qeForm.tbQuestionSequence.Text) + "', \n" +
                        "QUESTION_TEXT='" + StrToSQLStr(qeForm.tbQuestionText.Text) + "', \n" +
                        "CHAPTER_NUMBER='" + chapterNumber + "', \n" +
                        "CHAPTER_NAME='" + StrToSQLStr(chapterName) + "' \n" +
                        "where TEMPLATE_GUID={" + dgvQuestions.CurrentRow.Cells["TEMPLATE_GUID"].Value.ToString() + "} \n" +
                        "and QUESTION_GUID={" + dgvQuestions.CurrentRow.Cells["QUESTION_GUID"].Value.ToString() + "}";
                    cmd.ExecuteNonQuery();
                }

                int curRow = dgvQuestions.CurrentRow.Index;
                int curCol = dgvQuestions.CurrentCell.ColumnIndex;

                fillQuestions();

                dgvQuestions.CurrentCell = dgvQuestions[curCol, curRow];

            }
        }

        private void FillChapters()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);


            cmd.CommandText =
                "select DISTINCT CHAPTER_NUMBER as CH_NUMBER, CHAPTER_NAME, CHAPTER_NUMBER+'. '+CHAPTER_NAME as FULL_NAME \n"+
                "from ["+tableName+"] \n"+
                "where TEMPLATE_GUID={"+templateGUID+"}";

            OleDbDataAdapter chaptersAdapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("VIQ_CHAPTERS"))
            {
                DS.Tables["VIQ_CHAPTERS"].Clear();
                DS.Tables["VIQ_CHAPTERS"].Columns.Clear();
            }

            chaptersAdapter.Fill(DS, "VIQ_CHAPTERS");
        }

        private string StrToSQLStr(string Text)
        {
            return Text.Replace("'", "''");
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            editQuestion();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddRoVIQQuestionForm addForm = new AddRoVIQQuestionForm(connection, DS, this.Font, 
                this.Icon,templateGUID,
                version,
                typeCode.Substring(2,2));

            var rslt = addForm.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                fillQuestions();


                String searchValue = addForm.textBox1.Text;

                int rowIndex = -1;

                foreach (DataGridViewRow dgRow in dgvQuestions.Rows)
                {
                    if (dgRow.Cells["SEQUENCE"].Value.ToString().Equals(searchValue))
                    {
                        rowIndex = dgRow.Index;
                        break;
                    }
                }

                if (rowIndex >= 0)
                {
                    //dataGridView1.Rows[rowIndex].Selected = true;
                    dgvQuestions.CurrentCell = dgvQuestions[1, rowIndex];
                }


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string msg = "You are going to delete from questionnaire the following question: \n\n" +
                "Question number : " + dgvQuestions.CurrentRow.Cells["QUESTION_NUMBER"].Value.ToString() + "\n" +
                "Question GUID : " + dgvQuestions.CurrentRow.Cells["QUESTION_GUID"].Value.ToString() + "\n" +
                "Question text : " + dgvQuestions.CurrentRow.Cells["QUESTION_TEXT"].Value.ToString() + "\n\n" +
                "Would you like to proceed?";

            var rslt = System.Windows.Forms.MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (rslt == DialogResult.Yes)
            {
                OleDbCommand cmd = new OleDbCommand("",connection);

                Guid templateGuid = MainForm.StrToGuid(dgvQuestions.CurrentRow.Cells["TEMPLATE_GUID"].Value.ToString());
                Guid questionGuid = MainForm.StrToGuid(dgvQuestions.CurrentRow.Cells["QUESTION_GUID"].Value.ToString());

                cmd.CommandText =
                    "delete from [" + tableName + "] \n" +
                    "where TEMPLATE_GUID=" + MainForm.GuidToStr(templateGuid) + "\n" +
                    "and QUESTION_GUID=" + MainForm.GuidToStr(questionGuid);

                cmd.ExecuteNonQuery();

                int row = dgvQuestions.CurrentCell.RowIndex;
                int col = dgvQuestions.CurrentCell.ColumnIndex;


                fillQuestions();

                if (dgvQuestions.Rows.Count > 0)
                {
                    if (dgvQuestions.Rows.Count >= row)
                        dgvQuestions.CurrentCell = dgvQuestions[col, row];
                    else
                        dgvQuestions.CurrentCell = dgvQuestions[col, dgvQuestions.Rows.Count - 1];
                }
            }
        }
    }
}
