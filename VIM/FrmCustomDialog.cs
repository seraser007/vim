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
    public partial class FrmCustomDialog : Form
    {

        public bool checkBoxState
        {
            set { cbDialog.Checked = value; }
            get { return cbDialog.Checked; }
        }


        public FrmCustomDialog(Font mainFont, string msgText, string msgCaption, string cbText)
        {
            this.Font = mainFont;

            InitializeComponent();

            tbDialog.Text = msgText;
            this.Text = msgCaption;
            cbDialog.Text = cbText;
        }
    }
}
