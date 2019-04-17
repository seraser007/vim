namespace VIM
{
    partial class FormCalendars
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCalendars));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblYear = new System.Windows.Forms.Label();
            this.btnIncrease = new System.Windows.Forms.Button();
            this.btnDecrease = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblToday = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvCalendar = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalendar)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.lblYear);
            this.panel1.Controls.Add(this.btnIncrease);
            this.panel1.Controls.Add(this.btnDecrease);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(190, 20);
            this.panel1.TabIndex = 0;
            // 
            // lblYear
            // 
            this.lblYear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblYear.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblYear.Location = new System.Drawing.Point(20, 0);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(150, 20);
            this.lblYear.TabIndex = 2;
            this.lblYear.Text = "2016";
            this.lblYear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnIncrease
            // 
            this.btnIncrease.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnIncrease.Image = ((System.Drawing.Image)(resources.GetObject("btnIncrease.Image")));
            this.btnIncrease.Location = new System.Drawing.Point(170, 0);
            this.btnIncrease.Name = "btnIncrease";
            this.btnIncrease.Size = new System.Drawing.Size(20, 20);
            this.btnIncrease.TabIndex = 1;
            this.btnIncrease.UseVisualStyleBackColor = true;
            this.btnIncrease.Click += new System.EventHandler(this.btnIncrease_Click);
            // 
            // btnDecrease
            // 
            this.btnDecrease.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDecrease.Image = ((System.Drawing.Image)(resources.GetObject("btnDecrease.Image")));
            this.btnDecrease.Location = new System.Drawing.Point(0, 0);
            this.btnDecrease.Name = "btnDecrease";
            this.btnDecrease.Size = new System.Drawing.Size(20, 20);
            this.btnDecrease.TabIndex = 0;
            this.btnDecrease.UseVisualStyleBackColor = true;
            this.btnDecrease.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblToday);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 140);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(190, 23);
            this.panel2.TabIndex = 2;
            // 
            // lblToday
            // 
            this.lblToday.AutoSize = true;
            this.lblToday.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblToday.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblToday.Location = new System.Drawing.Point(50, 5);
            this.lblToday.Name = "lblToday";
            this.lblToday.Size = new System.Drawing.Size(100, 13);
            this.lblToday.TabIndex = 1;
            this.lblToday.Text = "December, 2016";
            this.lblToday.Click += new System.EventHandler(this.lblToday_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Today:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvCalendar
            // 
            this.dgvCalendar.AllowUserToAddRows = false;
            this.dgvCalendar.AllowUserToDeleteRows = false;
            this.dgvCalendar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCalendar.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvCalendar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCalendar.ColumnHeadersVisible = false;
            this.dgvCalendar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCalendar.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCalendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCalendar.Location = new System.Drawing.Point(0, 20);
            this.dgvCalendar.Name = "dgvCalendar";
            this.dgvCalendar.ReadOnly = true;
            this.dgvCalendar.RowHeadersVisible = false;
            this.dgvCalendar.RowTemplate.Height = 42;
            this.dgvCalendar.Size = new System.Drawing.Size(190, 120);
            this.dgvCalendar.TabIndex = 3;
            this.dgvCalendar.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCalendar_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // FormCalendars
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(190, 163);
            this.Controls.Add(this.dgvCalendar);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormCalendars";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormCalendarcs";
            this.Shown += new System.EventHandler(this.FormCalendars_Shown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalendar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Button btnIncrease;
        private System.Windows.Forms.Button btnDecrease;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblToday;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvCalendar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}