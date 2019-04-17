namespace VIM
{
    partial class AboutForm
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
            this.lblAppName = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnLicense = new System.Windows.Forms.Button();
            this.lblOwner = new System.Windows.Forms.Label();
            this.lblExpire = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAppName
            // 
            this.lblAppName.AutoSize = true;
            this.lblAppName.BackColor = System.Drawing.Color.Transparent;
            this.lblAppName.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppName.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblAppName.Location = new System.Drawing.Point(12, 15);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(364, 38);
            this.lblAppName.TabIndex = 1;
            this.lblAppName.Text = "VIQ Personal Manager";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblVersion.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblVersion.Location = new System.Drawing.Point(12, 282);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(42, 13);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "Version";
            // 
            // btnLicense
            // 
            this.btnLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLicense.BackColor = System.Drawing.Color.Transparent;
            this.btnLicense.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnLicense.FlatAppearance.BorderSize = 0;
            this.btnLicense.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.btnLicense.FlatAppearance.MouseOverBackColor = System.Drawing.Color.RoyalBlue;
            this.btnLicense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLicense.ForeColor = System.Drawing.Color.White;
            this.btnLicense.Location = new System.Drawing.Point(581, 275);
            this.btnLicense.Name = "btnLicense";
            this.btnLicense.Size = new System.Drawing.Size(75, 23);
            this.btnLicense.TabIndex = 3;
            this.btnLicense.Text = "License";
            this.btnLicense.UseVisualStyleBackColor = true;
            this.btnLicense.Visible = false;
            this.btnLicense.Click += new System.EventHandler(this.btnLicense_Click);
            // 
            // lblOwner
            // 
            this.lblOwner.AutoSize = true;
            this.lblOwner.BackColor = System.Drawing.Color.Transparent;
            this.lblOwner.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblOwner.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblOwner.Location = new System.Drawing.Point(12, 69);
            this.lblOwner.Name = "lblOwner";
            this.lblOwner.Size = new System.Drawing.Size(80, 13);
            this.lblOwner.TabIndex = 4;
            this.lblOwner.Text = "License owner :";
            // 
            // lblExpire
            // 
            this.lblExpire.AutoSize = true;
            this.lblExpire.BackColor = System.Drawing.Color.Transparent;
            this.lblExpire.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblExpire.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblExpire.Location = new System.Drawing.Point(12, 92);
            this.lblExpire.Name = "lblExpire";
            this.lblExpire.Size = new System.Drawing.Size(87, 13);
            this.lblExpire.TabIndex = 5;
            this.lblExpire.Text = "License valid till :";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::VIM.Properties.Resources.Fotolia_46795878_Subscription_XXL_660x300_ref;
            this.pictureBox1.Location = new System.Drawing.Point(2, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(660, 300);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(663, 304);
            this.Controls.Add(this.lblExpire);
            this.Controls.Add(this.lblOwner);
            this.Controls.Add(this.btnLicense);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblAppName);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button btnLicense;
        private System.Windows.Forms.Label lblOwner;
        private System.Windows.Forms.Label lblExpire;
    }
}