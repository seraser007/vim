using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace VIM
{
    public partial class FormInspectionType : Form
    {
        private int id = 0;

        public string inspectionType
        {
            get { return tbInspectionType.Text.Trim(); }
            set { tbInspectionType.Text = value; }
        }

        public int InspectionTypeID
        {
            set { id = value; }
        }

        public FormInspectionType()
        {
            this.Font = MainForm.mainFont;
            this.Icon = MainForm.mainIcon;

            InitializeComponent();
        }

        private bool RecordExists()
        {
            DataRow[] rows = MainForm.DS.Tables["INSPECTION_TYPES"].Select();

            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    if (string.Compare(row["INSPECTION_TYPE"].ToString(),tbInspectionType.Text.Trim(),true)==0)
                    {
                        if (!row["INSPECTION_TYPE_ID"].Equals(id))
                            return true;
                    }
                }

                return false;
            }
            else
                return false;
        }

        private void FormInspectionType_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult!=DialogResult.OK) 
                return;
                
            if (RecordExists())
            {
                MessageBox.Show("There is a record with the same name. \n"+
                    "Please provide another type name.","Error",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);

                e.Cancel=true;

                return;
            }
        }
    }
}
