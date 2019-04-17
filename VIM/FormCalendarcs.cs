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
    public partial class FormCalendars : Form
    {
        public int selYear { get; set; }
        public int selMonth { get; set; }

        public int yearMonth
        {
            get { return selYear * 100 + selMonth; }
            set { selYear = (int)value / 100; selMonth = value - selYear; }
        }

        public FormCalendars()
        {
            InitializeComponent();

            dgvCalendar.Rows.Add(3);

            dgvCalendar.Rows[0].Cells[0].Value = "Jan";
            dgvCalendar.Rows[0].Cells[1].Value = "Feb";
            dgvCalendar.Rows[0].Cells[2].Value = "Mar";
            dgvCalendar.Rows[0].Cells[3].Value = "Apr";
            dgvCalendar.Rows[1].Cells[0].Value = "May";
            dgvCalendar.Rows[1].Cells[1].Value = "Jun";
            dgvCalendar.Rows[1].Cells[2].Value = "Jul";
            dgvCalendar.Rows[1].Cells[3].Value = "Aug";
            dgvCalendar.Rows[2].Cells[0].Value = "Sep";
            dgvCalendar.Rows[2].Cells[1].Value = "Oct";
            dgvCalendar.Rows[2].Cells[2].Value = "Nov";
            dgvCalendar.Rows[2].Cells[3].Value = "Dec";

            this.Height = panel1.Height + panel2.Height + dgvCalendar.RowTemplate.Height * 3 + 3;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            selYear--;

            if (selYear < 0)
                selYear = 0;

            lblYear.Text = selYear.ToString();
        }

        private void btnIncrease_Click(object sender, EventArgs e)
        {
            selYear++;

            lblYear.Text = selYear.ToString();
        }

        private void dgvCalendar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dgvCalendar.CurrentCell.RowIndex;
            int col = dgvCalendar.CurrentCell.ColumnIndex;

            selMonth = (col + 1) + 4 * row;

            this.Close();
        }

        private void lblToday_Click(object sender, EventArgs e)
        {
            selYear = DateTime.Today.Year;
            selMonth = DateTime.Today.Month;

            this.Close();
        }

        private void FormCalendars_Shown(object sender, EventArgs e)
        {

            lblYear.Text = selYear.ToString();

            int row = (int)(selMonth-1) / 4;
            int col=(selMonth-1) % 4;

            dgvCalendar.CurrentCell = dgvCalendar[col, row];

        }
    }
}
