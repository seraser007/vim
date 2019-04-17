namespace VIM
{
    partial class FormAuditReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbAuditor = new System.Windows.Forms.TextBox();
            this.tbAuditType = new System.Windows.Forms.TextBox();
            this.tbDate = new System.Windows.Forms.TextBox();
            this.tbVesselName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvAuditObs = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvInspectionsObs = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuditObs)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInspectionsObs)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbAuditor);
            this.panel1.Controls.Add(this.tbAuditType);
            this.panel1.Controls.Add(this.tbDate);
            this.panel1.Controls.Add(this.tbVesselName);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(780, 57);
            this.panel1.TabIndex = 0;
            // 
            // tbAuditor
            // 
            this.tbAuditor.BackColor = System.Drawing.SystemColors.Window;
            this.tbAuditor.Location = new System.Drawing.Point(453, 31);
            this.tbAuditor.Name = "tbAuditor";
            this.tbAuditor.ReadOnly = true;
            this.tbAuditor.Size = new System.Drawing.Size(315, 20);
            this.tbAuditor.TabIndex = 7;
            // 
            // tbAuditType
            // 
            this.tbAuditType.BackColor = System.Drawing.SystemColors.Window;
            this.tbAuditType.Location = new System.Drawing.Point(90, 31);
            this.tbAuditType.Name = "tbAuditType";
            this.tbAuditType.ReadOnly = true;
            this.tbAuditType.Size = new System.Drawing.Size(283, 20);
            this.tbAuditType.TabIndex = 6;
            // 
            // tbDate
            // 
            this.tbDate.BackColor = System.Drawing.SystemColors.Window;
            this.tbDate.Location = new System.Drawing.Point(453, 8);
            this.tbDate.Name = "tbDate";
            this.tbDate.ReadOnly = true;
            this.tbDate.Size = new System.Drawing.Size(100, 20);
            this.tbDate.TabIndex = 5;
            // 
            // tbVesselName
            // 
            this.tbVesselName.BackColor = System.Drawing.SystemColors.Window;
            this.tbVesselName.Location = new System.Drawing.Point(90, 8);
            this.tbVesselName.Name = "tbVesselName";
            this.tbVesselName.ReadOnly = true;
            this.tbVesselName.Size = new System.Drawing.Size(283, 20);
            this.tbVesselName.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(379, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Auditor";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Audit type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(379, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Date of audit";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vessel name";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 311);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(780, 31);
            this.panel2.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Image = global::VIM.Properties.Resources.Close_Box_Red;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Location = new System.Drawing.Point(693, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 57);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvAuditObs);
            this.splitContainer1.Panel1.Controls.Add(this.panel3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvInspectionsObs);
            this.splitContainer1.Panel2.Controls.Add(this.panel4);
            this.splitContainer1.Size = new System.Drawing.Size(780, 254);
            this.splitContainer1.SplitterDistance = 377;
            this.splitContainer1.TabIndex = 2;
            // 
            // dgvAuditObs
            // 
            this.dgvAuditObs.AllowUserToAddRows = false;
            this.dgvAuditObs.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvAuditObs.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAuditObs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAuditObs.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvAuditObs.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvAuditObs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAuditObs.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAuditObs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAuditObs.Location = new System.Drawing.Point(0, 25);
            this.dgvAuditObs.Name = "dgvAuditObs";
            this.dgvAuditObs.ReadOnly = true;
            this.dgvAuditObs.RowHeadersWidth = 25;
            this.dgvAuditObs.Size = new System.Drawing.Size(377, 229);
            this.dgvAuditObs.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(377, 25);
            this.panel3.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Audit observations";
            // 
            // dgvInspectionsObs
            // 
            this.dgvInspectionsObs.AllowUserToAddRows = false;
            this.dgvInspectionsObs.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvInspectionsObs.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvInspectionsObs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInspectionsObs.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvInspectionsObs.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvInspectionsObs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInspectionsObs.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvInspectionsObs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInspectionsObs.Location = new System.Drawing.Point(0, 25);
            this.dgvInspectionsObs.Name = "dgvInspectionsObs";
            this.dgvInspectionsObs.ReadOnly = true;
            this.dgvInspectionsObs.RowHeadersWidth = 25;
            this.dgvInspectionsObs.Size = new System.Drawing.Size(399, 229);
            this.dgvInspectionsObs.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(399, 25);
            this.panel4.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Inspections observations";
            // 
            // FormAuditReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 342);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "FormAuditReport";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Audit and inspections observations";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.FormAuditReport_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuditObs)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInspectionsObs)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbAuditor;
        private System.Windows.Forms.TextBox tbAuditType;
        private System.Windows.Forms.TextBox tbDate;
        private System.Windows.Forms.TextBox tbVesselName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvAuditObs;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvInspectionsObs;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label6;
    }
}