namespace VIM
{
    partial class FrmCrewLoadSettings
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
            this.rbAskForFile = new System.Windows.Forms.RadioButton();
            this.rbUsePredefinedFile = new System.Windows.Forms.RadioButton();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbRelative = new System.Windows.Forms.RadioButton();
            this.rbAbsolute = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbAskForFile
            // 
            this.rbAskForFile.AutoSize = true;
            this.rbAskForFile.Checked = true;
            this.rbAskForFile.Location = new System.Drawing.Point(21, 21);
            this.rbAskForFile.Name = "rbAskForFile";
            this.rbAskForFile.Size = new System.Drawing.Size(103, 17);
            this.rbAskForFile.TabIndex = 0;
            this.rbAskForFile.TabStop = true;
            this.rbAskForFile.Text = "Ask for file name";
            this.rbAskForFile.UseVisualStyleBackColor = true;
            this.rbAskForFile.CheckedChanged += new System.EventHandler(this.rbAskForFile_CheckedChanged);
            // 
            // rbUsePredefinedFile
            // 
            this.rbUsePredefinedFile.AutoSize = true;
            this.rbUsePredefinedFile.Location = new System.Drawing.Point(21, 57);
            this.rbUsePredefinedFile.Name = "rbUsePredefinedFile";
            this.rbUsePredefinedFile.Size = new System.Drawing.Size(113, 17);
            this.rbUsePredefinedFile.TabIndex = 1;
            this.rbUsePredefinedFile.Text = "Use predefined file";
            this.rbUsePredefinedFile.UseVisualStyleBackColor = true;
            this.rbUsePredefinedFile.CheckedChanged += new System.EventHandler(this.rbUsePredefinedFile_CheckedChanged);
            // 
            // tbFileName
            // 
            this.tbFileName.Location = new System.Drawing.Point(43, 80);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(500, 20);
            this.tbFileName.TabIndex = 2;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Enabled = false;
            this.btnBrowse.Location = new System.Drawing.Point(543, 78);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::VIM.Properties.Resources.Close_Box_Red;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.Location = new System.Drawing.Point(542, 132);
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
            this.btnOK.Location = new System.Drawing.Point(461, 132);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Excel files|*.xls;*.xlsx;*.xlsm";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbRelative);
            this.panel1.Controls.Add(this.rbAbsolute);
            this.panel1.Location = new System.Drawing.Point(143, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 27);
            this.panel1.TabIndex = 6;
            // 
            // rbRelative
            // 
            this.rbRelative.AutoSize = true;
            this.rbRelative.Checked = true;
            this.rbRelative.Enabled = false;
            this.rbRelative.Location = new System.Drawing.Point(99, 7);
            this.rbRelative.Name = "rbRelative";
            this.rbRelative.Size = new System.Drawing.Size(88, 17);
            this.rbRelative.TabIndex = 1;
            this.rbRelative.TabStop = true;
            this.rbRelative.Text = "Relative path";
            this.rbRelative.UseVisualStyleBackColor = true;
            this.rbRelative.Click += new System.EventHandler(this.rbRelative_Click);
            // 
            // rbAbsolute
            // 
            this.rbAbsolute.AutoSize = true;
            this.rbAbsolute.Enabled = false;
            this.rbAbsolute.Location = new System.Drawing.Point(3, 7);
            this.rbAbsolute.Name = "rbAbsolute";
            this.rbAbsolute.Size = new System.Drawing.Size(90, 17);
            this.rbAbsolute.TabIndex = 0;
            this.rbAbsolute.Text = "Absolute path";
            this.rbAbsolute.UseVisualStyleBackColor = true;
            this.rbAbsolute.Click += new System.EventHandler(this.rbAbsolute_Click);
            // 
            // FrmCrewLoadSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(630, 170);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tbFileName);
            this.Controls.Add(this.rbUsePredefinedFile);
            this.Controls.Add(this.rbAskForFile);
            this.Name = "FrmCrewLoadSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crew Load Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCrewLoadSettings_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbAskForFile;
        private System.Windows.Forms.RadioButton rbUsePredefinedFile;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbRelative;
        private System.Windows.Forms.RadioButton rbAbsolute;
    }
}