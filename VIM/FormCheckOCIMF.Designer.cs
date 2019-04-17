namespace VIM
{
    partial class FormCheckOCIMF
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dgvOCIMFRecords = new ADGV.AdvancedDataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOCIMFRecords)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 282);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(616, 35);
            this.panel1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Image = global::VIM.Properties.Resources.open;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.Location = new System.Drawing.Point(451, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 25);
            this.button2.TabIndex = 1;
            this.button2.Text = "Open";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Image = global::VIM.Properties.Resources.Close_Box_Red;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.Location = new System.Drawing.Point(532, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 0;
            this.button1.Text = "Close";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // dgvOCIMFRecords
            // 
            this.dgvOCIMFRecords.AllowUserToAddRows = false;
            this.dgvOCIMFRecords.AllowUserToDeleteRows = false;
            this.dgvOCIMFRecords.AutoGenerateContextFilters = true;
            this.dgvOCIMFRecords.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOCIMFRecords.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvOCIMFRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOCIMFRecords.DateWithTime = false;
            this.dgvOCIMFRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOCIMFRecords.Location = new System.Drawing.Point(0, 0);
            this.dgvOCIMFRecords.Name = "dgvOCIMFRecords";
            this.dgvOCIMFRecords.ReadOnly = true;
            this.dgvOCIMFRecords.RowHeadersWidth = 25;
            this.dgvOCIMFRecords.Size = new System.Drawing.Size(616, 282);
            this.dgvOCIMFRecords.TabIndex = 1;
            this.dgvOCIMFRecords.TimeFilter = false;
            this.dgvOCIMFRecords.SortStringChanged += new System.EventHandler(this.dgvOCIMFRecords_SortStringChanged);
            this.dgvOCIMFRecords.FilterStringChanged += new System.EventHandler(this.dgvOCIMFRecords_FilterStringChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Excel files|*.xls;*.xlsx|All files|*.*";
            // 
            // FormCheckOCIMF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 317);
            this.Controls.Add(this.dgvOCIMFRecords);
            this.Controls.Add(this.panel1);
            this.Name = "FormCheckOCIMF";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Missed inspection reports";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCheckOCIMF_FormClosing);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOCIMFRecords)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private ADGV.AdvancedDataGridView dgvOCIMFRecords;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}