using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Globalization;

namespace VIM
{
    public partial class VesselsForm : Form
    {
        OleDbConnection connection;
        DataSet DS;
        OleDbDataAdapter vesselsAdapter;
        BindingSource bsVessels = new BindingSource();

        public VesselsForm()
        {
            this.Font = MainForm.mainFont;
            this.Icon = MainForm.mainIcon;

            InitializeComponent();

            DS = MainForm.DS;
            connection = MainForm.connection;

            FillVessels(false);

            ProtectFields(MainForm.isPowerUser);
        }

        private void FillVessels(bool showHidden)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                string cmdText =
                    "select VESSELS.VESSEL_GUID, VESSEL_NAME, VESSEL_IMO, VESSEL_EMAIL, FLEET, OFFICE, DOC, HULL_CLASS, " +
                    "NOTES, REPORT_NUMBER, OBS_NUMBER, HIDDEN \n" +
                    "from (VESSELS left join  \n" +
                    "(select Q01.VESSEL_GUID, REPORT_NUMBER, OBS_NUMBER  \n" +
                    "from  \n" +
                    "(select VESSEL_GUID, COUNT(REPORT_CODE) as REPORT_NUMBER  \n" +
                    "from REPORTS \n" +
                    "group by VESSEL_GUID) as Q01  \n" +
                    "left join  \n" +
                    "(select REPORTS.VESSEL_GUID, COUNT(OBSERVATION) as OBS_NUMBER  \n" +
                    "from REPORT_ITEMS inner join REPORTS  \n" +
                    "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE \n" +
                    "where LEN(OBSERVATION)>0  \n" +
                    "group by VESSEL_GUID) as Q02  \n" +
                    "on Q01.VESSEL_GUID=Q02.VESSEL_GUID) as Q1  \n" +
                    "on VESSELS.VESSEL_GUID=Q1.VESSEL_GUID)  \n" +
                    "where VESSELS.HIDDEN=FALSE or TRUE=" + showHidden.ToString() + "\n" +
                    "order by VESSEL_NAME";

                vesselsAdapter = new OleDbDataAdapter(cmdText, connection);


                if (DS.Tables.Contains("FULL_VESSEL_LIST"))
                {
                    DS.Tables["FULL_VESSEL_LIST"].Clear();
                }

                vesselsAdapter.Fill(DS, "FULL_VESSEL_LIST");

                bsVessels.DataSource = DS;
                bsVessels.DataMember = "FULL_VESSEL_LIST";

                adgvVessels.DataSource = bsVessels;
                adgvVessels.AutoGenerateColumns = true;

                adgvVessels.Columns["VESSEL_GUID"].Visible = false;

                adgvVessels.Columns["VESSEL_NAME"].HeaderText = "Vessel name";
                adgvVessels.Columns["VESSEL_NAME"].FillWeight = 70;

                adgvVessels.Columns["VESSEL_IMO"].HeaderText = "IMO number";
                adgvVessels.Columns["VESSEL_IMO"].FillWeight = 30;

                adgvVessels.Columns["VESSEL_EMAIL"].HeaderText = "Email";
                adgvVessels.Columns["VESSEL_EMAIL"].FillWeight = 40;

                adgvVessels.Columns["FLEET"].HeaderText = "Fleet";
                adgvVessels.Columns["FLEET"].FillWeight = 20;

                adgvVessels.Columns["OFFICE"].HeaderText = "Office";
                adgvVessels.Columns["OFFICE"].FillWeight = 20;

                adgvVessels.Columns["DOC"].HeaderText = "DOC";
                adgvVessels.Columns["DOC"].FillWeight = 20;

                adgvVessels.Columns["HULL_CLASS"].HeaderText = "Hull Class";
                adgvVessels.Columns["HULL_CLASS"].FillWeight = 50;

                adgvVessels.Columns["NOTES"].Visible = false;

                adgvVessels.Columns["REPORT_NUMBER"].HeaderText = "Inspections";
                adgvVessels.Columns["REPORT_NUMBER"].FillWeight = 20;

                adgvVessels.Columns["OBS_NUMBER"].HeaderText = "Observations";
                adgvVessels.Columns["OBS_NUMBER"].FillWeight = 20;

                adgvVessels.Columns["HIDDEN"].HeaderText = "Hidden";
                adgvVessels.Columns["HIDDEN"].FillWeight = 20;
                adgvVessels.Columns["HIDDEN"].Visible = showHidden;

                OleDbCommand cmd = new OleDbCommand("", connection);

                cmd.CommandText =
                    "select * from \n" +
                    "(select VESSEL_GUID, VESSEL_IMO, VESSEL_NAME, DOC, HULL_CLASS, VESSEL_NAME+' (IMO:'+VESSEL_IMO+')' as FULL_VESSEL \n" +
                    "from VESSELS \n" +
                    "where \n" +
                    "VESSELS.HIDDEN=FALSE or TRUE=" + showHidden.ToString() + "\n" +
                    "union \n" +
                    "select top 1 "+MainForm.GuidToStr(MainForm.zeroGuid)+" as VESSEL_GUID, '' as VESSEL_IMO, '' as VESSEL_NAME, '' as DOC, '' as HULL_CLASS, '' as FULL_VESSEL \n" +
                    "from VESSELS) \n" +
                    "order by VESSEL_NAME";

                OleDbDataAdapter vesselsFindAdapter = new OleDbDataAdapter(cmd);

                if (DS.Tables.Contains("VESSELS_FIND_LIST"))
                    DS.Tables["VESSELS_FIND_LIST"].Clear();

                vesselsFindAdapter.Fill(DS, "VESSELS_FIND_LIST");

                DataTable vessels = DS.Tables["VESSELS_FIND_LIST"];

                findBox.DataSource = vessels;
                findBox.DisplayMember = "FULL_VESSEL";
                findBox.ValueMember = "VESSEL_GUID";

                CountRecords();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

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

        private void button3_Click(object sender, EventArgs e)
        {
            editVessel();
        }

        public string StrToSQLStr(string Text)
        {
            if (Text == null)
                return "";
            else
                return Text.Replace("'", "''");
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            editVessel();
        }

        private void editVessel()
        {
            //Edit vessel
            string vesselNameOld = adgvVessels.CurrentRow.Cells["VESSEL_NAME"].Value.ToString();
            string vesselIMOOld = adgvVessels.CurrentRow.Cells["VESSEL_IMO"].Value.ToString();
            Guid vesselGuid = MainForm.StrToGuid(adgvVessels.CurrentRow.Cells["VESSEL_GUID"].Value.ToString());

            VesselForm frmVessel = new VesselForm(connection, DS, this.Font, this.Icon, vesselIMOOld, vesselNameOld);
            
            int curRow = adgvVessels.CurrentCell.RowIndex;
            int curCol = adgvVessels.CurrentCell.ColumnIndex;


            OleDbCommand cmd = new OleDbCommand("", connection);
 
            frmVessel.vesselName = adgvVessels.CurrentRow.Cells["VESSEL_NAME"].Value.ToString();
            frmVessel.vesselIMO = adgvVessels.CurrentRow.Cells["VESSEL_IMO"].Value.ToString();
            frmVessel.vesselOffice = adgvVessels.CurrentRow.Cells["OFFICE"].Value.ToString();
            frmVessel.vesselDOC = adgvVessels.CurrentRow.Cells["DOC"].Value.ToString();
            frmVessel.vesselHullClass = adgvVessels.CurrentRow.Cells["HULL_CLASS"].Value.ToString();
            frmVessel.vesselNotes = adgvVessels.CurrentRow.Cells["NOTES"].Value.ToString();
            frmVessel.vesselEmail = adgvVessels.CurrentRow.Cells["VESSEL_EMAIL"].Value.ToString();
            frmVessel.vesselFleet = adgvVessels.CurrentRow.Cells["FLEET"].Value.ToString();
            frmVessel.hidden = Convert.ToBoolean(adgvVessels.CurrentRow.Cells["HIDDEN"].Value);
            frmVessel.vesselGuid = vesselGuid;

            var rslt = frmVessel.ShowDialog();

            if ((rslt == DialogResult.OK) && (MainForm.isPowerUser))
            {
                cmd.CommandText =
                    "update VESSELS set\n" +
                    "VESSEL_NAME='" + StrToSQLStr(frmVessel.vesselName) + "',\n" +
                    "VESSEL_IMO='" + StrToSQLStr(frmVessel.vesselIMO) + "',\n" +
                    "VESSEL_EMAIL='"+StrToSQLStr(frmVessel.vesselEmail)+"',\n"+
                    "OFFICE='"+MainForm.StrToIniStr(frmVessel.vesselOffice)+"',\n"+
                    "DOC='" + StrToSQLStr(frmVessel.vesselDOC) + "',\n" +
                    "HULL_CLASS='"+StrToSQLStr(frmVessel.vesselHullClass)+"', \n"+
                    "NOTES='"+StrToSQLStr(frmVessel.vesselNotes)+"', \n"+
                    "FLEET='"+StrToSQLStr(frmVessel.vesselFleet)+"', \n"+
                    "HIDDEN="+frmVessel.hidden.ToString()+" \n"+
                    "where VESSEL_GUID=" + MainForm.GuidToStr(vesselGuid);
                MainForm.cmdExecute(cmd);
 
                    //cmd.ExecuteNonQuery();

                string vesselNameNew = frmVessel.vesselName;
                string vesselIMONew = frmVessel.vesselIMO;

                //check vessel name and IMO number
                if ((vesselNameOld != vesselNameNew) || (vesselIMOOld != vesselIMONew))
                {
                    cmd.CommandText =
                        "update REPORTS set \n"+
                        "VESSEL='" + StrToSQLStr(vesselNameNew) + "', \n" +
                        "VESSEL_IMO='" + StrToSQLStr(vesselIMONew) + "' \n" +
                        "where VESSEL_GUID=" + MainForm.GuidToStr(vesselGuid);
                    MainForm.cmdExecute(cmd);

                }

                if (vesselNameOld != vesselNameNew)
                {
                    MainForm.UpdateVesselGuid4CrewOnBoard();
                }

                int row = adgvVessels.CurrentRow.Index;
                int firstVisibleRow = adgvVessels.FirstDisplayedScrollingRowIndex;

                DS.Tables["FULL_VESSEL_LIST"].Clear();
                vesselsAdapter.Fill(DS, "FULL_VESSEL_LIST");

                adgvVessels.FirstDisplayedScrollingRowIndex = firstVisibleRow;

                adgvVessels.CurrentCell = adgvVessels[curCol, curRow];
            }
            else
            {
                if (frmVessel.dataChanged)
                {
                    int row = adgvVessels.CurrentRow.Index;
                    int firstVisibleRow = adgvVessels.FirstDisplayedScrollingRowIndex;

                    DS.Tables["FULL_VESSEL_LIST"].Clear();
                    vesselsAdapter.Fill(DS, "FULL_VESSEL_LIST");

                    adgvVessels.FirstDisplayedScrollingRowIndex = firstVisibleRow;

                    adgvVessels.CurrentCell = adgvVessels[curCol, curRow];
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Add new vessel
            VesselForm form = new VesselForm(connection, DS, this.Font, this.Icon,"","");

            OleDbCommand cmd = new OleDbCommand("", connection);

            form.vesselName = "";
            form.vesselIMO = "";
            form.vesselEmail = "";
            form.vesselOffice = "";
            form.vesselDOC = "";
            form.vesselHullClass = "";
            form.vesselNotes = "";
            form.vesselFleet = "";
            form.hidden = false;

            var rslt = form.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                cmd.CommandText =
                    "insert into VESSELS (VESSEL_GUID, VESSEL_NAME, VESSEL_IMO, VESSEL_EMAIL, OFFICE, DOC, HULL_CLASS, NOTES, FLEET, HIDDEN)\n" +
                    "values(" + MainForm.GuidToStr(Guid.NewGuid())+",'" + 
                                StrToSQLStr(form.vesselName) + "','" +
                                StrToSQLStr(form.vesselIMO) + "','" +
                                StrToSQLStr(form.vesselEmail)+"','"+
                                MainForm.StrToIniStr(form.vesselOffice)+"','"+
                                StrToSQLStr(form.vesselDOC) + "','"+
                                StrToSQLStr(form.vesselHullClass)+"','"+
                                StrToSQLStr(form.vesselNotes)+"','"+
                                StrToSQLStr(form.vesselFleet)+"',"+
                                form.hidden.ToString()+")";
                cmd.ExecuteNonQuery();

                int row = adgvVessels.CurrentRow.Index;

                DS.Tables["FULL_VESSEL_LIST"].Clear();
                vesselsAdapter.Fill(DS, "FULL_VESSEL_LIST");

                CountRecords();

                String searchValue = form.vesselName;
                
                int rowIndex = -1;

                foreach (DataGridViewRow dgRow in adgvVessels.Rows)
                {
                    if (dgRow.Cells[1].Value.ToString().Equals(searchValue))
                    {
                        rowIndex = dgRow.Index;
                        break;
                    }
                }


                if (rowIndex >= 0)
                {
                    adgvVessels.FirstDisplayedScrollingRowIndex = rowIndex;
                    adgvVessels.Rows[rowIndex].Selected = true;
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Delete vessel
            string vesselName = adgvVessels.CurrentRow.Cells["VESSEL_NAME"].Value.ToString();
            string vesselIMO = adgvVessels.CurrentRow.Cells["VESSEL_IMO"].Value.ToString();
            Guid vesselGuid = MainForm.StrToGuid(adgvVessels.CurrentRow.Cells["VESSEL_GUID"].Value.ToString());

            string msg = "You are going to delete the following record:\n\n" +
                "Vessel name : " + vesselName + "\n" +
                "IMO number : " + vesselIMO + "\n\n" +
                "Would you like to proceed?";

            var rslt = System.Windows.Forms.MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (rslt == DialogResult.Yes)
            {
                //Проверяем есть ли записи для данного судна
                OleDbCommand cmd = new OleDbCommand("", connection);
                cmd.CommandText =
                    "select Count(VESSEL_GUID) from \n" +
                    "REPORTS\n" +
                    "where \n" +
                    "VESSEL_GUID=" + MainForm.GuidToStr(vesselGuid);

                int count=(int) cmd.ExecuteScalar();

                if (count>0)
                {
                    //There are reports
                        
                    if (MessageBox.Show("There is at least one inspection report for this vessel.\n"+
                        "Would you like to delete this vessel anyway?", "Confirmation", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                    {
                        //Agree to delete
                        if (MessageBox.Show("All reports for this vessel will have not information about vessel.\n"+
                            "Would you like to proceed?","Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                        {
                            //Clear vessel name and IMO number in reports table
                            cmd.CommandText =
                                "update REPORTS set \n" +
                                "VESSEL_GUID=Null \n" +
                                "where VESSEL_GUID=" + MainForm.GuidToStr(vesselGuid);

                            if (MainForm.cmdExecute(cmd)>=0)
                                DeleteVessel(MainForm.StrToGuid(adgvVessels.CurrentRow.Cells["VESSEL_GUID"].Value.ToString()));
                        }
                    }
                    else
                        return;
                    
                }
                else
                {
                    DeleteVessel(MainForm.StrToGuid(adgvVessels.CurrentRow.Cells["VESSEL_GUID"].Value.ToString()));
                }
            }
        }

        private void locateRecord()
        {
            if (findBox.SelectedValue == null)
            {
                String searchValue = findBox.Text.ToUpper();

                if (searchValue.Length == 0) return;

                int rowIndex = -1;

                foreach (DataGridViewRow dgRow in adgvVessels.Rows)
                {
                    string vslName = dgRow.Cells["VESSEL_NAME"].Value.ToString().ToUpper();

                    if (vslName.Contains(searchValue))
                    {
                        rowIndex = dgRow.Index;
                        break;
                    }
                }

                if (rowIndex >= 0)
                {
                    //dataGridView1.Rows[rowIndex].Selected = true;
                    adgvVessels.CurrentCell = adgvVessels[1, rowIndex];
                }
                else
                    MessageBox.Show("Nothing was found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            else
            {
                String searchValue = findBox.SelectedValue.ToString();

                if (searchValue.Length == 0) return;

                int rowIndex = -1;

                foreach (DataGridViewRow dgRow in adgvVessels.Rows)
                {
                    if (dgRow.Cells["VESSEL_GUID"].Value.ToString().Equals(searchValue))
                    {
                        rowIndex = dgRow.Index;
                        break;
                    }
                }

                if (rowIndex >= 0)
                {
                    //dataGridView1.Rows[rowIndex].Selected = true;
                    adgvVessels.CurrentCell = adgvVessels[1, rowIndex];
                }
                else
                    MessageBox.Show("Nothing was found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void findBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                locateRecord();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            locateRecord();
        }

        private void btnSummary_Click(object sender, EventArgs e)
        {
            if (adgvVessels.Rows.Count == 0)
                return;

            //Read settings from ini file
            string iniFileName = Path.Combine(MainForm.serviceFolder, "Settings_Common.ini");
            IniFile iniFile = new IniFile(iniFileName);

            //Use global section for bulletin settings
            string section = "VesselSummary";

            int actionCode = iniFile.ReadInteger(section, "SummaryAction", 0);
            
            string messageSubject = iniFile.ReadString(section, "MessageSubject", "");
            string messageText = MainForm.LineToText(iniFile.ReadString(section, "MessageText", ""));
            string fileNameTemplate = iniFile.ReadString(section, "FileNameTemplate", "Observations for mv %VesselName till %Date");
            bool showFile = iniFile.ReadBoolean(section, "ShowFile", true);
            string fileFormatText = iniFile.ReadString(section, "FileFormatExt", "Microsoft Word (.docx)");
            string CCAddresses = iniFile.ReadString(section, "CCAddresses", "");
            bool useTemplate = iniFile.ReadBoolean(section, "UseTemplate", false);
            string templateFile = iniFile.ReadString(section, "TemplateFile", "");

            string fleetEmail = "";
            bool sendToFleet = iniFile.ReadBoolean(section, "SendCopyToFleet", true);

            if (sendToFleet)
            {
                fleetEmail = MainForm.GetFleetEmail(MainForm.StrToGuid(adgvVessels.CurrentRow.Cells["VESSEL_GUID"].Value.ToString()));
            }

            if (fleetEmail.Length>0)
            {
                if (CCAddresses.Length == 0)
                    CCAddresses = fleetEmail;
                else
                    CCAddresses = CCAddresses + "; " + fleetEmail;
            }

            string fileExt = "";
            string fileName = "";

            if (actionCode == 1)
            {
                if (fileFormatText.Contains("(.doc)"))
                    fileExt = ".doc";
                else
                {
                    if (fileFormatText.Contains("(.pdf)"))
                        fileExt = ".pdf";
                    else
                        fileExt = ".docx";
                }


                if (fileNameTemplate.Length > 0)
                {
                    fileName = fileNameTemplate;

                    //Replace literals
                    fileName = fileName.Replace("%VesselName", adgvVessels.CurrentRow.Cells["VESSEL_NAME"].Value.ToString());
                    fileName = fileName.Replace("%DateTime", DateTime.Now.ToString());
                    fileName = fileName.Replace("%Date", DateTime.Now.ToShortDateString());

                    fileName = fileName.Replace("\\", "-");
                    fileName = fileName.Replace("/", ".");
                    fileName = fileName.Replace(":", "-");
                }
                else
                {
                    fileName = "Observations for mv \"" + adgvVessels.CurrentRow.Cells["VESSEL_NAME"].Value.ToString() + "\" till " + DateTime.Now.ToShortDateString();
                    fileName = fileName.Replace("\\", "-");
                    fileName = fileName.Replace("/", ".");
                    fileName = fileName.Replace(":", "-");
                }

                string reportFolder = MainForm.appTempFolderName + "\\Inspectors summary";

                if (!Directory.Exists(reportFolder))
                {
                    Directory.CreateDirectory(reportFolder);
                }

                fileName = reportFolder + "\\" + fileName + fileExt;

                if (File.Exists(fileName))
                    File.Delete(fileName);

                showFile = false;
            }

            string createdFile = CreateSummary(actionCode, fileExt, fileName, showFile, useTemplate, templateFile);
            string vesselEmail = adgvVessels.CurrentRow.Cells["VESSEL_EMAIL"].Value.ToString(); ;

            if (createdFile.Length>0 && actionCode==1)
            {
                int mailClientID = MainForm.GetMailClientID();

                switch (mailClientID)
                {
                    case 0: //Outlook
                        //Send message to Outlook
                        Outlook.Application application = new Outlook.Application();

                        Outlook.MailItem mail = (Outlook.MailItem)application.CreateItem(Outlook.OlItemType.olMailItem);

                        //vesselEmail = adgvVessels.CurrentRow.Cells["VESSEL_EMAIL"].Value.ToString();

                        if (vesselEmail.Length > 0)
                            mail.Recipients.Add(vesselEmail);

                        if (CCAddresses.Length > 0)
                            mail.CC = CCAddresses.Replace("%nl", ";");

                        mail.Subject = messageSubject;
                        mail.Body = messageText;
                        mail.Attachments.Add(createdFile,
                                   Outlook.OlAttachmentType.olByValue, Type.Missing, Type.Missing);

                        try
                        {
                            mail.Display(1);
                        }
                        catch (Exception E)
                        {
                            MessageBox.Show(E.Message);
                        }

                        break;
                    case 1: //MAPI
                        //Use MAPI

                        SimpleMAPI sMapi = new SimpleMAPI();

                        //vesselEmail = adgvVessels.CurrentRow.Cells["VESSEL_EMAIL"].Value.ToString();

                        if (sMapi.Logon(this.Handle))
                        {

                            if (vesselEmail.Length > 0)
                                sMapi.AddRecipient(vesselEmail, null, false);

                            if (CCAddresses.Length > 0)
                                sMapi.AddRecipient(CCAddresses.Replace("%nl", ";"), null, true);

                            sMapi.Attach(createdFile);

                            if (!sMapi.Send(messageSubject, messageText, true))
                            {
                                MessageBox.Show("Failed to create message", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            sMapi.Logoff();
                        }
                        else
                        {
                            MessageBox.Show("Failed to login in mail client", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        break;
                    case 2: //Application
                        FrmMail form = new FrmMail();

                        form.msgToAddress = vesselEmail;

                        if (CCAddresses.Length > 0)
                            form.msgCcAddress = CCAddresses.Replace("%nl", ";");

                        form.msgText = messageText;
                        form.msgSubject = messageSubject;

                        form.AddAttachment(createdFile);

                        form.ShowDialog();

                        break;
                }

            }
        }

        private string CreateSummary(int actionCode, string fileExt, string fileName, bool showFile, bool useTemplate, string templateName)
        {
            

            string VesselName = adgvVessels.CurrentRow.Cells["VESSEL_NAME"].Value.ToString();
            string VesselIMO = adgvVessels.CurrentRow.Cells["VESSEL_IMO"].Value.ToString();
            Guid vesselGuid = MainForm.StrToGuid(adgvVessels.CurrentRow.Cells["VESSEL_GUID"].Value.ToString());

            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select \n"+
                "Q.QUESTION_NUMBER, \n"+
                "Q.REPORT_CODE, \n"+
                "Q.OBSERVATION, \n"+
                "Q.OPERATOR_COMMENTS, \n"+
                "INSPECTION_DATE, \n"+
                "SEQUENCE, \n"+
                "CHAPTER_NUMBER \n"+
                "from \n"+
                "((select \n"+
                "REPORT_ITEMS.QUESTION_NUMBER, \n" +
                "REPORT_ITEMS.REPORT_CODE, \n" +
                "REPORT_ITEMS.OBSERVATION, \n" +
                "REPORT_ITEMS.OPERATOR_COMMENTS,  \n" +
                "REPORT_ITEMS.QUESTION_GUID \n" +
                "from REPORT_ITEMS inner join REPORTS \n" +
                "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE \n" +
                "where \n"+
                "LEN(REPORT_ITEMS.OBSERVATION)>0 \n" +
                "and REPORTS.VESSEL_GUID="+MainForm.GuidToStr(vesselGuid)+") as Q \n"+
                "inner join \n"+
                "(select  \n"+
                "REPORT_CODE, \n"+
                "INSPECTION_DATE, \n"+
                "VESSEL_IMO, \n"+
                "TEMPLATES.TEMPLATE_GUID \n"+
                "from \n"+
                "REPORTS left join TEMPLATES \n"+
                "on (TEMPLATES.VERSION=REPORTS.VIQ_VERSION \n"+
                "and TEMPLATES.TYPE_CODE=REPORTS.VIQ_TYPE_CODE) \n"+
                "where REPORTS.VESSEL_GUID="+MainForm.GuidToStr(vesselGuid)+") as RP \n"+
                "on Q.REPORT_CODE=RP.REPORT_CODE) \n"+
                "left join TEMPLATE_QUESTIONS \n"+
                "on Q.QUESTION_GUID=TEMPLATE_QUESTIONS.QUESTION_GUID \n"+
                "where \n"+
                "RP.TEMPLATE_GUID=TEMPLATE_QUESTIONS.TEMPLATE_GUID \n" +
                "order by TEMPLATE_QUESTIONS.SEQUENCE";

            this.Cursor = Cursors.WaitCursor;

            OleDbDataReader obsReader = cmd.ExecuteReader();

            if (!obsReader.HasRows)
                return "";
            else
            {
                if (useTemplate)
                {
                    //Check template file exists
                    string templateFile = Path.Combine(Path.Combine(MainForm.workFolder, "Templates"), templateName);
                    
                    if (File.Exists(templateFile))
                    {
                        return CreateTemplateSummary(VesselName, obsReader, actionCode, fileName, fileExt, showFile,templateFile);
                    }
                    else
                    {
                        var rslt = MessageBox.Show("Template file \"" + templateFile + "\" was not found.\n" +
                            "Default summary will be created in default format.\n" +
                            "Do you agree to create summary in default format?",
                            "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (rslt == DialogResult.Yes)
                            return CreateDefaultSummary(VesselName, obsReader, actionCode, fileName, fileExt, showFile);
                        else
                            return "";
                    }
                }

                return CreateDefaultSummary(VesselName, obsReader, actionCode, fileName, fileExt, showFile);
            }

        }

        private string CreateTemplateSummary(string VesselName, OleDbDataReader obsReader, int actionCode, string fileName, string fileExt, bool showFile, string templateFile)
        {
            Word.Document wordDocument;

            //Create Word document
            Word.Application wordapp = new Word.Application();

            //Show Word window
            wordapp.Visible = false;

            this.Cursor = Cursors.WaitCursor;

            try
            {
                Object template = templateFile;
                Object newTemplate = false;
                Object documentType = Word.WdNewDocumentType.wdNewBlankDocument;
                Object visible = true;

                //Создаем документ
                wordDocument = wordapp.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible);

                object o = Missing.Value;
                object oFalse = false;
                object oTrue = true;

                foreach (Word.Range range in wordDocument.StoryRanges)
                {
                    Word.Find find = range.Find;
                    object findText = "%ObsType";
                    object replacText = MainForm.appType;
                    object replace = Word.WdReplace.wdReplaceAll;
                    object findWrap = Word.WdFindWrap.wdFindContinue;

                    find.Execute(ref findText, ref o, ref o, ref o, ref oFalse, ref o,
                        ref o, ref findWrap, ref o, ref replacText,
                        ref replace, ref o, ref o, ref o, ref o);

                    Marshal.FinalReleaseComObject(find);
                    Marshal.FinalReleaseComObject(range);
                }

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");
                string strDate = DateTime.Today.ToString("D", culture);

                foreach (Word.Range range in wordDocument.StoryRanges)
                {
                    Word.Find find = range.Find;
                    object findText = "%ReportDate";
                    object replacText = strDate;
                    object replace = Word.WdReplace.wdReplaceAll;
                    object findWrap = Word.WdFindWrap.wdFindContinue;

                    find.Execute(ref findText, ref o, ref o, ref o, ref oFalse, ref o,
                        ref o, ref findWrap, ref o, ref replacText,
                        ref replace, ref o, ref o, ref o, ref o);

                    Marshal.FinalReleaseComObject(find);
                    Marshal.FinalReleaseComObject(range);
                }

                foreach (Word.Range range in wordDocument.StoryRanges)
                {
                    Word.Find find = range.Find;
                    object findText = "%VesselName";
                    object replacText = VesselName;
                    object replace = Word.WdReplace.wdReplaceAll;
                    object findWrap = Word.WdFindWrap.wdFindContinue;

                    find.Execute(ref findText, ref o, ref o, ref o, ref oFalse, ref o,
                        ref o, ref findWrap, ref o, ref replacText,
                        ref replace, ref o, ref o, ref o, ref o);

                    Marshal.FinalReleaseComObject(find);
                    Marshal.FinalReleaseComObject(range);
                }

                Word.Range chapter=null;
                Word.Range aChapter = null;
                Word.Style chapterStyle = null;

                foreach (Word.Range range in wordDocument.StoryRanges)
                {
                    Word.Find find = range.Find;
                    object findText = "%ChapterHeader";
                    object replacText = "";
                    object replace = Word.WdReplace.wdReplaceNone;
                    object findWrap = Word.WdFindWrap.wdFindStop;

                    if (find.Execute(ref findText, ref o, ref o, ref o, ref o, ref o, ref o, ref findWrap, ref o, ref o, ref o,
                        ref o, ref o, ref o, ref o))
                    {
                        chapter = range.Duplicate;
                        aChapter = chapter.Duplicate;
                        chapterStyle = chapter.ParagraphStyle;

                        //MessageBox.Show(chapter.Text);

                        break;
                    }

                    Marshal.FinalReleaseComObject(find);
                    Marshal.FinalReleaseComObject(range);
                }

                Word.Table table = null;
                Word.Range tableRange = null;
                int paraCount = 0;

                bool tableFound = false;

                for (int i = 1; i <= wordDocument.Tables.Count; i++)
                {
                    table = wordDocument.Tables[i];

                    if (table.Rows.Count>1 && table.Cell(2, 1).Range.Text.StartsWith("%QN"))
                    {
                        tableFound = true;
                        break;
                    }
                }

                if (tableFound)
                {
                    int counter = 0;
                    
                    //Find values columns
                    int qnColumn=0;
                    int obsColumn=0;
                    int comColumn=0;
                    string chapterText=chapter.Text;

                    tableRange = table.Range.Duplicate;
                    paraCount = wordDocument.Range(ref o, tableRange.End).Paragraphs.Count;

                    Word.Table aTable = table;

                    foreach (Word.Cell cell in table.Rows[2].Cells)
                    {
                        if (cell.Range.Text.Contains("%QN"))
                            qnColumn=cell.ColumnIndex;
                        else
                        {
                            if (cell.Range.Text.Contains("%Observation"))
                                obsColumn=cell.ColumnIndex;
                            else
                            {
                                if (cell.Range.Text.Contains("%Comments"))
                                    comColumn=cell.ColumnIndex;
                            }
                        }
                    }


                    Word.Range qnHeaderRange=table.Cell(1,qnColumn).Range;
                    Word.Range obsHeaderRange=table.Cell(1,obsColumn).Range;
                    Word.Range comHeaderRange=table.Cell(1,comColumn).Range;

                    Word.Range qnRange=table.Cell(2,1).Range;
                    Word.Range obsRange=table.Cell(2,2).Range;
                    Word.Range comRange=table.Cell(2,3).Range;

                    int chapterNumber = 0;
                    int chapterCounter = 0;

                    if (obsReader.HasRows)
                    {
                        while (obsReader.Read())
                        {
                            Word.Row row = null;

                            int chnum = Convert.ToInt16(obsReader["CHAPTER_NUMBER"]);

                            if (chnum!=chapterNumber)
                            {

                                if (chapterCounter > 0)
                                {
                                    Word.Range lastTableRange = wordDocument.Tables[wordDocument.Tables.Count].Range;
                                    int curPara=wordDocument.Range(ref o, lastTableRange.End).Paragraphs.Count;
                                    //wordDocument.Range(ref o, table.Range.End).InsertParagraphAfter();

                                    //MessageBox.Show("No : " + curPara.ToString() + " Text : " + wordDocument.Paragraphs[curPara].Range.Text);

                                    wordDocument.Paragraphs[curPara+1].Range.InsertParagraphBefore();
                                    wordDocument.Paragraphs[curPara+1].Range.InsertParagraphBefore();
                                    wordDocument.Paragraphs[curPara+1].Range.InsertParagraphBefore();

                                    if (paraCount < curPara) paraCount = curPara;

                                    paraCount=paraCount+2;

                                    chapter = wordDocument.Paragraphs[paraCount].Range;
                                    //chapter.Paragraphs[1].set_Style(chapterStyle);
                                    chapter.Text = "Chapter X";
                                    chapter.set_Style(chapterStyle.NameLocal);

                                    wordDocument.Range(ref o, chapter.End).InsertParagraphAfter();
                                    //wordDocument.Range(ref o, chapter.End).InsertParagraphAfter();
                                    
                                    paraCount = paraCount + 1;
                                    Word.Range tRange = wordDocument.Paragraphs[paraCount].Range;
                                    tRange.Tables.Add(tRange,2, aTable.Columns.Count);
                                    table = tRange.Tables[1];

                                    table.Rows.AllowBreakAcrossPages = 0;


                                    for (int i = 1; i <= aTable.Columns.Count;i++ )
                                    {
                                        table.Columns[i].Width = aTable.Columns[i].Width;
                                    }

                                    //Word.Style tabStyle = aTable.get_Style();
                                    //table.set_Style(tabStyle);

                                    table.Columns.Borders.Enable = aTable.Columns.Borders.Enable;
                                    table.Columns.Borders.InsideColor = aTable.Columns.Borders.InsideColor;
                                    table.Range.Borders.OutsideLineStyle = aTable.Range.Borders.OutsideLineStyle;
                                    table.Range.Borders.InsideLineStyle = aTable.Range.Borders.InsideLineStyle;
                                    table.Range.Borders.InsideColor = aTable.Range.Borders.InsideColor;
                                    table.Range.Borders.OutsideColor = aTable.Range.Borders.OutsideColor;

                                    for (int i = 1; i <= aTable.Columns.Count; i++)
                                    {
                                        string s = aTable.Cell(1, i).Range.Text.Trim();
                                        table.Cell(1, i).Range.Text = s.Trim(' ', '\r', '\a');


                                        for (int j = 1; j <= table.Rows.Count; j++)
                                        {
                                            table.Cell(j, i).Range.Font = aTable.Cell(j, i).Range.Font;
                                        }

                                        table.Cell(1, i).Range.Shading.BackgroundPatternColor = aTable.Cell(1, i).Range.Shading.BackgroundPatternColor;
                                    }

                                    try
                                    {
                                        table.Rows[1].HeadingFormat = -1;
                                    }
                                    catch (Exception E)
                                    {
                                        MessageBox.Show(E.Message);
                                    }

                                    row = table.Rows[2];

                                    counter = 1;

                                }
                                else
                                {
                                    if (counter == 0)
                                    {
                                        row = table.Rows[2];
                                        counter++;
                                    }
                                    else
                                        row = table.Rows.Add(o);
                                }

                                if (chapter != null)
                                {
                                    chapterText = "Chapter " + chnum.ToString();
                                    chapterNumber = chnum;

                                    chapter.Text = chapterText;


                                    chapterCounter = 1;
                                }
                            }
                            else
                            {
                                if (counter == 0)
                                {
                                    row = table.Rows[2];
                                    counter++;
                                }
                                else
                                    row = table.Rows.Add(o);
                            }

                            row.Cells[qnColumn].Range.Text = obsReader["QUESTION_NUMBER"].ToString();
                            
                            string obstext = obsReader["OBSERVATION"].ToString().Trim(' ', '\t', '\r', '\a', '\n');
                            row.Cells[obsColumn].Range.Text = obstext;
                            row.Cells[obsColumn].Range.Font.Bold = -1;
                            
                            //row.Cells[obsColumn].Range.InsertParagraphAfter();
                            row.Cells[obsColumn].Range.InsertParagraphAfter();
                            Word.Range obsCellRange = row.Cells[obsColumn].Range.Paragraphs[row.Cells[obsColumn].Range.Paragraphs.Count].Range;
                            DateTime inspDate = Convert.ToDateTime(obsReader["INSPECTION_DATE"]);
                            obsCellRange.Text = "(" + inspDate.ToShortDateString() + ")";
                            obsCellRange.Font.Name = "Times New Roman";
                            obsCellRange.Font.Size = 10;
                            obsCellRange.Font.Italic = -1;
                            obsCellRange.Font.Bold = 0;
                            

                            string comText = obsReader["OPERATOR_COMMENTS"].ToString().Trim(' ', '\t', '\r', '\a', '\n');
                            row.Cells[comColumn].Range.Text = comText;

                            //Word.Range range = row.Range;
                            //range.Text = obs;

                        }
                    }
                    else
                    {
                        Word.Row row = table.Rows[1];
                        row.Range.Text = "There is no observation";
                    }

                    obsReader.Close();
                }

                //((Word._Application)wordapp).Visible = true;

                bool saveSuccessfull = false;

                if (actionCode == 1)
                {
                    //Save Word document as PDF
                    try
                    {
                        switch (fileExt)
                        {
                            case ".docx":
                                wordDocument.SaveAs2(fileName, Word.WdSaveFormat.wdFormatDocumentDefault);
                                saveSuccessfull = true;
                                break;
                            case ".doc":
                                wordDocument.SaveAs2(fileName, Word.WdSaveFormat.wdFormatDocument);
                                saveSuccessfull = true;
                                break;
                            case ".pdf":
                                wordDocument.SaveAs2(fileName, Word.WdSaveFormat.wdFormatPDF);
                                saveSuccessfull = true;
                                break;
                        }

                        Object saveChanges = Word.WdSaveOptions.wdDoNotSaveChanges;
                        Object originalFormat = Word.WdOriginalFormat.wdOriginalDocumentFormat;
                        Object routeDocument = Type.Missing;

                        ((Word._Application)wordapp).Quit(ref saveChanges, ref originalFormat, ref routeDocument);

                    }
                    catch (Exception E)
                    {
                        MessageBox.Show("Error during saving file " + fileName + "\nError message: \n" + E.Message, "External Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (showFile)
                    ((Word._Application)wordapp).Visible = true;

                if (saveSuccessfull)
                {
                    this.Cursor = Cursors.Default;
                    return fileName;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    return "";
                }

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //((Word._Application)wordapp).Visible = true;
                
                this.Cursor = Cursors.Default;

                return "";
            }

        }

        private string CreateDefaultSummary(string VesselName,OleDbDataReader obsReader, int actionCode, string fileName, string fileExt, bool showFile)
        {
            //Create summary in default format
            Word.Document wordDocument;

            //Create Word document
            Word.Application wordapp = new Word.Application();

            //Show Word window
            wordapp.Visible = false;


            try
            {
                Object template = Type.Missing;
                Object newTemplate = false;
                Object documentType = Word.WdNewDocumentType.wdNewBlankDocument;
                Object visible = true;

                //Создаем документ
                wordDocument = wordapp.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible);

                wordDocument.PageSetup.LeftMargin = 42;
                wordDocument.PageSetup.RightMargin = 42;
                wordDocument.PageSetup.TopMargin = 42;
                wordDocument.PageSetup.BottomMargin = 42;

                Word.Paragraphs wordParagraphs;
                Word.Paragraph wordParagraph;

                wordParagraphs = wordDocument.Paragraphs;

                //wordParagraphs.Add();
                wordParagraph = wordParagraphs[wordParagraphs.Count];
                wordParagraph.Range.Font.Bold = 0;
                wordParagraph.Range.Font.Name = "Calibri Light";
                wordParagraph.Range.Font.Size = 28;
                wordParagraph.Range.Text = VesselName;

                wordParagraphs.Add();
                wordParagraph = wordParagraphs[wordParagraphs.Count];
                wordParagraph.Range.Font.Bold = 0;
                wordParagraph.Range.Font.Name = "Calibri Light";
                wordParagraph.Range.Font.Size = 16;
                wordParagraph.Range.Font.ColorIndex = Word.WdColorIndex.wdBlue;
                wordParagraph.Range.Text = "History of SIRE Observations";

                while (obsReader.Read())
                {
                    //Question number and text of observation

                    DateTime inspDate = Convert.ToDateTime(obsReader["INSPECTION_DATE"].ToString());

                    string obsText = obsReader["QUESTION_NUMBER"].ToString() + " " + obsReader["OBSERVATION"].ToString() +
                        " (" + inspDate.Date.ToShortDateString() + ")";

                    wordParagraphs.Add();
                    wordParagraph = wordParagraphs[wordParagraphs.Count];
                    wordParagraph.Range.Font.Bold = 1;
                    wordParagraph.Range.Font.Name = "Calibri";
                    wordParagraph.Range.Font.Size = 11;
                    wordParagraph.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    wordParagraph.Range.ParagraphFormat.SpaceAfter = 0;
                    wordParagraph.Range.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle;
                    wordParagraph.Range.Font.ColorIndex = Word.WdColorIndex.wdBlack;

                    wordParagraph.Range.Text = obsText;

                    string obsComments = obsReader["OPERATOR_COMMENTS"].ToString();

                    wordParagraphs.Add();
                    wordParagraph = wordParagraphs[wordParagraphs.Count];
                    wordParagraph.Range.Font.Bold = 0;
                    wordParagraph.Range.Font.Name = "Calibri Light";
                    wordParagraph.Range.Font.Size = 11;
                    wordParagraph.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    wordParagraph.Range.ParagraphFormat.SpaceAfter = 0;
                    wordParagraph.Range.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle;

                    wordParagraph.Range.Text = obsComments;

                    wordParagraphs.Add();

                }

                bool saveSuccessfull = false;

                if (actionCode == 1)
                {
                    //Save Word document as PDF
                    try
                    {
                        switch (fileExt)
                        {
                            case ".docx":
                                wordDocument.SaveAs2(fileName, Word.WdSaveFormat.wdFormatDocumentDefault);
                                saveSuccessfull = true;
                                break;
                            case ".doc":
                                wordDocument.SaveAs2(fileName, Word.WdSaveFormat.wdFormatDocument);
                                saveSuccessfull = true;
                                break;
                            case ".pdf":
                                wordDocument.SaveAs2(fileName, Word.WdSaveFormat.wdFormatPDF);
                                saveSuccessfull = true;
                                break;
                        }

                        Object saveChanges = Word.WdSaveOptions.wdDoNotSaveChanges;
                        Object originalFormat = Word.WdOriginalFormat.wdOriginalDocumentFormat;
                        Object routeDocument = Type.Missing;

                        ((Word._Application)wordapp).Quit(ref saveChanges, ref originalFormat, ref routeDocument);

                    }
                    catch (Exception E)
                    {
                        MessageBox.Show("Error during saving file " + fileName + "\nError message: \n" + E.Message, "External Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                //Object saveChanges = Word.WdSaveOptions.wdDoNotSaveChanges;
                //Object originalFormat = Word.WdOriginalFormat.wdOriginalDocumentFormat;
                //Object routeDocument = Type.Missing;

                //((Word._Application)wordapp).Quit(ref saveChanges, ref originalFormat, ref routeDocument);

                if (showFile)
                    ((Word._Application)wordapp).Visible = true;

                if (saveSuccessfull)
                    return fileName;
                else
                    return "";
            }
            finally
            {
                wordapp = null;
                this.Cursor = Cursors.Default;
            }
        }
        private void btnSummarySettings_Click(object sender, EventArgs e)
        {
            //Open summary settings form
            string iniFileName = Path.Combine(MainForm.serviceFolder, "Settings_Common.ini");
            IniFile iniFile = new IniFile(iniFileName);
            
            //Use global section for bulletin settings
            string section = "VesselSummary";

            FrmVesselSummarySettings form = new FrmVesselSummarySettings(this.Font, this.Icon);

            form.actionCode = iniFile.ReadInteger(section, "SummaryAction", 0);
            form.messageSubject = iniFile.ReadString(section, "MessageSubject", "");
            form.messageText = MainForm.LineToText(iniFile.ReadString(section, "MessageText", ""));
            form.showFile = iniFile.ReadBoolean(section, "ShowFile", true);
            form.fileNameTemplate = iniFile.ReadString(section, "FileNAmeTemplate", "");
            form.fileFormatText = iniFile.ReadString(section, "FileFormatExt", "Microsoft Word (.docx)");
            form.ccAddresses = iniFile.ReadString(section, "CCAddresses", "");
            form.sendToFleet = iniFile.ReadBoolean(section, "SendCopyToFleet", true);
            form.useTemplate = iniFile.ReadBoolean(section, "UseTemplate", false);
            form.templateFile = iniFile.ReadString(section, "TemplateFile", "");

            var rslt = form.ShowDialog();

            if (rslt==DialogResult.OK)
            {
                //Save settings in ini file

                iniFile.Write(section, "SummaryAction", form.actionCode);

                FormatSummaryButton(form.actionCode);

                iniFile.Write(section, "MessageSubject", form.messageSubject);
                iniFile.Write(section, "MessageText", MainForm.TextToLine(form.messageText));
                iniFile.Write(section, "ShowFile", form.showFile);
                iniFile.Write(section, "FileNameTemplate", form.fileNameTemplate);
                iniFile.Write(section, "FileFormatExt", form.fileFormatText);
                iniFile.Write(section, "CCAddresses", form.ccAddresses);
                iniFile.Write(section, "SendCopyToFleet", form.sendToFleet);
                iniFile.Write(section, "UseTemplate", form.useTemplate);
                iniFile.Write(section, "TemplateFile", form.templateFile);
            }
        }

        private void FormatSummaryButton(int code)
        {
            if (code == 0)
            {
                btnSummary.Text = "Create summary";
                btnSummary.Image = VIM.Properties.Resources.Select_All_Clear;
            }
            else
            {
                btnSummary.Text = "Create and send summary";
                btnSummary.Image = VIM.Properties.Resources.mail;
            }
        }

        private void VesselsForm_Shown(object sender, EventArgs e)
        {
            string iniFileName = Path.Combine(MainForm.serviceFolder, "Settings_Common.ini");
            IniFile iniFile = new IniFile(iniFileName);

            //Use global section for bulletin settings
            string section = "VesselSummary";

            int actionCode = iniFile.ReadInteger(section, "SummaryAction", 0);

            FormatSummaryButton(actionCode);

        }

        private void VesselsForm_Load(object sender, EventArgs e)
        {
            //Restore form settings
            // Upgrade?
            if (Properties.Settings.Default.VesselsFormSize.Width == 0) 
                Properties.Settings.Default.Upgrade();

            if (Properties.Settings.Default.VesselsFormSize.Width == 0 || Properties.Settings.Default.VesselsFormSize.Height == 0)
            {
                // first start
                // optional: add default values
            }
            else
            {
                this.WindowState = Properties.Settings.Default.VesselsFormState;

                // we don't want a minimized window at startup
                if (this.WindowState == FormWindowState.Minimized) this.WindowState = FormWindowState.Normal;

                this.Location = Properties.Settings.Default.VesselsFormLocation;
                this.Size = Properties.Settings.Default.VesselsFormSize;
            }

        }

        private void VesselsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Store form settings
            Properties.Settings.Default.VesselsFormState = this.WindowState;
            
            if (this.WindowState == FormWindowState.Normal)
            {
                // save location and size if the state is normal
                Properties.Settings.Default.VesselsFormLocation = this.Location;
                Properties.Settings.Default.VesselsFormSize = this.Size;
            }
            else
            {
                // save the RestoreBounds if the form is minimized or maximized!
                Properties.Settings.Default.VesselsFormLocation = this.RestoreBounds.Location;
                Properties.Settings.Default.VesselsFormSize = this.RestoreBounds.Size;
            }

            // don't forget to save the settings
            Properties.Settings.Default.Save();
        }

        private void chbShowHidden_CheckedChanged(object sender, EventArgs e)
        {
            FillVessels(chbShowHidden.Checked);
        }

        private void DeleteVessel(Guid vesselGuid)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "delete from VESSELS\n" +
                "where VESSEL_GUID=" + MainForm.GuidToStr(vesselGuid);

            if (MainForm.cmdExecute(cmd)>=0)
            {
                DS.Tables["FULL_VESSEL_LIST"].Clear();
                vesselsAdapter.Fill(DS, "FULL_VESSEL_LIST");
                CountRecords();
            }
            else
            {
                MessageBox.Show("Failed to delete vessel", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void adgvVessels_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            editVessel();
        }

        private void adgvVessels_FilterStringChanged(object sender, EventArgs e)
        {
            bsVessels.Filter = adgvVessels.FilterString;
            CountRecords();
        }

        private void adgvVessels_SortStringChanged(object sender, EventArgs e)
        {
            bsVessels.Sort = adgvVessels.SortString;
        }

        private void CountRecords()
        {
            lblRecs.Text = adgvVessels.Rows.Count.ToString();
        }
    }
}
