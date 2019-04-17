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
    public partial class FrmFleets : Form
    {
        DataSet DS;
        OleDbConnection connection;
        OleDbDataAdapter fleetAdapter;
        OleDbCommand cmd;
        BindingSource bsFleetList = new BindingSource();

        public bool dataChanged = false;

        public FrmFleets(OleDbConnection mainConnection, DataSet mainDS, Font mainFont, Icon mainIcon)
        {
            DS = mainDS;
            connection = mainConnection;
            this.Font = mainFont;
            this.Icon = mainIcon;
            
            InitializeComponent();

            cmd = new OleDbCommand("", connection);
            cmd.CommandText =
                "select * \n" +
                "from FLEETS \n" +
                "order by FLEET_ID";

            fleetAdapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("FLEET_LIST"))
                DS.Tables["FLEET_LIST"].Clear();

            fleetAdapter.Fill(DS, "FLEET_LIST");

            bsFleetList.DataSource = DS;
            bsFleetList.DataMember = "FLEET_LIST";

            adgvFleetList.DataSource = bsFleetList;

            adgvFleetList.Columns["ID"].Visible = false;

            adgvFleetList.Columns["FLEET_ID"].HeaderText = "Fleet";
            adgvFleetList.Columns["FLEET_ID"].FillWeight = 20;

            adgvFleetList.Columns["FLEET_TEXT"].HeaderText = "Description";
            adgvFleetList.Columns["FLEET_TEXT"].FillWeight = 40;

            adgvFleetList.Columns["FLEET_EMAILS"].HeaderText = "Email";
            adgvFleetList.Columns["FLEET_EMAILS"].FillWeight = 60;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void adgvFleetList_FilterStringChanged(object sender, EventArgs e)
        {
            bsFleetList.Filter = adgvFleetList.FilterString;
        }

        private void adgvFleetList_SortStringChanged(object sender, EventArgs e)
        {
            bsFleetList.Sort = adgvFleetList.SortString;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmFleet form = new FrmFleet();

            form.fleetId = "";
            form.fleetText = "";
            form.recordID = 0;

            var rslt = form.ShowDialog();

            if (form.recordID>0)
            {
                dataChanged = true;

                if (DS.Tables.Contains("FLEET_LIST"))
                    DS.Tables["FLEET_LIST"].Clear();

                fleetAdapter.Fill(DS, "FLEET_LIST");

                MainForm.LocateAdvGridRecord(form.fleetId, "FLEET_ID", 1, adgvFleetList);
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (adgvFleetList.Rows.Count == 0)
                return;

            string fleetID=adgvFleetList.CurrentRow.Cells["FLEET_ID"].Value.ToString();

            var rslt = MessageBox.Show("You are going to delete record for the following fleet:\n\n" +
                "Fleet code : " + fleetID + "\n" +
                "Fleet description : " + adgvFleetList.CurrentRow.Cells["FLEET_TEXT"].Value.ToString() + "\n\n" +
                "Would you like to proceed?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (rslt==DialogResult.Yes)
            {
                OleDbCommand cmd = new OleDbCommand("", connection);

                cmd.CommandText =
                    "select Count(FLEET) as Recs \n" +
                    "from VESSELS \n" +
                    "where FLEET='" + MainForm.StrToSQLStr(fleetID) + "'";

                int recs = (int)cmd.ExecuteScalar();

                if (recs>0)
                {
                    rslt = MessageBox.Show("Code \"" + fleetID + "\" is in use. " +
                        "Would you like to delete it anyway?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (rslt == DialogResult.No)
                        return;

                    cmd.CommandText =
                        "update VESSELS set \n" +
                        "FLEET='' \n" +
                        "where FLEET='" + MainForm.StrToSQLStr(fleetID) + "'";

                    MainForm.cmdExecute(cmd);
                }

                cmd.CommandText =
                    "delete from FLEETS \n" +
                    "where FLEET_ID='" + MainForm.StrToSQLStr(fleetID) + "'";
                MainForm.cmdExecute(cmd);

                cmd.CommandText=
                    "delete from FLEET_EMAILS \n"+
                    "where FLEET_ID='" + MainForm.StrToSQLStr(fleetID) + "'";
                MainForm.cmdExecute(cmd);

                dataChanged = true;

                int col = adgvFleetList.CurrentCell.ColumnIndex;
                int row = adgvFleetList.CurrentCell.RowIndex;

                if (DS.Tables.Contains("FLEET_LIST"))
                    DS.Tables["FLEET_LIST"].Clear();

                fleetAdapter.Fill(DS, "FLEET_LIST");

                if (row >= adgvFleetList.Rows.Count)
                    adgvFleetList.CurrentCell = adgvFleetList[col, adgvFleetList.Rows.Count - 1];
                else
                    adgvFleetList.CurrentCell = adgvFleetList[col, row];
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditFleet();
        }

        private void EditFleet()
        {
            if (adgvFleetList.Rows.Count == 0)
                return;

            FrmFleet form = new FrmFleet();

            form.fleetId = adgvFleetList.CurrentRow.Cells["FLEET_ID"].Value.ToString();
            form.fleetText = adgvFleetList.CurrentRow.Cells["FLEET_TEXT"].Value.ToString();
            form.recordID = Convert.ToInt32(adgvFleetList.CurrentRow.Cells["ID"].Value);

            var rslt = form.ShowDialog();

            if (form.dataChanged)
            {
                dataChanged = true;

                int col = adgvFleetList.CurrentCell.ColumnIndex;
                int row = adgvFleetList.CurrentCell.RowIndex;

                if (DS.Tables.Contains("FLEET_LIST"))
                    DS.Tables["FLEET_LIST"].Clear();

                fleetAdapter.Fill(DS, "FLEET_LIST");

                adgvFleetList.CurrentCell = adgvFleetList[col, row];
            }
        }

        private void adgvFleetList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditFleet();
        }
    }
}
