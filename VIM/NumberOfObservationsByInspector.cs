using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VIM
{
    public partial class NumberOfObservationsByInspector : Form
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

        public bool hideInspectors
        {
            get { return chbHideInspector.Checked; }
            set { chbHideInspector.Checked = value; }
        }

        public NumberOfObservationsByInspector(Font mainFont, Icon mainIcon)
        {
            this.Font = mainFont;
            this.Icon = mainIcon;
            
            InitializeComponent();
        }
    }
}
