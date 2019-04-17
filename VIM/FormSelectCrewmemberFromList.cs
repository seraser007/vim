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
    public partial class FormSelectCrewmemberFromList : Form
    {
        private string _crewmemberName = "";
        private Guid _crewGuid = MainForm.zeroGuid;

        public string crewmemberName
        {
            set { _crewmemberName = value; }
        }

        public Guid crewGuid
        {
            get { return _crewGuid; }
        }

        public Guid positionGuid;

        public FormSelectCrewmemberFromList()
        {
            this.Icon = MainForm.mainIcon;
            this.Font = MainForm.mainFont;

            InitializeComponent();
        }

        private void FormSelectCrewmemberFromList_Shown(object sender, EventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

            cmd.CommandText =
                "select CREW_NAME, PERSONAL_ID \n" +
                "from CREW \n" +
                "where \n" +
                "CREW_NAME='" + MainForm.StrToSQLStr(_crewmemberName) + "', \n" +
                "and CREW_POSITION_GUID="+MainForm.GuidToStr(positionGuid)+" \n"+
                "order by PERSONAL_ID";

            if (MainForm.DS.Tables.Contains("CREW_SELECT"))
                MainForm.DS.Tables["CREW_SELECT"].Clear();

            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            adapter.Fill(MainForm.DS, "CREW_SELECT");


        }

        private void FormSelectCrewmemberFromList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
                return;

            if (dgvCrew.Rows.Count == 0)
            {
                _crewGuid = MainForm.zeroGuid;
                return;
            }

            _crewGuid = (Guid)dgvCrew.CurrentRow.Cells["CREW_GUID"].Value;
        }
    }
}
