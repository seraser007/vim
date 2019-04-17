namespace VIM
{
    partial class frmMonthlyBulletin
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnOpen = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSubject = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtIssue = new System.Windows.Forms.DateTimePicker();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtTill = new System.Windows.Forms.DateTimePicker();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.btnTextColor = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chbIncludePeriod = new System.Windows.Forms.CheckBox();
            this.rbtnPeriod = new System.Windows.Forms.RadioButton();
            this.rbtnMonth = new System.Windows.Forms.RadioButton();
            this.gbComments = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbSelectedStyle = new System.Windows.Forms.TextBox();
            this.tbTextColor = new System.Windows.Forms.TextBox();
            this.rbtnSelectedStyle = new System.Windows.Forms.RadioButton();
            this.rbtnSelectedColor = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbtnCommonComments = new System.Windows.Forms.RadioButton();
            this.rbtnOperatorComments = new System.Windows.Forms.RadioButton();
            this.tbComments = new System.Windows.Forms.TextBox();
            this.tbLabel = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chbUseComments = new System.Windows.Forms.CheckBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.cbInspectionType = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.monthSlector = new VIM.UserControlMonthSelector();
            this.groupBox1.SuspendLayout();
            this.gbComments.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bulletin template";
            // 
            // tbFileName
            // 
            this.tbFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFileName.Location = new System.Drawing.Point(12, 73);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(356, 20);
            this.tbFileName.TabIndex = 2;
            this.tbFileName.Text = "Vetting Bulletin template.doc";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "doc";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Word files (*.doc,*.docx)|*.doc;*.docx|All files (*.*)|*.*";
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Image = global::VIM.Properties.Resources.open;
            this.btnOpen.Location = new System.Drawing.Point(369, 71);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(23, 23);
            this.btnOpen.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnOpen, "Select template file");
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Subject";
            // 
            // tbSubject
            // 
            this.tbSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSubject.Location = new System.Drawing.Point(12, 111);
            this.tbSubject.Name = "tbSubject";
            this.tbSubject.Size = new System.Drawing.Size(356, 20);
            this.tbSubject.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Issue date";
            // 
            // dtIssue
            // 
            this.dtIssue.Location = new System.Drawing.Point(12, 149);
            this.dtIssue.Name = "dtIssue";
            this.dtIssue.Size = new System.Drawing.Size(200, 20);
            this.dtIssue.TabIndex = 5;
            // 
            // dtFrom
            // 
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFrom.Location = new System.Drawing.Point(52, 45);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(103, 20);
            this.dtFrom.TabIndex = 9;
            this.dtFrom.ValueChanged += new System.EventHandler(this.dtFrom_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(161, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "till";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // dtTill
            // 
            this.dtTill.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtTill.Location = new System.Drawing.Point(183, 45);
            this.dtTill.Name = "dtTill";
            this.dtTill.Size = new System.Drawing.Size(103, 20);
            this.dtTill.TabIndex = 10;
            this.dtTill.ValueChanged += new System.EventHandler(this.dtTill_ValueChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::VIM.Properties.Resources.Close_Box_Red;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.Location = new System.Drawing.Point(823, 307);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::VIM.Properties.Resources.Ok;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.Location = new System.Drawing.Point(742, 307);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 13;
            this.btnOK.Text = "OK";
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = global::VIM.Properties.Resources.save;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.Location = new System.Drawing.Point(661, 307);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Save";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnSave, "Save template name and subject");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnTextColor
            // 
            this.btnTextColor.Image = global::VIM.Properties.Resources.Select_Color;
            this.btnTextColor.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTextColor.Location = new System.Drawing.Point(172, 26);
            this.btnTextColor.Name = "btnTextColor";
            this.btnTextColor.Size = new System.Drawing.Size(23, 23);
            this.btnTextColor.TabIndex = 19;
            this.toolTip1.SetToolTip(this.btnTextColor, "Select text color");
            this.btnTextColor.UseVisualStyleBackColor = true;
            this.btnTextColor.Click += new System.EventHandler(this.btnTextColor_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chbIncludePeriod);
            this.groupBox1.Controls.Add(this.monthSlector);
            this.groupBox1.Controls.Add(this.rbtnPeriod);
            this.groupBox1.Controls.Add(this.rbtnMonth);
            this.groupBox1.Controls.Add(this.dtTill);
            this.groupBox1.Controls.Add(this.dtFrom);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(12, 177);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(293, 97);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Period";
            // 
            // chbIncludePeriod
            // 
            this.chbIncludePeriod.AutoSize = true;
            this.chbIncludePeriod.Location = new System.Drawing.Point(6, 73);
            this.chbIncludePeriod.Name = "chbIncludePeriod";
            this.chbIncludePeriod.Size = new System.Drawing.Size(150, 17);
            this.chbIncludePeriod.TabIndex = 11;
            this.chbIncludePeriod.Text = "Include period into subject";
            this.chbIncludePeriod.UseVisualStyleBackColor = true;
            // 
            // rbtnPeriod
            // 
            this.rbtnPeriod.AutoSize = true;
            this.rbtnPeriod.Location = new System.Drawing.Point(6, 46);
            this.rbtnPeriod.Name = "rbtnPeriod";
            this.rbtnPeriod.Size = new System.Drawing.Size(45, 17);
            this.rbtnPeriod.TabIndex = 8;
            this.rbtnPeriod.Text = "from";
            this.rbtnPeriod.UseVisualStyleBackColor = true;
            // 
            // rbtnMonth
            // 
            this.rbtnMonth.AutoSize = true;
            this.rbtnMonth.Checked = true;
            this.rbtnMonth.Location = new System.Drawing.Point(6, 19);
            this.rbtnMonth.Name = "rbtnMonth";
            this.rbtnMonth.Size = new System.Drawing.Size(54, 17);
            this.rbtnMonth.TabIndex = 6;
            this.rbtnMonth.TabStop = true;
            this.rbtnMonth.Text = "month";
            this.rbtnMonth.UseVisualStyleBackColor = true;
            // 
            // gbComments
            // 
            this.gbComments.Controls.Add(this.groupBox3);
            this.gbComments.Controls.Add(this.groupBox2);
            this.gbComments.Controls.Add(this.tbLabel);
            this.gbComments.Controls.Add(this.label4);
            this.gbComments.Controls.Add(this.chbUseComments);
            this.gbComments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbComments.Location = new System.Drawing.Point(3, 3);
            this.gbComments.Name = "gbComments";
            this.gbComments.Size = new System.Drawing.Size(497, 288);
            this.gbComments.TabIndex = 7;
            this.gbComments.TabStop = false;
            this.gbComments.Text = "Unified comments";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnTextColor);
            this.groupBox3.Controls.Add(this.tbSelectedStyle);
            this.groupBox3.Controls.Add(this.tbTextColor);
            this.groupBox3.Controls.Add(this.rbtnSelectedStyle);
            this.groupBox3.Controls.Add(this.rbtnSelectedColor);
            this.groupBox3.Location = new System.Drawing.Point(6, 174);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(478, 90);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Comments text color";
            // 
            // tbSelectedStyle
            // 
            this.tbSelectedStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSelectedStyle.Location = new System.Drawing.Point(105, 60);
            this.tbSelectedStyle.Name = "tbSelectedStyle";
            this.tbSelectedStyle.Size = new System.Drawing.Size(361, 20);
            this.tbSelectedStyle.TabIndex = 21;
            // 
            // tbTextColor
            // 
            this.tbTextColor.BackColor = System.Drawing.SystemColors.Window;
            this.tbTextColor.Location = new System.Drawing.Point(105, 28);
            this.tbTextColor.Name = "tbTextColor";
            this.tbTextColor.ReadOnly = true;
            this.tbTextColor.Size = new System.Drawing.Size(66, 20);
            this.tbTextColor.TabIndex = 18;
            this.tbTextColor.Text = "Text";
            // 
            // rbtnSelectedStyle
            // 
            this.rbtnSelectedStyle.AutoSize = true;
            this.rbtnSelectedStyle.Location = new System.Drawing.Point(6, 61);
            this.rbtnSelectedStyle.Name = "rbtnSelectedStyle";
            this.rbtnSelectedStyle.Size = new System.Drawing.Size(91, 17);
            this.rbtnSelectedStyle.TabIndex = 20;
            this.rbtnSelectedStyle.Text = "Selected style";
            this.rbtnSelectedStyle.UseVisualStyleBackColor = true;
            // 
            // rbtnSelectedColor
            // 
            this.rbtnSelectedColor.AutoSize = true;
            this.rbtnSelectedColor.Checked = true;
            this.rbtnSelectedColor.Location = new System.Drawing.Point(6, 28);
            this.rbtnSelectedColor.Name = "rbtnSelectedColor";
            this.rbtnSelectedColor.Size = new System.Drawing.Size(93, 17);
            this.rbtnSelectedColor.TabIndex = 17;
            this.rbtnSelectedColor.TabStop = true;
            this.rbtnSelectedColor.Text = "Selected color";
            this.rbtnSelectedColor.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.rbtnCommonComments);
            this.groupBox2.Controls.Add(this.rbtnOperatorComments);
            this.groupBox2.Controls.Add(this.tbComments);
            this.groupBox2.Location = new System.Drawing.Point(6, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(478, 84);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Comments text";
            // 
            // rbtnCommonComments
            // 
            this.rbtnCommonComments.AutoSize = true;
            this.rbtnCommonComments.Location = new System.Drawing.Point(6, 19);
            this.rbtnCommonComments.Name = "rbtnCommonComments";
            this.rbtnCommonComments.Size = new System.Drawing.Size(117, 17);
            this.rbtnCommonComments.TabIndex = 14;
            this.rbtnCommonComments.Text = "Common comments";
            this.rbtnCommonComments.UseVisualStyleBackColor = true;
            // 
            // rbtnOperatorComments
            // 
            this.rbtnOperatorComments.AutoSize = true;
            this.rbtnOperatorComments.Checked = true;
            this.rbtnOperatorComments.Location = new System.Drawing.Point(6, 63);
            this.rbtnOperatorComments.Name = "rbtnOperatorComments";
            this.rbtnOperatorComments.Size = new System.Drawing.Size(137, 17);
            this.rbtnOperatorComments.TabIndex = 16;
            this.rbtnOperatorComments.TabStop = true;
            this.rbtnOperatorComments.Text = "Use operator comments";
            this.rbtnOperatorComments.UseVisualStyleBackColor = true;
            // 
            // tbComments
            // 
            this.tbComments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbComments.Location = new System.Drawing.Point(25, 37);
            this.tbComments.Name = "tbComments";
            this.tbComments.Size = new System.Drawing.Size(441, 20);
            this.tbComments.TabIndex = 15;
            // 
            // tbLabel
            // 
            this.tbLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLabel.Location = new System.Drawing.Point(25, 55);
            this.tbLabel.Name = "tbLabel";
            this.tbLabel.Size = new System.Drawing.Size(447, 20);
            this.tbLabel.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Comments label";
            // 
            // chbUseComments
            // 
            this.chbUseComments.AutoSize = true;
            this.chbUseComments.Location = new System.Drawing.Point(9, 19);
            this.chbUseComments.Name = "chbUseComments";
            this.chbUseComments.Size = new System.Drawing.Size(103, 17);
            this.chbUseComments.TabIndex = 12;
            this.chbUseComments.Text = "Insert comments";
            this.chbUseComments.UseVisualStyleBackColor = true;
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            this.colorDialog1.FullOpen = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Inspection type";
            // 
            // cbInspectionType
            // 
            this.cbInspectionType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbInspectionType.FormattingEnabled = true;
            this.cbInspectionType.Location = new System.Drawing.Point(12, 31);
            this.cbInspectionType.Name = "cbInspectionType";
            this.cbInspectionType.Size = new System.Drawing.Size(356, 21);
            this.cbInspectionType.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.cbInspectionType);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.tbFileName);
            this.splitContainer1.Panel1.Controls.Add(this.btnOpen);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.tbSubject);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.dtIssue);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gbComments);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.splitContainer1.Size = new System.Drawing.Size(913, 296);
            this.splitContainer1.SplitterDistance = 404;
            this.splitContainer1.TabIndex = 21;
            // 
            // monthSlector
            // 
            this.monthSlector.Location = new System.Drawing.Point(62, 17);
            this.monthSlector.Name = "monthSlector";
            this.monthSlector.selectedMonth = 12;
            this.monthSlector.selectedYear = 2016;
            this.monthSlector.Size = new System.Drawing.Size(224, 22);
            this.monthSlector.TabIndex = 7;
            // 
            // frmMonthlyBulletin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(913, 342);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Name = "frmMonthlyBulletin";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Monthly Bulletin Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMonthlyBulletin_FormClosing);
            this.Load += new System.EventHandler(this.frmMonthlyBulletin_Load);
            this.Shown += new System.EventHandler(this.frmMonthlyBulletin_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbComments.ResumeLayout(false);
            this.gbComments.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbSubject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtIssue;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtTill;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnPeriod;
        private System.Windows.Forms.RadioButton rbtnMonth;
        private UserControlMonthSelector monthSlector;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chbIncludePeriod;
        private System.Windows.Forms.GroupBox gbComments;
        private System.Windows.Forms.TextBox tbComments;
        private System.Windows.Forms.TextBox tbLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chbUseComments;
        private System.Windows.Forms.RadioButton rbtnOperatorComments;
        private System.Windows.Forms.RadioButton rbtnCommonComments;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnTextColor;
        private System.Windows.Forms.TextBox tbSelectedStyle;
        private System.Windows.Forms.TextBox tbTextColor;
        private System.Windows.Forms.RadioButton rbtnSelectedStyle;
        private System.Windows.Forms.RadioButton rbtnSelectedColor;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbInspectionType;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}