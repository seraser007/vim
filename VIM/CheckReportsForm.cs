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
using System.Runtime.InteropServices;

namespace VIM
{
    public partial class CheckReportsForm : Form
    {
        OleDbConnection connection;
        DataSet DS;
        string workFolder="";

        public CheckReportsForm(OleDbConnection mainConnection, DataSet mainDS, Icon mainIcon, Font mainFont, string mainWorkFolder)
        {
            this.Icon = mainIcon;
            this.Font = mainFont;

            connection = mainConnection;
            DS = mainDS;

            workFolder = mainWorkFolder;

            InitializeComponent();

            lblProgress.Text = "";
            lblReport.Text = "";
        }

        private int StartCheck()
        {
            this.Update();

            btnStartCheck.Enabled = false;

            progressBar1.Value = 0;
            progressBar1.Update();

            this.Cursor = Cursors.WaitCursor;
            
            lblProgress.Text = "Reading list of files";
            lblProgress.Update();
            lblReport.Text = "";
            lblReport.Update();

            int filesCount = 0;
            int reportsWOFileCount = 0;
            int reportsWFileCount = 0;

            while (dgFiles.Rows.Count > 0)
            {
                dgFiles.Rows.Remove(dgFiles.CurrentRow);
            }

            updateFilesLabel(filesCount.ToString());

            while (dgReportsWFile.Rows.Count > 0)
            {
                dgReportsWFile.Rows.Remove(dgReportsWFile.CurrentRow);
            }

            updateReportsWFileLabel(reportsWFileCount.ToString());

            while (dgReportsWOFile.Rows.Count > 0)
            {
                dgReportsWOFile.Rows.Remove(dgReportsWOFile.CurrentRow);
            }
            
            updateReportsWOFileLabel(reportsWOFileCount.ToString());

            string[] files1 = Directory.GetFiles(workFolder + "\\Reports\\", "*.pdf");

            filesCount = files1.Length;

            dgFiles.SuspendDrawing();

            for (int i=0; i<files1.Length; i++)
            {
                dgFiles.Rows.Add(1);
                dgFiles.Rows[dgFiles.Rows.Count-1].Cells["Files1"].Value = Path.GetFileName(files1[i]);
                

                updateFilesLabel(dgFiles.Rows.Count.ToString());

                Application.DoEvents();
            }

            //dgFiles.Update();
            dgFiles.ResumeDrawing();

            updateFilesLabel(filesCount.ToString());

            DataTable reportsTable = DS.Tables["REPORTS"];

            DataRow[] reportRows = reportsTable.Select();

            OleDbCommand cmd=new OleDbCommand("",connection);

            int index=DS.Tables["REPORTS"].Columns["REPORT_CODE"].Ordinal;

            lblProgress.Text = "Reading list of reports";
            lblProgress.Update();

            reportsWOFileCount = DS.Tables["REPORTS"].Rows.Count;

            dgReportsWOFile.SuspendDrawing();

            for (int i = 0; i < DS.Tables["REPORTS"].Rows.Count; i++)
            {
                dgReportsWOFile.Rows.Add(1);

                if (dgReportsWOFile.Rows.Count > 0)
                {
                    dgReportsWOFile.Rows[dgReportsWOFile.Rows.Count - 1].Cells["Reports2"].Value = reportRows[i].ItemArray[index].ToString();
                }

                updateReportsWOFileLabel(dgReportsWOFile.Rows.Count.ToString());

                Application.DoEvents();
            }

            dgReportsWOFile.ResumeDrawing();

            updateReportsWOFileLabel(reportsWOFileCount.ToString());

            int reportCount = reportRows.Length;

            progressBar1.Maximum = reportCount;

            this.Cursor = Cursors.WaitCursor;

            int filesFound = 0;

            //dgReportsWOFile.SuspendDrawing();
            //dgReportsWFile.SuspendDrawing();
            //dgFiles.SuspendDrawing();

            for (int i=0; i<reportCount; i++)
            {
                lblProgress.Text = Convert.ToString(i + 1) + " of " + reportCount.ToString();
                lblProgress.Update();

                progressBar1.Value = i + 1;
                progressBar1.Update();

                string localReportCode = reportRows[i].ItemArray[index].ToString();

                lblReport.Text = localReportCode + " ... ";
                lblReport.Update();


                string fileSearchValue = localReportCode + ".pdf";
                int fileRowIndex = -1;

                foreach (DataGridViewRow dgRow in dgFiles.Rows)
                {
                    if (dgRow.Cells["Files1"].Value.ToString().Equals(fileSearchValue))
                    {
                        fileRowIndex = dgRow.Index;
                        break;
                    }
                }

                //string fileName = workFolder + "\\Reports\\" + localReportCode + ".pdf";

                if (fileRowIndex>=0)
                {
                    lblReport.Text = lblReport.Text + "located";
                    lblReport.Update();

                    cmd.CommandText =
                        "update REPORTS set FILE_AVAILABLE=TRUE \n" +
                        "where REPORT_CODE='" + localReportCode + "'";
                    cmd.ExecuteNonQuery();

                    filesFound++;

                    //Add new row to the grid
                    dgReportsWFile.Rows.Add(1);

                    if (dgReportsWFile.Rows.Count > 0)
                    {
                        dgReportsWFile.Rows[dgReportsWFile.Rows.Count - 1].Cells["Reports1"].Value = localReportCode;
                    }

                    reportsWFileCount++;
                    updateReportsWFileLabel(reportsWFileCount.ToString());

                    //Delete file from list of files
                    String searchValue = localReportCode;
                    int rowIndex = -1;

                    foreach (DataGridViewRow dgRow in dgReportsWOFile.Rows)
                    {
                        if (dgRow.Cells["Reports2"].Value.ToString().Equals(searchValue))
                        {
                            rowIndex = dgRow.Index;
                            break;
                        }
                    }

                    if (rowIndex >= 0)
                    {
                        try
                        {
                            dgReportsWOFile.Rows.RemoveAt(rowIndex);
                            reportsWOFileCount--;
                            updateReportsWOFileLabel(reportsWOFileCount.ToString());
                        }
                        catch
                        {

                        }
                    }

                    //Delete file from the list of files

                    try
                    {
                        dgFiles.Rows.RemoveAt(fileRowIndex);
                        filesCount--;
                        updateFilesLabel(filesCount.ToString());
                    }
                    catch
                    {

                    }

                }
                else
                {
                    lblReport.Text = lblReport.Text + "was not found";
                    lblReport.Update();

                    cmd.CommandText =
                        "update REPORTS set FILE_AVAILABLE=FALSE \n" +
                        "where REPORT_CODE='" + localReportCode + "'";
                    cmd.ExecuteNonQuery();
                }

                Application.DoEvents();

            }

            //dgReportsWFile.ResumeDrawing();
            //dgReportsWOFile.ResumeDrawing();
            //dgFiles.ResumeDrawing();

            this.Cursor = Cursors.Default;

            dgFiles.Cursor = Cursors.Default;
            dgReportsWFile.Cursor = Cursors.Default;
            dgReportsWOFile.Cursor = Cursors.Default;

            MessageBox.Show(reportCount.ToString() + " reports were checked and " + filesFound.ToString() + " files were located", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            //this.Close();

            btnStartCheck.Enabled = true;

            return 1;
        }

        private void CheckReportsForm_Shown(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartCheck();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void updateFilesLabel(string Text)
        {
            lblFiles.Text = "Files without report (" + Text + ")";
            lblFiles.Update();
        }

        private void updateReportsWOFileLabel(string Text)
        {
            lblReportsWOFile.Text = "Reports without file (" + Text + ")";
            lblReportsWOFile.Update();

        }

        private void updateReportsWFileLabel(string Text)
        {
            lblReportsWFile.Text = "Reports with file (" + Text + ")";
            lblReportsWFile.Update();

        }
    }

    public static class ControlHelper
    {
        #region Redraw Suspend/Resume
        [DllImport("user32.dll", EntryPoint = "SendMessageA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        private const int WM_SETREDRAW = 0xB;

        public static void SuspendDrawing(this Control target)
        {
            SendMessage(target.Handle, WM_SETREDRAW, 0, 0);
        }

        public static void ResumeDrawing(this Control target) { ResumeDrawing(target, true); }
        public static void ResumeDrawing(this Control target, bool redraw)
        {
            SendMessage(target.Handle, WM_SETREDRAW, 1, 0);

            if (redraw)
            {
                target.Refresh();
            }
        }
        #endregion
    }
}
