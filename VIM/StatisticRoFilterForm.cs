using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace VIM
{
    public partial class StatisticRoFilterForm : Form
    {
        OleDbConnection connection;
        DataSet DS;

        public StatisticRoFilterForm(OleDbConnection mainConnection, DataSet mainDS, Font mainFont, Icon mainIcon)
        {
            connection = mainConnection;
            DS = mainDS;
            this.Font = mainFont;
            this.Icon = mainIcon;

            InitializeComponent();
        }

        private void conditionDate_TextChanged(object sender, EventArgs e)
        {
            valueDate2.Visible = conditionDate.Text.Contains("Between");
        }


    }
}
