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
    public partial class MergeForm : Form
    {
        DataSet DS;
        OleDbConnection connection;

        public MergeForm(DataSet mainDS, OleDbConnection mainConnection, Font mainFont, Icon mainIcon)
        {
            this.Font = mainFont;
            this.Icon = mainIcon;

            DS = mainDS;
            connection = mainConnection;

            fillInspectorTable("INSPECTORS");
            fillInspectorTable("INSPECTORS_TWO");

            InitializeComponent();

            photo1.Text = "";
            photo2.Text = "";
            notes1.Text = "";
            notes2.Text = "";
            notesMerged.Text = "";
            value1.Text = "";
            value2.Text = "";
            valueMerged.Text = "";

            FillInspectors();
        }

        public void FillInspectors()
        {
            DataTable inspectorsOne = DS.Tables["INSPECTORS"];
            DataTable inspectorsTwo = DS.Tables["INSPECTORS_TWO"];

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            comboBox1.DataSource = inspectorsOne;
            comboBox1.DisplayMember = "INSPECTOR_NAME";
            comboBox1.ValueMember = "INSPECTOR_GUID";

            comboBox2.DataSource = inspectorsTwo;
            comboBox2.DisplayMember = "INSPECTOR_NAME";
            comboBox2.ValueMember = "INSPECTOR_GUID";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = comboBox1.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = comboBox2.Text;
        }

        private bool SameName(string name)
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            Guid firstGuid = MainForm.StrToGuid(comboBox1.SelectedValue.ToString());
            Guid secondGuid = MainForm.StrToGuid(comboBox2.SelectedValue.ToString());

            cmd.CommandText = "select Count(INSPECTOR_GUID) from INSPECTORS \n" +
                "where INSPECTOR_NAME='" + StrToSQLStr(textBox1.Text) + "' \n"+
                "and INSPECTOR_GUID<>" + MainForm.GuidToStr(firstGuid) + "\n" +
                "and INSPECTOR_GUID<>" + MainForm.GuidToStr(secondGuid);

            int recs = (int)cmd.ExecuteScalar();

            if (recs == 0)
                return false;
            else
                return true;
        }

        public string StrToSQLStr(string Text)
        {
            return Text.Replace("'", "''");
        }

        private void MergeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.Cancel) return;
            
            if ((radioButton11.Checked) && (SameName(textBox1.Text.Trim()))) 
            {
                string msg = "There is a record for inspector with the same name";

                System.Windows.Forms.MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                e.Cancel = true;

                return;
            }
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = radioButton11.Checked;     
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if ((comboBox1.Text.Length == 0) || (comboBox1.Text.StartsWith("System.Data")))
            {
                photo1.Text = "";
                notes1.Text = "";
                value1.Text = "";
                background1.Text = "";
                updateNotes();
            }
            else
            {
                if (comboBox1.SelectedValue == null || MainForm.StrToGuid(comboBox1.SelectedValue.ToString())!=MainForm.zeroGuid)
                    return;

                OleDbCommand cmd = new OleDbCommand("", connection);

                Guid inspectorGuid = MainForm.StrToGuid(comboBox1.SelectedValue.ToString());

                cmd.CommandText =
                    "select INSPECTOR_GUID, INSPECTOR_NAME, Notes, Photo, Unfavourable, Background \n" +
                    "from INSPECTORS \n" +
                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid);

                OleDbDataReader inspector = cmd.ExecuteReader();

                inspector.Read();

                photo1.Text = inspector[3].ToString();

                notes1.Text = inspector[2].ToString();

                value1.Text = inspector[4].ToString();

                background1.Text = inspector[5].ToString();

                updateNotes();
                updateValues();
            }

        }

        private void updateNotes()
        {
            if ((notes1.Text.Length > 0) && (notes2.Text.Length > 0))
            {
                notesMerged.Text = notes1.Text + "\n" + notes2.Text;
            }
            else
            {
                notesMerged.Text = notes1.Text + notes2.Text;
            }
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            if ((comboBox2.Text.Length == 0) || (comboBox2.Text.StartsWith("System.Data")))
            {
                photo2.Text = "";
                notes2.Text = "";
                value2.Text = "";
                background2.Text = "";
                updateNotes();
            }
            else
            {
                if (comboBox2.SelectedValue == null || MainForm.StrToGuid(comboBox2.SelectedValue.ToString())!=MainForm.zeroGuid)
                    return;

                Guid inspectorGuid = MainForm.StrToGuid(comboBox2.SelectedValue.ToString());

                OleDbCommand cmd = new OleDbCommand("", connection);
                cmd.CommandText =
                    "select INSPECTOR_GUID, INSPECTOR_NAME, Notes, Photo, Unfavourable, Background \n" +
                    "from INSPECTORS \n" +
                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid);

                OleDbDataReader inspector = cmd.ExecuteReader();

                inspector.Read();

                photo2.Text = inspector[3].ToString();

                notes2.Text = inspector[2].ToString();

                value2.Text = inspector[4].ToString();

                background2.Text = inspector[5].ToString();

                updateNotes();
                updateValues();
            }

        }

        private void updateValues()
        {
            if ((value1.Text.StartsWith("True")) || (value2.Text.StartsWith("True")))
            {
                valueMerged.Text="True";
            }
            else
                valueMerged.Text="False";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Show 1 inspector card

            if (comboBox1.Text.Trim().Length == 0)
                return;

            OleDbCommand cmd = new OleDbCommand("", connection);

            DataTable dt = DS.Tables["INSPECTORS"];

            DataRow[] dr = dt.Select("INSPECTOR_GUID=" + comboBox1.SelectedValue.ToString());

            if (dr.Length == 0)
                return;

            
            foreach (DataRow row in dr)
            {
                Guid inspectorGuid = MainForm.StrToGuid(row.ItemArray[0].ToString());

                InspectorForm inspForm = new InspectorForm(inspectorGuid, 0);

                inspForm.inspectorName = row.ItemArray[1].ToString();

                inspForm.inspectorNotes = row.ItemArray[2].ToString();

                inspForm.inspectorPhoto = row.ItemArray[3].ToString();

                inspForm.inspectorUnfavourable = Convert.ToBoolean(row.ItemArray[5]);

                inspForm.inspectorBackground = row.ItemArray[6].ToString();

                var rslt = inspForm.ShowDialog();

                if (rslt == DialogResult.OK)
                {
                    //Сохраняем изменения
                    cmd.CommandText =
                    "update INSPECTORS set\n" +
                    "INSPECTOR_NAME='" + StrToSQLStr(inspForm.inspectorName) + "',\n" +
                    "Notes='" + StrToSQLStr(inspForm.inspectorNotes) + "',\n" +
                    "Photo='" + StrToSQLStr(inspForm.inspectorPhoto) + "',\n" +
                    "Unfavourable=" + Convert.ToString(inspForm.inspectorUnfavourable) + "\n" +
                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid);
                    cmd.ExecuteNonQuery();

                    string inspector2 = comboBox2.Text;

                    UpdateInspectorsList();

                    //Записываем имя в поле
                    comboBox1.Text = inspForm.inspectorName;
                    comboBox2.Text = inspector2;
                }

            }
        }

        private void UpdateInspectorsList()
        {
            fillInspectorTable("INSPECTORS");
            fillInspectorTable("INSPECTORS_TWO");
            fillInspectors();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Show 2nd inspector card

            if (comboBox2.Text.Trim().Length == 0) return;

            OleDbCommand cmd = new OleDbCommand("", connection);

            DataTable dt = DS.Tables["INSPECTORS_TWO"];

            DataRow[] dr = dt.Select("INSPECTOR_GUID=" + comboBox2.SelectedValue.ToString());

            if (dr.Length == 0) return;


            foreach (DataRow row in dr)
            {
                Guid inspectorGuid = MainForm.StrToGuid(row.ItemArray[0].ToString());

                InspectorForm inspForm = new InspectorForm(inspectorGuid, 0);

                inspForm.inspectorName = row.ItemArray[1].ToString();

                inspForm.inspectorNotes = row.ItemArray[2].ToString();

                inspForm.inspectorPhoto = row.ItemArray[3].ToString();

                inspForm.inspectorUnfavourable = Convert.ToBoolean(row.ItemArray[4]);

                inspForm.inspectorBackground = row.ItemArray[5].ToString();

                var rslt = inspForm.ShowDialog();

                if (rslt == DialogResult.OK)
                {
                    //Сохраняем изменения
                    cmd.CommandText =
                    "update INSPECTORS set\n" +
                    "INSPECTOR_NAME='" + StrToSQLStr(inspForm.inspectorName) + "',\n" +
                    "Notes='" + StrToSQLStr(inspForm.inspectorNotes) + "',\n" +
                    "Photo='" + StrToSQLStr(inspForm.inspectorPhoto) + "',\n" +
                    "Unfavourable=" + Convert.ToString(inspForm.inspectorUnfavourable) + "\n" +
                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid);
                    cmd.ExecuteNonQuery();

                    string inspector1 = comboBox1.Text;

                    UpdateInspectorsList();

                    //Записываем имя в поле
                    comboBox2.Text = inspForm.inspectorName;
                    comboBox1.Text = inspector1;
                }

            }

        }

        private void fillInspectors()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select * from \n" +
                "(select INSPECTOR_GUID, INSPECTOR_NAME, Notes, Photo, Unfavourable, Background \n" +
                "from INSPECTORS \n" +
                "union \n" +
                "select TOP 1 "+MainForm.GuidToStr(MainForm.zeroGuid)+" as INSPECTOR_GUID, '' as INSPECTOR_NAME, '' as Notes, '' as Photo, " +
                "false as Unfavourable, '' as Background \n" +
                "from INSPECTORS) \n" +
                "order by INSPECTOR_NAME";

            OleDbDataAdapter inspectorsAdapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("INSPECTORS_LIST"))
                DS.Tables["INSPECTORS_LIST"].Clear();

            inspectorsAdapter.Fill(DS, "INSPECTORS_LIST");

        }

        private void fillInspectorTable(string tableName)
        {
            string cmdText =
                "select INSPECTOR_GUID, INSPECTOR_NAME, Notes, Photo, Unfavourable, Background\n" +
                "from INSPECTORS\n" +
                "order by INSPECTOR_NAME";

            OleDbDataAdapter inspectorsAdapter = new OleDbDataAdapter(cmdText, connection);

            if (DS.Tables.Contains(tableName))
                DS.Tables[tableName].Clear();


            inspectorsAdapter.Fill(DS, tableName);

        }

    }
}
