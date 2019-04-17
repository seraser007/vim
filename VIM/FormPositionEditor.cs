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
    public partial class FormPositionEditor : Form
    {
        private Guid _positionGuid = MainForm.zeroGuid;
        private bool _wasChanged = false;
        private string _positionName = "";
        private int _positionIndex = 0;

        public bool wasChanged
        {
            get { return _wasChanged; }
        }

        public Guid positionGuid
        {
            get { return _positionGuid; }
            set { _positionGuid = value; }
        }

        public string positionName
        {
            get { return tbPositionName.Text.Trim(); }
            set { _positionName = value; tbPositionName.Text = _positionName; _wasChanged = false; }
        }

        public int positionIndex
        {
            get { return Convert.ToInt32(neIndex.Value); }
            set { _positionIndex = value; neIndex.Value = _positionIndex; _wasChanged = false; }
        }

        public FormPositionEditor()
        {
            this.Icon = MainForm.mainIcon;
            this.Font = MainForm.mainFont;

            InitializeComponent();
        }

        private void FormPositionEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
                return;

            if (_positionName.CompareTo(tbPositionName.Text.Trim()) != 0)
                _wasChanged = true;

            int anIndex = Convert.ToInt32(neIndex.Value);

            if (_positionIndex != anIndex)
                _wasChanged = true;

            if (!_wasChanged)
                return;

            if (NameExists())
            {
                MessageBox.Show("There is a record with the same name", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }

            if (anIndex<=0)
            {
                MessageBox.Show("Index should be more than zero", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }

            if (_positionGuid==MainForm.zeroGuid)
            {
                //New record
                OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

                Guid newID = new Guid();

                cmd.CommandText =
                    "INSERT INTO CREW_POSITIONS (CREW_POSITION_GUID, POSITION_NAME, POSITION_INDEX) \n"+
                    "values("+MainForm.GuidToStr(newID)+",'"+
                    MainForm.StrToSQLStr(tbPositionName.Text)+"',"+
                    anIndex.ToString()+")";

                if (MainForm.cmdExecute(cmd)<0)
                {
                    MessageBox.Show("Failed to save new record", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }

                _positionGuid = newID;
            }
            else
            {
                OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

                cmd.CommandText =
                    "update CREW_POSITIONS set \n" +
                    "POSITION_NAME='" + MainForm.StrToSQLStr(tbPositionName.Text) + "', \n" +
                    "POSITION_INDEX=" + anIndex.ToString() + " \n" +
                    "where CREW_POSITION_GUID=" + MainForm.GuidToStr(_positionGuid);

                if (MainForm.cmdExecute(cmd)<0)
                {
                    MessageBox.Show("Failed to update position record", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
            }
        }

        private bool NameExists()
        {
            OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

            cmd.CommandText =
                "select Count(CREW_POSITION_GUID) as RecCount \n" +
                "from CREW_POSITIONS \n" +
                "where POSITION_NAME='" + MainForm.StrToSQLStr(tbPositionName.Text) + "' \n" +
                "and CREW_POSITION_GUID<>" + MainForm.GuidToStr(_positionGuid);

            int count = (int)cmd.ExecuteScalar();

            return count > 0;
        }
    }
}
