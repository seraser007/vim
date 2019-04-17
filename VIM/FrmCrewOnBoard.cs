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
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace VIM
{

    public partial class FrmCrewOnBoard : Form
    {
        DataSet DS;
        OleDbConnection connection;
        OleDbDataAdapter crewAdapter;
        OleDbCommand cmd;
        BindingSource bsCrew = new BindingSource();
        private bool blockButtons = false;

        private string _filterString = "";
        private string _sortString = "";
        private string _queryFilter = "";
        private string _querySort = "";
        private string _formCaption = "";

        public string filterString
        {
            set { _filterString = value; }
        }

        public string sortString
        {
            set { _sortString = value; }
        }

        public string queryFilter
        {
            set { _queryFilter = value; }
        }

        public string querySort
        {
            set { _querySort = value; }
        }

        public string formCaption
        {
            set { _formCaption = value; }
        }

        public FrmCrewOnBoard()
        {
            this.Icon = MainForm.mainIcon;
            this.Font = MainForm.mainFont;
            DS = MainForm.DS;
            connection = MainForm.connection;
            cmd = new OleDbCommand("", connection);

            InitializeComponent();

        }

        private void FillCrew()
        {
            string cmdText =
                "select CREW_ON_BOARD.*, " +
                "iif(IsNull(CREW_POSITIONS.POSITION_INDEX),0,CREW_POSITIONS.POSITION_INDEX) as POSITION_INDEX \n" +
                "from CREW_ON_BOARD left join CREW_POSITIONS \n" +
                "on CREW_ON_BOARD.CREW_POSITION=CREW_POSITIONS.POSITION_NAME \n";

            if (_queryFilter.Length > 0)
            {
                if (_queryFilter.StartsWith("where"))
                    cmdText = cmdText + _queryFilter + "\n";
                else
                    cmdText = cmdText + "where " + _queryFilter + "\n";
            }

            if (_querySort.Length == 0)
                cmdText = cmdText + "order by CREW_NAME";
            else
            {
                if (_querySort.StartsWith("order by"))
                    cmdText = cmdText + _querySort;
                else
                    cmdText = cmdText + "order by " + _querySort;
            }

            crewAdapter = new OleDbDataAdapter(cmdText, connection);

            if (DS.Tables.Contains("CREW_ON_BOARD"))
                DS.Tables["CREW_ON_BOARD"].Clear();

            this.Cursor = Cursors.WaitCursor;

            try
            {
                crewAdapter.Fill(DS, "CREW_ON_BOARD");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            bsCrew.DataSource = DS;
            bsCrew.DataMember = "CREW_ON_BOARD";

            adgvCrew.DataSource = bsCrew;

            adgvCrew.Columns["ID"].Visible = false;

            adgvCrew.Columns["CREW_NAME"].HeaderText = "Name";
            adgvCrew.Columns["CREW_NAME"].FillWeight = 40;

            adgvCrew.Columns["VESSEL_NAME"].HeaderText = "Vessel";
            adgvCrew.Columns["VESSEL_NAME"].FillWeight = 20;

            adgvCrew.Columns["CREW_POSITION"].HeaderText = "Position";
            adgvCrew.Columns["CREW_POSITION"].FillWeight = 20;

            adgvCrew.Columns["DATE_ON"].FillWeight = 15;
            adgvCrew.Columns["DATE_ON"].HeaderText = "Sign on";

            adgvCrew.Columns["DATE_OFF"].HeaderText = "Sign off";
            adgvCrew.Columns["DATE_OFF"].FillWeight = 13;

            adgvCrew.Columns["VESSEL_GUID"].Visible = false;

            if (adgvCrew.Columns.Contains("VESSEL_GUID"))
                adgvCrew.Columns["VESSEL_GUID"].Visible = false;

            if (adgvCrew.Columns.Contains("VESSEL_ID"))
                adgvCrew.Columns["VESSEL_ID"].Visible = false;

            if (adgvCrew.Columns.Contains("DB_TYPE"))
                adgvCrew.Columns["DB_TYPE"].Visible = false;

            adgvCrew.Columns["POSITION_INDEX"].Visible = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditCrewmember();
        }

        private void EditCrewmember()
        {
            if (adgvCrew.Rows.Count == 0)
                return;

            if (blockButtons)
                return;

            FrmCrewOnBoardEditor form = new FrmCrewOnBoardEditor();

            form.crewName = adgvCrew.CurrentRow.Cells["CREW_NAME"].Value.ToString();

            if (adgvCrew.CurrentRow.Cells["VESSEL_GUID"].Value != null && adgvCrew.CurrentRow.Cells["VESSEL_GUID"].Value != System.DBNull.Value)
                form.vesselGuid = MainForm.StrToGuid(adgvCrew.CurrentRow.Cells["VESSEL_GUID"].Value.ToString());
            else
                form.vesselGuid = MainForm.zeroGuid;

            form.crewVessel = adgvCrew.CurrentRow.Cells["VESSEL_NAME"].Value.ToString();
            form.crewPosition = adgvCrew.CurrentRow.Cells["CREW_POSITION"].Value.ToString();

            if (adgvCrew.CurrentRow.Cells["DATE_ON"].Value == null)
                form.crewSignOn = DateTimePicker.MinimumDateTime;
            else
                form.crewSignOn = Convert.ToDateTime(adgvCrew.CurrentRow.Cells["DATE_ON"].Value);

            if (adgvCrew.CurrentRow.Cells["DATE_OFF"].Value == null)
                form.crewSignOff = DateTimePicker.MinimumDateTime;
            else
                form.crewSignOff = Convert.ToDateTime(adgvCrew.CurrentRow.Cells["DATE_OFF"].Value);

            if (form.ShowDialog() == DialogResult.OK)
            {
                string OnDateStr = "";
                string OffDateStr = "";

                if (form.crewSignOn == DateTimePicker.MinimumDateTime)
                    OnDateStr = "Null";
                else
                    OnDateStr = MainForm.DateTimeToQueryStr(form.crewSignOn);

                if (form.crewSignOff == DateTimePicker.MinimumDateTime)
                    OffDateStr = "Null";
                else
                    OffDateStr = MainForm.DateTimeToQueryStr(form.crewSignOff);

                cmd.CommandText =
                    "update CREW_ON_BOARD set \n" +
                    "CREW_NAME='" + MainForm.StrToSQLStr(form.crewName) + "',\n" +
                    "VESSEL_NAME='" + MainForm.StrToSQLStr(form.crewVessel) + "',\n" +
                    "CREW_POSITION='" + MainForm.StrToSQLStr(form.crewPosition) + "',\n" +
                    "DATE_ON=" + OnDateStr + ",\n" +
                    "DATE_OFF=" + OffDateStr + ",\n" +
                    "VESSEL_GUID=" + MainForm.GuidToStr(form.vesselGuid) + "\n" +
                    "where \n" +
                    "ID=" + adgvCrew.CurrentRow.Cells["ID"].Value.ToString();

                MainForm.cmdExecute(cmd);

                this.Cursor = Cursors.WaitCursor;

                int curRow = adgvCrew.CurrentRow.Index;
                int curCol = adgvCrew.CurrentCell.ColumnIndex;

                try
                {
                    DS.Tables["CREW_ON_BOARD"].Clear();

                    crewAdapter.Fill(DS, "CREW_ON_BOARD");

                    adgvCrew.CurrentCell = adgvCrew[curCol, curRow];
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (blockButtons)
                return;

            FrmCrewOnBoardEditor form = new FrmCrewOnBoardEditor();

            form.crewSignOn = DateTimePicker.MinimumDateTime;
            form.crewSignOff = DateTimePicker.MinimumDateTime;

            var rslt = form.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                string OnDateStr = "";
                string OffDateStr = "";

                if (form.crewSignOn == DateTimePicker.MinimumDateTime)
                    OnDateStr = "Null";
                else
                    OnDateStr = MainForm.DateTimeToQueryStr(form.crewSignOn);

                if (form.crewSignOff == DateTimePicker.MinimumDateTime)
                    OffDateStr = "Null";
                else
                    OffDateStr = MainForm.DateTimeToQueryStr(form.crewSignOff);

                cmd.CommandText =
                    "insert into CREW_ON_BOARD (CREW_NAME,VESSEL_NAME,CREW_POSITION,DATE_ON,DATE_OFF) \n" +
                    "values('" + MainForm.StrToSQLStr(form.crewName) + "','" +
                        MainForm.StrToSQLStr(form.crewVessel) + "','" +
                        MainForm.StrToSQLStr(form.crewPosition) + "'," +
                        OnDateStr + "," + OffDateStr + ")";

                MainForm.cmdExecute(cmd);

                this.Cursor = Cursors.WaitCursor;

                try
                {
                    crewAdapter.Fill(DS, "CREW_ON_BOARD");
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            if (blockButtons)
                return;

            FrmCrewLoadSettings form = new FrmCrewLoadSettings();

            form.ShowDialog();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (blockButtons)
                return;

            bool askForFile = Convert.ToBoolean(MainForm.ReadIniValue(MainForm.iniPersonalFile, "LoadCrewSettings", "AskForFile", "True"));
            string fileName = MainForm.ReadIniValue(MainForm.iniPersonalFile, "LoadCrewSettings", "FileName");
            bool relativePath = Convert.ToBoolean(MainForm.ReadIniValue(MainForm.iniPersonalFile, "LoadCrewSettings", "RelativePath", "False"));


            if (askForFile)
            {
                openFileDialog1.InitialDirectory = Path.Combine(MainForm.workFolder, "data\\crew");
                openFileDialog1.FileName = "";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    LoadCrewFromFile(openFileDialog1.FileName);
                }
            }
            else
            {
                if (relativePath)
                {
                    if (fileName.StartsWith("\\"))
                    {
                        fileName = Path.Combine(MainForm.workFolder, fileName.Substring(1));
                    }
                    else
                    {
                        fileName = Path.Combine(MainForm.workFolder, fileName);
                    }
                }

                if (File.Exists(fileName))
                    LoadCrewFromFile(fileName);
                else
                    MessageBox.Show("File \"" + fileName + "\" does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void LoadCrewFromFile(string fileName)
        {
            if (!File.Exists(fileName)) return;

            LoadFromExcel(fileName);
        }


        private void LoadFromExcel(string fileName)
        {
            Size minSize = this.MinimumSize;
            Size maxSize = this.MaximumSize;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                bool importTerminated = false;

                blockButtons = true;

                this.MaximumSize = this.Size;
                this.MinimumSize = this.Size;

                this.MaximizeBox = false;
                this.MinimizeBox = false;

                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

                excelApp.Visible = false;

                Workbook workbook = excelApp.Workbooks.Open(fileName);

                //select the first sheet        
                Worksheet worksheet = (Worksheet)workbook.Worksheets[1];

                //find the used range in worksheet
                Range excelRange = worksheet.UsedRange;

                object[,] valueArray = excelRange.Value2;

                int rows = excelRange.Rows.Count;
                int cols = excelRange.Columns.Count;

                //clean up stuffs
                workbook.Close(false);
                Marshal.ReleaseComObject(workbook);

                excelApp.Quit();
                Marshal.FinalReleaseComObject(excelApp);

                OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

                string tempTable = "TEMP_" + MainForm.userName.ToUpper();

                //cmd.CommandText = "drop table " + tempTable;

                //MainForm.cmdExecute(cmd);

                MainForm.tempTableCreate(tempTable, "create table " + tempTable + " (TEMP_ID counter Primary key, CREW_NAME varchar(255), VESSEL_NAME varchar(255), CREW_POSITION varchar(255), DATE_ON DateTime, DATE_OFF DateTime)");

                int nameCol = 1;
                int vesselCol = 2;
                int positionCol = 3;
                int onDateCol = 4;
                int offDateCol = 5;

                FormProgress progress = new FormProgress(rows - 1);

                progress.Show();

                System.Windows.Forms.Application.DoEvents();

                int lineCounter = 0;
                string cmdText = "";

                for (int row = 1; row <= rows; row++)
                {
                    if (row == 1)
                    {
                        for (int col = 1; col <= cols; col++)
                        {
                            string fieldName = "";

                            if (valueArray[row, col] != null)
                                fieldName = valueArray[row, col].ToString();

                            switch (fieldName)
                            {
                                case "Name":
                                    nameCol = col;
                                    break;
                                case "Vessel":
                                    vesselCol = col;
                                    break;
                                case "Position":
                                    positionCol = col;
                                    break;
                                case "SIGN ON":
                                    onDateCol = col;
                                    break;
                                case "SIGN OFF":
                                    offDateCol = col;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        lineCounter++;

                        if (lineCounter < 10)
                        {
                            if (cmdText.Length == 0)
                            {
                                cmdText =
                                    "select TOP 1 \n" +
                                    "'" + MainForm.StrToSQLStr(valueArray[row, nameCol].ToString()) + "' as CREW_NAME,'" +
                                    MainForm.StrToSQLStr(valueArray[row, vesselCol].ToString()) + "' as VESSEL_NAME,'" +
                                    MainForm.StrToSQLStr(valueArray[row, positionCol].ToString()) + "' as CREW_POSITION," +
                                    MainForm.DateTimeToQueryStr(DateTime.FromOADate(Convert.ToDouble(valueArray[row, onDateCol]))) + " as DATE_ON," +
                                    MainForm.DateTimeToQueryStr(DateTime.FromOADate(Convert.ToDouble(valueArray[row, offDateCol]))) + " as DATE_OFF \n" +
                                    "from OPTIONS";
                            }
                            else
                            {
                                cmdText = cmdText + "\n union \n" +
                                    "select TOP 1 \n" +
                                    "'" + MainForm.StrToSQLStr(valueArray[row, nameCol].ToString()) + "','" +
                                    MainForm.StrToSQLStr(valueArray[row, vesselCol].ToString()) + "','" +
                                    MainForm.StrToSQLStr(valueArray[row, positionCol].ToString()) + "'," +
                                    MainForm.DateTimeToQueryStr(DateTime.FromOADate(Convert.ToDouble(valueArray[row, onDateCol]))) + "," +
                                    MainForm.DateTimeToQueryStr(DateTime.FromOADate(Convert.ToDouble(valueArray[row, offDateCol]))) + " \n" +
                                    "from OPTIONS";
                            }
                        }
                        else
                        {
                            cmd.CommandText =
                            "insert into " + tempTable + " (CREW_NAME, VESSEL_NAME, CREW_POSITION, DATE_ON, DATE_OFF) \n" +
                            "select * from (\n" +
                            cmdText + "\n)";

                            MainForm.cmdExecute(cmd);

                            progress.position = row - 1;

                            System.Windows.Forms.Application.DoEvents();

                            lineCounter = 0;
                            cmdText = "";

                            if (progress.terminate)
                            {
                                importTerminated = true;
                                progress.Close();
                                blockButtons = false;
                                break;
                            }

                        }

                        //cmd.CommandText =
                        //    "insert into " + tempTable + " (CREW_NAME, VESSEL_NAME, CREW_POSITION, DATE_ON, DATE_OFF) \n" +
                        //    "values('" + MainForm.StrToSQLStr(valueArray[row, nameCol].ToString()) + "','" +
                        //    MainForm.StrToSQLStr(valueArray[row, vesselCol].ToString()) + "','" +
                        //    MainForm.StrToSQLStr(valueArray[row, positionCol].ToString()) + "'," +
                        //    MainForm.DateTimeToQueryStr(DateTime.FromOADate(Convert.ToDouble(valueArray[row, onDateCol]))) + "," +
                        //    MainForm.DateTimeToQueryStr(DateTime.FromOADate(Convert.ToDouble(valueArray[row, offDateCol]))) + ")";

                    }
                }

                if (lineCounter > 0)
                {
                    cmd.CommandText =
                            "insert into " + tempTable + " (CREW_NAME, VESSEL_NAME, CREW_POSITION, DATE_ON, DATE_OFF) \n" +
                            "select * from (\n" +
                            cmdText + "\n)";

                    MainForm.cmdExecute(cmd);

                    progress.position = rows - 1;

                    System.Windows.Forms.Application.DoEvents();

                    lineCounter = 0;

                    if (progress.terminate)
                    {
                        importTerminated = true;
                        progress.Close();
                        blockButtons = false;
                    }
                }

                if (!importTerminated)
                {
                    progress.Close();

                    cmd.CommandText =
                        "delete from CREW_ON_BOARD \n" +
                        "where \n" +
                        "DATE_OFF is Null";

                    MainForm.cmdExecute(cmd);

                    cmd.CommandText =
                        "insert into CREW_ON_BOARD(CREW_NAME,VESSEL_NAME,CREW_POSITION,DATE_ON,DATE_OFF) \n" +
                        "select Q.CREW_NAME, Q.VESSEL_NAME, Q.CREW_POSITION, Q.DATE_ON, Q.DATE_OFF \n" +
                        "from " + tempTable + " as Q left join CREW_ON_BOARD as C \n" +
                        "on Q.CREW_NAME=C.CREW_NAME \n" +
                        "and Q.VESSEL_NAME=C.VESSEL_NAME \n" +
                        "and Q.CREW_POSITION=C.CREW_POSITION \n" +
                        "and Q.DATE_ON=C.DATE_ON \n" +
                        "and Q.DATE_OFF=C.DATE_OFF \n" +
                        "where \n" +
                        "C.CREW_NAME is null \n" +
                        "and Q.DATE_ON>DateSerial(2005,1,1)";

                    MainForm.cmdExecute(cmd);

                    cmd.CommandText =
                        "update CREW_ON_BOARD set \n" +
                        "DATE_OFF=Null \n" +
                        "where DATE_OFF<DateSerial(2000,1,1)";

                    MainForm.cmdExecute(cmd);

                    MainForm.UpdateVesselGuid4CrewOnBoard();

                    DS.Tables["CREW_ON_BOARD"].Clear();

                    crewAdapter.Fill(DS, "CREW_ON_BOARD");
                }

                MainForm.tempTableDrop(tempTable);

            }
            finally
            {
                this.MinimizeBox = true;
                this.MaximizeBox = true;

                this.MaximumSize = maxSize;
                this.MinimumSize = minSize;

                blockButtons = false;

                this.Cursor = Cursors.Default;
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void FrmCrewOnBoard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (blockButtons)
            {
                e.Cancel = true;
                return;
            }

            //Store form settings
            Properties.Settings.Default.FrmCrewOnBoardState = this.WindowState;

            if (this.WindowState == FormWindowState.Normal)
            {
                // save location and size if the state is normal
                Properties.Settings.Default.FrmCrewOnBoardLocation = this.Location;
                Properties.Settings.Default.FrmCrewOnBoardSize = this.Size;
            }
            else
            {
                // save the RestoreBounds if the form is minimized or maximized!
                Properties.Settings.Default.FrmCrewOnBoardLocation = this.RestoreBounds.Location;
                Properties.Settings.Default.FrmCrewOnBoardSize = this.RestoreBounds.Size;
            }

            // don't forget to save the settings
            Properties.Settings.Default.Save();


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (blockButtons) return;

            if (adgvCrew.SelectedRows.Count > 0)
            {
                int rowCount = adgvCrew.SelectedRows.Count;

                if (rowCount == 1)
                {
                    string onDate = Convert.ToDateTime(adgvCrew.SelectedRows[0].Cells["DATE_ON"].Value).ToShortDateString();

                    string offDate = "On board";

                    if (adgvCrew.SelectedRows[0].Cells["DATE_OFF"].Value != System.DBNull.Value)
                    {
                        offDate = Convert.ToDateTime(adgvCrew.SelectedRows[0].Cells["DATE_OFF"].Value).ToShortDateString();
                    }

                    var rslt = MessageBox.Show("You are going to delete the following record: \n\n" +
                        "Name: " + adgvCrew.SelectedRows[0].Cells["CREW_NAME"].Value.ToString() + "\n" +
                        "Vessel: " + adgvCrew.SelectedRows[0].Cells["VESSEL_NAME"].Value.ToString() + "\n" +
                        "Position: " + adgvCrew.SelectedRows[0].Cells["CREW_POSITION"].Value.ToString() + "\n" +
                        "Signed on: " + onDate + "\n" +
                        "Signed off: " + offDate + "\n\n" +
                        "Would you like to proceed?",
                        "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (rslt == DialogResult.Yes)
                    {
                        this.Cursor = Cursors.WaitCursor;

                        int curRow = adgvCrew.SelectedRows[0].Index;

                        try
                        {
                            DS.Tables["CREW_ON_BOARD"].Clear();

                            crewAdapter.Fill(DS, "CREW_ON_BOARD");

                            if (adgvCrew.Rows.Count > curRow)
                                adgvCrew.CurrentCell = adgvCrew[1, curRow];
                            else
                            {
                                if (adgvCrew.Rows.Count > 0)
                                {
                                    adgvCrew.CurrentCell = adgvCrew[1, adgvCrew.Rows.Count - 1];
                                }
                            }
                        }
                        finally
                        {
                            this.Cursor = Cursors.Default;
                        }
                    }
                }
                else
                {
                    if (rowCount < 10)
                    {
                        string s = "You are going to delete following records:";

                        string crew = "";

                        for (int i = 0; i < rowCount; i++)
                        {
                            string onDate = Convert.ToDateTime(adgvCrew.SelectedRows[i].Cells["DATE_ON"].Value).ToShortDateString();

                            string offDate = "On board";

                            if (adgvCrew.SelectedRows[i].Cells["DATE_OFF"].Value != System.DBNull.Value)
                            {
                                offDate = Convert.ToDateTime(adgvCrew.SelectedRows[i].Cells["DATE_OFF"].Value).ToShortDateString();
                            }

                            if (crew.Length == 0)
                                crew = adgvCrew.SelectedRows[i].Cells["CREW_NAME"].Value.ToString() + " \"" +
                                    adgvCrew.SelectedRows[i].Cells["VESSEL_NAME"].Value.ToString() + "\" " +
                                    adgvCrew.SelectedRows[i].Cells["CREW_POSITION"].Value.ToString() + " " +
                                    onDate + "-" + offDate;
                            else
                                crew = crew + "\n\n" +
                                    adgvCrew.SelectedRows[i].Cells["CREW_NAME"].Value.ToString() + " \"" +
                                    adgvCrew.SelectedRows[i].Cells["VESSEL_NAME"].Value.ToString() + "\" " +
                                    adgvCrew.SelectedRows[i].Cells["CREW_POSITION"].Value.ToString() + " " +
                                    onDate + "-" + offDate;
                        }

                        s = s + "\n\n" + crew + "\n\n" + "Would you like to proceed?";

                        var rslt = MessageBox.Show(s, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (rslt == DialogResult.Yes)
                        {
                            DeleteSelectedRows();
                        }
                    }
                    else
                    {
                        var rslt = MessageBox.Show("You are going to delete " + rowCount.ToString() + " records.\n" +
                            "Would you like to proceed?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (rslt == DialogResult.Yes)
                        {
                            DeleteSelectedRows();
                        }
                    }
                }
            }
            else
            {
                string onDate = Convert.ToDateTime(adgvCrew.CurrentRow.Cells["DATE_ON"].Value).ToShortDateString();

                string offDate = "On board";

                if (adgvCrew.CurrentRow.Cells["DATE_OFF"].Value != System.DBNull.Value)
                {
                    offDate = Convert.ToDateTime(adgvCrew.CurrentRow.Cells["DATE_OFF"].Value).ToShortDateString();
                }

                var rslt = MessageBox.Show("You are going to delete the following record: \n\n" +
                        "Name: " + adgvCrew.CurrentRow.Cells["CREW_NAME"].Value.ToString() + "\n" +
                        "Vessel: " + adgvCrew.CurrentRow.Cells["VESSEL_NAME"].Value.ToString() + "\n" +
                        "Position: " + adgvCrew.CurrentRow.Cells["CREW_POSITION"].Value.ToString() + "\n" +
                        "Signed on: " + onDate + "\n" +
                        "Signed off: " + offDate + "\n\n" +
                        "Would you like to proceed?",
                        "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (rslt == DialogResult.Yes)
                {
                    OleDbCommand cmd = new OleDbCommand("", connection);

                    cmd.CommandText =
                        "delete from CREW_ON_BOARD \n" +
                        "where ID=" + adgvCrew.CurrentRow.Cells["ID"].Value.ToString();

                    MainForm.cmdExecute(cmd);

                    this.Cursor = Cursors.WaitCursor;

                    int curRow = adgvCrew.CurrentRow.Index;

                    try
                    {
                        DS.Tables["CREW_ON_BOARD"].Clear();

                        crewAdapter.Fill(DS, "CREW_ON_BOARD");

                        if (adgvCrew.Rows.Count > curRow)
                            adgvCrew.CurrentCell = adgvCrew[1, curRow];
                        else
                        {
                            if (adgvCrew.Rows.Count > 0)
                            {
                                adgvCrew.CurrentCell = adgvCrew[1, adgvCrew.Rows.Count - 1];
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

        private void DeleteSelectedRows()
        {
            if (adgvCrew.SelectedRows.Count == 0)
                return;

            OleDbCommand cmd = new OleDbCommand("", connection);

            if (adgvCrew.SelectedRows.Count == adgvCrew.Rows.Count)
            {
                cmd.CommandText =
                    "delete from CREW_ON_BOARD";

                MainForm.cmdExecute(cmd);
            }
            else
            {
                for (int i = 0; i < adgvCrew.SelectedRows.Count; i++)
                {
                    cmd.CommandText =
                        "delete from CREW_ON_BOARD \n" +
                        "where \n" +
                        "ID=" + adgvCrew.SelectedRows[i].Cells["ID"].Value.ToString();

                    MainForm.cmdExecute(cmd);
                }
            }

            this.Cursor = Cursors.WaitCursor;

            try
            {
                DS.Tables["CREW_ON_BOARD"].Clear();

                crewAdapter.Fill(DS, "CREW_ON_BOARD");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void FrmCrewOnBoard_ResizeBegin(object sender, EventArgs e)
        {
        }

        private void adgvCrew_SortStringChanged(object sender, EventArgs e)
        {
            string sortString = adgvCrew.SortString;

            bsCrew.Sort = sortString;

        }

        private void adgvCrew_FilterStringChanged(object sender, EventArgs e)
        {
            bsCrew.Filter = adgvCrew.FilterString;
        }

        private void adgvCrew_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditCrewmember();
        }

        private void FrmCrewOnBoard_Load(object sender, EventArgs e)
        {
            //Restore form settings
            // Upgrade?
            if (Properties.Settings.Default.FrmCrewOnBoardSize.Width == 0)
                Properties.Settings.Default.Upgrade();

            if (Properties.Settings.Default.FrmCrewOnBoardSize.Width == 0 || Properties.Settings.Default.FrmCrewOnBoardSize.Height == 0)
            {
                // first start
                // optional: add default values
            }
            else
            {
                this.WindowState = Properties.Settings.Default.FrmCrewOnBoardState;

                // we don't want a minimized window at startup
                if (this.WindowState == FormWindowState.Minimized) this.WindowState = FormWindowState.Normal;

                this.Location = Properties.Settings.Default.FrmCrewOnBoardLocation;
                this.Size = Properties.Settings.Default.FrmCrewOnBoardSize;
            }

            if (_formCaption.Length > 0)
                this.Text = this.Text + _formCaption;

            FillCrew();

            if (_filterString.Length > 0)
                bsCrew.Filter = _filterString;

            if (_sortString.Length > 0)
                try
                {
                    bsCrew.Sort = _sortString;
                }
                catch
                {

                }
        }

        private void btnUpdateCrew_Click(object sender, EventArgs e)
        {
            int records = MainForm.GetReportsCount();

            FormProgress progress = new FormProgress(records);

            progress.Show();

            System.Windows.Forms.Application.DoEvents();

            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select REPORT_CODE, VESSEL_GUID, INSPECTION_DATE\n" +
                "from REPORTS";

            OleDbDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                int i = 0;

                while (reader.Read())
                {
                    if (progress.terminate)
                        break;

                    try
                    {
                        string reportCode = reader["REPORT_CODE"].ToString();
                        Guid vesselGuid = MainForm.StrToGuid(reader["VESSEL_GUID"].ToString());
                        DateTime inspectionDate = Convert.ToDateTime(reader["INSPECTION_DATE"].ToString());

                        FillReportCrew(reportCode, vesselGuid, inspectionDate);
                    }
                    catch
                    {

                    }

                    i++;

                    progress.position = i;
                }

                progress.Close();
            }

        }

        private void FillReportCrew(string reportCode, Guid vesselGuid, DateTime inspectionDate)
        {
            if (reportCode.Length == 0)
                return;

            if (vesselGuid == MainForm.zeroGuid)
                return;

            if (inspectionDate < DateTime.Parse("2000-01-01"))
                return;

            string crewName = "";

            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select * \n" +
                "from CREW_POSITIONS \n" +
                "order by POSITION_INDEX";

            if (MainForm.DS.Tables.Contains("CREW_POSITIONS"))
                MainForm.DS.Tables["CREW_POSITIONS"].Clear();

            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            adapter.Fill(MainForm.DS, "CREW_POSITIONS");

            DataRow[] rows = MainForm.DS.Tables["CREW_POSITIONS"].Select();

            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    string crewPosition = row["POSITION_NAME"].ToString();
                    Guid positionGuid = MainForm.StrToGuid(row["CREW_POSITION_GUID"].ToString());

                    cmd.CommandText =
                        "select TOP 1 CREW_NAME, DATE_ON, DATE_OFF \n" +
                        "from CREW_ON_BOARD \n" +
                        "where \n" +
                        "VESSEL_GUID=" + MainForm.GuidToStr(vesselGuid) + "\n" +
                        "and CREW_POSITION='" + MainForm.StrToSQLStr(crewPosition) + "' \n" +
                        "and DATE_ON<=" + MainForm.DateTimeToQueryStr(inspectionDate) + "\n" +
                        "and (IsNull(DATE_OFF) or DATE_OFF>=" + MainForm.DateTimeToQueryStr(inspectionDate) + ") \n" +
                        "order by DATE_ON";

                    OleDbDataReader reader = cmd.ExecuteReader();


                    if (reader.HasRows)
                    {
                        reader.Read();

                        crewName = reader["CREW_NAME"].ToString();

                        Guid crewGuid = MainForm.GetCrewGuid(crewName, positionGuid);

                        if (crewGuid != MainForm.zeroGuid)
                        {
                            OleDbCommand cmd1 = new OleDbCommand("", MainForm.connection);

                            cmd1.CommandText =
                                "select Count(REPORT_CODE) as RecCount \n" +
                                "from REPORTS_CREW \n" +
                                "where \n" +
                                "REPORT_CODE='" + MainForm.StrToSQLStr(reportCode) + "' \n" +
                                "and CREW_POSITION_GUID=" + MainForm.GuidToStr(positionGuid);

                            int recCount = (int)cmd1.ExecuteScalar();

                            if (recCount == 0)
                            {
                                cmd1.CommandText =
                                    "insert into REPORTS_CREW (REPORT_CODE, CREW_GUID, CREW_POSITION_GUID) \n" +
                                    "values ('" + MainForm.StrToSQLStr(reportCode) + "'," +
                                    MainForm.GuidToStr(crewGuid) + "," +
                                    MainForm.GuidToStr(positionGuid) + ")";

                                if (MainForm.cmdExecute(cmd1) < 0)
                                {
                                    MessageBox.Show("Failed to insert new record for the crew during inspection",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                cmd1.CommandText =
                                    "update REPORTS_CREW set \n" +
                                    "CREW_GUID=" + MainForm.GuidToStr(crewGuid) + "\n" +
                                    "where \n" +
                                    "REPORT_CODE='" + MainForm.StrToSQLStr(reportCode) + "' \n" +
                                    "and CREW_POSITION_GUID=" + MainForm.GuidToStr(positionGuid);

                                if (MainForm.cmdExecute(cmd1) < 0)
                                {
                                    MessageBox.Show("Failed to update record for the crew during inspection",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    else
                    {
                        //MessageBox.Show("There is no suitable record in the list of crew on board", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    reader.Close();
                }

            }
        }
    }
}
