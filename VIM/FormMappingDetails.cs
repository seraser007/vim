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
    public partial class FormMappingDetails : Form
    {
        public string masterNumber
        {
            set { tbMasterNumber.Text = value; }
        }

        public string masterGUID
        {
            set { tbMasterGUID.Text = value; }
        }

        public string slaveNumber
        {
            set { tbSlaveNumber.Text = value; }
        }

        public string slaveGUID
        {
            set { tbSlaveGUID.Text = value; }
        }

        public FormMappingDetails()
        {
            InitializeComponent();
        }
    }
}
