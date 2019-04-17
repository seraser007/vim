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
    public partial class StatisticFilterForm : Form
    {

        public string conditionDate
        {
            get { return cbConditionDate.Text; }
            set { cbConditionDate.Text = value; }
        }

        public DateTime valueDate1
        {
            get { return dtValueDate.Value.Date; }
            set { dtValueDate.Value = value; }
        }

        public DateTime valueDate2
        {
            get { return dtValueDate2.Value; }
            set { dtValueDate2.Value = value; }
        }

        public string conditionChapterNumber
        {
            get { return cbConditionChapterNumber.Text; }
            set { cbConditionChapterNumber.Text = value; }
        }

        public string valueChapterNumber
        {
            get { return cbValueChapterNumber.Text; }
            set { cbValueChapterNumber.Text = value; }
        }

        public string conditionQType
        {
            get { return cbConditionQType.Text; }
            set { cbConditionQType.Text = valueChapterNumber; }
        }

        public string valueQType
        {
            get { return cbValueQType.Text; }
            set { cbValueQType.Text = value; }
        }

        public string conditionOffice
        {
            get { return cbConditionOffice.Text; }
            set { cbConditionOffice.Text = value; }
        }

        public string valueOffice
        {
            get { return cbValueOffice.Text; }
            set { cbValueOffice.Text = value; }
        }

        public string conditionDOC
        {
            get { return cbConditionDOC.Text; }
            set { cbConditionDOC.Text = value; }
        }

        public string valueDOC
        {
            get { return cbValueDOC.Text; }
            set { cbValueDOC.Text = value; }
        }

        public bool groupByType
        {
            get { return chbGroupByType.Checked; }
            set { chbGroupByType.Checked = value; }
        }

        public bool useMapping
        {
            get { return chbUseMapping.Checked; }
            set { chbUseMapping.Checked = value; }
        }

        public StatisticFilterForm()
        {
            this.Font = MainForm.mainFont;
            this.Icon = MainForm.mainIcon;

            InitializeComponent();

            FillOffices();
            FillDOCs();
        }

        private void conditionChapterNumber_TextChanged(object sender, EventArgs e)
        {
            dtValueDate2.Visible = cbConditionDate.Text.Contains("Between");
        }

        private void FillOffices()
        {
            cbValueOffice.Items.Clear();
            cbValueOffice.Items.Add("");

            OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

            cmd.CommandText =
                "select OFFICE_ID \n" +
                "from OFFICES \n" +
                "order by OFFICE_ID";

            OleDbDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cbValueOffice.Items.Add(reader["OFFICE_ID"].ToString());
                }
            }

            reader.Close();
        }

        private void FillDOCs()
        {
            cbValueDOC.Items.Clear();
            cbValueDOC.Items.Add("");

            OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

            cmd.CommandText =
                "select DOC_ID \n" +
                "from DOCS \n" +
                "order by DOC_ID";

            OleDbDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cbValueDOC.Items.Add(reader["DOC_ID"].ToString());
                }
            }

            reader.Close();
        }
    }
}
