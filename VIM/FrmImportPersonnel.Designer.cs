namespace VIM
{
    partial class FrmImportPersonnel
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvInspections = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnEditChEng = new System.Windows.Forms.Button();
            this.btnAddChEng = new System.Windows.Forms.Button();
            this.tbChEng = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pbChEng = new System.Windows.Forms.PictureBox();
            this.btnCopyChEng = new System.Windows.Forms.Button();
            this.cbChEngName = new ComboBoxExt.ComboBoxExt();
            this.tbChEngName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnEditMaster = new System.Windows.Forms.Button();
            this.btnAddMaster = new System.Windows.Forms.Button();
            this.tbMaster = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pbMaster = new System.Windows.Forms.PictureBox();
            this.btnCopyMaster = new System.Windows.Forms.Button();
            this.cbMasterName = new ComboBoxExt.ComboBoxExt();
            this.tbMasterName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnEditInspector = new System.Windows.Forms.Button();
            this.btnAddInspector = new System.Windows.Forms.Button();
            this.tbInspector = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pbInspector = new System.Windows.Forms.PictureBox();
            this.btnCopyInspector = new System.Windows.Forms.Button();
            this.cbInspectorName = new ComboBoxExt.ComboBoxExt();
            this.tbInspectorName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chbInspector = new System.Windows.Forms.CheckBox();
            this.chbMaster = new System.Windows.Forms.CheckBox();
            this.chbChEng = new System.Windows.Forms.CheckBox();
            this.pbInspectorSaved = new System.Windows.Forms.PictureBox();
            this.pbMasterSaved = new System.Windows.Forms.PictureBox();
            this.pbChengSaved = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInspections)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbChEng)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMaster)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbInspector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInspectorSaved)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMasterSaved)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbChengSaved)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 506);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(801, 33);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Image = global::VIM.Properties.Resources.Close_Box_Red;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.Location = new System.Drawing.Point(714, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 0;
            this.button1.Text = "Close";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvInspections);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(801, 506);
            this.splitContainer1.SplitterDistance = 267;
            this.splitContainer1.TabIndex = 1;
            // 
            // dgvInspections
            // 
            this.dgvInspections.AllowUserToAddRows = false;
            this.dgvInspections.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvInspections.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvInspections.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvInspections.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInspections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInspections.Location = new System.Drawing.Point(0, 0);
            this.dgvInspections.Name = "dgvInspections";
            this.dgvInspections.ReadOnly = true;
            this.dgvInspections.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInspections.Size = new System.Drawing.Size(267, 506);
            this.dgvInspections.TabIndex = 0;
            this.dgvInspections.SelectionChanged += new System.EventHandler(this.dgvInspections_SelectionChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btnPrevious);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.btnNext);
            this.panel2.Location = new System.Drawing.Point(14, 444);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(504, 32);
            this.panel2.TabIndex = 3;
            // 
            // btnPrevious
            // 
            this.btnPrevious.Image = global::VIM.Properties.Resources.Move_Left_2;
            this.btnPrevious.Location = new System.Drawing.Point(8, 4);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(25, 25);
            this.btnPrevious.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnPrevious, "Previous record (Ctrl+Left)");
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::VIM.Properties.Resources.save;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.Location = new System.Drawing.Point(33, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnSave, "Save report record (Ctrl+S)");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnNext
            // 
            this.btnNext.Image = global::VIM.Properties.Resources.Move_Right_2;
            this.btnNext.Location = new System.Drawing.Point(108, 4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(25, 25);
            this.btnNext.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnNext, "Next record (Ctrl+Right)");
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.pbChengSaved);
            this.groupBox3.Controls.Add(this.chbChEng);
            this.groupBox3.Controls.Add(this.btnEditChEng);
            this.groupBox3.Controls.Add(this.btnAddChEng);
            this.groupBox3.Controls.Add(this.tbChEng);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.pbChEng);
            this.groupBox3.Controls.Add(this.btnCopyChEng);
            this.groupBox3.Controls.Add(this.cbChEngName);
            this.groupBox3.Controls.Add(this.tbChEngName);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(13, 302);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(505, 140);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Chief Engineer";
            // 
            // btnEditChEng
            // 
            this.btnEditChEng.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditChEng.Image = global::VIM.Properties.Resources.edit;
            this.btnEditChEng.Location = new System.Drawing.Point(476, 72);
            this.btnEditChEng.Name = "btnEditChEng";
            this.btnEditChEng.Size = new System.Drawing.Size(23, 22);
            this.btnEditChEng.TabIndex = 9;
            this.toolTip1.SetToolTip(this.btnEditChEng, "Edit selected chief engineer");
            this.btnEditChEng.UseVisualStyleBackColor = true;
            this.btnEditChEng.Click += new System.EventHandler(this.btnEditChEng_Click);
            // 
            // btnAddChEng
            // 
            this.btnAddChEng.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddChEng.Image = global::VIM.Properties.Resources.db_insert;
            this.btnAddChEng.Location = new System.Drawing.Point(454, 72);
            this.btnAddChEng.Name = "btnAddChEng";
            this.btnAddChEng.Size = new System.Drawing.Size(23, 22);
            this.btnAddChEng.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btnAddChEng, "Create new chief engineer record");
            this.btnAddChEng.UseVisualStyleBackColor = true;
            this.btnAddChEng.Click += new System.EventHandler(this.btnAddChEng_Click);
            // 
            // tbChEng
            // 
            this.tbChEng.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbChEng.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbChEng.Location = new System.Drawing.Point(9, 114);
            this.tbChEng.Name = "tbChEng";
            this.tbChEng.ReadOnly = true;
            this.tbChEng.Size = new System.Drawing.Size(468, 20);
            this.tbChEng.TabIndex = 7;
            this.tbChEng.TextChanged += new System.EventHandler(this.tbChEng_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 97);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Value in database";
            // 
            // pbChEng
            // 
            this.pbChEng.Location = new System.Drawing.Point(87, 56);
            this.pbChEng.Name = "pbChEng";
            this.pbChEng.Size = new System.Drawing.Size(16, 16);
            this.pbChEng.TabIndex = 5;
            this.pbChEng.TabStop = false;
            // 
            // btnCopyChEng
            // 
            this.btnCopyChEng.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyChEng.Image = global::VIM.Properties.Resources.Next_Down_Set_2;
            this.btnCopyChEng.Location = new System.Drawing.Point(476, 30);
            this.btnCopyChEng.Name = "btnCopyChEng";
            this.btnCopyChEng.Size = new System.Drawing.Size(23, 22);
            this.btnCopyChEng.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnCopyChEng, "Copy chief engineer name to value validation field");
            this.btnCopyChEng.UseVisualStyleBackColor = true;
            this.btnCopyChEng.Click += new System.EventHandler(this.btnCopyChEng_Click);
            // 
            // cbChEngName
            // 
            this.cbChEngName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbChEngName.FormattingEnabled = true;
            this.cbChEngName.Location = new System.Drawing.Point(9, 73);
            this.cbChEngName.Name = "cbChEngName";
            this.cbChEngName.SearchRule = ComboBoxExt.ComboBoxExt.searchRules.cbsContains;
            this.cbChEngName.Size = new System.Drawing.Size(447, 21);
            this.cbChEngName.SuggestBorderStyle = ComboBoxExt.ComboBoxExt.suggestBorders.sbsNone;
            this.cbChEngName.SuggestBoxHeight = 106;
            this.cbChEngName.TabIndex = 3;
            this.cbChEngName.TextChanged += new System.EventHandler(this.cbChEngName_TextChanged);
            // 
            // tbChEngName
            // 
            this.tbChEngName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbChEngName.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbChEngName.Location = new System.Drawing.Point(9, 32);
            this.tbChEngName.Name = "tbChEngName";
            this.tbChEngName.ReadOnly = true;
            this.tbChEngName.Size = new System.Drawing.Size(467, 20);
            this.tbChEngName.TabIndex = 2;
            this.tbChEngName.TextChanged += new System.EventHandler(this.tbChEngName_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Value validation";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Value from file";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.pbMasterSaved);
            this.groupBox2.Controls.Add(this.chbMaster);
            this.groupBox2.Controls.Add(this.btnEditMaster);
            this.groupBox2.Controls.Add(this.btnAddMaster);
            this.groupBox2.Controls.Add(this.tbMaster);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.pbMaster);
            this.groupBox2.Controls.Add(this.btnCopyMaster);
            this.groupBox2.Controls.Add(this.cbMasterName);
            this.groupBox2.Controls.Add(this.tbMasterName);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(13, 157);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(505, 139);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Master";
            // 
            // btnEditMaster
            // 
            this.btnEditMaster.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditMaster.Image = global::VIM.Properties.Resources.edit;
            this.btnEditMaster.Location = new System.Drawing.Point(476, 72);
            this.btnEditMaster.Name = "btnEditMaster";
            this.btnEditMaster.Size = new System.Drawing.Size(23, 22);
            this.btnEditMaster.TabIndex = 9;
            this.toolTip1.SetToolTip(this.btnEditMaster, "Edit selected master");
            this.btnEditMaster.UseVisualStyleBackColor = true;
            this.btnEditMaster.Click += new System.EventHandler(this.btnEditMaster_Click);
            // 
            // btnAddMaster
            // 
            this.btnAddMaster.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddMaster.Image = global::VIM.Properties.Resources.db_insert;
            this.btnAddMaster.Location = new System.Drawing.Point(454, 72);
            this.btnAddMaster.Name = "btnAddMaster";
            this.btnAddMaster.Size = new System.Drawing.Size(23, 22);
            this.btnAddMaster.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btnAddMaster, "Crewate new master record");
            this.btnAddMaster.UseVisualStyleBackColor = true;
            this.btnAddMaster.Click += new System.EventHandler(this.btnAddMaster_Click);
            // 
            // tbMaster
            // 
            this.tbMaster.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMaster.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbMaster.Location = new System.Drawing.Point(9, 113);
            this.tbMaster.Name = "tbMaster";
            this.tbMaster.ReadOnly = true;
            this.tbMaster.Size = new System.Drawing.Size(468, 20);
            this.tbMaster.TabIndex = 7;
            this.tbMaster.TextChanged += new System.EventHandler(this.tbMaster_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Value in database";
            // 
            // pbMaster
            // 
            this.pbMaster.Location = new System.Drawing.Point(87, 55);
            this.pbMaster.Name = "pbMaster";
            this.pbMaster.Size = new System.Drawing.Size(16, 16);
            this.pbMaster.TabIndex = 5;
            this.pbMaster.TabStop = false;
            // 
            // btnCopyMaster
            // 
            this.btnCopyMaster.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyMaster.Image = global::VIM.Properties.Resources.Next_Down_Set_2;
            this.btnCopyMaster.Location = new System.Drawing.Point(476, 32);
            this.btnCopyMaster.Name = "btnCopyMaster";
            this.btnCopyMaster.Size = new System.Drawing.Size(23, 22);
            this.btnCopyMaster.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnCopyMaster, "Copy master\'s name to value validaion field");
            this.btnCopyMaster.UseVisualStyleBackColor = true;
            this.btnCopyMaster.Click += new System.EventHandler(this.btnCopyMaster_Click);
            // 
            // cbMasterName
            // 
            this.cbMasterName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMasterName.FormattingEnabled = true;
            this.cbMasterName.Location = new System.Drawing.Point(9, 73);
            this.cbMasterName.Name = "cbMasterName";
            this.cbMasterName.SearchRule = ComboBoxExt.ComboBoxExt.searchRules.cbsContains;
            this.cbMasterName.Size = new System.Drawing.Size(447, 21);
            this.cbMasterName.SuggestBorderStyle = ComboBoxExt.ComboBoxExt.suggestBorders.sbsNone;
            this.cbMasterName.SuggestBoxHeight = 106;
            this.cbMasterName.TabIndex = 3;
            this.cbMasterName.TextChanged += new System.EventHandler(this.cbMasterName_TextChanged);
            // 
            // tbMasterName
            // 
            this.tbMasterName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMasterName.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbMasterName.Location = new System.Drawing.Point(9, 32);
            this.tbMasterName.Name = "tbMasterName";
            this.tbMasterName.ReadOnly = true;
            this.tbMasterName.Size = new System.Drawing.Size(467, 20);
            this.tbMasterName.TabIndex = 2;
            this.tbMasterName.TextChanged += new System.EventHandler(this.tbMasterName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Value validation";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Value from file";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.pbInspectorSaved);
            this.groupBox1.Controls.Add(this.chbInspector);
            this.groupBox1.Controls.Add(this.btnEditInspector);
            this.groupBox1.Controls.Add(this.btnAddInspector);
            this.groupBox1.Controls.Add(this.tbInspector);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.pbInspector);
            this.groupBox1.Controls.Add(this.btnCopyInspector);
            this.groupBox1.Controls.Add(this.cbInspectorName);
            this.groupBox1.Controls.Add(this.tbInspectorName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(505, 139);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Inspector";
            // 
            // btnEditInspector
            // 
            this.btnEditInspector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditInspector.Image = global::VIM.Properties.Resources.edit;
            this.btnEditInspector.Location = new System.Drawing.Point(476, 70);
            this.btnEditInspector.Name = "btnEditInspector";
            this.btnEditInspector.Size = new System.Drawing.Size(23, 22);
            this.btnEditInspector.TabIndex = 9;
            this.toolTip1.SetToolTip(this.btnEditInspector, "Edit selected inspector");
            this.btnEditInspector.UseVisualStyleBackColor = true;
            this.btnEditInspector.Click += new System.EventHandler(this.btnEditInspector_Click);
            // 
            // btnAddInspector
            // 
            this.btnAddInspector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddInspector.Image = global::VIM.Properties.Resources.db_insert;
            this.btnAddInspector.Location = new System.Drawing.Point(454, 70);
            this.btnAddInspector.Name = "btnAddInspector";
            this.btnAddInspector.Size = new System.Drawing.Size(23, 22);
            this.btnAddInspector.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btnAddInspector, "Create new inspector record");
            this.btnAddInspector.UseVisualStyleBackColor = true;
            this.btnAddInspector.Click += new System.EventHandler(this.btnAddInspector_Click);
            // 
            // tbInspector
            // 
            this.tbInspector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInspector.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbInspector.Location = new System.Drawing.Point(9, 111);
            this.tbInspector.Name = "tbInspector";
            this.tbInspector.ReadOnly = true;
            this.tbInspector.Size = new System.Drawing.Size(467, 20);
            this.tbInspector.TabIndex = 7;
            this.tbInspector.TextChanged += new System.EventHandler(this.tbInspector_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Value in database";
            // 
            // pbInspector
            // 
            this.pbInspector.Location = new System.Drawing.Point(87, 53);
            this.pbInspector.Name = "pbInspector";
            this.pbInspector.Size = new System.Drawing.Size(16, 16);
            this.pbInspector.TabIndex = 5;
            this.pbInspector.TabStop = false;
            // 
            // btnCopyInspector
            // 
            this.btnCopyInspector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyInspector.Image = global::VIM.Properties.Resources.Next_Down_Set_2;
            this.btnCopyInspector.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnCopyInspector.Location = new System.Drawing.Point(476, 30);
            this.btnCopyInspector.Name = "btnCopyInspector";
            this.btnCopyInspector.Size = new System.Drawing.Size(23, 22);
            this.btnCopyInspector.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnCopyInspector, "Copy inspector\'s name to value validation field\r\n");
            this.btnCopyInspector.UseVisualStyleBackColor = true;
            this.btnCopyInspector.Click += new System.EventHandler(this.btnCopyInspector_Click);
            // 
            // cbInspectorName
            // 
            this.cbInspectorName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbInspectorName.FormattingEnabled = true;
            this.cbInspectorName.Location = new System.Drawing.Point(9, 71);
            this.cbInspectorName.Name = "cbInspectorName";
            this.cbInspectorName.SearchRule = ComboBoxExt.ComboBoxExt.searchRules.cbsContains;
            this.cbInspectorName.Size = new System.Drawing.Size(447, 21);
            this.cbInspectorName.SuggestBorderStyle = ComboBoxExt.ComboBoxExt.suggestBorders.sbsNone;
            this.cbInspectorName.SuggestBoxHeight = 106;
            this.cbInspectorName.TabIndex = 3;
            this.cbInspectorName.SelectedValueChanged += new System.EventHandler(this.cbInspectorName_SelectedValueChanged);
            this.cbInspectorName.TextChanged += new System.EventHandler(this.cbInspectorName_TextChanged);
            // 
            // tbInspectorName
            // 
            this.tbInspectorName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInspectorName.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbInspectorName.Location = new System.Drawing.Point(9, 32);
            this.tbInspectorName.Name = "tbInspectorName";
            this.tbInspectorName.ReadOnly = true;
            this.tbInspectorName.Size = new System.Drawing.Size(467, 20);
            this.tbInspectorName.TabIndex = 2;
            this.tbInspectorName.TextChanged += new System.EventHandler(this.tbInspectorName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Value validation";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Value from file";
            // 
            // chbInspector
            // 
            this.chbInspector.AutoSize = true;
            this.chbInspector.Checked = true;
            this.chbInspector.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbInspector.Location = new System.Drawing.Point(119, 53);
            this.chbInspector.Name = "chbInspector";
            this.chbInspector.Size = new System.Drawing.Size(97, 17);
            this.chbInspector.TabIndex = 10;
            this.chbInspector.Text = "Save inspector";
            this.chbInspector.UseVisualStyleBackColor = true;
            // 
            // chbMaster
            // 
            this.chbMaster.AutoSize = true;
            this.chbMaster.Checked = true;
            this.chbMaster.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbMaster.Location = new System.Drawing.Point(119, 54);
            this.chbMaster.Name = "chbMaster";
            this.chbMaster.Size = new System.Drawing.Size(86, 17);
            this.chbMaster.TabIndex = 10;
            this.chbMaster.Text = "Save Master";
            this.chbMaster.UseVisualStyleBackColor = true;
            // 
            // chbChEng
            // 
            this.chbChEng.AutoSize = true;
            this.chbChEng.Checked = true;
            this.chbChEng.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbChEng.Location = new System.Drawing.Point(119, 56);
            this.chbChEng.Name = "chbChEng";
            this.chbChEng.Size = new System.Drawing.Size(123, 17);
            this.chbChEng.TabIndex = 10;
            this.chbChEng.Text = "Save Chief Engineer";
            this.chbChEng.UseVisualStyleBackColor = true;
            // 
            // pbInspectorSaved
            // 
            this.pbInspectorSaved.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbInspectorSaved.Location = new System.Drawing.Point(479, 111);
            this.pbInspectorSaved.Name = "pbInspectorSaved";
            this.pbInspectorSaved.Size = new System.Drawing.Size(18, 18);
            this.pbInspectorSaved.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbInspectorSaved.TabIndex = 11;
            this.pbInspectorSaved.TabStop = false;
            // 
            // pbMasterSaved
            // 
            this.pbMasterSaved.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbMasterSaved.Location = new System.Drawing.Point(479, 113);
            this.pbMasterSaved.Name = "pbMasterSaved";
            this.pbMasterSaved.Size = new System.Drawing.Size(18, 18);
            this.pbMasterSaved.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbMasterSaved.TabIndex = 12;
            this.pbMasterSaved.TabStop = false;
            // 
            // pbChengSaved
            // 
            this.pbChengSaved.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbChengSaved.InitialImage = null;
            this.pbChengSaved.Location = new System.Drawing.Point(479, 114);
            this.pbChengSaved.Name = "pbChengSaved";
            this.pbChengSaved.Size = new System.Drawing.Size(18, 18);
            this.pbChengSaved.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbChengSaved.TabIndex = 13;
            this.pbChengSaved.TabStop = false;
            // 
            // FrmImportPersonnel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 539);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FrmImportPersonnel";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update inspector, master and chief engineer from Excel file";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmImportPersonnel_FormClosing);
            this.Load += new System.EventHandler(this.FrmImportPersonnel_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmImportPersonnel_KeyDown);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInspections)).EndInit();
            this.panel2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbChEng)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMaster)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbInspector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInspectorSaved)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMasterSaved)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbChengSaved)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvInspections;
        private System.Windows.Forms.GroupBox groupBox2;
        private ComboBoxExt.ComboBoxExt cbMasterName;
        private System.Windows.Forms.TextBox tbMasterName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCopyInspector;
        private ComboBoxExt.ComboBoxExt cbInspectorName;
        private System.Windows.Forms.TextBox tbInspectorName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnCopyChEng;
        private ComboBoxExt.ComboBoxExt cbChEngName;
        private System.Windows.Forms.TextBox tbChEngName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCopyMaster;
        private System.Windows.Forms.PictureBox pbInspector;
        private System.Windows.Forms.PictureBox pbMaster;
        private System.Windows.Forms.PictureBox pbChEng;
        private System.Windows.Forms.TextBox tbChEng;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbMaster;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbInspector;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnAddChEng;
        private System.Windows.Forms.Button btnAddMaster;
        private System.Windows.Forms.Button btnAddInspector;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnEditChEng;
        private System.Windows.Forms.Button btnEditMaster;
        private System.Windows.Forms.Button btnEditInspector;
        private System.Windows.Forms.CheckBox chbChEng;
        private System.Windows.Forms.CheckBox chbMaster;
        private System.Windows.Forms.CheckBox chbInspector;
        private System.Windows.Forms.PictureBox pbChengSaved;
        private System.Windows.Forms.PictureBox pbMasterSaved;
        private System.Windows.Forms.PictureBox pbInspectorSaved;
    }
}