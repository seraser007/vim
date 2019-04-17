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
    public partial class ListOfChEngineers : Form
    {
        OleDbConnection connection;
        DataSet DS;
        //OleDbDataAdapter chEngAdapter;
        OleDbCommand cmd;
        BindingSource bsChEngineers = new BindingSource();

        public ListOfChEngineers(OleDbConnection mainConnection, DataSet mainDS, Font mainFont, Icon mainIcon)
        {
            this.Font = mainFont;
            this.Icon = mainIcon;

            InitializeComponent();

            DS = mainDS;
            connection = mainConnection;
            cmd = new OleDbCommand("", connection);


            fillChiefEngineers();

            bsChEngineers.DataSource = DS;
            bsChEngineers.DataMember = "CHIEF_ENGINEERS";

            adgvChEngineers.DataSource = bsChEngineers;

            adgvChEngineers.AutoGenerateColumns = true;

            adgvChEngineers.Columns["CHENG_GUID"].Visible = false;
            
            adgvChEngineers.Columns["CHENG_NAME"].HeaderText = "Name";
            adgvChEngineers.Columns["CHENG_NAME"].FillWeight = 50;

            adgvChEngineers.Columns["PERSONAL_ID"].HeaderText = "Personal ID";
            adgvChEngineers.Columns["PERSONAL_ID"].FillWeight = 20;
            
            adgvChEngineers.Columns["CHENG_NOTES"].Visible = false;

            adgvChEngineers.Columns["REPORT_NUMBER"].HeaderText = "Inspections";
            adgvChEngineers.Columns["REPORT_NUMBER"].FillWeight = 20;
            
            adgvChEngineers.Columns["OBS_NUMBER"].HeaderText = "Observations";
            adgvChEngineers.Columns["OBS_NUMBER"].FillWeight = 20;

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

        private void fillChiefEngineers()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            string cmdText =
                "select CHENG_GUID, CHENG_NAME, CHENG_NOTES, PERSONAL_ID, REPORT_NUMBER, OBS_NUMBER \n" +
                "from (CHIEF_ENGINEERS left join  \n" +
                "(select Q01.CHENG_GUID as CH_ID, REPORT_NUMBER, OBS_NUMBER  \n" +
                "from  \n" +
                "(select CHENG_GUID, COUNT(REPORT_CODE) as REPORT_NUMBER  \n" +
                "from REPORTS  \n" +
                "group by CHENG_GUID) as Q01  \n" +
                "left join  \n" +
                "(select CHENG_GUID, COUNT(OBSERVATION) as OBS_NUMBER  \n" +
                "from REPORT_ITEMS inner join REPORTS  \n" +
                "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE  \n" +
                "where LEN(OBSERVATION)>0  \n" +
                "group by CHENG_GUID) as Q02  \n" +
                "on Q01.CHENG_GUID=Q02.CHENG_GUID) as Q1  \n" +
                "on CHIEF_ENGINEERS.CHENG_GUID=Q1.CH_ID)  \n" +
                "order by CHENG_NAME";

            OleDbDataAdapter chEngAdapter = new OleDbDataAdapter(cmdText, connection);

            if (DS.Tables.Contains("CHIEF_ENGINEERS"))
                DS.Tables["CHIEF_ENGINEERS"].Clear();

            chEngAdapter.Fill(DS, "CHIEF_ENGINEERS");


            cmd.CommandText =
                "select * from \n" +
                "(select CHENG_GUID, CHENG_NAME, CHENG_NOTES, PERSONAL_ID \n" +
                "from CHIEF_ENGINEERS \n" +
                "union \n" +
                "select "+MainForm.GuidToStr(MainForm.zeroGuid)+" as CHENG_GUID, '' as CHENG_NAME, '' as CHENG_NOTES, '' as PERSONAL_ID \n" +
                "from Fonts) \n" +
                "order by CHENG_NAME";

            OleDbDataAdapter chengListAdapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("CHENG_LIST"))
                DS.Tables["CHENG_LIST"].Clear();

            chengListAdapter.Fill(DS, "CHENG_LIST");

            DataTable ChEngineers = DS.Tables["CHENG_LIST"];

            findBox.DataSource = ChEngineers;
            findBox.DisplayMember = "CHENG_NAME";
            findBox.ValueMember = "CHENG_GUID";

            CountRecords();
        }


        private void btnNew_Click(object sender, EventArgs e)
        {
            //Add new chief engineer

            ChEngDetailsForm chForm = new ChEngDetailsForm(connection, DS, this.Font, this.Icon,MainForm.zeroGuid);

            chForm.chengName = "";
            chForm.chengPersonalID = "";
            chForm.chengNotes = "";

            var rslt = chForm.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                if (checkSameName(chForm.chengName, MainForm.zeroGuid) != 0) return;

                if (checkSameID(chForm.chengPersonalID, MainForm.zeroGuid) != 0) return;

                OleDbCommand cmd = new OleDbCommand("", connection);

                cmd.CommandText =
                    "insert into CHIEF_ENGINEERS (CHENG_NAME,PERSONAL_ID,CHENG_NOTES) \n" +
                    "values('" + chForm.chengName + "','" + chForm.chengPersonalID + "','" + chForm.chengNotes + "')";
                cmd.ExecuteNonQuery();


                fillChiefEngineers();

                String searchValue = chForm.chengName;

                int rowIndex = -1;

                foreach (DataGridViewRow dgRow in adgvChEngineers.Rows)
                {
                    if (dgRow.Cells["CHENG_NAME"].Value.ToString().Equals(searchValue))
                    {
                        rowIndex = dgRow.Index;
                        break;
                    }
                }

                if (rowIndex >= 0)
                {
                    //dataGridView1.Rows[rowIndex].Selected = true;
                    adgvChEngineers.CurrentCell = adgvChEngineers[1, rowIndex];
                }
            }
        
        }

        public string StrToSQLStr(string Text)
        {
            return Text.Replace("'", "''");
        }

        public int checkSameName(string chengName, Guid chengGuid)
        {
            //Check for records with the same CHENG_NAME
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select Count(CHENG_NAME) \n" +
                "from CHIEF_ENGINEERS \n" +
                "where CHENG_NAME like '" + StrToSQLStr(chengName) + "' \n" +
                "and CHENG_GUID<>" + MainForm.GuidToStr(chengGuid);

            int recs = (int)cmd.ExecuteScalar();

            if (recs > 0)
            {
                //There is record with the same name
                cmd.CommandText =
                    "select CHENG_NAME, PERSONAL_ID \n" +
                    "from CHIEF_ENGINEERS \n" +
                    "where CHENG_NAME like '" + StrToSQLStr(chengName) + "' \n" +
                    "and CHENG_GUID<>" + MainForm.GuidToStr(chengGuid);

                OleDbDataReader mReader = cmd.ExecuteReader();

                int recsReader = 0;

                string msgText = "";

                while (mReader.Read())
                {
                    if (msgText.Length == 0)
                        msgText = "Name : \"" + mReader[0].ToString() + "\"; Personal ID : \"" + mReader[1].ToString() + "\"";
                    else
                        msgText = msgText + "\n" +
                            "Name : \"" + mReader[0].ToString() + "\"; Personal ID : \"" + mReader[1].ToString() + "\"";
                    recsReader++;
                }

                mReader.Close();

                if (recsReader == 1)
                    msgText = "There is a record with the same name: \n\n" + msgText;
                else
                    msgText = "There are " + recsReader.ToString() + " records with the same name: \n\n" + msgText;

                msgText = msgText + "\n\n Would you like to save this record?";

                var res1 = System.Windows.Forms.MessageBox.Show(msgText, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res1 == DialogResult.Yes)
                    return 0;
                else
                    return 1;
            }
            else
                return 0;

        }

        private int checkSameID(string personalID, Guid chengGuid)
        {
            //Check for records with the same Personal ID
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
            "select Count(PERSONAL_ID) \n" +
            "from CHIEF_ENGINEERS \n" +
            "where PERSONAL_ID like '" + StrToSQLStr(personalID) + "' \n" +
            "and LEN(PERSONAL_ID)>0 \n" +
            "and CHENG_GUID<>" + MainForm.GuidToStr(chengGuid);

            int recs = (int)cmd.ExecuteScalar();

            if (recs > 0)
            {
                //There is record with the same name
                cmd.CommandText =
                    "select CHENG_NAME, PERSONAL_ID \n" +
                    "from CHIEF_ENGINEERS \n" +
                    "where PERSONAL_ID like '" + StrToSQLStr(personalID) + "' \n" +
                    "and LEN(PERSONAL_ID)>0 \n" +
                    "and CHENG_GUID<>" + MainForm.GuidToStr(chengGuid);

                OleDbDataReader mReader = cmd.ExecuteReader();

                int recsReader = 0;

                string msgText = "";

                while (mReader.Read())
                {
                    if (msgText.Length == 0)
                        msgText = "Name : \"" + mReader[0].ToString() + "\"; Personal ID : \"" + mReader[1].ToString() + "\"";
                    else
                        msgText = msgText + "\n" +
                            "Name : \"" + mReader[0].ToString() + "\"; Personal ID : \"" + mReader[1].ToString() + "\"";
                    recsReader++;
                }

                if (recsReader == 1)
                    msgText = "There is a record with the same Personal ID: \n\n" + msgText;
                else
                    msgText = "There are " + recsReader.ToString() + " records with the same Personal ID: \n\n" + msgText;

                msgText = msgText + "\n\n Would you like to save this record?";

                var res1 = System.Windows.Forms.MessageBox.Show(msgText, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res1 == DialogResult.Yes)
                    return 0;
                else
                    return 1;
            }
            else
                return 0;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //Edit Chief Engineer
            editChEng();
        }

        private void editChEng()
        {
            //Edit Chief Engineers

            if (adgvChEngineers.Rows.Count == 0) return;

            int curRow = adgvChEngineers.CurrentCell.RowIndex;
            int curCol = adgvChEngineers.CurrentCell.ColumnIndex;
            int visibleRow = adgvChEngineers.FirstDisplayedCell.RowIndex;
            Guid chengGuid = MainForm.StrToGuid(adgvChEngineers.CurrentRow.Cells["CHENG_GUID"].Value.ToString());

            ChEngDetailsForm chForm = new ChEngDetailsForm(connection, DS, this.Font, this.Icon, chengGuid);

            chForm.chengName = adgvChEngineers.CurrentRow.Cells["CHENG_NAME"].Value.ToString();
            chForm.chengPersonalID = adgvChEngineers.CurrentRow.Cells["PERSONAL_ID"].Value.ToString();
            chForm.chengNotes = adgvChEngineers.CurrentRow.Cells["CHENG_NOTES"].Value.ToString();

            var rslt = chForm.ShowDialog();

            if ((rslt == DialogResult.OK) && (MainForm.isPowerUser))
            {

                if (checkSameName(chForm.chengName, chengGuid) != 0)
                    return;

                if (checkSameID(chForm.chengPersonalID, chengGuid) != 0)
                    return;

                OleDbCommand cmd = new OleDbCommand("", connection);

                cmd.CommandText =
                "update CHIEF_ENGINEERS set \n" +
                "CHENG_NAME='" + StrToSQLStr(chForm.chengName) + "', \n" +
                "PERSONAL_ID='" + StrToSQLStr(chForm.chengPersonalID) + "', \n" +
                "CHENG_NOTES='" + StrToSQLStr(chForm.chengNotes) + "' \n" +
                "where CHENG_GUID=" + MainForm.GuidToStr(chengGuid);
                cmd.ExecuteNonQuery();
            }

            fillChiefEngineers();

            adgvChEngineers.FirstDisplayedScrollingRowIndex = visibleRow;

            adgvChEngineers.CurrentCell = adgvChEngineers[curCol, curRow];

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Delete chief engineer

            ////////////////////// NEW CODE ///////////////////////////////

            if (adgvChEngineers.Rows.Count == 0)
                return;

            int rowIndex = adgvChEngineers.CurrentRow.Index;
            int visibleRow = adgvChEngineers.FirstDisplayedCell.RowIndex;

            if (adgvChEngineers.SelectedRows.Count < 2)
            {
                string chEngName = adgvChEngineers.CurrentRow.Cells["CHENG_NAME"].Value.ToString();

                string msg = "You are going to delete record for the following chief engineer:\n\n" +
                    "Name : \"" + chEngName + "\"\n" +
                    "Personal ID : \"" + adgvChEngineers.CurrentRow.Cells["PERSONAL_ID"].Value.ToString() + "\"\n\n" +
                    "Would you like to proceed?";

                var rslt = System.Windows.Forms.MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (rslt == DialogResult.Yes)
                {
                    //Проверяем возможность удаления

                    OleDbCommand cmd = new OleDbCommand("", connection);

                    Guid chengGuid = MainForm.StrToGuid(adgvChEngineers.CurrentRow.Cells["CHENG_GUID"].Value.ToString());

                    cmd.CommandText = 
                        "select Count(CHENG_GUID) \n" +
                        "from REPORTS \n" +
                        "where CHENG_GUID=" + MainForm.GuidToStr(chengGuid);

                    int recCount = (int)cmd.ExecuteScalar();

                    if (recCount > 0)
                    {
                        if (recCount == 1)
                            msg = "There is an inspection assosiated with the chief engineer " + chEngName + ".\n" +
                                "Would you like to delete records for this chief engineer anyway?";
                        else
                            msg = "There are " + recCount.ToString() + " inspections assosiated with the chief engineer " + chEngName + ".\n" +
                                "Would you like to delete records for this chief engineer anyway?";

                        var rs1 = System.Windows.Forms.MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                        if (rs1 == DialogResult.No)
                            return;

                        msg = "All inspections assosiated with the chief engineer " + chEngName + " will have not any information about the chief engineer.";

                        System.Windows.Forms.MessageBox.Show(msg);
                    }

                    cmd.CommandText = 
                        "delete from CHIEF_ENGINEERS " +
                        "where CHENG_GUID=" + MainForm.GuidToStr(chengGuid);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText =
                        "update REPORTS set \n" +
                        "CHENG_GUID=Null \n" +
                        "where CHENG_GUID=" + MainForm.GuidToStr(chengGuid);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("All records for chief engineer " + chEngName + " were deleted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (DS.Tables.Contains("CHIEF_ENGINEERS"))
                        DS.Tables["CHIEF_ENGINEERS"].Clear();

                    //chEngAdapter.Fill(DS, "CHIEF_ENGINEERS");
                    fillChiefEngineers();

                    if (adgvChEngineers.Rows.Count > 0)
                    {
                        adgvChEngineers.FirstDisplayedScrollingRowIndex = visibleRow;

                        if (adgvChEngineers.Rows.Count <= rowIndex)
                        {
                            adgvChEngineers.CurrentCell = adgvChEngineers[1, adgvChEngineers.Rows.Count - 1];
                        }
                        else
                        {
                            adgvChEngineers.CurrentCell = adgvChEngineers[1, rowIndex];
                        }
                    }
                }
            }
            else
            {
                string msg = "You are going to delete records for the following chief engineers:\n\n";
                int chEngSelected = adgvChEngineers.SelectedRows.Count;
                int chEngCounter = 0;


                for (int i = chEngSelected - 1; i >= 0; i--)
                {
                    if (chEngSelected < 14)
                    {
                        msg = msg + adgvChEngineers.SelectedRows[i].Cells["CHENG_NAME"].Value.ToString() + "\n";
                    }
                    else
                    {
                        if (chEngCounter < 10)
                        {
                            msg = msg + adgvChEngineers.SelectedRows[i].Cells["CHENG_NAME"].Value.ToString() + "\n";
                        }
                    }

                    chEngCounter++;
                }

                if (chEngSelected >= 14)
                {
                    msg = msg + "...\nTotal : " + chEngSelected.ToString() + " records\n";
                }

                msg = msg + "\n"+"Would you like to proceed?";

                var rslt = System.Windows.Forms.MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (rslt == DialogResult.Yes)
                {
                    int delCounter = 0;
                    int remainCounter = 0;

                    string delChEngs = "";
                    string remainChEngs = "";

                    this.Cursor = Cursors.WaitCursor;

                    try
                    {

                        for (int i = adgvChEngineers.SelectedRows.Count - 1; i >= 0; i--)
                        {
                            OleDbCommand cmd = new OleDbCommand("", connection);
                            string chEngName = adgvChEngineers.SelectedRows[i].Cells["CHENG_NAME"].Value.ToString();

                            Guid chengGuid = MainForm.StrToGuid(adgvChEngineers.SelectedRows[i].Cells["CHENG_GUID"].Value.ToString());

                            cmd.CommandText =
                                "select Count(CHENG_GUID) \n" +
                                "from REPORTS \n" +
                                "where CHENG_GUID=" + MainForm.GuidToStr(chengGuid);

                            int recCount = (int)cmd.ExecuteScalar();

                            if (recCount > 0)
                            {
                                if (recCount == 1)
                                    msg = "There is an inspection assosiated with the chief engineer " + chEngName + ".\n" +
                                        "Would you like to delete records for this chief engineer anyway?";
                                else
                                    msg = "There are " + recCount.ToString() + " inspections assosiated with the chief engineer " + chEngName + ".\n" +
                                        "Would you like to delete records for this chief engineer anyway?";

                                var rs1 = System.Windows.Forms.MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                                if (rs1 == DialogResult.Yes)
                                {

                                    msg = "All inspections assosiated with the chief engineer " + chEngName + " will have not any information about the chief engineer.";

                                    System.Windows.Forms.MessageBox.Show(msg);

                                    cmd.CommandText =
                                        "update REPORTS set \n" +
                                        "CHENG_GUID=Null \n" +
                                        "where CHENG_GUID=" + MainForm.GuidToStr(chengGuid);
                                    cmd.ExecuteNonQuery();

                                    cmd.CommandText =
                                        "delete from CHEIF_ENGINEERS \n" +
                                        "where CHENG_GUID=" + MainForm.GuidToStr(chengGuid);
                                    cmd.ExecuteNonQuery();

                                    delCounter++;

                                    if (delChEngs.Length == 0)
                                        delChEngs = chEngName;
                                    else
                                    {
                                        if (delCounter < 11)
                                            delChEngs = delChEngs + "\n" + chEngName;
                                    }
                                }
                                else
                                {
                                    remainCounter++;

                                    if (remainChEngs.Length == 0)
                                        remainChEngs = chEngName;
                                    else
                                    {
                                        if (remainCounter < 11)
                                            remainChEngs = remainChEngs + "\n" + chEngName;
                                    }
                                }
                            }
                            else
                            {
                                cmd.CommandText =
                                    "delete from CHIEF_ENGINEERS \n" +
                                    "where CHENG_GUID=" + MainForm.GuidToStr(chengGuid);
                                cmd.ExecuteNonQuery();

                                delCounter++;

                                if (delChEngs.Length == 0)
                                    delChEngs = chEngName;
                                else
                                {
                                    if (delCounter < 11)
                                        delChEngs = delChEngs + "\n" + chEngName;
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
                                    if (delCounter == chEngSelected)
                                    {
                                        msgText = "Records for chief engineer " + delChEngs + " were deleted";
                                    }
                                    else
                                    {
                                        msgText = "Records for chief engineer " + delChEngs + " were deleted.";

                                        if (chEngSelected - delCounter > 1)
                                        {
                                            msgText = msgText + "\n" +
                                                "Records for the following chief engineers were not deleted:\n\n" + remainChEngs;

                                            if (chEngSelected - delCounter > 10)
                                                msgText = msgText + "\n...\n" +
                                                    "Total : " + (chEngSelected - delCounter).ToString() + " records";
                                        }
                                        else
                                            msgText = msgText + "\n" +
                                                "Record for the following chief engineers " + remainChEngs + " were not deleted";
                                    }
                                    break;
                                default:
                                    msgText = "Records for the following chef engineers were deleted:\n\n" + delChEngs;

                                    if (delCounter > 10)
                                        msgText = msgText + "\n...\n" +
                                            "Total : " + delCounter.ToString() + " records";

                                    if (delCounter != chEngSelected)
                                    {
                                        msgText = msgText + "\n\n" +
                                            "Records for the following chief engineers were not deleted:\n\n" + remainChEngs;

                                        if (chEngSelected - delCounter > 10)
                                            msgText = msgText + "\n...\n" +
                                                "Total : " + (chEngSelected - delCounter).ToString() + " records";
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            //Nothig was deleted
                            msgText = "Records for the following chief engineers were not deleted:\n\n" + remainChEngs;

                            if (chEngSelected > 10)
                                msgText = msgText + "\n...\n" +
                                    "Total : " + chEngSelected.ToString() + " records";
                        }

                        MessageBox.Show(msgText, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (DS.Tables.Contains("CHIEF_ENGINEERS"))
                            DS.Tables["CHIEF_ENGINEERS"].Clear();

                        //chEngAdapter.Fill(DS, "CHIEF_ENGINEERS");

                        fillChiefEngineers();

                        if (adgvChEngineers.Rows.Count > 0)
                        {
                            if (visibleRow > adgvChEngineers.Rows.Count)
                                adgvChEngineers.FirstDisplayedScrollingRowIndex = 0;
                            else
                                adgvChEngineers.FirstDisplayedScrollingRowIndex = visibleRow;

                            if (adgvChEngineers.Rows.Count <= rowIndex)
                            {
                                adgvChEngineers.CurrentCell = adgvChEngineers[1, adgvChEngineers.Rows.Count - 1];
                            }
                            else
                            {
                                adgvChEngineers.CurrentCell = adgvChEngineers[1, rowIndex];
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

        private void btnFind_Click(object sender, EventArgs e)
        {
            //Find Chief Engineer
            locateRecord();
        }

        private void locateRecord()
        {
            String searchValue = findBox.Text;

            int rowIndex = -1;

            foreach (DataGridViewRow dgRow in adgvChEngineers.Rows)
            {
                if (dgRow.Cells["CHENG_NAME"].Value.ToString().Equals(searchValue))
                {
                    rowIndex = dgRow.Index;
                    break;
                }
            }

            if (rowIndex >= 0)
            {
                //dataGridView1.Rows[rowIndex].Selected = true;
                adgvChEngineers.CurrentCell = adgvChEngineers[1, rowIndex];
            }
        }

        private void adgvChEngineers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Edit Chief Engineer
            editChEng();
        }

        private void CountRecords()
        {
            lblRecs.Text = adgvChEngineers.Rows.Count.ToString();
        }

        private void adgvChEngineers_FilterStringChanged(object sender, EventArgs e)
        {
            bsChEngineers.Filter = adgvChEngineers.FilterString;

            CountRecords();
        }

        private void adgvChEngineers_SortStringChanged(object sender, EventArgs e)
        {
            bsChEngineers.Sort = adgvChEngineers.SortString;
        }

    }
}
