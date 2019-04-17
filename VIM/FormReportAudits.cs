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
    public partial class FormReportAudits : Form
    {
        private DateTime _inspectionDate;
        private DateTime _auditDate;

        public string vesselName
        {
            set { tbVesselName.Text = value; }
        }
        
        public DateTime inspectionDate
        {
            set { _inspectionDate = value; tbDate.Text = _inspectionDate.ToShortDateString(); }
        }

        public DateTime auditDate
        {
            set { _auditDate = value; }
        }

        public string reportCode
        {
            set { tbReportCode.Text = value; }
        }
        public FormReportAudits()
        {
            this.Icon = MainForm.mainIcon;
            this.Font = MainForm.mainFont;

            InitializeComponent();
        }

        private void FillInspectionObs()
        {
            OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

            cmd.CommandText =
                "select QUESTION_NUMBER, OBSERVATION \n" +
                "from QUESTIONS  \n" +
                "where \n" +
                "REPORT_CODE='" + MainForm.StrToSQLStr(tbReportCode.Text) + "' \n" +
                "and LEN(OBSERVATION)>0 \n" +
                "order by QSEQUENCE";

            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            if (MainForm.DS.Tables.Contains("INSP_AUDIT_OBS"))
                MainForm.DS.Tables["INSP_AUDIT_OBS"].Clear();

            adapter.Fill(MainForm.DS, "INSP_AUDIT_OBS");

            dgvReportsObs.DataSource = MainForm.DS;
            dgvReportsObs.DataMember = "INSP_AUDIT_OBS";

            dgvReportsObs.Columns["QUESTION_NUMBER"].HeaderText = "No.";
            dgvReportsObs.Columns["QUESTION_NUMBER"].FillWeight = 10;

            dgvReportsObs.Columns["OBSERVATION"].HeaderText = "Onservation";
            dgvReportsObs.Columns["OBSERVATION"].FillWeight = 90;
        }

        private void FillAuditObs()
        {
            OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

            cmd.CommandText =
                "select INSPECTED_DATE, TITLE, DESCRIPTIONS \n" +
                "from AUDITS \n" +
                "where \n" +
                "VESSEL_NAME='"+MainForm.StrToSQLStr(tbVesselName.Text)+"' \n"+
                "and INSPECTED_DATE<" + MainForm.DateTimeToQueryStr(_inspectionDate) + " \n" +
                "and INSPECTED_DATE>=" + MainForm.DateTimeToQueryStr(_inspectionDate.AddDays(-30)) + " \n" +
                "order by INSPECTED_DATE, TITLE";

            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            if (MainForm.DS.Tables.Contains("AUDIT_INSP_OBS"))
                MainForm.DS.Tables["AUDIT_INSP_OBS"].Clear();

            adapter.Fill(MainForm.DS, "AUDIT_INSP_OBS");

            dgvAuditObs.DataSource = MainForm.DS;
            dgvAuditObs.DataMember = "AUDIT_INSP_OBS";

            dgvAuditObs.Columns["INSPECTED_DATE"].HeaderText = "Date";
            dgvAuditObs.Columns["INSPECTED_DATE"].FillWeight = 20;

            dgvAuditObs.Columns["TITLE"].HeaderText = "Audit type";
            dgvAuditObs.Columns["TITLE"].FillWeight = 20;

            dgvAuditObs.Columns["DESCRIPTIONS"].HeaderText = "Observation";
            dgvAuditObs.Columns["DESCRIPTIONS"].FillWeight = 70;
        }

        private void FormReportAudits_Shown(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            FillInspectionObs();
            FillAuditObs();

            this.Cursor = Cursors.Default;
        }
    }
}
