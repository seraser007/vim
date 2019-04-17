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
    public partial class FrmOffices : Form
    {
        OleDbConnection connection;
        DataSet DS;
        OleDbDataAdapter officesAdapter;
        BindingSource bsOffices = new BindingSource();

        public bool dataChanged = false;

        public FrmOffices()
        {
            DS = MainForm.DS;
            connection = MainForm.connection;
            this.Icon = MainForm.mainIcon;
            this.Font = MainForm.mainFont;
            
            InitializeComponent();

            OleDbCommand cmd = new OleDbCommand("", connection);
            cmd.CommandText =
                "select * \n" +
                "from OFFICES \n" +
                "order by OFFICE_ID";

            officesAdapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("OFFICES"))
                DS.Tables["OFFICES"].Clear();

            officesAdapter.Fill(DS, "OFFICES");

            bsOffices.DataSource = DS;
            bsOffices.DataMember = "OFFICES";

            adgvOffices.DataSource = bsOffices;

            adgvOffices.Columns["ID"].Visible = false;

            adgvOffices.Columns["OFFICE_ID"].HeaderText = "Office code";
            adgvOffices.Columns["OFFICE_ID"].FillWeight = 30;

            adgvOffices.Columns["OFFICE_TEXT"].HeaderText = "Description";
            adgvOffices.Columns["OFFICE_TEXT"].FillWeight = 70;

        }

        private void adgvOffices_FilterStringChanged(object sender, EventArgs e)
        {
            bsOffices.Filter = adgvOffices.FilterString;
        }

        private void adgvOffices_SortStringChanged(object sender, EventArgs e)
        {
            bsOffices.Sort = adgvOffices.SortString;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmOffice form = new FrmOffice();

            form.officeID = "";
            form.officeText = "";

            if (form.ShowDialog()==DialogResult.OK)
            {
                if (DS.Tables.Contains("OFFICES"))
                    DS.Tables["OFFICES"].Clear();

                officesAdapter.Fill(DS, "OFFICES");

                string id = form.officeID;

                MainForm.LocateAdvGridRecord(id, "OFFICE_ID", 1, adgvOffices);

                dataChanged = true;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditOffice();
        }

        private void EditOffice()
        {
            if (adgvOffices.Rows.Count == 0)
                return;

            FrmOffice form = new FrmOffice();

            form.officeID = adgvOffices.CurrentRow.Cells["OFFICE_ID"].Value.ToString();
            form.officeText = adgvOffices.CurrentRow.Cells["OFFICE_TEXT"].Value.ToString();
            form.recordID = Convert.ToInt32(adgvOffices.CurrentRow.Cells["ID"].Value);

            var rslt = form.ShowDialog();

            if (rslt == DialogResult.OK && form.needUpdate)
            {
                int col = adgvOffices.CurrentCell.ColumnIndex;
                int row = adgvOffices.CurrentCell.RowIndex;

                if (DS.Tables.Contains("OFFICES"))
                    DS.Tables["OFFICES"].Clear();

                officesAdapter.Fill(DS, "OFFICES");

                adgvOffices.CurrentCell = adgvOffices[col, row];

                dataChanged = true;
            }

        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (adgvOffices.Rows.Count == 0)
                return;

            string code = adgvOffices.CurrentRow.Cells["OFFICE_ID"].Value.ToString();
            string text = adgvOffices.CurrentRow.Cells["OFFICE_TEXT"].Value.ToString();

            var rslt = MessageBox.Show("You are going to delete record for the following office:\n\n" +
                "Office code : " + code + "\n" +
                "Office description : " + text + "\n\n" +
                "Would you like to proceed?",
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (rslt==DialogResult.Yes)
            {
                OleDbCommand cmd = new OleDbCommand("", connection);
                cmd.CommandText =
                    "delete from OFFICES \n" +
                    "where OFFICE_ID='" + MainForm.StrToSQLStr(code) + "'";

                if (MainForm.cmdExecute(cmd)>=0)
                {
                    dataChanged = true;

                    cmd.CommandText =
                        "update VESSELS set \n" +
                        "OFFICE='' \n" +
                        "where OFFICE='" + MainForm.StrToSQLStr(code) + "'";

                    if (MainForm.cmdExecute(cmd)<0)
                    {
                        MessageBox.Show("Failed to clear vessels table. Please check for correct office code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        int col = adgvOffices.CurrentCell.ColumnIndex;
                        int row = adgvOffices.CurrentCell.RowIndex;

                        if (DS.Tables.Contains("OFFICES"))
                            DS.Tables["OFFICES"].Clear();

                        officesAdapter.Fill(DS, "OFFICES");

                        if (adgvOffices.Rows.Count > 0)
                        {
                            if (row >= adgvOffices.Rows.Count)
                                adgvOffices.CurrentCell = adgvOffices[col, adgvOffices.Rows.Count - 1];
                            else
                                adgvOffices.CurrentCell = adgvOffices[col, row];
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Failed to delete office record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void adgvOffices_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditOffice();
        }
    }
}
