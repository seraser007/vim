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
    public partial class FormAuditReport : Form
    {
        private string _vesselName = "";
        private DateTime _auditDate;

        public string vesselName
        {
            set { _vesselName = value; tbVesselName.Text = _vesselName; }
        }

        public DateTime auditDate
        {
            set { _auditDate = value; tbDate.Text = _auditDate.ToShortDateString(); }
        }

        public string auditType
        {
            set { tbAuditType.Text = value; }
        }

        public string auditor
        {
            set { tbAuditor.Text = value; }
        }

        public FormAuditReport()
        {
            this.Icon = MainForm.mainIcon;
            this.Font = MainForm.mainFont;

            InitializeComponent();
        }

        private void FillAuditObs()
        {
            OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

            cmd.CommandText =
                "select DESCRIPTIONS \n" +
                "from AUDITS \n" +
                "where \n" +
                "INSPECTED_DATE=" + MainForm.DateTimeToQueryStr(_auditDate) + " \n" +
                "and VESSEL_NAME='" + MainForm.StrToSQLStr(_vesselName) + "' \n" +
                "and TITLE='" + MainForm.StrToSQLStr(tbAuditType.Text) + "' \n" +
                "and LEAD_AUDITOR='" + MainForm.StrToSQLStr(tbAuditor.Text) + "'";

            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            if (MainForm.DS.Tables.Contains("AUDIT_OBS"))
                MainForm.DS.Tables["AUDIT_OBS"].Clear();

            adapter.Fill(MainForm.DS, "AUDIT_OBS");

            dgvAuditObs.DataSource = MainForm.DS;
            dgvAuditObs.DataMember = "AUDIT_OBS";

            dgvAuditObs.Columns["DESCRIPTIONS"].HeaderText = "Observations";
        }

        private void FillInspectionObs()
        {
            OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

            cmd.CommandText =
                "select REPORTS.REPORT_CODE, INSPECTION_DATE, QUESTION_NUMBER, OBSERVATION \n" +
                "from \n" +
                "(REPORTS inner join QUESTIONS \n" +
                "on REPORTS.REPORT_CODE=QUESTIONS.REPORT_CODE) \n" +
                "left join VESSELS \n" +
                "on REPORTS.VESSEL_GUID=VESSELS.VESSEL_GUID \n" +
                "where \n" +
                "VESSELS.VESSEL_NAME='" + MainForm.StrToSQLStr(_vesselName) + "' \n" +
                "and REPORTS.INSPECTION_DATE>" + MainForm.DateTimeToQueryStr(_auditDate) + " \n" +
                "and REPORTS.INSPECTION_DATE<=" + MainForm.DateTimeToQueryStr(_auditDate.AddDays(30)) + " \n" +
                "and REPORTS.OBS_COUNT>0 \n" +
                "and LEN(QUESTIONS.OBSERVATION)>0 \n" +
                "order by INSPECTION_DATE, QSEQUENCE";

            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            if (MainForm.DS.Tables.Contains("INSPECTION_OBS"))
                MainForm.DS.Tables["INSPECTION_OBS"].Clear();

            adapter.Fill(MainForm.DS, "INSPECTION_OBS");

            dgvInspectionsObs.DataSource = MainForm.DS;
            dgvInspectionsObs.DataMember = "INSPECTION_OBS";

            dgvInspectionsObs.Columns["REPORT_CODE"].HeaderText = "Report code";
            dgvInspectionsObs.Columns["REPORT_CODE"].FillWeight = 20;
            dgvInspectionsObs.Columns["REPORT_CODE"].Visible = false;

            dgvInspectionsObs.Columns["INSPECTION_DATE"].HeaderText = "Date";
            dgvInspectionsObs.Columns["INSPECTION_DATE"].FillWeight = 15;

            dgvInspectionsObs.Columns["QUESTION_NUMBER"].HeaderText = "No.";
            dgvInspectionsObs.Columns["QUESTION_NUMBER"].FillWeight = 10;

            dgvInspectionsObs.Columns["OBSERVATION"].HeaderText = "Observations";
            dgvInspectionsObs.Columns["OBSERVATION"].FillWeight = 65;
        }

        private void FormAuditReport_Shown(object sender, EventArgs e)
        {
            FillAuditObs();
            FillInspectionObs();
        }
    }
}
