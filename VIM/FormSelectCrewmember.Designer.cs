namespace VIM
{
    partial class FormSelectCrewmember
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbVesselName = new System.Windows.Forms.TextBox();
            this.btnInfo = new System.Windows.Forms.Button();
            this.btnCrewOnBoard = new System.Windows.Forms.Button();
            this.btnLocateCrew = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.cbCrewmember = new System.Windows.Forms.ComboBox();
            this.tbPosition = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.dtInspectionDate = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vessel name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Position";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Crewmember";
            // 
            // tbVesselName
            // 
            this.tbVesselName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbVesselName.BackColor = System.Drawing.SystemColors.Window;
            this.tbVesselName.Location = new System.Drawing.Point(15, 25);
            this.tbVesselName.Name = "tbVesselName";
            this.tbVesselName.ReadOnly = true;
            this.tbVesselName.Size = new System.Drawing.Size(392, 20);
            this.tbVesselName.TabIndex = 3;
            // 
            // btnInfo
            // 
            this.btnInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInfo.Image = global::VIM.Properties.Resources.user_male_information_16;
            this.btnInfo.Location = new System.Drawing.Point(340, 153);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(23, 22);
            this.btnInfo.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btnInfo, "View crewmember information");
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // btnCrewOnBoard
            // 
            this.btnCrewOnBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCrewOnBoard.Image = global::VIM.Properties.Resources.user_group_16;
            this.btnCrewOnBoard.Location = new System.Drawing.Point(362, 153);
            this.btnCrewOnBoard.Name = "btnCrewOnBoard";
            this.btnCrewOnBoard.Size = new System.Drawing.Size(23, 22);
            this.btnCrewOnBoard.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnCrewOnBoard, "Show list of crewmembers for selected vessel");
            this.btnCrewOnBoard.UseVisualStyleBackColor = true;
            this.btnCrewOnBoard.Click += new System.EventHandler(this.btnCrewOnBoard_Click);
            // 
            // btnLocateCrew
            // 
            this.btnLocateCrew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLocateCrew.Image = global::VIM.Properties.Resources.user_male_select_16;
            this.btnLocateCrew.Location = new System.Drawing.Point(384, 153);
            this.btnLocateCrew.Name = "btnLocateCrew";
            this.btnLocateCrew.Size = new System.Drawing.Size(23, 22);
            this.btnLocateCrew.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btnLocateCrew, "Locate proper crewmember");
            this.btnLocateCrew.UseVisualStyleBackColor = true;
            this.btnLocateCrew.Click += new System.EventHandler(this.btnLocateCrew_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::VIM.Properties.Resources.cancel_16;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.Location = new System.Drawing.Point(332, 197);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 9;
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
            this.btnOk.Location = new System.Drawing.Point(251, 197);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 25);
            this.btnOk.TabIndex = 10;
            this.btnOk.Text = "OK";
            this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // cbCrewmember
            // 
            this.cbCrewmember.FormattingEnabled = true;
            this.cbCrewmember.Location = new System.Drawing.Point(15, 154);
            this.cbCrewmember.Name = "cbCrewmember";
            this.cbCrewmember.Size = new System.Drawing.Size(324, 21);
            this.cbCrewmember.TabIndex = 11;
            // 
            // tbPosition
            // 
            this.tbPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPosition.BackColor = System.Drawing.SystemColors.Window;
            this.tbPosition.Location = new System.Drawing.Point(15, 110);
            this.tbPosition.Name = "tbPosition";
            this.tbPosition.ReadOnly = true;
            this.tbPosition.Size = new System.Drawing.Size(392, 20);
            this.tbPosition.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Date of inspection";
            // 
            // dtInspectionDate
            // 
            this.dtInspectionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtInspectionDate.Location = new System.Drawing.Point(15, 67);
            this.dtInspectionDate.Name = "dtInspectionDate";
            this.dtInspectionDate.Size = new System.Drawing.Size(107, 20);
            this.dtInspectionDate.TabIndex = 14;
            // 
            // FormSelectCrewmember
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 232);
            this.Controls.Add(this.dtInspectionDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbPosition);
            this.Controls.Add(this.cbCrewmember);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLocateCrew);
            this.Controls.Add(this.btnCrewOnBoard);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.tbVesselName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormSelectCrewmember";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select crewmember";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSelectCrewmember_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbVesselName;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.Button btnCrewOnBoard;
        private System.Windows.Forms.Button btnLocateCrew;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox cbCrewmember;
        private System.Windows.Forms.TextBox tbPosition;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtInspectionDate;
    }
}