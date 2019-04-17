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
    public partial class FormPeriodSelector : Form
    {

        public DateTime startDate
        {
            get { return dtStart.Value; }
            set { dtStart.Value = value; }
        }

        public DateTime finishDate
        {
            get { return dtFinish.Value; }
            set { dtFinish.Value = value; }
        }

        public int selector
        {
            get { if (rbtnQuater.Checked) return 1; else return 2; }
            set
            {
                if (value == 1) { rbtnQuater.Checked = true; cbQuater.Focus(); }
                else { rbtnDates.Checked = true; dtStart.Focus(); }
            }
        }

        public int quater
        {
            get { return cbQuater.SelectedIndex + 1; }
            set { cbQuater.Text = "Quater " + value.ToString(); }
        }

        public int quaterYear
        {
            get { return Convert.ToInt32(neYear.Value); }
            set { neYear.Value = Convert.ToDecimal(value); }
        }

        public FormPeriodSelector()
        {
            this.Icon = MainForm.mainIcon;
            this.Font = MainForm.mainFont;

            InitializeComponent();

        }

        private void FormPeriodSelector_Shown(object sender, EventArgs e)
        {
            if (selector == 1)
                cbQuater.Focus();
            else
                dtStart.Focus();
        }
    }
}
