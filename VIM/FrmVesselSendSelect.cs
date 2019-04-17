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
    public partial class FrmVesselSendSelect : Form
    {
        private DataSet DS;
        private OleDbConnection connection;
        private string _vesselEmail = "";
        private string _vesselName = "";

        public Guid vesselGuid
        {
            get 
            {
                if (cbeVessels.SelectedValue != System.DBNull.Value && cbeVessels.SelectedValue != null)
                {
                    object obj = cbeVessels.SelectedValue;
                    return MainForm.StrToGuid(obj.ToString());
                }
                else
                    return MainForm.zeroGuid;
            }
        }

        public string vesselEmail
        {
            get { return _vesselEmail; }
        }

        public string vesselName
        {
            get { return _vesselName; }
        }

        public FrmVesselSendSelect()
        {
            this.Font = MainForm.mainFont;
            this.Icon = MainForm.mainIcon;

            DS = MainForm.DS;
            connection = MainForm.connection;

            InitializeComponent();

            FillVessels();
        }

        private void FillVessels()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select VESSEL_GUID, VESSEL_NAME, VESSEL_EMAIL, VESSEL_NAME+' - IMO:'+VESSEL_IMO+' Email:'+VESSEL_EMAIL as VESSEL \n" +
                "from VESSELS \n" +
                "where \n" +
                "HIDDEN=FALSE \n" +
                "order by VESSEL_NAME";

            OleDbDataAdapter vesselAdapter = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("VESSEL_SELECTION"))
                DS.Tables["VESSEL_SELECTION"].Clear();

            vesselAdapter.Fill(DS, "VESSEL_SELECTION");


            cbeVessels.DataSource = DS.Tables["VESSEL_SELECTION"];
            cbeVessels.DisplayMember = "VESSEL";
            cbeVessels.ValueMember = "VESSET_GUID";
            cbeVessels.SelectedValue = 0;
        }

        private void cbVessels_TextChanged(object sender, EventArgs e)
        {
        }

        private void cbeVessels_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbeVessels.SelectedValue != System.DBNull.Value && cbeVessels.SelectedValue != null)
            {
                try
                {
                    DataRow[] rows = DS.Tables["VESSEL_SELECTION"].Select("ID=" + cbeVessels.SelectedValue.ToString());

                    if (rows.Length > 0)
                    {
                        _vesselEmail = rows[0]["VESSEL_EMAIL"].ToString();
                        _vesselName = rows[0]["VESSEL_NAME"].ToString();
                    }
                    else
                    {
                        _vesselEmail = "";
                        _vesselName = "";
                    }
                }
                catch
                {

                }
            }
            else
            {
                _vesselEmail = "";
                _vesselName = "";
            }

        }
    }
}
