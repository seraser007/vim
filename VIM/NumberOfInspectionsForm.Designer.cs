namespace VIM
{
    partial class NumberOfInspectionsForm
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
            this.valueDate2 = new System.Windows.Forms.DateTimePicker();
            this.valueDate = new System.Windows.Forms.DateTimePicker();
            this.conditionDate = new System.Windows.Forms.ComboBox();
            this.panel27 = new System.Windows.Forms.Panel();
            this.labelDate = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.group3 = new System.Windows.Forms.ComboBox();
            this.group2 = new System.Windows.Forms.ComboBox();
            this.group1 = new System.Windows.Forms.ComboBox();
            this.calcObservations = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel22.SuspendLayout();
            this.panel21.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel27.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel22
            // 
            this.panel22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel22.Controls.Add(this.label20);
            this.panel22.Location = new System.Drawing.Point(259, 9);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(191, 22);
            this.panel22.TabIndex = 71;
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
            this.panel21.Location = new System.Drawing.Point(131, 9);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(125, 22);
            this.panel21.TabIndex = 69;
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
            this.panel3.Location = new System.Drawing.Point(7, 9);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(121, 22);
            this.panel3.TabIndex = 66;
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
            // valueDate2
            // 
            this.valueDate2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.valueDate2.Location = new System.Drawing.Point(359, 37);
            this.valueDate2.Name = "valueDate2";
            this.valueDate2.Size = new System.Drawing.Size(91, 20);
            this.valueDate2.TabIndex = 124;
            this.valueDate2.Visible = false;
            // 
            // valueDate
            // 
            this.valueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.valueDate.Location = new System.Drawing.Point(260, 37);
            this.valueDate.Name = "valueDate";
            this.valueDate.Size = new System.Drawing.Size(93, 20);
            this.valueDate.TabIndex = 123;
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
            this.conditionDate.Location = new System.Drawing.Point(132, 37);
            this.conditionDate.Name = "conditionDate";
            this.conditionDate.Size = new System.Drawing.Size(125, 21);
            this.conditionDate.TabIndex = 122;
            this.conditionDate.TextChanged += new System.EventHandler(this.conditionDate_TextChanged);
            // 
            // panel27
            // 
            this.panel27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel27.Controls.Add(this.labelDate);
            this.panel27.Location = new System.Drawing.Point(7, 37);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.group3);
            this.groupBox1.Controls.Add(this.group2);
            this.groupBox1.Controls.Add(this.group1);
            this.groupBox1.Location = new System.Drawing.Point(7, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(173, 109);
            this.groupBox1.TabIndex = 135;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Group by";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "3.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "2.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "1.";
            // 
            // group3
            // 
            this.group3.FormattingEnabled = true;
            this.group3.Items.AddRange(new object[] {
            "",
            "Vessel",
            "Inspection",
            "Inspector",
            "DOC",
            "Inspecting Company"});
            this.group3.Location = new System.Drawing.Point(37, 73);
            this.group3.Name = "group3";
            this.group3.Size = new System.Drawing.Size(121, 21);
            this.group3.TabIndex = 5;
            // 
            // group2
            // 
            this.group2.FormattingEnabled = true;
            this.group2.Items.AddRange(new object[] {
            "",
            "Vessel",
            "Inspection",
            "Inspector",
            "DOC",
            "Inspecting Company"});
            this.group2.Location = new System.Drawing.Point(37, 46);
            this.group2.Name = "group2";
            this.group2.Size = new System.Drawing.Size(121, 21);
            this.group2.TabIndex = 4;
            // 
            // group1
            // 
            this.group1.FormattingEnabled = true;
            this.group1.Items.AddRange(new object[] {
            "",
            "Vessel",
            "Inspection",
            "Inspector",
            "DOC",
            "Inspecting Company"});
            this.group1.Location = new System.Drawing.Point(37, 19);
            this.group1.Name = "group1";
            this.group1.Size = new System.Drawing.Size(121, 21);
            this.group1.TabIndex = 3;
            this.group1.Text = "Vessel";
            // 
            // calcObservations
            // 
            this.calcObservations.AutoSize = true;
            this.calcObservations.Location = new System.Drawing.Point(263, 72);
            this.calcObservations.Name = "calcObservations";
            this.calcObservations.Size = new System.Drawing.Size(133, 17);
            this.calcObservations.TabIndex = 136;
            this.calcObservations.Text = "Calculate observations";
            this.calcObservations.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Image = global::VIM.Properties.Resources.delete;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.Location = new System.Drawing.Point(376, 187);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 137;
            this.button1.Text = "Cancel";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.Image = global::VIM.Properties.Resources.Ok;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.Location = new System.Drawing.Point(295, 187);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 25);
            this.button2.TabIndex = 138;
            this.button2.Text = "OK";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // NumberOfInspectionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 222);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.calcObservations);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.valueDate2);
            this.Controls.Add(this.valueDate);
            this.Controls.Add(this.conditionDate);
            this.Controls.Add(this.panel27);
            this.Controls.Add(this.panel22);
            this.Controls.Add(this.panel21);
            this.Controls.Add(this.panel3);
            this.Name = "NumberOfInspectionsForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Number Of Inspections Report Settings";
            this.panel22.ResumeLayout(false);
            this.panel22.PerformLayout();
            this.panel21.ResumeLayout(false);
            this.panel21.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel27.ResumeLayout(false);
            this.panel27.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.DateTimePicker valueDate2;
        private System.Windows.Forms.DateTimePicker valueDate;
        private System.Windows.Forms.ComboBox conditionDate;
        private System.Windows.Forms.Panel panel27;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox calcObservations;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox group3;
        private System.Windows.Forms.ComboBox group2;
        private System.Windows.Forms.ComboBox group1;
    }
}