using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Data.OleDb;

namespace VIM
{
    public partial class FormCheckOCIMF : Form
    {
        private OleDbConnection connection;
        private DataSet DS;
        private string tempTable = "";
        private BindingSource bsRecords = new BindingSource();

        public FormCheckOCIMF()
        {
            this.Icon = MainForm.mainIcon;
            this.Font = MainForm.mainFont;

            DS = MainForm.DS;
            connection = MainForm.connection;

            InitializeComponent();

            FillTable();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                LoadFile(openFileDialog1.FileName);
            }
        }

        private void LoadFile(string fileName)
        {
            this.Cursor = Cursors.WaitCursor;

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

            tempTable = "TEMP_" + MainForm.userName.ToUpper();

            MainForm.tempTableCreate(tempTable, "create table " + tempTable + " (TEMP_ID counter Primary key, VESSEL_NAME varchar(255), VESSEL_IMO varchar(255), LAST_DATE DateTime)");

            int vslCol = 1;
            int imoCol = 2;
            int dateCol = 10;

            int lineCounter = 0;
            string cmdText = "";

            for (int row = 2; row <= rows; row++)
            {
                lineCounter++;

                if (lineCounter < 10)
                {
                    if (cmdText.Length == 0)
                    {
                        cmdText =
                            "select TOP 1 \n" +
                            "'" + MainForm.StrToSQLStr(valueArray[row, vslCol].ToString()) + "' as VESSEL_NAME,'" +
                            MainForm.StrToSQLStr(valueArray[row, imoCol].ToString()) + "' as VESSEL_IMO," +
                            MainForm.DateTimeToQueryStr(DateTime.FromOADate(Convert.ToDouble(valueArray[row, dateCol]))) + " as LAST_DATE \n" +
                            "from OPTIONS";
                    }
                    else
                    {
                        cmdText = cmdText + "\n union \n" +
                            "select TOP 1 \n" +
                            "'" + MainForm.StrToSQLStr(valueArray[row, vslCol].ToString()) + "','" +
                            MainForm.StrToSQLStr(valueArray[row, imoCol].ToString()) + "'," +
                            MainForm.DateTimeToQueryStr(DateTime.FromOADate(Convert.ToDouble(valueArray[row, dateCol]))) + " \n" +
                            "from OPTIONS";
                    }
                }
                else
                {
                    cmdText = cmdText + "\n union \n" +
                            "select TOP 1 \n" +
                            "'" + MainForm.StrToSQLStr(valueArray[row, vslCol].ToString()) + "','" +
                            MainForm.StrToSQLStr(valueArray[row, imoCol].ToString()) + "'," +
                            MainForm.DateTimeToQueryStr(DateTime.FromOADate(Convert.ToDouble(valueArray[row, dateCol]))) + " \n" +
                            "from OPTIONS";

                    cmd.CommandText =
                    "insert into " + tempTable + " (VESSEL_NAME, VESSEL_IMO, LAST_DATE) \n" +
                    "select * from (\n" +
                    cmdText + "\n)";

                    MainForm.cmdExecute(cmd);

                    System.Windows.Forms.Application.DoEvents();

                    lineCounter = 0;
                    cmdText = "";

                }


            }

            if (lineCounter > 0)
            {
                cmd.CommandText =
                        "insert into " + tempTable + " (VESSEL_NAME, VESSEL_IMO, LAST_DATE) \n" +
                        "select * from (\n" +
                        cmdText + "\n)";

                MainForm.cmdExecute(cmd);

                System.Windows.Forms.Application.DoEvents();

                lineCounter = 0;
            }

            FillTable();

            this.Cursor = Cursors.Default;
        }


        private void FillTable()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            if (DS.Tables.Contains("OCIMF_RECORDS"))
                DS.Tables["OCIMF_RECORDS"].Clear();

            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            if (tempTable.Length==0)
            {
                cmd.CommandText =
                    "select top 1 VESSEL_NAME, VESSEL_IMO, OFFICE, DATE() as LAST_DATE \n" +
                    "from VESSELS";
            }
            else
            {
                cmd.CommandText =
                    "select \n" +
                    "Q1.VESSEL_NAME, \n" +
                    "Q1.VESSEL_IMO, \n" +
                    "Q1.OFFICE, \n" +
                    "Q0.LAST_DATE \n" +
                    "from \n" +
                    tempTable + " as Q0 left join \n" +
                    "( \n" +
                    "select \n" +
                    "VESSELS.VESSEL_NAME, \n" +
                    "VESSELS.VESSEL_IMO, \n" +
                    "VESSELS.OFFICE, \n" +
                    "MAX(REPORTS.INSPECTION_DATE) as LAST_REPORT \n" +
                    "from \n" +
                    "VESSELS inner join REPORTS \n" +
                    "on VESSELS.VESSEL_GUID=REPORTS.VESSEL_GUID \n" +
                    "where \n" +
                    "REPORTS.MANUAL=FALSE \n" +
                    "group by VESSELS.VESSEL_NAME, VESSELS.VESSEL_IMO, VESSELS.OFFICE \n" +
                    ") as Q1 \n" +
                    "on Q0.VESSEL_NAME like Q1.VESSEL_NAME \n" +
                    "and Q0.VESSEL_IMO=Q1.VESSEL_IMO \n" +
                    "where \n" +
                    "Q0.LAST_DATE>Q1.LAST_REPORT \n" +
                    "order by Q1.VESSEL_NAME";
            }

            adapter.Fill(DS, "OCIMF_RECORDS");

            bsRecords.DataSource = DS;
            bsRecords.DataMember = "OCIMF_RECORDS";

            dgvOCIMFRecords.AutoGenerateColumns = true;

            dgvOCIMFRecords.DataSource = bsRecords;

            dgvOCIMFRecords.Columns["VESSEL_NAME"].HeaderText = "Vessel";
            dgvOCIMFRecords.Columns["VESSEL_NAME"].FillWeight = 50;

            dgvOCIMFRecords.Columns["VESSEL_IMO"].HeaderText = "IMO";
            dgvOCIMFRecords.Columns["VESSEL_IMO"].FillWeight = 20;

            dgvOCIMFRecords.Columns["OFFICE"].HeaderText = "Office";
            dgvOCIMFRecords.Columns["OFFICE"].FillWeight = 10;

            dgvOCIMFRecords.Columns["LAST_DATE"].HeaderText = "Last inspection";
            dgvOCIMFRecords.Columns["LAST_DATE"].FillWeight = 20;

            if (tempTable.Length == 0)
                DS.Tables["OCIMF_RECORDS"].Clear();
        }

        private void FormCheckOCIMF_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tempTable.Length==0)
                MainForm.tempTableDrop(tempTable);
        }

        private void dgvOCIMFRecords_FilterStringChanged(object sender, EventArgs e)
        {
            bsRecords.Filter = dgvOCIMFRecords.FilterString;
        }

        private void dgvOCIMFRecords_SortStringChanged(object sender, EventArgs e)
        {
            bsRecords.Sort = dgvOCIMFRecords.SortString;
        }
    }
}
