namespace VIM
{
    partial class FrmCrewOnBoardEditor
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.cbVessel = new System.Windows.Forms.ComboBox();
            this.cbPosition = new System.Windows.Forms.ComboBox();
            this.dtSignOn = new System.Windows.Forms.DateTimePicker();
            this.dtSignOff = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Location = new System.Drawing.Point(22, 30);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(384, 20);
            this.tbName.TabIndex = 1;
            // 
            // cbVessel
            // 
            this.cbVessel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbVessel.FormattingEnabled = true;
            this.cbVessel.Location = new System.Drawing.Point(22, 79);
            this.cbVessel.Name = "cbVessel";
            this.cbVessel.Size = new System.Drawing.Size(384, 21);
            this.cbVessel.TabIndex = 2;
            this.cbVessel.SelectedValueChanged += new System.EventHandler(this.cbVessel_SelectedValueChanged);
            // 
            // cbPosition
            // 
            this.cbPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPosition.FormattingEnabled = true;
            this.cbPosition.Location = new System.Drawing.Point(22, 131);
            this.cbPosition.Name = "cbPosition";
            this.cbPosition.Size = new System.Drawing.Size(384, 21);
            this.cbPosition.TabIndex = 3;
            // 
            // dtSignOn
            // 
            this.dtSignOn.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtSignOn.Location = new System.Drawing.Point(22, 182);
            this.dtSignOn.Name = "dtSignOn";
            this.dtSignOn.Size = new System.Drawing.Size(112, 20);
            this.dtSignOn.TabIndex = 4;
            this.dtSignOn.ValueChanged += new System.EventHandler(this.dtSignOn_ValueChanged);
            this.dtSignOn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtSignOn_KeyDown);
            // 
            // dtSignOff
            // 
            this.dtSignOff.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtSignOff.Location = new System.Drawing.Point(22, 232);
            this.dtSignOff.Name = "dtSignOff";
            this.dtSignOff.Size = new System.Drawing.Size(112, 20);
            this.dtSignOff.TabIndex = 5;
            this.dtSignOff.ValueChanged += new System.EventHandler(this.dtSignOff_ValueChanged);
            this.dtSignOff.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtSignOff_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Vessel";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Position";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Date sign on";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 216);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Date sign off";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::VIM.Properties.Resources.delete;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.Location = new System.Drawing.Point(329, 270);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::VIM.Properties.Resources.Ok;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.Location = new System.Drawing.Point(248, 270);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "OK";
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // FrmCrewOnBoardEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(421, 308);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtSignOff);
            this.Controls.Add(this.dtSignOn);
            this.Controls.Add(this.cbPosition);
            this.Controls.Add(this.cbVessel);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label1);
            this.Name = "FrmCrewOnBoardEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crew on board editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCrewOnBoardEditor_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.ComboBox cbVessel;
        private System.Windows.Forms.ComboBox cbPosition;
        private System.Windows.Forms.DateTimePicker dtSignOn;
        private System.Windows.Forms.DateTimePicker dtSignOff;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}