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
    public partial class FormMemorandums : Form
    {
        public DataSet DS;
        public OleDbConnection connection;

        public FormMemorandums()
        {
            this.Font = MainForm.mainFont;
            this.Icon = MainForm.mainIcon;

            DS = MainForm.DS;
            connection=MainForm.connection;

            InitializeComponent();

            FillList();
        }

        private void FillList()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select * \n" +
                "from MEMORANDUMS \n" +
                "order by MEMORANDUM_TYPE";

            OleDbDataAdapter da = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("MEMORANDUMS"))
                DS.Tables["MEMORANDUMS"].Clear();

            da.Fill(DS, "MEMORANDUMS");

            dgvMemorandums.DataSource = DS;
            dgvMemorandums.DataMember = "MEMORANDUMS";

            dgvMemorandums.Columns["MEMORANDUM_ID"].Visible = false;
            dgvMemorandums.Columns["MEMORANDUM_TYPE"].HeaderText = "Memorandum";


        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (!MainForm.isPowerUser)
                return;

            FormMemorandum form = new FormMemorandum();

            if (form.ShowDialog()==DialogResult.OK)
            {
                OleDbCommand cmd = new OleDbCommand("", connection);

                cmd.CommandText =
                    "insert into MEMORANDUMS (MEMORANDUM_TYPE) \n" +
                    "values('" + MainForm.StrToSQLStr(form.memorandumText) + "')";

                if (MainForm.cmdExecute(cmd)<0)
                {
                    MessageBox.Show("Failed to create new record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                FillList();
                
                MainForm.LocateGridRecord(form.memorandumText, "MEMORANDUM_TYPE", 1, dgvMemorandums);
            }
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            EditRecord();
        }

        private void EditRecord()
        {
            if (!MainForm.isPowerUser)
                return;

            if (dgvMemorandums.Rows.Count == 0)
                return;

            FormMemorandum form = new FormMemorandum();

            int id=Convert.ToInt32(dgvMemorandums.CurrentRow.Cells["MEMORANDUM_ID"].Value);
            string mem = dgvMemorandums.CurrentRow.Cells["MEMORANDUM_TYPE"].Value.ToString();

            form.memorandumId = id;
            form.memorandumText = mem;

            if (form.ShowDialog()==DialogResult.OK)
            {
                if (string.Compare(mem,form.memorandumText)!=0)
                {
                    OleDbCommand cmd = new OleDbCommand("", connection);

                    cmd.CommandText =
                        "update MEMORANDUMS set \n" +
                        "MEMORANDUM_TYPE='" + MainForm.StrToSQLStr(form.memorandumText) + "' \n" +
                        "where MEMORANDUM_ID=" + id.ToString();

                    if (MainForm.cmdExecute(cmd)<0)
                    {
                        MessageBox.Show("Failed to update record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }

                    FillList();

                    MainForm.LocateGridRecord(form.memorandumText.Trim(), "MEMORANDUM_TYPE", 1, dgvMemorandums);
                }
            }

        }

        private void dgvMemorandums_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditRecord();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!MainForm.isPowerUser)
                return;

            if (dgvMemorandums.Rows.Count == 0)
                return;

            string txt = "You are going to delete the following memorandum record: \n\n" +
                dgvMemorandums.CurrentRow.Cells["MEMORANDUM_TYPE"].Value.ToString() + "\n\n" +
                "Would you like to proceed?";

            var rslt = MessageBox.Show(txt, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (rslt==DialogResult.Yes)
            {
                if (CheckInUse())
                {
                    MessageBox.Show("Seleted record is in use. You are unable to delete it.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    return;
                }

                OleDbCommand cmd = new OleDbCommand("", connection);

                cmd.CommandText =
                    "delete from MEMORANDUMS \n" +
                    "where MEMORANDUM_ID=" + dgvMemorandums.CurrentRow.Cells["MEMORANDUM_ID"].Value.ToString();

                if (MainForm.cmdExecute(cmd)<0)
                {
                    MessageBox.Show("Failed to delete selected record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                FillList();
            }
        }

        private bool CheckInUse()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select Count(MEMORANDUM_ID) as RecCount \n" +
                "from REPORTS \n" +
                "where \n" +
                "MEMORANDUM_ID=" + dgvMemorandums.CurrentRow.Cells["MEMORANDUM_ID"].Value.ToString();

            int count = (int)cmd.ExecuteScalar();

            if (count == 0)
                return false;
            else
                return true;
        }
    }
}
