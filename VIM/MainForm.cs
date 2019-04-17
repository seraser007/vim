using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using ADOX;
using System.Collections;
using System.Xml;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using System.Security.Principal;
using JRO;
using System.Threading;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Security;
using System.Security.AccessControl;
using System.Diagnostics;

namespace VIM
{
    //Version 2.0.8 - add new field HAS_PROFILE in INSPECTORS table

    public struct FilterStringItem
    {
        public string condition;
        public string value;
        public string label;
        public bool blocked;
        public bool show;
        public string typeCode;

        public FilterStringItem(string conditionString, string valueString, string labelString, bool showValue, bool blockedValue, string typeCodeString="")
        {
            condition=conditionString;
            value = valueString;
            label = labelString;
            blocked = blockedValue;
            show = showValue;
            typeCode = typeCodeString;
        }
    }

    public struct FilterDateItem
    {
        public string condition;
        public DateTime value;
        public DateTime value2;
        public string label;
        public bool blocked;
        public bool show;

        public FilterDateItem(string conditionString, DateTime valueDate, DateTime value2Date, string labelString, bool showValue, bool blockedValue)
        {
            condition = conditionString;
            value = valueDate;
            value2 = value2Date;
            blocked = blockedValue;
            label=labelString;
            show = showValue;
        }
    }

    public struct DefaultFilterItem
    {
        public string Name;
        public bool Show;
        public string Condition;
        public object Value;
        public object Value2;

        public DefaultFilterItem (string itemName, bool itemShow, string itemCondition, object itemValue = null, object itemValue2 = null)
        {
            Name = itemName;
            Show = itemShow;
            Condition = itemCondition;
            Value = itemValue;
            Value2 = itemValue2;
        }
    }


    public partial class MainForm : Form
    {
        //string dbName = "";
        string dbPath = "";
        string dbFullName = "";
        public static string appName = "";
        public static string appType = "";
        public static string iniPersonalFile = "";
        public static string iniCommonFile = "";

        public static string workFolder;
        public static OleDbConnection connection;

        public static bool anyUserMayChangeFont = false;
        public static bool oneFontForPowerUsers = true;

        string connectionString;
        string reportCode = "";
        public static DataSet DS = new DataSet();
        OleDbDataAdapter reportsAdapter = new OleDbDataAdapter();
        OleDbDataAdapter questionAdapter = new OleDbDataAdapter();
        OleDbDataAdapter viqAdapter ;
        OleDbCommand selectReports = new OleDbCommand();
        OleDbCommand selectQuestions = new OleDbCommand();
        string reportFilter = "1=1";
        string templateTypeCode = "";
        Guid templateGUID;
        public Guid instanceGuid = Guid.NewGuid();
        bool askForOverwrite = true;
        DialogResult defaultAnswer = DialogResult.No;
        int fileCount = 0;
        string reportVersion = "";

        private int highestPercentageReached = 0;

        string loadTemplateType = "";

        public static Guid zeroGuid = new Guid();

        //Questions Filter variables
        FilterStringItem qfVessel = new FilterStringItem("", "", "Vessel name", false, false);
        FilterStringItem qfInspector = new FilterStringItem("", "", "Inspector", false, false);
        FilterStringItem qfReportNumber = new FilterStringItem("", "", "Report code", false, false);
        FilterStringItem qfQuestionNumber = new FilterStringItem("", "", "Question number", false, false);
        FilterStringItem qfObservation = new FilterStringItem("", "", "Observation", true, false);
        FilterStringItem qfInspectorComments = new FilterStringItem("", "", "Inspector comments", false, false);
        FilterStringItem qfTechnicalComments = new FilterStringItem("", "", "Technical comments", false, false);
        FilterStringItem qfOperatorComments = new FilterStringItem("", "", "Operator commants", false, false);
        FilterStringItem qfQuestionGUID = new FilterStringItem("", "", "Question GUID", false, false);
        FilterDateItem qfDate = new FilterDateItem("", DateTime.Today, DateTime.Today, "Date of inspection", false, false);
        FilterStringItem qfCompany = new FilterStringItem("", "", "Company", false, false);
        FilterStringItem qfPort = new FilterStringItem("", "", "Port of inspection", false, false);
        FilterStringItem qfOffice = new FilterStringItem("", "", "Office", false, false);
        FilterStringItem qfDOC = new FilterStringItem("", "", "DOC", false, false);
        FilterStringItem qfHullClass = new FilterStringItem("", "", "Hull Class", false, false);
        FilterStringItem qfMaster = new FilterStringItem("", "", "Master", false, false);
        FilterStringItem qfChiefEngineer = new FilterStringItem("", "", "Chief Engineer", false, false);
        FilterStringItem qfSubchapter = new FilterStringItem("", "", "Subchapter", false, false);
        FilterStringItem qfKeyWords = new FilterStringItem("", "", "Key words", false, false);
        FilterStringItem qfQuestionnaireType = new FilterStringItem("", "", "Questionnaire type", false, false);
        FilterStringItem qfFleet = new FilterStringItem("", "", "Fleet", false, false);

        bool questionsMapping = true;
        //string qType = "";

        //Reports filter variables
        FilterStringItem rfVessel = new FilterStringItem("", "", "Vessel name", true, false);
        FilterStringItem rfInspector = new FilterStringItem("", "", "Inspector", true, false);
        FilterStringItem rfReportNumber = new FilterStringItem("", "", "Report code", true, false);
        FilterDateItem rfDate = new FilterDateItem("", DateTime.Today, DateTime.Today, "Date of inspection", true, false);
        FilterStringItem rfCompany = new FilterStringItem("", "", "Company", true, false);
        FilterStringItem rfPort = new FilterStringItem("", "", "Port of inspection", true, false);
        FilterStringItem rfOffice = new FilterStringItem("", "", "Office", true, false);
        FilterStringItem rfDOC = new FilterStringItem("", "", "DOC", true, false);
        FilterStringItem rfHullClass = new FilterStringItem("", "", "Hull Class", true, false);
        FilterStringItem rfMaster = new FilterStringItem("", "", "Master", true, false);
        FilterStringItem rfChiefEngineer = new FilterStringItem("", "", "Chief Engineer", true, false);
        FilterStringItem rfInspectionType = new FilterStringItem("Equal", "All", "Inspection type", true, false);
        FilterStringItem rfMemorandum = new FilterStringItem("", "", "Memorandum", true, false);

        //Number of observations filter variables
        FilterStringItem norVessel = new FilterStringItem("", "", "Vessel", true, false);
        FilterStringItem norInspector = new FilterStringItem("", "", "Inspector", true, false);
        FilterStringItem norObservationCause = new FilterStringItem("", "", "Observation cause", true, false);
        FilterDateItem norDate = new FilterDateItem("", DateTime.Today, DateTime.Today, "Date of inspection", true, false);
        FilterStringItem norCompany = new FilterStringItem("", "", "Company", true, false);
        FilterStringItem norOffice = new FilterStringItem("", "", "Office", true, false);
        FilterStringItem norDOC = new FilterStringItem("", "", "DOC", true, false);

        //Statistic filter variables
        FilterDateItem statDate = new FilterDateItem("", DateTime.Today, DateTime.Today, "Date of inspection", true, false);
        FilterStringItem statChapter = new FilterStringItem("", "", "Chapter number", true, false);
        FilterStringItem statType = new FilterStringItem("", "", "Questionnaire type", true, false);

        FilterDateItem statRDate = new FilterDateItem("", DateTime.Today, DateTime.Today, "Date of inspection", true, false);
        FilterStringItem statRChapter = new FilterStringItem("", "", "Chapter number", true, false);
        FilterStringItem statRType = new FilterStringItem("", "", "Questionnaire type", true, false);
        FilterStringItem statOffice = new FilterStringItem("", "", "Office", true, false);
        FilterStringItem statDOC = new FilterStringItem("", "", "DOC", true, false);

        bool reportFilterOn = false;

        bool updateGridOnFilterChange = true;

        bool sinchronizeFilter = false;

        string altTemplate = "";

        bool viqStatisticUseMapping = true;
        bool roviqStatisticUseMapping = true;
        bool viqGroupByType = false;
        bool roviqGroupByType = false;

        public static string betaString = "";
        public static WindowsIdentity user;
        public static List<string> MAPIClients=new List<string>();

        BindingSource bsDetails = new BindingSource();

        public static DefaultFilterItem defVessel = new DefaultFilterItem("VESSEL", false, "");
        public static DefaultFilterItem defInspector = new DefaultFilterItem("INSPECTOR", false, "");
        public static DefaultFilterItem defReportNumber = new DefaultFilterItem("REPORT NUMBER", false, "");
        public static DefaultFilterItem defQuestionNumber = new DefaultFilterItem("QUESTION_NUMBER", false, "");
        public static DefaultFilterItem defObservation = new DefaultFilterItem("OBSERVATION", true, "");
        public static DefaultFilterItem defInspectorComments = new DefaultFilterItem("INSPECTOR COMMENTS", false, "");
        public static DefaultFilterItem defTechnicalComments = new DefaultFilterItem("TECHNICAL COMMENTS", false, "");
        public static DefaultFilterItem defOperatorComments = new DefaultFilterItem("OPERATOR COMMENTS", false, "");
        public static DefaultFilterItem defQuestionGUID = new DefaultFilterItem("QUESTION GUID", false, "");
        public static DefaultFilterItem defDateOfInspection = new DefaultFilterItem("DATE OF INSPECTION", false, "");
        public static DefaultFilterItem defCompany = new DefaultFilterItem("COMPANY", false, "");
        public static DefaultFilterItem defPort = new DefaultFilterItem("PORT", false, "");
        public static DefaultFilterItem defOffice = new DefaultFilterItem("OFFICE", false, "");
        public static DefaultFilterItem defDOC = new DefaultFilterItem("DOC", false, "");
        public static DefaultFilterItem defHullClass = new DefaultFilterItem("HULL CLASS", false, "");
        public static DefaultFilterItem defMaster = new DefaultFilterItem("MASTER", false, "");
        public static DefaultFilterItem defChiefEngineer = new DefaultFilterItem("CHIEF ENGINEER", false, "");
        public static DefaultFilterItem defSubchapter = new DefaultFilterItem("SUBCHAPTER", false, "");
        public static DefaultFilterItem defKeyWords = new DefaultFilterItem("KEY WORDS", false, "");
        public static DefaultFilterItem defUseMapping = new DefaultFilterItem("USE MAPPING", true, "");
        public static DefaultFilterItem defUpdateOnClose = new DefaultFilterItem("UPDATE ON CLOSE", false, "");
        public static DefaultFilterItem defUseDefault = new DefaultFilterItem("USE DEFAULT", false, "");
        public static DefaultFilterItem defQuestionnaireType = new DefaultFilterItem("TEMPLATE TYPE", false, "");
        public static DefaultFilterItem defFleet = new DefaultFilterItem("FLEET", false, "");

        public bool useLastSetting = true;
        public bool filterUsed = false;
        public static string userName = "";
        public static string appTempFolderName = "";
        public DirectoryInfo appTempFolder;
        public static string userTempFolder = "";
        public static string shortAppTempFolderName = "";
        public static string programID = "";
        public string tempDBFileName = "";
        public DateTime loginTime = DateTime.Now;
        public static bool isPowerUser = false;
        //public static bool isPowerUserNew = false;
        //private bool WasPowerUser = false;

        private string licenseOwner = "";
        private DateTime licenseExpire = DateTime.Today;
        private string licenseString = "";

        public static string appRegKey = "SOFTWARE\\NBK Software\\VIQ Manager";
        public static string serviceFolder = "";

        public static string templatesType = "";

        public static string appDataFolder = "";

        public static Font mainFont;
        public static Icon mainIcon;

        private string dbFolder = "";

        private int inspectionType = 0;

        public MainForm()
        {
            InitializeComponent();

            GetMAPIClients();

            workFolder = Directory.GetCurrentDirectory();
            serviceFolder = workFolder + "\\Service";

            user = WindowsIdentity.GetCurrent();

            userName = Path.GetFileName(user.Name);

            iniPersonalFile = Path.Combine(MainForm.serviceFolder, "Settings_" + userName + ".ini");
            iniCommonFile = Path.Combine(MainForm.serviceFolder, "Settings_Common.ini");

            //Create program ID
            programID = Guid.NewGuid().ToString();

            appName = ReadIniValue(iniCommonFile, "General", "AppName", "Vessel Inspections Manager");

            SplashForm Splash = new SplashForm(appName);

            Splash.Show();
            Splash.BringToFront();
            Splash.Update();

            if (betaString.Length == 0)
            {
                licenseString = LoadLicense();

                if (licenseString.Trim().Length == 0)
                {
                    MessageBox.Show("File with license was not found. Application terminated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }

                LicenseCheck lCheck = new LicenseCheck("");

                lCheck.ReadLicense(licenseString, ref licenseOwner, ref licenseExpire);

                if (licenseOwner.Length == 0)
                {
                    MessageBox.Show("You have not valid license to use this application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }

                if (licenseExpire.CompareTo(DateTime.Today) <= 0)
                {
                    MessageBox.Show("Your license already expired", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }
            }

            //Define temporary folder name
            userTempFolder = GetUserTempPath();

            if (userTempFolder.Length == 0)
                userTempFolder = Path.GetTempPath();

            if (!CheckAccess(userTempFolder))
            {
                MessageBox.Show("You have not access to the folder \"" + userTempFolder + "\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            shortAppTempFolderName = "VIM_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            appTempFolderName = Path.Combine(userTempFolder, shortAppTempFolderName);

            //Check folder exists
            if (!Directory.Exists(appTempFolderName))
            {
                //Folder was not found. Create folder
                try
                {
                    appTempFolder = Directory.CreateDirectory(appTempFolderName);
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message, "Create folder error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //Check whether folder created
                if (appTempFolder == null)
                {
                    //Folder was not created
                    MessageBox.Show("Unable to create temporary folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }
            }
            else
            {
                appTempFolder = new DirectoryInfo(appTempFolderName);
            }


            dbFolder = ReadIniValue(iniCommonFile, "General", "DBFolder");

            //Try to found database
            locateDatabase();

            if (dbFullName.Length == 0)
            {
                //Database was not found
                MessageBox.Show("Application is unable to run without database. \n" +
                    "Application terminated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //this.Close();
                ClearTempFolder(userTempFolder);
                Environment.Exit(0);
            }

            //Try to open connection
            if (!createConnection())
            {
                //Unable to open connection
                MessageBox.Show("Unable to connect to selected database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //this.Close();
                ClearTempFolder(userTempFolder);
                Environment.Exit(0);
            }

            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel5.Visible = false;
            toolStripStatusLabel6.Visible = false;

            setAppTitle();

            CheckDatabase();
            checkTemplates();

            appType = GetApplicationType();

            isPowerUser = IsPowerUser();

            ClearTempFolder(userTempFolder, shortAppTempFolderName);

            this.Icon = VIM.Properties.Resources.Tanker;

            if (isPowerUser == true)
            {
                toolStripStatusLabel8.Text = "Power";

                menuPowerUsers.Visible = true;

                tbtnUpdateDB.Visible = true;

                //this.Icon = VIM.Properties.Resources.SIRE_16_Power_v4;

                /*
                if (appType.Length == 0)
                {
                    if (appName.StartsWith("OVIQ"))
                    {
                        this.Icon = VIM.Properties.Resources.OVID_16_Power_v4;
                        SetApplicationType("OVIQ");
                    }
                    else
                    {
                        this.Icon = VIM.Properties.Resources.SIRE_16_Power_v4;
                        SetApplicationType("SIRE");
                    }

                    appType = GetApplicationType();
                }
                else
                {
                    switch (appType)
                    {
                        case "SIRE":
                            this.Icon = VIM.Properties.Resources.SIRE_16_Power_v4;
                            break;
                        case "OVIQ":
                            this.Icon = VIM.Properties.Resources.OVID_16_Power_v4;
                            break;
                    }
                }

                */
            }
            else
            {
                toolStripStatusLabel8.Text = "User";

                menuPowerUsers.Visible = false;

                tbtnUpdateDB.Visible = false;

                //this.Icon = VIM.Properties.Resources.SIRE_16x16_blue;

                /*
                if (appType.Length == 0)
                {
                    if (appName.StartsWith("OVIQ"))
                    {
                        this.Icon = VIM.Properties.Resources.OVIQ_16x16_blue;
                        SetApplicationType("OVIQ");
                    }
                    else
                    {
                        this.Icon = VIM.Properties.Resources.SIRE_16x16_blue;
                        SetApplicationType("SIRE");
                    }

                    appType = GetApplicationType();
                }
                else
                {
                    switch (appType)
                    {
                        case "OVIQ":
                            this.Icon = VIM.Properties.Resources.OVIQ_16x16_blue;
                            break;
                        case "SIRE":
                            this.Icon = VIM.Properties.Resources.SIRE_16x16_blue;
                            break;
                    }
                }

                */
            }

            mainIcon = this.Icon;

            string optionValue = "";

            optionValue = GetOptionValue(110);

            if (optionValue == null || optionValue.Length == 0)
            {
                SaveOptionValue(110, anyUserMayChangeFont.ToString());
            }
            else
                anyUserMayChangeFont = Convert.ToBoolean(optionValue);

            optionValue = GetOptionValue(111);

            if (optionValue == null || optionValue.Length == 0)
            {
                SaveOptionValue(110, oneFontForPowerUsers.ToString());
            }
            else
                oneFontForPowerUsers = Convert.ToBoolean(GetOptionValue(111));

            loadFonts();
            getShowReports();
            getShowVIQ();

            //showReports();
            //showVIQ();

            selectReports.Connection = connection;
            reportsAdapter.SelectCommand = selectReports;

            fillInspectionTypeCombo();

            string insType = ReadIniValue(iniPersonalFile, "USER_OPTIONS", "INSPECTION_TYPE", "All");

            if (GetInspectionTypeID(insType) > 0)
            {
                tscbInspectionType.Text = insType;
            }
            else
                tscbInspectionType.Text = "All";
                
            dgvInspections.DataSource = DS;
            dgvInspections.AutoGenerateColumns=true;
            dgvInspections.DataMember="REPORTS";

            dgvInspections.Columns["VIQ_TYPE"].HeaderText = "Questionnaire type";
            dgvInspections.Columns["VIQ_VERSION"].HeaderText = "Questionnaire version";
            dgvInspections.Columns["VIQ_TYPE_CODE"].HeaderText = "Questionnaire Code";
            dgvInspections.Columns["MANUAL"].HeaderText = "Manual";

            dgvInspections.Columns["VESSEL_IMO"].Visible = false;

            dgvInspections.Columns["REPORT_CODE"].HeaderText = "Report";
            dgvInspections.Columns["VESSEL_NAME"].HeaderText = "Vessel";
            dgvInspections.Columns["OBS_COUNT"].HeaderText = "Observations";
            dgvInspections.Columns["OBS_COUNT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvInspections.Columns["INSPECTOR_GUID"].Visible = false;
            
            dgvInspections.Columns["INSPECTOR_NAME"].HeaderText = "Inspector";
            dgvInspections.Columns["INSPECTION_DATE"].HeaderText = "Date";
            dgvInspections.Columns["INSPECTION_PORT"].HeaderText = "Port";
            dgvInspections.Columns["INSPECTION_TYPE"].HeaderText = "Inspection type";

            dgvInspections.Columns["INSPECTION_TYPE_ID"].Visible = false;
            dgvInspections.Columns["MEMORANDUM_ID"].Visible = false;

            dgvInspections.Columns["COMPANY"].HeaderText = "Company / Memorandum";
            dgvInspections.Columns["OFFICE"].HeaderText = "Office";
            dgvInspections.Columns["DOC"].HeaderText = "DOC";
            dgvInspections.Columns["HULL_CLASS"].HeaderText = "Hull Class";
            
            dgvInspections.Columns["MASTER_GUID"].Visible = false;
            dgvInspections.Columns["MASTER_NAME"].HeaderText = "Master";
            dgvInspections.Columns["CHENG_GUID"].Visible = false;
            dgvInspections.Columns["CHENG_NAME"].HeaderText = "Ch.Engineer";
            dgvInspections.Columns["FILE_AVAILABLE"].HeaderText = "PDF File";
            dgvInspections.Columns["TEMPLATE_GUID"].HeaderText = "Template GUID";
            dgvInspections.Columns["VESSEL_GUID"].Visible = false;


            selectQuestions.Connection = connection;
            selectQuestions.CommandText =
                "select "+
                "VESSELS.VESSEL_NAME, "+
                "INSPECTORS.INSPECTOR_NAME, "+
                "REPORT_ITEMS.REPORT_CODE, \n" +
                "REPORT_ITEMS.QUESTION_NUMBER, " +
                "REPORT_ITEMS.QUESTION_GUID, " +
                "COMMENTS, \n" +
                "TECHNICAL_COMMENTS, "+
                "OBSERVATION, "+
                "OPERATOR_COMMENTS, "+
                "REPORTS.INSPECTION_DATE, \n"+
                "REPORTS.COMPANY, "+
                "REPORTS.INSPECTION_PORT, "+
                "VESSELS.OFFICE, "+
                "VESSELS.DOC, "+
                "VESSELS.HULL_CLASS, \n"+
                "MASTER_LIST.MASTER_GUID, "+
                "MASTER_LIST.MASTER_NAME, "+
                "CHENG_LIST.CHENG_GUID, "+
                "CHENG_LIST.CHENG_NAME, \n"+
                "QUESTION_KEYS.SUBCHAPTER, "+
                "QUESTION_KEYS.KEY_INDEX \n" +
                "from "+
                "(((((REPORT_ITEMS inner join REPORTS \n" +
                "on REPORTS.REPORT_CODE=REPORT_ITEMS.REPORT_CODE) \n" +
                "left join VESSELS \n"+
                "on REPORTS.VESSEL_GUID=VESSELS.VESSEL_GUID) \n"+
                "left join INSPECTORS \n"+
                "on REPORTS.INSPECTOR_GUID=INSPECTORS.INSPECTOR_GUID) \n"+
                "left join\n"+
                "( \n" +
                "select CREW.CREW_NAME as MASTER_NAME, CREW.CREW_GUID as MASTER_GUID, REPORT_CODE \n" +
                "from \n" +
                "REPORTS_CREW inner join CREW \n" +
                "on REPORTS_CREW.CREW_GUID = CREW.CREW_GUID \n" +
                "where \n" +
                "REPORTS_CREW.CREW_POSITION_GUID ={753D9AAC-1376-4FAE-AB00-144D81341F70} \n" +
                ") as MASTER_LIST \n" +
                "on REPORTS.REPORT_CODE = MASTER_LIST.REPORT_CODE) \n" +
                "left join \n" +
                "( \n" +
                "select CREW.CREW_NAME as CHENG_NAME, CREW.CREW_GUID as CHENG_GUID, REPORT_CODE \n" +
                "from \n" +
                "REPORTS_CREW inner join CREW \n" +
                "on REPORTS_CREW.CREW_GUID = CREW.CREW_GUID \n" +
                "where \n" +
                "REPORTS_CREW.CREW_POSITION_GUID ={012156E3-2990-468F-AFB4-DAD5401844F7} \n" +
                ") as CHENG_LIST \n" +
                "on REPORTS.REPORT_CODE = CHENG_LIST.REPORT_CODE)  \n" +
                "left join QUESTION_KEYS \n" +
                "on REPORT_ITEMS.QUESTION_GUID=QUESTION_KEYS.QUESTION_GUID \n" +
                "where REPORT_ITEMS.REPORT_CODE='0'";

            //FUTURE MODIFICATION:
            //ADD SEQUENCE field to sort by questions number correctly

            questionAdapter.SelectCommand = selectQuestions;
            questionAdapter.Fill(DS, "REPORT_ITEMS");

            dgvDetails.AutoGenerateColumns = true;

            bsDetails.DataSource = DS;
            bsDetails.DataMember = "REPORT_ITEMS";

            dgvDetails.DataSource = bsDetails;

            viqAdapter = new OleDbDataAdapter("select QUESTION_NUMBER, QUESTION_TEXT, '0' as SEQUENCE \n" +
                "from TEMPLATE_QUESTIONS \n" +
                "where QUESTION_NUMBER='0'", connection);
            viqAdapter.Fill(DS, "VIQ");


            updateGrid2();

            fillInspectors();
            fillVessels();
            fillReports();
            fillPortFilter();
            fillCompanyFilter();
            fillOfficeFilter();
            fillDOCFilter();
            fillHullClassFilter();
            loadFilterDefaultsIni();

            ProtectFields(isPowerUser);

            Splash.Close();
        }

        private int GetInspectionTypeID(string typeString)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select INSPECTION_TYPE_ID \n" +
                "from INSPECTION_TYPES \n" +
                "where \n" +
                "INSPECTION_TYPE='" + StrToSQLStr(typeString) + "'";

            object id = cmd.ExecuteScalar();

            if (id!=null)
            {
                return Convert.ToInt32(id);
            }

            return 0;
        }

        private void fillInspectionTypeCombo()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select INSPECTION_TYPE_ID, INSPECTION_TYPE \n" +
                "from INSPECTION_TYPES \n" +
                "order by INSPECTION_TYPE";

            OleDbDataReader reader = cmd.ExecuteReader();

            tscbInspectionType.Items.Clear();

            tscbInspectionType.Items.Add("All");

            if (reader.HasRows)
            {
                while (reader.Read())
                    tscbInspectionType.Items.Add(reader["INSPECTION_TYPE"].ToString());
            }

            reader.Close();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_SHOWME)
            {
                ShowMe();
            }
            base.WndProc(ref m);
        }

        private void ShowMe()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
            // get our current "TopMost" value (ours will always be false though)
            bool top = TopMost;
            // make our form jump to the top of everything
            TopMost = true;
            // set it back to whatever it was
            TopMost = top;
        }

        private void ProtectFields(bool status)
        {
            btnLoadReport.Enabled = status;
            btnNewReport.Enabled = status;
            btnDeleteReport.Enabled = status;

            if (status)
            {
                btnEditReport.Image = VIM.Properties.Resources.edit;
                btnEditReport.ToolTipText = "Edit selected report";
            }
            else
            {
                btnEditReport.Image = VIM.Properties.Resources.normal_view;
                btnEditReport.ToolTipText = "View selected report";
            }

            menuFileLoad.Enabled = status;
        }

        private void ClearTempFolder(string tempFolderName, string excludeFolders = "")
        {
            if (tempFolderName.Length==0)
                tempFolderName = Path.GetTempPath();

            DirectoryInfo dir = new DirectoryInfo(tempFolderName);
            
            foreach (var item in dir.GetDirectories())
            {
                string folderName = item.Name;

                if (folderName.StartsWith("VIM_"))
                {
                    if (excludeFolders.Length > 0 && !excludeFolders.Contains(folderName))
                    {
                        
                        if (isPowerUser)
                        {
                            string text = "Old temporary folder was found: \n\n" + folderName + "\n\n" +
                                "Would you like to delete it?";

                            var rslt = MessageBox.Show(text, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (rslt == DialogResult.Yes)
                            {
                                try
                                {
                                    Directory.Delete(Path.Combine(tempFolderName, folderName), true);
                                }
                                catch (Exception E)
                                {
                                    MessageBox.Show(E.Message);
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                Directory.Delete(Path.Combine(tempFolderName, folderName), true);
                            }
                            catch 
                            {
                                //Do nothing
                            }
                        }
                    }
                }

            }
        }

        private string LoadLicense()
        {
            //try to find license file
            string appFolder = Path.GetDirectoryName(Application.ExecutablePath);
            
            string[] licFiles = Directory.GetFiles(appFolder, "*.vmlic");

            if (licFiles.Length==0)
                return "";

            if (licFiles.Length>1)
            {
                LicenseListForm llForm = new LicenseListForm(this.Icon, this.Font, licFiles);

                var rslt = llForm.ShowDialog();

                if (rslt == DialogResult.OK)
                    return File.ReadAllText(llForm.selectedFile);
                else
                    return "";
            }

            return File.ReadAllText(licFiles[0]);
        }

        private void setAppTitle()
        {
            if (betaString.Length == 0)
                this.Text = appName + " - " + Application.ProductVersion.ToString()+" (owned by "+licenseOwner+" till "+licenseExpire.ToShortDateString()+")";
            else
                this.Text = appName + " - " + Application.ProductVersion.ToString() + " Beta " + betaString;
        }

        private void locateDatabase()
        {
            var files = new List<string>();
            var rslt = DialogResult.No;

            if (dbFolder.Length == 0)
            {
                dbPath = Path.GetDirectoryName(Application.ExecutablePath);
            }
            else
            {
                if (dbFolder.StartsWith("\\\\"))
                {
                    dbPath = dbFolder;
                }
                else
                {
                    if (dbFolder.StartsWith("\\"))
                    {
                        dbPath = Path.Combine(workFolder, dbFolder.Substring(1));
                    }
                    else
                    {
                        dbPath = dbFolder;
                    }
                }
            }

            var dir=new DirectoryInfo(dbPath);

            foreach (FileInfo file in dir.GetFiles("*.dat")) 
            {
                files.Add(file.FullName);
            }

            if (files.Count == 1)
                dbFullName = files[0];
            else
            {
                if (files.Count == 0)
                    rslt = MessageBox.Show("Application is unable to locate database in the working path. \n" +
                        "Would you like to locate it?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                else
                    rslt = MessageBox.Show("There are several database in the working path. \n" +
                        "Would you like to select database?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (rslt==DialogResult.Yes)
                {
                    openFileDialog1.Filter = "Data files (*.dat)|*.dat|All files (*.*)|*.*";
                    openFileDialog1.InitialDirectory = dbPath;
                    openFileDialog1.FileName = "";

                    rslt = openFileDialog1.ShowDialog();

                    if (rslt==DialogResult.OK)
                    {
                        dbFullName = openFileDialog1.FileName;
                    }
                }
            }
        }

        public static void AddDirectorySecurity(string folderName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(folderName);

            // Get a DirectorySecurity object that represents the 
            // current security settings.
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings. 
            dSecurity.AddAccessRule(new FileSystemAccessRule(Account,
                                                            Rights,
                                                            ControlType));

            // Set the new access settings.
            dInfo.SetAccessControl(dSecurity);

        }

        public static void RemoveDirectorySecurity(string FileName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(FileName);

            // Get a DirectorySecurity object that represents the 
            // current security settings.
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings. 
            dSecurity.RemoveAccessRule(new FileSystemAccessRule(Account,
                                                            Rights,
                                                            ControlType));

            // Set the new access settings.
            dInfo.SetAccessControl(dSecurity);

        }

        public bool CheckAccess(string folder)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(folder);
            WindowsIdentity wi = WindowsIdentity.GetCurrent();

            DirectorySecurity ds = dirInfo.GetAccessControl(AccessControlSections.Access);
            AuthorizationRuleCollection rules = ds.GetAccessRules(true, true, typeof(SecurityIdentifier));
            
            foreach (FileSystemAccessRule rl in rules)
            {
                SecurityIdentifier sid = (SecurityIdentifier)rl.IdentityReference;
                if (((rl.FileSystemRights & FileSystemRights.WriteData) == FileSystemRights.WriteData))
                {
                    if ((sid.IsAccountSid() && wi.User == sid) ||
                        (!sid.IsAccountSid() && wi.Groups.Contains(sid)))
                    {
                        if (rl.AccessControlType == AccessControlType.Allow)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }

            return false;
        }

        private Boolean createConnection()
        {
            bool dbExists = false;

            if (File.Exists(dbFullName))
            {
                //Database was found
                try
                {
                    
                    //Copy database to temporary folder
                    tempDBFileName = Path.Combine(appTempFolderName, Path.GetFileName(dbFullName));

                    if (File.Exists(tempDBFileName))
                    {
                        //File already exists
                        File.Delete(tempDBFileName);
                    }


                    try
                    {
                        //File.Copy(dbFullName, tempDBFileName, true);
                        ExecuteCommand("copy \"" + dbFullName + "\" \"" + tempDBFileName + "\" /Y");
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                    if (!File.Exists(tempDBFileName))
                    {
                        return false;
                    }

                    connection = new OleDbConnection();
                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                        "Data Source=" + tempDBFileName + ";" +
                        "Jet OLEDB:Engine Type=5";
                    connection.ConnectionString = connectionString;
                    connection.Open();

                    dbExists = true;
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message, "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
 
            return dbExists;

        }

        private string getAppName()
        {
            string s = ReadIniValue(iniCommonFile, "General", "AppName", "Vessel Inspections Manager");

            return s;
        }

        private void CheckDatabase()
        {
            workFolder = Directory.GetCurrentDirectory();
            bool dbExists = true;
            

            if (dbExists)
            {
                OleDbCommand cmd = new OleDbCommand("",connection);

                //Check field exists
                //Обновляем отметки о наличии отзыва
                cmd.CommandText =
                    "update INSPECTORS set HAS_PROFILE=False";
                cmdExecute(cmd);

                cmd.CommandText =
                    "update INSPECTORS set HAS_PROFILE=True \n" +
                    "where INSPECTOR_GUID in (select DISTINCT INSPECTOR_GUID from UNIFIED_COMMENTS)";
                cmdExecute(cmd);

                cmd.CommandText =
                    "delete from UNIFIED_COMMENTS \n" +
                    "where INSPECTOR_GUID is Null";
                cmdExecute(cmd);

                //Check for summary message table
                if (!tableExists("MESSAGE_SETTINGS"))
                {
                    cmd.CommandText =
                        "CREATE TABLE MESSAGE_SETTINGS \n" +
                        "(ID counter Primary Key, \n" +
                        "TAG integer, \n" +
                        "MESSAGE_SUBJECT varchar(255), \n" +
                        "MESSAGE_TEXT Memo)";
                    
                    if (cmdExecute(cmd)>=0)
                    {
                        //Initialize first record
                        string messageSubject = "Vetting Inspector %1 – Performance Report";
                        string messageText = 
                            "Dear Captain and Chief Engineer,\n\n\n"+
                            "PLEASE KEEP THIS INFORMATION STRICTLY CONFIDENTIAL AND FOR INTERNAL USE ONLY.\n\n\n"+
                            "Please find attached the profile report for the nominated inspector for the forthcoming Inspection, regarding which, please acknowledge safe receipt.\n\n"+
                            "Hope attached will be of assistance in preparation.\n\n"+
                            "Please do not hesitate to contact us should you require further assistance.\n\n"+
                            "Best Regards\n\n"+
                            "Vetting Team\n\n"+
                            "Email:  uni.vetting@scf-group.com\n";

                        cmd.CommandText =
                            "insert into MESSAGE_SETTINGS (TAG,MESSAGE_SUBJECT,MESSAGE_TEXT) \n" +
                            "values(1,'" + StrToSQLStr(messageSubject) + "','" + StrToSQLStr(messageText) + "')";
                        cmdExecute(cmd);
                    }


                }

                //Check for FILTER_ITEMS table
                if (!tableExists("FILTER_ITEMS"))
                {
                    //Create new table
                    cmd.CommandText =
                        "create table FILTER_ITEMS \n" +
                        "(ID counter Primary Key, \n" +
                        "ITEM_NAME varchar(255), \n" +
                        "ITEM_SHOW bit, \n" +
                        "ITEM_CONDITION varchar(255), \n" +
                        "USER_NAME varchar(255))";
                    cmdExecute(cmd);

                    createFilterItems();
                }
                else
                {
                    cmd.CommandText =
                        "select Count(ID) \n" +
                        "from FILTER_ITEMS \n" +
                        "where USER_NAME='" + StrToSQLStr(user.Name) + "'";

                    int xCount = (int) cmd.ExecuteScalar();

                    if (xCount == 0)
                        createFilterItems();
                    else
                        checkFilterItems();
                }

                
                cmd.CommandText =
                    "select Count(ID) as IDCount from USER_LOG \n" +
                    "where USER_NAME = '" + StrToSQLStr(userName) + "'";

                int recCount = (int)cmd.ExecuteScalar();

                if (recCount==0)
                {
                    cmd.CommandText =
                        "insert into USER_LOG (USER_NAME,USER_LOGIN) \n" +
                        "values('" + StrToSQLStr(userName) + "'," +
                                    DateTimeToQueryStr(loginTime) + ")";
                    cmdExecute(cmd);
                }
                else
                {
                    cmd.CommandText =
                        "update USER_LOG set \n" +
                        "USER_LOGIN=" + DateTimeToQueryStr(loginTime) + " \n" +
                        "where USER_NAME='" + StrToSQLStr(userName) + "'";
                    cmdExecute(cmd);
                }
                
                loadFonts();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //bgw.RunWorkerAsync();

            startLoadFileNew();

            //openFileDialog1.InitialDirectory = workFolder;
            /*
            askForOverwrite = true;

            openFileDialog1.FileName = "";

            var rslt = openFileDialog1.ShowDialog();
            fileCount = 0;

            this.Update();

            if (rslt == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;

                try
                {
                    foreach (String file in openFileDialog1.FileNames)
                    {
                        loadFile(file);
                    }

                    //if (openFileDialog1.FileNames.Length==1)
                    switch (fileCount)
                    {
                        case 0:
                            if (openFileDialog1.FileNames.Length==1)
                                System.Windows.Forms.MessageBox.Show("1 file of 1 was rejected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                System.Windows.Forms.MessageBox.Show(openFileDialog1.FileNames.Length.ToString()+" files of "+openFileDialog1.FileNames.Length.ToString()+" were rejected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case 1:
                            System.Windows.Forms.MessageBox.Show("1 file of " + openFileDialog1.FileNames.Length.ToString() + " was loaded successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        default:
                            System.Windows.Forms.MessageBox.Show(Convert.ToString(fileCount) + " files of " + openFileDialog1.FileNames.Length.ToString() + " were loaded successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;                        
                    }
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }

            }
            */
        }

        private bool startLoadFile(BackgroundWorker worker, DoWorkEventArgs e)
        {
            askForOverwrite = true;

            openFileDialog1.FileName = "";

            int fileNumber = 0;
            int fileLoaded = 0;

            highestPercentageReached = 0;

            var rslt = openFileDialog1.ShowDialog();
            
            fileCount = 0;

            this.Update();

            if (rslt == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;

                fileNumber = openFileDialog1.FileNames.Length;

                toolStripProgressBar1.Value = 0;
                toolStripProgressBar1.Visible = true;

                toolStripStatusLabel5.Text = "0 of " + fileNumber.ToString();
                toolStripStatusLabel5.Visible = true;

                toolStripStatusLabel6.Text = "";
                toolStripStatusLabel6.Visible = true;


                try
                {
                    foreach (String file in openFileDialog1.FileNames)
                    {
                        string fName = file.Substring(file.LastIndexOf("\\") + 1);
                        toolStripStatusLabel6.Text = fName;

                        loadFile(file);
                        
                        fileLoaded++;

                        toolStripStatusLabel5.Text = fileLoaded.ToString() + " of " + fileNumber.ToString();

                        // Report progress as a percentage of the total task.
                        int percentComplete = (int) (fileLoaded / fileNumber * 100);
                        if (percentComplete > highestPercentageReached)
                        {
                            highestPercentageReached = percentComplete;
                            worker.ReportProgress(percentComplete);
                        }
                    }

                    //if (openFileDialog1.FileNames.Length==1)
                    switch (fileCount)
                    {
                        case 0:
                            if (openFileDialog1.FileNames.Length == 1)
                                System.Windows.Forms.MessageBox.Show("1 file of 1 was rejected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                System.Windows.Forms.MessageBox.Show(openFileDialog1.FileNames.Length.ToString() + " files of " + openFileDialog1.FileNames.Length.ToString() + " were rejected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case 1:
                            System.Windows.Forms.MessageBox.Show("1 file of " + openFileDialog1.FileNames.Length.ToString() + " was loaded successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        default:
                            System.Windows.Forms.MessageBox.Show(Convert.ToString(fileCount) + " files of " + openFileDialog1.FileNames.Length.ToString() + " were loaded successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                    }
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }

            }

            return true;
        }


        private void startLoadFileNew()
        {
            askForOverwrite = true;
            openFileDialog1.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog1.FileName = "";

            int fileNumber = 0;
            int fileLoaded = 0;

            highestPercentageReached = 0;

            var rslt = openFileDialog1.ShowDialog();

            fileCount = 0;

            this.Update();

            if (rslt == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;

                fileNumber = openFileDialog1.FileNames.Length;

                toolStripProgressBar1.Value = 0;
                toolStripProgressBar1.Visible = true;

                toolStripStatusLabel5.Text = "0 of " + fileNumber.ToString();
                toolStripStatusLabel5.Visible = true;

                toolStripStatusLabel6.Text = "";
                toolStripStatusLabel6.Visible = true;

                this.Update();

                try
                {
                    foreach (String file in openFileDialog1.FileNames)
                    {
                        string fName = file.Substring(file.LastIndexOf("\\") + 1);
                        toolStripStatusLabel6.Text = fName;

                        this.Update();

                        loadFile(file);

                        fileLoaded++;

                        toolStripStatusLabel5.Text = fileLoaded.ToString() + " of " + fileNumber.ToString();

                        // Report progress as a percentage of the total task.
                        int percentComplete = (int)(((float)fileLoaded / (float)fileNumber) * 100);

                        if (percentComplete > highestPercentageReached)
                        {
                            toolStripProgressBar1.Value = percentComplete;
                            statusStrip1.Update();
                            highestPercentageReached = percentComplete;
                        }

                        this.Update();
                    }

                    //if (openFileDialog1.FileNames.Length==1)
                    switch (fileCount)
                    {
                        case 0:
                            if (openFileDialog1.FileNames.Length == 1)
                                MessageBox.Show("1 file of 1 was rejected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                MessageBox.Show(openFileDialog1.FileNames.Length.ToString() + " files of " + openFileDialog1.FileNames.Length.ToString() + " were rejected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case 1:
                            MessageBox.Show("1 file of " + openFileDialog1.FileNames.Length.ToString() + " was loaded successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        default:
                            MessageBox.Show(Convert.ToString(fileCount) + " files of " + openFileDialog1.FileNames.Length.ToString() + " were loaded successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                    }
                }
                finally
                {
                    RecalcObs();

                    this.Cursor = Cursors.Default;

                    toolStripProgressBar1.Visible = false;
                    toolStripStatusLabel5.Visible = false;
                    toolStripStatusLabel6.Visible = false;

                }

            }

        }

        private void loadFile(String fileName)
        {
            //Загружаем отчет из файла

            XmlReader inFile = XmlReader.Create(fileName);

            while (inFile.Read())
            {
                switch (inFile.NodeType)
                {
                    case XmlNodeType.Element:

                        if (inFile.Name == "SireDocumentResponse")
                        {
                            loadReport(inFile);
                        }
                        else
                        {
                            if (inFile.Name == "SireDocumentTemplate")
                                loadTemplate(inFile);
                            else
                            {
                                if (inFile.Name == "DocumentTemplate")
                                    loadTemplate(inFile);
                            }
                        }

                        break;
                }
            }

        }

        private void loadReport(XmlReader inFile)
        {
            this.Cursor = Cursors.WaitCursor;

            while (inFile.Read())
            {
                switch (inFile.NodeType)
                {
                    case XmlNodeType.Element:

                        if (inFile.Name == "Header")
                        {
                            if (!readHeader(inFile.ReadSubtree()))
                                return;
                        }
                        else
                            if (inFile.Name == "Document")
                            {
                                readDocument(inFile.ReadSubtree());
                            }
                        break;
                }
            }

            DS.Tables["REPORTS"].Clear();
            reportsAdapter.Fill(DS, "REPORTS");

            fillInspectors();
            fillVessels();
            fillReports();

            this.Cursor = Cursors.Default;
        }

        public void loadTemplate(XmlReader inFile)
        {
            while (inFile.Read())
            {
                switch (inFile.NodeType)
                {
                    case XmlNodeType.Element:

                        if (inFile.Name == "Header")
                        {
                            if (!readTemplateHeader(inFile.ReadSubtree()))
                                return;
                        }
                        else
                            if (inFile.Name == "Document")
                            {
                                readTemplateDocument(inFile.ReadSubtree());
                            }
                        break;
                }
            }

        }

        public bool readHeader(XmlReader header)
        {
            //Считываем заголовок
            string vesselName="";
            string vesselIMO = "";
            string inspectionDate="";
            string inspectionPort="";
            string inspectionCompany="";
            bool isCode = false;

            while (header.Read())
            {
                switch (header.NodeType)
                {
                    case XmlNodeType.Element:
                        if (header.Name == "DocumentKey")
                        {
                            isCode = true;
                        }
                        else
                        {
                            if (header.Name=="ContentAttributes")
                            {
                                XmlReader attributes=header.ReadSubtree();

                                    while (attributes.Read())
                                    {
                                        string value="";
                                        string key="";
                                        while (attributes.MoveToNextAttribute())
                                        {
                                            if (attributes.Name == "Value")
                                            {
                                                value = attributes.Value;
                                                switch (key)
                                                {
                                                    case "Vessel name":
                                                        vesselName = value;
                                                        break;
                                                    case "Vessel IMO":
                                                        vesselIMO = value;
                                                        break;
                                                    case "Inspection date":
                                                        inspectionDate = value;
                                                        break;
                                                    case "Inspection port":
                                                        inspectionPort = value;
                                                        break;
                                                    case "Inspecting company":
                                                        inspectionCompany = value;
                                                        break;
                                                }
                                            }
                                            else
                                            {
                                                key = attributes.Value;
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        case XmlNodeType.Text:
                            if (isCode)
                            {
                                reportCode = header.Value;
                                isCode = false;
                            }
                            break;
                    }
                

            }



            //Запизываем данные об отчете

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = connection;

            //Проверяем есть ли такой же отчет в базе данных
            cmd.CommandText = "select Count(REPORT_CODE) from REPORTS where REPORT_CODE='" + reportCode + "'";
            int recCount = (int)cmd.ExecuteScalar();
            
            ConfirmationForm frm4=new ConfirmationForm(this.Icon);
            frm4.label1.Text="There is already report with the code \""+reportCode+"\".\nWhould you like to overwrite it?";
            frm4.checkBox1.Checked=!askForOverwrite;
            var rslt=DialogResult.No;

            if (recCount > 0)
            {
                //Есть отчет с таким же номером

                if (askForOverwrite)
                {
                    //rslt = System.Windows.Forms.MessageBox.Show("There is a report with the same code. Whould you like to overwrite it?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    rslt = frm4.ShowDialog();
                    
                    this.Update();


                    if (frm4.checkBox1.Checked)
                    {
                        defaultAnswer = rslt;
                        askForOverwrite = false;
                    }

                    
                    if (rslt == DialogResult.Yes)
                    {
                        this.Cursor = Cursors.WaitCursor;

                        //Удаляем предыдущий отчет
                        //cmd.CommandText = "delete from REPORTS where REPORT_CODE='" + reportCode + "'";
                        //cmdExecute(cmd);

                        if (updateVessel(ref vesselIMO, ref vesselName, reportCode))
                        {
                            cmd.CommandText = "delete from REPORT_ITEMS where REPORT_CODE='" + reportCode + "'";
                            cmdExecute(cmd);

                            cmd.CommandText =
                                "update REPORTS set\n" +
                                "INSPECTION_DATE=DateSerial(" + inspectionDate.Substring(0, 4) + "," +
                                inspectionDate.Substring(5, 2) + "," + inspectionDate.Substring(8, 2) + "),\n" +
                                "INSPECTION_PORT='" + StrToSQLStr(inspectionPort) + "',\n" +
                                "VESSEL_GUID="+GuidToStr(GetVesselGuid(vesselName,vesselIMO))+", \n"+
                                "COMPANY='" + StrToSQLStr(inspectionCompany) + "'\n" +
                                "where REPORT_CODE='" + StrToSQLStr(reportCode) + "'";
                            cmdExecute(cmd);


                            fileCount++;

                            return true;
                        }
                        else
                            return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    //Обновляем без спроса

                    if (defaultAnswer == DialogResult.Yes)
                    {

                        if (updateVessel(ref vesselIMO, ref vesselName, reportCode))
                        {
                            //Удаляем предыдущий отчет
                            cmd.CommandText = "delete from REPORT_ITEMS where REPORT_CODE='" + reportCode + "'";
                            cmdExecute(cmd);

                            cmd.CommandText =
                                "update REPORTS set\n" +
                                "INSPECTION_DATE=DateSerial(" + inspectionDate.Substring(0, 4) + "," +
                                inspectionDate.Substring(5, 2) + "," + inspectionDate.Substring(8, 2) + "),\n" +
                                "INSPECTION_PORT='" + StrToSQLStr(inspectionPort) + "',\n" +
                                "VESSEL_GUID="+GuidToStr(GetVesselGuid(vesselName,vesselIMO))+", \n"+
                                "COMPANY='" + StrToSQLStr(inspectionCompany) + "'\n" +
                                "where REPORT_CODE='" + StrToSQLStr(reportCode) + "'";
                            cmdExecute(cmd);


                            fileCount++;

                            return true;
                        }
                        else
                            return false;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                //Такого отчета еще нет
                if (updateVessel(ref vesselIMO, ref vesselName, reportCode))
                {
                    cmd.CommandText =
                        "insert into REPORTS (REPORT_CODE, INSPECTION_DATE, INSPECTION_PORT, " +
                        "VESSEL, VESSEL_IMO, VESSEL_GUID, COMPANY, INSPECTION_TYPE_ID)\n" +
                        "values ('" + reportCode + "',DateSerial(" + inspectionDate.Substring(0, 4) + "," +
                        inspectionDate.Substring(5, 2) + "," + inspectionDate.Substring(8, 2) + "),'" +
                        StrToSQLStr(inspectionPort) + "','" + StrToSQLStr(vesselName) + "','" +
                        StrToSQLStr(vesselIMO) + "',"+GuidToStr(GetVesselGuid(vesselName,vesselIMO))+",'" + 
                        inspectionCompany + "',1)";
                    cmdExecute(cmd);

                    fileCount++;
                    
                    return true;
                }
                else
                    return false;
            }


        }

        public static Guid GetVesselGuid(string vesselName, string vesselIMO)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select VESSEL_GUID \n" +
                "from VESSELS \n" +
                "where \n" +
                "VESSEL_IMO='" + StrToSQLStr(vesselIMO) + "' \n" +
                "and UCASE(VESSEL_NAME)='" + StrToSQLStr(vesselName.ToUpper()) + "'";

            object id = cmd.ExecuteScalar();

            if (id != null)
                return MainForm.StrToGuid(id.ToString());
            else
                return zeroGuid;
        }

        public static string GetVesselNameForID(Guid vesselGuid)
        {
            if (vesselGuid == null)
                return "";

            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select VESSEL_NAME \n" +
                "from VESSELS \n" +
                "where \n" +
                "VESSEL_GUID=" + MainForm.GuidToStr(vesselGuid);

            object vesselName = cmd.ExecuteScalar();

            if (vesselName == null)
                return "";
            else
                return vesselName.ToString();
        }

        public static string GetVesselIMOForID(Guid vesselGuid)
        {
            if (vesselGuid == null)
                return "";

            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select VESSEL_IMO \n" +
                "from VESSELS \n" +
                "where \n" +
                "VESSEL_GUID=" + GuidToStr(vesselGuid);

            object vesselIMO = cmd.ExecuteScalar();

            if (vesselIMO == null)
                return "";
            else
                return vesselIMO.ToString();
        }

        public bool readTemplateHeader(XmlReader header)
        {

            //Считываем заголовок
            string title = "";
            string templateVersion = "";
            string templateGuidStr = "";

            altTemplate = "";

            while (header.Read())
            {
                if (header.NodeType==XmlNodeType.Element)
                {
                    switch (header.Name)
                    {
                        case "Header":
                            int aCount=header.AttributeCount;
                            if (aCount == 1)
                            {
                                header.MoveToFirstAttribute();
                                templateTypeCode = header.Value;
                            }
                            else
                            {
                                if (aCount > 1)
                                {
                                    header.MoveToAttribute(0);
                                    templateTypeCode = header.Value;
                                    header.MoveToAttribute(1);

                                    if (header.Name=="Header")
                                        altTemplate = header.Value;
                                }
                            }
                            break;
                        case "Title":
                            header.Read();
                            title=header.Value;
                            break;
                        case "TemplateVersion":
                            header.Read();
                            templateVersion = header.Value;
                            break;
                        case "DocumentVersion":
                            header.Read();
                            templateVersion = header.Value;
                            break;
                        case "TypeCode":
                            header.Read();
                            templateTypeCode = header.Value;
                            break;
                        case "Guid":
                            header.Read();
                            templateGuidStr = header.Value;
                            break;
                    }
                }
            }

            loadTemplateType = "";

            if (title.StartsWith("VIQ",StringComparison.OrdinalIgnoreCase))
            {
                if (altTemplate.Length == 0)
                    loadTemplateType = "VIQ";
                else
                    loadTemplateType = "RoVIQ";
            }
            else
            {
                if (title.StartsWith("OVIQ", StringComparison.OrdinalIgnoreCase))
                    loadTemplateType = "OVIQ";
                else
                {
                    if (title.StartsWith("OVID", StringComparison.OrdinalIgnoreCase))
                        loadTemplateType = "OVID";
                    else
                    {
                        if (title.StartsWith("CDI", StringComparison.OrdinalIgnoreCase))
                            loadTemplateType = "CDI";
                    }
                }
            }
            
            if (loadTemplateType.Length==0)
            {
                MessageBox.Show("Unable to determine template type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Запизываем данные о шаблоне

            string tableName = "";

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = connection;

            switch (loadTemplateType)
            {
                case "VIQ":
                    tableName="TEMPLATES";
                    break;
                case "RoVIQ":
                    tableName = "RTEMPLATES";
                    break;
                case "OVIQ":
                    tableName = "TEMPLATES";
                    break;
                case "OVID":
                    tableName = "TEMPLATES";
                    break;
                case "CDI":
                    tableName = "TEMPLATES";
                    break;
            }

            try
            {

                //Проверяем есть ли такая таблица
                if (!tableExists(tableName))
                {
                    //Создаем таблицу TEMPLATES
                    cmd.CommandText =
                        "create table "+tableName+ " (TEMPLATE_GUID GUID Primary key, TYPE_CODE varchar(10), " +
                        "TITLE varchar(255), VERSION varchar(20),TEMPLATE_TYPE varchar(20), "+
                        "TEMPLATE_TYPE_GUID GUID, PUBLISHED DateTime)";
                    cmdExecute(cmd);
                }

                //Проверяем есть ли такой же шпаблон в базе данных
                cmd.CommandText = "select Count(TYPE_CODE) from " + tableName + "\n" +
                    "where TYPE_CODE='" + templateTypeCode + "' and VERSION='" + templateVersion +
                    "' and TEMPLATE_TYPE='" + loadTemplateType + "'";

                int recCount = (int)cmd.ExecuteScalar();

                if (recCount > 0)
                {

                    cmd.CommandText =
                        "update "+tableName+" set\n" +
                        "TITLE='" + StrToSQLStr(title) + "'\n" +
                        "where TYPE_CODE='" + templateTypeCode + "' and VERSION='" + templateVersion +
                        "' and TEMPLATE_TYPE='" + loadTemplateType + "'";
                    cmdExecute(cmd);

                    cmd.CommandText = "select TEMPLATE_GUID from "+tableName+"\n" +
                        "where TYPE_CODE='" + templateTypeCode + "' and VERSION='" + templateVersion +
                        "' and TEMPLATE_TYPE='" + loadTemplateType + "'";

                    templateGUID = (Guid)cmd.ExecuteScalar();

                    fileCount++;

                    return true;
                }
                else
                {
                    Guid xGuid = StrToGuid(templateGuidStr);

                    if (xGuid == zeroGuid)
                        templateGUID = Guid.NewGuid();
                    else
                        templateGUID = xGuid;

                    Guid typeGuid = GetTeplateTypeGuid(loadTemplateType);

                    cmd.CommandText =
                        "insert into "+tableName+" (TEMPLATE_GUID, TYPE_CODE, TITLE, VERSION, "+
                        "TEMPLATE_TYPE, TEMPLATE_TYPE_GUID)\n" +
                        "values ({" + Convert.ToString(templateGUID) + "},'" + templateTypeCode + "','" +
                        StrToSQLStr(title) + "','" + StrToSQLStr(templateVersion) + "','"+
                        StrToSQLStr(loadTemplateType)+"',"+GuidToStr(typeGuid,true)+")";
                    cmdExecute(cmd);

                    fileCount++;

                    return true;
                }

            }
            catch (Exception E)
            {
                System.Windows.Forms.MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool readCDITemplateHeader(XmlReader header)
        {

            //Считываем заголовок
            string title = "";
            string templateVersion = "";

            altTemplate = "";

            while (header.Read())
            {
                if (header.NodeType == XmlNodeType.Element)
                {
                    switch (header.Name)
                    {
                        case "Header":
                            int aCount = header.AttributeCount;
                            if (aCount == 1)
                            {
                                header.MoveToFirstAttribute();
                                templateTypeCode = header.Value;
                            }
                            else
                            {
                                if (aCount > 1)
                                {
                                    header.MoveToAttribute(0);
                                    templateTypeCode = header.Value;
                                    header.MoveToAttribute(1);

                                    if (header.Name == "Header")
                                        altTemplate = header.Value;
                                }
                            }
                            break;
                        case "Title":
                            header.Read();
                            title = header.Value;
                            break;
                        case "DocumentVersion":
                            header.Read();
                            templateVersion = header.Value;
                            break;
                    }
                }
            }

            loadTemplateType = "";

            if (title.StartsWith("CDI", StringComparison.OrdinalIgnoreCase))
            {
                if (altTemplate.Length == 0)
                    loadTemplateType = "CDI";
            }

            if (loadTemplateType.Length == 0)
            {
                MessageBox.Show("Unable to determine template type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Запизываем данные о шаблоне

            string tableName = "";

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = connection;

            switch (loadTemplateType)
            {
                case "VIQ":
                    tableName = "TEMPLATES";
                    break;
                case "RoVIQ":
                    tableName = "RTEMPLATES";
                    break;
                case "OVIQ":
                    tableName = "TEMPLATES";
                    break;
                case "OVID":
                    tableName = "TEMPLATES";
                    break;
            }

            try
            {

                //Проверяем есть ли такая таблица
                if (!tableExists(tableName))
                {
                    //Создаем таблицу TEMPLATES
                    cmd.CommandText =
                        "create table " + tableName + " (ID counter Primary key, TEMPLATE_GUID GUID, TYPE_CODE varchar(10), " +
                        "TITLE varchar(255), VERSION varchar(20),TEMPLATE_TYPE varchar(20))";
                    cmdExecute(cmd);
                }

                //Проверяем есть ли такой же шпаблон в базе данных
                cmd.CommandText = "select Count(TYPE_CODE) from " + tableName + "\n" +
                    "where TYPE_CODE='" + templateTypeCode + "' and VERSION='" + templateVersion +
                    "' and TEMPLATE_TYPE='" + loadTemplateType + "'";

                int recCount = (int)cmd.ExecuteScalar();

                if (recCount > 0)
                {

                    cmd.CommandText =
                        "update " + tableName + " set\n" +
                        "TITLE='" + StrToSQLStr(title) + "'\n" +
                        "where TYPE_CODE='" + templateTypeCode + "' and VERSION='" + templateVersion +
                        "' and TEMPLATE_TYPE='" + loadTemplateType + "'";
                    cmdExecute(cmd);

                    cmd.CommandText = "select TEMPLATE_GUID from " + tableName + "\n" +
                        "where TYPE_CODE='" + templateTypeCode + "' and VERSION='" + templateVersion +
                        "' and TEMPLATE_TYPE='" + loadTemplateType + "'";

                    templateGUID = (Guid)cmd.ExecuteScalar();

                    fileCount++;

                    return true;
                }
                else
                {
                    templateGUID = Guid.NewGuid();

                    cmd.CommandText =
                        "insert into " + tableName + " (TEMPLATE_GUID, TYPE_CODE, TITLE, VERSION, TEMPLATE_TYPE)\n" +
                        "values ({" + Convert.ToString(templateGUID) + "},'" + templateTypeCode + "','" +
                        StrToSQLStr(title) + "','" + StrToSQLStr(templateVersion) + "','" + StrToSQLStr(loadTemplateType) + "')";
                    cmdExecute(cmd);

                    fileCount++;

                    return true;
                }

            }
            catch (Exception E)
            {
                System.Windows.Forms.MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void readDocument(XmlReader document)
        {
            string questionNum="";
            string inspectorComments = "";
            string inspectorObservation = "";
            string questionGUID = "";
            string operatorComments="";
            string responseString = "";
            string viqVersion = "";
            string viqType = "";
            string viqTypeCode = "";


            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = connection;

            reportVersion = "";

            cmd.CommandText =
                "select QUESTION_GUID, TITLE, TYPE_CODE, VERSION \n" +
                "from \n" +
                "TEMPLATE_QUESTIONS inner join TEMPLATES \n" +
                "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID \n" +
                "order by VERSION DESC, TYPE_CODE";

            OleDbDataAdapter qvAdapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("QUESTION_VERSION"))
                DS.Tables["QUESTION_VERSION"].Clear();

            qvAdapter.Fill(DS, "QUESTION_VERSION");

            while (document.Read())
            {
                switch (document.NodeType)
                {
                    case XmlNodeType.Element:
                        if (document.Name == "Question")
                        {
                            XmlReader question = document.ReadSubtree();

                            //Считываем атрибут
                            while (document.MoveToNextAttribute())
                            {
                                if (document.Name == "questionNum")
                                    questionNum = document.Value;
                            }

                            inspectorObservation = "";
                            inspectorComments = "";
                            operatorComments = "";

                            while (question.Read())
                            {
                                switch (question.NodeType)
                                {
                                    case XmlNodeType.Element:
                                        if (question.Name == "InspectorComments")
                                        {
                                            question.Read();
                                            inspectorComments = question.Value;
                                        }
                                        else
                                        {
                                            if (question.Name == "InspectorObservations")
                                            {
                                                question.Read();
                                                inspectorObservation = question.Value;
                                            }
                                            else
                                            {
                                                if (question.Name=="OperatorCommentsInitial")
                                                {
                                                    question.Read();
                                                    operatorComments=question.Value;
                                                }
                                                else
                                                if (question.Name == "ElementKey")
                                                {
                                                    question.Read();
                                                    questionGUID = question.Value;
                                                }
                                                else
                                                    if (question.Name == "ResponseDataYesNoNotSeen")
                                                    {
                                                        question.Read();
                                                        responseString = question.Value;
                                                    }
                                            }
                                        }
                                        break;
                                }
                            }


                            //Записываем вопрос в базу данных
                            if ((inspectorComments.Length > 0) || (inspectorObservation.Length > 0))
                            {
                                if (responseString.StartsWith("NS",StringComparison.OrdinalIgnoreCase))
                                cmd.CommandText =
                                    "insert into REPORT_ITEMS (REPORT_CODE, QUESTION_NUMBER, COMMENTS, COMMENTS_LEN, " +
                                    "TECHNICAL_COMMENTS, TECHNICAL_COMMENTS_LEN, OPERATOR_COMMENTS, " +
                                    "OPERATOR_COMMENTS_LEN, QUESTION_GUID)\n" +
                                    "values ('" + StrToSQLStr(reportCode) + "', '" + 
                                        StrToSQLStr(questionNum) + "', '" + 
                                        StrToSQLStr(inspectorComments) +"', " +
                                        inspectorComments.Length.ToString()+", '"+
                                        StrToSQLStr(inspectorObservation) + "', "+
                                        inspectorObservation.Length.ToString()+", '"+
                                        StrToSQLStr(operatorComments)+"', "+
                                        operatorComments.Length.ToString()+", "+
                                        FormatGuidString(questionGUID)+")";
                                else
                                cmd.CommandText =
                                    "insert into REPORT_ITEMS (REPORT_CODE, QUESTION_NUMBER, COMMENTS, COMMENTS_LEN, " +
                                    "OBSERVATION, OBSERVATION_LEN, OPERATOR_COMMENTS, OPERATOR_COMMENTS_LEN, " +
                                    "QUESTION_GUID)\n" +
                                    "values ('" + 
                                        StrToSQLStr(reportCode) + "', '" + 
                                        StrToSQLStr(questionNum) + "', '" + 
                                        StrToSQLStr(inspectorComments) +"', " + 
                                        inspectorComments.Length.ToString()+", '"+
                                        StrToSQLStr(inspectorObservation) + "', "+
                                        inspectorObservation.Length.ToString()+", '"+
                                        StrToSQLStr(operatorComments)+"', "+
                                        operatorComments.Length.ToString()+", "+
                                        FormatGuidString(questionGUID)+")";
                                
                                
                                cmdExecute(cmd);
                            }

                            //Получаем тип отчета
                            if ((viqType.Length == 0) || (viqType.Length == 0))
                            {
                                GetReportType(questionGUID, ref viqType, ref viqTypeCode);
                            }
                            
                            //Получаем версию вопроса

                            viqVersion=GetQuestionVersionNew(questionGUID);

                            //Сравниваем полученную версию с текущей
                            if (reportVersion.Length == 0)
                            {
                                reportVersion = viqVersion;
                            }
                            else
                            {
                                //Если версия вопроса меньше, чем текущая версия отчета, 
                                //то устанавливаем версию отчета равной версии вопроса
                                if (String.Compare(reportVersion, viqVersion) > 0)
                                    reportVersion = viqVersion;
                            }
                        }
                        break;
                }
            }

            string xTemplateGUID = GetTemplateGUID(reportVersion, viqType);

            if (xTemplateGUID.Length == 0)
                xTemplateGUID = "Null";
            else
                xTemplateGUID = "'" + xTemplateGUID + "'";

            //Обновляем версию отчета
            cmd.CommandText =
                "update REPORT_ITEMS set \n" +
                "TEMPLATE_GUID=" + FormatGuidString(xTemplateGUID) + "\n" +
                "where \n" +
                "REPORT_CODE='" + StrToSQLStr(reportCode) + "'";
            cmdExecute(cmd);

            cmd.CommandText =
                "update REPORTS set \n" +
                "VIQ_VERSION='" + StrToSQLStr(reportVersion) + "', \n" +
                "VIQ_TYPE='" + StrToSQLStr(viqType) + "', \n" +
                "VIQ_TYPE_CODE='" + StrToSQLStr(viqTypeCode) + "', \n" +
                "TEMPLATE_GUID="+xTemplateGUID+" \n"+
                "where REPORT_CODE='" + StrToSQLStr(reportCode) + "'";
            cmdExecute(cmd);

            if (DS.Tables.Contains("QUESTION_VERSION"))
                DS.Tables["QUESTION_VERSION"].Dispose();

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
        public static string StrToSQLStr(string Text)
        {
            string s = "";

            if (Text != null)
                s = Text.Replace("'", "''");

            return s;
        }

        private void fillInspectors()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select * from \n" +
                "(select INSPECTOR_GUID, INSPECTOR_NAME, Notes, Photo, Unfavourable, Background \n" +
                "from INSPECTORS \n" +
                "union \n" +
                "select " + MainForm.GuidToStr(MainForm.zeroGuid) + " as INSPECTOR_GUID, '' as INSPECTOR_NAME, '' as Notes, '' as Photo, " +
                "false as Unfavourable, '' as Background \n" +
                "from Fonts) \n" +
                "order by INSPECTOR_NAME";

            OleDbDataAdapter inspectorsAdapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("INSPECTORS_LIST"))
                DS.Tables["INSPECTORS_LIST"].Clear();

            inspectorsAdapter.Fill(DS, "INSPECTORS_LIST");

        }

        private void fillVessels()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText = 
                "select * from \n"+
                "(select VESSEL_GUID, VESSEL_IMO, VESSEL_NAME, DOC, HULL_CLASS \n"+
                "from VESSELS \n"+
                "union \n"+
                "select top 1 "+MainForm.GuidToStr(MainForm.zeroGuid)+" as VESSEL_GUID, '' as VESSEL_IMO, '' as VESSEL_NAME, '' as DOC, '' as HULL_CLASS \n"+
                "from VESSELS) \n"+
                "order by VESSEL_NAME";

            OleDbDataAdapter vesselsAdapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("VESSELS_LIST"))
                DS.Tables["VESSELS_LIST"].Clear();

            vesselsAdapter.Fill(DS, "VESSELS_LIST");

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //dataGridView2.Columns["REPORT_CODE"].Visible = checkBox1.Checked;
            updateQuestions();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //dataGridView2.Columns["QUESTION_NUMBER"].Visible = checkBox2.Checked;
            updateQuestions();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void updateQuestions()
        {
            //bool useVIQ=false;

            bool useTemplates = false;

            bool hideQNumber = true;

            bool useVessel = (qfVessel.condition.Length > 0 && qfVessel.value.Length > 0) ||
                (qfOffice.condition.Length > 0 && qfOffice.value.Length > 0) ||
                (qfDOC.condition.Length > 0 && qfDOC.value.Length > 0) ||
                (qfFleet.condition.Length > 0 && qfFleet.value.Length > 0) ||
                (qfHullClass.condition.Length > 0 && qfHullClass.value.Length > 0);

            this.Cursor = Cursors.WaitCursor;

            int recSelect = 0;

            if (toolStripTextBox1.Text.Length>0)
                recSelect=Convert.ToInt32(toolStripTextBox1.Text);

            try
            {
                //Check whether it is necessary to remove !!!!!

                string fields = "";

                if (qfVessel.show)
                    fields = "VESSELS.VESSEL_NAME";


                if (qfInspector.show)
                {
                    if (fields.Length == 0)
                        fields = "INSPECTORS.INSPECTOR_NAME";
                    else
                        fields = fields + ", INSPECTORS.INSPECTOR_NAME";
                }

                //if (qfReportNumber.show)
                {
                    if (fields.Length == 0)
                        fields = "REPORTITEMS.REPORT_CODE";
                    else
                        fields = fields + ", REPORTITEMS.REPORT_CODE";
                }

                if (qfQuestionNumber.show)
                {
                    if (fields.Length == 0)
                        fields = "REPORTITEMS.QUESTION_NUMBER, TEMPLATE_QUESTIONS.SEQUENCE";
                    else
                        fields = fields + ", REPORTITEMS.QUESTION_NUMBER, TEMPLATE_QUESTIONS.SEQUENCE";

                    hideQNumber = false;
                }
                else
                {
                    if (fields.Length == 0)
                        fields = "REPORTITEMS.QUESTION_NUMBER";
                    else
                        fields = fields + ", REPORTITEMS.QUESTION_NUMBER";

                    hideQNumber = true;
                }

                //if (qfQuestionGUID.show)
                {
                    if (fields.Length == 0)
                        fields = "REPORTITEMS.QUESTION_GUID";
                    else
                        fields = fields + ", REPORTITEMS.QUESTION_GUID";
                }

                if (qfInspectorComments.show)
                {
                    if (fields.Length == 0)
                        fields = "REPORTITEMS.COMMENTS";
                    else
                        fields = fields + ", REPORTITEMS.COMMENTS";
                }

                if (qfObservation.show)
                {
                    if (fields.Length == 0)
                        fields = "REPORTITEMS.OBSERVATION";
                    else
                        fields = fields + ", REPORTITEMS.OBSERVATION";
                }

                if (qfOperatorComments.show)
                {
                    if (fields.Length == 0)
                        fields = "REPORTITEMS.OPERATOR_COMMENTS";
                    else
                        fields = fields + ", REPORTITEMS.OPERATOR_COMMENTS";
                }

                if (qfTechnicalComments.show)
                {
                    if (fields.Length == 0)
                        fields = "REPORTITEMS.TECHNICAL_COMMENTS";
                    else
                        fields = fields + ", REPORTITEMS.TECHNICAL_COMMENTS";
                }

                if (qfFleet.show)
                {
                    if (fields.Length == 0)
                        fields = "VESSELS.FLEET";
                    else
                        fields = fields + ", VESSELS.FLEET";
                }

                if (qfOffice.show)
                {
                    if (fields.Length == 0)
                        fields = "VESSELS.OFFICE";
                    else
                        fields = fields + ", VESSELS.OFFICE";
                }

                if (qfDOC.show)
                {
                    if (fields.Length == 0)
                        fields = "VESSELS.DOC";
                    else
                        fields = fields + ", VESSELS.DOC";
                }

                //if (qfDate.show)
                {
                    if (fields.Length == 0)
                        fields = "REPORTS.INSPECTION_DATE";
                    else
                        fields = fields + ", REPORTS.INSPECTION_DATE";
                }

                if (qfCompany.show)
                {
                    if (fields.Length == 0)
                        fields = "REPORTS.COMPANY";
                    else
                        fields = fields + ", REPORTS.COMPANY";
                }

                if (qfPort.show)
                {
                    if (fields.Length == 0)
                        fields = "REPORTS.INSPECTION_PORT";
                    else
                        fields = fields + ", REPORTS.INSPECTION_PORT";
                }

                if (qfHullClass.show)
                {
                    if (fields.Length == 0)
                        fields = "VESSELS.HULL_CLASS";
                    else
                        fields = fields + ", VESSELS.HULL_CLASS";
                }

                if (qfMaster.show)
                {
                    if (fields.Length == 0)
                        fields = "MASTERS.MASTER_NAME";
                    else
                        fields = fields + ", MASTERS.MASTER_NAME";
                }

                if (qfChiefEngineer.show)
                {
                    if (fields.Length == 0)
                        fields = "CHIEF_ENGINEERS.CHENG_NAME";
                    else
                        fields = fields + ", CHIEF_ENGINEERS.CHENG_NAME";
                }

                if (qfSubchapter.show)
                {
                    if (fields.Length == 0)
                        fields = "QUESTION_KEYS.SUBCHAPTER";
                    else
                        fields = fields + ", QUESTION_KEYS.SUBCHAPTER";
                }

                if (qfKeyWords.show)
                {
                    if (fields.Length == 0)
                        fields = "QUESTION_KEYS.KEY_INDEX";
                    else
                        fields = fields + ", QUESTION_KEYS.KEY_INDEX";
                }

                if (qfQuestionnaireType.show)
                {
                    if (fields.Length == 0)
                        fields = "TEMPLATES.TEMPLATE_TYPE";
                    else
                        fields = fields + ", TEMPLATES.TEMPLATE_TYPE";
                }

                if (fields.Length == 0)
                {
                    DS.Tables["REPORT_ITEMS"].Clear();
                    return;
                }

                string s = "";

                string itemsSQL =
                    "(select * from REPORT_ITEMS \n";

                string itemsWhere = "";

                if (isValidCondition(qfQuestionNumber.condition, qfQuestionNumber.value))
                {
                    if (!questionsMapping)
                    {
                        if (itemsWhere.Length == 0)
                            itemsWhere = setTextFilter("QUESTION_NUMBER", qfQuestionNumber.condition, qfQuestionNumber.value);
                        else
                            itemsWhere = itemsWhere + " and " + setTextFilter("QUESTION_NUMBER", qfQuestionNumber.condition, qfQuestionNumber.value);
                    }
                }

                if (isValidCondition(qfReportNumber.condition, qfReportNumber.value))
                {
                    if (itemsWhere.Length == 0)
                        itemsWhere = setTextFilter("REPORT_CODE", qfReportNumber.condition, qfReportNumber.value);
                    else
                        itemsWhere = itemsWhere + " and " + setTextFilter("REPORT_CODE", qfReportNumber.condition, qfReportNumber.value);
                }

                if (isValidCondition(qfInspectorComments.condition, qfInspectorComments.value))
                {
                    if (itemsWhere.Length == 0)
                        itemsWhere = setTextFilter("COMMENTS", qfInspectorComments.condition, qfInspectorComments.value);
                    else
                        itemsWhere = itemsWhere + " and " + setTextFilter("COMMENTS", qfInspectorComments.condition, qfInspectorComments.value);
                }

                if (isValidCondition(qfObservation.condition, qfObservation.value))
                {
                    if (itemsWhere.Length == 0)
                        itemsWhere = setTextFilter("OBSERVATION", qfObservation.condition, qfObservation.value);
                    else
                        itemsWhere = itemsWhere + " and " + setTextFilter("OBSERVATION", qfObservation.condition, qfObservation.value);
                }

                if (isValidCondition(qfOperatorComments.condition, qfOperatorComments.value))
                {
                    if (itemsWhere.Length == 0)
                        itemsWhere = setTextFilter("OPERATOR_COMMENTS", qfOperatorComments.condition, qfOperatorComments.value);
                    else
                        itemsWhere = itemsWhere + " and " + setTextFilter("OPERATOR_COMMENTS", qfOperatorComments.condition, qfOperatorComments.value);
                }

                if (isValidCondition(qfTechnicalComments.condition, qfTechnicalComments.value))
                {
                    if (itemsWhere.Length == 0)
                        itemsWhere = setTextFilter("TECHNICAL_COMMENTS", qfTechnicalComments.condition, qfTechnicalComments.value);
                    else
                        itemsWhere = itemsWhere + " and " + setTextFilter("TECHNICAL_COMMENTS", qfTechnicalComments.condition, qfTechnicalComments.value);
                }

                string textWhere = "";

                if (qfInspectorComments.show)
                {
                    textWhere = "COMMENTS_LEN>0";
                }

                if (qfObservation.show)
                {
                    if (textWhere.Length == 0)
                        textWhere = "OBSERVATION_LEN>0";
                    else
                        textWhere = textWhere + " or OBSERVATION_LEN>0";
                }
                if (qfOperatorComments.show)
                {
                    if (textWhere.Length == 0)
                        textWhere = "OPERATOR_COMMENTS_LEN>0";
                    else
                        textWhere = textWhere + " or OPERATOR_COMMENTS_LEN>0";
                }

                if (qfTechnicalComments.show)
                {
                    if (textWhere.Length == 0)
                        textWhere = "TECHNICAL_COMMENTS_LEN>0";
                    else
                        textWhere = textWhere + " or TECHNICAL_COMMENTS_LEN>0";
                }

                if (textWhere.Length>0)
                {
                    if (itemsWhere.Length == 0)
                        itemsWhere = "(" + textWhere + ")";
                    else
                        itemsWhere = itemsWhere + " and (" + textWhere + ")";
                }

                if (itemsWhere.Length == 0)
                    itemsSQL = itemsSQL + ") as REPORTITEMS";
                else
                    itemsSQL = itemsSQL + " where "+itemsWhere + ") as REPORTITEMS";


                if (questionsMapping && qfQuestionNumber.condition.Length>0 && qfQuestionNumber.value.Length>0)
                {
                    BuildMappingTable();

                    if (recSelect > 0)
                    {
                        s = "select TOP "+recSelect.ToString()+" * \n" +
                            "from \n" +
                            "( \n" +
                            "select  " + fields + "\n" +
                            "from \n";
                    }
                    else
                    {
                        s = "select  * \n" +
                            "from \n" +
                            "( \n" +
                            "select  " + fields + "\n" +
                            "from \n";
                    }

                    string brakets = "(((";

                    string sqlStr =
                        itemsSQL+" inner join TEMP_GUIDS \n" +
                        "on REPORTITEMS.QUESTION_GUID=TEMP_GUIDS.QUESTION_GUID) \n" +
                        "inner join TEMPLATE_QUESTIONS \n" +
                        "on REPORTITEMS.QUESTION_GUID=TEMPLATE_QUESTIONS.QUESTION_GUID \n" +
                        "and REPORTITEMS.TEMPLATE_GUID=TEMPLATE_QUESTIONS.TEMPLATE_GUID) \n" +
                        "inner join TEMPLATES \n" +
                        "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID) \n" +
                        "left join REPORTS  \n" +
                        "on REPORTITEMS.REPORT_CODE=REPORTS.REPORT_CODE";

                    if (qfVessel.show || useVessel)
                    {
                        brakets = brakets + "(";

                        sqlStr = sqlStr + ") \n" +
                            "left join VESSELS \n" +
                            "on REPORTS.VESSEL_GUID=VESSELS.VESSEL_GUID";
                    }

                    if (qfInspector.show || (qfInspector.condition.Length>0 && qfInspector.value.Length>0))
                    {
                        brakets = brakets + "(";

                        sqlStr = sqlStr + ") \n" +
                            "left join INSPECTORS \n" +
                            "on REPORTS.INSPECTOR_GUID=INSPECTORS.INSPECTOR_GUID";
                    }

                    if (qfMaster.show || (qfMaster.condition.Length>0 && qfMaster.value.Length>0))
                    {
                        brakets = brakets + "(";

                        sqlStr = sqlStr + ") \n" +
                            "left join \n" +
                            "( \n" +
                            "select CREW.CREW_NAME as MASTER_NAME, CREW.CREW_GUID as MASTER_GUID, REPORT_CODE \n" +
                            "from \n" +
                            "REPORTS_CREW inner join CREW \n" +
                            "on REPORTS_CREW.CREW_GUID = CREW.CREW_GUID \n" +
                            "where \n" +
                            "REPORTS_CREW.CREW_POSITION_GUID ={753D9AAC-1376-4FAE-AB00-144D81341F70} \n" +
                            ") as MASTER_LIST \n" +
                            "on REPORTS.REPORT_CODE = MASTER_LIST.REPORT_CODE) \n";
                    }

                    if (qfChiefEngineer.show || (qfChiefEngineer.condition.Length>0 && qfChiefEngineer.value.Length>0))
                    {
                        brakets = brakets + "(";

                        sqlStr = sqlStr + ") \n" +
                            "left join \n" +
                            "( \n" +
                            "select CREW.CREW_NAME as CHENG_NAME, CREW.CREW_GUID as CHENG_GUID, REPORT_CODE \n" +
                            "from \n" +
                            "REPORTS_CREW inner join CREW \n" +
                            "on REPORTS_CREW.CREW_GUID = CREW.CREW_GUID \n" +
                            "where \n" +
                            "REPORTS_CREW.CREW_POSITION_GUID ={012156E3-2990-468F-AFB4-DAD5401844F7} \n" +
                            ") as CHENG_LIST \n" +
                            "on REPORTS.REPORT_CODE = CHENG_LIST.REPORT_CODE";
                    }

                    s = s + brakets + " \n" +
                        sqlStr + " \n";

                    useTemplates = true;
                }
                else
                {
                    if (recSelect > 0)
                    {
                        s = "select TOP " + recSelect.ToString() + " * \n" +
                            "from \n" +
                            "( \n" +
                            "select  " + fields + "\n" +
                            "from \n";
                    }
                    else
                    {
                        s = "select  * \n" +
                            "from \n" +
                            "( \n" +
                            "select  " + fields + "\n" +
                            "from \n";
                    }

                    s = s + "((((((" +
                        itemsSQL + " left join REPORTS \n" +
                        "on REPORTITEMS.REPORT_CODE = REPORTS.REPORT_CODE) \n" +
                        "inner join TEMPLATE_QUESTIONS \n" +
                        "on REPORTITEMS.TEMPLATE_GUID = TEMPLATE_QUESTIONS.TEMPLATE_GUID \n" +
                        "and REPORTITEMS.QUESTION_GUID = TEMPLATE_QUESTIONS.QUESTION_GUID) \n" +
                        "left join TEMPLATES \n" +
                        "on TEMPLATE_QUESTIONS.TEMPLATE_GUID = TEMPLATES.TEMPLATE_GUID) \n" +
                        "left join VESSELS \n" +
                        "on REPORTS.VESSEL_GUID = VESSELS.VESSEL_GUID)  \n" +
                        "left join INSPECTORS \n" +
                        "on REPORTS.INSPECTOR_GUID = INSPECTORS.INSPECTOR_GUID)  \n" +
                        "left join \n" +
                        "( \n" +
                        "select CREW.CREW_NAME as MASTER_NAME, CREW.CREW_GUID as MASTER_GUID, REPORT_CODE \n" +
                        "from \n" +
                        "REPORTS_CREW inner join CREW \n" +
                        "on REPORTS_CREW.CREW_GUID = CREW.CREW_GUID \n" +
                        "where \n" +
                        "REPORTS_CREW.CREW_POSITION_GUID ={753D9AAC-1376-4FAE-AB00-144D81341F70} \n" +
                        ") as MASTER_LIST \n" +
                        "on REPORTS.REPORT_CODE = MASTER_LIST.REPORT_CODE) \n" +
                        "left join \n" +
                        "( \n" +
                        "select CREW.CREW_NAME as CHENG_NAME, CREW.CREW_GUID as CHENG_GUID, REPORT_CODE \n" +
                        "from \n" +
                        "REPORTS_CREW inner join CREW \n" +
                        "on REPORTS_CREW.CREW_GUID = CREW.CREW_GUID \n" +
                        "where \n" +
                        "REPORTS_CREW.CREW_POSITION_GUID ={012156E3-2990-468F-AFB4-DAD5401844F7} \n" +
                        ") as CHENG_LIST \n" +
                        "on REPORTS.REPORT_CODE = CHENG_LIST.REPORT_CODE";

                    useTemplates = true;

                }
 
                string where = "";

                if ((questionsMapping) && (qfQuestionNumber.typeCode.Length > 0))
                    where = "TEMPLATES.TYPE_CODE like '%" + qfQuestionNumber.typeCode + "' ";

                string templateType = toolStripComboBox1.Text;

                if (isValidCondition(qfInspector.condition, qfInspector.value))
                {
                    if (where.Length == 0)
                        where = setTextFilter("INSPECTORS.INSPECTOR_NAME", qfInspector.condition, qfInspector.value);
                    else
                        where = where + " and " + setTextFilter("INSPECTORS.INSPECTOR_NAME", qfInspector.condition, qfInspector.value);
                }

                if (isValidCondition(qfVessel.condition, qfVessel.value))
                {
                    if (where.Length == 0)
                        where = setTextFilter("VESSELS.VESSEL_NAME", qfVessel.condition, qfVessel.value);
                    else
                        where = where + " and " + setTextFilter("VESSELS.VESSEL_NAME", qfVessel.condition, qfVessel.value);
                }

                if (isValidCondition(qfPort.condition, qfPort.value))
                {
                    if (where.Length == 0)
                        where = setTextFilter("REPORTS.INSPECTION_PORT", qfPort.condition, qfPort.value);
                    else
                        where = where + " and " + setTextFilter("REPORTS.INSPECTION_PORT", qfPort.condition, qfPort.value);
                }

                if (isValidCondition(qfFleet.condition, qfFleet.value))
                {
                    if (where.Length == 0)
                        where = setTextFilter("VESSELS.FLEET", qfFleet.condition, qfFleet.value);
                    else
                        where = where + " and " + setTextFilter("VESSELS.FLEET", qfFleet.condition, qfFleet.value);
                }

                if (isValidCondition(qfOffice.condition, qfOffice.value))
                {
                    if (where.Length == 0)
                        where = setTextFilter("VESSELS.OFFICE", qfOffice.condition, qfOffice.value);
                    else
                        where = where + " and " + setTextFilter("VESSELS.OFFICE", qfOffice.condition, qfOffice.value);
                }

                if (isValidCondition(qfDOC.condition, qfDOC.value))
                {
                    if (where.Length == 0)
                        where = setTextFilter("VESSELS.DOC", qfDOC.condition, qfDOC.value);
                    else
                        where = where + " and " + setTextFilter("VESSELS.DOC", qfDOC.condition, qfDOC.value);
                }

                if (isValidCondition(qfHullClass.condition, qfHullClass.value))
                {
                    if (where.Length == 0)
                        where = setTextFilter("VESSELS.HULL_CLASS", qfHullClass.condition, qfHullClass.value);
                    else
                        where = where + " and " + setTextFilter("VESSELS.HULL_CLASS", qfHullClass.condition, qfHullClass.value);
                }

                if (isValidCondition(qfCompany.condition, qfCompany.value))
                {
                    if (where.Length == 0)
                        where = setTextFilter("REPORTS.COMPANY", qfCompany.condition, qfCompany.value);
                    else
                        where = where + " and " + setTextFilter("REPORTS.COMPANY", qfCompany.condition, qfCompany.value);
                }

                if (qfDate.condition.Length > 0)
                {
                    if (where.Length == 0)
                        where = setDateFilter("INSPECTION_DATE", qfDate.condition, qfDate.value, qfDate.value2);
                    else
                        where = where + " and " + setDateFilter("INSPECTION_DATE", qfDate.condition, qfDate.value, qfDate.value2);
                }


                if (isValidCondition(qfMaster.condition, qfMaster.value))
                {
                    if (where.Length == 0)
                        where = setTextFilter("MASTERS.MASTER_NAME", qfMaster.condition, qfMaster.value);
                    else
                        where = where + " and " + setTextFilter("MASTERS.MASTER_NAME", qfMaster.condition, qfMaster.value);
                }


                if (isValidCondition(qfChiefEngineer.condition, qfChiefEngineer.value))
                {
                    if (where.Length == 0)
                        where = setTextFilter("CHIEF_ENGINEERS.CHENG_NAME", qfChiefEngineer.condition, qfChiefEngineer.value);
                    else
                        where = where + " and " + setTextFilter("CHIEF_ENGINEERS.CHENG_NAME", qfChiefEngineer.condition, qfChiefEngineer.value);
                }

                if (isValidCondition(qfSubchapter.condition, qfSubchapter.value))
                {
                    if (where.Length == 0)
                        where = setTextFilter("QUESTION_KEYS.SUBCHAPTER", qfSubchapter.condition, qfSubchapter.value);
                    else
                        where = where + " and " + setTextFilter("QUESTION_KEYS.SUBCHAPTER", qfSubchapter.condition, qfSubchapter.value);
                }

                if (isValidCondition(qfKeyWords.condition, qfKeyWords.value))
                {
                    if (where.Length == 0)
                        where = setTextFilter("QUESTION_KEYS.KEY_INDEX", qfKeyWords.condition, qfKeyWords.value);
                    else
                        where = where + " and " + setTextFilter("QUESTION_KEYS.KEY_INDEX", qfKeyWords.condition, qfKeyWords.value);
                }

                if (isValidCondition(qfQuestionnaireType.condition,qfQuestionnaireType.value))
                {
                    if (useTemplates)
                    {
                        if (where.Length == 0)
                            where = setTextFilter("TEMPLATES.TEMPLATE_TYPE", qfQuestionnaireType.condition, qfQuestionnaireType.value);
                        else
                            where = where+" and "+setTextFilter("TEMPLATES.TEMPLATE_TYPE", qfQuestionnaireType.condition, qfQuestionnaireType.value);
                    }
                }

                if (where.Length > 0)
                    s = s + " where " + where;
                
                DS.Tables["REPORT_ITEMS"].Dispose();

                questionAdapter.SelectCommand.CommandText = s;

                questionAdapter.SelectCommand.CommandText = questionAdapter.SelectCommand.CommandText + "\n" +
                    "order by REPORTS.INSPECTION_DATE DESC, REPORTS.REPORT_CODE, TEMPLATE_QUESTIONS.SEQUENCE)";
                
                dgvDetails.Columns["QUESTION_NUMBER"].Visible = !hideQNumber;

                if (dgvDetails.Columns.Contains("SEQUENCE"))
                {
                    dgvDetails.Columns["SEQUENCE"].Visible = false;

                    if (dgvDetails.Columns.Contains("QUESTION_NUMBER"))
                        dgvDetails.Columns["QUESTION_NUMBER"].SortMode = DataGridViewColumnSortMode.Programmatic;
                }
                

                updateGrid2();

            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }


        private bool BuildMappingTable()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);
            int level = 1;

            if (!ClearTable("TEMP_MAPPING"))
            {
                MessageBox.Show("Failed to clear TEMP_MAPPING table","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }

            Guid templateGuid = GetLatestTemplateGUID();

            List<string> guidList = new List<string>();

            guidList.AddRange(GetLastTemplateGuidList(qfQuestionnaireType.value));

            if (guidList.Count>0)
            {
                string qnFilter = "";

                if (isValidCondition(qfQuestionNumber.condition, qfQuestionNumber.value))
                {
                    qnFilter = setTextFilter("TEMPLATE_QUESTIONS.QUESTION_NUMBER", qfQuestionNumber.condition, qfQuestionNumber.value);

                    if (qfQuestionNumber.typeCode.Length > 0)
                    {
                        qnFilter = qnFilter + " \n" +
                            "and RIGHT(TYPE_CODE,2)='" + MainForm.StrToSQLStr(qfQuestionNumber.typeCode) + "'";
                    }
                }    
                
                for (int i=0; i<guidList.Count; i++)
                {
                    cmd.CommandText =
                        "insert into TEMP_MAPPING (LEVEL_ID, MASTER_GUID, SLAVE_GUID) \n" +
                        "select DISTINCT \n" +
                        "1 as LEVEL_ID,  \n" +
                        "QUESTIONS_MAPPING.QUESTION_MASTER_GUID, \n" +
                        "QUESTIONS_MAPPING.QUESTION_SLAVE_GUID \n" +
                        "from  \n" +
                        "(QUESTIONS_MAPPING inner join TEMPLATE_QUESTIONS \n" +
                        "on QUESTIONS_MAPPING.QUESTION_MASTER_GUID=TEMPLATE_QUESTIONS.QUESTION_GUID) \n" +
                        "left join TEMPLATES \n" +
                        "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID \n" +
                        "where \n" +
                        "TEMPLATE_QUESTIONS.TEMPLATE_GUID=" + FormatGuidString(guidList[i]) + " \n";

                    if (qnFilter.Length > 0)
                        cmd.CommandText = cmd.CommandText + "and " + qnFilter;

                    if (cmdExecute(cmd)<0)
                    {
                        MessageBox.Show("Failed to fill TEMP_MAPPING table", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return false;
                    }
                }

                while (HasSubsRecords(level))
                {
                    int newLevel = level + 1;

                    cmd.CommandText =
                        "insert into TEMP_MAPPING (LEVEL_ID,MASTER_GUID,SLAVE_GUID) \n" +
                        "select DISTINCT \n" +
                        newLevel.ToString() + " as LEVEL_ID, \n" +
                        "QUESTIONS_MAPPING.QUESTION_MASTER_GUID, \n" +
                        "QUESTIONS_MAPPING.QUESTION_SLAVE_GUID \n" +
                        "from \n" +
                        "(QUESTIONS_MAPPING inner join TEMP_MAPPING \n" +
                        "on QUESTIONS_MAPPING.QUESTION_MASTER_GUID=TEMP_MAPPING.SLAVE_GUID) \n" +
                        "left join \n" +
                        "(select * from TEMP_MAPPING) as Q1 \n" +
                        "on QUESTIONS_MAPPING.QUESTION_MASTER_GUID=Q1.MASTER_GUID \n" +
                        "where \n" +
                        "TEMP_MAPPING.LEVEL_ID=" + level.ToString() + " \n" +
                        "and QUESTIONS_MAPPING.QUESTION_MASTER_GUID<>QUESTIONS_MAPPING.QUESTION_SLAVE_GUID \n";
                        //"and QUESTIONS_MAPPING.QUESTION_SLAVE_GUID<>Q1.SLAVE_GUID";

                    if (cmdExecute(cmd)<0)
                    {
                        MessageBox.Show("Failed to fill TEPM_MAPPING table on level " + level.ToString(),
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                        return false;
                    }

                    level = newLevel;
                }


                cmd.CommandText =
                    "delete from TEMP_MAPPING \n" +
                    "where MASTER_GUID=SLAVE_GUID \n" +
                    "and LEVEL_ID>1";

                if (cmdExecute(cmd)<0)
                {
                    MessageBox.Show("Failed to clear TEPM_MAPPING from self mapping records",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }

                if (!ClearTable("TEMP_GUIDS"))
                {
                    MessageBox.Show("Failed to clear TEMP_GUIDS table","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    
                    return false;
                }

                cmd.CommandText =
                    "insert into TEMP_GUIDS (QUESTION_GUID) \n"+
                    "select MASTER_GUID \n"+
                    "from ( \n"+
                    "select \n" +
                    "MASTER_GUID \n" +
                    "from TEMP_MAPPING \n" +
                    "where LEVEL_ID=1 \n" +
                    "union \n" +
                    "select  \n" +
                    "SLAVE_GUID \n" +
                    "from TEMP_MAPPING)";

                if (cmdExecute(cmd)<0)
                {
                    MessageBox.Show("Failed to build list of mapping questions GUIDs","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    
                    return false;
                }

                return true;
            }
            else
                return false;
        }

        private bool HasSubsRecords(int level)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select \n" +
                "Count(Q1.QUESTION_MASTER_GUID) as RecCount \n" +
                "from \n" +
                "( \n" +
                "select  \n" +
                "QUESTIONS_MAPPING.QUESTION_MASTER_GUID, \n" +
                "QUESTIONS_MAPPING.QUESTION_SLAVE_GUID  \n" +
                "from  \n" +
                "QUESTIONS_MAPPING inner join TEMP_MAPPING  \n" +
                "on QUESTIONS_MAPPING.QUESTION_MASTER_GUID=TEMP_MAPPING.SLAVE_GUID  \n" +
                "where  \n" +
                "TEMP_MAPPING.LEVEL_ID=" + level.ToString() + " \n" +
                ") as Q1 \n" +
                "left join \n" +
                "( \n" +
                "select * \n" +
                "from \n" +
                "TEMP_MAPPING \n" +
                ") as Q2 \n" +
                "on Q1.QUESTION_MASTER_GUID=Q2.MASTER_GUID \n" +
                "and Q1.QUESTION_SLAVE_GUID=Q2.SLAVE_GUID \n" +
                "where Q2.MASTER_GUID is Null \n" +
                "and Q1.QUESTION_MASTER_GUID<>Q1.QUESTION_SLAVE_GUID";

            int count = (int)cmd.ExecuteScalar();

            if (count == 0)
                return false;
            else
                return true;
        }

        private Guid GetLatestTemplateGUID()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            string qnFilter="";

            if (isValidCondition(qfQuestionNumber.condition,qfQuestionNumber.value))
            {
                if (qfQuestionNumber.typeCode.Length>0)
                    qnFilter = "RIGHT(TYPE_CODE,2)='" + StrToSQLStr(qfQuestionNumber.typeCode) + "'";
            }

            if (qnFilter.Length > 0)
            {
                cmd.CommandText =
                    "select TEMPLATE_GUID \n" +
                    "from ( \n" + "select TOP 1 MAX(PUBLISHED) as MAX_PUBLISHED, TEMPLATE_GUID \n" +
                    "from TEMPLATES \n" +
                    "where "+qnFilter+" \n"+
                    "group by TEMPLATE_GUID \n" +
                    "order by MAX(PUBLISHED) DESC)";
            }
            else
            {
                cmd.CommandText =
                    "select TEMPLATE_GUID \n"+
                    "from ( \n"+
                    "select TOP 1 MAX(PUBLISHED) as MAX_PUBLISHED, TEMPLATE_GUID \n" +
                    "from TEMPLATES \n" +
                    "group by TEMPLATE_GUID \n" +
                    "order by MAX(PUBLISHED) DESC)";
            }

            object id = cmd.ExecuteScalar();

            if (id==System.DBNull.Value || id==null)
            {
                return zeroGuid;
            }
            else
            {
                return (Guid)id;
            }
        }


        private List<string> GetLastTemplateGuidList(string qType)
        {
            List<string> guidList=new List<string>();

            switch (qType)
            {
                case "OVIQ":
                    guidList.Add(GetLatestTemplateGUID().ToString());
                    return guidList;
                case "VIQ":
                    OleDbCommand cmd = new OleDbCommand("", connection);

                    cmd.CommandText =
                        "select \n" +
                        "TEMPLATE_GUID \n" +
                        "from TEMPLATES \n" +
                        "where \n" +
                        "PUBLISHED= \n" +
                        "(select Max(PUBLISHED) \n" +
                        "from TEMPLATES \n" +
                        "where \n" +
                        "TYPE_CODE like '%01') \n" +
                        "and TYPE_CODE like '%01' \n" +
                        "union \n" +
                        "select \n" +
                        "TEMPLATE_GUID \n" +
                        "from TEMPLATES \n" +
                        "where \n" +
                        "PUBLISHED= \n" +
                        "(select Max(PUBLISHED) \n" +
                        "from TEMPLATES \n" +
                        "where \n" +
                        "TYPE_CODE like '%02') \n" +
                        "and TYPE_CODE like '%02' \n" +
                        "union \n" +
                        "select \n" +
                        "TEMPLATE_GUID \n" +
                        "from TEMPLATES \n" +
                        "where \n" +
                        "PUBLISHED= \n" +
                        "(select Max(PUBLISHED) \n" +
                        "from TEMPLATES \n" +
                        "where \n" +
                        "TYPE_CODE like '%03') \n" +
                        "and TYPE_CODE like '%03' \n" +
                        "union \n" +
                        "select \n" +
                        "TEMPLATE_GUID \n" +
                        "from TEMPLATES \n" +
                        "where \n" +
                        "PUBLISHED= \n" +
                        "(select Max(PUBLISHED) \n" +
                        "from TEMPLATES \n" +
                        "where \n" +
                        "TYPE_CODE like '%04') \n" +
                        "and TYPE_CODE like '%04'";

                    OleDbDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            guidList.Add(reader["TEMPLATE_GUID"].ToString());
                        }
                    }

                    reader.Close();

                    return guidList;
                default:
                    return guidList;
            }
        }

        private void dgvInspections_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            System.Windows.Forms.Keys mod = Control.ModifierKeys & System.Windows.Forms.Keys.Modifiers;
            bool ctrlOnly = mod == System.Windows.Forms.Keys.Control;

            //if (ctrlOnly)
            //{
            //    editInspection();
            //}
            //else
                editReportDetails();
        }

        private void editReportDetails()
        {
            //Modified on 10.10.2016 in version 2.0.15.1
            //  Initialize reportID and reportCode on the new form
            //Modified on 12.10.2016 in version 2.0.15.1
            //  Stop using public components
            //  Using public variable of the form
            //  Move save procedures to the form

            if (dgvInspections.Rows.Count == 0) 
                return;

            this.Cursor = Cursors.WaitCursor;

            string reportCode = dgvInspections.CurrentRow.Cells["REPORT_CODE"].Value.ToString();

            FormReportSummary form = new FormReportSummary(false, reportCode, true, false);

            var rslt = form.ShowDialog();

            this.Cursor = Cursors.Default;

            if (rslt == DialogResult.OK)
            {
                int curRow = dgvInspections.CurrentCell.RowIndex;
                int curCol = dgvInspections.CurrentCell.ColumnIndex;
                int firstVisibleRow = dgvInspections.FirstDisplayedCell.RowIndex;

                fillInspectors();
                RecalcObs();

                DS.Tables["REPORTS"].Clear();

                reportsAdapter.Fill(DS, "REPORTS");

                if (dgvInspections.Rows.Count > 0)
                {
                    try
                    {
                        dgvInspections.FirstDisplayedScrollingRowIndex = firstVisibleRow;
                        dgvInspections.CurrentCell = dgvInspections[curCol, curRow];
                    }
                    catch
                    {
                        //Do nothing
                    }
                }

            }
        }


        private void updateGrid2()
        {
            DS.Tables["REPORT_ITEMS"].Clear();


            questionAdapter.Fill(DS, "REPORT_ITEMS");

            dgvDetails.Columns["REPORT_CODE"].HeaderText = "Report";
            dgvDetails.Columns["REPORT_CODE"].Visible = qfReportNumber.show;
            dgvDetails.Columns["REPORT_CODE"].FillWeight = 30;

            dgvDetails.Columns["QUESTION_NUMBER"].HeaderText = "Question No.";
            dgvDetails.Columns["QUESTION_NUMBER"].DefaultCellStyle.Font = new Font(dgvDetails.DefaultCellStyle.Font.Name, dgvDetails.DefaultCellStyle.Font.Size, FontStyle.Bold);
            dgvDetails.Columns["QUESTION_NUMBER"].Visible = qfQuestionNumber.show;
            dgvDetails.Columns["QUESTION_NUMBER"].FillWeight = 10;

            if (dgvDetails.Columns.Contains("SEQUENCE"))
                dgvDetails.Columns["SEQUENCE"].Visible = false;

            dgvDetails.Columns["QUESTION_GUID"].HeaderText = "GUID";
            dgvDetails.Columns["QUESTION_GUID"].Visible = qfQuestionGUID.show;
            dgvDetails.Columns["QUESTION_GUID"].FillWeight = 32;

            dgvDetails.Columns["VESSEL_NAME"].HeaderText = "Vessel name";
            dgvDetails.Columns["VESSEL_NAME"].Visible = qfVessel.show;

            dgvDetails.Columns["VESSEL_NAME"].DefaultCellStyle.Font = new Font(dgvDetails.DefaultCellStyle.Font.Name, dgvDetails.DefaultCellStyle.Font.Size, FontStyle.Bold);
            dgvDetails.Columns["VESSEL_NAME"].FillWeight = 30;

            dgvDetails.Columns["INSPECTOR_NAME"].HeaderText = "Name of inspector";
            dgvDetails.Columns["INSPECTOR_NAME"].Visible = qfInspector.show;
            dgvDetails.Columns["INSPECTOR_NAME"].FillWeight = 30;

            dgvDetails.Columns["COMMENTS"].HeaderText = "Comments of inspector";
            dgvDetails.Columns["COMMENTS"].Visible = qfInspectorComments.show;

            dgvDetails.Columns["OBSERVATION"].HeaderText = "Observation";
            dgvDetails.Columns["OBSERVATION"].DefaultCellStyle.ForeColor = Color.Red;
            dgvDetails.Columns["OBSERVATION"].Visible = qfObservation.show;

            dgvDetails.Columns["OPERATOR_COMMENTS"].HeaderText = "Comments of operator";
            dgvDetails.Columns["OPERATOR_COMMENTS"].DefaultCellStyle.ForeColor = Color.FromArgb(53, 94, 169);
            dgvDetails.Columns["OPERATOR_COMMENTS"].Visible = qfOperatorComments.show;

            dgvDetails.Columns["TECHNICAL_COMMENTS"].HeaderText = "Technical comments";
            dgvDetails.Columns["TECHNICAL_COMMENTS"].Visible = qfTechnicalComments.show;

            dgvDetails.Columns["INSPECTION_DATE"].HeaderText = "Date of inspection";
            dgvDetails.Columns["INSPECTION_DATE"].Visible = qfDate.show;
            dgvDetails.Columns["INSPECTION_DATE"].FillWeight = 30;
            dgvDetails.Columns["INSPECTION_DATE"].DefaultCellStyle.Format = "d";

            dgvDetails.Columns["COMPANY"].HeaderText = "Company";
            dgvDetails.Columns["COMPANY"].Visible = qfCompany.show;
            dgvDetails.Columns["COMPANY"].FillWeight = 30;

            dgvDetails.Columns["INSPECTION_PORT"].HeaderText = "Port";
            dgvDetails.Columns["INSPECTION_PORT"].Visible = qfPort.show;
            dgvDetails.Columns["INSPECTION_PORT"].FillWeight = 30;

            dgvDetails.Columns["OFFICE"].HeaderText = "Office";
            dgvDetails.Columns["OFFICE"].Visible = qfOffice.show;
            dgvDetails.Columns["OFFICE"].FillWeight = 20;

            dgvDetails.Columns["DOC"].HeaderText = "DOC";
            dgvDetails.Columns["DOC"].Visible = qfDOC.show;
            dgvDetails.Columns["DOC"].FillWeight = 20;

            dgvDetails.Columns["HULL_CLASS"].HeaderText = "Hull class";
            dgvDetails.Columns["HULL_CLASS"].Visible = qfHullClass.show;
            dgvDetails.Columns["HULL_CLASS"].FillWeight = 30;

            dgvDetails.Columns["MASTER_GUID"].Visible = false;

            dgvDetails.Columns["MASTER_NAME"].HeaderText = "Master";
            dgvDetails.Columns["MASTER_NAME"].Visible = qfMaster.show;
            dgvDetails.Columns["MASTER_NAME"].FillWeight = 20;

            dgvDetails.Columns["CHENG_GUID"].Visible = false;

            dgvDetails.Columns["CHENG_NAME"].HeaderText = "Ch.Engineer";
            dgvDetails.Columns["CHENG_NAME"].Visible = qfChiefEngineer.show;
            dgvDetails.Columns["CHENG_NAME"].FillWeight = 20;

            dgvDetails.Columns["SUBCHAPTER"].HeaderText = "Subchapter";
            dgvDetails.Columns["SUBCHAPTER"].Visible = qfSubchapter.show;
            dgvDetails.Columns["SUBCHAPTER"].FillWeight = 30;

            dgvDetails.Columns["KEY_INDEX"].HeaderText = "Key";
            dgvDetails.Columns["KEY_INDEX"].Visible = qfKeyWords.show;
            dgvDetails.Columns["KEY_INDEX"].FillWeight = 30;

            if (dgvDetails.Columns.Contains("TEMPLATE_TYPE"))
            {
                dgvDetails.Columns["TEMPLATE_TYPE"].HeaderText = "Type";
                dgvDetails.Columns["TEMPLATE_TYPE"].Visible = qfQuestionnaireType.show;
                dgvDetails.Columns["TEMPLATE_TYPE"].FillWeight = 30;
            }

            toolStripStatusLabel2.Text = Convert.ToString(dgvDetails.Rows.Count);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void fillReports()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);
            cmd.CommandText =
                "select DISTINCT REPORT_CODE \n"+
                "from REPORTS \n"+
                "union \n"+
                "select Top 1 '' as CODE \n"+
                "from REPORTS";

            toolStripStatusLabel4.Text = dgvInspections.Rows.Count.ToString();

            OleDbDataAdapter reportsAdapter = new OleDbDataAdapter(cmd);

            reportsAdapter.Fill(DS, "REPORTS_LIST");

        }

        private void fillVesselFilter()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);
            cmd.CommandText =
                "select DISTINCT UCASE(Vessel) from REPORTS";

            toolStripStatusLabel4.Text = dgvInspections.Rows.Count.ToString();
        }

        private void fillCompanyFilter()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);
            cmd.CommandText =
                "select DISTINCT COMPANY from\n"+
                "(select DISTINCT COMPANY from REPORTS \n"+
                "union \n"+
                "select Top 1 '' as Company from REPORTS)";


            toolStripStatusLabel4.Text = dgvInspections.Rows.Count.ToString();

            OleDbDataAdapter companiesAdapter = new OleDbDataAdapter(cmd);

            companiesAdapter.Fill(DS, "COMPANIES_LIST");
        }

        private void fillPortFilter()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);
            cmd.CommandText =
                "select DISTINCT INSPECTION_PORT \n"+
                "from \n"+
                "(select INSPECTION_PORT \n"+
                "from REPORTS \n"+
                "union \n"+
                "select top 1 '' as INSPECTION_PORT \n"+
                "from REPORTS) \n";

            toolStripStatusLabel4.Text = dgvInspections.Rows.Count.ToString();

            OleDbDataAdapter portsAdapter = new OleDbDataAdapter(cmd);

            portsAdapter.Fill(DS, "PORTS_LIST");
        }

        private void fillOfficeFilter()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);
            cmd.CommandText =
                "select OFFICE_ID \n"+
                "from OFFICES \n"+
                "union \n"+
                "select top 1 '' as OFFICE_ID from VESSELS";

            toolStripStatusLabel4.Text = dgvInspections.Rows.Count.ToString();

            OleDbDataAdapter officesAdapter = new OleDbDataAdapter(cmd);

            officesAdapter.Fill(DS, "OFFICES_LIST");
        }

        private void fillDOCFilter()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);
            cmd.CommandText =
                "select DOC_ID \n" +
                "from DOCS \n" +
                "union \n" +
                "select top 1 '' as DOC_ID from VESSELS";

            toolStripStatusLabel4.Text = dgvInspections.Rows.Count.ToString();

            OleDbDataAdapter docsAdapter = new OleDbDataAdapter(cmd);

            docsAdapter.Fill(DS, "DOCS_LIST");
        }

        private void fillHullClassFilter()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);
            cmd.CommandText =
                "select DISTINCT HULL_CLASS from \n"+
                "(select DISTINCT IIF(IsNull(HULL_CLASS),'',Trim(HULL_CLASS)) as HULL_CLASS from VESSELS \n" +
                "union \n"+
                "select Top 1 '' as HULL_CLASS from VESSELS)";


            toolStripStatusLabel4.Text = dgvInspections.Rows.Count.ToString();

            OleDbDataAdapter classAdapter = new OleDbDataAdapter(cmd);

            classAdapter.Fill(DS, "CLASSES_LIST");
        }


        private void updateReports()
        {

            int firstVisibleRow = 0;

            if (dgvInspections.FirstDisplayedCell != null)
                firstVisibleRow = dgvInspections.FirstDisplayedCell.RowIndex;

            int curRow = -1;
            
            if (dgvInspections.CurrentRow!=null)
                curRow=dgvInspections.CurrentRow.Index;

            int curCol = -1;
            
            if (dgvInspections.CurrentCell!=null)
                curCol=dgvInspections.CurrentCell.ColumnIndex;

            if (DS.Tables.Contains("REPORTS"))
                DS.Tables["REPORTS"].Clear();

            string selectSQL=
                "select "+
                "REPORTS.REPORT_CODE, "+
                "VESSELS.VESSEL_NAME, "+
                "REPORTS.OBS_COUNT, "+
                "VESSELS.VESSEL_IMO, " +
                "REPORTS.INSPECTOR_GUID, "+
                "INSPECTORS.INSPECTOR_NAME, "+
                "REPORTS.INSPECTION_DATE, "+
                "REPORTS.INSPECTION_PORT, " +
                "REPORTS.INSPECTION_TYPE_ID, "+
                "INSPECTION_TYPES.INSPECTION_TYPE, "+
                "iif(REPORTS.INSPECTION_TYPE_ID=2, MEMORANDUMS.MEMORANDUM_TYPE, REPORTS.COMPANY) as COMPANY, \n" +
                "REPORTS.MEMORANDUM_ID, "+
                "REPORTS.VIQ_TYPE, "+
                "REPORTS.VIQ_VERSION, "+
                "REPORTS.VIQ_TYPE_CODE, "+
                "VESSELS.OFFICE, "+
                "VESSELS.DOC, "+
                "VESSELS.HULL_CLASS, " +
                "MASTER_LIST.MASTER_GUID, "+
                "MASTER_LIST.MASTER_NAME, "+
                "CHENG_LIST.CHENG_GUID, "+
                "CHENG_LIST.CHENG_NAME, " +
                "REPORTS.MANUAL, "+
                "REPORTS.FILE_AVAILABLE, "+
                "REPORTS.TEMPLATE_GUID, "+
                "REPORTS.VESSEL_GUID \n" +

                "from "+
                "(((((REPORTS left join VESSELS \n" +
                "on REPORTS.VESSEL_GUID=VESSELS.VESSEL_GUID) \n" +
                "left join \n" +
                "( \n" +
                "select CREW.CREW_NAME as MASTER_NAME, CREW.CREW_GUID as MASTER_GUID, REPORT_CODE \n" +
                "from \n" +
                "REPORTS_CREW inner join CREW \n" +
                "on REPORTS_CREW.CREW_GUID = CREW.CREW_GUID \n" +
                "where \n" +
                "REPORTS_CREW.CREW_POSITION_GUID ={753D9AAC-1376-4FAE-AB00-144D81341F70} \n" +
                ") as MASTER_LIST \n" +
                "on REPORTS.REPORT_CODE = MASTER_LIST.REPORT_CODE) \n" +
                "left join \n" +
                "( \n" +
                "select CREW.CREW_NAME as CHENG_NAME, CREW.CREW_GUID as CHENG_GUID, REPORT_CODE \n" +
                "from \n" +
                "REPORTS_CREW inner join CREW \n" +
                "on REPORTS_CREW.CREW_GUID = CREW.CREW_GUID \n" +
                "where \n" +
                "REPORTS_CREW.CREW_POSITION_GUID ={012156E3-2990-468F-AFB4-DAD5401844F7} \n" +
                ") as CHENG_LIST \n" +
                "on REPORTS.REPORT_CODE = CHENG_LIST.REPORT_CODE)  \n" +
                "left join INSPECTORS \n"+
                "on REPORTS.INSPECTOR_GUID=INSPECTORS.INSPECTOR_GUID) \n"+
                "left join INSPECTION_TYPES \n"+
                "on REPORTS.INSPECTION_TYPE_ID=INSPECTION_TYPES.INSPECTION_TYPE_ID) \n"+
                "left join MEMORANDUMS \n"+
                "on REPORTS.MEMORANDUM_ID=MEMORANDUMS.MEMORANDUM_ID \n"+
                "where \n"+
                "(REPORTS.INSPECTION_TYPE_ID="+inspectionType.ToString()+" \n"+
                "or 0="+ inspectionType.ToString() + ") \n"+
                "and 1=1 \n"+
                "order by REPORTS.INSPECTION_DATE DESC";


            if (reportFilterOn)
            {
                reportFilter = BuildReportFilter();

                selectSQL=selectSQL.Replace("1=1", reportFilter);
            }

            reportsAdapter.SelectCommand.CommandText = selectSQL;
            
            int recs=reportsAdapter.Fill(DS, "REPORTS");

            toolStripStatusLabel4.Text = dgvInspections.Rows.Count.ToString();

            if (recs > 0)
            {
                try
                {
                    dgvInspections.FirstDisplayedScrollingRowIndex = firstVisibleRow;
                }
                catch
                { }

                if ((curRow >= 0) && (curCol >= 0))
                {
                    if (recs > curRow)
                        dgvInspections.CurrentCell = dgvInspections[curCol, curRow];
                }
            }

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void setFilter(int mode)
        {
            
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void checkQuestionGUID()
        {
            DataTable tablesColumns;
            DataRow[] foundRows;
            string tableName = "REPORT_ITEMS";
            string fieldName = "QUESTION_GUID";

            tablesColumns = connection.GetSchema("Columns", new string[] { null, null, tableName, fieldName });

            foundRows = tablesColumns.Select();

            if (foundRows.GetLength(0) == 0)
            {
                OleDbCommand cmd = new OleDbCommand("alter table REPORT_ITEMS add column QUESTION_GUID GUID", connection);
                cmdExecute(cmd);
            }

        }

        public void showMessage(string text)
        {
            System.Windows.Forms.MessageBox.Show(text);
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            updateQuestions();
        }

        public static Boolean tableExists(string tableName)
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

        private void readTemplateDocument(XmlReader document)
        {
            //Чтение XML файла со структурой вопросника

            string questionNum = "";
            string questionGUID = "";
            string chapterNumber = "";
            string chapterName = "";
            string sectionNumber = "";
            string sectionName = "";
            bool inside = false;
            string questionText="";
            string sequence = "";
            string guidance = "";
            string responseDataType = "";

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = connection;

            reportVersion = "";

            while (document.Read())
            {
                switch (document.Name)
                {
                    case "ChapterNumber":
                        if (document.NodeType == XmlNodeType.Element)
                        {
                            document.Read();
                            chapterNumber = document.Value;
                            sectionNumber = "";
                            sectionName = "";
                        }
                        break;
                    case "ChapterName":
                        if (document.NodeType == XmlNodeType.Element)
                        {
                            document.Read();
                            chapterName = document.Value;
                        }
                        break;
                    case "SectionNumber":
                        if (document.NodeType == XmlNodeType.Element)
                        {
                            document.Read();
                            sectionNumber = document.Value;
                        }
                        break;
                    case "SectionName":
                        if (document.NodeType == XmlNodeType.Element)
                        {
                            document.Read();
                            sectionName = document.Value;
                        }
                        break;
                    case "Question":
                        if (document.NodeType == XmlNodeType.Element)
                        {
                            sequence = "";
                            for (int i = 0; i < document.AttributeCount; i++)
                            {
                                if (i == 0)
                                    document.MoveToFirstAttribute();
                                else
                                    document.MoveToNextAttribute();

                                if (document.Name == "sequence")
                                {
                                    sequence = document.Value;
                                }
                            }
                            inside = true;
                            questionGUID = "";
                            questionNum = "";
                            questionText = "";
                            responseDataType = "";
                        }
                        else
                            if (document.NodeType == XmlNodeType.EndElement)
                            {
                                inside = false;

                                //Сохраняем вопрос
                                if (responseDataType!= "Guidance")
                                    saveTemplateQuestion(chapterNumber, chapterName, sectionNumber, sectionName, 
                                        questionNum, questionGUID, questionText, sequence);

                            }
                        break;
                    case "ElementKey":
                        if ((inside) && (document.NodeType == XmlNodeType.Element))
                        {
                            document.Read();
                            questionGUID = document.Value;
                        }
                        break;
                    case "QuestionNumber":
                        if (document.NodeType == XmlNodeType.Element)
                        {
                            document.Read();
                            questionNum = document.Value;
                        }
                        break;
                    case "QuestionText":
                        if (document.NodeType == XmlNodeType.Element)
                        {
                            document.Read();
                            questionText = MainForm.StrToSingleLine(document.Value);
                        }
                        break;
                    case "ResponseDataType":
                        if (document.NodeType == XmlNodeType.Element)
                        {
                            document.Read();
                            responseDataType = document.Value;
                        }
                        break;
                }

            }
            return;
        }

        private void saveTemplateQuestion(string chapterNumber, string chapterName, string sectionNumber,
            string sectionName, string questionNumber, string questionGUID, string questionText, string sequence)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = connection;

            string tableName = "TEMPLATE_QUESTIONS";

            //Проверяем наличие таблицы TEMPLATE_QUESTIONS
            if (!tableExists(tableName))
            {
                cmd.CommandText =
                    "create table "+tableName+" (ID counter Primary Key, TEMPLATE_GUID GUID,\n" +
                    "CHAPTER_NUMBER varchar(10), CHAPTER_NAME varchar(255), SECTION_NUMBER varchar(10),\n" +
                    "SECTION_NAME varchar(255), QUESTION_NUMBER varchar(10), QUESTION_GUID GUID,\n" +
                    "QUESTION_TEXT varchar(255), SEQUENCE varchar(10))";
                cmdExecute(cmd);
            }

            //Проверяем наличие вопроса
            cmd.CommandText = "select Count(QUESTION_GUID) from "+tableName+"\n" +
                "where QUESTION_GUID={" + questionGUID + "} \n" +
                "and TEMPLATE_GUID={" + Convert.ToString(templateGUID) + "}";
            int rslt = (int)cmd.ExecuteScalar();

            if (sequence.Length==0)
            {
                //Получаем значение из номера вопроса
                sequence = GetSequenceFromNumber(questionNumber);
            }

            if (rslt == 0)
            {
                //Такой записи нет
                cmd.CommandText = "insert into "+tableName+" (TEMPLATE_GUID,CHAPTER_NUMBER,CHAPTER_NAME,SECTION_NUMBER,\n" +
                    "SECTION_NAME,QUESTION_NUMBER,QUESTION_GUID,QUESTION_TEXT,SEQUENCE)\n" +
                    "values({" + Convert.ToString(templateGUID) + "},'" +
                               StrToSQLStr(chapterNumber) + "','" +
                               StrToSQLStr(chapterName) + "','" +
                               StrToSQLStr(sectionNumber) + "','" +
                               StrToSQLStr(sectionName) + "','" +
                               StrToSQLStr(questionNumber) + "',{" +
                               questionGUID + "},'" +
                               StrToSQLStr(questionText) + "','" +
                               StrToSQLStr(sequence) + "')";

                cmdExecute(cmd);

            }
            else
            {
                //Такая запись есть
                cmd.CommandText =
                    "update "+tableName+" set\n" +
                    "CHAPTER_NUMBER='" + StrToSQLStr(chapterNumber) + "'," +
                    "CHAPTER_NAME='" + StrToSQLStr(chapterName) + "'," +
                    "SECTION_NUMBER='" + StrToSQLStr(sectionNumber) + "'," +
                    "SECTION_NAME='" + StrToSQLStr(sectionName) + "'," +
                    "QUESTION_NUMBER='" + StrToSQLStr(questionNumber) + "'," +
                    "QUESTION_TEXT='" + StrToSQLStr(questionText) + "'," +
                    "SEQUENCE='" + StrToSQLStr(sequence) + "'\n" +
                    "where QUESTION_GUID={" + questionGUID + "}";
                cmdExecute(cmd);
            }

        }


        private void checkTemplates()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            if (!tableExists("TEMPLATES"))
            {
                //Создаем таблицу TEMPLATES
                cmd.CommandText =
                    "create table TEMPLATES (ID counter Primary key, TEMPLATE_GUID GUID, TYPE_CODE varchar(10), " +
                    "TITLE varchar(255), VERSION varchar(20))";
                cmdExecute(cmd);
            }

            if (!tableExists("TEMPLATE_QUESTIONS"))
            {
                cmd.CommandText =
                    "create table TEMPLATE_QUESTIONS (ID counter Primary Key, TEMPLATE_GUID GUID,\n" +
                    "CHAPTER_NUMBER varchar(10), CHAPTER_NAME varchar(255), SECTION_NUMBER varchar(10),\n" +
                    "SECTION_NAME varchar(255), QUESTION_NUMBER varchar(10), QUESTION_GUID GUID,\n" +
                    "QUESTION_TEXT varchar(255), SEQUENCE varchar(50))";
                cmdExecute(cmd);
            }

        }

 
        private int CheckFieldInTable(string tableName, string fieldName, string fieldString, bool create=true)
        {
            //Modified on 01.11.2016 in version 2.0.15.2
            //  Return integer value instead of boolean
            //  Return values:
            //      0 - field exists
            //      1 - new field created
            //      -1 - field was not created

            DataTable tablesColumns;
            DataRow[] foundRows;
            string localCommand;

            tablesColumns = connection.GetSchema("Columns", new string[] { null, null, tableName, fieldName });

            foundRows = tablesColumns.Select();

            if (foundRows.Length == 0)
            {
                if (create)
                {
                    try
                    {
                        localCommand = "alter table [" + tableName + "] add column [" + fieldName + "] " + fieldString;
                        OleDbCommand alterCalls = new OleDbCommand(localCommand, connection);
                        alterCalls.ExecuteNonQuery();

                        return 1;
                    }
                    catch
                    {
                        MessageBox.Show("Error during adding field '" + fieldName + "' in table '" + tableName + "'\nImport terminated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
                }
                else
                    return -1;
            }
            else
                return 0;

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            updateQuestions();
        }

        private void checkBox5_CheckedChanged_1(object sender, EventArgs e)
        {
            updateQuestions();
        }

        private string setTextFilter(string fieldName, string selector, string text, string altText="", string altOperator="")
        {
            string filter="";
            string opr = "";

            if (altOperator.Length == 0)
                opr = "or";
            else
                opr = altOperator;

            switch (selector)
            {
                case "Equal":
                    if (altText.Length == 0)
                        filter = fieldName + " like '" + StrToSQLStr(text) + "'";
                    else
                        filter="("+fieldName + " like '" + StrToSQLStr(text) + "' "+opr+" "+fieldName + " like '" + StrToSQLStr(altText) + "')";
                    break;
                case "Not Equal":
                    if (altText.Length == 0)
                        filter = fieldName + " not like '" + StrToSQLStr(text) + "'";
                    else
                        filter = "(" + fieldName + " not like '" + StrToSQLStr(text) + "' " + opr + " " + fieldName + " not like '" + StrToSQLStr(altText) + "')";
                    break;
                case "Starts from":
                    if (altText.Length == 0)
                        filter = fieldName + " like '" + StrToSQLStr(text) + "%'";
                    else
                        filter = "(" + fieldName + " like '" + StrToSQLStr(text) + "%' " + opr + " " + fieldName + " like '" + StrToSQLStr(altText) + "%')";
                    break;
                case "Not Starts from":
                    if (altText.Length == 0)
                        filter = fieldName + " not like '" + StrToSQLStr(text) + "%'";
                    else
                        filter = "(" + fieldName + " not like '" + StrToSQLStr(text) + "%' " + opr + " " + fieldName + " not like '" + StrToSQLStr(altText) + "%')";
                    break;
                case "Contains":
                    if (altText.Length == 0)
                        filter = fieldName + " like '%" + StrToSQLStr(text) + "%'";
                    else
                        filter = "(" + fieldName + " like '%" + StrToSQLStr(text) + "%' " + opr + " " + fieldName + " like '%" + StrToSQLStr(altText) + "%')";
                    break;
                case "Not Contains":
                    if (altText.Length == 0)
                        filter = fieldName + " not like '%" + StrToSQLStr(text) + "%'";
                    else
                        filter = "(" + fieldName + " not like '%" + StrToSQLStr(text) + "%' " + opr + " " + fieldName + " not like '%" + StrToSQLStr(altText) + "%')";
                    break;
                case "Ends on":
                    if (altText.Length == 0)
                        filter = fieldName + " like '%" + StrToSQLStr(text) + "'";
                    else
                        filter = "(" + fieldName + " like '%" + StrToSQLStr(text) + "' " + opr + " " + fieldName + " like '%" + StrToSQLStr(altText) + "')";
                    break;
                case "Not Ends on":
                    if (altText.Length == 0)
                        filter = fieldName + " not like '%" + StrToSQLStr(text) + "'";
                    else
                        filter = "(" + fieldName + " not like '%" + StrToSQLStr(text) + "' " + opr + " " + fieldName + " not like '%" + StrToSQLStr(altText) + "')";
                    break;
                case "Is Empty":
                    filter = "(IsNull(" + fieldName + ") or Len(" + fieldName + ")=0)";
                    break;
                case "Is Not Empty":
                    filter = "(not IsNull(" + fieldName + ") and Len(" + fieldName + ")>0)";
                    break;
            }

            return filter;
        }

        private string setIfTextFilter(string fieldName, string fieldNameAlt, string selector, string text, string altText = "", string altOperator = "")
        {
            string filter = "";
            string opr = "";

            if (altOperator.Length == 0)
                opr = "or";
            else
                opr = altOperator;

            switch (selector)
            {
                case "Equal":
                    if (altText.Length == 0)
                        filter = "IIF(IsNull(" + fieldName + ")," + fieldNameAlt + "='" + StrToSQLStr(text) + "'," + fieldName + "='" + StrToSQLStr(text) + "')";
                    else
                        filter = "IIF(IsNull("+fieldName+"),("+fieldNameAlt+"='"+StrToSQLStr(text)+"' "+opr+" "+fieldNameAlt+"='"+StrToSQLStr(altText)+"'),(" + fieldName + "='" + StrToSQLStr(text) + "' " + opr + " " + fieldName + "='" + StrToSQLStr(altText) + "'))";
                    break;
                case "Not Equal":
                    if (altText.Length == 0)
                        filter = "IIF(IsNull("+fieldName+"),"+fieldNameAlt+"<>'"+StrToSQLStr(text)+"',"+fieldName + " <> '" + StrToSQLStr(text) + "')";
                    else
                        filter = "IIF(IsNull("+fieldName+"),("+fieldNameAlt+"<>'"+StrToSQLStr(text)+"' "+opr+" "+fieldNameAlt+"<>'"+StrToSQLStr(altText)+"'),(" + fieldName + "<>'" + StrToSQLStr(text) + "' " + opr + " " + fieldName + "<>'" + StrToSQLStr(altText) + "'))";
                    break;
                case "Starts from":
                    if (altText.Length == 0)
                        filter = "IIF(IsNull("+fieldName+"),"+fieldNameAlt+" like '"+StrToSQLStr(text)+"%',"+fieldName + " like '" + StrToSQLStr(text) + "%')";
                    else
                        filter = "IIF(IsNull("+fieldName+"),("+fieldNameAlt+" like '"+StrToSQLStr(text)+"%' "+opr+" "+fieldNameAlt+" like '"+StrToSQLStr(altText)+"'),"+"(" + fieldName + " like '" + StrToSQLStr(text) + "%' " + opr + " " + fieldName + " like '" + StrToSQLStr(altText) + "%'))";
                    break;
                case "Not Starts from":
                    if (altText.Length == 0)
                        filter = "IIF(IsNull("+fieldName+"),"+fieldNameAlt+" not like '"+StrToSQLStr(text)+"%',"+fieldName + " not like '" + StrToSQLStr(text) + "%')";
                    else
                        filter = "IIF(IsNull("+fieldName+"),("+fieldNameAlt+" not like '"+StrToSQLStr(text)+"%' "+opr+" "+fieldNameAlt+" not like '"+StrToSQLStr(altText)+"'),(" + fieldName + " not like '" + StrToSQLStr(text) + "%' " + opr + " " + fieldName + " not like '" + StrToSQLStr(altText) + "%'))";
                    break;
                case "Contains":
                    if (altText.Length == 0)
                        filter = "IIF(IsNull("+fieldName+"),"+fieldNameAlt+" like '%"+StrToSQLStr(text)+"%',"+fieldName + " like '%" + StrToSQLStr(text) + "%')";
                    else
                        filter = "IIF(IsNull("+fieldName+"),("+fieldNameAlt+" like '%"+StrToSQLStr(text)+"%' "+opr+" "+fieldNameAlt+" like '%"+StrToSQLStr(altText)+"%'),(" + fieldName + " like '%" + StrToSQLStr(text) + "%' " + opr + " " + fieldName + " like '%" + StrToSQLStr(altText) + "%'))";
                    break;
                case "Not Contains":
                    if (altText.Length == 0)
                        filter = "IIF(IsNull("+fieldName+"),"+fieldNameAlt+" not like '%"+StrToSQLStr(text)+"%',"+fieldName + " not like '%" + StrToSQLStr(text) + "%')";
                    else
                        filter = "IIF(IsNull("+fieldName+"),("+fieldNameAlt+" not like '%"+StrToSQLStr(text)+"%' "+opr+" "+fieldNameAlt+" not like '%"+StrToSQLStr(altText)+"%'),"+"(" + fieldName + " not like '%" + StrToSQLStr(text) + "%' " + opr + " " + fieldName + " not like '%" + StrToSQLStr(altText) + "%'))";
                    break;
                case "Ends on":
                    if (altText.Length == 0)
                        filter = "IIF(IsNull(" + fieldName + ")," + fieldNameAlt + " like '%" + StrToSQLStr(text) + "'," + fieldName + " like '%" + StrToSQLStr(text) + "')";
                    else
                        filter = "IIF(IsNull(" + fieldName + "),(" + fieldNameAlt + " like '%" + StrToSQLStr(text) + "' " + opr + " " + fieldNameAlt + " like '%" + StrToSQLStr(altText) + "'),(" + fieldName + " like '%" + StrToSQLStr(text) + "' " + opr + " " + fieldName + " like '%" + StrToSQLStr(altText) + "'))";
                    break;
                case "Not Ends on":
                    if (altText.Length == 0)
                        filter = "IIF(IsNull(" + fieldName + ")," + fieldNameAlt + " not like '%" + StrToSQLStr(text) + "'," + fieldName + " not like '%" + StrToSQLStr(text) + "')";
                    else
                        filter = "IIF(IsNull(" + fieldName + "),(" + fieldNameAlt + " not like '%" + StrToSQLStr(text) + "' " + opr + " " + fieldNameAlt + " not like '%" + StrToSQLStr(altText) + "'),(" + fieldName + " not like '%" + StrToSQLStr(text) + "' " + opr + " " + fieldName + " not like '%" + StrToSQLStr(altText) + "'))";
                    break;
                case "Is Empty":
                    filter = "IIF(IsNull(" + fieldName + "),IIF(IsNull("+fieldNameAlt+"),True,Len("+fieldNameAlt+")=0),Len(" + fieldName + ")=0)";
                    break;
                case "Is Not Empty":
                    filter = "IIF(IsNull(" + fieldName + "),IIF(IsNull("+fieldNameAlt+"),False,Len("+fieldNameAlt+")>0),Len(" + fieldName + ")>0)";
                    break;
            }

            return filter;
        }

        /*
        private string setDateFilter(string fieldName, string selector, DateTime date1, DateTime date2)
        {
            string filter = "";

            switch (selector)
            {
                case "Equal":
                    filter = fieldName + "=" + DateTimeToQueryStr(date1);
                    break;
                case "Not Equal":
                    filter = fieldName + "<>" + DateTimeToQueryStr(date1);
                    break;
                case "Between":
                    filter = fieldName + " between " + DateTimeToQueryStr(date1)+" and "+DateTimeToQueryStr(date2);
                    break;
                case "Not Between":
                    filter = fieldName + " not between " + DateTimeToQueryStr(date1) + " and " + DateTimeToQueryStr(date2);
                    break;
                case "Before":
                    filter = fieldName + "<" + DateTimeToQueryStr(date1);
                    break;
                case "After":
                    filter = fieldName + ">" + DateTimeToQueryStr(date1);
                    break;
                case "On and Before":
                    filter = fieldName + "<=" + DateTimeToQueryStr(date1);
                    break;
                case "On and After":
                    filter = fieldName + ">=" + DateTimeToQueryStr(date1);
                    break;
            }

            return filter;
        }
        
        */

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void setFormsFont(Font cFont)
        {
            this.Font = cFont;

            mainFont = cFont;

            menuStrip1.Font = cFont;

        }

        private void loadFonts()
        {
            //Load font properties from database
            string fontName = "Tahoma";
            float fontSize = 10;
            bool fontBold = false;
            bool fontItalic = false;
            bool fontUnderline = false;
            bool fontStrikeout = false;
            FontStyle fontStyle = FontStyle.Regular;

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = connection;
            cmd.CommandText = 
                "select * from FONTS";

            OleDbDataReader fonts = cmd.ExecuteReader();

            if (fonts.HasRows)
            {
                while (fonts.Read())
                {
                    fontName = fonts["FONT_NAME"].ToString();
                    fontSize = Convert.ToSingle(fonts["FONT_SIZE"]);
                    fontBold = Convert.ToBoolean(fonts["BOLD"]);
                    fontItalic = Convert.ToBoolean(fonts["ITALIC"]);
                    fontUnderline = Convert.ToBoolean(fonts["UNDERLINE"]);
                    fontStrikeout = Convert.ToBoolean(fonts["STRIKEOUT"]);

                }
            }

            if ((!isPowerUser && anyUserMayChangeFont) || (isPowerUser && !oneFontForPowerUsers))
            {
                

                IniFile iniFile = new IniFile(iniPersonalFile);

                string section = "COMMON_FONT";

                fontName = iniFile.ReadString(section, "FONT_NAME", fontName);
                fontSize = Convert.ToSingle(iniFile.ReadString(section, "FONT_SIZE", fontSize.ToString()));
                fontBold = iniFile.ReadBoolean(section, "FONT_BOLD", fontBold);
                fontItalic = iniFile.ReadBoolean(section, "FONT_ITALIC", fontItalic);
                fontUnderline = iniFile.ReadBoolean(section, "FONT_UNDERLINE", fontUnderline);
                fontStrikeout = iniFile.ReadBoolean(section, "FONT_STRIKEOUT", fontStrikeout);
            }

            if (fontBold)
                fontStyle |= FontStyle.Bold;

            if (fontItalic)
                fontStyle |= FontStyle.Italic;

            if (fontUnderline)
                fontStyle |= FontStyle.Underline;

            if (fontStrikeout)
                fontStyle |= FontStyle.Strikeout;

            Font cFont = new Font(fontName, fontSize, fontStyle);

            setFormsFont(cFont);

        }

        private void saveFont(string Component, Font cFont)
        {
            string fontName = cFont.FontFamily.Name;
            float fontSize = cFont.Size;
            bool fBold = cFont.Bold;
            bool fItalic = cFont.Italic;
            bool fUnderline = cFont.Underline;
            bool fStrikeout = cFont.Strikeout;

            
            if ((!isPowerUser && anyUserMayChangeFont) || (isPowerUser && !oneFontForPowerUsers))
            {
                IniFile iniFile = new IniFile(iniPersonalFile);

                string section = "COMMON_FONT";

                iniFile.Write(section, "FONT_NAME", fontName);
                iniFile.Write(section, "FONT_SIZE", fontSize.ToString());
                iniFile.Write(section, "FONT_BOLD", fBold);
                iniFile.Write(section, "FONT_ITALIC", fItalic);
                iniFile.Write(section, "FONT_UNDERLINE", fUnderline);
                iniFile.Write(section, "FONT_STRIKEOUT", fStrikeout);

                return;
            }

            if (isPowerUser)
            {
                OleDbCommand cmd = new OleDbCommand("", connection);
                cmd.CommandText =
                    "select Count(Component) \n"+
                    "from FONTS \n"+
                    "where Component='" + Component + "'";

                int count = (int)cmd.ExecuteScalar();

                if (count > 0)
                {
                    cmd.CommandText =
                        "update FONTS set\n" +
                        "FONT_NAME='" + StrToSQLStr(fontName) + "',\n" +
                        "FONT_SIZE=" + FloatToSQLStr(fontSize) + ",\n" +
                        "BOLD=" + Convert.ToString(fBold) + ",\n" +
                        "ITALIC=" + Convert.ToString(fItalic) + ",\n" +
                        "UNDERLINE=" + Convert.ToString(fUnderline) + ",\n" +
                        "STRIKEOUT=" + Convert.ToString(fStrikeout) + "\n" +
                        "where COMPONENT='" + StrToSQLStr(Component) + "'";
                    cmdExecute(cmd);
                }
                else
                {
                    cmd.CommandText =
                        "insert into FONTS (COMPONENT, FONT_NAME, FONT_SIZE, BOLD, ITALIC, UNDERLINE, STRIKEOUT)\n" +
                        "values('" + StrToSQLStr(Component) + "','" +
                                   StrToSQLStr(fontName) + "'," +
                                   FloatToSQLStr(fontSize) + "," +
                                   Convert.ToString(fBold) + "," +
                                   Convert.ToString(fItalic) + "," +
                                   Convert.ToString(fUnderline) + "," +
                                   Convert.ToString(fStrikeout) + ")";
                    cmdExecute(cmd);
                }


            }

        }

        private string FloatToSQLStr(float inValue)
        {
            string s = Convert.ToString(inValue);
            s = s.Replace(",", ".");
            return s;
        }

        private void btnEditReport_Click(object sender, EventArgs e)
        {
            editReportDetails();
        }

        private void btnNewReport_Click(object sender, EventArgs e)
        {
            //Modified on 12.10.2016 in version 2.0.15.1
            //  Move all code to separate procedure

            CreateNewReport();
        }

        private void CreateNewReport()
        {
            //Add new report
            //Modified on 07.10.2016 in version 2.0.15.1
            //  Always create new record in REPORTS table on create new record
            //  Use programID for temp report code
            //  Delete temp record if used refused to create new report
            //Modified on 12.10.2016 in version 2.0.15.1
            //  Move code to separate procedure
            //  Stop using public components
            //  Using public properties of the form
            //  Save report in the form

            OleDbCommand cmd = new OleDbCommand("", connection);

            //Delete records with temp report code
            cmd.CommandText =
                "delete from REPORTS \n" +
                "where REPORT_CODE='" + programID + "' \n"+
                "or LEN(REPORT_CODE)=0";

            if (cmdExecute(cmd)<0)
            {
                MessageBox.Show(
                    "Fail to clean reports table from temporary records",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            //Create new record for report
            cmd.CommandText =
                "insert into REPORTS (REPORT_CODE) \n" +
                "values('" + programID + "')";

            if (cmdExecute(cmd)<0)
            {
                MessageBox.Show(
                    "Fail to create new report",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            //Get report ID
            cmd.CommandText =
                "select Count(REPORT_CODE) as RecCount \n" +
                "from REPORTS \n" +
                "where REPORT_CODE='" + programID + "'";

            int newReportID = (int)cmd.ExecuteScalar();

            if (newReportID == 0)
            {
                MessageBox.Show(
                    "Fail to create new report",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            //Delete temp report code
            cmd.CommandText =
                "update REPORTS set \n" +
                "REPORT_CODE='' \n" +
                "where REPORT_CODE='" + programID + "'";

            if (cmdExecute(cmd)<0)
            {
                MessageBox.Show(
                    "Fail to update new report",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            FormReportSummary form = new FormReportSummary(true, "", false, false);

            var rslt = form.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                //Locate new report

                fillInspectors();

                DS.Tables["REPORTS"].Clear();

                reportsAdapter.Fill(DS, "REPORTS");

                MainForm.LocateGridRecord(form.reportCode, "REPORT_CODE", -1, dgvInspections);

                toolStripStatusLabel4.Text = dgvInspections.Rows.Count.ToString();

            }
            else
            {
                //Delete created record

                cmd.CommandText =
                    "delete from REPORTS \n" +
                    "where REPORT_CODE='" + programID+"'";
                cmdExecute(cmd);
            }

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //Delete report
            int curRow = dgvInspections.CurrentCell.RowIndex;
            int curCol = dgvInspections.CurrentCell.ColumnIndex;

            //if (Convert.ToBoolean(dgvInspections.CurrentRow.Cells["MANUAL"].Value))
            {
                string s = "You are going to delete following reports:\n\n" +
                    "Report code : " + dgvInspections.CurrentRow.Cells["REPORT_CODE"].Value.ToString() + "\n" +
                    "Vessel : " + dgvInspections.CurrentRow.Cells["VESSEL_NAME"].Value.ToString() + "\n" +
                    "Date of inspection : " + dgvInspections.CurrentRow.Cells["INSPECTION_DATE"].Value + "\n\n" +
                    "Would you like to proceed?";
                var rslt = System.Windows.Forms.MessageBox.Show(s, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (rslt == DialogResult.Yes)
                {
                    //Delete report
                    OleDbCommand cmd = new OleDbCommand("", connection);

                    string reportCode=dgvInspections.CurrentRow.Cells["REPORT_CODE"].Value.ToString();

                    OleDbTransaction transaction;
                    transaction = connection.BeginTransaction();

                    cmd.Transaction = transaction;

                    try
                    {
                        cmd.CommandText =
                            "delete from REPORT_ITEMS where REPORT_CODE='" + StrToSQLStr(reportCode) + "'";
                        cmdExecute(cmd);

                        cmd.CommandText =
                            "delete from REPORTS where REPORT_CODE='" + StrToSQLStr(reportCode) + "'";
                        cmdExecute(cmd);

                        transaction.Commit();

                        DS.Tables["REPORTS"].Clear();
                        reportsAdapter.Fill(DS, "REPORTS");

                        fillInspectors();
                        fillVessels();
                        fillReports();

                        if (dgvInspections.Rows.Count > 0)
                        {
                            if (dgvInspections.Rows.Count >= curRow)
                                dgvInspections.CurrentCell = dgvInspections[curCol, curRow];
                            else
                                dgvInspections.CurrentCell = dgvInspections[curCol, dgvInspections.Rows.Count - 1];
                        }

                        toolStripStatusLabel4.Text = dgvInspections.Rows.Count.ToString();
                    }
                    catch (Exception E)
                    {
                        if ((transaction != null) && (transaction.Connection != null))
                        {
                            transaction.Rollback();
                            System.Windows.Forms.MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            //else
            //    System.Windows.Forms.MessageBox.Show("You are unanble to delete imported report", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private void getShowReports()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select STR_VALUE from Options\n" +
                "where TAG=1";
            string value = (string)cmd.ExecuteScalar();

        }

        private void getShowVIQ()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select STR_VALUE from Options\n" +
                "where TAG=2";
            string value = (string)cmd.ExecuteScalar();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            tempTablesClear();

            try
            {
                connection.Close();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            while (connection.State != ConnectionState.Closed)
            {
                Thread.Sleep(300);
            }

            connection.Dispose();
            connection = null;
            

            bool fileCopied = false;

            if (isPowerUser) // || isPowerUserNew)
            {
                //Copy database from temp folder to application folder
                fileCopied = SaveDatabase();

            }
            else
                fileCopied = true;

            if (fileCopied)
            {
                try
                {
                    Directory.Delete(appTempFolderName, true);
                }
                catch //(Exception E)
                {
                    //Do nothing
                    //MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private bool SaveDatabase()
        {
            bool fileCopied = false;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                try
                {
                    //File.Copy(tempDBFileName, dbFullName, true);

                    ExecuteCommand("copy \"" + tempDBFileName + "\" \"" + dbFullName + "\" /Y");

                    fileCopied = true;
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (!fileCopied)
                {
                    string newFileName = GetNewFileName(dbFullName);

                    while (File.Exists(newFileName))
                    {
                        newFileName = GetNewFileName(dbFullName);
                    }

                    try
                    {
                        //File.Copy(tempDBFileName, newFileName, true);

                        ExecuteCommand("copy \"" + tempDBFileName + "\" \"" + newFileName + "\" /Y");

                        fileCopied = true;

                        string message = "It was impossible to replace file \"" + dbFullName + "\" by file \"" + tempDBFileName + "\" so it was copied to file \"" + newFileName + "\". \n" +
                            "You should rename it to \"" + Path.GetFileName(dbFullName) + "\" at the first opportunity.";

                        MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        string message = "It is impossible to copy file \"" + tempDBFileName + "\" to \"" + newFileName + "\". \n" +
                            "You should copy file manually to avoid data lost.";

                        MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            return fileCopied;
        }

        private bool IsPowerUser()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);
            cmd.CommandText =
                "select Count(USER_NAME) as recs \n" +
                "from POWER_USERS \n" +
                "where USER_NAME='" + StrToSQLStr(userName) + "'";

            int recs = (int)cmd.ExecuteScalar();

            if (recs > 0)
                return true;
            else
                return false;
        }

        private string GetNewFileName(string fileName)
        {
            return Path.Combine(Path.GetDirectoryName(fileName), Path.GetFileNameWithoutExtension(fileName) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "." + Path.GetExtension(fileName));
        }
    

        private void toolStripComboBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void toolStripComboBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private string getVesselIMO(string vesselName)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);
            cmd.CommandText =
                "select Count(VESSEL_IMO) from VESSELS\n" +
                "where VESSEL_NAME like '" + vesselName + "'";

            int recs = (int)cmd.ExecuteScalar();
            string imo = "";

            switch (recs)
            {
                case 0:
                    return "";
                case 1:
                    cmd.CommandText=
                        "select VESSEL_IMO from VESSELS\n" +
                        "where VESSEL_NAME like '" + vesselName + "'";
                    imo = (string)cmd.ExecuteScalar();
                    return imo;
                default:
                    cmd.CommandText=
                        "select VESSEL_IMO from VESSELS\n" +
                        "where VESSEL_NAME like '" + vesselName + "'";
                    imo = (string)cmd.ExecuteScalar();
                    return imo;
            }
        }

        private bool updateVessel(ref string vesselIMO, ref string vesselName,string reportCode)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            //Проверяем данные судна
            cmd.CommandText =
                "select Count(VESSEL_IMO) from Vessels\n" +
                "where VESSEL_IMO='" + StrToSQLStr(vesselIMO) + "'\n" +
                "and VESSEL_NAME like '" + StrToSQLStr(vesselName) + "'";

            int recs = (int)cmd.ExecuteScalar();

            if (recs > 0)
            {
                //Есть похожие записи
                //Проверяем имеются ли запись с таким же номером и названием судна
                cmd.CommandText =
                "select Count(VESSEL_IMO) from Vessels\n" +
                "where VESSEL_IMO='" + StrToSQLStr(vesselIMO) + "'\n" +
                "and VESSEL_NAME='" + StrToSQLStr(vesselName) + "'";
                int imoRecs = (int)cmd.ExecuteScalar();

                if (imoRecs > 0)
                    //Выходим из процедуры
                    return true;
                else
                {
                    //Имеются записи с таким же номером IMO и другим названием судна
                    cmd.CommandText =
                        "select VESSEL_IMO, VESSEL_NAME\n" +
                        "from VESSELS\n" +
                        "where VESSEL_IMO like '" + StrToSQLStr(vesselIMO) + "'\n" +
                        "and VESSEL_NAME like '" + StrToSQLStr(vesselName) + "'\n" +
                        "order by VESSEL_NAME";
                    OleDbDataReader vslsReader = cmd.ExecuteReader();
                    ArrayList vslsList = new ArrayList();
                    string msg = "";

                    if (vslsReader.HasRows)
                    {
                        while (vslsReader.Read())
                        {
                            vslsList.Add("IMO number : " + vslsReader[0].ToString() + " Vessel name : " + vslsReader[1].ToString());
                        }

                        if (vslsList.Count == 1)
                        {
                            msg =
                                "Report \"" + reportCode + "\" was created for vessel \"" + vesselName + "\" with IMO number \"" + vesselIMO + "\".\n" +
                                "However there is a record for the vessel with the same IMO number but another vessel name:";
                        }
                        else
                        {
                            msg =
                                "Report \"" + reportCode + "\" was created for then vessel \"" + vesselName + "\" with IMO number \"" + vesselIMO + "\".\n" +
                                "However there are several records for the vessels with the same IMO number but another vessel name:";
                        }

                        vslsReader.Close();
                        SelectVesselForm frm5 = new SelectVesselForm(msg, connection, cmd.CommandText, DS, this.Font, this.Icon);

                        var rslt = frm5.ShowDialog();

                        switch (rslt)
                        {
                            case DialogResult.Yes:
                                //Добавляем новое судно в список судов
                                insertNewVessel(vesselName, vesselIMO);
                                return true;
                            case DialogResult.No:
                                //Изменяем название судна с таблице отчетов
                                updateReportVessel(GetVesselGuid(vesselName, vesselIMO), 
                                    GetVesselGuid(frm5.dataGridView1.CurrentRow.Cells["VESSEL_NAME"].Value.ToString(),
                                    frm5.dataGridView1.CurrentRow.Cells["VESSEL_IMO"].Value.ToString()));

                                vesselName = frm5.dataGridView1.CurrentRow.Cells["VESSEL_NAME"].Value.ToString();
                                vesselIMO = frm5.dataGridView1.CurrentRow.Cells["VESSEL_IMO"].Value.ToString();
                                return true;
                            case DialogResult.Ignore:
                                //Изменяем название судна в таблице судов
                                updateListVessel(frm5.dataGridView1.CurrentRow.Cells["VESSEL_NAME"].Value.ToString(),
                                    frm5.dataGridView1.CurrentRow.Cells["VESSEL_IMO"].Value.ToString(),
                                    vesselName, vesselIMO);
                                return true;
                            case DialogResult.Abort:
                                return false;
                            default:
                                return false;
                        }

                    }
                    else
                    {
                        vslsReader.Close();
                        return false;
                    }

                }
            }
            else
            {
                //Похожих записей нет
                //Проверяем есть ли записи с таким же номером IMO

                cmd.CommandText =
                    "select Count(VESSEL_IMO) from Vessels\n" +
                    "where VESSEL_IMO='" + StrToSQLStr(vesselIMO) + "'";

                int imoRecs = (int)cmd.ExecuteScalar();

                if (imoRecs > 0)
                {
                    //Есть записи с таким же номером ИМО
                    string msg = "";

                    if (imoRecs == 1)
                    {
                        msg =
                            "Report \"" + reportCode + "\" was created for vessel \"" + vesselName + "\" with IMO number \"" + vesselIMO + "\".\n" +
                            "There is not vessel with the such name in database but there is vessel with the same IMO number:";
                        cmd.CommandText =
                            "select VESSEL_IMO, VESSEL_NAME\n" +
                            "from VESSELS\n" +
                            "where VESSEL_IMO='" + StrToSQLStr(vesselIMO) + "'";
                    }
                    else
                    {
                        msg =
                            "Report \"" + reportCode + "\" was created for vessel \"" + vesselName + "\" with IMO number \"" + vesselIMO + "\".\n" +
                            "There is not vessel with the such name in database but there are a few vessels with the same IMO number:";
                        cmd.CommandText =
                            "select VESSEL_IMO, VESSEL_NAME\n" +
                            "from VESSELS\n" +
                            "where VESSEL_IMO='" + StrToSQLStr(vesselIMO) + "'\n" +
                            "order by VESSEL_NAME";
                    }

                    SelectVesselForm frm5 = new SelectVesselForm(msg, connection, cmd.CommandText, DS, this.Font, this.Icon);

                    var rslt = frm5.ShowDialog();

                    switch (rslt)
                    {
                        case DialogResult.Yes:
                            //Добавляем новое судно
                            insertNewVessel(vesselName, vesselIMO);
                            return true;
                        case DialogResult.No:
                            //Изменяем название судна с таблице отчетов
                            updateReportVessel(GetVesselGuid(vesselName, vesselIMO),
                                GetVesselGuid(frm5.dataGridView1.CurrentRow.Cells["VESSEL_NAME"].Value.ToString(),
                                frm5.dataGridView1.CurrentRow.Cells["VESSEL_IMO"].Value.ToString()));

                            vesselName = frm5.dataGridView1.CurrentRow.Cells["VESSEL_NAME"].Value.ToString();
                            vesselIMO = frm5.dataGridView1.CurrentRow.Cells["VESSEL_IMO"].Value.ToString();
                            return true;
                        case DialogResult.Ignore:
                            //Изменяем название судна в таблице судов
                            updateListVessel(frm5.dataGridView1.CurrentRow.Cells["VESSEL_NAME"].Value.ToString(),
                                frm5.dataGridView1.CurrentRow.Cells["VESSEL_IMO"].Value.ToString(),
                                vesselName, vesselIMO);
                            return true;
                        case DialogResult.Abort:
                            //Отказ от импорта
                            return false;
                        default:
                            return false;
                    }
                }
                else
                {
                    //Похожих записей не обнаружено

                    cmd.CommandText=
                        "select Count(VESSEL_NAME) from Vessels\n" +
                        "where VESSEL_NAME like '" + StrToSQLStr(vesselName) + "'";

                    int recsName =(int) cmd.ExecuteScalar();

                    if (recsName > 0)
                    {
                        //Имеются записи с таким же именем судна
                        string msg = "";

                        if (recsName == 1)
                        {
                            //Имеется одна запись с таким же имененм судна, но с другим номером ИМО
                            msg=
                                "Report \"" + reportCode + "\" was created for vessel \"" + vesselName + "\" with IMO number \"" + vesselIMO + "\".\n" +
                                "There is not vessel with the such IMO number in database but there is vessel with the same name:";
                        }
                        else
                        {
                            //Имеется несколько записей с таким же именем судна, нос другими номерами ИМО
                            msg =
                                "Report \"" + reportCode + "\" was created for vessel \"" + vesselName + "\" with IMO number \"" + vesselIMO + "\".\n" +
                                "There is not vessel with the such IMO number in database but there are a few vessels with the same name:";
                        }

                        cmd.CommandText =
                            "select VESSEL_IMO, VESSEL_NAME\n" +
                            "from VESSELS\n" +
                            "where VESSEL_NAME like '" + StrToSQLStr(vesselName) + "'\n" +
                            "order by VESSEL_NAME";

                        SelectVesselForm frm5 = new SelectVesselForm(msg, connection, cmd.CommandText, DS, this.Font, this.Icon);

                        var rslt = frm5.ShowDialog();

                        switch (rslt)
                        {
                            case DialogResult.Yes:
                                //Добавляем новое судно
                                insertNewVessel(vesselName, vesselIMO);
                                return true;
                            case DialogResult.No:
                                //Изменяем название судна с таблице отчетов
                                updateReportVessel(GetVesselGuid(vesselName, vesselIMO),
                                    GetVesselGuid(frm5.dataGridView1.CurrentRow.Cells["VESSEL_NAME"].Value.ToString(),
                                    frm5.dataGridView1.CurrentRow.Cells["VESSEL_IMO"].Value.ToString()));

                                vesselName = frm5.dataGridView1.CurrentRow.Cells["VESSEL_NAME"].Value.ToString();
                                vesselIMO = frm5.dataGridView1.CurrentRow.Cells["VESSEL_IMO"].Value.ToString();
                                return true;
                            case DialogResult.Ignore:
                                //Изменяем название судна в таблице судов
                                updateListVessel(frm5.dataGridView1.CurrentRow.Cells["VESSEL_NAME"].Value.ToString(),
                                    frm5.dataGridView1.CurrentRow.Cells["VESSEL_IMO"].Value.ToString(),
                                    vesselName, vesselIMO);
                                return true;
                            case DialogResult.Abort:
                                //Отказ от импорта
                                return false;
                            default:
                                return false;
                        }
                    }
                    else
                    {
                        //Нет записей с таким же именем судна
                        insertNewVessel(vesselName, vesselIMO);
                        return true;
                    }
                }
            }

        }

        private void insertNewVessel(string vesselName, string vesselIMO)
        {
            //Добавляем новое судно в список судов
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "insert into VESSELS (VESSEL_IMO,VESSEL_NAME)\n" +
                "values ('" + vesselIMO + "','" + StrToSQLStr(vesselName) + "')";
            cmdExecute(cmd);

        }

        private void updateReportVessel(Guid oldVesselGuid, Guid newVesselGuid)
        {
            //Изменяем название судна с таблице отчетов
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "update REPORTS set\n" +
                "VESSEL_GUID="+GuidToStr(newVesselGuid)+" \n"+
                "where \n" +
                "VESSEL_GUID=" + GuidToStr(oldVesselGuid);
            cmdExecute(cmd);
        }

        private void updateListVessel(string oldVesselName, string oldVesselIMO, string newVesselName, string newVesselIMO)
        {
            //Изменяем запись судна в таблице судов
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "update VESSELS set\n" +
                "VESSEL_NAME='" + StrToSQLStr(newVesselName) + "',\n" +
                "VESSEL_IMO='" + StrToSQLStr(newVesselIMO) + "'\n" +
                "where VESSEL_NAME='" + StrToSQLStr(oldVesselName) + "'\n" +
                "and VESSEL_IMO='" + StrToSQLStr(oldVesselIMO) + "'";
            cmdExecute(cmd);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
        }

        private void vesselsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void inspectorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {
            updateQuestions();
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void setFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showFilterForm();
        }

        private void toolStripSplitButton1_Click(object sender, EventArgs e)
        {
            showFilterForm();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            updateQuestions();
        }

        private void showFilterForm()
        {
            this.Cursor = Cursors.WaitCursor;

            FilterForm filterForm = new FilterForm(connection, DS, this.Font, this.Icon);

            if (!filterUsed)
            {
                setFilterDefaults();
                questionsMapping = defUseMapping.Show;
                updateGridOnFilterChange = defUpdateOnClose.Show;

                if (defUseDefault.Show)
                    filterForm.useDefault = true;
                else
                    filterForm.lastSetting = true;
            }
            else
            {
                if (useLastSetting)
                {
                    filterForm.lastSetting = true;
                }
                else
                {
                    setFilterDefaults();
                    questionsMapping = defUseMapping.Show;
                    updateGridOnFilterChange = defUpdateOnClose.Show;
                    filterForm.useDefault = defUseDefault.Show;
                }
            }

            filterForm.showVessel = qfVessel.show;
            filterForm.showInspector = qfInspector.show;
            filterForm.showReportNumber = qfReportNumber.show;
            filterForm.showQuestionNumber = qfQuestionNumber.show;
            filterForm.showObservation = qfObservation.show;
            filterForm.showInspectorComments = qfInspectorComments.show;
            filterForm.showTechnicalComments = qfTechnicalComments.show;
            filterForm.showOperatorComments = qfOperatorComments.show;
            filterForm.showQuestionGUID = qfQuestionGUID.show;
            filterForm.showDate = qfDate.show;
            filterForm.showCompany = qfCompany.show;
            filterForm.showPort = qfPort.show;
            filterForm.showOffice = qfOffice.show;
            filterForm.showDOC = qfDOC.show;
            filterForm.showHullClass = qfHullClass.show;
            filterForm.showMaster = qfMaster.show;
            filterForm.showChiefEngineer = qfChiefEngineer.show;
            filterForm.showSubchapter = qfSubchapter.show;
            filterForm.showKeyWords = qfKeyWords.show;
            filterForm.showQuestionnaireType = qfQuestionnaireType.show;
            filterForm.showFleet = qfFleet.show;

            filterForm.conditionVessel = qfVessel.condition;
            filterForm.conditionInspector = qfInspector.condition;
            filterForm.conditionQuestionNumber = qfQuestionNumber.condition;
            filterForm.conditionReportNumber = qfReportNumber.condition;
            filterForm.conditionObservation = qfObservation.condition;
            filterForm.conditionInspectorComments = qfInspectorComments.condition;
            filterForm.conditionTechnicalComments = qfTechnicalComments.condition;
            filterForm.conditionOperatorComments = qfOperatorComments.condition;
            filterForm.conditionQuestionGUID = qfQuestionGUID.condition;
            filterForm.conditionDate = qfDate.condition;
            filterForm.conditionCompany = qfCompany.condition;
            filterForm.conditionPort = qfPort.condition;
            filterForm.conditionOffice = qfOffice.condition;
            filterForm.conditionDOC = qfDOC.condition;
            filterForm.conditionHullClass = qfHullClass.condition;
            filterForm.conditionMaster = qfMaster.condition;
            filterForm.conditionChiefEngineer = qfChiefEngineer.condition;
            filterForm.conditionSubchapter = qfSubchapter.condition;
            filterForm.conditionKeyWords = qfKeyWords.condition;
            filterForm.conditionQuestionnaireType = qfQuestionnaireType.condition;
            filterForm.conditionFleet = qfFleet.condition;

            filterForm.valueVessel = qfVessel.value;
            filterForm.valueInspector = qfInspector.value;
            filterForm.valueReportNumber = qfReportNumber.value;
            filterForm.valueQuestionNumber = qfQuestionNumber.value;
            filterForm.valueObservation = qfObservation.value;
            filterForm.valueInspectorComments = qfInspectorComments.value;
            filterForm.valueTechnicalComments = qfTechnicalComments.value;
            filterForm.valueOperatorComments = qfOperatorComments.value;
            filterForm.valueQuestionGUID = qfQuestionGUID.value;
            filterForm.valueDate = qfDate.value;
            filterForm.valueDate2 = qfDate.value2;
            filterForm.valueCompany = qfCompany.value;
            filterForm.valuePort = qfPort.value;
            filterForm.valueOffice = qfOffice.value;
            filterForm.valueDOC = qfDOC.value;
            filterForm.valueHullClass = qfHullClass.value;
            filterForm.valueMaster = qfMaster.value;
            filterForm.valueChiefEngineer = qfChiefEngineer.value;
            filterForm.valueSubchapter = qfSubchapter.value;
            filterForm.valueKeyWords = qfKeyWords.value;
            filterForm.valueQuestionnaireType = qfQuestionnaireType.value;
            filterForm.valueFleet = qfFleet.value;

            filterForm.useMapping = questionsMapping;
            filterForm.typesBox = qfQuestionNumber.typeCode;

            if (sinchronizeFilter)
            {
                if (qfVessel.blocked)
                    filterForm.labelVessel = qfVessel.label+"*";

                if (qfInspector.blocked)
                    filterForm.labelInspector = qfInspector.label + "*";

                if (qfReportNumber.blocked)
                    filterForm.labelReportNumber = qfReportNumber.label + "*";

                if (qfDate.blocked)
                    filterForm.labelDate = qfDate.label + "*";

                if (qfCompany.blocked)
                    filterForm.labelCompany = qfCompany.label + "*";

                if (qfPort.blocked)
                    filterForm.labelPort = qfPort.label + "*";

                if (qfOffice.blocked)
                    filterForm.labelOffice = qfOffice.label + "*";

                if (qfDOC.blocked)
                    filterForm.labelDOC = qfDOC.label + "*";

                if (qfHullClass.blocked)
                    filterForm.labelHullClass = qfHullClass.label + "*";

                if (qfMaster.blocked)
                    filterForm.labelMaster = qfHullClass.label + "*";

                if (qfChiefEngineer.blocked)
                    filterForm.labelChiefEngneer = qfChiefEngineer.label + "*";

                if (qfQuestionnaireType.blocked)
                    filterForm.labelQuestionnaireType = qfQuestionnaireType.label + "*";

                if (qfFleet.blocked)
                    filterForm.labelFleet = qfFleet.label + "*";
            }

            filterForm.updateOnClosed = updateGridOnFilterChange;

            this.Cursor = Cursors.Default;

            var rslt=filterForm.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                filterUsed = true;
                useLastSetting = filterForm.lastSetting;

                qfVessel.show=filterForm.showVessel;
                qfInspector.show=filterForm.showInspector;
                qfReportNumber.show=filterForm.showReportNumber;
                qfQuestionNumber.show = filterForm.showQuestionNumber;
                qfObservation.show = filterForm.showObservation;
                qfInspectorComments.show = filterForm.showInspectorComments;
                qfTechnicalComments.show = filterForm.showTechnicalComments;
                qfOperatorComments.show=filterForm.showOperatorComments;
                qfQuestionGUID.show=filterForm.showQuestionGUID;
                qfDate.show=filterForm.showDate;
                qfCompany.show=filterForm.showCompany;
                qfPort.show=filterForm.showPort;
                qfOffice.show=filterForm.showOffice;
                qfDOC.show = filterForm.showDOC;
                qfHullClass.show=filterForm.showHullClass;
                qfMaster.show = filterForm.showMaster;
                qfChiefEngineer.show = filterForm.showChiefEngineer;
                qfSubchapter.show = filterForm.showSubchapter;
                qfKeyWords.show = filterForm.showKeyWords;
                qfQuestionnaireType.show = filterForm.showQuestionnaireType;
                qfFleet.show = filterForm.showFleet;

                qfVessel.condition=filterForm.conditionVessel;
                qfInspector.condition= filterForm.conditionInspector;
                qfQuestionNumber.condition=filterForm.conditionQuestionNumber;
                qfReportNumber.condition=filterForm.conditionReportNumber;
                qfObservation.condition=filterForm.conditionObservation;
                qfInspectorComments.condition=filterForm.conditionInspectorComments;
                qfTechnicalComments.condition=filterForm.conditionTechnicalComments;
                qfOperatorComments.condition=filterForm.conditionOperatorComments;
                qfQuestionGUID.condition=filterForm.conditionQuestionGUID;
                qfDate.condition=filterForm.conditionDate;
                qfCompany.condition=filterForm.conditionCompany;
                qfPort.condition=filterForm.conditionPort;
                qfOffice.condition=filterForm.conditionOffice;
                qfDOC.condition = filterForm.conditionDOC;
                qfHullClass.condition=filterForm.conditionHullClass;
                qfMaster.condition = filterForm.conditionMaster;
                qfChiefEngineer.condition = filterForm.conditionChiefEngineer;
                qfSubchapter.condition = filterForm.conditionSubchapter;
                qfKeyWords.condition = filterForm.conditionKeyWords;
                qfQuestionnaireType.condition = filterForm.conditionQuestionnaireType;
                qfFleet.condition = filterForm.conditionFleet;

                qfVessel.value=filterForm.valueVessel;
                qfInspector.value=filterForm.valueInspector;
                qfReportNumber.value=filterForm.valueReportNumber;
                qfQuestionNumber.value=filterForm.valueQuestionNumber;
                qfObservation.value=filterForm.valueObservation;
                qfInspectorComments.value=filterForm.valueInspectorComments ;
                qfTechnicalComments.value=filterForm.valueTechnicalComments ;
                qfOperatorComments.value=filterForm.valueOperatorComments;
                qfQuestionGUID.value=filterForm.valueQuestionGUID;
                qfDate.value=filterForm.valueDate;
                qfDate.value2=filterForm.valueDate2;
                qfCompany.value=filterForm.valueCompany;
                qfPort.value=filterForm.valuePort;
                qfOffice.value=filterForm.valueOffice;
                qfDOC.value = filterForm.valueDOC;
                qfHullClass.value=filterForm.valueHullClass;
                qfMaster.value = filterForm.valueMaster;
                qfChiefEngineer.value = filterForm.valueChiefEngineer;
                qfSubchapter.value = filterForm.valueSubchapter;
                qfKeyWords.value = filterForm.valueKeyWords;
                qfQuestionnaireType.value = filterForm.valueQuestionnaireType;
                qfFleet.value = filterForm.valueFleet;

                questionsMapping = filterForm.useMapping;

                qfQuestionNumber.typeCode = filterForm.typesBox;
                
                if (!checkQuestionFilter())
                {
                    toolStripLabel1.Text = "Filter is OFF";
                    toolStripLabel1.ForeColor =Color.Black;
                }
                else
                {
                    toolStripLabel1.Text = "Filter is ON";
                    toolStripLabel1.ForeColor = Color.Crimson;
                }

                updateGridOnFilterChange = filterForm.updateOnClosed;

                if (updateGridOnFilterChange) 
                    updateQuestions();
            }
        }

        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                updateQuestions();
        }

        private void ShowReportFilter()
        {
            //Show report filter form
            ReportFilterForm rff = new ReportFilterForm(DS, connection, this.Font, this.Icon);

            rff.vesselCondition = rfVessel.condition;
            rff.inspectorCondition = rfInspector.condition;
            rff.reportNumberCondition = rfReportNumber.condition;
            rff.dateCondition = rfDate.condition;
            rff.companyCondition = rfCompany.condition;
            rff.portCondition = rfPort.condition;
            rff.officeConfition = rfOffice.condition;
            rff.docCondition = rfDOC.condition;
            rff.classCondition = rfHullClass.condition;
            rff.masterCondition = rfMaster.condition;
            rff.chiefEngineerCondition = rfChiefEngineer.condition;

            rff.inspectionTypeCondition = rfInspectionType.condition;
            rff.memorandumCondition = rfMemorandum.condition;

            rff.date1Value = rfDate.value;
            rff.date2Value = rfDate.value2;
            rff.synchronize = sinchronizeFilter;

            rff.strReportNumber = rfReportNumber.value;
            rff.strVessel = rfVessel.value;
            rff.strInspector = rfInspector.value;
            rff.strCompany = rfCompany.value;
            rff.strPort = rfPort.value;
            rff.strOffice = rfOffice.value;
            rff.strDOC = rfDOC.value;
            rff.strHullClass = rfHullClass.value;
            rff.strMaster = rfMaster.value;
            rff.strChiefEngineer = rfChiefEngineer.value;

            rfInspectionType.value = tscbInspectionType.Text;
            rff.strInspectionType = rfInspectionType.value;
            rff.strMemorandum = rfMemorandum.value;

            var rslt=rff.ShowDialog();

            if (rslt == DialogResult.OK)
            {

                rfVessel.condition = rff.vesselCondition;
                rfInspector.condition = rff.inspectorCondition;
                rfReportNumber.condition = rff.reportNumberCondition;
                rfDate.condition = rff.dateCondition;
                rfCompany.condition = rff.companyCondition;
                rfPort.condition = rff.portCondition;
                rfOffice.condition = rff.officeConfition;
                rfDOC.condition = rff.docCondition;
                rfHullClass.condition = rff.classCondition;
                rfMaster.condition = rff.masterCondition;
                rfChiefEngineer.condition = rff.chiefEngineerCondition;
                rfInspectionType.condition = rff.inspectionTypeCondition;
                rfMemorandum.condition = rff.memorandumCondition;

                rfVessel.value = rff.strVessel;
                rfInspector.value = rff.strInspector;
                rfReportNumber.value = rff.strReportNumber;
                rfDate.value = rff.date1Value;
                rfDate.value2 = rff.date2Value;
                rfCompany.value = rff.strCompany;
                rfPort.value = rff.strPort;
                rfOffice.value = rff.strOffice;
                rfDOC.value = rff.strDOC;
                rfHullClass.value = rff.strHullClass;
                rfMaster.value = rff.strMaster;
                rfChiefEngineer.value = rff.strChiefEngineer;
                rfInspectionType.value = rff.strInspectionType;
                tscbInspectionType.Text = rfInspectionType.value;
                rfMemorandum.value = rff.strMemorandum;
                

                bool filterActive = isValidCondition(rfVessel.condition,rfVessel.value) ||
                    isValidCondition(rfInspector.condition, rfInspector.value) ||
                    isValidCondition(rfReportNumber.condition, rfReportNumber.value) ||
                    (rfDate.condition.Length > 0) ||
                    isValidCondition(rfCompany.condition, rfCompany.value) ||
                    isValidCondition(rfPort.condition, rfPort.value) ||
                    isValidCondition(rfOffice.condition, rfOffice.value) ||
                    isValidCondition(rfDOC.condition, rfDOC.value) ||
                    isValidCondition(rfHullClass.condition, rfHullClass.value) ||
                    isValidCondition(rfMaster.condition, rfMaster.value) ||
                    isValidCondition(rfChiefEngineer.condition, rfChiefEngineer.value) ||
                    isValidCondition(rfInspectionType.condition,rfInspectionType.value) ||
                    isValidCondition(rfMemorandum.condition,rfMemorandum.value)
                    ;

                if (!filterActive)
                {
                    toolStripLabel4.Text = "Filter is OFF";
                    toolStripLabel4.ForeColor = Color.Black;
                    reportFilterOn = false;
                }
                else
                {
                    toolStripLabel4.Text = "Filter is ON";
                    toolStripLabel4.ForeColor = Color.Crimson;
                    reportFilterOn = true;
                }

                updateReports();

                if (rff.synchronize)
                {
                    //Синхронизация фильтров
                    sinchronizeFilter = true;

                    bool qFilter = checkQuestionFilter();

                    if (isValidCondition(rfVessel.condition,rfVessel.value))
                    {
                        qfVessel.condition = rfVessel.condition;
                        qfVessel.value = rfVessel.value;
                        qfVessel.blocked = true;
                    }
                    else
                    {
                        qfVessel.blocked = false;
                    }

                    if (isValidCondition(rfInspector.condition, rfInspector.value))
                    {
                        qfInspector.condition = rfInspector.condition;
                        qfInspector.value = rfInspector.value;
                        qfInspector.blocked = true;
                    }
                    else
                        qfInspector.blocked = false;

                    if (isValidCondition(rfReportNumber.condition, rfReportNumber.value))
                    {
                        qfReportNumber.condition = rfReportNumber.condition;
                        qfReportNumber.value = rfReportNumber.value;
                        qfReportNumber.blocked = true;
                    }
                    else
                        qfReportNumber.blocked = false;

                    if (rfDate.condition.Length > 0)
                    {
                        qfDate.condition = rfDate.condition;
                        qfDate.value = rfDate.value;
                        qfDate.value2 = rfDate.value2;
                        qfDate.blocked = true;
                    }
                    else
                        qfDate.blocked = false;



                    if (isValidCondition(rfCompany.condition, rfCompany.value))
                    {
                        qfCompany.condition = rfCompany.condition;
                        qfCompany.value = rfCompany.value;
                        qfCompany.blocked = true;
                    }
                    else
                        qfCompany.blocked = false;

                    if (isValidCondition(rfPort.condition, rfPort.value))
                    {
                        qfPort.condition = rfPort.condition;
                        qfPort.value = rfPort.value;
                        qfPort.blocked = true;
                    }
                    else
                        qfPort.blocked = false;

                    if (isValidCondition(rfOffice.condition, rfOffice.value))
                    {
                        qfOffice.condition = rfOffice.condition;
                        qfOffice.value = rfOffice.value;
                        qfOffice.blocked = true;
                    }
                    else
                        qfOffice.blocked = false;

                    if (isValidCondition(rfDOC.condition, rfDOC.value))
                    {
                        qfDOC.condition = rfDOC.condition;
                        qfDOC.value = rfDOC.value;
                        qfDOC.blocked = true;
                    }
                    else
                        qfDOC.blocked = false;

                    if (isValidCondition(rfHullClass.condition, rfHullClass.value))
                    {
                        qfHullClass.condition = rfHullClass.condition;
                        qfHullClass.value = rfHullClass.value;
                        qfHullClass.blocked = true;
                    }
                    else
                        qfHullClass.blocked = false;

                    if (isValidCondition(rfMaster.condition, rfMaster.value))
                    {
                        qfMaster.condition = rfMaster.condition;
                        qfMaster.value = rfMaster.value;
                        qfMaster.blocked = true;
                    }
                    else
                        qfMaster.blocked = false;

                    if (isValidCondition(rfChiefEngineer.condition, rfChiefEngineer.value))
                    {
                        qfChiefEngineer.condition = rfChiefEngineer.condition;
                        qfChiefEngineer.value = rfChiefEngineer.value;
                        qfChiefEngineer.blocked = true;
                    }
                    else
                        qfChiefEngineer.blocked = false;

                    if (!checkQuestionFilter())
                    {
                        toolStripLabel1.Text = "Filter is OFF";
                        toolStripLabel1.ForeColor = Color.Black;
                    }
                    else
                    {
                        toolStripLabel1.Text = "Filter is ON";
                        toolStripLabel1.ForeColor = Color.Crimson;
                    }

                    if ((qFilter!=checkQuestionFilter()) || (checkQuestionFilter()))
                    {
                        updateQuestions();
                    }
                }
                else
                {
                    sinchronizeFilter = false;

                    if (checkQuestionFilter())
                    {
                        qfVessel.blocked = false;
                        qfInspector.blocked = false;
                        qfReportNumber.blocked = false;
                        qfDate.blocked = false;
                        qfCompany.blocked = false;
                        qfPort.blocked = false;
                        qfOffice.blocked = false;
                        qfDOC.blocked = false;
                        qfHullClass.blocked = false;
                        qfMaster.blocked = false;
                        qfChiefEngineer.blocked = false;

                        if (!checkQuestionFilter())
                        {
                            toolStripLabel1.Text = "Filter is OFF";
                            toolStripLabel1.ForeColor = Color.Black;
                        }
                        else
                        {
                            toolStripLabel1.Text = "Filter is ON";
                            toolStripLabel1.ForeColor = Color.Crimson;
                        }

                        updateQuestions();
                    }
                }
            }
        }

        private string BuildReportFilter()
        {
            string fText="";

            if (isValidCondition(rfVessel.condition,rfVessel.value))
                {
                    fText=setTextFilter("VESSEL_NAME",rfVessel.condition,rfVessel.value);
                }

                if (isValidCondition(rfInspector.condition,rfInspector.value))
                {
                    if (fText.Length==0)
                        fText=setTextFilter("INSPECTORS.INSPECTOR_NAME",rfInspector.condition,rfInspector.value);
                    else
                        fText = fText + " and " + setTextFilter("INSPECTORS.INSPECTOR_NAME", rfInspector.condition, rfInspector.value);
                }

                if (isValidCondition(rfReportNumber.condition,rfReportNumber.value))
                {
                    if (fText.Length==0)
                        fText=setTextFilter("REPORT_CODE",rfReportNumber.condition,rfReportNumber.value);
                    else
                        fText = fText + " and " + setTextFilter("REPORT_CODE", rfReportNumber.condition, rfReportNumber.value);
                }

                if (rfDate.condition.Length>0)
                {
                    if (fText.Length==0)
                        fText=setDateFilter("INSPECTION_DATE",rfDate.condition,rfDate.value,rfDate.value2);
                    else
                        fText = fText + " and " + setDateFilter("INSPECTION_DATE", rfDate.condition, rfDate.value, rfDate.value2);
                }

                if (isValidCondition(rfInspectionType.condition,rfInspectionType.value))
                {
                    int inspectionTypeID = GetInspectionTypeID(rfInspectionType.value);

                    if (inspectionTypeID > 0)
                    {
                        if (fText.Length == 0)
                            fText = setTextFilter("INSPECTION_TYPE", rfInspectionType.condition, rfInspectionType.value);
                        else
                            fText = fText + " and " + setTextFilter("INSPECTION_TYPE", rfInspectionType.condition, rfInspectionType.value);
                    }
                }

                if (isValidCondition(rfCompany.condition,rfCompany.value))
                {
                    if (fText.Length == 0)
                        fText = setTextFilter("COMPANY", rfCompany.condition, rfCompany.value);
                    else
                        fText = fText + " and " + setTextFilter("COMPANY", rfCompany.condition, rfCompany.value);
                }

                if (isValidCondition(rfMemorandum.condition, rfMemorandum.value))
                {
                    if (fText.Length == 0)
                        fText = setTextFilter("MEMORANDYM_TYPE", rfMemorandum.condition, rfMemorandum.value);
                    else
                        fText = fText + " and " + setTextFilter("MEMORANDUM_TYPE", rfMemorandum.condition, rfMemorandum.value);
                }    

                if (isValidCondition(rfPort.condition,rfPort.value))
                {
                    if (fText.Length==0)
                        fText=setTextFilter("INSPECTION_PORT",rfPort.condition,rfPort.value);
                    else
                        fText = fText + " and " + setTextFilter("INSPECTION_PORT", rfPort.condition, rfPort.value);
                }

                if (isValidCondition(rfOffice.condition,rfOffice.value))
                {
                    if (fText.Length==0)
                        fText=setTextFilter("VESSELS.OFFICE",rfOffice.condition,rfOffice.value);
                    else
                        fText = fText + " and " + setTextFilter("VESSELS.OFFICE", rfOffice.condition, rfOffice.value);
                }

                if (isValidCondition(rfDOC.condition, rfDOC.value))
                {
                    if (fText.Length == 0)
                        fText = setTextFilter("VESSELS.DOC", rfDOC.condition, rfDOC.value);
                    else
                        fText = fText + " and " + setTextFilter("VESSELS.DOC", rfDOC.condition, rfDOC.value);
                }

                if (isValidCondition(rfHullClass.condition, rfHullClass.value))
                {
                    if (fText.Length==0)
                        fText=setTextFilter("VESSELS.HULL_CLASS",rfHullClass.condition,rfHullClass.value);
                    else
                        fText = fText + " and " + setTextFilter("VESSELS.HULL_CLASS", rfHullClass.condition, rfHullClass.value);
                }

                if (isValidCondition(rfMaster.condition,rfMaster.value))
                {
                    if (fText.Length == 0)
                        fText = setTextFilter("MASTERS.MASTER_NAME", rfMaster.condition, rfMaster.value);
                    else
                        fText = fText + " and " + setTextFilter("MASTERS.MASTER_NAME", rfMaster.condition, rfMaster.value);
                }

                if (isValidCondition(rfChiefEngineer.condition,rfChiefEngineer.value))
                {
                    if (fText.Length == 0)
                        fText = setTextFilter("CHIEF_ENGINEERS.CHENG_NAME", rfChiefEngineer.condition, rfChiefEngineer.value);
                    else
                        fText = fText + " and " + setTextFilter("CHIEF_ENGINEERS.CHENG_NAME", rfChiefEngineer.condition, rfChiefEngineer.value);
                }

                return fText;
        }

        private string setDateFilter(string field, string condition, DateTime date1, DateTime date2)
        {
                string s="";

                switch (condition)
                {
                    case "Equal":
                        s=field+"="+DateTimeToQueryStr(date1);
                        break;
                    case "Not Equal":
                        s=field+"<>"+DateTimeToQueryStr(date1);
                        break;
                    case "Between":
                        s=field+" Between "+DateTimeToQueryStr(date1)+" and "+DateTimeToQueryStr(date2);
                        break;
                    case "Not Between":
                        s=field+" Not Between "+DateTimeToQueryStr(date1)+" and "+DateTimeToQueryStr(date2);
                        break;
                    case "On and Before":
                        s=field+"<="+DateTimeToQueryStr(date1);
                        break;
                    case "On and After":
                        s = field + ">=" + DateTimeToQueryStr(date1);
                        break;
                    case "Before":
                        s = field + "<" + DateTimeToQueryStr(date1);
                        break;
                    case "After":
                        s = field + ">" + DateTimeToQueryStr(date1);
                        break;
                }
                return s;
        }

        public static string DateTimeToQueryStr(DateTime inDateTime)
        {

            string s;
            string Year;
            string Month;
            string Day;
            string Hour;
            string Min;
            string Sec;

            DateTime AMinimumDate = DateTime.Parse("1900-01-01");

            if (inDateTime > DateTimePicker.MinimumDateTime)
            {
                Year = Convert.ToString(inDateTime.Year);
                Month = Convert.ToString(inDateTime.Month);
                Day = Convert.ToString(inDateTime.Day);
                Hour = Convert.ToString(inDateTime.Hour);
                Min = Convert.ToString(inDateTime.Minute);
                Sec = Convert.ToString(inDateTime.Second);

                s = "DateSerial(" + Year + "," + Month + "," + Day + ")+TimeSerial(" + Hour + "," + Min + "," + Sec + ")";

                return s;
            }
            else
            {
                return "Null";
            }
        }

        private void toolStripSplitButton2_ButtonClick(object sender, EventArgs e)
        {
            ShowReportFilter();
            toolStripStatusLabel4.Text = dgvInspections.Rows.Count.ToString();
        }

        private void setFilterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowReportFilter();
            toolStripStatusLabel4.Text = dgvInspections.Rows.Count.ToString();
        }

        private void resetFilterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            rfVessel.condition = "";
            rfInspector.condition = "";
            rfReportNumber.condition = "";
            rfDate.condition = "";
            rfCompany.condition = "";
            rfPort.condition = "";
            rfOffice.condition = "";
            rfDOC.condition = "";
            rfHullClass.condition = "";
            rfMaster.condition = "";
            rfChiefEngineer.condition = "";

            rfVessel.value = "";
            rfInspector.value = "";
            rfReportNumber.value = "";
            rfDate.value = DateTime.Today;
            rfDate.value2 = DateTime.Today;
            rfCompany.value = "";
            rfPort.value = "";
            rfOffice.value = "";
            rfDOC.value = "";
            rfHullClass.value = "";
            rfMaster.value = "";
            rfChiefEngineer.value = "";

            reportFilterOn = false;


            toolStripLabel4.Text = "Filter is OFF";
            toolStripLabel4.ForeColor = Color.Black;

            updateReports();

            toolStripStatusLabel4.Text = dgvInspections.Rows.Count.ToString();

            if (sinchronizeFilter)
            {
                bool qFilter = checkQuestionFilter();

                if (isValidCondition(rfVessel.condition,rfVessel.value))
                {
                    qfVessel.condition = rfVessel.condition;
                    qfVessel.value = rfVessel.value;
                    qfVessel.blocked = true;
                }
                else
                {
                    qfVessel.blocked = false;
                }

                if (isValidCondition(rfInspector.condition,rfInspector.value))
                {
                    qfInspector.condition = rfInspector.condition;
                    qfInspector.value = rfInspector.value;
                    qfInspector.blocked = true;
                }
                else
                    qfInspector.blocked = false;

                if (isValidCondition(rfReportNumber.condition,rfReportNumber.value))
                {
                    qfReportNumber.condition = rfReportNumber.condition;
                    qfReportNumber.value = rfReportNumber.value;
                    qfReportNumber.blocked = true;
                }
                else
                    qfReportNumber.blocked = false;

                if (rfDate.condition.Length > 0)
                {
                    qfDate.condition = rfDate.condition;
                    qfDate.value = rfDate.value;
                    qfDate.value2 = rfDate.value2;
                    qfDate.blocked = true;
                }
                else
                    qfDate.blocked = false;

                if (isValidCondition(rfCompany.condition,rfCompany.value))
                {
                    qfCompany.condition = rfCompany.condition;
                    qfCompany.value = rfCompany.value;
                    qfCompany.blocked = true;
                }
                else
                    qfCompany.blocked = false;

                if (isValidCondition(rfPort.condition,rfPort.value))
                {
                    qfPort.condition = rfPort.condition;
                    qfPort.value = rfPort.value;
                    qfPort.blocked = true;
                }
                else
                    qfPort.blocked = false;

                if (isValidCondition(rfOffice.condition,rfOffice.value))
                {
                    qfOffice.condition = rfOffice.condition;
                    qfOffice.value = rfOffice.value;
                    qfOffice.blocked = true;
                }
                else
                    qfOffice.blocked = false;

                if (isValidCondition(rfDOC.condition, rfDOC.value))
                {
                    qfDOC.condition = rfDOC.condition;
                    qfDOC.value = rfDOC.value;
                    qfDOC.blocked = true;
                }
                else
                    qfDOC.blocked = false;

                if (isValidCondition(rfHullClass.condition, rfHullClass.value))
                {
                    qfHullClass.condition = rfHullClass.condition;
                    qfHullClass.value = rfHullClass.value;
                    qfHullClass.blocked = true;
                }
                else
                    qfHullClass.blocked = false;

                if (isValidCondition(rfMaster.condition,rfMaster.value))
                {
                    qfMaster.condition = rfMaster.condition;
                    qfMaster.value = rfMaster.value;
                    qfMaster.blocked = true;
                }
                else
                    qfMaster.blocked = false;

                if (isValidCondition(rfChiefEngineer.condition,rfChiefEngineer.value))
                {
                    qfChiefEngineer.condition = rfChiefEngineer.condition;
                    qfChiefEngineer.value = rfChiefEngineer.value;
                    qfChiefEngineer.blocked = true;
                }
                else
                    qfChiefEngineer.blocked = false;


                if (!checkQuestionFilter())
                {
                    toolStripLabel1.Text = "Filter is OFF";
                    toolStripLabel1.ForeColor = Color.Black;
                }
                else
                {
                    toolStripLabel1.Text = "Filter is ON";
                    toolStripLabel1.ForeColor = Color.Crimson;
                }

                if ((qFilter != checkQuestionFilter()) || (checkQuestionFilter()))
                {
                    updateQuestions();
                }
            }
        }

        private bool checkQuestionFilter()
        {
            bool filterActive = isValidCondition(qfVessel.condition, qfVessel.value) ||
                    isValidCondition(qfInspector.condition, qfInspector.value) ||
                    isValidCondition(qfReportNumber.condition, qfReportNumber.value) ||
                    isValidCondition(qfQuestionNumber.condition, qfQuestionNumber.value) ||
                    isValidCondition(qfObservation.condition, qfObservation.value) ||
                    isValidCondition(qfInspectorComments.condition, qfInspectorComments.value) ||
                    isValidCondition(qfTechnicalComments.condition, qfTechnicalComments.value) ||
                    isValidCondition(qfOperatorComments.condition, qfOperatorComments.value) ||
                    isValidCondition(qfQuestionGUID.condition, qfQuestionGUID.value) ||
                    (qfDate.condition.Length > 0) ||
                    isValidCondition(qfCompany.condition, qfCompany.value) ||
                    isValidCondition(qfPort.condition, qfPort.value) ||
                    isValidCondition(qfOffice.condition, qfOffice.value) ||
                    isValidCondition(qfDOC.condition, qfDOC.value) ||
                    isValidCondition(qfHullClass.condition, qfHullClass.value) ||
                    isValidCondition(qfMaster.condition, qfMaster.value) ||
                    isValidCondition(qfChiefEngineer.condition, qfChiefEngineer.value);
                    
            
            return filterActive;
        }

        private void toolStripButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (toolStripButton5.Checked)
                dgvDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            else
                dgvDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void questionsMappingToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        public static int cmdExecute(OleDbCommand cmd)
        {
            int rslt = -1;

            if (connection.State == ConnectionState.Closed)
                return rslt;

            try
            {
                rslt = cmd.ExecuteNonQuery();
                return rslt;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                MemoForm mForm = new MemoForm(cmd.CommandText);
                
                mForm.ShowDialog();

                return -1;
            }
        }

        private void mastersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void chiefEngineersToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private string IntStrToSQL(string Value)
        {
            if (Value.Length == 0)
                return "Null";
            else
                return Value;  
        }

        private void vesselsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            VesselsForm frm6 = new VesselsForm();

            frm6.ShowDialog();

            //Update reports
            updateReports();
        }

        private void inspectorsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            InspectorsForm InsFrm = new InspectorsForm(connection, DS, this.Font, this.Icon);

            InsFrm.ShowDialog();

            //Update reports
            updateReports();
        }

        private void menuPersonnel_Click(object sender, EventArgs e)
        {
            ListOfMastersForm form = new ListOfMastersForm();

            form.ShowDialog();
        }
 
        private void fontToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (isPowerUser)
            {
                FormOptions form = new FormOptions(this.Icon, this.Font);

                form.allowedToAll = anyUserMayChangeFont;
                form.onePowerFont = oneFontForPowerUsers;

                if (form.ShowDialog() == DialogResult.OK)
                {
                    anyUserMayChangeFont = form.allowedToAll;
                    oneFontForPowerUsers = form.onePowerFont;

                    SaveOptionValue(110, anyUserMayChangeFont.ToString());
                    SaveOptionValue(111, oneFontForPowerUsers.ToString());

                    setFormsFont(form.Font);
                    saveFont("Forms", form.Font);
                }
            }
            else
            {
                if (anyUserMayChangeFont)
                {
                    fontDialog1.Font = this.Font;

                    var rslt = fontDialog1.ShowDialog();

                    if (rslt == DialogResult.OK)
                    {
                        setFormsFont(fontDialog1.Font);

                        saveFont("Forms", fontDialog1.Font);
                    }
                }
                else
                {
                    MessageBox.Show("You are not allowed to change font", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void questionsMappingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!isPowerUser)
            {
                MessageBox.Show("You have not rights to access question mapping", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            QuestionsMappingForm qmf = new QuestionsMappingForm(connection, DS, this.Font, this.Icon);

            qmf.ShowDialog();

        }

        private void listOfQuestionnairiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuestionnairiesForm form = new QuestionnairiesForm(connection, DS, this.Font, this.Icon);

            form.ShowDialog();

            if (form.templateChnaged)
            {
                if (tabControl1.SelectedIndex == 0)
                    updateReports();
            }
        }

        private string GetQuestionVersion(string questionGUID)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select top 1 VERSION \n" +
                "from TEMPLATE_QUESTIONS inner join TEMPLATES \n" +
                "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID \n" +
                "where QUESTION_GUID like {" + questionGUID + "} \n"+
                "order by TEMPLATES.VERSION DESC";

            string version = (string)cmd.ExecuteScalar();

            return version;
        }

        private string GetQuestionVersionNew(string questionGUID)
        {
            DataTable qv = DS.Tables["QUESTION_VERSION"];

            DataRow[] foundRows;

            foundRows = qv.Select("QUESTION_GUID='" + questionGUID + "'");

            if (foundRows.Length > 0)
            {
                return foundRows[0][3].ToString();
            }
            else
                return "";
        }

        private void GetQuestionVersionAndType(string questionGUID, ref string viqVersion, ref string viqType, ref string viqTypeCode)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select top 1 VERSION, TYPE_CODE, TITLE \n" +
                "from TEMPLATE_QUESTIONS inner join TEMPLATES \n" +
                "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID \n" +
                "where QUESTION_GUID like {" + questionGUID + "} \n" +
                "order by TEMPLATES.VERSION DESC";
            
            OleDbDataReader versionReader = cmd.ExecuteReader();

            while (versionReader.Read())
            {
                viqVersion = versionReader[0].ToString();
                viqTypeCode = versionReader[1].ToString();
                viqType = versionReader[2].ToString();
            }

            versionReader.Close();

        }

        private void GetReportType(string questionGUID, ref string viqType, ref string viqTypeCode)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select top 1 TYPE_CODE, TITLE \n" +
                "from TEMPLATE_QUESTIONS inner join TEMPLATES \n" +
                "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID \n" +
                "where QUESTION_GUID like {" + questionGUID + "} \n" +
                "order by TEMPLATES.VERSION DESC";

            OleDbDataReader versionReader = cmd.ExecuteReader();

            while (versionReader.Read())
            {
                viqTypeCode = versionReader[0].ToString();
                viqType = versionReader[1].ToString();
            }

            versionReader.Close();

        }

        private void ExportToExcel2(DataGridView dgv, bool calcTotals, int startRow, int startCol)
        {
            if (dgv.Rows.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("There is no any data to export.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            var excelApp = new Excel.Application();
            // Make the object visible.
            excelApp.Visible = false;
            excelApp.WindowState = Excel.XlWindowState.xlMaximized;
            excelApp.Workbooks.Add();
            Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;

            int columns = 0;

            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                if (dgv.Columns[i].Visible)
                    columns++;
            }
            
            
            int rows = dgv.Rows.Count;

            if (calcTotals)
                rows++;

            //Create an array.
            Object[,] varArray = new Object[rows, columns];


            int cIndex = 0;

            for (int i = 0; i < columns; i++)
            {
                while ((cIndex < dgv.ColumnCount) && !dgv.Columns[cIndex].Visible)
                    cIndex++;

                if (cIndex < dgv.ColumnCount)
                {
                    varArray[0, i] = dgv.Columns[cIndex].HeaderText;

                    System.Type type = dgv.Columns[cIndex].ValueType;

                    if (String.Compare(type.Name,"String")==0)
                        workSheet.Range[workSheet.Cells[startRow, startCol+i], workSheet.Cells[startRow+rows-1, startCol+i]].NumberFormat = "@";

                    cIndex++;
                }
            }

            for (int i = 0; i <rows-1; i++)
            {
                cIndex = 0;

                for (int j = 0; j < columns; j++)
                {
                    while ((cIndex < dgv.ColumnCount) && !dgv.Columns[cIndex].Visible)
                    cIndex++;

                    if (cIndex < dgv.ColumnCount)
                    {
                        if (dgv[cIndex, i].Value != null)
                        {
                            System.Type type = dgv.Columns[j].ValueType;

                            if (type == System.Type.GetType("System.DateTime"))
                            {
                                DateTime xDate = Convert.ToDateTime(dgv[cIndex, i].Value);
                                varArray[i + 1, j] = xDate.ToString("yyyy-MM-dd");
                            }
                            else
                                varArray[i + 1, j] = dgv[cIndex, i].Value.ToString();
                        }

                        cIndex++;
                    }
                }
            }

            //workSheet.Range[workSheet.Cells[1, 1], workSheet.Cells[rows, 1]].NumberFormat = "@";

            workSheet.Range[workSheet.Cells[startRow,startCol],workSheet.Cells[startRow+rows-1,startCol+columns-1]].Value2 = varArray;

            for (int i = 0; i < columns; i++)
            {
                try
                {
                    ((Excel.Range)workSheet.Columns[i + 1]).AutoFit();
                }
                catch (Exception E)
                {
                    System.Windows.Forms.MessageBox.Show(E.Message);
                }
            }

            workSheet.Range[workSheet.Cells[startRow, startCol], workSheet.Cells[startRow+rows-1, startCol+columns-1]].Borders.Weight = Excel.XlBorderWeight.xlThin;
            workSheet.Range[workSheet.Cells[startRow, startCol], workSheet.Cells[startRow + rows - 1, startCol + columns - 1]].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            workSheet.Range[workSheet.Cells[startRow, startCol], workSheet.Cells[startRow, startCol + columns - 1]].EntireRow.Font.Bold = true;

            if (calcTotals)
            {
                workSheet.Range[workSheet.Cells[startRow+rows, startCol], workSheet.Cells[startRow+rows, startCol]].Value2 = "Total";
 
                for (int i = 2; i <= columns; i++)
                {
                    if (Convert.ToString(workSheet.Range[workSheet.Cells[startRow, startCol+i-1], workSheet.Cells[startRow+rows-1, startCol+i-1]].NumberFormat) != "@")
                    {
                        string A = "A";
                        int codeA = (int)A[0];

                        workSheet.Range[workSheet.Cells[startRow+rows, startCol+i-1], workSheet.Cells[startCol+rows, startCol+i-1]].FormulaLocal = "=SUM(" + ((char)(codeA + i - 1)).ToString() + "2:" + ((char)(codeA + i - 1)).ToString() + rows.ToString() + ")";
                    }
                }

                workSheet.Range[workSheet.Cells[startRow+rows, startCol], workSheet.Cells[startRow+rows, startCol+columns-1]].EntireRow.Font.Bold = true;
            }

            workSheet.Range[workSheet.Cells[startRow+1, startCol+1], workSheet.Cells[startRow+1, startCol+1]].Select();

            workSheet.Application.ActiveWindow.FreezePanes = true;

            excelApp.Visible = true;
        }

        private void exportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportToExcel2(dgvInspections,false,1,1);
        }

        private void copySelectionToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(dgvInspections.GetClipboardContent());
        }

        private void exportGridToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportToExcel2(dgvDetails, false, 1, 1);
        }

        private void copySelectionToClipboardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(dgvDetails.GetClipboardContent());
        }

        private void numberOfInspectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showNumberReportForm();
        }

        private void showNumberReportForm()
        {
            NumberOfInspectionsForm nrf = new NumberOfInspectionsForm(this.Font, this.Icon);

            var rslt = nrf.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                OleDbCommand cmd = new OleDbCommand("", connection);

                string select = "";
                string groupBy = "";
                string itemSelect = "";
                string itemGroup = "";

                if (nrf.group1Text.Length>0)
                {
                    if (String.Compare(nrf.group1Text, "DOC", true) == 0)
                    {
                        itemSelect = "Vessels.DOC";
                        itemGroup = "Vessels.DOC";
                    }
                    else
                    {
                        if (String.Compare(nrf.group1Text, "OFFICE", true) == 0)
                        {
                            itemSelect = "Vessels.OFFICE as [Office]";
                            itemGroup = "Vessels.OFFICE";
                        }
                        else
                        {
                            if (String.Compare(nrf.group1Text, "Inspection", true) == 0)
                            {
                                itemSelect = "Reports.Report_Code as [Report]";
                                itemGroup = "Reports.Report_Code";
                            }
                            else
                            {
                                if (String.Compare(nrf.group1Text, "Vessel", true) == 0)
                                {
                                    itemSelect = "VESSELS.VESSEL_NAME as [Vessel name], VESSELS.VESSEL_IMO as [IMO number]";
                                    itemGroup = "VESSELS.VESSEL_NAME, VESSELS.VESSEL_IMO";
                                }
                                else
                                {
                                    if (String.Compare(nrf.group1Text,"Inspecting Company",true)==0)
                                    {
                                        itemSelect = "REPORTS.COMPANY as [Inspecting Company]";
                                        itemGroup = "REPORTS.COMPANY";
                                    }
                                    else
                                    {
                                        itemSelect = nrf.group1Text;
                                        itemGroup = nrf.group1Text;
                                    }
                                }
                            }
                        }
                    }

                    if (select.Length == 0)
                        select = itemSelect;
                    else
                        select = select + ", " + itemSelect;

                    if (groupBy.Length == 0)
                        groupBy = itemGroup;
                    else
                        groupBy = groupBy + ", " + itemGroup;
                }

                if (nrf.group2Text.Length>0)
                {
                    if (String.Compare(nrf.group2Text, "DOC", true) == 0)
                    {
                        itemSelect = "Vessels.DOC";
                        itemGroup = "Vessels.DOC";
                    }
                    else
                    {
                        if (String.Compare(nrf.group2Text, "OFFICE", true) == 0)
                        {
                            itemSelect = "Vessels.OFFICE as [Office]";
                            itemGroup = "Vessels.OFFICE";
                        }
                        else
                        {
                            if (String.Compare(nrf.group2Text, "Inspection", true) == 0)
                            {
                                itemSelect = "Reports.Report_Code as [Report]";
                                itemGroup = "Reports.Report_Code";
                            }
                            else
                            {
                                if (String.Compare(nrf.group2Text, "Vessel", true) == 0)
                                {
                                    itemSelect = "VESSELS.VESSEL_NAME as [Vessel name], VESSELS.VESSEL_IMO as [IMO number]";
                                    itemGroup = "VESSELS.VESSEL_NAME, VESSELS.VESSEL_IMO";
                                }
                                else
                                {
                                    if (String.Compare(nrf.group2Text, "Inspecting Company", true) == 0)
                                    {
                                        itemSelect = "REPORTS.COMPANY as [Inspecting Company]";
                                        itemGroup = "REPORTS.COMPANY";
                                    }
                                    else
                                    {
                                        itemSelect = nrf.group2Text;
                                        itemGroup = nrf.group2Text;
                                    }
                                }
                            }
                        }
                    }

                    if (select.Length == 0)
                        select = itemSelect;
                    else
                        select = select + ", " + itemSelect;

                    if (groupBy.Length == 0)
                        groupBy = itemGroup;
                    else
                        groupBy = groupBy + ", " + itemGroup;
                }

                if (nrf.group3Text.Length>0)
                {
                    if (String.Compare(nrf.group3Text, "DOC", true) == 0)
                    {
                        itemSelect = "Vessels.DOC";
                        itemGroup = "Vessels.DOC";
                    }
                    else
                    {
                        if (String.Compare(nrf.group3Text, "DOC", true) == 0)
                        {
                            itemSelect = "Vessels.OFFICE as [Office]";
                            itemGroup = "Vessels.OFFICE";
                        }
                        else
                        {
                            if (String.Compare(nrf.group3Text, "Inspection", true) == 0)
                            {
                                itemSelect = "Reports.Report_Code as [Report]";
                                itemGroup = "Reports.Report_Code";
                            }
                            else
                            {
                                if (String.Compare(nrf.group3Text, "Vessel", true) == 0)
                                {
                                    itemSelect = "VESSELS.VESSEL_NAME as [Vessel name], VESSELS.VESSEL_IMO as [IMO number]";
                                    itemGroup = "VESSELS.VESSEL_NAME, VESSELS.VESSEL_IMO";
                                }
                                else
                                {
                                    if (String.Compare(nrf.group3Text, "Inspecting Company",true)==0)
                                    {
                                        itemSelect = "REPORTS.COMPANY as [Inspecting Company]";
                                        itemGroup = "REPORTS.COMPANY";
                                    }
                                    else
                                    {
                                        itemSelect = nrf.group3Text;
                                        itemGroup = nrf.group3Text;
                                    }
                                }
                            }
                        }
                    }

                    if (select.Length == 0)
                        select = itemSelect;
                    else
                        select = select + ", " + itemSelect;

                    if (groupBy.Length == 0)
                        groupBy = itemGroup;
                    else
                        groupBy = groupBy + ", " + itemGroup;
                }

                string where = "";

                if (nrf.date1 > DateTime.MinValue)
                {
                    if (where.Length == 0)
                        where = setDateFilter("INSPECTION_DATE", nrf.condition, nrf.date1, nrf.date2);
                    else
                        where = where + " and " + setDateFilter("INSPECTION_DATE", nrf.condition, nrf.date1, nrf.date2);
                }

                if (where.Length > 0)
                    where = "where " + where + "\n";

                if (nrf.countObservations)
                {
                    cmd.CommandText =
                        "select " + select + ", COUNT(REPORTS.REPORT_CODE) as Inspections, SUM(RepObs) as Observations\n" +
                        "from (REPORTS \n" +
                        "left join \n"+
                        "(select REPORT_CODE, COUNT(QUESTION_GUID) as RepObs \n"+
                        "from REPORT_ITEMS \n" +
                        "where Len(OBSERVATION)>0 \n"+
                        "group by REPORT_CODE) as OBS \n"+
                        "on REPORTS.REPORT_CODE=OBS.REPORT_CODE) \n" +
                        "left join VESSELS \n"+
                        "on REPORTS.VESSEL_GUID=VESSELS.VESSEL_GUID \n"+
                        where+
                        "group by "+groupBy;
                }
                else
                {
                    cmd.CommandText =
                        "select " + select + ", COUNT(REPORT_CODE) as Inspections\n" +
                        "from REPORTS left join VESSELS\n" +
                        "on REPORTS.VESSEL_GUID=VESSELS.VESSEL_GUID \n" +
                        where+
                        "group by "+groupBy;
                }

                if (DS.Tables.Contains("INSPECTION_NUMBER_REPORT"))
                {
                    DS.Tables["INSPECTION_NUMBER_REPORT"].Clear();
                    DS.Tables["INSPECTION_NUMBER_REPORT"].Dispose();
                }


                OleDbDataAdapter inrAdapter = new OleDbDataAdapter(cmd);

                inrAdapter.Fill(DS, "INSPECTION_NUMBER_REPORT");

                DataGridView dgvn = new DataGridView();

                dgvn.Parent = this;
                dgvn.AllowUserToAddRows = false;
                dgvn.Columns.Clear();

                dgvn.DataSource = DS;
                dgvn.AutoGenerateColumns = true;
                dgvn.DataMember = "INSPECTION_NUMBER_REPORT";
                
                ExportToExcel2(dgvn,false,1,1);

                if (DS.Tables.Contains("INSPECTION_NUMBER_REPORT"))
                {
                    DS.Tables["INSPECTION_NUMBER_REPORT"].Clear();
                    DS.Tables["INSPECTION_NUMBER_REPORT"].Dispose();
                    //DS.Tables.Remove("INSPECTION_NUMBER_REPORT");
                    DS.Tables["INSPECTION_NUMBER_REPORT"].Columns.Clear();
                }

                dgvn.Columns.Clear();
                dgvn.Dispose();
                

            }
        }

        private void numberOfObservationsByToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showNumberObservationsForm();
        }

        private void showNumberObservationsForm()
        {
            NumberOfObservationsForm nof = new NumberOfObservationsForm(connection, DS, this.Font, this.Icon);

            nof.conditionVessel = norVessel.condition;
            nof.valueVessel = norVessel.value;

            nof.conditionInspector = norInspector.condition;
            nof.valueInspector = norInspector.value;

            nof.conditionDate = norDate.condition;
            nof.valueDate1 = norDate.value;
            nof.valueDate2 = norDate.value2;

            nof.conditionObservation = norObservationCause.condition;
            nof.valueObservation = norObservationCause.value;

            nof.conditionCompany = norCompany.condition;
            nof.valueCompany = norCompany.value;

            nof.conditionOffice = norOffice.condition;
            nof.valueOffice = norOffice.value;

            nof.conditionDOC = norDOC.condition;
            nof.valueDOC = norDOC.value;

            var rslt = nof.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                norVessel.condition = nof.conditionVessel;
                norVessel.value = nof.valueVessel;

                norInspector.condition = nof.conditionInspector;
                norInspector.value = nof.valueInspector;

                norDate.condition = nof.conditionDate;
                norDate.value = nof.valueDate1;
                norDate.value2 = nof.valueDate2;

                norObservationCause.condition = nof.conditionObservation;
                norObservationCause.value = nof.valueObservation;

                norCompany.condition = nof.conditionCompany;
                norCompany.value = nof.valueCompany;

                norOffice.condition = nof.conditionOffice;
                norOffice.value = nof.valueOffice;

                norDOC.condition = nof.conditionDOC;
                norDOC.value = nof.valueDOC;

                string where = "";

                if (isValidCondition(norVessel.condition,norVessel.value))
                {
                    if (where.Length==0)
                        where = setTextFilter("VESSELS.VESSEL_NAME", norVessel.condition, norVessel.value);
                    else
                        where = where + " and " + setTextFilter("VESSELS.VESSEL_NAME", norVessel.condition, norVessel.value);
                }

                if (isValidCondition(norInspector.condition,norInspector.value))
                {
                    if (where.Length == 0)
                        where = setTextFilter("INSPECTORS.INSPECTOR_NAME", norInspector.condition, norInspector.value);
                    else
                        where = where + " and " + setTextFilter("INSPECTORS.INSPECTOR_NAME", norInspector.condition, norInspector.value);
                }

                if (norDate.condition.Length > 0)
                {
                    if (where.Length == 0)
                        where = setDateFilter("REPORTS.INSPECTION_DATE", norDate.condition, norDate.value, norDate.value2);
                    else
                        where = where + " and " + setDateFilter("REPORTS.INSPECTION_DATE", norDate.condition, norDate.value, norDate.value2);
                }

                if (isValidCondition(norOffice.condition,norOffice.value))
                {
                    if (where.Length == 0)
                        where = setTextFilter("VESSELS.OFFICE", norOffice.condition, norOffice.value);
                    else
                        where = where + " and " + setTextFilter("VESSELS.OFFICE", norOffice.condition, norOffice.value);
                }

                if (isValidCondition(norDOC.condition, norDOC.value))
                {
                    if (where.Length == 0)
                        where = setTextFilter("VESSELS.DOC", norDOC.condition, norDOC.value);
                    else
                        where = where + " and " + setTextFilter("VESSELS.DOC", norDOC.condition, norDOC.value);
                }

                if (isValidCondition(norCompany.condition, norCompany.value))
                {
                    if (where.Length == 0)
                        where = setTextFilter("REPORTS.COMPANY", norCompany.condition, norCompany.value);
                    else
                        where = where + " and " + setTextFilter("REPORTS.COMPANY", norCompany.condition, norCompany.value);
                }


                if (where.Length > 0)
                    where = "and " + where;

                OleDbCommand cmd = new OleDbCommand("", connection);

                if (nof.useMapping)
                {
                    cmd.CommandText=
                        "select \n"+
                        "QUESTION_NUMBER as [Question No], \n"+
                        "QN_MAPPED as [Mapped No], \n"+
                        "VIQTYPE as [VIQ Type], \n"+
                        "COUNT(QN_MAPPED) as Observations \n"+
                        "from\n"+
                        "(select \n"+
                        "REPORT_ITEMS.QUESTION_NUMBER, \n" +
                        "QUESTION_KEYS.QN_MAPPED, \n"+
                        "iif(LEFT(REPORT_ITEMS.QUESTION_NUMBER,2)='8.',RIGHT(TEMPLATES.TYPE_CODE,2),'') as VIQTYPE, \n" +
                        "TEMPLATE_QUESTIONS.SEQUENCE, \n"+
                        "REPORTS.REPORT_CODE \n"+
                        "from \n"+
                        "((((REPORT_ITEMS inner join TEMPLATE_QUESTIONS \n" +
                        "on REPORT_ITEMS.QUESTION_GUID=TEMPLATE_QUESTIONS.QUESTION_GUID) \n" +
                        "left join TEMPLATES \n"+
                        "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID)  \n"+
                        "inner join REPORTS \n"+
                        "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE) \n" +
                        "left join VESSELS \n"+
                        "on VESSELS.VESSEL_GUID=REPORTS.VESSEL_GUID) \n"+
                        "left join QUESTION_KEYS  \n"+
                        "on REPORT_ITEMS.QUESTION_GUID=QUESTION_KEYS.QUESTION_GUID \n" +
                        "where LEN(OBSERVATION)>0 \n"+
                        where+"\n"+
                        "group by  \n"+
                        "REPORT_ITEMS.QUESTION_NUMBER,  \n" +
                        "QUESTION_KEYS.QN_MAPPED, \n"+
                        "TEMPLATE_QUESTIONS.SEQUENCE, \n" +
                        "iif(LEFT(REPORT_ITEMS.QUESTION_NUMBER,2)='8.',RIGHT(TEMPLATES.TYPE_CODE,2),''),  \n" +
                        "REPORTS.REPORT_CODE) \n"+
                        "group by \n"+
                        "QUESTION_NUMBER,  \n"+
                        "QN_MAPPED,  \n"+
                        "VIQTYPE, \n"+
                        "TEMPLATE_QUESTIONS.SEQUENCE \n" +
                        "order by TEMPLATE_QUESTIONS.SEQUENCE";
                }
                else
                {
                    cmd.CommandText =
                        "select QUESTION_NUMBER as [Question No], COUNT(QUESTION_NUMBER) as Observations \n" +
                        "from \n" +
                        "(select REPORT_ITEMS.QUESTION_NUMBER, REPORTS.REPORT_CODE, TEMPLATE_QUESTIONS.SEQUENCE \n" +
                        "from \n" +
                        "(((REPORT_ITEMS left join TEMPLATE_QUESTIONS \n" +
                        "on REPORT_ITEMS.QUESTION_GUID=TEMPLATE_QUESTIONS.QUESTION_GUID) \n" +
                        "left join TEMPLATES \n" +
                        "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID)  \n" +
                        "inner join REPORTS \n" +
                        "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE) \n" +
                        "left join VESSELS \n" +
                        "on VESSELS.VESSEL_GUID=REPORTS.VESSEL_GUID \n" +
                        "where LEN(OBSERVATION)>0 \n" +
                        where + "\n" +
                        "group by REPORT_ITEMS.QUESTION_NUMBER, REPORTS.REPORT_CODE, TEMPLATE_QUESTIONS.SEQUENCE) as Q \n" +
                        "group by QUESTION_NUMBER, TEMPLATE_QUESTIONS.SEQUENCE \n" +
                        "order by TEMPLATE_QUESTIONS.SEQUENCE";
                }

                OleDbDataAdapter norAdapter = new OleDbDataAdapter(cmd);

                if (DS.Tables.Contains("NUMBER_OBSERVATION_REPORT"))
                {
                    DS.Tables["NUMBER_OBSERVATION_REPORT"].Clear();
                }

                norAdapter.Fill(DS, "NUMBER_OBSERVATION_REPORT");

                DataGridView dgv = new DataGridView();

                dgv.Parent = this;

                dgv.DataSource = DS;
                dgv.AutoGenerateColumns = true;
                dgv.AllowUserToAddRows = false;
                dgv.DataMember = "NUMBER_OBSERVATION_REPORT";

                ExportToExcel2(dgv,false,1,1);

                DS.Tables["NUMBER_OBSERVATION_REPORT"].Dispose();
            }

        }

        private void numberOfObservationsByInspectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InspectionsPerInspectorReport();
        }

        private void InspectionsPerInspectorReport()
        {

            NumberOfObservationsByInspector nbif = new NumberOfObservationsByInspector(this.Font, this.Icon);

            var rslt = nbif.ShowDialog();


            if (rslt == DialogResult.OK)
            {
                OleDbCommand cmd = new OleDbCommand("", connection);

                string where2 = "";

                if (nbif.hideInspectors)
                    where2 = "where Inspections>0";

                if (nbif.conditionDate.Length > 0)
                {
                    string where = setDateFilter("REPORTS.INSPECTION_DATE", nbif.conditionDate, nbif.valueDate1, nbif.valueDate2);

                    cmd.CommandText =
                        "select * \n" +
                        "from ( \n" +
                        "select \n" +
                        "INSPECTORS.INSPECTOR_NAME,  \n" +
                        "ICOUNT as Inspections, \n" +
                        "QCOUNT as [Total Obs], \n" +
                        "QCOUNT/ICOUNT as [Obs per insp], \n" +
                        "SUM(QCOUNT1) as [Chapter 1],  \n" +
                        "SUM(QCOUNT2) as [Chapter 2],  \n" +
                        "SUM(QCOUNT3) as [Chapter 3],  \n" +
                        "SUM(QCOUNT4) as [Chapter 4], \n" +
                        "SUM(QCOUNT5) as [Chapter 5], \n" +
                        "SUM(QCOUNT6) as [Chapter 6], \n" +
                        "SUM(QCOUNT7) as [Chapter 7], \n" +
                        "SUM(QCOUNT8) as [Chapter 8], \n" +
                        "SUM(QCOUNT9) as [Chapter 9], \n" +
                        "SUM(QCOUNT10) as [Chapter 10], \n" +
                        "SUM(QCOUNT11) as [Chapter 11], \n" +
                        "SUM(QCOUNT12) as [Chapter 12], \n" +
                        "SUM(QCOUNT13) as [Chapter 13] \n" +
                        "from \n" +
                        "(((((((((((((((INSPECTORS left join REPORTS \n" +
                        "on INSPECTORS.INSPECTOR_GUID=REPORTS.INSPECTOR_GUID) \n" +
                        "left join \n" +
                        "(select REPORTS.INSPECTOR_GUID, COUNT(REPORT_CODE) as ICOUNT \n" +
                        "from REPORTS \n" +
                        "where " + where + " \n" +
                        "group by INSPECTOR_GUID) as QI \n" +
                        "on INSPECTORS.INSPECTOR_GOID=QI.INSPECTOR_GOID) \n" +
                        "left join \n" +
                        "(select REPORTS.INSPECTOR_GUID, COUNT(QUESTION_NUMBER) AS QCOUNT \n" +
                        "from REPORT_ITEMS inner join REPORTS \n" +
                        "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE \n" +
                        "where not IsNull(OBSERVATION) and LEN(OBSERVATION)>0 \n" +
                        "and " + where + " \n" +
                        "group by INSPECTOR_GUID) as QC \n" +
                        "on INSPECTORS.INSPECTOR_GUID=QC.INSPECTOR_GUID) \n" +
                        "left join \n" +
                        "(select REPORT_ITEMS.REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT1 \n" +
                        "from REPORT_ITEMS inner join REPORTS \n" +
                        "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE \n" +
                        "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='1') \n" +
                        "and LEN(OBSERVATION)>0 \n" +
                        " and " + where + " \n" +
                        "group by REPORT_ITEMS.REPORT_CODE) as Q1 \n" +
                        "on REPORTS.REPORT_CODE=Q1.REPORT_CODE) \n" +
                        "left join \n" +
                        "(select REPORT_ITEMS.REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT2 \n" +
                        "from REPORT_ITEMS inner join REPORTS \n" +
                        "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE \n" +
                        "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='2') \n" +
                        "and LEN(OBSERVATION)>0 \n" +
                        "and " + where + " \n" +
                        "group by REPORT_ITEMS.REPORT_CODE) as Q2 \n" +
                        "on REPORTS.REPORT_CODE=Q2.REPORT_CODE) \n" +
                        "left join \n" +
                        "(select REPORT_ITEMS.REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT3 \n" +
                        "from REPORT_ITEMS inner join REPORTS \n" +
                        "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE \n" +
                        "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='3') \n" +
                        "and LEN(OBSERVATION)>0 \n" +
                        "and " + where + " \n" +
                        "group by REPORT_ITEMS.REPORT_CODE) as Q3 \n" +
                        "on REPORTS.REPORT_CODE=Q3.REPORT_CODE) \n" +
                        "left join \n" +
                        "(select REPORT_ITEMS.REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT4 \n" +
                        "from REPORT_ITEMS inner join REPORTS \n" +
                        "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE \n" +
                        "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='4') \n" +
                        "and LEN(OBSERVATION)>0 \n" +
                        "and " + where + " \n" +
                        "group by REPORT_ITEMS.REPORT_CODE) as Q4 \n" +
                        "on REPORTS.REPORT_CODE=Q4.REPORT_CODE) \n" +
                        "left join \n" +
                        "(select REPORT_ITEMS.REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT5 \n" +
                        "from REPORT_ITEMS inner join REPORTS \n" +
                        "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE \n" +
                        "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='5') \n" +
                        "and LEN(OBSERVATION)>0 \n" +
                        "and " + where + " \n" +
                        "group by REPORT_ITEMS.REPORT_CODE) as Q5 \n" +
                        "on REPORTS.REPORT_CODE=Q5.REPORT_CODE) \n" +
                        "left join \n" +
                        "(select REPORT_ITEMS.REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT6 \n" +
                        "from REPORT_ITEMS inner join REPORTS \n" +
                        "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE \n" +
                        "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='6') \n" +
                        "and LEN(OBSERVATION)>0 \n" +
                        "and " + where + " \n" +
                        "group by REPORT_ITEMS.REPORT_CODE) as Q6 \n" +
                        "on REPORTS.REPORT_CODE=Q6.REPORT_CODE) \n" +
                        "left join \n" +
                        "(select REPORT_ITEMS.REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT7 \n" +
                        "from REPORT_ITEMS inner join REPORTS \n" +
                        "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE \n" +
                        "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='7') \n" +
                        "and LEN(OBSERVATION)>0 \n" +
                        "and " + where + " \n" +
                        "group by REPORT_ITEMS.REPORT_CODE) as Q7 \n" +
                        "on REPORTS.REPORT_CODE=Q7.REPORT_CODE) \n" +
                        "left join \n" +
                        "(select REPORT_ITEMS.REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT8 \n" +
                        "from REPORT_ITEMS inner join REPORTS \n" +
                        "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE \n" +
                        "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='8') \n" +
                        "and LEN(OBSERVATION)>0 \n" +
                        "and " + where + " \n" +
                        "group by REPORT_ITEMS.REPORT_CODE) as Q8 \n" +
                        "on REPORTS.REPORT_CODE=Q8.REPORT_CODE) \n" +
                        "left join \n" +
                        "(select REPORT_ITEMS.REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT9 \n" +
                        "from REPORT_ITEMS inner join REPORTS \n" +
                        "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE \n" +
                        "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='9') \n" +
                        "and LEN(OBSERVATION)>0 \n" +
                        "and " + where + " \n" +
                        "group by REPORT_ITEMS.REPORT_CODE) as Q9 \n" +
                        "on REPORTS.REPORT_CODE=Q9.REPORT_CODE) \n" +
                        "left join \n" +
                        "(select REPORT_ITEMS.REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT10 \n" +
                        "from REPORT_ITEMS inner join REPORTS \n" +
                        "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE \n" +
                        "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='10') \n" +
                        "and LEN(OBSERVATION)>0 \n" +
                        "and " + where + " \n" +
                        "group by REPORT_ITEMS.REPORT_CODE) as Q10 \n" +
                        "on REPORTS.REPORT_CODE=Q10.REPORT_CODE) \n" +
                        "left join \n" +
                        "(select REPORT_ITEMS.REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT11 \n" +
                        "from REPORT_ITEMS inner join REPORTS \n" +
                        "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE \n" +
                        "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='11') \n" +
                        "and LEN(OBSERVATION)>0 \n" +
                        "and " + where + " \n" +
                        "group by REPORT_ITEMS.REPORT_CODE) as Q11 \n" +
                        "on REPORTS.REPORT_CODE=Q11.REPORT_CODE) \n" +
                        "left join \n" +
                        "(select REPORT_ITEMS.REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT12 \n" +
                        "from REPORT_ITEMS inner join REPORTS \n" +
                        "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE \n" +
                        "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='12') \n" +
                        "and LEN(OBSERVATION)>0 \n" +
                        "and " + where + " \n" +
                        "group by REPORT_ITEMS.REPORT_CODE) as Q12 \n" +
                        "on REPORTS.REPORT_CODE=Q12.REPORT_CODE) \n" +
                        "left join \n" +
                        "(select REPORT_ITEMS.REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT13 \n" +
                        "from REPORT_ITEMS inner join REPORTS \n" +
                        "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE \n" +
                        "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='13') \n" +
                        "and LEN(OBSERVATION)>0 \n" +
                        "and " + where + " \n" +
                        "group by REPORT_ITEMS.REPORT_CODE) as Q13 \n" +
                        "on REPORTS.REPORT_CODE=Q13.REPORT_CODE \n" +
                        "group by INSPECTORS.INSPECTOR_NAME, ICOUNT, QCOUNT \n" +
                        ") as QQQ \n" +
                        where2;
                }
                else
                {
                    cmd.CommandText =
                        "select * from ( \n"+
                        "select \n" +
                        "INSPECTORS.INSPECTOR_NAME,  \n" +
                        "ICOUNT as Inspections, \n" +
                        "QCOUNT as [Total Obs], \n" +
                        "QCOUNT/ICOUNT as [Obs per insp], \n" +
                        "SUM(QCOUNT1) as [Chapter 1],  \n" +
                        "SUM(QCOUNT2) as [Chapter 2],  \n" +
                        "SUM(QCOUNT3) as [Chapter 3],  \n" +
                        "SUM(QCOUNT4) as [Chapter 4], \n" +
                        "SUM(QCOUNT5) as [Chapter 5], \n" +
                        "SUM(QCOUNT6) as [Chapter 6], \n" +
                        "SUM(QCOUNT7) as [Chapter 7], \n" +
                        "SUM(QCOUNT8) as [Chapter 8], \n" +
                        "SUM(QCOUNT9) as [Chapter 9], \n" +
                        "SUM(QCOUNT10) as [Chapter 10], \n" +
                        "SUM(QCOUNT11) as [Chapter 11], \n" +
                        "SUM(QCOUNT12) as [Chapter 12], \n" +
                        "SUM(QCOUNT13) as [Chapter 13] \n" +
                        "from \n" +
                        "(((((((((((((((INSPECTORS left join REPORTS \n" +
                        "on INSPECTORS.INSPECTOR_GUID=REPORTS.INSPECTOR_GUID) \n" +
                        "left join \n" +
                        "(select REPORTS.INSPECTOR_GUID, COUNT(REPORT_CODE) as ICOUNT \n" +
                        "from REPORTS \n" +
                        "group by INSPECTOR_GUID) as QI \n" +
                        "on INSPECTORS.INSPECTOR_GUID=QI.INSPECTOR_GUID) \n" +
                        "left join \n" +
                        "(select REPORTS.INSPECTOR_GUID, COUNT(QUESTION_NUMBER) AS QCOUNT \n" +
                        "from REPORT_ITEMS inner join REPORTS \n" +
                        "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE \n" +
                        "where not IsNull(OBSERVATION) and LEN(OBSERVATION)>0 \n" +
                        "group by INSPECTOR_GUID) as QC \n" +
                        "on INSPECTORS.INSPECTOR_GUID=QC.INSPECTOR_GUID) \n" +
                        "left join \n" +
                        "(select REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT1 \n" +
                        "from REPORT_ITEMS \n" +
                        "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='1') \n" +
                        "and LEN(OBSERVATION)>0 \n" +
                        "group by REPORT_CODE) as Q1 \n" +
                        "on REPORTS.REPORT_CODE=Q1.REPORT_CODE) \n" +
                        "left join \n" +
                        "(select REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT2 \n" +
                        "from REPORT_ITEMS \n" +
                        "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='2') \n" +
                        "and LEN(OBSERVATION)>0 \n" +
                        "group by REPORT_CODE) as Q2 \n" +
                        "on REPORTS.REPORT_CODE=Q2.REPORT_CODE) \n" +
                        "left join \n" +
                        "(select REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT3 \n" +
                        "from REPORT_ITEMS \n" +
                        "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='3') \n" +
                        "and LEN(OBSERVATION)>0 \n" +
                        "group by REPORT_CODE) as Q3 \n" +
                        "on REPORTS.REPORT_CODE=Q3.REPORT_CODE) \n" +
                        "left join \n" +
                        "(select REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT4 \n" +
                        "from REPORT_ITEMS \n" +
                        "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='4') \n" +
                        "and LEN(OBSERVATION)>0 \n" +
                        "group by REPORT_CODE) as Q4 \n" +
                        "on REPORTS.REPORT_CODE=Q4.REPORT_CODE) \n" +
                        "left join \n" +
                        "(select REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT5 \n" +
                        "from REPORT_ITEMS \n" +
                        "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='5') \n" +
                        "and LEN(OBSERVATION)>0 \n" +
                        "group by REPORT_CODE) as Q5 \n" +
                        "on REPORTS.REPORT_CODE=Q5.REPORT_CODE) \n" +
                        "left join \n" +
                        "(select REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT6 \n" +
                        "from REPORT_ITEMS \n" +
                        "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='6') \n" +
                        "and LEN(OBSERVATION)>0 \n" +
                        "group by REPORT_CODE) as Q6 \n" +
                        "on REPORTS.REPORT_CODE=Q6.REPORT_CODE) \n" +
                        "left join \n" +
                        "(select REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT7 \n" +
                        "from REPORT_ITEMS \n" +
                        "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='7') \n" +
                        "and LEN(OBSERVATION)>0 \n" +
                        "group by REPORT_CODE) as Q7 \n" +
                        "on REPORTS.REPORT_CODE=Q7.REPORT_CODE) \n" +
                        "left join \n" +
                        "(select REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT8 \n" +
                        "from REPORT_ITEMS \n" +
                        "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='8') \n" +
                        "and LEN(OBSERVATION)>0 \n" +
                        "group by REPORT_CODE) as Q8 \n" +
                        "on REPORTS.REPORT_CODE=Q8.REPORT_CODE) \n" +
                        "left join \n" +
                        "(select REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT9 \n" +
                        "from REPORT_ITEMS \n" +
                        "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='9') \n" +
                        "and LEN(OBSERVATION)>0 \n" +
                        "group by REPORT_CODE) as Q9 \n" +
                        "on REPORTS.REPORT_CODE=Q9.REPORT_CODE) \n" +
                        "left join \n" +
                        "(select REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT10 \n" +
                        "from REPORT_ITEMS \n" +
                        "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='10') \n" +
                        "and LEN(OBSERVATION)>0 \n" +
                        "group by REPORT_CODE) as Q10 \n" +
                        "on REPORTS.REPORT_CODE=Q10.REPORT_CODE) \n" +
                        "left join \n" +
                        "(select REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT11 \n" +
                        "from REPORT_ITEMS \n" +
                        "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='11') \n" +
                        "and LEN(OBSERVATION)>0 \n" +
                        "group by REPORT_CODE) as Q11 \n" +
                        "on REPORTS.REPORT_CODE=Q11.REPORT_CODE) \n" +
                        "left join \n" +
                        "(select REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT12 \n" +
                        "from REPORT_ITEMS \n" +
                        "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='12') \n" +
                        "and LEN(OBSERVATION)>0 \n" +
                        "group by REPORT_CODE) as Q12 \n" +
                        "on REPORTS.REPORT_CODE=Q12.REPORT_CODE) \n" +
                        "left join \n" +
                        "(select REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT13 \n" +
                        "from REPORT_ITEMS \n" +
                        "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='13') \n" +
                        "and LEN(OBSERVATION)>0 \n" +
                        "group by REPORT_CODE) as Q13 \n" +
                        "on REPORTS.REPORT_CODE=Q13.REPORT_CODE \n" +
                        "group by INSPECTORS.INSPECTOR_NAME, ICOUNT, QCOUNT) \n"+
                        where2;
                }

                OleDbDataAdapter inAdapter = new OleDbDataAdapter(cmd);

                if (DS.Tables.Contains("INSPECTION_PER_INSPECTOR_REPORT"))
                    DS.Tables["INSPECTION_PER_INSPECTOR_REPORT"].Clear();

                this.Update();

                this.Cursor = Cursors.WaitCursor;

                inAdapter.Fill(DS, "INSPECTION_PER_INSPECTOR_REPORT");

                DataGridView dgv = new DataGridView();

                dgv.Parent = this;

                dgv.DataSource = DS;
                dgv.AutoGenerateColumns = true;
                dgv.AllowUserToAddRows = false;
                dgv.DataMember = "INSPECTION_PER_INSPECTOR_REPORT";

                ExportToExcel2(dgv, true, 1,1);

                DS.Tables["INSPECTION_PER_INSPECTOR_REPORT"].Dispose();

                this.Cursor = Cursors.Default;
            }
        }

        private void vIQToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void updateVIQStatistics()
        {
            StatisticFilterForm sff = new StatisticFilterForm();

            sff.valueDate1 = statDate.value;
            sff.valueDate2 = statDate.value2;
            sff.conditionDate = statDate.condition;

            sff.conditionChapterNumber = statChapter.condition;
            sff.valueChapterNumber = statChapter.value;

            sff.conditionQType = statType.condition;
            sff.valueQType = statType.value;

            sff.conditionOffice = statOffice.condition;
            sff.valueOffice = statOffice.value;

            sff.conditionDOC = statDOC.condition;
            sff.valueDOC = statDOC.value;

            sff.useMapping = viqStatisticUseMapping;
            sff.groupByType = viqGroupByType;

            var rslt = sff.ShowDialog();

            string subchapterSQL = "";
            
            if (rslt == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;

                statDate.condition = sff.conditionDate;
                statDate.value = sff.valueDate1;
                statDate.value2 = sff.valueDate2;

                statChapter.condition = sff.conditionChapterNumber;
                statChapter.value = sff.valueChapterNumber;

                statType.condition = sff.conditionQType;
                statType.value = sff.valueQType;

                statOffice.condition = sff.conditionOffice;
                statOffice.value = sff.valueOffice;

                statDOC.condition = sff.conditionDOC;
                statDOC.value = sff.valueDOC;

                viqStatisticUseMapping = sff.useMapping;
                viqGroupByType = sff.groupByType;
                
                string where = "";

                if (isValidCondition(statChapter.condition,statChapter.value))
                {
                    if (where.Length == 0)
                        where = setTextFilter("TEMPLATE_QUESTIONS.CHAPTER_NUMBER", statChapter.condition, statChapter.value);
                    else
                        where = where + " and " + setTextFilter("TEMPLATE_QUESTIONS.CHAPTER_NUMBER", statChapter.condition, statChapter.value);
                }

                if (isValidCondition(statType.condition,statType.value))
                {
                    if (where.Length == 0)
                    {
                        if (viqStatisticUseMapping)
                            where = setTextFilter("QTYPE", statType.condition, statType.value);
                        else
                            where = setTextFilter("RIGHT(TEMPLATES.TYPE_CODE,2)", statType.condition, statType.value);
                    }
                    else
                    {
                        if (viqStatisticUseMapping)
                            where = where + " and " + setTextFilter("QTYPE", statType.condition, statType.value);
                        else
                            where = where + " and " + setTextFilter("RIGHT(TEMPLATES.TYPE_CODE,2)", statType.condition, statType.value);
                    }
                }

                if (isValidCondition(statOffice.condition,statOffice.value))
                {
                    if (where.Length == 0)
                    {
                            where = setTextFilter("VESSELS.OFFICE", statOffice.condition, statOffice.value);
                    }
                    else
                    {
                        where = where + " and " + setTextFilter("VESSELS.OFFICE", statOffice.condition, statOffice.value);
                    }
                }

                if (isValidCondition(statDOC.condition, statDOC.value))
                {
                    if (where.Length == 0)
                    {
                        where = setTextFilter("VESSELS.DOC", statDOC.condition, statDOC.value);
                    }
                    else
                    {
                        where = where + " and " + setTextFilter("VESSELS.DOC", statDOC.condition, statDOC.value);
                    }
                }

                if (statDate.condition.Length > 0)
                {
                    if (where.Length == 0)
                        where = setDateFilter("REPORTS.INSPECTION_DATE", statDate.condition, statDate.value, statDate.value2);
                    else
                        where = where + " and " + setDateFilter("REPORTS.INSPECTION_DATE", statDate.condition, statDate.value, statDate.value2);
                }


                if (where.Length > 0)
                    where = "and " + where;

                OleDbCommand cmd = new OleDbCommand("", connection);

                if (viqStatisticUseMapping)
                {
                    if (viqGroupByType)
                    {
                        cmd.CommandText =
                            "select CHAPTER_NUMBER as [Chapter], SECTION_NAME as [Subchapter], QUESTION_NUMBER as [Question No], "+
                            "QTYPE as [Type], COUNT(QUESTION_NUMBER) As [Observations] \n" +
                            "from \n" +
                            "( \n" +
                            "select VQ.QUESTION_NUMBER, VQ.SECTION_NAME, VQ.CHAPTER_NUMBER,VQ.QTYPE \n" +
                            "from \n" +
                            "((REPORT_ITEMS inner join  \n" +
                            "( \n" +
                            "select DISTINCT TEMPLATE_QUESTIONS.QUESTION_GUID, TEMPLATE_QUESTIONS.QUESTION_NUMBER, "+
                            "TEMPLATE_QUESTIONS.CHAPTER_NUMBER, TEMPLATE_QUESTIONS.SECTION_NAME, RIGHT(TEMPLATES.TYPE_CODE,2) AS QTYPE \n" +
                            "from  \n" +
                            "(TEMPLATE_QUESTIONS inner join \n" +
                            "( \n" +
                            "	select TEMPLATES.* \n" +
                            "	from \n" +
                            "		TEMPLATES inner join  \n" +
                            "		( \n" +
                            "			select MAX(VERSION) as MaxVersion, RIGHT(TYPE_CODE,2) as TYPE \n" +
                            "			from TEMPLATES \n" +
                            "			group by RIGHT(TYPE_CODE,2) \n" +
                            "		) as LastTemplates \n" +
                            "		on TEMPLATES.VERSION=LastTemplates.MaxVersion and  \n" +
                            "		RIGHT(TEMPLATES.TYPE_CODE,2)=LastTemplates.TYPE \n" +
                            ") as LT \n" +
                            "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=LT.TEMPLATE_GUID) \n" +
                            "left join TEMPLATES \n" +
                            "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID \n" +
                            "union \n" +
                            "select DISTINCT MQ.QUESTION_SLAVE_GUID, TEMPLATE_QUESTIONS.QUESTION_NUMBER, TEMPLATE_QUESTIONS.CHAPTER_NUMBER, "+
                            "TEMPLATE_QUESTIONS.SECTION_NAME, RIGHT(TEMPLATES.TYPE_CODE,2) as QTYPE \n" +
                            "from  \n" +
                            "	( \n" +
                            "		( \n" +
                            "			select DISTINCT QUESTIONS_MAPPING.QUESTION_SLAVE_GUID, QUESTIONS_MAPPING.QUESTION_MASTER_GUID \n" +
                            "			from \n" +
                            "				( \n" +
                            "					TEMPLATE_QUESTIONS inner join \n" +
                            "					( \n" +
                            "						select TEMPLATES.* \n" +
                            "						from \n" +
                            "							TEMPLATES inner join  \n" +
                            "							( \n" +
                            "								select MAX(VERSION) as MaxVersion, RIGHT(TYPE_CODE,2) as TYPE \n" +
                            "								from TEMPLATES \n" +
                            "								group by RIGHT(TYPE_CODE,2) \n" +
                            "							) as LastTemplates \n" +
                            "							on TEMPLATES.VERSION=LastTemplates.MaxVersion and  \n" +
                            "								RIGHT(TEMPLATES.TYPE_CODE,2)=LastTemplates.TYPE \n" +
                            "					) as LT \n" +
                            "					on TEMPLATE_QUESTIONS.TEMPLATE_GUID=LT.TEMPLATE_GUID \n" +
                            "				) \n" +
                            "				inner join QUESTIONS_MAPPING \n" +
                            "				on TEMPLATE_QUESTIONS.QUESTION_GUID=QUESTIONS_MAPPING.QUESTION_MASTER_GUID \n" +
                            "		) as MQ \n" +
                            "		inner join TEMPLATE_QUESTIONS \n" +
                            "		on MQ.QUESTION_MASTER_GUID=TEMPLATE_QUESTIONS.QUESTION_GUID \n" +
                            "	) \n" +
                            "	left join TEMPLATES \n" +
                            "	on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID \n" +
                            ") as VQ \n" +
                            "on REPORT_ITEMS.QUESTION_GUID=VQ.QUESTION_GUID) \n" +
                            "inner join REPORTS \n"+
                            "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE) \n" +
                            "inner join VESSELS \n"+
                            "on REPORTS.VESSEL_GUID=VESSELS.VESSEL_GUID \n"+
                            "where LEN(REPORT_ITEMS.OBSERVATION)>0 and not IsNull(REPORT_ITEMS.QUESTION_GUID) \n" +
                            where + " \n"+
                            ") \n" +
                            "group by CHAPTER_NUMBER, SECTION_NAME, QUESTION_NUMBER, QTYPE \n" +
                            "order by LEN(CHAPTER_NUMBER), CHAPTER_NUMBER, LEN(QUESTION_NUMBER), QUESTION_NUMBER, QTYPE";

                    subchapterSQL=
                            "select SECTION_NAME as [Subchapter], QTYPE as [Type], COUNT(QUESTION_NUMBER) As [Observations] \n" +
                            "from \n" +
                            "( \n" +
                            "select VQ.QUESTION_NUMBER, VQ.SECTION_NAME, VQ.CHAPTER_NUMBER,VQ.QTYPE \n" +
                            "from \n" +
                            "((REPORT_ITEMS inner join  \n" +
                            "( \n" +
                            "select DISTINCT TEMPLATE_QUESTIONS.QUESTION_GUID, TEMPLATE_QUESTIONS.QUESTION_NUMBER, " +
                            "TEMPLATE_QUESTIONS.CHAPTER_NUMBER, TEMPLATE_QUESTIONS.SECTION_NAME, RIGHT(TEMPLATES.TYPE_CODE,2) AS QTYPE \n" +
                            "from  \n" +
                            "(TEMPLATE_QUESTIONS inner join \n" +
                            "( \n" +
                            "	select TEMPLATES.* \n" +
                            "	from \n" +
                            "		TEMPLATES inner join  \n" +
                            "		( \n" +
                            "			select MAX(VERSION) as MaxVersion, RIGHT(TYPE_CODE,2) as TYPE \n" +
                            "			from TEMPLATES \n" +
                            "			group by RIGHT(TYPE_CODE,2) \n" +
                            "		) as LastTemplates \n" +
                            "		on TEMPLATES.VERSION=LastTemplates.MaxVersion and  \n" +
                            "		RIGHT(TEMPLATES.TYPE_CODE,2)=LastTemplates.TYPE \n" +
                            ") as LT \n" +
                            "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=LT.TEMPLATE_GUID) \n" +
                            "left join TEMPLATES \n" +
                            "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID \n" +
                            "union \n" +
                            "select DISTINCT MQ.QUESTION_SLAVE_GUID, TEMPLATE_QUESTIONS.QUESTION_NUMBER, TEMPLATE_QUESTIONS.CHAPTER_NUMBER, " +
                            "TEMPLATE_QUESTIONS.SECTION_NAME, RIGHT(TEMPLATES.TYPE_CODE,2) as QTYPE \n" +
                            "from  \n" +
                            "	( \n" +
                            "		( \n" +
                            "			select DISTINCT QUESTIONS_MAPPING.QUESTION_SLAVE_GUID, QUESTIONS_MAPPING.QUESTION_MASTER_GUID \n" +
                            "			from \n" +
                            "				( \n" +
                            "					TEMPLATE_QUESTIONS inner join \n" +
                            "					( \n" +
                            "						select TEMPLATES.* \n" +
                            "						from \n" +
                            "							TEMPLATES inner join  \n" +
                            "							( \n" +
                            "								select MAX(VERSION) as MaxVersion, RIGHT(TYPE_CODE,2) as TYPE \n" +
                            "								from TEMPLATES \n" +
                            "								group by RIGHT(TYPE_CODE,2) \n" +
                            "							) as LastTemplates \n" +
                            "							on TEMPLATES.VERSION=LastTemplates.MaxVersion and  \n" +
                            "								RIGHT(TEMPLATES.TYPE_CODE,2)=LastTemplates.TYPE \n" +
                            "					) as LT \n" +
                            "					on TEMPLATE_QUESTIONS.TEMPLATE_GUID=LT.TEMPLATE_GUID \n" +
                            "				) \n" +
                            "				inner join QUESTIONS_MAPPING \n" +
                            "				on TEMPLATE_QUESTIONS.QUESTION_GUID=QUESTIONS_MAPPING.QUESTION_MASTER_GUID \n" +
                            "		) as MQ \n" +
                            "		inner join TEMPLATE_QUESTIONS \n" +
                            "		on MQ.QUESTION_MASTER_GUID=TEMPLATE_QUESTIONS.QUESTION_GUID \n" +
                            "	) \n" +
                            "	left join TEMPLATES \n" +
                            "	on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID \n" +
                            ") as VQ \n" +
                            "on REPORT_ITEMS.QUESTION_GUID=VQ.QUESTION_GUID) \n" +
                            "inner join REPORTS \n" +
                            "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE ) \n" +
                            "inner join VESSELS \n"+
                            "on REPORTS.VESSEL_GUID=VESSELS.VESSEL_GUID \n"+
                            "where LEN(REPORT_ITEMS.OBSERVATION)>0 and not IsNull(REPORT_ITEMS.QUESTION_GUID) \n" +
                            where + " \n" +
                            ") \n" +
                            "group by SECTION_NAME, QTYPE \n" +
                            "order by Min(QUESTION_NUMBER), SECTION_NAME, QTYPE";
                    }
                    else
                    {
                        cmd.CommandText =
                            "select CHAPTER_NUMBER as [Chapter], SECTION_NAME as [Subchapter], QUESTION_NUMBER as [Question No], "+
                            "COUNT(QUESTION_NUMBER) As Observations \n" +
                            "from \n"+
                            "( \n"+
                            "select VQ.QUESTION_NUMBER,VQ.CHAPTER_NUMBER, VQ.SECTION_NAME, VQ.QTYPE \n"+
                            "from \n"+
                            "((REPORT_ITEMS inner join  \n" +
                            "( \n"+
                            "select DISTINCT TEMPLATE_QUESTIONS.QUESTION_GUID, TEMPLATE_QUESTIONS.QUESTION_NUMBER, "+
                            "TEMPLATE_QUESTIONS.CHAPTER_NUMBER, TEMPLATE_QUESTIONS.SECTION_NAME, RIGHT(TEMPLATES.TYPE_CODE,2) AS QTYPE \n"+
                            "from  \n"+
                            "(TEMPLATE_QUESTIONS inner join \n"+
                            "( \n"+
                            "	select TEMPLATES.* \n"+
                            "	from \n"+
                            "		TEMPLATES inner join  \n"+
                            "		( \n"+
                            "			select MAX(VERSION) as MaxVersion, RIGHT(TYPE_CODE,2) as TYPE \n"+
                            "			from TEMPLATES \n"+
                            "			group by RIGHT(TYPE_CODE,2) \n"+
                            "		) as LastTemplates \n"+
                            "		on TEMPLATES.VERSION=LastTemplates.MaxVersion and  \n"+
                            "		RIGHT(TEMPLATES.TYPE_CODE,2)=LastTemplates.TYPE \n"+
                            ") as LT \n"+
                            "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=LT.TEMPLATE_GUID) \n"+
                            "left join TEMPLATES \n"+
                            "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID \n"+
                            "union \n"+
                            "select DISTINCT MQ.QUESTION_SLAVE_GUID, TEMPLATE_QUESTIONS.QUESTION_NUMBER, TEMPLATE_QUESTIONS.CHAPTER_NUMBER, "+
                            "TEMPLATE_QUESTIONS.SECTION_NAME, RIGHT(TEMPLATES.TYPE_CODE,2) as QTYPE \n"+
                            "from  \n"+
                            "	( \n"+
                            "		( \n"+
                            "			select DISTINCT QUESTIONS_MAPPING.QUESTION_SLAVE_GUID, QUESTIONS_MAPPING.QUESTION_MASTER_GUID \n"+
                            "			from \n"+
                            "				( \n"+
                            "					TEMPLATE_QUESTIONS inner join \n"+
                            "					( \n"+
                            "						select TEMPLATES.* \n"+
                            "						from \n"+
                            "							TEMPLATES inner join  \n"+
                            "							( \n"+
                            "								select MAX(VERSION) as MaxVersion, RIGHT(TYPE_CODE,2) as TYPE \n"+
                            "								from TEMPLATES \n"+
                            "								group by RIGHT(TYPE_CODE,2) \n"+
                            "							) as LastTemplates \n"+
                            "							on TEMPLATES.VERSION=LastTemplates.MaxVersion and  \n"+
                            "								RIGHT(TEMPLATES.TYPE_CODE,2)=LastTemplates.TYPE \n"+
                            "					) as LT \n"+
                            "					on TEMPLATE_QUESTIONS.TEMPLATE_GUID=LT.TEMPLATE_GUID \n"+
                            "				) \n"+
                            "				inner join QUESTIONS_MAPPING \n"+
                            "				on TEMPLATE_QUESTIONS.QUESTION_GUID=QUESTIONS_MAPPING.QUESTION_MASTER_GUID \n"+
                            "		) as MQ \n"+
                            "		inner join TEMPLATE_QUESTIONS \n"+
                            "		on MQ.QUESTION_MASTER_GUID=TEMPLATE_QUESTIONS.QUESTION_GUID \n"+
                            "	) \n"+
                            "	left join TEMPLATES \n"+
                            "	on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID \n"+
                            ") as VQ \n"+
                            "on REPORT_ITEMS.QUESTION_GUID=VQ.QUESTION_GUID) \n" +
                            "inner join REPORTS \n"+
                            "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE) \n" +
                            "inner join VESSELS \n"+
                            "on REPORTS.VESSEL_GUID=VESSELS.VESSEL_GUID \n"+
                            "where LEN(REPORT_ITEMS.OBSERVATION)>0 and not IsNull(REPORT_ITEMS.QUESTION_GUID) \n" +
                            where+" \n"+
                            ") \n"+
                            "group by CHAPTER_NUMBER, SECTION_NAME, QUESTION_NUMBER \n"+
                            "order by LEN(CHAPTER_NUMBER), CHAPTER_NUMBER, LEN(QUESTION_NUMBER), QUESTION_NUMBER";

                        subchapterSQL=
                            "select SECTION_NAME as [Subchapter], COUNT(QUESTION_NUMBER) As Observations \n" +
                            "from \n" +
                            "( \n" +
                            "select VQ.QUESTION_NUMBER,VQ.CHAPTER_NUMBER, VQ.SECTION_NAME, VQ.QTYPE \n" +
                            "from \n" +
                            "((REPORT_ITEMS inner join  \n" +
                            "( \n" +
                            "select DISTINCT TEMPLATE_QUESTIONS.QUESTION_GUID, TEMPLATE_QUESTIONS.QUESTION_NUMBER, " +
                            "TEMPLATE_QUESTIONS.CHAPTER_NUMBER, TEMPLATE_QUESTIONS.SECTION_NAME, RIGHT(TEMPLATES.TYPE_CODE,2) AS QTYPE \n" +
                            "from  \n" +
                            "(TEMPLATE_QUESTIONS inner join \n" +
                            "( \n" +
                            "	select TEMPLATES.* \n" +
                            "	from \n" +
                            "		TEMPLATES inner join  \n" +
                            "		( \n" +
                            "			select MAX(VERSION) as MaxVersion, RIGHT(TYPE_CODE,2) as TYPE \n" +
                            "			from TEMPLATES \n" +
                            "			group by RIGHT(TYPE_CODE,2) \n" +
                            "		) as LastTemplates \n" +
                            "		on TEMPLATES.VERSION=LastTemplates.MaxVersion and  \n" +
                            "		RIGHT(TEMPLATES.TYPE_CODE,2)=LastTemplates.TYPE \n" +
                            ") as LT \n" +
                            "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=LT.TEMPLATE_GUID) \n" +
                            "left join TEMPLATES \n" +
                            "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID \n" +
                            "union \n" +
                            "select DISTINCT MQ.QUESTION_SLAVE_GUID, TEMPLATE_QUESTIONS.QUESTION_NUMBER, TEMPLATE_QUESTIONS.CHAPTER_NUMBER, " +
                            "TEMPLATE_QUESTIONS.SECTION_NAME, RIGHT(TEMPLATES.TYPE_CODE,2) as QTYPE \n" +
                            "from  \n" +
                            "	( \n" +
                            "		( \n" +
                            "			select DISTINCT QUESTIONS_MAPPING.QUESTION_SLAVE_GUID, QUESTIONS_MAPPING.QUESTION_MASTER_GUID \n" +
                            "			from \n" +
                            "				( \n" +
                            "					TEMPLATE_QUESTIONS inner join \n" +
                            "					( \n" +
                            "						select TEMPLATES.* \n" +
                            "						from \n" +
                            "							TEMPLATES inner join  \n" +
                            "							( \n" +
                            "								select MAX(VERSION) as MaxVersion, RIGHT(TYPE_CODE,2) as TYPE \n" +
                            "								from TEMPLATES \n" +
                            "								group by RIGHT(TYPE_CODE,2) \n" +
                            "							) as LastTemplates \n" +
                            "							on TEMPLATES.VERSION=LastTemplates.MaxVersion and  \n" +
                            "								RIGHT(TEMPLATES.TYPE_CODE,2)=LastTemplates.TYPE \n" +
                            "					) as LT \n" +
                            "					on TEMPLATE_QUESTIONS.TEMPLATE_GUID=LT.TEMPLATE_GUID \n" +
                            "				) \n" +
                            "				inner join QUESTIONS_MAPPING \n" +
                            "				on TEMPLATE_QUESTIONS.QUESTION_GUID=QUESTIONS_MAPPING.QUESTION_MASTER_GUID \n" +
                            "		) as MQ \n" +
                            "		inner join TEMPLATE_QUESTIONS \n" +
                            "		on MQ.QUESTION_MASTER_GUID=TEMPLATE_QUESTIONS.QUESTION_GUID \n" +
                            "	) \n" +
                            "	left join TEMPLATES \n" +
                            "	on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID \n" +
                            ") as VQ \n" +
                            "on REPORT_ITEMS.QUESTION_GUID=VQ.QUESTION_GUID) \n" +
                            "inner join REPORTS \n" +
                            "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE) \n" +
                            "inner join VESSELS \n"+
                            "on REPORTS.VESSEL_GUID=VESSELS.VESSEL_GUID \n"+
                            "where LEN(REPORT_ITEMS.OBSERVATION)>0 and not IsNull(REPORT_ITEMS.QUESTION_GUID) \n" +
                            where + " \n" +
                            ") \n" +
                            "group by SECTION_NAME \n" +
                            "order by Min(QUESTION_NUMBER), SECTION_NAME";

                    }

                    
                }
                else
                {
                    if (viqGroupByType)
                    {
                        cmd.CommandText =
                            "SELECT TEMPLATE_QUESTIONS.CHAPTER_NUMBER as [Chapter], "+
                            "TEMPLATE_QUESTIONS.SECTION_NAME as [Subchapter],"+
                            "REPORT_ITEMS.QUESTION_NUMBER as [Question Number], " +
                            "RIGHT(TEMPLATES.TYPE_CODE,2) as [Type], "+
                            "COUNT(REPORT_ITEMS.QUESTION_NUMBER) AS [Observations], \n" +
                            "TEMPLATE_QUESTIONS.SEQUENCE \n"+
                            "FROM \n" +
                            "(((REPORT_ITEMS inner join REPORTS \n" +
                            "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE) \n" +
                            "inner join TEMPLATE_QUESTIONS \n" +
                            "on REPORT_ITEMS.QUESTION_GUID=TEMPLATE_QUESTIONS.QUESTION_GUID) \n" +
                            "inner join TEMPLATES \n" +
                            "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID) \n" +
                            "inner join VESSELS \n"+
                            "on REPORTS.VESSEL_GUID=VESSELS.VESSEL_GUID \n"+
                            "where \n" +
                            "REPORTS.VIQ_TYPE=TEMPLATES.TITLE \n" +
                            "and REPORTS.VIQ_VERSION=TEMPLATES.VERSION \n" +
                            "and Len(OBSERVATION)>0 \n" +
                            where + " \n" +
                            "GROUP BY TEMPLATE_QUESTIONS.CHAPTER_NUMBER, TEMPLATE_QUESTIONS.SECTION_NAME, REPORT_ITEMS.QUESTION_NUMBER, " +
                            "TEMPLATE_QUESTIONS.SEQUENCE," +
                            "RIGHT(TEMPLATES.TYPE_CODE,2) \n" +
                            "order by Len(TEMPLATE_QUESTIONS.CHAPTER_NUMBER), " +
                            "TEMPLATE_QUESTIONS.CHAPTER_NUMBER, TEMPLATE_QUESTIONS.SEQUENCE, " +
                            "RIGHT(TEMPLATES.TYPE_CODE,2)";

                        subchapterSQL=
                            "SELECT TEMPLATE_QUESTIONS.SECTION_NAME as [Subchapter]," +
                            "RIGHT(TEMPLATES.TYPE_CODE,2) as [Type], " +
                            "COUNT(REPORT_ITEMS.QUESTION_NUMBER) AS [Observations] \n" +
                            "FROM \n" +
                            "(((REPORT_ITEMS inner join REPORTS \n" +
                            "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE) \n" +
                            "inner join TEMPLATE_QUESTIONS \n" +
                            "on REPORT_ITEMS.QUESTION_GUID=TEMPLATE_QUESTIONS.QUESTION_GUID) \n" +
                            "inner join TEMPLATES \n" +
                            "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID) \n" +
                            "inner join VESSELS \n"+
                            "on REPORTS.VESSEL_GUID=VESSELS.VESSEL_GUID \n"+
                            "where \n" +
                            "REPORTS.VIQ_TYPE=TEMPLATES.TITLE \n" +
                            "and REPORTS.VIQ_VERSION=TEMPLATES.VERSION \n" +
                            "and Len(OBSERVATION)>0 \n" +
                            where + " \n" +
                            "GROUP BY TEMPLATE_QUESTIONS.SECTION_NAME, RIGHT(TEMPLATES.TYPE_CODE,2) \n" +
                            "order by Min(REPORT_ITEMS.QUESTION_NUMBER), TEMPLATE_QUESTIONS.SECTION_NAME, RIGHT(TEMPLATES.TYPE_CODE,2)";


                    }
                    else
                    {
                        cmd.CommandText =
                            "SELECT TEMPLATE_QUESTIONS.CHAPTER_NUMBER as [Chapter], "+
                            "TEMPLATE_QUESTIONS.SECTION_NAME as [Subchapter], "+
                            "REPORT_ITEMS.QUESTION_NUMBER as [Question Number], " +
                            "COUNT(REPORT_ITEMS.QUESTION_NUMBER) AS [Observations], \n" +
                            "TEMPLATE_QUESTIONS.SEQUENCE \n"+
                            "FROM \n" +
                            "(((REPORT_ITEMS inner join REPORTS \n" +
                            "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE) \n" +
                            "inner join TEMPLATE_QUESTIONS \n" +
                            "on REPORT_ITEMS.QUESTION_GUID=TEMPLATE_QUESTIONS.QUESTION_GUID) \n" +
                            "inner join TEMPLATES \n" +
                            "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID) \n" +
                            "inner join VESSELS \n"+
                            "on REPORTS.VESSEL_GUID=VESSELS.VESSEL_GUID"+
                            "where \n" +
                            "REPORTS.VIQ_TYPE=TEMPLATES.TITLE \n" +
                            "and REPORTS.VIQ_VERSION=TEMPLATES.VERSION \n" +
                            "and Len(OBSERVATION)>0 \n" +
                            where + " \n" +
                            "GROUP BY TEMPLATE_QUESTIONS.CHAPTER_NUMBER, TEMPLATE_QUESTIONS.SECTION_NAME, REPORT_ITEMS.QUESTION_NUMBER, \n" +
                            "TEMPLATE_QUESTIONS.SEQUENCE \n"+
                            "order by Len(TEMPLATE_QUESTIONS.CHAPTER_NUMBER), "+
                            "TEMPLATE_QUESTIONS.CHAPTER_NUMBER, TEMPLATE_QUESTIONS.SEQUENCE";

                        subchapterSQL=
                            "SELECT TEMPLATE_QUESTIONS.SECTION_NAME as [Subchapter], " +
                            "COUNT(REPORT_ITEMS.QUESTION_NUMBER) AS [Observations] \n" +
                            "FROM \n" +
                            "(((REPORT_ITEMS inner join REPORTS \n" +
                            "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE) \n" +
                            "inner join TEMPLATE_QUESTIONS \n" +
                            "on REPORT_ITEMS.QUESTION_GUID=TEMPLATE_QUESTIONS.QUESTION_GUID) \n" +
                            "inner join TEMPLATES \n" +
                            "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID) \n" +
                            "inner join VESSELS \n"+
                            "on REPORTS.VESSEL_GUID=VESSELS.VESSEL_GUID \n"+
                            "where \n" +
                            "REPORTS.VIQ_TYPE=TEMPLATES.TITLE \n" +
                            "and REPORTS.VIQ_VERSION=TEMPLATES.VERSION \n" +
                            "and Len(OBSERVATION)>0 \n" +
                            where + " \n" +
                            "GROUP BY TEMPLATE_QUESTIONS.SECTION_NAME \n" +
                            "order by Min(REPORT_ITEMS.QUESTION_NUMBER), TEMPLATE_QUESTIONS.SECTION_NAME";


                    }
                }

                OleDbDataAdapter statAdapter = new OleDbDataAdapter(cmd);



                if (DS.Tables.Contains("STATISTICS_VIQ"))
                {
                    DS.Tables["STATISTICS_VIQ"].Clear();
                }

                statAdapter.Fill(DS, "STATISTICS_VIQ");

                DataGridView dgv = new DataGridView();

                dgv.Parent = this;

                dgv.DataSource = DS;
                dgv.AutoGenerateColumns = true;
                dgv.AllowUserToAddRows = false;
                dgv.DataMember = "STATISTICS_VIQ";

                cmd.CommandText = subchapterSQL;
                OleDbDataAdapter subchapterAdapter = new OleDbDataAdapter(cmd);

                if (DS.Tables.Contains("STATISTICS_SUBCHAPTER"))
                {
                    DS.Tables["STATISTICS_SUBCHAPTER"].Clear();
                }

                subchapterAdapter.Fill(DS, "STATISTICS_SUBCHAPTER");

                DataGridView dgvs = new DataGridView();
                dgvs.Parent = this;
                dgvs.DataSource = DS;
                dgvs.AutoGenerateColumns = true;
                dgvs.AllowUserToAddRows = false;
                dgvs.DataMember = "STATISTICS_SUBCHAPTER";

                ExportToExcel2Grid(dgv, false, 1, 1, dgvs, false, 1, 6);

                DS.Tables["STATISTICS_VIQ"].Dispose();
                DS.Tables["STATISTICS_VIQ"].Columns.Clear();

                DS.Tables["STATISTICS_SUBCHAPTER"].Dispose();
                DS.Tables["STATISTICS_SUBCHAPTER"].Columns.Clear();

                this.Cursor = Cursors.Default;
            }

        }

        private void roVIQToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateRoVIQStatistics();
        }

        public void updateRoVIQStatistics()
        {
            StatisticRoFilterForm sff = new StatisticRoFilterForm(connection, DS, this.Font, this.Icon);

            fillRoChapters();

            sff.valueChapterName.DataSource = DS.Tables["ROVIQ_CHAPTERS"];
            sff.valueChapterName.DisplayMember = "FULL_NAME";
            sff.valueChapterName.ValueMember = "CH_NUMBER";

            sff.valueDate.Value = statRDate.value;
            sff.valueDate2.Value = statRDate.value2;
            sff.conditionDate.Text = statRDate.condition;

            sff.conditionChapterName.Text = statRChapter.condition;
            sff.valueChapterName.Text = statRChapter.value;

            sff.conditionQType.Text = statRType.condition;
            sff.valueQType.Text = statRType.value;

            sff.useMapping.Checked = roviqStatisticUseMapping;
            sff.groupByType.Checked = roviqGroupByType;

            var rslt = sff.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                statRDate.condition = sff.conditionDate.Text;
                statRDate.value = sff.valueDate.Value;
                statRDate.value2 = sff.valueDate2.Value;

                statRChapter.condition = sff.conditionChapterName.Text;
                statRChapter.value = sff.valueChapterName.Text;

                statRType.condition = sff.conditionQType.Text;
                statRType.value = sff.valueQType.Text;

                roviqStatisticUseMapping = sff.useMapping.Checked;
                roviqGroupByType = sff.groupByType.Checked;

                string where = "";

                string altText = "";
                string mainText = sff.valueChapterName.Text.Substring(3).Trim(); 

                if (mainText.StartsWith("Approaching and  boarding the vessel", StringComparison.OrdinalIgnoreCase))
                    altText = "Approaching the vessel and during boarding";

                if (mainText.StartsWith("Approaching the vessel and during boarding", StringComparison.OrdinalIgnoreCase))
                    altText = "Approaching and  boarding the vessel";

                if (isValidCondition(statRChapter.condition,statRChapter.value))
                {
                    if (where.Length == 0)
                        where = setTextFilter("RTEMPLATE_QUESTIONS.CHAPTER_NAME", statRChapter.condition, mainText, altText, "or");
                    else
                        where = where + " and " + setTextFilter("RTEMPLATE_QUESTIONS.CHAPTER_NAME", statRChapter.condition, mainText, altText, "or");
                }

                if (isValidCondition(statRType.condition,statRType.value))
                {
                    if (where.Length == 0)
                        where = setTextFilter("RIGHT(RTEMPLATES.TYPE_CODE,2)", statRType.condition, statRType.value);
                    else
                        where = where + " and " + setTextFilter("RIGHT(RTEMPLATES.TYPE_CODE,2)", statRType.condition, statRType.value);
                }

                if (statRDate.condition.Length > 0)
                {
                    if (where.Length == 0)
                        where = setDateFilter("REPORTS.INSPECTION_DATE", statRDate.condition, statRDate.value, statRDate.value2);
                    else
                        where = where + " and " + setDateFilter("REPORTS.INSPECTION_DATE", statRDate.condition, statRDate.value, statRDate.value2);
                }


                if (where.Length > 0)
                    where = "and " + where;

                OleDbCommand cmd = new OleDbCommand("", connection);

                if (roviqStatisticUseMapping)
                {
                    if (roviqGroupByType)
                    {
                        cmd.CommandText =
                            "select \n"+
                            "	RTEMPLATE_QUESTIONS.CHAPTER_NAME as [Chapter],  \n"+
                            "	RTEMPLATE_QUESTIONS.QUESTION_NUMBER as [Question No],  \n"+
                            "	RIGHT(RTEMPLATES.TYPE_CODE,2) as [Type],  \n"+
                            "	COUNT(Q1.VALID_GUID) AS [Observations],  \n"+
                            "	RTEMPLATE_QUESTIONS.SEQUENCE \n"+
                            "from \n"+
                            "	(((REPORT_ITEMS left join \n" +
                            "	(select DISTINCT  \n"+
                            "		TEMPLATE_QUESTIONS.QUESTION_GUID,  \n"+
                            "		QUESTIONS_MAPPING.QUESTION_MASTER_GUID, \n"+
                            "		IIF(IsNull(QUESTIONS_MAPPING.QUESTION_MASTER_GUID), TEMPLATE_QUESTIONS.QUESTION_GUID, \n"+
                            "			QUESTIONS_MAPPING.QUESTION_MASTER_GUID) as VALID_GUID \n"+
                            "	from \n"+
                            "		TEMPLATE_QUESTIONS left join QUESTIONS_MAPPING \n"+
                            "		on TEMPLATE_QUESTIONS.QUESTION_GUID=QUESTIONS_MAPPING.QUESTION_SLAVE_GUID) as Q1 \n"+
                            "	on REPORT_ITEMS.QUESTION_GUID=Q1.QUESTION_GUID) \n" +
                            "	left join RTEMPLATE_QUESTIONS \n"+
                            "	on Q1.VALID_GUID=RTEMPLATE_QUESTIONS.QUESTION_GUID) \n"+
                            "	left join RTEMPLATES \n"+
                            "	on RTEMPLATE_QUESTIONS.TEMPLATE_GUID=RTEMPLATES.TEMPLATE_GUID) \n"+
                            "   left join REPORTS \n"+
                            "   on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE \n" +
                            "where  \n"+
                            "	not IsNull(REPORT_ITEMS.QUESTION_GUID) \n" +
                            "	and LEN(REPORT_ITEMS.OBSERVATION)>0 \n" +
                            "	and not IsNull(RTEMPLATE_QUESTIONS.CHAPTER_NAME) \n"+
                            where+" \n"+
                            "group by  \n"+
                            "	RTEMPLATE_QUESTIONS.CHAPTER_NAME,  \n"+
                            "	RTEMPLATE_QUESTIONS.QUESTION_NUMBER,  \n"+
                            "	RTEMPLATE_QUESTIONS.SEQUENCE, \n"+
                            "	RIGHT(RTEMPLATES.TYPE_CODE,2) \n"+
                            "order by  \n"+
                            "	RTEMPLATE_QUESTIONS.SEQUENCE, \n"+ 
                            "	RIGHT(RTEMPLATES.TYPE_CODE,2)";
                    }
                    else
                    {
                        cmd.CommandText =
                            "select \n" +
                            "	RTEMPLATE_QUESTIONS.CHAPTER_NAME as [Chapter],  \n" +
                            "   RTEMPLATE_QUESTIONS.QUESTION_NUMBER as [Question No],  \n" +
                            "	COUNT(Q1.VALID_GUID) AS [Observations],  \n" +
                            "	RTEMPLATE_QUESTIONS.SEQUENCE \n" +
                            "from \n" +
                            "   (((REPORT_ITEMS left join \n" +
                            "   (select DISTINCT  \n" +
                            "   TEMPLATE_QUESTIONS.QUESTION_GUID,  \n" +
                            "   QUESTIONS_MAPPING.QUESTION_MASTER_GUID, \n" +
                            "   IIF(IsNull(QUESTIONS_MAPPING.QUESTION_MASTER_GUID),TEMPLATE_QUESTIONS.QUESTION_GUID,QUESTIONS_MAPPING.QUESTION_MASTER_GUID) as VALID_GUID \n" +
                            "   from \n" +
                            "   TEMPLATE_QUESTIONS left join QUESTIONS_MAPPING \n" +
                            "   on TEMPLATE_QUESTIONS.QUESTION_GUID=QUESTIONS_MAPPING.QUESTION_SLAVE_GUID) as Q1 \n" +
                            "   on REPORT_ITEMS.QUESTION_GUID=Q1.QUESTION_GUID) \n" +
                            "   left join RTEMPLATE_QUESTIONS \n" +
                            "   on Q1.VALID_GUID=RTEMPLATE_QUESTIONS.QUESTION_GUID) \n" +
                            "   left join RTEMPLATES \n"+
                            "   on RTEMPLATES.TEMPLATE_GUID=RTEMPLATE_QUESTIONS.TEMPLATE_GUID) \n"+
                            "   left join REPORTS \n" +
                            "   on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE \n" +
                            "where not IsNull(REPORT_ITEMS.QUESTION_GUID) \n" +
                            "   and LEN(REPORT_ITEMS.OBSERVATION)>0 \n" +
                            "   and not IsNull(RTEMPLATE_QUESTIONS.CHAPTER_NAME) \n" +
                            where + " \n" +
                            "group by \n"+
                            "   RTEMPLATE_QUESTIONS.CHAPTER_NAME, \n"+
                            "   RTEMPLATE_QUESTIONS.QUESTION_NUMBER, \n"+
                            "   RTEMPLATE_QUESTIONS.SEQUENCE \n" +
                            "order by \n"+
                            "   RTEMPLATE_QUESTIONS.SEQUENCE \n";
                            
                    }
                }
                else
                {
                    if (roviqGroupByType)
                    {
                        cmd.CommandText =
                            "SELECT RTEMPLATE_QUESTIONS.CHAPTER_NAME as [Chapter], "+
                            "REPORT_ITEMS.QUESTION_NUMBER as [Question Number], " +
                            "RIGHT(RTEMPLATES.TYPE_CODE,2) as [Type], "+
                            "COUNT(REPORT_ITEMS.QUESTION_NUMBER) AS [Observations], \n" +
                            "RTEMPLATE_QUESTIONS.SEQUENCE \n"+
                            "FROM \n" +
                            "((REPORT_ITEMS inner join REPORTS \n" +
                            "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE) \n" +
                            "inner join RTEMPLATE_QUESTIONS \n" +
                            "on REPORT_ITEMS.QUESTION_GUID=RTEMPLATE_QUESTIONS.QUESTION_GUID) \n" +
                            "inner join RTEMPLATES \n" +
                            "on RTEMPLATE_QUESTIONS.TEMPLATE_GUID=RTEMPLATES.TEMPLATE_GUID \n" +
                            "where \n" +
                            "REPORTS.VIQ_TYPE=RTEMPLATES.TITLE \n" +
                            "and REPORTS.VIQ_VERSION=RTEMPLATES.VERSION \n" +
                            "and Len(OBSERVATION)>0 \n" +
                            where + " \n" +
                            "GROUP BY RTEMPLATE_QUESTIONS.CHAPTER_NAME, REPORT_ITEMS.QUESTION_NUMBER, RTEMPLATE_QUESTIONS.SEQUENCE," +
                            "RIGHT(RTEMPLATES.TYPE_CODE,2) \n" +
                            "order " +
                            "RTEMPLATE_QUESTIONS.SEQUENCE, " +
                            "RIGHT(RTEMPLATES.TYPE_CODE,2)";
                    }
                    else
                    {
                        cmd.CommandText =
                            "SELECT RTEMPLATE_QUESTIONS.CHAPTER_NAME as [Chapter], "+
                            "REPORT_ITEMS.QUESTION_NUMBER as [Question Number], " +
                            "COUNT(REPORT_ITEMS.QUESTION_NUMBER) AS [Observations], \n" +
                            "RTEMPLATE_QUESTIONS.SEQUENCE \n"+
                            "FROM \n" +
                            "((REPORT_ITEMS inner join REPORTS \n" +
                            "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE) \n" +
                            "inner join RTEMPLATE_QUESTIONS \n" +
                            "on REPORT_ITEMS.QUESTION_GUID=RTEMPLATE_QUESTIONS.QUESTION_GUID) \n" +
                            "inner join RTEMPLATES \n" +
                            "on RTEMPLATE_QUESTIONS.TEMPLATE_GUID=RTEMPLATES.TEMPLATE_GUID \n" +
                            "where \n" +
                            "REPORTS.VIQ_TYPE=RTEMPLATES.TITLE \n" +
                            "and REPORTS.VIQ_VERSION=RTEMPLATES.VERSION \n" +
                            "and Len(OBSERVATION)>0 \n" +
                            where + " \n" +
                            "GROUP BY RTEMPLATE_QUESTIONS.CHAPTER_NAME, REPORT_ITEMS.QUESTION_NUMBER, \n" +
                            "RTEMPLATE_QUESTIONS.SEQUENCE \n"+
                            "order "+
                            "RTEMPLATE_QUESTIONS.SEQUENCE";
                    }
                }

                OleDbDataAdapter statAdapter = new OleDbDataAdapter(cmd);

                if (DS.Tables.Contains("STATISTICS_ROVIQ"))
                {
                    DS.Tables["STATISTICS_ROVIQ"].Clear();
                }

                statAdapter.Fill(DS, "STATISTICS_ROVIQ");

                DS.Tables["STATISTICS_ROVIQ"].Columns.Remove("SEQUENCE");

                DataGridView dgv = new DataGridView();

                dgv.Parent = this;

                dgv.DataSource = DS;
                dgv.AutoGenerateColumns = true;
                dgv.AllowUserToAddRows = false;
                dgv.DataMember = "STATISTICS_ROVIQ";
                

                ExportToExcel2(dgv, false,1,1);

                DS.Tables["STATISTICS_ROVIQ"].Dispose();
                DS.Tables["STATISTICS_ROVIQ"].Columns.Clear();
            }
        }

        private void fillRoChapters()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);


            cmd.CommandText =
                "select DISTINCT CInt(CHAPTER_NUMBER) as CH_NUMBER, CHAPTER_NAME, CHAPTER_NUMBER+'. '+CHAPTER_NAME as FULL_NAME \n" +
                "from  RTEMPLATE_QUESTIONS \n" +
                "where TEMPLATE_GUID=" + getLastRoTemplateGUID();

            OleDbDataAdapter chaptersAdapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("ROVIQ_CHAPTERS"))
            {
                DS.Tables["ROVIQ_CHAPTERS"].Clear();
                DS.Tables["ROVIQ_CHAPTERS"].Columns.Clear();
            }

            chaptersAdapter.Fill(DS, "ROVIQ_CHAPTERS");
        }

        private string getLastRoTemplateGUID()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select CStr(TEMPLATE_GUID) \n" +
                "from \n" +
                "RTEMPLATES inner join \n" +
                "(select MAX(VERSION) as MaxVersion \n" +
                "from RTEMPLATES \n" +
                "where RIGHT(TYPE_CODE,2)='01') as MaxOil \n" +
                "on RTEMPLATES.VERSION=MaxOil.MaxVersion \n" +
                "where RIGHT(TYPE_CODE,2)='01'";

            return (string)cmd.ExecuteScalar();
        }

        private bool isValidCondition(string condition, string value)
        {
            if (condition.Length > 0)
            {
                if ((condition.StartsWith("Is Empty", StringComparison.OrdinalIgnoreCase)) ||
                    (condition.StartsWith("Is Not Empty", StringComparison.OrdinalIgnoreCase)))
                {
                    return true;
                }
                else
                {
                    if (value.Length > 0)
                        return true;
                    else
                        return false;

                }
            }
            else
                return false;

        }

        private void toolStripButton6_Click_1(object sender, EventArgs e)
        {
            //Go through the list of reports and set FileAvailable field

            CheckReportsForm crForm = new CheckReportsForm(connection, DS, this.Icon, this.Font, workFolder);

            crForm.ShowDialog();


            int curRow = 0;

            if (dgvInspections.Rows.Count > 0)
            {
                curRow=dgvInspections.CurrentRow.Index;
            }

            DS.Tables["REPORTS"].Clear();

            reportsAdapter.Fill(DS, "REPORTS");

            try
            {
                dgvInspections.CurrentCell = dgvInspections[0, curRow];
            }
            catch
            {

            }

            /*
            OleDbCommand cmd = new OleDbCommand("", connection);
            this.Cursor = Cursors.WaitCursor;

            try
            {

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    string localReportCode = dataGridView1.Rows[i].Cells["REPORT_CODE"].Value.ToString();
                    string fileName = workFolder + "\\Reports\\" + localReportCode + ".pdf";

                    if (File.Exists(fileName))
                    {
                        cmd.CommandText =
                            "update REPORTS set FILE_AVAILABLE=TRUE \n" +
                            "where REPORT_CODE='" + localReportCode + "'";
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd.CommandText =
                            "update REPORTS set FILE_AVAILABLE=FALSE \n" +
                            "where REPORT_CODE='" + localReportCode + "'";
                        cmd.ExecuteNonQuery();
                    }
                }

                DS.Tables["REPORTS"].Clear();

                reportsAdapter.Fill(DS, "REPORTS");

            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            */
        }

        private void toolStripComboBox1_TextChanged_1(object sender, EventArgs e)
        {
            updateReports();
        }

        private void exportRecordsToWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create Word document with seleted records

            Word.Document wordDocument;

            //Create Word document
            Word.Application wordapp = new Word.Application();

            //Show Word window
            wordapp.Visible = false;

            this.Cursor = Cursors.WaitCursor;

            try
            {
                Object template = Type.Missing;
                Object newTemplate = false;
                Object documentType = Word.WdNewDocumentType.wdNewBlankDocument;
                Object visible = true;

                //Создаем документ
                wordDocument = wordapp.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible);

                wordDocument.PageSetup.LeftMargin = 42;
                wordDocument.PageSetup.RightMargin = 42;
                wordDocument.PageSetup.TopMargin = 42;
                wordDocument.PageSetup.BottomMargin = 42;

                Word.Paragraphs wordParagraphs;
                Word.Paragraph wordParagraph;

                wordParagraphs = wordDocument.Paragraphs;

                for (int i=0; i<dgvDetails.Rows.Count; i++)
                {
                    if (i > 0)
                        wordParagraphs.Add();

                    wordParagraph = wordParagraphs[wordParagraphs.Count];
                    wordParagraph.Range.Font.Bold = 1;
                    wordParagraph.Range.Font.Name = "Calibri";
                    wordParagraph.Range.Font.Size = 11;
                    wordParagraph.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    wordParagraph.Range.ParagraphFormat.SpaceAfter = 0;
                    wordParagraph.Range.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle;

                    wordParagraph.Range.Text = dgvDetails.Rows[i].Cells["QUESTION_NUMBER"].Value.ToString() +
                        "\t" + dgvDetails.Rows[i].Cells["OBSERVATION"].Value.ToString();

                    wordParagraphs.Add();
                    wordParagraph = wordParagraphs[wordParagraphs.Count];
                    wordParagraph.Range.Font.Bold = 0;
                    wordParagraph.Range.Font.Name = "Calibri Light";
                    wordParagraph.Range.Font.Size = 11;
                    wordParagraph.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    wordParagraph.Range.ParagraphFormat.SpaceAfter = 0;
                    wordParagraph.Range.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle;

                    wordParagraph.Range.Text = dgvDetails.Rows[i].Cells["OPERATOR_COMMENTS"].Value.ToString();
                    wordParagraph = wordParagraphs.Add();
                }

                ((Word._Application)wordapp).Visible = true;
                

            }
            finally
            {
                wordapp = null;
                this.Cursor = Cursors.Default;
            }

        }

        public void createFilterItems()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            OleDbTransaction transaction;

            transaction = connection.BeginTransaction();

            cmd.Transaction = transaction;

            try
            {
                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('VESSEL',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('INSPECTOR',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('REPORT NUMBER',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('QUESTION NUMBER',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('OBSERVATION',-1,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('INSPECTOR COMMENTS',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('TECHNICAL COMMENTS',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('OPERATOR COMMENTS',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('QUESTION GUID',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('DATE OF INSPECTION',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('COMPANY',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('PORT',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('OFFICE',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('DOC',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('HULL CLASS',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('MASTER',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('CHIEF ENGINEER',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('SUBCHAPTER',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('KEY WORDS',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('USE MAPPING',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('UPDATE ON CLOSE',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('USE DEFAULT',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();

                cmd.CommandText=
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('TEMPLATE TYPE',0,'','" + StrToSQLStr(user.Name) + "')";

                transaction.Commit();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                transaction.Rollback();
            }
        }

        private void checkFilterItems()
        {
            //check for ll filter items

            OleDbCommand cmd = new OleDbCommand("", connection);
            int recs = 0;

            cmd.CommandText =
                "select Count(ID) as recs \n" +
                "from FILTER_ITEMS \n" +
                "where USER_NAME='" + StrToSQLStr(user.Name) + "' \n"+
                "and ITEM_NAME='VESSEL'";

            recs = (int)cmd.ExecuteScalar();

            if (recs==0)
            {
                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('VESSEL',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText =
                "select Count(ID) as recs \n" +
                "from FILTER_ITEMS \n" +
                "where USER_NAME='" + StrToSQLStr(user.Name) + "' \n" +
                "and ITEM_NAME='INSPECTOR'";

            recs = (int)cmd.ExecuteScalar();

            if (recs == 0)
            {
                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('INSPECTOR',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText =
                "select Count(ID) as recs \n" +
                "from FILTER_ITEMS \n" +
                "where USER_NAME='" + StrToSQLStr(user.Name) + "' \n" +
                "and ITEM_NAME='REPORT NUMBER'";

            recs = (int)cmd.ExecuteScalar();

            if (recs == 0)
            {
                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('REPORT NUMBER',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText =
                "select Count(ID) as recs \n" +
                "from FILTER_ITEMS \n" +
                "where USER_NAME='" + StrToSQLStr(user.Name) + "' \n" +
                "and ITEM_NAME='QUESTION NUMBER'";

            recs = (int)cmd.ExecuteScalar();

            if (recs == 0)
            {
                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('QUESTION NUMBER',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText =
                "select Count(ID) as recs \n" +
                "from FILTER_ITEMS \n" +
                "where USER_NAME='" + StrToSQLStr(user.Name) + "' \n" +
                "and ITEM_NAME='OBSERVATION'";

            recs = (int)cmd.ExecuteScalar();

            if (recs == 0)
            {
                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('OBSERVATION',-1,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText =
                "select Count(ID) as recs \n" +
                "from FILTER_ITEMS \n" +
                "where USER_NAME='" + StrToSQLStr(user.Name) + "' \n" +
                "and ITEM_NAME='INSPECTOR COMMENTS'";

            recs = (int)cmd.ExecuteScalar();

            if (recs == 0)
            {
                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('INSPECTOR COMMENTS',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText =
                "select Count(ID) as recs \n" +
                "from FILTER_ITEMS \n" +
                "where USER_NAME='" + StrToSQLStr(user.Name) + "' \n" +
                "and ITEM_NAME='TECHNICAL COMMENTS'";

            recs = (int)cmd.ExecuteScalar();

            if (recs == 0)
            {
                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('TECHNICAL COMMENTS',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText =
                "select Count(ID) as recs \n" +
                "from FILTER_ITEMS \n" +
                "where USER_NAME='" + StrToSQLStr(user.Name) + "' \n" +
                "and ITEM_NAME='OPERATOR COMMENTS'";

            recs = (int)cmd.ExecuteScalar();

            if (recs == 0)
            {
                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('OPERATOR COMMENTS',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText =
                "select Count(ID) as recs \n" +
                "from FILTER_ITEMS \n" +
                "where USER_NAME='" + StrToSQLStr(user.Name) + "' \n" +
                "and ITEM_NAME='QUESTION GUID'";

            recs = (int)cmd.ExecuteScalar();

            if (recs == 0)
            {
                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('QUESTION GUID',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText =
                "select Count(ID) as recs \n" +
                "from FILTER_ITEMS \n" +
                "where USER_NAME='" + StrToSQLStr(user.Name) + "' \n" +
                "and ITEM_NAME='DATE OF INSPECTION'";

            recs = (int)cmd.ExecuteScalar();

            if (recs == 0)
            {
                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('DATE OF INSPECTION',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText =
                "select Count(ID) as recs \n" +
                "from FILTER_ITEMS \n" +
                "where USER_NAME='" + StrToSQLStr(user.Name) + "' \n" +
                "and ITEM_NAME='COMPANY'";

            recs = (int)cmd.ExecuteScalar();

            if (recs == 0)
            {
                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('COMPANY',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText =
                "select Count(ID) as recs \n" +
                "from FILTER_ITEMS \n" +
                "where USER_NAME='" + StrToSQLStr(user.Name) + "' \n" +
                "and ITEM_NAME='PORT'";

            recs = (int)cmd.ExecuteScalar();

            if (recs == 0)
            {
                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('PORT',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText =
                "select Count(ID) as recs \n" +
                "from FILTER_ITEMS \n" +
                "where USER_NAME='" + StrToSQLStr(user.Name) + "' \n" +
                "and ITEM_NAME='OFFICE'";

            recs = (int)cmd.ExecuteScalar();

            if (recs == 0)
            {
                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('OFFICE',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText =
                "select Count(ID) as recs \n" +
                "from FILTER_ITEMS \n" +
                "where USER_NAME='" + StrToSQLStr(user.Name) + "' \n" +
                "and ITEM_NAME='HULL CLASS'";

            recs = (int)cmd.ExecuteScalar();

            if (recs == 0)
            {
                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('HULL CLASS',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText =
                "select Count(ID) as recs \n" +
                "from FILTER_ITEMS \n" +
                "where USER_NAME='" + StrToSQLStr(user.Name) + "' \n" +
                "and ITEM_NAME='MASTER'";

            recs = (int)cmd.ExecuteScalar();

            if (recs == 0)
            {
                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('MASTER',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText =
                "select Count(ID) as recs \n" +
                "from FILTER_ITEMS \n" +
                "where USER_NAME='" + StrToSQLStr(user.Name) + "' \n" +
                "and ITEM_NAME='CHIEF ENGINEER'";

            recs = (int)cmd.ExecuteScalar();

            if (recs == 0)
            {
                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('CHIEF ENGINEER',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText =
                "select Count(ID) as recs \n" +
                "from FILTER_ITEMS \n" +
                "where USER_NAME='" + StrToSQLStr(user.Name) + "' \n" +
                "and ITEM_NAME='SUBCHAPTER'";

            recs = (int)cmd.ExecuteScalar();

            if (recs == 0)
            {
                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('SUBCHAPTER',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText =
                "select Count(ID) as recs \n" +
                "from FILTER_ITEMS \n" +
                "where USER_NAME='" + StrToSQLStr(user.Name) + "' \n" +
                "and ITEM_NAME='KEY WORDS'";

            recs = (int)cmd.ExecuteScalar();

            if (recs == 0)
            {
                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('KEY WORDS',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText =
                "select Count(ID) as recs \n" +
                "from FILTER_ITEMS \n" +
                "where USER_NAME='" + StrToSQLStr(user.Name) + "' \n" +
                "and ITEM_NAME='TEMPLATE TYPE'";

            recs = (int)cmd.ExecuteScalar();

            if (recs == 0)
            {
                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('TEMPLATE TYPE',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();
            }


            cmd.CommandText =
                "select Count(ID) as recs \n" +
                "from FILTER_ITEMS \n" +
                "where USER_NAME='" + StrToSQLStr(user.Name) + "' \n" +
                "and ITEM_NAME='USE MAPPING'";

            recs = (int)cmd.ExecuteScalar();

            if (recs == 0)
            {
                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('USE MAPPING',-1,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText =
                "select Count(ID) as recs \n" +
                "from FILTER_ITEMS \n" +
                "where USER_NAME='" + StrToSQLStr(user.Name) + "' \n" +
                "and ITEM_NAME='UPDATE ON CLOSE'";

            recs = (int)cmd.ExecuteScalar();

            if (recs == 0)
            {
                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('UPDATE ON CLOSE',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText =
                "select Count(ID) as recs \n" +
                "from FILTER_ITEMS \n" +
                "where USER_NAME='" + StrToSQLStr(user.Name) + "' \n" +
                "and ITEM_NAME='USE DEFAULT'";

            recs = (int)cmd.ExecuteScalar();

            if (recs == 0)
            {
                cmd.CommandText =
                    "insert into FILTER_ITEMS (ITEM_NAME,ITEM_SHOW,ITEM_CONDITION,USER_NAME) \n" +
                    "values('USE DEFAULT',0,'','" + StrToSQLStr(user.Name) + "')";
                cmd.ExecuteNonQuery();
            }

        }

        public static void loadFilterDefaultsIni()
        {


            IniFile iniFile = new IniFile(iniPersonalFile);

            string section = "Details filter";
            string iniKey = "Vessel";

            defVessel.Show = iniFile.ReadBoolean(section, iniKey+".Show", false);
            defVessel.Condition = iniFile.ReadString(section, iniKey+".Condition", "");
            defVessel.Value = iniFile.ReadString(section, iniKey+".Value", "");

            iniKey = "Inspector";
            defInspector.Show = iniFile.ReadBoolean(section, iniKey+".Show", false);
            defInspector.Condition = iniFile.ReadString(section, iniKey+".Condition", "");
            defInspector.Value = iniFile.ReadString(section, iniKey + ".Value", "");

            iniKey = "ReportNumber";
            defReportNumber.Show = iniFile.ReadBoolean(section, iniKey + ".Show", false);
            defReportNumber.Condition = iniFile.ReadString(section, iniKey + ".Condition", "");
            defReportNumber.Value = iniFile.ReadString(section, iniKey + ".Value", "");

            iniKey = "QuestionNumber";
            defQuestionNumber.Show = iniFile.ReadBoolean(section, iniKey + ".Show", false);
            defQuestionNumber.Condition = iniFile.ReadString(section, iniKey + ".Condition", "");
            defQuestionNumber.Value = iniFile.ReadString(section, iniKey + ".Value", "");

            iniKey = "Observation";
            defObservation.Show = iniFile.ReadBoolean(section, iniKey + ".Show", false);
            defObservation.Condition = iniFile.ReadString(section, iniKey + ".Condition", "");
            defObservation.Value = iniFile.ReadString(section, iniKey + ".Value", "");

            iniKey = "InspectorComments";
            defInspectorComments.Show = iniFile.ReadBoolean(section, iniKey + ".Show", false);
            defInspectorComments.Condition = iniFile.ReadString(section, iniKey + ".Condition", "");
            defInspectorComments.Value = iniFile.ReadString(section, iniKey + ".Value", "");

            iniKey = "TechnicalComments";
            defTechnicalComments.Show = iniFile.ReadBoolean(section, iniKey + ".Show", false);
            defTechnicalComments.Condition = iniFile.ReadString(section, iniKey + ".Condition", "");
            defTechnicalComments.Value = iniFile.ReadString(section, iniKey + ".Value", "");

            iniKey = "OperatorComments";
            defOperatorComments.Show = iniFile.ReadBoolean(section, iniKey + ".Show", false);
            defOperatorComments.Condition = iniFile.ReadString(section, iniKey + ".Condition", "");
            defOperatorComments.Value = iniFile.ReadString(section, iniKey + ".Value", "");

            iniKey = "QuestionGUID";
            defQuestionGUID.Show = iniFile.ReadBoolean(section, iniKey + ".Show", false);
            defQuestionGUID.Condition = iniFile.ReadString(section, iniKey + ".Condition", "");
            defQuestionGUID.Value = iniFile.ReadString(section, iniKey + ".Value", "");

            iniKey = "DateOfInspection";
            defDateOfInspection.Show = iniFile.ReadBoolean(section, iniKey + ".Show", false);
            defDateOfInspection.Condition = iniFile.ReadString(section, iniKey + ".Condition", "");
            
            string dateStr = "";
            dateStr = iniFile.ReadString(section, iniKey + ".Value", "");
            
            if (dateStr.Length == 0)
                defDateOfInspection.Value = DateTime.Today;
            else
            {
                try
                {
                    defDateOfInspection.Value = Convert.ToDateTime(dateStr);
                }
                catch
                {
                    defDateOfInspection.Value = DateTime.Today;
                }
            }

            dateStr = iniFile.ReadString(section, iniKey + ".Value2", "");

            if (dateStr.Length == 0)
                defDateOfInspection.Value2 = DateTime.Today;
            else
            {
                try
                {
                    defDateOfInspection.Value2 = Convert.ToDateTime(dateStr);
                }
                catch
                {
                    defDateOfInspection.Value2 = DateTime.Today;
                }
            }

            iniKey = "Company";
            defCompany.Show = iniFile.ReadBoolean(section, iniKey + ".Show", false);
            defCompany.Condition = iniFile.ReadString(section, iniKey + ".Condition", "");
            defCompany.Value = iniFile.ReadString(section, iniKey + ".Value", "");

            iniKey = "Port";
            defPort.Show = iniFile.ReadBoolean(section, iniKey + ".Show", false);
            defPort.Condition = iniFile.ReadString(section, iniKey + ".Condition", "");
            defPort.Value = iniFile.ReadString(section, iniKey + ".Value", "");

            iniKey = "Office";
            defOffice.Show = iniFile.ReadBoolean(section, iniKey + ".Show", false);
            defOffice.Condition = iniFile.ReadString(section, iniKey + ".Condition", "");
            defOffice.Value = iniFile.ReadString(section, iniKey + ".Value", "");

            iniKey = "HullClass";
            defHullClass.Show = iniFile.ReadBoolean(section, iniKey + ".Show", false);
            defHullClass.Condition = iniFile.ReadString(section, iniKey + ".Condition", "");
            defHullClass.Value = iniFile.ReadString(section, iniKey + ".Value", "");

            iniKey = "Master";
            defMaster.Show = iniFile.ReadBoolean(section, iniKey + ".Show", false);
            defMaster.Condition = iniFile.ReadString(section, iniKey + ".Condition", "");
            defMaster.Value = iniFile.ReadString(section, iniKey + ".Value", "");

            iniKey = "ChiefEngineer";
            defChiefEngineer.Show = iniFile.ReadBoolean(section, iniKey + ".Show", false);
            defChiefEngineer.Condition = iniFile.ReadString(section, iniKey + ".Condition", "");
            defChiefEngineer.Value = iniFile.ReadString(section, iniKey + ".Value", "");

            iniKey = "Subchapter";
            defSubchapter.Show = iniFile.ReadBoolean(section, iniKey + ".Show", false);
            defSubchapter.Condition = iniFile.ReadString(section, iniKey + ".Condition", "");
            defSubchapter.Value = iniFile.ReadString(section, iniKey + ".Value", "");

            iniKey = "KeyWords";
            defKeyWords.Show = iniFile.ReadBoolean(section, iniKey + ".Show", false);
            defKeyWords.Condition = iniFile.ReadString(section, iniKey + ".Condition", "");
            defKeyWords.Value = iniFile.ReadString(section, iniKey + ".Value", "");

            iniKey = "UseMapping";
            defUseMapping.Show = iniFile.ReadBoolean(section, iniKey, true);

            iniKey = "UpdateOnClose";
            defUpdateOnClose.Show = iniFile.ReadBoolean(section, iniKey, true);

            iniKey = "UseDefault";
            defUseDefault.Show = iniFile.ReadBoolean(section, iniKey, true);

            iniKey = "QuestionnaireType";
            defQuestionnaireType.Show = iniFile.ReadBoolean(section, iniKey + ".Show", false);
            defQuestionnaireType.Condition = iniFile.ReadString(section, iniKey + ".Condition", "");
            defQuestionnaireType.Value = iniFile.ReadString(section, iniKey + ".Value", "");

        }

        public static void loadFilterDefaults()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select * from FILTER_ITEMS \n" +
                "where USER_NAME='" + StrToSQLStr(user.Name) + "'";

            OleDbDataReader defReader = cmd.ExecuteReader();

            if (defReader.HasRows)
            {
                while (defReader.Read())
                {
                    switch (defReader["ITEM_NAME"].ToString())
                    {
                        case "VESSEL":
                            defVessel.Show = Convert.ToBoolean(defReader["ITEM_SHOW"]);
                            defVessel.Condition = defReader["ITEM_CONDITION"].ToString();
                            break;
                        case "INSPECTOR":
                            defInspector.Show = Convert.ToBoolean(defReader["ITEM_SHOW"]);
                            defInspector.Condition = defReader["ITEM_CONDITION"].ToString();
                            break;
                        case "REPORT NUMBER":
                            defReportNumber.Show = Convert.ToBoolean(defReader["ITEM_SHOW"]);
                            defReportNumber.Condition = defReader["ITEM_CONDITION"].ToString();
                            break;
                        case "QUESTION NUMBER":
                            defQuestionNumber.Show = Convert.ToBoolean(defReader["ITEM_SHOW"]);
                            defQuestionNumber.Condition = defReader["ITEM_CONDITION"].ToString();
                            break;
                        case "OBSERVATION":
                            defObservation.Show = Convert.ToBoolean(defReader["ITEM_SHOW"]);
                            defObservation.Condition = defReader["ITEM_CONDITION"].ToString();
                            break;
                        case "INSPECTOR COMMENTS":
                            defInspectorComments.Show = Convert.ToBoolean(defReader["ITEM_SHOW"]);
                            defInspectorComments.Condition = defReader["ITEM_CONDITION"].ToString();
                            break;
                        case "TECHNICAL COMMENTS":
                            defTechnicalComments.Show = Convert.ToBoolean(defReader["ITEM_SHOW"]);
                            defTechnicalComments.Condition = defReader["ITEM_CONDITION"].ToString();
                            break;
                        case "OPERATOR COMMENTS":
                            defOperatorComments.Show = Convert.ToBoolean(defReader["ITEM_SHOW"]);
                            defOperatorComments.Condition = defReader["ITEM_CONDITION"].ToString();
                            break;
                        case "QUESTION GUID":
                            defQuestionGUID.Show = Convert.ToBoolean(defReader["ITEM_SHOW"]);
                            defQuestionGUID.Condition = defReader["ITEM_CONDITION"].ToString();
                            break;
                        case "DATE OF INSPECTION":
                            defDateOfInspection.Show = Convert.ToBoolean(defReader["ITEM_SHOW"]);
                            defDateOfInspection.Condition = defReader["ITEM_CONDITION"].ToString();
                            break;
                        case "COMPANY":
                            defCompany.Show = Convert.ToBoolean(defReader["ITEM_SHOW"]);
                            defCompany.Condition = defReader["ITEM_CONDITION"].ToString();
                            break;
                        case "PORT":
                            defPort.Show = Convert.ToBoolean(defReader["ITEM_SHOW"]);
                            defPort.Condition = defReader["ITEM_CONDITION"].ToString();
                            break;
                        case "OFFICE":
                            defOffice.Show = Convert.ToBoolean(defReader["ITEM_SHOW"]);
                            defOffice.Condition = defReader["ITEM_CONDITION"].ToString();
                            break;
                        case "HULL CLASS":
                            defHullClass.Show = Convert.ToBoolean(defReader["ITEM_SHOW"]);
                            defHullClass.Condition = defReader["ITEM_CONDITION"].ToString();
                            break;
                        case "MASTER":
                            defMaster.Show = Convert.ToBoolean(defReader["ITEM_SHOW"]);
                            defMaster.Condition = defReader["ITEM_CONDITION"].ToString();
                            break;
                        case "CHIEF ENGINEER":
                            defChiefEngineer.Show = Convert.ToBoolean(defReader["ITEM_SHOW"]);
                            defChiefEngineer.Condition = defReader["ITEM_CONDITION"].ToString();
                            break;
                        case "SUBCHAPTER":
                            defSubchapter.Show = Convert.ToBoolean(defReader["ITEM_SHOW"]);
                            defSubchapter.Condition = defReader["ITEM_CONDITION"].ToString();
                            break;
                        case "KEY WORDS":
                            defKeyWords.Show = Convert.ToBoolean(defReader["ITEM_SHOW"]);
                            defKeyWords.Condition = defReader["ITEM_CONDITION"].ToString();
                            break;
                        case "USE MAPPING":
                            defUseMapping.Show = Convert.ToBoolean(defReader["ITEM_SHOW"]);
                            break;
                        case "UPDATE ON CLOSE":
                            defUpdateOnClose.Show = Convert.ToBoolean(defReader["ITEM_SHOW"]);
                            break;
                        case "USE DEFAULT":
                            defUseDefault.Show = Convert.ToBoolean(defReader["ITEM_SHOW"]);
                            break;
                        case "TEMPLATE TYPE":
                            try
                            {
                                defQuestionnaireType.Show = Convert.ToBoolean(defReader["TEMPLATE_TYPE"]);
                                defQuestionnaireType.Condition = defReader["ITEM_CONDITION"].ToString();
                            }
                            catch
                            {
                                defQuestionnaireType.Show = false;
                                defQuestionnaireType.Condition = "";
                            }
                            break;
                    }

                }
                defReader.Close();
            }

        }

        private void setFilterDefaults()
        {
            qfVessel.show=defVessel.Show;
            qfVessel.condition = defVessel.Condition;

            qfInspector.show = defInspector.Show;
            qfInspector.condition = defInspector.Condition;

            qfReportNumber.show = defReportNumber.Show;
            qfReportNumber.condition = defReportNumber.Condition;

            qfQuestionNumber.show = defQuestionNumber.Show;
            qfQuestionNumber.condition = defQuestionNumber.Condition;

            qfObservation.show = defObservation.Show;
            qfObservation.condition = defObservation.Condition;

            qfInspectorComments.show = defInspectorComments.Show;
            qfInspectorComments.condition = defInspectorComments.Condition;

            qfTechnicalComments.show = defTechnicalComments.Show;
            qfTechnicalComments.condition = defTechnicalComments.Condition;

            qfOperatorComments.show = defOperatorComments.Show;
            qfOperatorComments.condition = defOperatorComments.Condition;

            qfQuestionGUID.show = defQuestionGUID.Show;
            qfQuestionGUID.condition = defQuestionGUID.Condition;

            qfDate.show = defDateOfInspection.Show;
            qfDate.condition = defDateOfInspection.Condition;

            qfCompany.show = defCompany.Show;
            qfCompany.condition = defCompany.Condition;

            qfPort.show = defPort.Show;
            qfPort.condition = defPort.Condition;

            qfOffice.show = defOffice.Show;
            qfOffice.condition = defOffice.Condition;

            qfHullClass.show = defHullClass.Show;
            qfHullClass.condition = defHullClass.Condition;

            qfMaster.show = defMaster.Show;
            qfMaster.condition = defMaster.Condition;

            qfChiefEngineer.show = defChiefEngineer.Show;
            qfChiefEngineer.condition = defChiefEngineer.Condition;

            qfSubchapter.show = defSubchapter.Show;
            qfSubchapter.condition = defSubchapter.Condition;

            qfKeyWords.show = defKeyWords.Show;
            qfKeyWords.condition = defKeyWords.Condition;

            qfQuestionnaireType.show = defQuestionnaireType.Show;
            qfQuestionnaireType.condition = defQuestionnaireType.Condition;

            qfFleet.show = defFleet.Show;
            qfFleet.condition = defFleet.Condition;
        }

        private void compactDatabase(string database)
        {
            string tempDB = Path.Combine(Path.GetDirectoryName(database),
                Guid.NewGuid().ToString("N") + ".dat");

            string sourceConnection = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                "Data Source=" + database + ";";
            string destConnection = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                "Data Source=" + tempDB + ";";

            long iniSize = 0;
            long endSize = 0;

            try
            {

                this.Cursor = Cursors.WaitCursor;

                if (File.Exists(database))
                {
                    FileInfo iniFileInfo = new FileInfo(database);

                    iniSize = iniFileInfo.Length;

                    JetEngine engine = new JetEngine();

                    int i = 0;
                    bool ready = false;

                    while ((i < 10) || ready)
                    {
                        try
                        {
                            engine.CompactDatabase(sourceConnection, destConnection);
                            ready = true;
                        }
                        catch
                        {
                            i++;
                            Thread.Sleep(1000);
                        }
                    }

                    if (!ready) return;

                    FileInfo endFileSize = new FileInfo(tempDB);

                    endSize = endFileSize.Length;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("File \"" + database + "\" was not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (File.Exists(tempDB))
                {
                    File.Delete(database);
                    File.Move(tempDB, database);

                    this.Cursor = Cursors.Default;

                    if (iniSize > endSize)
                    {
                        MessageBox.Show("Database was compacted successfully \n" +
                            "Initial size :\t" + iniSize.ToString() + "\n" +
                            "Compact size :\t" + endSize.ToString(),
                            "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Database is already compacted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Database was not compacted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception E)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //while (connection.State != ConnectionState.Closed) ;

            //Thread.Sleep(300);

            //compactDatabase(Path.Combine(workFolder, dbName));

        }

        private string GetSequenceFromNumber(string questionNumber)
        {
            if (questionNumber.Trim().Length == 0)
                return "";

            string qn=questionNumber;

            List<string> numList = new List<string>();

            numList = ParseStringToList(qn, ".");

            string sequence = "";

            for (int i=0; i<numList.Count;i++)
            {
                string aChar = numList[i];
                string atom = "";

                if (IsNumericString(aChar))
                {
                    atom = FormatIntString(aChar, 3);
                }
                else
                {
                    atom = FormatIntString(aChar, 4);
                }

                if (sequence.Length == 0)
                    sequence = atom;
                else
                    sequence = sequence + "." + atom;
            }

            return sequence;

            /*
            string[] s = new string[5];

            int pos = qn.IndexOf(".");
            int i = 0;

            for (i = 0; i < 5; i++)
                s[i] = "";

            i = 0;

            while ((pos > 0) && (i < 5))
            {
                s[i] = qn.Substring(0, pos);

                s[i] = FormatIntString(s[i], 3);

                i++;

                if (qn.Length > pos + 1)
                    qn = qn.Substring(pos + 1, qn.Length - pos - 1);
                else
                    qn = "";

                if (qn.Length > 0)
                {
                    pos = qn.IndexOf(".");
                }
                else
                    pos = 0;
            }

            if (i==5)
            {
                //Превышен размер массива
                MessageBox.Show("Array index out of bounds (>4)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }

            if (qn.Length > 0)
                s[i] = FormatIntString(qn, 3);

            string result = "";

            for (i=0; i<5; i++)
            {
                if (s[i].Length > 0)
                {
                    if (i == 0)
                        result = s[i];
                    else
                        result = result + "." + s[i];
                }
                else
                    return result;
            }

            return result;
            */
        }

        public static bool IsNumericString(string aString)
        {
            if (aString.Length == 0)
                return false;

            string numbers = "1234567890";

            for (int i=0;i<aString.Length; i++)
            {
                if (!numbers.Contains(aString[i].ToString()))
                    return false;
            }

            return true;
        }

        private string FormatIntString(string text, int length)
        {
            if (text.Trim().Length == 0) return text;

            string s = text.Trim();

            while (s.Length<length)
            {
                s = "0" + s;
            }

            return s;
        }

        private void applicationNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Edit application name
            if (isPowerUser)
            {
                ApplicationNameForm anForm = new ApplicationNameForm(this.Icon, this.Font);

                anForm.textBox1.Text = appName;

                var rslt = anForm.ShowDialog();

                if (rslt == DialogResult.OK)
                {
                    appName = anForm.textBox1.Text;

                    setAppTitle();

                    WriteIniValue(iniCommonFile, "General", "AppName", appName);
                }
            }
            else
            {
                MessageBox.Show("You are not allower to change application name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void statisticsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            updateVIQStatistics();
        }


        private void ExportToExcel2Grid(DataGridView dgv1, bool calcTotals1, int startRow1, int startCol1, DataGridView dgv2, bool calcTotals2, int startRow2, int startCol2)
        {
            if (dgv1.Rows.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("There is no any data to export.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var excelApp = createExcel();

            Excel._Worksheet workSheet = getWorksheet(excelApp);

            GridToExcel(dgv1, workSheet, calcTotals1, startRow1, startCol1);
            GridToExcel(dgv2, workSheet, calcTotals2, startRow2, startCol2);

            workSheet.Application.ActiveWindow.FreezePanes = true;

            excelApp.Visible = true;
        }

        private Excel.Application createExcel(string fileName="")
        {
            var excelApp = new Excel.Application();
            
            // Make the object visible.
            excelApp.Visible = false;
            
            excelApp.WindowState = Excel.XlWindowState.xlMaximized;

            if (fileName.Length == 0)
                excelApp.Workbooks.Add();
            else
                excelApp.Workbooks.Open(fileName);

            return excelApp;
        }

        private Excel._Worksheet getWorksheet(Excel.Application excelApp)
        {
            Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;

            return workSheet;
        }

        private void GridToExcel(DataGridView dgv, Excel._Worksheet workSheet, bool calcTotals, int startRow, int startCol)
        {
            int columns = 0;

            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                if (dgv.Columns[i].Visible)
                    columns++;
            }


            int rows = dgv.Rows.Count + 1;

            //Create an array.
            Object[,] varArray = new Object[rows, columns];


            int cIndex = 0;

            for (int i = 0; i < columns; i++)
            {
                while ((cIndex < dgv.ColumnCount) && !dgv.Columns[cIndex].Visible)
                    cIndex++;

                if (cIndex < dgv.ColumnCount)
                {
                    varArray[0, i] = dgv.Columns[cIndex].HeaderText;

                    System.Type type = dgv.Columns[cIndex].ValueType;

                    if (String.Compare(type.Name, "String") == 0)
                        workSheet.Range[workSheet.Cells[startRow, startCol + i], workSheet.Cells[startRow + rows - 1, startCol + i]].NumberFormat = "@";

                    cIndex++;
                }
            }

            for (int i = 0; i < rows - 1; i++)
            {
                cIndex = 0;

                for (int j = 0; j < columns; j++)
                {
                    while ((cIndex < dgv.ColumnCount) && !dgv.Columns[cIndex].Visible)
                        cIndex++;

                    if (cIndex < dgv.ColumnCount)
                    {
                        if (dgv[cIndex, i].Value != null)
                        {
                            varArray[i + 1, j] = dgv[cIndex, i].Value.ToString();
                        }

                        cIndex++;
                    }
                }
            }

            //workSheet.Range[workSheet.Cells[1, 1], workSheet.Cells[rows, 1]].NumberFormat = "@";

            workSheet.Range[workSheet.Cells[startRow, startCol], workSheet.Cells[startRow + rows - 1, startCol + columns - 1]].Value2 = varArray;

            for (int i = 0; i < columns; i++)
            {
                try
                {
                    ((Excel.Range)workSheet.Columns[startCol + i]).AutoFit();
                }
                catch (Exception E)
                {
                    System.Windows.Forms.MessageBox.Show(E.Message);
                }
            }

            workSheet.Range[workSheet.Cells[startRow, startCol], workSheet.Cells[startRow + rows - 1, startCol + columns - 1]].Borders.Weight = Excel.XlBorderWeight.xlThin;
            workSheet.Range[workSheet.Cells[startRow, startCol], workSheet.Cells[startRow + rows - 1, startCol + columns - 1]].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            workSheet.Range[workSheet.Cells[startRow, startCol], workSheet.Cells[startRow, startCol + columns - 1]].EntireRow.Font.Bold = true;

            if (calcTotals)
            {
                workSheet.Range[workSheet.Cells[startRow + rows, startCol], workSheet.Cells[startRow + rows, startCol]].Value2 = "Total";

                for (int i = 2; i <= columns; i++)
                {
                    if (Convert.ToString(workSheet.Range[workSheet.Cells[startRow, startCol + i - 1], workSheet.Cells[startRow + rows - 1, startCol + i - 1]].NumberFormat) != "@")
                    {
                        string A = "A";
                        int codeA = (int)A[0];

                        workSheet.Range[workSheet.Cells[startRow + rows, startCol + i - 1], workSheet.Cells[startCol + rows, startCol + i - 1]].FormulaLocal = "=SUM(" + ((char)(codeA + i - 1)).ToString() + "2:" + ((char)(codeA + i - 1)).ToString() + rows.ToString() + ")";
                    }
                }

                workSheet.Range[workSheet.Cells[startRow + rows, startCol], workSheet.Cells[startRow + rows, startCol + columns - 1]].EntireRow.Font.Bold = true;
            }

            workSheet.Range[workSheet.Cells[startRow + 1, startCol + 1], workSheet.Cells[startRow + 1, startCol + 1]].Select();

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aForm = new AboutForm(appName, this.Icon, this.Font, licenseString, licenseOwner, licenseExpire);

            aForm.ShowDialog();
        }

        private void powerUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PowerUsersForm puf = new PowerUsersForm();

            puf.ShowDialog();

            //isPowerUserNew = IsPowerUser();
            isPowerUser = IsPowerUser();

            if (isPowerUser)  //((isPowerUser==true) || (isPowerUserNew==true))
            {
                toolStripStatusLabel8.Text = "Power";
                menuPowerUsers.Visible = true;
                tbtnUpdateDB.Visible = true;

                if (appType.Length == 0)
                {
                    /*
                    if (appName.StartsWith("OVIQ"))
                    {
                        this.Icon = VIM.Properties.Resources.OVID_16_Power_v4;
                        SetApplicationType("OVIQ");
                    }
                    else
                    {
                        this.Icon = VIM.Properties.Resources.SIRE_16_Power_v4;
                        SetApplicationType("SIRE");
                    }
                    */

                    appType = GetApplicationType();
                }
                else
                {
                    /*
                    switch (appType)
                    {
                        case "OVIQ":
                            this.Icon = VIM.Properties.Resources.OVID_16_Power_v4;
                            break;
                        case "SIRE":
                            this.Icon = VIM.Properties.Resources.SIRE_16_Power_v4;
                            break;
                    }
                    */
                }
            }
            else
            {
                toolStripStatusLabel8.Text = "User";
                menuPowerUsers.Visible = false;
                tbtnUpdateDB.Visible = false;

                if (appType.Length == 0)
                {
                    /*
                    if (appName.StartsWith("OVIQ"))
                    {
                        this.Icon = VIM.Properties.Resources.OVIQ_16x16_blue;
                        SetApplicationType("OVIQ");
                    }
                    else
                    {
                        this.Icon = VIM.Properties.Resources.SIRE_16x16_blue;
                        SetApplicationType("SIRE");
                    }
                    */

                    appType = GetApplicationType();
                }
                else
                {
                    /*
                    switch (appType)
                    {
                        case "OVIQ":
                            this.Icon = VIM.Properties.Resources.OVIQ_16x16_blue;
                            break;
                        case "SIRE":
                            this.Icon = VIM.Properties.Resources.SIRE_16x16_blue;
                            break;
                    }
                    */
                }
            }

            //mainIcon = this.Icon;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            startLoadFileNew();
        }

        public static string GetOptionValue(int Tag)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select STR_VALUE \n" +
                "from OPTIONS \n"+
                "where TAG=" + Tag.ToString();

            return (string)cmd.ExecuteScalar();
        }

        public static void SetOptionValue(int Tag, string Value)
        {
            OleDbCommand cmd = new OleDbCommand("",connection);

            cmd.CommandText=
                "select Count(TAG) as TagCount \n"+
                "from Options \n"+
                "where TAG="+Tag.ToString();

            int count = (int)cmd.ExecuteScalar();

            if (count==0)
            {
                cmd.CommandText =
                    "insert into OPTIONS (TAG,STR_VALUE) \n" +
                    "values(" + Tag.ToString() + ",'" +
                    StrToSQLStr(Value) + "')";
                cmd.ExecuteNonQuery();
            }
            else
            {
                cmd.CommandText =
                    "update OPTIONS set \n" +
                    "STR_VALUE='" + StrToSQLStr(Value) + "' \n" +
                    "where TAG=" + Tag.ToString();
                cmd.ExecuteNonQuery();
            }
        }

        private void powerUsersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!isPowerUser)
            {
                PowerUserCheckForm form = new PowerUserCheckForm();

                var rslt = form.ShowDialog();

                if (rslt == DialogResult.OK)
                {
                    string powerKey = form.powerKey;

                    string key = "";
                    key = "1";
                    key = key + "2" + ".0" + "4.1" + "961";
                    string key2 = "61";
                    key2 = "1" + "2/" + "0" + "4/1" + "9"+key2;

                    if ((key.CompareTo(powerKey) == 0) || (key2.CompareTo(powerKey)==0))
                    {
                        //Add to power user
                        OleDbCommand cmd = new OleDbCommand("", connection);
                        cmd.CommandText =
                            "insert into POWER_USERS (USER_NAME) \n" +
                            "values('" + StrToSQLStr(userName) + "')";

                        if (cmdExecute(cmd)>=0)
                            IsPowerUser();
                        else
                            return;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                    return;
            }

            PowerUsersForm puf = new PowerUsersForm();

            puf.ShowDialog();

            //isPowerUserNew = IsPowerUser();

            bool oldStatus = isPowerUser;

            isPowerUser = IsPowerUser();

            if (isPowerUser)  //((isPowerUser == true) || (isPowerUserNew == true))
            {
                menuPowerUsers.Visible = true;
                tbtnUpdateDB.Visible = true;

                if (appType.Length == 0)
                {
                    /*
                    if (appName.StartsWith("OVIQ"))
                    {
                        this.Icon = VIM.Properties.Resources.OVID_16_Power_v4;
                        SetApplicationType("OVIQ");
                    }
                    else
                    {
                        this.Icon = VIM.Properties.Resources.SIRE_16_Power_v4;
                        SetApplicationType("SIRE");
                    }
                    */
                    appType = GetApplicationType();
                }
                else
                {
                    /*
                    switch (appType)
                    {
                        case "OVIQ":
                            this.Icon = VIM.Properties.Resources.OVID_16_Power_v4;
                            break;
                        case "SIRE":
                            this.Icon = VIM.Properties.Resources.SIRE_16_Power_v4;
                            break;
                    }
                    */
                }
            }
            else
            {
                menuPowerUsers.Visible = false;
                tbtnUpdateDB.Visible = false;

                if (appType.Length == 0)
                {
                    /*
                    if (appName.StartsWith("OVIQ"))
                    {
                        this.Icon = VIM.Properties.Resources.OVIQ_16x16_blue;
                        SetApplicationType("OVIQ");
                    }
                    else
                    {
                        this.Icon = VIM.Properties.Resources.SIRE_16x16_blue;
                        SetApplicationType("SIRE");
                    }
                    */
                    appType=GetApplicationType();
                }
                else
                {
                    /*
                    switch (appType)
                    {
                        case "OVIQ":
                            this.Icon = VIM.Properties.Resources.OVIQ_16x16_blue;
                            break;
                        case "SIRE":
                            this.Icon = VIM.Properties.Resources.SIRE_16x16_blue;
                            break;
                    }
                    */
                }
            }

            mainIcon = this.Icon;

            ProtectFields(isPowerUser);

            if ((oldStatus) && (oldStatus!=isPowerUser))
            {
                //Save database
                if (!SaveDatabase())
                    MessageBox.Show("Failed to save changes in database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        public static bool tempTableCreate(string tableName, string script)
        {
            //Check table exists

            if (tableExists(tableName))
                if (!tempTableDrop(tableName))
                    return false;

            OleDbCommand cmd = new OleDbCommand(script, connection);

            try
            {
                if (cmdExecute(cmd)>=0)
                {
                    tempTableAddLogRecord(tableName);

                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }

        }

        public static bool tempTableDrop(string tableName)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);
            cmd.CommandText =
                "drop table [" + tableName + "]";

            try
            {
                if (cmdExecute(cmd)>=0)
                {
                    tempTableDeleteLogRecord(tableName);
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool tempTableAddLogRecord(string tableName)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            if (!tableExists("TEMP_TABLES"))
            {
                cmd.CommandText =
                    "create table TEMP_TABLES (ID counter Primary key,\n" +
                    "TABLE_NAME varchar(255), CREATED_ON DateTime, PROGRAM_ID varchar(50))";

                if (cmdExecute(cmd)<0)
                    return false;
            }
            
            cmd.CommandText =
                "insert into TEMP_TABLES(TABLE_NAME,CREATED_ON,PROGRAM_ID) \n" +
                "values ('" + tableName + "'," +
                DateTimeToQueryStr(DateTime.Now) + ",'" +
                programID + "')";

            return cmdExecute(cmd)>=0; 
            
        }

        public static bool tempTableDeleteLogRecord(string tableName)
        {
            if (!tableExists("TEMP_TABLES"))
                return true;

            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "delete from TEMP_TABLES \n" +
                "where TABLE_NAME='" + tableName + "' \n" +
                "and PROGRAM_ID='" + programID+"'";

            return cmdExecute(cmd)>=0;
        }

        public static bool tempTablesClear()
        {
            if (!tableExists("TEMP_TABLES"))
                return true;

            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select * from TEMP_TABLES \n" +
                "where PROGRAM_ID='" + programID + "' \n" +
                "or CREATED_ON<" + DateTimeToQueryStr(DateTime.Now.AddDays(-3));

            DataSet tempDS = new DataSet();
            OleDbDataAdapter tempAdapter = new OleDbDataAdapter(cmd);

            try
            {
                tempAdapter.Fill(tempDS, "TEMP_TABLES");

                DataTable tempTables = tempDS.Tables["TEMP_TABLES"];

                foreach (DataRow row in tempTables.Rows)
                {
                    string tableName = row["TABLE_NAME"].ToString();

                    if (tableExists(tableName))
                    {
                        tempTableDrop(tableName);
                    }
                    else
                    {
                        tempTableDeleteLogRecord(tableName);
                    }
                }

                return true;
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
                return false;
            }

            
        }

        public static void LocateGridRecord(string searchString, string searchField, int colIndex, DataGridView dgv)
        {
            //Added on 30.09.2016 in version 2.0.15.1
            //Modified on 10.10.2016 in version 2.0.15.1
            //  Locate first visible column if colIndex<0

            String searchValue = searchString;
            int rowIndex = -1;

            int ind = 0;

            if (colIndex < 0)
            {
                //Locate first visible column
                int colInd = 0;

                for (int i = 0; i < dgv.Columns.Count;i++ )
                {
                    if (dgv.Columns[i].Visible)
                    {
                        colInd = i;
                        break;
                    }
                }

                ind = colInd;
            }
            else
                ind = colIndex;

            if (!dgv.Columns.Contains(searchField)) return;

            foreach (DataGridViewRow dgRow in dgv.Rows)
            {
                if (dgRow.Cells[searchField].Value.ToString().Equals(searchValue))
                {
                    rowIndex = dgRow.Index;
                    break;
                }
            }

            if (rowIndex >= 0)
            {
                try
                {
                    dgv.CurrentCell = dgv[ind, rowIndex];
                }
                catch
                {

                }
            }
        }

        public static void LocateAdvGridRecord(string searchString, string searchField, int colIndex, ADGV.AdvancedDataGridView dgv)
        {
            //Added on 30.09.2016 in version 2.0.15.1
            //Modified on 10.10.2016 in version 2.0.15.1
            //  Locate first visible column if colIndex<0

            String searchValue = searchString;
            int rowIndex = -1;

            int ind = 0;

            if (colIndex < 0)
            {
                //Locate first visible column
                int colInd = 0;

                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    if (dgv.Columns[i].Visible)
                    {
                        colInd = i;
                        break;
                    }
                }

                ind = colInd;
            }
            else
                ind = colIndex;

            if (!dgv.Columns.Contains(searchField)) return;

            foreach (DataGridViewRow dgRow in dgv.Rows)
            {
                if (dgRow.Cells[searchField].Value.ToString().Equals(searchValue))
                {
                    rowIndex = dgRow.Index;
                    break;
                }
            }

            if (rowIndex >= 0)
            {
                try
                {
                    dgv.CurrentCell = dgv[ind, rowIndex];
                }
                catch
                {

                }
            }
        }

        public static string BoolToIntStr(bool value)
        {
            if (value)
                return "1";
            else
                return "0";
        }

        private void monthlyBulletinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create monthly bulletin
            
            IniFile iniFile = new IniFile(iniCommonFile);

            frmMonthlyBulletin form = new frmMonthlyBulletin();

            form.dateIssue = DateTime.Today;

            DateTime startDate = DateTime.Today;
            startDate=startDate.AddDays((startDate.Day - 1) * (-1));
            startDate=startDate.AddMonths(-1);

            DateTime endDate = startDate.AddMonths(1);
            endDate = endDate.AddDays(-1);

            form.dateFrom = startDate;
            form.dateTill = endDate;

            form.selectedMonth = startDate.Month;
            form.selectedYear = startDate.Year;

            //Use global section for bulletin settings
            string section = "MonthlyBulletin";

            form.useMonth = iniFile.ReadBoolean(section, "UseMonth", true);

            form.templateName = iniFile.ReadString(section,"TemplateFile","Vetting Bulletin template.doc");
            form.subject = iniFile.ReadString(section,"Subject","Monthly Observations");
            form.includePeriod = iniFile.ReadBoolean(section, "IncludePeriod", true);

            form.useComments = iniFile.ReadBoolean(section, "UseComments", true);
            form.commentsLabel = iniFile.ReadString(section, "CommentsLabel", "SCF: ");
            form.commentsText = iniFile.ReadString(section, "CommentsText", "");
            form.useCommonComments = iniFile.ReadBoolean(section, "UseCommonComments", false);
            form.useSelectedColor = iniFile.ReadBoolean(section, "UseSelectedColor", false);
            form.selectedColorRGB = iniFile.ReadString(section, "SelectedColor", "RGB(0,0,153)");
            form.selectedStyle = iniFile.ReadString(section, "SelectedStyle", "SCF");
            form.inspectionType = iniFile.ReadInteger(section, "InspectionType", 1);

            iniFile = null;

            var rslt = form.ShowDialog();

            if (rslt==DialogResult.OK)
            {
                DateTime fromDate;
                DateTime tillDate;
                int inspectionType = form.inspectionType;

                if (form.useMonth)
                {
                    int month=form.selectedMonth;
                    int year=form.selectedYear;

                    startDate = startDate.AddYears(year - startDate.Year);
                    startDate = startDate.AddMonths(month - startDate.Month);
                    
                    fromDate = startDate;
                    tillDate = startDate.AddMonths(1);
                }
                else
                {
                    fromDate = form.dateFrom;
                    tillDate = form.dateTill.AddDays(1);
                }

                string subject = form.subject;

                if (form.includePeriod)
                {
                    if (form.useMonth)
                        subject = subject + " for " + startDate.ToString("MMMM, yyyy");
                    else
                        subject = subject + " from " + fromDate.ToShortDateString() + " to " + form.dateTill.ToShortDateString();
                }

                //Create file
                createBulletin(form.templateName, subject, form.dateIssue, fromDate, tillDate, form.useComments,
                    form.commentsLabel, form.commentsText, form.useCommonComments, form.useSelectedColor,
                    form.selectedColor, form.selectedStyle, inspectionType);
            }
        }

        private void createBulletin(string templateFile, string subject, DateTime issueDate, DateTime fromDate, 
            DateTime tillDate, bool useComments, string commentsLabel, string commentsText, bool useCommonComments, 
            bool useSelectedColor, Color selectedColor, string selectedStyle, int inspectionType)
        {
            Word.Document wordDocument;

            //Create Word document
            Word.Application wordapp = new Word.Application();

            //Show Word window
            wordapp.Visible = false;

            this.Cursor = Cursors.WaitCursor;

            try
            {
                Object template = templateFile;
                Object newTemplate = false;
                Object documentType = Word.WdNewDocumentType.wdNewBlankDocument;
                Object visible = true;

                //Создаем документ
                wordDocument = wordapp.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible);

                object o = Missing.Value;
                object oFalse = false;
                object oTrue = true;

                foreach (Word.Range range in wordDocument.StoryRanges)
                {
                    Word.Find find = range.Find;
                    object findText = "%Subject";
                    object replacText = subject;
                    object replace = Word.WdReplace.wdReplaceAll;
                    object findWrap = Word.WdFindWrap.wdFindContinue;

                    find.Execute(ref findText, ref o, ref o, ref o, ref oFalse, ref o,
                        ref o, ref findWrap, ref o, ref replacText,
                        ref replace, ref o, ref o, ref o, ref o);

                    Marshal.FinalReleaseComObject(find);
                    Marshal.FinalReleaseComObject(range);
                }

                string strDate = issueDate.ToLongDateString();

                foreach (Word.Range range in wordDocument.StoryRanges)
                {
                    Word.Find find = range.Find;
                    object findText = "%Date";
                    object replacText = strDate;
                    object replace = Word.WdReplace.wdReplaceAll;
                    object findWrap = Word.WdFindWrap.wdFindContinue;

                    find.Execute(ref findText, ref o, ref o, ref o, ref oFalse, ref o,
                        ref o, ref findWrap, ref o, ref replacText,
                        ref replace, ref o, ref o, ref o, ref o);

                    Marshal.FinalReleaseComObject(find);
                    Marshal.FinalReleaseComObject(range);
                }

                OleDbCommand cmd = new OleDbCommand("", connection);
                cmd.CommandText =
                    "select \n" +
                    "REPORTS.REPORT_CODE,\n" +
                    "VESSELS.VESSEL_NAME,\n" +
                    "REPORTS.COMPANY,\n" +
                    "IIF(IsNull(Q.ObsCount),0,Q.ObsCount) as ObsCount \n" +
                    "from\n" +
                    "(REPORTS left join \n" +
                    "(select REPORT_CODE, COUNT(OBSERVATION) as ObsCount \n" +
                    "from REPORT_ITEMS \n" +
                    "where \n" +
                    "LEN(REPORT_ITEMS.OBSERVATION)>0 \n" +
                    "group by REPORT_CODE) as Q \n" +
                    "on REPORTS.REPORT_CODE=Q.REPORT_CODE) \n" +
                    "left join VESSELS \n"+
                    "on REPORTS.VESSEL_GUID=VESSELS.VESSEL_GUID \n"+
                    "where \n" +
                    "REPORTS.INSPECTION_DATE>="+DateTimeToQueryStr(fromDate)+" \n" +
                    "and REPORTS.INSPECTION_DATE<"+DateTimeToQueryStr(tillDate)+" \n" +
                    "and (REPORTS.INSPECTION_TYPE_ID="+inspectionType.ToString()+
                    " or 0="+inspectionType.ToString()+") \n"+
                    "and ObsCount is Null \n"+
                    "order by VESSELS.VESSEL_NAME";

                OleDbDataReader reader = cmd.ExecuteReader();

                string zeroObs = "";

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (zeroObs.Length == 0)
                            zeroObs = reader["VESSEL_NAME"].ToString() + "\t" + reader["COMPANY"].ToString();
                        else
                            zeroObs = zeroObs + "\r" + reader["VESSEL_NAME"].ToString() + "\t" + reader["COMPANY"].ToString();
                    }
                }

                reader.Close();

                if (zeroObs.Length == 0)
                    zeroObs = "Nobody";

                foreach (Word.Range range in wordDocument.StoryRanges)
                {
                    //Word.Find find = range.Find;
                    object findText = "%ZeroObs";
                    object replacText = zeroObs;
                    object replace = Word.WdReplace.wdReplaceNone;
                    object findWrap = Word.WdFindWrap.wdFindContinue;

                    //find.Execute(ref findText, ref o, ref o, ref o, ref oFalse, ref o,
                    //    ref o, ref findWrap, ref o, ref replacText,
                    //    ref replace, ref o, ref o, ref o, ref o);

                    range.Find.Execute(ref findText, ref o, ref o, ref o, ref oFalse, ref o,
                        ref o, ref findWrap, ref o, ref o,
                        ref replace, ref o, ref o, ref o, ref o);

                    if (range.Find.Found)
                    {
                        range.Text = zeroObs;
                    }

                    //Marshal.FinalReleaseComObject(find);
                    Marshal.FinalReleaseComObject(range);
                }

                cmd.CommandText =
                    "select \n" +
                    "REPORTS.REPORT_CODE,\n" +
                    "VESSELS.VESSEL_NAME,\n" +
                    "REPORTS.COMPANY,\n" +
                    "Q.ObsCount \n" +
                    "from\n" +
                    "(REPORTS left join \n" +
                    "(select REPORT_CODE, COUNT(OBSERVATION) as ObsCount \n" +
                    "from REPORT_ITEMS \n" +
                    "where \n" +
                    "LEN(REPORT_ITEMS.OBSERVATION)>0 \n" +
                    "group by REPORT_CODE) as Q \n" +
                    "on REPORTS.REPORT_CODE=Q.REPORT_CODE) \n" +
                    "left join VESSELS \n"+
                    "on REPORTS.VESSEL_GUID=VESSELS.VESSEL_GUID \n"+
                    "where \n" +
                    "REPORTS.INSPECTION_DATE>="+DateTimeToQueryStr(fromDate)+" \n" +
                    "and REPORTS.INSPECTION_DATE<"+DateTimeToQueryStr(tillDate)+" \n" +
                    "and (REPORTS.INSPECTION_TYPE_ID=" + inspectionType.ToString() +
                    " or 0=" + inspectionType.ToString() + ") \n" +
                    "and Q.ObsCount=1 \n" +
                    "order by VESSELS.VESSEL_NAME";

                reader = cmd.ExecuteReader();

                string oneObs = "";

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (oneObs.Length == 0)
                            oneObs = reader["VESSEL_NAME"].ToString() + "\t" + reader["COMPANY"].ToString();
                        else
                            oneObs = oneObs + "\r" + reader["VESSEL_NAME"].ToString() + "\t" + reader["COMPANY"].ToString();
                    }
                }

                reader.Close();
                reader.Dispose();

                if (oneObs.Length == 0)
                    oneObs = "Nobody";

                foreach (Word.Range range in wordDocument.StoryRanges)
                {
                    //Word.Find find = range.Find;
                    object findText = "%OneObs";
                    object replacText = oneObs;
                    object replace = Word.WdReplace.wdReplaceNone;
                    object findWrap = Word.WdFindWrap.wdFindContinue;

                    //find.Execute(ref findText, ref o, ref o, ref o, ref oFalse, ref o,
                    //    ref o, ref findWrap, ref o, ref replacText,
                    //    ref replace, ref o, ref o, ref o, ref o);

                    range.Find.Execute(ref findText, ref o, ref o, ref o, ref oFalse, ref o,
                        ref o, ref findWrap, ref o, ref o,
                        ref replace, ref o, ref o, ref o, ref o);

                    if (range.Find.Found)
                    {
                        range.Text = oneObs;
                    }

                    //Marshal.FinalReleaseComObject(find);
                    Marshal.FinalReleaseComObject(range);
                }

                Word.Table table = null;

                bool tableFound = false;

                for (int i = 1; i <= wordDocument.Tables.Count;i++ )
                {
                    table = wordDocument.Tables[i];

                    if (table.Cell(1, 1).Range.Text.StartsWith("%TableObservations"))
                    {
                        tableFound = true;
                        break;
                    }
                }

                if (tableFound)
                {
                    cmd.CommandText =
                        "select \n"+
                        "iif (REPORT_ITEMS.QUESTION_NUMBER like '8.%',REPORT_ITEMS.QUESTION_NUMBER+'('+iif(TEMPLATES.TYPE_CODE like '%01','P',IIF(TEMPLATES.TYPE_CODE like '%02','C',iif(TEMPLATES.TYPE_CODE like '%03','LPG',iif(TEMPLATES.TYPE_CODE like '%04','LNG','X'))))+')',REPORT_ITEMS.QUESTION_NUMBER) as QUESTION_NUMBER, \n" +
                        "REPORT_ITEMS.OBSERVATION, \n" +
                        "REPORT_ITEMS.OPERATOR_COMMENTS, \n" +
                        "TEMPLATE_QUESTIONS.SEQUENCE,  \n"+
                        "VESSELS.VESSEL_NAME, \n"+
                        "REPORTS.COMPANY, \n"+
                        "DateValue(REPORTS.INSPECTION_DATE) as INSPECTION_DATE \n"+
                        "from  \n"+
                        "(((REPORTS inner join REPORT_ITEMS  \n" +
                        "on REPORTS.REPORT_CODE=REPORT_ITEMS.REPORT_CODE) \n" +
                        "inner join TEMPLATES \n"+
                        "on REPORTS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID)  \n"+
                        "inner join TEMPLATE_QUESTIONS  \n"+
                        "on REPORTS.TEMPLATE_GUID=TEMPLATE_QUESTIONS.TEMPLATE_GUID)  \n" +
                        "left join VESSELS \n"+
                        "on REPORTS.VESSEL_GUID=VESSELS.VESSEL_GUID \n"+
                        "where \n" +
                        "LEN(OBSERVATION)>0 \n" +
                        "and (REPORTS.INSPECTION_TYPE_ID=" + inspectionType.ToString() +
                        " or 0=" + inspectionType.ToString() + ") \n" +
                        "and REPORTS.INSPECTION_DATE>=" + DateTimeToQueryStr(fromDate) + " \n" +
                        "and REPORTS.INSPECTION_DATE<" + DateTimeToQueryStr(tillDate) + " \n" +
                        "and REPORT_ITEMS.QUESTION_GUID=TEMPLATE_QUESTIONS.QUESTION_GUID \n" +
                        "order by TEMPLATE_QUESTIONS.SEQUENCE";

                    reader = cmd.ExecuteReader();

                    string obs = "";
                    int counter = 0;

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Word.Row row = null;

                            if (counter == 0)
                            {
                                row = table.Rows[1];
                                counter++;
                            }
                            else
                                row = table.Rows.Add(o);

                            DateTime inspectionDate = Convert.ToDateTime(reader["INSPECTION_DATE"]);

                            string obsDetails = reader["VESSEL_NAME"].ToString() + " - " + reader["COMPANY"].ToString() + " - " + inspectionDate.ToShortDateString();

                            obs = reader["QUESTION_NUMBER"].ToString() + " " + reader["OBSERVATION"].ToString();

                            Word.Range range = row.Range;
                            range.Text = obs;
                            
                            Word.Paragraph detailsPara = range.Paragraphs.Add();

                            Word.Range detailsRange = detailsPara.Range;

                            detailsRange.Text = "(" + obsDetails + ")";
                            detailsRange.Font.Color = (Word.WdColor)(Color.DarkGray.R + 0x100 * Color.DarkGray.G + 0x10000 * Color.DarkGray.B);

                            if (useComments)
                            {
                                range.Paragraphs.Add();
                                Word.Paragraph para = range.Paragraphs.Add();

                                Word.Range commentsRange = para.Range;

                                if (useCommonComments)
                                    commentsRange.Text = commentsLabel + " " + commentsText;
                                else
                                    commentsRange.Text = commentsLabel + " " + reader["OPERATOR_COMMENTS"].ToString();

                                if (useSelectedColor)
                                {
                                    commentsRange.Font.Color = (Word.WdColor)(selectedColor.R + 0x100 * selectedColor.G + 0x10000 * selectedColor.B);
                                }
                                else
                                {
                                    Word.Style scfStyle = null;

                                    try
                                    {
                                        scfStyle = wordDocument.Styles["SCF"];
                                    }
                                    catch
                                    {
                                        //Do nothing
                                    }

                                    if (scfStyle != null)
                                        commentsRange.set_Style(scfStyle);
                                    else
                                        commentsRange.Font.Color = (Word.WdColor)(selectedColor.R + 0x100 * selectedColor.G + 0x10000 * selectedColor.B);
                                }
                            }
                        }
                    }
                    else
                    {
                        Word.Row row = table.Rows[1];
                        row.Range.Text = "There is no observation";
                    }

                    reader.Close();
                }

                ((Word._Application)wordapp).Visible = true;
            }

            catch (Exception E)
            {
                MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Cursor = Cursors.Default;
        }

        public static string TextToLine(string text)
        {
            string line = text;

            line = line.Replace("\t", "%tb");
            line = line.Replace("\n", "%nl");
            line = line.Replace("\r", "%lf");

            return line;
        }

        public static string LineToText(string line)
        {
            string text = line;

            text = text.Replace("%tb", "\t");
            text = text.Replace("%nl", "\n");
            text = text.Replace("%lf", "\r");

            return text;
        }

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        private void tbtnLoadPersons_Click(object sender, EventArgs e)
        {
            //Load persons from Excel file
            LoadPersons();
        }

        private void LoadPersons()
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Excel files(*.xls,*.xlsx,*.xlsm)|*.xls;*.xlsx;*.xlsm|All files(*.*)|*.*";

            var rslt = openFileDialog1.ShowDialog();

            if (rslt==DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;

                var excelApp = createExcel(fileName);
                excelApp.Visible = false;

                if (excelApp == null)
                    return;

                Excel._Worksheet ws = excelApp.Worksheets.get_Item(1);

                int usedRows = ws.UsedRange.Rows.Count;
                int usedCols = ws.UsedRange.Columns.Count;

                var data = ws.UsedRange.Value2;

                excelApp.Quit();

                int reportCol = 0;
                int inspectorCol = 0;
                int masterCol = 0;
                int chEngCol = 0;
                int vesselCol = 0;
                int dateCol = 0;

                if (usedRows > 1)
                {
                    if (DS.Tables.Contains("PERSONS_IMPORT"))
                        DS.Tables["PERSONS_IMPORT"].Clear();


                    DataTable personsImport = null;

                    for (int i = 1; i <= usedRows; i++)
                    {

                        if (i == 1)
                        {
                            for (int j = 1; j <= usedCols; j++)
                            {
                                string s = data[i, j];

                                switch (s)
                                {
                                    case "Report":
                                        reportCol = j;
                                        break;
                                    case "Vessel":
                                        vesselCol = j;
                                        break;
                                    case "Date":
                                        dateCol = j;
                                        break;
                                    case "Inspector":
                                        inspectorCol = j;
                                        break;
                                    case "Master":
                                        masterCol = j;
                                        break;
                                    case "Chief Enigneer":
                                        chEngCol = j;
                                        break;
                                }
                            }

                            
                            if (reportCol>0)
                            {
                                if (DS.Tables.Contains("PERSONS_IMPORT"))
                                    personsImport = DS.Tables["PERSONS_IMPORT"];
                                else
                                {
                                    personsImport = DS.Tables.Add("PERSONS_IMPORT");
                                    personsImport.Columns.Add("REPORT_CODE", Type.GetType("System.String"));
                                }
                            }

                            if (personsImport!=null && vesselCol>0)
                            {
                                if (!personsImport.Columns.Contains("VESSEL"))
                                    personsImport.Columns.Add("VESSEL", Type.GetType("System.String"));
                            }

                            if (personsImport!=null && dateCol>0)
                            {
                                if (!personsImport.Columns.Contains("INSPECTION_DATE"))
                                    personsImport.Columns.Add("INSPECTION_DATE",Type.GetType("System.DateTime"));
                            }

                            if (personsImport != null && inspectorCol>0)
                            {
                                if (!personsImport.Columns.Contains("INSPECTOR"))
                                    personsImport.Columns.Add("INSPECTOR", Type.GetType("System.String"));
                            }

                            if (personsImport != null && masterCol>0)
                            {
                                if (!personsImport.Columns.Contains("MASTER"))
                                    personsImport.Columns.Add("MASTER", Type.GetType("System.String"));
                            }

                            if (personsImport != null && chEngCol>0)
                            {
                                if (!personsImport.Columns.Contains("CHENG"))
                                    personsImport.Columns.Add("CHENG", Type.GetType("System.String"));
                            }
                        }
                        else
                        {
                            if (personsImport!=null && reportCol > 0)
                            {
                                string rc = data[i, reportCol];

                                if (rc != null && rc.Length > 0)
                                {
                                    DataRow row = personsImport.NewRow();

                                    row["REPORT_CODE"] = rc;

                                    if (vesselCol > 0)
                                        row["VESSEL"] = data[i, vesselCol];

                                    if (dateCol > 0)
                                    {
                                        var xlValue = data[i, dateCol];

                                        try
                                        {
                                            DateTime dtValue = Convert.ToDateTime(xlValue);
                                            row["INSPECTION_DATE"] = dtValue;
                                        }
                                        catch
                                        {
                                            double dt = double.Parse(Convert.ToString(xlValue));
                                            row["INSPECTION_DATE"] = DateTime.FromOADate(dt);
                                        }
                                    }

                                    if (inspectorCol>0)
                                        row["INSPECTOR"] = data[i, inspectorCol];

                                    if (masterCol>0)
                                        row["MASTER"] = data[i, masterCol];
                                    
                                    if (chEngCol>0)
                                        row["CHENG"] = data[i, chEngCol];


                                    personsImport.Rows.Add(row);
                                }
                            }

                        }
                    }

                    //open form
                    if (personsImport != null && personsImport.Rows.Count > 0)
                    {
                        FrmImportPersonnel form = new FrmImportPersonnel(connection, DS, this.Icon, this.Font);

                        form.ShowDialog();
                    }

                }
            }
        }

        private void dgvDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Modified on 12.10.2016 in version 2.0.15.1
            //  Use ReportSummaryForm

            if (dgvDetails.Rows.Count == 0) 
                return;

            if (dgvDetails.Columns[e.ColumnIndex].Name == "OBSERVATION" || dgvDetails.Columns[e.ColumnIndex].Name == "OPERATOR_COMMENTS")
            {
                FormObsView form = new FormObsView(this.Icon, this.Font);

                form.txtObservation = dgvDetails.CurrentRow.Cells["OBSERVATION"].Value.ToString();
                form.txtComments = dgvDetails.CurrentRow.Cells["OPERATOR_COMMENTS"].Value.ToString();
                form.questionText = GetQuestionNumberText(dgvDetails.CurrentRow.Cells["QUESTION_GUID"].Value.ToString());

                form.ShowDialog();

                return;
            }
            else
                ShowInspection();


        }

        private string GetQuestionNumberText(string questionGUID)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select TOP 1 QUESTION_NUMBER, QUESTION_TEXT \n" +
                "from TEMPLATE_QUESTIONS \n" +
                "where QUESTION_GUID like '{" + questionGUID + "}'";

            OleDbDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();

                string q = "";

                q = reader["QUESTION_NUMBER"].ToString() + " " + reader["QUESTION_TEXT"].ToString();

                return q;
            }
            else
            {
                return "";
            }
        }

        private void dgvDetails_FilterStringChanged(object sender, EventArgs e)
        {
            bsDetails.Filter = dgvDetails.FilterString;
        }

        private void dgvDetails_SortStringChanged(object sender, EventArgs e)
        {
            string sortString = dgvDetails.SortString;

            if (sortString.Contains("[QUESTION_NUMBER]"))
                sortString = sortString.Replace("[QUESTION_NUMBER]", "[SEQUENCE]");

            bsDetails.Sort = sortString;
        }

        public static object ReadRegValue(string key)
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(appRegKey);

            object regValue = null;

            try
            {
                regValue = regKey.GetValue(key);
            }
            catch
            {
                //do nothing
            }
            
            return regValue;
        }
        
        public static bool WriteRegValue(string key, string name, object value)
        {
            string activeKey = appRegKey;

            if (key.Trim().Length > 0)
                activeKey = activeKey + "\\" + key;

            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(activeKey);

            if (regKey == null)
                regKey = Registry.CurrentUser.CreateSubKey(activeKey);

            try
            {
                regKey.SetValue(name, value);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void SetType()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select Count(TEMPLATE_TYPE) as TYPE_COUNT \n" +
                "from \n" +
                "(SELECT TEMPLATE_TYPE \n" +
                "from TEMPLATES \n" +
                "group by TEMPLATE_TYPE)";

            int typeCount = (int)cmd.ExecuteScalar();

            
            if (typeCount==1)
            {
                //Just one type
                cmd.CommandText =
                    "SELECT TEMPLATE_TYPE \n" +
                    "from TEMPLATES \n" +
                    "group by TEMPLATE_TYPE";

                string typeCode = (string)cmd.ExecuteScalar();

                templatesType = typeCode;
            }
            else
            {
                if (typeCount == 0)
                    templatesType = "VIQ";
                else
                {
                    cmd.CommandText=
                        "SELECT TEMPLATE_TYPE \n" +
                        "from TEMPLATES \n" +
                        "group by TEMPLATE_TYPE";

                    if (DS.Tables.Contains("TEMPLATE_TYPES"))
                        DS.Tables["TEMPLATE_TYPES"].Clear();

                    OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);

                    adpt.Fill(DS, "TEMPLATE_TYPES");

                    FormSelectType form = new FormSelectType(DS, this.Icon, this.Font);

                    var rslt = form.ShowDialog();

                    if (rslt==DialogResult.OK)
                    {
                        templatesType = form.selectedType;

                        if (templatesType.Trim().Length == 0)
                        {
                            templatesType = "VIQ";

                            MessageBox.Show("Selected type is wrong. Application will use VIQ templates", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        templatesType = "VIQ";
                        MessageBox.Show("Proper type was not selected. Application will use VIQ templates", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            //SetType();
        }

        private void dgvDetails_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
        }

        private void miOpenInspection_Click(object sender, EventArgs e)
        {
            //show inspection
            if (dgvDetails.Rows.Count > 0)
                ShowInspection();
        }

        private void ShowInspection()
        {
            string reportCode = dgvDetails.CurrentRow.Cells["REPORT_CODE"].Value.ToString();

            OleDbCommand cmd = new OleDbCommand("", connection);
            cmd.CommandText =
                "select REPORTS.REPORT_CODE, VESSELS.VESSEL_NAME, VESSELS.VESSEL_IMO, REPORTS.VESSEL_GUID, " +
                "REPORTS.INSPECTOR_GUID, INSPECTORS.INSPECTOR_NAME, REPORTS.INSPECTION_DATE, REPORTS.INSPECTION_PORT, " +
                "REPORTS.COMPANY, REPORTS.VIQ_TYPE, REPORTS.VIQ_VERSION, VESSELS.OFFICE, VESSELS.DOC, VESSELS.HULL_CLASS, " +
                "MASTER_LIST.MASTER_GUID, MASTER_LIST.MASTER_NAME, CHENG_LIST.CHENG_GUID, CHENG_LIST.CHENG_NAME, " +
                "REPORTS.MANUAL, REPORTS.FILE_AVAILABLE, REPORTS.VIQ_TYPE_CODE, REPORTS.TEMPLATE_GUID \n" +
                "from (((REPORTS left join VESSELS \n" +
                "on REPORTS.VESSEL_GUID=VESSELS.VESSEL_GUID) \n" +
                "left join \n" +
                "( \n" +
                "select CREW.CREW_NAME as MASTER_NAME, CREW.CREW_GUID as MASTER_GUID, REPORT_CODE \n" +
                "from \n" +
                "REPORTS_CREW inner join CREW \n" +
                "on REPORTS_CREW.CREW_GUID = CREW.CREW_GUID \n" +
                "where \n" +
                "REPORTS_CREW.CREW_POSITION_GUID ={753D9AAC-1376-4FAE-AB00-144D81341F70} \n" +
                ") as MASTER_LIST \n" +
                "on REPORTS.REPORT_CODE = MASTER_LIST.REPORT_CODE) \n" +
                "left join \n" +
                "( \n" +
                "select CREW.CREW_NAME as CHENG_NAME, CREW.CREW_GUID as CHENG_GUID, REPORT_CODE \n" +
                "from \n" +
                "REPORTS_CREW inner join CREW \n" +
                "on REPORTS_CREW.CREW_GUID = CREW.CREW_GUID \n" +
                "where \n" +
                "REPORTS_CREW.CREW_POSITION_GUID ={012156E3-2990-468F-AFB4-DAD5401844F7} \n" +
                ") as CHENG_LIST \n" +
                "on REPORTS.REPORT_CODE = CHENG_LIST.REPORT_CODE)  \n" +
                "left join INSPECTORS \n" +
                "on REPORTS.INSPECTOR_GUID=INSPECTORS.INSPECTOR_GUID \n" +
                "where REPORT_CODE='" + StrToSQLStr(reportCode) + "'";

            OleDbDataReader rr = cmd.ExecuteReader();

            if (rr.HasRows)
            {
                rr.Read();

                this.Cursor = Cursors.WaitCursor;

                FormReportSummary form = new FormReportSummary(false, reportCode, false, false);

                var rslt = form.ShowDialog();

                this.Cursor = Cursors.Default;

                if (rslt == DialogResult.OK)
                {
                    fillInspectors();

                    int curRow = dgvDetails.CurrentRow.Index;
                    int curCol = dgvDetails.CurrentCell.ColumnIndex;

                    updateGrid2();

                    dgvDetails.CurrentCell = dgvDetails[curCol, curRow];

                }

            }
            else
                rr.Close();

        }

        public static void SetAppDataFolder()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

            if (!Directory.Exists(appData))
            {
                try
                {
                    Directory.CreateDirectory("C:\\ProgramData");
                    appData = "C:\\ProgramData";
                }
                catch
                {
                    appData="C:\\";
                }
            }

            string curAppData = Path.Combine(appData, appName);

            if (!Directory.Exists(curAppData))
            {
                Directory.CreateDirectory(curAppData);
            }

            appDataFolder = curAppData;
        }

        public bool SaveOptionValue(int tag,string value)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select Count(TAG) as RecNum \n" +
                "from OPTIONS \n" +
                "where TAG=" + tag.ToString();

            int recs = (int)cmd.ExecuteScalar();

            if (recs==0)
            {
                cmd.CommandText =
                    "insert into OPTIONS (TAG, STR_VALUE) \n" +
                    "values(" + tag.ToString() + ",'" +
                        StrToSQLStr(value) + "')";

                return cmdExecute(cmd)>=0;
            }
            else
            {
                cmd.CommandText =
                    "update OPTIONS set \n" +
                    "STR_VALUE='" + StrToSQLStr(value) + "' \n" +
                    "where TAG=" + tag.ToString();

                return cmdExecute(cmd)>=0;
            }
        }

        private void RecalcObs()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            string tempName="TEMPOBS-" + programID;
            string tempSQL =
                "create table [" + tempName + "] (ID counter primary key, REPORT_CODE varchar(50), OBS integer)";

            bool needDefault = false;

            try
            {

                if (this.Cursor != Cursors.WaitCursor)
                {
                    this.Cursor = Cursors.WaitCursor;
                    needDefault = true;
                }

                if (tempTableCreate(tempName, tempSQL))
                {
                    try
                    {
                        cmd.CommandText =
                            "insert into [" + tempName + "] (REPORT_CODE, OBS) \n" +
                            "select \n" +
                            "REPORT_CODE, \n" +
                            "count(QUESTION_GUID) as OBS \n" +
                            "from \n"+
                            "REPORT_ITEMS \n" +
                            "where \n" +
                            "LEN(OBSERVATION)>0 \n" +
                            "group by REPORT_CODE";

                        if (cmdExecute(cmd)<0)
                            return;

                        cmd.CommandText =
                            "update REPORTS left join [" + tempName + "] \n" +
                            "on REPORTS.REPORT_CODE=[" + tempName + "].REPORT_CODE \n" +
                            "set \n" +
                            "REPORTS.OBS_COUNT=IIF(IsNull([" + tempName + "].OBS),0,[" + tempName + "].OBS)";
                        cmdExecute(cmd);
                    }
                    finally
                    {
                        tempTableDrop(tempName);
                    }
                }
            }
            finally
            {
                if (needDefault)
                    this.Cursor = Cursors.Default;
            }
        }

        private string GetUserTempPath()
        {

            IniFile iniFile = new IniFile(iniPersonalFile);

            string xSection = "USER_OPTIONS";

            string xUserTempFolder = iniFile.ReadString(xSection, "USER_TEMP_FOLDER", "");

            if (xUserTempFolder.Length == 0)
                xUserTempFolder = Path.GetTempPath();

            return xUserTempFolder;
        }

        public static int ExecuteCommand(string command)
        {
            int ExitCode;
            ProcessStartInfo ProcessInfo;
            Process process;

            ProcessInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.UseShellExecute = false;
            //ProcessInfo.WorkingDirectory = Application.StartupPath + "\\txtmanipulator";
            // *** Redirect the output ***
            ProcessInfo.RedirectStandardError = true;
            ProcessInfo.RedirectStandardOutput = true;

            process = Process.Start(ProcessInfo);
            process.WaitForExit();

            // *** Read the streams ***
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            ExitCode = process.ExitCode;

            process.Close();

            return ExitCode;
        }

        public static void GetMAPIClients()
        {
            MAPIClients.Clear();
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Clients\\Mail");

            string[] clients = regKey.GetSubKeyNames();

            for (int i=0;i<clients.GetLength(0);i++)
            {
                MAPIClients.Insert(i, clients[i]);
            }
        }

        public static int GetMailClientID()
        {
            IniFile iniFile = new IniFile(iniPersonalFile);

            string section = "Mail client";

            return iniFile.ReadInteger(section, "MailClientID", 0);
        }

        public static void SetMailClientID(int mailClientID)
        {
             IniFile iniFile = new IniFile(iniPersonalFile);

            string section = "Mail client";

            int id = 0;

            id = mailClientID;

            iniFile.Write(section, "MailClientID", id);
        }

        public static string StrToIniStr(string text)
        {
            string _text = text;
 
            _text = _text.Replace("\n", "&nl");
            _text = _text.Replace("\r", "&lf");
            _text = _text.Replace("\t", "&tb");
            _text = _text.Replace("[", "&bo");
            _text = _text.Replace("]", "&bc");
            _text = _text.Replace("=", "&eq");

            return _text;
        }

        public static string IniStrToStr(string text)
        {
            string _text = text;

            _text = _text.Replace("&nl", "\n");
            _text = _text.Replace("&lf", "\r");
            _text = _text.Replace("&tb", "\t");
            _text = _text.Replace("&bo", "[");
            _text = _text.Replace("&bc", "]");
            _text = _text.Replace("&eq", "=");

            if (_text.Contains("\n") && !_text.Contains("\r"))
                _text = _text.Replace("\n", "\r\n");

            return _text;
        }

        private void fleetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFleets form = new FrmFleets(connection, DS, this.Font, this.Icon);

            form.ShowDialog();

            if (form.dataChanged)
            {
                int col = dgvInspections.CurrentCell.ColumnIndex;
                int row = dgvInspections.CurrentCell.RowIndex;

                fillReports();

                if (dgvInspections.Rows.Count > 0)
                {
                    if (row >= dgvInspections.Rows.Count)
                        dgvInspections.CurrentCell = dgvInspections[col, dgvInspections.Rows.Count - 1];
                    else
                        dgvInspections.CurrentCell = dgvInspections[col, row];
                }
            }
        }

        private void officesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOffices form = new FrmOffices();

            form.ShowDialog();

            if (form.dataChanged)
            {
                int col = dgvInspections.CurrentCell.ColumnIndex;
                int row = dgvInspections.CurrentCell.RowIndex;

                fillReports();

                if (dgvInspections.Rows.Count > 0)
                {
                    if (row >= dgvInspections.Rows.Count)
                        dgvInspections.CurrentCell = dgvInspections[col, dgvInspections.Rows.Count - 1];
                    else
                        dgvInspections.CurrentCell = dgvInspections[col, row];
                }
            }
        }

        private void dOCsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDOCs form = new FrmDOCs();

            form.ShowDialog();

            if (form.dataChanged)
            {
                int col = dgvInspections.CurrentCell.ColumnIndex;
                int row = dgvInspections.CurrentCell.RowIndex;

                fillReports();

                if (dgvInspections.Rows.Count>0)
                {
                    if (row >= dgvInspections.Rows.Count)
                        dgvInspections.CurrentCell = dgvInspections[col, dgvInspections.Rows.Count - 1];
                    else
                        dgvInspections.CurrentCell = dgvInspections[col, row];
                }
            }
        }

        public static string GetApplicationType()
        {
            //Read settings from ini file
            IniFile iniFile = new IniFile(iniCommonFile);

            //Use global section for bulletin settings
            string section = "General";

            string typeString = iniFile.ReadString(section, "Type", "");

            return typeString;
        }

        public static void SetApplicationType(string type)
        {
            //Write settings to ini file
            
            IniFile iniFile = new IniFile(iniCommonFile);

            //Use global section for bulletin settings
            string section = "General";

            iniFile.Write(section, "Type", type);
        }

        private void lastInspectionByVesselToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select DOCS.*, 0 as SELECTED \n" +
                "from DOCS \n" +
                "order by DOC_TEXT, DOC_ID";

            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("DOC_LIST"))
                DS.Tables["DOC_LIST"].Clear();

            adapter.Fill(DS, "DOC_LIST");

            FrmLastInspectionReport form = new FrmLastInspectionReport();

            var rslt = form.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                string s = "";
                int counter = 0;

                DataRow[] rows = DS.Tables["DOC_LIST"].Select();

                foreach (DataRow row in rows)
                {
                    if (Convert.ToBoolean(Convert.ToInt32(row["SELECTED"])==1))
                    {
                        if (s.Length == 0)
                            s = "and VESSELS.DOC='" + row["DOC_ID"].ToString() + "'";
                        else
                            s = s + "\n" + "and VESSELS.DOC='" + row["DOC_ID"].ToString() + "'";

                        counter++;
                    }
                    else
                    {
                        if (s.Length == 0)
                            s = "and VESSELS.DOC<>'" + row["DOC_ID"].ToString() + "'";
                        else
                            s = s + "\n" + "and VESSELS.DOC<>'" + row["DOC_ID"].ToString() + "'";
                    }
                }

                if (counter > 0)
                {
                    if (s.Length > 0)
                        s = s + "\n";

                    cmd.CommandText =
                        "select \n" +
                        "VESSELS.VESSEL_NAME as [Vessel name], \n" +
                        "VESSELS.VESSEL_IMO as [IMO number], \n" +
                        "MAX(DateValue(REPORTS.INSPECTION_DATE)) as [Last inspection] \n" +
                        "from VESSELS inner join REPORTS \n" +
                        "on VESSELS.VESSEL_GUID=REPORTS.VESSEL_GUID \n" +
                        "where VESSELS.HIDDEN=FALSE \n" + s +
                        "group by VESSELS.VESSEL_NAME, VESSELS.VESSEL_IMO";

                    adapter.SelectCommand = cmd;

                    DataGridView dgv = new DataGridView();
                    dgv.Visible = false;
                    dgv.Parent = this;

                    if (DS.Tables.Contains("LAST_INSPECTIONS"))
                        DS.Tables["LAST_INSPECTIONS"].Clear();


                    adapter.Fill(DS, "LAST_INSPECTIONS");

                    dgv.AutoGenerateColumns = true;
                    dgv.DataSource = DS;
                    dgv.DataMember = "LAST_INSPECTIONS";

                    ExportToExcel2(dgv, false, 1, 1);

                    DS.Tables["LAST_INSPECTIONS"].Dispose();
                }
                else
                {
                    MessageBox.Show("Non of the DOCs was selected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            DS.Tables["DOC_LIST"].Dispose();
        }

        public static List<string> ParseStringToList (string inString, string delimiters)
        {
            List<string> list = new List<string>();
            string s = "";

            for (int i=0; i < inString.Length; i++ )
            {
                if (delimiters.Contains(inString[i].ToString()))
                {
                    list.Add(s.Trim());
                    s = "";
                }
                else
                {
                    s = s + inString[i].ToString();
                }
            }
            
            list.Add(s.Trim());

            return list;
        }

        private void crewOnBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCrewOnBoard form = new FrmCrewOnBoard();

            form.ShowDialog();

        }

        public static string ReadIniValue(string iniFileName,string iniSection,string iniName, string defValue="")
        {
            //Read settings from ini file
            IniFile iniFile = new IniFile(iniFileName);

            //Use global section for bulletin settings
            string section = iniSection;

            string iniString = iniFile.ReadString(section, iniName, defValue);

            return iniString;

        }

        public static bool WriteIniValue(string iniFileName, string iniSection, string iniName, string iniValue)
        {
            //Write settings to ini file

            IniFile iniFile = new IniFile(iniFileName);

            //Use global section for bulletin settings
            string section = iniSection;

            return iniFile.Write(section, iniName, iniValue);

        }

        public static bool StrToBool(string anyString)
        {
            return anyString.StartsWith("True", StringComparison.OrdinalIgnoreCase);
        }

        public static string BoolToStr(bool anyValue)
        {
            if (anyValue)
                return "True";
            else
                return "False";
        }

        private void tbtnUpdateDB_Click(object sender, EventArgs e)
        {

            if (isPowerUser) 
            {
                connection.Close();

                //Copy database from temp folder to application folder
                if (SaveDatabase())
                {
                    MessageBox.Show("Database updated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DateTime now = DateTime.Now;

                    tbtnUpdateDB.ToolTipText = "Update database in Dropbox (Last update - " + now.ToShortDateString()+" "+now.ToLongTimeString() + ")";
                }
                else
                {
                    MessageBox.Show("Failed to update database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                connection.Open();

            }
        }

        public static void UpdateVesselGuid4CrewOnBoard()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                        "update CREW_ON_BOARD left join VESSELS \n" +
                        "on CREW_ON_BOARD.VESSEL_NAME=VESSELS.VESSEL_NAME \n" +
                        "set CREW_ON_BOARD.VESSEL_GUID=VESSELS.VESSEL_GUID";

            MainForm.cmdExecute(cmd);
        }

        public static string GetFleetEmail(Guid vesselGuid)
        {
            string fleetEmail = "";

            if (vesselGuid == null || vesselGuid == zeroGuid)
                return fleetEmail;


            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select top 1 FLEET \n" +
                "from VESSELS \n" +
                "where \n" +
                "VESSEL_GUID=" + GuidToStr(vesselGuid);

            string fleet = (string)cmd.ExecuteScalar();

            if (fleet.Length > 0)
            {

                cmd.CommandText =
                    "select FLEET_EMAIL \n" +
                    "from FLEET_EMAILS \n" +
                    "where FLEET_ID='" + MainForm.StrToSQLStr(fleet) + "' \n" +
                    "order by FLEET_EMAIL";

                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (fleetEmail.Length == 0)
                            fleetEmail = reader["FLEET_EMAIL"].ToString();
                        else
                            fleetEmail = fleetEmail + "; " + reader["FLEET_EMAIL"].ToString();
                    }
                }

                reader.Close();

            }
        
            return fleetEmail;
        }

        private void checkLastReportAgainstOCIMFFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCheckOCIMF form = new FormCheckOCIMF();

            form.ShowDialog();
        }

        private void inspectionTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormInspectionTypes form = new FormInspectionTypes();

            form.ShowDialog();
        }

        private void pSCMemorandumsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMemorandums form = new FormMemorandums();

            form.ShowDialog();
        }

        public static string FormatGuidString(string guidStr)
        {
            if (guidStr.Length == 0)
                return "Null";

            if (!guidStr.StartsWith("{"))
                return "{" + guidStr.ToUpper() + "}";
            else
                return guidStr.ToUpper();
        }

        public static string GuidToStr(Guid guid, bool UseNull=false)
        {
            Guid zGuid = zeroGuid;

            if (guid != null)
            {
                zGuid = guid;
            }

            string str = zGuid.ToString();

            if (!str.StartsWith("{"))
                str = "{" + str + "}";

            if (zGuid == zeroGuid && UseNull)
                str = "Null";

            return str;
        }

        public static bool ClearTable(string tableName, string where="")
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            string _where="";

            if (where.Length>0)
            {
                if (where.StartsWith("where"))
                    _where=where;
                else
                    _where="where "+where;
            }

            cmd.CommandText =
                "delete from [" + tableName + "] \n" +
                where;

            return cmdExecute(cmd)>=0;
        }

        public static string BuildInspectorScoring(Guid inspectorGuid)
        {
            string tempTableName = "$SCORING_" + MainForm.programID;

            tempTableName = tempTableName.Replace("{", "");
            tempTableName = tempTableName.Replace("}", "");
            tempTableName = tempTableName.Replace("-", "");

            string script =
                "create table [" + tempTableName + "] \n" +
                "(ID counter Primary key, CHAPTER varchar(50), OBS_COUNT varchar(10))";

            if (!tempTableCreate(tempTableName, script))
                return "";

            OleDbCommand cmd = new OleDbCommand("", connection);

            for (int i = 1; i < 14; i++)
            {
                cmd.CommandText =
                    "select iif(IsNull(Count(QUESTION_NUMBER)),0,Count(QUESTION_NUMBER)) AS QCOUNT \n" +
                    "from REPORT_ITEMS inner join  \n" +
                    "(select REPORT_CODE as RC \n" +
                    "from REPORTS \n" +
                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + ") as INSP_REPORTS \n" +
                    "on REPORT_ITEMS.REPORT_CODE=INSP_REPORTS.RC \n" +
                    "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0" +
                    ",False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='" + i.ToString() + "')  \n" +
                    "and LEN(OBSERVATION)>0";

                int obs = (int)cmd.ExecuteScalar();

                string count = "0";

                if (obs > 0)
                    count = obs.ToString();


                cmd.CommandText =
                    "insert into [" + tempTableName + "] (CHAPTER,OBS_COUNT) \n" +
                    "values('" + i.ToString() + "','" + count + "')";
                MainForm.cmdExecute(cmd);
            }

            return tempTableName;
        }

        private void tscbInspectionType_TextChanged(object sender, EventArgs e)
        {
            inspectionType = GetInspectionTypeID(tscbInspectionType.Text);
            updateReports();
        }

        public static Guid StrToGuid(string strGuid)
        {
            Guid gValue = zeroGuid;

            if (strGuid != null)
            {
                try
                {
                    gValue = Guid.Parse(strGuid);
                }
                catch
                {
                    //do nothing
                }
            }

            return gValue;
        }

        private void tscbInspectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            inspectionType = GetInspectionTypeID(tscbInspectionType.Text);
            updateReports();
        }

        public static string GetPositionName(Guid positionGuid)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select TOP 1 POSITION_NAME \n" +
                "from CREW_POSITIONS \n" +
                "where CREW_POSITION_GUID";

            object pn = cmd.ExecuteScalar();

            if (pn != null)
                return pn.ToString();
            else
                return "";
        }

        public static Guid GetPositionGuid(string positionName)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select TOP 1 CREW_POSITION_GUID \n" +
                "from CREW_POSITIONS \n" +
                "where POSITION_NAME='" + StrToIniStr(positionName) + "'";

            object pg = cmd.ExecuteScalar();

            if (pg != null)
            {
                return StrToGuid(pg.ToString());
            }
            else
                return zeroGuid;
        }

        private void crewPositionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Show crew position list

            FormCrewPositions form = new FormCrewPositions();

            form.ShowDialog();
        }

        public static int GetReportsCount()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select Count(REPORT_CODE) as RecCount \n" +
                "from REPORTS";

            int recs = (int)cmd.ExecuteScalar();

            return recs;
        }

        public static Guid GetCrewGuid(string crewName, Guid positionGuid)
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

        public static Guid SaveNewCrew(string crewName, Guid positionGuid)
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

        private void gapReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGapReport form = new FormGapReport();

            form.ShowDialog();
        }

        public static Guid GetTeplateTypeGuid(string TemplateTypeStr)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select TEMPLATE_TYPE_GUID \n" +
                "from TEMPLATE_TYPES \n" +
                "where TEMPLATE_TYPE='" + StrToSQLStr(TemplateTypeStr) + "'";

            object rslt = cmd.ExecuteScalar();

            if (rslt != null)
                return (Guid)rslt;
            else
                return zeroGuid;
        }

        public static string StrToSingleLine(string anyString)
        {
            string rsltString = anyString.Trim();

            rsltString = rsltString.Replace("\r", " ");
            rsltString = rsltString.Replace("\n", " ");

            rsltString = rsltString.Trim();

            int strLen = 0;

            while (strLen != rsltString.Length)
            {
                strLen = rsltString.Length;
                rsltString = rsltString.Replace("  ", " ");
            }

            return rsltString;
        }
    }
}
