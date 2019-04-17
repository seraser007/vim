namespace VIQManager
{
    partial class FrmMailClientSelection
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.tbMAPIPassword = new System.Windows.Forms.TextBox();
            this.tbMAPILogin = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbMAPIClients = new System.Windows.Forms.ComboBox();
            this.rbtnMAPI = new System.Windows.Forms.RadioButton();
            this.rbtnOutlook = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.Cancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 108);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(412, 31);
            this.panel1.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Image = global::VIQManager.Properties.Resources.Ok;
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOk.Location = new System.Drawing.Point(248, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 25);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Image = global::VIQManager.Properties.Resources.Close_Box_Red;
            this.Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Cancel.Location = new System.Drawing.Point(329, 3);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 25);
            this.Cancel.TabIndex = 0;
            this.Cancel.Text = "Cancel";
            this.Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // tbMAPIPassword
            // 
            this.tbMAPIPassword.Location = new System.Drawing.Point(102, 80);
            this.tbMAPIPassword.Name = "tbMAPIPassword";
            this.tbMAPIPassword.Size = new System.Drawing.Size(137, 20);
            this.tbMAPIPassword.TabIndex = 13;
            // 
            // tbMAPILogin
            // 
            this.tbMAPILogin.Location = new System.Drawing.Point(102, 56);
            this.tbMAPILogin.Name = "tbMAPILogin";
            this.tbMAPILogin.Size = new System.Drawing.Size(137, 20);
            this.tbMAPILogin.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(41, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Password";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Profile";
            // 
            // cbMAPIClients
            // 
            this.cbMAPIClients.FormattingEnabled = true;
            this.cbMAPIClients.Location = new System.Drawing.Point(162, 30);
            this.cbMAPIClients.Name = "cbMAPIClients";
            this.cbMAPIClients.Size = new System.Drawing.Size(239, 21);
            this.cbMAPIClients.TabIndex = 9;
            // 
            // rbtnMAPI
            // 
            this.rbtnMAPI.AutoSize = true;
            this.rbtnMAPI.Location = new System.Drawing.Point(12, 33);
            this.rbtnMAPI.Name = "rbtnMAPI";
            this.rbtnMAPI.Size = new System.Drawing.Size(144, 17);
            this.rbtnMAPI.TabIndex = 8;
            this.rbtnMAPI.Text = "Use selected MAPI client";
            this.rbtnMAPI.UseVisualStyleBackColor = true;
            this.rbtnMAPI.CheckedChanged += new System.EventHandler(this.rbtnMAPI_CheckedChanged);
            // 
            // rbtnOutlook
            // 
            this.rbtnOutlook.AutoSize = true;
            this.rbtnOutlook.Checked = true;
            this.rbtnOutlook.Location = new System.Drawing.Point(12, 12);
            this.rbtnOutlook.Name = "rbtnOutlook";
            this.rbtnOutlook.Size = new System.Drawing.Size(227, 17);
            this.rbtnOutlook.TabIndex = 7;
            this.rbtnOutlook.TabStop = true;
            this.rbtnOutlook.Text = "Use direct connection to Microsoft Outlook";
            this.rbtnOutlook.UseVisualStyleBackColor = true;
            this.rbtnOutlook.CheckedChanged += new System.EventHandler(this.rbtnOutlook_CheckedChanged);
            // 
            // FrmMailClientSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(412, 139);
            this.Controls.Add(this.tbMAPIPassword);
            this.Controls.Add(this.tbMAPILogin);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbMAPIClients);
            this.Controls.Add(this.rbtnMAPI);
            this.Controls.Add(this.rbtnOutlook);
            this.Controls.Add(this.panel1);
            this.Name = "FrmMailClientSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select mail client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMailClientSelection_FormClosing);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.TextBox tbMAPIPassword;
        private System.Windows.Forms.TextBox tbMAPILogin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbMAPIClients;
        private System.Windows.Forms.RadioButton rbtnMAPI;
        private System.Windows.Forms.RadioButton rbtnOutlook;
    }
}