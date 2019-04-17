namespace VIM
{
    partial class FrmVesselSummarySettings
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tbFileNameTemplate = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.chbShowFile = new System.Windows.Forms.CheckBox();
            this.rbtnSendSummary = new System.Windows.Forms.RadioButton();
            this.rbtnCreateSummary = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tbTemplate = new System.Windows.Forms.TextBox();
            this.chbUseTemplate = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbFileFormat = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.rbtnInnerClient = new System.Windows.Forms.RadioButton();
            this.rbtnUseMAPI = new System.Windows.Forms.RadioButton();
            this.rbtnUseOutlook = new System.Windows.Forms.RadioButton();
            this.chbSendToFleet = new System.Windows.Forms.CheckBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tbMessageText = new System.Windows.Forms.TextBox();
            this.tbMessageSubject = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbCarbonCopy = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 539);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(676, 31);
            this.panel1.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::VIM.Properties.Resources.Ok;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.Location = new System.Drawing.Point(515, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::VIM.Properties.Resources.Close_Box_Red;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.Location = new System.Drawing.Point(596, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tbFileNameTemplate
            // 
            this.tbFileNameTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFileNameTemplate.Location = new System.Drawing.Point(19, 68);
            this.tbFileNameTemplate.Name = "tbFileNameTemplate";
            this.tbFileNameTemplate.Size = new System.Drawing.Size(645, 20);
            this.tbFileNameTemplate.TabIndex = 19;
            this.toolTip1.SetToolTip(this.tbFileNameTemplate, "Template may contains the following constants:\r\n%VesselName - name of the vessel\r" +
        "\n%Date - current date\r\n%DateTime - current date and time\r\n");
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::VIM.Properties.Resources.minus;
            this.btnDelete.Location = new System.Drawing.Point(1, 42);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(22, 22);
            this.btnDelete.TabIndex = 25;
            this.toolTip1.SetToolTip(this.btnDelete, "Delete selected address");
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Image = global::VIM.Properties.Resources.edit;
            this.btnEdit.Location = new System.Drawing.Point(1, 21);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(22, 22);
            this.btnEdit.TabIndex = 24;
            this.toolTip1.SetToolTip(this.btnEdit, "Edit selected address");
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            this.btnNew.Image = global::VIM.Properties.Resources.plus;
            this.btnNew.Location = new System.Drawing.Point(1, 0);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(22, 22);
            this.btnNew.TabIndex = 23;
            this.toolTip1.SetToolTip(this.btnNew, "Add new address");
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // chbShowFile
            // 
            this.chbShowFile.AutoSize = true;
            this.chbShowFile.Location = new System.Drawing.Point(40, 37);
            this.chbShowFile.Name = "chbShowFile";
            this.chbShowFile.Size = new System.Drawing.Size(108, 17);
            this.chbShowFile.TabIndex = 15;
            this.chbShowFile.Text = "Show created file";
            this.chbShowFile.UseVisualStyleBackColor = true;
            // 
            // rbtnSendSummary
            // 
            this.rbtnSendSummary.AutoSize = true;
            this.rbtnSendSummary.Location = new System.Drawing.Point(18, 56);
            this.rbtnSendSummary.Name = "rbtnSendSummary";
            this.rbtnSendSummary.Size = new System.Drawing.Size(147, 17);
            this.rbtnSendSummary.TabIndex = 10;
            this.rbtnSendSummary.Text = "Create and send summary";
            this.rbtnSendSummary.UseVisualStyleBackColor = true;
            this.rbtnSendSummary.Click += new System.EventHandler(this.rbtnSendSummary_Click);
            // 
            // rbtnCreateSummary
            // 
            this.rbtnCreateSummary.AutoSize = true;
            this.rbtnCreateSummary.Checked = true;
            this.rbtnCreateSummary.Location = new System.Drawing.Point(18, 14);
            this.rbtnCreateSummary.Name = "rbtnCreateSummary";
            this.rbtnCreateSummary.Size = new System.Drawing.Size(100, 17);
            this.rbtnCreateSummary.TabIndex = 9;
            this.rbtnCreateSummary.TabStop = true;
            this.rbtnCreateSummary.Text = "Create summary";
            this.rbtnCreateSummary.UseVisualStyleBackColor = true;
            this.rbtnCreateSummary.Click += new System.EventHandler(this.rbtnCreateSummary_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Carbon copy addresses";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnOpen);
            this.panel4.Controls.Add(this.btnBrowse);
            this.panel4.Controls.Add(this.tbTemplate);
            this.panel4.Controls.Add(this.chbUseTemplate);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.cbFileFormat);
            this.panel4.Controls.Add(this.tbFileNameTemplate);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(676, 94);
            this.panel4.TabIndex = 24;
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Image = global::VIM.Properties.Resources.open;
            this.btnOpen.Location = new System.Drawing.Point(642, 23);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(22, 22);
            this.btnOpen.TabIndex = 25;
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Image = global::VIM.Properties.Resources.folder_explore;
            this.btnBrowse.Location = new System.Drawing.Point(621, 23);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(22, 22);
            this.btnBrowse.TabIndex = 24;
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // tbTemplate
            // 
            this.tbTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTemplate.Location = new System.Drawing.Point(266, 24);
            this.tbTemplate.Name = "tbTemplate";
            this.tbTemplate.Size = new System.Drawing.Size(355, 20);
            this.tbTemplate.TabIndex = 23;
            // 
            // chbUseTemplate
            // 
            this.chbUseTemplate.AutoSize = true;
            this.chbUseTemplate.Location = new System.Drawing.Point(266, 7);
            this.chbUseTemplate.Name = "chbUseTemplate";
            this.chbUseTemplate.Size = new System.Drawing.Size(88, 17);
            this.chbUseTemplate.TabIndex = 22;
            this.chbUseTemplate.Text = "Use template";
            this.chbUseTemplate.UseVisualStyleBackColor = true;
            this.chbUseTemplate.CheckedChanged += new System.EventHandler(this.cbUseTemplate_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "File format";
            // 
            // cbFileFormat
            // 
            this.cbFileFormat.FormattingEnabled = true;
            this.cbFileFormat.Items.AddRange(new object[] {
            "Microsoft Word (.doc)",
            "Microsoft Word (.docx)",
            "Adobe Acrobat (.pdf)"});
            this.cbFileFormat.Location = new System.Drawing.Point(19, 24);
            this.cbFileFormat.Name = "cbFileFormat";
            this.cbFileFormat.Size = new System.Drawing.Size(230, 21);
            this.cbFileFormat.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(445, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "File name template (Example: \"History of SIRE Observations for mv %VesselName til" +
    "l %Date\")";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.rbtnInnerClient);
            this.panel6.Controls.Add(this.rbtnUseMAPI);
            this.panel6.Controls.Add(this.rbtnUseOutlook);
            this.panel6.Location = new System.Drawing.Point(33, 74);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(631, 23);
            this.panel6.TabIndex = 28;
            // 
            // rbtnInnerClient
            // 
            this.rbtnInnerClient.AutoSize = true;
            this.rbtnInnerClient.Location = new System.Drawing.Point(246, 4);
            this.rbtnInnerClient.Name = "rbtnInnerClient";
            this.rbtnInnerClient.Size = new System.Drawing.Size(126, 17);
            this.rbtnInnerClient.TabIndex = 30;
            this.rbtnInnerClient.TabStop = true;
            this.rbtnInnerClient.Text = "Application mail client";
            this.rbtnInnerClient.UseVisualStyleBackColor = true;
            // 
            // rbtnUseMAPI
            // 
            this.rbtnUseMAPI.AutoSize = true;
            this.rbtnUseMAPI.Location = new System.Drawing.Point(128, 4);
            this.rbtnUseMAPI.Name = "rbtnUseMAPI";
            this.rbtnUseMAPI.Size = new System.Drawing.Size(102, 17);
            this.rbtnUseMAPI.TabIndex = 29;
            this.rbtnUseMAPI.TabStop = true;
            this.rbtnUseMAPI.Text = "MAPI Mail Client";
            this.rbtnUseMAPI.UseVisualStyleBackColor = true;
            // 
            // rbtnUseOutlook
            // 
            this.rbtnUseOutlook.AutoSize = true;
            this.rbtnUseOutlook.Checked = true;
            this.rbtnUseOutlook.Location = new System.Drawing.Point(7, 4);
            this.rbtnUseOutlook.Name = "rbtnUseOutlook";
            this.rbtnUseOutlook.Size = new System.Drawing.Size(108, 17);
            this.rbtnUseOutlook.TabIndex = 28;
            this.rbtnUseOutlook.TabStop = true;
            this.rbtnUseOutlook.Text = "Microsoft Outlook";
            this.rbtnUseOutlook.UseVisualStyleBackColor = true;
            // 
            // chbSendToFleet
            // 
            this.chbSendToFleet.AutoSize = true;
            this.chbSendToFleet.Location = new System.Drawing.Point(161, 111);
            this.chbSendToFleet.Name = "chbSendToFleet";
            this.chbSendToFleet.Size = new System.Drawing.Size(142, 17);
            this.chbSendToFleet.TabIndex = 29;
            this.chbSendToFleet.Text = "Send a copy to the Fleet";
            this.chbSendToFleet.UseVisualStyleBackColor = true;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.panel5);
            this.panel7.Controls.Add(this.tbMessageSubject);
            this.panel7.Controls.Add(this.label2);
            this.panel7.Controls.Add(this.label1);
            this.panel7.Controls.Add(this.panel2);
            this.panel7.Controls.Add(this.rbtnCreateSummary);
            this.panel7.Controls.Add(this.chbSendToFleet);
            this.panel7.Controls.Add(this.rbtnSendSummary);
            this.panel7.Controls.Add(this.panel6);
            this.panel7.Controls.Add(this.chbShowFile);
            this.panel7.Controls.Add(this.label5);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 94);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(676, 445);
            this.panel7.TabIndex = 30;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.tbMessageText);
            this.panel5.Location = new System.Drawing.Point(30, 269);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(620, 170);
            this.panel5.TabIndex = 34;
            // 
            // tbMessageText
            // 
            this.tbMessageText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMessageText.Location = new System.Drawing.Point(0, 0);
            this.tbMessageText.Multiline = true;
            this.tbMessageText.Name = "tbMessageText";
            this.tbMessageText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMessageText.Size = new System.Drawing.Size(620, 170);
            this.tbMessageText.TabIndex = 15;
            // 
            // tbMessageSubject
            // 
            this.tbMessageSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMessageSubject.Location = new System.Drawing.Point(28, 230);
            this.tbMessageSubject.Name = "tbMessageSubject";
            this.tbMessageSubject.Size = new System.Drawing.Size(623, 20);
            this.tbMessageSubject.TabIndex = 33;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 253);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Message text";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 212);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Message subject";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.lbCarbonCopy);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(25, 130);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(625, 77);
            this.panel2.TabIndex = 30;
            // 
            // lbCarbonCopy
            // 
            this.lbCarbonCopy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCarbonCopy.FormattingEnabled = true;
            this.lbCarbonCopy.Location = new System.Drawing.Point(0, 0);
            this.lbCarbonCopy.Name = "lbCarbonCopy";
            this.lbCarbonCopy.Size = new System.Drawing.Size(601, 77);
            this.lbCarbonCopy.TabIndex = 20;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnDelete);
            this.panel3.Controls.Add(this.btnEdit);
            this.panel3.Controls.Add(this.btnNew);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(601, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(24, 77);
            this.panel3.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FrmVesselSummarySettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 570);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(500, 500);
            this.Name = "FrmVesselSummarySettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vessel Observations Summary Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmVesselSummarySettings_FormClosing);
            this.Load += new System.EventHandler(this.FrmVesselSummarySettings_Load);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox chbShowFile;
        private System.Windows.Forms.RadioButton rbtnSendSummary;
        private System.Windows.Forms.RadioButton rbtnCreateSummary;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbFileFormat;
        private System.Windows.Forms.TextBox tbFileNameTemplate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.RadioButton rbtnUseMAPI;
        private System.Windows.Forms.RadioButton rbtnUseOutlook;
        private System.Windows.Forms.CheckBox chbSendToFleet;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox tbTemplate;
        private System.Windows.Forms.CheckBox chbUseTemplate;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox tbMessageText;
        private System.Windows.Forms.TextBox tbMessageSubject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox lbCarbonCopy;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RadioButton rbtnInnerClient;
    }
}