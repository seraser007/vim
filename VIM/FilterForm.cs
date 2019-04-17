using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Microsoft.Win32;
using System.IO;

namespace VIM
{
    public partial class FilterForm : Form
    {
        DataSet DS;
        OleDbConnection connection;

        public bool useDefault
        {
            get { return rbtnUseDefault.Checked; }
            set { rbtnUseDefault.Checked = value; }
        }

        public bool lastSetting
        {
            get { return rbtnLastSetting.Checked; }
            set { rbtnLastSetting.Checked = value; }
        }

        public bool showVessel
        {
            get { return chbShowVessel.Checked; }
            set { chbShowVessel.Checked = value; }
        }

        public bool showInspector
        {
            get { return chbShowInspector.Checked; }
            set { chbShowInspector.Checked = value; }
        }

        public bool showReportNumber
        {
            get { return chbShowReportNumber.Checked; }
            set { chbShowReportNumber.Checked = value; }
        }

        public bool showQuestionNumber
        {
            get { return chbShowQuestionNumber.Checked; }
            set { chbShowQuestionNumber.Checked = value; }
        }

        public bool showObservation
        {
            get { return chbShowObservation.Checked; }
            set { chbShowObservation.Checked = value; }
        }

        public bool showInspectorComments
        {
            get { return chbShowInspectorComments.Checked; }
            set { chbShowInspectorComments.Checked = value; }
        }

        public bool showTechnicalComments
        {
            get { return chbShowTechnicalComments.Checked; }
            set { chbShowTechnicalComments.Checked = value; }
        }

        public bool showOperatorComments
        {
            get { return chbShowOperatorComments.Checked; }
            set { chbShowOperatorComments.Checked = value; }
        }

        public bool showQuestionGUID
        {
            get { return chbShowQuestionGUID.Checked; }
            set { chbShowQuestionGUID.Checked = value; }
        }

        public bool showDate
        {
            get { return chbShowDate.Checked; }
            set { chbShowDate.Checked = value; }
        }

        public bool showCompany
        {
            get { return chbShowCompany.Checked; }
            set { chbShowCompany.Checked = value; }
        }

        public bool showPort
        {
            get { return chbShowPort.Checked; }
            set { chbShowPort.Checked = value; }
        }

        public bool showOffice
        {
            get { return chbShowOffice.Checked; }
            set { chbShowOffice.Checked = value; }
        }

        public bool showDOC
        {
            get { return chbShowDOC.Checked; }
            set { chbShowDOC.Checked = value; }
        }

        public bool showHullClass
        {
            get { return chbShowHullClass.Checked; }
            set { chbShowHullClass.Checked = value; }
        }

        public bool showMaster
        {
            get { return chbShowMaster.Checked; }
            set { chbShowMaster.Checked = value; }
        }

        public bool showChiefEngineer
        {
            get { return chbShowChiefEngineer.Checked; }
            set { chbShowChiefEngineer.Checked = value; }
        }

        public bool showSubchapter
        {
            get { return chbShowSubchapter.Checked; }
            set { chbShowSubchapter.Checked = value; }
        }

        public bool showKeyWords
        {
            get { return chbShowKeyWords.Checked; }
            set { chbShowKeyWords.Checked = value; }
        }

        public bool showQuestionnaireType
        {
            get { return chbShowQuestionnaireType.Checked; }
            set { chbShowQuestionnaireType.Checked = value; }
        }

        public bool showFleet
        {
            get { return chbShowFleet.Checked; }
            set { chbShowFleet.Checked = value; }
        }

        public string conditionVessel
        {
            get { return cbConditionVessel.Text; }
            set { cbConditionVessel.Text = value; }
        }

        public string conditionInspector
        {
            get { return cbConditionInspector.Text; }
            set { cbConditionInspector.Text = value; }
        }

        public string conditionQuestionNumber
        {
            get { return cbConditionQuestionNumber.Text; }
            set { cbConditionQuestionNumber.Text = value; }
        }

        public string conditionReportNumber
        {
            get { return cbConditionReportNumber.Text; }
            set { cbConditionReportNumber.Text = value; }
        }

        public string conditionObservation
        {
            get { return cbConditionObservation.Text; }
            set { cbConditionObservation.Text = value; }
        }

        public string conditionInspectorComments
        {
            get { return cbConditionInspectorComments.Text; }
            set { cbConditionInspectorComments.Text = value; }
        }

        public string conditionTechnicalComments
        {
            get { return cbConditionTechnicalComments.Text; }
            set { cbConditionTechnicalComments.Text = value; }
        }

        public string conditionOperatorComments
        {
            get { return cbConditionOperatorComments.Text; }
            set { cbConditionOperatorComments.Text = value; }
        }

        public string conditionQuestionGUID
        {
            get { return cbConditionQuestionGUID.Text; }
            set { cbConditionQuestionGUID.Text = value; }
        }

        public string conditionDate
        {
            get { return cbConditionDate.Text; }
            set { cbConditionDate.Text = value; }
        }

        public string conditionCompany
        {
            get { return cbConditionCompany.Text; }
            set { cbConditionCompany.Text = value; }
        }

        public string conditionPort
        {
            get { return cbConditionPort.Text; }
            set { cbConditionPort.Text = value; }
        }

        public string conditionOffice
        {
            get { return cbConditionOffice.Text; }
            set { cbConditionOffice.Text = value; }
        }

        public string conditionDOC
        {
            get { return cbConditionDOC.Text; }
            set { cbConditionDOC.Text = value; }
        }

        public string conditionHullClass
        {
            get { return cbConditionHullClass.Text; }
            set { cbConditionHullClass.Text = value; }
        }

        public string conditionMaster
        {
            get { return cbConditionMaster.Text; }
            set { cbConditionMaster.Text = value; }
        }

        public string conditionChiefEngineer
        {
            get { return cbConditionChiefEngineer.Text; }
            set { cbConditionChiefEngineer.Text = value; }
        }

        public string conditionSubchapter
        {
            get { return cbConditionSubchapter.Text; }
            set { cbConditionSubchapter.Text = value; }
        }

        public string conditionKeyWords
        {
            get { return cbConditionKeyWords.Text; }
            set { cbConditionKeyWords.Text = value; }
        }

        public string conditionQuestionnaireType
        {
            get { return cbConditionQuestionnaireType.Text; }
            set { cbConditionQuestionnaireType.Text = value; }
        }

        public string conditionFleet
        {
            get { return cbConditionFleet.Text; }
            set { cbConditionFleet.Text = value; }
        }

        public string valueVessel
        {
            get { return cbValueVessel.Text; }
            set { cbValueVessel.Text = value; }
        }

        public string valueInspector
        {
            get { return cbValueInspector.Text; }
            set { cbValueInspector.Text = value; }
        }

        public string valueReportNumber
        {
            get { return cbValueReportNumber.Text; }
            set { cbValueReportNumber.Text = value; }
        }

        public string valueQuestionNumber
        {
            get { return cbValueQuestionNumber.Text; }
            set { cbValueQuestionNumber.Text = value; }
        }

        public string valueObservation
        {
            get { return cbValueObservation.Text; }
            set { cbValueObservation.Text = value; }
        }

        public string valueInspectorComments
        {
            get { return cbValueInspectorComments.Text; }
            set { cbValueInspectorComments.Text = value; }
        }

        public string valueTechnicalComments
        {
            get { return cbValueTechnicalComments.Text; }
            set { cbValueTechnicalComments.Text = value; }
        }

        public string valueOperatorComments
        {
            get { return cbValueOperatorComments.Text; }
            set { cbValueOperatorComments.Text = value; }
        }

        public string valueQuestionGUID
        {
            get { return cbValueQuestionGUID.Text; }
            set { cbValueQuestionGUID.Text = value; }
        }

        public DateTime valueDate
        {
            get { return dtValueDate.Value.Date; }
            set { dtValueDate.Value = value; }
        }

        public DateTime valueDate2
        {
            get { return dtValueDate2.Value; }
            set { dtValueDate2.Value = value; }
        }

        public string valueCompany
        {
            get { return cbValueCompany.Text; }
            set { cbValueCompany.Text = value; }
        }

        public string valuePort
        {
            get { return cbValuePort.Text; }
            set { cbValuePort.Text = value; }
        }

        public string valueOffice
        {
            get { return cbValueOffice.Text; }
            set { cbValueOffice.Text = value; }
        }

        public string valueDOC
        {
            get { return cbValueDOC.Text; }
            set { cbValueDOC.Text = value; }
        }

        public string valueHullClass
        {
            get { return cbValueHullClass.Text; }
            set { cbValueHullClass.Text = value; }
        }

        public string valueMaster
        {
            get { return cbValueMaster.Text; }
            set { cbValueMaster.Text = value; }
        }

        public string valueChiefEngineer
        {
            get { return cbValueChiefEngineer.Text; }
            set { cbValueChiefEngineer.Text = value; }
        }

        public string valueSubchapter
        {
            get { return cbValueSubchapter.Text; }
            set { cbValueSubchapter.Text = value; }
        }

        public string valueKeyWords
        {
            get { return cbValueKeyWords.Text; }
            set { cbValueKeyWords.Text = value; }
        }

        public string valueQuestionnaireType
        {
            get { return cbValueQuestionnaireType.Text; }
            set { cbValueQuestionnaireType.Text = value; }
        }

        public string valueFleet
        {
            get { return cbValueFleet.Text; }
            set { cbValueFleet.Text = value; }
        }

        public bool useMapping
        {
            get { return chbUseMapping.Checked; }
            set { chbUseMapping.Checked = value; }
        }

        public string typesBox
        {
            get { return cbTypesBox.Text; }
            set { cbTypesBox.Text = value; }
        }

        public bool updateOnClosed
        {
            get { return chbUpdateOnClosed.Checked; }
            set { chbUpdateOnClosed.Checked = value; }
        }

        public string labelVessel
        {
            get { return lblVessel.Text; }
            set { lblVessel.Text = value; }
        }

        public string labelInspector
        {
            get { return lblInspector.Text; }
            set { lblInspector.Text = value; }
        }

        public string labelReportNumber
        {
            get { return lblReportNumber.Text; }
            set { lblReportNumber.Text = value; }
        }

        public string labelDate
        {
            get { return lblDate.Text; }
            set { lblDate.Text = value; }
        }

        public string labelCompany
        {
            get { return lblCompany.Text; }
            set { lblCompany.Text = value; }
        }

        public string labelPort
        {
            get { return lblPort.Text; }
            set { lblPort.Text = value; }
        }

        public string labelOffice
        {
            get { return lblOffice.Text; }
            set { lblOffice.Text = value; }
        }

        public string labelDOC
        {
            get { return lblDOC.Text; }
            set { lblDOC.Text = value; }
        }

        public string labelHullClass
        {
            get { return lblHullClass.Text; }
            set { lblHullClass.Text = value; }
        }

        public string labelMaster
        {
            get { return lblMaster.Text; }
            set { lblMaster.Text = value; }
        }

        public string labelChiefEngneer
        {
            get { return lblChiefEngneer.Text; }
            set { lblChiefEngneer.Text = value; }
        }

        public string labelQuestionnaireType
        {
            get { return lblQuestionnaireType.Text; }
            set { lblQuestionnaireType.Text = value; }
        }

        public string labelFleet
        {
            get { return lblFleet.Text; }
            set { lblFleet.Text = value; }
        }

        
        public FilterForm(OleDbConnection mainConnection, DataSet mainDS, Font mainFont, Icon mainIcon)
        {
            connection = mainConnection;
            DS = mainDS;
            this.Font = mainFont;
            this.Icon = mainIcon;

            InitializeComponent();

            Font fontBold = new Font(mainFont.Name, mainFont.Size, (mainFont.Style | FontStyle.Bold));
            lblField.Font = fontBold;
            lblShow.Font = fontBold;
            lblCondition.Font = fontBold;
            lblValue.Font = fontBold;

            lblVessel.Font = fontBold;
            labelObservation.Font = fontBold;
            labelOperatorComments.Font = fontBold;
            labelQuestionNumber.Font = fontBold;
            lblDate.Font = fontBold;

            //Connect to vessels list
            DataTable vessels = DS.Tables["VESSELS_LIST"];

            cbValueVessel.DataSource = vessels;
            cbValueVessel.DisplayMember = "VESSEL_NAME";
            cbValueVessel.ValueMember = "VESSEL_GUID";


            //Connect to list of inspectors
            DataTable inspectors = DS.Tables["INSPECTORS_LIST"];

            cbValueInspector.DataSource = inspectors;
            cbValueInspector.DisplayMember = "INSPECTOR_NAME";
            cbValueInspector.ValueMember = "INSPECTOR_GUID";
            cbValueInspector.Text = "";

            //Connect to list of reports
            DataTable reports = DS.Tables["REPORTS_LIST"];

            cbValueReportNumber.DataSource = reports;
            cbValueReportNumber.DisplayMember = "REPORT_CODE";
            cbValueReportNumber.Text = "";

            //Connect to list of ports
            DataTable ports = DS.Tables["PORTS_LIST"];

            cbValuePort.DataSource = ports;
            cbValuePort.DisplayMember = "INSPECTION_PORT";
            cbValuePort.Text = "";

            //Connect to list of companies
            DataTable companies = DS.Tables["COMPANIES_LIST"];

            cbValueCompany.DataSource = companies;
            cbValueCompany.DisplayMember = "COMPANY";
            cbValueCompany.Text = "";

            //Connect to list of officers
            DataTable offices = DS.Tables["OFFICES_LIST"];

            cbValueOffice.DataSource = offices;
            cbValueOffice.DisplayMember = "OFFICE_ID";
            cbValueOffice.Text = "";

            DataTable docs = DS.Tables["DOCS_LIST"];

            cbValueDOC.DataSource = docs;
            cbValueDOC.DisplayMember = "DOC_ID";
            cbValueDOC.Text = "";

            //Connect to list of hull classes
            DataTable classes = DS.Tables["CLASSES_LIST"];

            cbValueHullClass.DataSource = classes;
            cbValueHullClass.DisplayMember = "HULL_CLASS";
            cbValueHullClass.Text = "";

            fillMasters();
            fillChiefEngineers();
            fillSubchapters();
            fillKeyWords();
            fillTypes();
            FillFleet();
            FillQuestionnaireTypes();
        }

        private void FillQuestionnaireTypes()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select * from TEMPLATE_TYPES \n" +
                "order by TEMPLATE_TYPE";

            OleDbDataReader reader = cmd.ExecuteReader();

            cbValueQuestionnaireType.Items.Clear();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cbValueQuestionnaireType.Items.Add(reader["TEMPLATE_TYPE"].ToString());
                }
            }

            reader.Close();
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            if (!lblVessel.Text.Contains("*"))
            {
                cbConditionVessel.Text = "";
                cbValueVessel.Text = "";
            }

            if (!lblInspector.Text.Contains("*"))
            {
                cbConditionInspector.Text = "";
                cbValueInspector.Text = "";
            }

            if (!lblReportNumber.Text.Contains("*"))
            {
                cbConditionReportNumber.Text = "";
                cbValueReportNumber.Text = "";
            }


            cbConditionQuestionNumber.Text = "";
            cbConditionObservation.Text = "";
            cbConditionInspectorComments.Text = "";
            cbConditionTechnicalComments.Text = "";
            cbConditionOperatorComments.Text = "";
            cbConditionQuestionGUID.Text = "";

            if (!lblDate.Text.Contains("*"))
            {
                cbConditionDate.Text = "";
                dtValueDate.Value = DateTime.Today;
                dtValueDate2.Value = DateTime.Today;
            }

            if (!lblCompany.Text.Contains("*"))
            {
                cbConditionCompany.Text = "";
                cbValueCompany.Text = "";
            }

            if (!lblPort.Text.Contains("*"))
            {
                cbConditionPort.Text = "";
                cbValuePort.Text = "";
            }

            if (!lblOffice.Text.Contains("*"))
            {
                cbConditionOffice.Text = "";
                cbValueOffice.Text = "";
            }

            if (!lblDOC.Text.Contains("*"))
            {
                cbConditionDOC.Text = "";
                cbValueDOC.Text = "";
            }

            if (!lblHullClass.Text.Contains("*"))
            {
                cbConditionHullClass.Text = "";
                cbValueHullClass.Text = "";
            }

            cbValueQuestionNumber.Text = "";
            cbValueObservation.Text = "";
            cbValueInspectorComments.Text = "";
            cbValueTechnicalComments.Text = "";
            cbValueOperatorComments.Text = "";
            cbValueQuestionGUID.Text = "";

            if (!lblMaster.Text.Contains("*"))
            {
                cbConditionMaster.Text = "";
                cbValueMaster.Text = "";
            }

            if (!lblChiefEngneer.Text.Contains("*"))
            {
                cbConditionChiefEngineer.Text = "";
                cbValueChiefEngineer.Text = "";
            }

            if (!labelSubchapter.Text.Contains("*"))
            {
                cbConditionSubchapter.Text = "";
                cbValueSubchapter.Text = "";
            }

            if (!labelKeyWords.Text.Contains("*"))
            {
                cbConditionKeyWords.Text = "";
                cbValueKeyWords.Text = "";
            }

            if (!lblQuestionnaireType.Text.Contains("*"))
            {
                cbConditionQuestionnaireType.Text = "";
                cbValueQuestionnaireType.Text = "";
            }
        }

        private void conditionDate_TextChanged(object sender, EventArgs e)
        {
            dtValueDate2.Visible = cbConditionDate.Text.Contains("Between");
        }

        private void labelVessel_TextChanged(object sender, EventArgs e)
        {
            cbConditionVessel.Enabled = !lblVessel.Text.Contains("*");
            cbValueVessel.Enabled = !lblVessel.Text.Contains("*");
        }

        private void labelInspector_TextChanged(object sender, EventArgs e)
        {
            cbConditionInspector.Enabled = !lblInspector.Text.Contains("*");
            cbValueInspector.Enabled = !lblInspector.Text.Contains("*");
        }

        private void labelReportNumber_TextChanged(object sender, EventArgs e)
        {
            cbConditionReportNumber.Enabled = !lblReportNumber.Text.Contains("*");
            cbValueReportNumber.Enabled = !lblReportNumber.Text.Contains("*");
        }

        private void labelDate_TextChanged(object sender, EventArgs e)
        {
            cbConditionDate.Enabled = !lblDate.Text.Contains("*");
            dtValueDate.Enabled = !lblDate.Text.Contains("*");
            dtValueDate2.Enabled = !lblDate.Text.Contains("*");
        }

        private void labelCompany_TextChanged(object sender, EventArgs e)
        {
            cbConditionCompany.Enabled = !lblCompany.Text.Contains("*");
            cbValueCompany.Enabled = !lblCompany.Text.Contains("*");
        }

        private void labelPort_TextChanged(object sender, EventArgs e)
        {
            cbConditionPort.Enabled = !lblPort.Text.Contains("*");
            cbValuePort.Enabled = !lblPort.Text.Contains("*");
        }

        private void labelOffice_TextChanged(object sender, EventArgs e)
        {
            cbConditionOffice.Enabled = !lblOffice.Text.Contains("*");
            cbValueOffice.Enabled = !lblOffice.Text.Contains("*");
        }

        private void labelDOC_TextChanged(object sender, EventArgs e)
        {
            cbConditionDOC.Enabled = !lblDOC.Text.Contains("*");
            cbValueDOC.Enabled = !lblDOC.Text.Contains("*");
        }

        private void labelHullClass_TextChanged(object sender, EventArgs e)
        {
            cbConditionHullClass.Enabled = !lblHullClass.Text.Contains("*");
            cbValueHullClass.Enabled = !lblHullClass.Text.Contains("*");
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

            cbValueMaster.DataSource = DS.Tables["MASTERS_LIST"];
            cbValueMaster.DisplayMember = "MASTER_NAME";
            cbValueMaster.ValueMember = "MASTER_GUID";

        }

        private void fillChiefEngineers()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select * from \n" +
                "(select CHENG_GUID, CHENG_NAME, CHENG_NOTES, PERSONAL_ID \n" +
                "from CHIEF_ENGINEERS \n" +
                "union \n" +
                "select TOP 1 "+MainForm.GuidToStr(MainForm.zeroGuid)+" as CHENG_GUID, '' as CHENG_NAME, '' as CHENG_NOTES, '' as PERSONAL_ID \n" +
                "from CHIEF_ENGINEERS) \n" +
                "order by CHENG_NAME";

            OleDbDataAdapter chengListAdapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("CHENG_LIST"))
                DS.Tables["CHENG_LIST"].Clear();

            chengListAdapter.Fill(DS, "CHENG_LIST");

            cbValueChiefEngineer.DataSource = DS.Tables["CHENG_LIST"];
            cbValueChiefEngineer.DisplayMember = "CHENG_NAME";
            cbValueChiefEngineer.ValueMember = "CHENG_GUID";

        }

        private void labelMaster_TextChanged(object sender, EventArgs e)
        {
            cbConditionMaster.Enabled = !lblMaster.Text.Contains("*");
            cbValueMaster.Enabled = !lblMaster.Text.Contains("*");
        }

        private void labelChiefEngneer_TextChanged(object sender, EventArgs e)
        {
            cbConditionChiefEngineer.Enabled = !lblChiefEngneer.Text.Contains("*");
            cbValueChiefEngineer.Enabled = !lblChiefEngneer.Text.Contains("*");
        }

        private void fillSubchapters()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select SUBCHAPTER from \n" +
                "TEMPLATE_KEYS \n" +
                "group by SUBCHAPTER";

            OleDbDataAdapter subchaptersAdapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("SUBCHAPTER_LIST"))
                DS.Tables["SUBCHAPTER_LIST"].Clear();

            subchaptersAdapter.Fill(DS, "SUBCHAPTER_LIST");

            cbValueSubchapter.DataSource = DS.Tables["SUBCHAPTER_LIST"];
            cbValueSubchapter.DisplayMember = "SUBCHAPTER";
            cbValueSubchapter.ValueMember = "SUBCHAPTER";

        }

        private void fillKeyWords()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select KEY_INDEX from \n" +
                "TEMPLATE_KEYS \n" +
                "union \n"+
                "select TOP 1 '' as T from TEMPLATE_KEYS \n"+
                "group by KEY_INDEX";

            OleDbDataAdapter keyIndexAdapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("KEY_INDEX_LIST"))
                DS.Tables["KEY_INDEX_LIST"].Clear();

            keyIndexAdapter.Fill(DS, "KEY_INDEX_LIST");

            cbValueKeyWords.DataSource = DS.Tables["KEY_INDEX_LIST"];
            cbValueKeyWords.DisplayMember = "KEY_INDEX";
            cbValueKeyWords.ValueMember = "KEY_INDEX";
        }

        private void fillTypes()
        {
            cbTypesBox.Items.Clear();
            cbTypesBox.Items.Add("Select...");
            cbTypesBox.Items.Add("01");
            cbTypesBox.Items.Add("02");
            cbTypesBox.Items.Add("03");
            cbTypesBox.Items.Add("04");

            /*
            OleDbCommand cmd = new OleDbCommand("", connection);

             cmd.CommandText =
                 "select TYPE_CODE \n" +
                 "from TEMPLATES \n" +
                 "where \n" +
                 "LEFT(VERSION,INSTR(VERSION,'.')-1)= \n" +
                 "(select Max(Major) as MaxMajor from \n" +
                 "(SELECT LEFT(VERSION,INSTR(VERSION,'.')-1) as MAJOR \n" +
                 "FROM [TEMPLATES])) \n" +
                 "group by TYPE_CODE";

             OleDbDataReader typesReader = cmd.ExecuteReader();

             typesBox.Items.Add("Select...");

             while (typesReader.Read())
                 typesBox.Items.Add(typesReader[0].ToString());

             typesReader.Close();
            */
        }

        private void FillFleet()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select FLEET \n" +
                "from VESSELS \n" +
                "group by FLEET";

            OleDbDataAdapter fleetAdapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("FLEET_LIST"))
                DS.Tables["FLEET_LIST"].Clear();

            fleetAdapter.Fill(DS, "FLEET_LIST");

            cbValueFleet.DataSource = DS.Tables["FLEET_LIST"];
            cbValueFleet.DisplayMember = "FLEET";
            cbValueFleet.ValueMember = "FLEET";
        }

        private void valueQuestionNumber_TextChanged(object sender, EventArgs e)
        {
            if (cbValueQuestionNumber.Text.StartsWith("8") && cbValueQuestionnaireType.Text == "VIQ")
            {
                cbTypesBox.Enabled = true;
                cbTypesBox.Text = "Select...";
            }
            else
            {
                cbTypesBox.Enabled = false;
                cbTypesBox.Text = "";
            }
        }

        private void FilterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cbValueQuestionnaireType.Text.Trim().Length == 0 && cbValueQuestionNumber.Text.Trim().Length>0 )
            {
                MessageBox.Show("Please select questionnire type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbValueQuestionnaireType.Focus();
                e.Cancel = true;
            }

            if ((this.DialogResult==DialogResult.OK)
                && cbValueQuestionNumber.Text.StartsWith("8.")
                && (cbValueQuestionnaireType.Text == "VIQ")
                && ((cbTypesBox.Text=="Select...") || (cbTypesBox.Text.Length==0)))
            {
                MessageBox.Show("Select type of questionnaire","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                cbTypesBox.Focus();
                e.Cancel=true;
            }


            if (this.DialogResult == DialogResult.OK 
                && cbConditionQuestionNumber.Text.Trim().Length==0 
                && cbValueQuestionNumber.Text.Trim().Length > 0 
                && chbUseMapping.Checked)
            {
                //Check number exists
                OleDbCommand cmd=new OleDbCommand("",MainForm.connection);

                cmd.CommandText =
                    "select \n"+ 
                    "Count(TEMPLATE_QUESTIONS.QUESTION_NUMBER) as RecCount \n"+
                    "from \n"+
                    "TEMPLATE_QUESTIONS \n"+
                    "inner join \n"+
                    "( \n"+
                    "select TEMPLATE_GUID \n"+
                    "from \n"+
                    "TEMPLATES \n"+
                    "inner join \n"+
                    "( \n"+
                    "select RIGHT(TYPE_CODE,2) as ATYPE, MAX(VERSION) as MAXV \n"+
                    "from TEMPLATES \n"+
                    "where \n"+
                    "RIGHT(TYPE_CODE,2) < '05' \n"+
                    "group by RIGHT(TYPE_CODE,2) \n"+
                    ") as Q \n"+
                    "on TEMPLATES.VERSION=Q.MaxV \n"+
                    "and RIGHT(TEMPLATES.TYPE_CODE,2)=Q.ATYPE \n"+
                    "where \n"+
                    "TEMPLATES.TEMPLATE_TYPE='"+MainForm.StrToSQLStr(cbValueQuestionnaireType.Text)+"' \n"+
                    ") as Q1 \n"+
                    "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=Q1.TEMPLATE_GUID \n"+
                    "where \n" +
                    "QUESTION_NUMBER='"+MainForm.StrToSQLStr(cbValueQuestionNumber.Text)+"'";

                int recCount = (int)cmd.ExecuteScalar();

                if (recCount==0)
                {
                    var rslt = MessageBox.Show("There is not question with number " + cbValueQuestionNumber.Text + " in the active questionnaire. \n" +
                        "Would you like to provide another question number?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (rslt==DialogResult.Yes)
                    {
                        cbValueQuestionNumber.Focus();
                        e.Cancel = true;
                    }
                }
            }
        }

        private void labelSubchapter_TextChanged(object sender, EventArgs e)
        {
            cbConditionSubchapter.Enabled = !labelSubchapter.Text.Contains("*");
            cbValueSubchapter.Enabled = !labelSubchapter.Text.Contains("*");
        }

        private void labelKeyWords_TextChanged(object sender, EventArgs e)
        {
            cbConditionKeyWords.Enabled = !labelKeyWords.Text.Contains("*");
            cbValueKeyWords.Enabled = !labelKeyWords.Text.Contains("*");
        }

        private void btnSaveDefaults_Click(object sender, EventArgs e)
        {
            SaveDefaults();
        }

        private void SaveDefaults()
        {
            //OleDbCommand cmd = new OleDbCommand("", connection);
            //OleDbTransaction transaction;

            //transaction = connection.BeginTransaction();
            //cmd.Transaction = transaction;

            try
            {
                /*
                if (MainForm.appDataFolder.Length==0 || !Directory.Exists(MainForm.appDataFolder))
                {
                    MainForm.SetAppDataFolder();

                    if (MainForm.appDataFolder.Length==0 || !Directory.Exists(MainForm.appDataFolder))
                    {
                        MessageBox.Show("Unable to locate data folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                */

                string fileName = Path.Combine(MainForm.serviceFolder, "Settings_" + MainForm.userName + ".ini");
                
                IniFile iniFile = new IniFile(fileName);

                string section = "Details filter";
                string iniKey="";
                
                //cmd.CommandText =
                //    "update FILTER_ITEMS set ITEM_SHOW=" + Convert.ToString(showVessel.Checked) + "," +
                //    "ITEM_CONDITION='" + MainForm.StrToSQLStr(conditionVessel.Text) + "',\n" +
                //    "USER_NAME='"+MainForm.StrToSQLStr(MainForm.user.Name)+"' \n"+
                //    "where ITEM_NAME='VESSEL'";
                //cmd.ExecuteNonQuery();

                //MainForm.WriteRegValue("Details filter\\Show", "Vessel", showVessel.Checked);
                //MainForm.WriteRegValue("Details filter\\Condition", "Vessel", conditionVessel.Text);
                //MainForm.WriteRegValue("Details filter\\Value", "Vessel", valueVessel.Text);

                iniKey = "Vessel";

                iniFile.Write(section, iniKey+".Show", chbShowVessel.Checked);

                if (cbConditionVessel.Text.Length == 0)
                {
                    iniFile.Write(section, iniKey+".Condition", "");
                    iniFile.Write(section, iniKey+".Value", "");
                }
                else
                {
                    iniFile.Write(section, iniKey + ".Condition", cbConditionVessel.Text);
                    iniFile.Write(section, iniKey + ".Value", cbValueVessel.Text);
                }

                //cmd.CommandText =
                //    "update FILTER_ITEMS set ITEM_SHOW=" + Convert.ToString(showInspector.Checked) + "," +
                //    "ITEM_CONDITION='" + MainForm.StrToSQLStr(conditionInspector.Text) + "', \n" +
                //    "USER_NAME='" + MainForm.StrToSQLStr(MainForm.user.Name) + "' \n" +
                //    "where ITEM_NAME='INSPECTOR'";
                //cmd.ExecuteNonQuery();

                //MainForm.WriteRegValue("Details filter\\Show", "Inspector", showInspector.Checked);
                //MainForm.WriteRegValue("Details filter\\Condition", "Inspector", conditionInspector.Text);
                //MainForm.WriteRegValue("Details filter\\Value", "Inspector", valueInspector.Text);

                iniKey="Inspector";

                iniFile.Write(section, iniKey+".Show", chbShowInspector.Checked);

                if (cbConditionInspector.Text.Length == 0)
                {
                    iniFile.Write(section, iniKey+".Condition", "");
                    iniFile.Write(section, iniKey+".Value", "");
                }
                else
                {
                    iniFile.Write(section, iniKey + ".Condition", cbConditionInspector.Text);
                    iniFile.Write(section, iniKey + ".Value", cbValueInspector.Text);
                }

                //cmd.CommandText =
                //    "update FILTER_ITEMS set ITEM_SHOW=" + showReportNumber.Checked.ToString() + "," +
                //    "ITEM_CONDITION='" + MainForm.StrToSQLStr(conditionReportNumber.Text) + "', \n" +
                //    "USER_NAME='" + MainForm.StrToSQLStr(MainForm.user.Name) + "' \n" +
                //    "where ITEM_NAME='REPORT NUMBER'";
                //cmd.ExecuteNonQuery();

                //MainForm.WriteRegValue("Details filter\\Show", "Report number", showReportNumber.Checked);
                //MainForm.WriteRegValue("Details filter\\Condition", "Report number", conditionReportNumber.Text);
                //MainForm.WriteRegValue("Details filter\\Value", "Report number", valueReportNumber.Text);

                iniKey="ReportNumber";

                iniFile.Write(section, iniKey+".Show", chbShowReportNumber.Checked);

                if (cbConditionReportNumber.Text.Length == 0)
                {
                    iniFile.Write(section, iniKey + ".Condition", "");
                    iniFile.Write(section, iniKey + ".Value", "");
                }
                else
                {
                    iniFile.Write(section, iniKey + ".Condition", cbConditionReportNumber.Text);
                    iniFile.Write(section, iniKey + ".Value", cbValueReportNumber.Text);
                }

                iniKey = "QuestionNumber";

                iniFile.Write(section, iniKey + ".Show", chbShowQuestionNumber.Checked);

                if (cbConditionQuestionNumber.Text.Length == 0)
                {
                    iniFile.Write(section, iniKey + ".Condition", "");
                    iniFile.Write(section, iniKey + ".Value", "");
                    iniFile.Write(section, iniKey + ".Type", "");
                }
                else
                {
                    iniFile.Write(section, iniKey + ".Condition", cbConditionQuestionNumber.Text);
                    iniFile.Write(section, iniKey + ".Value", cbValueQuestionNumber.Text);
                    iniFile.Write(section, iniKey + ".Type", cbTypesBox.Text);
                }

                iniKey = "Observation";

                iniFile.Write(section, iniKey + ".Show", chbShowObservation.Checked);

                if (cbConditionObservation.Text.Length == 0)
                {
                    iniFile.Write(section, iniKey + ".Condition", "");
                    iniFile.Write(section, iniKey + ".Value", "");
                }
                else
                {
                    iniFile.Write(section, iniKey + ".Condition", cbConditionObservation.Text);
                    iniFile.Write(section, iniKey + ".Value", cbValueObservation.Text);
                }

                iniKey = "InspectorComments";

                iniFile.Write(section, iniKey + ".Show", chbShowInspectorComments.Checked);

                if (cbConditionInspectorComments.Text.Length == 0)
                {
                    iniFile.Write(section, iniKey + ".Condition", "");
                    iniFile.Write(section, iniKey + ".Value", "");
                }
                else
                {
                    iniFile.Write(section, iniKey + ".Condition", cbConditionInspectorComments.Text);
                    iniFile.Write(section, iniKey + ".Value", cbValueInspectorComments.Text);
                }

                iniKey = "TechnicalComments";

                iniFile.Write(section, iniKey + ".Show", chbShowTechnicalComments.Checked);

                if (cbConditionInspectorComments.Text.Length == 0)
                {
                    iniFile.Write(section, iniKey + ".Condition", "");
                    iniFile.Write(section, iniKey + ".Value", "");
                }
                else
                {
                    iniFile.Write(section, iniKey + ".Condition", cbConditionTechnicalComments.Text);
                    iniFile.Write(section, iniKey + ".Value", cbValueTechnicalComments.Text);
                }

                iniKey = "OperatorComments";

                iniFile.Write(section, iniKey + ".Show", chbShowOperatorComments.Checked);

                if (cbConditionOperatorComments.Text.Length == 0)
                {
                    iniFile.Write(section, iniKey + ".Condition", "");
                    iniFile.Write(section, iniKey + ".Value", "");
                }
                else
                {
                    iniFile.Write(section, iniKey + ".Condition", cbConditionOperatorComments.Text);
                    iniFile.Write(section, iniKey + ".Value", cbValueOperatorComments.Text);
                }

                iniKey = "QuestionGUID";

                iniFile.Write(section, iniKey + ".Show", chbShowQuestionGUID.Checked);

                if (cbConditionQuestionGUID.Text.Length == 0)
                {
                    iniFile.Write(section, iniKey + ".Condition", "");
                    iniFile.Write(section, iniKey + ".Value", "");
                }
                else
                {
                    iniFile.Write(section, iniKey + ".Condition", cbConditionQuestionGUID.Text);
                    iniFile.Write(section, iniKey + ".Value", cbValueQuestionGUID.Text);
                }


                iniKey = "DateOfInspection";

                iniFile.Write(section, iniKey + ".Show", chbShowDate.Checked);

                if (cbConditionDate.Text.Length == 0)
                {
                    iniFile.Write(section, iniKey + ".Condition", "");
                    iniFile.Write(section, iniKey + ".Value1", "");
                    iniFile.Write(section, iniKey + ".Value2", "");
                }
                else
                {
                    iniFile.Write(section, iniKey + ".Condition", cbConditionDate.Text);
                    iniFile.Write(section, iniKey + ".Value1", dtValueDate.Value.ToString("yyyy-MM-dd"));
                    iniFile.Write(section,iniKey+".Value2",dtValueDate2.Value.ToString("yyyy-MM-dd"));
                }


                iniKey = "Company";

                iniFile.Write(section, iniKey + ".Show", chbShowCompany.Checked);

                if (cbConditionCompany.Text.Length == 0)
                {
                    iniFile.Write(section, iniKey + ".Condition", "");
                    iniFile.Write(section, iniKey + ".Value", "");
                }
                else
                {
                    iniFile.Write(section, iniKey + ".Condition", cbConditionCompany.Text);
                    iniFile.Write(section, iniKey + ".Value", cbValueCompany.Text);
                }

                iniKey = "Port";

                iniFile.Write(section, iniKey + ".Show", chbShowPort.Checked);

                if (cbConditionPort.Text.Length == 0)
                {
                    iniFile.Write(section, iniKey + ".Condition", "");
                    iniFile.Write(section, iniKey + ".Value", "");
                }
                else
                {
                    iniFile.Write(section, iniKey + ".Condition", cbConditionPort.Text);
                    iniFile.Write(section, iniKey + ".Value", cbValuePort.Text);
                }

                iniKey = "Office";

                iniFile.Write(section, iniKey + ".Show", chbShowOffice.Checked);

                if (cbConditionOffice.Text.Length == 0)
                {
                    iniFile.Write(section, iniKey + ".Condition", "");
                    iniFile.Write(section, iniKey + ".Value", "");
                }
                else
                {
                    iniFile.Write(section, iniKey + ".Condition", cbConditionOffice.Text);
                    iniFile.Write(section, iniKey + ".Value", cbValueOffice.Text);
                }

                iniKey = "DOC";

                iniFile.Write(section, iniKey + ".Show", chbShowDOC.Checked);

                if (cbConditionDOC.Text.Length == 0)
                {
                    iniFile.Write(section, iniKey + ".Condition", "");
                    iniFile.Write(section, iniKey + ".Value", "");
                }
                else
                {
                    iniFile.Write(section, iniKey + ".Condition", cbConditionDOC.Text);
                    iniFile.Write(section, iniKey + ".Value", cbValueDOC.Text);
                }

                iniKey = "HullClass";

                iniFile.Write(section, iniKey + ".Show", chbShowHullClass.Checked);

                if (cbConditionHullClass.Text.Length == 0)
                {
                    iniFile.Write(section, iniKey + ".Condition", "");
                    iniFile.Write(section, iniKey + ".Value", "");
                }
                else
                {
                    iniFile.Write(section, iniKey + ".Condition", cbConditionHullClass.Text);
                    iniFile.Write(section, iniKey + ".Value", cbValueHullClass.Text);
                }

                iniKey = "Master";

                iniFile.Write(section, iniKey + ".Show", chbShowMaster.Checked);

                if (cbConditionMaster.Text.Length == 0)
                {
                    iniFile.Write(section, iniKey + ".Condition", "");
                    iniFile.Write(section, iniKey + ".Value", "");
                }
                else
                {
                    iniFile.Write(section, iniKey + ".Condition", cbConditionMaster.Text);
                    iniFile.Write(section, iniKey + ".Value", cbValueMaster.Text);
                }

                iniKey = "ChiefEngineer";

                iniFile.Write(section, iniKey + ".Show", chbShowChiefEngineer.Checked);

                if (cbConditionChiefEngineer.Text.Length == 0)
                {
                    iniFile.Write(section, iniKey + ".Condition", "");
                    iniFile.Write(section, iniKey + ".Value", "");
                }
                else
                {
                    iniFile.Write(section, iniKey + ".Condition", cbConditionChiefEngineer.Text);
                    iniFile.Write(section, iniKey + ".Value", cbValueChiefEngineer.Text);
                }

                iniKey = "Subchapter";

                iniFile.Write(section, iniKey + ".Show", chbShowSubchapter.Checked);

                if (cbConditionSubchapter.Text.Length == 0)
                {
                    iniFile.Write(section, iniKey + ".Condition", "");
                    iniFile.Write(section, iniKey + ".Value", "");
                }
                else
                {
                    iniFile.Write(section, iniKey + ".Condition", cbConditionSubchapter.Text);
                    iniFile.Write(section, iniKey + ".Value", cbValueSubchapter.Text);
                }

                iniKey = "KeyWords";

                iniFile.Write(section, iniKey + ".Show", chbShowKeyWords.Checked);

                if (cbConditionKeyWords.Text.Length == 0)
                {
                    iniFile.Write(section, iniKey + ".Condition", "");
                    iniFile.Write(section, iniKey + ".Value", "");
                }
                else
                {
                    iniFile.Write(section, iniKey + ".Condition", cbConditionKeyWords.Text);
                    iniFile.Write(section, iniKey + ".Value", cbValueKeyWords.Text);
                }

                iniKey = "QuestionnaireType";

                iniFile.Write(section, iniKey + ".Show", chbShowQuestionnaireType.Checked);

                if (cbConditionQuestionnaireType.Text.Length == 0)
                {
                    iniFile.Write(section, iniKey + ".Condition", "");
                    iniFile.Write(section, iniKey + ".Value", "");
                }
                else
                {
                    iniFile.Write(section, iniKey + ".Condition", cbConditionQuestionnaireType.Text);
                    iniFile.Write(section, iniKey + ".Value", cbValueQuestionnaireType.Text);
                }

                iniKey = "KeyWords";

                iniFile.Write(section, iniKey + ".Show", chbShowKeyWords.Checked);

                if (cbConditionKeyWords.Text.Length == 0)
                {
                    iniFile.Write(section, iniKey + ".Condition", "");
                    iniFile.Write(section, iniKey + ".Value", "");
                }
                else
                {
                    iniFile.Write(section, iniKey + ".Condition", cbConditionKeyWords.Text);
                    iniFile.Write(section, iniKey + ".Value", cbValueKeyWords.Text);
                }


                iniKey="UseMapping";

                iniFile.Write(section,iniKey,chbUseMapping.Checked);

                iniKey="UpdateOnClose";
                iniFile.Write(section,iniKey,chbUpdateOnClosed.Checked);


                iniKey="UseDefault";

                iniFile.Write(section,iniKey,rbtnUseDefault.Checked);


                MainForm.loadFilterDefaultsIni();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRestoreDefaults_Click(object sender, EventArgs e)
        {
            MainForm.loadFilterDefaultsIni();
            restoreDefaults();
        }

        private void restoreDefaults()
        {
            chbShowVessel.Checked = MainForm.defVessel.Show;
            chbShowInspector.Checked = MainForm.defInspector.Show;
            chbShowReportNumber.Checked = MainForm.defReportNumber.Show;
            chbShowQuestionNumber.Checked = MainForm.defQuestionNumber.Show;
            chbShowObservation.Checked = MainForm.defObservation.Show;
            chbShowInspectorComments.Checked = MainForm.defInspectorComments.Show;
            chbShowTechnicalComments.Checked = MainForm.defTechnicalComments.Show;
            chbShowOperatorComments.Checked = MainForm.defOperatorComments.Show;
            chbShowQuestionGUID.Checked = MainForm.defQuestionGUID.Show;
            chbShowDate.Checked = MainForm.defDateOfInspection.Show;
            chbShowCompany.Checked = MainForm.defCompany.Show;
            chbShowPort.Checked = MainForm.defPort.Show;
            chbShowOffice.Checked = MainForm.defOffice.Show;
            chbShowDOC.Checked = MainForm.defDOC.Show;
            chbShowHullClass.Checked = MainForm.defHullClass.Show;
            chbShowMaster.Checked = MainForm.defMaster.Show;
            chbShowChiefEngineer.Checked = MainForm.defChiefEngineer.Show;
            chbShowSubchapter.Checked = MainForm.defSubchapter.Show;
            chbShowKeyWords.Checked = MainForm.defKeyWords.Show;
            chbUseMapping.Checked = MainForm.defUseMapping.Show;
            chbUpdateOnClosed.Checked = MainForm.defUpdateOnClose.Show;
            chbShowQuestionnaireType.Checked = MainForm.defQuestionnaireType.Show;

            if (MainForm.defUseDefault.Show)
                rbtnUseDefault.Checked = true;
            else
            {
                rbtnLastSetting.Checked = true;
            }

            cbConditionVessel.Text = MainForm.defVessel.Condition;
            cbConditionInspector.Text = MainForm.defInspector.Condition;
            cbConditionQuestionNumber.Text = MainForm.defQuestionNumber.Condition;
            cbConditionReportNumber.Text = MainForm.defReportNumber.Condition;
            cbConditionObservation.Text = MainForm.defObservation.Condition;
            cbConditionInspectorComments.Text = MainForm.defInspectorComments.Condition;
            cbConditionTechnicalComments.Text = MainForm.defTechnicalComments.Condition;
            cbConditionOperatorComments.Text = MainForm.defOperatorComments.Condition;
            cbConditionQuestionGUID.Text = MainForm.defQuestionGUID.Condition;
            cbConditionDate.Text = MainForm.defDateOfInspection.Condition;
            cbConditionCompany.Text = MainForm.defCompany.Condition;
            cbConditionPort.Text = MainForm.defPort.Condition;
            cbConditionOffice.Text = MainForm.defOffice.Condition;
            cbConditionDOC.Text = MainForm.defDOC.Condition;
            cbConditionHullClass.Text = MainForm.defHullClass.Condition;
            cbConditionMaster.Text = MainForm.defMaster.Condition;
            cbConditionChiefEngineer.Text = MainForm.defChiefEngineer.Condition;
            cbConditionSubchapter.Text = MainForm.defSubchapter.Condition;
            cbConditionKeyWords.Text = MainForm.defKeyWords.Condition;
            cbConditionQuestionnaireType.Text = MainForm.defQuestionnaireType.Condition;

            cbValueVessel.Text = (string)MainForm.defVessel.Value;
            cbValueInspector.Text = (string)MainForm.defInspector.Value;
            cbValueQuestionNumber.Text = (string)MainForm.defQuestionNumber.Value;
            cbTypesBox.Text = (string)MainForm.defQuestionNumber.Value2;
            cbValueReportNumber.Text = (string)MainForm.defReportNumber.Value;
            cbValueObservation.Text = (string)MainForm.defObservation.Value;
            cbValueInspectorComments.Text = (string)MainForm.defInspectorComments.Value;
            cbValueTechnicalComments.Text = (string)MainForm.defTechnicalComments.Value;
            cbValueOperatorComments.Text = (string)MainForm.defOperatorComments.Value;
            cbValueQuestionGUID.Text = (string)MainForm.defQuestionGUID.Value;
            dtValueDate.Value = (DateTime)MainForm.defDateOfInspection.Value;
            dtValueDate2.Value = (DateTime)MainForm.defDateOfInspection.Value2;
            cbValueCompany.Text = (string)MainForm.defCompany.Value;
            cbValuePort.Text = (string)MainForm.defPort.Value;
            cbValueOffice.Text = (string)MainForm.defOffice.Value;
            cbValueDOC.Text = (string)MainForm.defDOC.Value;
            cbValueHullClass.Text = (string)MainForm.defHullClass.Value;
            cbValueMaster.Text = (string)MainForm.defMaster.Value;
            cbValueChiefEngineer.Text = (string)MainForm.defChiefEngineer.Value;
            cbValueSubchapter.Text = (string)MainForm.defSubchapter.Value;
            cbValueKeyWords.Text = (string)MainForm.defKeyWords.Value;
            cbValueQuestionnaireType.Text = (string)MainForm.defQuestionnaireType.Value;

        }

        private void labelQuestionnaireType_TextChanged(object sender, EventArgs e)
        {
            cbConditionQuestionnaireType.Enabled = !lblQuestionnaireType.Text.Contains("*");
            cbValueQuestionnaireType.Enabled = !lblQuestionnaireType.Text.Contains("*");
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void FilterForm_Shown(object sender, EventArgs e)
        {
        }

     }
}
