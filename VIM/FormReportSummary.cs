using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;

namespace VIM
{
    public partial class FormReportSummary : Form
    {
        public DataTable dt = new DataTable();
        OleDbConnection connection;
        DataSet DS;
        //OleDbDataAdapter inspectorsAdapter;
        //DataTable inspectors;
        string workFolder;
        bool newRec = false;

        //All below variables were added in version 2.0.15.1
        bool cbUpdate = false;
        //bool typesUpdate = false;
        bool needSave = false;
        bool gridUpdate = false;
        bool fillingNumbers = false;
        private static bool formOpen = false;
        string _questionGUID = "";

        private string _reportCode = "";
        string _templateGUID = "";

        //Store original text
        string storeObservation = "";
        string storeInspectorComments = "";
        string storeOperatorComments = "";

        bool isVisible = false;
        bool readOnly = false;
        bool inspectionTypeFilled = false;
        bool fileAvailable = false;

        List<string> doubleNumbers = new List<string>();

        public string templateGUID
        {
            set { _templateGUID = MainForm.FormatGuidString(value); }
        }

        public string reportCode
        {
            set { _reportCode = value; tbReportCode.Text = _reportCode; }
            get { return _reportCode; }
        }

        private int _reportID = 0;

        public int reportID
        {
            set { _reportID = value; }
            get { return _reportID; }
        }

        //Added on 12.10.2016
        public string vesselName
        {
            set { cbVesselName.Text = value; }
            get { return cbVesselName.Text; }
        }

        private string _vesselIMO = "";

        public string vesselIMO
        {
            set { _vesselIMO = value; }
            get { return _vesselIMO; }
        }

        private Guid _vesselGuid = MainForm.zeroGuid;

        public Guid vesselGuid
        {
            set { _vesselGuid = value; }
            get { return _vesselGuid; }
        }

        public string inspectionDate
        {
            set { dtInspectionDate.Text = value; }
            get { return dtInspectionDate.Text; }
        }

        public string inspectionPort
        {
            set { cbInspectionPort.Text = value; }
            get { return cbInspectionPort.Text; }
        }

        private string _company = "";

        public string inspectionCompany
        {
            set { _company = value; cbCompany.Text = _company; }
            get { return _company; }
        }

        private int _inspectionTypeID = 0;

        public int inspectionTypeID
        {
            get { return _inspectionTypeID; }
            set { 
                _inspectionTypeID = value; 
                cbInspectionType.SelectedValue = _inspectionTypeID; 
            }
        }

        private string _inspectionType = "";

        public string inspectionType
        {
            get { return _inspectionType; }
            set { 
                _inspectionType = value; 
                cbInspectionType.Text = _inspectionType; 
            }
        }

        private int _memorandumID = 0;

        public int memorandumID
        {
            get { return _memorandumID; }
            set { _memorandumID = value; cbMemorandumType.SelectedValue = _memorandumID; }
        }

        private string _memorandumType = "";

        public string memorandumType
        {
            get { return _memorandumType; }
            set { _memorandumType = value; cbMemorandumType.Text = _memorandumType; }
        }

        public string inspectorName
        {
            set { cbInspector.Text = value; }
            get { return cbInspector.Text; }
        }

        public Guid inspectorGuid
        {
            set { if (value == null)
                    cbInspector.SelectedValue = MainForm.zeroGuid;
                else
                    cbInspector.SelectedValue = value; }
            get { if (cbInspector.Text.Trim().Length == 0)
                    return MainForm.zeroGuid;
                else
                    return MainForm.StrToGuid(cbInspector.SelectedValue.ToString()); }
        }


        public string qVersion
        {
            set { cbQVersion.Text = value; }
            get { return cbQVersion.Text; }
        }

        public string qType
        {
            set { cbQType.Text = value; }
            get { return cbQType.Text; }
        }

        private string _selectedNumber = "";

        public string selectedNumber
        {
            set { _selectedNumber = value; }
            get { return _selectedNumber; }
        }

        // End of variables added on 12.10.2016

        public bool justObservations
        {
            set { if (value) cbItems.Text = "Observations"; }
        }

        public bool manual
        {
            set { chbManual.Checked = value; }
            get { return chbManual.Checked; }
        }

        public FormReportSummary(bool mainNew, string mainReportCode, bool mainJustObservations, bool mainReadOnly)
        {
            //Modified on 12.10.2016 in version 2.0.15.1
            //  Initilize comboboxes

            InitializeComponent();

            this.Font = MainForm.mainFont;
            this.Icon = MainForm.mainIcon;

            //MessageBox.Show(mainFont.Name);

            workFolder = MainForm.workFolder;

            newRec = mainNew;
            readOnly = mainReadOnly;

            
            connection = MainForm.connection;
            DS = MainForm.DS;

            FillVessels();
            FillPorts();
            FillCompanies();
            FillVersions();

            fillInspectors();
            fillInspectionType();
            fillMemorandums();

            FillComboBox();

            reportCode = mainReportCode;

            FillForm();

            if (mainJustObservations)
                cbItems.Text = "Observations";
            else
                cbItems.Text = "All";

            ProtectFields(mainReadOnly);

            SetSaveInactive();

        }

        private void FillForm()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);
            cmd.CommandText =
                "select * \n" +
                "from REPORTS \n" +
                "where REPORT_CODE='" + reportCode + "'";

            OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("REPORT_SUMMARY"))
                DS.Tables["REPORT_SUMMARY"].Clear();

            adpt.Fill(DS, "REPORT_SUMMARY");

            DataRow[] rows = DS.Tables["REPORT_SUMMARY"].Select();

            if (rows.Length > 0)
            {
                vesselName = rows[0]["VESSEL"].ToString();
                vesselIMO = rows[0]["VESSEL_IMO"].ToString();

                if (rows[0]["INSPECTION_DATE"] == System.DBNull.Value)
                    inspectionDate = DateTime.Today.ToShortDateString();
                else
                    inspectionDate = Convert.ToDateTime(rows[0]["INSPECTION_DATE"]).ToShortDateString();
                
                inspectionPort = rows[0]["INSPECTION_PORT"].ToString();

                if (rows[0]["INSPECTION_TYPE_ID"] == System.DBNull.Value)
                    inspectionTypeID = 0;
                else
                    inspectionTypeID = Convert.ToInt32(rows[0]["INSPECTION_TYPE_ID"]);

                inspectionCompany = rows[0]["COMPANY"].ToString();

                if (rows[0]["MEMORANDUM_ID"] == System.DBNull.Value)
                    memorandumID = 0;
                else
                    memorandumID = Convert.ToInt32(rows[0]["MEMORANDUM_ID"]);

                inspectorGuid = MainForm.StrToGuid(rows[0]["INSPECTOR_GUID"].ToString());

                qVersion = rows[0]["VIQ_VERSION"].ToString();
                qType = rows[0]["VIQ_TYPE"].ToString();

                chbManual.Checked = Convert.ToBoolean(rows[0]["MANUAL"]);

                fileAvailable = ReportFileExists();

                FillCrew();
            }

        }

        private void ProtectFields(bool readOnly)
        {
            if (MainForm.isPowerUser && !readOnly)
            {
                cbVesselName.BackColor = SystemColors.Window;
            }
            else
            {
                cbVesselName.Enabled = false;

                cbCompany.Enabled = false;
                cbCorrect.Enabled = false;
                cbInspectionPort.Enabled = false;
                cbInspector.Enabled = false;
                cbQType.Enabled = false;
                cbQVersion.Enabled = false;
                cbReason.Enabled = false;
                cbType.Enabled = false;
                tbInspectorComments.ReadOnly = true;
                tbObservation.ReadOnly = true;
                tbOperatorComments.ReadOnly = true;
                tbReportCode.ReadOnly = true;
                tbReportCode.BackColor = SystemColors.ButtonFace;
                cbMemorandumType.Enabled = false;
                cbInspectionType.Enabled = false;
                chbManual.Enabled = false;

                btnQAdd.Enabled = false;
                btnQEdit.Enabled = false;
                btnQDelete.Enabled = false;
                dtInspectionDate.Enabled = false;
                btnShowInspectors.Enabled = false;
                btnNewCode.Enabled = false;
                btnOk.Enabled = false;
            }
        }

        private void FillCrew()
        {
            SetCrewValid();

            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select CREW_POSITIONS.POSITION_NAME, CREW_NAME, CREW_GUID, \n"+
                "CREW_POSITIONS.CREW_POSITION_GUID, CREW_VALID \n"+
                "from CREW_POSITIONS left join \n"+
                "(select CREW.CREW_POSITION_GUID, CREW.CREW_GUID, CREW.CREW_NAME, "+
                "REPORTS_CREW.CREW_VALID \n" +
                "from REPORTS_CREW inner join CREW \n" +
                "on REPORTS_CREW.CREW_GUID = CREW.CREW_GUID \n" +
                "where REPORT_CODE = '" +MainForm.StrToSQLStr(reportCode)+ "') as RQ \n" +
                "on CREW_POSITIONS.CREW_POSITION_GUID = RQ.CREW_POSITION_GUID \n" +
                "order by CREW_POSITIONS.POSITION_INDEX";

            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("REPORT_CREW"))
                DS.Tables["REPORT_CREW"].Clear();

            adapter.Fill(DS, "REPORT_CREW");

            dgvCrew.DataSource = DS;
            dgvCrew.DataMember = "REPORT_CREW";

            dgvCrew.Columns["POSITION_NAME"].HeaderText = "Position";
            dgvCrew.Columns["POSITION_NAME"].FillWeight = 30;

            dgvCrew.Columns["CREW_NAME"].HeaderText = "Name";
            dgvCrew.Columns["CREW_NAME"].FillWeight = 70;

            dgvCrew.Columns["CREW_GUID"].Visible = false;
            dgvCrew.Columns["CREW_POSITION_GUID"].Visible = false;

            dgvCrew.Columns["CREW_VALID"].HeaderText = "Valid";
            dgvCrew.Columns["CREW_VALID"].FillWeight = 10;
            dgvCrew.Columns["CREW_VALID"].Visible = false;
        }

        private void SetCrewValid()
        {
            if (reportCode.Length == 0)
                return;

            if (vesselGuid == MainForm.zeroGuid)
                return;

            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select CREW.CREW_GUID, CREW.CREW_NAME, CREW_POSITIONS.POSITION_NAME \n" +
                "from \n" +
                "(CREW inner join \n" +
                "(select CREW_GUID \n"+
                "from REPORTS_CREW \n" +
                "where REPORT_CODE='" + MainForm.StrToSQLStr(reportCode) + "') as Q1 \n" +
                "on CREW.CREW_GUID=Q1.CREW_GUID) \n" +
                "inner join CREW_POSITIONS \n" +
                "on CREW.CREW_POSITION_GUID=CREW_POSITIONS.CREW_POSITION_GUID";

            OleDbDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Guid crewGuid = MainForm.StrToGuid(reader["CREW_GUID"].ToString());
                    string crewName = reader["CREW_NAME"].ToString();
                    string crewPosition = reader["POSITION_NAME"].ToString();

                    OleDbCommand cmd1 = new OleDbCommand("", connection);
                    cmd1.CommandText =
                        "select Count(ID) \n" +
                        "from CREW_ON_BOARD \n" +
                        "where \n" +
                        "CREW_NAME='" + MainForm.StrToSQLStr(crewName) + "' \n" +
                        "and CREW_POSITION='" + MainForm.StrToSQLStr(crewPosition) + "' \n" +
                        "and VESSEL_GUID=" + MainForm.GuidToStr(vesselGuid);

                    bool valid = (int)cmd1.ExecuteScalar()>0;

                    cmd1.CommandText =
                        "update REPORTS_CREW set \n" +
                        "CREW_VALID=" + valid.ToString() + " \n" +
                        "where \n" +
                        "REPORT_CODE='" + MainForm.StrToSQLStr(reportCode) + "' \n" +
                        "and CREW_GUID=" + MainForm.GuidToStr(crewGuid);

                    if (MainForm.cmdExecute(cmd1) < 0)
                        MessageBox.Show("Failed to update validity mark for crew Guid=" + MainForm.GuidToStr(crewGuid),
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void btnShowInspectors_Click(object sender, EventArgs e)
        {
            if (!CheckForNewInspector()) return;

            //Show inspector's card

            OleDbCommand cmd = new OleDbCommand("", connection);

            //Проверяем наличие имени инспектора в списке
            
            cmd.CommandText =
                "select Count(INSPECTOR_GUID) \n"+
                "from INSPECTORS \n"+
                "where INSPECTOR_NAME like '" + StrToSQLStr(cbInspector.Text) + "'";
            int recCount = (int)cmd.ExecuteScalar();

            if (recCount == 0) return;

            cmd.CommandText = 
                "select INSPECTOR_GUID, INSPECTOR_NAME, Notes, Photo , Unfavourable, Background\n" +
                "from INSPECTORS \n"+
                "where INSPECTOR_NAME like '" + StrToSQLStr(cbInspector.Text) + "'";

            OleDbDataReader inspReader = cmd.ExecuteReader();

            if (MainForm.StrToGuid(cbInspector.SelectedValue.ToString())!=MainForm.zeroGuid)
            {
                Guid inspectorGuid = MainForm.StrToGuid(cbInspector.SelectedValue.ToString());

                InspectorForm inspForm = new InspectorForm(inspectorGuid, 0);
                
                inspReader.Read();

                inspForm.inspectorName = inspReader[1].ToString();

                inspForm.inspectorNotes = inspReader[2].ToString();

                inspForm.inspectorPhoto = inspReader[3].ToString();

                inspForm.inspectorUnfavourable = Convert.ToBoolean(inspReader[4]);


                inspReader.Close();

                var rslt = inspForm.ShowDialog();

                if (rslt == DialogResult.OK)
                {
                    //Сохраняем изменения
                    cmd.CommandText =
                    "update INSPECTORS set\n" +
                    "INSPECTOR_NAME='" + StrToSQLStr(inspForm.inspectorName) + "',\n" +
                    "Notes='" + StrToSQLStr(inspForm.inspectorNotes) + "',\n" +
                    "Photo='" + StrToSQLStr(inspForm.inspectorPhoto) + "',\n" +
                    "Unfavourable=" + Convert.ToString(inspForm.inspectorUnfavourable) + "\n" +
                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid);
                    cmd.ExecuteNonQuery();

                    //Обновляем имя для всех записей с выбранным ID
                    cmd.CommandText =
                        "update REPORTS set INSPECTOR_NAME='" + StrToSQLStr(inspForm.inspectorName) + "' \n" +
                        "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid);
                    cmd.ExecuteNonQuery();

                    fillInspectors();

                    //Записываем имя в поле
                    cbInspector.Text = inspForm.inspectorName;
                }
            }
        }

        public string StrToSQLStr(string Text)
        {
            return Text.Replace("'", "''");
        }

        private bool CheckForNewInspector()
        {
            if (cbInspector.Text.Trim().Length == 0) return false;

            if (cbInspector.SelectedValue == null)
            {
                OleDbCommand cmd = new OleDbCommand("", connection);

                string inspector = cbInspector.Text;

                string msg = "Inspector with name \"" + inspector + "\" was not found in database.\n" +
                    "Would you like to add him to the list of inspectors?";

                var rslt = System.Windows.Forms.MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (rslt == DialogResult.Yes)
                {
                    cmd.CommandText =
                        "insert into INSPECTORS (INSPECTOR_NAME)\n" +
                        "values('" + StrToSQLStr(inspector) + "')";
                    cmd.ExecuteNonQuery();

                    fillInspectors();

                    cbInspector.Text = inspector;

                    return true;
                }
                else
                    return false;
            }
            else
                return true;
        }

        private void fillInspectionType()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select * \n" +
                "from INSPECTION_TYPES \n" +
                "order by INSPECTION_TYPE";

            OleDbDataAdapter da = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("INSPECTION_TYPES"))
                DS.Tables["INSPECTION_TYPES"].Clear();

            da.Fill(DS, "INSPECTION_TYPES");

            cbInspectionType.DataSource = DS.Tables["INSPECTION_TYPES"];
            cbInspectionType.DisplayMember = "INSPECTION_TYPE";
            cbInspectionType.ValueMember = "INSPECTION_TYPE_ID";

            cbInspectionType.SelectedValue = 0;

            inspectionTypeFilled = true;
        }

        private void fillMemorandums()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select * \n" +
                "from MEMORANDUMS \n" +
                "order by MEMORANDUM_TYPE";

            OleDbDataAdapter da = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("MEMORANDUMS"))
                DS.Tables["MEMORANDUMS"].Clear();

            da.Fill(DS, "MEMORANDUMS");

            cbMemorandumType.DataSource = DS.Tables["MEMORANDUMS"];
            cbMemorandumType.DisplayMember = "MEMORANDUM_TYPE";
            cbMemorandumType.ValueMember = "MEMORANDUM_ID";
        }

        private void fillInspectors()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select * from \n" +
                "(select INSPECTOR_GUID, INSPECTOR_NAME, Notes, Photo, Unfavourable, Background \n" +
                "from INSPECTORS \n" +
                "union \n" +
                "select "+MainForm.GuidToStr(MainForm.zeroGuid)+" as INSPECTOR_GUID, '' as INSPECTOR_NAME, '' as Notes, '' as Photo, " +
                "false as Unfavourable, '' as Background \n" +
                "from Fonts) \n" +
                "order by INSPECTOR_NAME";

            OleDbDataAdapter inspectorsAdapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("INSPECTORS_LIST"))
                DS.Tables["INSPECTORS_LIST"].Clear();

            inspectorsAdapter.Fill(DS, "INSPECTORS_LIST");

            cbInspector.DataSource = DS.Tables["INSPECTORS_LIST"];
            cbInspector.DisplayMember = "INSPECTOR_NAME";
            cbInspector.ValueMember = "INSPECTOR_GUID";


        }

        private void btnNewCode_Click(object sender, EventArgs e)
        {
            string oldCode = _reportCode;

            if (tbReportCode.Text.Length > 0)
            {
                var rslt = MessageBox.Show("This report already has a code. Would you like to re-create it?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (rslt == DialogResult.No) 
                    return;
            }
            
            if (cbInspectionType.Text.Trim().Length==0)
            {
                MessageBox.Show("Please select inspection type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            /*
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select Max(IIF(IsNull(REPORT_CODE),'',REPORT_CODE)) as MAX_CODE\n" +
                "from REPORTS \n" +
                "where \n"+
                "REPORT_CODE like 'NS-%' \n"+
                "and Len(REPORT_CODE)=12 \n"+
                "and IsNumeric(Mid(REPORT_CODE,4,4)) \n"+
                "and IsNumeric(Mid(REPORT_CODE,9,4))";

            var vMaxCode = cmd.ExecuteScalar();

            string MaxCode = vMaxCode.ToString();

            if (MaxCode.Length > 0)
            {
                string Code = MaxCode.Substring(3);

                Code=Code.Substring(0,4)+Code.Substring(5,4);


                int value = -1;
                int newValue = 0;

                try
                {
                    value = Convert.ToInt32(Code);
                }
                catch
                {

                }

                if (value < 0)
                {
                    newValue = 1;
                }
                else
                {
                    newValue = value + 1;
                }

                Code = newValue.ToString("0000-0000");

                tbReportCode.Text = "NS-" + Code;

                _reportCode = tbReportCode.Text;
            }
            else
            {
                tbReportCode.Text = "NS-0000-0001";

                _reportCode = tbReportCode.Text;
            }
            */

            //if (oldCode.Length > 0 && _reportCode!=oldCode && dgvQuestions.Rows.Count > 0)

            reportCode = CreateReportCode();
            ChangeReportCode(oldCode, _reportCode);
        }


        private string CreateReportCode()
        {
            string prefix = "";

            switch (cbInspectionType.Text)
            {
                case "SIRE":
                    prefix = "SIR";
                    break;
                case "OVID":
                    prefix = "OVI";
                    break;
                case "PSC":
                    prefix = "PSC";
                    break;
                case "Non-SIRE (Safety)":
                    prefix = "NSS";
                    break;
                case "Non-SIRE (Terminal)":
                    prefix = "NST";
                    break;
                case "CDI":
                    prefix = "CDI";
                    break;
                default:
                    prefix = "XXX";
                    break;
            }

            string code = DateTime.Now.ToString("HHmm-ddMM-yyyy");

            string rCode = prefix + "A" + "-" + code;

            int index = 0;

            while (ReportCodeExists(rCode))
            {
                index++;

                char ch = (char)(65 + index);

                rCode = prefix + ch.ToString() + "-" + code;
            }

            return rCode;
        }

        private bool ReportCodeExists(string rCode)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select Count(REPORT_CODE) as RecCount \n" +
                "from REPORTS \n" +
                "where REPORT_CODE='" + StrToSQLStr(rCode) + "'";

            int recs = (int)cmd.ExecuteScalar();

            if (recs > 0)
                return true;
            else
                return false;
        }
        private void FormReportSummary_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Modified on 10.10.2016 in version 2.0.15.1
            //  Call CheckUnsaved before form close
            //Modified on 11.10.2016 in version 2.0.15.1
            //  Check for report code 

            CheckUnsaved();

            if (this.DialogResult == DialogResult.OK)
            {
                if (_reportCode.Length == 0)
                {
                    MessageBox.Show("Please provide report code",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    e.Cancel = true;
                    return;
                }
                
                if ((cbInspector.Text.Length > 0) && (cbInspector.SelectedValue == null))
                {
                    CheckForNewInspector();
                }

                if (MainForm.isPowerUser) 
                    SaveReport();
            }

            Properties.Settings.Default.FormReportSummaryState = this.WindowState;

            if (this.WindowState == FormWindowState.Normal)
            {
                // save location and size if the state is normal
                Properties.Settings.Default.FormReportSummaryLocation = this.Location;
                Properties.Settings.Default.FormReportSummarySize = this.Size;
            }
            else
            {
                // save the RestoreBounds if the form is minimized or maximized!
                Properties.Settings.Default.FormReportSummaryLocation = this.RestoreBounds.Location;
                Properties.Settings.Default.FormReportSummarySize = this.RestoreBounds.Size;
            }

            // don't forget to save the settings
            Properties.Settings.Default.Save();
        }


        private void cbQVersion_TextChanged(object sender, EventArgs e)
        {
            // Create list of questionnaire types
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText = 
                "select * from \n"+
                "(select TOP 1 '' as TITLE, '' as TYPE_CODE \n"+
                "from TEMPLATES \n"+
                "union \n"+
                "select TITLE, TYPE_CODE \n"+
                "from TEMPLATES \n"+
                "where VERSION='" + StrToSQLStr(cbQVersion.Text) + "') as Q \n"+
                "order by TITLE";
            
            if (DS.Tables.Contains("VIQ_TYPES"))
            {
                DS.Tables["VIQ_TYPES"].Clear();
            }

            OleDbDataAdapter viqTypesAdapter = new OleDbDataAdapter(cmd);

            viqTypesAdapter.Fill(DS, "VIQ_TYPES");

            cbQType.DataSource = DS.Tables["VIQ_TYPES"];
            cbQType.DisplayMember = "TITLE";
            cbQType.ValueMember = "TYPE_CODE";

        }

        private Guid SaveNewCrew(string crewName, Guid positionGuid)
        {
            if (crewName.Trim().Length == 0)
                return MainForm.zeroGuid;

            OleDbCommand cmd = new OleDbCommand("", connection);

            Guid crewGuid = Guid.NewGuid();

            cmd.CommandText =
                "insert into CREW (CREW_GUID,CREW_NAME,CREW_POSITION_GUID)\n" +
                "values(" + MainForm.GuidToStr(crewGuid) + ",'" +
                StrToSQLStr(crewName) + "'," +
                MainForm.GuidToStr(positionGuid) + ")";

            if (MainForm.cmdExecute(cmd) < 0)
                return MainForm.zeroGuid;
            else
                return crewGuid;

        }



        private void button7_Click(object sender, EventArgs e)
        {
            string reportFile = workFolder + "\\Reports\\" + tbReportCode.Text + ".pdf";

            fileAvailable = ReportFileExists();

            if (fileAvailable)
            {
                Process.Start(reportFile);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("File \"" + tbReportCode.Text + ".pdf was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbReportCode_DoubleClick(object sender, EventArgs e)
        {
            if (tbReportCode.ReadOnly)
            {
                var rslt = MessageBox.Show("Would you like to edit report code?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (rslt==DialogResult.Yes)
                {
                    tbReportCode.ReadOnly = false;

                    MessageBox.Show("You are able to edit report number now.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void tbReportCode_Leave(object sender, EventArgs e)
        {
        }

        private bool reportCodeExists(string reportCode)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select Count(REPORT_CODE) as ReportCount \n" +
                "from REPORTS \n" +
                "where REPORT_CODE='" + StrToSQLStr(reportCode) + "' \n"+
                "and REPORT_CODE<>'" + StrToSQLStr(_reportCode)+"'";

            int recs = (int) cmd.ExecuteScalar();

            if (recs == 0)
                return false;
            else
                return true;
        }

        private void updateQuestions()
        {
            //Modified on 10.10.2016 in version 2.0.15.1
            //  Add QUESTION_ID to select script
            //Modified on 12.10.2016 in version 2.0.15.1
            //  Switch on programatically sorting for QUESTION_NUMBER column

            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select \n" +
                "TEMPLATE_QUESTIONS.QUESTION_NUMBER, \n" +
                "REPORT_ITEMS.QUESTION_NUMBER as REPORT_QUESTION_NUMBER, \n" +
                "TEMPLATE_QUESTIONS.QUESTION_TEXT, \n" +
                "REPORT_ITEMS.SEQUENCE, \n"+
                "REPORT_ITEMS.REPORT_ITEM_ID, \n" +
                "REPORT_ITEMS.QUESTION_GUID, \n" +
                "REPORT_ITEMS.OBSERVATION, \n" +
                "REPORT_ITEMS.COMMENTS, \n" +
                "REPORT_ITEMS.OPERATOR_COMMENTS, \n" +
                "REPORT_ITEMS.OBSERVATION_CAUSE, \n" +
                "REPORT_ITEMS.SUBJECT_INDEX, \n" +
                "REPORT_ITEMS.TECHNICAL_COMMENTS, \n" +
                "REPORT_ITEMS.CAUSE_INDEX, \n" +
                "REPORT_ITEMS.OBS_CORRECT, \n" +
                "REPORT_ITEMS.OBS_TYPE, \n" +
                "REPORT_ITEMS.OBS_REASON \n" +
                "from REPORT_ITEMS left join TEMPLATE_QUESTIONS  \n" +
                "on (TEMPLATE_QUESTIONS.QUESTION_GUID=REPORT_ITEMS.QUESTION_GUID \n" +
                " and TEMPLATE_QUESTIONS.TEMPLATE_GUID='{" + JustGUID(_templateGUID) + "}') \n" +
                "where \n" +
                "REPORT_ITEMS.REPORT_CODE='" + tbReportCode.Text + "' \n" +
                "and 1=1 \n" +
                "order by TEMPLATE_QUESTIONS.SEQUENCE";

            switch (cbItems.Text)
            {
                case "All":
                    break;
                case "Observations":
                    cmd.CommandText = cmd.CommandText.Replace("1=1", "LEN(OBSERVATION)>0");
                    break;
                case "Inspector comments":
                    cmd.CommandText = cmd.CommandText.Replace("1=1", "LEN(COMMENTS)>0");
                    break;
                case "Office comments":
                    cmd.CommandText = cmd.CommandText.Replace("1=1", "LEN(OPERATOR_COMMENTS)>0");
                    break;
            }


            OleDbDataAdapter qAdapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("REPORT_QUESTIONS"))
            {
                try
                {
                    DS.Tables["REPORT_QUESTIONS"].Clear();
                }
                catch
                {

                }
            }

            qAdapter.Fill(DS, "REPORT_QUESTIONS");

            BindingSource bs = new BindingSource(DS, "REPORT_QUESTIONS");

            dgvQuestions.DataSource = bs;

            dgvQuestions.AutoGenerateColumns = true;

            dgvQuestions.Columns["QUESTION_NUMBER"].Visible = false;
            dgvQuestions.Columns["QUESTION_NUMBER"].HeaderText = "No.";
            dgvQuestions.Columns["QUESTION_NUMBER"].FillWeight = 20;
            dgvQuestions.Columns["QUESTION_NUMBER"].SortMode = DataGridViewColumnSortMode.Programmatic;

            dgvQuestions.Columns["REPORT_QUESTION_NUMBER"].Visible = true;
            dgvQuestions.Columns["REPORT_QUESTION_NUMBER"].HeaderText = "No.";
            dgvQuestions.Columns["REPORT_QUESTION_NUMBER"].FillWeight = 20;
            dgvQuestions.Columns["REPORT_QUESTION_NUMBER"].SortMode = DataGridViewColumnSortMode.Programmatic;

            dgvQuestions.Columns["QUESTION_TEXT"].HeaderText = "Question text";
            dgvQuestions.Columns["QUESTION_TEXT"].FillWeight = 80;

            dgvQuestions.Columns["OBSERVATION"].Visible = false;

            if (dgvQuestions.CurrentRow != null)
                tbObservation.Text = dgvQuestions.CurrentRow.Cells["OBSERVATION"].Value.ToString();
            else
                tbObservation.Text = "";

            dgvQuestions.Columns["SEQUENCE"].Visible = false;
            dgvQuestions.Columns["REPORT_ITEM_ID"].Visible = false;
            dgvQuestions.Columns["QUESTION_GUID"].Visible = false;
            dgvQuestions.Columns["COMMENTS"].Visible = false;
            dgvQuestions.Columns["OPERATOR_COMMENTS"].Visible = false;
            dgvQuestions.Columns["OBSERVATION_CAUSE"].Visible = false;
            dgvQuestions.Columns["SUBJECT_INDEX"].Visible = false;
            dgvQuestions.Columns["TECHNICAL_COMMENTS"].Visible = false;
            dgvQuestions.Columns["CAUSE_INDEX"].Visible = false;
            dgvQuestions.Columns["OBS_CORRECT"].Visible = false;
            dgvQuestions.Columns["OBS_TYPE"].Visible = false;
            dgvQuestions.Columns["OBS_REASON"].Visible = false;
        }

        private void cbItems_TextChanged(object sender, EventArgs e)
        {
            if (isVisible)
                updateQuestions();
        }

        private void dgvQuestions_SelectionChanged(object sender, EventArgs e)
        {
            //Modified on 10.10.2016 in version 2.0.1.15
            //  Call UpdateControlValues;

            if (dgvQuestions.CurrentRow != null)
            {
                gridUpdate = true;

                UpdateControlValues();

                SetSaveInactive();


                gridUpdate = false;
            }
            else
            {
                tbObservation.Text = "";
                tbInspectorComments.Text = "";
                tbOperatorComments.Text = "";
                cbCorrect.Text = "";
                cbType.Text = "";
                cbReason.Text = "";
                _questionGUID = "Null";
                SetSaveInactive();
            }
        }

        private void UpdateControlValues()
        {
            //Created on 10.10.2016 in version 2.0.15.1
            if (dgvQuestions.CurrentRow == null)
            {
                if (dgvQuestions.Rows.Count > 0)
                {
                    tbObservation.Text = dgvQuestions.Rows[0].Cells["OBSERVATION"].Value.ToString();
                    tbInspectorComments.Text = dgvQuestions.Rows[0].Cells["COMMENTS"].Value.ToString();
                    tbOperatorComments.Text = dgvQuestions.Rows[0].Cells["OPERATOR_COMMENTS"].Value.ToString();
                    cbCorrect.SelectedValue = dgvQuestions.Rows[0].Cells["OBS_CORRECT"].Value;
                    cbType.SelectedValue = dgvQuestions.Rows[0].Cells["OBS_TYPE"].Value;
                    cbReason.SelectedValue = dgvQuestions.Rows[0].Cells["OBS_REASON"].Value;
                    _questionGUID = dgvQuestions.Rows[0].Cells["QUESTION_GUID"].Value.ToString();
                }
                else
                {
                    tbObservation.Text = "";
                    tbInspectorComments.Text = "";
                    tbOperatorComments.Text = "";
                    cbCorrect.SelectedValue = 0;
                    cbType.SelectedValue = 0;
                    cbReason.SelectedValue = 0;
                    _questionGUID = "";
                }
            }
            else
            {
                tbObservation.Text = dgvQuestions.CurrentRow.Cells["OBSERVATION"].Value.ToString();
                tbInspectorComments.Text = dgvQuestions.CurrentRow.Cells["COMMENTS"].Value.ToString();
                tbOperatorComments.Text = dgvQuestions.CurrentRow.Cells["OPERATOR_COMMENTS"].Value.ToString();
                cbCorrect.SelectedValue = dgvQuestions.CurrentRow.Cells["OBS_CORRECT"].Value;
                cbType.SelectedValue = dgvQuestions.CurrentRow.Cells["OBS_TYPE"].Value;
                cbReason.SelectedValue = dgvQuestions.CurrentRow.Cells["OBS_REASON"].Value;
                _questionGUID = dgvQuestions.CurrentRow.Cells["QUESTION_GUID"].Value.ToString();
            }
        }

        private void CheckUnsaved()
        {
            //return;

            if (needSave && !fillingNumbers && !formOpen && !readOnly)
            {
                var rslt = MessageBox.Show(
                    "Some data was not saved. Would you like to save data now?", 
                    "Confirmation", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question);

                if (rslt == DialogResult.Yes)
                {
                    saveItem();
                }

                SetSaveInactive();
            }
        }

        private void SetSaveActive()
        {
            if (gridUpdate) return;

            if (cbUpdate) return;

            btnSaveItem.Enabled = true;
         
            needSave = true;
        }

        private void SetSaveInactive()
        {
            btnSaveItem.Enabled = false;

            needSave = false;
        }

        private void saveItem()
        {
            //Save current question
            //Modified on 10.10.2016 in version 2.0.15.1
            //  Use question ID to save information
            //  Check global question GUID and change it if it is necessary

            string observation = tbObservation.Text.Trim();
            string inspectorComments = tbInspectorComments.Text.Trim();
            string operatorComments = tbOperatorComments.Text.Trim();

            if (dgvQuestions.CurrentRow == null)
            {
                dgvQuestions.CurrentCell = dgvQuestions[2, 0];
            }

            string xGUID = dgvQuestions.CurrentRow.Cells["QUESTION_GUID"].Value.ToString();

            string qGUID = xGUID;
 
            if (!qGUID.StartsWith("{"))
                qGUID="{"+qGUID+"}";

            if (_questionGUID != xGUID)
                _questionGUID = xGUID;

            string questionID = dgvQuestions.CurrentRow.Cells["REPORT_ITEM_ID"].Value.ToString();

            string reportCode=tbReportCode.Text.Trim();
            string questionNumber=dgvQuestions.CurrentRow.Cells["QUESTION_NUMBER"].Value.ToString();
            string sequence = dgvQuestions.CurrentRow.Cells["SEQUENCE"].Value.ToString();

            int valType = 0;
            int valCorrect = 0;
            int valReason = 0;

            OleDbCommand cmd = new OleDbCommand("", connection);
            int rowIndex = dgvQuestions.CurrentRow.Index;

            if (observation.Length == 0 && inspectorComments.Length == 0 && operatorComments.Length == 0)
            {
                var rslt = System.Windows.Forms.MessageBox.Show(
                    "There is not any information for the item besides question number.\n" +
                    "Would you like to save this item anyway?",
                    "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (rslt != DialogResult.Yes)
                    return;
            }

            if (cbCorrect.SelectedValue != null)
                valCorrect = Convert.ToInt16(cbCorrect.SelectedValue);

            if (cbType.SelectedValue != null)
                valType = Convert.ToInt16(cbType.SelectedValue);

            if (cbReason.SelectedValue != null)
                valReason = Convert.ToInt16(cbReason.SelectedValue);

            //Save report item
            dgvQuestions.Rows[rowIndex].Cells["OBSERVATION"].Value = observation;
            dgvQuestions.Rows[rowIndex].Cells["COMMENTS"].Value = inspectorComments;
            dgvQuestions.Rows[rowIndex].Cells["OPERATOR_COMMENTS"].Value = operatorComments;
            dgvQuestions.Rows[rowIndex].Cells["OBS_CORRECT"].Value = valCorrect;
            dgvQuestions.Rows[rowIndex].Cells["OBS_TYPE"].Value = valType;
            dgvQuestions.Rows[rowIndex].Cells["OBS_REASON"].Value = valReason;
            dgvQuestions.Rows[rowIndex].Cells["QUESTION_NUMBER"].Value = questionNumber;
            dgvQuestions.Rows[rowIndex].Cells["SEQUENCE"].Value = sequence;


            //check wheather there is a record with the same question number
            cmd.CommandText =
                "select Count(QUESTION_NUMBER) \n" +
                "from REPORT_ITEMS \n" +
                "where REPORT_CODE='" + StrToSQLStr(reportCode) + "'\n" +
                "and QUESTION_GUID like '" + qGUID + "'";

            int qcount = (int)cmd.ExecuteScalar();

            int curRow = 0;

            if (qcount == 0)
            {
                //New item
                cmd.CommandText =
                    "insert into REPORT_ITEMS (REPORT_CODE, QUESTION_NUMBER, OBSERVATION, OBSERVATION_LEN, COMMENTS, " +
                    "COMMENTS_LEN, QUESTION_GUID, OPERATOR_COMMENTS, OPERATOR_COMMENTS_LEN, OBS_CORRECT, OBS_TYPE, " +
                    "OBS_REASON, TEMPLATE_GUID, SEQUENCE)\n" +
                    "values ('" + StrToSQLStr(reportCode) + "', '" +
                                StrToSQLStr(questionNumber) + "', '" +
                                StrToSQLStr(observation) + "', " +
                                observation.Length.ToString() + ", '" +
                                StrToSQLStr(inspectorComments) + "', " +
                                inspectorComments.Length.ToString() + ", '" +
                                qGUID + "', '" +
                                StrToSQLStr(operatorComments) + "', " +
                                operatorComments.Length.ToString() + ", " +
                                valCorrect.ToString() + ", " +
                                valType.ToString() + ", " +
                                valReason.ToString() + ", " +
                                MainForm.FormatGuidString(_templateGUID) + ", '" +
                                MainForm.StrToSQLStr(sequence) + "')";
                cmd.ExecuteNonQuery();

            }
            else
            {
                if (dgvQuestions.CurrentRow == null)
                    return;

                curRow = dgvQuestions.CurrentRow.Index;

                //Update item
                cmd.CommandText =
                    "update REPORT_ITEMS set \n" +
                    "OBSERVATION='" + StrToSQLStr(observation) + "', \n" +
                    "OBSERVATION_LEN=" + observation.Length.ToString() + ", \n" +
                    "COMMENTS='" + StrToSQLStr(inspectorComments) + "', \n" +
                    "COMMENTS_LEN=" + inspectorComments.Length.ToString() + ", \n" +
                    "OPERATOR_COMMENTS='" + StrToSQLStr(operatorComments) + "', \n" +
                    "OPERATOR_COMMENTS_LEN=" + operatorComments.Length.ToString() + ", \n" +
                    "OBS_CORRECT=" + valCorrect.ToString() + ", \n" +
                    "OBS_TYPE=" + valType.ToString() + ", \n" +
                    "OBS_REASON=" + valReason.ToString() + ", \n" +
                    "QUESTION_NUMBER='" + StrToSQLStr(questionNumber) + "', \n" +
                    "SEQUENCE='" + MainForm.StrToSQLStr(sequence) + ", \n" +
                    "TEMPLATE_GUID=" + MainForm.FormatGuidString(_templateGUID) + " \n" +
                    "where REPORT_ITEMS_ID = " + questionID;
                cmd.ExecuteNonQuery();

            }


            SetSaveInactive();

        }

        private void cbCorrect_TextChanged(object sender, EventArgs e)
        {
            switch (cbCorrect.Text)
            {
                case "":
                    cbType.DataSource = null;
                    cbType.Text = "";
                    cbType.Items.Clear();
                    cbType.Update();
                    cbReason.DataSource = null;
                    cbReason.Text = "";
                    cbReason.Items.Clear();
                    cbReason.Update();
                    break;
                case "No":
                    cbType.DataSource = null;
                    cbType.Text = "";
                    cbType.Items.Clear();
                    cbReason.DataSource = null;
                    cbReason.Text = "";
                    cbReason.Items.Clear();
                    break;
                case "Yes":
                    cbType.DataSource = DS.Tables["TYPE_ITEMS"];
                    cbType.DisplayMember = "LIST_ITEM";
                    cbType.ValueMember = "LIST_ITEM_VALUE";
                    break;
            }

            SetSaveActive();

        }

        private void cbType_TextChanged(object sender, EventArgs e)
        {
            switch (cbType.Text)
            {
                case "":
                    cbReason.DataSource = null;
                    cbReason.Text = "";
                    break;
                case "Technical":
                    cbReason.DataSource = DS.Tables["TECHNICAL_ITEMS"];
                    cbReason.DisplayMember = "LIST_ITEM";
                    cbReason.ValueMember = "LIST_ITEM_VALUE";
                    cbReason.Text = "";
                    break;
                case "Procedural":
                    cbReason.DataSource = DS.Tables["PROCEDURAL_ITEMS"];
                    cbReason.DisplayMember = "LIST_ITEM";
                    cbReason.ValueMember = "LIST_ITEM_VALUE";
                    cbReason.Text = "";
                    break;
            }

            SetSaveActive();
        }

        private void cbReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSaveActive();
        }

        private void FillComboBox()
        {
            cbUpdate = true;

            OleDbCommand cmd = new OleDbCommand("", connection);
            cmd.CommandText =
                "select * from LIST_ITEMS \n" +
                "where LIST_CODE='0001' \n" +
                "order by LIST_ITEM_VALUE";

            OleDbDataAdapter daCorrect = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("CORRECT_ITEMS"))
            {
                DS.Tables["CORRECT_ITEMS"].Clear();
            }

            daCorrect.Fill(DS, "CORRECT_ITEMS");

            cbCorrect.DataSource = DS.Tables["CORRECT_ITEMS"];

            cbCorrect.ValueMember = "LIST_ITEM_VALUE";
            cbCorrect.DisplayMember = "LIST_ITEM";

            cmd.CommandText =
                "select * from LIST_ITEMS \n" +
                "where LIST_CODE='0002' \n" +
                "order by LIST_ITEM_VALUE";

            OleDbDataAdapter daType = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("TYPE_ITEMS"))
            {
                DS.Tables["TYPE_ITEMS"].Clear();
            }

            daType.Fill(DS, "TYPE_ITEMS");
            /*
            cbType.DataSource = DS.Tables["TYPE_ITEMS"];
            cbType.DisplayMember = "LIST_ITEM";
            cbType.ValueMember = "LIST_ITEM_VALUE";
            */

            cmd.CommandText =
                "select * from LIST_ITEMS \n" +
                "where LIST_CODE='0003' \n" +
                "order by LIST_ITEM_VALUE";

            OleDbDataAdapter daTechReason = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("TECHNICAL_ITEMS"))
            {
                DS.Tables["TECHNICAL_ITEMS"].Clear();
            }

            daTechReason.Fill(DS, "TECHNICAL_ITEMS");

            cmd.CommandText =
                "select * from LIST_ITEMS \n" +
                "where LIST_CODE='0004' \n" +
                "order by LIST_ITEM_VALUE";

            OleDbDataAdapter daProcReason = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("PROCEDURAL_ITEMS"))
            {
                DS.Tables["PROCEDURAL_ITEMS"].Clear();
            }

            daProcReason.Fill(DS, "PROCEDURAL_ITEMS");

            cbUpdate = false;

        }

        private void FormReportSummary_Shown(object sender, EventArgs e)
        {
            //Modified on 12.10.2016 in version 2.0.15.1
            //  Locate selected question

            updateQuestions();

            if (_selectedNumber.Length > 0)
            {
                MainForm.LocateGridRecord(_selectedNumber, "QUESTION_NUMBER", -1, dgvQuestions);
                UpdateControlValues();
                SetSaveInactive();
            }
            else
            {
                if (dgvQuestions.Rows.Count > 0)
                {
                    try
                    {
                        dgvQuestions.CurrentCell = dgvQuestions["QUESTION_NUMBER", dgvQuestions.FirstDisplayedScrollingRowIndex];
                    }
                    catch
                    {

                    }
                }

                UpdateControlValues();
                
            }

            SetSaveInactive();

            isVisible = true;
        }

        private void btnQAdd_Click(object sender, EventArgs e)
        {
            //Created in version 2.10.15.1
            //Modified on 10.10.2016 in version 2.0.15.1
            //  Call UpdateControlValues and SetSaveInactive

            if (_reportCode.Length == 0)
            {
                MessageBox.Show(
                    "Please provide report code",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (cbQType.Text.Length == 0)
            {
                MessageBox.Show(
                    "Please select questionnaire type", 
                    "Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
                return;
            }

            if (cbQVersion.Text.Length == 0)
            {
                MessageBox.Show(
                    "Please select questionnaire version", 
                    "Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
                return;
            }

            FrmSelectQuestion form = new FrmSelectQuestion(GetTemplateGUID());

            var rslt = form.ShowDialog();

            if (rslt==DialogResult.OK)
            {
                string questionGUID = form.selectedQuestionGUID;
                string questionNumber = form.selectedQuestionNumber;
                string questionSequence = form.sequence;

                //Create new question record
                OleDbCommand cmd = new OleDbCommand("", connection);

                cmd.CommandText =
                    "insert into REPORT_ITEMS (REPORT_CODE, QUESTION_NUMBER, QUESTION_GUID, TEMPLATE_GUID, SEQUENCE) \n" +
                    "values('" + StrToSQLStr(tbReportCode.Text) + "','" +
                        questionNumber + "'," +
                        MainForm.FormatGuidString(questionGUID) + "," +
                        MainForm.FormatGuidString(_templateGUID) + ",'" +
                        MainForm.StrToSQLStr(questionSequence) + "')";
                
                if (MainForm.cmdExecute(cmd)<0)
                {
                    MessageBox.Show("Failed to insert record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (isVisible)
                    updateQuestions();

                MainForm.LocateGridRecord(questionNumber, "QUESTION_NUMBER", -1, dgvQuestions);

                UpdateControlValues();

                SetSaveInactive();
            }
        }

        private string GetTemplateGUID()
        {
            string templateGUID = "";

            if (cbQVersion.Text.Length == 0)
                return templateGUID;

            if (cbQType.Text.Length == 0)
                return templateGUID;

            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select top 1 CStr(TEMPLATE_GUID) \n" +
                "from TEMPLATES \n" +
                "where TYPE_CODE='" + cbQType.SelectedValue.ToString() + "' \n" +
                "and VERSION='" + cbQVersion.Text + "'";

            templateGUID = (string) cmd.ExecuteScalar();

            return templateGUID;
        }

        private void tbObservation_TextChanged(object sender, EventArgs e)
        {
            SetSaveActive();
        }

        private void tbInspectorComments_TextChanged(object sender, EventArgs e)
        {
            SetSaveActive();
        }

        private void tbOperatorComments_TextChanged(object sender, EventArgs e)
        {
            SetSaveActive();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FrmTestCombo form = new FrmTestCombo(connection, DS);

            form.ShowDialog();
        }

        private void btnSaveItem_Click(object sender, EventArgs e)
        {
            saveItem();
        }

        private void dgvQuestions_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            //Leave row
            CheckUnsaved();


        }

        private void tbReportCode_Validating(object sender, CancelEventArgs e)
        {
            if (this.ActiveControl == btnCancel)
            {
                this.DialogResult = DialogResult.Cancel;
                return;
            }

            string tempCode = tbReportCode.Text.Trim();

            if (newRec)
            {
                //Check whether code exists
                if ((tbReportCode.Text.Trim().Length>0) && (reportCodeExists(tbReportCode.Text)))
                {
                    MessageBox.Show(
                        "Report with code \"" + tbReportCode.Text + "\" already exists",
                        "Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    e.Cancel = true;
                    return;
                }

                if ((_reportCode.Length > 0) && (tempCode.Length == 0) && (dgvQuestions.Rows.Count > 0))
                {
                    MessageBox.Show(
                        "Please provide report code",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    e.Cancel = true;
                    return;
                }

                if ((dgvQuestions.Rows.Count > 0) && (_reportCode != tempCode))
                {
                    if (!ChangeReportCode(_reportCode, tempCode))
                    {
                        MessageBox.Show(
                            "Fail to update report code",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        e.Cancel = true;
                        return;
                    }
                }

                if (_reportCode != tempCode)
                    _reportCode = tempCode;
            }
            else
            {
                //Check whether code exists
                if ((tbReportCode.Text.Trim().Length > 0) && (reportCodeExists(tbReportCode.Text)))
                {
                    MessageBox.Show(
                        "Report with code \"" + tbReportCode.Text + "\" already exists",
                        "Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    e.Cancel = true;
                    return;
                }

                if ((_reportCode.Length > 0) && (tempCode.Length == 0) && (dgvQuestions.Rows.Count > 0))
                {
                    MessageBox.Show(
                        "Please provide report code",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    e.Cancel = true;
                    return;
                }

                if ((dgvQuestions.Rows.Count > 0) && (_reportCode != tempCode))
                {
                    if (!ChangeReportCode(_reportCode, tempCode))
                    {
                        MessageBox.Show(
                            "Fail to update report code",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        e.Cancel = true;
                        return;
                    }
                }

                if (_reportCode != tempCode)
                    _reportCode = tempCode;
            }

            fileAvailable = ReportFileExists();

            //_reportCode = tbReportCode.Text.Trim();
        }

        private bool ChangeReportCode(string oldCode, string newCode)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);
            OleDbTransaction transaction = connection.BeginTransaction();
            cmd.Transaction = transaction;

            try
            {
                cmd.CommandText =
                    "update REPORT_ITEMS set \n" +
                    "REPORT_CODE='" + StrToSQLStr(newCode) + "' \n" +
                    "where REPORT_CODE='" + StrToSQLStr(oldCode) + "'";

                if (MainForm.cmdExecute(cmd) < 0)
                    return false;

                cmd.CommandText=
                    "update REPORTS_CREW set \n"+
                    "REPORT_CODE='" + StrToSQLStr(newCode) + "' \n" +
                    "where REPORT_CODE='" + StrToSQLStr(oldCode) + "'";

                if (MainForm.cmdExecute(cmd) < 0)
                    return false;

                transaction.Commit();

                return true;
            }
            finally
            {
                if (transaction.Connection!=null)
                {
                    transaction.Rollback();
                }
            }
        }

        private void btnQDelete_Click(object sender, EventArgs e)
        {
            //Delete item
            //Created on 10.10.2016

            if (dgvQuestions.Rows.Count == 0)
                return;

            var rslt = MessageBox.Show(
                "You are going to delete record for the following question:\n\n" +
                dgvQuestions.CurrentRow.Cells["QUESTION_NUMBER"].Value.ToString() +" "+
                dgvQuestions.CurrentRow.Cells["QUESTION_TEXT"].Value.ToString() + "\n\n" +
                "Would you like to proceed?",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (rslt==DialogResult.Yes)
            {
                int curRow = dgvQuestions.CurrentRow.Index;

                OleDbCommand cmd = new OleDbCommand("", connection);

                cmd.CommandText =
                    "delete from REPORT_ITEMS \n" +
                    "where REPORT_ITEM_ID=" + dgvQuestions.CurrentRow.Cells["REPORT_ITEM_ID"].Value.ToString();

                if (MainForm.cmdExecute(cmd)<0)
                {
                    MessageBox.Show(
                        "Failed to remove record",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    if (isVisible)
                        updateQuestions();

                    if (dgvQuestions.Rows.Count > 0)
                    {
                        if (dgvQuestions.Rows.Count > curRow)
                        {
                            try
                            {
                                dgvQuestions.CurrentCell = dgvQuestions["QUESTION_NUMBER", curRow];
                            }
                            catch
                            {
                                //do nothing
                            }
                        }
                        else
                        {
                            dgvQuestions.CurrentCell = dgvQuestions["QUESTION_NUMBER", dgvQuestions.Rows.Count - 1];
                        }
                    }

                    UpdateControlValues();

                    SetSaveInactive();

                }
            }
        }

        private void dgvQuestions_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            
        }


        private void dgvQuestions_Sorted(object sender, EventArgs e)
        {

        }

        private void dgvQuestions_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Created on 12.10.2016 in version 2.0.15.1
            
            if ((dgvQuestions.Columns[e.ColumnIndex].Name == "QUESTION_NUMBER") ||
                (dgvQuestions.Columns[e.ColumnIndex].Name == "REPORT_QUESTION_NUMBER"))
            {
                // Check which column is selected, otherwise set NewColumn to null.
                DataGridViewColumn newColumn = dgvQuestions.Columns[e.ColumnIndex];
                DataGridViewColumn newSortColumn = dgvQuestions.Columns["SEQUENCE"];

                DataGridViewColumn oldColumn = dgvQuestions.SortedColumn;
                ListSortDirection direction;

                // If oldColumn is null, then the DataGridView is not currently sorted.
                if (oldColumn != null)
                {
                    // Sort the same column again, reversing the SortOrder.
                    if (oldColumn == newSortColumn &&
                        dgvQuestions.SortOrder == SortOrder.Ascending)
                    {
                        direction = ListSortDirection.Descending;
                    }
                    else
                    {
                        // Sort a new column and remove the old SortGlyph.
                        direction = ListSortDirection.Ascending;
                        oldColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                    }
                }
                else
                {
                    direction = ListSortDirection.Descending;
                }

                // If no column has been selected, display an error dialog  box.
                if (newColumn == null)
                {
                    MessageBox.Show("Select a single column and try again.",
                        "Error: Invalid Selection", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    dgvQuestions.Sort(newSortColumn, direction);

                    newColumn.HeaderCell.SortGlyphDirection =
                        direction == ListSortDirection.Ascending ?
                        SortOrder.Ascending : SortOrder.Descending;
                }
            }
        }

        private void FillVessels()
        {
            //Created on 12.10.2016 in version 2.0.15.1

            OleDbCommand cmd = new OleDbCommand("", connection);

            //Заполняем список судов
            cbVesselName.Items.Clear();
            
            cmd.CommandText =
                "select * from \n"+
                "( \n" +
                "select VESSEL_GUID, VESSEL_NAME, VESSEL_IMO, VESSEL_NAME+' (IMO '+VESSEL_IMO+')' as VESSEL_FULL_NAME \n" +
                "from VESSELS \n" +
                "union \n" +
                "select top 1 " + MainForm.GuidToStr(MainForm.zeroGuid)+" as VESSEL_GUID, '' as VESSEL_NAME, '' AS VESSEL_IMO, '' as VESSEL_FULL_NAME \n" +
                "from VESSELS \n"+
                ") as Q \n"+
                "order by VESSEL_NAME";

            OleDbDataAdapter vessels = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("SUMMARY_VESSEL_LIST"))
                DS.Tables["SUMMARY_VESSEL_LIST"].Clear();

            vessels.Fill(DS, "SUMMARY_VESSEL_LIST");
            cbVesselName.DataSource = DS.Tables["SUMMARY_VESSEL_LIST"];
            cbVesselName.DisplayMember = "VESSEL_NAME";
            cbVesselName.ValueMember = "VESSEL_GUID";
        }

        private void FillPorts()
        {
            //Created on 12.10.2016 in version 2.0.15.1

            //Заполняем данные порта
            cbInspectionPort.Items.Clear();

            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText = 
                "select DISTINCT INSPECTION_PORT \n"+
                "from REPORTS \n"+
                "order by INSPECTION_PORT";

            OleDbDataReader ports = cmd.ExecuteReader();
            
            while (ports.Read())
            {
                cbInspectionPort.Items.Add(ports[0].ToString());
            }

            ports.Close();
        }

        private void FillCompanies()
        {
            //Created on 12.10.2016 in version 2.0.15.1

            //Заполняем данные компаний
            cbCompany.Items.Clear();

            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText = 
                "select DISTINCT COMPANY \n"+
                "from REPORTS \n"+
                "order by COMPANY";
            
            OleDbDataReader companies = cmd.ExecuteReader();

            while (companies.Read())
                cbCompany.Items.Add(companies[0].ToString());

            companies.Close();
        }

        private Guid GetTemplateTypeGuid()
        {
            string inspectionTypeStr = cbInspectionType.Text;

            if (inspectionTypeStr == "SIRE")
                inspectionTypeStr = "VIQ";
            else
            {
                if (inspectionTypeStr == "OVID")
                    inspectionTypeStr = "OVIQ";
            }

            if (inspectionTypeStr.Trim().Length == 0)
                return MainForm.zeroGuid;

            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select TEMPLATE_TYPE_GUID \n" +
                "from TEMPLATE_TYPES \n" +
                "where TEMPLATE_TYPE='" + MainForm.StrToSQLStr(inspectionTypeStr) + "'";

            object rec = cmd.ExecuteScalar();

            if (rec == null)
                return MainForm.zeroGuid;
            else
                return (Guid)rec;
        }

        private void FillVersions()
        {
            //Created on 12.10.2016 in version 2.0.15.1

            //Заполняем версию вопросника
            OleDbCommand cmd = new OleDbCommand("", connection);

            Guid templateTypeGuid = GetTemplateTypeGuid();

            if (templateTypeGuid != MainForm.zeroGuid)
            {
                cmd.CommandText =
                    "select * from \n" +
                    "(select top 1 '' as VERSION \n" +
                    "from TEMPLATES \n" +
                    "union \n" +
                    "select DISTINCT VERSION \n" +
                    "from TEMPLATES \n"+
                    "where TEMPLATE_TYPE_GUID="+MainForm.GuidToStr(templateTypeGuid)+") as Q \n" +
                    "order by VERSION";
            }
            else
            {
                cmd.CommandText =
                    "select * from \n" +
                    "(select top 1 '' as VERSION \n" +
                    "from TEMPLATES \n" +
                    "union \n" +
                    "select DISTINCT VERSION \n" +
                    "from TEMPLATES) as Q \n" +
                    "order by VERSION";
            }

            OleDbDataReader versions = cmd.ExecuteReader();

            cbQVersion.Items.Clear();

            while (versions.Read())
                cbQVersion.Items.Add(versions["VERSION"].ToString());
            
            versions.Close();
        }

        private void SaveReport()
        {
            //Created on 12.10.2016 in version 2.0.15.1

            OleDbCommand cmd = new OleDbCommand("", connection);

            //string masterID = "Null";

            string typeCode = "";

            if ((cbQType.SelectedValue != null) && (cbQType.SelectedValue.ToString().Length > 0))
                typeCode = cbQType.SelectedValue.ToString();

            cmd.CommandText =
                "update REPORTS set " +
                "REPORT_CODE='" + StrToSQLStr(reportCode) + "', \n" +
                "INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + ", \n" +
                "INSPECTION_DATE=" + MainForm.DateTimeToQueryStr(dtInspectionDate.Value) + ", \n" +
                "VIQ_TYPE='" + StrToSQLStr(qType) + "', \n" +
                "VIQ_TYPE_CODE='" + StrToSQLStr(typeCode) + "', \n" +
                "VIQ_VERSION='" + StrToSQLStr(qVersion) + "', \n" +
                "VESSEL_GUID="+MainForm.GuidToStr(vesselGuid)+", \n"+
                "INSPECTION_PORT='" + StrToSQLStr(inspectionPort) + "', \n" +
                "COMPANY='" + StrToSQLStr(inspectionCompany) + "', \n" +
                "INSPECTION_TYPE_ID=" + inspectionTypeID.ToString() + ", \n" +
                "MEMORANDUM_ID=" + memorandumID.ToString() + ", \n" +
                "OBS_COUNT="+GetObsCount().ToString()+", \n"+
                "TEMPLATE_GUID=" + MainForm.FormatGuidString(_templateGUID) + ", \n" +
                "MANUAL="+MainForm.BoolToIntStr(chbManual.Checked)+", \n"+
                "FILE_AVAILABLE="+MainForm.BoolToIntStr(fileAvailable)+" \n"+
                "where REPORT_CODE='" + StrToSQLStr(_reportCode)+"'";
            MainForm.cmdExecute(cmd);

        }

        private string GetVesselIMO(Guid vesselGuid,string defaultValue="")
        {
            //Created on 12.10.2016 in version 2.0.15.1

            if (!DS.Tables.Contains("SUMMARY_VESSEL_LIST"))
                return "";

            DataRow[] rows = DS.Tables["SUMMARY_VESSEL_LIST"].Select("VESSEL_GUID=" + MainForm.GuidToStr(vesselGuid));

            if (rows.Length > 0)
            {
                return rows[0]["VESSEL_IMO"].ToString();
            }
            else
                return defaultValue;
        }

        private void FormReportSummary_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DS.Tables.Contains("SUMMARY_VESSEL_LIST"))
            {
                DS.Tables["SUMMARY_VESSEL_LIST"].Columns.Clear();
                DS.Tables["SUMMARY_VESSEL_LIST"].Dispose();
            }
        }

        private void FormReportSummary_Load(object sender, EventArgs e)
        {
            // Upgrade?
            if (Properties.Settings.Default.FormReportSummarySize.Width == 0) Properties.Settings.Default.Upgrade(); 
            
            if (Properties.Settings.Default.FormReportSummarySize.Width == 0 || Properties.Settings.Default.FormReportSummarySize.Height == 0)
            {
                // first start
                // optional: add default values
            }
            else
            {
                this.WindowState = Properties.Settings.Default.FormReportSummaryState;

                // we don't want a minimized window at startup
                if (this.WindowState == FormWindowState.Minimized) this.WindowState = FormWindowState.Normal;

                this.Location = Properties.Settings.Default.FormReportSummaryLocation;
                this.Size = Properties.Settings.Default.FormReportSummarySize;
            }
        }

        private void cbQType_SelectedIndexChanged(object sender, EventArgs e)
        {
            TemplateChanged();
        }

        private string GetTemplateGUID(string version, string title)
        {
            if (version.Length == 0)
                return "";

            if (title.Length == 0)
                return "";

            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select top 1 CStr(TEMPLATE_GUID) \n" +
                "from TEMPLATES \n" +
                "where VERSION='" + MainForm.StrToSQLStr(version) + "' \n" +
                "and TITLE='" + StrToSQLStr(title) + "'";

            string id = (string)cmd.ExecuteScalar();

            if ((id == null) || (id.Length == 0))
                return "";
            else
                return id;
        }

        private void TemplateChanged()
        {
            _templateGUID = GetTemplateGUID(cbQVersion.Text, cbQType.Text);

            if (_templateGUID.Length>0)
            {
                //Check questions GUID
                OleDbCommand cmd = new OleDbCommand("", connection);
                cmd.CommandText =
                    "select QUESTION_NUMBER, QUESTION_GUID, SEQUENCE \n" +
                    "from TEMPLATE_QUESTIONS \n" +
                    "where TEMPLATE_GUID='" + _templateGUID + "' \n" +
                    "order by SEQUENCE";

                if (DS.Tables.Contains("TEMPLATE_QUESTIONS"))
                    DS.Tables["TEMPLATE_QUESTIONS"].Clear();

                OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);

                adpt.Fill(DS, "TEMPLATE_QUESTIONS");

                DataTable templateQuestion = DS.Tables["TEMPLATE_QUESTIONS"];

                adpt.SelectCommand.CommandText =
                    "select REPORT_ITEM_ID, QUESTION_NUMBER, QUESTION_GUID, SEQUENCE \n" +
                    "from REPORT_ITEMS \n" +
                    "where REPORT_CODE='" + reportCode + "'";

                if (DS.Tables.Contains("_REPORT_QUESTIONS"))
                    DS.Tables["_REPORT_QUESTIONS"].Clear();

                adpt.Fill(DS, "_REPORT_QUESTIONS");

                DataRow[] qRows = DS.Tables["_REPORT_QUESTIONS"].Select();

                bool applyToAll=false;
                int result=0;
                int status = 0;

                for (int i=0; i<qRows.Length;i++)
                {
                    string curQuestionNumber = qRows[i]["QUESTION_NUMBER"].ToString();
                    string curQuestionGUID = qRows[i]["QUESTION_GUID"].ToString();
                    string curQuestionID = qRows[i]["REPORT_ITEM_ID"].ToString();
                    //string qn = qRows[i]["QN"].ToString();
                    string newQuestionGUID = "";
                    string newQuestionNumber = "";

                    DataRow[] rowsNumber = templateQuestion.Select("QUESTION_NUMBER='" + curQuestionNumber + "'");
                    DataRow[] rowsGUID = templateQuestion.Select("QUESTION_GUID='" + curQuestionGUID + "'");

                    if (rowsNumber.Length>0)
                    {
                        string tempQuestionGUID = rowsNumber[0]["QUESTION_GUID"].ToString();
                        string tempQuestionNumber = "";

                        if (rowsGUID.Length > 0)
                            tempQuestionNumber = rowsGUID[0]["QUESTION_NUMBER"].ToString();

                        if (tempQuestionGUID != curQuestionGUID)
                        {
                            if (!applyToAll)
                            {
                                if (isVisible) //Form  is visible
                                {
                                    string msgText = "Application will correct question GUID in accordance with selected questionnaire. Would you like to proceed?";
                                    string msgCaption = "Confirmation";
                                    string cbText = "Apply answer to the next questions";
                                    status = 1;

                                    FrmCustomDialog dlg = new FrmCustomDialog(this.Font, msgText, msgCaption, cbText);

                                    var rslt = dlg.ShowDialog();

                                    applyToAll = dlg.checkBoxState;

                                    if (rslt == DialogResult.Yes)
                                    {
                                        newQuestionGUID = tempQuestionGUID;
                                        newQuestionNumber = curQuestionNumber;
                                        result = 1;
                                    }
                                    else
                                        result = 0;
                                }
                                else //Form is hidden
                                {
                                    //Check current question GUID in report
                                    if (curQuestionGUID.Length == 0)
                                    {
                                        var res0 = MsgBoxExt.Show("Question number \"" + curQuestionNumber + "\" in report " + reportCode + " has not GUID. \n" +
                                            "Would you like to set GUID in accordance with selected questionnaire type?",
                                            "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MsgBoxExtDefaultButton.Button1,
                                            true, "Apply to all");

                                        applyToAll = res0.CheckBoxState;

                                        if (res0.Result == DialogResult.Yes)
                                        {
                                            newQuestionGUID = tempQuestionGUID;
                                            newQuestionNumber = curQuestionNumber;
                                            result = 1;
                                            status = 1;
                                        }
                                        else
                                        {
                                            result = 0;
                                        }
                                    }
                                    else
                                    {
                                        if (curQuestionNumber != tempQuestionNumber)
                                        {
                                            //Question number in report is differ from question number in template

                                            var res1 = MsgBoxExt.Show("Record in the report \"" + reportCode + "\" for question number " + curQuestionNumber +
                                                " has GUID assigned to question number " + tempQuestionNumber + " in questionnaire.\n" +
                                                "Would you like to change question number to the correct one in accordance with question GUID?",
                                                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MsgBoxExtDefaultButton.Button1,
                                                true, "Apply to all");

                                            if (res1.Result == DialogResult.Yes)
                                            {
                                                //Assign correct number
                                                newQuestionNumber = tempQuestionNumber;
                                                newQuestionGUID = curQuestionGUID;
                                                result = 1;
                                                status = 2;
                                                applyToAll = res1.CheckBoxState;
                                            }
                                            else
                                            {
                                                //Ask to change GUID
                                                var res2 = MsgBoxExt.Show("Would you like to set correct question GUID in accordance with question " + curQuestionNumber + " number?",
                                                    "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MsgBoxExtDefaultButton.Button1,
                                                    true, "Apply to all");

                                                if (res2.Result == DialogResult.Yes)
                                                {
                                                    newQuestionGUID = tempQuestionGUID;
                                                    newQuestionNumber = curQuestionNumber;
                                                    result = 1;
                                                    status = 3;
                                                    applyToAll = res2.CheckBoxState;
                                                }
                                                else
                                                {
                                                    result = 0;
                                                    applyToAll = res2.CheckBoxState;
                                                }
                                            }

                                        }
                                        else
                                        {
                                            //Question number in report is equal to question number in template
                                            //
                                            FrmWrongQuestionnaire form = new FrmWrongQuestionnaire(connection, DS, this.Font);
                                            form.questionGUID = tempQuestionGUID;
                                            form.qTitle = cbQType.Text;
                                            form.qVersion = cbQVersion.Text;
                                            form.qType = cbQType.SelectedValue.ToString();

                                            var rst = form.ShowDialog();

                                            if (rst != DialogResult.OK)
                                            {
                                                result = 0;
                                                applyToAll = true;
                                            }
                                        }

                                    }
                                }
                            }
                            else
                            {
                                switch (status)
                                {
                                    case 1:
                                        newQuestionGUID = tempQuestionGUID;
                                        newQuestionNumber = curQuestionNumber;
                                        break;
                                    case 2:
                                        newQuestionNumber = tempQuestionNumber;
                                        newQuestionGUID = curQuestionGUID;
                                        break;
                                    case 3:
                                        newQuestionGUID = tempQuestionGUID;
                                        newQuestionNumber = curQuestionNumber;
                                        break;
                                }
                            }

                            if (result==1)
                            {
                                cmd.CommandText =
                                    "update REPORT_ITEMS set \n" +
                                    "QUESTION_GUID='{" + newQuestionGUID + "}', \n" +
                                    "QUESTION_NUMBER='"+newQuestionNumber+"', \n"+
                                    "TEMPLATE_GUID="+MainForm.FormatGuidString(_templateGUID)+" \n"+
                                    "where REPORT_ITEM_ID=" + curQuestionID;
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }

                //Check for double numbers
                cmd.CommandText =
                    "select \n" +
                    "QUESTION_NUMBER \n" +
                    "from \n" +
                    "( \n" +
                    "select \n" +
                    "REPORT_CODE, \n" +
                    "QUESTION_NUMBER, \n" +
                    "QUESTION_GUID, \n" +
                    "Count(QUESTION_NUMBER) as RECS \n" +
                    "from \n" +
                    "REPORT_ITEMS \n" +
                    "where  \n" +
                    "REPORT_CODE='" + reportCode + "' \n" +
                    "group by REPORT_CODE,QUESTION_NUMBER,QUESTION_GUID \n" +
                    ") \n" +
                    "where RECS>1";

                OleDbDataReader rd = cmd.ExecuteReader();

                doubleNumbers.Clear();

                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        doubleNumbers.Add(rd["QUESTION_NUMBER"].ToString());
                    }
                }

                rd.Close();

                if ((!isVisible) && doubleNumbers.Count>0)
                {
                    string nums = "";

                    for (int i=0;i<doubleNumbers.Count;i++)
                    {
                        if (nums.Length == 0)
                            nums = doubleNumbers[i];
                        else
                            nums = nums + "\n" + doubleNumbers[i];
                    }

                    MessageBox.Show("The following number(s) has double records: \n\n" + nums, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }

            if (isVisible)
                updateQuestions();
        }

        private void miRemoveCR_Click(object sender, EventArgs e)
        {
            

        }

        private void RemoveCR(TextBox tb, ref string storeString)
        {
            string s = tb.Text;
            string bak = storeString;
            storeString = s;

            if (tb.SelectionLength > 0)
            {
                int start = tb.SelectionStart;
                int length = tb.SelectionLength;

                string s1 = "";
                string s2 = tb.SelectedText;

                if (start > 0)
                    s1 = s.Substring(0, start);

                string s3 = s.Substring(start + length);

                s2 = s2.Replace("\r\n", " ");
                s2 = s2.Replace("\r", " ");
                s2 = s2.Replace("\n", " ");

                s = s1 + s2 + s3;
            }
            else
            {
                s = s.Replace("\r\n", " ");
                s = s.Replace("\r", " ");
                s = s.Replace("\n", " ");
            }

            s = s.Replace("  ", " ");

            if (s.CompareTo(tb.Text) != 0)
            {
                tb.Text = s;
                SetSaveActive();
            }
            else
                storeString = s;
        }

        private void tbObservation_KeyPress(object sender, KeyPressEventArgs e)
        {
            storeObservation = "";
            btnUndoObs.Enabled = true;
        }

        private void btnObsRemoveCR_Click(object sender, EventArgs e)
        {
            RemoveCR(tbObservation, ref storeObservation);

            if (storeObservation.Length > 0)
                btnUndoObs.Enabled = true;
            else
                btnUndoObs.Enabled = false;
        }

        private void btnUndoObs_Click(object sender, EventArgs e)
        {
            if (storeObservation.Length > 0)
            {
                tbObservation.Text = storeObservation;
                storeObservation = "";
                btnUndoObs.Enabled = false;
            }
            else
            {
                if (tbObservation.CanUndo)
                {
                    tbObservation.Undo();
                    btnUndoObs.Enabled = tbObservation.CanUndo;
                }
            }
        }

        private void btnRemoveCRInspCom_Click(object sender, EventArgs e)
        {
            RemoveCR(tbInspectorComments, ref storeInspectorComments);

            if (storeInspectorComments.Length > 0)
                btnUndoInsCom.Enabled = true;
            else
                btnUndoInsCom.Enabled = false;
        }

        private void btnRemoveCROpComm_Click(object sender, EventArgs e)
        {
            RemoveCR(tbOperatorComments, ref storeOperatorComments);

            if (storeOperatorComments.Length > 0)
                btnUndoOpCom.Enabled = true;
            else
                btnUndoOpCom.Enabled = false;
        }

        private void btnUndoInsCom_Click(object sender, EventArgs e)
        {
            if (storeInspectorComments.Length > 0)
            {
                tbInspectorComments.Text = storeInspectorComments;
                storeInspectorComments = "";
                btnUndoInsCom.Enabled = false;
            }
            else
            {
                if (tbInspectorComments.CanUndo)
                {
                    tbInspectorComments.Undo();
                    btnUndoInsCom.Enabled = tbInspectorComments.CanUndo;
                }
            }

        }

        private void btnUndoOpCom_Click(object sender, EventArgs e)
        {
            if (storeOperatorComments.Length > 0)
            {
                tbOperatorComments.Text = storeOperatorComments;
                storeOperatorComments = "";
                btnUndoOpCom.Enabled = false;
            }
            else
            {
                if (tbOperatorComments.CanUndo)
                {
                    tbOperatorComments.Undo();
                    btnUndoOpCom.Enabled = tbOperatorComments.CanUndo;
                }
            }
        }

        private void tbInspectorComments_KeyPress(object sender, KeyPressEventArgs e)
        {
            storeInspectorComments = "";
            btnUndoInsCom.Enabled = true;
        }

        private void tbOperatorComments_KeyPress(object sender, KeyPressEventArgs e)
        {
            storeOperatorComments = "";
            btnUndoOpCom.Enabled = true;
        }

        private void GetQuestionnaireDetails(string QuestionGUID, string QuestionnaireType,ref string TemplateVersion)
        {
            string _type = QuestionnaireType.Substring(QuestionnaireType.Length);


        }

        private void dgvQuestions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvQuestions.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && 
                doubleNumbers.Contains(dgvQuestions.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
            {
                dgvQuestions.Rows[e.RowIndex].Cells[e.ColumnIndex].Style = new DataGridViewCellStyle { ForeColor = Color.Red};
            }
            else
            {
                dgvQuestions.Rows[e.RowIndex].Cells[e.ColumnIndex].Style = dgvQuestions.DefaultCellStyle;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //Select another question

            if (dgvQuestions.Rows.Count == 0)
                return;

            if (_reportCode.Length == 0)
            {
                MessageBox.Show(
                    "Please provide report code",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (cbQType.Text.Length == 0)
            {
                MessageBox.Show(
                    "Please select questionnaire type",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (cbQVersion.Text.Length == 0)
            {
                MessageBox.Show(
                    "Please select questionnaire version",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            FrmSelectQuestion form = new FrmSelectQuestion(GetTemplateGUID());

            var rslt = form.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                string questionGUID = form.selectedQuestionGUID;
                string questionNumber = form.selectedQuestionNumber;
                string questionSequence = form.sequence;

                //Update question record
                OleDbCommand cmd = new OleDbCommand("", connection);

                cmd.CommandText =
                    "update REPORT_ITEMS set \n" +
                    "QUESTION_NUMBER='" + MainForm.StrToSQLStr(questionNumber) + "', \n" +
                    "QUESTION_GUID=" + MainForm.FormatGuidString(questionGUID) + ", \n" +
                    "SEQUENCE='" + MainForm.StrToSQLStr(questionSequence) + "', \n" +
                    "TEMPLATE_GUID=" + MainForm.FormatGuidString(_templateGUID) + " \n" +
                    "where REPORT_ITEM_ID=" + dgvQuestions.CurrentRow.Cells["REPORT_ITEM_ID"].Value.ToString();
                MainForm.cmdExecute(cmd);

                if (isVisible)
                    updateQuestions();

                MainForm.LocateGridRecord(questionNumber, "QUESTION_NUMBER", -1, dgvQuestions);

                UpdateControlValues();

                SetSaveInactive();
            }

        }

        private string JustGUID(string anyGUID)
        {
            string s = anyGUID.Replace("{", "");
            s = s.Replace("}", "");
            
            return s;
        }

        private void cbMemorandumType_TextChanged(object sender, EventArgs e)
        {
            if (cbMemorandumType.SelectedValue==System.DBNull.Value || cbMemorandumType.SelectedValue==null)
            {
                _memorandumID = 0;
                _memorandumType = cbMemorandumType.Text;
            }
            else
            {
                try
                {
                    _memorandumID = (int)cbMemorandumType.SelectedValue;
                    _memorandumType = cbMemorandumType.Text;
                }
                catch
                {
                    _memorandumID = 0;
                    _memorandumType = cbMemorandumType.Text;
                }
            }

        }

        private void cbInspectionType_TextChanged(object sender, EventArgs e)
        {
            if (!inspectionTypeFilled)
                return;

            cbQVersion.Text = "";

            FillVersions();

            if (cbInspectionType.SelectedValue==System.DBNull.Value || cbInspectionType.SelectedValue==null)
            {
                _inspectionTypeID = 0;
                _inspectionType = cbInspectionType.Text;
            }
            else
            {
                _inspectionTypeID = (int) cbInspectionType.SelectedValue;
                _inspectionType = cbInspectionType.Text;
            }

            if (inspectionTypeID == 2)
            {
                cbMemorandumType.Left = cbCompany.Left;
                cbMemorandumType.Top = cbCompany.Top;
                cbMemorandumType.Width = cbCompany.Width;

                cbMemorandumType.Visible = true;
                cbCompany.Visible = false;
                label6.Text = "Memorandum";
            }
            else
            {
                cbMemorandumType.Visible = false;
                cbCompany.Visible = true;
                label6.Text = "Inspected by";
            }

        }

        private void cbCompany_TextChanged(object sender, EventArgs e)
        {
            _company = cbCompany.Text.Trim();
        }

        private void chbManual_Click(object sender, EventArgs e)
        {
        }

        private void cbVesselName_TextChanged(object sender, EventArgs e)
        {
            if (cbVesselName.SelectedValue == null || cbVesselName.SelectedValue == System.DBNull.Value)
                vesselGuid = MainForm.zeroGuid;
            else
            {
                if (cbVesselName.SelectedIndex == 0)
                    vesselGuid = MainForm.zeroGuid;
                else
                    vesselGuid = MainForm.StrToGuid(cbVesselName.SelectedValue.ToString());
            }

            FillCrew();
        }


        private int GetObsCount()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select Count(REPORT_ITEM_ID) as RecCount \n" +
                "from REPORT_ITEMS \n" +
                "where \n" +
                "REPORT_CODE='" + reportCode + "'";

            int count = (int)cmd.ExecuteScalar();

            return count;
        }

        private bool ReportFileExists()
        {
            string reportFile = workFolder + "\\Reports\\" + tbReportCode.Text + ".pdf";

            return File.Exists(reportFile);
        }

        private void btnCrewEdit_Click(object sender, EventArgs e)
        {
            EditCrewmember();
        }

        private void EditCrewmember()
        {
            if (dtInspectionDate.Value == DateTimePicker.MinimumDateTime)
            {
                MessageBox.Show("Please provide date of inspection", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FormSelectCrewmember form = new FormSelectCrewmember();

            form.positionGuid = MainForm.StrToGuid(dgvCrew.CurrentRow.Cells["CREW_POSITION_GUID"].Value.ToString());
            form.position = dgvCrew.CurrentRow.Cells["POSITION_NAME"].Value.ToString();
            form.crewGuid = MainForm.StrToGuid(dgvCrew.CurrentRow.Cells["CREW_GUID"].Value.ToString());
            form.vesselName = cbVesselName.Text;
            form.vesselGuid = vesselGuid;
            form.dateInspected = dtInspectionDate.Value;
            form.reportCode = _reportCode;

            var rslt = form.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                if (form.wasChanged)
                {
                    int curRow = dgvCrew.CurrentCell.RowIndex;
                    int curCol = dgvCrew.CurrentCell.ColumnIndex;

                    FillCrew();

                    dgvCrew.CurrentCell = dgvCrew[curCol, curRow];
                }
            }
        }

        private void btnShowCrew_Click(object sender, EventArgs e)
        {
            FrmCrewOnBoard form = new FrmCrewOnBoard();

            form.queryFilter = "VESSEL_NAME='" + cbVesselName.Text + "' " +
                "and DATE_ON<=" + MainForm.DateTimeToQueryStr(dtInspectionDate.Value) + " and " +
                "(IsNull(DATE_OFF) or DATE_OFF>" + MainForm.DateTimeToQueryStr(dtInspectionDate.Value)+")";

            form.querySort = "order by CREW_POSITIONS.POSITION_INDEX";

            form.formCaption = " on " + dtInspectionDate.Value.ToShortDateString();

            form.ShowDialog();
        }

        private Guid GetCrewGuid(string crewName, Guid positionGuid)
        {
            if (crewName.Trim().Length == 0)
                return MainForm.zeroGuid;

            if (positionGuid == MainForm.zeroGuid)
                return MainForm.zeroGuid;

            OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

            cmd.CommandText =
                "select Count(CREW_GUID) as ReCount \n" +
                "from CREW \n" +
                "where \n" +
                "CREW_NAME='" + MainForm.StrToSQLStr(crewName) + "' \n" +
                "and CREW_POSITION_GUID=" + MainForm.GuidToStr(positionGuid);

            int recCount = (int)cmd.ExecuteScalar();

            switch (recCount)
            {
                case 0:
                    return SaveNewCrew(crewName, positionGuid);
                case 1:
                    cmd.CommandText =
                        "select CREW_GUID \n" +
                        "from CREW \n" +
                        "where \n" +
                        "CREW_NAME='" + MainForm.StrToSQLStr(crewName) + "' \n" +
                        "and CREW_POSITION_GUID=" + MainForm.GuidToStr(positionGuid);

                    return (Guid)cmd.ExecuteScalar();
                default:
                    FormSelectCrewmemberFromList form = new FormSelectCrewmemberFromList();
                    form.crewmemberName = crewName;
                    form.positionGuid = positionGuid;

                    if (form.ShowDialog() == DialogResult.OK)
                        return form.crewGuid;
                    else
                        return MainForm.zeroGuid;
            }
        }

        private void btnLocateCrew_Click(object sender, EventArgs e)
        {

            if (dtInspectionDate.Value < DateTime.Parse("2000-01-01"))
            {
                MessageBox.Show("Please provide date of inspection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Guid vesselGuid = MainForm.zeroGuid;

            if (cbVesselName.SelectedValue != System.DBNull.Value && cbVesselName.SelectedValue != null)
            {
                vesselGuid = MainForm.StrToGuid(cbVesselName.SelectedValue.ToString());
            }
            else
            {
                MessageBox.Show("Please select vessel name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string crewName = "";

            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select * \n" +
                "from CREW_POSITIONS \n" +
                "order by POSITION_INDEX";

            if (MainForm.DS.Tables.Contains("CREW_POSITIONS"))
                MainForm.DS.Tables["CREW_POSITIONS"].Clear();

            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            adapter.Fill(MainForm.DS, "CREW_POSITIONS");

            DataRow[] rows = MainForm.DS.Tables["CREW_POSITIONS"].Select();

            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    string crewPosition = row["POSITION_NAME"].ToString();
                    Guid positionGuid = MainForm.StrToGuid(row["CREW_POSITION_GUID"].ToString());

                    cmd.CommandText =
                        "select TOP 1 CREW_NAME, DATE_ON, DATE_OFF \n" +
                        "from CREW_ON_BOARD \n" +
                        "where \n" +
                        "VESSEL_GUID=" + MainForm.GuidToStr(vesselGuid) + "\n" +
                        "and CREW_POSITION='"+MainForm.StrToSQLStr(crewPosition)+"' \n" +
                        "and DATE_ON<=" + MainForm.DateTimeToQueryStr(dtInspectionDate.Value) + "\n" +
                        "and (IsNull(DATE_OFF) or DATE_OFF>=" + MainForm.DateTimeToQueryStr(dtInspectionDate.Value) + ") \n" +
                        "order by DATE_ON";

                    OleDbDataReader reader = cmd.ExecuteReader();


                    if (reader.HasRows)
                    {
                        reader.Read();

                        crewName =reader["CREW_NAME"].ToString();

                        Guid crewGuid = GetCrewGuid(crewName, positionGuid);

                        if (crewGuid != MainForm.zeroGuid)
                        {
                            OleDbCommand cmd1 = new OleDbCommand("", MainForm.connection);

                            cmd1.CommandText =
                                "select Count(REPORT_CODE) as RecCount \n" +
                                "from REPORTS_CREW \n" +
                                "where \n" +
                                "REPORT_CODE='" + MainForm.StrToSQLStr(tbReportCode.Text) + "' \n" +
                                "and CREW_POSITION_GUID=" + MainForm.GuidToStr(positionGuid);

                            int recCount = (int)cmd1.ExecuteScalar();

                            if (recCount == 0)
                            {
                                cmd1.CommandText =
                                    "insert into REPORTS_CREW (REPORT_CODE, CREW_GUID, CREW_POSITION_GUID) \n" +
                                    "values ('" + MainForm.StrToSQLStr(tbReportCode.Text) + "'," +
                                    MainForm.GuidToStr(crewGuid) + "," +
                                    MainForm.GuidToStr(positionGuid) + ")";

                                if (MainForm.cmdExecute(cmd1) < 0)
                                {
                                    MessageBox.Show("Failed to insert new record for the crew during inspection",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                cmd1.CommandText =
                                    "update REPORTS_CREW set \n" +
                                    "CREW_GUID=" + MainForm.GuidToStr(crewGuid) + "\n" +
                                    "where \n" +
                                    "REPORT_CODE='" + MainForm.StrToSQLStr(tbReportCode.Text) + "' \n" +
                                    "and CREW_POSITION_GUID=" + MainForm.GuidToStr(positionGuid);

                                if (MainForm.cmdExecute(cmd1) < 0)
                                {
                                    MessageBox.Show("Failed to update record for the crew during inspection",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    else
                    {
                        //MessageBox.Show("There is no suitable record in the list of crew on board", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    reader.Close();
                }

                FillCrew();
            }
        }

        private void dgvCrew_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0 || e.ColumnIndex==1)
            {
                if (!Convert.ToBoolean(dgvCrew.Rows[e.RowIndex].Cells[4].Value))
                {
                    if (dgvCrew.Rows[e.RowIndex].Cells[1].Value != null &&
                        dgvCrew.Rows[e.RowIndex].Cells[1].Value.ToString().Length>0)
                        dgvCrew.Rows[e.RowIndex].Cells[e.ColumnIndex].Style =
                            new DataGridViewCellStyle { ForeColor = Color.Red };
                }
            }
        }

        private void dgvCrew_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
                EditCrewmember();
        }
    }
}
