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
    public partial class VesselForm : Form
    {
        OleDbConnection connection;
        DataSet DS;

        public string vesselEmail
        {
            get { return tbEmail.Text.Trim(); }
            set { tbEmail.Text = value; }
        }

        public string vesselName
        {
            get { return tbVesselName.Text.Trim(); }
            set { tbVesselName.Text = value; }
        }

        public string vesselIMO
        {
            get { return tbImoNumber.Text.Trim(); }
            set { tbImoNumber.Text = value; }
        }

        public string vesselOffice
        {
            get { return cbOffice.Text; }
            set { cbOffice.Text = value; }
        }

        public string vesselDOC
        {
            get { return cbDOC.Text; }
            set { cbDOC.Text = value; }
        }

        public string vesselHullClass
        {
            get { return cbHullClass.Text; }
            set { cbHullClass.Text = value; }
        }

        public string vesselNotes
        {
            get { return tbNotes.Text.Trim(); }
            set { tbNotes.Text = value; }
        }

        public bool hidden
        {
            get { if (cbHidden.Text == "Yes") return true; else return false; }
            set { if (value) cbHidden.Text = "Yes"; else cbHidden.Text = "No"; }
        }

        public string vesselFleet
        {
            get { return cbFleet.Text.Trim(); }
            set { cbFleet.Text = value; }
        }

        public Guid vesselGuid = MainForm.zeroGuid;

        public bool dataChanged = false;

        public VesselForm(OleDbConnection mainConnection, DataSet mainDS, Font mainFont, Icon mainIcon, string vesselIMO, string vesselName)
        {
            connection = mainConnection;
            DS = mainDS;

            this.Font = mainFont;
            this.Icon = mainIcon;

            InitializeComponent();

            FillLists();

            ProtectFields(MainForm.isPowerUser);
        }

        private void FillLists()
        {
            FillOffices();
            FillHullClasses();
            FillFleets();
            FillDOCs();
        }

        private void ProtectFields(bool status)
        {
            tbVesselName.ReadOnly = !status;
            tbImoNumber.ReadOnly = !status;
            tbNotes.ReadOnly = !status;

            cbHullClass.Enabled = status;
            cbOffice.Enabled = status;
        }

        private void fillInspections(string vesselIMO, string vesselName)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select REPORTS.REPORT_CODE, INSPECTORS.INSPECTOR_NAME, INSPECTION_DATE, INSPECTION_PORT, Q1.OBS_NUMBER \n" +
                "from (REPORTS left join\n" +
                "(select REPORT_CODE, COUNT(OBSERVATION) as OBS_NUMBER \n" +
                "from REPORT_ITEMS \n" +
                "where LEN(OBSERVATION)>0 \n" +
                "group by REPORT_CODE) as Q1 \n" +
                "on REPORTS.REPORT_CODE=Q1.REPORT_CODE) \n" +
                "left join INSPECTORS \n" +
                "on REPORTS.INSPECTOR_GUID=INSPECTORS.INSPECTOR_GUID \n" +
                "where REPORTS.VESSEL_GUID=" + MainForm.GuidToStr(vesselGuid) + "\n" +
                "order by INSPECTION_DATE";

            OleDbDataAdapter inspections = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("VESSEL_INSPECTIONS"))
                DS.Tables["VESSEL_INSPECTIONS"].Clear();

            inspections.Fill(DS, "VESSEL_INSPECTIONS");

            dgvInspections.DataSource = DS;
            dgvInspections.DataMember = "VESSEL_INSPECTIONS";
            dgvInspections.AutoGenerateColumns = true;

            dgvInspections.Columns["REPORT_CODE"].HeaderText = "Report code";
            dgvInspections.Columns["INSPECTOR_NAME"].HeaderText = "Inspector";
            dgvInspections.Columns["INSPECTION_DATE"].HeaderText = "Date of inspection";
            dgvInspections.Columns["INSPECTION_PORT"].HeaderText = "Port of inspection";
            dgvInspections.Columns["OBS_NUMBER"].HeaderText = "Observations";
        }

        private void VesselForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
                return;

            //Check vessel exists


            //Check vessel name
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select VESSEL_IMO, VESSEL_NAME \n" +
                "from VESSELS \n" +
                "where VESSEL_NAME = '" + MainForm.StrToSQLStr(tbVesselName.Text) + "' \n" +
                "and VESSEL_GUID <> "+MainForm.GuidToStr(vesselGuid)+"\n"+
                "order by VESSEL_NAME";

            OleDbDataReader reader = cmd.ExecuteReader();

            int i = 0;

            if (reader.HasRows)
            {
                string vsls = "";

                while (reader.Read())
                {
                    if (vsls.Length==0)
                    {
                        vsls = reader["VESSEL_NAME"].ToString() + " IMO No." + reader["VESSEL_IMO"].ToString();
                    }
                    else
                    {
                        vsls = vsls + "\n" + reader["VESSEL_NAME"].ToString() + " IMO No." + reader["VESSEL_IMO"].ToString();
                    }

                    i++;
                }

                string txt = "";

                if (i == 1)
                    txt = "There is a vessel with the same name:\n\n" +
                        vsls + "\n\n" +
                        "Would you like to use this name anyway.";
                else
                    txt = "There are vessels with the same name:\n\n" +
                        vsls + "\n\n" +
                        "Would you like to use this name anyway.";

                if (MessageBox.Show(txt, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }
            }

            reader.Close();

            cmd.CommandText=
                "select VESSEL_IMO, VESSEL_NAME \n" +
                "from VESSELS \n" +
                "where VESSEL_IMO = '" + MainForm.StrToSQLStr(tbImoNumber.Text) + "' \n" +
                "and VESSEL_GUID <> " + MainForm.GuidToStr(vesselGuid) + "\n" +
                "order by VESSEL_NAME";

            reader = cmd.ExecuteReader();

            i = 0;

            if (reader.HasRows)
            {
                string vsls = "";

                while (reader.Read())
                {
                    if (vsls.Length == 0)
                    {
                        vsls = reader["VESSEL_NAME"].ToString() + " IMO No." + reader["VESSEL_IMO"].ToString();
                    }
                    else
                    {
                        vsls = vsls + "\n" + reader["VESSEL_NAME"].ToString() + " IMO No." + reader["VESSEL_IMO"].ToString();
                    }

                    i++;
                }

                string txt = "";

                if (i == 1)
                    txt = "There is a vessel with the same IMO number:\n\n" +
                        vsls + "\n\n" +
                        "Would you like to use this IMO number anyway.";
                else
                    txt = "There are vessels with the same IMO number:\n\n" +
                        vsls + "\n\n" +
                        "Would you like to use this IMO number anyway.";

                if (MessageBox.Show(txt, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }
            }

            reader.Close();

        }

        private void btnShowFleets_Click(object sender, EventArgs e)
        {
            FrmFleets form = new FrmFleets(connection, DS, this.Font, this.Icon);

            form.ShowDialog();

            if (form.dataChanged)
            {
                dataChanged = true;
                FillFleets();
                CheckFleetExists();
            }
        }

        private void VesselForm_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tbVesselName_Validating(object sender, CancelEventArgs e)
        {
            fillInspections(tbImoNumber.Text, tbVesselName.Text);
        }

        private void tbImoNumber_Validating(object sender, CancelEventArgs e)
        {
            fillInspections(tbImoNumber.Text, tbVesselName.Text);
        }

        private void FillOffices()
        {
            //Заполяем список оффисов
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select OFFICE_ID \n" +
                "from OFFICES \n" +
                "order by OFFICE_ID";

            OleDbDataReader reader = cmd.ExecuteReader();

            cbOffice.Items.Clear();
            cbOffice.Items.Add("");

            while (reader.Read())
                cbOffice.Items.Add(reader["OFFICE_ID"].ToString());

            reader.Close();
        }

        private void FillHullClasses()
        {
            //Заполняем список классов
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select DISTINCT Trim(IIF(IsNull(HULL_CLASS),'',HULL_CLASS)) as HULL_CLASS from VESSELS";

            OleDbDataReader reader = cmd.ExecuteReader();

            cbHullClass.Items.Clear();

            while (reader.Read())
                cbHullClass.Items.Add(reader[0].ToString());

            reader.Close();
        }

        private void FillFleets()
        {
            //Заполняем список флотов
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select FLEET_ID \n" +
                "from FLEETS \n" +
                "order by FLEET_ID";

            OleDbDataReader reader = cmd.ExecuteReader();

            cbFleet.Items.Clear();
            cbFleet.Items.Add("");

            while (reader.Read())
            {
                cbFleet.Items.Add(reader["FLEET_ID"].ToString());
            }

            reader.Close();

        }

        private void btnOffice_Click(object sender, EventArgs e)
        {
            FrmOffices form = new FrmOffices();

            form.ShowDialog();

            if (form.dataChanged)
            {
                dataChanged = true;

                FillOffices();
                CheckOfficeExists();
            }

        }

        private void CheckFleetExists()
        {
            string fleet = cbFleet.Text;

            if (!cbFleet.Items.Contains(fleet))
                cbFleet.Text = "";
        }

        private void CheckOfficeExists()
        {
            string office = cbOffice.Text;

            if (!cbOffice.Items.Contains(office))
                cbOffice.Text = "";
        }

        private void btnDOC_Click(object sender, EventArgs e)
        {
            FrmDOCs form = new FrmDOCs();

            form.ShowDialog();

            if (form.dataChanged)
            {
                dataChanged = true;

                FillDOCs();
                CheckDOCExists();

            }
        }

        private void CheckDOCExists()
        {
            if (!cbDOC.Items.Contains(cbDOC.Text))
                cbDOC.Text = "";
        }

        private void FillDOCs()
        {
            cbDOC.Items.Clear();
            cbDOC.Items.Add("");

            OleDbCommand cmd = new OleDbCommand("", connection);
            
            cmd.CommandText =
                "select DOC_ID \n" +
                "from DOCS \n" +
                "order by DOC_ID";

            OleDbDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cbDOC.Items.Add(reader["DOC_ID"].ToString());
                }
            }
        }

        private void dgvInspections_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Show inspection
            if (dgvInspections.Rows.Count == 0)
                return;

            this.Cursor = Cursors.WaitCursor;

            string reportCode = dgvInspections.CurrentRow.Cells["REPORT_CODE"].Value.ToString();
            FormReportSummary form = new FormReportSummary(false, reportCode, true, true);

            this.Cursor = Cursors.Default;

            form.ShowDialog();
        }

        private void VesselForm_Shown(object sender, EventArgs e)
        {
            fillInspections(vesselIMO, vesselName);
        }
     }
}
