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
    public partial class PowerUserCheckForm : Form
    {
        public string powerKey
        {
            get { return tbPowerKey.Text; }
        }

        public PowerUserCheckForm()
        {
            this.Icon = MainForm.mainIcon;
            this.Font = MainForm.mainFont;

            InitializeComponent();
        }
    }
}
