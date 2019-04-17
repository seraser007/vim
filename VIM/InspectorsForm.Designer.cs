namespace VIM
{
    partial class InspectorsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InspectorsForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblRecNo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnSendSummary = new System.Windows.Forms.Button();
            this.btnMerge = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.adgvInspectors = new ADGV.AdvancedDataGridView();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.findBox = new SuggestComboBox20.SuggestComboBox20();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adgvInspectors)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblRecNo);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnSettings);
            this.panel1.Controls.Add(this.btnSendSummary);
            this.panel1.Controls.Add(this.btnMerge);
            this.panel1.Controls.Add(this.btnNew);
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 415);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(785, 36);
            this.panel1.TabIndex = 1;
            // 
            // lblRecNo
            // 
            this.lblRecNo.AutoSize = true;
            this.lblRecNo.Location = new System.Drawing.Point(66, 14);
            this.lblRecNo.Name = "lblRecNo";
            this.lblRecNo.Size = new System.Drawing.Size(13, 13);
            this.lblRecNo.TabIndex = 8;
            this.lblRecNo.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Records :";
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettings.Image = global::VIM.Properties.Resources.Settings_4;
            this.btnSettings.Location = new System.Drawing.Point(351, 8);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(25, 25);
            this.btnSettings.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btnSettings, "Open message settings");
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnSendSummary
            // 
            this.btnSendSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendSummary.Image = global::VIM.Properties.Resources.mail;
            this.btnSendSummary.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSendSummary.Location = new System.Drawing.Point(246, 8);
            this.btnSendSummary.Name = "btnSendSummary";
            this.btnSendSummary.Size = new System.Drawing.Size(106, 25);
            this.btnSendSummary.TabIndex = 5;
            this.btnSendSummary.Text = "Send summary";
            this.btnSendSummary.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSendSummary.UseVisualStyleBackColor = true;
            this.btnSendSummary.Click += new System.EventHandler(this.btnSendSummary_Click);
            // 
            // btnMerge
            // 
            this.btnMerge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMerge.Image = global::VIM.Properties.Resources.Merge_2;
            this.btnMerge.Location = new System.Drawing.Point(383, 8);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(75, 25);
            this.btnMerge.TabIndex = 4;
            this.btnMerge.Text = "Merge";
            this.btnMerge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.Image = global::VIM.Properties.Resources.plus;
            this.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNew.Location = new System.Drawing.Point(464, 8);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 25);
            this.btnNew.TabIndex = 3;
            this.btnNew.Text = "New";
            this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEdit.Location = new System.Drawing.Point(545, 8);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 25);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Edit";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.Location = new System.Drawing.Point(626, 8);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Image = global::VIM.Properties.Resources.Close_Box_Red;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Location = new System.Drawing.Point(707, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.adgvInspectors);
            this.panel2.Controls.Add(this.btnBrowse);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.findBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.panel2.Size = new System.Drawing.Size(785, 415);
            this.panel2.TabIndex = 2;
            // 
            // adgvInspectors
            // 
            this.adgvInspectors.AllowUserToAddRows = false;
            this.adgvInspectors.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.adgvInspectors.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.adgvInspectors.AutoGenerateContextFilters = true;
            this.adgvInspectors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.adgvInspectors.BackgroundColor = System.Drawing.SystemColors.Window;
            this.adgvInspectors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.adgvInspectors.DateWithTime = false;
            this.adgvInspectors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.adgvInspectors.Location = new System.Drawing.Point(0, 25);
            this.adgvInspectors.Name = "adgvInspectors";
            this.adgvInspectors.ReadOnly = true;
            this.adgvInspectors.RowHeadersWidth = 25;
            this.adgvInspectors.Size = new System.Drawing.Size(785, 390);
            this.adgvInspectors.TabIndex = 8;
            this.adgvInspectors.TimeFilter = false;
            this.adgvInspectors.SortStringChanged += new System.EventHandler(this.adgvInspectors_SortStringChanged);
            this.adgvInspectors.FilterStringChanged += new System.EventHandler(this.adgvInspectors_FilterStringChanged);
            this.adgvInspectors.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.adgvInspectors_RowPrePaint);
            this.adgvInspectors.DoubleClick += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Image = global::VIM.Properties.Resources.search;
            this.btnBrowse.Location = new System.Drawing.Point(245, 2);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(22, 22);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Find";
            // 
            // findBox
            // 
            this.findBox.FormattingEnabled = true;
            this.findBox.Location = new System.Drawing.Point(40, 3);
            this.findBox.Name = "findBox";
            this.findBox.SearchRule = SuggestComboBox20.SuggestComboBox20.searchRules.cbsContains;
            this.findBox.Size = new System.Drawing.Size(204, 21);
            this.findBox.SuggestBoxHeight = 96;
            this.findBox.TabIndex = 2;
            this.findBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.suggestComboBox201_KeyPress);
            // 
            // InspectorsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(785, 451);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "InspectorsForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "List of inspectors";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InspectorsForm_FormClosing);
            this.Load += new System.EventHandler(this.InspectorsForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adgvInspectors)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private SuggestComboBox20.SuggestComboBox20 findBox;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnSendSummary;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.ToolTip toolTip1;
        private ADGV.AdvancedDataGridView adgvInspectors;
        private System.Windows.Forms.Label lblRecNo;
        private System.Windows.Forms.Label label2;
    }
}