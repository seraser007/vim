using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace VIM
{
    public partial class FormCrewPositions : Form
    {
        private BindingSource BS = new BindingSource();
        private bool _wasChanged = false;

        public bool wasChanged
        {
            get { return _wasChanged; }
        }

        public FormCrewPositions()
        {
            this.Icon = MainForm.mainIcon;
            this.Font = MainForm.mainFont;

            InitializeComponent();

            FillPositions();
        }

        private void FillPositions()
        {
            OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

            cmd.CommandText =
                "select CREW_POSITION_GUID, POSITION_NAME, POSITION_INDEX \n" +
                "from CREW_POSITIONS \n" +
                "order by POSITION_INDEX";

            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            if (MainForm.DS.Tables.Contains("CREW_POSITION_LIST"))
                MainForm.DS.Tables["CREW_POSITION_LIST"].Clear();

            adapter.Fill(MainForm.DS, "CREW_POSITION_LIST");

            BS.DataSource = MainForm.DS;
            BS.DataMember = "CREW_POSITION_LIST";

            adgvCrewPositions.DataSource = BS;

            adgvCrewPositions.Columns["CREW_POSITION_GUID"].Visible = false;

            adgvCrewPositions.Columns["POSITION_NAME"].HeaderText = "Position";
            adgvCrewPositions.Columns["POSITION_NAME"].FillWeight = 80;

            adgvCrewPositions.Columns["POSITION_INDEX"].HeaderText = "Index";
            adgvCrewPositions.Columns["POSITION_INDEX"].FillWeight = 20;
        }

        private void adgvCrewPositions_FilterStringChanged(object sender, EventArgs e)
        {
            BS.Filter = adgvCrewPositions.FilterString;
        }

        private void adgvCrewPositions_SortStringChanged(object sender, EventArgs e)
        {
            BS.Sort = adgvCrewPositions.SortString;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //Edit position
            if (adgvCrewPositions.Rows.Count == 0)
                return;

            FormPositionEditor form = new FormPositionEditor();

            form.positionGuid = MainForm.StrToGuid(adgvCrewPositions.CurrentRow.Cells["CREW_POSITION_GUID"].Value.ToString());
            form.positionName = adgvCrewPositions.CurrentRow.Cells["POSITION_NAME"].Value.ToString();
            form.positionIndex = Convert.ToInt32(adgvCrewPositions.CurrentRow.Cells["POSITION_INDEX"].Value);

            var rslt = form.ShowDialog();

            if (rslt==DialogResult.OK && form.wasChanged)
            {
                _wasChanged = true;

                FillPositions();

                MainForm.LocateAdvGridRecord(form.positionName, "POSITION_NAME", 1, adgvCrewPositions);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            //Create new position

            FormPositionEditor form = new FormPositionEditor();

            var rslt = form.ShowDialog();

            if (rslt==DialogResult.OK)
            {
                _wasChanged = true;

                FillPositions();

                MainForm.LocateAdvGridRecord(form.positionName, "POSITION_NAME", 1, adgvCrewPositions);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Delete position
            if (adgvCrewPositions.Rows.Count == 0)
                return;

            Guid posGuid = MainForm.StrToGuid(adgvCrewPositions.CurrentRow.Cells["CREW_POSITION_GUID"].Value.ToString());
            string posName = adgvCrewPositions.CurrentRow.Cells["POSITION_NAME"].Value.ToString();

            string text =
                "You are going to delete the following position record: \n\n" +
                posName + "\n\n" +
                "Whould you like to proceed?";

            if (MessageBox.Show(text,"Confirmation",MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)==DialogResult.Yes)
            {
                //Check for position in use

                OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

                cmd.CommandText =
                    "select Count(CREW_POSITION_GUID) as RecCount \n" +
                    "from CREW \n" +
                    "where CREW_POSITION_GUID=" + MainForm.GuidToStr(posGuid);

                int count = (int)cmd.ExecuteScalar();

                if (count>0)
                {
                    MessageBox.Show("Selected position is in use. You are unable to delete it.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                cmd.CommandText=
                    "delete from CREW_POSITIONS \n"+
                    "where CREW_POSITION_GUID=" + MainForm.GuidToStr(posGuid);

                if (MainForm.cmdExecute(cmd)<0)
                {
                    MessageBox.Show("Failed to delete selected record", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                FillPositions();
            }

        }
    }
}
