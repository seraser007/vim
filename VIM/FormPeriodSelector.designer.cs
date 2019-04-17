namespace VIM
{
    partial class FormPeriodSelector
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.dtFinish = new System.Windows.Forms.DateTimePicker();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.rbtnDates = new System.Windows.Forms.RadioButton();
            this.rbtnQuater = new System.Windows.Forms.RadioButton();
            this.cbQuater = new System.Windows.Forms.ComboBox();
            this.neYear = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.neYear)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Start of period";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(174, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "End of period";
            // 
            // dtStart
            // 
            this.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtStart.Location = new System.Drawing.Point(35, 99);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(121, 20);
            this.dtStart.TabIndex = 2;
            // 
            // dtFinish
            // 
            this.dtFinish.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFinish.Location = new System.Drawing.Point(177, 99);
            this.dtFinish.Name = "dtFinish";
            this.dtFinish.Size = new System.Drawing.Size(121, 20);
            this.dtFinish.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::VIM.Properties.Resources.cancel_16;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.Location = new System.Drawing.Point(230, 138);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 4;
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
            this.btnOK.Location = new System.Drawing.Point(149, 138);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // rbtnDates
            // 
            this.rbtnDates.AutoSize = true;
            this.rbtnDates.Location = new System.Drawing.Point(12, 62);
            this.rbtnDates.Name = "rbtnDates";
            this.rbtnDates.Size = new System.Drawing.Size(116, 17);
            this.rbtnDates.TabIndex = 6;
            this.rbtnDates.Text = "Between two dates";
            this.rbtnDates.UseVisualStyleBackColor = true;
            // 
            // rbtnQuater
            // 
            this.rbtnQuater.AutoSize = true;
            this.rbtnQuater.Checked = true;
            this.rbtnQuater.Location = new System.Drawing.Point(12, 12);
            this.rbtnQuater.Name = "rbtnQuater";
            this.rbtnQuater.Size = new System.Drawing.Size(57, 17);
            this.rbtnQuater.TabIndex = 7;
            this.rbtnQuater.TabStop = true;
            this.rbtnQuater.Text = "Quater";
            this.rbtnQuater.UseVisualStyleBackColor = true;
            // 
            // cbQuater
            // 
            this.cbQuater.FormattingEnabled = true;
            this.cbQuater.Items.AddRange(new object[] {
            "Quater 1",
            "Quater 2",
            "Quater 3",
            "Quater 4"});
            this.cbQuater.Location = new System.Drawing.Point(35, 35);
            this.cbQuater.Name = "cbQuater";
            this.cbQuater.Size = new System.Drawing.Size(121, 21);
            this.cbQuater.TabIndex = 8;
            // 
            // neYear
            // 
            this.neYear.Location = new System.Drawing.Point(197, 36);
            this.neYear.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.neYear.Name = "neYear";
            this.neYear.Size = new System.Drawing.Size(75, 20);
            this.neYear.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(162, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Year";
            // 
            // FormPeriodSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 175);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.neYear);
            this.Controls.Add(this.cbQuater);
            this.Controls.Add(this.rbtnQuater);
            this.Controls.Add(this.rbtnDates);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dtFinish);
            this.Controls.Add(this.dtStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormPeriodSelector";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Period Selector";
            this.Shown += new System.EventHandler(this.FormPeriodSelector_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.neYear)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.DateTimePicker dtFinish;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.RadioButton rbtnDates;
        private System.Windows.Forms.RadioButton rbtnQuater;
        private System.Windows.Forms.ComboBox cbQuater;
        private System.Windows.Forms.NumericUpDown neYear;
        private System.Windows.Forms.Label label3;
    }
}