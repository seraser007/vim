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
    public partial class FrmDOCs : Form
    {
        DataSet DS;
        OleDbConnection connection;
        OleDbDataAdapter docAdapter;
        BindingSource bsDOCS = new BindingSource();

        public bool dataChanged = false;

        public FrmDOCs()
        {
            DS = MainForm.DS;
            connection = MainForm.connection;
            this.Font = MainForm.mainFont;
            this.Icon = MainForm.mainIcon;

            InitializeComponent();

            OleDbCommand cmd = new OleDbCommand("", connection);
            cmd.CommandText =
                "select * \n" +
                "from DOCS \n" +
                "order by DOC_ID";

            docAdapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("DOCS"))
                DS.Tables["DOCS"].Clear();

            docAdapter.Fill(DS, "DOCS");

            bsDOCS.DataSource = DS;
            bsDOCS.DataMember = "DOCS";

            adgvDOCs.DataSource = bsDOCS;

            adgvDOCs.Columns["ID"].Visible = false;

            adgvDOCs.Columns["DOC_ID"].HeaderText = "DOC Code";
            adgvDOCs.Columns["DOC_TEXT"].HeaderText = "DOC Description";

            adgvDOCs.Columns["DOC_ID"].FillWeight = 30;
            adgvDOCs.Columns["DOC_TEXT"].FillWeight = 70;

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmDOC form = new FrmDOC();

            form.docID = "";
            form.docText = "";
            form.recordID = 0;

            if (form.ShowDialog()==DialogResult.OK)
            {
                dataChanged = true;

                if (DS.Tables.Contains("DOCS"))
                    DS.Tables["DOCS"].Clear();

                docAdapter.Fill(DS, "DOCS");

                MainForm.LocateAdvGridRecord(form.docID, "DOC_ID", 1, adgvDOCs);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditDOC();
        }

        private void EditDOC()
        {
            if (adgvDOCs.Rows.Count == 0)
                return;

            FrmDOC form = new FrmDOC();

            form.docID = adgvDOCs.CurrentRow.Cells["DOC_ID"].Value.ToString();
            form.docText = adgvDOCs.CurrentRow.Cells["DOC_TEXT"].Value.ToString();
            form.recordID = Convert.ToInt32(adgvDOCs.CurrentRow.Cells["ID"].Value);

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.dataChanged)
                {
                    dataChanged = true;

                    int col = adgvDOCs.CurrentCell.ColumnIndex;
                    int row = adgvDOCs.CurrentCell.RowIndex;

                    if (DS.Tables.Contains("DOCS"))
                        DS.Tables["DOCS"].Clear();

                    docAdapter.Fill(DS, "DOCS");

                    adgvDOCs.CurrentCell = adgvDOCs[col, row];
                }
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (adgvDOCs.Rows.Count == 0)
                return;

            string code = adgvDOCs.CurrentRow.Cells["DOC_ID"].Value.ToString();
            string text = adgvDOCs.CurrentRow.Cells["DOC_TEXT"].Value.ToString();

            var rslt = MessageBox.Show("You are going to delete the following record:\n\n" +
                "DOC Code : " + code + "\n" +
                "DOC Decription : " + text + "\n\n" +
                "Would you like to proceed?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (rslt==DialogResult.Yes)
            {
                OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

                cmd.CommandText =
                    "delete from DOCS \n" +
                    "where DOC_ID='" + MainForm.StrToSQLStr(code) + "'";

                if (MainForm.cmdExecute(cmd)>=0)
                {
                    dataChanged = true;

                    cmd.CommandText =
                        "update VESSELS set \n" +
                        "DOC='' \n" +
                        "where DOC='" + MainForm.StrToSQLStr(code) + "'";

                    if (MainForm.cmdExecute(cmd)<0)
                    {
                        MessageBox.Show("Failed to update vessel information. Check DOC information manually.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Faled to delete record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void adgvDOCs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditDOC();
        }
    }
}
