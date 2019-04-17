namespace VIM
{
    partial class VesselsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VesselsForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.adgvVessels = new ADGV.AdvancedDataGridView();
            this.chbShowHidden = new System.Windows.Forms.CheckBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.findBox = new SuggestComboBox20.SuggestComboBox20();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblRecs = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSummarySettings = new System.Windows.Forms.Button();
            this.btnSummary = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adgvVessels)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(786, 429);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.adgvVessels);
            this.panel3.Controls.Add(this.chbShowHidden);
            this.panel3.Controls.Add(this.btnFind);
            this.panel3.Controls.Add(this.findBox);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.panel3.Size = new System.Drawing.Size(786, 396);
            this.panel3.TabIndex = 3;
            // 
            // adgvVessels
            // 
            this.adgvVessels.AllowUserToAddRows = false;
            this.adgvVessels.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.adgvVessels.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.adgvVessels.AutoGenerateContextFilters = true;
            this.adgvVessels.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.adgvVessels.BackgroundColor = System.Drawing.SystemColors.Window;
            this.adgvVessels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.adgvVessels.DateWithTime = false;
            this.adgvVessels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.adgvVessels.Location = new System.Drawing.Point(0, 25);
            this.adgvVessels.Name = "adgvVessels";
            this.adgvVessels.ReadOnly = true;
            this.adgvVessels.RowHeadersWidth = 25;
            this.adgvVessels.Size = new System.Drawing.Size(786, 371);
            this.adgvVessels.TabIndex = 8;
            this.adgvVessels.TimeFilter = false;
            this.adgvVessels.SortStringChanged += new System.EventHandler(this.adgvVessels_SortStringChanged);
            this.adgvVessels.FilterStringChanged += new System.EventHandler(this.adgvVessels_FilterStringChanged);
            this.adgvVessels.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.adgvVessels_CellDoubleClick);
            // 
            // chbShowHidden
            // 
            this.chbShowHidden.AutoSize = true;
            this.chbShowHidden.Location = new System.Drawing.Point(286, 5);
            this.chbShowHidden.Name = "chbShowHidden";
            this.chbShowHidden.Size = new System.Drawing.Size(88, 17);
            this.chbShowHidden.TabIndex = 6;
            this.chbShowHidden.Text = "Show hidden";
            this.chbShowHidden.UseVisualStyleBackColor = true;
            this.chbShowHidden.CheckedChanged += new System.EventHandler(this.chbShowHidden_CheckedChanged);
            // 
            // btnFind
            // 
            this.btnFind.Image = global::VIM.Properties.Resources.search;
            this.btnFind.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFind.Location = new System.Drawing.Point(253, 2);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(20, 20);
            this.btnFind.TabIndex = 6;
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // findBox
            // 
            this.findBox.FormattingEnabled = true;
            this.findBox.Location = new System.Drawing.Point(48, 2);
            this.findBox.Name = "findBox";
            this.findBox.SearchRule = SuggestComboBox20.SuggestComboBox20.searchRules.cbsContains;
            this.findBox.Size = new System.Drawing.Size(204, 21);
            this.findBox.SuggestBoxHeight = 96;
            this.findBox.TabIndex = 4;
            this.findBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.findBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Find";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblRecs);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btnSummarySettings);
            this.panel2.Controls.Add(this.btnSummary);
            this.panel2.Controls.Add(this.btnNew);
            this.panel2.Controls.Add(this.btnEdit);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 396);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(786, 33);
            this.panel2.TabIndex = 2;
            // 
            // lblRecs
            // 
            this.lblRecs.AutoSize = true;
            this.lblRecs.Location = new System.Drawing.Point(60, 11);
            this.lblRecs.Name = "lblRecs";
            this.lblRecs.Size = new System.Drawing.Size(13, 13);
            this.lblRecs.TabIndex = 7;
            this.lblRecs.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Records :";
            // 
            // btnSummarySettings
            // 
            this.btnSummarySettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSummarySettings.Image = global::VIM.Properties.Resources.Settings_4;
            this.btnSummarySettings.Location = new System.Drawing.Point(436, 5);
            this.btnSummarySettings.Name = "btnSummarySettings";
            this.btnSummarySettings.Size = new System.Drawing.Size(23, 25);
            this.btnSummarySettings.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnSummarySettings, "Summary settings");
            this.btnSummarySettings.UseVisualStyleBackColor = true;
            this.btnSummarySettings.Click += new System.EventHandler(this.btnSummarySettings_Click);
            // 
            // btnSummary
            // 
            this.btnSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSummary.Image = global::VIM.Properties.Resources.Select_All_Clear;
            this.btnSummary.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSummary.Location = new System.Drawing.Point(280, 5);
            this.btnSummary.Name = "btnSummary";
            this.btnSummary.Size = new System.Drawing.Size(158, 25);
            this.btnSummary.TabIndex = 4;
            this.btnSummary.Text = "Create summary";
            this.btnSummary.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnSummary, "Create summary of observations");
            this.btnSummary.UseVisualStyleBackColor = true;
            this.btnSummary.Click += new System.EventHandler(this.btnSummary_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.Image = global::VIM.Properties.Resources.plus;
            this.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNew.Location = new System.Drawing.Point(465, 5);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 25);
            this.btnNew.TabIndex = 3;
            this.btnNew.Text = "New";
            this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEdit.Location = new System.Drawing.Point(546, 5);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 25);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Edit";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.Location = new System.Drawing.Point(627, 5);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Image = global::VIM.Properties.Resources.Close_Box_Red;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Location = new System.Drawing.Point(708, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // VesselsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 429);
            this.Controls.Add(this.panel1);
            this.Name = "VesselsForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "List of vessels";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VesselsForm_FormClosing);
            this.Load += new System.EventHandler(this.VesselsForm_Load);
            this.Shown += new System.EventHandler(this.VesselsForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adgvVessels)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnFind;
        private SuggestComboBox20.SuggestComboBox20 findBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSummary;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnSummarySettings;
        private System.Windows.Forms.CheckBox chbShowHidden;
        private ADGV.AdvancedDataGridView adgvVessels;
        private System.Windows.Forms.Label lblRecs;
        private System.Windows.Forms.Label label2;
    }
}