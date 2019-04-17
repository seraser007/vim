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
    public partial class FrmCrewOnBoardEditor : Form
    {
        private string _vesselName = "";
        private Guid _vesselGuid = MainForm.zeroGuid;

        public string crewName
        {
            get { return tbName.Text; }
            set { tbName.Text = value; }
        }

        public Guid vesselGuid
        {
            get { return _vesselGuid; }
            set { _vesselGuid = value; }
        }
        public string crewVessel
        {
            get { return _vesselName; }
            set
            {
                if (_vesselGuid == MainForm.zeroGuid)
                { 
                    cbVessel.Text = value; 
                }
                else
                {
                    cbVessel.Text = value+" (IMO:"+MainForm.GetVesselIMOForID(_vesselGuid)+")";
                }
            }
        }

        public string crewPosition
        {
            get { return cbPosition.Text; }
            set { cbPosition.Text = value; }
        }

        public DateTime crewSignOn
        {
            get { return dtSignOn.Value; }
            set { SetSignOn(value); }
        }

        public DateTime crewSignOff
        {
            get { return dtSignOff.Value; }
            set { SetSignOff(value); }
        }

        private OleDbConnection connection;
        private DataSet DS;

        public FrmCrewOnBoardEditor()
        {
            this.Icon = MainForm.mainIcon;
            this.Font = MainForm.mainFont;

            connection = MainForm.connection;
            DS = MainForm.DS;

            InitializeComponent();

            FillVessels();
        }

        private void FillVessels()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select VESSEL_NAME, VESSEL_GUID, VESSEL_NAME+' (IMO:'+VESSEL_IMO+')' as VESSEL_WITH_IMO \n" +
                "from VESSELS \n" +
                "order by VESSEL_NAME, VESSEL_IMO";

            if (DS.Tables.Contains("VESSELS_FOR_CREW"))
            {
                DS.Tables["VESSELS_FOR_CREW"].Clear();
            }

            OleDbDataAdapter vslAdapter = new OleDbDataAdapter(cmd);

            vslAdapter.Fill(DS, "VESSELS_FOR_CREW");

            DataTable dt = DS.Tables["VESSELS_FOR_CREW"];

            DataRow[] rows = dt.Select();

            cbVessel.Items.Clear();
            cbVessel.Items.Add("");

            foreach (DataRow row in rows)
            {
                cbVessel.Items.Add(row["VESSEL_WITH_IMO"].ToString());
            }


        }

        private void dtSignOff_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46)
            {
                dtSignOff.Value = DateTimePicker.MinimumDateTime;
            }
        }

        private void dtSignOff_ValueChanged(object sender, EventArgs e)
        {
            if (dtSignOff.Value == DateTimePicker.MinimumDateTime)
            {
                dtSignOff.Format = DateTimePickerFormat.Custom;
                dtSignOff.CustomFormat = " ";
                dtSignOff.Value = DateTimePicker.MinimumDateTime; ;
            }
            else
            {
                dtSignOff.Format = DateTimePickerFormat.Short;
            }
        }

        private void SetSignOff(DateTime value)
        {
            if ((value==null) || (value <= DateTimePicker.MinimumDateTime))
            {
                dtSignOff.Format = DateTimePickerFormat.Custom;
                dtSignOff.CustomFormat = " ";
                dtSignOff.Value = DateTimePicker.MinimumDateTime;
            }
            else
            {
                dtSignOff.Format = DateTimePickerFormat.Short;

                if (value >= DateTimePicker.MinimumDateTime)
                    dtSignOff.Value = value;
                else
                    dtSignOff.Value = DateTimePicker.MinimumDateTime;
            }
        }

        private void dtSignOn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46)
            {
                dtSignOn.Value = DateTimePicker.MinimumDateTime;
            }
        }

        private void dtSignOn_ValueChanged(object sender, EventArgs e)
        {
            if (dtSignOn.Value == DateTimePicker.MinimumDateTime)
            {
                dtSignOn.Format = DateTimePickerFormat.Custom;
                dtSignOn.CustomFormat = " ";
                dtSignOn.Value = DateTimePicker.MinimumDateTime; ;
            }
            else
            {
                dtSignOn.Format = DateTimePickerFormat.Short;
            }
        }

        private void SetSignOn(DateTime value)
        {
            if ((value == null) || (value <= DateTimePicker.MinimumDateTime))
            {
                dtSignOn.Format = DateTimePickerFormat.Custom;
                dtSignOn.CustomFormat = " ";
                dtSignOn.Value = DateTimePicker.MinimumDateTime;
            }
            else
            {
                dtSignOn.Format = DateTimePickerFormat.Short;

                if (value >= DateTimePicker.MinimumDateTime)
                    dtSignOn.Value = value;
                else
                    dtSignOn.Value = DateTimePicker.MinimumDateTime;
            }
        }

        private void cbVessel_SelectedValueChanged(object sender, EventArgs e)
        {
        }

        private void FrmCrewOnBoardEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult==DialogResult.OK)
            {
                string xName;
                string xIMO;
                string fName=cbVessel.Text;

                ParseFullVesselName(fName, out xName, out xIMO);

                _vesselGuid = MainForm.GetVesselGuid(xName, xIMO);
                _vesselName = xName;

            }
        }

        private void ParseFullVesselName(string fullVesselName, out string aVesselName, out string aVesselIMO)
        {
            if (fullVesselName.Contains("IMO"))
            {
                int psn = fullVesselName.IndexOf("(");

                aVesselName = fullVesselName.Substring(0, psn - 1).Trim();

                psn = fullVesselName.IndexOf("IMO:");

                aVesselIMO = fullVesselName.Substring(psn + 4, 7);
            }
            else
            {
                aVesselName = fullVesselName;
                aVesselIMO = "";
            }
        }
        
    }
}
