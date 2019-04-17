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
    public partial class AddRoVIQQuestionForm : Form
    {
        OleDbConnection connection;
        DataSet DS;
        string templateGUID;

        public AddRoVIQQuestionForm(OleDbConnection mainConnection, DataSet mainDS, Font mainFont, 
            Icon mainIcon, string mainTemplateGUID, string mainVersion, string mainType)
        {
            this.Font = mainFont;
            this.Icon = mainIcon;
            connection = mainConnection;
            DS = mainDS;

            InitializeComponent();

            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select DISTINCT VERSION \n" +
                "from TEMPLATES";

            OleDbDataAdapter versions = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("VIQ_VERSIONS"))
            {
                DS.Tables["VIQ_VERSIONS"].Clear();
                DS.Tables["VIQ_VERSIONS"].Columns.Clear();
            }

            versions.Fill(DS, "VIQ_VERSIONS");

            comboBox1.DataSource=DS.Tables["VIQ_VERSIONS"];
            comboBox1.DisplayMember="VERSION";
            comboBox1.ValueMember = "VERSION";

            comboBox1.Text=mainVersion;
            comboBox3.Text=mainType;
            templateGUID=mainTemplateGUID;

        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            comboBox3.Text = "01";
            fillQuestions();
        }

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            fillQuestions();
        }

        private void fillQuestions()
        {
            if ((comboBox1.Text.Length > 0) && (comboBox3.Text.Length > 0))
            {
                //Get list of question numbers

                OleDbCommand cmd = new OleDbCommand("", connection);

                cmd.CommandText =
                    "select CStr(TEMPLATE_GUID) from TEMPLATES \n" +
                    "where VERSION='" + comboBox1.Text + "' \n" +
                    "and TYPE_CODE like '%" + comboBox3.Text + "'";

                string tempGUID = (string)cmd.ExecuteScalar();

                if ((tempGUID!=null) && (tempGUID.Length > 0))
                {
                    cmd.CommandText =
                        "select * from TEMPLATE_QUESTIONS \n" +
                        "where TEMPLATE_GUID=" + tempGUID + " \n" +
                        "order by SEQUENCE";

                    if (DS.Tables.Contains("XTQUESTIONS"))
                    {
                        DS.Tables["XTQUESTIONS"].Clear();
                        DS.Tables["XTQUESTIONS"].Columns.Clear();
                    }

                    OleDbDataAdapter xtQuestions = new OleDbDataAdapter(cmd);

                    xtQuestions.Fill(DS, "XTQUESTIONS");

                    comboBox2.DataSource = DS.Tables["XTQUESTIONS"];
                    comboBox2.DisplayMember = "QUESTION_NUMBER";
                    comboBox2.ValueMember = "QUESTION_GUID";
                }
                else
                {
                    comboBox2.DataSource = null;
                }

            }
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((comboBox2.SelectedIndex >= 0) && (!comboBox2.SelectedValue.ToString().StartsWith("System")))
            {
                OleDbCommand cmd = new OleDbCommand("", connection);

                cmd.CommandText =
                    "select TOP 1 QUESTION_TEXT from TEMPLATE_QUESTIONS \n" +
                    "where QUESTION_GUID={" + comboBox2.SelectedValue.ToString() + "} \n" +
                    "order by ID";

                string text = (string)cmd.ExecuteScalar();

                textBox3.Text = text;
                textBox2.Text = comboBox2.SelectedValue.ToString();
            }
            else
            {
                textBox3.Clear();
            }
        }

        private void AddRoVIQQuestionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                if ((textBox1.Text.Trim().Length > 0) && (textBox2.Text.Trim().Length > 0))
                {
                    OleDbCommand cmd = new OleDbCommand("", connection);
                    
                    string seq=textBox1.Text.Trim().Substring(0,3);


                    cmd.CommandText=
                        "insert into RTEMPLATE_QUESTIONS (TEMPLATE_GUID,CHAPTER_NUMBER,CHAPTER_NAME, \n"+
                        "SECTION_NUMBER, SECTION_NAME, QUESTION_NUMBER, QUESTION_GUID, \n"+
                        "QUESTION_TEXT, SEQUENCE) \n"+
                        "select TOP 1 {"+templateGUID+"},CHAPTER_NUMBER, CHAPTER_NAME, SECTION_NUMBER, \n"+
                        "SECTION_NAME,'"+comboBox2.Text+"',{"+textBox2.Text.Trim()+"},'"+StrToSQLStr(textBox3.Text)+"','"+
                        StrToSQLStr(textBox1.Text.Trim())+"' \n"+
                        "from RTEMPLATE_QUESTIONS \n"+
                        "where TEMPLATE_GUID={"+templateGUID+"} \n"+
                        "and SEQUENCE like '"+seq+"%'";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private string StrToSQLStr(string Text)
        {
            return Text.Replace("'", "''");
        }

    }
}
