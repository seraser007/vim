namespace VIM
{
    partial class frmMessageSettings
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.chbAskForVessel = new System.Windows.Forms.CheckBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lbCarbonCopy = new System.Windows.Forms.ListBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.chbSendToFleet = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tbSelectedFolder = new System.Windows.Forms.TextBox();
            this.rbtnUseSelectedFolder = new System.Windows.Forms.RadioButton();
            this.rbtnUseTempFolder = new System.Windows.Forms.RadioButton();
            this.panel6 = new System.Windows.Forms.Panel();
            this.rbtnApplication = new System.Windows.Forms.RadioButton();
            this.rbtnMAPI = new System.Windows.Forms.RadioButton();
            this.rbtnOutlook = new System.Windows.Forms.RadioButton();
            this.rbtnCreateSend = new System.Windows.Forms.RadioButton();
            this.rbtnCreateSave = new System.Windows.Forms.RadioButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tbMessageBody = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbMessageSubject = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.numCount = new System.Windows.Forms.NumericUpDown();
            this.chbSendLast = new System.Windows.Forms.CheckBox();
            this.chbSizeLimit = new System.Windows.Forms.CheckBox();
            this.cbDimension = new System.Windows.Forms.ComboBox();
            this.numSize = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.chbUseChart = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSize)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 520);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(775, 33);
            this.panel1.TabIndex = 12;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::VIM.Properties.Resources.Ok;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.Location = new System.Drawing.Point(607, 3);
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
            this.btnCancel.Location = new System.Drawing.Point(688, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(775, 520);
            this.panel2.TabIndex = 15;
            // 
            // panel4
            // 
            this.panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel4.Controls.Add(this.chbUseChart);
            this.panel4.Controls.Add(this.chbAskForVessel);
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Controls.Add(this.chbSendToFleet);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.rbtnCreateSend);
            this.panel4.Controls.Add(this.rbtnCreateSave);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.tbMessageSubject);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(775, 470);
            this.panel4.TabIndex = 28;
            // 
            // chbAskForVessel
            // 
            this.chbAskForVessel.AutoSize = true;
            this.chbAskForVessel.Location = new System.Drawing.Point(12, 164);
            this.chbAskForVessel.Name = "chbAskForVessel";
            this.chbAskForVessel.Size = new System.Drawing.Size(180, 17);
            this.chbAskForVessel.TabIndex = 42;
            this.chbAskForVessel.Text = "Ask for vessel name before send";
            this.chbAskForVessel.UseVisualStyleBackColor = true;
            this.chbAskForVessel.CheckStateChanged += new System.EventHandler(this.chbAskForVessel_CheckStateChanged);
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel8.Controls.Add(this.lbCarbonCopy);
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Location = new System.Drawing.Point(12, 218);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(745, 77);
            this.panel8.TabIndex = 41;
            // 
            // lbCarbonCopy
            // 
            this.lbCarbonCopy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCarbonCopy.FormattingEnabled = true;
            this.lbCarbonCopy.Location = new System.Drawing.Point(0, 0);
            this.lbCarbonCopy.Name = "lbCarbonCopy";
            this.lbCarbonCopy.Size = new System.Drawing.Size(721, 77);
            this.lbCarbonCopy.TabIndex = 20;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.btnDelete);
            this.panel9.Controls.Add(this.btnEdit);
            this.panel9.Controls.Add(this.btnNew);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel9.Location = new System.Drawing.Point(721, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(24, 77);
            this.panel9.TabIndex = 0;
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
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
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
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
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
            // chbSendToFleet
            // 
            this.chbSendToFleet.AutoSize = true;
            this.chbSendToFleet.Location = new System.Drawing.Point(39, 183);
            this.chbSendToFleet.Name = "chbSendToFleet";
            this.chbSendToFleet.Size = new System.Drawing.Size(142, 17);
            this.chbSendToFleet.TabIndex = 40;
            this.chbSendToFleet.Text = "Send a copy to the Fleet";
            this.chbSendToFleet.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 202);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 13);
            this.label5.TabIndex = 39;
            this.label5.Text = "Carbon copy addresses";
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.Controls.Add(this.btnBrowse);
            this.panel7.Controls.Add(this.tbSelectedFolder);
            this.panel7.Controls.Add(this.rbtnUseSelectedFolder);
            this.panel7.Controls.Add(this.rbtnUseTempFolder);
            this.panel7.Location = new System.Drawing.Point(35, 57);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(728, 49);
            this.panel7.TabIndex = 38;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Image = global::VIM.Properties.Resources.folder_explore;
            this.btnBrowse.Location = new System.Drawing.Point(703, 23);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(22, 22);
            this.btnBrowse.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnBrowse, "Browse for folder");
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // tbSelectedFolder
            // 
            this.tbSelectedFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSelectedFolder.Location = new System.Drawing.Point(119, 25);
            this.tbSelectedFolder.Name = "tbSelectedFolder";
            this.tbSelectedFolder.Size = new System.Drawing.Size(582, 20);
            this.tbSelectedFolder.TabIndex = 2;
            // 
            // rbtnUseSelectedFolder
            // 
            this.rbtnUseSelectedFolder.AutoSize = true;
            this.rbtnUseSelectedFolder.Location = new System.Drawing.Point(4, 26);
            this.rbtnUseSelectedFolder.Name = "rbtnUseSelectedFolder";
            this.rbtnUseSelectedFolder.Size = new System.Drawing.Size(116, 17);
            this.rbtnUseSelectedFolder.TabIndex = 1;
            this.rbtnUseSelectedFolder.TabStop = true;
            this.rbtnUseSelectedFolder.Text = "Use selected folder";
            this.rbtnUseSelectedFolder.UseVisualStyleBackColor = true;
            // 
            // rbtnUseTempFolder
            // 
            this.rbtnUseTempFolder.AutoSize = true;
            this.rbtnUseTempFolder.Location = new System.Drawing.Point(4, 3);
            this.rbtnUseTempFolder.Name = "rbtnUseTempFolder";
            this.rbtnUseTempFolder.Size = new System.Drawing.Size(176, 17);
            this.rbtnUseTempFolder.TabIndex = 0;
            this.rbtnUseTempFolder.TabStop = true;
            this.rbtnUseTempFolder.Text = "Use application temporary folder";
            this.rbtnUseTempFolder.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.rbtnApplication);
            this.panel6.Controls.Add(this.rbtnMAPI);
            this.panel6.Controls.Add(this.rbtnOutlook);
            this.panel6.Location = new System.Drawing.Point(31, 132);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(732, 26);
            this.panel6.TabIndex = 37;
            // 
            // rbtnApplication
            // 
            this.rbtnApplication.AutoSize = true;
            this.rbtnApplication.Location = new System.Drawing.Point(261, 3);
            this.rbtnApplication.Name = "rbtnApplication";
            this.rbtnApplication.Size = new System.Drawing.Size(126, 17);
            this.rbtnApplication.TabIndex = 39;
            this.rbtnApplication.TabStop = true;
            this.rbtnApplication.Text = "Application mail client";
            this.rbtnApplication.UseVisualStyleBackColor = true;
            // 
            // rbtnMAPI
            // 
            this.rbtnMAPI.AutoSize = true;
            this.rbtnMAPI.Location = new System.Drawing.Point(136, 3);
            this.rbtnMAPI.Name = "rbtnMAPI";
            this.rbtnMAPI.Size = new System.Drawing.Size(102, 17);
            this.rbtnMAPI.TabIndex = 38;
            this.rbtnMAPI.Text = "MAPI Mail Client";
            this.rbtnMAPI.UseVisualStyleBackColor = true;
            // 
            // rbtnOutlook
            // 
            this.rbtnOutlook.AutoSize = true;
            this.rbtnOutlook.Checked = true;
            this.rbtnOutlook.Location = new System.Drawing.Point(3, 3);
            this.rbtnOutlook.Name = "rbtnOutlook";
            this.rbtnOutlook.Size = new System.Drawing.Size(108, 17);
            this.rbtnOutlook.TabIndex = 37;
            this.rbtnOutlook.TabStop = true;
            this.rbtnOutlook.Text = "Microsoft Outlook";
            this.rbtnOutlook.UseVisualStyleBackColor = true;
            // 
            // rbtnCreateSend
            // 
            this.rbtnCreateSend.AutoSize = true;
            this.rbtnCreateSend.Location = new System.Drawing.Point(15, 111);
            this.rbtnCreateSend.Name = "rbtnCreateSend";
            this.rbtnCreateSend.Size = new System.Drawing.Size(147, 17);
            this.rbtnCreateSend.TabIndex = 34;
            this.rbtnCreateSend.Text = "Create and send summary";
            this.rbtnCreateSend.UseVisualStyleBackColor = true;
            this.rbtnCreateSend.Click += new System.EventHandler(this.rbtnCreateSend_Click);
            // 
            // rbtnCreateSave
            // 
            this.rbtnCreateSave.AutoSize = true;
            this.rbtnCreateSave.Checked = true;
            this.rbtnCreateSave.Location = new System.Drawing.Point(15, 36);
            this.rbtnCreateSave.Name = "rbtnCreateSave";
            this.rbtnCreateSave.Size = new System.Drawing.Size(147, 17);
            this.rbtnCreateSave.TabIndex = 33;
            this.rbtnCreateSave.TabStop = true;
            this.rbtnCreateSave.Text = "Create and save summary";
            this.rbtnCreateSave.UseVisualStyleBackColor = true;
            this.rbtnCreateSave.Click += new System.EventHandler(this.rbtnCreateSave_Click);
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.SystemColors.Control;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.tbMessageBody);
            this.panel5.Location = new System.Drawing.Point(12, 352);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(752, 106);
            this.panel5.TabIndex = 32;
            // 
            // tbMessageBody
            // 
            this.tbMessageBody.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbMessageBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMessageBody.Location = new System.Drawing.Point(0, 0);
            this.tbMessageBody.Name = "tbMessageBody";
            this.tbMessageBody.Size = new System.Drawing.Size(750, 104);
            this.tbMessageBody.TabIndex = 31;
            this.tbMessageBody.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 336);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Message text";
            // 
            // tbMessageSubject
            // 
            this.tbMessageSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMessageSubject.Location = new System.Drawing.Point(13, 313);
            this.tbMessageSubject.Name = "tbMessageSubject";
            this.tbMessageSubject.Size = new System.Drawing.Size(751, 20);
            this.tbMessageSubject.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 298);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Message subject";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.numCount);
            this.panel3.Controls.Add(this.chbSendLast);
            this.panel3.Controls.Add(this.chbSizeLimit);
            this.panel3.Controls.Add(this.cbDimension);
            this.panel3.Controls.Add(this.numSize);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 470);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(775, 50);
            this.panel3.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(438, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "inspections";
            // 
            // numCount
            // 
            this.numCount.Location = new System.Drawing.Point(355, 24);
            this.numCount.Name = "numCount";
            this.numCount.Size = new System.Drawing.Size(77, 20);
            this.numCount.TabIndex = 30;
            // 
            // chbSendLast
            // 
            this.chbSendLast.AutoSize = true;
            this.chbSendLast.Location = new System.Drawing.Point(279, 27);
            this.chbSendLast.Name = "chbSendLast";
            this.chbSendLast.Size = new System.Drawing.Size(70, 17);
            this.chbSendLast.TabIndex = 29;
            this.chbSendLast.Text = "Send last";
            this.chbSendLast.UseVisualStyleBackColor = true;
            // 
            // chbSizeLimit
            // 
            this.chbSizeLimit.AutoSize = true;
            this.chbSizeLimit.Location = new System.Drawing.Point(13, 28);
            this.chbSizeLimit.Name = "chbSizeLimit";
            this.chbSizeLimit.Size = new System.Drawing.Size(66, 17);
            this.chbSizeLimit.TabIndex = 28;
            this.chbSizeLimit.Text = "Size limit";
            this.chbSizeLimit.UseVisualStyleBackColor = true;
            // 
            // cbDimension
            // 
            this.cbDimension.FormattingEnabled = true;
            this.cbDimension.Items.AddRange(new object[] {
            "KB",
            "MB",
            "GB"});
            this.cbDimension.Location = new System.Drawing.Point(172, 24);
            this.cbDimension.Name = "cbDimension";
            this.cbDimension.Size = new System.Drawing.Size(55, 21);
            this.cbDimension.TabIndex = 27;
            this.cbDimension.Text = "KB";
            // 
            // numSize
            // 
            this.numSize.Location = new System.Drawing.Point(89, 25);
            this.numSize.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numSize.Name = "numSize";
            this.numSize.Size = new System.Drawing.Size(77, 20);
            this.numSize.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Attachment restrictions";
            // 
            // chbUseChart
            // 
            this.chbUseChart.AutoSize = true;
            this.chbUseChart.Location = new System.Drawing.Point(12, 12);
            this.chbUseChart.Name = "chbUseChart";
            this.chbUseChart.Size = new System.Drawing.Size(84, 17);
            this.chbUseChart.TabIndex = 43;
            this.chbUseChart.Text = "Create chart";
            this.chbUseChart.UseVisualStyleBackColor = true;
            // 
            // frmMessageSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 553);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(550, 450);
            this.Name = "frmMessageSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inspector summary message settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMessageSettings_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbMessageSubject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numCount;
        private System.Windows.Forms.CheckBox chbSendLast;
        private System.Windows.Forms.CheckBox chbSizeLimit;
        private System.Windows.Forms.ComboBox cbDimension;
        private System.Windows.Forms.NumericUpDown numSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RichTextBox tbMessageBody;
        private System.Windows.Forms.RadioButton rbtnCreateSend;
        private System.Windows.Forms.RadioButton rbtnCreateSave;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.RadioButton rbtnMAPI;
        private System.Windows.Forms.RadioButton rbtnOutlook;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox tbSelectedFolder;
        private System.Windows.Forms.RadioButton rbtnUseSelectedFolder;
        private System.Windows.Forms.RadioButton rbtnUseTempFolder;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.RadioButton rbtnApplication;
        private System.Windows.Forms.CheckBox chbSendToFleet;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.ListBox lbCarbonCopy;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.CheckBox chbAskForVessel;
        private System.Windows.Forms.CheckBox chbUseChart;
    }
}