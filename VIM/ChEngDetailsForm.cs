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
    public partial class ChEngDetailsForm : Form
    {
        OleDbConnection connection;
        DataSet DS;

        public string chengName
        {
            get { return tbName.Text.Trim(); }
            set { tbName.Text = value; }
        }

        public string chengNotes
        {
            get { return tbNotes.Text.Trim(); }
            set { tbNotes.Text = value; }
        }

        public string chengPersonalID
        {
            get { return tbPersonalID.Text.Trim(); }
            set { tbPersonalID.Text = value; }
        }

        public ChEngDetailsForm(OleDbConnection mainConnection, DataSet mainDS, Font mainFont, Icon mainIcon, Guid chengGuid)
        {
            connection = mainConnection;
            DS = mainDS;

            this.Font = mainFont;
            this.Icon = mainIcon;

            InitializeComponent();

            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select REPORTS.REPORT_CODE, VESSELS.VESSEL_NAME, INSPECTION_DATE, INSPECTION_PORT, Q1.OBS_NUMBER, \n" +
                "COMPANY, INSPECTOR_GUID, MASTER_GUID, CHENG_GUID, VIQ_VERSION, VIQ_TYPE, TEMPLATE_GUID \n" +
                "from (REPORTS left join\n" +
                "(select REPORT_CODE, COUNT(OBSERVATION) as OBS_NUMBER \n" +
                "from REPORT_ITEMS \n" +
                "where LEN(OBSERVATION)>0 \n" +
                "group by REPORT_CODE) as Q1 \n" +
                "on REPORTS.REPORT_CODE=Q1.REPORT_CODE) \n" +
                "left join VESSELS \n"+
                "on REPORTS.VESSEL_GUID=VESSELS.VESSEL_GUID \n"+
                "where CHENG_GUID=" + MainForm.GuidToStr(chengGuid) + "\n" +
                "order by INSPECTION_DATE";

            OleDbDataAdapter inspections = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("CHENG_INSPECTIONS"))
                DS.Tables["CHENG_INSPECTIONS"].Clear();

            inspections.Fill(DS, "CHENG_INSPECTIONS");

            dgvInspections.DataSource = DS;
            dgvInspections.DataMember = "CHENG_INSPECTIONS";
            dgvInspections.AutoGenerateColumns = true;

            dgvInspections.Columns["REPORT_CODE"].HeaderText = "Report code";
            dgvInspections.Columns["VESSEL_NAME"].HeaderText = "Vessel name";
            dgvInspections.Columns["INSPECTION_DATE"].HeaderText = "Date of inspection";
            dgvInspections.Columns["INSPECTION_PORT"].HeaderText = "Port of inspection";
            dgvInspections.Columns["OBS_NUMBER"].HeaderText = "Observations";

            dgvInspections.Columns["COMPANY"].Visible = false;
            dgvInspections.Columns["INSPECTOR_GUID"].Visible = false;
            dgvInspections.Columns["MASTER_GUID"].Visible = false;
            dgvInspections.Columns["CHENG_GUID"].Visible = false;
            dgvInspections.Columns["VIQ_VERSION"].Visible = false;
            dgvInspections.Columns["VIQ_TYPE"].Visible = false;
            dgvInspections.Columns["TEMPLATE_GUID"].Visible = false;

            lblRecs.Text = dgvInspections.Rows.Count.ToString();
            
            ProtectFields(MainForm.isPowerUser);
        }

        private void ProtectFields(bool status)
        {
            tbName.ReadOnly = !status;
            tbPersonalID.ReadOnly = !status;
            tbNotes.ReadOnly = !status;
        }

        private void dgvInspections_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Show report form

            if (dgvInspections.Rows.Count == 0)
                return;

            this.Cursor = Cursors.WaitCursor;

            string reportCode=dgvInspections.CurrentRow.Cells["REPORT_CODE"].Value.ToString();

            FormReportSummary form = new FormReportSummary(false, reportCode, true, true);

            this.Cursor = Cursors.Default;

            form.ShowDialog();
        }
    }
}
