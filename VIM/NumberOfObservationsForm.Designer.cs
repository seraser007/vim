namespace VIM
{
    partial class NumberOfObservationsForm
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
            this.cbValueInspector = new SuggestComboBox20.SuggestComboBox20();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelInspector = new System.Windows.Forms.Label();
            this.cbValueVessel = new System.Windows.Forms.ComboBox();
            this.panel22 = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.cbConditionInspector = new System.Windows.Forms.ComboBox();
            this.cbConditionVessel = new System.Windows.Forms.ComboBox();
            this.panel21 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.labelVessel = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.cbValueObservation = new System.Windows.Forms.TextBox();
            this.cbConditionObservation = new System.Windows.Forms.ComboBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.labelObservation = new System.Windows.Forms.Label();
            this.cbValueOffice = new System.Windows.Forms.ComboBox();
            this.cbValueCompany = new System.Windows.Forms.ComboBox();
            this.dtValueDate2 = new System.Windows.Forms.DateTimePicker();
            this.dtValueDate = new System.Windows.Forms.DateTimePicker();
            this.cbConditionOffice = new System.Windows.Forms.ComboBox();
            this.panel24 = new System.Windows.Forms.Panel();
            this.labelOffice = new System.Windows.Forms.Label();
            this.cbConditionCompany = new System.Windows.Forms.ComboBox();
            this.panel26 = new System.Windows.Forms.Panel();
            this.labelCompany = new System.Windows.Forms.Label();
            this.cbConditionDate = new System.Windows.Forms.ComboBox();
            this.panel27 = new System.Windows.Forms.Panel();
            this.labelDate = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.chbUseMapping = new System.Windows.Forms.CheckBox();
            this.chbIgnore = new System.Windows.Forms.CheckBox();
            this.cbValueDOC = new System.Windows.Forms.ComboBox();
            this.cbConditionDOC = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelDOC = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel22.SuspendLayout();
            this.panel21.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel24.SuspendLayout();
            this.panel26.SuspendLayout();
            this.panel27.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbValueInspector
            // 
            this.cbValueInspector.FormattingEnabled = true;
            this.cbValueInspector.Location = new System.Drawing.Point(259, 54);
            this.cbValueInspector.Name = "cbValueInspector";
            this.cbValueInspector.SearchRule = SuggestComboBox20.SuggestComboBox20.searchRules.cbsContains;
            this.cbValueInspector.Size = new System.Drawing.Size(190, 21);
            this.cbValueInspector.SuggestBoxHeight = 96;
            this.cbValueInspector.TabIndex = 92;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.labelInspector);
            this.panel2.Location = new System.Drawing.Point(8, 55);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(120, 20);
            this.panel2.TabIndex = 91;
            // 
            // labelInspector
            // 
            this.labelInspector.AutoSize = true;
            this.labelInspector.Location = new System.Drawing.Point(4, 2);
            this.labelInspector.Name = "labelInspector";
            this.labelInspector.Size = new System.Drawing.Size(51, 13);
            this.labelInspector.TabIndex = 0;
            this.labelInspector.Text = "Inspector";
            // 
            // cbValueVessel
            // 
            this.cbValueVessel.FormattingEnabled = true;
            this.cbValueVessel.Location = new System.Drawing.Point(259, 32);
            this.cbValueVessel.Name = "cbValueVessel";
            this.cbValueVessel.Size = new System.Drawing.Size(190, 21);
            this.cbValueVessel.TabIndex = 90;
            // 
            // panel22
            // 
            this.panel22.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel22.Controls.Add(this.label20);
            this.panel22.Location = new System.Drawing.Point(259, 6);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(190, 25);
            this.panel22.TabIndex = 89;
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
            // cbConditionInspector
            // 
            this.cbConditionInspector.FormattingEnabled = true;
            this.cbConditionInspector.Items.AddRange(new object[] {
            "",
            "Equal",
            "Not Equal",
            "Starts from",
            "Not Starts from",
            "Contains",
            "Not Contains",
            "Ends on",
            "Not Ends on"});
            this.cbConditionInspector.Location = new System.Drawing.Point(132, 54);
            this.cbConditionInspector.Name = "cbConditionInspector";
            this.cbConditionInspector.Size = new System.Drawing.Size(125, 21);
            this.cbConditionInspector.TabIndex = 88;
            // 
            // cbConditionVessel
            // 
            this.cbConditionVessel.FormattingEnabled = true;
            this.cbConditionVessel.Items.AddRange(new object[] {
            "",
            "Equal",
            "Not Equal",
            "Starts from",
            "Not Starts from",
            "Contains",
            "Not Contains",
            "Ends on",
            "Not Ends on"});
            this.cbConditionVessel.Location = new System.Drawing.Point(132, 32);
            this.cbConditionVessel.Name = "cbConditionVessel";
            this.cbConditionVessel.Size = new System.Drawing.Size(125, 21);
            this.cbConditionVessel.TabIndex = 87;
            // 
            // panel21
            // 
            this.panel21.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel21.Controls.Add(this.label19);
            this.panel21.Location = new System.Drawing.Point(131, 6);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(125, 25);
            this.panel21.TabIndex = 86;
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
            // panel11
            // 
            this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel11.Controls.Add(this.labelVessel);
            this.panel11.Location = new System.Drawing.Point(8, 33);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(120, 20);
            this.panel11.TabIndex = 84;
            // 
            // labelVessel
            // 
            this.labelVessel.AutoSize = true;
            this.labelVessel.Location = new System.Drawing.Point(4, 2);
            this.labelVessel.Name = "labelVessel";
            this.labelVessel.Size = new System.Drawing.Size(38, 13);
            this.labelVessel.TabIndex = 0;
            this.labelVessel.Text = "Vessel";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.label9);
            this.panel3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panel3.Location = new System.Drawing.Point(8, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(121, 25);
            this.panel3.TabIndex = 81;
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
            // cbValueObservation
            // 
            this.cbValueObservation.Location = new System.Drawing.Point(259, 76);
            this.cbValueObservation.Name = "cbValueObservation";
            this.cbValueObservation.Size = new System.Drawing.Size(190, 20);
            this.cbValueObservation.TabIndex = 97;
            // 
            // cbConditionObservation
            // 
            this.cbConditionObservation.FormattingEnabled = true;
            this.cbConditionObservation.Items.AddRange(new object[] {
            "",
            "Equal",
            "Not Equal",
            "Starts from",
            "Not Starts from",
            "Contains",
            "Not Contains",
            "Ends on",
            "Not Ends on",
            "Is Empty",
            "Is Not Empty"});
            this.cbConditionObservation.Location = new System.Drawing.Point(132, 76);
            this.cbConditionObservation.Name = "cbConditionObservation";
            this.cbConditionObservation.Size = new System.Drawing.Size(125, 21);
            this.cbConditionObservation.TabIndex = 96;
            // 
            // panel9
            // 
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Controls.Add(this.labelObservation);
            this.panel9.Location = new System.Drawing.Point(8, 77);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(120, 20);
            this.panel9.TabIndex = 94;
            // 
            // labelObservation
            // 
            this.labelObservation.AutoSize = true;
            this.labelObservation.Location = new System.Drawing.Point(4, 2);
            this.labelObservation.Name = "labelObservation";
            this.labelObservation.Size = new System.Drawing.Size(96, 13);
            this.labelObservation.TabIndex = 0;
            this.labelObservation.Text = "Observation cause";
            // 
            // cbValueOffice
            // 
            this.cbValueOffice.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbValueOffice.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbValueOffice.FormattingEnabled = true;
            this.cbValueOffice.Location = new System.Drawing.Point(259, 142);
            this.cbValueOffice.Name = "cbValueOffice";
            this.cbValueOffice.Size = new System.Drawing.Size(190, 21);
            this.cbValueOffice.TabIndex = 133;
            // 
            // cbValueCompany
            // 
            this.cbValueCompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbValueCompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbValueCompany.FormattingEnabled = true;
            this.cbValueCompany.Location = new System.Drawing.Point(259, 120);
            this.cbValueCompany.Name = "cbValueCompany";
            this.cbValueCompany.Size = new System.Drawing.Size(190, 21);
            this.cbValueCompany.TabIndex = 132;
            // 
            // dtValueDate2
            // 
            this.dtValueDate2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtValueDate2.Location = new System.Drawing.Point(358, 98);
            this.dtValueDate2.Name = "dtValueDate2";
            this.dtValueDate2.Size = new System.Drawing.Size(91, 20);
            this.dtValueDate2.TabIndex = 131;
            this.dtValueDate2.Visible = false;
            // 
            // dtValueDate
            // 
            this.dtValueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtValueDate.Location = new System.Drawing.Point(259, 98);
            this.dtValueDate.Name = "dtValueDate";
            this.dtValueDate.Size = new System.Drawing.Size(91, 20);
            this.dtValueDate.TabIndex = 130;
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
            this.cbConditionOffice.Location = new System.Drawing.Point(132, 142);
            this.cbConditionOffice.Name = "cbConditionOffice";
            this.cbConditionOffice.Size = new System.Drawing.Size(125, 21);
            this.cbConditionOffice.TabIndex = 129;
            // 
            // panel24
            // 
            this.panel24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel24.Controls.Add(this.labelOffice);
            this.panel24.Location = new System.Drawing.Point(8, 143);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(120, 20);
            this.panel24.TabIndex = 128;
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
            // cbConditionCompany
            // 
            this.cbConditionCompany.FormattingEnabled = true;
            this.cbConditionCompany.Items.AddRange(new object[] {
            "",
            "Equal",
            "Not Equal",
            "Starts from",
            "Not Starts from",
            "Contains",
            "Not Contains",
            "Ends on",
            "Not Ends on"});
            this.cbConditionCompany.Location = new System.Drawing.Point(132, 120);
            this.cbConditionCompany.Name = "cbConditionCompany";
            this.cbConditionCompany.Size = new System.Drawing.Size(125, 21);
            this.cbConditionCompany.TabIndex = 127;
            // 
            // panel26
            // 
            this.panel26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel26.Controls.Add(this.labelCompany);
            this.panel26.Location = new System.Drawing.Point(8, 121);
            this.panel26.Name = "panel26";
            this.panel26.Size = new System.Drawing.Size(120, 20);
            this.panel26.TabIndex = 126;
            // 
            // labelCompany
            // 
            this.labelCompany.AutoSize = true;
            this.labelCompany.Location = new System.Drawing.Point(4, 2);
            this.labelCompany.Name = "labelCompany";
            this.labelCompany.Size = new System.Drawing.Size(51, 13);
            this.labelCompany.TabIndex = 0;
            this.labelCompany.Text = "Company";
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
            this.cbConditionDate.Location = new System.Drawing.Point(132, 98);
            this.cbConditionDate.Name = "cbConditionDate";
            this.cbConditionDate.Size = new System.Drawing.Size(125, 21);
            this.cbConditionDate.TabIndex = 125;
            this.cbConditionDate.TextChanged += new System.EventHandler(this.conditionDate_TextChanged);
            // 
            // panel27
            // 
            this.panel27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel27.Controls.Add(this.labelDate);
            this.panel27.Location = new System.Drawing.Point(8, 99);
            this.panel27.Name = "panel27";
            this.panel27.Size = new System.Drawing.Size(120, 20);
            this.panel27.TabIndex = 124;
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
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Image = global::VIM.Properties.Resources.delete;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.Location = new System.Drawing.Point(372, 258);
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
            this.button2.Location = new System.Drawing.Point(291, 258);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 25);
            this.button2.TabIndex = 138;
            this.button2.Text = "OK";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // chbUseMapping
            // 
            this.chbUseMapping.AutoSize = true;
            this.chbUseMapping.Location = new System.Drawing.Point(8, 197);
            this.chbUseMapping.Name = "chbUseMapping";
            this.chbUseMapping.Size = new System.Drawing.Size(136, 17);
            this.chbUseMapping.TabIndex = 139;
            this.chbUseMapping.Text = "Use questions mapping";
            this.chbUseMapping.UseVisualStyleBackColor = true;
            // 
            // chbIgnore
            // 
            this.chbIgnore.AutoSize = true;
            this.chbIgnore.Location = new System.Drawing.Point(8, 220);
            this.chbIgnore.Name = "chbIgnore";
            this.chbIgnore.Size = new System.Drawing.Size(171, 17);
            this.chbIgnore.TabIndex = 140;
            this.chbIgnore.Text = "Ignore questions without GUID";
            this.chbIgnore.UseVisualStyleBackColor = true;
            // 
            // cbValueDOC
            // 
            this.cbValueDOC.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbValueDOC.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbValueDOC.FormattingEnabled = true;
            this.cbValueDOC.Location = new System.Drawing.Point(259, 165);
            this.cbValueDOC.Name = "cbValueDOC";
            this.cbValueDOC.Size = new System.Drawing.Size(190, 21);
            this.cbValueDOC.TabIndex = 144;
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
            this.cbConditionDOC.Location = new System.Drawing.Point(132, 165);
            this.cbConditionDOC.Name = "cbConditionDOC";
            this.cbConditionDOC.Size = new System.Drawing.Size(125, 21);
            this.cbConditionDOC.TabIndex = 143;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labelDOC);
            this.panel1.Location = new System.Drawing.Point(8, 166);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(120, 20);
            this.panel1.TabIndex = 142;
            // 
            // labelDOC
            // 
            this.labelDOC.AutoSize = true;
            this.labelDOC.Location = new System.Drawing.Point(4, 2);
            this.labelDOC.Name = "labelDOC";
            this.labelDOC.Size = new System.Drawing.Size(30, 13);
            this.labelDOC.TabIndex = 0;
            this.labelDOC.Text = "DOC";
            // 
            // NumberOfObservationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 293);
            this.Controls.Add(this.cbValueDOC);
            this.Controls.Add(this.cbConditionDOC);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chbIgnore);
            this.Controls.Add(this.chbUseMapping);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbValueOffice);
            this.Controls.Add(this.cbValueCompany);
            this.Controls.Add(this.dtValueDate2);
            this.Controls.Add(this.dtValueDate);
            this.Controls.Add(this.cbConditionOffice);
            this.Controls.Add(this.panel24);
            this.Controls.Add(this.cbConditionCompany);
            this.Controls.Add(this.panel26);
            this.Controls.Add(this.cbConditionDate);
            this.Controls.Add(this.panel27);
            this.Controls.Add(this.cbValueObservation);
            this.Controls.Add(this.cbConditionObservation);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.cbValueInspector);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.cbValueVessel);
            this.Controls.Add(this.panel22);
            this.Controls.Add(this.cbConditionInspector);
            this.Controls.Add(this.cbConditionVessel);
            this.Controls.Add(this.panel21);
            this.Controls.Add(this.panel11);
            this.Controls.Add(this.panel3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NumberOfObservationsForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Number Of Observations Report Settings";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel22.ResumeLayout(false);
            this.panel22.PerformLayout();
            this.panel21.ResumeLayout(false);
            this.panel21.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel24.ResumeLayout(false);
            this.panel24.PerformLayout();
            this.panel26.ResumeLayout(false);
            this.panel26.PerformLayout();
            this.panel27.ResumeLayout(false);
            this.panel27.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SuggestComboBox20.SuggestComboBox20 cbValueInspector;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelInspector;
        private System.Windows.Forms.ComboBox cbValueVessel;
        private System.Windows.Forms.Panel panel22;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cbConditionInspector;
        private System.Windows.Forms.ComboBox cbConditionVessel;
        private System.Windows.Forms.Panel panel21;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label labelVessel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox cbValueObservation;
        private System.Windows.Forms.ComboBox cbConditionObservation;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label labelObservation;
        private System.Windows.Forms.ComboBox cbValueOffice;
        private System.Windows.Forms.ComboBox cbValueCompany;
        private System.Windows.Forms.DateTimePicker dtValueDate2;
        private System.Windows.Forms.DateTimePicker dtValueDate;
        private System.Windows.Forms.ComboBox cbConditionOffice;
        private System.Windows.Forms.Panel panel24;
        private System.Windows.Forms.Label labelOffice;
        private System.Windows.Forms.ComboBox cbConditionCompany;
        private System.Windows.Forms.Panel panel26;
        private System.Windows.Forms.Label labelCompany;
        private System.Windows.Forms.ComboBox cbConditionDate;
        private System.Windows.Forms.Panel panel27;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox chbUseMapping;
        private System.Windows.Forms.CheckBox chbIgnore;
        private System.Windows.Forms.ComboBox cbValueDOC;
        private System.Windows.Forms.ComboBox cbConditionDOC;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelDOC;

    }
}