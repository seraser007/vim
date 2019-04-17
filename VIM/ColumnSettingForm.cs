using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VIM
{
    public partial class ColumnSettingForm : Form
    {
        public ColumnSettingForm(Font mainFont, Icon mainIcon)
        {
            this.Font = mainFont;
            this.Icon = mainIcon;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = textBox1.BackColor;

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.BackColor = colorDialog1.Color;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = textBox2.BackColor;

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.BackColor = colorDialog1.Color;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = textBox3.BackColor;

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox3.BackColor = colorDialog1.Color;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = textBox4.BackColor;

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox4.BackColor = colorDialog1.Color;
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = textBox5.BackColor;

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox5.BackColor = colorDialog1.Color;
            }

        }
    }
}
