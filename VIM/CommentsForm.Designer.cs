namespace VIM
{
    partial class CommentsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommentsForm));
            this.tbItem = new System.Windows.Forms.TextBox();
            this.tbComments = new System.Windows.Forms.TextBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbItem
            // 
            this.tbItem.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.tbItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbItem.Location = new System.Drawing.Point(0, 0);
            this.tbItem.Multiline = true;
            this.tbItem.Name = "tbItem";
            this.tbItem.ReadOnly = true;
            this.tbItem.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbItem.Size = new System.Drawing.Size(770, 141);
            this.tbItem.TabIndex = 1;
            // 
            // tbComments
            // 
            this.tbComments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbComments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbComments.Location = new System.Drawing.Point(0, 0);
            this.tbComments.Multiline = true;
            this.tbComments.Name = "tbComments";
            this.tbComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbComments.Size = new System.Drawing.Size(770, 270);
            this.tbComments.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "Ok-2.bmp");
            this.imageList1.Images.SetKeyName(1, "delete.bmp");
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.ImageIndex = 0;
            this.button2.ImageList = this.imageList1;
            this.button2.Location = new System.Drawing.Point(611, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 25);
            this.button2.TabIndex = 3;
            this.button2.Text = "OK";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.ImageIndex = 1;
            this.button1.ImageList = this.imageList1;
            this.button1.Location = new System.Drawing.Point(692, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 2;
            this.button1.Text = "Cancel";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 415);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(770, 31);
            this.panel1.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tbItem);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbComments);
            this.splitContainer1.Size = new System.Drawing.Size(770, 415);
            this.splitContainer1.SplitterDistance = 141;
            this.splitContainer1.TabIndex = 5;
            // 
            // CommentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 446);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "CommentsForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unified comments";
            this.Shown += new System.EventHandler(this.CommentsForm_Shown);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TextBox tbItem;
        public System.Windows.Forms.TextBox tbComments;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}