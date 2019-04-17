using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VIM
{
    public partial class NumberOfInspectionsForm : Form
    {
        public string condition
        {
            get { return conditionDate.Text; }
        }

        public DateTime date1
        {
            get { return valueDate.Value.Date; }
        }

        public DateTime date2
        {
            get { return valueDate2.Value; }
        }

        public bool countObservations
        {
            get { return calcObservations.Checked; }
        }

        public string group1Text
        {
            get { return group1.Text; }
        }

        public string group2Text
        {
            get { return group2.Text; }
        }

        public string group3Text
        {
            get { return group3.Text; }
        }

        public NumberOfInspectionsForm(Font mainFont, Icon mainIcon)
        {
            this.Font = mainFont;
            this.Icon = mainIcon;

            InitializeComponent();


        }

        private void conditionDate_TextChanged(object sender, EventArgs e)
        {
            valueDate2.Visible = conditionDate.Text.Contains("Between");
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
