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
    public partial class FormSelectCrewmember : Form
    {
        private Guid _crewGuid = MainForm.zeroGuid;
        private Guid _positionGuid = MainForm.zeroGuid;
        private Guid _vesselGuid = MainForm.zeroGuid;
        private bool _wasChanged = false;
        private string _reportCode = "";

        public string vesselName
        {
            set { tbVesselName.Text = value; }
        }

        public Guid vesselGuid
        {
            set { _vesselGuid = value; }
            get { return _vesselGuid; }
        }
        
        public string position
        {
            set { tbPosition.Text = value; }
        }

        public Guid positionGuid
        {
            set { _positionGuid = value; FillCrewmembers(_positionGuid); }

        }

        public string crewmember
        {
            set { cbCrewmember.Text = value; }
            get { return cbCrewmember.Text; }
        }

        public Guid crewGuid
        {
            set { _crewGuid = value; cbCrewmember.SelectedValue = _crewGuid; }
            get { return _crewGuid; }
        }

        public string reportCode
        {
            set { _reportCode = value; }
        }

        public bool wasChanged
        {
            get { return _wasChanged; }
        }

        public FormSelectCrewmember()
        {
            this.Icon = MainForm.mainIcon;
            this.Font = MainForm.mainFont;

            InitializeComponent();

        }

        public DateTime dateInspected
        {
            set { dtInspectionDate.Value = value; }
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            if (!CheckCrewExists())
            {
                MessageBox.Show("Unable to show details for \"" + cbCrewmember.Text + "\"", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

            //Проверяем наличие имени в списке

            cmd.CommandText =
                "select Count(CREW_GUID) \n" +
                "from CREW \n" +
                "where \n" +
                "CREW_NAME like '" + MainForm.StrToSQLStr(cbCrewmember.Text) + "' \n" +
                "and CREW_POSITION_GUID=" + MainForm.GuidToStr(_positionGuid);

            int recCount = (int)cmd.ExecuteScalar();

            if (recCount == 0)
                return;

            if ( _crewGuid==MainForm.zeroGuid)
            {
                if (recCount==1)
                {
                    cmd.CommandText=
                        "select CREW_GUID \n" +
                        "from CREW \n" +
                        "where \n" +
                        "CREW_NAME like '" + MainForm.StrToSQLStr(cbCrewmember.Text) + "' \n" +
                        "and CREW_POSITION_GUID=" + MainForm.GuidToStr(_positionGuid);

                    object x = cmd.ExecuteScalar();

                    try
                    {
                        _crewGuid = (Guid)x;
                    }
                    catch
                    {
                        MessageBox.Show("Unable to get crewmember GUID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }                
                }
                else
                {
                    FormSelectCrewmemberFromList form = new FormSelectCrewmemberFromList();

                    form.crewmemberName = cbCrewmember.Text;
                    form.positionGuid = _positionGuid;

                    if (form.ShowDialog()==DialogResult.OK)
                    {

                    }
                }
            }

            if (cbCrewmember.SelectedValue.ToString().Length > 0)
            {
                this.Cursor = Cursors.WaitCursor;

                CrewDetailsForm form = new CrewDetailsForm();

                //form.positionGuid = _positionGuid;
                
                form.crewGuid = MainForm.StrToGuid(cbCrewmember.SelectedValue.ToString());
                //form.crewmemberName = cbCrewmember.Text;

                var rslt = form.ShowDialog();

                if (rslt == DialogResult.OK)
                {
                    FillCrewmembers(_positionGuid);

                    //Записываем имя в поле
                    cbCrewmember.Text = form.crewName;
                }

                this.Cursor = Cursors.Default;
            }
        }

        private void cbPosition_SelectedValueChanged(object sender, EventArgs e)
        {
            FillCrewmembers(_positionGuid);
        }

        private void FillCrewmembers(Guid pGuid)
        {
            OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

            cmd.CommandText =
                "select CREW_NAME, CREW_GUID \n" +
                "from CREW \n" +
                "where \n" +
                "CREW_POSITION_GUID=" + MainForm.GuidToStr(pGuid) + " \n" +
                "order by CREW_NAME";

            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            if (MainForm.DS.Tables.Contains("CREW_LIST"))
                MainForm.DS.Tables["CREW_LIST"].Clear();

            adapter.Fill(MainForm.DS, "CREW_LIST");

            cbCrewmember.DataSource = MainForm.DS.Tables["CREW_LIST"];
            cbCrewmember.DisplayMember = "CREW_NAME";
            cbCrewmember.ValueMember = "CREW_GUID";

        }

        private bool CheckCrewExists()
        {
            if (cbCrewmember.SelectedValue==null)
            {
                var rslt = MessageBox.Show("There is no record for " + cbCrewmember.Text + " in the database. \n" +
                    "Whould you like to add in to the list of " + tbPosition.Text + "?",
                    "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (rslt==DialogResult.Yes)
                {
                    OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

                    Guid newGuid = new Guid();

                    cmd.CommandText =
                        "insert into CREW (CREW_GUID, CREW_NAME, CREW_POSITION_GUID) \n" +
                        "values(" + MainForm.GuidToStr(newGuid) + ",'" +
                        MainForm.StrToSQLStr(cbCrewmember.Text) + "'," +
                        MainForm.GuidToStr(_positionGuid) + ")";

                    if (MainForm.cmdExecute(cmd)<0)
                    {
                        MessageBox.Show("Failed to create new crewmember record", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else
                    {
                        FillCrewmembers(_positionGuid);

                        cbCrewmember.SelectedValue = newGuid;

                        _crewGuid = newGuid;

                        return true;
                    }
                }
                else
                {
                    MessageBox.Show("You will be unable to work with crewmember if it is not saved in database.",
                        "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        private void btnLocateCrew_Click(object sender, EventArgs e)
        {
            string crewName = "";

            if (dtInspectionDate.Value < DateTime.Parse("2000-01-01"))
            {
                MessageBox.Show("Please provide date of inspection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

            cmd.CommandText =
                "select CREW_NAME, DATE_ON, DATE_OFF \n" +
                "from CREW_ON_BOARD \n" +
                "where \n" +
                "VESSEL_GUID=" + MainForm.GuidToStr(vesselGuid) + "\n" +
                "and CREW_POSITION='"+tbPosition.Text+"' \n" +
                "and DATE_ON<=" + MainForm.DateTimeToQueryStr(dtInspectionDate.Value) + "\n" +
                "and (IsNull(DATE_OFF) or DATE_OFF>=" + MainForm.DateTimeToQueryStr(dtInspectionDate.Value) + ") \n" +
                "order by DATE_ON";

            OleDbDataReader reader = cmd.ExecuteReader();

            List<string> crew = new List<string>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    crew.Add(reader["CREW_NAME"].ToString());
                }

                if (crew.Count == 1)
                    crewName = crew[0];
                else
                {
                    crewName = crew[0];

                    MessageBox.Show("There are several crewmembers on board at the date on inspection. \n" +
                        "Application select the master that joined the vessel first. \n" +
                        "For more details look at the list of crew on board.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                cbCrewmember.Text = crewName;
            }
            else
            {
                MessageBox.Show("There is no suitable record in the list of crew on board", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reader.Close();
        }

        private void btnCrewOnBoard_Click(object sender, EventArgs e)
        {
            FrmCrewOnBoard form = new FrmCrewOnBoard();

            form.queryFilter = "VESSEL_NAME='" + tbVesselName.Text + "' and CREW_POSITION='" + tbPosition.Text + "' " +
                "and DATE_ON<=" + MainForm.DateTimeToQueryStr(dtInspectionDate.Value) + " and " +
                "(IsNull(DATE_OFF) or DATE_OFF>" + MainForm.DateTimeToQueryStr(dtInspectionDate.Value) + ")";
            form.querySort = "DATE_ON DESC";

            form.formCaption = " on " + dtInspectionDate.Value.ToShortDateString();

            form.ShowDialog();
        }

        private void FormSelectCrewmember_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
                return;

            if (cbCrewmember.Text.Trim().Length > 0)
            {
                if (cbCrewmember.SelectedValue == null ||
                    cbCrewmember.SelectedValue.ToString() == MainForm.GuidToStr(MainForm.zeroGuid))
                {
                    string msgText =
                        tbPosition.Text + " with name " + cbCrewmember.Text + " was not found in the list of personnel. \n" +
                        "It is necessary to include this crewmember into the list. Would you like to do it now?";

                    var rslt = MessageBox.Show(msgText, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (rslt != DialogResult.Yes)
                    {
                        e.Cancel = true;
                        return;
                    }

                    OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

                    Guid newCrewGuid = Guid.NewGuid();

                    cmd.CommandText =
                        "insert into CREW (CREW_GUID, CREW_NAME, CREW_POSITION_GUID) \n" +
                        "values(" + MainForm.GuidToStr(newCrewGuid) + ",'" +
                        MainForm.StrToSQLStr(cbCrewmember.Text) + "'," +
                        MainForm.GuidToStr(_positionGuid) + ")";

                    if (MainForm.cmdExecute(cmd) < 0)
                    {
                        MessageBox.Show("Failed to add new record to the list of personnel",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                        return;
                    }

                    _wasChanged = true;
                    _crewGuid = newCrewGuid;
                    //newRecord = true;
                }
            }

            if (!_wasChanged)
            {
                if (cbCrewmember.SelectedValue == null)
                {
                    _crewGuid = MainForm.zeroGuid;
                    _wasChanged = true;
                }
                else
                {
                    if (_crewGuid != MainForm.StrToGuid(cbCrewmember.SelectedValue.ToString()))
                    {
                        _crewGuid = MainForm.StrToGuid(cbCrewmember.SelectedValue.ToString());
                        _wasChanged = true;
                    }
                }
            }

            if (_wasChanged)
            {
                OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

                //Check record exists
                cmd.CommandText=
                    "select Count(REPORT_CODE) \n"+
                    "from REPORTS_CREW \n"+
                    "where \n" +
                    "REPORT_CODE='" + MainForm.StrToSQLStr(_reportCode) + "' \n" +
                    "and CREW_POSITION_GUID=" + MainForm.GuidToStr(_positionGuid);

                bool recordExists = (int)cmd.ExecuteScalar() > 0;

                if (recordExists)
                {
                    cmd.CommandText =
                        "update REPORTS_CREW set \n" +
                        "CREW_GUID=" + MainForm.GuidToStr(_crewGuid,true) + " \n" +
                        "where \n" +
                        "REPORT_CODE='" + MainForm.StrToSQLStr(_reportCode) + "' \n" +
                        "and CREW_POSITION_GUID=" + MainForm.GuidToStr(_positionGuid);

                    if (MainForm.cmdExecute(cmd) < 0)
                    {
                        MessageBox.Show("Failed to update crew for report", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                        return;
                    }
                }
                else
                {
                    cmd.CommandText =
                        "insert into REPORTS_CREW (REPORT_CODE, CREW_GUID, CREW_POSITION_GUID) \n" +
                        "values('" + MainForm.StrToSQLStr(_reportCode) + "'," +
                        MainForm.GuidToStr(_crewGuid, true) + "," +
                        MainForm.GuidToStr(_positionGuid) + ")";

                    if (MainForm.cmdExecute(cmd) < 0)
                    {
                        MessageBox.Show("Failed to create new crew record for report", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                        return;
                    }
                }
            }

        }
    }
}
