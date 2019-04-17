using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VIM
{
    public partial class UserControlMonthSelector : UserControl
    {
        [Description("Selected year"), Category("Data")]
        int selYear;
		int selMonth;
		
		public int selectedYear 
        { 
            get { return selYear; } 
            set { selYear = value; Refresh(); } 
        }
        [Description("Selected month"), Category("Data")]
        public int selectedMonth 
        {
            get { return selMonth; }
            set { selMonth = value; Refresh(); } 
        }

        DateTime selectedDate=DateTime.Today;

        public UserControlMonthSelector()
        {
            InitializeComponent();
            tbMonthYear.Text = DateTime.Today.ToString("MMMM, yyyy");
            selectedYear = DateTime.Today.Year;
            selectedMonth = DateTime.Today.Month;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            selectedDate = selectedDate.AddMonths(-1);

            selYear = selectedDate.Year;
            selMonth = selectedDate.Month;

            tbMonthYear.Text = selectedDate.ToString("MMMM, yyyy");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selectedDate = selectedDate.AddMonths(1);
            selYear = selectedDate.Year;
            selMonth = selectedDate.Month;
            tbMonthYear.Text = selectedDate.ToString("MMMM, yyyy");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormCalendars form = new FormCalendars();

            form.selYear = selectedYear;
            form.selMonth = selectedMonth;

            Point location = panel1.PointToScreen(Point.Empty);

            //Point formLocation=panel1.FindForm().PointToScreen(Point.Empty);

            //location.X = location.X + formLocation.X;
            //location.Y = location.Y + formLocation.Y;

            Rectangle screenBounds = Screen.GetWorkingArea(this);
            
            if (screenBounds.Height - form.Height - location.Y < 1)
                location.Y = location.Y - form.Height;
            else
                location.Y = location.Y + panel1.Height;
                

            form.Location = location;

            form.ShowDialog();

            selectedDate = selectedDate.AddYears(form.selYear - selectedYear);
            selectedYear = selectedDate.Year;
            selectedDate = selectedDate.AddMonths(form.selMonth - selectedMonth);
            selectedMonth = selectedDate.Month;
            tbMonthYear.Text = selectedDate.ToString("MMMM, yyyy");
        }

        public override void Refresh()
        {
            if (selectedMonth > 0 && selectedMonth < 13 && selectedYear > 0)
            {
                selectedDate = selectedDate.AddYears(selectedYear - selectedDate.Year);
                selectedDate = selectedDate.AddMonths(selectedMonth - selectedDate.Month);
                tbMonthYear.Text = selectedDate.ToString("MMMM, yyyy");
            }
        }
    }
}
