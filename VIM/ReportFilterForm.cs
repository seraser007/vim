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
    public partial class ReportFilterForm : Form
    {
        OleDbConnection connection;
        DataSet DS;

        public string strReportNumber
        {
            get { return valueReportNumber.Text.Trim(); }
            set { valueReportNumber.Text = value; }
        }

        public string strInspector
        {
            get { return valueInspector.Text; }
            set { valueInspector.Text = value; }
        }

        public string strVessel
        {
            get { return valueVessel.Text; }
            set { valueVessel.Text = value; }
        }

        public string strCompany
        {
            get { return valueCompany.Text; }
            set { valueCompany.Text = value; }
        }

        public string strPort
        {
            get { return valuePort.Text; }
            set { valuePort.Text = value; }
        }

        public string strOffice
        {
            get { return valueOffice.Text; }
            set { valueOffice.Text = value; }
        }

        public string strDOC
        {
            get { return valueDOC.Text; }
            set { valueDOC.Text = value; }
        }

        public string strHullClass
        {
            get { return valueClass.Text; }
            set { valueClass.Text = value; }
        }

        public string strMaster
        {
            get { return valueMaster.Text; }
            set { valueMaster.Text = value; }
        }

        public string strChiefEngineer
        {
            get { return valueChiefEngineer.Text; }
            set { valueChiefEngineer.Text = value; }
        }

        public string strInspectionType
        {
            get { return valueInspectionType.Text.Trim(); }
            set { valueInspectionType.Text = value; }
        }

        public string strMemorandum
        {
            get { return valueMemorandum.Text.Trim(); }
            set { valueMemorandum.Text = value; }
        }

        public string inspectionTypeCondition
        {
            get { return conditionInspectionType.Text.Trim(); }
            set { conditionInspectionType.Text = value; }
        }

        public string memorandumCondition
        {
            get { return conditionMemorandum.Text.Trim(); }
            set { conditionMemorandum.Text = value; }
        }

        public string reportNumberCondition
        {
            get { return conditionReportNumber.Text.Trim(); }
            set { conditionReportNumber.Text = value; }
        }

        public string inspectorCondition
        {
            get { return conditionInspector.Text.Trim(); }
            set { conditionInspector.Text = value; }
        }

        public string vesselCondition
        {
            get { return conditionVessel.Text.Trim(); }
            set { conditionVessel.Text = value; }
        }

        public string dateCondition
        {
            get { return conditionDate.Text.Trim(); }
            set { conditionDate.Text = value; }
        }

        public string companyCondition
        {
            get { return conditionCompany.Text.Trim(); }
            set { conditionCompany.Text = value; }
        }

        public string portCondition
        {
            get { return conditionPort.Text.Trim(); }
            set { conditionPort.Text = value; }
        }

        public string officeConfition
        {
            get { return conditionOffice.Text.Trim(); }
            set { conditionOffice.Text = value; }
        }

        public string docCondition
        {
            get { return conditionDOC.Text.Trim(); }
            set { conditionDOC.Text = value; }
        }

        public string classCondition
        {
            get { return conditionClass.Text.Trim(); }
            set { conditionClass.Text = value; }
        }

        public string masterCondition
        {
            get { return conditionMaster.Text.Trim(); }
            set { conditionMaster.Text = value; }
        }

        public string chiefEngineerCondition
        {
            get { return conditionChiefEngineer.Text.Trim(); }
            set { conditionChiefEngineer.Text = value; }
        }

        public DateTime date1Value
        {
            get { return valueDate.Value; }
            set { valueDate.Value = value; }
        }

        public DateTime date2Value
        {
            get { return valueDate2.Value; }
            set { valueDate2.Value = value; }
        }

        public bool synchronize
        {
            get { return checkSynchronize.Checked; }
            set { checkSynchronize.Checked = value; }
        }

        public ReportFilterForm(DataSet mainDS, OleDbConnection mainConnection, Font mainFont, Icon mainIcon)
        {
            this.Icon = mainIcon;

            DS = mainDS;
            connection = mainConnection;
            this.Font = mainFont;

            InitializeComponent();

            label9.Font = new Font(mainFont.Name, mainFont.Size, FontStyle.Bold);
            label19.Font = new Font(mainFont.Name, mainFont.Size, FontStyle.Bold);
            label20.Font = new Font(mainFont.Name, mainFont.Size, FontStyle.Bold);

            DataTable vessels = DS.Tables["VESSELS_LIST"];

            valueVessel.DataSource = vessels;
            valueVessel.DisplayMember = "VESSEL_NAME";
            valueVessel.ValueMember = "VESSEL_GUID";
            

            DataTable inspectors = DS.Tables["INSPECTORS_LIST"];

            valueInspector.DataSource = inspectors;
            valueInspector.DisplayMember = "INSPECTOR_NAME";
            valueInspector.ValueMember = "INSPECTOR_GUID";
            valueInspector.Text = "";

            DataTable reports = DS.Tables["REPORTS_LIST"];

            valueReportNumber.DataSource = reports;
            valueReportNumber.DisplayMember = "REPORT_CODE";
            valueReportNumber.Text = "";

            DataTable ports = DS.Tables["PORTS_LIST"];

            valuePort.DataSource = ports;
            valuePort.DisplayMember = "INSPECTION_PORT";
            valuePort.Text = "";

            DataTable companies = DS.Tables["COMPANIES_LIST"];

            valueCompany.DataSource = companies;
            valueCompany.DisplayMember = "COMPANY";
            valueCompany.Text = "";

            DataTable offices = DS.Tables["OFFICES_LIST"];

            valueOffice.DataSource = offices;
            valueOffice.DisplayMember = "OFFICE_ID";
            valueOffice.Text = "";

            DataTable docs = DS.Tables["DOCS_LIST"];

            valueDOC.DataSource = docs;
            valueDOC.DisplayMember = "DOC_ID";
            valueDOC.Text = "";

            DataTable classes = DS.Tables["CLASSES_LIST"];

            valueClass.DataSource = classes;
            valueClass.DisplayMember = "HULL_CLASS";
            valueClass.Text = "";

            fillMasters();
            fillChiefEngineers();
            FillInspectionType();
            FillMemorandums();

            

        }

        private void FillInspectionType()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select INSPECTION_TYPE_ID, INSPECTION_TYPE \n" +
                "from INSPECTION_TYPES \n" +
                "union \n" +
                "select TOP 1 0 as INSPECTION_TYPE_ID, 'All' as INSPECTION_TYPE \n" +
                "from OPTIONS \n" +
                "order by INSPECTION_TYPE";

            OleDbDataAdapter da = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("INSPECTION_TYPE_LIST"))
                DS.Tables["INSPECTION_TYPE_LIST"].Clear();

            da.Fill(DS, "INSPECTION_TYPE_LIST");

            valueInspectionType.DataSource = DS.Tables["INSPECTION_TYPE_LIST"];
            valueInspectionType.DisplayMember = "INSPECTION_TYPE";
            valueInspectionType.ValueMember = "INSPECTION_TYPE_ID";
        }

        private void FillMemorandums()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select MEMORANDUM_ID, MEMORANDUM_TYPE \n" +
                "from MEMORANDUMS \n" +
                "union \n" +
                "select TOP 1 0 as MEMORANDUM_ID, '' as MEMORANDUM_TYPE \n" +
                "from OPTIONS \n" +
                "order by MEMORANDUM_TYPE";

            OleDbDataAdapter da = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("MEMORANDUM_LIST"))
                DS.Tables["MEMORANDUM_LIST"].Clear();

            da.Fill(DS, "MEMORANDUM_LIST");

            valueMemorandum.DataSource = DS.Tables["MEMORANDUM_LIST"];
            valueMemorandum.DisplayMember = "MEMORANDUM_TYPE";
            valueMemorandum.ValueMember = "MEMORANDUM_ID";
        }

        private void conditionDate_TextChanged(object sender, EventArgs e)
        {
            valueDate2.Visible=conditionDate.Text.Contains("Between");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conditionClass.Text = "";
            conditionCompany.Text = "";
            conditionDate.Text = "";
            conditionInspector.Text = "";
            conditionOffice.Text = "";
            conditionDOC.Text = "";
            conditionPort.Text = "";
            conditionReportNumber.Text = "";
            conditionVessel.Text = "";

            valueClass.Text = "";
            valueCompany.Text = "";
            valueDate.Value = DateTime.Today;
            valueDate2.Value = DateTime.Today;
            valueInspector.Text = "";
            valueOffice.Text = "";
            valueDOC.Text = "";
            valuePort.Text = "";
            valueReportNumber.Text = "";
            valueVessel.Text = "";
        }

        private void fillMasters()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select * from \n" +
                "(select MASTER_GUID, MASTER_NAME, MASTER_NOTES, PERSONAL_ID \n" +
                "from MASTERS \n" +
                "union \n" +
                "select Null as MASTER_GUID, '' as MASTER_NAME, '' as MASTER_NOTES, '' as PERSONAL_ID \n" +
                "from Fonts) \n" +
                "order by MASTER_NAME";

            OleDbDataAdapter mastersListAdapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("MASTERS_LIST"))
                DS.Tables["MASTERS_LIST"].Clear();

            mastersListAdapter.Fill(DS, "MASTERS_LIST");

            valueMaster.DataSource = DS.Tables["MASTERS_LIST"];
            valueMaster.DisplayMember = "MASTER_NAME";
            valueMaster.ValueMember = "MASTER_GUID";

        }

        private void fillChiefEngineers()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select * from \n" +
                "(select CHENG_GUID, CHENG_NAME, CHENG_NOTES, PERSONAL_ID \n" +
                "from CHIEF_ENGINEERS \n" +
                "union \n" +
                "select "+MainForm.GuidToStr(MainForm.zeroGuid)+" as CHENG_GUID, '' as CHENG_NAME, '' as CHENG_NOTES, '' as PERSONAL_ID \n" +
                "from CHIEF_ENGINEERS ) \n" +
                "order by CHENG_NAME";

            OleDbDataAdapter chengListAdapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("CHENG_LIST"))
                DS.Tables["CHENG_LIST"].Clear();

            chengListAdapter.Fill(DS, "CHENG_LIST");

            valueChiefEngineer.DataSource = DS.Tables["CHENG_LIST"];
            valueChiefEngineer.DisplayMember = "CHENG_NAME";
            valueChiefEngineer.ValueMember = "CHENG_GUID";

        }

        private void ReportFilterForm_Shown(object sender, EventArgs e)
        {
            valueReportNumber.Text = strReportNumber;
            valueVessel.Text = strVessel;
            valueInspector.Text = strInspector;
            valuePort.Text = strPort;
            valueOffice.Text = strOffice;
            valueDOC.Text = strDOC;
            valueCompany.Text = strCompany;
            valueClass.Text = strHullClass;
            valueMaster.Text = strMaster;
            valueChiefEngineer.Text = strChiefEngineer;
        }
    }
}
