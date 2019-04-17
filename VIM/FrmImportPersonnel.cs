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
    public partial class FrmImportPersonnel : Form
    {
        private OleDbConnection connection;
        private DataSet DS;
        private bool stateInspector = false;
        private bool stateMaster = false;
        private bool stateChEng = false;
        
        public FrmImportPersonnel(OleDbConnection mainConnection, DataSet mainDS, Icon mainIcon, Font mainFont)
        {
            connection = mainConnection;
            DS = mainDS;
            this.Icon = mainIcon;
            this.Font = mainFont;

            InitializeComponent();

            dgvInspections.DataSource = DS;
            dgvInspections.DataMember = "PERSONS_IMPORT";

            dgvInspections.Columns["REPORT_CODE"].HeaderText = "Report code";
            dgvInspections.Columns["VESSEL_NAME"].HeaderText = "Vessel";
            dgvInspections.Columns["INSPECTION_DATE"].HeaderText = "Date";
            dgvInspections.Columns["INSPECTOR_NAME"].HeaderText = "Inspector";
            dgvInspections.Columns["MASTER_NAME"].HeaderText = "Master";
            dgvInspections.Columns["CHENG_NAME"].HeaderText = "Chief Engineer";

            fillInspectors();
            fillMasters();
            fillChiefEngineers();
            fillReportPersons();
        }

        private void dgvInspections_SelectionChanged(object sender, EventArgs e)
        {
            SelectionChanged();
        }

        private void SelectionChanged()
        {
            pbInspectorSaved.Image = null;
            pbMasterSaved.Image = null;
            pbChengSaved.Image = null;

            if (dgvInspections.CurrentRow != null)
            {
                tbInspectorName.Clear();
                tbMasterName.Clear();
                tbChEngName.Clear();

                cbInspectorName.Text = "";
                cbMasterName.Text = "";
                cbChEngName.Text = "";
                
                tbInspectorName.Text = dgvInspections.CurrentRow.Cells["INSPECTOR_NAME"].Value.ToString().Trim();
                tbMasterName.Text = dgvInspections.CurrentRow.Cells["MASTER_NAME"].Value.ToString().Trim();
                tbChEngName.Text = dgvInspections.CurrentRow.Cells["CHENG_NAME"].Value.ToString().Trim();

                tbInspector.Text = "";
                tbMaster.Text = "";
                tbChEng.Text = "";

                string reportCode = dgvInspections.CurrentRow.Cells["REPORT_CODE"].Value.ToString();

                DataRow[] rows = DS.Tables["REPORT_PERSONS"].Select("REPORT_CODE='" + reportCode + "'");

                if (rows.Length > 0)
                {
                    tbInspector.Text = rows[0]["INSPECTOR_NAME"].ToString();
                    tbMaster.Text = rows[0]["MASTER_NAME"].ToString();
                    tbChEng.Text = rows[0]["CHENG_NAME"].ToString();
                }
            }
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

            cbMasterName.DataSource = DS.Tables["MASTERS_LIST"];
            cbMasterName.DisplayMember = "MASTER_NAME";
            cbMasterName.ValueMember = "MASTER_GUID";

        }

        private void fillInspectors()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select * from \n" +
                "(select INSPECTOR_GUID, INSPECTOR_NAME, Notes, Photo, Unfavourable, Background \n" +
                "from INSPECTORS \n" +
                "union \n" +
                "select TOP 1 "+MainForm.GuidToStr(MainForm.zeroGuid)+" as INSPECTOR_GUID, '' as INSPECTOR_NAME, '' as Notes, '' as Photo, " +
                "false as Unfavourable, '' as Background \n" +
                "from INSPECTORS) \n" +
                "order by INSPECTOR_NAME";

            OleDbDataAdapter inspectorsAdapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("INSPECTORS_LIST"))
                DS.Tables["INSPECTORS_LIST"].Clear();

            inspectorsAdapter.Fill(DS, "INSPECTORS_LIST");

            cbInspectorName.DataSource = DS.Tables["INSPECTORS_LIST"];
            cbInspectorName.DisplayMember = "INSPECTOR_NAME";
            cbInspectorName.ValueMember = "INSPECTOR_GUID";

        }

        private void fillChiefEngineers()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select * from \n" +
                "(select CHENG_GUID, CHENG_NAME, CHENG_NOTES, PERSONAL_ID \n" +
                "from CHIEF_ENGINEERS \n" +
                "union \n" +
                "select " + MainForm.GuidToStr(MainForm.zeroGuid) + " as CHENG_GUID, '' as CHENG_NAME, '' as CHENG_NOTES, '' as PERSONAL_ID \n" +
                "from Fonts) \n" +
                "order by CHENG_NAME";

            OleDbDataAdapter chengListAdapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("CHENG_LIST"))
                DS.Tables["CHENG_LIST"].Clear();

            chengListAdapter.Fill(DS, "CHENG_LIST");

            cbChEngName.DataSource = DS.Tables["CHENG_LIST"];
            cbChEngName.DisplayMember = "CHENG_NAME";
            cbChEngName.ValueMember = "CHENG_GUID";

        }

        private void btnCopyInspector_Click(object sender, EventArgs e)
        {
            cbInspectorName.Text = tbInspectorName.Text;
        }

        private void btnCopyMaster_Click(object sender, EventArgs e)
        {
            cbMasterName.Text = tbMasterName.Text;
        }

        private void btnCopyChEng_Click(object sender, EventArgs e)
        {
            cbChEngName.Text = tbChEngName.Text;
        }

        private void tbInspectorName_TextChanged(object sender, EventArgs e)
        {
            cbInspectorName.SelectedValue = -1;
            cbInspectorName.Text = tbInspectorName.Text;
        }

        private void cbInspectorName_SelectedValueChanged(object sender, EventArgs e)
        {
        }

        private void tbMasterName_TextChanged(object sender, EventArgs e)
        {
            cbMasterName.SelectedValue = -1;
            cbMasterName.Text = tbMasterName.Text;
        }

        private void cbMasterName_TextChanged(object sender, EventArgs e)
        {
            if (cbMasterName.Text.Trim().Length == 0)
            {
                pbMaster.Image = null;
                stateMaster = true;
            }
            else
            {
                if (cbMasterName.SelectedValue != null)
                {
                    pbMaster.Image = VIM.Properties.Resources.check_16;
                    stateMaster = true;
                }
                else
                {
                    pbMaster.Image = VIM.Properties.Resources.cancel_16;
                    stateMaster = false;
                }
            }

            CheckMasterState();
        }

        private void CheckMasterState()
        {
            if (cbMasterName.Text.Trim()==tbMaster.Text)
                tbMaster.BackColor = Color.PaleGreen;
            else
                tbMaster.BackColor = Color.BlanchedAlmond;
        }

        private void cbInspectorName_TextChanged(object sender, EventArgs e)
        {
            if (cbInspectorName.Text.Trim().Length == 0)
            {
                pbInspector.Image = null;
                stateInspector = true;
            }
            else
            {
                if (cbInspectorName.SelectedValue != null)
                {
                    pbInspector.Image = VIM.Properties.Resources.check_16;
                    stateInspector = true;
                }
                else
                {
                    pbInspector.Image = VIM.Properties.Resources.cancel_16;
                    stateInspector = false;
                }
            }

            CheckInspectorState();
        }

        private void CheckInspectorState()
        {
            if (cbInspectorName.Text.Trim() == tbInspector.Text)
                tbInspector.BackColor = Color.PaleGreen;
            else
                tbInspector.BackColor = Color.BlanchedAlmond;
        }
        private void cbChEngName_TextChanged(object sender, EventArgs e)
        {
            if (cbChEngName.Text.Trim().Length == 0)
            {
                pbChEng.Image = null;
                stateChEng = true;
            }
            else
            {
                if (cbChEngName.SelectedValue != null)
                {
                    pbChEng.Image = VIM.Properties.Resources.check_16;
                    stateChEng = true;
                }
                else
                {
                    pbChEng.Image = VIM.Properties.Resources.cancel_16;
                    stateChEng = false;
                }
            }

            CheckChEngState();
        }

        private void CheckChEngState()
        {
            if (cbChEngName.Text.Trim() == tbChEng.Text)
                tbChEng.BackColor = Color.PaleGreen;
            else
                tbChEng.BackColor = Color.BlanchedAlmond;
        }

        private void tbChEngName_TextChanged(object sender, EventArgs e)
        {
            cbChEngName.SelectedValue = -1;
            cbChEngName.Text = tbChEngName.Text;
        }

        private void fillReportPersons()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select \n" +
                "REPORTS.REPORT_CODE, \n" +
                "REPORTS.INSPECTOR_GUID, \n" +
                "MASTER_LIST.MASTER_GUID, \n" +
                "CHENG_LIST.CHENG_GUID, \n" +
                "INSPECTORS.INSPECTOR_NAME, \n" +
                "MASTER_LIST.MASTER_NAME, \n" +
                "CHENG_LIST.CHENG_NAME \n" +
                "from \n" +
                "((REPORTS left join INSPECTORS \n" +
                "on REPORTS.INSPECTOR_GUID=INSPECTORS.INSPECTOR_GUID) \n" +
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
                "on REPORTS.REPORT_CODE = CHENG_LIST.REPORT_CODE)";

            OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("REPORT_PERSONS"))
                DS.Tables["REPORT_PERSONS"].Clear();

            adpt.Fill(DS, "REPORT_PERSONS");

        }

        private void btnAddInspector_Click(object sender, EventArgs e)
        {
            NewInspector(tbInspectorName.Text);
        }

        private void NewInspector(string newName="")
        {
            //Add new inspector
            
            InspectorForm inspectorForm = new InspectorForm(MainForm.zeroGuid, 1);

            OleDbCommand cmd = new OleDbCommand("", connection);

            //Full name
            inspectorForm.inspectorName = "";
            //Notes
            inspectorForm.inspectorNotes = "";
            //Photo file
            inspectorForm.inspectorPhoto = "";
            //Unfavourable
            inspectorForm.inspectorUnfavourable = false;

            inspectorForm.inspectorBackground = "";

            var rslt = inspectorForm.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                if (!InspectorExists(inspectorForm.inspectorName,MainForm.zeroGuid))
                {
                    cmd.CommandText =
                        "insert into INSPECTORS (INSPECTOR_NAME,Notes,Photo,Unfavourable,Background)\n" +
                        "values('" + MainForm.StrToSQLStr(inspectorForm.inspectorName) + "',\n" +
                        "'" + MainForm.StrToSQLStr(inspectorForm.inspectorNotes) + "',\n" +
                        "'" + MainForm.StrToSQLStr(inspectorForm.inspectorPhoto) + "',\n" +
                        Convert.ToString(inspectorForm.inspectorUnfavourable) + "," +
                        "'" + MainForm.StrToSQLStr(inspectorForm.inspectorBackground) + "')";
                    MainForm.cmdExecute(cmd);

                    //int row = dataGridView1.CurrentRow.Index;

                    if (DS.Tables.Contains("INSPECTORS"))
                        DS.Tables["INSPECTORS"].Clear();

                    cmd.CommandText =
                       "update INSPECTORS set HAS_PROFILE=False";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText =
                        "update INSPECTORS set HAS_PROFILE=True \n" +
                        "where INSPECTOR_GUID in (select DISTINCT INSPECTOR_GUID from UNIFIED_COMMENTS)";
                    cmd.ExecuteNonQuery();

                    fillInspectors();
                }
            }

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            MoveNext();
        }

        private void MoveNext()
        {
            if (dgvInspections.Rows.Count == 0)
                return;

            int cRow = dgvInspections.CurrentRow.Index;
            int cCol = dgvInspections.CurrentCell.ColumnIndex;

            if (cRow < dgvInspections.Rows.Count - 1)
            {
                dgvInspections.CurrentCell = dgvInspections[cCol, cRow + 1];
                SelectionChanged();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            MovePrevious();
        }

        private void MovePrevious()
        {
            int cRow = dgvInspections.CurrentRow.Index;
            int cCol = dgvInspections.CurrentCell.ColumnIndex;

            if (cRow > 0)
            {
                dgvInspections.CurrentCell = dgvInspections[cCol, cRow - 1];
                SelectionChanged();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Save persons
            SavePersons();
        }

        private void SavePersons()
        {
            //Check selected values

            if (!stateInspector)
            {
                //Wrong inspector name
                MessageBox.Show("Name of inspector is not registered", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!stateMaster)
            {
                //Wrong master
                MessageBox.Show("Name of the Master is not registered", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!stateChEng)
            {
                //Wrong Chief Engineer
                MessageBox.Show("Name of the Chief Engineer is not registered", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            OleDbCommand cmd = new OleDbCommand("", connection);

            string reportCode = dgvInspections.CurrentRow.Cells["REPORT_CODE"].Value.ToString();

            Guid inspectorGuid = MainForm.zeroGuid;

            string inspectorName = "";

            if (cbInspectorName.SelectedValue != null && cbInspectorName.SelectedValue != DBNull.Value)
            {
                try
                {
                    inspectorGuid = MainForm.StrToGuid(cbInspectorName.SelectedValue.ToString());
                    inspectorName = cbInspectorName.Text;
                }
                catch (Exception E)
                {
                    MessageBox.Show("Inspector : " + E.Message);
                }
            }

            Guid masterGuid = MainForm.zeroGuid;

            string masterName = "";

            if (cbMasterName.SelectedValue != null && cbMasterName.SelectedValue != DBNull.Value)
            {
                try
                {
                    masterGuid = MainForm.StrToGuid(cbMasterName.SelectedValue.ToString());
                    masterName = cbMasterName.Text;
                }
                catch (Exception E)
                {
                    MessageBox.Show("Master : " + E.Message);
                }
            }

            Guid chengGuid = MainForm.zeroGuid;
            string chengName = "";

            if (cbChEngName.SelectedValue != null && cbChEngName.SelectedValue != DBNull.Value)
            {
                try
                {
                    chengGuid = MainForm.StrToGuid(cbChEngName.SelectedValue.ToString());
                    chengName = cbChEngName.Text;
                }
                catch (Exception E)
                {
                    MessageBox.Show("Chief engieer : " + E.Message);
                }
            }

            string persons = "";

            if (chbInspector.Checked)
            {
                persons = "INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid);
            }

            if (chbMaster.Checked)
            {
                if (persons.Length == 0)
                    persons = "MASTER_GUID=" + MainForm.GuidToStr(masterGuid);
                else
                    persons = persons + ", \n" + "MASTER_GUID=" + MainForm.GuidToStr(masterGuid);
            }

            if (chbChEng.Checked)
            {
                if (persons.Length == 0)
                    persons = "CHENG_GUID=" + MainForm.GuidToStr(chengGuid);
                else
                    persons = persons + ", \n" + "CHENG_GUID=" + MainForm.GuidToStr(chengGuid);
            }

            if (persons.Length == 0)
            {
                pbInspectorSaved.Image = VIM.Properties.Resources.cancel_16;
                pbMasterSaved.Image = VIM.Properties.Resources.cancel_16;
                pbMasterSaved.Image = VIM.Properties.Resources.cancel_16;

                return;
            }

            cmd.CommandText =
                "update REPORTS set \n" +
                persons + " \n" +
                "where REPORT_CODE='" + reportCode + "'";

            if (MainForm.cmdExecute(cmd)>=0)
            {
                DataTable tbl = DS.Tables["REPORT_PERSONS"];

                DataRow[] rows = tbl.Select("REPORT_CODE='" + reportCode + "'");

                if (rows.Length > 0)
                {
                    if (chbInspector.Checked)
                    {
                        rows[0]["INSPECTOR_GUID"] = inspectorGuid;
                        rows[0]["INSPECTOR_NAME"] = inspectorName;
                        pbInspectorSaved.Image = VIM.Properties.Resources.save;
                    }
                    else
                        pbInspectorSaved.Image = VIM.Properties.Resources.cancel_16;

                    if (chbMaster.Checked)
                    {
                        rows[0]["MASTER_GUID"] = masterGuid;
                        rows[0]["MASTER_NAME"] = masterName;
                        pbMasterSaved.Image = VIM.Properties.Resources.save;
                    }
                    else
                        pbMasterSaved.Image = VIM.Properties.Resources.cancel_16;

                    if (chbChEng.Checked)
                    {
                        rows[0]["CHENG_GUID"] = chengGuid;
                        rows[0]["CHENG_NAME"] = chengName;
                        pbChengSaved.Image = VIM.Properties.Resources.save;
                    }
                    else
                        pbMasterSaved.Image = VIM.Properties.Resources.cancel_16;
                }

                tbInspector.Text = cbInspectorName.Text;
                tbMaster.Text = cbMasterName.Text;
                tbChEng.Text = cbChEngName.Text;

            }
        }

        private void btnAddMaster_Click(object sender, EventArgs e)
        {
            //Add new master

            CrewDetailsForm form = new CrewDetailsForm();

            var rslt = form.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                fillMasters();
            }

        }

        public bool MasterExists(string masterName, Guid masterGuid)
        {
            //Check for records with the same MASTER_NAME
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select Count(MASTER_NAME) \n" +
                "from MASTERS \n" +
                "where MASTER_NAME like '" + MainForm.StrToSQLStr(masterName) + "' \n" +
                "and MASTER_GUID<>" + MainForm.GuidToStr(masterGuid);

            int recs = (int)cmd.ExecuteScalar();

            if (recs > 0)
            {
                //There is record with the same name
                cmd.CommandText =
                    "select MASTER_NAME, PERSONAL_ID \n" +
                    "from MASTERS \n" +
                    "where MASTER_NAME like '" + MainForm.StrToSQLStr(masterName) + "' \n" +
                    "and MASTER_GUID<>" + MainForm.GuidToStr(masterGuid);

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

                var res1 = System.Windows.Forms.MessageBox.Show(msgText, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res1 == DialogResult.Yes)
                    return false;
                else
                    return true;
            }
            else
                return false;

        }

        private bool InspectorExists(string name, Guid inspectorGuid)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select Count(INSPECTOR_NAME) \n" +
                "from INSPECTORS \n" +
                "where INSPECTOR_NAME like '" + MainForm.StrToSQLStr(name) + "' \n" +
                "and INSPECTOR_GUID<>" + MainForm.GuidToStr(inspectorGuid);

            int recs = (int)cmd.ExecuteScalar();

            if (recs > 0)
            {
                //There is record with the same name
                cmd.CommandText =
                    "select INSPECTOR_NAME \n" +
                    "from INSPECTORS \n" +
                    "where INSPECTOR_NAME like '" + MainForm.StrToSQLStr(name) + "' \n" +
                    "and INSPECTOR_GUID<>" + MainForm.GuidToStr(inspectorGuid);

                OleDbDataReader mReader = cmd.ExecuteReader();

                int recsReader = 0;

                string msgText = "";

                while (mReader.Read())
                {
                    if (msgText.Length == 0)
                        msgText = "Name : \"" + mReader[0].ToString() + "\"";
                    else
                        msgText = msgText + "\n" +
                            "Name : \"" + mReader[0].ToString() + "\"";
                    recsReader++;
                }

                mReader.Close();

                if (recsReader == 1)
                    msgText = "There is a record for inspector with the same name: \n\n" + msgText;
                else
                    msgText = "There are " + recsReader.ToString() + " records for inspector with the same name: \n\n" + msgText;

                msgText = msgText + "\n\n Would you like to save this record anyway?";

                var res1 = System.Windows.Forms.MessageBox.Show(msgText, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res1 == DialogResult.Yes)
                    return false;
                else
                    return true;
            }
            else
                return false;
        }

        private void btnAddChEng_Click(object sender, EventArgs e)
        {
            //Add new Chief Engineer
            ChEngDetailsForm chForm = new ChEngDetailsForm(connection, DS, this.Font, this.Icon, MainForm.zeroGuid);

            chForm.chengName = "";
            chForm.chengPersonalID = "";
            chForm.chengNotes = "";

            var rslt = chForm.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                if (ChEngExists(chForm.chengName, MainForm.zeroGuid)) return;

                OleDbCommand cmd = new OleDbCommand("", connection);

                cmd.CommandText =
                    "insert into CHIEF_ENGINEERS (CHENG_NAME,PERSONAL_ID,CHENG_NOTES) \n" +
                    "values('" + MainForm.StrToSQLStr(chForm.chengName) + "','" + 
                            MainForm.StrToSQLStr(chForm.chengPersonalID) + "','" + 
                            MainForm.StrToSQLStr(chForm.chengNotes) + "')";
                MainForm.cmdExecute(cmd);

                fillChiefEngineers();
            }

        }

        public bool ChEngExists(string chengName,Guid chengGuid)
        {
            //Check for records with the same CHENG_NAME
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select Count(CHENG_NAME) \n" +
                "from CHIEF_ENGINEERS \n" +
                "where CHENG_NAME like '" + MainForm.StrToSQLStr(chengName) + "' \n" +
                "and CHENG_GUID<>"+MainForm.GuidToStr(chengGuid);

            int recs = (int)cmd.ExecuteScalar();

            if (recs > 0)
            {
                //There is record with the same name
                cmd.CommandText =
                    "select CHENG_NAME, PERSONAL_ID \n" +
                    "from CHIEF_ENGINEERS \n" +
                    "where CHENG_NAME like '" + MainForm.StrToSQLStr(chengName) + "' \n" +
                    "and CHENG_GUID<>"+MainForm.GuidToStr(chengGuid);

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

                msgText = msgText + "\n\n Would you like to save this record?";

                var res1 = System.Windows.Forms.MessageBox.Show(msgText, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res1 == DialogResult.Yes)
                    return false;
                else
                    return true;
            }
            else
                return false;

        }

        private void FrmImportPersonnel_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Store form settings
            Properties.Settings.Default.FrmImportPersonsState = this.WindowState;

            if (this.WindowState == FormWindowState.Normal)
            {
                // save location and size if the state is normal
                Properties.Settings.Default.FrmImportPersonsLocation = this.Location;
                Properties.Settings.Default.FrmImportPersonsSize = this.Size;
            }
            else
            {
                // save the RestoreBounds if the form is minimized or maximized!
                Properties.Settings.Default.FrmImportPersonsLocation = this.RestoreBounds.Location;
                Properties.Settings.Default.FrmImportPersonsSize = this.RestoreBounds.Size;
            }

            Properties.Settings.Default.FrmImportPersonsSplitterPosition = splitContainer1.SplitterDistance;

            // don't forget to save the settings
            Properties.Settings.Default.Save();

        }

        private void FrmImportPersonnel_Load(object sender, EventArgs e)
        {
            //Restore form settings
            // Upgrade?
            if (Properties.Settings.Default.FrmImportPersonsSize.Width == 0)
                Properties.Settings.Default.Upgrade();

            if (Properties.Settings.Default.FrmImportPersonsSize.Width == 0 || Properties.Settings.Default.FrmImportPersonsSize.Height == 0)
            {
                // first start
                // optional: add default values
            }
            else
            {
                this.WindowState = Properties.Settings.Default.FrmImportPersonsState;

                // we don't want a minimized window at startup
                if (this.WindowState == FormWindowState.Minimized) this.WindowState = FormWindowState.Normal;

                this.Location = Properties.Settings.Default.FrmImportPersonsLocation;
                this.Size = Properties.Settings.Default.FrmImportPersonsSize;

                splitContainer1.SplitterDistance = Properties.Settings.Default.FrmImportPersonsSplitterPosition;
            }

        }

        private void btnEditChEng_Click(object sender, EventArgs e)
        {
            //Edit Chief Engineer
            if (cbChEngName.SelectedValue==null)
            {
                MessageBox.Show("You can edit just registered chief engineer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Guid chengGuid = MainForm.StrToGuid(cbChEngName.SelectedValue.ToString());

            ChEngDetailsForm chForm = new ChEngDetailsForm(connection, DS, this.Font, this.Icon, chengGuid);

            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select * \n" +
                "from CHIEF_ENGINEERS \n" +
                "where CHENG_GUID=" + MainForm.GuidToStr(chengGuid);

            OleDbDataReader reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                MessageBox.Show("Chief Engineer with GUID=" + chengGuid.ToString() + " was not found in database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                reader.Close();

                return;
            }

            reader.Read();

            chForm.chengName = reader["CHENG_NAME"].ToString();
            chForm.chengPersonalID = reader["PERSONAL_ID"].ToString();
            chForm.chengNotes = reader["CHENG_NOTES"].ToString();

            reader.Close();

            var rslt = chForm.ShowDialog();

            if ((rslt == DialogResult.OK) && (MainForm.isPowerUser))
            {

                if (ChEngExists(chForm.chengName, chengGuid))
                {
                    MessageBox.Show("There is a recor for Chief Engineer with the same name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                cmd.CommandText =
                "update CHIEF_ENGINEERS set \n" +
                "CHENG_NAME='" + MainForm.StrToSQLStr(chForm.chengName) + "', \n" +
                "PERSONAL_ID='" + MainForm.StrToSQLStr(chForm.chengPersonalID) + "', \n" +
                "CHENG_NOTES='" + MainForm.StrToSQLStr(chForm.chengNotes) + "' \n" +
                "where CHENG_GUID=" + MainForm.GuidToStr(chengGuid);
                MainForm.cmdExecute(cmd);
            }

            fillChiefEngineers();

            cbChEngName.Text = chForm.chengName;

        }

        private void btnEditInspector_Click(object sender, EventArgs e)
        {
            //Edit inspector

            if (cbInspectorName.SelectedValue==null || MainForm.StrToGuid(cbInspectorName.SelectedValue.ToString())==MainForm.zeroGuid)
            {
                MessageBox.Show("You are able to change just registered inspector", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Guid inspectorGuid = MainForm.StrToGuid(cbInspectorName.SelectedValue.ToString());

            this.Cursor = Cursors.WaitCursor;

            InspectorForm inspectorForm = new InspectorForm(inspectorGuid, 0);

            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select * \n" +
                "from INSPECTORS \n" +
                "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid);

            OleDbDataReader reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                reader.Close();

                MessageBox.Show("Inspector with GUID=" + inspectorGuid.ToString() + " was not found in database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return;
            }

            reader.Read();
            //Full name
            inspectorForm.inspectorName = reader["INSPECTOR_NAME"].ToString();
            //Notes
            inspectorForm.inspectorNotes = reader["Notes"].ToString();
            //Photo file
            inspectorForm.inspectorPhoto = reader["Photo"].ToString();
            //Background
            inspectorForm.inspectorBackground = reader["Background"].ToString();

            inspectorForm.inspectorUnfavourable = Convert.ToBoolean(reader["Unfavourable"]);
            //Unfavourable
            reader.Close();

            var rslt = inspectorForm.ShowDialog();

            this.Cursor = Cursors.Default;

            if ((rslt == DialogResult.OK) && (MainForm.isPowerUser))
            {
                if (InspectorExists(inspectorForm.inspectorName,inspectorGuid))
                {
                    MessageBox.Show("Inspector with the same name already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                cmd.CommandText =
                    "update INSPECTORS set\n" +
                    "INSPECTOR_NAME='" + MainForm.StrToSQLStr(inspectorForm.inspectorName) + "',\n" +
                    "Notes='" + MainForm.StrToSQLStr(inspectorForm.inspectorNotes) + "',\n" +
                    "Photo='" + MainForm.StrToSQLStr(inspectorForm.inspectorPhoto) + "',\n" +
                    "Unfavourable=" + Convert.ToString(inspectorForm.inspectorUnfavourable) + ",\n" +
                    "Background='" + MainForm.StrToSQLStr(inspectorForm.inspectorBackground) + "'\n" +
                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid);
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    "update INSPECTORS set HAS_PROFILE=False";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    "update INSPECTORS set HAS_PROFILE=True \n" +
                    "where INSPECTOR_GUID in (select DISTINCT INSPECTOR_GUID from UNIFIED_COMMENTS)";
                cmd.ExecuteNonQuery();

                fillInspectors();

                cbInspectorName.Text = inspectorForm.inspectorName;
            }
        }

        private void btnEditMaster_Click(object sender, EventArgs e)
        {
            //Edit master

            if (cbMasterName.SelectedValue==null)
            {
                MessageBox.Show("You are able to edit just registered master record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Guid masterGuid = MainForm.StrToGuid(cbMasterName.SelectedValue.ToString());

            CrewDetailsForm form = new CrewDetailsForm();

            form.crewGuid = masterGuid;

            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select * \n" +
                "from CREW \n" +
                "where CREW_GUID=" + MainForm.GuidToStr(masterGuid);

            OleDbDataReader reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                reader.Close();
                MessageBox.Show("Master with GUID=" + masterGuid.ToString() + " was not found in database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            reader.Close();

            var rslt = form.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                if (form.wasChanged)
                {
                    fillMasters();

                    cbMasterName.Text = form.crewName;
                }
            }

        }

        private void FrmImportPersonnel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Control)
                SavePersons();

            if (e.KeyCode == Keys.Right && e.Control)
                MoveNext();

            if (e.KeyCode == Keys.Left && e.Control)
                MovePrevious();
        }

        private void tbMaster_TextChanged(object sender, EventArgs e)
        {
            CheckMasterState();
        }

        private void tbInspector_TextChanged(object sender, EventArgs e)
        {
            CheckInspectorState();
        }

        private void tbChEng_TextChanged(object sender, EventArgs e)
        {
            CheckChEngState();
        }

    }
}
