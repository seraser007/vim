namespace VIM
{
    partial class CrewDetailsForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.dgvInspections = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnList = new System.Windows.Forms.Button();
            this.cbPosition = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tbNotes = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPersonalID = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblRecs = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInspections)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::VIM.Properties.Resources.delete;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.Location = new System.Drawing.Point(715, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Image = global::VIM.Properties.Resources.Ok;
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOk.Location = new System.Drawing.Point(634, 2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 25);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "OK";
            this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // dgvInspections
            // 
            this.dgvInspections.AllowUserToAddRows = false;
            this.dgvInspections.AllowUserToDeleteRows = false;
            this.dgvInspections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvInspections.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInspections.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvInspections.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInspections.Location = new System.Drawing.Point(10, 22);
            this.dgvInspections.Name = "dgvInspections";
            this.dgvInspections.ReadOnly = true;
            this.dgvInspections.RowHeadersWidth = 25;
            this.dgvInspections.Size = new System.Drawing.Size(781, 195);
            this.dgvInspections.TabIndex = 11;
            this.dgvInspections.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInspections_CellDoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Inspections:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnList);
            this.panel1.Controls.Add(this.cbPosition);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tbPersonalID);
            this.panel1.Controls.Add(this.tbName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.MinimumSize = new System.Drawing.Size(0, 160);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(799, 218);
            this.panel1.TabIndex = 13;
            // 
            // btnList
            // 
            this.btnList.Image = global::VIM.Properties.Resources.normal_view;
            this.btnList.Location = new System.Drawing.Point(409, 61);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(23, 23);
            this.btnList.TabIndex = 20;
            this.toolTip1.SetToolTip(this.btnList, "Show list of positions");
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // cbPosition
            // 
            this.cbPosition.FormattingEnabled = true;
            this.cbPosition.Location = new System.Drawing.Point(10, 62);
            this.cbPosition.Name = "cbPosition";
            this.cbPosition.Size = new System.Drawing.Size(399, 21);
            this.cbPosition.TabIndex = 19;
            this.cbPosition.SelectedValueChanged += new System.EventHandler(this.cbPosition_SelectedValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Position";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.tbNotes);
            this.panel4.Location = new System.Drawing.Point(10, 102);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(781, 110);
            this.panel4.TabIndex = 17;
            // 
            // tbNotes
            // 
            this.tbNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbNotes.Location = new System.Drawing.Point(0, 0);
            this.tbNotes.Multiline = true;
            this.tbNotes.Name = "tbNotes";
            this.tbNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbNotes.Size = new System.Drawing.Size(781, 110);
            this.tbNotes.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Notes";
            // 
            // tbPersonalID
            // 
            this.tbPersonalID.Location = new System.Drawing.Point(437, 24);
            this.tbPersonalID.Name = "tbPersonalID";
            this.tbPersonalID.Size = new System.Drawing.Size(100, 20);
            this.tbPersonalID.TabIndex = 14;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(10, 24);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(399, 20);
            this.tbName.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(434, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Personal ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Name";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnOk);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 444);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(799, 31);
            this.panel2.TabIndex = 14;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 218);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(799, 3);
            this.splitter1.TabIndex = 15;
            this.splitter1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblRecs);
            this.panel3.Controls.Add(this.dgvInspections);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 221);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(799, 223);
            this.panel3.TabIndex = 16;
            // 
            // lblRecs
            // 
            this.lblRecs.AutoSize = true;
            this.lblRecs.Location = new System.Drawing.Point(75, 4);
            this.lblRecs.Name = "lblRecs";
            this.lblRecs.Size = new System.Drawing.Size(13, 13);
            this.lblRecs.TabIndex = 13;
            this.lblRecs.Text = "0";
            // 
            // CrewDetailsForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(799, 475);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "CrewDetailsForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crewmember details";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CrewDetailsForm_FormClosing);
            this.Load += new System.EventHandler(this.MasterDetailsForm_Load);
            this.Shown += new System.EventHandler(this.CrewDetailsForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInspections)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.DataGridView dgvInspections;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox tbNotes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbPersonalID;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblRecs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnList;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cbPosition;
    }
}