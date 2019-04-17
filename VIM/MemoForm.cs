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
    public partial class MemoForm : Form
    {
        public MemoForm(string Text)
        {

            InitializeComponent();
            richTextBox1.Text = Text;
        }
    }
}
