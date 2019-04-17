namespace VIM
{
    partial class FormGapReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tstbPeriod = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.adgvAudits = new ADGV.AdvancedDataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssLabelRecCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.adgvAuditReports = new ADGV.AdvancedDataGridView();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslAuditCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.adgvReporAudits = new ADGV.AdvancedDataGridView();
            this.statusStrip3 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslInspCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adgvAudits)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adgvAuditReports)).BeginInit();
            this.statusStrip2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adgvReporAudits)).BeginInit();
            this.statusStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tstbPeriod,
            this.toolStripButton2,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(736, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::VIM.Properties.Resources.open;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(41, 22);
            this.toolStripLabel1.Text = "Period";
            // 
            // tstbPeriod
            // 
            this.tstbPeriod.Name = "tstbPeriod";
            this.tstbPeriod.Size = new System.Drawing.Size(150, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::VIM.Properties.Resources.calendar_1;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.ToolTipText = "Set period";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 285);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(736, 31);
            this.panel1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(736, 260);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.adgvAudits);
            this.tabPage1.Controls.Add(this.statusStrip1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(728, 234);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "List of audit observations";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // adgvAudits
            // 
            this.adgvAudits.AllowUserToAddRows = false;
            this.adgvAudits.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.adgvAudits.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.adgvAudits.AutoGenerateContextFilters = true;
            this.adgvAudits.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.adgvAudits.BackgroundColor = System.Drawing.SystemColors.Window;
            this.adgvAudits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.adgvAudits.DateWithTime = false;
            this.adgvAudits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.adgvAudits.Location = new System.Drawing.Point(3, 3);
            this.adgvAudits.Name = "adgvAudits";
            this.adgvAudits.ReadOnly = true;
            this.adgvAudits.RowHeadersWidth = 25;
            this.adgvAudits.Size = new System.Drawing.Size(722, 206);
            this.adgvAudits.TabIndex = 0;
            this.adgvAudits.TimeFilter = false;
            this.adgvAudits.SortStringChanged += new System.EventHandler(this.adgvAudits_SortStringChanged);
            this.adgvAudits.FilterStringChanged += new System.EventHandler(this.adgvAudits_FilterStringChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tssLabelRecCount});
            this.statusStrip1.Location = new System.Drawing.Point(3, 209);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(722, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(55, 17);
            this.toolStripStatusLabel1.Text = "Records :";
            // 
            // tssLabelRecCount
            // 
            this.tssLabelRecCount.Name = "tssLabelRecCount";
            this.tssLabelRecCount.Size = new System.Drawing.Size(13, 17);
            this.tssLabelRecCount.Text = "0";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.adgvAuditReports);
            this.tabPage2.Controls.Add(this.statusStrip2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(728, 234);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Audits before inspections";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // adgvAuditReports
            // 
            this.adgvAuditReports.AllowUserToAddRows = false;
            this.adgvAuditReports.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.adgvAuditReports.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.adgvAuditReports.AutoGenerateContextFilters = true;
            this.adgvAuditReports.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.adgvAuditReports.BackgroundColor = System.Drawing.SystemColors.Window;
            this.adgvAuditReports.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.adgvAuditReports.DateWithTime = false;
            this.adgvAuditReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.adgvAuditReports.Location = new System.Drawing.Point(3, 3);
            this.adgvAuditReports.Name = "adgvAuditReports";
            this.adgvAuditReports.ReadOnly = true;
            this.adgvAuditReports.Size = new System.Drawing.Size(722, 206);
            this.adgvAuditReports.TabIndex = 0;
            this.adgvAuditReports.TimeFilter = false;
            this.adgvAuditReports.SortStringChanged += new System.EventHandler(this.adgvAuditReports_SortStringChanged);
            this.adgvAuditReports.FilterStringChanged += new System.EventHandler(this.adgvAuditReports_FilterStringChanged);
            this.adgvAuditReports.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.adgvAuditReports_CellDoubleClick);
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.tsslAuditCount});
            this.statusStrip2.Location = new System.Drawing.Point(3, 209);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(722, 22);
            this.statusStrip2.TabIndex = 1;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(55, 17);
            this.toolStripStatusLabel2.Text = "Records :";
            // 
            // tsslAuditCount
            // 
            this.tsslAuditCount.Name = "tsslAuditCount";
            this.tsslAuditCount.Size = new System.Drawing.Size(13, 17);
            this.tsslAuditCount.Text = "0";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.adgvReporAudits);
            this.tabPage3.Controls.Add(this.statusStrip3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(728, 234);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Inspections after audit";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // adgvReporAudits
            // 
            this.adgvReporAudits.AllowUserToAddRows = false;
            this.adgvReporAudits.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.adgvReporAudits.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.adgvReporAudits.AutoGenerateContextFilters = true;
            this.adgvReporAudits.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.adgvReporAudits.BackgroundColor = System.Drawing.SystemColors.Window;
            this.adgvReporAudits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.adgvReporAudits.DateWithTime = false;
            this.adgvReporAudits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.adgvReporAudits.Location = new System.Drawing.Point(3, 3);
            this.adgvReporAudits.Name = "adgvReporAudits";
            this.adgvReporAudits.ReadOnly = true;
            this.adgvReporAudits.RowHeadersWidth = 25;
            this.adgvReporAudits.Size = new System.Drawing.Size(722, 206);
            this.adgvReporAudits.TabIndex = 0;
            this.adgvReporAudits.TimeFilter = false;
            this.adgvReporAudits.SortStringChanged += new System.EventHandler(this.adgvReporAudits_SortStringChanged);
            this.adgvReporAudits.FilterStringChanged += new System.EventHandler(this.adgvReporAudits_FilterStringChanged);
            this.adgvReporAudits.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.adgvReporAudits_CellDoubleClick);
            // 
            // statusStrip3
            // 
            this.statusStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel3,
            this.tsslInspCount});
            this.statusStrip3.Location = new System.Drawing.Point(3, 209);
            this.statusStrip3.Name = "statusStrip3";
            this.statusStrip3.Size = new System.Drawing.Size(722, 22);
            this.statusStrip3.TabIndex = 1;
            this.statusStrip3.Text = "statusStrip3";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(55, 17);
            this.toolStripStatusLabel3.Text = "Records :";
            // 
            // tsslInspCount
            // 
            this.tsslInspCount.Name = "tsslInspCount";
            this.tsslInspCount.Size = new System.Drawing.Size(13, 17);
            this.tsslInspCount.Text = "0";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Excel files (*.xls,*.xlsx)|*.xls;*.xlsx|All files (*.*)|*.*";
            // 
            // FormGapReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 316);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormGapReport";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gap report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adgvAudits)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adgvAuditReports)).EndInit();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adgvReporAudits)).EndInit();
            this.statusStrip3.ResumeLayout(false);
            this.statusStrip3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tstbPeriod;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private ADGV.AdvancedDataGridView adgvAudits;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private ADGV.AdvancedDataGridView adgvAuditReports;
        private System.Windows.Forms.TabPage tabPage3;
        private ADGV.AdvancedDataGridView adgvReporAudits;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tssLabelRecCount;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel tsslAuditCount;
        private System.Windows.Forms.StatusStrip statusStrip3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel tsslInspCount;
    }
}