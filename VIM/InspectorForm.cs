using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;

namespace VIM
{

    public partial class InspectorForm : Form
    {
        private string photoFolder="";
        private DataSet DS;
        private OleDbConnection connection;
        private Guid inspectorGuid;
        private int mode = 0;
        private int section1Color;
        private int section2Color;
        private int section3Color;
        private int section4Color;
        private int headerColor;
        private int columnWidth;
        private int formMaximize;
        private string filesFolder = "";

        public string inspectorName
        {
            get { return tbFullName.Text; }
            set { tbFullName.Text = value; }
        }

        public bool inspectorUnfavourable
        {
            get { return chbUnfavourable.Checked; }
            set { chbUnfavourable.Checked = value; }
        }

        public string inspectorNotes
        {
            get { return tbNotes.Text; }
            set { tbNotes.Text = value; }
        }

        public string inspectorPhoto
        {
            get { return labelPhoto.Text; }
            set { labelPhoto.Text = value; }
        }

        public string inspectorBackground
        {
            get { return tbBackground.Text; }
            set { tbBackground.Text = value; }
        }

        public string linkedIn
        {
            get { return tbLinkedIn.Text.Trim(); }
            set { tbLinkedIn.Text = value; }
        }

        public DateTime profileDate
        {
            get { return dtProfileDate.Value; }
            set { SetProfileDate(value); }
        }

        public InspectorForm(Guid mainInspectorGuid, int mainMode)
        {
            this.Cursor = Cursors.WaitCursor;

            this.Icon = MainForm.mainIcon;

            DS = MainForm.DS;
            connection = MainForm.connection;
            inspectorGuid = mainInspectorGuid;
            mode = mainMode;

            this.Font = MainForm.mainFont;

            InitializeComponent();

            getOptionColors();

            if (formMaximize == 1)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;

            //if (inspectorGuid < 0)
            //{
            //    tbNotes.ReadOnly = true;
            //}

            photoFolder = Directory.GetCurrentDirectory();

            string defaultPhotoFolder = MainForm.GetOptionValue(101);

            if ((defaultPhotoFolder==null) || (defaultPhotoFolder.Length==0))
            {
                defaultPhotoFolder = "Photo";
                MainForm.SetOptionValue(101, defaultPhotoFolder);
            }

            photoFolder=photoFolder+"\\"+defaultPhotoFolder;

            if (!Directory.Exists(photoFolder))
            {
                Directory.CreateDirectory(photoFolder);
            }

            string defaultInspectorFilesFolder = MainForm.GetOptionValue(102);

            if ((defaultInspectorFilesFolder==null) || (defaultInspectorFilesFolder.Length==0))
            {
                defaultInspectorFilesFolder = "Related";
                MainForm.SetOptionValue(102, defaultInspectorFilesFolder);
            }

            filesFolder = Directory.GetCurrentDirectory() + "\\"+defaultInspectorFilesFolder;

            if (!Directory.Exists(filesFolder))
                Directory.CreateDirectory(filesFolder);

            fillProfile();

            //fillInspections();

            //fillScoring();

            newFillScoring();

            //updateInspectorFiles();

            tabControl1.TabIndex = 0;

            this.Cursor = Cursors.Default;

            ProtectFields(MainForm.isPowerUser);

        }

        private void ProtectFields(bool status)
        {
            tbFullName.ReadOnly = !status;
            tbBackground.ReadOnly = !status;
            tbNotes.ReadOnly = !status;
            tbLinkedIn.ReadOnly = !status;

            chbUnfavourable.Enabled = status;

            btnLoadPhoto.Enabled = status;
            btnLoadProfile.Enabled = status;
            btnNew.Enabled = status;
            btnCheck.Enabled = status;
            btnDelete.Enabled = status;
            btnDeletePhoto.Enabled = status;
            btnOpen.Enabled = status;

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

        private void fillInspections()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select REPORTS.REPORT_CODE, VESSELS.VESSEL_NAME, INSPECTION_DATE, INSPECTION_PORT, Q1.OBS_NUMBER \n" +
                "from (REPORTS left join VESSELS \n" +
                "on REPORTS.VESSEL_GUID=VESSELS.VESSEL_GUID) \n"+
                "left join\n" +
                "(select REPORT_CODE, COUNT(OBSERVATION) as OBS_NUMBER \n" +
                "from REPORT_ITEMS \n" +
                "where LEN(OBSERVATION)>0 \n" +
                "group by REPORT_CODE) as Q1 \n" +
                "on REPORTS.REPORT_CODE=Q1.REPORT_CODE \n" +
                "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + "\n" +
                "order by INSPECTION_DATE";

            OleDbDataAdapter inspections = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("INSPECTOR_INSPECTIONS"))
                DS.Tables["INSPECTOR_INSPECTIONS"].Clear();

            inspections.Fill(DS, "INSPECTOR_INSPECTIONS");

            dgvInspections.DataSource = DS;
            dgvInspections.DataMember = "INSPECTOR_INSPECTIONS";
            dgvInspections.AutoGenerateColumns = true;

            dgvInspections.Columns["REPORT_CODE"].HeaderText = "Report code";
            dgvInspections.Columns["VESSEL_NAME"].HeaderText = "Vessel name";
            dgvInspections.Columns["INSPECTION_DATE"].HeaderText = "Date of inspection";
            dgvInspections.Columns["INSPECTION_PORT"].HeaderText = "Port of inspection";
            dgvInspections.Columns["OBS_NUMBER"].HeaderText = "Observations";

        }

        private void fillProfile()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

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

            dgvProfile.DataSource = DS;
            dgvProfile.DataMember = "UNIFIED_QUESTIONS";
            dgvProfile.AutoGenerateColumns = true;

            dgvProfile.Columns["ID"].Visible = false;

            dgvProfile.Columns["SECTION_ID"].Visible = false;

            dgvProfile.Columns["QUESTION_ID"].Visible = false;

            dgvProfile.Columns["QUESTION"].Visible = true;
            //dataGridView1.Columns["QUESTION"].FillWeight = 70;
            dgvProfile.Columns["QUESTION"].HeaderText = "Questions";
            dgvProfile.Columns["QUESTION"].ReadOnly = true;
            //dataGridView1.Columns["QUESTION"].Width = 300;
            dgvProfile.Columns["QUESTION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //dataGridView1.Columns["QUESTION"].Resizable = DataGridViewTriState.False;

            dgvProfile.Columns["COMMENT_ID"].Visible = false;

            dgvProfile.Columns["COMMENTS"].Visible = true;
            dgvProfile.Columns["COMMENTS"].HeaderText = "Comments";
            dgvProfile.Columns["COMMENTS"].ReadOnly = false;
            dgvProfile.Columns["COMMENTS"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dgvProfile.Columns["COLOR"].Visible = false;

            setGridColor();

        }
        private void updateInspectorFiles()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);
 
            cmd.CommandText =
                "select * \n" +
                "from INSPECTOR_FILES \n" +
                "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + "\n" +
                "order by FILE_NAME";

            OleDbDataAdapter files = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("INSPECTOR_FILES"))
            {
                DS.Tables["INSPECTOR_FILES"].Clear();
                DS.Tables["INSPECTOR_FILES"].Columns.Clear();
            }

            files.Fill(DS, "INSPECTOR_FILES");

            dgvInspectorFiles.DataSource = DS;
            dgvInspectorFiles.DataMember = "INSPECTOR_FILES";
            dgvInspectorFiles.AutoGenerateColumns = true;

            dgvInspectorFiles.Columns["INSPECTOR_FILE_ID"].Visible = false;
            dgvInspectorFiles.Columns["INSPECTOR_GUID"].Visible = false;

            dgvInspectorFiles.Columns["FILE_NAME"].Visible = true;
            dgvInspectorFiles.Columns["FILE_NAME"].HeaderText = "File name";
            dgvInspectorFiles.Columns["FILE_NAME"].FillWeight = 95;

            dgvInspectorFiles.Columns["FILE_EXISTS"].Visible = true;
            dgvInspectorFiles.Columns["FILE_EXISTS"].HeaderText = "Exists";
            dgvInspectorFiles.Columns["FILE_EXISTS"].FillWeight = 5;

        }

        private void getOptionColors()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select * from OPTIONS";

            OleDbDataReader options = cmd.ExecuteReader();

            section1Color = 13434828;
            section2Color = 13434879;
            section3Color = 16764159;
            section4Color = 16777164;
            headerColor = 15921906;
            columnWidth = 300;
            formMaximize = 1;

            if (options.HasRows)
            {
                while (options.Read())
                {
                    string optionValue = options[2].ToString();

                    switch (options[1].ToString())
                    {
                        case "9":
                            if (optionValue == "0")
                                formMaximize = 0;
                            else
                                formMaximize = 1;
                            break;
                        case "10":
                            section1Color=Convert.ToInt32(optionValue);
                            break;
                        case "11":
                            section2Color=Convert.ToInt32(optionValue);
                            break;
                        case "12":
                            section3Color=Convert.ToInt32(optionValue);
                            break;
                        case "13":
                            section4Color=Convert.ToInt32(optionValue);
                            break;
                        case "14":
                            headerColor=Convert.ToInt32(optionValue);
                            break;
                        case "15":
                            columnWidth=Convert.ToInt32(optionValue);
                            break;
                    }
                    
                }
            }
            options.Close();
        }

        private void setGridColor()
        {
            for (int i = 0; i < dgvProfile.Rows.Count; i++)
            {
                Color cellColor=Color.FromArgb(255,255,255);

                switch (dgvProfile.Rows[i].Cells["SECTION_ID"].Value.ToString())
                {
                    case "1":
                        cellColor = getColor(section1Color);
                        break;
                    case "2":
                        cellColor=getColor(section2Color);
                        break;
                    case "3":
                        cellColor = getColor(section3Color);
                        break;
                    case "4":
                        cellColor = getColor(section4Color);
                        break;
                }

                if (dgvProfile.Rows[i].Cells["QUESTION_ID"].Value.ToString() == "0")
                {
                    dgvProfile.Rows[i].Cells["COMMENTS"].ReadOnly = true;
                    dgvProfile.Rows[i].Cells["QUESTION"].Style.BackColor = getColor(headerColor);
                    dgvProfile.Rows[i].Cells["COMMENTS"].Style.BackColor = getColor(headerColor);


                    //if (dataGridView1.Rows[i].Cells["QUESTION"].Style.Font != null)
                    {
                        string fontFamily = dgvProfile.DefaultCellStyle.Font.FontFamily.ToString();
                        float fontSize = dgvProfile.DefaultCellStyle.Font.Size;
                        FontStyle fontStyle = dgvProfile.DefaultCellStyle.Font.Style;
                        fontStyle |= FontStyle.Bold;

                        Font cellFont = new Font(fontFamily, fontSize, fontStyle);


                        dgvProfile.Rows[i].Cells["QUESTION"].Style.Font = cellFont;
                    }
                }
                else
                {
                    dgvProfile.Rows[i].Cells["QUESTION"].Style.BackColor = cellColor;
                }
            }
            dgvProfile.Columns["QUESTION"].Width = columnWidth;
            //dataGridView1.Update();
        }

        private Color getColor(int fullColor)
        {
            int red;
            int green;
            int blue;

            string hexOutput = fullColor.ToString("X8");

            blue=Convert.ToInt32(hexOutput.Substring(2,2), 16);
            green=Convert.ToInt32(hexOutput.Substring(4,2), 16);
            red=Convert.ToInt32(hexOutput.Substring(6,2), 16);

            return Color.FromArgb(red, green, blue);
        }

        private void bthLoadPhoto_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = photoFolder;
            openFileDialog1.FileName = "";
            openFileDialog1.Multiselect = false;

            openFileDialog1.Filter = "JPEG files(*.jpg)|*.jpg|Bitmap files (*.bmp)|*.bmp|PNG files (*.png)|*.png";

            var rslt=openFileDialog1.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;

                //Проверяем, что файл находится в папке Photo

                string openPath = Path.GetDirectoryName(fileName);

                if (String.Compare(photoFolder, openPath, true) != 0)
                {
                    string folderName = photoFolder.Substring(Directory.GetCurrentDirectory().Length + 1);

                    MessageBox.Show("Files with photo should be located in \""+folderName+"\" folder of application directory.\n" +
                        "Copy file to \""+folderName+"\" folder and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //pictureBox1.Image = Image.FromFile(fileName);

                labelPhoto.Text = Path.GetFileName(fileName);

                ShowPicture(labelPhoto.Text);
            }
        }

        private void bthDeletePhoto_Click(object sender, EventArgs e)
        {
            labelPhoto.Text = "";
            pictureBox1.Image = null;
        }

        private void ShowPicture(string fileName)
        {
            string photoFile = photoFolder + "\\" + fileName;

            if (File.Exists(photoFile))
            {
                try
                {
                    pictureBox1.Image = Image.FromFile(photoFile);
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message,"Open file error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        private void InspectorForm_Activated(object sender, EventArgs e)
        {
            ShowPicture(labelPhoto.Text);
        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }

        private void dgvProfile_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (inspectorGuid == MainForm.zeroGuid)
                return;

            if (dgvProfile.CurrentCell.ColumnIndex != dgvProfile.CurrentRow.Cells["COMMENTS"].ColumnIndex)
            {
                if (MainForm.isPowerUser)
                    updateColumnSettings();
                
                return;
            }

            if (dgvProfile.CurrentRow.Cells["QUESTION_ID"].Value.ToString() == "0")
                return;

            CommentsForm cf = new CommentsForm(this.Font);

            cf.tbItem.Text = dgvProfile.CurrentRow.Cells["QUESTION"].Value.ToString();
            cf.tbComments.Text = dgvProfile.CurrentRow.Cells["COMMENTS"].Value.ToString();

            var rslt = cf.ShowDialog();

            if ((rslt == DialogResult.OK) && (MainForm.isPowerUser))
            {
                string sectionID=dgvProfile.CurrentRow.Cells["SECTION_ID"].Value.ToString();
                string questionID=dgvProfile.CurrentRow.Cells["QUESTION_ID"].Value.ToString();

                SaveComments(cf.tbComments.Text, sectionID, questionID, dgvProfile.CurrentRow.Index);
            }
        }

        public void updateColumnSettings()
        {
            ColumnSettingForm csf = new ColumnSettingForm(this.Font, this.Icon);

            csf.textBox1.BackColor = getColor(section1Color);
            csf.textBox2.BackColor = getColor(section2Color);
            csf.textBox3.BackColor = getColor(section3Color);
            csf.textBox4.BackColor = getColor(section4Color);
            csf.textBox5.BackColor = getColor(headerColor);
            csf.numericUpDown1.Value = columnWidth;

            var rslt = csf.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                //Save new settings
                section1Color = colorToInt(csf.textBox1.BackColor);
                saveOption(10, section1Color.ToString());

                section2Color = colorToInt(csf.textBox2.BackColor);
                saveOption(11, section2Color.ToString());

                section3Color = colorToInt(csf.textBox3.BackColor);
                saveOption(12, section3Color.ToString());

                section4Color = colorToInt(csf.textBox4.BackColor);
                saveOption(13, section4Color.ToString());

                headerColor = colorToInt(csf.textBox5.BackColor);
                saveOption(14, headerColor.ToString());

                columnWidth = (int)csf.numericUpDown1.Value;
                saveOption(15, columnWidth.ToString());

                setGridColor();
            }
        }

        public int colorToInt(Color color)
        {
            int result = 0;

            result = color.B * 256 * 256 + color.G * 256 + color.R;

            return result;
        }

        public void saveOption(int tag, string value)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);
            cmd.CommandText =
                "select Count(TAG) from OPTIONS where TAG="+tag.ToString();

            int recs = (int)cmd.ExecuteScalar();

            if (recs == 0)
            {
                cmd.CommandText =
                    "insert into OPTIONS (TAG,STR_VALUE) \n" +
                    "values("+tag.ToString()+",'" + value + "')";
                cmd.ExecuteNonQuery();
            }
            else
            {
                cmd.CommandText =
                    "update OPTIONS set \n" +
                    "STR_VALUE='" + value + "' \n" +
                    "where TAG="+tag.ToString();
                cmd.ExecuteNonQuery();
            }
        }

        public string StrToSQLStr(string Text)
        {
            return Text.Replace("'", "''");
        }


        private void InspectorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Check for double name

            if (this.DialogResult == DialogResult.Cancel) return;

            OleDbCommand cmd = new OleDbCommand("", connection);

            if (mode == 0) //Edit
            {
                cmd.CommandText =
                    "select Count(INSPECTOR_GUID) as recs \n" +
                    "from INSPECTORS \n" +
                    "where INSPECTOR_NAME='" + StrToSQLStr(tbFullName.Text) + "' \n" +
                    "and INSPECTOR_GUID<>" + MainForm.GuidToStr(inspectorGuid);

                int recs = (int)cmd.ExecuteScalar();

                if (recs > 0)
                {
                    string msg = "There is an inspector with the same name.";
                    System.Windows.Forms.MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
            else
            {
                cmd.CommandText=
                    "select Count(INSPECTOR_NAME) as recs \n"+
                    "from INSPECTORS \n"+
                    "where INSPECTOR_NAME='"+StrToSQLStr(tbFullName.Text)+"'";

                int recs = (int)cmd.ExecuteScalar();

                if (recs > 0)
                {
                    string msg = "There is an inspector with the same name.";
                    System.Windows.Forms.MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }

            //Проверяем нужно ли сохранять комментарии
            if (inspectorGuid != MainForm.zeroGuid)
            {
                for (int i = 0; i < dgvProfile.Rows.Count; i++)
                {
                    string comments = dgvProfile.Rows[i].Cells["COMMENTS"].Value.ToString();

                    if (comments.Length > 0)
                    {
                        //Имеется комментарий

                        if (dgvProfile.Rows[i].Cells["COMMENT_ID"].Value.ToString() == "")
                        {
                            //Проверяем наличие записи в базе данных
                            cmd.CommandText =
                                "select count(INSPECTOR_GUID) as RowCount \n" +
                                "from UNIFIED_COMMENTS \n" +
                                "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + " \n" +
                                "and SECTION_ID=" + dgvProfile.Rows[i].Cells["SECTION_ID"].Value.ToString() + "\n" +
                                "and QUESTION_ID=" + dgvProfile.Rows[i].Cells["QUESTION_ID"].Value.ToString();

                            int rowCount = (int)cmd.ExecuteScalar();

                            if (rowCount == 0)
                            {
                                //Вставляем новую запись в таблицу комментариев
                                cmd.CommandText =
                                    "insert into UNIFIED_COMMENTS (INSPECTOR_GUID, SECTION_ID, QUESTION_ID, COMMENTS)\n" +
                                    "values (" + MainForm.GuidToStr(inspectorGuid) + "," +
                                               dgvProfile.Rows[i].Cells["SECTION_ID"].Value.ToString() + "," +
                                               dgvProfile.Rows[i].Cells["QUESTION_ID"].Value.ToString() + ",'" +
                                               StrToSQLStr(comments) + "')";
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                cmd.CommandText =
                                    "update UNIFIED_COMMENTS set \n" +
                                    "COMMENTS='" + StrToSQLStr(comments) + "' \n" +
                                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + " \n" +
                                    "and SECTION_ID=" + dgvProfile.Rows[i].Cells["SECTION_ID"].Value.ToString() + "\n" +
                                    "and QUESTION_ID=" + dgvProfile.Rows[i].Cells["QUESTION_ID"].Value.ToString();
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            cmd.CommandText =
                                "update UNIFIED_COMMENTS set \n" +
                                "COMMENTS='" + StrToSQLStr(comments) + "' \n" +
                                "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + " \n" +
                                "and SECTION_ID=" + dgvProfile.Rows[i].Cells["SECTION_ID"].Value.ToString() + "\n" +
                                "and QUESTION_ID=" + dgvProfile.Rows[i].Cells["QUESTION_ID"].Value.ToString();
                            cmd.ExecuteNonQuery();
                        }


                    }
                    else
                    {
                        if (dgvProfile.Rows[i].Cells["COMMENT_ID"].Value.ToString() != "")
                        {
                            cmd.CommandText =
                                "delete from UNIFIED_COMMENTS \n" +
                                "where COMMENT_ID=" + dgvProfile.Rows[i].Cells["COMMENT_ID"].Value.ToString();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            else
            {
                cmd.CommandText =
                    "delete from UNIFIED_COMMENTS \n" +
                    "where INSPECTOR_GUID is Null";
                cmd.ExecuteNonQuery();
            }

            if (this.WindowState == FormWindowState.Maximized)
                saveOption(9, "1");
            else
                saveOption(9, "0");


        }

        private void InspectorForm_Shown(object sender, EventArgs e)
        {
            setGridColor();
        }

        private void newFillScoring()
        {
            string tempTableName = MainForm.BuildInspectorScoring(inspectorGuid);

            if (tempTableName.Length==0)
            {
                MessageBox.Show("Failed to build inspector scoring", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            OleDbCommand cmd = new OleDbCommand("", connection);

            int obsCount = 0;

            chart1.Series[0].ChartType = SeriesChartType.Column;
            chart1.Series[0].IsXValueIndexed = true;

            cmd.CommandText =
                "select Sum(OBS_COUNT) as OBS_SUM \n" +
                "from [" + tempTableName + "] ";

            obsCount = Convert.ToInt32(cmd.ExecuteScalar());


            cmd.CommandText =
                "select * \n" +
                "from [" + tempTableName + "] \n" +
                "order by CInt(CHAPTER)";

            OleDbDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int i = Convert.ToInt32(reader["CHAPTER"]);
                    int obs = Convert.ToInt32(reader["OBS_COUNT"]);

                    chart1.Series[0].Points.Add(obs);

                    if (obs == 0)
                        chart1.Series[0].Points[i - 1].Label = "";
                    else
                        chart1.Series[0].Points[i - 1].Label = obs.ToString();
                }
            }

            reader.Close();

            cmd.CommandText =
                "select Count(INSPECTOR_GUID) as repCount \n" +
                "from REPORTS \n" +
                "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid);

            int inspCount = (int)cmd.ExecuteScalar();

            lblInspections.Text = inspCount.ToString();
            lblObservations.Text = obsCount.ToString();

            double aver = 0.00;

            if (inspCount>0)
            {
                aver = Math.Round((float)obsCount / (float)inspCount,2);
            }

            lblAverage.Text = aver.ToString();


            cmd.CommandText =
                "select * from [" + tempTableName + "]";

            OleDbDataAdapter scoring = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("SCORING"))
            {
                DS.Tables["SCORING"].Clear();
                DS.Tables["SCORING"].Columns.Clear();
            }

            scoring.Fill(DS, "SCORING");

            dgvScoring.AutoGenerateColumns = true;
            dgvScoring.DataSource = DS;
            dgvScoring.DataMember = "SCORING";

            dgvScoring.Columns["ID"].Visible = false;
            
            dgvScoring.Columns["CHAPTER"].Visible = true;
            dgvScoring.Columns["CHAPTER"].HeaderText = "Chapter";
            dgvScoring.Columns["CHAPTER"].FillWeight = 50;

            dgvScoring.Columns["OBS_COUNT"].Visible = true;
            dgvScoring.Columns["OBS_COUNT"].HeaderText = "Number";
            dgvScoring.Columns["OBS_COUNT"].FillWeight = 50;

            MainForm.tempTableDrop(tempTableName);

            
        }

        private void fillScoring()
        {
            OleDbCommand cmd=new OleDbCommand("",connection);

            cmd.CommandText=
                "select * from ( \n"+
                "select \n"+
                "INSPECTORS.INSPECTOR_GUID,\n"+
                "SUM(QCOUNT1) as [Ch 1],  \n"+
                "SUM(QCOUNT2) as [Ch 2],  \n"+
                "SUM(QCOUNT3) as [Ch 3],  \n"+
                "SUM(QCOUNT4) as [Ch 4], \n"+
                "SUM(QCOUNT5) as [Ch 5], \n"+
                "SUM(QCOUNT6) as [Ch 6], \n"+
                "SUM(QCOUNT7) as [Ch 7], \n"+
                "SUM(QCOUNT8) as [Ch 8], \n"+
                "SUM(QCOUNT9) as [Ch 9], \n"+
                "SUM(QCOUNT10) as [Ch 10], \n"+
                "SUM(QCOUNT11) as [Ch 11], \n"+
                "SUM(QCOUNT12) as [Ch 12], \n"+
                "SUM(QCOUNT13) as [Ch 13], \n"+
                "Left(CStr(QCOUNT/ICOUNT),4) as [Average] \n" +
                "from \n"+
                "(((((((((((((((INSPECTORS left join REPORTS \n"+
                "on INSPECTORS.INSPECTOR_GUID=REPORTS.INSPECTOR_GUID\n"+
                ") \n"+
                "left join \n"+
                "(select REPORTS.INSPECTOR_GUID, COUNT(REPORT_CODE) as ICOUNT \n"+
                "from REPORTS\n"+
                "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + " \n" +
                "group by INSPECTOR_GUID) as QI \n" +
                "on INSPECTORS.INSPECTOR_GUID=QI.INSPECTOR_GUID) \n" +
                "left join \n"+
                "(select REPORTS.INSPECTOR_GUID, COUNT(QUESTION_NUMBER) AS QCOUNT \n" +
                "from REPORT_ITEMS inner join REPORTS \n"+
                "on REPORT_ITEMS.REPORT_CODE=REPORTS.REPORT_CODE \n" +
                "where not IsNull(OBSERVATION) and LEN(OBSERVATION)>0 \n"+
                "and INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + " \n" +
                "group by INSPECTOR_GUID) as QC \n" +
                "on INSPECTORS.INSPECTOR_GUID=QC.INSPECTOR_GUID) \n" +
                "left join \n"+
                "(select REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT1 \n"+
                "from REPORT_ITEMS inner join \n" +
                "(select REPORT_CODE as RC from REPORTS where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + ") as INSP_REPORTS\n" +
                "on REPORT_ITEMS.REPORT_CODE=INSP_REPORTS.RC\n" +
                "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='1') \n"+
                "and LEN(OBSERVATION)>0\n"+
                "group by REPORT_ITEMS.REPORT_CODE) as Q1 \n" +
                "on REPORTS.REPORT_CODE=Q1.REPORT_CODE) \n"+
                "left join \n"+
                "(select REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT2 \n"+
                "from REPORT_ITEMS inner join \n" +
                "(select REPORT_CODE as RC from REPORTS where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + ") as INSP_REPORTS\n" +
                "on REPORT_ITEMS.REPORT_CODE=INSP_REPORTS.RC\n" +
                "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='2') \n"+
                "and LEN(OBSERVATION)>0 \n"+
                "group by REPORT_CODE) as Q2 \n"+
                "on REPORTS.REPORT_CODE=Q2.REPORT_CODE) \n"+
                "left join \n"+
                "(select REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT3 \n"+
                "from REPORT_ITEMS inner join \n" +
                "(select REPORT_CODE as RC from REPORTS where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + ") as INSP_REPORTS\n" +
                "on REPORT_ITEMS.REPORT_CODE=INSP_REPORTS.RC\n" +
                "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='3') \n"+
                "and LEN(OBSERVATION)>0\n"+
                "group by REPORT_CODE) as Q3 \n"+
                "on REPORTS.REPORT_CODE=Q3.REPORT_CODE) \n"+
                "left join \n"+
                "(select REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT4 \n"+
                "from REPORT_ITEMS inner join \n" +
                "(select REPORT_CODE as RC from REPORTS where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + ") as INSP_REPORTS\n" +
                "on REPORT_ITEMS.REPORT_CODE=INSP_REPORTS.RC\n" +
                "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='4') \n"+
                "and LEN(OBSERVATION)>0 \n"+
                "group by REPORT_CODE) as Q4 \n"+
                "on REPORTS.REPORT_CODE=Q4.REPORT_CODE) \n"+
                "left join \n"+
                "(select REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT5 \n"+
                "from REPORT_ITEMS inner join \n" +
                "(select REPORT_CODE as RC from REPORTS where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + ") as INSP_REPORTS\n" +
                "on REPORT_ITEMS.REPORT_CODE=INSP_REPORTS.RC\n" +
                "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='5') \n"+
                "and LEN(OBSERVATION)>0 \n"+
                "group by REPORT_CODE) as Q5 \n"+
                "on REPORTS.REPORT_CODE=Q5.REPORT_CODE) \n"+
                "left join \n"+
                "(select REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT6 \n"+
                "from REPORT_ITEMS inner join \n" +
                "(select REPORT_CODE as RC from REPORTS where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + ") as INSP_REPORTS\n" +
                "on REPORT_ITEMS.REPORT_CODE=INSP_REPORTS.RC\n" +
                "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='6') \n"+
                "and LEN(OBSERVATION)>0 \n"+
                "group by REPORT_CODE) as Q6 \n"+
                "on REPORTS.REPORT_CODE=Q6.REPORT_CODE) \n"+
                "left join \n"+
                "(select REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT7 \n"+
                "from REPORT_ITEMS inner join \n" +
                "(select REPORT_CODE as RC from REPORTS where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + ") as INSP_REPORTS\n" +
                "on REPORT_ITEMS.REPORT_CODE=INSP_REPORTS.RC\n" +
                "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='7') \n"+
                "and LEN(OBSERVATION)>0 \n"+
                "group by REPORT_CODE) as Q7 \n"+
                "on REPORTS.REPORT_CODE=Q7.REPORT_CODE) \n"+
                "left join \n"+
                "(select REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT8 \n"+
                "from REPORT_ITEMS inner join \n" +
                "(select REPORT_CODE as RC from REPORTS where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + ") as INSP_REPORTS\n" +
                "on REPORT_ITEMS.REPORT_CODE=INSP_REPORTS.RC\n" +
                "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='8') \n"+
                "and LEN(OBSERVATION)>0 \n" +
                "group by REPORT_CODE) as Q8 \n"+
                "on REPORTS.REPORT_CODE=Q8.REPORT_CODE) \n"+
                "left join \n"+
                "(select REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT9 \n"+
                "from REPORT_ITEMS inner join \n" +
                "(select REPORT_CODE as RC from REPORTS where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + ") as INSP_REPORTS\n" +
                "on REPORT_ITEMS.REPORT_CODE=INSP_REPORTS.RC\n" +
                "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='9') \n"+
                "and LEN(OBSERVATION)>0 \n"+
                "group by REPORT_CODE) as Q9 \n"+
                "on REPORTS.REPORT_CODE=Q9.REPORT_CODE) \n"+
                "left join \n"+
                "(select REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT10 \n"+
                "from REPORT_ITEMS inner join \n" +
                "(select REPORT_CODE as RC from REPORTS where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + ") as INSP_REPORTS\n" +
                "on REPORT_ITEMS.REPORT_CODE=INSP_REPORTS.RC\n" +
                "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='10') \n"+
                "and LEN(OBSERVATION)>0 \n"+
                "group by REPORT_CODE) as Q10 \n"+
                "on REPORTS.REPORT_CODE=Q10.REPORT_CODE) \n"+
                "left join \n"+
                "(select REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT11 \n"+
                "from REPORT_ITEMS inner join \n" +
                "(select REPORT_CODE as RC from REPORTS where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + ") as INSP_REPORTS\n" +
                "on REPORT_ITEMS.REPORT_CODE=INSP_REPORTS.RC\n" +
                "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='11') \n"+
                "and LEN(OBSERVATION)>0 \n"+
                "group by REPORT_CODE) as Q11 \n"+
                "on REPORTS.REPORT_CODE=Q11.REPORT_CODE) \n"+
                "left join \n"+
                "(select REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT12 \n"+
                "from REPORT_ITEMS inner join \n" +
                "(select REPORT_CODE as RC from REPORTS where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + ") as INSP_REPORTS\n" +
                "on REPORT_ITEMS.REPORT_CODE=INSP_REPORTS.RC\n" +
                "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='12') \n"+
                "and LEN(OBSERVATION)>0 \n"+
                "group by REPORT_CODE) as Q12 \n"+
                "on REPORTS.REPORT_CODE=Q12.REPORT_CODE) \n"+
                "left join \n"+
                "(select REPORT_CODE, COUNT(QUESTION_NUMBER) AS QCOUNT13 \n"+
                "from REPORT_ITEMS inner join \n" +
                "(select REPORT_CODE as RC from REPORTS where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + ") as INSP_REPORTS\n" +
                "on REPORT_ITEMS.REPORT_CODE=INSP_REPORTS.RC\n" +
                "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0,False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='13') \n"+
                "and LEN(OBSERVATION)>0 \n"+
                "group by REPORT_CODE) as Q13 \n"+
                "on REPORTS.REPORT_CODE=Q13.REPORT_CODE \n"+
                "group by INSPECTORS.INSPECTOR_GUID, ICOUNT, QCOUNT) \n"+
                "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid);

            OleDbDataAdapter inAdapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("INSPECTOR_SCORING"))
                DS.Tables["INSPECTOR_SCORING"].Clear();

            this.Update();

            try
            {
                inAdapter.Fill(DS, "INSPECTOR_SCORING");
            }
            catch
            {
                return;
            }

            scoringGrid.DataSource = DS;
            scoringGrid.AutoGenerateColumns = true;
            scoringGrid.DataMember = "INSPECTOR_SCORING";

            scoringGrid.Columns["INSPECTOR_GUID"].Visible = false;
            scoringGrid.Columns["Ch 1"].HeaderText = "1";
            scoringGrid.Columns["Ch 1"].SortMode = DataGridViewColumnSortMode.NotSortable;
            scoringGrid.Columns["Ch 2"].HeaderText = "2";
            scoringGrid.Columns["Ch 2"].SortMode = DataGridViewColumnSortMode.NotSortable;
            scoringGrid.Columns["Ch 3"].HeaderText = "3";
            scoringGrid.Columns["Ch 3"].SortMode = DataGridViewColumnSortMode.NotSortable;
            scoringGrid.Columns["Ch 4"].HeaderText = "4";
            scoringGrid.Columns["Ch 4"].SortMode = DataGridViewColumnSortMode.NotSortable;
            scoringGrid.Columns["Ch 5"].HeaderText = "5";
            scoringGrid.Columns["Ch 5"].SortMode = DataGridViewColumnSortMode.NotSortable;
            scoringGrid.Columns["Ch 6"].HeaderText = "6";
            scoringGrid.Columns["Ch 6"].SortMode = DataGridViewColumnSortMode.NotSortable;
            scoringGrid.Columns["Ch 7"].HeaderText = "7";
            scoringGrid.Columns["Ch 7"].SortMode = DataGridViewColumnSortMode.NotSortable;
            scoringGrid.Columns["Ch 8"].HeaderText = "8";
            scoringGrid.Columns["Ch 8"].SortMode = DataGridViewColumnSortMode.NotSortable;
            scoringGrid.Columns["Ch 9"].HeaderText = "9";
            scoringGrid.Columns["Ch 9"].SortMode = DataGridViewColumnSortMode.NotSortable;
            scoringGrid.Columns["Ch 10"].HeaderText = "10";
            scoringGrid.Columns["Ch 10"].SortMode = DataGridViewColumnSortMode.NotSortable;
            scoringGrid.Columns["Ch 11"].HeaderText = "11";
            scoringGrid.Columns["Ch 11"].SortMode = DataGridViewColumnSortMode.NotSortable;
            scoringGrid.Columns["Ch 12"].HeaderText = "12";
            scoringGrid.Columns["Ch 12"].SortMode = DataGridViewColumnSortMode.NotSortable;
            scoringGrid.Columns["Ch 13"].HeaderText = "13";
            scoringGrid.Columns["Ch 13"].SortMode = DataGridViewColumnSortMode.NotSortable;

            //this.Cursor = Cursors.Default;


        }

        private void btnLoadProfile_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "";
            openFileDialog1.FileName = "";
            
            openFileDialog1.Filter = "Excel files(*.xls;*.xlsx)|*.xls;*.xlsx";

            var rslt=openFileDialog1.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                var excelApp = new Excel.Application();

                // Make the object invisible.
                excelApp.Visible = false;
                string fileName=openFileDialog1.FileName;
                excelApp.Workbooks.Open(fileName);
                Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;

                int rows = workSheet.UsedRange.Rows.Count;
                int cols = workSheet.UsedRange.Columns.Count;

                Object[,] varArray = new Object[rows, cols];

                varArray = workSheet.Range[workSheet.Cells[1, 1], workSheet.Cells[1 + rows - 1, 1 + cols - 1]].Value2;

                excelApp.Workbooks[1].Close();
                excelApp.Workbooks.Close();
                excelApp.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                workSheet = null;
                excelApp = null;

                System.GC.Collect();

                //start data import
                //Check whether cell is empty

                if (varArray.GetLength(0)!=31)
                {
                    if (MessageBox.Show("File has invalid format (Line count<>31). Would you like to try to import data anyway?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)!=DialogResult.Yes)
                    return;
                }

                string background = "";

                if (varArray[9, 2] != null)
                    background = varArray[9, 2].ToString().Trim();

                if (background.Length>0)
                { 
                    if (tbBackground.Text.Trim().Length == 0)
                        tbBackground.Text = background;
                    else
                    {
                        if (tbBackground.Text.Trim() != background)
                        {
                            var reslt = MessageBox.Show("New backgrund \"" + background + "\" differ from current background \"" + tbBackground.Text + "\".\n" +
                                "Would you like to use new background?",
                                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (reslt == DialogResult.Yes)
                                tbBackground.Text = background;
                        }
                    }
                }

                if (CheckComments(1, varArray, 11) < 0)
                    return;

                if (CheckComments(3, varArray, 14) < 0)
                    return;

                if (CheckComments(4, varArray, 16) < 0)
                    return;

                if (CheckComments(5, varArray, 18) < 0)
                    return;

                if (CheckComments(7, varArray, 20) < 0)
                    return;

                if (CheckComments(9, varArray, 23) < 0)
                    return;

                if (CheckComments(10, varArray, 25) < 0)
                    return;

                if (CheckComments(11, varArray, 27) < 0)
                    return;

                if (CheckComments(13, varArray, 30) < 0)
                    return;

                if (CheckComments(14, varArray, 31) < 0)
                    return;

                if (MessageBox.Show("Would you like to delete file \"" + Path.GetFileName(fileName) + "\"?",
                    "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    try
                    {
                        //File.Delete(fileName);
                        DeleteFileCommand(fileName);
                    }
                    catch (Exception E)
                    {
                        var rst = MessageBox.Show("Unable to delete file \"" + Path.GetFileName(fileName) + "\" due to following error:\n\n" +
                            E.Message+"\n\n"+
                            "Would you like to try to rename and delete file?", "Error",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                        if (rst==DialogResult.Yes)
                            //RenameAndDelete(fileName);
                            DeleteFileCommand(fileName);
                    }
                }
            }

        }

        private void RenameAndDelete(string fileName)
        {
            string newFileName=Path.Combine(Path.GetDirectoryName(fileName), Path.GetFileNameWithoutExtension(fileName)+".xyz");

            try
            {
                File.Move(fileName, newFileName);
            }
            catch (Exception E)
            {
                MessageBox.Show("Unable to rename file \"" + Path.GetFileName(fileName) + "\" to \"" +
                    Path.GetFileName(newFileName) + "\" due to following error:\n\n" +
                    E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                File.Delete(newFileName);
            }
            catch (Exception E)
            {
                MessageBox.Show("Unable to delete file \"" + Path.GetFileName(newFileName) + "\" due to following error:\n\n" +
                    E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int CheckComments(int rowIndex, Object[,] varArray, int excelRow)
        {
                string str = "";
                string comments = "";
                string sectionID = dgvProfile.Rows[rowIndex].Cells["SECTION_ID"].Value.ToString();
                string questionID = dgvProfile.Rows[rowIndex].Cells["QUESTION_ID"].Value.ToString();

                str = dgvProfile.Rows[rowIndex].Cells["COMMENTS"].Value.ToString();

                switch (excelRow)
                {
                    case 11:
                        if ((varArray[11, 2] != null) && (varArray[11, 2].ToString().Trim().Length > 0))
                            comments = varArray[11, 2].ToString().Trim();

                        if ((varArray[12, 2] != null) && (varArray[12, 2].ToString().Trim().Length > 0))
                        {
                            if (comments.Length > 0)
                                comments = comments + ". " + varArray[12, 2].ToString().Trim();
                            else
                                comments = varArray[12, 2].ToString().Trim();
                        }

                        break;
                    case 14:
                        if ((varArray[14, 2] != null) && (varArray[14, 2].ToString().Trim().Length > 0))
                            comments = varArray[14, 2].ToString().Trim();

                        if ((varArray[15, 2] != null) && (varArray[15, 2].ToString().Trim().Length > 0))
                        {
                            if (comments.Length > 0)
                                comments = comments + ". " + varArray[15, 2].ToString().Trim();
                            else
                                comments = varArray[15, 2].ToString().Trim();
                        }

                        break;
                    case 16:
                        if ((varArray[16, 2] != null) && (varArray[16, 2].ToString().Trim().Length > 0))
                            comments = varArray[16, 2].ToString().Trim();

                        if ((varArray[17, 2] != null) && (varArray[17, 2].ToString().Trim().Length > 0))
                        {
                            if (comments.Length > 0)
                                comments = comments + ". " + varArray[17, 2].ToString().Trim();
                            else
                                comments = varArray[17, 2].ToString().Trim();
                        }

                        break;
                    case 18:
                        if ((varArray[18, 2] != null) && (varArray[18, 2].ToString().Trim().Length > 0))
                            comments = varArray[18, 2].ToString().Trim();

                        if ((varArray[19, 2] != null) && (varArray[19, 2].ToString().Trim().Length > 0))
                        {
                            if (comments.Length > 0)
                                comments = comments + ". " + varArray[19, 2].ToString().Trim();
                            else
                                comments = varArray[19, 2].ToString().Trim();
                        }

                        break;
                    case 20:
                        if ((varArray[20, 2] != null) && (varArray[20, 2].ToString().Trim().Length > 0))
                            comments = varArray[20, 2].ToString().Trim();

                        if ((varArray[21, 2] != null) && (varArray[21, 2].ToString().Trim().Length > 0))
                        {
                            if (comments.Length > 0)
                                comments = comments + ". " + varArray[21, 2].ToString().Trim();
                            else
                                comments = varArray[21, 2].ToString().Trim();
                        }

                        break;
                    case 23:
                        if ((varArray[23, 2] != null) && (varArray[23, 2].ToString().Trim().Length > 0))
                            comments = varArray[23, 2].ToString().Trim();

                        if ((varArray[24, 2] != null) && (varArray[24, 2].ToString().Trim().Length > 0))
                        {
                            if (comments.Length > 0)
                                comments = comments + ". " + varArray[24, 2].ToString().Trim();
                            else
                                comments = varArray[24, 2].ToString().Trim();
                        }

                        break;
                    case 25:
                        if ((varArray[25, 2] != null) && (varArray[25, 2].ToString().Trim().Length > 0))
                            comments = varArray[25, 2].ToString().Trim();

                        if ((varArray[26, 2] != null) && (varArray[26, 2].ToString().Trim().Length > 0))
                        {
                            if (comments.Length > 0)
                                comments = comments + ". " + varArray[26, 2].ToString().Trim();
                            else
                                comments = varArray[26, 2].ToString().Trim();
                        }

                        break;
                    case 27:
                        if ((varArray[27, 2] != null) && (varArray[27, 2].ToString().Trim().Length > 0))
                            comments = varArray[27, 2].ToString().Trim();

                        if ((varArray[28, 2] != null) && (varArray[28, 2].ToString().Trim().Length > 0))
                        {
                            if (comments.Length > 0)
                                comments = comments + ". " + varArray[28, 2].ToString().Trim();
                            else
                                comments = varArray[28, 2].ToString().Trim();
                        }

                        break;
                    case 30:
                        if ((varArray[30, 2] != null) && (varArray[30, 2].ToString().Trim().Length > 0))
                            comments = varArray[30, 2].ToString().Trim();

                        break;
                    case 31:
                        if ((varArray[31, 2] != null) && (varArray[31, 2].ToString().Trim().Length > 0))
                            comments = varArray[31, 2].ToString().Trim();

                        break;
                }

                if (str.Length == 0)
                {
                    if (comments.Length > 0)
                        SaveComments(comments, sectionID, questionID, rowIndex);

                }
                else
                {
                    if (comments.Length>0)
                    {
                        ImportCommentsForm icf = new ImportCommentsForm(this.Icon, this.Font, dgvProfile.Rows[rowIndex].Cells["COMMENTS"].Value.ToString());
                        
                        icf.tbQuestion.Text = dgvProfile.Rows[rowIndex].Cells["QUESTION"].Value.ToString();
                        icf.tbOriginal.Text = dgvProfile.Rows[rowIndex].Cells["COMMENTS"].Value.ToString();
                        icf.tbNew.Text = comments;

                        var rslt = icf.ShowDialog();
    
                        if (rslt == DialogResult.OK)
                        {
                            SaveComments(icf.tbOriginal.Text, sectionID, questionID,rowIndex);
                        }
                        else
                        {
                            if (rslt == DialogResult.Abort)
                                return -1;
                        }
                    }

                }

                return 0;

        }

        private void SaveComments(string comments, string sectionID, string questionID,int rowIndex)
        {
            dgvProfile.Rows[rowIndex].Cells["COMMENTS"].Value = comments;

            OleDbCommand cmd = new OleDbCommand("", connection);

            //Проверяем существует ли такая запись
            cmd.CommandText =
                "select Count(COMMENTS) as num \n" +
                "from UNIFIED_COMMENTS \n" +
                "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + " \n" +
                "and SECTION_ID=" + sectionID + "\n" +
                "and QUESTION_ID=" + questionID;

            int recs = (int)cmd.ExecuteScalar();

            if (recs == 0)
                cmd.CommandText =
                    "insert into UNIFIED_COMMENTS (INSPECTOR_GUID, SECTION_ID, QUESTION_ID, COMMENTS)\n" +
                    "values (" + MainForm.GuidToStr(inspectorGuid) + "," +
                               sectionID + "," +
                               questionID + ",'" +
                               StrToSQLStr(comments) + "')";
            else
                cmd.CommandText =
                    "update UNIFIED_COMMENTS set \n" +
                    "COMMENTS='" + StrToSQLStr(comments) + "' \n" +
                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + " \n" +
                    "and SECTION_ID=" + sectionID + "\n" +
                    "and QUESTION_ID=" + questionID;

            cmd.ExecuteNonQuery();

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            string fullName = "";
            openFileDialog1.InitialDirectory = photoFolder;
            openFileDialog1.FileName = "";

            openFileDialog1.Filter = "All files (*.*)|*.*";

            openFileDialog1.InitialDirectory = filesFolder;
            openFileDialog1.Multiselect = true;
            openFileDialog1.FileName = "";

            var rslt=openFileDialog1.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                string[] fileNames = openFileDialog1.FileNames;

                foreach (string fileName in fileNames)
                {
                    if (fileName.StartsWith(filesFolder, StringComparison.OrdinalIgnoreCase))
                    {
                        if (File.Exists(fileName))
                        {
                            OleDbCommand cmd = new OleDbCommand("", connection);

                            fullName = fileName.Substring(filesFolder.Length+1);

                            //Check whether file already exists
                            cmd.CommandText =
                                "select Count(FILE_NAME) as FileCount \n" +
                                "from INSPECTOR_FILES \n" +
                                "where FILE_NAME like '" + MainForm.StrToSQLStr(fullName) + "' \n" +
                                "and INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid);

                            int recs = (int)cmd.ExecuteScalar();

                            if (recs > 0)
                            {
                                MessageBox.Show("There is already a record for file \"" + fileName + "\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                cmd.CommandText =
                                    "insert into INSPECTOR_FILES (INSPECTOR_GUID,FILE_NAME,FILE_EXISTS) \n" +
                                    "values(" + MainForm.GuidToStr(inspectorGuid) + "," +
                                        "'" + MainForm.StrToSQLStr(fullName) + "'," +
                                        true.ToString() + ")";

                                MainForm.cmdExecute(cmd);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Information for file \"" + fileName + "\" was not saved as it does not located in \"" + filesFolder + "\".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                updateInspectorFiles();

                if (fullName.Length>0)
                {
                    String searchValue = fullName;

                    int rowIndex = -1;

                    foreach (DataGridViewRow dgRow in dgvInspectorFiles.Rows)
                    {
                        if (dgRow.Cells["FILE_NAME"].Value.ToString().Equals(searchValue))
                        {
                            rowIndex = dgRow.Index;
                            break;
                        }
                    }

                    if (rowIndex >= 0)
                    {
                        //dataGridView1.Rows[rowIndex].Selected = true;
                        dgvInspectorFiles.CurrentCell = dgvInspectorFiles["FILE_NAME", rowIndex];
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvInspectorFiles.Rows.Count==0)
                return;

            int curRow = dgvInspectorFiles.CurrentCell.RowIndex;
            int curCol = dgvInspectorFiles.CurrentCell.ColumnIndex; 

            string fileName = filesFolder + "\\"+dgvInspectorFiles.CurrentRow.Cells["FILE_NAME"].Value.ToString();

            string msgText =
                "You are going to remove record for the following file: \n\n" +
                fileName + "\n\n" +
                "Would you like to proceed?";

            var rslt = MessageBox.Show(msgText, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (rslt==DialogResult.Yes)
            {
                OleDbCommand cmd = new OleDbCommand("", connection);
                cmd.CommandText =
                    "delete from INSPECTOR_FILES \n" +
                    "where INSPECTOR_FILE_ID=" + dgvInspectorFiles.CurrentRow.Cells["INSPECTOR_FILE_ID"].Value.ToString();

                if (MainForm.cmdExecute(cmd)>=0)
                {
                    updateInspectorFiles();

                    if (dgvInspectorFiles.Rows.Count > 0)
                    {
                        if (dgvInspectorFiles.Rows.Count - 1 > curRow)
                            dgvInspectorFiles.CurrentCell = dgvInspectorFiles[curCol, curRow];
                        else
                            dgvInspectorFiles.CurrentCell = dgvInspectorFiles[curCol, dgvInspectorFiles.Rows.Count - 1];
                    }
                }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenSelectedFile();
        }

        private void OpenSelectedFile()
        {
            if (dgvInspectorFiles.Rows.Count == 0)
                return;

            string fileName = filesFolder + "\\" + dgvInspectorFiles.CurrentRow.Cells["FILE_NAME"].Value.ToString();
            string id = dgvInspectorFiles.CurrentRow.Cells["INSPECTOR_FILE_ID"].Value.ToString();

            if (File.Exists(fileName))
            {
                Process.Start(fileName);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("File \"" + fileName + "\" was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                OleDbCommand cmd = new OleDbCommand("", connection);
                cmd.CommandText =
                    "update INSPECTOR_FILES set \n" +
                    "FILE_EXISTS=" + false.ToString() + " \n" +
                    "where INSPECTOR_FILE_ID=" + id;

                MainForm.cmdExecute(cmd);

                updateInspectorFiles();

                String searchValue = id;

                int rowIndex = -1;

                foreach (DataGridViewRow dgRow in dgvInspectorFiles.Rows)
                {
                    if (dgRow.Cells["INSPECTOR_FILE_ID"].Value.ToString().Equals(searchValue))
                    {
                        rowIndex = dgRow.Index;
                        break;
                    }
                }

                if (rowIndex >= 0)
                {
                    //dataGridView1.Rows[rowIndex].Selected = true;
                    dgvInspectorFiles.CurrentCell = dgvInspectorFiles["FILE_NAME", rowIndex];
                }

            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvInspectorFiles.Rows.Count == 0)
                return;

            int curRow = dgvInspectorFiles.CurrentCell.RowIndex;
            int curCol = dgvInspectorFiles.CurrentCell.ColumnIndex; 
            
            string fileName = filesFolder + "\\" + dgvInspectorFiles.CurrentRow.Cells["FILE_NAME"].Value.ToString();

            InspectorFileEditForm form = new InspectorFileEditForm(this.Icon, this.Font);

            form.fileName = fileName;

            var rslt = form.ShowDialog();

            bool fileExists = false;

            if ((rslt==DialogResult.OK) && (MainForm.isPowerUser))
            {
                if (File.Exists(form.fileName))
                {
                    fileName = form.fileName;
                    fileExists = true;
                }
                else
                {
                    var result = MessageBox.Show("File \"" + form.fileName + "\" does not exist. Would you like to use it anyway?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                        fileName = form.fileName;
                    else
                        return;
                }

                OleDbCommand cmd = new OleDbCommand("", connection);

                string fullName = fileName.Substring(filesFolder.Length+1);

                cmd.CommandText =
                    "update INSPECTOR_FILES set \n" +
                    "FILE_NAME='" + MainForm.StrToSQLStr(fullName) + "', \n" +
                    "FILE_EXISTS=" + fileExists.ToString() + "\n" +
                    "where INSPECTOR_FILE_ID=" + dgvInspectorFiles.CurrentRow.Cells["INSPECTOR_FILE_ID"].Value.ToString();

                if (MainForm.cmdExecute(cmd)>=0)
                {
                    updateInspectorFiles();

                    dgvInspectorFiles.CurrentCell = dgvInspectorFiles[curCol, curRow];
                }
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void dgvInspectorFiles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenSelectedFile();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            //Check file exists
            if (dgvInspectorFiles.Rows.Count == 0)
                return;

            this.Cursor = Cursors.WaitCursor;

            try
            {
                OleDbCommand cmd = new OleDbCommand("", connection);

                int curRow = -1;
                
                if (dgvInspectorFiles.CurrentCell!=null)
                    curRow=dgvInspectorFiles.CurrentCell.RowIndex;

                int curCol = -1;
                
                if (dgvInspectorFiles.CurrentCell!=null)
                    curCol=dgvInspectorFiles.CurrentCell.ColumnIndex;

                for (int i = 0; i < dgvInspectorFiles.Rows.Count; i++)
                {
                    if (File.Exists(filesFolder + "\\" + dgvInspectorFiles.Rows[i].Cells["FILE_NAME"].Value.ToString()))
                    {
                        cmd.CommandText =
                            "update INSPECTOR_FILES set \n" +
                            "FILE_EXISTS=" + true.ToString() + "\n" +
                            "where INSPECTOR_FILE_ID=" + dgvInspectorFiles.Rows[i].Cells["INSPECTOR_FILE_ID"].Value.ToString();
                        MainForm.cmdExecute(cmd);
                    }
                    else
                    {
                        cmd.CommandText =
                            "update INSPECTOR_FILES set \n" +
                            "FILE_EXISTS=" + false.ToString() + "\n" +
                            "where INSPECTOR_FILE_ID=" + dgvInspectorFiles.Rows[i].Cells["INSPECTOR_FILE_ID"].Value.ToString();
                        MainForm.cmdExecute(cmd);
                    }
                }

                updateInspectorFiles();

                if ((curCol>=0) && (curRow>=0))
                    dgvInspectorFiles.CurrentCell = dgvInspectorFiles[curCol, curRow];
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 2:
                    fillInspections();
                    break;
                case 3:
                    updateInspectorFiles();
                    break;
            }

        }

        private void dgvInspections_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Show inspection
            if (dgvInspections.Rows.Count == 0)
                return;

            this.Cursor = Cursors.WaitCursor;

            string reportCode = dgvInspections.CurrentRow.Cells["REPORT_CODE"].Value.ToString();

            FormReportSummary form = new FormReportSummary(false, reportCode, true, true);

            this.Cursor = Cursors.Default;

            form.ShowDialog();
        }

        private void chbUnfavourable_CheckedChanged(object sender, EventArgs e)
        {
            if (chbUnfavourable.Checked)
            {
                tbFullName.BackColor = Color.FromArgb(255, 190, 206);
            }
            else
            {
                tbFullName.BackColor = Color.FromArgb(255, 255, 255);
            }
        }

        private void DeleteFileCommand(string fileName)
        {
            string command = "del /F \"" + fileName + "\" > result.txt 2>&1";

            int code = MainForm.ExecuteCommand(command);

            if (code!=0)
            {
                MessageBox.Show("Unable to delete file. Error code=" + code.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!File.Exists(fileName))
                    MessageBox.Show("File was deleted successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Delete command was executed successfully but file was not deleted", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnInterner_Click(object sender, EventArgs e)
        {
            if (tbLinkedIn.Text.Trim().Length>0)
            {
                try
                {
                    Process.Start(tbLinkedIn.Text);
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SetProfileDate(DateTime value)
        {
            if (value<=DateTimePicker.MinimumDateTime)
            {
                dtProfileDate.Format = DateTimePickerFormat.Custom;
                dtProfileDate.CustomFormat = " ";
                dtProfileDate.Value = DateTimePicker.MinimumDateTime;
            }
            else
            {
                dtProfileDate.Format = DateTimePickerFormat.Short;

                if (value >= DateTimePicker.MinimumDateTime)
                    dtProfileDate.Value = value;
                else
                    dtProfileDate.Value = DateTimePicker.MinimumDateTime;
            }
        }

        private void dtProfileDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtProfileDate.Value==DateTimePicker.MinimumDateTime)
            {
                dtProfileDate.Format = DateTimePickerFormat.Custom;
                dtProfileDate.CustomFormat = " ";
                dtProfileDate.Value = DateTimePicker.MinimumDateTime; ;
            }
            else
            {
                dtProfileDate.Format = DateTimePickerFormat.Short;
            }
        }

        private void dtProfileDate_KeyDown(object sender, KeyEventArgs e)
        {
            //Clear control in case of Del button click

            if (e.KeyValue == 46)
            {
                dtProfileDate.Value = DateTimePicker.MinimumDateTime;
            }
        }

        private void dtProfileDate_DropDown(object sender, EventArgs e)
        {
            if (dtProfileDate.Value == DateTimePicker.MinimumDateTime)
                dtProfileDate.Value = DateTime.Today;
        }
    }
}
