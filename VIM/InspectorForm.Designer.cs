namespace VIM
{
    partial class InspectorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InspectorForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title5 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFullName = new System.Windows.Forms.TextBox();
            this.chbUnfavourable = new System.Windows.Forms.CheckBox();
            this.labelPhoto = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnLoadProfile = new System.Windows.Forms.Button();
            this.dgvProfile = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbNotes = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgvInspections = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dgvInspectorFiles = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCheck = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbBackground = new System.Windows.Forms.TextBox();
            this.scoringGrid = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDeletePhoto = new System.Windows.Forms.Button();
            this.btnLoadPhoto = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.dgvScoring = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.lblInspections = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblObservations = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblAverage = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label7 = new System.Windows.Forms.Label();
            this.tbLinkedIn = new System.Windows.Forms.TextBox();
            this.btnInternet = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.dtProfileDate = new System.Windows.Forms.DateTimePicker();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProfile)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInspections)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInspectorFiles)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scoringGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScoring)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Full name";
            // 
            // tbFullName
            // 
            this.tbFullName.Location = new System.Drawing.Point(10, 26);
            this.tbFullName.MaxLength = 255;
            this.tbFullName.Name = "tbFullName";
            this.tbFullName.Size = new System.Drawing.Size(268, 20);
            this.tbFullName.TabIndex = 1;
            // 
            // chbUnfavourable
            // 
            this.chbUnfavourable.AutoSize = true;
            this.chbUnfavourable.Location = new System.Drawing.Point(14, 52);
            this.chbUnfavourable.Name = "chbUnfavourable";
            this.chbUnfavourable.Size = new System.Drawing.Size(90, 17);
            this.chbUnfavourable.TabIndex = 8;
            this.chbUnfavourable.Text = "Unfavourable";
            this.chbUnfavourable.UseVisualStyleBackColor = true;
            this.chbUnfavourable.CheckedChanged += new System.EventHandler(this.chbUnfavourable_CheckedChanged);
            // 
            // labelPhoto
            // 
            this.labelPhoto.AutoSize = true;
            this.labelPhoto.Location = new System.Drawing.Point(158, 56);
            this.labelPhoto.Name = "labelPhoto";
            this.labelPhoto.Size = new System.Drawing.Size(57, 13);
            this.labelPhoto.TabIndex = 15;
            this.labelPhoto.Text = "labelPhoto";
            this.labelPhoto.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "JPEG files(*.jpg)|*.jpg|Bitmap files (*.bmp)|*.bmp|PNG files (*.png)|*.png";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(10, 232);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1137, 300);
            this.tabControl1.TabIndex = 17;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.TabIndexChanged += new System.EventHandler(this.tabControl1_TabIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnLoadProfile);
            this.tabPage1.Controls.Add(this.dgvProfile);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1129, 274);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Profile";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // btnLoadProfile
            // 
            this.btnLoadProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadProfile.Image = global::VIM.Properties.Resources.load_1;
            this.btnLoadProfile.Location = new System.Drawing.Point(1051, 242);
            this.btnLoadProfile.Name = "btnLoadProfile";
            this.btnLoadProfile.Size = new System.Drawing.Size(75, 25);
            this.btnLoadProfile.TabIndex = 2;
            this.btnLoadProfile.Text = "Load";
            this.btnLoadProfile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLoadProfile.UseVisualStyleBackColor = true;
            this.btnLoadProfile.Click += new System.EventHandler(this.btnLoadProfile_Click);
            // 
            // dgvProfile
            // 
            this.dgvProfile.AllowUserToAddRows = false;
            this.dgvProfile.AllowUserToDeleteRows = false;
            this.dgvProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProfile.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProfile.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgvProfile.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvProfile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProfile.Location = new System.Drawing.Point(3, 3);
            this.dgvProfile.Name = "dgvProfile";
            this.dgvProfile.ReadOnly = true;
            this.dgvProfile.RowHeadersWidth = 25;
            this.dgvProfile.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProfile.RowsDefaultCellStyle = dataGridViewCellStyle25;
            this.dgvProfile.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProfile.Size = new System.Drawing.Size(1123, 233);
            this.dgvProfile.TabIndex = 1;
            this.dgvProfile.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProfile_CellDoubleClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbNotes);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1129, 274);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Notes";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbNotes
            // 
            this.tbNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbNotes.Location = new System.Drawing.Point(3, 3);
            this.tbNotes.Multiline = true;
            this.tbNotes.Name = "tbNotes";
            this.tbNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbNotes.Size = new System.Drawing.Size(1123, 268);
            this.tbNotes.TabIndex = 12;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgvInspections);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1129, 274);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Inspections";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgvInspections
            // 
            this.dgvInspections.AllowUserToAddRows = false;
            this.dgvInspections.AllowUserToDeleteRows = false;
            this.dgvInspections.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInspections.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvInspections.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInspections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInspections.Location = new System.Drawing.Point(3, 3);
            this.dgvInspections.Name = "dgvInspections";
            this.dgvInspections.ReadOnly = true;
            this.dgvInspections.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvInspections.Size = new System.Drawing.Size(1123, 268);
            this.dgvInspections.TabIndex = 0;
            this.dgvInspections.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInspections_CellDoubleClick);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dgvInspectorFiles);
            this.tabPage4.Controls.Add(this.panel1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1129, 274);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Files";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dgvInspectorFiles
            // 
            this.dgvInspectorFiles.AllowUserToAddRows = false;
            this.dgvInspectorFiles.AllowUserToDeleteRows = false;
            this.dgvInspectorFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInspectorFiles.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvInspectorFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInspectorFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInspectorFiles.Location = new System.Drawing.Point(3, 3);
            this.dgvInspectorFiles.Name = "dgvInspectorFiles";
            this.dgvInspectorFiles.ReadOnly = true;
            this.dgvInspectorFiles.RowHeadersWidth = 25;
            this.dgvInspectorFiles.Size = new System.Drawing.Size(1123, 237);
            this.dgvInspectorFiles.TabIndex = 1;
            this.dgvInspectorFiles.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInspectorFiles_CellDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCheck);
            this.panel1.Controls.Add(this.btnNew);
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Controls.Add(this.btnOpen);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 240);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1123, 31);
            this.panel1.TabIndex = 0;
            // 
            // btnCheck
            // 
            this.btnCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheck.Image = ((System.Drawing.Image)(resources.GetObject("btnCheck.Image")));
            this.btnCheck.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCheck.Location = new System.Drawing.Point(964, 3);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(75, 25);
            this.btnCheck.TabIndex = 4;
            this.btnCheck.Text = "Check";
            this.btnCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.Image = global::VIM.Properties.Resources.plus;
            this.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNew.Location = new System.Drawing.Point(721, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 25);
            this.btnNew.TabIndex = 3;
            this.btnNew.Text = "New";
            this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Image = global::VIM.Properties.Resources.edit;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEdit.Location = new System.Drawing.Point(802, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 25);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Edit";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Image = global::VIM.Properties.Resources.open;
            this.btnOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOpen.Location = new System.Drawing.Point(883, 3);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 25);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Open";
            this.btnOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Image = global::VIM.Properties.Resources.minus;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.Location = new System.Drawing.Point(1045, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.Text = "Delete";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Background";
            // 
            // tbBackground
            // 
            this.tbBackground.Location = new System.Drawing.Point(10, 124);
            this.tbBackground.MaxLength = 255;
            this.tbBackground.Name = "tbBackground";
            this.tbBackground.Size = new System.Drawing.Size(268, 20);
            this.tbBackground.TabIndex = 19;
            // 
            // scoringGrid
            // 
            this.scoringGrid.AllowUserToAddRows = false;
            this.scoringGrid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.scoringGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle26;
            this.scoringGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.scoringGrid.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle27.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle27.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.scoringGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle27;
            this.scoringGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle28.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle28.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle28.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle28.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.scoringGrid.DefaultCellStyle = dataGridViewCellStyle28;
            this.scoringGrid.Location = new System.Drawing.Point(513, 174);
            this.scoringGrid.Name = "scoringGrid";
            this.scoringGrid.ReadOnly = true;
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle29.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle29.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle29.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle29.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle29.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.scoringGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle29;
            this.scoringGrid.RowHeadersVisible = false;
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.scoringGrid.RowsDefaultCellStyle = dataGridViewCellStyle30;
            this.scoringGrid.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.scoringGrid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.scoringGrid.Size = new System.Drawing.Size(93, 31);
            this.scoringGrid.TabIndex = 20;
            this.scoringGrid.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(515, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Scoring by chapter";
            this.label3.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::VIM.Properties.Resources.delete;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.Location = new System.Drawing.Point(1072, 543);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnDeletePhoto
            // 
            this.btnDeletePhoto.Image = global::VIM.Properties.Resources.remove_image;
            this.btnDeletePhoto.Location = new System.Drawing.Point(110, 76);
            this.btnDeletePhoto.Name = "btnDeletePhoto";
            this.btnDeletePhoto.Size = new System.Drawing.Size(105, 25);
            this.btnDeletePhoto.TabIndex = 16;
            this.btnDeletePhoto.Text = "Delete photo";
            this.btnDeletePhoto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeletePhoto.UseVisualStyleBackColor = true;
            this.btnDeletePhoto.Click += new System.EventHandler(this.bthDeletePhoto_Click);
            // 
            // btnLoadPhoto
            // 
            this.btnLoadPhoto.Image = global::VIM.Properties.Resources.insert_image;
            this.btnLoadPhoto.Location = new System.Drawing.Point(13, 76);
            this.btnLoadPhoto.Name = "btnLoadPhoto";
            this.btnLoadPhoto.Size = new System.Drawing.Size(94, 25);
            this.btnLoadPhoto.TabIndex = 14;
            this.btnLoadPhoto.Text = "Load photo";
            this.btnLoadPhoto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLoadPhoto.UseVisualStyleBackColor = true;
            this.btnLoadPhoto.Click += new System.EventHandler(this.bthLoadPhoto_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(284, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(221, 214);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::VIM.Properties.Resources.Ok;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.Location = new System.Drawing.Point(991, 543);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "OK";
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // dgvScoring
            // 
            this.dgvScoring.AllowUserToAddRows = false;
            this.dgvScoring.AllowUserToDeleteRows = false;
            this.dgvScoring.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvScoring.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvScoring.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScoring.Location = new System.Drawing.Point(513, 84);
            this.dgvScoring.Name = "dgvScoring";
            this.dgvScoring.ReadOnly = true;
            this.dgvScoring.RowHeadersWidth = 25;
            this.dgvScoring.Size = new System.Drawing.Size(73, 53);
            this.dgvScoring.TabIndex = 22;
            this.dgvScoring.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(514, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Inspections:";
            // 
            // lblInspections
            // 
            this.lblInspections.AutoSize = true;
            this.lblInspections.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblInspections.Location = new System.Drawing.Point(584, 13);
            this.lblInspections.Name = "lblInspections";
            this.lblInspections.Size = new System.Drawing.Size(14, 13);
            this.lblInspections.TabIndex = 24;
            this.lblInspections.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(514, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Observations:";
            // 
            // lblObservations
            // 
            this.lblObservations.AutoSize = true;
            this.lblObservations.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblObservations.Location = new System.Drawing.Point(584, 33);
            this.lblObservations.Name = "lblObservations";
            this.lblObservations.Size = new System.Drawing.Size(14, 13);
            this.lblObservations.TabIndex = 26;
            this.lblObservations.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(514, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Average:";
            // 
            // lblAverage
            // 
            this.lblAverage.AutoSize = true;
            this.lblAverage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblAverage.Location = new System.Drawing.Point(584, 53);
            this.lblAverage.Name = "lblAverage";
            this.lblAverage.Size = new System.Drawing.Size(14, 13);
            this.lblAverage.TabIndex = 28;
            this.lblAverage.Text = "0";
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart1.BackColor = System.Drawing.Color.Transparent;
            this.chart1.BackImageTransparentColor = System.Drawing.Color.Transparent;
            this.chart1.BackSecondaryColor = System.Drawing.Color.Transparent;
            this.chart1.BorderlineColor = System.Drawing.Color.Transparent;
            chartArea5.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea5.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea5.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea5.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea5.AxisX.MajorGrid.Enabled = false;
            chartArea5.AxisX.MajorTickMark.Interval = 0D;
            chartArea5.AxisX.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea5.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea5.AxisY.MajorGrid.Enabled = false;
            chartArea5.BackColor = System.Drawing.Color.Transparent;
            chartArea5.BackImageTransparentColor = System.Drawing.Color.Transparent;
            chartArea5.BackSecondaryColor = System.Drawing.Color.Transparent;
            chartArea5.BorderColor = System.Drawing.Color.Transparent;
            chartArea5.BorderWidth = 0;
            chartArea5.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea5);
            this.chart1.Location = new System.Drawing.Point(612, 10);
            this.chart1.Name = "chart1";
            series5.BorderWidth = 0;
            series5.ChartArea = "ChartArea1";
            series5.Name = "Series1";
            this.chart1.Series.Add(series5);
            this.chart1.Size = new System.Drawing.Size(535, 223);
            this.chart1.TabIndex = 29;
            this.chart1.Text = "chart1";
            title5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            title5.Name = "Title1";
            title5.Text = "Scoring by chapter";
            this.chart1.Titles.Add(title5);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "LinkedIn";
            // 
            // tbLinkedIn
            // 
            this.tbLinkedIn.Location = new System.Drawing.Point(10, 166);
            this.tbLinkedIn.Name = "tbLinkedIn";
            this.tbLinkedIn.Size = new System.Drawing.Size(246, 20);
            this.tbLinkedIn.TabIndex = 31;
            // 
            // btnInternet
            // 
            this.btnInternet.Image = global::VIM.Properties.Resources.internet_explorer_16;
            this.btnInternet.Location = new System.Drawing.Point(255, 165);
            this.btnInternet.Name = "btnInternet";
            this.btnInternet.Size = new System.Drawing.Size(23, 22);
            this.btnInternet.TabIndex = 32;
            this.btnInternet.UseVisualStyleBackColor = true;
            this.btnInternet.Click += new System.EventHandler(this.btnInterner_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 196);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 33;
            this.label8.Text = "Profile date";
            // 
            // dtProfileDate
            // 
            this.dtProfileDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtProfileDate.Location = new System.Drawing.Point(78, 193);
            this.dtProfileDate.Name = "dtProfileDate";
            this.dtProfileDate.Size = new System.Drawing.Size(101, 20);
            this.dtProfileDate.TabIndex = 34;
            this.dtProfileDate.ValueChanged += new System.EventHandler(this.dtProfileDate_ValueChanged);
            this.dtProfileDate.DropDown += new System.EventHandler(this.dtProfileDate_DropDown);
            this.dtProfileDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtProfileDate_KeyDown);
            // 
            // InspectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1159, 575);
            this.Controls.Add(this.dtProfileDate);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnInternet);
            this.Controls.Add(this.tbLinkedIn);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.lblAverage);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblObservations);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblInspections);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgvScoring);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.scoringGrid);
            this.Controls.Add(this.tbBackground);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnDeletePhoto);
            this.Controls.Add(this.labelPhoto);
            this.Controls.Add(this.btnLoadPhoto);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.chbUnfavourable);
            this.Controls.Add(this.tbFullName);
            this.Controls.Add(this.label1);
            this.Name = "InspectorForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inspector details";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.InspectorForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InspectorForm_FormClosing);
            this.Shown += new System.EventHandler(this.InspectorForm_Shown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProfile)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInspections)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInspectorFiles)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scoringGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScoring)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFullName;
        private System.Windows.Forms.CheckBox chbUnfavourable;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnLoadPhoto;
        private System.Windows.Forms.Label labelPhoto;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnDeletePhoto;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbBackground;
        private System.Windows.Forms.DataGridView dgvProfile;
        private System.Windows.Forms.TextBox tbNotes;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgvInspections;
        private System.Windows.Forms.DataGridView scoringGrid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLoadProfile;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dgvInspectorFiles;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.DataGridView dgvScoring;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblInspections;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblObservations;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblAverage;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbLinkedIn;
        private System.Windows.Forms.Button btnInternet;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtProfileDate;
    }
}