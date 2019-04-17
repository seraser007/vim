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
    public partial class FrmOffice : Form
    {
        private string _officeID = "";

        public string officeID
        {
            get { return tbOfficeCode.Text.Trim(); }
            set { _officeID = value;  tbOfficeCode.Text = value; }
        }

        private string _officeText = "";

        public string officeText
        {
            get { return tbOfficeText.Text.Trim(); }
            set { _officeText = value; tbOfficeText.Text = value; }
        }

        private int _recordID = 0;

        public int recordID
        {
            get { return _recordID; }
            set { _recordID = value; }
        }

        public bool needUpdate = false;

        public FrmOffice()
        {
            this.Font = MainForm.mainFont;
            this.Icon = MainForm.mainIcon;

            InitializeComponent();
        }

        private void FrmOffice_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
                return;

            string newID=tbOfficeCode.Text.Trim();
            string newText=tbOfficeText.Text.Trim();

            if (_officeID == newID && _officeText == newText)
                return;

            if (newID.Length==0)
            {
                MessageBox.Show("Please provide office code or use Cancel button to close the form",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }

            OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

            if (_officeID != newID)
            {
                //Check code in use

                cmd.CommandText =
                    "select Count(OFFICE_ID) as Recs \n" +
                    "from OFFICES \n" +
                    "where OFFICE_ID='" + MainForm.StrToSQLStr(newID) + "' \n" +
                    "and ID<>" + _recordID.ToString();

                int recs = (int)cmd.ExecuteScalar();

                if (recs>0)
                {
                    MessageBox.Show("There ia an office with this code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
            }

            if (_recordID==0)
            {
                //New record
                cmd.CommandText =
                    "insert into OFFICES (OFFICE_ID, OFFICE_TEXT) \n" +
                    "values('" + MainForm.StrToSQLStr(newID) + "','" +
                        MainForm.StrToSQLStr(newText) + "')";

                if (MainForm.cmdExecute(cmd)>=0)
                    needUpdate = true;
                else
                {
                    MessageBox.Show("Failed to create new record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                //Update record
                cmd.CommandText =
                    "update OFFICES set \n" +
                    "OFFICE_ID='" + MainForm.StrToSQLStr(newID) + "',\n" +
                    "OFFICE_TEXT='" + MainForm.StrToSQLStr(newText) + "' \n" +
                    "where ID=" + _recordID.ToString();

                if (MainForm.cmdExecute(cmd)>=0)
                {
                    needUpdate = true;

                    if (_officeID!=newID)
                    {
                        cmd.CommandText =
                            "update VESSELS set \n" +
                            "OFFICE='" + MainForm.StrToSQLStr(newID) + "' \n" +
                            "where OFFICE='" + MainForm.StrToSQLStr(_officeID) + "'";

                        if (MainForm.cmdExecute(cmd)<0)
                        {
                            MessageBox.Show("Failed to update office code to the new one in the table of vessels.\n" +
                                "Please check correct office code for the vessels manually", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Failed to update record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
            }

        }


    }
}
