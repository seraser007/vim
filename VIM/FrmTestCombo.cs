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
    public partial class FrmTestCombo : Form
    {
        OleDbConnection connection;
        DataSet DS;
        OleDbDataAdapter adpt=new OleDbDataAdapter();
        DataTable dt = new DataTable();

        public FrmTestCombo(OleDbConnection mainConnection, DataSet mainDS)
        {
            connection = mainConnection;
            DS = mainDS;

            InitializeComponent();

            //updateList();

            
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select INSPECTOR_GUID, INSPECTOR_NAME \n" +
                "from INSPECTORS \n" +
                "order by INSPECTOR_NAME";
            
            //adpt = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("TEST_COMBO"))
                DS.Tables["TEST_COMBO"].Clear();

            adpt.SelectCommand = cmd;
            adpt.Fill(DS, "TEST_COMBO");

            dt = DS.Tables["TEST_COMBO"];
            comboBox1.DataSource = dt.DefaultView;
            comboBox1.DisplayMember = "INSPECTOR_NAME";
            comboBox1.ValueMember = "INSPECTOR_GUID";

            ComboBoxExt1.DataSource = DS.Tables["TEST_COMBO"];
            ComboBoxExt1.DisplayMember = "INSPECTOR_NAME";
            ComboBoxExt1.ValueMember = "INSPECTOR_GUID";
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            //updateList();
            if (comboBox1.SelectedIndex == -1)
            {
                dt.DefaultView.RowFilter = "INSPECTOR_NAME LIKE '%" + comboBox1.Text + "%'";
            }
        }

        private void updateList()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            if (comboBox1.Text.Length==0)
            {
                cmd.CommandText =
                    "select INSPECTOR_GUID, INSPECTOR_NAME \n" +
                    "from INSPECTORS \n" +
                    "order by INSPECTOR_NAME";
            }
            else
            {
                cmd.CommandText =
                    "select INSPECTOR_GUID, INSPECTOR_NAME \n" +
                    "from INSPECTORS \n" +
                    "where INSPECTOR_NAME like '%" + comboBox1.Text + "%' \n" +
                    "order by INSPECTOR_NAME";
            }

            adpt.SelectCommand = cmd;

            if (DS.Tables.Contains("TEST_FILTER_COMBO"))
                DS.Tables["TEST_FILTER_COMBO"].Clear();

            adpt.Fill(DS, "TEST_FILTER_COMBO");

            var source = new AutoCompleteStringCollection();

            DataTable tbl = DS.Tables["TEST_FILTER_COMBO"];

            DataRow[] rows = tbl.Select();

            foreach(DataRow row in rows)
            {
                source.Add(row["INSPECTOR_NAME"].ToString());
            }

            comboBox1.AutoCompleteCustomSource = source;

        }
    }
}
