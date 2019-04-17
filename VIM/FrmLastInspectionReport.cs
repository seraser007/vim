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
    public partial class FrmLastInspectionReport : Form
    {
        DataSet DS;
        OleDbConnection connection;
        public FrmLastInspectionReport()
        {
            this.Icon = MainForm.mainIcon;
            this.Font = MainForm.mainFont;
            DS = MainForm.DS;
            connection = MainForm.connection;

            InitializeComponent();

            clbDOCs.Items.Clear();

            if (DS.Tables.Contains("DOC_LIST"))
            {
                DataTable table = DS.Tables["DOC_LIST"];

                DataRow[] rows = table.Select();

                foreach (DataRow row in rows)
                {
                    clbDOCs.Items.Add(row["DOC_ID"].ToString());
                }
            }
        }

        private void FrmLastInspectionReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult==DialogResult.OK)
            {
                DataRow[] rows = DS.Tables["DOC_LIST"].Select();

                for (int i=0; i<clbDOCs.Items.Count;i++)
                {
                    if (clbDOCs.GetItemCheckState(i)==CheckState.Checked)
                    {
                        rows[i]["SELECTED"] = 1;
                    }
                }
            }
        }

        private void miSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbDOCs.Items.Count; i++ )
            {
                clbDOCs.SetItemCheckState(i, CheckState.Checked);
            }
        }

        private void miResetAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbDOCs.Items.Count; i++ )
            {
                clbDOCs.SetItemCheckState(i, CheckState.Unchecked);
            }
        }
    }
}
