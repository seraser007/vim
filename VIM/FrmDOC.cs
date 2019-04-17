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
    public partial class FrmDOC : Form
    {
        public bool dataChanged = false;

        private string _docID = "";
        
        public string docID
        {
            get { return tbDOCCode.Text.Trim(); }
            set { _docID = value; tbDOCCode.Text = _docID; }
        }

        private string _docText = "";

        public string docText
        {
            get { return tbDOCText.Text.Trim(); }
            set { _docText = value; tbDOCText.Text = _docText; }
        }

        private int _recordID = 0;

        public int recordID
        {
            get { return _recordID; }
            set { _recordID = value; }
        }

        public FrmDOC()
        {
            this.Font = MainForm.mainFont;
            this.Icon = MainForm.mainIcon;

            InitializeComponent();
        }

        private void FrmDOC_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
                return;

            string newID = tbDOCCode.Text.Trim();
            string newText = tbDOCText.Text.Trim();

            if (newID.Length==0)
            {
                MessageBox.Show("Please provide DOC code or click Cancel button to close the form.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }

            if (_docID == newID && _docText == newText)
                return;

            if (!SaveData())
            {
                e.Cancel = true;
                return;
            }
        }

        private bool SaveData()
        {
            OleDbCommand cmd = new OleDbCommand("", MainForm.connection);
            string newID=tbDOCCode.Text.Trim();
            string newText=tbDOCText.Text.Trim();

            if (CodeExists()) return false;

            if (_recordID == 0)
            {
                //Create record

                cmd.CommandText =
                    "insert into DOCS (DOC_ID, DOC_TEXT) \n" +
                    "values('" + MainForm.StrToSQLStr(newID) + "','" +
                        MainForm.StrToSQLStr(newText) + "')";

                if (MainForm.cmdExecute(cmd)>=0)
                {
                    dataChanged = true;
                    return true;
                }
                else
                {
                    MessageBox.Show("Failed to create new DOC record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                //Update record

                cmd.CommandText =
                    "update DOCS set \n" +
                    "DOC_ID='" + MainForm.StrToSQLStr(newID) + "', \n" +
                    "DOC_TEXT='" + MainForm.StrToSQLStr(newText) + "' \n" +
                    "where ID=" + _recordID.ToString();

                if (MainForm.cmdExecute(cmd)>=0)
                {
                    dataChanged = true;

                    cmd.CommandText =
                        "update VESSELS set \n" +
                        "DOC='" + MainForm.StrToSQLStr(newID) + "' \n" +
                        "where DOC='" + MainForm.StrToSQLStr(_docID) + "'";

                    if (MainForm.cmdExecute(cmd)<0)
                        MessageBox.Show("Failed to update vessels information. Please check DOC for vessels manually",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return true;
                }
                else
                {
                    MessageBox.Show("Failed to update DOC record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            
        }

        private bool CodeExists()
        {
            OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

            cmd.CommandText =
                "select Count(DOC_ID) as Recs \n" +
                "from DOCS \n" +
                "where DOC_ID='" + MainForm.StrToSQLStr(tbDOCCode.Text.Trim()) + "' \n" +
                "and ID<>" + _recordID.ToString();

            int recs = (int)cmd.ExecuteScalar();

            if (recs>0)
            {
                MessageBox.Show("There is a record with the same code. Please provide another code.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return true;
            }

            return false;
        }
    }
}
