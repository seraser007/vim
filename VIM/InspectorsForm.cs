using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Outlook = Microsoft.Office.Interop.Outlook;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using MessageBox = System.Windows.Forms.MessageBox;
using System.IO;
using System.Diagnostics;

namespace VIM
{
    public partial class InspectorsForm : Form
    {
        OleDbConnection connection;
        DataSet DS;
        OleDbDataAdapter inspectorsAdapter;
        OleDbCommand cmd;
        BindingSource bsInspectors = new BindingSource();

        private string _subject = "";
        private string _body = "";
        private bool _useSizeLimit = false;
        private int _maxSize = 0;
        private string _dimension = "KB";
        private bool _useReportCount = false;
        private int _maxReports = 0;
        private bool _send = false;
        private bool _useTempFolder = true;
        private string _userFolder = "";
        private bool _sendCopyToFleet = false;
        private string _copyAddresses = "";
        private bool _askForVessel = false;
        private bool _useChart = true;

        private string fleetEmail = "";
        private string vesselEmail = "";
        private string vesselName = "";
        private string vesselNameEmail = "";

        public InspectorsForm(OleDbConnection mainConnection, DataSet mainDS, Font mainFont, Icon mainIcon)
        {
            this.Font = mainFont;
            this.Icon = mainIcon;
            
            InitializeComponent();

            adgvInspectors.RowPrePaint += new DataGridViewRowPrePaintEventHandler(adgvInspectors_RowPrePaint);

            DS = mainDS;
            connection = mainConnection;
            cmd=new OleDbCommand("",connection);

            string cmdText =
                "select INSPECTORS.INSPECTOR_GUID, INSPECTOR_NAME, Background, Notes, Photo,iif(LEN(Photo)>0,'Yes','No') as HAS_PHOTO, "+
                "HAS_PROFILE, PROFILE_DATE, EXTRA_FILES, Unfavourable, REPORT_NUMBER, OBS_NUMBER, LinkedIn\n" +
                "from (INSPECTORS left join \n" +
                "(select Q01.INSPECTOR_GUID as INS_ID, REPORT_NUMBER, OBS_NUMBER \n" +
                "from \n" +
                "(select INSPECTOR_GUID, COUNT(REPORT_CODE) as REPORT_NUMBER \n" +
                "from REPORTS \n" +
                "group by INSPECTOR_GUID) as Q01 \n" +
                "left join \n" +
                "(select INSPECTOR_GUID, COUNT(OBSERVATION) as OBS_NUMBER \n" +
                "from REPORT_ITEMS inner join REPORTS \n" +
                "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE \n" +
                "where LEN(OBSERVATION)>0 \n" +
                "group by INSPECTOR_GUID) as Q02 \n" +
                "on Q01.INSPECTOR_GUID=Q02.INSPECTOR_GUID) as Q1 \n"+
                "on INSPECTORS.INSPECTOR_GUID=Q1.INS_ID) \n"+
                "left join \n"+
                "(select INSPECTOR_GUID, Count(INSPECTOR_GUID) as EXTRA_FILES \n"+
                "from INSPECTOR_FILES  \n"+
                "where \n"+
                "FILE_EXISTS=TRUE \n"+
                "group by INSPECTOR_GUID) as InspectorFiles \n" +
                "on INSPECTORS.INSPECTOR_GUID=InspectorFiles.INSPECTOR_GUID \n"+
                "order by INSPECTOR_NAME";

            inspectorsAdapter = new OleDbDataAdapter(cmdText, connection);

            
            if (DS.Tables.Contains("INSPECTORS"))
                DS.Tables["INSPECTORS"].Clear();

            this.Cursor = Cursors.WaitCursor;

            try
            {
                inspectorsAdapter.Fill(DS, "INSPECTORS");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            bsInspectors.DataSource = DS;
            bsInspectors.DataMember = "INSPECTORS";

            adgvInspectors.DataSource = bsInspectors;
            
            adgvInspectors.Columns["INSPECTOR_GUID"].Visible = false;
            
            adgvInspectors.Columns["INSPECTOR_NAME"].HeaderText = "Inspector name";
            adgvInspectors.Columns["INSPECTOR_NAME"].FillWeight = 40;
            
            adgvInspectors.Columns["Background"].Visible = true;
            adgvInspectors.Columns["Background"].FillWeight = 20;
            
            adgvInspectors.Columns["Notes"].Visible = false;
            
            adgvInspectors.Columns["Photo"].Visible = false;
            
            adgvInspectors.Columns["HAS_PHOTO"].Visible = true;
            adgvInspectors.Columns["HAS_PHOTO"].HeaderText = "Photo";
            adgvInspectors.Columns["HAS_PHOTO"].FillWeight = 15;
            adgvInspectors.Columns["HAS_PHOTO"].ValueType = typeof(Boolean);
            
            adgvInspectors.Columns["Unfavourable"].FillWeight = 20;

            adgvInspectors.Columns["HAS_PROFILE"].FillWeight = 15;
            adgvInspectors.Columns["HAS_PROFILE"].HeaderText = "Profile";

            adgvInspectors.Columns["PROFILE_DATE"].HeaderText = "Profile date";
            adgvInspectors.Columns["PROFILE_DATE"].FillWeight = 20;

            adgvInspectors.Columns["EXTRA_FILES"].HeaderText = "Files";
            adgvInspectors.Columns["EXTRA_FILES"].FillWeight = 10;

            adgvInspectors.Columns["REPORT_NUMBER"].HeaderText = "Inspections";
            adgvInspectors.Columns["REPORT_NUMBER"].FillWeight = 15;
            adgvInspectors.Columns["OBS_NUMBER"].HeaderText = "Observations";
            adgvInspectors.Columns["OBS_NUMBER"].FillWeight = 15;

            adgvInspectors.Columns["LinkedIn"].Visible=false;

            DataTable inspectors=DS.Tables["INSPECTORS_LIST"];

            findBox.DataSource = inspectors;
            findBox.DisplayMember = "INSPECTOR_NAME";
            findBox.ValueMember = "INSPECTOR_GUID";

            ProtectFields(MainForm.isPowerUser);

            CountRecords();

            GetMessageSettings();
        }

        private void ProtectFields(bool status)
        {
            btnMerge.Enabled = status;
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            editInspector();
        }

        private void editInspector()
        {
            //Edit inspector
            Guid inspectorGuid = MainForm.StrToGuid(adgvInspectors.CurrentRow.Cells["INSPECTOR_GUID"].Value.ToString());

            this.Cursor = Cursors.WaitCursor;

            InspectorForm inspectorForm = new InspectorForm(inspectorGuid, 0);
            int curRow = adgvInspectors.CurrentCell.RowIndex;
            int curCol = adgvInspectors.CurrentCell.ColumnIndex;
            int visibleRow = adgvInspectors.FirstDisplayedCell.RowIndex;
            OleDbCommand cmd = new OleDbCommand("", connection);

            //Full name
            inspectorForm.inspectorName = adgvInspectors.CurrentRow.Cells["INSPECTOR_NAME"].Value.ToString();
            //Notes
            inspectorForm.inspectorNotes = adgvInspectors.CurrentRow.Cells["Notes"].Value.ToString();
            //Photo file
            inspectorForm.inspectorPhoto = adgvInspectors.CurrentRow.Cells["Photo"].Value.ToString();
            //Background
            inspectorForm.inspectorBackground = adgvInspectors.CurrentRow.Cells["Background"].Value.ToString();

            inspectorForm.linkedIn = adgvInspectors.CurrentRow.Cells["LinkedIn"].Value.ToString();

            if (adgvInspectors.CurrentRow.Cells["PROFILE_DATE"].Value.ToString().Length == 0)
            {
                inspectorForm.profileDate = DateTimePicker.MinimumDateTime;
            }
            else
            {
                inspectorForm.profileDate = (DateTime) adgvInspectors.CurrentRow.Cells["PROFILE_DATE"].Value;
            }
            
            //Unfavourable
            inspectorForm.inspectorUnfavourable = Convert.ToBoolean(adgvInspectors.CurrentRow.Cells["Unfavourable"].Value);

            var rslt = inspectorForm.ShowDialog();

            this.Cursor = Cursors.Default;

            if ((rslt == DialogResult.OK) && (MainForm.isPowerUser))
            {
                cmd.CommandText =
                    "update INSPECTORS set\n" +
                    "INSPECTOR_NAME='" + StrToSQLStr(inspectorForm.inspectorName) + "',\n" +
                    "Notes='" + StrToSQLStr(inspectorForm.inspectorNotes) + "',\n"+
                    "Photo='"+StrToSQLStr(inspectorForm.inspectorPhoto)+"',\n"+
                    "Unfavourable="+Convert.ToString(inspectorForm.inspectorUnfavourable)+",\n"+
                    "Background='"+StrToSQLStr(inspectorForm.inspectorBackground)+"',\n"+
                    "LinkedIn='"+StrToSQLStr(inspectorForm.linkedIn)+"',\n"+
                    "PROFILE_DATE="+MainForm.DateTimeToQueryStr(inspectorForm.profileDate)+"\n"+
                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid);
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    "update INSPECTORS set HAS_PROFILE=False";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    "update INSPECTORS set HAS_PROFILE=True \n" +
                    "where INSPECTOR_GUID in (select DISTINCT INSPECTOR_GUID from UNIFIED_COMMENTS)";
                cmd.ExecuteNonQuery();


                int row = adgvInspectors.CurrentRow.Index;

                if (DS.Tables.Contains("INSPECTORS"))
                    DS.Tables["INSPECTORS"].Clear();
    
                inspectorsAdapter.Fill(DS, "INSPECTORS");

                fillInspectors();

                adgvInspectors.FirstDisplayedScrollingRowIndex = visibleRow;

                adgvInspectors.CurrentCell = adgvInspectors[curCol, curRow];
            }

        }

        public string StrToSQLStr(string Text)
        {
            return Text.Replace("'", "''");
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            //Add new inspector
            
            InspectorForm inspectorForm = new InspectorForm(MainForm.zeroGuid, 1);

            int curRow = adgvInspectors.CurrentCell.RowIndex;
            int curCol = adgvInspectors.CurrentCell.ColumnIndex;
            int firstVisibleRow = adgvInspectors.FirstDisplayedCell.RowIndex;

            OleDbCommand cmd = new OleDbCommand("", connection);

            //Full name
            inspectorForm.inspectorName = "";
            //Notes
            inspectorForm.inspectorNotes = "";
            //Photo file
            inspectorForm.inspectorPhoto = "";
            //Unfavourable
            inspectorForm.inspectorUnfavourable = false;
            //Profile date
            inspectorForm.profileDate = DateTimePicker.MinimumDateTime;

            var rslt = inspectorForm.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                Guid newGuid = Guid.NewGuid();

                cmd.CommandText =
                    "insert into INSPECTORS (INSPECTOR_GUID, INSPECTOR_NAME, NOTES, PHOTO, UNFAVOURABLE, BACKGROUND, LINKEDIN, PROFILE_DATE)\n" +
                    "values("+MainForm.GuidToStr(newGuid)+", '" + StrToSQLStr(inspectorForm.inspectorName) + "',\n" +
                    "'" + StrToSQLStr(inspectorForm.inspectorNotes) + "',\n" +
                    "'" + StrToSQLStr(inspectorForm.inspectorPhoto) + "',\n" +
                    Convert.ToString(inspectorForm.inspectorUnfavourable) + "," +
                    "'" + StrToSQLStr(inspectorForm.inspectorBackground) + "',\n" +
                    "'" + StrToSQLStr(inspectorForm.linkedIn) + "', \n" +
                    MainForm.DateTimeToQueryStr(inspectorForm.profileDate) + ")";
                cmd.ExecuteNonQuery();

                //int row = dataGridView1.CurrentRow.Index;

                if (DS.Tables.Contains("INSPECTORS"))
                    DS.Tables["INSPECTORS"].Clear();

                cmd.CommandText =
                   "update INSPECTORS set HAS_PROFILE=False";
                cmd.ExecuteNonQuery();

                cmd.CommandText =
                    "update INSPECTORS set HAS_PROFILE=True \n" +
                    "where INSPECTOR_GUID in (select DISTINCT INSPECTOR_GUID from UNIFIED_COMMENTS)";
                cmd.ExecuteNonQuery();
                
                fillInspectors();

                String searchValue = inspectorForm.inspectorName;

                int rowIndex = -1;

                foreach (DataGridViewRow dgRow in adgvInspectors.Rows)
                {
                    if (dgRow.Cells[1].Value.ToString().Equals(searchValue))
                    {
                        rowIndex = dgRow.Index;
                        break;
                    }
                }

                if (rowIndex >= 0)
                {
                    //dataGridView1.Rows[rowIndex].Selected = true;
                    adgvInspectors.CurrentCell=adgvInspectors[1,rowIndex];
                }
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Delete inspector
            if (adgvInspectors.Rows.Count == 0)
                return;

            int rowIndex = adgvInspectors.CurrentRow.Index;
            int visibleRow = adgvInspectors.FirstDisplayedCell.RowIndex;

            if (adgvInspectors.SelectedRows.Count<2)
            { 
                string msg = "You are going to delete record for the following inspector:\n\n" +
                    adgvInspectors.CurrentRow.Cells["INSPECTOR_NAME"].Value.ToString() + "\n\n" +
                    "Would you like to proceed?";

                var rslt=System.Windows.Forms.MessageBox.Show(msg,"Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                if (rslt == DialogResult.Yes)
                {
                    //Проверяем возможность удаления

                    OleDbCommand cmd = new OleDbCommand("", connection);

                    Guid inspectorGuid = MainForm.StrToGuid(adgvInspectors.CurrentRow.Cells[0].Value.ToString());

                    cmd.CommandText = 
                        "select Count(REPORT_CODE) \n" +
                        "from REPORTS \n" +
                        "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid);

                    int recCount = (int)cmd.ExecuteScalar();

                    if (recCount > 0)
                    {
                        if (recCount == 1)
                            msg = "There is one inspection that was conducted by this inspector.\n" +
                                "Would you like to delete this inspector anyway?";
                        else
                            msg = "There are " + recCount.ToString() + " inspections that were conducted by this inspector.\n" +
                                "Would you like to delete this inspector anyway?";

                        var rs1 = System.Windows.Forms.MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                        if (rs1 == DialogResult.No)
                            return;

                        msg = "All inspections that was condudcted by this inspector will have not any record about inspector";

                        System.Windows.Forms.MessageBox.Show(msg);

                        cmd.CommandText =
                            "update REPORTS set \n" +
                            "INSPECTOR_GUID=Null, \n" +
                            "INSPECTOR_NAME='' \n" +
                            "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid);
                        cmd.ExecuteNonQuery();
                    }

                    cmd.CommandText = 
                        "delete from INSPECTORS where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = 
                        "delete from UNIFIED_COMMENTS where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid);
                    cmd.ExecuteNonQuery();

                   fillInspectors();

                    if (adgvInspectors.Rows.Count > 0)
                    {
                        adgvInspectors.FirstDisplayedScrollingRowIndex = visibleRow;

                        if (adgvInspectors.Rows.Count > 0)
                        {
                            if (adgvInspectors.Rows.Count <= rowIndex)
                            {
                                adgvInspectors.CurrentCell = adgvInspectors[1, adgvInspectors.Rows.Count - 1];
                            }
                            else
                            {
                                adgvInspectors.CurrentCell = adgvInspectors[1, rowIndex];
                            }
                        }
                    }
                }
            }
            else
            {
                string msg = "You are going to delete records for the following inspectors:\n\n";
                int inspectorsSelected = adgvInspectors.SelectedRows.Count;
                int inspectorCounter = 0;


                for (int i = inspectorsSelected - 1; i >= 0; i--)
                {
                    if (inspectorsSelected < 14)
                    {
                        msg = msg + adgvInspectors.SelectedRows[i].Cells["INSPECTOR_NAME"].Value.ToString() + "\n";
                    }
                    else
                    {
                        if (inspectorCounter < 10)
                        {
                            msg = msg + adgvInspectors.SelectedRows[i].Cells["INSPECTOR_NAME"].Value.ToString() + "\n";
                        }
                    }

                    inspectorCounter++;
                }

                if (inspectorsSelected >= 14)
                {
                    msg = msg + "...\nTotal : " + inspectorsSelected.ToString() + " records\n";
                }

                msg = msg + "\n"+"Would you like to proceed?";

                var rslt = System.Windows.Forms.MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (rslt == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;

                    try
                    {
                        int delCounter = 0;
                        int remainCounter = 0;

                        string delInspectors = "";
                        string remainInspectors = "";

                        for (int i = adgvInspectors.SelectedRows.Count - 1; i >= 0; i--)
                        {
                            OleDbCommand cmd = new OleDbCommand("", connection);
                            string inspectorName = adgvInspectors.SelectedRows[i].Cells["INSPECTOR_NAME"].Value.ToString();

                            Guid inspectorGuid = MainForm.StrToGuid(adgvInspectors.SelectedRows[i].Cells["INSPECTOR_GUID"].Value.ToString());

                            cmd.CommandText = 
                                "select Count(REPORT_CODE) \n" +
                                "from REPORTS \n" +
                                "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid);

                            int recCount = (int)cmd.ExecuteScalar();

                            if (recCount > 0)
                            {
                                if (recCount == 1)
                                    msg = "There is an inspection assosiated with inspector " + inspectorName + ".\n" +
                                        "Would you like to delete records for this inspector anyway?";
                                else
                                    msg = "There are " + recCount.ToString() + " inspections assosiated with inspector " + inspectorName + ".\n" +
                                        "Would you like to delete records for this inspector anyway?";

                                var rs1 = System.Windows.Forms.MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                                if (rs1 == DialogResult.Yes)
                                {

                                    msg = "All inspections assosiated with incpector " + inspectorName + " will have not any information about inspector.";

                                    System.Windows.Forms.MessageBox.Show(msg);

                                    cmd.CommandText =
                                        "update REPORTS set \n" +
                                        "INSPECTOR_GUID=Null, \n" +
                                        "INSPECTOR_NAME='' \n" +
                                        "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid);
                                    cmd.ExecuteNonQuery();

                                    cmd.CommandText =
                                        "delete from INSPECTORS \n" +
                                        "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid);
                                    cmd.ExecuteNonQuery();

                                    cmd.CommandText =
                                        "delete from UNIFIED_COMMENTS " +
                                        "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid);
                                    cmd.ExecuteNonQuery();

                                    delCounter++;

                                    if (delInspectors.Length == 0)
                                        delInspectors = inspectorName;
                                    else
                                    {
                                        if (delCounter < 11)
                                            delInspectors = delInspectors + "\n" + inspectorName;
                                    }
                                }
                                else
                                {
                                    remainCounter++;

                                    if (remainInspectors.Length == 0)
                                        remainInspectors = inspectorName;
                                    else
                                    {
                                        if (remainCounter < 11)
                                            remainInspectors = remainInspectors + "\n" + inspectorName;
                                    }
                                }
                            }
                            else
                            {
                                cmd.CommandText =
                                    "delete from INSPECTORS \n" +
                                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid);
                                cmd.ExecuteNonQuery();

                                cmd.CommandText =
                                    "delete from UNIFIED_COMMENTS \n" +
                                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid);
                                cmd.ExecuteNonQuery();

                                delCounter++;

                                if (delInspectors.Length == 0)
                                    delInspectors = inspectorName;
                                else
                                {
                                    if (delCounter < 11)
                                        delInspectors = delInspectors + "\n" + inspectorName;
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
                                    if (delCounter == inspectorsSelected)
                                    {
                                        msgText = "Records for inspector " + delInspectors + " were deleted";
                                    }
                                    else
                                    {
                                        msgText = "Records for inspector " + delInspectors + " were deleted.";

                                        if (inspectorsSelected - delCounter > 1)
                                        {
                                            msgText = msgText + "\n" +
                                                "Records for the following inspectors were not deleted:\n\n" + remainInspectors;

                                            if (inspectorsSelected - delCounter > 10)
                                                msgText = msgText + "\n...\n" +
                                                    "Total : " + (inspectorsSelected - delCounter).ToString() + " records";
                                        }
                                        else
                                            msgText = msgText + "\n" +
                                                "Record for the following inspector " + remainInspectors + " were not deleted";
                                    }
                                    break;
                                default:
                                    msgText = "Records for the following inspectors were deleted:\n\n" + delInspectors;

                                    if (delCounter > 10)
                                        msgText = msgText + "\n...\n" +
                                            "Total : " + delCounter.ToString() + " records";

                                    if (delCounter != inspectorsSelected)
                                    {
                                        msgText = msgText + "\n\n" +
                                            "Records for the following inspectors were not deleted:\n\n" + remainInspectors;

                                        if (inspectorsSelected - delCounter > 10)
                                            msgText = msgText + "\n...\n" +
                                                "Total : " + (inspectorsSelected - delCounter).ToString() + " records";
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            //Nothig was deleted
                            msgText = "Records for the following inspectors were not deleted:\n\n" + remainInspectors;

                            if (inspectorsSelected > 10)
                                msgText = msgText + "\n...\n" +
                                    "Total : " + inspectorsSelected.ToString() + " records";
                        }

                        MessageBox.Show(msgText, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        fillInspectors();

                        if (adgvInspectors.Rows.Count > 0)
                        {
                            if (visibleRow > adgvInspectors.Rows.Count)
                                adgvInspectors.FirstDisplayedScrollingRowIndex = 0;
                            else
                                adgvInspectors.FirstDisplayedScrollingRowIndex = visibleRow;

                            if (adgvInspectors.Rows.Count <= rowIndex)
                            {
                                adgvInspectors.CurrentCell = adgvInspectors[1, adgvInspectors.Rows.Count - 1];
                            }
                            else
                            {
                                adgvInspectors.CurrentCell = adgvInspectors[1, rowIndex];
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

        private void btnMerge_Click(object sender, EventArgs e)
        {

            int firstVisibleRow = 0;

            if (adgvInspectors.FirstDisplayedCell == null) return;


            firstVisibleRow = adgvInspectors.FirstDisplayedCell.RowIndex;
            
            string inspector = adgvInspectors.CurrentRow.Cells["INSPECTOR_NAME"].Value.ToString();

            MergeForm mergeForm = new MergeForm(DS, connection, this.Font, this.Icon);

            if (inspector.Length > 0)
                mergeForm.comboBox1.Text = inspector;

            var rslt=mergeForm.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                //Merge two inspectors
                Guid firstInsGuid = MainForm.StrToGuid(mergeForm.comboBox1.SelectedValue.ToString());

                if (firstInsGuid == MainForm.zeroGuid)
                    return;

                Guid secondInsGuid = MainForm.StrToGuid(mergeForm.comboBox2.SelectedValue.ToString());

                if (secondInsGuid == MainForm.zeroGuid)
                    return;

                int mergeMode = 0;

                if (mergeForm.radioButton11.Checked)
                {
                    mergeMode = 3;
                }
                else
                {
                    if (mergeForm.radioButton9.Checked)
                        mergeMode = 1;
                    else
                        mergeMode = 2;
                }

                OleDbTransaction transaction = connection.BeginTransaction();

                switch (mergeMode)
                {
                    case 1:
                        //Merge to first record
                        //OleDbTransaction transaction=connection.BeginTransaction();
                        cmd.Transaction=transaction;

                        try
                        {
                            if (mergeForm.radioButton2.Checked) //Use photo 2
                            {
                                cmd.CommandText =
                                    "update INSPECTORS set PHOTO='"+StrToSQLStr(mergeForm.photo2.Text)+"' \n"+
                                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(firstInsGuid);
                                cmd.ExecuteNonQuery();
                            }

                            if (mergeForm.radioButton3.Checked) //Merge notes
                            {
                                cmd.CommandText =
                                    "update INSPECTORS set \n" +
                                    "NOTES=NOTES+Chr(13)+Chr(10)+'" +StrToSQLStr(mergeForm.notes2.Text)+"' \n"+
                                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(firstInsGuid);
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                if (mergeForm.radioButton5.Checked) //Use notes 2
                                {
                                    cmd.CommandText =
                                        "update INSPECTORS set \n" +
                                        "NOTES='" + StrToSQLStr(mergeForm.notes2.Text)+"' \n"+
                                        "where INSPECTOR_GUID=" + MainForm.GuidToStr(firstInsGuid);
                                    cmd.ExecuteNonQuery();

                                }
                            }

                            if (mergeForm.radioButton6.Checked) //Merge unfavourable
                            {
                                cmd.CommandText =
                                    "update INSPECTORS set \n" +
                                    "Unfavourable="+mergeForm.valueMerged.Text+" \n"+
                                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(firstInsGuid);
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                if (mergeForm.radioButton8.Checked) //Use value 2
                                {
                                    cmd.CommandText =
                                    "update INSPECTORS set \n" +
                                    "Unfavourable="+mergeForm.value2.Text+" \n"+
                                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(firstInsGuid);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            //Update background
                            if (mergeForm.radioBG2.Checked) //Use 2 backgound
                            {
                                cmd.CommandText=
                                    "update INSPECTORS set \n"+
                                    "Background='"+StrToSQLStr(mergeForm.background2.Text)+"' \n"+
                                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(firstInsGuid);
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                if (mergeForm.radioBGEmpty.Checked)
                                {
                                    cmd.CommandText =
                                        "update INSPECTORS set \n" +
                                        "Background='' \n" +
                                        "where INSPECTOR_GUID=" + MainForm.GuidToStr(firstInsGuid);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            //Update unified comments
                            if (mergeForm.radioUC2.Checked) //Use 2nd comments
                            {
                                cmd.CommandText=
                                    "delete from UNIFIED_COMMENTS \n"+
                                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(firstInsGuid);
                                cmd.ExecuteNonQuery();

                                cmd.CommandText =
                                    "insert into UNIFIED_COMMENTS (INSPECTOR_GUID,SECTION_ID,QUESTION_ID,COMMENTS) \n" +
                                    "select "+firstInsGuid.ToString()+",SECTION_ID, QUESTION_ID, COMMENTS \n" +
                                    "from UNIFIED_COMMENTS \n" +
                                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(secondInsGuid) + ") as Q2 \n" +
                                    "on (UNIFIED_COMMENTS.SECTION_ID=Q2.SECTION_ID \n" +
                                    "and UNIFIED_COMMENTS.QUESTION_ID=Q2.QUESTION_ID)";
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                if (mergeForm.radioUCEmpty.Checked) //Clear comments
                                {
                                    cmd.CommandText =
                                        "delete from UNIFIED_COMMENTS \n" +
                                        "where INSPECTOR_GUID=" + MainForm.GuidToStr(firstInsGuid);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            //Use first record instead of the second record in REPORTS table
                            cmd.CommandText =
                                "update REPORTS set \n" +
                                "INSPECTOR_GUID=" + MainForm.GuidToStr(firstInsGuid) + ", \n" +
                                "INSPECTOR_NAME='" + StrToSQLStr(mergeForm.comboBox1.Text) + "' \n" +
                                "where INSPECTOR_GUID=" + MainForm.GuidToStr(secondInsGuid);
                            cmd.ExecuteNonQuery();

                            //Delete second record
                            cmd.CommandText =
                                "delete from INSPECTORS \n" +
                                "where INSPECTOR_GUID=" + MainForm.GuidToStr(secondInsGuid);
                            cmd.ExecuteNonQuery();

                            //Delete comments for 2 records
                            cmd.CommandText =
                                "delete from UNIFIED_COMMENTS \n" +
                                "where INSPECTOR_GUID=" + MainForm.GuidToStr(secondInsGuid);
                            cmd.ExecuteNonQuery();

                            transaction.Commit();

                            if (DS.Tables.Contains("INSPECTORS"))
                                DS.Tables["INSPECTORS"].Clear();

                            cmd.CommandText =
                                "update INSPECTORS set HAS_PROFILE=False";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText =
                                "update INSPECTORS set HAS_PROFILE=True \n" +
                                "where INSPECTOR_GUID in (select DISTINCT INSPECTOR_GUID from UNIFIED_COMMENTS)";
                            cmd.ExecuteNonQuery();

                            inspectorsAdapter.Fill(DS, "INSPECTORS");

                            fillInspectors();

                            //Восстанавливаем режим просмотра
                            adgvInspectors.FirstDisplayedScrollingRowIndex = firstVisibleRow;

                            //Ищем строку с первым инспектором
                            String searchValue = mergeForm.comboBox1.Text;

                            int rowIndex = -1;

                            foreach (DataGridViewRow dgRow in adgvInspectors.Rows)
                            {
                                if (dgRow.Cells[1].Value.ToString().Equals(searchValue))
                                {
                                    rowIndex = dgRow.Index;
                                    break;
                                }
                            }

                            if (rowIndex >= 0)
                            {
                                adgvInspectors.CurrentCell = adgvInspectors[1, rowIndex];
                            }


                        }
                        catch (System.Exception E)
                        {
                            transaction.Rollback();
                            MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        break;
                    case 2:
                        //Merge to second record
                        cmd.Transaction=transaction;

                        try
                        {
                            if (mergeForm.radioButton1.Checked) //use photo 1
                            {
                                cmd.CommandText =
                                    "update INSPECTORS set PHOTO='"+StrToSQLStr(mergeForm.photo1.Text)+"' \n"+
                                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(secondInsGuid);
                                cmd.ExecuteNonQuery();
                            }

                            if (mergeForm.radioButton3.Checked) //Merge notes
                            {
                                cmd.CommandText =
                                    "update INSPECTORS set \n" +
                                    "NOTES='" +StrToSQLStr(mergeForm.notesMerged.Text)+"' \n"+
                                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(secondInsGuid);
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                if (mergeForm.radioButton4.Checked) //Use notes 1
                                {
                                    cmd.CommandText =
                                        "update INSPECTORS set \n" +
                                        "NOTES='" + StrToSQLStr(mergeForm.notes1.Text)+"' \n"+
                                        "where INSPECTOR_GUID=" + MainForm.GuidToStr(secondInsGuid);
                                    cmd.ExecuteNonQuery();

                                }
                            }

                            if (mergeForm.radioButton6.Checked) //Merge unfavourable
                            {
                                cmd.CommandText =
                                    "update INSPECTORS set \n" +
                                    "UNFAVOURABLE="+mergeForm.valueMerged.Text+" \n"+
                                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(secondInsGuid);
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                if (mergeForm.radioButton7.Checked) //Use value 1
                                {
                                    cmd.CommandText =
                                    "update INSPECTORS set \n" +
                                    "UNFAVOURABLE=" + mergeForm.value1.Text+" \n"+
                                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(secondInsGuid);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            //Update background
                            if (mergeForm.radioBG1.Checked) //Use 1st backgound
                            {
                                cmd.CommandText =
                                    "update INSPECTORS set \n" +
                                    "BACKGROUND='" + StrToSQLStr(mergeForm.background1.Text) + "' \n" +
                                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(secondInsGuid);
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                if (mergeForm.radioBGEmpty.Checked)
                                {
                                    cmd.CommandText =
                                        "update INSPECTORS set \n" +
                                        "BACKGROUND='' \n" +
                                        "where INSPECTOR_GUID=" + MainForm.GuidToStr(secondInsGuid);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            //Update unified comments
                            if (mergeForm.radioUC1.Checked) //Use 1st comments
                            {
                                cmd.CommandText =
                                    "delete from UNIFIED_COMMENTS \n" +
                                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(secondInsGuid);
                                cmd.ExecuteNonQuery();

                                cmd.CommandText =
                                    "insert into UNIFIED_COMMENTS (INSPECTOR_GUID,SECTION_ID,QUESTION_ID,COMMENTS) \n" +
                                    "select " + MainForm.GuidToStr(secondInsGuid) + ",SECTION_ID, QUESTION_ID, COMMENTS \n" +
                                    "from UNIFIED_COMMENTS \n" +
                                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(firstInsGuid) + ") as Q2 \n" +
                                    "on (UNIFIED_COMMENTS.SECTION_ID=Q2.SECTION_ID \n" +
                                    "and UNIFIED_COMMENTS.QUESTION_ID=Q2.QUESTION_ID)";
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                if (mergeForm.radioUCEmpty.Checked) //Clear comments
                                {
                                    cmd.CommandText =
                                        "delete from UNIFIED_COMMENTS \n" +
                                        "where INSPECTOR_GUID=" + MainForm.GuidToStr(secondInsGuid);
                                    cmd.ExecuteNonQuery();
                                }
                            }


                            //Update inspector information in REPORTS table
                            cmd.CommandText =
                                "update REPORTS set \n" +
                                "INSPECTOR_GUID=" + MainForm.GuidToStr(secondInsGuid) + ", \n" +
                                "INSPECTOR_NAME='" + StrToSQLStr(mergeForm.comboBox2.Text) + "' \n" +
                                "where INSPECTOR_GUID=" + MainForm.GuidToStr(firstInsGuid);
                            cmd.ExecuteNonQuery();

                            //Delete first record
                            cmd.CommandText =
                                "delete from INSPECTORS \n" +
                                "where INSPECTOR_GUID=" + MainForm.GuidToStr(firstInsGuid);
                            cmd.ExecuteNonQuery();

                            //Delete comments for 1st record
                            cmd.CommandText =
                                        "delete from UNIFIED_COMMENTS \n" +
                                        "where INSPECTOR_GUID=" + MainForm.GuidToStr(firstInsGuid);
                            cmd.ExecuteNonQuery();

                            transaction.Commit();

                            if (DS.Tables.Contains("INSPECTORS"))
                                DS.Tables["INSPECTORS"].Clear();

                            inspectorsAdapter.Fill(DS, "INSPECTORS");

                            fillInspectors();

                            adgvInspectors.FirstDisplayedScrollingRowIndex = firstVisibleRow;

                            //Ищем строку со вторым инспектором
                            String searchValue = mergeForm.comboBox2.Text;

                            int rowIndex = -1;

                            foreach (DataGridViewRow dgRow in adgvInspectors.Rows)
                            {
                                if (dgRow.Cells[1].Value.ToString().Equals(searchValue))
                                {
                                    rowIndex = dgRow.Index;
                                    break;
                                }
                            }

                            if (rowIndex >= 0)
                            {
                                adgvInspectors.CurrentCell = adgvInspectors[1, rowIndex];
                            }


                        }
                        catch (System.Exception E)
                        {
                            transaction.Rollback();
                            System.Windows.Forms.MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        break;
                    case 3:
                        //Merge records to new record
                        cmd.Transaction=transaction;
                        
                        string newName=mergeForm.textBox1.Text.Trim();
                        Guid newGuid = Guid.NewGuid();

                        try
                        {
                            //Create new record
                            string insStr = "insert into INSPECTORS (INSPECTOR_GUID, INSPECTOR_NAME";
                            string valueStr = "values (" + MainForm.GuidToStr(newGuid) + ",'" + StrToSQLStr(newName) + "'";

                            if (mergeForm.radioButton1.Checked) //Use photo 1
                            {
                                insStr = insStr + ", PHOTO";
                                valueStr = valueStr + ", '" + StrToSQLStr(mergeForm.photo1.Text) + "'";
                            }
                            else //Use photo 2
                            {
                                insStr = insStr + ", PHOTO";
                                valueStr = valueStr + ", '" + StrToSQLStr(mergeForm.photo2.Text) + "'";
                            }


                            if (mergeForm.radioButton3.Checked) //Merge notes
                            {
                                insStr = insStr + ", NOTES";
                                valueStr = valueStr + ", '" + StrToSQLStr(mergeForm.notesMerged.Text) + "'";
                            }
                            else
                            {
                                if (mergeForm.radioButton4.Checked) //Use notes 1
                                {
                                    insStr = insStr + ", NOTES";
                                    valueStr = valueStr + ", '" + StrToSQLStr(mergeForm.notes1.Text) + "'";
                                }
                                else //Use notes 2
                                {
                                    insStr = insStr + ", NOTES";
                                    valueStr = valueStr + ", '" + StrToSQLStr(mergeForm.notes2.Text) + "'";
                                }
                            }

                            if (mergeForm.radioButton6.Checked) //Merge unfavourable
                            {
                                insStr = insStr + ", UNFAVOURABLE";
                                valueStr = valueStr + ", " + mergeForm.valueMerged.Text;
                            }
                            else
                            {
                                if (mergeForm.radioButton7.Checked) //Use value 1
                                {
                                    insStr = insStr + ", UNFAVOURABLE";
                                    valueStr = valueStr + ", " + mergeForm.value1.Text;
                                }
                                else //Use value2
                                {
                                    insStr = insStr + ", UNFAVOURABLE";
                                    valueStr = valueStr + ", " + mergeForm.value2.Text;
                                }
                            }

                            //Update background
                            if (mergeForm.radioBG1.Checked) //Use 1st backgound
                            {
                                insStr = insStr + ", BACKGROUND";
                                valueStr = valueStr + ", '" + StrToSQLStr(mergeForm.background1.Text) + "'";
                            }
                            else
                            {
                                insStr = insStr + ", BACKGROUND";
                                valueStr = valueStr + ", '" + StrToSQLStr(mergeForm.background2.Text) + "'";
                            }

                            cmd.CommandText = insStr + ") \n" +
                                valueStr + ")";

                            MainForm.cmdExecute(cmd);

                            //Update unified comments
                            if (mergeForm.radioUC1.Checked) //Use 1st comments
                            {
                                cmd.CommandText =
                                    "insert into UNIFIED_COMMENTS (INSPECTOR_GUID,SECTION_ID,QUESTION_ID,COMMENTS) \n" +
                                    "select " + MainForm.GuidToStr(newGuid) + ",SECTION_ID, QUESTION_ID, COMMENTS \n" +
                                    "from UNIFIED_COMMENTS \n" +
                                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(firstInsGuid) + ") as Q2 \n" +
                                    "on (UNIFIED_COMMENTS.SECTION_ID=Q2.SECTION_ID \n" +
                                    "and UNIFIED_COMMENTS.QUESTION_ID=Q2.QUESTION_ID)";
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                if (mergeForm.radioUC2.Checked) //Use 2nd comments
                                {
                                    cmd.CommandText =
                                    "insert into UNIFIED_COMMENTS (INSPECTOR_GUID,SECTION_ID,QUESTION_ID,COMMENTS) \n" +
                                    "select " + MainForm.GuidToStr(newGuid) + ",SECTION_ID, QUESTION_ID, COMMENTS \n" +
                                    "from UNIFIED_COMMENTS \n" +
                                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(secondInsGuid) + ") as Q2 \n" +
                                    "on (UNIFIED_COMMENTS.SECTION_ID=Q2.SECTION_ID \n" +
                                    "and UNIFIED_COMMENTS.QUESTION_ID=Q2.QUESTION_ID)";
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            //Update inspector information in REPORTS table
                            cmd.CommandText =
                                "update REPORTS set \n" +
                                "INSPECTOR_GUID=" + MainForm.GuidToStr(newGuid) + " \n" +
                                "where INSPECTOR_GUID=" + MainForm.GuidToStr(firstInsGuid) + " \n" +
                                "or INSPECTOR_GUID=" + MainForm.GuidToStr(secondInsGuid);
                            cmd.ExecuteNonQuery();

                            //Delete first and second record
                            cmd.CommandText =
                                "delete from INSPECTORS \n" +
                                "where INSPECTOR_GUID=" + MainForm.GuidToStr(firstInsGuid) + "\n" +
                                "or INSPECTOR_GUID=" + MainForm.GuidToStr(secondInsGuid);
                            cmd.ExecuteNonQuery();

                            //Delete 1st and 2nd comments
                            cmd.CommandText =
                                "delete from UNIFIED_COMMENTS \n" +
                                "where INSPECTOR_GUID=" + MainForm.GuidToStr(firstInsGuid) + "\n" +
                                "or INSPECTOR_GUID=" + MainForm.GuidToStr(secondInsGuid);
                            cmd.ExecuteNonQuery();

                            transaction.Commit();

                            if (DS.Tables.Contains("INSPECTORS"))
                                DS.Tables["INSPECTORS"].Clear();

                            inspectorsAdapter.Fill(DS, "INSPECTORS");

                            fillInspectors();

                            adgvInspectors.FirstDisplayedScrollingRowIndex = firstVisibleRow;

                            //Ищем строку с новым инспектором
                            String searchValue = newName;

                            int rowIndex = -1;

                            foreach (DataGridViewRow dgRow in adgvInspectors.Rows)
                            {
                                if (dgRow.Cells[1].Value.ToString().Equals(searchValue))
                                {
                                    rowIndex = dgRow.Index;
                                    break;
                                }
                            }

                            if (rowIndex >= 0)
                            {
                                adgvInspectors.CurrentCell = adgvInspectors[1, rowIndex];
                            }


                        }
                        catch (System.Exception E)
                        {
                            transaction.Rollback();
                            System.Windows.Forms.MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }


                        break;
                }

            }
        }

        private int GetInspectorID(string inspectorName)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText = "select top 1 INSPECTOR_GUID from INSPECTORS \n" +
                "where INSPECTOR_NAME='" + StrToSQLStr(inspectorName) + "'";

            int ID = (int)cmd.ExecuteScalar();

            return ID;
        }

        private void fillInspectors()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select * from \n" +
                "(select INSPECTOR_GUID, INSPECTOR_NAME, NOTES, PHOTO, UNFAVOURABLE, BACKGROUND \n" +
                "from INSPECTORS \n" +
                "union \n" +
                "select TOP 1 "+MainForm.GuidToStr(MainForm.zeroGuid)+" as INSPECTOR_GUID, '' as INSPECTOR_NAME, '' as Notes, '' as Photo, " +
                "false as Unfavourable, '' as Background \n" +
                "from INSPECTORS) \n" +
                "order by INSPECTOR_NAME";

            OleDbDataAdapter inspectorsAdapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("INSPECTORS_LIST"))
                DS.Tables["INSPECTORS_LIST"].Clear();

            inspectorsAdapter.Fill(DS, "INSPECTORS_LIST");

            CountRecords();
        }

        private void suggestComboBox201_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                locateRecord();
            }
        }

        private void locateRecord()
        {
            String searchValue = findBox.Text;

            int rowIndex = -1;

            foreach (DataGridViewRow dgRow in adgvInspectors.Rows)
            {
                if (dgRow.Cells[1].Value.ToString().Equals(searchValue))
                {
                    rowIndex = dgRow.Index;
                    break;
                }
            }

            if (rowIndex >= 0)
            {
                //dataGridView1.Rows[rowIndex].Selected = true;
                adgvInspectors.CurrentCell = adgvInspectors[1, rowIndex];
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            locateRecord();
        }

        private void btnSendSummary_Click(object sender, EventArgs e)
        {
            //Create PDF file

            if (adgvInspectors.CurrentRow.Cells["REPORT_NUMBER"].Value==DBNull.Value)
            {
                if (adgvInspectors.Columns["HAS_PROFILE"].Visible == false)
                {
                    MessageBox.Show("There is no any inspection for selected inspector", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }


            string pdfFileName = createPDFFileNew();

            if (pdfFileName.Length == 0)
            {
                if (_send)
                {
                    string s = "Summary file was not created. Would you like to proceed with message creation?";

                    var rslt = MessageBox.Show(s, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (rslt == DialogResult.No)
                        return;
                }
                else
                {
                    MessageBox.Show("Summary file was not created.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                if (!File.Exists(pdfFileName))
                {
                    if (_send)
                    {
                        var rslt = MessageBox.Show("File \"" + pdfFileName + "\" was not found. \n" + "Would you like to proceed with message creation anyway?",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        if (rslt == DialogResult.No)
                            return;
                    }
                    else
                    {
                        MessageBox.Show("Summary file \"" + pdfFileName + "\" was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            if (_send)
            {
                if (_askForVessel)
                {
                    FrmVesselSendSelect form = new FrmVesselSendSelect();

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        vesselEmail = form.vesselEmail;
                        vesselName = form.vesselName;

                        if (vesselEmail.Length == 0)
                        {
                            vesselNameEmail = "";
                        }
                        else
                            vesselNameEmail = vesselName + "<" + vesselEmail + ">";

                        fleetEmail = MainForm.GetFleetEmail(form.vesselGuid);
                    }

                    if (vesselEmail.Length==0)
                    {
                        if (vesselName.Length==0)
                        {
                            var rslt = MessageBox.Show("Vessel was not selected. Would you like to create message without addressee?",
                                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (rslt == DialogResult.No)
                                return;
                        }
                        else
                        {
                            var rslt = MessageBox.Show("Selected vessel has not email address. Would you like to create message without addressee?",
                                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (rslt == DialogResult.No)
                                return;
                        }
                    }
                }

                switch (MainForm.GetMailClientID())
                {
                    case 0: //Outlook
                        SendOutlook(pdfFileName);
                        break;
                    case 1: //MAPI
                        SendMAPI(pdfFileName);
                        break;
                    case 2: //Application
                        SendApplication(pdfFileName);
                        break;
                    default: //Outlook
                        SendOutlook(pdfFileName);
                        break;
                }
                
            }
            else
            {
                var rslt = MessageBox.Show("File \"" + pdfFileName + "\" was created successfully. Would you like to open it?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (rslt==DialogResult.Yes)
                {
                    Process.Start(pdfFileName);
                }

                rslt = MessageBox.Show("Would you like to copy reports file to the folder?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (rslt==DialogResult.Yes)
                {
                    string reportFolder=Path.GetDirectoryName(pdfFileName);

                    long maxSize = -1;
                    int reportCounter = 0;
                    long filesSize = 0;
                    int reportCopied = 0;

                    if (_useSizeLimit)
                    {
                        switch (_dimension)
                        {
                            case "KB":
                                maxSize = _maxSize * 1024;
                                break;
                            case "MB":
                                maxSize = _maxSize * 1024 * 1024;
                                break;
                            case "GB":
                                maxSize = _maxSize * 1024 * 1024 * 1024;
                                break;
                        }
                    }

                    Guid inspectorGuid = MainForm.StrToGuid(adgvInspectors.CurrentRow.Cells["INSPECTOR_GUID"].Value.ToString());

                    cmd.CommandText =
                        "select REPORT_CODE, INSPECTION_DATE \n" +
                        "from REPORTS \n" +
                        "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + "\n" +
                        "order by INSPECTION_DATE DESC";

                    OleDbDataReader reports = cmd.ExecuteReader();

                    if (reports.HasRows)
                    {
                        while ((reports.Read()) && (!_useSizeLimit || (filesSize < maxSize)))
                        {
                            string reportFile = MainForm.workFolder + "\\Reports\\" + reports[0].ToString() + ".pdf";


                            if (File.Exists(reportFile))
                            {
                                FileInfo fi = new FileInfo(reportFile);

                                filesSize = filesSize + fi.Length;

                                if ((!_useSizeLimit) || ((_useSizeLimit) && (filesSize < maxSize)))
                                {
                                    if ((!_useReportCount) || ((_useReportCount) && (reportCounter < _maxReports)))
                                    {
                                        string newFileName = Path.Combine(reportFolder, reports[0].ToString()) + ".pdf";
                                        File.Copy(reportFile, newFileName, true);
                                        reportCopied++;
                                    }
                                }

                                reportCounter++;
                            }
                        }
                    }

                    reports.Close();

                    MessageBox.Show(reportCopied.ToString() + " file(s) was copied to \"" + reportFolder + "\"",
                        "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void getMessageSettings(int tag, ref string  messageSubject, ref string messageText)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select MESSAGE_SUBJECT, MESSAGE_TEXT \n" +
                "from MESSAGE_SETTINGS \n" +
                "where TAG="+tag.ToString();

            OleDbDataReader messageSettings = cmd.ExecuteReader();

            while (messageSettings.Read())
            {
                messageSubject = messageSettings["MESSAGE_SUBJECT"].ToString();
                messageText = messageSettings["MESSAGE_TEXT"].ToString();
            }

            messageSettings.Close();

        }

        private string createPDFFile()
        {
            string inspectorName = adgvInspectors.CurrentRow.Cells["INSPECTOR_NAME"].Value.ToString();
            Guid inspectorGuid = MainForm.StrToGuid(adgvInspectors.CurrentRow.Cells["INSPECTOR_GUID"].Value.ToString());

            //string reportFolder = MainForm.workFolder + "\\Inspectors summary";
            string reportFolder = MainForm.appTempFolderName + "\\Inspectors summary";

            if (!Directory.Exists(reportFolder))
            {
                Directory.CreateDirectory(reportFolder);
            }

            string strToday=DateTime.Today.ToString("yyyy-MM-dd");

            string pdfFileName = reportFolder+"\\Summary for "+inspectorName+" ("+strToday+").pdf";
            
            //Word.Documents wordDocuments;
            Word.Document wordDocument;

            //Create Word document
            Word.Application wordapp = new Word.Application();
                
            //Show Word window
            wordapp.Visible = false;

            this.Cursor = Cursors.WaitCursor;

            try
            {
                Object template = Type.Missing;
                Object newTemplate = false;
                Object documentType = Word.WdNewDocumentType.wdNewBlankDocument;
                Object visible = true;
                OleDbCommand cmd = new OleDbCommand("", connection);
                Object defaultTableBehavior = Word.WdDefaultTableBehavior.wdWord9TableBehavior;
                Object autoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitWindow;

                //Создаем документ
                wordDocument = wordapp.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible);

                wordapp.Options.CheckGrammarAsYouType = false;
                wordapp.Options.CheckGrammarWithSpelling = false;
                wordapp.Options.CheckSpellingAsYouType = false;
                wordapp.Options.ContextualSpeller = false;
                wordapp.Options.ShowReadabilityStatistics = false;
                wordDocument.ShowGrammaticalErrors = false;
                wordDocument.ShowSpellingErrors = false;

                wordDocument.PageSetup.LeftMargin = 42;
                wordDocument.PageSetup.RightMargin = 42;
                wordDocument.PageSetup.TopMargin = 42;
                wordDocument.PageSetup.BottomMargin = 42;

                Word.Paragraphs wordParagraphs;
                Word.Paragraph wordParagraph;

                wordParagraphs = wordDocument.Paragraphs;

                wordParagraph = wordParagraphs[1];

                wordParagraph.Range.Text = "Inspector : " + inspectorName;

                Object begin = 12;
                Object end = inspectorName.Length + 12;
                Word.Range namerange = wordDocument.Range(ref begin, ref end);
                namerange.Font.Bold = 1;

                wordDocument.Paragraphs.Add();
                wordParagraph = wordDocument.Paragraphs[wordDocument.Paragraphs.Count];
                wordParagraph.Range.Font.Size = 14;

                wordParagraph.Range.Text = "1. Performance report";

                //Check unified comments for inspector
                cmd.CommandText =
                    "select Count(COMMENTS) as Records \n" +
                    "from UNIFIED_COMMENTS \n" +
                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + " \n" +
                    "and LEN(TRIM(COMMENTS))>0";

                int profileRecCount = (int)cmd.ExecuteScalar();

                //Check observations for inspector
                cmd.CommandText =
                    "select Count(REPORT_ITEMS.OBSERVATION) as ObsCount \n" +
                    "from REPORT_ITEMS inner join \n" +
                    "(select DISTINCT REPORT_ITEMS.QUESTION_NUMBER, REPORT_ITEMS.REPORT_CODE, SEQUENCE \n" +
                    "from (REPORT_ITEMS inner join REPORTS \n" +
                    "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE) \n" +
                    "left join TEMPLATE_QUESTIONS \n" +
                    "on REPORT_ITEMS.QUESTION_GUID=TEMPLATE_QUESTIONS.QUESTION_GUID \n" +
                    "where REPORTS.INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + "\n" +
                    "and LEN(OBSERVATION)>0) as Q1 \n" +
                    "on REPORT_ITEMS.REPORT_CODE=Q1.REPORT_CODE \n" +
                    "and REPORT_ITEMS.QUESTION_NUMBER=Q1.QUESTION_NUMBER";

                int obsRecCount = (int)cmd.ExecuteScalar();


                if (profileRecCount == 0)
                {
                    wordDocument.Paragraphs.Add();
                    wordParagraph = wordDocument.Paragraphs[wordDocument.Paragraphs.Count];
                    wordParagraph.Range.Font.Size = 10;
                    wordParagraph.Range.Text = "There is no performance report yet.";
                }
                else
                {
                    //Add first table

                    //Добавляем таблицу и получаем объект wordtable

                    wordDocument.Paragraphs.Add();
                    wordParagraph = wordDocument.Paragraphs[wordDocument.Paragraphs.Count];
                    wordParagraph.Range.Font.Size = 10;

                    Word.Range table1range = wordParagraph.Range;


                    cmd.CommandText =
                        "select UNIFIED_QUESTIONS.ID, UNIFIED_QUESTIONS.SECTION_ID, " +
                        "UNIFIED_QUESTIONS.QUESTION_ID, UNIFIED_QUESTIONS.QUESTION, " +
                        "UNIFIED_COMMENTS.COMMENT_ID, UNIFIED_COMMENTS.COMMENTS, " +
                        "UNIFIED_QUESTIONS.COLOR \n" +
                        "from UNIFIED_QUESTIONS left join UNIFIED_COMMENTS \n" +
                        "on (UNIFIED_QUESTIONS.SECTION_ID=UNIFIED_COMMENTS.SECTION_ID \n" +
                        "and UNIFIED_QUESTIONS.QUESTION_ID=UNIFIED_COMMENTS.QUESTION_ID \n" +
                        "and UNIFIED_COMMENTS.INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + ") \n" +
                        "order by UNIFIED_QUESTIONS.SECTION_ID, UNIFIED_QUESTIONS.QUESTION_ID";

                    OleDbDataAdapter questions = new OleDbDataAdapter(cmd);

                    if (DS.Tables.Contains("UNIFIED_QUESTIONS"))
                        DS.Tables["UNIFIED_QUESTIONS"].Clear();

                    questions.Fill(DS, "UNIFIED_QUESTIONS");

                    DataTable evalTable = DS.Tables["UNIFIED_QUESTIONS"];

                    int table1Rows = evalTable.Rows.Count + 1;
                    int table1Columns = 2;

                    Word.Table wordtable1 = wordDocument.Tables.Add(table1range, table1Rows, table1Columns,
                                      ref defaultTableBehavior, ref autoFitBehavior);

                    //return "";

                    wordtable1.Cell(1, 1).Range.Text = "Criteria";
                    wordtable1.Cell(1, 1).Range.Font.Bold = 1;
                    wordtable1.Cell(1, 1).Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray05;
                    wordtable1.Cell(1, 2).Range.Text = "Comments";
                    wordtable1.Cell(1, 2).Range.Font.Bold = 1;
                    wordtable1.Cell(1, 2).Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray05;

                    for (int i = 0; i < table1Rows - 1; i++)
                    {
                        wordtable1.Cell(i + 2, 1).Range.Text = evalTable.Rows[i].ItemArray[3].ToString();
                        wordtable1.Cell(i + 2, 2).Range.Text = evalTable.Rows[i].ItemArray[5].ToString();
                        if (evalTable.Rows[i].ItemArray[2].ToString() == "0")
                        {
                            wordtable1.Cell(i + 2, 1).Range.Font.Bold = 1;
                            wordtable1.Cell(i + 2, 1).Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorLightYellow;
                            wordtable1.Cell(i + 2, 2).Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorLightYellow;
                        }
                    }
                }

                //Add second table
                wordDocument.Paragraphs.Add();
                wordParagraph = wordDocument.Paragraphs[wordDocument.Paragraphs.Count];
                wordParagraph.Range.Text = "2. Inspector’s observations";
                wordParagraph.Range.Font.Size = 14;

                wordDocument.Paragraphs.Add();
                wordParagraph = wordDocument.Paragraphs[wordDocument.Paragraphs.Count];
                wordParagraph.Range.Font.Size = 10;

                if (obsRecCount == 0)
                {
                    wordParagraph.Range.Text = "There is no observation yet.";
                }
                else
                {
                    Word.Range table2range = wordParagraph.Range;

                    cmd.CommandText =
                        "select REPORT_ITEMS.QUESTION_NUMBER, REPORT_ITEMS.OBSERVATION \n" +
                        "from REPORT_ITEMS inner join \n" +
                        "(select DISTINCT REPORT_ITEMS.QUESTION_NUMBER, REPORT_ITEMS.REPORT_CODE, SEQUENCE \n" +
                        "from (REPORT_ITEMS inner join REPORTS \n" +
                        "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE) \n" +
                        "left join TEMPLATE_QUESTIONS \n" +
                        "on REPORT_ITEMS.QUESTION_GUID=TEMPLATE_QUESTIONS.QUESTION_GUID \n" +
                        "where REPORTS.INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + "\n" +
                        "and LEN(OBSERVATION)>0) as Q1 \n" +
                        "on REPORT_ITEMS.REPORT_CODE=Q1.REPORT_CODE \n" +
                        "and REPORT_ITEMS.QUESTION_NUMBER=Q1.QUESTION_NUMBER \n" +
                        "order by Q1.SEQUENCE";

                    OleDbDataAdapter inspectorObs = new OleDbDataAdapter(cmd);

                    if (DS.Tables.Contains("OBS_BY_INSPECTOR"))
                        DS.Tables["OBS_BY_INSPECTOR"].Clear();

                    inspectorObs.Fill(DS, "OBS_BY_INSPECTOR");

                    DataTable obsTable = DS.Tables["OBS_BY_INSPECTOR"];


                    int table2Rows = obsTable.Rows.Count + 1;
                    int table2Columns = 2;

                    Word.Table wordtable2 = wordDocument.Tables.Add(table2range, table2Rows, table2Columns,
                                      ref defaultTableBehavior, ref autoFitBehavior);

                    float fullWidth = wordtable2.Columns[1].Width + wordtable2.Columns[2].Width;
                    wordtable2.Columns[1].Width = 50;
                    wordtable2.Columns[2].Width = fullWidth - wordtable2.Columns[1].Width;
                    wordtable2.Cell(1, 1).Range.Text = "VIQ ref";
                    wordtable2.Cell(1, 1).Range.Font.Bold = 1;
                    wordtable2.Cell(1, 1).Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray05;
                    wordtable2.Cell(1, 2).Range.Text = "Observation";
                    wordtable2.Cell(1, 2).Range.Font.Bold = 1;
                    wordtable2.Cell(1, 2).Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray05;
                    wordtable2.PreferredWidthType = Word.WdPreferredWidthType.wdPreferredWidthAuto;

                    for (int i = 0; i < table2Rows - 1; i++)
                    {
                        wordtable2.Cell(i + 2, 1).Range.Text = obsTable.Rows[i].ItemArray[0].ToString();
                        wordtable2.Cell(i + 2, 2).Range.Text = obsTable.Rows[i].ItemArray[1].ToString();
                    }
                }

                string notes = adgvInspectors.CurrentRow.Cells["Notes"].Value.ToString().Trim();

                if (notes.Length > 0)
                {
                    wordDocument.Paragraphs.Add();
                    wordParagraph = wordDocument.Paragraphs[wordDocument.Paragraphs.Count];
                    wordParagraph.Range.Text = "3. Notes";
                    wordParagraph.Range.Font.Size = 14;

                    wordDocument.Paragraphs.Add();
                    wordParagraph = wordDocument.Paragraphs[wordDocument.Paragraphs.Count];
                    wordParagraph.Range.Font.Size = 10;

                    wordParagraph.Range.Text = notes;
                }

                //Save Word document as PDF
                bool saveSuccessfull = false;
                try
                {
                    wordDocument.SaveAs2(pdfFileName, Word.WdSaveFormat.wdFormatPDF);
                    saveSuccessfull = true;
                }
                catch (Exception E)
                {
                    MessageBox.Show("Error during saving file in PDF format: \n"+E.Message,"External Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }

                Object saveChanges = Word.WdSaveOptions.wdDoNotSaveChanges;
                Object originalFormat = Word.WdOriginalFormat.wdOriginalDocumentFormat;
                Object routeDocument = Type.Missing;

                ((Word._Application)wordapp).Quit(ref saveChanges, ref originalFormat, ref routeDocument);

                

                //MessageBox.Show("File \"" + pdfFileName + "\" created");
                if (saveSuccessfull)
                    return pdfFileName;
                else
                    return "";
            }
            finally
            {
                wordapp = null;
                this.Cursor = Cursors.Default;
            }


        }

        private string createPDFFileNew()
        {
            //if (adgvInspectors.CurrentRow.Cells["REPORT_NUMBER"].Value == DBNull.Value)
            //    return "";

            string inspectorName = adgvInspectors.CurrentRow.Cells["INSPECTOR_NAME"].Value.ToString();
            Guid inspectorGuid = MainForm.StrToGuid(adgvInspectors.CurrentRow.Cells["INSPECTOR_GUID"].Value.ToString());
            string inspectorBackground = adgvInspectors.CurrentRow.Cells["BACKGROUND"].Value.ToString();
            string fontName = "Times New Roman";

            //string reportFolder = MainForm.workFolder + "\\Inspectors summary";

            string reportFolder = MainForm.appTempFolderName + "\\Inspectors summary";

            if (!_send)
            {
                if (!_useTempFolder)
                {
                    if (Directory.Exists(_userFolder))
                        reportFolder = _userFolder;
                    else
                    {    
                        var rslt = MessageBox.Show("Folder \"" + _userFolder + "\" does not exist. \n" +
                            "Would youlike to create it?","Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                        if (rslt == DialogResult.Yes)
                        {
                            try
                            {
                                Directory.CreateDirectory(_userFolder);
                                reportFolder = _userFolder;
                            }
                            catch (Exception E)
                            {
                                MessageBox.Show("Failed to create folder: \n"+E.Message+"\n\n"+
                                    "Applcation temporary folder will be used.", "Warning",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                            MessageBox.Show("Applcation temporary folder will be used.", "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                if (!Directory.Exists(reportFolder))
                {
                    try
                    {
                        Directory.CreateDirectory(reportFolder);
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show("Failed to create folder \"" + reportFolder + "\" \n\n" +
                            E.Message + "\n\n" +
                            "Report creation terminated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return "";
                    }
                }
            }

            //bool DelResult;

            try
            {
                //Delete report folder
                Directory.Delete(reportFolder, true);
                //Create report folder
                Directory.CreateDirectory(reportFolder);
                //DelResult = true;
            }
            catch (Exception E)
            {
                MessageBox.Show("Upable to delete folder:\n"+E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Clear report folder
            /*
            DirectoryInfo di = new DirectoryInfo(reportFolder);

            foreach (FileInfo file in di.GetFiles())
            {
                try
                {
                    file.Delete();
                }
                catch (Exception E)
                {
                    MessageBox.Show("Unable to delete file \"" + file.Name + "\"\n" + E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            */

            if (!Directory.Exists(reportFolder))
            {
                MessageBox.Show("Folder " + reportFolder + " does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }

            Color fontColor = Color.FromArgb(47, 84, 150);
            //$96542F

            Word.WdColor wdFontColor = (Word.WdColor)(fontColor.R + 0x100 * fontColor.G + 0x10000 * fontColor.B);

            Color headerColor = Color.FromArgb(211, 223, 230);
            Word.WdColor wdHeaderColor = (Word.WdColor)(headerColor.R + 0x100 * headerColor.G + 0x10000 * headerColor.B);

            Color headerSubColor = Color.FromArgb(217, 217, 217);
            Word.WdColor wdHeaderSubColor = (Word.WdColor)(headerSubColor.R + 0x100 * headerSubColor.G + 0x10000 * headerSubColor.B);

            Color borderColor=Color.FromArgb(79,129,189);
            Word.WdColor wdBorderColor=(Word.WdColor)(borderColor.R+0x100*borderColor.G+0x10000*borderColor.B);

            Word.WdColor wdTextColor=Word.WdColor.wdColorBlack;


            string summaryDate = "";
                
            if (adgvInspectors.CurrentRow.Cells["PROFILE_DATE"].Value.ToString().Length==0)
            {
                //Get last inspection date
                OleDbCommand cmd = new OleDbCommand("", connection);

                cmd.CommandText =
                    "select Max(INSPECTION_DATE) as MaxDate \n" +
                    "from REPORTS \n" +
                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid);

                Object value = cmd.ExecuteScalar();

                if (value == DBNull.Value)
                    summaryDate = "Unknown";
                else
                {
                    DateTime d = Convert.ToDateTime(value);
                    summaryDate = d.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                DateTime sDate = Convert.ToDateTime(adgvInspectors.CurrentRow.Cells["PROFILE_DATE"].Value);
                summaryDate = sDate.ToString("yyyy-MM-dd");
            }

            string pdfFileName = reportFolder + "\\Summary for " + inspectorName + " (" + summaryDate + ").pdf";

            //Word.Documents wordDocuments;
            Word.Document wordDocument;

            //Create Word document
            Word.Application wordapp = new Word.Application();

            //Show Word window
            wordapp.Visible = false;

            this.Cursor = Cursors.WaitCursor;

            try
            {
                Object template = Type.Missing;
                Object newTemplate = false;
                Object documentType = Word.WdNewDocumentType.wdNewBlankDocument;
                Object visible = true;
                OleDbCommand cmd = new OleDbCommand("", connection);
                Object defaultTableBehavior = Word.WdDefaultTableBehavior.wdWord9TableBehavior;
                Object autoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitWindow;

                //Создаем документ
                wordDocument = wordapp.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible);

                wordapp.Options.CheckGrammarAsYouType = false;
                wordapp.Options.CheckGrammarWithSpelling = false;
                wordapp.Options.CheckSpellingAsYouType = false;
                wordapp.Options.ContextualSpeller = false;
                wordapp.Options.ShowReadabilityStatistics = false;
                wordDocument.ShowGrammaticalErrors = false;
                wordDocument.ShowSpellingErrors = false;

                wordDocument.PageSetup.LeftMargin = 42;
                wordDocument.PageSetup.RightMargin = 42;
                wordDocument.PageSetup.TopMargin = 42;
                wordDocument.PageSetup.BottomMargin = 42;

                Word.Paragraphs wordParagraphs;
                Word.Paragraph wordParagraph;

                wordParagraphs = wordDocument.Paragraphs;

                wordParagraph = wordParagraphs[1];
                wordParagraph.Range.Font.Name = fontName;
                //wordParagraph.Range.Font.Color = wdFontColor;

                //Create header table
                Word.Range tableHeaderRange = wordParagraph.Range;

                Word.Table tableHeader = wordDocument.Tables.Add(tableHeaderRange, 3, 2);

                tableHeader.Columns[1].Width = 340;
                tableHeader.Columns[2].Width = 160;
                tableHeader.Rows[3].Height = 180;

                //Inspector name
                tableHeader.Cell(1, 1).Range.Text = inspectorName;
                tableHeader.Cell(1, 1).Range.Font.Name = fontName;
                tableHeader.Cell(1, 1).Range.Font.Size = 14;
                tableHeader.Cell(1, 1).Range.Font.Bold = 1;
                tableHeader.Cell(1, 1).Range.Font.Color = wdFontColor;

                tableHeader.Cell(2, 1).Range.Font.Name = fontName;
                tableHeader.Cell(2, 1).Range.Font.Size = 11;
                tableHeader.Cell(2, 1).Range.Font.Bold = 1;
                tableHeader.Cell(2, 1).Range.Font.Italic = 1;
                tableHeader.Cell(2, 1).Range.Font.Color = wdFontColor;
                tableHeader.Cell(2, 1).Range.Text = inspectorBackground;

                tableHeader.Cell(1, 2).Merge(tableHeader.Cell(3, 2));

                Word.Range picRange = tableHeader.Cell(1, 2).Range;
                Word.Range chartRange = tableHeader.Cell(2,1).Range;

                string photoFile=adgvInspectors.CurrentRow.Cells["Photo"].Value.ToString();

                if (photoFile.Length>0)
                {
                    photoFile = Path.Combine(MainForm.workFolder + "\\Photo", photoFile);
                    
                    if (!File.Exists(photoFile))
                        photoFile = "";
                }

                if (photoFile.Length==0)
                {
                    photoFile = Path.Combine(MainForm.serviceFolder, "NoPhotoAvailable-4x5.jpg");

                    if (!File.Exists(photoFile))
                        photoFile = "";
                }

                if (photoFile.Length > 0)
                {
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                    pictureBox.Image = Image.FromFile(photoFile);

                    double pictureHeight = pictureBox.Height;
                    double pictureWidth = pictureBox.Width;

                    Word.InlineShape shape = picRange.InlineShapes.AddPicture(photoFile);
                    
                    if (pictureHeight == pictureWidth)
                    {
                        shape.Height = 150;
                        shape.Width = 150;
                    }
                    else
                    {
                        if (pictureHeight > pictureWidth)
                        {
                            shape.Height = 150;
                            double xFactor = (double)150 * (pictureWidth / pictureHeight);
                            shape.Width =Convert.ToInt16(150 * (pictureWidth / pictureHeight));
                        }
                        else
                        {
                            shape.Width = 150;
                            shape.Height = Convert.ToInt16(150 * (pictureHeight / pictureWidth));
                        }
                    }

                    pictureBox.Dispose();
                }

                if (_useChart && adgvInspectors.CurrentRow.Cells["REPORT_NUMBER"].Value != DBNull.Value)
                {
                    //Create chart
                    Word.Chart aChart = (Word.Chart)wordapp.ActiveDocument.Shapes.AddChart().Chart;

                    aChart.ChartData.Workbook.Application.Visible = false;

                    //Word.Chart iChart = chartRange.InlineShapes.AddChart().Chart;

                    aChart.HasLegend = false;
                    aChart.HasTitle = true;
                    aChart.ShowDataLabelsOverMaximum = true;
                    aChart.ChartArea.Format.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                    aChart.ChartTitle.Caption = "Scoring by chapter";
                    aChart.ChartTitle.Top = 2;
                    aChart.ChartArea.Format.TextFrame2.TextRange.Font.Size = 12;
                    aChart.ApplyDataLabels(Word.XlDataLabelsType.xlDataLabelsShowValue);
                    aChart.Axes(Word.XlAxisType.xlValue).MajorGridlines.Format.Line.Visible = false;
                    aChart.Axes(Word.XlAxisType.xlValue).MajorTickMark = Word.XlTickMark.xlTickMarkNone;
                    aChart.Axes(Word.XlAxisType.xlValue).TickLabelPosition = Word.XlTickLabelPosition.xlTickLabelPositionNone;
                    aChart.Axes(Word.XlAxisType.xlValue).Border.Weight = Word.XlBorderWeight.xlHairline;
                    aChart.Axes(Word.XlAxisType.xlValue).Border.LineStyle = Word.XlLineStyle.xlLineStyleNone;
                    aChart.Axes(Word.XlAxisType.xlValue).MinorGridlines.Format.Line.Visible = false;

                    Excel.Worksheet chartSheet = aChart.ChartData.Workbook.Worksheets(1);

                    chartSheet.ListObjects[1].DataBodyRange.Delete();
                    chartSheet.ListObjects[1].Resize(chartSheet.Range["A1:B14"]);


                    string tempTableName = MainForm.BuildInspectorScoring(inspectorGuid);

                    if (tempTableName.Length == 0)
                    {
                        MessageBox.Show("Failed to build inspector scoring", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return "";
                    }

                    cmd.CommandText =
                        "select * \n" +
                        "from [" + tempTableName + "] \n" +
                        "order by CHAPTER";

                    OleDbDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        int[,] array = new int[13, 2];

                        while (reader.Read())
                        {
                            int chapter = Convert.ToInt32(reader["CHAPTER"]);
                            int obsCount = Convert.ToInt32(reader["OBS_COUNT"]);

                            array[chapter - 1, 0] = chapter;
                            array[chapter - 1, 1] = obsCount;
                        }

                        string[,] header = new string[1, 2];

                        header[0, 0] = "Chapter";
                        header[0, 1] = "Value";

                        chartSheet.Range["A1:B1"].Value2 = header;
                        chartSheet.Range["A2:B14"].Value2 = array;
                    }

                    reader.Close();

                    aChart.ChartData.Workbook.Close();

                    //wordDocument.Paragraphs.Add();
                    //wordParagraph = wordDocument.Paragraphs[wordDocument.Paragraphs.Count];
                    //wordParagraph.Range.Font.Size = 11;

                    aChart.Parent.Width = 320;
                    aChart.Parent.Height = 160;

                    aChart.ChartGroups(1).GapWidth = 20;

                    aChart.Parent.Left = 0;
                    aChart.Parent.Top = 60;
                }

                wordDocument.Paragraphs.Add();
                wordParagraph = wordDocument.Paragraphs[wordDocument.Paragraphs.Count];
                wordParagraph.Range.Font.Size = 11;
                wordParagraph.Range.Font.Name =fontName;
                wordParagraph.Range.Font.Bold = 1;
                wordParagraph.Range.Font.Color = wdFontColor;
                wordParagraph.Range.Text = "1. Notes";

                wordDocument.Paragraphs.Add();
                wordParagraph = wordDocument.Paragraphs[wordDocument.Paragraphs.Count];
                wordParagraph.Range.Font.Size = 10;
                wordParagraph.Range.Font.Name = fontName;
                wordParagraph.Range.Font.Bold = 0;
                wordParagraph.Range.Font.Color = wdTextColor;

                string notesText = adgvInspectors.CurrentRow.Cells["Notes"].Value.ToString();

                if (notesText.Trim().Length == 0)
                    notesText = "Notes was not provided";

                wordParagraph.Range.Text = notesText;

                wordDocument.Paragraphs.Add();
                wordParagraph = wordDocument.Paragraphs[wordDocument.Paragraphs.Count];

                wordParagraph.Range.Text = "2. Performance report";
                wordParagraph.Range.Font.Size = 11;
                wordParagraph.Range.Font.Name = fontName;
                wordParagraph.Range.Font.Bold = 1;
                wordParagraph.Range.Font.Color = wdFontColor;

                //Check unified comments for inspector
                cmd.CommandText =
                    "select Count(COMMENTS) as Records \n" +
                    "from UNIFIED_COMMENTS \n" +
                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + " \n" +
                    "and LEN(TRIM(COMMENTS))>0";

                int profileRecCount = (int)cmd.ExecuteScalar();

                //Check observations for inspector
                cmd.CommandText =
                    "select Count(REPORT_ITEMS.OBSERVATION) as ObsCount \n" +
                    "from REPORT_ITEMS inner join \n" +
                    "(select DISTINCT REPORT_ITEMS.QUESTION_NUMBER, REPORT_ITEMS.REPORT_CODE, SEQUENCE \n" +
                    "from (REPORT_ITEMS inner join REPORTS \n" +
                    "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE) \n" +
                    "left join TEMPLATE_QUESTIONS \n" +
                    "on REPORT_ITEMS.QUESTION_GUID=TEMPLATE_QUESTIONS.QUESTION_GUID \n" +
                    "where REPORTS.INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + "\n" +
                    "and LEN(OBSERVATION)>0) as Q1 \n" +
                    "on REPORT_ITEMS.REPORT_CODE=Q1.REPORT_CODE \n" +
                    "and REPORT_ITEMS.QUESTION_NUMBER=Q1.QUESTION_NUMBER";

                int obsRecCount = (int)cmd.ExecuteScalar();


                if (profileRecCount == 0)
                {
                    wordDocument.Paragraphs.Add();
                    wordParagraph = wordDocument.Paragraphs[wordDocument.Paragraphs.Count];
                    wordParagraph.Range.Font.Size = 10;
                    wordParagraph.Range.Font.Name = fontName;
                    wordParagraph.Range.Font.Color = wdTextColor;
                    wordParagraph.Range.Font.Bold = 0;
                    wordParagraph.Range.Text = "There is no performance report yet.";
                }
                else
                {
                    //Add first table

                    //Добавляем таблицу и получаем объект wordtable

                    wordDocument.Paragraphs.Add();
                    wordParagraph = wordDocument.Paragraphs[wordDocument.Paragraphs.Count];
                    wordParagraph.Range.Font.Size = 10;
                    wordParagraph.Range.Font.Color = wdTextColor;

                    Word.Range table1range = wordParagraph.Range;


                    cmd.CommandText =
                        "select UNIFIED_QUESTIONS.ID, UNIFIED_QUESTIONS.SECTION_ID, " +
                        "UNIFIED_QUESTIONS.QUESTION_ID, UNIFIED_QUESTIONS.QUESTION, " +
                        "UNIFIED_COMMENTS.COMMENT_ID, UNIFIED_COMMENTS.COMMENTS, " +
                        "UNIFIED_QUESTIONS.COLOR \n" +
                        "from UNIFIED_QUESTIONS left join UNIFIED_COMMENTS \n" +
                        "on (UNIFIED_QUESTIONS.SECTION_ID=UNIFIED_COMMENTS.SECTION_ID \n" +
                        "and UNIFIED_QUESTIONS.QUESTION_ID=UNIFIED_COMMENTS.QUESTION_ID \n" +
                        "and UNIFIED_COMMENTS.INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + ") \n" +
                        "order by UNIFIED_QUESTIONS.SECTION_ID, UNIFIED_QUESTIONS.QUESTION_ID";

                    OleDbDataAdapter questions = new OleDbDataAdapter(cmd);

                    if (DS.Tables.Contains("UNIFIED_QUESTIONS"))
                        DS.Tables["UNIFIED_QUESTIONS"].Clear();

                    questions.Fill(DS, "UNIFIED_QUESTIONS");

                    DataTable evalTable = DS.Tables["UNIFIED_QUESTIONS"];

                    int table1Rows = evalTable.Rows.Count + 1;
                    int table1Columns = 2;

                    Word.Table wordtable1 = wordDocument.Tables.Add(table1range, table1Rows, table1Columns,
                                      ref defaultTableBehavior, ref autoFitBehavior);

                    //return "";

                    //wordtable1.Range.Borders[Word.WdBorderType.wdBorderLeft].Color = wdBorderColor;
                    //wordtable1.Range.Borders[Word.WdBorderType.wdBorderRight].Color = wdBorderColor;
                    //wordtable1.Range.Borders[Word.WdBorderType.wdBorderTop].Color = wdBorderColor;
                    //wordtable1.Range.Borders[Word.WdBorderType.wdBorderBottom].Color = wdBorderColor;
                    //wordtable1.Range.Borders[Word.WdBorderType.wdBorderHorizontal].Color = wdBorderColor;

                    wordtable1.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    wordtable1.Borders.InsideLineWidth = Word.WdLineWidth.wdLineWidth025pt;
                    wordtable1.Borders.InsideColor = wdBorderColor;

                    wordtable1.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    wordtable1.Borders.OutsideLineWidth = Word.WdLineWidth.wdLineWidth025pt;
                    wordtable1.Borders.OutsideColor = wdBorderColor;

                    /*
                    try
                    {
                        if (wordtable1.Range.Borders[Word.WdBorderType.wdBorderVertical] != null)
                            wordtable1.Range.Borders[Word.WdBorderType.wdBorderVertical].Color = wdBorderColor;
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show(E.Message);
                    }

                    */

                    wordtable1.Cell(1, 1).Range.Text = "Criteria";
                    wordtable1.Cell(1, 1).Range.Font.Bold = 1;
                    wordtable1.Cell(1, 1).Range.Font.Name = fontName;
                    wordtable1.Cell(1, 1).Range.Font.Size = 8;
                    wordtable1.Cell(1, 1).Range.Shading.BackgroundPatternColor = wdHeaderColor;
                    //wordtable1.Cell(1, 1).Range.Borders[Word.WdBorderType.wdBorderLeft].Color = wdBorderColor;

                    //Word.WdColor bdColor = wordtable1.Cell(1, 1).Range.Borders[Word.WdBorderType.wdBorderLeft].Color;

                    //MessageBox.Show("Border color=" + wdBorderColor.ToString() + " Real color=" + bdColor.ToString());
                   
                    wordtable1.Cell(1, 2).Range.Text = "Comments";
                    wordtable1.Cell(1, 2).Range.Font.Bold = 1;
                    wordtable1.Cell(1, 2).Range.Font.Name = fontName;
                    wordtable1.Cell(1, 2).Range.Font.Size = 8;
                    wordtable1.Cell(1, 2).Range.Shading.BackgroundPatternColor = wdHeaderColor;

                    wordtable1.Rows.AllowBreakAcrossPages = 0;
                    
                    for (int i = 0; i < table1Rows - 1; i++)
                    {
                        wordtable1.Cell(i + 2, 1).Range.Text = evalTable.Rows[i].ItemArray[3].ToString();
                        wordtable1.Cell(i + 2, 1).Range.Font.Name = fontName;
                        wordtable1.Cell(i + 2, 1).Range.Font.Size = 8;
                        wordtable1.Cell(i + 2, 1).Range.Font.Bold = 0;
                        wordtable1.Cell(i + 2, 1).Range.Borders[Word.WdBorderType.wdBorderLeft].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
                        wordtable1.Cell(i + 2, 1).Range.Borders[Word.WdBorderType.wdBorderLeft].Color = wdBorderColor;

                        wordtable1.Cell(i + 2, 2).Range.Text = evalTable.Rows[i].ItemArray[5].ToString();
                        wordtable1.Cell(i + 2, 2).Range.Font.Name = fontName;
                        wordtable1.Cell(i + 2, 2).Range.Font.Size = 8;
                        wordtable1.Cell(i + 2, 2).Range.Font.Bold = 0;
                        wordtable1.Cell(i + 2, 2).Range.Font.Italic = 1;
                        wordtable1.Cell(i + 2, 2).Range.Borders[Word.WdBorderType.wdBorderLeft].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
                        wordtable1.Cell(i + 2, 2).Range.Borders[Word.WdBorderType.wdBorderRight].Color = wdBorderColor;

                        if (evalTable.Rows[i].ItemArray[2].ToString() == "0")
                        {
                            wordtable1.Cell(i + 2, 1).Merge(wordtable1.Cell(i + 2, 2));
                            wordtable1.Cell(i + 2, 1).Range.Font.Name = fontName;
                            wordtable1.Cell(i + 2, 1).Range.Font.Size = 8;
                            wordtable1.Cell(i + 2, 1).Range.Font.Bold = 1;

                            wordtable1.Cell(i + 2, 1).Range.Shading.BackgroundPatternColor = wdHeaderSubColor;
                            //wordtable1.Cell(i + 2, 2).Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorLightYellow;
                        }
                    }
                }

                //Add second table
                if (adgvInspectors.CurrentRow.Cells["REPORT_NUMBER"].Value != DBNull.Value)
                {
                    wordDocument.Paragraphs.Add();
                    wordParagraph = wordDocument.Paragraphs[wordDocument.Paragraphs.Count];
                    wordParagraph.Range.Text = "3. Inspector’s observations";
                    wordParagraph.Range.Font.Size = 11;
                    wordParagraph.Range.Font.Name = fontName;
                    wordParagraph.Range.Font.Bold = 1;
                    wordParagraph.Range.Font.Color = wdFontColor;

                    wordDocument.Paragraphs.Add();
                    wordParagraph = wordDocument.Paragraphs[wordDocument.Paragraphs.Count];
                    wordParagraph.Range.Font.Size = 10;
                    wordParagraph.Range.Font.Color = wdTextColor;

                    if (obsRecCount == 0)
                    {
                        wordParagraph.Range.Text = "There is no observation yet.";
                    }
                    else
                    {
                        Word.Range table2range = wordParagraph.Range;

                        cmd.CommandText =
                            "select REPORT_ITEMS.QUESTION_NUMBER, REPORT_ITEMS.OBSERVATION, REPORT_ITEMS.OPERATOR_COMMENTS \n" +
                            "from REPORT_ITEMS inner join \n" +
                            "(select DISTINCT REPORT_ITEMS.QUESTION_NUMBER, REPORT_ITEMS.REPORT_CODE, SEQUENCE \n" +
                            "from (REPORT_ITEMS inner join REPORTS \n" +
                            "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE) \n" +
                            "left join TEMPLATE_QUESTIONS \n" +
                            "on REPORT_ITEMS.QUESTION_GUID=TEMPLATE_QUESTIONS.QUESTION_GUID \n" +
                            "where REPORTS.INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + "\n" +
                            "and LEN(OBSERVATION)>0) as Q1 \n" +
                            "on REPORT_ITEMS.REPORT_CODE=Q1.REPORT_CODE \n" +
                            "and REPORT_ITEMS.QUESTION_NUMBER=Q1.QUESTION_NUMBER \n" +
                            "order by Q1.SEQUENCE";

                        OleDbDataAdapter inspectorObs = new OleDbDataAdapter(cmd);

                        if (DS.Tables.Contains("OBS_BY_INSPECTOR"))
                            DS.Tables["OBS_BY_INSPECTOR"].Clear();

                        inspectorObs.Fill(DS, "OBS_BY_INSPECTOR");

                        DataTable obsTable = DS.Tables["OBS_BY_INSPECTOR"];


                        int table2Rows = obsTable.Rows.Count + 1;
                        int table2Columns = 2;

                        Word.Table wordtable2 = wordDocument.Tables.Add(table2range, table2Rows, table2Columns,
                                          ref defaultTableBehavior, ref autoFitBehavior);

                        float fullWidth = wordtable2.Columns[1].Width + wordtable2.Columns[2].Width;
                        wordtable2.Columns[1].Width = 50;
                        wordtable2.Columns[2].Width = fullWidth - wordtable2.Columns[1].Width;
                        wordtable2.Cell(1, 1).Range.Text = "VIQ ref";
                        wordtable2.Cell(1, 1).Range.Font.Bold = 1;
                        wordtable2.Cell(1, 1).Range.Font.Size = 8;
                        wordtable2.Cell(1, 1).Range.Font.Name = fontName;
                        wordtable2.Cell(1, 1).Range.Shading.BackgroundPatternColor = wdHeaderColor;

                        wordtable2.Cell(1, 2).Range.Text = "Observation";
                        wordtable2.Cell(1, 2).Range.Font.Bold = 1;
                        wordtable2.Cell(1, 2).Range.Font.Bold = 1;
                        wordtable2.Cell(1, 2).Range.Font.Size = 8;
                        wordtable2.Cell(1, 2).Range.Font.Name = fontName;
                        wordtable2.Cell(1, 2).Range.Shading.BackgroundPatternColor = wdHeaderColor;
                        wordtable2.PreferredWidthType = Word.WdPreferredWidthType.wdPreferredWidthAuto;

                        wordtable2.Rows[1].Select();

                        wordapp.Selection.Rows.HeadingFormat = -1;

                        wordtable2.Rows.AllowBreakAcrossPages = 0;

                        //wordtable2.Range.Borders[Word.WdBorderType.wdBorderLeft].Color = wdBorderColor;
                        //wordtable2.Range.Borders[Word.WdBorderType.wdBorderRight].Color = wdBorderColor;
                        //wordtable2.Range.Borders[Word.WdBorderType.wdBorderTop].Color = wdBorderColor;
                        //wordtable2.Range.Borders[Word.WdBorderType.wdBorderBottom].Color = wdBorderColor;
                        //wordtable2.Range.Borders[Word.WdBorderType.wdBorderHorizontal].Color = wdBorderColor;

                        wordtable2.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                        wordtable2.Borders.InsideLineWidth = Word.WdLineWidth.wdLineWidth025pt;
                        wordtable2.Borders.InsideColor = wdBorderColor;

                        wordtable2.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                        wordtable2.Borders.OutsideLineWidth = Word.WdLineWidth.wdLineWidth025pt;
                        wordtable2.Borders.OutsideColor = wdBorderColor;

                        //if (wordtable2.Range.Borders[Word.WdBorderType.wdBorderVertical]!=null)
                        //    wordtable2.Range.Borders[Word.WdBorderType.wdBorderVertical].Color = wdBorderColor;

                        for (int i = 0; i < table2Rows - 1; i++)
                        {
                            wordtable2.Cell(i + 2, 1).Range.Text = obsTable.Rows[i].ItemArray[0].ToString();
                            wordtable2.Cell(i + 2, 1).Range.Font.Size = 8;
                            wordtable2.Cell(i + 2, 1).Range.Font.Name = fontName;
                            wordtable2.Cell(i + 2, 1).Range.Font.Bold = 1;

                            Word.Range obsRange = wordtable2.Cell(i + 2, 2).Range;

                            obsRange.Text = obsTable.Rows[i].ItemArray[1].ToString();
                            obsRange.Font.Size = 8;
                            obsRange.Font.Name = fontName;
                            obsRange.Font.Bold = 0;

                            string comments = obsTable.Rows[i].ItemArray[2].ToString();

                            if (comments.Length > 0)
                            {
                                Word.Paragraph para = obsRange.Paragraphs.Add();
                                Word.Range comRange = para.Range;

                                comRange.Text = "SCF comments: " + comments;
                                comRange.Font.Color = Word.WdColor.wdColorBlue;

                                Word.Range boldRange = comRange;
                                //boldRange.Start = 1;
                                boldRange.End = boldRange.Start + 13;
                                boldRange.Font.Bold = 1;
                            }
                        }
                    }
                }

                /*
                string notes = adgvInspectors.CurrentRow.Cells["Notes"].Value.ToString().Trim();

                if (notes.Length > 0)
                {
                    wordDocument.Paragraphs.Add();
                    wordParagraph = wordDocument.Paragraphs[wordDocument.Paragraphs.Count];
                    wordParagraph.Range.Text = "3. Notes";
                    wordParagraph.Range.Font.Size = 14;

                    wordDocument.Paragraphs.Add();
                    wordParagraph = wordDocument.Paragraphs[wordDocument.Paragraphs.Count];
                    wordParagraph.Range.Font.Size = 10;

                    wordParagraph.Range.Text = notes;
                }
                */

                //Save Word document as PDF
                bool saveSuccessfull = false;
                try
                {
                    wordDocument.SaveAs2(pdfFileName, Word.WdSaveFormat.wdFormatPDF);
                    saveSuccessfull = true;
                }
                catch (Exception E)
                {
                    MessageBox.Show("Error during saving file in PDF format: \n" + E.Message, "External Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Object saveChanges = Word.WdSaveOptions.wdDoNotSaveChanges;
                Object originalFormat = Word.WdOriginalFormat.wdOriginalDocumentFormat;
                Object routeDocument = Type.Missing;

                ((Word._Application)wordapp).Quit(ref saveChanges, ref originalFormat, ref routeDocument);



                //MessageBox.Show("File \"" + pdfFileName + "\" created");
                if (saveSuccessfull)
                    return pdfFileName;
                else
                    return "";
            }
            finally
            {
                wordapp = null;
                this.Cursor = Cursors.Default;
            }


        }
  
        private void btnSettings_Click(object sender, EventArgs e)
        {
            frmMessageSettings form = new frmMessageSettings(this.Icon, this.Font);

            form.msgSubject = _subject;
            form.msgText = _body;
            form.useSizeLimit = _useSizeLimit;
            form.maxSize = _maxSize;
            form.sizeDimension = _dimension;
            form.useReportCount = _useReportCount;
            form.reportCount = _maxReports;
            form.sendMessage = _send;
            form.useTempFolder = _useTempFolder;
            form.userFolder = _userFolder;
            form.sendToFleet = _sendCopyToFleet;
            form.ccAddresses = _copyAddresses;
            form.askForVessel = _askForVessel;
            form.useChart = _useChart;

            var rslt = form.ShowDialog();

            if (rslt==DialogResult.OK)
            {
                _subject = form.msgSubject;
                _body = form.msgText;

                //Check whether TextBox or RichTextBox is used
                if (!_body.Contains("\r"))
                    _body = _body.Replace("\n", "\r\n");

                _useReportCount = form.useSizeLimit;
                _useSizeLimit = form.useSizeLimit;
                _maxSize = form.maxSize;
                _dimension = form.sizeDimension;
                _useReportCount = form.useReportCount;
                _maxReports = form.reportCount;
                _send = form.sendMessage;
                _useTempFolder = form.useTempFolder;
                _userFolder = form.userFolder;
                _sendCopyToFleet = form.sendToFleet;
                _copyAddresses = form.ccAddresses;
                _askForVessel = form.askForVessel;
                _useChart = form.useChart;

                SaveMessageSettings();

                UpdateSendButton();
            }

        }

        private void GetMessageSettings()
        {
            string fileName = Path.Combine(MainForm.serviceFolder, "Settings_Common.ini");

            IniFile iniFile = new IniFile(fileName);

            string section = "Inspector summary message settings";

            _subject = iniFile.ReadString(section, "Subject", "");
            _body = MainForm.IniStrToStr(iniFile.ReadString(section, "Body", ""));
            _useSizeLimit = iniFile.ReadBoolean(section, "UseSizeLimit", false);
            _maxSize = iniFile.ReadInteger(section, "MaxSize", 0);
            _dimension = iniFile.ReadString(section, "Dimension", "KB");
            _useReportCount = iniFile.ReadBoolean(section, "UseReportCount", false);
            _maxReports = iniFile.ReadInteger(section, "ReportCount", 0);
            _send = iniFile.ReadBoolean(section, "CreateAndSend", false);
            _sendCopyToFleet = iniFile.ReadBoolean(section, "SendCopyToFleet", false);
            _copyAddresses = iniFile.ReadString(section, "CopyAddresses", "");
            _askForVessel = iniFile.ReadBoolean(section, "AskForVessel", false);
            _useChart = iniFile.ReadBoolean(section, "UseChart", true);

            UpdateSendButton();

            fileName = Path.Combine(MainForm.serviceFolder, "Settings_" + MainForm.userName + ".ini");
            IniFile userIni = new IniFile(fileName);

            _useTempFolder = userIni.ReadBoolean(section, "UseTempFolder", true);
            _userFolder = userIni.ReadString(section, "UserFolder", "");
        }

        private void SaveMessageSettings()
        {
            string fileName = Path.Combine(MainForm.serviceFolder, "Settings_Common.ini");

            IniFile iniFile = new IniFile(fileName);

            string section = "Inspector summary message settings";

            iniFile.Write(section, "Subject", _subject);
            iniFile.Write(section, "Body", MainForm.StrToIniStr(_body));
            iniFile.Write(section, "UseSizeLimit", _useSizeLimit);
            iniFile.Write(section, "MaxSize", _maxSize);
            iniFile.Write(section, "Dimension", _dimension);
            iniFile.Write(section, "UseReportCount", _useReportCount);
            iniFile.Write(section, "ReportCount", _maxReports);
            iniFile.Write(section, "CreateAndSend", _send);
            iniFile.Write(section, "SendCopyToFleet", _sendCopyToFleet);
            iniFile.Write(section, "CopyAddresses", _copyAddresses);
            iniFile.Write(section, "AskForVessel", _askForVessel);
            iniFile.Write(section, "UseChart", _useChart);

            fileName = Path.Combine(MainForm.serviceFolder, "Settings_" + MainForm.userName + ".ini");
            IniFile userIni = new IniFile(fileName);

            userIni.Write(section, "UseTempFolder", _useTempFolder);
            userIni.Write(section, "UserFolder", _userFolder);
        }

        private void UpdateSendButton()
        {
            if (_send)
            {
                btnSendSummary.Text = "Send summary";
                btnSendSummary.Image = VIM.Properties.Resources.mail;
            }
            else
            {
                btnSendSummary.Text = "Save summary";
                btnSendSummary.Image = VIM.Properties.Resources.save;
            }

        }

        private void SendOutlook(string pdfFileName)
        {
            this.Cursor = Cursors.WaitCursor;

            string messageSubject = "";
            long filesSize = 0;

            //long maxSize = Convert.ToInt64(MainForm.GetOptionValue(100));

            long maxSize = -1;
            int reportCounter = 0;

            if (_useSizeLimit)
            {
                switch (_dimension)
                {
                    case "KB":
                        maxSize = _maxSize * 1024;
                        break;
                    case "MB":
                        maxSize = _maxSize * 1024 * 1024;
                        break;
                    case "GB":
                        maxSize = _maxSize * 1024 * 1024 * 1024;
                        break;
                }
            }

            OleDbCommand cmd = new OleDbCommand("", connection);

            messageSubject = _subject.Replace("%1", adgvInspectors.CurrentRow.Cells["INSPECTOR_NAME"].Value.ToString());

            //Send message to Outlook
            Outlook.Application application = new Outlook.Application();

            Outlook.MailItem mail = (Outlook.MailItem)application.CreateItem(Outlook.OlItemType.olMailItem);

            if (vesselNameEmail.Length > 0)
                mail.To = vesselNameEmail;
            
            string cc = "";

            if (_copyAddresses.Length>0)
            {
                cc = _copyAddresses;
            }
            
            if (_sendCopyToFleet)
            {
                if (fleetEmail.Length > 0)
                {
                    if (cc.Length > 0)
                        cc = cc + ";" + fleetEmail;
                    else
                        cc = fleetEmail;
                }
            }

            if (cc.Length > 0)
                mail.CC = cc;

            mail.Subject = messageSubject;
            mail.Body = _body;

            try
            {

                //Check for attachments
                bool hasAttachments = true;

                if (hasAttachments)
                {
                    //Add summary
                    if (pdfFileName.Length > 0)
                    {
                        FileInfo pfi = new FileInfo(pdfFileName);

                        filesSize = filesSize + pfi.Length;

                        mail.Attachments.Add(pdfFileName,
                            Outlook.OlAttachmentType.olByValue, Type.Missing, Type.Missing);
                    }

                    //Add copy of reports if any

                    Guid inspectorGuid = MainForm.StrToGuid(adgvInspectors.CurrentRow.Cells["INSPECTOR_GUID"].Value.ToString());

                    cmd.CommandText =
                        "select REPORT_CODE, INSPECTION_DATE \n" +
                        "from REPORTS \n" +
                        "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + "\n" +
                        "order by INSPECTION_DATE DESC";

                    OleDbDataReader reports = cmd.ExecuteReader();

                    if (reports.HasRows)
                    {
                        while ((reports.Read()) && (!_useSizeLimit || (filesSize < maxSize)))
                        {
                            string reportFile = MainForm.workFolder + "\\Reports\\" + reports[0].ToString() + ".pdf";


                            if (File.Exists(reportFile))
                            {
                                FileInfo fi = new FileInfo(reportFile);

                                filesSize = filesSize + fi.Length;

                                if ((!_useSizeLimit) || ((_useSizeLimit) && (filesSize < maxSize)))
                                {
                                    if ((!_useReportCount) || ((_useReportCount) && (reportCounter < _maxReports)))
                                    {
                                        mail.Attachments.Add(reportFile, Outlook.OlAttachmentType.olByValue, Type.Missing, Type.Missing);
                                    }
                                }

                                reportCounter++;
                            }
                        }
                    }

                    reports.Close();
                }


                try
                {
                    mail.Display(1);
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }

            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void SendMAPI(string pdfFileName)
        {
            this.Cursor = Cursors.WaitCursor;

            string messageSubject = "";
            long filesSize = 0;

            //long maxSize = Convert.ToInt64(MainForm.GetOptionValue(100));

            long maxSize = -1;
            int reportCounter = 0;

            if (_useSizeLimit)
            {
                switch (_dimension)
                {
                    case "KB":
                        maxSize = _maxSize * 1024;
                        break;
                    case "MB":
                        maxSize = _maxSize * 1024 * 1024;
                        break;
                    case "GB":
                        maxSize = _maxSize * 1024 * 1024 * 1024;
                        break;
                }
            }

            OleDbCommand cmd = new OleDbCommand("", connection);

            messageSubject = _subject.Replace("%1", adgvInspectors.CurrentRow.Cells["INSPECTOR_NAME"].Value.ToString());

            //Send message to MAPI
            SimpleMAPI sMapi = new SimpleMAPI();

            sMapi.Logon(this.Handle);

            if (vesselEmail.Length > 0)
                sMapi.AddRecipient(vesselNameEmail, null, false);

            if (_copyAddresses.Length > 0)
            {
                string cc = _copyAddresses;

                if (_sendCopyToFleet)
                {
                    if (fleetEmail.Length > 0)
                    {
                        if (cc.Length > 0)
                            cc = cc + ";" + fleetEmail;
                        else
                            cc = fleetEmail;
                    }
                }

                if (cc.Length > 0)
                    sMapi.AddRecipient(cc, null, true);
            }


            try
            {

                //Check for attachments
                bool hasAttachments = true;

                if (hasAttachments)
                {
                    //Add summary
                    if (pdfFileName.Length > 0)
                    {
                        FileInfo pfi = new FileInfo(pdfFileName);

                        filesSize = filesSize + pfi.Length;

                        sMapi.Attach(pdfFileName);
                    }

                    //Add copy of reports if any

                    Guid inspectorGuid = MainForm.StrToGuid(adgvInspectors.CurrentRow.Cells["INSPECTOR_GUID"].Value.ToString());

                    cmd.CommandText =
                        "select REPORT_CODE, INSPECTION_DATE \n" +
                        "from REPORTS \n" +
                        "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + "\n" +
                        "order by INSPECTION_DATE DESC";

                    OleDbDataReader reports = cmd.ExecuteReader();

                    if (reports.HasRows)
                    {
                        while ((reports.Read()) && (!_useSizeLimit || (filesSize < maxSize)))
                        {
                            string reportFile = MainForm.workFolder + "\\Reports\\" + reports[0].ToString() + ".pdf";


                            if (File.Exists(reportFile))
                            {
                                FileInfo fi = new FileInfo(reportFile);

                                filesSize = filesSize + fi.Length;

                                if ((!_useSizeLimit) || ((_useSizeLimit) && (filesSize < maxSize)))
                                {
                                    if ((!_useReportCount) || ((_useReportCount) && (reportCounter < _maxReports)))
                                    {
                                        sMapi.Attach(reportFile);
                                    }
                                }

                                reportCounter++;
                            }
                        }
                    }

                    reports.Close();
                }

                sMapi.Send(messageSubject, _body, true);

                sMapi.Logoff();

            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void SendApplication(string pdfFileName)
        {
            this.Cursor = Cursors.WaitCursor;

            string messageSubject = "";
            long filesSize = 0;

            //long maxSize = Convert.ToInt64(MainForm.GetOptionValue(100));

            long maxSize = -1;
            int reportCounter = 0;

            if (_useSizeLimit)
            {
                switch (_dimension)
                {
                    case "KB":
                        maxSize = _maxSize * 1024;
                        break;
                    case "MB":
                        maxSize = _maxSize * 1024 * 1024;
                        break;
                    case "GB":
                        maxSize = _maxSize * 1024 * 1024 * 1024;
                        break;
                }
            }

            OleDbCommand cmd = new OleDbCommand("", connection);

            messageSubject = _subject.Replace("%1", adgvInspectors.CurrentRow.Cells["INSPECTOR_NAME"].Value.ToString());

            try
            {
                FrmMail form = new FrmMail();

                //Check for attachments
                bool hasAttachments = true;

                form.msgToAddress = vesselNameEmail;
                
                string cc="";

                if (_copyAddresses.Length>0)
                {
                    cc=_copyAddresses;
                }
                
                if (_sendCopyToFleet)
                {
                    if (fleetEmail.Length>0)
                    {
                        if (cc.Length>0)
                            cc=cc+";"+fleetEmail;
                        else
                            cc=fleetEmail;
                    }
                }

                if (cc.Length > 0)
                    form.msgCcAddress = cc;

                if (hasAttachments)
                {
                    //Add summary
                    if (pdfFileName.Length > 0)
                    {
                        FileInfo pfi = new FileInfo(pdfFileName);

                        filesSize = filesSize + pfi.Length;

                        form.AddAttachment(pdfFileName);
                    }

                    //Add copy of reports if any

                    Guid inspectorGuid = MainForm.StrToGuid(adgvInspectors.CurrentRow.Cells["INSPECTOR_GUID"].Value.ToString());

                    cmd.CommandText =
                        "select REPORT_CODE, INSPECTION_DATE \n" +
                        "from REPORTS \n" +
                        "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + "\n" +
                        "order by INSPECTION_DATE DESC";

                    OleDbDataReader reports = cmd.ExecuteReader();

                    if (reports.HasRows)
                    {
                        while ((reports.Read()) && (!_useSizeLimit || (filesSize < maxSize)))
                        {
                            string reportFile = MainForm.workFolder + "\\Reports\\" + reports[0].ToString() + ".pdf";


                            if (File.Exists(reportFile))
                            {
                                FileInfo fi = new FileInfo(reportFile);

                                filesSize = filesSize + fi.Length;

                                if ((!_useSizeLimit) || ((_useSizeLimit) && (filesSize < maxSize)))
                                {
                                    if ((!_useReportCount) || ((_useReportCount) && (reportCounter < _maxReports)))
                                    {
                                        form.AddAttachment(reportFile);
                                    }
                                }

                                reportCounter++;
                            }
                        }
                    }

                    reports.Close();
                }

                form.msgSubject = messageSubject;
                form.msgText = _body;

                form.ShowDialog();

            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void InspectorsForm_Load(object sender, EventArgs e)
        {
            //Restore form settings
            // Upgrade?
            if (Properties.Settings.Default.InspectorsFormSize.Width == 0)
                Properties.Settings.Default.Upgrade();

            if (Properties.Settings.Default.InspectorsFormSize.Width == 0 || Properties.Settings.Default.InspectorsFormSize.Height == 0)
            {
                // first start
                // optional: add default values
            }
            else
            {
                this.WindowState = Properties.Settings.Default.InspectorsFormState;

                // we don't want a minimized window at startup
                if (this.WindowState == FormWindowState.Minimized) this.WindowState = FormWindowState.Normal;

                this.Location = Properties.Settings.Default.InspectorsFormLocation;
                this.Size = Properties.Settings.Default.InspectorsFormSize;
            }
        }

        private void InspectorsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Store form settings
            Properties.Settings.Default.InspectorsFormState = this.WindowState;

            if (this.WindowState == FormWindowState.Normal)
            {
                // save location and size if the state is normal
                Properties.Settings.Default.InspectorsFormLocation = this.Location;
                Properties.Settings.Default.InspectorsFormSize = this.Size;
            }
            else
            {
                // save the RestoreBounds if the form is minimized or maximized!
                Properties.Settings.Default.InspectorsFormLocation = this.RestoreBounds.Location;
                Properties.Settings.Default.InspectorsFormSize = this.RestoreBounds.Size;
            }

            // don't forget to save the settings
            Properties.Settings.Default.Save();
        }

        private void adgvInspectors_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (Convert.ToBoolean(adgvInspectors.Rows[e.RowIndex].Cells["Unfavourable"].Value))
            {
                adgvInspectors.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 199, 206);
            }
        }

        private void adgvInspectors_FilterStringChanged(object sender, EventArgs e)
        {
            bsInspectors.Filter = adgvInspectors.FilterString;
            CountRecords();
        }

        private void adgvInspectors_SortStringChanged(object sender, EventArgs e)
        {
            bsInspectors.Sort = adgvInspectors.SortString;
        }

        private void CountRecords()
        {
            lblRecNo.Text = adgvInspectors.Rows.Count.ToString();
        }
    }
}
