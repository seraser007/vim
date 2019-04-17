namespace VIM
{
    partial class StatisticRoFilterForm
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
            this.groupByType = new System.Windows.Forms.CheckBox();
            this.valueQType = new System.Windows.Forms.ComboBox();
            this.conditionQType = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.useMapping = new System.Windows.Forms.CheckBox();
            this.valueChapterName = new System.Windows.Forms.ComboBox();
            this.conditionChapterName = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.labelChapterNumber = new System.Windows.Forms.Label();
            this.valueDate2 = new System.Windows.Forms.DateTimePicker();
            this.valueDate = new System.Windows.Forms.DateTimePicker();
            this.conditionDate = new System.Windows.Forms.ComboBox();
            this.panel27 = new System.Windows.Forms.Panel();
            this.labelDate = new System.Windows.Forms.Label();
            this.panel22 = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.panel21 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel27.SuspendLayout();
            this.panel22.SuspendLayout();
            this.panel21.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupByType
            // 
            this.groupByType.AutoSize = true;
            this.groupByType.Location = new System.Drawing.Point(13, 105);
            this.groupByType.Name = "groupByType";
            this.groupByType.Size = new System.Drawing.Size(158, 17);
            this.groupByType.TabIndex = 154;
            this.groupByType.Text = "Group by questionnaire type";
            this.groupByType.UseVisualStyleBackColor = true;
            // 
            // valueQType
            // 
            this.valueQType.FormattingEnabled = true;
            this.valueQType.Items.AddRange(new object[] {
            "",
            "01",
            "02",
            "03",
            "04"});
            this.valueQType.Location = new System.Drawing.Point(259, 79);
            this.valueQType.Name = "valueQType";
            this.valueQType.Size = new System.Drawing.Size(190, 21);
            this.valueQType.TabIndex = 153;
            // 
            // conditionQType
            // 
            this.conditionQType.FormattingEnabled = true;
            this.conditionQType.Items.AddRange(new object[] {
            "",
            "Equal",
            "Not Equal",
            "Starts from",
            "Not Starts from",
            "Contains",
            "Not Contains",
            "Ends on",
            "Not Ends on"});
            this.conditionQType.Location = new System.Drawing.Point(132, 79);
            this.conditionQType.Name = "conditionQType";
            this.conditionQType.Size = new System.Drawing.Size(125, 21);
            this.conditionQType.TabIndex = 152;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(9, 79);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(120, 20);
            this.panel2.TabIndex = 151;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Questionnaire type";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.Image = global::VIM.Properties.Resources.Ok;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.Location = new System.Drawing.Point(291, 131);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 25);
            this.button2.TabIndex = 150;
            this.button2.Text = "OK";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Image = global::VIM.Properties.Resources.delete;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.Location = new System.Drawing.Point(372, 131);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 149;
            this.button1.Text = "Cancel";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // useMapping
            // 
            this.useMapping.AutoSize = true;
            this.useMapping.Location = new System.Drawing.Point(13, 127);
            this.useMapping.Name = "useMapping";
            this.useMapping.Size = new System.Drawing.Size(88, 17);
            this.useMapping.TabIndex = 148;
            this.useMapping.Text = "Use mapping";
            this.useMapping.UseVisualStyleBackColor = true;
            // 
            // valueChapterName
            // 
            this.valueChapterName.FormattingEnabled = true;
            this.valueChapterName.Location = new System.Drawing.Point(259, 57);
            this.valueChapterName.Name = "valueChapterName";
            this.valueChapterName.Size = new System.Drawing.Size(190, 21);
            this.valueChapterName.TabIndex = 147;
            // 
            // conditionChapterName
            // 
            this.conditionChapterName.FormattingEnabled = true;
            this.conditionChapterName.Items.AddRange(new object[] {
            "",
            "Equal",
            "Not Equal",
            "Starts from",
            "Not Starts from",
            "Contains",
            "Not Contains",
            "Ends on",
            "Not Ends on"});
            this.conditionChapterName.Location = new System.Drawing.Point(132, 57);
            this.conditionChapterName.Name = "conditionChapterName";
            this.conditionChapterName.Size = new System.Drawing.Size(125, 21);
            this.conditionChapterName.TabIndex = 146;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.labelChapterNumber);
            this.panel4.Location = new System.Drawing.Point(9, 57);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(120, 20);
            this.panel4.TabIndex = 145;
            // 
            // labelChapterNumber
            // 
            this.labelChapterNumber.AutoSize = true;
            this.labelChapterNumber.Location = new System.Drawing.Point(4, 2);
            this.labelChapterNumber.Name = "labelChapterNumber";
            this.labelChapterNumber.Size = new System.Drawing.Size(73, 13);
            this.labelChapterNumber.TabIndex = 0;
            this.labelChapterNumber.Text = "Chapter name";
            // 
            // valueDate2
            // 
            this.valueDate2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.valueDate2.Location = new System.Drawing.Point(358, 35);
            this.valueDate2.Name = "valueDate2";
            this.valueDate2.Size = new System.Drawing.Size(91, 20);
            this.valueDate2.TabIndex = 144;
            this.valueDate2.Visible = false;
            // 
            // valueDate
            // 
            this.valueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.valueDate.Location = new System.Drawing.Point(259, 36);
            this.valueDate.Name = "valueDate";
            this.valueDate.Size = new System.Drawing.Size(86, 20);
            this.valueDate.TabIndex = 143;
            // 
            // conditionDate
            // 
            this.conditionDate.FormattingEnabled = true;
            this.conditionDate.Items.AddRange(new object[] {
            "",
            "Equal",
            "Not Equal",
            "Between",
            "Not Between",
            "Before",
            "After",
            "On and Before",
            "On and After"});
            this.conditionDate.Location = new System.Drawing.Point(132, 36);
            this.conditionDate.Name = "conditionDate";
            this.conditionDate.Size = new System.Drawing.Size(125, 21);
            this.conditionDate.TabIndex = 142;
            this.conditionDate.TextChanged += new System.EventHandler(this.conditionDate_TextChanged);
            // 
            // panel27
            // 
            this.panel27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel27.Controls.Add(this.labelDate);
            this.panel27.Location = new System.Drawing.Point(9, 36);
            this.panel27.Name = "panel27";
            this.panel27.Size = new System.Drawing.Size(120, 20);
            this.panel27.TabIndex = 141;
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Location = new System.Drawing.Point(4, 2);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(93, 13);
            this.labelDate.TabIndex = 0;
            this.labelDate.Text = "Date of inspection";
            // 
            // panel22
            // 
            this.panel22.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel22.Controls.Add(this.label20);
            this.panel22.Location = new System.Drawing.Point(259, 10);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(190, 23);
            this.panel22.TabIndex = 140;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label20.Location = new System.Drawing.Point(4, 2);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(38, 13);
            this.label20.TabIndex = 0;
            this.label20.Text = "Value";
            // 
            // panel21
            // 
            this.panel21.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel21.Controls.Add(this.label19);
            this.panel21.Location = new System.Drawing.Point(132, 10);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(124, 23);
            this.panel21.TabIndex = 139;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label19.Location = new System.Drawing.Point(-1, 2);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(60, 13);
            this.label19.TabIndex = 0;
            this.label19.Text = "Condition";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.label9);
            this.panel3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panel3.Location = new System.Drawing.Point(8, 9);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(121, 25);
            this.panel3.TabIndex = 138;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Field";
            // 
            // StatisticRoFilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 165);
            this.Controls.Add(this.groupByType);
            this.Controls.Add(this.valueQType);
            this.Controls.Add(this.conditionQType);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.useMapping);
            this.Controls.Add(this.valueChapterName);
            this.Controls.Add(this.conditionChapterName);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.valueDate2);
            this.Controls.Add(this.valueDate);
            this.Controls.Add(this.conditionDate);
            this.Controls.Add(this.panel27);
            this.Controls.Add(this.panel22);
            this.Controls.Add(this.panel21);
            this.Controls.Add(this.panel3);
            this.Name = "StatisticRoFilterForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filter for RoVIQ statistics";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel27.ResumeLayout(false);
            this.panel27.PerformLayout();
            this.panel22.ResumeLayout(false);
            this.panel22.PerformLayout();
            this.panel21.ResumeLayout(false);
            this.panel21.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.CheckBox groupByType;
        public System.Windows.Forms.ComboBox valueQType;
        public System.Windows.Forms.ComboBox conditionQType;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.CheckBox useMapping;
        public System.Windows.Forms.ComboBox valueChapterName;
        public System.Windows.Forms.ComboBox conditionChapterName;
        private System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.Label labelChapterNumber;
        public System.Windows.Forms.DateTimePicker valueDate2;
        public System.Windows.Forms.DateTimePicker valueDate;
        public System.Windows.Forms.ComboBox conditionDate;
        private System.Windows.Forms.Panel panel27;
        public System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Panel panel22;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel panel21;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label9;
    }
}