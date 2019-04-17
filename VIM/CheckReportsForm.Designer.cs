namespace VIM
{
    partial class CheckReportsForm
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.lblReport = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblReportsWFile = new System.Windows.Forms.Label();
            this.dgReportsWFile = new System.Windows.Forms.DataGridView();
            this.Reports1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgReportsWOFile = new System.Windows.Forms.DataGridView();
            this.Reports2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblReportsWOFile = new System.Windows.Forms.Label();
            this.dgFiles = new System.Windows.Forms.DataGridView();
            this.Files1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblFiles = new System.Windows.Forms.Label();
            this.btnStartCheck = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgReportsWFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgReportsWOFile)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFiles)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Progress";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(15, 25);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(518, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Report :";
            // 
            // lblReport
            // 
            this.lblReport.AutoSize = true;
            this.lblReport.Location = new System.Drawing.Point(63, 63);
            this.lblReport.Name = "lblReport";
            this.lblReport.Size = new System.Drawing.Size(64, 13);
            this.lblReport.TabIndex = 3;
            this.lblReport.Text = "ReportCode";
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(66, 9);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(34, 13);
            this.lblProgress.TabIndex = 4;
            this.lblProgress.Text = "1 of 1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(15, 91);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel4);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(518, 318);
            this.splitContainer1.SplitterDistance = 163;
            this.splitContainer1.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblReportsWFile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(163, 20);
            this.panel1.TabIndex = 6;
            // 
            // lblReportsWFile
            // 
            this.lblReportsWFile.AutoSize = true;
            this.lblReportsWFile.Location = new System.Drawing.Point(2, 4);
            this.lblReportsWFile.Name = "lblReportsWFile";
            this.lblReportsWFile.Size = new System.Drawing.Size(82, 13);
            this.lblReportsWFile.TabIndex = 6;
            this.lblReportsWFile.Text = "Reports with file";
            // 
            // dgReportsWFile
            // 
            this.dgReportsWFile.AllowUserToAddRows = false;
            this.dgReportsWFile.AllowUserToDeleteRows = false;
            this.dgReportsWFile.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgReportsWFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgReportsWFile.ColumnHeadersVisible = false;
            this.dgReportsWFile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Reports1});
            this.dgReportsWFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgReportsWFile.Location = new System.Drawing.Point(0, 0);
            this.dgReportsWFile.Name = "dgReportsWFile";
            this.dgReportsWFile.ReadOnly = true;
            this.dgReportsWFile.RowHeadersVisible = false;
            this.dgReportsWFile.Size = new System.Drawing.Size(163, 298);
            this.dgReportsWFile.TabIndex = 6;
            // 
            // Reports1
            // 
            this.Reports1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Reports1.HeaderText = "Reports";
            this.Reports1.Name = "Reports1";
            this.Reports1.ReadOnly = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgReportsWOFile);
            this.splitContainer2.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dgFiles);
            this.splitContainer2.Panel2.Controls.Add(this.panel3);
            this.splitContainer2.Size = new System.Drawing.Size(351, 318);
            this.splitContainer2.SplitterDistance = 175;
            this.splitContainer2.TabIndex = 0;
            // 
            // dgReportsWOFile
            // 
            this.dgReportsWOFile.AllowUserToAddRows = false;
            this.dgReportsWOFile.AllowUserToDeleteRows = false;
            this.dgReportsWOFile.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgReportsWOFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgReportsWOFile.ColumnHeadersVisible = false;
            this.dgReportsWOFile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Reports2});
            this.dgReportsWOFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgReportsWOFile.Location = new System.Drawing.Point(0, 20);
            this.dgReportsWOFile.Name = "dgReportsWOFile";
            this.dgReportsWOFile.ReadOnly = true;
            this.dgReportsWOFile.RowHeadersVisible = false;
            this.dgReportsWOFile.Size = new System.Drawing.Size(175, 298);
            this.dgReportsWOFile.TabIndex = 2;
            // 
            // Reports2
            // 
            this.Reports2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Reports2.HeaderText = "Reports";
            this.Reports2.Name = "Reports2";
            this.Reports2.ReadOnly = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblReportsWOFile);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(175, 20);
            this.panel2.TabIndex = 1;
            // 
            // lblReportsWOFile
            // 
            this.lblReportsWOFile.AutoSize = true;
            this.lblReportsWOFile.Location = new System.Drawing.Point(3, 3);
            this.lblReportsWOFile.Name = "lblReportsWOFile";
            this.lblReportsWOFile.Size = new System.Drawing.Size(97, 13);
            this.lblReportsWOFile.TabIndex = 0;
            this.lblReportsWOFile.Text = "Reports without file";
            // 
            // dgFiles
            // 
            this.dgFiles.AllowUserToAddRows = false;
            this.dgFiles.AllowUserToDeleteRows = false;
            this.dgFiles.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFiles.ColumnHeadersVisible = false;
            this.dgFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Files1});
            this.dgFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgFiles.Location = new System.Drawing.Point(0, 20);
            this.dgFiles.Name = "dgFiles";
            this.dgFiles.ReadOnly = true;
            this.dgFiles.RowHeadersVisible = false;
            this.dgFiles.Size = new System.Drawing.Size(172, 298);
            this.dgFiles.TabIndex = 2;
            // 
            // Files1
            // 
            this.Files1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Files1.HeaderText = "Files";
            this.Files1.Name = "Files1";
            this.Files1.ReadOnly = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblFiles);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(172, 20);
            this.panel3.TabIndex = 1;
            // 
            // lblFiles
            // 
            this.lblFiles.AutoSize = true;
            this.lblFiles.Location = new System.Drawing.Point(3, 3);
            this.lblFiles.Name = "lblFiles";
            this.lblFiles.Size = new System.Drawing.Size(95, 13);
            this.lblFiles.TabIndex = 0;
            this.lblFiles.Text = "Files without report";
            // 
            // btnStartCheck
            // 
            this.btnStartCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartCheck.Location = new System.Drawing.Point(437, 53);
            this.btnStartCheck.Name = "btnStartCheck";
            this.btnStartCheck.Size = new System.Drawing.Size(96, 23);
            this.btnStartCheck.TabIndex = 6;
            this.btnStartCheck.Text = "Start check";
            this.btnStartCheck.UseVisualStyleBackColor = true;
            this.btnStartCheck.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(458, 425);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgReportsWFile);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 20);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(163, 298);
            this.panel4.TabIndex = 8;
            // 
            // CheckReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 460);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnStartCheck);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.lblReport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CheckReportsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Check Report Files";
            this.Shown += new System.EventHandler(this.CheckReportsForm_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgReportsWFile)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgReportsWOFile)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFiles)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblReport;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblReportsWFile;
        private System.Windows.Forms.DataGridView dgReportsWFile;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgReportsWOFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reports2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblReportsWOFile;
        private System.Windows.Forms.DataGridView dgFiles;
        private System.Windows.Forms.DataGridViewTextBoxColumn Files1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblFiles;
        private System.Windows.Forms.Button btnStartCheck;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reports1;
        private System.Windows.Forms.Panel panel4;
    }
}