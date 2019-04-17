namespace VIM
{
    partial class FrmSMTPSettings
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
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbSender = new System.Windows.Forms.TextBox();
            this.tbAddress = new System.Windows.Forms.TextBox();
            this.tbSMTPServer = new System.Windows.Forms.TextBox();
            this.nePort = new System.Windows.Forms.NumericUpDown();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.cbConnectionSecurity = new System.Windows.Forms.ComboBox();
            this.cbAuthenticationMethod = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSMTP = new System.Windows.Forms.TabPage();
            this.tabExchange = new System.Windows.Forms.TabPage();
            this.rbtnSMTP = new System.Windows.Forms.RadioButton();
            this.rbtnExchange = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tbExUser = new System.Windows.Forms.TextBox();
            this.tbExPassword = new System.Windows.Forms.TextBox();
            this.tbServerURL = new System.Windows.Forms.TextBox();
            this.tbDomain = new System.Windows.Forms.TextBox();
            this.tbEmail = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nePort)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabSMTP.SuspendLayout();
            this.tabExchange.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sender";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Address";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "SMTP Server";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Port";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "User name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 207);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Connection security";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 247);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Authentication method";
            // 
            // tbSender
            // 
            this.tbSender.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSender.Location = new System.Drawing.Point(9, 28);
            this.tbSender.Name = "tbSender";
            this.tbSender.Size = new System.Drawing.Size(475, 20);
            this.tbSender.TabIndex = 7;
            // 
            // tbAddress
            // 
            this.tbAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAddress.Location = new System.Drawing.Point(8, 67);
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Size = new System.Drawing.Size(475, 20);
            this.tbAddress.TabIndex = 8;
            // 
            // tbSMTPServer
            // 
            this.tbSMTPServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSMTPServer.Location = new System.Drawing.Point(9, 106);
            this.tbSMTPServer.Name = "tbSMTPServer";
            this.tbSMTPServer.Size = new System.Drawing.Size(475, 20);
            this.tbSMTPServer.TabIndex = 9;
            // 
            // nePort
            // 
            this.nePort.Location = new System.Drawing.Point(9, 145);
            this.nePort.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nePort.Name = "nePort";
            this.nePort.Size = new System.Drawing.Size(120, 20);
            this.nePort.TabIndex = 10;
            // 
            // tbUserName
            // 
            this.tbUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUserName.Location = new System.Drawing.Point(9, 184);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(475, 20);
            this.tbUserName.TabIndex = 11;
            // 
            // cbConnectionSecurity
            // 
            this.cbConnectionSecurity.FormattingEnabled = true;
            this.cbConnectionSecurity.Items.AddRange(new object[] {
            "None",
            "STARTTLS",
            "SSL/TLS"});
            this.cbConnectionSecurity.Location = new System.Drawing.Point(8, 223);
            this.cbConnectionSecurity.Name = "cbConnectionSecurity";
            this.cbConnectionSecurity.Size = new System.Drawing.Size(232, 21);
            this.cbConnectionSecurity.TabIndex = 12;
            this.cbConnectionSecurity.Text = "None";
            // 
            // cbAuthenticationMethod
            // 
            this.cbAuthenticationMethod.FormattingEnabled = true;
            this.cbAuthenticationMethod.Items.AddRange(new object[] {
            "No authentication",
            "Normal password",
            "Encrypted password",
            ""});
            this.cbAuthenticationMethod.Location = new System.Drawing.Point(8, 263);
            this.cbAuthenticationMethod.Name = "cbAuthenticationMethod";
            this.cbAuthenticationMethod.Size = new System.Drawing.Size(232, 21);
            this.cbAuthenticationMethod.TabIndex = 13;
            this.cbAuthenticationMethod.Text = "No authentication";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 287);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Password";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(8, 303);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(232, 20);
            this.tbPassword.TabIndex = 15;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::VIM.Properties.Resources.cancel_16;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.Location = new System.Drawing.Point(442, 409);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 16;
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
            this.btnOK.Location = new System.Drawing.Point(361, 409);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 17;
            this.btnOK.Text = "OK";
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabSMTP);
            this.tabControl1.Controls.Add(this.tabExchange);
            this.tabControl1.Location = new System.Drawing.Point(12, 42);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(505, 358);
            this.tabControl1.TabIndex = 18;
            // 
            // tabSMTP
            // 
            this.tabSMTP.Controls.Add(this.label1);
            this.tabSMTP.Controls.Add(this.label2);
            this.tabSMTP.Controls.Add(this.label3);
            this.tabSMTP.Controls.Add(this.tbPassword);
            this.tabSMTP.Controls.Add(this.label4);
            this.tabSMTP.Controls.Add(this.label8);
            this.tabSMTP.Controls.Add(this.label5);
            this.tabSMTP.Controls.Add(this.cbAuthenticationMethod);
            this.tabSMTP.Controls.Add(this.label6);
            this.tabSMTP.Controls.Add(this.cbConnectionSecurity);
            this.tabSMTP.Controls.Add(this.label7);
            this.tabSMTP.Controls.Add(this.tbUserName);
            this.tabSMTP.Controls.Add(this.tbSender);
            this.tabSMTP.Controls.Add(this.nePort);
            this.tabSMTP.Controls.Add(this.tbAddress);
            this.tabSMTP.Controls.Add(this.tbSMTPServer);
            this.tabSMTP.Location = new System.Drawing.Point(4, 22);
            this.tabSMTP.Name = "tabSMTP";
            this.tabSMTP.Padding = new System.Windows.Forms.Padding(3);
            this.tabSMTP.Size = new System.Drawing.Size(497, 332);
            this.tabSMTP.TabIndex = 0;
            this.tabSMTP.Text = "SMTP";
            this.tabSMTP.UseVisualStyleBackColor = true;
            // 
            // tabExchange
            // 
            this.tabExchange.Controls.Add(this.label13);
            this.tabExchange.Controls.Add(this.tbEmail);
            this.tabExchange.Controls.Add(this.tbDomain);
            this.tabExchange.Controls.Add(this.tbServerURL);
            this.tabExchange.Controls.Add(this.tbExPassword);
            this.tabExchange.Controls.Add(this.tbExUser);
            this.tabExchange.Controls.Add(this.label12);
            this.tabExchange.Controls.Add(this.label11);
            this.tabExchange.Controls.Add(this.label10);
            this.tabExchange.Controls.Add(this.label9);
            this.tabExchange.Location = new System.Drawing.Point(4, 22);
            this.tabExchange.Name = "tabExchange";
            this.tabExchange.Padding = new System.Windows.Forms.Padding(3);
            this.tabExchange.Size = new System.Drawing.Size(497, 332);
            this.tabExchange.TabIndex = 1;
            this.tabExchange.Text = "Exchange";
            this.tabExchange.UseVisualStyleBackColor = true;
            // 
            // rbtnSMTP
            // 
            this.rbtnSMTP.AutoSize = true;
            this.rbtnSMTP.Checked = true;
            this.rbtnSMTP.Location = new System.Drawing.Point(20, 17);
            this.rbtnSMTP.Name = "rbtnSMTP";
            this.rbtnSMTP.Size = new System.Drawing.Size(55, 17);
            this.rbtnSMTP.TabIndex = 19;
            this.rbtnSMTP.TabStop = true;
            this.rbtnSMTP.Text = "SMTP";
            this.rbtnSMTP.UseVisualStyleBackColor = true;
            this.rbtnSMTP.CheckedChanged += new System.EventHandler(this.rbtnSMTP_CheckedChanged);
            // 
            // rbtnExchange
            // 
            this.rbtnExchange.AutoSize = true;
            this.rbtnExchange.Location = new System.Drawing.Point(91, 17);
            this.rbtnExchange.Name = "rbtnExchange";
            this.rbtnExchange.Size = new System.Drawing.Size(73, 17);
            this.rbtnExchange.TabIndex = 20;
            this.rbtnExchange.Text = "Exchange";
            this.rbtnExchange.UseVisualStyleBackColor = true;
            this.rbtnExchange.CheckedChanged += new System.EventHandler(this.rbtnExchange_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "User";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Password";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 96);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Server URL";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 140);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Domain";
            // 
            // tbExUser
            // 
            this.tbExUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbExUser.Location = new System.Drawing.Point(9, 30);
            this.tbExUser.Name = "tbExUser";
            this.tbExUser.Size = new System.Drawing.Size(475, 20);
            this.tbExUser.TabIndex = 4;
            // 
            // tbExPassword
            // 
            this.tbExPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbExPassword.Location = new System.Drawing.Point(9, 74);
            this.tbExPassword.Name = "tbExPassword";
            this.tbExPassword.Size = new System.Drawing.Size(475, 20);
            this.tbExPassword.TabIndex = 5;
            // 
            // tbServerURL
            // 
            this.tbServerURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbServerURL.Location = new System.Drawing.Point(9, 112);
            this.tbServerURL.Name = "tbServerURL";
            this.tbServerURL.Size = new System.Drawing.Size(475, 20);
            this.tbServerURL.TabIndex = 6;
            // 
            // tbDomain
            // 
            this.tbDomain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDomain.Location = new System.Drawing.Point(9, 156);
            this.tbDomain.Name = "tbDomain";
            this.tbDomain.Size = new System.Drawing.Size(475, 20);
            this.tbDomain.TabIndex = 7;
            // 
            // tbEmail
            // 
            this.tbEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbEmail.Location = new System.Drawing.Point(9, 202);
            this.tbEmail.Name = "tbEmail";
            this.tbEmail.Size = new System.Drawing.Size(475, 20);
            this.tbEmail.TabIndex = 8;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 186);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 13);
            this.label13.TabIndex = 9;
            this.label13.Text = "Email";
            // 
            // FrmSMTPSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 444);
            this.Controls.Add(this.rbtnExchange);
            this.Controls.Add(this.rbtnSMTP);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Name = "FrmSMTPSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SMTP server settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSMTPSettings_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.nePort)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabSMTP.ResumeLayout(false);
            this.tabSMTP.PerformLayout();
            this.tabExchange.ResumeLayout(false);
            this.tabExchange.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbSender;
        private System.Windows.Forms.TextBox tbAddress;
        private System.Windows.Forms.TextBox tbSMTPServer;
        private System.Windows.Forms.NumericUpDown nePort;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.ComboBox cbConnectionSecurity;
        private System.Windows.Forms.ComboBox cbAuthenticationMethod;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabSMTP;
        private System.Windows.Forms.TabPage tabExchange;
        private System.Windows.Forms.TextBox tbDomain;
        private System.Windows.Forms.TextBox tbServerURL;
        private System.Windows.Forms.TextBox tbExPassword;
        private System.Windows.Forms.TextBox tbExUser;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton rbtnSMTP;
        private System.Windows.Forms.RadioButton rbtnExchange;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbEmail;
    }
}