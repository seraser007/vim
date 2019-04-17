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
    public partial class SelectVesselForm : Form
    {
        OleDbConnection connection;
        DataSet DS;

        public SelectVesselForm(string Text, OleDbConnection mainConnection, string commandText, DataSet mainDS, Font mainFont, Icon mainIcon)
        {
            this.Font = mainFont;
            this.Icon = mainIcon;

            InitializeComponent();
            textBox1.Text = Text;

            connection = mainConnection;
            DS = mainDS;

            OleDbDataAdapter sameVslsAdapter = new OleDbDataAdapter(commandText, connection);

            if (DS.Tables.Contains("SameVessels"))
            {
                DS.Tables["SameVessels"].Clear();
                DS.Tables["SameVessels"].Columns.Clear();
            }

            sameVslsAdapter.Fill(DS, "SameVessels");

            dataGridView1.DataSource = DS;
            dataGridView1.DataMember = "SameVessels";
            dataGridView1.AutoGenerateColumns = true;

            dataGridView1.Columns["VESSEL_IMO"].HeaderText = "IMO number";
            dataGridView1.Columns["VESSEL_IMO"].FillWeight = 30;

            dataGridView1.Columns["VESSEL_NAME"].HeaderText = "Vessel name";

        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult==DialogResult.Cancel)
                e.Cancel=true;
        }

    }
}
