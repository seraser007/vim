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
    public partial class QuestionDetailsForm : Form
    {
        OleDbConnection connection;
        DataSet DS;

        public QuestionDetailsForm(OleDbConnection mainConnection, DataSet mainDS, Font mainFont, Icon mainIcon)
        {
            connection = mainConnection;
            DS = mainDS;

            this.Font = mainFont;
            this.Icon = mainIcon;

            InitializeComponent();

            fillSubchapters();
        }

        private void fillSubchapters()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select SUBCHAPTER \n" +
                "from TEMPLATE_KEYS \n" +
                "group by SUBCHAPTER";

            OleDbDataAdapter scAdapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("SUBCHAPTERS"))
                DS.Tables["SUBCHAPTERS"].Clear();

            scAdapter.Fill(DS, "SUBCHAPTERS");

            cbSubchapter.DataSource = DS.Tables["SUBCHAPTERS"];
            cbSubchapter.DisplayMember = "SUBCHAPTER";

        }
    }
}
