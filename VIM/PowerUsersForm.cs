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
    
    public partial class PowerUsersForm : Form
    {
        OleDbConnection connection;
        DataSet DS;
        OleDbDataAdapter daPowerUsers;
        
        public PowerUsersForm()
        {
            connection = MainForm.connection;
            DS = MainForm.DS;

            this.Icon = MainForm.mainIcon;
            this.Font = MainForm.mainFont;

            InitializeComponent();

            textBox1.Text = MainForm.userName;

            OleDbCommand cmd = new OleDbCommand("", connection);
            cmd.CommandText =
                "select * from POWER_USERS";

            if (DS.Tables.Contains("POWER_USERS"))
                DS.Tables["POWER_USERS"].Clear();

            daPowerUsers = new OleDbDataAdapter(cmd);

            daPowerUsers.Fill(DS, "POWER_USERS");

            dataGridView1.DataSource = DS;
            dataGridView1.DataMember = "POWER_USERS";

            dataGridView1.Columns["USER_NAME"].HeaderText = "User name";

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Insert new user

            PowerUserForm pUserF = new PowerUserForm(this.Icon, this.Font);

            var rslt = pUserF.ShowDialog();

            if (rslt==DialogResult.OK)
            {
                if (pUserF.textBox1.Text.Trim().Length > 0)
                {
                    OleDbCommand cmd = new OleDbCommand("", connection);
                    cmd.CommandText =
                        "insert into POWER_USERS (USER_NAME) \n" +
                        "values('" + MainForm.StrToSQLStr(pUserF.textBox1.Text) + "')";

                    if (MainForm.cmdExecute(cmd)>=0)
                    {
                        DS.Tables["POWER_USERS"].Clear();
                        daPowerUsers.Fill(DS, "POWER_USERS");
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            RecordEdit();
        }

        private void RecordEdit()
        {
            //Edit user
            string cUser = dataGridView1.CurrentRow.Cells["USER_NAME"].Value.ToString();

            PowerUserForm pUserF = new PowerUserForm(this.Icon, this.Font);

            pUserF.textBox1.Text = cUser;

            var rslt = pUserF.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                if (pUserF.textBox1.Text.Trim().Length > 0)
                {
                    OleDbCommand cmd = new OleDbCommand("", connection);
                    cmd.CommandText =
                        "update POWER_USERS set \n" +
                        "USER_NAME='" + MainForm.StrToSQLStr(pUserF.textBox1.Text) + "' \n" +
                        "where USER_NAME='" + MainForm.StrToSQLStr(cUser) + "'";

                    if (MainForm.cmdExecute(cmd)>=0)
                    {
                        DS.Tables["POWER_USERS"].Clear();
                        daPowerUsers.Fill(DS, "POWER_USERS");
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Delete user
            string cUser = dataGridView1.CurrentRow.Cells["USER_NAME"].Value.ToString();

            string msgText =
                "You are going to delete record for the following power user: \n\n" +
                cUser + "\n\n" +
                "Would you like to proceed?";

            var rslt = MessageBox.Show(msgText, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (rslt==DialogResult.Yes)
            {
                OleDbCommand cmd = new OleDbCommand("", connection);

                cmd.CommandText =
                    "delete from POWER_USERS \n" +
                    "where USER_NAME='" + MainForm.StrToSQLStr(cUser) + "'";

                if (MainForm.cmdExecute(cmd)>=0)
                {
                    DS.Tables["POWER_USERS"].Clear();
                    daPowerUsers.Fill(DS, "POWER_USERS");
                }
            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            RecordEdit();
        }

    }
}
