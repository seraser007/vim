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
    public partial class QuestionnaireDetailsForm : Form
    {
        OleDbConnection connection;
        DataSet DS;

        private Guid _templateGuid = MainForm.zeroGuid;
        private bool _templateChanged = false;

        public Guid templateGuid
        {
            set { _templateGuid = value; }
            get { return _templateGuid; }
        }

        public bool templateChanged
        {
            get { return _templateChanged; }
        }

        private string qVersion = "";
        private string qType = "";
        private string qTypeCode = "";
        private string qGuid = MainForm.zeroGuid.ToString();
        private DateTime qPublished = DateTimePicker.MinimumDateTime;

        public QuestionnaireDetailsForm()
        {
            connection = MainForm.connection;
            DS = MainForm.DS;
            this.Font = MainForm.mainFont;
            this.Icon = MainForm.mainIcon;

            InitializeComponent();

            FillTemplateTypes();
        }

        private void FillTemplateTypes()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select * \n" +
                "from TEMPLATE_TYPES \n" +
                "order by TEMPLATE_TYPE";

            OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("TEMPLATE_TYPES"))
                DS.Tables["TEMPLATE_TYPES"].Clear();

            adpt.Fill(DS, "TEMPLATE_TYPES");

            cbQuestionnaireType.DataSource = DS.Tables["TEMPLATE_TYPES"];
            cbQuestionnaireType.DisplayMember = "TEMPLATE_TYPE";
            cbQuestionnaireType.ValueMember = "TEMPLATE_TYPE_GUID";
        }

        private void QuestionnaireDetailsForm_Shown(object sender, EventArgs e)
        {
            dtDatePublication.Value = DateTimePicker.MinimumDateTime;

            if (_templateGuid!=MainForm.zeroGuid)
            {
                OleDbCommand cmd = new OleDbCommand("", connection);

                cmd.CommandText =
                    "select * \n" +
                    "from TEMPLATES \n" +
                    "where TEMPLATE_GUID=" + MainForm.GuidToStr(_templateGuid);

                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    cbQuestionnaireType.Text = reader["TEMPLATE_TYPE"].ToString();

                    qVersion = reader["VERSION"].ToString();
                    tbVersion.Text = qVersion;

                    qType = reader["TITLE"].ToString();
                    tbTitle.Text = qType;

                    qTypeCode = reader["TYPE_CODE"].ToString();
                    tbCode.Text = qTypeCode;

                    object dt = reader["PUBLISHED"];

                    if (dt != System.DBNull.Value)
                    {
                        qPublished = Convert.ToDateTime(dt);
                        dtDatePublication.Value = qPublished;
                    }

                    qGuid = reader["TEMPLATE_GUID"].ToString();
                    tbGuid.Text = qGuid;
                }
            }
        }

        private void QuestionnaireDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
                return;

            // check values

            if (tbVersion.Text.Trim().Length==0)
            {
                MessageBox.Show("Please provide version", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }

            if (tbTitle.Text.Trim().Length==0)
            {
                MessageBox.Show("Please provide description", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }

            if (tbCode.Text.Trim().Length==0)
            {
                MessageBox.Show("Please provide type code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }

            if (tbGuid.Text.Trim().Length==0)
            {
                MessageBox.Show("Please provide template Guid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }


            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select Count(TEMPLATE_GUID) as RecCount \n" +
                "from TEMPLATES \n" +
                "where \n" +
                "TEMPLATE_GUID='" + MainForm.FormatGuidString(tbGuid.Text) + "' \n" +
                "and TEMPLATE_GUID<>" + MainForm.GuidToStr(_templateGuid);

            int count = (int)cmd.ExecuteScalar();

            if (count>0)
            {
                MessageBox.Show("There is a questionnaire with the same GUID value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }

            cmd.CommandText =
                "select Count(TEMPLATE_GUID) as RecCount \n" +
                "from TEMPLATES \n" +
                "where \n" +
                "TEMPLATE_TYPE='" + MainForm.StrToSQLStr(cbQuestionnaireType.Text) + "' \n" +
                "and TYPE_CODE='" + MainForm.StrToSQLStr(tbCode.Text) + "' \n" +
                "and TITLE='" + MainForm.StrToSQLStr(tbTitle.Text) + "' \n" +
                "and VERSION='" + MainForm.StrToSQLStr(tbVersion.Text) + "' \n" +
                "and TEMPLATE_GUID<>" + MainForm.GuidToStr(_templateGuid);

            count = (int)cmd.ExecuteScalar();

            if (count>0)
            {
                MessageBox.Show("There is another record with the same questionnaire type, code, version and description.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }

            _templateChanged = !(qVersion.Equals(tbVersion.Text.Trim(), StringComparison.Ordinal) &&
                qType.Equals(tbTitle.Text.Trim(), StringComparison.Ordinal) &&
                qTypeCode.Equals(tbCode.Text.Trim(), StringComparison.Ordinal) &&
                qGuid.Equals(tbGuid.Text.Trim(), StringComparison.Ordinal) &&
                qPublished.Equals(dtDatePublication.Value));

            if (_templateChanged)
            {
                if (_templateGuid == MainForm.zeroGuid)
                {
                    cmd.CommandText =
                        "insert into TEMPLATES (TEMPLATE_GUID, TYPE_CODE, TITLE, VERSION, TEMPLATE_TYPE, PUBLISHED) \n" +
                        "values('" + MainForm.FormatGuidString(tbGuid.Text) + "','" +
                            MainForm.StrToSQLStr(tbCode.Text) + "','" +
                            MainForm.StrToSQLStr(tbTitle.Text) + "','" +
                            MainForm.StrToSQLStr(tbVersion.Text) + "','" +
                            MainForm.StrToSQLStr(cbQuestionnaireType.Text) + "'," +
                            MainForm.DateTimeToQueryStr(dtDatePublication.Value) + ")";

                    if (MainForm.cmdExecute(cmd)<0)
                    {
                        MessageBox.Show("Failed to create new record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                        _templateChanged = false;
                        e.Cancel = true;
                        return;
                    }
                }
                else
                {
                    OleDbTransaction transaction = connection.BeginTransaction();

                    cmd.Transaction = transaction;

                    try
                    {
                        cmd.CommandText =
                            "update TEMPLATES set \n" +
                            "TEMPLATE_GUID='" + MainForm.FormatGuidString(tbGuid.Text) + "', \n" +
                            "TYPE_CODE='" + MainForm.StrToSQLStr(tbCode.Text) + "', \n" +
                            "TITLE='" + MainForm.StrToSQLStr(tbTitle.Text) + "', \n" +
                            "VERSION='" + MainForm.StrToSQLStr(tbVersion.Text) + "', \n" +
                            "TEMPLATE_TYPE='" + MainForm.StrToSQLStr(cbQuestionnaireType.Text) + "', \n" +
                            "PUBLISHED=" + MainForm.DateTimeToQueryStr(dtDatePublication.Value) + " \n" +
                            "where TEMPLATE_GUID=" + MainForm.GuidToStr(_templateGuid);

                        if (MainForm.cmdExecute(cmd)<0)
                        {
                            MessageBox.Show("Failed to update TEMPLATES record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            transaction.Rollback();

                            _templateChanged = false;
                            e.Cancel = true;
                            return;
                        }

                        bool guidChanged = !qGuid.Equals(tbGuid.Text.Trim(), StringComparison.Ordinal);

                        cmd.CommandText =
                            "update REPORTS set \n" +
                            "VIQ_TYPE_CODE='" + MainForm.StrToSQLStr(tbCode.Text) + "', \n" +
                            "VIQ_TYPE='" + MainForm.StrToSQLStr(tbTitle.Text) + "', \n" +
                            "VIQ_VERSION='" + MainForm.StrToSQLStr(tbVersion.Text) + "'";

                        string updateTemplateQuestions = "";

                        if (guidChanged)
                        {
                            cmd.CommandText =
                                cmd.CommandText + ", \n" +
                                "TEMPLATE_GUID='" + MainForm.FormatGuidString(tbGuid.Text) + "' \n";


                            updateTemplateQuestions =
                                "update TEMPLATE_QUESTIONS set \n" +
                                "TEMPLATE_GUID='" + MainForm.FormatGuidString(tbGuid.Text) + "' \n" +
                                "where \n" +
                                "TEMPLATE_GUID like '" + MainForm.FormatGuidString(qGuid) + "'";
                        }

                        cmd.CommandText =
                            cmd.CommandText + " \n" +
                            "where \n" +
                            "TEMPLATE_GUID like '" + MainForm.FormatGuidString(qGuid) + "'";

                        if (MainForm.cmdExecute(cmd)<0)
                        {
                            MessageBox.Show("Failed to update REPORTS records", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            transaction.Rollback();

                            _templateChanged = false;
                            e.Cancel = true;
                            return;
                        }

                        if (updateTemplateQuestions.Length > 0)
                        {
                            cmd.CommandText = updateTemplateQuestions;

                            if (MainForm.cmdExecute(cmd)<0)
                            {
                                MessageBox.Show("Failed to update TEMPLATE_QUESTIONS records", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                transaction.Rollback();

                                _templateChanged = false;
                                e.Cancel = true;
                                return;
                            }
                        }
                        

                        transaction.Commit();
                    }
                    catch (Exception E)
                    {
                        transaction.Rollback();

                        MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        _templateChanged = false;
                        e.Cancel = true;
                    }
                }
            }
        }

        private void dtDatePublication_KeyDown(object sender, KeyEventArgs e)
        {
            //Clear control in case of Del button click

            if (e.KeyValue == 46)
            {
                dtDatePublication.Value = DateTimePicker.MinimumDateTime;
            }
            else
            {
                if (dtDatePublication.Value == DateTimePicker.MinimumDateTime)
                {
                    if (e.KeyValue > 48 && e.KeyValue < 58)
                    {
                        dtDatePublication.Value = DateTime.Today;
                        dtDatePublication.Format = DateTimePickerFormat.Short;
                    }
                }
            }
        }

        private void dtDatePublication_DropDown(object sender, EventArgs e)
        {
            if (dtDatePublication.Value == DateTimePicker.MinimumDateTime)
            {
                dtDatePublication.Value = DateTime.Today;

                dtDatePublication.Select();

                SendKeys.Send("%{DOWN}");
            }
        }

        private void dtDatePublication_ValueChanged(object sender, EventArgs e)
        {
            if (dtDatePublication.Value == DateTimePicker.MinimumDateTime)
            {
                dtDatePublication.Format = DateTimePickerFormat.Custom;
                dtDatePublication.CustomFormat = " ";
                dtDatePublication.Value = DateTimePicker.MinimumDateTime; ;
            }
            else
            {
                dtDatePublication.Format = DateTimePickerFormat.Short;
            }
        }

    }
}
