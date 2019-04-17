using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VIM
{
    public partial class ApplicationNameForm : Form
    {
        public ApplicationNameForm(Icon mainIcon, Font mainFont)
        {
            InitializeComponent();

            this.Icon = mainIcon;
            this.Font = mainFont;
        }
    }
}
