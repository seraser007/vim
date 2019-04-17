using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Runtime.InteropServices;
using System.IO;

namespace VIM
{
    public partial class FormGapReport : Form
    {
        private DateTime startDate = DateTime.Now;
        private DateTime finishDate = DateTime.Now;
        private int quater;
        private int quaterYear;
        private int selector = 1;

        private BindingSource BS = new BindingSource();
        private BindingSource xBS = new BindingSource();
        private BindingSource yBS = new BindingSource();

        public FormGapReport()
        {
            this.Icon = MainForm.mainIcon;
            this.Font = MainForm.mainFont;

            int month = DateTime.Now.Month;

            quaterYear = DateTime.Now.Year;

            if (month < 4)
                quater = 1;
            else
            {
                if (month < 7)
                    quater = 2;
                else
                {
                    if (month < 10)
                        quater = 3;
                    else
                        quater = 4;

                }
            }

            switch (quater)
            {
                case 1:
                    quater = 4;
                    quaterYear = quaterYear - 1;
                    startDate = new DateTime(quaterYear, 10, 1);
                    finishDate = new DateTime(quaterYear, 12, 31);
                    break;
                case 2:
                    quater = 1;
                    startDate = new DateTime(quaterYear, 1, 1);
                    finishDate = new DateTime(quaterYear, 3, 31);
                    break;
                case 3:
                    quater = 2;
                    startDate = new DateTime(quaterYear, 4, 1);
                    finishDate = new DateTime(quaterYear, 6, 30);
                    break;
                case 4:
                    quater = 3;
                    startDate = new DateTime(quaterYear, 7, 1);
                    finishDate = new DateTime(quaterYear, 9, 30);
                    break;
            }

            InitializeComponent();

            selector = Convert.ToInt32(MainForm.ReadIniValue(MainForm.iniCommonFile, "Gap report", "Selector", "1"));

            if (selector == 1)
                tstbPeriod.Text = quaterYear.ToString()+ ", Quater " + quater.ToString();
            else
                tstbPeriod.Text = startDate.ToShortDateString() + " - " + finishDate.ToShortDateString();

            UpdateAudits();
        }

        private void UpdateAudits()
        {
            OleDbCommand cmd=new OleDbCommand("",MainForm.connection);

            DateTime start = startDate;
            DateTime finish = finishDate;

            if (selector==1)
            {
                DateTime now=DateTime.Now;

                switch (quater)
                {
                    case 1:
                        start = new DateTime(quaterYear, 1, 1);
                        finish = new DateTime(quaterYear, 3, 31);
                        break;
                    case 2:
                        start = new DateTime(quaterYear, 4, 1);
                        finish = new DateTime(quaterYear, 6, 30);
                        break;
                    case 3:
                        start = new DateTime(quaterYear, 7, 1);
                        finish = new DateTime(quaterYear, 9, 30);
                        break;
                    case 4:
                        start = new DateTime(quaterYear, 10, 1);
                        finish = new DateTime(quaterYear, 12, 31);
                        break;
                }
            }

            cmd.CommandText=
                "select * \n"+
                "from AUDITS \n"+
                "where INSPECTED_DATE>="+MainForm.DateTimeToQueryStr(start)+" \n"+
                "and INSPECTED_DATE<=" + MainForm.DateTimeToQueryStr(finish);

            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            if (MainForm.DS.Tables.Contains("AUDITS"))
                MainForm.DS.Tables["AUDITS"].Clear();

            int recs = adapter.Fill(MainForm.DS, "AUDITS");

            BS.DataSource = MainForm.DS;
            BS.DataMember = "AUDITS";

            adgvAudits.DataSource = BS;

            //if (recs > 0)
            {
                adgvAudits.AutoGenerateColumns = true;

                adgvAudits.Columns["AUDIT_ID"].Visible = false;

                adgvAudits.Columns["VESSEL_NAME"].HeaderText = "Vessel";
                adgvAudits.Columns["VESSEL_NAME"].FillWeight = 20;

                adgvAudits.Columns["FLEET_TEAM"].HeaderText = "Fleet";
                adgvAudits.Columns["FLEET_TEAM"].FillWeight = 10;

                adgvAudits.Columns["GRADE"].HeaderText = "Grade";
                adgvAudits.Columns["GRADE"].FillWeight = 20;

                adgvAudits.Columns["TITLE"].HeaderText = "Audit type";
                adgvAudits.Columns["TITLE"].FillWeight = 20;

                adgvAudits.Columns["INSPECTED_DATE"].HeaderText = "Date";
                adgvAudits.Columns["INSPECTED_DATE"].FillWeight = 10;

                adgvAudits.Columns["LEAD_AUDITOR"].HeaderText = "Auditor";
                adgvAudits.Columns["LEAD_AUDITOR"].FillWeight = 20;

                adgvAudits.Columns["DESCRIPTIONS"].HeaderText = "Description";
                adgvAudits.Columns["DESCRIPTIONS"].FillWeight = 60;
            }

            tssLabelRecCount.Text = adgvAudits.Rows.Count.ToString();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                if (File.Exists(openFileDialog1.FileName))
                    LoadAuditsFile(openFileDialog1.FileName);
            }
        }

        private void LoadAuditsFile(string fileName)
        {
            this.Cursor = Cursors.WaitCursor;

            bool importTerminated = false;

            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

            excelApp.Visible = false;

            Workbook workbook = excelApp.Workbooks.Open(fileName);

            //select the first sheet        
            Worksheet worksheet = (Worksheet)workbook.Worksheets[1];

            //find the used range in worksheet
            Range excelRange = worksheet.UsedRange;

            object[,] valueArray = excelRange.Value2;

            int rows = excelRange.Rows.Count;
            int cols = excelRange.Columns.Count;

            //clean up stuffs
            workbook.Close(false);
            Marshal.ReleaseComObject(workbook);

            excelApp.Quit();
            Marshal.FinalReleaseComObject(excelApp);

            OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

            string tempTable = "TEMP_AUDIT_" + MainForm.userName.ToUpper();

            string sqlText = 
                "create table " + tempTable + " (TEMP_ID counter Primary key, " +
                "VESSEL_NAME varchar(255), FLEET_TEAM varchar(50), GRADE varchar(50), TITLE varchar(255), " +
                "INSPECTED_DATE DateTime, LEAD_AUDITOR varchar(255), DESCRIPTIONS Memo)";

            if (!MainForm.tempTableCreate(tempTable, sqlText))
            {
                MessageBox.Show("Failed to create temporary table", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int colVessel = 1;
            int colFleet = 2;
            int colGrade = 3;
            int colTitle = 4;
            int colDate = 5;
            int colAuditor = 6;
            int colDescription = 7;

            for (int i=0; i<cols; i++)
            {
                switch(valueArray[1,i+1].ToString())
                {
                    case "vessel_name":
                        colVessel = i + 1;
                        break;
                    case "fleet_team":
                        colFleet = i + 1;
                        break;
                    case "grade":
                        colGrade = i + 1;
                        break;
                    case "title":
                        colTitle = i + 1;
                        break;
                    case "inspected_date":
                        colDate = i + 1;
                        break;
                    case "lead_auditor":
                        colAuditor = i + 1;
                        break;
                    case "description":
                        colDescription = i + 1;
                        break;
                }
            }

                FormProgress progress = new FormProgress(rows - 1);

                progress.Show();

                System.Windows.Forms.Application.DoEvents();

                int lineCounter = 0;
                string cmdText = "";

                for (int row = 2; row <= rows; row++)
                {
                    {
                        lineCounter++;

                        if (lineCounter < 10)
                        {
                            if (cmdText.Length == 0)
                            {
                                string str = "";

                                if (valueArray[row, colVessel] != null)
                                    str = valueArray[row, colVessel].ToString();

                                if (str.Length > 0)
                                {
                                    cmdText =
                                        "select TOP 1 \n" +
                                        "'" + MainForm.StrToSQLStr(valueArray[row, colVessel].ToString()) + "' as VESSEL_NAME, '" +
                                        MainForm.StrToSQLStr(valueArray[row, colFleet].ToString()) + "' as FLEET_TEAM, '" +
                                        MainForm.StrToSQLStr(valueArray[row, colGrade].ToString()) + "' as GRADE, '" +
                                        MainForm.StrToSQLStr(valueArray[row, colTitle].ToString()) + "' as TITLE, " +
                                        MainForm.DateTimeToQueryStr(DateTime.FromOADate(Convert.ToDouble(valueArray[row, colDate]))) + " as INSPECTED_DATE, '" +
                                        MainForm.StrToSQLStr(valueArray[row, colAuditor].ToString()) + "' as LEAD_AUDITOR, '" +
                                        MainForm.StrToSQLStr(valueArray[row, colDescription].ToString()) + "' as DESCRIPTIONS \n" +
                                        "from OPTIONS";
                                }
                            }
                            else
                            {
                                string str = "";
                                
                                if (valueArray[row,colVessel] != null)
                                    str= valueArray[row, colVessel].ToString();

                                if (str.Length > 0)
                                {
                                    if (cmdText.Length == 0)
                                    {
                                        cmdText = 
                                            "select TOP 1 \n" +
                                            "'" + MainForm.StrToSQLStr(valueArray[row, colVessel].ToString()) + "' as VESSEL_NAME, '" +
                                            MainForm.StrToSQLStr(valueArray[row, colFleet].ToString()) + "' as FLEET_TEAM, '" +
                                            MainForm.StrToSQLStr(valueArray[row, colGrade].ToString()) + "' as GRADE, '" +
                                            MainForm.StrToSQLStr(valueArray[row, colTitle].ToString()) + "' as TITLE, " +
                                            MainForm.DateTimeToQueryStr(DateTime.FromOADate(Convert.ToDouble(valueArray[row, colDate]))) + " as INSPECTED_DATE, '" +
                                            MainForm.StrToSQLStr(valueArray[row, colAuditor].ToString()) + "' as LEAD_AUDITOR, '" +
                                            MainForm.StrToSQLStr(valueArray[row, colDescription].ToString()) + "' as DESCRIPTIONS \n" +
                                            "from OPTIONS";
                                    }
                                    else
                                    {
                                        cmdText = cmdText + "\n union \n" +
                                            "select TOP 1 \n" +
                                            "'" + MainForm.StrToSQLStr(valueArray[row, colVessel].ToString()) + "' as VESSEL_NAME, '" +
                                            MainForm.StrToSQLStr(valueArray[row, colFleet].ToString()) + "' as FLEET_TEAM, '" +
                                            MainForm.StrToSQLStr(valueArray[row, colGrade].ToString()) + "' as GRADE, '" +
                                            MainForm.StrToSQLStr(valueArray[row, colTitle].ToString()) + "' as TITLE, " +
                                            MainForm.DateTimeToQueryStr(DateTime.FromOADate(Convert.ToDouble(valueArray[row, colDate]))) + " as INSPECTED_DATE, '" +
                                            MainForm.StrToSQLStr(valueArray[row, colAuditor].ToString()) + "' as LEAD_AUDITOR, '" +
                                            MainForm.StrToSQLStr(valueArray[row, colDescription].ToString()) + "' as DESCRIPTIONS \n" +
                                            "from OPTIONS";
                                    }
                                }
                            }
                        }
                        else
                        {
                            string str = "";

                            if (valueArray[row, colVessel] != null)
                                str = valueArray[row, colVessel].ToString();

                            if (str.Length > 0)
                            {
                                if (cmdText.Length == 0)
                                {
                                    cmdText = 
                                            "select TOP 1 \n" +
                                            "'" + MainForm.StrToSQLStr(valueArray[row, colVessel].ToString()) + "' as VESSEL_NAME, '" +
                                            MainForm.StrToSQLStr(valueArray[row, colFleet].ToString()) + "' as FLEET_TEAM, '" +
                                            MainForm.StrToSQLStr(valueArray[row, colGrade].ToString()) + "' as GRADE, '" +
                                            MainForm.StrToSQLStr(valueArray[row, colTitle].ToString()) + "' as TITLE, " +
                                            MainForm.DateTimeToQueryStr(DateTime.FromOADate(Convert.ToDouble(valueArray[row, colDate]))) + " as INSPECTED_DATE, '" +
                                            MainForm.StrToSQLStr(valueArray[row, colAuditor].ToString()) + "' as LEAD_AUDITOR, '" +
                                            MainForm.StrToSQLStr(valueArray[row, colDescription].ToString()) + "' as DESCRIPTIONS \n" +
                                            "from OPTIONS";
                                }
                                else
                                {
                                    cmdText = cmdText + "\n union \n" +
                                            "select TOP 1 \n" +
                                            "'" + MainForm.StrToSQLStr(valueArray[row, colVessel].ToString()) + "' as VESSEL_NAME, '" +
                                            MainForm.StrToSQLStr(valueArray[row, colFleet].ToString()) + "' as FLEET_TEAM, '" +
                                            MainForm.StrToSQLStr(valueArray[row, colGrade].ToString()) + "' as GRADE, '" +
                                            MainForm.StrToSQLStr(valueArray[row, colTitle].ToString()) + "' as TITLE, " +
                                            MainForm.DateTimeToQueryStr(DateTime.FromOADate(Convert.ToDouble(valueArray[row, colDate]))) + " as INSPECTED_DATE, '" +
                                            MainForm.StrToSQLStr(valueArray[row, colAuditor].ToString()) + "' as LEAD_AUDITOR, '" +
                                            MainForm.StrToSQLStr(valueArray[row, colDescription].ToString()) + "' as DESCRIPTIONS \n" +
                                            "from OPTIONS";
                                }
                            }

                            if (cmdText.Length > 0)
                            {
                                cmd.CommandText =
                                "insert into " + tempTable + " (VESSEL_NAME, FLEET_TEAM, GRADE, TITLE, INSPECTED_DATE, LEAD_AUDITOR, DESCRIPTIONS) \n" +
                                "select * from (\n" +
                                cmdText + "\n)";

                                MainForm.cmdExecute(cmd);
                            }

                            progress.position = row - 1;

                            System.Windows.Forms.Application.DoEvents();

                            lineCounter = 0;
                            cmdText = "";

                            if (progress.terminate)
                            {
                                importTerminated = true;
                                progress.Close();
                                //blockButtons = false;
                                break;
                            }

                        }

                    }
                }

                if (lineCounter > 0)
                {
                    if (cmdText.Length > 0)
                    {
                        cmd.CommandText =
                                "insert into " + tempTable + " (VESSEL_NAME, FLEET_TEAM, GRADE, TITLE, INSPECTED_DATE, LEAD_AUDITOR, DESCRIPTIONS) \n" +
                                "select * from (\n" +
                                cmdText + "\n)";

                        MainForm.cmdExecute(cmd);
                    }

                    progress.position = rows - 1;

                    System.Windows.Forms.Application.DoEvents();

                    lineCounter = 0;

                    if (progress.terminate)
                    {
                        importTerminated = true;
                        progress.Close();
                        //blockButtons = false;
                    }
                }

                if (!importTerminated)
                {
                    progress.Close();

                    cmd.CommandText =
                        "select MIN(INSPECTED_DATE) as MIN_DATE, MAX(INSPECTED_DATE) as MAX_DATE \n" +
                        "from [" + tempTable + "]";

                    OleDbDataReader reader = cmd.ExecuteReader();

                    DateTime MinDate = DateTime.MinValue;
                    DateTime MaxDate = DateTime.MinValue;

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            MinDate = Convert.ToDateTime(reader["MIN_DATE"]);
                            MaxDate = Convert.ToDateTime(reader["MAX_DATE"]);
                        }
                    }

                    reader.Close();

                    if (MinDate > DateTime.MinValue && MaxDate > DateTime.MinValue)
                    {
                        cmd.CommandText =
                            "delete from AUDITS \n" +
                            "where \n" +
                            "INSPECTED_DATE >=" + MainForm.DateTimeToQueryStr(MinDate) + " \n" +
                            "and INSPECTED_DATE<=" + MainForm.DateTimeToQueryStr(MaxDate);

                        MainForm.cmdExecute(cmd);

                    }

                    cmd.CommandText =
                        "insert into AUDITS(VESSEL_NAME, FLEET_TEAM, GRADE, TITLE, INSPECTED_DATE, LEAD_AUDITOR, DESCRIPTIONS) \n" +
                        "select Q.VESSEL_NAME, Q.FLEET_TEAM, Q.GRADE, Q.TITLE, Q.INSPECTED_DATE, Q.LEAD_AUDITOR, Q.DESCRIPTIONS \n" +
                        "from " + tempTable + " as Q left join AUDITS as C \n" +
                        "on Q.VESSEL_NAME=C.VESSEL_NAME \n" +
                        "and Q.FLEET_TEAM=C.FLEET_TEAM \n" +
                        "and Q.GRADE=C.GRADE \n" +
                        "and Q.TITLE=C.TITLE \n" +
                        "and Q.INSPECTED_DATE=C.INSPECTED_DATE \n" +
                        "and Q.LEAD_AUDITOR=C.LEAD_AUDITOR \n"+
                        "and Left(Q.DESCRIPTIONS,255)=Left(C.DESCRIPTIONS,255) \n"+
                        "where \n" +
                        "C.VESSEL_NAME is null \n" +
                        "and Q.INSPECTED_DATE>DateSerial(2005,1,1)";

                    MainForm.cmdExecute(cmd);

                    //MainForm.UpdateVesselID4CrewOnBoard();

                    UpdateAudits();

                }

                MainForm.tempTableDrop(tempTable);

                this.Cursor = Cursors.Default;


            
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            FormPeriodSelector form = new FormPeriodSelector();

            form.startDate = startDate;
            form.finishDate = finishDate;
            form.selector = selector;
            form.quater = quater;
            form.quaterYear = quaterYear;

            if (form.ShowDialog()==DialogResult.OK)
            {
                selector = form.selector;
                quater=form.quater;
                quaterYear = form.quaterYear;
                startDate = form.startDate;
                finishDate = form.finishDate;

                if (selector == 1)
                    tstbPeriod.Text = quaterYear.ToString()+", Quater " + quater.ToString();
                else
                    tstbPeriod.Text = startDate.ToShortDateString() + " - " + finishDate.ToShortDateString();

                switch (tabControl1.SelectedIndex)
                {
                    case 0:
                        UpdateAudits();
                        break;
                    case 1:
                        UpdateAuditReports();
                        break;
                    case 2:
                        UpdateReportsAudit();
                        break;
                }
            }
        }

        private void UpdateAuditReports()
        {
            DateTime start = startDate;
            DateTime finish = finishDate;

            if (selector==1)
            {
                DateTime now=DateTime.Now;

                switch (quater)
                {
                    case 1:
                        start = new DateTime(quaterYear, 1, 1);
                        finish = new DateTime(quaterYear, 3, 31);
                        break;
                    case 2:
                        start = new DateTime(quaterYear, 4, 1);
                        finish = new DateTime(quaterYear, 6, 30);
                        break;
                    case 3:
                        start = new DateTime(quaterYear, 7, 1);
                        finish = new DateTime(quaterYear, 9, 30);
                        break;
                    case 4:
                        start = new DateTime(quaterYear, 10, 1);
                        finish = new DateTime(quaterYear, 12, 31);
                        break;
                }
            }

            OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

            cmd.CommandText =
                "select Q1.VESSEL_NAME, Q1.TITLE, Q1.LEAD_AUDITOR, Q1.INSPECTED_DATE, Q2.INSPECTION_DATE \n" +
                "from \n" +
                "( \n" +
                "select DISTINCT VESSEL_NAME, TITLE, INSPECTED_DATE, LEAD_AUDITOR \n" +
                "from AUDITS \n" +
                "where \n" +
                "INSPECTED_DATE>=" + MainForm.DateTimeToQueryStr(start) + " \n" +
                "and INSPECTED_DATE<=" + MainForm.DateTimeToQueryStr(finish) + " \n" +
                ") as Q1 \n" +
                "left join \n" +
                "( \n" +
                "select VESSEL_NAME, INSPECTION_DATE \n" +
                "from REPORTS inner join VESSELS \n" +
                "on REPORTS.VESSEL_GUID=VESSELS.VESSEL_GUID \n" +
                "where \n" +
                "INSPECTION_DATE>=" + MainForm.DateTimeToQueryStr(start) + " \n" +
                "and INSPECTION_DATE<=" + MainForm.DateTimeToQueryStr(finish) + " \n" +
                "and OBS_COUNT>0 \n"+
                ") as Q2 \n" +
                "on Q1.VESSEL_NAME=Q2.VESSEL_NAME \n" +
                "where \n" +
                "Q2.INSPECTION_DATE>Q1.INSPECTED_DATE \n" +
                "and Q2.INSPECTION_DATE-Q1.INSPECTED_DATE<=30 \n" +
                "order by Q1.VESSEL_NAME, Q1.INSPECTED_DATE, Q1.LEAD_AUDITOR";

            if (MainForm.DS.Tables.Contains("AUDIT_REPORTS"))
                MainForm.DS.Tables["AUDIT_REPORTS"].Clear();

            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            adapter.Fill(MainForm.DS, "AUDIT_REPORTS");

            xBS.DataSource = MainForm.DS;
            xBS.DataMember = "AUDIT_REPORTS";

            adgvAuditReports.DataSource = xBS;

            adgvAuditReports.Columns["VESSEL_NAME"].HeaderText = "Vessel";
            adgvAuditReports.Columns["VESSEL_NAME"].FillWeight = 20;

            adgvAuditReports.Columns["TITLE"].HeaderText = "Audit type";
            adgvAuditReports.Columns["TITLE"].FillWeight = 20;

            adgvAuditReports.Columns["LEAD_AUDITOR"].HeaderText = "Auditor";
            adgvAuditReports.Columns["LEAD_AUDITOR"].FillWeight = 30;

            adgvAuditReports.Columns["INSPECTED_DATE"].HeaderText = "Audit date";
            adgvAuditReports.Columns["INSPECTED_DATE"].FillWeight = 15;

            adgvAuditReports.Columns["INSPECTION_DATE"].HeaderText = "Inspection date";
            adgvAuditReports.Columns["INSPECTION_DATE"].FillWeight = 15;

            tsslAuditCount.Text = adgvAuditReports.Rows.Count.ToString();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
           
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    UpdateAudits();
                    break;
                case 1:
                    UpdateAuditReports();
                    break;
                case 2:
                    UpdateReportsAudit();
                    break;
            }
        }

        private void adgvAuditReports_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FormAuditReport form = new FormAuditReport();

            form.vesselName = adgvAuditReports.CurrentRow.Cells["VESSEL_NAME"].Value.ToString();
            form.auditDate = Convert.ToDateTime(adgvAuditReports.CurrentRow.Cells["INSPECTED_DATE"].Value);
            form.auditType = adgvAuditReports.CurrentRow.Cells["TITLE"].Value.ToString();
            form.auditor = adgvAuditReports.CurrentRow.Cells["LEAD_AUDITOR"].Value.ToString();

            form.ShowDialog();
        }

        private void UpdateReportsAudit()
        {
            DateTime start = startDate;
            DateTime finish = finishDate;

            if (selector == 1)
            {
                DateTime now = DateTime.Now;

                switch (quater)
                {
                    case 1:
                        start = new DateTime(quaterYear, 1, 1);
                        finish = new DateTime(quaterYear, 3, 31);
                        break;
                    case 2:
                        start = new DateTime(quaterYear, 4, 1);
                        finish = new DateTime(quaterYear, 6, 30);
                        break;
                    case 3:
                        start = new DateTime(quaterYear, 7, 1);
                        finish = new DateTime(quaterYear, 9, 30);
                        break;
                    case 4:
                        start = new DateTime(quaterYear, 10, 1);
                        finish = new DateTime(quaterYear, 12, 31);
                        break;
                }
            }

            OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

            cmd.CommandText =
                "select VESSEL_NAME, REPORT_CODE, OBS_COUNT, INSPECTION_DATE, COUNT(INSPECTED_DATE) as AUDITS \n" +
                "from \n" +
                "( \n" +
                "select Q2.VESSEL_NAME, Q2.REPORT_CODE, Q2.OBS_COUNT, Q2.INSPECTION_DATE, Q1.INSPECTED_DATE \n" +
                "from \n" +
                "( \n" +
                "select VESSEL_NAME, INSPECTION_DATE, REPORT_CODE, OBS_COUNT \n" +
                "from REPORTS inner join VESSELS \n" +
                "on REPORTS.VESSEL_GUID=VESSELS.VESSEL_GUID \n" +
                "where \n" +
                "INSPECTION_DATE>=" + MainForm.DateTimeToQueryStr(start) + " \n" +
                "and INSPECTION_DATE<=" + MainForm.DateTimeToQueryStr(finish) + " \n" +
                "and REPORTS.OBS_COUNT>0 \n" +
                ") as Q2 \n" +
                "left join \n" +
                "( \n" +
                "select DISTINCT VESSEL_NAME, INSPECTED_DATE \n" +
                "from AUDITS \n" +
                ") as Q1 \n" +
                "on Q1.VESSEL_NAME=Q2.VESSEL_NAME \n" +
                "where \n" +
                "Q2.INSPECTION_DATE>Q1.INSPECTED_DATE \n" +
                "and Q2.INSPECTION_DATE-Q1.INSPECTED_DATE<=30 \n" +
                "order by Q2.VESSEL_NAME, Q2.INSPECTION_DATE, Q1.INSPECTED_DATE \n" +
                ") \n" +
                "group by VESSEL_NAME, REPORT_CODE, OBS_COUNT, INSPECTION_DATE";

            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            if (MainForm.DS.Tables.Contains("REPORT_AUDITS"))
                MainForm.DS.Tables["REPORT_AUDITS"].Clear();

            adapter.Fill(MainForm.DS, "REPORT_AUDITS");

            yBS.DataSource = MainForm.DS;
            yBS.DataMember = "REPORT_AUDITS";

            adgvReporAudits.DataSource = yBS;

            adgvReporAudits.Columns["VESSEL_NAME"].HeaderText = "Vessel";
            adgvReporAudits.Columns["VESSEL_NAME"].FillWeight = 20;

            adgvReporAudits.Columns["REPORT_CODE"].HeaderText = "Report code";
            adgvReporAudits.Columns["REPORT_CODE"].FillWeight = 20;

            adgvReporAudits.Columns["OBS_COUNT"].HeaderText = "Number of observations";
            adgvReporAudits.Columns["OBS_COUNT"].FillWeight = 15;

            adgvReporAudits.Columns["INSPECTION_DATE"].HeaderText = "Date of inspection";
            adgvReporAudits.Columns["INSPECTION_DATE"].FillWeight = 15;

            adgvReporAudits.Columns["AUDITS"].HeaderText = "Number of audits";
            adgvReporAudits.Columns["AUDITS"].FillWeight = 15;

            tsslInspCount.Text = adgvReporAudits.Rows.Count.ToString();
        }

        private void adgvReporAudits_SortStringChanged(object sender, EventArgs e)
        {
            yBS.Sort = adgvReporAudits.SortString;
        }

        private void adgvReporAudits_FilterStringChanged(object sender, EventArgs e)
        {
            yBS.Filter = adgvReporAudits.FilterString;
        }

        private void adgvReporAudits_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FormReportAudits form = new FormReportAudits();

            form.vesselName = adgvReporAudits.CurrentRow.Cells["VESSEL_NAME"].Value.ToString();
            form.inspectionDate = Convert.ToDateTime(adgvReporAudits.CurrentRow.Cells["INSPECTION_DATE"].Value);
            //form.auditDate = Convert.ToDateTime(adgvReporAudits.CurrentRow.Cells["INSPECTED_DATE"].Value);
            form.reportCode = adgvReporAudits.CurrentRow.Cells["REPORT_CODE"].Value.ToString();

            form.ShowDialog();
        }

        private void adgvAudits_FilterStringChanged(object sender, EventArgs e)
        {
            BS.Filter = adgvAudits.FilterString;
        }

        private void adgvAudits_SortStringChanged(object sender, EventArgs e)
        {
            BS.Sort = adgvAudits.SortString;
        }

        private void adgvAuditReports_FilterStringChanged(object sender, EventArgs e)
        {
            xBS.Filter = adgvAuditReports.FilterString;
        }

        private void adgvAuditReports_SortStringChanged(object sender, EventArgs e)
        {
            xBS.Sort = adgvAuditReports.SortString;
        }
    }
}
