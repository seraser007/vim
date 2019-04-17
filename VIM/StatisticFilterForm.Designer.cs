namespace VIM
{
    partial class StatisticFilterForm
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
            this.panel22 = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.panel21 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.dtValueDate2 = new System.Windows.Forms.DateTimePicker();
            this.dtValueDate = new System.Windows.Forms.DateTimePicker();
            this.cbConditionDate = new System.Windows.Forms.ComboBox();
            this.panel27 = new System.Windows.Forms.Panel();
            this.labelDate = new System.Windows.Forms.Label();
            this.cbValueChapterNumber = new System.Windows.Forms.ComboBox();
            this.cbConditionChapterNumber = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.labelChapterNumber = new System.Windows.Forms.Label();
            this.chbUseMapping = new System.Windows.Forms.CheckBox();
            this.cbValueQType = new System.Windows.Forms.ComboBox();
            this.cbConditionQType = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.chbGroupByType = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cbValueOffice = new System.Windows.Forms.ComboBox();
            this.cbConditionOffice = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelOffice = new System.Windows.Forms.Label();
            this.cbValueDOC = new System.Windows.Forms.ComboBox();
            this.cbConditionDOC = new System.Windows.Forms.ComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblDOC = new System.Windows.Forms.Label();
            this.panel22.SuspendLayout();
            this.panel21.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel27.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel22
            // 
            this.panel22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel22.Controls.Add(this.label20);
            this.panel22.Location = new System.Drawing.Point(252, 4);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(190, 23);
            this.panel22.TabIndex = 67;
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
            this.panel21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel21.Controls.Add(this.label19);
            this.panel21.Location = new System.Drawing.Point(126, 4);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(124, 23);
            this.panel21.TabIndex = 66;
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
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label9);
            this.panel3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panel3.Location = new System.Drawing.Point(2, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(121, 25);
            this.panel3.TabIndex = 64;
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
            // dtValueDate2
            // 
            this.dtValueDate2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtValueDate2.Location = new System.Drawing.Point(352, 29);
            this.dtValueDate2.Name = "dtValueDate2";
            this.dtValueDate2.Size = new System.Drawing.Size(91, 20);
            this.dtValueDate2.TabIndex = 124;
            this.dtValueDate2.Visible = false;
            // 
            // dtValueDate
            // 
            this.dtValueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtValueDate.Location = new System.Drawing.Point(253, 30);
            this.dtValueDate.Name = "dtValueDate";
            this.dtValueDate.Size = new System.Drawing.Size(91, 20);
            this.dtValueDate.TabIndex = 123;
            // 
            // cbConditionDate
            // 
            this.cbConditionDate.FormattingEnabled = true;
            this.cbConditionDate.Items.AddRange(new object[] {
            "",
            "Equal",
            "Not Equal",
            "Between",
            "Not Between",
            "Before",
            "After",
            "On and Before",
            "On and After"});
            this.cbConditionDate.Location = new System.Drawing.Point(126, 29);
            this.cbConditionDate.Name = "cbConditionDate";
            this.cbConditionDate.Size = new System.Drawing.Size(125, 21);
            this.cbConditionDate.TabIndex = 122;
            this.cbConditionDate.TextChanged += new System.EventHandler(this.conditionChapterNumber_TextChanged);
            // 
            // panel27
            // 
            this.panel27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel27.Controls.Add(this.labelDate);
            this.panel27.Location = new System.Drawing.Point(3, 30);
            this.panel27.Name = "panel27";
            this.panel27.Size = new System.Drawing.Size(120, 20);
            this.panel27.TabIndex = 121;
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
            // cbValueChapterNumber
            // 
            this.cbValueChapterNumber.FormattingEnabled = true;
            this.cbValueChapterNumber.Items.AddRange(new object[] {
            "",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "99"});
            this.cbValueChapterNumber.Location = new System.Drawing.Point(253, 52);
            this.cbValueChapterNumber.Name = "cbValueChapterNumber";
            this.cbValueChapterNumber.Size = new System.Drawing.Size(190, 21);
            this.cbValueChapterNumber.TabIndex = 129;
            // 
            // cbConditionChapterNumber
            // 
            this.cbConditionChapterNumber.FormattingEnabled = true;
            this.cbConditionChapterNumber.Items.AddRange(new object[] {
            "",
            "Equal",
            "Not Equal",
            "Starts from",
            "Not Starts from",
            "Contains",
            "Not Contains",
            "Ends on",
            "Not Ends on"});
            this.cbConditionChapterNumber.Location = new System.Drawing.Point(126, 52);
            this.cbConditionChapterNumber.Name = "cbConditionChapterNumber";
            this.cbConditionChapterNumber.Size = new System.Drawing.Size(125, 21);
            this.cbConditionChapterNumber.TabIndex = 128;
            this.cbConditionChapterNumber.TextChanged += new System.EventHandler(this.conditionChapterNumber_TextChanged);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.labelChapterNumber);
            this.panel4.Location = new System.Drawing.Point(3, 52);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(120, 20);
            this.panel4.TabIndex = 126;
            // 
            // labelChapterNumber
            // 
            this.labelChapterNumber.AutoSize = true;
            this.labelChapterNumber.Location = new System.Drawing.Point(4, 2);
            this.labelChapterNumber.Name = "labelChapterNumber";
            this.labelChapterNumber.Size = new System.Drawing.Size(82, 13);
            this.labelChapterNumber.TabIndex = 0;
            this.labelChapterNumber.Text = "Chapter number";
            // 
            // chbUseMapping
            // 
            this.chbUseMapping.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbUseMapping.AutoSize = true;
            this.chbUseMapping.Location = new System.Drawing.Point(7, 176);
            this.chbUseMapping.Name = "chbUseMapping";
            this.chbUseMapping.Size = new System.Drawing.Size(88, 17);
            this.chbUseMapping.TabIndex = 130;
            this.chbUseMapping.Text = "Use mapping";
            this.chbUseMapping.UseVisualStyleBackColor = true;
            // 
            // cbValueQType
            // 
            this.cbValueQType.FormattingEnabled = true;
            this.cbValueQType.Items.AddRange(new object[] {
            "",
            "01",
            "02",
            "03",
            "04"});
            this.cbValueQType.Location = new System.Drawing.Point(253, 75);
            this.cbValueQType.Name = "cbValueQType";
            this.cbValueQType.Size = new System.Drawing.Size(190, 21);
            this.cbValueQType.TabIndex = 136;
            // 
            // cbConditionQType
            // 
            this.cbConditionQType.FormattingEnabled = true;
            this.cbConditionQType.Items.AddRange(new object[] {
            "",
            "Equal",
            "Not Equal",
            "Starts from",
            "Not Starts from",
            "Contains",
            "Not Contains",
            "Ends on",
            "Not Ends on"});
            this.cbConditionQType.Location = new System.Drawing.Point(126, 75);
            this.cbConditionQType.Name = "cbConditionQType";
            this.cbConditionQType.Size = new System.Drawing.Size(125, 21);
            this.cbConditionQType.TabIndex = 135;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(3, 75);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(120, 20);
            this.panel2.TabIndex = 133;
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
            // chbGroupByType
            // 
            this.chbGroupByType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbGroupByType.AutoSize = true;
            this.chbGroupByType.Location = new System.Drawing.Point(7, 153);
            this.chbGroupByType.Name = "chbGroupByType";
            this.chbGroupByType.Size = new System.Drawing.Size(158, 17);
            this.chbGroupByType.TabIndex = 137;
            this.chbGroupByType.Text = "Group by questionnaire type";
            this.chbGroupByType.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.Image = global::VIM.Properties.Resources.Ok;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.Location = new System.Drawing.Point(290, 176);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 25);
            this.button2.TabIndex = 132;
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
            this.button1.Location = new System.Drawing.Point(371, 176);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 131;
            this.button1.Text = "Cancel";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // cbValueOffice
            // 
            this.cbValueOffice.FormattingEnabled = true;
            this.cbValueOffice.Items.AddRange(new object[] {
            "",
            "CY",
            "MAR",
            "NS",
            "SGP",
            "SPB"});
            this.cbValueOffice.Location = new System.Drawing.Point(253, 98);
            this.cbValueOffice.Name = "cbValueOffice";
            this.cbValueOffice.Size = new System.Drawing.Size(190, 21);
            this.cbValueOffice.TabIndex = 140;
            // 
            // cbConditionOffice
            // 
            this.cbConditionOffice.FormattingEnabled = true;
            this.cbConditionOffice.Items.AddRange(new object[] {
            "",
            "Equal",
            "Not Equal",
            "Starts from",
            "Not Starts from",
            "Contains",
            "Not Contains",
            "Ends on",
            "Not Ends on"});
            this.cbConditionOffice.Location = new System.Drawing.Point(126, 98);
            this.cbConditionOffice.Name = "cbConditionOffice";
            this.cbConditionOffice.Size = new System.Drawing.Size(125, 21);
            this.cbConditionOffice.TabIndex = 139;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labelOffice);
            this.panel1.Location = new System.Drawing.Point(3, 98);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(120, 20);
            this.panel1.TabIndex = 138;
            // 
            // labelOffice
            // 
            this.labelOffice.AutoSize = true;
            this.labelOffice.Location = new System.Drawing.Point(4, 2);
            this.labelOffice.Name = "labelOffice";
            this.labelOffice.Size = new System.Drawing.Size(35, 13);
            this.labelOffice.TabIndex = 0;
            this.labelOffice.Text = "Office";
            // 
            // cbValueDOC
            // 
            this.cbValueDOC.FormattingEnabled = true;
            this.cbValueDOC.Items.AddRange(new object[] {
            "",
            "CY",
            "MAR",
            "NS",
            "SGP",
            "SPB"});
            this.cbValueDOC.Location = new System.Drawing.Point(253, 120);
            this.cbValueDOC.Name = "cbValueDOC";
            this.cbValueDOC.Size = new System.Drawing.Size(190, 21);
            this.cbValueDOC.TabIndex = 143;
            // 
            // cbConditionDOC
            // 
            this.cbConditionDOC.FormattingEnabled = true;
            this.cbConditionDOC.Items.AddRange(new object[] {
            "",
            "Equal",
            "Not Equal",
            "Starts from",
            "Not Starts from",
            "Contains",
            "Not Contains",
            "Ends on",
            "Not Ends on"});
            this.cbConditionDOC.Location = new System.Drawing.Point(126, 120);
            this.cbConditionDOC.Name = "cbConditionDOC";
            this.cbConditionDOC.Size = new System.Drawing.Size(125, 21);
            this.cbConditionDOC.TabIndex = 142;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.lblDOC);
            this.panel5.Location = new System.Drawing.Point(3, 120);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(120, 20);
            this.panel5.TabIndex = 141;
            // 
            // lblDOC
            // 
            this.lblDOC.AutoSize = true;
            this.lblDOC.Location = new System.Drawing.Point(4, 2);
            this.lblDOC.Name = "lblDOC";
            this.lblDOC.Size = new System.Drawing.Size(30, 13);
            this.lblDOC.TabIndex = 0;
            this.lblDOC.Text = "DOC";
            // 
            // StatisticFilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 211);
            this.Controls.Add(this.cbValueDOC);
            this.Controls.Add(this.cbConditionDOC);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.cbValueOffice);
            this.Controls.Add(this.cbConditionOffice);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chbGroupByType);
            this.Controls.Add(this.cbValueQType);
            this.Controls.Add(this.cbConditionQType);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chbUseMapping);
            this.Controls.Add(this.cbValueChapterNumber);
            this.Controls.Add(this.cbConditionChapterNumber);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.dtValueDate2);
            this.Controls.Add(this.dtValueDate);
            this.Controls.Add(this.cbConditionDate);
            this.Controls.Add(this.panel27);
            this.Controls.Add(this.panel22);
            this.Controls.Add(this.panel21);
            this.Controls.Add(this.panel3);
            this.Name = "StatisticFilterForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filter for VIQ statistics";
            this.panel22.ResumeLayout(false);
            this.panel22.PerformLayout();
            this.panel21.ResumeLayout(false);
            this.panel21.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel27.ResumeLayout(false);
            this.panel27.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel22;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel panel21;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtValueDate2;
        private System.Windows.Forms.DateTimePicker dtValueDate;
        private System.Windows.Forms.ComboBox cbConditionDate;
        private System.Windows.Forms.Panel panel27;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.ComboBox cbValueChapterNumber;
        private System.Windows.Forms.ComboBox cbConditionChapterNumber;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label labelChapterNumber;
        private System.Windows.Forms.CheckBox chbUseMapping;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cbValueQType;
        private System.Windows.Forms.ComboBox cbConditionQType;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chbGroupByType;
        private System.Windows.Forms.ComboBox cbValueOffice;
        private System.Windows.Forms.ComboBox cbConditionOffice;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelOffice;
        private System.Windows.Forms.ComboBox cbValueDOC;
        private System.Windows.Forms.ComboBox cbConditionDOC;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblDOC;
    }
}