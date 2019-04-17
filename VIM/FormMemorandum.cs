using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VIM
{
    public partial class FormMemorandum : Form
    {
        private int id = 0;

        public int memorandumId
        {
            set { id = value; }
            get { return id; }
        }

        public string memorandumText
        {
            set { tbMemorandum.Text = value; }
            get { return tbMemorandum.Text.Trim(); }
        }

        public FormMemorandum()
        {
            this.Font = MainForm.mainFont;
            this.Icon = MainForm.mainIcon;

            InitializeComponent();
        }

 
        private bool SameRecordExists()
        {
             DataRow[] rows = MainForm.DS.Tables["MEMORANDUMS"].Select();

            foreach (DataRow row in rows)
            {
                if (row["MEMORANDUM_TYPE"].Equals(tbMemorandum.Text.Trim()))
                {
                    if (!row["MEMORANDUM_ID"].Equals(id))
                        return true;
                }
            }

            return false;
        }


    }
}
