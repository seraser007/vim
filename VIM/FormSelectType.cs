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
    public partial class FormSelectType : Form
    {
        public string selectedType
        {
            get { return cbTypes.Text; }
            set { cbTypes.Text = value; }
        }

        public FormSelectType(DataSet mainDS, Icon mainIcon, Font mainFont)
        {
            DataSet DS = mainDS;
            this.Icon = mainIcon;
            this.Font = mainFont;

            InitializeComponent();

            cbTypes.DataSource = DS.Tables["TEMPLATE_TYPES"];
            cbTypes.DisplayMember = "TEMPLATE_TYPE";
            cbTypes.ValueMember = "TEMPLATE_TYPE";
        }

        private void FormSelectType_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult==DialogResult.OK)
            {
                if (cbTypes.Text.Trim().Length==0)
                {
                    MessageBox.Show("Please select template type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
            }
        }
    }
}
