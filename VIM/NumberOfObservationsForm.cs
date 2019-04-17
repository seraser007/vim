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
    public partial class NumberOfObservationsForm : Form
    {
        DataSet DS;
        OleDbConnection connection;

        public string conditionVessel
        {
            get { return cbConditionVessel.Text; }
            set { cbConditionVessel.Text = value; }
        }

        public string valueVessel
        {
            get { return cbValueVessel.Text; }
            set { cbValueVessel.Text = value; }
        }

        public string conditionInspector
        {
            get { return cbConditionInspector.Text; }
            set { cbConditionInspector.Text = value; }
        }

        public string valueInspector
        {
            get { return cbValueInspector.Text; }
            set { cbValueInspector.Text = value; }
        }

        public string conditionObservation
        {
            get { return cbConditionObservation.Text; }
            set { cbConditionObservation.Text = value; }
        }

        public string valueObservation
        {
            get { return cbValueObservation.Text; }
            set { cbValueObservation.Text = value; }
        }

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

        public string conditionCompany
        {
            get { return cbConditionCompany.Text; }
            set { cbConditionCompany.Text = value; }
        }

        public string valueCompany
        {
            get { return cbValueCompany.Text; }
            set { cbValueCompany.Text = value; }
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

        public bool useMapping
        {
            get { return chbUseMapping.Checked; }
            set { chbUseMapping.Checked = value; }
        }

        public bool ignore
        {
            get { return chbIgnore.Checked; }
            set { chbIgnore.Checked = value; }
        }

        public NumberOfObservationsForm(OleDbConnection mainConnection, DataSet mainDS, Font mainFont, Icon mainIcon)
        {
            DS = mainDS;
            connection = mainConnection;
            this.Font = mainFont;
            this.Icon = mainIcon;
            
            InitializeComponent();

            //Connect to vessels list
            DataTable vessels = DS.Tables["VESSELS_LIST"];

            cbValueVessel.DataSource = vessels;
            cbValueVessel.DisplayMember = "VESSEL_NAME";
            cbValueVessel.ValueMember = "VESSEL_IMO";


            //Connect to list of inspectors
            DataTable inspectors = DS.Tables["INSPECTORS_LIST"];

            cbValueInspector.DataSource = inspectors;
            cbValueInspector.DisplayMember = "INSPECTOR_NAME";
            cbValueInspector.ValueMember = "INSPECTOR_GUID";
            cbValueInspector.Text = "";

            //Connect to list of companies
            DataTable companies = DS.Tables["COMPANIES_LIST"];

            cbValueCompany.DataSource = companies;
            cbValueCompany.DisplayMember = "COMPANY";
            cbValueCompany.Text = "";

            //Connect to list of offices
            DataTable offices = DS.Tables["OFFICES_LIST"];

            cbValueOffice.DataSource = offices;
            cbValueOffice.DisplayMember = "OFFICE_ID";
            cbValueOffice.Text = "";

            //Connect to list of DOCs
            DataTable docs = DS.Tables["DOCS_LIST"];

            cbValueDOC.DataSource = docs;
            cbValueDOC.DisplayMember = "DOC_ID";
            cbValueDOC.Text = "";
        }

        private void conditionDate_TextChanged(object sender, EventArgs e)
        {
            dtValueDate2.Visible = cbConditionDate.Text.Contains("Between");
        }
    }
}
