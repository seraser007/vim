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
    public partial class FrmWrongQuestionnaire : Form
    {
        string _questionGUID = "";
        string _qTitle = "";
        string _qVersion = "";
        string _qType = "";

        public string qTitle
        {
            set { lblTitle.Text = value; }
            get { return _qTitle; }
        }

        public string qVersion
        {
            set { lblVersion.Text = value; }
            get { return _qVersion; }
        }

        public string qType
        {
            set { lblType.Text = value; }
            get { return _qType; }
        }

        public string questionGUID
        {
            set { _questionGUID = value; }
        }

        OleDbConnection connection;
        DataSet DS;

        public FrmWrongQuestionnaire(OleDbConnection mainConnection, DataSet mainDS, Font mainFont)
        {
            connection = mainConnection;
            DS = mainDS;
            this.Font = mainFont;

            InitializeComponent();
        }

        private void FrmWrongQuestionnaire_Shown(object sender, EventArgs e)
        {
            //Show form first time
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select \n" +
                "TEMPLATES.TEMPLATE_GUID, \n" +
                "TEMPLATES.VERSION, \n" +
                "TEMPLATES.TITLE, \n" +
                "TEMPLATES.TYPE_CODE \n" +
                "from \n" +
                "TEMPLATES inner join \n" +
                "( \n" +
                "select \n" +
                "TEMPLATE_GUID \n" +
                "from  \n" +
                "TEMPLATE_QUESTIONS \n" +
                "where \n" +
                "QUESTION_GUID='"+_questionGUID+"' \n" +
                ") as TQ \n" +
                "on TEMPLATES.TEMPLATE_GUID=TQ.TEMPLATE_GUID \n" +
                "where  \n" +
                "TEMPLATES.TYPE_CODE like '%1' \n" +
                "order by TEMPLATES.VERSION";

            OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("WRONG_TEMPLATES"))
                DS.Tables["WRONG_TEMPLATES"].Clear();

            adpt.Fill(DS, "WRONG_TEMPLATES");

            dgvTemplates.AutoGenerateColumns = true;
            dgvTemplates.DataSource = DS;
            dgvTemplates.DataMember = "WRONG_TEMPLATES";

            dgvTemplates.Columns["TEMPLATE_GUID"].Visible = false;

            dgvTemplates.Columns["VERSION"].HeaderText = "Version";
            dgvTemplates.Columns["VERSION"].FillWeight = 30;

            dgvTemplates.Columns["TITLE"].HeaderText = "Type";
            dgvTemplates.Columns["TITLE"].FillWeight = 50;

            dgvTemplates.Columns["TYPE_CODE"].HeaderText = "Code";
            dgvTemplates.Columns["TYPE_CODE"].FillWeight = 20;
        }

    }
}
