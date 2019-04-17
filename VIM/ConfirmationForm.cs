using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VIM
{
    public partial class ConfirmationForm : Form
    {
        public ConfirmationForm(Icon mainIcon)
        {
            this.Icon = mainIcon;

            InitializeComponent();
        }
    }
}
