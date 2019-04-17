namespace VIM
{
    partial class QuestionnairiesForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvVIQ = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dgvOVIQ = new System.Windows.Forms.DataGridView();
            this.tabPSC = new System.Windows.Forms.TabPage();
            this.dgvPSC = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvCDI = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVIQ)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOVIQ)).BeginInit();
            this.tabPSC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPSC)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCDI)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Image = global::VIM.Properties.Resources.load_1;
            this.btnLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLoad.Location = new System.Drawing.Point(212, 375);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 25);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text = "Load";
            this.btnLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnLoad, "Load questionnaire from file");
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Visible = false;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Image = global::VIM.Properties.Resources.edit;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEdit.Location = new System.Drawing.Point(293, 375);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 25);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "Edit";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnEdit, "Edit questionnaire details");
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnView
            // 
            this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnView.Image = global::VIM.Properties.Resources.normal_view;
            this.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnView.Location = new System.Drawing.Point(374, 375);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 25);
            this.btnView.TabIndex = 3;
            this.btnView.Text = "View";
            this.btnView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnView, "View questionnaire");
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Image = global::VIM.Properties.Resources.minus;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.Location = new System.Drawing.Point(455, 375);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnDelete, "Delete questionnaire");
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Image = global::VIM.Properties.Resources.Close_Box_Red;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Location = new System.Drawing.Point(536, 375);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 25);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnClose, "Close form");
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPSC);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(1, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(610, 366);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvVIQ);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(602, 340);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "VIQ Questionnaires";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvVIQ
            // 
            this.dgvVIQ.AllowUserToAddRows = false;
            this.dgvVIQ.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvVIQ.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvVIQ.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVIQ.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvVIQ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVIQ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVIQ.Location = new System.Drawing.Point(3, 3);
            this.dgvVIQ.Name = "dgvVIQ";
            this.dgvVIQ.ReadOnly = true;
            this.dgvVIQ.RowHeadersWidth = 25;
            this.dgvVIQ.Size = new System.Drawing.Size(596, 334);
            this.dgvVIQ.TabIndex = 1;
            this.dgvVIQ.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVIQ_CellDoubleClick);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dgvOVIQ);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(602, 340);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "OVIQ Questionnaires";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dgvOVIQ
            // 
            this.dgvOVIQ.AllowUserToAddRows = false;
            this.dgvOVIQ.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvOVIQ.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvOVIQ.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOVIQ.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvOVIQ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOVIQ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOVIQ.Location = new System.Drawing.Point(3, 3);
            this.dgvOVIQ.Name = "dgvOVIQ";
            this.dgvOVIQ.ReadOnly = true;
            this.dgvOVIQ.RowHeadersWidth = 25;
            this.dgvOVIQ.Size = new System.Drawing.Size(596, 334);
            this.dgvOVIQ.TabIndex = 0;
            this.dgvOVIQ.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOVIQ_CellDoubleClick);
            // 
            // tabPSC
            // 
            this.tabPSC.Controls.Add(this.dgvPSC);
            this.tabPSC.Location = new System.Drawing.Point(4, 22);
            this.tabPSC.Name = "tabPSC";
            this.tabPSC.Padding = new System.Windows.Forms.Padding(3);
            this.tabPSC.Size = new System.Drawing.Size(602, 340);
            this.tabPSC.TabIndex = 4;
            this.tabPSC.Text = "PSC Questionnairies";
            this.tabPSC.UseVisualStyleBackColor = true;
            // 
            // dgvPSC
            // 
            this.dgvPSC.AllowUserToAddRows = false;
            this.dgvPSC.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvPSC.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPSC.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPSC.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvPSC.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvPSC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPSC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPSC.Location = new System.Drawing.Point(3, 3);
            this.dgvPSC.Name = "dgvPSC";
            this.dgvPSC.ReadOnly = true;
            this.dgvPSC.RowHeadersWidth = 25;
            this.dgvPSC.Size = new System.Drawing.Size(596, 334);
            this.dgvPSC.TabIndex = 0;
            this.dgvPSC.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPSC_CellDoubleClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvCDI);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(602, 340);
            this.tabPage2.TabIndex = 5;
            this.tabPage2.Text = "CDI";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvCDI
            // 
            this.dgvCDI.AllowUserToAddRows = false;
            this.dgvCDI.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvCDI.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvCDI.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCDI.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvCDI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCDI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCDI.Location = new System.Drawing.Point(3, 3);
            this.dgvCDI.Name = "dgvCDI";
            this.dgvCDI.ReadOnly = true;
            this.dgvCDI.RowHeadersWidth = 25;
            this.dgvCDI.Size = new System.Drawing.Size(596, 334);
            this.dgvCDI.TabIndex = 0;
            this.dgvCDI.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCDI_CellDoubleClick);
            // 
            // QuestionnairiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 408);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClose);
            this.Name = "QuestionnairiesForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "List of Questionnairies";
            this.Load += new System.EventHandler(this.QuestionnairiesForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVIQ)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOVIQ)).EndInit();
            this.tabPSC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPSC)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCDI)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvVIQ;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dgvOVIQ;
        private System.Windows.Forms.TabPage tabPSC;
        private System.Windows.Forms.DataGridView dgvPSC;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvCDI;
    }
}