namespace VIM
{
    partial class FrmCustomDialog
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
            this.tbDialog = new System.Windows.Forms.TextBox();
            this.cbDialog = new System.Windows.Forms.CheckBox();
            this.btnNo = new System.Windows.Forms.Button();
            this.btnYes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbDialog
            // 
            this.tbDialog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbDialog.Enabled = false;
            this.tbDialog.Location = new System.Drawing.Point(12, 12);
            this.tbDialog.Multiline = true;
            this.tbDialog.Name = "tbDialog";
            this.tbDialog.ReadOnly = true;
            this.tbDialog.Size = new System.Drawing.Size(382, 94);
            this.tbDialog.TabIndex = 0;
            this.tbDialog.Text = "Message text";
            // 
            // cbDialog
            // 
            this.cbDialog.AutoSize = true;
            this.cbDialog.Location = new System.Drawing.Point(12, 120);
            this.cbDialog.Name = "cbDialog";
            this.cbDialog.Size = new System.Drawing.Size(80, 17);
            this.cbDialog.TabIndex = 1;
            this.cbDialog.Text = "checkBox1";
            this.cbDialog.UseVisualStyleBackColor = true;
            // 
            // btnNo
            // 
            this.btnNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnNo.Location = new System.Drawing.Point(319, 120);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(75, 23);
            this.btnNo.TabIndex = 2;
            this.btnNo.Text = "No";
            this.btnNo.UseVisualStyleBackColor = true;
            // 
            // btnYes
            // 
            this.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnYes.Location = new System.Drawing.Point(238, 120);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(75, 23);
            this.btnYes.TabIndex = 3;
            this.btnYes.Text = "Yes";
            this.btnYes.UseVisualStyleBackColor = true;
            // 
            // FrmCustomDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 149);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.cbDialog);
            this.Controls.Add(this.tbDialog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmCustomDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCustomDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbDialog;
        private System.Windows.Forms.CheckBox cbDialog;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Button btnYes;
    }
}