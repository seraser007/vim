namespace VIM
{
    partial class FormObsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormObsView));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rtbObservation = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tcbFontsObs = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tcbSizeObs = new System.Windows.Forms.ToolStripComboBox();
            this.tbtnColorObs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtnBoldObs = new System.Windows.Forms.ToolStripButton();
            this.tbtnItalicObs = new System.Windows.Forms.ToolStripButton();
            this.tbtnUnderObs = new System.Windows.Forms.ToolStripButton();
            this.tbtnStrikeoutObs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtnSaveObs = new System.Windows.Forms.ToolStripButton();
            this.rtbComments = new System.Windows.Forms.RichTextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tcbFontsComm = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.tcbSizeComm = new System.Windows.Forms.ToolStripComboBox();
            this.tbtnColorComm = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtnBoldComm = new System.Windows.Forms.ToolStripButton();
            this.tbtnItalicComm = new System.Windows.Forms.ToolStripButton();
            this.tbtnUnderComm = new System.Windows.Forms.ToolStripButton();
            this.tbtnStrikeoutComm = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtnSaveComm = new System.Windows.Forms.ToolStripButton();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tbQuestion = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 436);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(773, 33);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Image = global::VIM.Properties.Resources.Close_Box_Red;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.Location = new System.Drawing.Point(691, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 0;
            this.button1.Text = "Close";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 84);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.rtbObservation);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rtbComments);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainer1.Size = new System.Drawing.Size(773, 352);
            this.splitContainer1.SplitterDistance = 172;
            this.splitContainer1.TabIndex = 1;
            // 
            // rtbObservation
            // 
            this.rtbObservation.BackColor = System.Drawing.SystemColors.Window;
            this.rtbObservation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbObservation.Location = new System.Drawing.Point(0, 25);
            this.rtbObservation.Name = "rtbObservation";
            this.rtbObservation.ReadOnly = true;
            this.rtbObservation.Size = new System.Drawing.Size(773, 147);
            this.rtbObservation.TabIndex = 1;
            this.rtbObservation.Text = "";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tcbFontsObs,
            this.toolStripLabel2,
            this.tcbSizeObs,
            this.tbtnColorObs,
            this.toolStripSeparator1,
            this.tbtnBoldObs,
            this.tbtnItalicObs,
            this.tbtnUnderObs,
            this.tbtnStrikeoutObs,
            this.toolStripSeparator3,
            this.tbtnSaveObs});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(773, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(31, 22);
            this.toolStripLabel1.Text = "Font";
            // 
            // tcbFontsObs
            // 
            this.tcbFontsObs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tcbFontsObs.Name = "tcbFontsObs";
            this.tcbFontsObs.Size = new System.Drawing.Size(121, 25);
            this.tcbFontsObs.SelectedIndexChanged += new System.EventHandler(this.tcbFontsObs_SelectedIndexChanged);
            this.tcbFontsObs.TextChanged += new System.EventHandler(this.tcbFontsObs_TextChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(27, 22);
            this.toolStripLabel2.Text = "Size";
            // 
            // tcbSizeObs
            // 
            this.tcbSizeObs.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "26",
            "28",
            "32",
            "36",
            "40",
            "44",
            "48",
            "56",
            "64",
            "72"});
            this.tcbSizeObs.Name = "tcbSizeObs";
            this.tcbSizeObs.Size = new System.Drawing.Size(75, 25);
            this.tcbSizeObs.TextChanged += new System.EventHandler(this.tcbSizeObs_TextChanged);
            // 
            // tbtnColorObs
            // 
            this.tbtnColorObs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tbtnColorObs.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbtnColorObs.Image = ((System.Drawing.Image)(resources.GetObject("tbtnColorObs.Image")));
            this.tbtnColorObs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnColorObs.Name = "tbtnColorObs";
            this.tbtnColorObs.Size = new System.Drawing.Size(45, 22);
            this.tbtnColorObs.Text = "Color";
            this.tbtnColorObs.ToolTipText = "Font color";
            this.tbtnColorObs.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tbtnBoldObs
            // 
            this.tbtnBoldObs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnBoldObs.Image = global::VIM.Properties.Resources.Bold;
            this.tbtnBoldObs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnBoldObs.Name = "tbtnBoldObs";
            this.tbtnBoldObs.Size = new System.Drawing.Size(23, 22);
            this.tbtnBoldObs.Text = "Bold";
            this.tbtnBoldObs.Click += new System.EventHandler(this.tbtnBoldObs_Click);
            // 
            // tbtnItalicObs
            // 
            this.tbtnItalicObs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnItalicObs.Image = global::VIM.Properties.Resources.Italic;
            this.tbtnItalicObs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnItalicObs.Name = "tbtnItalicObs";
            this.tbtnItalicObs.Size = new System.Drawing.Size(23, 22);
            this.tbtnItalicObs.Text = "Italic";
            this.tbtnItalicObs.Click += new System.EventHandler(this.tbtnItalicObs_Click);
            // 
            // tbtnUnderObs
            // 
            this.tbtnUnderObs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnUnderObs.Image = global::VIM.Properties.Resources.Underline;
            this.tbtnUnderObs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnUnderObs.Name = "tbtnUnderObs";
            this.tbtnUnderObs.Size = new System.Drawing.Size(23, 22);
            this.tbtnUnderObs.Text = "Underline";
            this.tbtnUnderObs.Click += new System.EventHandler(this.tbtnUnderObs_Click);
            // 
            // tbtnStrikeoutObs
            // 
            this.tbtnStrikeoutObs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnStrikeoutObs.Image = global::VIM.Properties.Resources.strikeout;
            this.tbtnStrikeoutObs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnStrikeoutObs.Name = "tbtnStrikeoutObs";
            this.tbtnStrikeoutObs.Size = new System.Drawing.Size(23, 22);
            this.tbtnStrikeoutObs.Text = "Strikeout";
            this.tbtnStrikeoutObs.Click += new System.EventHandler(this.tbtnStrikeoutObs_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tbtnSaveObs
            // 
            this.tbtnSaveObs.Image = global::VIM.Properties.Resources.save;
            this.tbtnSaveObs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnSaveObs.Name = "tbtnSaveObs";
            this.tbtnSaveObs.Size = new System.Drawing.Size(51, 22);
            this.tbtnSaveObs.Text = "Save";
            this.tbtnSaveObs.ToolTipText = "Save observation font";
            this.tbtnSaveObs.Click += new System.EventHandler(this.tbtnSaveObs_Click);
            // 
            // rtbComments
            // 
            this.rtbComments.BackColor = System.Drawing.SystemColors.Window;
            this.rtbComments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbComments.Location = new System.Drawing.Point(0, 25);
            this.rtbComments.Name = "rtbComments";
            this.rtbComments.ReadOnly = true;
            this.rtbComments.Size = new System.Drawing.Size(773, 151);
            this.rtbComments.TabIndex = 1;
            this.rtbComments.Text = "";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.tcbFontsComm,
            this.toolStripLabel4,
            this.tcbSizeComm,
            this.tbtnColorComm,
            this.toolStripSeparator2,
            this.tbtnBoldComm,
            this.tbtnItalicComm,
            this.tbtnUnderComm,
            this.tbtnStrikeoutComm,
            this.toolStripSeparator4,
            this.tbtnSaveComm});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(773, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(31, 22);
            this.toolStripLabel3.Text = "Font";
            // 
            // tcbFontsComm
            // 
            this.tcbFontsComm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tcbFontsComm.Name = "tcbFontsComm";
            this.tcbFontsComm.Size = new System.Drawing.Size(121, 25);
            this.tcbFontsComm.SelectedIndexChanged += new System.EventHandler(this.tcbFontsComm_SelectedIndexChanged);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(27, 22);
            this.toolStripLabel4.Text = "Size";
            // 
            // tcbSizeComm
            // 
            this.tcbSizeComm.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "26",
            "28",
            "32",
            "36",
            "40",
            "44",
            "48",
            "56",
            "64",
            "72"});
            this.tcbSizeComm.Name = "tcbSizeComm";
            this.tcbSizeComm.Size = new System.Drawing.Size(75, 25);
            this.tcbSizeComm.TextChanged += new System.EventHandler(this.tcbSizeComm_TextChanged);
            // 
            // tbtnColorComm
            // 
            this.tbtnColorComm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tbtnColorComm.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbtnColorComm.Image = ((System.Drawing.Image)(resources.GetObject("tbtnColorComm.Image")));
            this.tbtnColorComm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnColorComm.Name = "tbtnColorComm";
            this.tbtnColorComm.Size = new System.Drawing.Size(45, 22);
            this.tbtnColorComm.Text = "Color";
            this.tbtnColorComm.Click += new System.EventHandler(this.tbtnColorComm_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tbtnBoldComm
            // 
            this.tbtnBoldComm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnBoldComm.Image = global::VIM.Properties.Resources.Bold;
            this.tbtnBoldComm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnBoldComm.Name = "tbtnBoldComm";
            this.tbtnBoldComm.Size = new System.Drawing.Size(23, 22);
            this.tbtnBoldComm.Text = "toolStripButton2";
            this.tbtnBoldComm.Click += new System.EventHandler(this.tbtnBoldComm_Click);
            // 
            // tbtnItalicComm
            // 
            this.tbtnItalicComm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnItalicComm.Image = global::VIM.Properties.Resources.Italic;
            this.tbtnItalicComm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnItalicComm.Name = "tbtnItalicComm";
            this.tbtnItalicComm.Size = new System.Drawing.Size(23, 22);
            this.tbtnItalicComm.Text = "toolStripButton3";
            this.tbtnItalicComm.Click += new System.EventHandler(this.tbtnItalicComm_Click);
            // 
            // tbtnUnderComm
            // 
            this.tbtnUnderComm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnUnderComm.Image = global::VIM.Properties.Resources.Underline;
            this.tbtnUnderComm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnUnderComm.Name = "tbtnUnderComm";
            this.tbtnUnderComm.Size = new System.Drawing.Size(23, 22);
            this.tbtnUnderComm.Text = "toolStripButton4";
            this.tbtnUnderComm.Click += new System.EventHandler(this.tbtnUnderComm_Click);
            // 
            // tbtnStrikeoutComm
            // 
            this.tbtnStrikeoutComm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnStrikeoutComm.Image = global::VIM.Properties.Resources.strikeout;
            this.tbtnStrikeoutComm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnStrikeoutComm.Name = "tbtnStrikeoutComm";
            this.tbtnStrikeoutComm.Size = new System.Drawing.Size(23, 22);
            this.tbtnStrikeoutComm.Text = "Strikeout";
            this.tbtnStrikeoutComm.Click += new System.EventHandler(this.tbtnStrikeoutComm_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tbtnSaveComm
            // 
            this.tbtnSaveComm.Image = global::VIM.Properties.Resources.save;
            this.tbtnSaveComm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnSaveComm.Name = "tbtnSaveComm";
            this.tbtnSaveComm.Size = new System.Drawing.Size(51, 22);
            this.tbtnSaveComm.Text = "Save";
            this.tbtnSaveComm.ToolTipText = "Save comments font";
            this.tbtnSaveComm.Click += new System.EventHandler(this.tbtnSaveComm_Click);
            // 
            // fontDialog1
            // 
            this.fontDialog1.ShowApply = true;
            this.fontDialog1.ShowColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbQuestion);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(773, 81);
            this.panel2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Questtion";
            // 
            // tbQuestion
            // 
            this.tbQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbQuestion.BackColor = System.Drawing.SystemColors.Window;
            this.tbQuestion.Location = new System.Drawing.Point(6, 25);
            this.tbQuestion.Multiline = true;
            this.tbQuestion.Name = "tbQuestion";
            this.tbQuestion.ReadOnly = true;
            this.tbQuestion.Size = new System.Drawing.Size(760, 53);
            this.tbQuestion.TabIndex = 1;
            // 
            // FormObsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 469);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "FormObsView";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Observetion viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormObsView_FormClosing);
            this.Load += new System.EventHandler(this.FormObsView_Load);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox rtbObservation;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tcbFontsObs;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox tcbSizeObs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tbtnColorObs;
        private System.Windows.Forms.ToolStripButton tbtnBoldObs;
        private System.Windows.Forms.ToolStripButton tbtnItalicObs;
        private System.Windows.Forms.RichTextBox rtbComments;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripButton tbtnUnderObs;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripComboBox tcbFontsComm;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripComboBox tcbSizeComm;
        private System.Windows.Forms.ToolStripButton tbtnColorComm;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tbtnBoldComm;
        private System.Windows.Forms.ToolStripButton tbtnItalicComm;
        private System.Windows.Forms.ToolStripButton tbtnUnderComm;
        private System.Windows.Forms.ToolStripButton tbtnStrikeoutObs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tbtnSaveObs;
        private System.Windows.Forms.ToolStripButton tbtnStrikeoutComm;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tbtnSaveComm;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tbQuestion;
        private System.Windows.Forms.Label label1;
    }
}