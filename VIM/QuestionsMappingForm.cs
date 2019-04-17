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
    public partial class QuestionsMappingForm : Form
    {
        OleDbConnection connection;
        DataSet DS;
        OleDbDataAdapter viqMasterAdapter;
        OleDbDataAdapter viqSlaveAdapter;
        OleDbDataAdapter viqMappingAdapter;
        OleDbDataAdapter masterVIQTypes;
        OleDbDataAdapter searchMasterAdapter;
        OleDbDataAdapter searchSlaveAdapter;

        int masterMajorVersion = 0;
        int slaveMajorVersion = 0;
        string viqShortType = "";
        BindingSource bsSlave = new BindingSource();
        BindingSource bsMaster = new BindingSource();

        BindingSource bsSlaveSearch = new BindingSource();
        DataGridViewColumn oldSlaveColumn = null;
        DataGridViewColumn oldMasterColumn = null;
        SortOrder oldSlaveOrder = SortOrder.None;
        SortOrder oldMasterOrder = SortOrder.None;

        private string listOfChapters = "";
        private string masterTemplateGUID = "";
        private string slaveTemplateGUID = "";

        public QuestionsMappingForm(OleDbConnection mainConnection, DataSet mainDS, Font mainFont, Icon mainIcon)
        {
            InitializeComponent();
            connection = mainConnection;
            DS = mainDS;
            this.Font = mainFont;
            this.Icon = mainIcon;

            fillQuestionnaireVersions();
        }

        private void fillQuestionnaireTypes(string VIQVersion, ComboBox cb, bool useMaster)
        {
            string s =
                "select * from \n"+
                "(select TITLE, TYPE_CODE \n"+
                "from TEMPLATES \n"+
                "where [VERSION]='" + VIQVersion + "' \n"+
                "union \n"+
                "select TOP 1 '' as TITLE, '' as TYPE_CODE \n"+
                "from TEMPLATES) \n"+
                "order by TITLE";

            if (useMaster)
            {
                if (DS.Tables.Contains("MASTER_TYPES"))
                    DS.Tables["MASTER_TYPES"].Clear();

                masterVIQTypes = new OleDbDataAdapter(s, connection);

                masterVIQTypes.Fill(DS, "MASTER_TYPES");

                DataTable dt = DS.Tables["MASTER_TYPES"];

                cb.DataSource = dt;
                cb.DisplayMember = "TITLE";
                cb.ValueMember = "TYPE_CODE";
            }
            else
            {
                if (DS.Tables.Contains("SLAVE_TYPES"))
                    DS.Tables["SLAVE_TYPES"].Clear();

                masterVIQTypes = new OleDbDataAdapter(s, connection);

                masterVIQTypes.Fill(DS, "SLAVE_TYPES");

                DataTable dt = DS.Tables["SLAVE_TYPES"];

                cb.DataSource = dt;
                cb.DisplayMember = "TITLE";
                cb.ValueMember = "TYPE_CODE";
            }
        }

        private void fillQuestionnaireVersions()
        {
            OleDbCommand cmd = new OleDbCommand("select DISTINCT VERSION from TEMPLATES order by VERSION", connection);

            try
            {
                OleDbDataReader qVersions = cmd.ExecuteReader();

                cbMasterVersion.Items.Clear();
                cbSlaveVersion.Items.Clear();

                while (qVersions.Read())
                {
                    cbMasterVersion.Items.Add(Convert.ToString(qVersions[0]));
                    cbSlaveVersion.Items.Add(Convert.ToString(qVersions[0]));
                }

                qVersions.Close();
            }
            catch
            {
                //Do nothing
            }
        }

        private void cbMasterVersion_TextChanged(object sender, EventArgs e)
        {
            fillQuestionnaireTypes(cbMasterVersion.Text, cbMasterType, true);
            cbMasterType.SelectedValue = "";
            cbMasterType.Text = "";

            if (cbMasterVersion.Text.Length > 0)
                masterMajorVersion = Convert.ToInt16(cbMasterVersion.Text.Substring(0, cbMasterVersion.Text.IndexOf(".")));
            else
                masterMajorVersion = 0;

            //fillMasterVIQQuestions();
        }

        private void cbSlaveVersion_TextChanged(object sender, EventArgs e)
        {
            fillQuestionnaireTypes(cbSlaveVersion.Text, cbSlaveType,false);
            cbSlaveType.Text = "";

            if (cbSlaveVersion.Text.Length > 0)
                slaveMajorVersion = Convert.ToInt16(cbSlaveVersion.Text.Substring(0, cbSlaveVersion.Text.IndexOf(".")));
            else
                slaveMajorVersion = 0;
        }

        private void fillMasterVIQQuestions()
        {
            listOfChapters = "";
            masterTemplateGUID = "";

            if (cbMasterVersion.SelectedIndex < 1)
                return;

            if (cbMasterType.SelectedIndex < 1)
                return;
            
            if (DS.Tables.Contains("MasterVIQ"))
                DS.Tables["MasterVIQ"].Clear();

            if (DS.Tables.Contains("SearchMasterVIQ"))
                DS.Tables["SearchMasterVIQ"].Clear();

            string s="";
            string where = "";
            string orderBy = "";

            string searchSQL = "";
            string searchWhere = "";
            string searchOrderBy = "";

            masterTemplateGUID = GetTemplateGUID(cbMasterVersion.Text, cbMasterType.Text);
            listOfChapters = GetChapters(masterTemplateGUID);

            //if ((comboBox1.Text.Length > 0) && (comboBox2.Text.Length > 0))

            s = 
                "select TEMPLATE_QUESTIONS.QUESTION_NUMBER, TEMPLATE_QUESTIONS.QUESTION_TEXT, "+
                "TEMPLATE_QUESTIONS.QUESTION_GUID, "+
                "TEMPLATE_QUESTIONS.SEQUENCE, TEMPLATE_KEYS.ID as KEY_ID, \n" +
                "TEMPLATE_KEYS.SUBCHAPTER, TEMPLATE_KEYS.KEY_INDEX, \n"+
                "TEMPLATE_QUESTIONS.CHAPTER_NUMBER, TEMPLATE_QUESTIONS.CHAPTER_NAME \n"+
                "from (TEMPLATE_QUESTIONS inner join TEMPLATES\n" +
                "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID) \n"+
                "left join TEMPLATE_KEYS on TEMPLATE_QUESTIONS.QUESTION_NUMBER=TEMPLATE_KEYS.QUESTION_NUMBER \n";
            
            where = 
                "where TEMPLATES.TITLE='" + StrToSQLStr(cbMasterType.Text) + "'\n" +
                "and TEMPLATES.VERSION='" + StrToSQLStr(cbMasterVersion.Text) + "'\n"+
                "and (TEMPLATE_KEYS.VIQ_MAJOR_VERSION="+masterMajorVersion.ToString()+" or TEMPLATE_KEYS.VIQ_MAJOR_VERSION is Null) \n"+
                "and (TEMPLATE_KEYS.VIQ_SHORT_TYPE='"+StrToSQLStr(viqShortType)+
                "' or TEMPLATE_KEYS.VIQ_SHORT_TYPE is Null or LEN(TEMPLATE_KEYS.VIQ_SHORT_TYPE)=0)";

            orderBy = 
                "order by TEMPLATE_QUESTIONS.SEQUENCE";

            searchSQL = 
                "select DISTINCT LEFT(QUESTION_TEXT,255) as QUESTION_TEXT \n" +
                "from (TEMPLATE_QUESTIONS inner join TEMPLATES\n" +
                "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID) \n" +
                "left join TEMPLATE_KEYS on TEMPLATE_QUESTIONS.QUESTION_NUMBER=TEMPLATE_KEYS.QUESTION_NUMBER \n";
            
            searchWhere =
                "where TEMPLATES.TITLE='" + StrToSQLStr(cbMasterType.Text) + "'\n" +
                "and TEMPLATES.VERSION='" + StrToSQLStr(cbMasterVersion.Text) + "'\n" +
                "and (TEMPLATE_KEYS.VIQ_MAJOR_VERSION=" + masterMajorVersion.ToString() + " or TEMPLATE_KEYS.VIQ_MAJOR_VERSION is Null) \n" +
                "and (TEMPLATE_KEYS.VIQ_SHORT_TYPE='" + StrToSQLStr(viqShortType) +
                "' or TEMPLATE_KEYS.VIQ_SHORT_TYPE is Null or LEN(TEMPLATE_KEYS.VIQ_SHORT_TYPE)=0)";
            
            searchOrderBy = "";

            string addWhere = "";


            switch (comboBox5.Text)
            {
                case "Questions for chapter":
                    if (numericUpDown1.Value > 0)
                    {
                        addWhere = 
                            " and TEMPLATE_QUESTIONS.QUESTION_NUMBER like '" + numericUpDown1.Value.ToString() + ".%' \n";
                
                        where = where + addWhere;
                        searchWhere = searchWhere + addWhere;
                    }
                    break;
                case "Unmapped questions":
                    addWhere=
                        " and QUESTION_GUID not in \n" +
                        "(SELECT QUESTIONS_MAPPING.QUESTION_MASTER_GUID \n" +
                        "FROM (((QUESTIONS_MAPPING \n" +
                        "INNER JOIN TEMPLATE_QUESTIONS AS TQ1 \n" +
                        "ON QUESTIONS_MAPPING.QUESTION_MASTER_GUID = TQ1.QUESTION_GUID) \n" +
                        "INNER JOIN TEMPLATE_QUESTIONS AS TQ2 \n" +
                        "ON QUESTIONS_MAPPING.QUESTION_SLAVE_GUID = TQ2.QUESTION_GUID) \n" +
                        "INNER JOIN TEMPLATES AS TP1 \n" +
                        "ON TQ1.TEMPLATE_GUID = TP1.TEMPLATE_GUID) \n" +
                        "INNER JOIN TEMPLATES AS TP2 \n" +
                        "ON TQ2.TEMPLATE_GUID = TP2.TEMPLATE_GUID \n" +
                        "WHERE TP1.TITLE='" + StrToSQLStr(cbMasterType.Text) + "'\n" +
                        "and TP1.VERSION='" + StrToSQLStr(cbMasterVersion.Text) + "'\n" +
                        "and TP2.TITLE='" + StrToSQLStr(cbSlaveType.Text) + "'\n" +
                        "and TP2.VERSION='" + StrToSQLStr(cbSlaveVersion.Text) + "')";

                    where = where + addWhere;
                    searchWhere = searchWhere + addWhere;

                    break;
                case "Unmapped for chapter":
                    if (numericUpDown1.Value > 0)
                        addWhere = 
                            " and TEMPLATE_QUESTIONS.QUESTION_NUMBER like '" + numericUpDown1.Value.ToString() + ".%' \n";
                    
                    addWhere=addWhere+
                        " and QUESTION_GUID not in \n" +
                        "(SELECT QUESTIONS_MAPPING.QUESTION_MASTER_GUID \n" +
                        "FROM (((QUESTIONS_MAPPING \n" +
                        "INNER JOIN TEMPLATE_QUESTIONS AS TQ1 \n" +
                        "ON QUESTIONS_MAPPING.QUESTION_MASTER_GUID = TQ1.QUESTION_GUID) \n" +
                        "INNER JOIN TEMPLATE_QUESTIONS AS TQ2 \n" +
                        "ON QUESTIONS_MAPPING.QUESTION_SLAVE_GUID = TQ2.QUESTION_GUID) \n" +
                        "INNER JOIN TEMPLATES AS TP1 \n" +
                        "ON TQ1.TEMPLATE_GUID = TP1.TEMPLATE_GUID) \n" +
                        "INNER JOIN TEMPLATES AS TP2 \n" +
                        "ON TQ2.TEMPLATE_GUID = TP2.TEMPLATE_GUID \n" +
                        "WHERE TP1.TITLE='" + StrToSQLStr(cbMasterType.Text) + "'\n" +
                        "and TP1.VERSION='" + StrToSQLStr(cbMasterVersion.Text) + "'\n" +
                        "and TP2.TITLE='" + StrToSQLStr(cbSlaveType.Text) + "'\n" +
                        "and TP2.VERSION='" + StrToSQLStr(cbSlaveVersion.Text) + "')";

                    where = where + addWhere;
                    searchWhere = searchWhere + addWhere;

                    break;
                case "Mapped questions":
                    addWhere=
                        " and QUESTION_GUID in \n" +
                        "(SELECT QUESTIONS_MAPPING.QUESTION_MASTER_GUID \n" +
                        "FROM (((QUESTIONS_MAPPING \n" +
                        "INNER JOIN TEMPLATE_QUESTIONS AS TQ1 \n" +
                        "ON QUESTIONS_MAPPING.QUESTION_MASTER_GUID = TQ1.QUESTION_GUID) \n" +
                        "INNER JOIN TEMPLATE_QUESTIONS AS TQ2 \n" +
                        "ON QUESTIONS_MAPPING.QUESTION_SLAVE_GUID = TQ2.QUESTION_GUID) \n" +
                        "INNER JOIN TEMPLATES AS TP1 \n" +
                        "ON TQ1.TEMPLATE_GUID = TP1.TEMPLATE_GUID) \n" +
                        "INNER JOIN TEMPLATES AS TP2 \n" +
                        "ON TQ2.TEMPLATE_GUID = TP2.TEMPLATE_GUID \n" +
                        "WHERE TP1.TITLE='" + StrToSQLStr(cbMasterType.Text) + "'\n" +
                        "and TP1.VERSION='" + StrToSQLStr(cbMasterVersion.Text) + "'\n" +
                        "and TP2.TITLE='" + StrToSQLStr(cbSlaveType.Text) + "'\n" +
                        "and TP2.VERSION='" + StrToSQLStr(cbSlaveVersion.Text) + "')";

                    where = where + addWhere;
                    searchWhere = searchWhere + addWhere;

                    break;
                case "Mapped for chapter":
                    if (numericUpDown1.Value > 0)
                        addWhere = 
                            " and TEMPLATE_QUESTIONS.QUESTION_NUMBER like '" + numericUpDown1.Value.ToString() + ".%' \n";

                    addWhere=addWhere+
                        " and QUESTION_GUID in \n" +
                        "(SELECT QUESTIONS_MAPPING.QUESTION_MASTER_GUID \n" +
                        "FROM (((QUESTIONS_MAPPING \n" +
                        "INNER JOIN TEMPLATE_QUESTIONS AS TQ1 \n" +
                        "ON QUESTIONS_MAPPING.QUESTION_MASTER_GUID = TQ1.QUESTION_GUID) \n" +
                        "INNER JOIN TEMPLATE_QUESTIONS AS TQ2 \n" +
                        "ON QUESTIONS_MAPPING.QUESTION_SLAVE_GUID = TQ2.QUESTION_GUID) \n" +
                        "INNER JOIN TEMPLATES AS TP1 \n" +
                        "ON TQ1.TEMPLATE_GUID = TP1.TEMPLATE_GUID) \n" +
                        "INNER JOIN TEMPLATES AS TP2 \n" +
                        "ON TQ2.TEMPLATE_GUID = TP2.TEMPLATE_GUID \n" +
                        "WHERE TP1.TITLE='" + StrToSQLStr(cbMasterType.Text) + "'\n" +
                        "and TP1.VERSION='" + StrToSQLStr(cbMasterVersion.Text) + "'\n" +
                        "and TP2.TITLE='" + StrToSQLStr(cbSlaveType.Text) + "'\n" +
                        "and TP2.VERSION='" + StrToSQLStr(cbSlaveVersion.Text) + "')";

                    where = where + addWhere;
                    searchWhere = searchWhere + addWhere;

                    break;
            }


            viqMasterAdapter = new OleDbDataAdapter(s+where+orderBy, connection);
            searchMasterAdapter = new OleDbDataAdapter(searchSQL + searchWhere + searchOrderBy, connection);

            try
            {
                this.Cursor = Cursors.WaitCursor;

                viqMasterAdapter.Fill(DS, "MasterVIQ");
                searchMasterAdapter.Fill(DS, "SearchMasterVIQ");

                AutoCompleteStringCollection qList = new AutoCompleteStringCollection();
                DataRow[] rows = DS.Tables["SearchMasterVIQ"].Select();

                foreach (DataRow row in rows)
                {
                    qList.Add(row["QUESTION_TEXT"].ToString());
                }

                tbxMasterSearch.AutoCompleteCustomSource = qList;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            bsMaster.DataSource = DS;
            bsMaster.DataMember = "MasterVIQ";
            bsMaster.Sort = "SEQUENCE";

            adgvMaster.DataSource = bsMaster;
            adgvMaster.AutoGenerateColumns = true;
            

            //dataGridView1.SortCompare += new DataGridViewSortCompareEventHandler(
            //    this.dataGridView1_SortCompare);
            //dgvMaster.DataMember = "MasterVIQ";

            adgvMaster.Columns["QUESTION_NUMBER"].HeaderText = "No.";
            adgvMaster.Columns["QUESTION_NUMBER"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            adgvMaster.Columns["QUESTION_TEXT"].HeaderText = "Question text";
            adgvMaster.Columns["QUESTION_GUID"].Visible = false;
            adgvMaster.Columns["SEQUENCE"].Visible = false;
            adgvMaster.Columns["SUBCHAPTER"].HeaderText = "Subchapter";
            adgvMaster.Columns["SUBCHAPTER"].FillWeight = 25;
            adgvMaster.Columns["KEY_INDEX"].HeaderText = "Key index";
            adgvMaster.Columns["KEY_INDEX"].FillWeight = 25;
            adgvMaster.Columns["KEY_ID"].Visible = false;
            adgvMaster.Columns["CHAPTER_NUMBER"].Visible = false;
            adgvMaster.Columns["CHAPTER_NAME"].Visible = false;

            foreach (DataGridViewColumn column in adgvMaster.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }

        }

        private void fillSlaveVIQQuestions()
        {
            slaveTemplateGUID = "";

            if (cbSlaveType.SelectedIndex < 1)
                return;

            if (cbSlaveVersion.SelectedIndex < 1)
                return;

            if (DS.Tables.Contains("SlaveVIQ"))
                DS.Tables["SlaveVIQ"].Clear();

            if (DS.Tables.Contains("SearchSlaveVIQ"))
                DS.Tables["SearchSlaveVIQ"].Clear();

            string s = "";
            string where = "";
            string orderBy = "";

            string searchSQL = "";
            string searchWhere = "";
            string searchOrderBy = "";

            slaveTemplateGUID = GetTemplateGUID(cbSlaveVersion.Text, cbSlaveType.Text);

            //if ((comboBox3.Text.Length > 0) && (comboBox4.Text.Length > 0))

            s = "select QUESTION_NUMBER,QUESTION_TEXT, QUESTION_GUID, SEQUENCE\n" +
                "from TEMPLATE_QUESTIONS inner join TEMPLATES\n" +
                "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID\n";
            where = "where TEMPLATES.TITLE='" + StrToSQLStr(cbSlaveType.Text) + "'\n" +
                "and TEMPLATES.VERSION='" + StrToSQLStr(cbSlaveVersion.Text) + "'\n";
            orderBy="order by SEQUENCE";

            searchSQL = "select DISTINCT LEFT(QUESTION_TEXT,255) as QUESTION_TEXT \n" +
                "from TEMPLATE_QUESTIONS inner join TEMPLATES\n" +
                "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID\n";
            searchWhere = "where TEMPLATES.TITLE='" + StrToSQLStr(cbSlaveType.Text) + "'\n" +
                "and TEMPLATES.VERSION='" + StrToSQLStr(cbSlaveVersion.Text) + "'\n";
            searchOrderBy = "";

            string addWhere = "";

            switch (comboBox6.Text)
            {
                case "Questions for chapter":
                    if (numericUpDown2.Value > 0)
                    {
                        addWhere = " and QUESTION_NUMBER like '" + numericUpDown2.Value.ToString() + ".%' \n";
                        where = where + addWhere;
                        searchWhere = searchWhere + addWhere;
                    }
                    break;
                case "Unmapped questions":
                    addWhere=
                        " and QUESTION_GUID not in \n" +
                        "(SELECT QUESTIONS_MAPPING.QUESTION_SLAVE_GUID \n" +
                        "FROM (((QUESTIONS_MAPPING \n" +
                        "INNER JOIN TEMPLATE_QUESTIONS AS TQ1 \n" +
                        "ON QUESTIONS_MAPPING.QUESTION_MASTER_GUID = TQ1.QUESTION_GUID) \n" +
                        "INNER JOIN TEMPLATE_QUESTIONS AS TQ2 \n" +
                        "ON QUESTIONS_MAPPING.QUESTION_SLAVE_GUID = TQ2.QUESTION_GUID) \n" +
                        "INNER JOIN TEMPLATES AS TP1 \n" +
                        "ON TQ1.TEMPLATE_GUID = TP1.TEMPLATE_GUID) \n" +
                        "INNER JOIN TEMPLATES AS TP2 \n" +
                        "ON TQ2.TEMPLATE_GUID = TP2.TEMPLATE_GUID \n" +
                        "WHERE TP1.TITLE='" + StrToSQLStr(cbMasterType.Text) + "'\n" +
                        "and TP1.VERSION='" + StrToSQLStr(cbMasterVersion.Text) + "'\n" +
                        "and TP2.TITLE='" + StrToSQLStr(cbSlaveType.Text) + "'\n" +
                        "and TP2.VERSION='" + StrToSQLStr(cbSlaveVersion.Text) + "')";

                    where = where + addWhere;
                    searchWhere = searchWhere + addWhere;
                    break;
                case "Unmapped for chapter":
                    if (numericUpDown2.Value > 0)
                        addWhere = " and QUESTION_NUMBER like '" + numericUpDown2.Value.ToString() + ".%' \n";

                    addWhere = addWhere + " and QUESTION_GUID not in \n" +
                        "(SELECT QUESTIONS_MAPPING.QUESTION_SLAVE_GUID \n" +
                        "FROM (((QUESTIONS_MAPPING \n" +
                        "INNER JOIN TEMPLATE_QUESTIONS AS TQ1 \n" +
                        "ON QUESTIONS_MAPPING.QUESTION_MASTER_GUID = TQ1.QUESTION_GUID) \n" +
                        "INNER JOIN TEMPLATE_QUESTIONS AS TQ2 \n" +
                        "ON QUESTIONS_MAPPING.QUESTION_SLAVE_GUID = TQ2.QUESTION_GUID) \n" +
                        "INNER JOIN TEMPLATES AS TP1 \n" +
                        "ON TQ1.TEMPLATE_GUID = TP1.TEMPLATE_GUID) \n" +
                        "INNER JOIN TEMPLATES AS TP2 \n" +
                        "ON TQ2.TEMPLATE_GUID = TP2.TEMPLATE_GUID \n" +
                        "WHERE TP1.TITLE='" + StrToSQLStr(cbMasterType.Text) + "'\n" +
                        "and TP1.VERSION='" + StrToSQLStr(cbMasterVersion.Text) + "'\n" +
                        "and TP2.TITLE='" + StrToSQLStr(cbSlaveType.Text) + "'\n" +
                        "and TP2.VERSION='" + StrToSQLStr(cbSlaveVersion.Text) + "')";

                    where = where + addWhere;
                    searchWhere = searchWhere + addWhere;
                    break;
                case "Mapped questions":
                    addWhere = " and QUESTION_GUID in \n" +
                        "(SELECT QUESTIONS_MAPPING.QUESTION_SLAVE_GUID \n" +
                        "FROM (((QUESTIONS_MAPPING \n" +
                        "INNER JOIN TEMPLATE_QUESTIONS AS TQ1 \n" +
                        "ON QUESTIONS_MAPPING.QUESTION_MASTER_GUID = TQ1.QUESTION_GUID) \n" +
                        "INNER JOIN TEMPLATE_QUESTIONS AS TQ2 \n" +
                        "ON QUESTIONS_MAPPING.QUESTION_SLAVE_GUID = TQ2.QUESTION_GUID) \n" +
                        "INNER JOIN TEMPLATES AS TP1 \n" +
                        "ON TQ1.TEMPLATE_GUID = TP1.TEMPLATE_GUID) \n" +
                        "INNER JOIN TEMPLATES AS TP2 \n" +
                        "ON TQ2.TEMPLATE_GUID = TP2.TEMPLATE_GUID \n" +
                        "WHERE TP1.TITLE='" + StrToSQLStr(cbMasterType.Text) + "'\n" +
                        "and TP1.VERSION='" + StrToSQLStr(cbMasterVersion.Text) + "'\n" +
                        "and TP2.TITLE='" + StrToSQLStr(cbSlaveType.Text) + "'\n" +
                        "and TP2.VERSION='" + StrToSQLStr(cbSlaveVersion.Text) + "')";

                    where = where + addWhere;
                    searchWhere = searchWhere + addWhere;
                    break;
                case "Mapped for chapter":
                    if (numericUpDown2.Value > 0)
                        addWhere= " and QUESTION_NUMBER like '" + numericUpDown2.Value.ToString() + ".%' \n";

                    addWhere = addWhere + " and QUESTION_GUID in \n" +
                        "(SELECT QUESTIONS_MAPPING.QUESTION_SLAVE_GUID \n" +
                        "FROM (((QUESTIONS_MAPPING \n" +
                        "INNER JOIN TEMPLATE_QUESTIONS AS TQ1 \n" +
                        "ON QUESTIONS_MAPPING.QUESTION_MASTER_GUID = TQ1.QUESTION_GUID) \n" +
                        "INNER JOIN TEMPLATE_QUESTIONS AS TQ2 \n" +
                        "ON QUESTIONS_MAPPING.QUESTION_SLAVE_GUID = TQ2.QUESTION_GUID) \n" +
                        "INNER JOIN TEMPLATES AS TP1 \n" +
                        "ON TQ1.TEMPLATE_GUID = TP1.TEMPLATE_GUID) \n" +
                        "INNER JOIN TEMPLATES AS TP2 \n" +
                        "ON TQ2.TEMPLATE_GUID = TP2.TEMPLATE_GUID \n" +
                        "WHERE TP1.TITLE='" + StrToSQLStr(cbMasterType.Text) + "'\n" +
                        "and TP1.VERSION='" + StrToSQLStr(cbMasterVersion.Text) + "'\n" +
                        "and TP2.TITLE='" + StrToSQLStr(cbSlaveType.Text) + "'\n" +
                        "and TP2.VERSION='" + StrToSQLStr(cbSlaveVersion.Text) + "')";

                    where = where + addWhere;
                    searchWhere = searchWhere + addWhere;
                    break;
            }

            viqSlaveAdapter = new OleDbDataAdapter(s+where+orderBy, connection);
            searchSlaveAdapter = new OleDbDataAdapter(searchSQL + searchWhere + searchOrderBy, connection);

            try
            {
                this.Cursor = Cursors.WaitCursor;
                viqSlaveAdapter.Fill(DS, "SlaveVIQ");
                searchSlaveAdapter.Fill(DS, "SearchSlaveVIQ");

                AutoCompleteStringCollection qList = new AutoCompleteStringCollection();
                DataRow[] rows = DS.Tables["SearchSlaveVIQ"].Select();

                foreach (DataRow row in rows)
                {
                    qList.Add(row["QUESTION_TEXT"].ToString());
                }

                tbxSlaveSearch.AutoCompleteCustomSource = qList;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            bsSlave.DataSource = DS;
            bsSlave.DataMember = "SlaveVIQ";



            //tbSlaveSearch.AutoCompleteCustomSource = bsSlaveSearch.List;

            adgvSlave.DataSource = bsSlave;
            adgvSlave.AutoGenerateColumns = true;
            //adgvSlave.DataMember = "SlaveVIQ";

            adgvSlave.Columns["QUESTION_NUMBER"].HeaderText = "No.";
            //dataGridView3.Columns["QUESTION_NUMBER"].FillWeight = 10;
            //dataGridView3.Columns["QUESTION_NUMBER"].MinimumWidth = 30;
            adgvSlave.Columns["QUESTION_NUMBER"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            adgvSlave.Columns["QUESTION_TEXT"].HeaderText = "Question text";
            adgvSlave.Columns["QUESTION_GUID"].Visible = false;
            adgvSlave.Columns["SEQUENCE"].Visible = false;
            adgvSlave.Sort(adgvSlave.Columns["SEQUENCE"], ListSortDirection.Ascending);

            foreach (DataGridViewColumn column in adgvSlave.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }

            bsSlave.Sort = "SEQUENCE";
        }

        
        public string StrToSQLStr(string Text)
        {
            return Text.Replace("'", "''");
        }

        private void cbMasterType_TextChanged(object sender, EventArgs e)
        {
            string s = "";
            
            if (cbMasterType.SelectedValue!=null)
                s=cbMasterType.SelectedValue.ToString();

            if ((s.Length==4) && (s.Substring(2, 2) == "01"))
                button6.Visible = true;
            else
                button6.Visible = false;

            viqShortType = "";

            if (cbMasterType.Text.Contains("Petroleum"))
                viqShortType = "Petroleum";
            else
                if (cbMasterType.Text.Contains("Chemical"))
                    viqShortType = "Chemical";
                else
                    if (cbMasterType.Text.Contains("LPG"))
                        viqShortType = "LPG";
                    else
                        if (cbMasterType.Text.Contains("LNG"))
                            viqShortType = "LNG";

            fillMasterVIQQuestions();
            fillMapQuestions();
        }

        private void cbSlaveType_TextChanged(object sender, EventArgs e)
        {
            fillSlaveVIQQuestions();
            fillMapQuestions();
        }

        private void fillMapQuestions()
        {
            if (DS.Tables.Contains("MapQuestions"))
                DS.Tables["MapQuestions"].Clear();

            string s = "";

            s = "SELECT TQ1.QUESTION_NUMBER AS QN1, TQ2.QUESTION_NUMBER AS QN2, "+
                "QUESTIONS_MAPPING.QUESTION_MASTER_GUID, QUESTIONS_MAPPING.QUESTION_SLAVE_GUID \n"+
                "FROM (((QUESTIONS_MAPPING \n"+ 
	            "INNER JOIN TEMPLATE_QUESTIONS AS TQ1 \n"+
	            "ON QUESTIONS_MAPPING.QUESTION_MASTER_GUID = TQ1.QUESTION_GUID) \n"+
	            "INNER JOIN TEMPLATE_QUESTIONS AS TQ2 \n"+
	            "ON QUESTIONS_MAPPING.QUESTION_SLAVE_GUID = TQ2.QUESTION_GUID) \n"+
	            "INNER JOIN TEMPLATES AS TP1 \n"+
	            "ON TQ1.TEMPLATE_GUID = TP1.TEMPLATE_GUID) \n"+
	            "INNER JOIN TEMPLATES AS TP2 \n"+
	            "ON TQ2.TEMPLATE_GUID = TP2.TEMPLATE_GUID \n"+
                "WHERE TP1.TITLE='" + StrToSQLStr(cbMasterType.Text) + "'\n" +
                "and TP1.VERSION='" + StrToSQLStr(cbMasterVersion.Text) + "'\n" +
                "and TP2.TITLE='" + StrToSQLStr(cbSlaveType.Text) + "'\n" +
                "and TP2.VERSION='" + StrToSQLStr(cbSlaveVersion.Text) + "'\n" +
                "ORDER BY TQ1.SEQUENCE, TQ2.SEQUENCE";

            viqMappingAdapter = new OleDbDataAdapter(s, connection);

            viqMappingAdapter.Fill(DS, "MapQuestions");

            dgvMapping.DataSource = DS;
            dgvMapping.AutoGenerateColumns = true;
            dgvMapping.DataMember = "MapQuestions";

            dgvMapping.Columns["QN1"].HeaderText = "Master";
            dgvMapping.Columns["QN1"].FillWeight = 50;
            dgvMapping.Columns["QN1"].DefaultCellStyle.BackColor = adgvMaster.BackgroundColor;
            dgvMapping.Columns["QN2"].HeaderText = "Slave";
            dgvMapping.Columns["QN2"].FillWeight = 50;
            dgvMapping.Columns["QN2"].DefaultCellStyle.BackColor = adgvSlave.BackgroundColor;
            dgvMapping.Columns["QUESTION_MASTER_GUID"].Visible = false;
            dgvMapping.Columns["QUESTION_SLAVE_GUID"].Visible = false;
            
            foreach (DataGridViewColumn column in dgvMapping.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void btnMap_Click(object sender, EventArgs e)
        {
            //Map two selected questions

            if (adgvMaster.RowCount == 0) return;
            if (adgvSlave.RowCount == 0) return;

            string masterGUID = adgvMaster.CurrentRow.Cells["QUESTION_GUID"].Value.ToString();
            string slaveGUID = adgvSlave.CurrentRow.Cells["QUESTION_GUID"].Value.ToString();

            OleDbCommand cmd = new OleDbCommand("", connection);

            //Check whether the same record exists

            cmd.CommandText = "select Count(QUESTION_MASTER_GUID) as QC from QUESTIONS_MAPPING \n" +
                "where QUESTION_MASTER_GUID='{" + masterGUID + "}' and QUESTION_SLAVE_GUID='{" + slaveGUID + "}'";
            
            int recs = (int) cmd.ExecuteScalar();

            if (recs == 0)
            {
                cmd.CommandText = "insert into QUESTIONS_MAPPING (QUESTION_MASTER_GUID,QUESTION_SLAVE_GUID) \n" +
                    "values('" + masterGUID + "','" + slaveGUID + "')";
                cmd.ExecuteNonQuery();

                fillMapQuestions();
            }

            int rowIndex = -1;

            foreach (DataGridViewRow dgRow in dgvMapping.Rows)
            {
                if (dgRow.Cells["QUESTION_MASTER_GUID"].Value.ToString().Equals(masterGUID) &&
                    dgRow.Cells["QUESTION_SLAVE_GUID"].Value.ToString().Equals(slaveGUID))
                {
                    rowIndex = dgRow.Index;
                    break;
                }
            }

            if (rowIndex >= 0)
            {
                if (rowIndex <= dgvMapping.RowCount - 1)
                {
                    dgvMapping.CurrentCell = dgvMapping[0, rowIndex];
                    dgvMapping.FirstDisplayedCell = dgvMapping.CurrentCell;
                }
                else
                    dgvMapping.CurrentCell = dgvMapping[0, dgvMapping.RowCount - 1];
            }
        }

        private void btnAutoMap_Click(object sender, EventArgs e)
        {
            //Map questions with the same spelling

            if (adgvMaster.RowCount == 0) 
                return;
            
            if (adgvSlave.RowCount == 0) 
                return;

            var rslt = System.Windows.Forms.MessageBox.Show(
                "You are going to map questions with the same spelling for selected questionnaire by chapter.\n" +
                "Would you like to proceed?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (rslt != DialogResult.Yes) 
                return;

            string excludeChapters = "";

            FrmCopyMappingSettings form = new FrmCopyMappingSettings();

            form.excludeChapters = excludeChapters;
            form.defaultFormCaption = "Select chapters for mapping";
            form.defaultMapping = cbMasterType.Text + "(" + cbMasterVersion.Text + ") <-> " +
                cbSlaveType.Text + "(" + cbSlaveVersion.Text + ")";
            form.defaultMappingLable = "Mapping for";
            form.copyMapping = false;
            form.templateGUID = masterTemplateGUID;

            if (form.ShowDialog() == DialogResult.OK)
            {
                excludeChapters = form.excludeChapters;
                mapQuestions2(excludeChapters);
            }

        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            //Delete all mapped records for selected questionnairies
            if (adgvMaster.RowCount == 0) return;
            if (adgvSlave.RowCount == 0) return;
            if (dgvMapping.RowCount == 0) return;

            var rslt = System.Windows.Forms.MessageBox.Show(
                "You are going to delete all questions maping from questionnaire version \""+
                cbMasterVersion.Text+"\" and type \""+cbMasterType.Text+"\" to questions of questionnaire version \""+cbSlaveVersion.Text+
                "\" and type \""+cbSlaveType.Text+"\".\n" +
                "Would you like to proceed?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (rslt != DialogResult.Yes) return;

            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText = "delete from QUESTIONS_MAPPING \n" +
                "where QUESTION_MASTER_GUID in \n" +
                "(select QUESTION_GUID FROM TEMPLATE_QUESTIONS INNER JOIN TEMPLATES  \n" +
                "ON TEMPLATE_QUESTIONS.TEMPLATE_GUID = TEMPLATES.TEMPLATE_GUID \n" +
                "WHERE TEMPLATES.VERSION='" + StrToSQLStr(cbMasterVersion.Text) + "' \n" +
                "and TEMPLATES.TITLE='" + StrToSQLStr(cbMasterType.Text) + "') \n" +
                "and QUESTION_SLAVE_GUID in \n" +
                "(select QUESTION_GUID FROM TEMPLATE_QUESTIONS INNER JOIN TEMPLATES  \n" +
                "ON TEMPLATE_QUESTIONS.TEMPLATE_GUID = TEMPLATES.TEMPLATE_GUID \n" +
                "WHERE TEMPLATES.VERSION='" + StrToSQLStr(cbSlaveVersion.Text) + "' \n" +
                "and TEMPLATES.TITLE='" + StrToSQLStr(cbSlaveType.Text) + "') \n";
            cmd.ExecuteNonQuery();

            fillMapQuestions();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Delete selected question mapping

            if (adgvMaster.RowCount == 0) return;
            if (adgvSlave.RowCount == 0) return;
            if (dgvMapping.RowCount == 0) return;

            var rslt = System.Windows.Forms.MessageBox.Show(
                "You are going to delete maping for question No. \""+dgvMapping.CurrentRow.Cells["QN1"].Value.ToString()+
                "\" from questionnaire version \""+cbMasterVersion.Text+"\" and type \""+cbMasterType.Text+"\" with question No. \""+
                dgvMapping.CurrentRow.Cells["QN2"].Value.ToString()+"\" from questionnaire version \""+cbSlaveVersion.Text+
                "\" and type \""+cbSlaveType.Text+"\".\n" +
                "Would you like to proceed?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (rslt != DialogResult.Yes) return;

            int curRow = dgvMapping.CurrentCell.RowIndex;
            int curCol = dgvMapping.CurrentCell.ColumnIndex;
            
            string masterGUID = dgvMapping.CurrentRow.Cells["QUESTION_MASTER_GUID"].Value.ToString();
            string slaveGUID = dgvMapping.CurrentRow.Cells["QUESTION_SLAVE_GUID"].Value.ToString();

            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText = 
                "delete from QUESTIONS_MAPPING \n" +
                "where QUESTION_MASTER_GUID = '{" + masterGUID + "}' \n" +
                "and QUESTION_SLAVE_GUID = '{" + slaveGUID + "}'";
            cmd.ExecuteNonQuery();

            fillMapQuestions();

            if (dgvMapping.RowCount > curRow)
                dgvMapping.CurrentCell = dgvMapping[curCol, curRow];
        }

        private void dgvMapping_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Double click on mapping grid
            if (adgvMaster.RowCount == 0) return;
            if (adgvSlave.RowCount == 0) return;
            if (dgvMapping.RowCount == 0) return;

            int curRow = dgvMapping.CurrentCell.RowIndex;
            int curCol = dgvMapping.CurrentCell.ColumnIndex;

            string masterGUID = dgvMapping.CurrentRow.Cells["QUESTION_MASTER_GUID"].Value.ToString();
            string slaveGUID = dgvMapping.CurrentRow.Cells["QUESTION_SLAVE_GUID"].Value.ToString();

            int rowIndex = -1;

            if (curCol == 0)
            {
                //Click on Master cell

                foreach (DataGridViewRow dgRow in adgvMaster.Rows)
                {
                    if (dgRow.Cells["QUESTION_GUID"].Value.ToString().Equals(masterGUID))
                    {
                        rowIndex = dgRow.Index;
                        break;
                    }
                }

                if (rowIndex >= 0)
                {
                    adgvMaster.CurrentCell = adgvMaster[0, rowIndex];
                }
            }
            else
            {
                //Click on Slave cell

                foreach (DataGridViewRow dgRow in adgvSlave.Rows)
                {
                    if (dgRow.Cells["QUESTION_GUID"].Value.ToString().Equals(slaveGUID))
                    {
                        rowIndex = dgRow.Index;
                        break;
                    }
                }

                if (rowIndex >= 0)
                {
                    adgvSlave.CurrentCell = adgvSlave[0, rowIndex];
                }

            }

            //Show mapping questions form

            if (Control.ModifierKeys == Keys.Control)
            {

                FormMappingDetails form = new FormMappingDetails();

                form.masterNumber = dgvMapping.CurrentRow.Cells["QN1"].Value.ToString();
                form.masterGUID = dgvMapping.CurrentRow.Cells["QUESTION_MASTER_GUID"].Value.ToString();
                form.slaveNumber = dgvMapping.CurrentRow.Cells["QN2"].Value.ToString();
                form.slaveGUID = dgvMapping.CurrentRow.Cells["QUESTION_SLAVE_GUID"].Value.ToString();

                form.ShowDialog();
            }
        }

        private void dataGridView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
           
        }

        private void adgvSlave_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn newColumn = adgvSlave.Columns[e.ColumnIndex];
            //DataGridViewColumn oldColumn = adgvSlave.SortedColumn;
            DataGridViewColumn sequenceColumn = adgvSlave.Columns["SEQUENCE"];
            DataGridViewColumn sortColumn;
            SortOrder sortOrder;

            ListSortDirection direction;

            if (newColumn.Name == "QUESTION_NUMBER")
                sortColumn = sequenceColumn;
            else
                sortColumn = newColumn;

            // If oldColumn is null, then the DataGridView is not sorted. 
            if (oldSlaveColumn != null)
            {
                // Sort the same column again, reversing the SortOrder. 
                if (oldSlaveColumn == sortColumn)
                {
                    if (oldSlaveOrder == SortOrder.Ascending)
                    {
                        sortOrder=SortOrder.Descending;
                        direction = ListSortDirection.Descending;
                        oldSlaveColumn.HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                    else
                    {
                        sortOrder=SortOrder.Ascending;
                        direction = ListSortDirection.Ascending;
                        oldSlaveColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    sortOrder=SortOrder.Ascending;
                    direction = ListSortDirection.Ascending;
                    oldSlaveColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                }
            }
            else
            {
                if (newColumn.Name == "QUESTION_NUMBER")
                {
                    sortOrder=SortOrder.Descending;
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    sortOrder=SortOrder.Ascending;
                    direction = ListSortDirection.Ascending;
                }
            }

            // Sort the selected column.
            if (sortColumn.Name == "SEQUENCE")
            {
                adgvSlave.Sort(sortColumn, direction);

                if (oldSlaveColumn != newColumn)
                {
                    newColumn.HeaderCell.SortGlyphDirection =
                        direction == ListSortDirection.Ascending ?
                        SortOrder.Ascending : SortOrder.Descending;
                }
            }
            else
            {
                adgvSlave.Sort(newColumn, direction);

                if (oldSlaveColumn != newColumn)
                {
                    newColumn.HeaderCell.SortGlyphDirection =
                        direction == ListSortDirection.Ascending ?
                        SortOrder.Ascending : SortOrder.Descending;
                }
            }

            oldSlaveColumn = newColumn;
            oldSlaveOrder = sortOrder;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mapQuestions1()
        {

            OleDbCommand cmd = new OleDbCommand("", connection);
            cmd.CommandText = "insert into QUESTIONS_MAPPING (QUESTION_MASTER_GUID,QUESTION_SLAVE_GUID) \n" +
                "select QQ1.MASTER_GUID, QQ1.SLAVE_GUID \n" +
                "from \n" +
                "(SELECT Q1.QUESTION_GUID AS MASTER_GUID, Q2.QUESTION_GUID AS SLAVE_GUID  \n" +
                "FROM  \n" +
                "(SELECT *  \n" +
                "FROM TEMPLATE_QUESTIONS INNER JOIN TEMPLATES   \n" +
                "ON TEMPLATE_QUESTIONS.TEMPLATE_GUID = TEMPLATES.TEMPLATE_GUID  \n" +
                "WHERE TEMPLATES.VERSION='" + StrToSQLStr(cbMasterVersion.Text) + "'  \n" +
                "and TEMPLATES.TITLE='" + StrToSQLStr(cbMasterType.Text) + "' \n" +
                "and UCASE(QUESTION_TEXT)<>'ADDITIONAL COMMENTS') as Q1  \n" +
                "INNER JOIN  \n" +
                "(SELECT *  \n" +
                "FROM TEMPLATE_QUESTIONS INNER JOIN TEMPLATES   \n" +
                "ON TEMPLATE_QUESTIONS.TEMPLATE_GUID = TEMPLATES.TEMPLATE_GUID  \n" +
                "WHERE TEMPLATES.VERSION='" + StrToSQLStr(cbSlaveVersion.Text) + "'  \n" +
                "and TEMPLATES.TITLE='" + StrToSQLStr(cbSlaveType.Text) + "'  \n" +
                "and UCASE(QUESTION_TEXT)<>'ADDITIONAL COMMENTS') as Q2  \n" +
                "ON UCASE(LEFT(Q1.QUESTION_TEXT,255)) = UCASE(LEFT(Q2.QUESTION_TEXT,255))) AS QQ1 \n" +
                "LEFT JOIN \n" +
                "(SELECT QUESTIONS_MAPPING.QUESTION_MASTER_GUID, QUESTIONS_MAPPING.QUESTION_SLAVE_GUID  \n" +
                "FROM (((QUESTIONS_MAPPING  \n" +
                "INNER JOIN TEMPLATE_QUESTIONS AS TQ1  \n" +
                "ON QUESTIONS_MAPPING.QUESTION_MASTER_GUID = TQ1.QUESTION_GUID)  \n" +
                "INNER JOIN TEMPLATE_QUESTIONS AS TQ2  \n" +
                "ON QUESTIONS_MAPPING.QUESTION_SLAVE_GUID = TQ2.QUESTION_GUID)  \n" +
                "INNER JOIN TEMPLATES AS TP1  \n" +
                "ON TQ1.TEMPLATE_GUID = TP1.TEMPLATE_GUID)  \n" +
                "INNER JOIN TEMPLATES AS TP2  \n" +
                "ON TQ2.TEMPLATE_GUID = TP2.TEMPLATE_GUID  \n" +
                "WHERE TP1.TITLE='" + StrToSQLStr(cbMasterType.Text) + "' \n" +
                "and TP1.VERSION='" + StrToSQLStr(cbMasterVersion.Text) + "' \n" +
                "and TP2.TITLE='" + StrToSQLStr(cbSlaveType.Text) + "' \n" +
                "and TP2.VERSION='" + StrToSQLStr(cbSlaveVersion.Text) + "') AS QQ2 \n" +
                "ON QQ1.MASTER_GUID=QQ2.QUESTION_MASTER_GUID \n" +
                "AND QQ1.SLAVE_GUID=QQ2.QUESTION_SLAVE_GUID \n" +
                "WHERE QQ2.QUESTION_MASTER_GUID IS NULL";

            try
            {
                this.Cursor = Cursors.WaitCursor;

                cmd.ExecuteNonQuery();

                fillMapQuestions();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void mapQuestions2(string excludeChapters)
        {
            DataTable master = DS.Tables["MasterVIQ"];
            DataTable slave = DS.Tables["SlaveVIQ"];

            DataRow[] mRows = master.Select();
            DataRow[] sRows = slave.Select();

            string mGUID="";
            string sGUID="";
            bool wasChanged = false;


            List<string> listChapters = MainForm.ParseStringToList(listOfChapters, ",");


            //1 - Text
            //2 - GUID

            List<string> listExcludeChapters = MainForm.ParseStringToList(excludeChapters, ",");

            string section = "AutoMappingSettings";

            bool ignoreSigns = MainForm.StrToBool(MainForm.ReadIniValue(MainForm.iniPersonalFile, section, "IgnoreSigns"));
            string signs = MainForm.ReadIniValue(MainForm.iniPersonalFile, section, "Signs");
            bool clearMapping = MainForm.StrToBool(MainForm.ReadIniValue(MainForm.iniPersonalFile, section, "ClearMapping"));

            if (clearMapping)
            {
                for (int i = 0; i < listChapters.Count; i++)
                {
                    if (listExcludeChapters.IndexOf(Convert.ToString(i + 1)) < 0)
                        ClearChapterMapping(listChapters[i]);
                }

                fillMapQuestions();
            }

            DataTable map = DS.Tables["MapQuestions"];
            DataRow[] mapRows = map.Select();

            try
            {
                this.Cursor = Cursors.WaitCursor;

                foreach (DataRow dr in mRows)
                {
                    string chapterNumber = dr["CHAPTER_NUMBER"].ToString();

                    if (listExcludeChapters.IndexOf(chapterNumber) < 0)
                    {
                        string mText = dr.ItemArray[1].ToString().Trim();

                        if (ignoreSigns)
                        {
                            for (int i = 0; i < signs.Length; i++)
                            {
                                if (mText.EndsWith(signs[i].ToString()))
                                    mText = mText.Substring(0, mText.Length - 1);
                            }
                        }

                        if (!mText.Equals("Additional comments", StringComparison.OrdinalIgnoreCase))
                        {
                            bool eFlag = false;

                            foreach (DataRow drs in sRows)
                            {
                                string sText = drs.ItemArray[1].ToString().Trim();

                                if (ignoreSigns)
                                {
                                    for (int i = 0; i < signs.Length; i++)
                                    {
                                        if (sText.EndsWith(signs[i].ToString()))
                                            sText = sText.Substring(0, sText.Length - 1);
                                    }
                                }

                                if (sText.Equals(mText, StringComparison.OrdinalIgnoreCase))
                                {
                                    eFlag = true;
                                    mGUID = dr.ItemArray[2].ToString();
                                    sGUID = drs.ItemArray[2].ToString();

                                    break;
                                }
                            }

                            if (eFlag)
                            {
                                //Texts are the same

                                //Check whether there is the same record in questions mapping table

                                bool hasRec = false;

                                foreach (DataRow mapDr in mapRows)
                                {
                                    string m = mapDr.ItemArray[2].ToString();
                                    string s = mapDr.ItemArray[3].ToString();

                                    if (m.Equals(mGUID) && s.Equals(sGUID))
                                    {
                                        hasRec = true;
                                        break;
                                    }

                                }

                                if (!hasRec)
                                {
                                    OleDbCommand cmd = new OleDbCommand("", connection);

                                    cmd.CommandText =
                                        "insert into QUESTIONS_MAPPING(QUESTION_MASTER_GUID,QUESTION_SLAVE_GUID) \n" +
                                        "values('{" + mGUID + "}','{" + sGUID + "}')";
                                    cmd.ExecuteNonQuery();
                                    wasChanged = true;
                                }
                            }
                        }
                    }
                }

                if (wasChanged)
                {
                    fillMapQuestions();
                    System.Windows.Forms.MessageBox.Show("Automatic questions mapping completed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("There are no unmapped questions", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void copyGeneralMapping()
        {
            //Get master code
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select TYPE_CODE from TEMPLATES \n" +
                "where VERSION='" + StrToSQLStr(cbMasterVersion.Text) + "' \n" +
                "and TITLE='" + StrToSQLStr(cbMasterType.Text) + "'";

            string masterCode = (string)cmd.ExecuteScalar();

            //Get slave code
            cmd.CommandText =
                "select TYPE_CODE from TEMPLATES \n" +
                "where VERSION='" + StrToSQLStr(cbSlaveVersion.Text) + "' \n" +
                "and TITLE='" + StrToSQLStr(cbSlaveType.Text) + "'";

            string slaveCode = (string)cmd.ExecuteScalar();

            if ((cbMasterVersion.Text == cbSlaveVersion.Text) && (masterCode == slaveCode)) return;

            if (slaveCode.Substring(3, 2) == "01") return;

            string baseMasterCode = slaveCode.Substring(1, 2) + "01";

        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Resfresh master list of questions
            fillMasterVIQQuestions();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            fillSlaveVIQQuestions();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Map general questions
            GeneralMapping();
        }

        private void GeneralMapping()
        {
            //Update general questions mapping
            //Clear old mapping
            OleDbCommand cmd = new OleDbCommand("", connection);

            //Get list of available questionnairies for selected major version
            this.Cursor = Cursors.WaitCursor;

            try
            {
                cmd.CommandText =
                    "select TEMPLATE_GUID, TITLE, TYPE_CODE, VERSION \n" +
                    "from TEMPLATES \n" +
                    "where VERSION like '" + masterMajorVersion.ToString() + "' \n" +
                    "and VERSION not like '" + StrToSQLStr(cbMasterVersion.Text) + "' \n" +
                    "and TITLE not like '" + StrToSQLStr(cbMasterType.Text) + "'";

                OleDbDataAdapter masterTemplates = new OleDbDataAdapter(cmd);

                if (DS.Tables.Contains("MASTER_TEMPLATES"))
                    DS.Tables["MASTER_TEMPLATES"].Clear();

                masterTemplates.Fill(DS, "MASTER_TEMPLATES");


                cmd.CommandText =
                    "select TEMPLATE_GUID, TITLE, TYPE_CODE, VERSION \n" +
                    "from TEMPLATES \n" +
                    "where VERSION like '" + slaveMajorVersion.ToString() + "' \n" +
                    "and VERSION not like '" + StrToSQLStr(cbSlaveVersion.Text) + "' \n" +
                    "and TITLE not like '" + StrToSQLStr(cbSlaveType.Text) + "'";

                OleDbDataAdapter slaveTemplates = new OleDbDataAdapter(cmd);

                if (DS.Tables.Contains("SLAVE_TEMPLATES"))
                    DS.Tables["SLAVE_TEMPLATES"].Clear();

                slaveTemplates.Fill(DS, "SLAVE_TEMPLATES");

                //Create temporary table for mapping
                if (tableExists("TEMP_MAPPING"))
                {
                    //Clear table
                    cmd.CommandText =
                        "delete from TEMP_MAPPING";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd.CommandText =
                        "create table TEMP_MAPPING (ID Counter Primary Key, MGUID GUID, SGUID GUID)";

                    cmd.ExecuteNonQuery();
                }

                //Temporary table filling
                cmd.CommandText =
                    "insert into TEMP_MAPPING (MGUID, SGUID) \n" +
                    "select DISTINCT MGUID, SGUID \n" +
                    "from \n" +
                    "((select TEMPLATE_QUESTIONS.QUESTION_NUMBER, TEMPLATE_QUESTIONS.QUESTION_GUID as MGUID, \n" +
                    "TEMPLATE_QUESTIONS.TEMPLATE_GUID, TEMPLATES.TYPE_CODE, TEMPLATES.VERSION \n" +
                    "from \n" +
                    "TEMPLATE_QUESTIONS inner join TEMPLATES \n" +
                    "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID \n" +
                    "where \n" +
                    "TEMPLATES.VERSION like '" + masterMajorVersion.ToString() + "%' \n" +
                    "and TEMPLATE_QUESTIONS.CHAPTER_NUMBER not like '8%') as QQ1 \n" +
                    "inner join \n" +
                    "(SELECT TQ1.QUESTION_NUMBER AS QN1, TQ2.QUESTION_NUMBER AS QN2, " +
                    "QUESTIONS_MAPPING.QUESTION_MASTER_GUID, QUESTIONS_MAPPING.QUESTION_SLAVE_GUID \n" +
                    "FROM (((QUESTIONS_MAPPING \n" +
                    "INNER JOIN TEMPLATE_QUESTIONS AS TQ1 \n" +
                    "ON QUESTIONS_MAPPING.QUESTION_MASTER_GUID = TQ1.QUESTION_GUID) \n" +
                    "INNER JOIN TEMPLATE_QUESTIONS AS TQ2 \n" +
                    "ON QUESTIONS_MAPPING.QUESTION_SLAVE_GUID = TQ2.QUESTION_GUID) \n" +
                    "INNER JOIN TEMPLATES AS TP1 \n" +
                    "ON TQ1.TEMPLATE_GUID = TP1.TEMPLATE_GUID) \n" +
                    "INNER JOIN TEMPLATES AS TP2 \n" +
                    "ON TQ2.TEMPLATE_GUID = TP2.TEMPLATE_GUID \n" +
                    "WHERE TP1.TITLE='" + StrToSQLStr(cbMasterType.Text) + "'\n" +
                    "and TP1.VERSION='" + StrToSQLStr(cbMasterVersion.Text) + "'\n" +
                    "and TP2.TITLE='" + StrToSQLStr(cbSlaveType.Text) + "'\n" +
                    "and TP2.VERSION='" + StrToSQLStr(cbSlaveVersion.Text) + "'\n" +
                    "ORDER BY TQ1.SEQUENCE, TQ2.SEQUENCE ) as QMAP \n" +
                    "on QQ1.QUESTION_NUMBER=QMAP.QN1) \n" +
                    "inner join \n" +
                    "(select TEMPLATE_QUESTIONS.QUESTION_GUID as SGUID, TEMPLATE_QUESTIONS.QUESTION_NUMBER, TEMPLATES.TYPE_CODE \n" +
                    "from \n" +
                    "(TEMPLATE_QUESTIONS inner join TEMPLATES \n" +
                    "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID) \n" +
                    "where \n" +
                    "TEMPLATES.VERSION like '" + slaveMajorVersion.ToString() + "%' \n" +
                    ") as QQ2 \n" +
                    "on QMAP.QN2=QQ2.QUESTION_NUMBER \n" +
                    "where RIGHT(QQ1.TYPE_CODE,2)=RIGHT(QQ2.TYPE_CODE,2)";
                cmd.ExecuteNonQuery();

                //Add records to mapping table
                cmd.CommandText =
                    "insert into QUESTIONS_MAPPING (QUESTION_MASTER_GUID, QUESTION_SLAVE_GUID) \n" +
                    "select MGUID, SGUID \n" +
                    "from TEMP_MAPPING left join QUESTIONS_MAPPING \n" +
                    "on TEMP_MAPPING.MGUID=QUESTIONS_MAPPING.QUESTION_MASTER_GUID \n" +
                    "and TEMP_MAPPING.SGUID=QUESTIONS_MAPPING.QUESTION_SLAVE_GUID \n" +
                    "where QUESTIONS_MAPPING.QUESTION_MASTER_GUID is Null";
                cmd.ExecuteNonQuery();

                //Delete TEMP_MAPPING table
                cmd.CommandText =
                    "drop table TEMP_MAPPING";
                cmd.ExecuteNonQuery();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }


        }

        private Boolean tableExists(string tableName)
        {
            DataTable Tables = connection.GetSchema("Tables");
            DataRow[] tableRows;

            tableRows = Tables.Select();


            foreach (DataRow row in Tables.Rows)
            {
                if (tableName.CompareTo(Convert.ToString(row.ItemArray[2])) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        private void btnMasterSearch_Click(object sender, EventArgs e)
        {
            SearchMaster();
        }

        private void SearchMaster()
        {
            if (adgvMaster.Rows.Count == 0)
                return;

            String searchValue = tbxMasterSearch.Text;

            int rowIndex = -1;

            int curRow = adgvMaster.CurrentRow.Index;

            foreach (DataGridViewRow dgRow in adgvMaster.Rows)
            {
                string cellText = dgRow.Cells["QUESTION_TEXT"].Value.ToString().ToUpper();

                if (dgRow.Index > curRow && cellText.Contains(searchValue.ToUpper()))
                {
                    rowIndex = dgRow.Index;
                    break;
                }
            }

            if (rowIndex >= 0)
            {
                adgvMaster.CurrentCell = adgvMaster["QUESTION_TEXT", rowIndex];
            }
            else
            {
                MessageBox.Show("String was not found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSlaveSearch_Click(object sender, EventArgs e)
        {
            SearchSlave();
        }

        public void SearchSlave()
        {
            if (adgvSlave.Rows.Count == 0)
                return;

            String searchValue = tbxSlaveSearch.Text;

            int rowIndex = -1;

            int curRow = adgvSlave.CurrentRow.Index;

            foreach (DataGridViewRow dgRow in adgvSlave.Rows)
            {
                string cellText = dgRow.Cells["QUESTION_TEXT"].Value.ToString().ToUpper();

                if (dgRow.Index > curRow && cellText.Contains(searchValue.ToUpper()))
                {
                    rowIndex = dgRow.Index;
                    break;
                }
            }

            if (rowIndex >= 0)
            {
                adgvSlave.CurrentCell = adgvSlave["QUESTION_TEXT", rowIndex];
            }
            else
            {
                MessageBox.Show("String was not found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void adgvSlave_FilterStringChanged(object sender, EventArgs e)
        {
            bsSlave.Filter = adgvSlave.FilterString;
            SlaveSorting();
        }

        private void adgvSlave_SortStringChanged(object sender, EventArgs e)
        {
            SlaveSorting();
        }

        private void SlaveSorting()
        {
            string sortString = adgvSlave.SortString;

            if (sortString.Contains("[QUESTION_NUMBER]"))
                sortString = sortString.Replace("[QUESTION_NUMBER]", "[SEQUENCE]");

            bsSlave.Sort = sortString;
        }

        private void tbxSlaveSearch_BeforeDisplayingAutoComplete(object sender, TextBoxEx.TextBoxExAutoCompleteEventArgs e)
        {
            string Name = tbxSlaveSearch.Text.ToLower();

            List<string> Display = new List<string>();


            foreach (string Str in tbxSlaveSearch.AutoCompleteCustomSource)
            {
                if ((Str.ToLower().IndexOf(Name) > -1))
                {
                    Display.Add(Str);
                }
            }
            e.AutoCompleteList = Display;
            e.SelectedIndex = 0;
        }

        private void tbxSlaveSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchSlave();
        }

        private void tbxMasterSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchMaster();
        }

        private void tbxMasterSearch_BeforeDisplayingAutoComplete(object sender, TextBoxEx.TextBoxExAutoCompleteEventArgs e)
        {
            string Name = tbxMasterSearch.Text.ToLower();

            List<string> Display = new List<string>();


            foreach (string Str in tbxMasterSearch.AutoCompleteCustomSource)
            {
                if ((Str.ToLower().IndexOf(Name) > -1))
                {
                    Display.Add(Str);
                }
            }
            e.AutoCompleteList = Display;
            e.SelectedIndex = 0;
        }

        private void adgvMaster_FilterStringChanged(object sender, EventArgs e)
        {
            bsMaster.Filter = adgvMaster.FilterString;
            MasterSorting();
        }

        private void MasterSorting()
        {
            string sortString = adgvMaster.SortString;

            if (sortString.Contains("[QUESTION_NUMBER]"))
                sortString = sortString.Replace("[QUESTION_NUMBER]", "[SEQUENCE]");

            bsMaster.Sort = sortString;
        }

        private void adgvMaster_SortStringChanged(object sender, EventArgs e)
        {
            MasterSorting();
        }

        private void adgvMaster_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Double click on mapping grid
            if (adgvMaster.RowCount == 0) return;

            int curRow = adgvMaster.CurrentCell.RowIndex;
            int curCol = adgvMaster.CurrentCell.ColumnIndex;
            int vRow = adgvMaster.FirstDisplayedScrollingRowIndex;

            string masterGUID = adgvMaster.CurrentRow.Cells["QUESTION_GUID"].Value.ToString();
            int rowIndex = -1;

            if (curCol == 0)
            {
                if (adgvSlave.RowCount == 0) return;
                if (dgvMapping.RowCount == 0) return;

                foreach (DataGridViewRow dgRow in dgvMapping.Rows)
                {
                    if (dgRow.Cells["QUESTION_MASTER_GUID"].Value.ToString().Equals(masterGUID))
                    {
                        rowIndex = dgRow.Index;
                        break;
                    }
                }

                if (rowIndex >= 0)
                {
                    dgvMapping.CurrentCell = dgvMapping[0, rowIndex];
                }
            }
            else
            {
                //Show question details
                QuestionDetailsForm qdForm = new QuestionDetailsForm(connection, DS, this.Font, this.Icon);

                qdForm.textVersion.Text = masterMajorVersion.ToString();
                qdForm.textType.Text = cbMasterType.Text;
                qdForm.textQuestionText.Text = adgvMaster.CurrentRow.Cells["QUESTION_TEXT"].Value.ToString();
                qdForm.cbSubchapter.Text = adgvMaster.CurrentRow.Cells["SUBCHAPTER"].Value.ToString();
                qdForm.textKeyIndex.Text = adgvMaster.CurrentRow.Cells["KEY_INDEX"].Value.ToString();
                qdForm.textQuestionNo.Text = adgvMaster.CurrentRow.Cells["QUESTION_NUMBER"].Value.ToString();
                qdForm.textChapterNo.Text = adgvMaster.CurrentRow.Cells["CHAPTER_NUMBER"].Value.ToString();
                qdForm.textChapter.Text = adgvMaster.CurrentRow.Cells["CHAPTER_NAME"].Value.ToString();

                var rslt = qdForm.ShowDialog();

                if (rslt == DialogResult.OK)
                {
                    OleDbCommand cmd = new OleDbCommand("", connection);

                    if ((adgvMaster.CurrentRow.Cells["KEY_ID"].Value == null) ||
                        (adgvMaster.CurrentRow.Cells["KEY_ID"].Value.ToString().Length == 0))
                    {
                        string shortType = "";

                        if (adgvMaster.CurrentRow.Cells["QUESTION_NUMBER"].Value.ToString().StartsWith("8"))
                            shortType = viqShortType;

                        cmd.CommandText =
                            "insert into TEMPLATE_KEYS (QUESTION_NUMBER, SUBCHAPTER, KEY_INDEX, VIQ_MAJOR_VERSION, VIQ_SHORT_TYPE) \n" +
                            "values ('" + adgvMaster.CurrentRow.Cells["QUESTION_NUMBER"].Value.ToString() + "', \n'" +
                                        StrToSQLStr(qdForm.cbSubchapter.Text) + "', \n'" +
                                        StrToSQLStr(qdForm.textKeyIndex.Text) + "', \n" +
                                        masterMajorVersion.ToString() + ", \n'" +
                                        StrToSQLStr(viqShortType) + "')";
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        if ((qdForm.cbSubchapter.Text.Length == 0) && (qdForm.textKeyIndex.Text.Length == 0))
                        {
                            cmd.CommandText =
                                "delete from TEMPLATE_KEYS \n" +
                                "where ID=" + adgvMaster.CurrentRow.Cells["KEY_ID"].Value.ToString();
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd.CommandText =
                                "update TEMPLATE_KEYS set\n" +
                                "SUBCHAPTER='" + StrToSQLStr(qdForm.cbSubchapter.Text) + "', \n" +
                                "KEY_INDEX='" + StrToSQLStr(qdForm.textKeyIndex.Text) + "' \n" +
                                "where ID=" + adgvMaster.CurrentRow.Cells["KEY_ID"].Value.ToString();
                            cmd.ExecuteNonQuery();
                        }
                    }


                    if (DS.Tables.Contains("MasterVIQ"))
                        DS.Tables["MasterVIQ"].Clear();

                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        viqMasterAdapter.Fill(DS, "MasterVIQ");
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }

                    adgvMaster.FirstDisplayedScrollingRowIndex = vRow;
                    adgvMaster.CurrentCell = adgvMaster[curCol, curRow];
                }
            }
        }

        private void adgvMaster_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn newColumn = adgvMaster.Columns[e.ColumnIndex];
            //DataGridViewColumn oldColumn = dgvMaster.SortedColumn;
            DataGridViewColumn sequenceColumn = adgvMaster.Columns["SEQUENCE"];
            DataGridViewColumn sortColumn;
            SortOrder sortOrder;

            ListSortDirection direction;

            if (newColumn.Name == "QUESTION_NUMBER")
                sortColumn = sequenceColumn;
            else
                sortColumn = newColumn;

            // If oldColumn is null, then the DataGridView is not sorted. 
            if (oldMasterColumn != null)
            {
                // Sort the same column again, reversing the SortOrder. 
                if (oldMasterColumn == sortColumn)
                {
                    if (oldMasterOrder == SortOrder.Ascending)
                    {
                        sortOrder = SortOrder.Descending;
                        direction = ListSortDirection.Descending;
                        oldMasterColumn.HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                    else
                    {
                        sortOrder = SortOrder.Ascending;
                        direction = ListSortDirection.Ascending;
                        oldMasterColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    sortOrder = SortOrder.Ascending;
                    direction = ListSortDirection.Ascending;
                    oldMasterColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                }
            }
            else
            {
                if (newColumn.Name == "QUESTION_NUMBER")
                {
                    sortOrder = SortOrder.Descending;
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    sortOrder = SortOrder.Ascending;
                    direction = ListSortDirection.Ascending;
                }
            }

            // Sort the selected column.
            if (sortColumn.Name == "SEQUENCE")
            {
                adgvMaster.Sort(sortColumn, direction);

                if (oldMasterColumn != newColumn)
                {
                    newColumn.HeaderCell.SortGlyphDirection =
                        direction == ListSortDirection.Ascending ?
                        SortOrder.Ascending : SortOrder.Descending;
                }
            }
            else
            {
                adgvMaster.Sort(newColumn, direction);

                if (oldMasterColumn != newColumn)
                {
                    newColumn.HeaderCell.SortGlyphDirection =
                        direction == ListSortDirection.Ascending ?
                        SortOrder.Ascending : SortOrder.Descending;
                }
            }

            oldMasterColumn = sortColumn;
            oldMasterOrder = sortOrder;
        }

        private void btnCopyMapping_Click(object sender, EventArgs e)
        {
            //Copy mapping

            if (cbMasterVersion.Text.Length == 0 || cbMasterType.Text.Length == 0)
            {
                MessageBox.Show("Please select master questionnaire version and type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cbSlaveVersion.Text.Length==0 || cbSlaveType.Text.Length==0)
            {
                MessageBox.Show("Please select slave questionnaire version and type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FrmCopyMappingSettings form = new FrmCopyMappingSettings();

            form.qVersion = cbMasterVersion.Text;
            form.templateGUID = masterTemplateGUID;
            form.excludeChapters = "8";

            form.ShowDialog();

            string excludeChapters = form.excludeChapters;
            string masterSlaveGUID = form.masterSlaveGUID;

            int pos=masterSlaveGUID.IndexOf(";");

            string copyMasterGUID = "{"+masterSlaveGUID.Substring(0, pos)+"}";
            string copySlaveGUID = "{"+masterSlaveGUID.Substring(pos + 1)+"}";

            List<string> chaptersList = MainForm.ParseStringToList(listOfChapters, ",");
            List<string> excludeChaptersList=MainForm.ParseStringToList(excludeChapters,",");

            string sqlChapters="";

            if (excludeChaptersList.Count>0)
            {
                for (int i = 0; i < chaptersList.Count; i++)
                {
                    if (excludeChaptersList.IndexOf(chaptersList[i])<0)
                    {
                        if (sqlChapters.Length == 0)
                            sqlChapters = "TQ1.CHAPTER_NUMBER='" + chaptersList[i] + "'";
                        else
                            sqlChapters = sqlChapters + "\n" +
                                "or TQ1.CHAPTER_NUMBER='" + chaptersList[i] + "'";
                    }
                }

                if (sqlChapters.Length > 0)
                    sqlChapters = "and (" + sqlChapters + ") \n";
            }

            string sql=
                "insert into QUESTIONS_MAPPING (QUESTION_MASTER_GUID, QUESTION_SLAVE_GUID) \n"+
                "select \n"+
                "MASTER_GUID, \n"+
                "SLAVE_GUID \n"+
                "from \n"+
                "( \n"+
                "select \n"+
                "TQQ1.QUESTION_GUID as MASTER_GUID, \n"+
                "TQQ2.QUESTION_GUID as SLAVE_GUID \n"+
                "from \n"+
                "(( \n"+
                "select  \n"+
                "TQ1.QUESTION_NUMBER as MASTER_QNUMBER, \n"+
                "TQ2.QUESTION_NUMBER as SLAVE_QNUMBER \n"+
                "from \n"+
                "(QUESTIONS_MAPPING inner join \n"+
                "TEMPLATE_QUESTIONS as TQ1 \n"+
                "on QUESTIONS_MAPPING.QUESTION_MASTER_GUID=TQ1.QUESTION_GUID) \n"+
                "inner join \n"+
                "TEMPLATE_QUESTIONS as TQ2 \n"+
                "on QUESTIONS_MAPPING.QUESTION_SLAVE_GUID=TQ2.QUESTION_GUID \n"+
                "where \n"+
                "TQ1.TEMPLATE_GUID='"+copyMasterGUID+"' \n"+
                sqlChapters+
                "and TQ2.TEMPLATE_GUID='"+copySlaveGUID+"' \n"+
                ") as Q1 \n"+
                "inner join TEMPLATE_QUESTIONS as TQQ1 \n"+
                "on TQQ1.QUESTION_NUMBER=Q1.MASTER_QNUMBER) \n"+
                "inner join TEMPLATE_QUESTIONS as TQQ2 \n"+
                "on TQQ2.QUESTION_NUMBER=Q1.SLAVE_QNUMBER \n"+
                "where \n"+
                "TQQ1.TEMPLATE_GUID='" + masterTemplateGUID + "' \n" +
                "and TQQ2.TEMPLATE_GUID='" + slaveTemplateGUID + "' \n" +
                ") as QQ1 \n"+
                "left join QUESTIONS_MAPPING \n"+
                "on QQ1.MASTER_GUID=QUESTIONS_MAPPING.QUESTION_MASTER_GUID \n"+
                "and QQ1.SLAVE_GUID=QUESTIONS_MAPPING.QUESTION_SLAVE_GUID";

            OleDbCommand cmd = new OleDbCommand(sql, connection);

            if (MainForm.cmdExecute(cmd)<0)
                MessageBox.Show("Failed to copy mapping", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                fillMapQuestions();
        }

        private string GetTemplateGUID(string tempVersion, string tempTitle)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select CStr(TEMPLATE_GUID) \n" +
                "from TEMPLATES \n" +
                "where \n" +
                "TITLE='" + MainForm.StrToSQLStr(tempTitle) + "' \n" +
                "and VERSION='" + MainForm.StrToSQLStr(tempVersion) + "'";

            string tempGUID =(string)  cmd.ExecuteScalar();

            return tempGUID;
        }

        private string GetChapters(string templateGUID)
        {

            string chapters = "";

            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "SELECT \n" +
                "TEMPLATE_QUESTIONS.TEMPLATE_GUID, \n" +
                "CInt(CHAPTER_NUMBER) as CHAPTER_NUMBER, \n" +
                "CHAPTER_NAME \n" +
                "FROM  \n" +
                "[TEMPLATE_QUESTIONS] inner join TEMPLATES \n" +
                "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID \n" +
                "where \n" +
                "TEMPLATES.TEMPLATE_GUID='" + templateGUID + "' \n" +
                "group by TEMPLATE_QUESTIONS.TEMPLATE_GUID, CInt(CHAPTER_NUMBER), CHAPTER_NAME";

            OleDbDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int chapterNumber = Convert.ToInt32(reader["CHAPTER_NUMBER"].ToString());

                    if (chapters.Length == 0)
                        chapters = chapterNumber.ToString();
                    else
                        chapters = chapters + "," + chapterNumber.ToString();

                }
            }

            reader.Close();

            return chapters;
        }

        private void btnAutoMapSet_Click(object sender, EventArgs e)
        {
            FrmAutoMappingSettings form = new FrmAutoMappingSettings();

            form.ShowDialog();
        }

        private void ClearChapterMapping(string chapterNumber)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText=
                "delete from QUESTIONS_MAPPING \n"+
                "where \n"+
                "QUESTION_MASTER_GUID in \n"+
                "(select QUESTION_GUID \n"+
                "from TEMPLATE_QUESTIONS \n"+
                "where TEMPLATE_GUID='"+masterTemplateGUID+"' \n"+
                "and CHAPTER_NUMBER='"+chapterNumber+"')";

            if (MainForm.cmdExecute(cmd)<0)
                MessageBox.Show("Failed to delete mapping for chapter " + chapterNumber, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void CopyChapterMapping(string copyMasterGUID, string copySlaveGUID, string chapterNumber)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select";
        }
    }
}
