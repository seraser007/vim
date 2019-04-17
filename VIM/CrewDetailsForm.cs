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
    public partial class CrewDetailsForm : Form
    {
        OleDbConnection connection;
        DataSet DS;
        private Guid _positionGuid;
        private Guid _crewGuid = MainForm.zeroGuid;
        private bool _wasChanged = false;
        private string _name = "";
        private string _personalId = "";
        private string _notes = "";

        public Guid crewGuid
        {
            set { _crewGuid = value; FillDetails(); }
            get { return _crewGuid; }
        }

        public string crewName
        {
            get { return tbName.Text.Trim(); }
        }

        public bool wasChanged
        {
            get { return _wasChanged; }
        }

        public CrewDetailsForm()
        {
            connection = MainForm.connection;
            DS = MainForm.DS;

            this.Font = MainForm.mainFont;
            this.Icon = MainForm.mainIcon;

            InitializeComponent();

            FillPositions();

            ProtectFields(MainForm.isPowerUser);
        }

        private void FillPositions()
        {
            OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

            cmd.CommandText =
                "select POSITION_NAME, CREW_POSITION_GUID \n" +
                "from CREW_POSITIONS \n" +
                "order by POSITION_NAME";

            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            if (MainForm.DS.Tables.Contains("CREW_POSITIONS"))
                MainForm.DS.Tables["CREW_POSITIONS"].Clear();

            adapter.Fill(MainForm.DS, "CREW_POSITIONS");

            cbPosition.DataSource = MainForm.DS.Tables["CREW_POSITIONS"];
            cbPosition.DisplayMember = "POSITION_NAME";
            cbPosition.ValueMember = "CREW_POSITION_GUID";
        }

        private void FillDetails()
        {
            if (_crewGuid!=MainForm.zeroGuid)
            {
                OleDbCommand cmd = new OleDbCommand("", connection);

                cmd.CommandText =
                    "select * \n" +
                    "from CREW \n" +
                    "where \n" +
                    "CREW_GUID=" + MainForm.GuidToStr(_crewGuid);

                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        _name = reader["CREW_NAME"].ToString();
                        tbName.Text = _name;

                        _personalId = reader["PERSONAL_ID"].ToString();
                        tbPersonalID.Text = _personalId;

                        _positionGuid = MainForm.StrToGuid(reader["CREW_POSITION_GUID"].ToString());
                        cbPosition.SelectedValue = _positionGuid;

                        _notes= reader["CREW_NOTES"].ToString();
                        tbNotes.Text = _notes;

                        FillInspections();
                    }
                }
            }
        }

        private void FillInspections()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select REPORTS.REPORT_CODE, VESSELS.VESSEL_NAME, INSPECTION_DATE, INSPECTION_PORT, Q1.OBS_NUMBER \n" +
                "from ((REPORTS left join\n" +
                "(select REPORT_CODE, COUNT(OBSERVATION) as OBS_NUMBER \n" +
                "from REPORT_ITEMS \n" +
                "where LEN(OBSERVATION)>0 \n" +
                "group by REPORT_CODE) as Q1 \n" +
                "on REPORTS.REPORT_CODE=Q1.REPORT_CODE) \n" +
                "left join VESSELS \n" +
                "on REPORTS.VESSEL_GUID=VESSELS.VESSEL_GUID) \n" +
                "left join REPORTS_CREW \n"+
                "on REPORTS.REPORT_CODE=REPORTS_CREW.REPORT_CODE \n"+
                "where \n"+
                "REPORTS_CREW.CREW_GUID=" + MainForm.GuidToStr(_crewGuid) + "\n" +
                "order by INSPECTION_DATE";

            OleDbDataAdapter inspections = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("CREW_INSPECTIONS"))
                DS.Tables["CREW_INSPECTIONS"].Clear();

            inspections.Fill(DS, "CREW_INSPECTIONS");

            dgvInspections.DataSource = DS;
            dgvInspections.DataMember = "CREW_INSPECTIONS";
            dgvInspections.AutoGenerateColumns = true;

            dgvInspections.Columns["REPORT_CODE"].HeaderText = "Report code";
            dgvInspections.Columns["VESSEL_NAME"].HeaderText = "Vessel name";
            dgvInspections.Columns["INSPECTION_DATE"].HeaderText = "Date of inspection";
            dgvInspections.Columns["INSPECTION_PORT"].HeaderText = "Port of inspection";
            dgvInspections.Columns["OBS_NUMBER"].HeaderText = "Observations";

            lblRecs.Text = dgvInspections.Rows.Count.ToString();

        }

        private void ProtectFields(bool status)
        {
            tbName.ReadOnly = !status;
            tbPersonalID.ReadOnly = !status;
            tbNotes.ReadOnly = !status;
        }

        private void MasterDetailsForm_Load(object sender, EventArgs e)
        {

        }

        private void dgvInspections_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Show report form

            if (dgvInspections.Rows.Count == 0)
                return;

            this.Cursor = Cursors.WaitCursor;
            
            string reportCode = dgvInspections.CurrentRow.Cells["REPORT_CODE"].Value.ToString();

            FormReportSummary form = new FormReportSummary(false, reportCode, true, true);
            
            this.Cursor = Cursors.Default;

            form.ShowDialog();
        }

        private void CrewDetailsForm_Shown(object sender, EventArgs e)
        {
            
        }

        private void cbPosition_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbPosition.SelectedValue!=null)
                _positionGuid = MainForm.StrToGuid(cbPosition.SelectedValue.ToString());
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            FormCrewPositions form = new FormCrewPositions();

            form.ShowDialog();

            if (form.wasChanged)
                FillPositions();
        }

        private bool SaveCrew()
        {
            if (_crewGuid==MainForm.zeroGuid)
            {
                if (!checkSameName(tbName.Text, MainForm.zeroGuid))
                    return false;

                if (!checkSameID(tbPersonalID.Text, MainForm.zeroGuid))
                    return false;

                OleDbCommand cmd = new OleDbCommand("", connection);

                Guid newGuid = Guid.NewGuid();

                cmd.CommandText =
                    "insert into CREW (CREW_GUID,CREW_NAME,CREW_POSITION_GUID,PERSONAL_ID,CREW_NOTES) \n" +
                    "values(" + MainForm.GuidToStr(newGuid) + ",'" +
                        MainForm.StrToSQLStr(tbName.Text) + "'," +
                        MainForm.GuidToStr(MainForm.StrToGuid(cbPosition.SelectedValue.ToString())) + ",'" +
                        MainForm.StrToSQLStr(tbPersonalID.Text) + "','" +
                        MainForm.StrToSQLStr(tbNotes.Text) + "')";

                if (MainForm.cmdExecute(cmd) < 0)
                {
                    MessageBox.Show("Failed to create new record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    _crewGuid = newGuid;
                    return true;
                }

            }
            else
            {
                if (!checkSameName(tbName.Text, _crewGuid))
                    return false; ;

                if (!checkSameID(tbPersonalID.Text, _crewGuid))
                    return false;

                OleDbCommand cmd = new OleDbCommand("", connection);

                cmd.CommandText =
                "update CREW set \n" +
                "CREW_NAME='" + MainForm.StrToSQLStr(tbName.Text) + "', \n" +
                "CREW_POSITION_GUID=" + MainForm.GuidToStr(MainForm.StrToGuid(cbPosition.SelectedValue.ToString())) + ",\n" +
                "PERSONAL_ID='" + MainForm.StrToSQLStr(tbPersonalID.Text) + "', \n" +
                "CREW_NOTES='" + MainForm.StrToSQLStr(tbNotes.Text) + "' \n" +
                "where CREW_GUID=" + MainForm.GuidToStr(_crewGuid);

                if (MainForm.cmdExecute(cmd) < 0)
                {
                    MessageBox.Show("Failed to update record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                    return true;
            }
        }

        public bool checkSameName(string crewNameValue, Guid crewGuidValue)
        {
            //Check for records with the same MASTER_NAME
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select Count(CREW_NAME) \n" +
                "from CREW \n" +
                "where CREW_NAME like '" + MainForm.StrToSQLStr(crewNameValue) + "' \n" +
                "and CREW_GUID<>" + MainForm.GuidToStr(crewGuidValue);

            int recs = (int)cmd.ExecuteScalar();

            if (recs > 0)
            {
                //There is record with the same name
                cmd.CommandText =
                    "select CREW_NAME, PERSONAL_ID \n" +
                    "from CREW \n" +
                    "where CREW_NAME like '" + MainForm.StrToSQLStr(crewNameValue) + "' \n" +
                    "and CREW_GUID<>" + MainForm.GuidToStr(crewGuidValue);

                OleDbDataReader mReader = cmd.ExecuteReader();

                int recsReader = 0;

                string msgText = "";

                while (mReader.Read())
                {
                    if (msgText.Length == 0)
                        msgText = "Name : \"" + mReader[0].ToString() + "\"; Personal ID : \"" + mReader[1].ToString() + "\"";
                    else
                        msgText = msgText + "\n" +
                            "Name : \"" + mReader[0].ToString() + "\"; Personal ID : \"" + mReader[1].ToString() + "\"";
                    recsReader++;
                }

                mReader.Close();

                if (recsReader == 1)
                    msgText = "There is a record with the same name: \n\n" + msgText;
                else
                    msgText = "There are " + recsReader.ToString() + " records with the same name: \n\n" + msgText;

                msgText = msgText + "\n\n Would you like to save this record anyway?";

                var res1 = MessageBox.Show(msgText, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res1 == DialogResult.Yes)
                    return true;
                else
                    return false;
            }
            else
                return true;

        }

        private bool checkSameID(string personalID, Guid crewGuidValue)
        {
            //Check for records with the same Personal ID
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
            "select Count(PERSONAL_ID) \n" +
            "from CREW \n" +
            "where PERSONAL_ID like '" + MainForm.StrToSQLStr(personalID) + "' \n" +
            "and LEN(PERSONAL_ID)>0 \n" +
            "and CREW_GUID<>" + MainForm.GuidToStr(crewGuidValue);

            int recs = (int)cmd.ExecuteScalar();

            if (recs > 0)
            {
                //There is record with the same name
                cmd.CommandText =
                    "select CREW_NAME, PERSONAL_ID \n" +
                    "from CREW \n" +
                    "where PERSONAL_ID like '" + MainForm.StrToSQLStr(personalID) + "' \n" +
                    "and LEN(PERSONAL_ID)>0 \n" +
                    "and CREW_GUID<>" + MainForm.GuidToStr(crewGuidValue);

                OleDbDataReader mReader = cmd.ExecuteReader();

                int recsReader = 0;

                string msgText = "";

                while (mReader.Read())
                {
                    if (msgText.Length == 0)
                        msgText = "Name : \"" + mReader[0].ToString() + "\"; Personal ID : \"" + mReader[1].ToString() + "\"";
                    else
                        msgText = msgText + "\n" +
                            "Name : \"" + mReader[0].ToString() + "\"; Personal ID : \"" + mReader[1].ToString() + "\"";
                    recsReader++;
                }

                if (recsReader == 1)
                    msgText = "There is a record with the same Personal ID: \n\n" + msgText;
                else
                    msgText = "There are " + recsReader.ToString() + " records with the same Personal ID: \n\n" + msgText;

                msgText = msgText + "\n\n Would you like to save this record?";

                var res1 = MessageBox.Show(msgText, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res1 == DialogResult.Yes)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }

        private void CrewDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!MainForm.isPowerUser)
                return;

            if (this.DialogResult != DialogResult.OK)
                return;

            _wasChanged = _name == tbName.Text.Trim()
                && _personalId == tbPersonalID.Text.Trim()
                && _positionGuid == MainForm.StrToGuid(cbPosition.SelectedValue.ToString())
                && _notes == tbNotes.Text.Trim();

            if (_wasChanged)
            {
                if (!SaveCrew())
                {
                    e.Cancel = true;
                    return;
                }
            }
        }
    }
}
