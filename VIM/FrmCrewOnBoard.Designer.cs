namespace VIM
{
    partial class FrmCrewOnBoard
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUpdateCrew = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.adgvCrew = new ADGV.AdvancedDataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adgvCrew)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnUpdateCrew);
            this.panel1.Controls.Add(this.btnLoad);
            this.panel1.Controls.Add(this.btnSettings);
            this.panel1.Controls.Add(this.btnNew);
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 313);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(603, 32);
            this.panel1.TabIndex = 1;
            // 
            // btnUpdateCrew
            // 
            this.btnUpdateCrew.Image = global::VIM.Properties.Resources.refresh_7;
            this.btnUpdateCrew.Location = new System.Drawing.Point(444, 5);
            this.btnUpdateCrew.Name = "btnUpdateCrew";
            this.btnUpdateCrew.Size = new System.Drawing.Size(75, 25);
            this.btnUpdateCrew.TabIndex = 6;
            this.btnUpdateCrew.Text = "Update";
            this.btnUpdateCrew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnUpdateCrew, "Update crew for reports");
            this.btnUpdateCrew.UseVisualStyleBackColor = true;
            this.btnUpdateCrew.Click += new System.EventHandler(this.btnUpdateCrew_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Image = global::VIM.Properties.Resources.load_1;
            this.btnLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLoad.Location = new System.Drawing.Point(99, 5);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 25);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text = "Load";
            this.btnLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnLoad, "Load crew on board from Excel file");
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettings.Image = global::VIM.Properties.Resources.Settings_4;
            this.btnSettings.Location = new System.Drawing.Point(174, 5);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(23, 25);
            this.btnSettings.TabIndex = 4;
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.Image = global::VIM.Properties.Resources.plus;
            this.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNew.Location = new System.Drawing.Point(201, 5);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 25);
            this.btnNew.TabIndex = 3;
            this.btnNew.Text = "New";
            this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnNew, "Create new report for crew on board");
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Image = global::VIM.Properties.Resources.edit;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEdit.Location = new System.Drawing.Point(282, 5);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 25);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Edit";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnEdit, "Edit selected record");
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Image = global::VIM.Properties.Resources.minus;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.Location = new System.Drawing.Point(363, 5);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnDelete, "Delete selected record");
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Image = global::VIM.Properties.Resources.Close_Box_Red;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Location = new System.Drawing.Point(525, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // adgvCrew
            // 
            this.adgvCrew.AllowUserToAddRows = false;
            this.adgvCrew.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.adgvCrew.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.adgvCrew.AutoGenerateContextFilters = true;
            this.adgvCrew.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.adgvCrew.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.adgvCrew.BackgroundColor = System.Drawing.SystemColors.Window;
            this.adgvCrew.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.adgvCrew.DateWithTime = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.adgvCrew.DefaultCellStyle = dataGridViewCellStyle2;
            this.adgvCrew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.adgvCrew.Location = new System.Drawing.Point(0, 0);
            this.adgvCrew.Name = "adgvCrew";
            this.adgvCrew.ReadOnly = true;
            this.adgvCrew.RowHeadersWidth = 25;
            this.adgvCrew.Size = new System.Drawing.Size(603, 313);
            this.adgvCrew.TabIndex = 60;
            this.adgvCrew.TimeFilter = false;
            this.adgvCrew.SortStringChanged += new System.EventHandler(this.adgvCrew_SortStringChanged);
            this.adgvCrew.FilterStringChanged += new System.EventHandler(this.adgvCrew_FilterStringChanged);
            this.adgvCrew.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.adgvCrew_CellDoubleClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Excel files(*.xls,*.xlsx)|*.xls;*.xlsx|All files(*.*)|*.*";
            // 
            // FrmCrewOnBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 345);
            this.Controls.Add(this.adgvCrew);
            this.Controls.Add(this.panel1);
            this.Name = "FrmCrewOnBoard";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crew on board";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCrewOnBoard_FormClosing);
            this.Load += new System.EventHandler(this.FrmCrewOnBoard_Load);
            this.ResizeBegin += new System.EventHandler(this.FrmCrewOnBoard_ResizeBegin);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.adgvCrew)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private ADGV.AdvancedDataGridView adgvCrew;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnUpdateCrew;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}