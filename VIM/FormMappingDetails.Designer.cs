namespace VIM
{
    partial class FormMappingDetails
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMasterNumber = new System.Windows.Forms.TextBox();
            this.tbMasterGUID = new System.Windows.Forms.TextBox();
            this.tbSlaveNumber = new System.Windows.Forms.TextBox();
            this.tbSlaveGUID = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Master question number";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Master question GUID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Slave question number";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Slave question GUID";
            // 
            // tbMasterNumber
            // 
            this.tbMasterNumber.BackColor = System.Drawing.SystemColors.Window;
            this.tbMasterNumber.Location = new System.Drawing.Point(22, 44);
            this.tbMasterNumber.Name = "tbMasterNumber";
            this.tbMasterNumber.ReadOnly = true;
            this.tbMasterNumber.Size = new System.Drawing.Size(117, 20);
            this.tbMasterNumber.TabIndex = 4;
            // 
            // tbMasterGUID
            // 
            this.tbMasterGUID.BackColor = System.Drawing.SystemColors.Window;
            this.tbMasterGUID.Location = new System.Drawing.Point(22, 91);
            this.tbMasterGUID.Name = "tbMasterGUID";
            this.tbMasterGUID.ReadOnly = true;
            this.tbMasterGUID.Size = new System.Drawing.Size(381, 20);
            this.tbMasterGUID.TabIndex = 5;
            // 
            // tbSlaveNumber
            // 
            this.tbSlaveNumber.BackColor = System.Drawing.SystemColors.Window;
            this.tbSlaveNumber.Location = new System.Drawing.Point(22, 140);
            this.tbSlaveNumber.Name = "tbSlaveNumber";
            this.tbSlaveNumber.ReadOnly = true;
            this.tbSlaveNumber.Size = new System.Drawing.Size(117, 20);
            this.tbSlaveNumber.TabIndex = 6;
            // 
            // tbSlaveGUID
            // 
            this.tbSlaveGUID.BackColor = System.Drawing.SystemColors.Window;
            this.tbSlaveGUID.Location = new System.Drawing.Point(22, 189);
            this.tbSlaveGUID.Name = "tbSlaveGUID";
            this.tbSlaveGUID.ReadOnly = true;
            this.tbSlaveGUID.Size = new System.Drawing.Size(381, 20);
            this.tbSlaveGUID.TabIndex = 7;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Image = global::VIM.Properties.Resources.Close_Box_Red;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Location = new System.Drawing.Point(328, 234);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 25);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // FormMappingDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 274);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tbSlaveGUID);
            this.Controls.Add(this.tbSlaveNumber);
            this.Controls.Add(this.tbMasterGUID);
            this.Controls.Add(this.tbMasterNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormMappingDetails";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mapping details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbMasterNumber;
        private System.Windows.Forms.TextBox tbMasterGUID;
        private System.Windows.Forms.TextBox tbSlaveNumber;
        private System.Windows.Forms.TextBox tbSlaveGUID;
        private System.Windows.Forms.Button btnClose;
    }
}