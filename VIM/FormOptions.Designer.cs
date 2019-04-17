namespace VIM
{
    partial class FormOptions
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
            this.label2 = new System.Windows.Forms.Label();
            this.chbAnyUser = new System.Windows.Forms.CheckBox();
            this.lblFontName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFontSize = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblBold = new System.Windows.Forms.Label();
            this.lblItalic = new System.Windows.Forms.Label();
            this.lblUnderline = new System.Windows.Forms.Label();
            this.btnFont = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.label6 = new System.Windows.Forms.Label();
            this.lblStrikeOut = new System.Windows.Forms.Label();
            this.chbSameFont = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Font name:";
            // 
            // chbAnyUser
            // 
            this.chbAnyUser.AutoSize = true;
            this.chbAnyUser.Location = new System.Drawing.Point(15, 199);
            this.chbAnyUser.Name = "chbAnyUser";
            this.chbAnyUser.Size = new System.Drawing.Size(149, 17);
            this.chbAnyUser.TabIndex = 4;
            this.chbAnyUser.Text = "Any user may change font";
            this.chbAnyUser.UseVisualStyleBackColor = true;
            // 
            // lblFontName
            // 
            this.lblFontName.AutoSize = true;
            this.lblFontName.Location = new System.Drawing.Point(78, 9);
            this.lblFontName.Name = "lblFontName";
            this.lblFontName.Size = new System.Drawing.Size(66, 13);
            this.lblFontName.TabIndex = 5;
            this.lblFontName.Text = "lblFontName";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Font size:";
            // 
            // lblFontSize
            // 
            this.lblFontSize.AutoSize = true;
            this.lblFontSize.Location = new System.Drawing.Point(78, 33);
            this.lblFontSize.Name = "lblFontSize";
            this.lblFontSize.Size = new System.Drawing.Size(58, 13);
            this.lblFontSize.TabIndex = 7;
            this.lblFontSize.Text = "lblFontSize";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Bold:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Italic:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Underline:";
            // 
            // lblBold
            // 
            this.lblBold.AutoSize = true;
            this.lblBold.Location = new System.Drawing.Point(78, 57);
            this.lblBold.Name = "lblBold";
            this.lblBold.Size = new System.Drawing.Size(38, 13);
            this.lblBold.TabIndex = 11;
            this.lblBold.Text = "lblBold";
            // 
            // lblItalic
            // 
            this.lblItalic.AutoSize = true;
            this.lblItalic.Location = new System.Drawing.Point(78, 81);
            this.lblItalic.Name = "lblItalic";
            this.lblItalic.Size = new System.Drawing.Size(39, 13);
            this.lblItalic.TabIndex = 12;
            this.lblItalic.Text = "lblItalic";
            // 
            // lblUnderline
            // 
            this.lblUnderline.AutoSize = true;
            this.lblUnderline.Location = new System.Drawing.Point(78, 107);
            this.lblUnderline.Name = "lblUnderline";
            this.lblUnderline.Size = new System.Drawing.Size(62, 13);
            this.lblUnderline.TabIndex = 13;
            this.lblUnderline.Text = "lblUnderline";
            // 
            // btnFont
            // 
            this.btnFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFont.Location = new System.Drawing.Point(15, 158);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(226, 25);
            this.btnFont.TabIndex = 14;
            this.btnFont.Text = "Change font";
            this.btnFont.UseVisualStyleBackColor = true;
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::VIM.Properties.Resources.Close_Box_Red;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.Location = new System.Drawing.Point(166, 260);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 15;
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
            this.btnOk.Location = new System.Drawing.Point(85, 260);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 25);
            this.btnOk.TabIndex = 16;
            this.btnOk.Text = "OK";
            this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Strikeout:";
            // 
            // lblStrikeOut
            // 
            this.lblStrikeOut.AutoSize = true;
            this.lblStrikeOut.Location = new System.Drawing.Point(78, 132);
            this.lblStrikeOut.Name = "lblStrikeOut";
            this.lblStrikeOut.Size = new System.Drawing.Size(61, 13);
            this.lblStrikeOut.TabIndex = 18;
            this.lblStrikeOut.Text = "lblStrikeOut";
            // 
            // chbSameFont
            // 
            this.chbSameFont.AutoSize = true;
            this.chbSameFont.Checked = true;
            this.chbSameFont.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbSameFont.Location = new System.Drawing.Point(15, 222);
            this.chbSameFont.Name = "chbSameFont";
            this.chbSameFont.Size = new System.Drawing.Size(191, 17);
            this.chbSameFont.TabIndex = 19;
            this.chbSameFont.Text = "All power users have the same font";
            this.chbSameFont.UseVisualStyleBackColor = true;
            // 
            // FormOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 297);
            this.Controls.Add(this.chbSameFont);
            this.Controls.Add(this.lblStrikeOut);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFont);
            this.Controls.Add(this.lblUnderline);
            this.Controls.Add(this.lblItalic);
            this.Controls.Add(this.lblBold);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblFontSize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFontName);
            this.Controls.Add(this.chbAnyUser);
            this.Controls.Add(this.label2);
            this.Name = "FormOptions";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Default font";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chbAnyUser;
        private System.Windows.Forms.Label lblFontName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFontSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblBold;
        private System.Windows.Forms.Label lblItalic;
        private System.Windows.Forms.Label lblUnderline;
        private System.Windows.Forms.Button btnFont;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblStrikeOut;
        private System.Windows.Forms.CheckBox chbSameFont;
    }
}