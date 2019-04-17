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
    public partial class FormInspectionTypes : Form
    {
        DataSet DS;
        OleDbConnection connection;

        public FormInspectionTypes()
        {
            this.Font = MainForm.mainFont;
            this.Icon = MainForm.mainIcon;
            DS = MainForm.DS;
            connection = MainForm.connection;

            InitializeComponent();

            FillTypes();
        }

        private void FillTypes()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select * \n" +
                "from INSPECTION_TYPES \n" +
                "order by INSPECTION_TYPE";

            OleDbDataAdapter da=new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("INSPECTION_TYPES"))
                DS.Tables["INSPECTION_TYPES"].Clear();

            da.Fill(DS,"INSPECTION_TYPES");

            dgvInspectionTypes.DataSource=DS;
            dgvInspectionTypes.DataMember="INSPECTION_TYPES";

            dgvInspectionTypes.Columns["INSPECTION_TYPE_ID"].Visible=false;
            dgvInspectionTypes.Columns["INSPECTION_TYPE"].HeaderText="Inspection type";
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (!MainForm.isPowerUser)
                return;

            FormInspectionType form = new FormInspectionType();

            if (form.ShowDialog() == DialogResult.OK)
            {
                OleDbCommand cmd = new OleDbCommand("", connection);

                cmd.CommandText =
                    "insert into INSPECTION_TYPES (INSPECTION_TYPE) \n" +
                    "values('" + MainForm.StrToSQLStr(form.inspectionType) + "')";

                if (MainForm.cmdExecute(cmd)>=0)
                {
                    FillTypes();

                    MainForm.LocateGridRecord(form.inspectionType, "INSPECTION_TYPE", 1, dgvInspectionTypes);
                }
                else
                {
                    MessageBox.Show("Failed to create new record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditRecord();
        }

        private void EditRecord()
        {
            if (!MainForm.isPowerUser)
                return;

            if (dgvInspectionTypes.Rows.Count == 0)
                return;

            FormInspectionType form = new FormInspectionType();

            form.inspectionType = dgvInspectionTypes.CurrentRow.Cells["INSPECTION_TYPE"].Value.ToString();

            int id = Convert.ToInt32(dgvInspectionTypes.CurrentRow.Cells["INSPECTION_TYPE_ID"].Value);

            form.InspectionTypeID = id;

            if (form.ShowDialog() == DialogResult.OK)
            {
                OleDbCommand cmd = new OleDbCommand("", connection);

                cmd.CommandText =
                    "update INSPECTION_TYPES set \n" +
                    "INSPECTION_TYPE='" + MainForm.StrToSQLStr(form.inspectionType) + "' \n" +
                    "where \n" +
                    "INSPECTION_TYPE_ID=" + id.ToString();

                if (MainForm.cmdExecute(cmd)>=0)
                {
                    FillTypes();

                    MainForm.LocateGridRecord(form.inspectionType, "INSPECTION_TYPE_ID", 1, dgvInspectionTypes);
                }
                else
                {
                    MessageBox.Show("Failed to update record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!MainForm.isPowerUser)
                return;

            if (dgvInspectionTypes.Rows.Count == 0)
                return;

            string txt = "You are going to delete the following inspection type:\n\n" +
                dgvInspectionTypes.CurrentRow.Cells["INSPECTION_TYPE"].Value.ToString() + "\n\n" +
                "Would you like to proceed?";

            var rslt = MessageBox.Show(txt, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (rslt==DialogResult.Yes)
            {
                if (CheckTypeIsUsed())
                {
                    MessageBox.Show("Selected type is in use. You are unable to delete it.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                OleDbCommand cmd = new OleDbCommand("", connection);

                cmd.CommandText =
                    "delete from INSPECTION_TYPES \n" +
                    "where \n" +
                    "INSPECTION_TYPE_ID=" + dgvInspectionTypes.CurrentRow.Cells["INSPECTION_TYPE_ID"].Value.ToString();

                if (MainForm.cmdExecute(cmd)<0)
                {
                    MessageBox.Show("Failed to delete selected record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                FillTypes();
            }
        }

        private bool CheckTypeIsUsed()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select Count(INSPECTION_TYPE_ID) as RecCount \n" +
                "from REPORTS \n" +
                "where \n" +
                "INSPECTION_TYPE_ID=" + dgvInspectionTypes.CurrentRow.Cells["INSPECTION_TYPE_ID"].Value.ToString();

            int count = (int)cmd.ExecuteScalar();

            if (count == 0)
                return false;
            else
                return true;
        }

        private void dgvInspectionTypes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditRecord();
        }
    }
}
