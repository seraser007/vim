namespace VIM
{
    partial class FormSelectCrewmemberFromList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCalcel = new System.Windows.Forms.Button();
            this.dgvCrew = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCrew)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.btnCalcel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 223);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(415, 31);
            this.panel1.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Image = global::VIM.Properties.Resources.Ok;
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOk.Location = new System.Drawing.Point(256, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 25);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCalcel
            // 
            this.btnCalcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalcel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCalcel.Image = global::VIM.Properties.Resources.cancel_16;
            this.btnCalcel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCalcel.Location = new System.Drawing.Point(337, 3);
            this.btnCalcel.Name = "btnCalcel";
            this.btnCalcel.Size = new System.Drawing.Size(75, 25);
            this.btnCalcel.TabIndex = 0;
            this.btnCalcel.Text = "Cancel";
            this.btnCalcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCalcel.UseVisualStyleBackColor = true;
            // 
            // dgvCrew
            // 
            this.dgvCrew.AllowUserToAddRows = false;
            this.dgvCrew.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvCrew.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCrew.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCrew.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvCrew.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCrew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCrew.Location = new System.Drawing.Point(0, 0);
            this.dgvCrew.Name = "dgvCrew";
            this.dgvCrew.ReadOnly = true;
            this.dgvCrew.RowHeadersWidth = 25;
            this.dgvCrew.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCrew.Size = new System.Drawing.Size(415, 223);
            this.dgvCrew.TabIndex = 1;
            // 
            // FormSelectCrewmemberFromList
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCalcel;
            this.ClientSize = new System.Drawing.Size(415, 254);
            this.Controls.Add(this.dgvCrew);
            this.Controls.Add(this.panel1);
            this.Name = "FormSelectCrewmemberFromList";
            this.Text = "Select crewmember from list";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSelectCrewmemberFromList_FormClosing);
            this.Shown += new System.EventHandler(this.FormSelectCrewmemberFromList_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCrew)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCalcel;
        private System.Windows.Forms.DataGridView dgvCrew;
    }
}