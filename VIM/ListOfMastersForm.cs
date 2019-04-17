using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace VIM
{
    public partial class ListOfMastersForm : Form
    {
        OleDbConnection connection;
        DataSet DS;
        OleDbCommand cmd;
        BindingSource bsMasters = new BindingSource();

        public ListOfMastersForm()
        {
            this.Font = MainForm.mainFont;
            this.Icon = MainForm.mainIcon;

            InitializeComponent();

            this.Cursor = Cursors.WaitCursor;

            DS = MainForm.DS;
            connection = MainForm.connection;

            cmd = new OleDbCommand("", connection);

            fillMasters();

            bsMasters.DataSource = DS;
            bsMasters.DataMember = "CREW";

            adgvMasters.DataSource = bsMasters;

            adgvMasters.AutoGenerateColumns = true;

            adgvMasters.Columns["CREW_GUID"].Visible = false;
            
            adgvMasters.Columns["CREW_NAME"].HeaderText = "Name";
            adgvMasters.Columns["CREW_NAME"].FillWeight = 50;
            //adgvMasters.Columns["MASTER_NAME"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            adgvMasters.Columns["CREW_POSITION_GUID"].Visible = false;

            adgvMasters.Columns["POSITION_NAME"].HeaderText = "Position";
            adgvMasters.Columns["POSITION_NAME"].FillWeight = 30;

            adgvMasters.Columns["PERSONAL_ID"].HeaderText = "Personal ID";
            adgvMasters.Columns["PERSONAL_ID"].FillWeight = 20;
            //adgvMasters.Columns["PERSONAL_ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            
            adgvMasters.Columns["CREW_NOTES"].Visible = false;
            
            adgvMasters.Columns["REPORT_COUNT"].HeaderText = "Inspections";
            adgvMasters.Columns["REPORT_COUNT"].FillWeight = 20;
            //adgvMasters.Columns["REPORT_NUMBER"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            
            adgvMasters.Columns["OBS_COUNT"].HeaderText = "Observations";
            adgvMasters.Columns["OBS_COUNT"].FillWeight = 20;
            //adgvMasters.Columns["OBS_NUMBER"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            CountRecords();

            ProtectFields(MainForm.isPowerUser);
        }

        private void ProtectFields(bool status)
        {
            btnNew.Enabled = status;
            btnDelete.Enabled = status;

            if (status)
            {
                btnEdit.Text = "Edit";
                btnEdit.Image = VIM.Properties.Resources.edit;
            }
            else
            {
                btnEdit.Text = "View";
                btnEdit.Image = VIM.Properties.Resources.normal_view;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Add new master

            CrewDetailsForm form = new CrewDetailsForm();

            var rslt = form.ShowDialog();

            if (rslt == DialogResult.OK)
            {

                fillMasters();

                String searchValue = form.crewName;

                int rowIndex = -1;

                foreach (DataGridViewRow dgRow in adgvMasters.Rows)
                {
                    if (dgRow.Cells["CREW_NAME"].Value.ToString().Equals(searchValue))
                    {
                        rowIndex = dgRow.Index;
                        break;
                    }
                }

                if (rowIndex >= 0)
                {
                    adgvMasters.CurrentCell = adgvMasters[1, rowIndex];
                }
            }
        }

        private void fillMasters()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            string cmdText =
                "select CREW.CREW_GUID, CREW.CREW_NAME, CREW.CREW_POSITION_GUID, "+
                "CREW_POSITIONS.POSITION_NAME, CREW.CREW_NOTES, CREW.PERSONAL_ID, "+
                "REPORT_COUNT, OBS_COUNT \n" +
                "from (CREW left join  \n" +
                "(select Q01.CREW_GUID as CRW_ID, REPORT_COUNT, OBS_COUNT  \n" +
                "from  \n" +
                "(select CREW_GUID, COUNT(REPORT_CODE) as REPORT_COUNT  \n" +
                "from REPORTS_CREW  \n" +
                "group by CREW_GUID) as Q01  \n" +
                "left join  \n" +
                "(select CREW_GUID, SUM(OBS_COUNT)as OBS_COUNT \n"+
                "from REPORTS inner join REPORTS_CREW \n" +
                "on REPORTS.REPORT_CODE=REPORTS_CREW.REPORT_CODE \n"+
                "group by CREW_GUID) as Q02  \n" +
                "on Q01.CREW_GUID=Q02.CREW_GUID) as Q1  \n" +
                "on CREW.CREW_GUID=Q1.CRW_ID)  \n" +
                "left join CREW_POSITIONS \n"+
                "on CREW.CREW_POSITION_GUID=CREW_POSITIONS.CREW_POSITION_GUID \n"+
                "order by CREW_NAME";

            OleDbDataAdapter crewAdapter = new OleDbDataAdapter(cmdText, connection);


            if (DS.Tables.Contains("CREW"))
                DS.Tables["CREW"].Clear();


            crewAdapter.Fill(DS, "CREW");

            cmd.CommandText =
                "select * from \n" +
                "(select CREW_GUID, CREW_NAME, CREW_NOTES, PERSONAL_ID \n" +
                "from CREW \n" +
                "union \n" +
                "select "+MainForm.GuidToStr(MainForm.zeroGuid)+" as CREW_GUID, '' as CREW_NAME, '' as CREW_NOTES, '' as PERSONAL_ID \n" +
                "from Fonts) \n" +
                "order by CREW_NAME";

            OleDbDataAdapter crewListAdapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("CREW_LIST"))
                DS.Tables["CREW_LIST"].Clear();

            crewListAdapter.Fill(DS, "CREW_LIST");

            DataTable masters = DS.Tables["CREW_LIST"];

            cbeFind.DataSource = masters;
            cbeFind.DisplayMember = "CREW_NAME";
            cbeFind.ValueMember = "CREW_GUID";


            CountRecords();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Edit Master
            editMaster();
        }

        private void editMaster()
        {
            if (adgvMasters.Rows.Count == 0)
                return;

            Guid crewGuid = MainForm.StrToGuid(adgvMasters.CurrentRow.Cells["CREW_GUID"].Value.ToString());

            CrewDetailsForm form = new CrewDetailsForm();

            form.crewGuid = crewGuid;

            var rslt = form.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                if (form.wasChanged)
                {
                    int visibleRow = adgvMasters.FirstDisplayedScrollingRowIndex;
                    int curRow = adgvMasters.CurrentCell.RowIndex;
                    int curCol = adgvMasters.CurrentCell.ColumnIndex;

                    fillMasters();

                    adgvMasters.FirstDisplayedScrollingRowIndex = visibleRow;

                    adgvMasters.CurrentCell = adgvMasters[curCol, curRow];

                }


            }


        }
        
        private void btnFind_Click(object sender, EventArgs e)
        {
            //Find selected Master
            locateRecord();
        }

        private void locateRecord()
        {
            String searchValue = cbeFind.Text;

            int rowIndex = -1;

            foreach (DataGridViewRow dgRow in adgvMasters.Rows)
            {
                if (dgRow.Cells["CREW_NAME"].Value.ToString().Equals(searchValue))
                {
                    rowIndex = dgRow.Index;
                    break;
                }
            }

            if (rowIndex >= 0)
            {
                //dataGridView1.Rows[rowIndex].Selected = true;
                adgvMasters.CurrentCell = adgvMasters[1, rowIndex];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string StrToSQLStr(string Text)
        {
            return Text.Replace("'", "''");
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Delete master
            if (adgvMasters.Rows.Count == 0)
                return;

            int rowIndex = adgvMasters.CurrentRow.Index;
            int visibleRow = adgvMasters.FirstDisplayedCell.RowIndex;

            if (adgvMasters.SelectedRows.Count < 2)
            {
                string masterName = adgvMasters.CurrentRow.Cells["MASTER_NAME"].Value.ToString();

                string msg = "You are going to delete record for the following captain:\n\n" +
                    "Name : \"" + masterName + "\"\n" +
                    "Personal ID : \"" + adgvMasters.CurrentRow.Cells["PERSONAL_ID"].Value.ToString() + "\"\n\n" +
                    "Would you like to proceed?";

                var rslt = MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (rslt == DialogResult.Yes)
                {
                    //Проверяем возможность удаления

                    OleDbCommand cmd = new OleDbCommand("", connection);

                    Guid masterGuid = MainForm.StrToGuid(adgvMasters.CurrentRow.Cells["CREW_GUID"].Value.ToString());

                    cmd.CommandText =
                        "select Count(CREW_GUID) \n" +
                        "from REPORTS_CREW \n" +
                        "where CREW_GUID=" + MainForm.GuidToStr(masterGuid);

                    int recCount = (int)cmd.ExecuteScalar();

                    if (recCount > 0)
                    {
                        if (recCount == 1)
                            msg = "There is an inspection assosiated with captain "+masterName+".\n" +
                                "Would you like to delete records for this captain anyway?";
                        else
                            msg = "There are " + recCount.ToString() + " inspections assosiated with captain "+masterName+".\n" +
                                "Would you like to delete records for this captain anyway?";

                        var rs1 = MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                        if (rs1 == DialogResult.No)
                            return;

                        msg = "All inspections assosiated with captain "+masterName+" will have not any information about the master.";

                        MessageBox.Show(msg);

                    }

                    OleDbTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        cmd.Transaction = transaction;

                        cmd.CommandText =
                            "delete from CREW " +
                            "where CREW_GUID=" + MainForm.GuidToStr(masterGuid);
                        
                        if (MainForm.cmdExecute(cmd)>=0)
                        

                        cmd.CommandText =
                            "update REPORTS_CREW set \n" +
                            "CREW_GUID=Null \n" +
                            "where CREW_GUID=" + MainForm.GuidToStr(masterGuid);
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {

                    }

                    MessageBox.Show("All records for captain " + masterName + " were deleted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    fillMasters();

                    if (adgvMasters.Rows.Count > 0)
                    {
                        adgvMasters.FirstDisplayedScrollingRowIndex = visibleRow;

                        if (adgvMasters.Rows.Count <= rowIndex)
                        {
                            adgvMasters.CurrentCell = adgvMasters[1, adgvMasters.Rows.Count - 1];
                        }
                        else
                        {
                            adgvMasters.CurrentCell = adgvMasters[1, rowIndex];
                        }
                    }
                }
            }
            else
            {
                string msg = "You are going to delete records for the following captains:\n\n";
                int mastersSelected = adgvMasters.SelectedRows.Count;
                int masterCounter = 0;


                for (int i = mastersSelected-1; i >= 0; i--)
                {
                    if (mastersSelected<14)
                    {
                        msg = msg + adgvMasters.SelectedRows[i].Cells["CREW_NAME"].Value.ToString() + "\n";
                    }
                    else
                    {
                        if (masterCounter<10)
                        {
                            msg = msg + adgvMasters.SelectedRows[i].Cells["CREW_NAME"].Value.ToString() + "\n";
                        }
                    }

                    masterCounter++;
                }

                if (mastersSelected>=14)
                {
                    msg = msg + "...\nTotal : " + mastersSelected.ToString() + " records\n";
                }
                
                msg=msg+"\nWould you like to proceed?";

                var rslt = MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (rslt == DialogResult.Yes)
                {
                    int delCounter = 0;
                    int remainCounter = 0;

                    string delMasters = "";
                    string remainMasters = "";

                    this.Cursor = Cursors.WaitCursor;

                    try
                    {
                        for (int i = adgvMasters.SelectedRows.Count - 1; i >= 0; i--)
                        {
                            OleDbCommand cmd = new OleDbCommand("", connection);
                            string masterName = adgvMasters.SelectedRows[i].Cells["CREW_NAME"].Value.ToString();

                            Guid masterGuid = MainForm.StrToGuid(adgvMasters.SelectedRows[i].Cells["CREW_GUID"].Value.ToString());

                            cmd.CommandText = 
                                "select Count(CREW_GUID) " +
                                "from REPORTS_CREW " +
                                "where CREW_GUID=" + MainForm.GuidToStr(masterGuid);

                            int recCount = (int)cmd.ExecuteScalar();

                            if (recCount > 0)
                            {
                                if (recCount == 1)
                                    msg = "There is an inspection assosiated with captain " + masterName + ".\n" +
                                        "Would you like to delete records for this captain anyway?";
                                else
                                    msg = "There are " + recCount.ToString() + " inspections assosiated with captain " + masterName + ".\n" +
                                        "Would you like to delete records for this captain anyway?";

                                var rs1 = MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                                if (rs1 == DialogResult.Yes)
                                {

                                    msg = "All inspections assosiated with captain " + masterName + " will have not any information aabout the master.";

                                    MessageBox.Show(msg);

                                    cmd.CommandText =
                                        "update REPORTS_CREW set \n" +
                                        "CREW_GUID=Null \n" +
                                        "where CREW_GUID=" + MainForm.GuidToStr(masterGuid);
                                    cmd.ExecuteNonQuery();

                                    cmd.CommandText = 
                                        "delete from CREW ]n" +
                                        "where CREW_GUID=" + MainForm.GuidToStr(masterGuid);
                                    cmd.ExecuteNonQuery();

                                    delCounter++;

                                    if (delMasters.Length == 0)
                                        delMasters = masterName;
                                    else
                                    {
                                        if (delCounter < 11)
                                            delMasters = delMasters + "\n" + masterName;
                                    }
                                }
                                else
                                {
                                    remainCounter++;

                                    if (remainMasters.Length == 0)
                                        remainMasters = masterName;
                                    else
                                    {
                                        if (remainCounter < 11)
                                            remainMasters = remainMasters + "\n" + masterName;
                                    }
                                }
                            }
                            else
                            {
                                cmd.CommandText = 
                                    "delete from CREW \n" +
                                    "where CREW_GUID=" + MainForm.GuidToStr(masterGuid);
                                cmd.ExecuteNonQuery();

                                delCounter++;

                                if (delMasters.Length == 0)
                                    delMasters = masterName;
                                else
                                {
                                    if (delCounter < 11)
                                        delMasters = delMasters + "\n" + masterName;
                                }
                            }
                        }
                        string msgText = "";

                        if (delCounter > 0)
                        {
                            //Some records were deleted
                            switch (delCounter)
                            {
                                case 1:
                                    if (delCounter == mastersSelected)
                                    {
                                        msgText = "Records for captain " + delMasters + " were deleted";
                                    }
                                    else
                                    {
                                        msgText = "Records for captain " + delMasters + " were deleted.";

                                        if (mastersSelected - delCounter > 1)
                                        {
                                            msgText = msgText + "\n" +
                                                "Records for the following captains were not deleted:\n\n" + remainMasters;

                                            if (mastersSelected - delCounter > 10)
                                                msgText = msgText + "\n...\n" +
                                                    "Total : " + (mastersSelected - delCounter).ToString() + " records";
                                        }
                                        else
                                            msgText = msgText + "\n" +
                                                "Record for the following captain " + remainMasters + " were not deleted";
                                    }
                                    break;
                                default:
                                    msgText = "Records for the following captains were deleted:\n\n" + delMasters;

                                    if (delCounter > 10)
                                        msgText = msgText + "\n...\n" +
                                            "Total : " + delCounter.ToString() + " records";

                                    if (delCounter != mastersSelected)
                                    {
                                        msgText = msgText + "\n\n" +
                                            "Records for the following captains were not deleted:\n\n" + remainMasters;

                                        if (mastersSelected - delCounter > 10)
                                            msgText = msgText + "\n...\n" +
                                                "Total : " + (mastersSelected - delCounter).ToString() + " records";
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            //Nothig was deleted
                            msgText = "Records for the following captains were not deleted:\n\n" + remainMasters;

                            if (mastersSelected > 10)
                                msgText = msgText + "\n...\n" +
                                    "Total : " + mastersSelected.ToString() + " records";
                        }

                        MessageBox.Show(msgText, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        fillMasters();

                        if (adgvMasters.Rows.Count > 0)
                        {
                            if (visibleRow > adgvMasters.Rows.Count)
                                adgvMasters.FirstDisplayedScrollingRowIndex = 0;
                            else
                                adgvMasters.FirstDisplayedScrollingRowIndex = visibleRow;

                            if (adgvMasters.Rows.Count <= rowIndex)
                            {
                                adgvMasters.CurrentCell = adgvMasters[1, adgvMasters.Rows.Count - 1];
                            }
                            else
                            {
                                adgvMasters.CurrentCell = adgvMasters[1, rowIndex];
                            }
                        }
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }


        }

        private void adgvMasters_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            editMaster();
        }

        private void CountRecords()
        {
            lblRecs.Text = adgvMasters.Rows.Count.ToString();
        }

        private void adgvMasters_FilterStringChanged(object sender, EventArgs e)
        {
            bsMasters.Filter = adgvMasters.FilterString;
            CountRecords();
        }

        private void adgvMasters_SortStringChanged(object sender, EventArgs e)
        {
            bsMasters.Sort = adgvMasters.SortString;
        }

        private void ListOfMastersForm_Shown(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }
    }
}
