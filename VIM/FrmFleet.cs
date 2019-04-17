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
    public partial class FrmFleet : Form
    {
        DataSet DS;
        OleDbConnection connection;

        private string _fleetID = "";
        public string fleetId
        {
            get { return tbFleetID.Text.Trim(); }
            set { tbFleetID.Text = value; SetFleetID(value); }
        }

        public string fleetText
        {
            get { return tbFleetText.Text.Trim(); }
            set { tbFleetText.Text = value; }
        }

        private string _fleetEmails = "";

        public string fleetEmails
        {
            set { _fleetEmails = value; }
            get { return _fleetEmails; }
        }

        private int _recordID = 0;

        private bool newRecord = false;

        public int recordID
        {
            get { return _recordID; }
            set { _recordID = value; newRecord = _recordID == 0; }
        }

        public bool dataChanged = false;

        public FrmFleet()
        {
            DS = MainForm.DS;
            connection = MainForm.connection;
            this.Font = MainForm.mainFont;
            this.Icon = MainForm.mainIcon;


            InitializeComponent();


        }

        public void SetFleetID(string id)
        {
            _fleetID = id;

            FillEmails();
        }

        private void FillEmails()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select FLEET_EMAIL \n" +
                "from FLEET_EMAILS \n" +
                "where FLEET_ID='" + MainForm.StrToSQLStr(_fleetID) + "' \n" +
                "order by FLEET_EMAIL";

            OleDbDataReader reader = cmd.ExecuteReader();

            lbEMails.Items.Clear();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    lbEMails.Items.Add(reader[0].ToString());
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (tbFleetID.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please provide fleet code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (WasIDChanged())
            {
                var rst = MessageBox.Show("Fleet code was changed. It is necessary to save new code before adding new email. \n" +
                    "Would you like to save code now?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (rst!=DialogResult.Yes)
                {
                    MessageBox.Show("You are unable to add email address without fleet code saving", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                if (!SaveNewID())
                {
                    MessageBox.Show("Failed to save new ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            
            FrmFleetEmail form = new FrmFleetEmail(this.Font, this.Icon);

            form.fleetID = tbFleetID.Text.Trim();
            form.fleetEmail = "";
            form.EMails = lbEMails;

            var rslt = form.ShowDialog();

            if (rslt==DialogResult.OK)
            {
                if (form.needSave)
                {
                    OleDbCommand cmd = new OleDbCommand("", connection);

                    cmd.CommandText =
                        "insert into FLEET_EMAILS (FLEET_ID, FLEET_EMAIL) \n" +
                        "values('" + MainForm.StrToSQLStr(_fleetID) + "','" +
                                   MainForm.StrToSQLStr(form.fleetEmail) + "')";

                    MainForm.cmdExecute(cmd);

                    FillEmails();
                    UpdateFleetEmails();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditEmail();
        }

        private void EditEmail()
        {
            if (lbEMails.Items.Count == 0)
                return;

            if (tbFleetID.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please provide fleet code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (WasIDChanged())
            {
                var rst = MessageBox.Show("Fleet code was changed. It is necessary to save new code before edit email. \n" +
                    "Would you like to save code now?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (rst != DialogResult.Yes)
                {
                    MessageBox.Show("You are unable to edit email address without fleet code saving", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!SaveNewID())
                {
                    MessageBox.Show("Failed to save new ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            FrmFleetEmail form = new FrmFleetEmail(this.Font, this.Icon);

            form.fleetID = tbFleetID.Text.Trim();
            form.fleetEmail = lbEMails.SelectedItem.ToString();
            form.EMails = lbEMails;

            var rslt = form.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                if (form.needSave)
                {
                    OleDbCommand cmd = new OleDbCommand("", connection);

                    cmd.CommandText =
                        "update FLEET_EMAILS set \n" +
                        "FLEET_EMAIL='" + MainForm.StrToSQLStr(form.fleetEmail) + "' \n" +
                        "where FLEET_ID='" + MainForm.StrToSQLStr(_fleetID) + "' \n" +
                        "and FLEET_EMAIL='" + MainForm.StrToSQLStr(lbEMails.SelectedItem.ToString()) + "'";

                    MainForm.cmdExecute(cmd);

                    FillEmails();
                    UpdateFleetEmails();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lbEMails.Items.Count == 0)
                return;

            if (tbFleetID.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please provide fleet code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (WasIDChanged())
            {
                var rst = MessageBox.Show("Fleet code was changed. It is necessary to save new code before delete email. \n" +
                    "Would you like to save code now?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (rst != DialogResult.Yes)
                {
                    MessageBox.Show("You are unable to delete email address without fleet code saving", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!SaveNewID())
                {
                    MessageBox.Show("Failed to save new ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            
            var rslt = MessageBox.Show("You are going to delete the following email address: \n\n" +
                lbEMails.SelectedItem.ToString() + "\n\n" +
                "Would you like to proceed?",
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (rslt==DialogResult.Yes)
            {
                OleDbCommand cmd = new OleDbCommand("", connection);

                cmd.CommandText =
                    "delete from FLEET_EMAILS \n" +
                    "where FLEET_ID='" + MainForm.StrToSQLStr(_fleetID) + "' \n" +
                    "and FLEET_EMAIL='" + MainForm.StrToSQLStr(lbEMails.SelectedItem.ToString()) + "'";

                if (MainForm.cmdExecute(cmd)>=0)
                {
                    FillEmails();
                    UpdateFleetEmails();
                }
            }
        }

        private bool WasIDChanged()
        {
            if (_fleetID != tbFleetID.Text.Trim())
                return true;
            else
                return false;
        }

        private bool SaveNewID()
        {
            string newID = tbFleetID.Text.Trim();


            //Check ID exists
            if (CodeExists(newID))
            {
                return false;
            }

            OleDbCommand cmd = new OleDbCommand("", connection);

            if (_recordID == 0)
            {
                cmd.CommandText =
                    "insert into FLEETS (FLEET_ID) \n" +
                    "values('" + MainForm.StrToSQLStr(newID) + "')";

                if (MainForm.cmdExecute(cmd)>=0)
                {
                    cmd.CommandText =
                        "select ID \n" +
                        "from FLEETS \n" +
                        "where FLEET_ID='" + MainForm.StrToSQLStr(newID) + "'";

                    _recordID = (int)cmd.ExecuteScalar();
                    _fleetID = newID;

                    return true;
                }
                else
                {
                    MessageBox.Show("Failed to add new fleet code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
            }
            else
            {
                OleDbTransaction trans = connection.BeginTransaction();

                cmd.Transaction = trans;

                cmd.CommandText =
                    "update FLEET_EMAILS set \n" +
                    "FLEET_ID='" + MainForm.StrToSQLStr(newID) + "' \n" +
                    "where FLEET_ID='" + MainForm.StrToSQLStr(_fleetID) + "'";

                if (MainForm.cmdExecute(cmd)<0)
                {
                    trans.Rollback();

                    return false;
                }

                cmd.CommandText =
                    "update FLEETS set \n" +
                    "FLEET_ID='" + MainForm.StrToSQLStr(newID) + "' \n" +
                    "where FLEET_ID='" + MainForm.StrToSQLStr(_fleetID) + "'";

                if (MainForm.cmdExecute(cmd)<0)
                {
                    trans.Rollback();

                    return false;
                }

                trans.Commit();

                return true;
            }
        }

        private void UpdateFleetEmails()
        {
            string s = "";

            ListBox.ObjectCollection items = lbEMails.Items;

            foreach (string item in items)
            {
                if (s.Length == 0)
                    s = s + item;
                else
                    s = s + ";" + item;
            }

            if (s.Length > 255)
                s = s.Substring(0, 254);

            _fleetEmails = s;
        }

        private void FrmFleet_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                if (newRecord && _recordID>0)
                {
                    //New record created. Delete all records

                    OleDbCommand cmd = new OleDbCommand("", connection);
                    cmd.CommandText =
                        "delete from FLEET_EMAILS \n" +
                        "where FLEET_ID='" + MainForm.StrToSQLStr(tbFleetID.Text.Trim()) + "'";

                    MainForm.cmdExecute(cmd);

                    cmd.CommandText=
                        "delete from FLEETS \n"+
                        "where FLEET_ID='" + MainForm.StrToSQLStr(tbFleetID.Text.Trim()) + "'";

                    MainForm.cmdExecute(cmd);
                }

                return;
            }

            if (tbFleetID.Text.Trim().Length==0)
            {
                MessageBox.Show("Please provide fleet code or use Cancel button to close the form", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }

            if (CodeExists(tbFleetID.Text.Trim()))
            {
                e.Cancel = true;
                return;
            }

            if (!SaveRecord())
            {
                e.Cancel = true;
                return;
            }
        }

        private bool CodeExists(string code)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            //Check ID exists
            cmd.CommandText =
                "select Count(FLEET_ID) as Recs \n" +
                "from FLEETS \n" +
                "where FLEET_ID='" + MainForm.StrToSQLStr(code) + "' \n" +
                "and ID<>" + _recordID.ToString();

            int recs = (int)cmd.ExecuteScalar();

            if (recs > 0)
            {
                MessageBox.Show("There is already a record for fleet code \"" + code + "\".\n" +
                    "Please provide another fleet code.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return true;
            }

            return false;

        }

        private bool SaveRecord()
        {
            string newID = tbFleetID.Text.Trim();


            //Check ID exists
            if (CodeExists(newID))
            {
                return false;
            }

            OleDbCommand cmd = new OleDbCommand("", connection);

            if (_recordID == 0)
            {
                cmd.CommandText =
                    "insert into FLEETS (FLEET_ID,FLEET_TEXT,FLEET_EMAILS) \n" +
                    "values('" + MainForm.StrToSQLStr(newID) + "', \n" +
                        "'" + MainForm.StrToSQLStr(tbFleetText.Text.Trim()) + "', \n" +
                        "'" + MainForm.StrToSQLStr(_fleetEmails) + "')";

                if (MainForm.cmdExecute(cmd)>=0)
                {
                    cmd.CommandText =
                        "select ID \n" +
                        "from FLEETS \n" +
                        "where FLEET_ID='" + MainForm.StrToSQLStr(newID) + "'";

                    _recordID = (int)cmd.ExecuteScalar();

                    dataChanged = true;

                    return true;
                }
                else
                {
                    MessageBox.Show("Failed to create new record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
            }
            else
            {

                cmd.CommandText =
                    "update FLEETS set \n" +
                    "FLEET_ID='" + MainForm.StrToSQLStr(newID) + "', \n" +
                    "FLEET_TEXT='" + MainForm.StrToSQLStr(tbFleetText.Text.Trim()) + "', \n" +
                    "FLEET_EMAILS='" + MainForm.StrToSQLStr(_fleetEmails) + "' \n" +
                    "where ID=" + _recordID.ToString();

                if (MainForm.cmdExecute(cmd)<0)
                {
                    MessageBox.Show("Failed to update record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
                else
                {
                    dataChanged = true;

                    cmd.CommandText =
                        "update VESSELS set \n" +
                        "FLEET='" + MainForm.StrToSQLStr(newID) + "'\n" +
                        "where FLEET='" + MainForm.StrToSQLStr(_fleetID) + "'";

                    if (MainForm.cmdExecute(cmd)<0)
                    {
                        MessageBox.Show("Failed to update fleet code for the vessels. Please check fleet code manually.",
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                return true;
            }
        }

        private void lbEMails_DoubleClick(object sender, EventArgs e)
        {
            EditEmail();
        }

    }
}
