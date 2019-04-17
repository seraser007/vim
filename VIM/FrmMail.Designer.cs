namespace VIM
{
    partial class FrmMail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMail));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbCopy = new System.Windows.Forms.TextBox();
            this.tbBlindCopy = new System.Windows.Forms.TextBox();
            this.tbSubject = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.lvAttachments = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label6 = new System.Windows.Forms.Label();
            this.btnAttachment = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.tbFrom = new System.Windows.Forms.TextBox();
            this.btnSettings = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbeToAddress = new VIM.TextBoxEx();
            this.chbSelfCopy = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "To:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Copy:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Blind copy:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Subject:";
            // 
            // tbCopy
            // 
            this.tbCopy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCopy.Location = new System.Drawing.Point(72, 72);
            this.tbCopy.Name = "tbCopy";
            this.tbCopy.Size = new System.Drawing.Size(567, 20);
            this.tbCopy.TabIndex = 5;
            // 
            // tbBlindCopy
            // 
            this.tbBlindCopy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBlindCopy.Location = new System.Drawing.Point(72, 94);
            this.tbBlindCopy.Name = "tbBlindCopy";
            this.tbBlindCopy.Size = new System.Drawing.Size(567, 20);
            this.tbBlindCopy.TabIndex = 6;
            // 
            // tbSubject
            // 
            this.tbSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSubject.Location = new System.Drawing.Point(72, 116);
            this.tbSubject.Name = "tbSubject";
            this.tbSubject.Size = new System.Drawing.Size(567, 20);
            this.tbSubject.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Message:";
            // 
            // tbMessage
            // 
            this.tbMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMessage.Location = new System.Drawing.Point(10, 19);
            this.tbMessage.Multiline = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMessage.Size = new System.Drawing.Size(712, 158);
            this.tbMessage.TabIndex = 9;
            // 
            // lvAttachments
            // 
            this.lvAttachments.AllowDrop = true;
            this.lvAttachments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvAttachments.ContextMenuStrip = this.contextMenuStrip1;
            this.lvAttachments.Location = new System.Drawing.Point(10, 16);
            this.lvAttachments.Name = "lvAttachments";
            this.lvAttachments.Size = new System.Drawing.Size(668, 38);
            this.lvAttachments.TabIndex = 10;
            this.lvAttachments.UseCompatibleStateImageBehavior = false;
            this.lvAttachments.View = System.Windows.Forms.View.List;
            this.lvAttachments.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripMenuItem1,
            this.openToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(108, 76);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(104, 6);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Attachments:";
            // 
            // btnAttachment
            // 
            this.btnAttachment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAttachment.Image = ((System.Drawing.Image)(resources.GetObject("btnAttachment.Image")));
            this.btnAttachment.Location = new System.Drawing.Point(681, 13);
            this.btnAttachment.Name = "btnAttachment";
            this.btnAttachment.Size = new System.Drawing.Size(41, 41);
            this.btnAttachment.TabIndex = 12;
            this.toolTip1.SetToolTip(this.btnAttachment, "Add new attachment");
            this.btnAttachment.UseVisualStyleBackColor = true;
            this.btnAttachment.Click += new System.EventHandler(this.btnAttachment_Click);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Image = ((System.Drawing.Image)(resources.GetObject("btnSend.Image")));
            this.btnSend.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSend.Location = new System.Drawing.Point(647, 6);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 130);
            this.btnSend.TabIndex = 13;
            this.btnSend.Text = "Send";
            this.btnSend.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.btnSend, "Send message");
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "From:";
            // 
            // tbFrom
            // 
            this.tbFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFrom.Location = new System.Drawing.Point(72, 6);
            this.tbFrom.Name = "tbFrom";
            this.tbFrom.Size = new System.Drawing.Size(544, 20);
            this.tbFrom.TabIndex = 15;
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettings.Image = global::VIM.Properties.Resources.Settings_4;
            this.btnSettings.Location = new System.Drawing.Point(617, 5);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(23, 23);
            this.btnSettings.TabIndex = 16;
            this.toolTip1.SetToolTip(this.btnSettings, "SMTP Settings");
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbeToAddress);
            this.panel1.Controls.Add(this.chbSelfCopy);
            this.panel1.Controls.Add(this.tbFrom);
            this.panel1.Controls.Add(this.btnSettings);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnSend);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.tbCopy);
            this.panel1.Controls.Add(this.tbBlindCopy);
            this.panel1.Controls.Add(this.tbSubject);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(737, 145);
            this.panel1.TabIndex = 17;
            // 
            // tbeToAddress
            // 
            this.tbeToAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbeToAddress.AutoCompleteBoxAutoSize = true;
            this.tbeToAddress.AutoCompleteBoxMinHeight = 20;
            this.tbeToAddress.AutoCompleteBoxMinWidth = 50;
            this.tbeToAddress.AutoCompleteBoxSize = new System.Drawing.Size(600, 100);
            this.tbeToAddress.AutoCompleteFormBorder = System.Windows.Forms.FormBorderStyle.None;
            this.tbeToAddress.AutoCompleteList = null;
            this.tbeToAddress.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbeToAddress.Location = new System.Drawing.Point(72, 49);
            this.tbeToAddress.Name = "tbeToAddress";
            this.tbeToAddress.OnEnterSelect = false;
            this.tbeToAddress.SelectionMethods = VIM.TextBoxEx.SelectOptions.OnEnterDoubleClick;
            this.tbeToAddress.SelectTextAfterItemSelect = true;
            this.tbeToAddress.ShowAutoCompleteOnFocus = false;
            this.tbeToAddress.Size = new System.Drawing.Size(567, 20);
            this.tbeToAddress.TabIndex = 4;
            this.tbeToAddress.BeforeDisplayingAutoComplete += new System.EventHandler<VIM.TextBoxEx.TextBoxExAutoCompleteEventArgs>(this.tbeToAddress_BeforeDisplayingAutoComplete);
            this.tbeToAddress.Resize += new System.EventHandler(this.tbeToAddress_Resize);
            // 
            // chbSelfCopy
            // 
            this.chbSelfCopy.AutoSize = true;
            this.chbSelfCopy.Location = new System.Drawing.Point(72, 30);
            this.chbSelfCopy.Name = "chbSelfCopy";
            this.chbSelfCopy.Size = new System.Drawing.Size(174, 17);
            this.chbSelfCopy.TabIndex = 17;
            this.chbSelfCopy.Text = "Send a copy to \"From\" address";
            this.chbSelfCopy.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 145);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tbMessage);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvAttachments);
            this.splitContainer1.Panel2.Controls.Add(this.btnAttachment);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Size = new System.Drawing.Size(737, 245);
            this.splitContainer1.SplitterDistance = 180;
            this.splitContainer1.TabIndex = 18;
            // 
            // FrmMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 390);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "FrmMail";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Message editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMail_FormClosing);
            this.Load += new System.EventHandler(this.FrmMail_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbCopy;
        private System.Windows.Forms.TextBox tbBlindCopy;
        private System.Windows.Forms.TextBox tbSubject;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.ListView lvAttachments;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnAttachment;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbFrom;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chbSelfCopy;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private TextBoxEx tbeToAddress;
    }
}