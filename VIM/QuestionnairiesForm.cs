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
    public partial class QuestionnairiesForm : Form
    {
        OleDbConnection connection;
        DataSet DS;
        //OleDbDataAdapter qAdapter;
        //OleDbDataAdapter rqAdapter;
        //OleDbDataAdapter ovidAdapter;
        //OleDbDataAdapter oviqAdapter;
        bool _templateChanged = false;

        public bool templateChnaged
        {
            get { return _templateChanged; }
        }

        public QuestionnairiesForm(OleDbConnection mainConnection, DataSet mainDS, Font mainFont, Icon mainIcon)
        {
            connection = mainConnection;
            DS = mainDS;
            this.Font = mainFont;
            this.Icon = mainIcon;

            InitializeComponent();

            tabControl1.TabIndex = 0;

            //qAdapter = new OleDbDataAdapter();
            //rqAdapter = new OleDbDataAdapter();
            //ovidAdapter = new OleDbDataAdapter();
            //oviqAdapter = new OleDbDataAdapter();

            updateVIQList();

            updateOviqList();

            UpdatePSCList();

            UpdateCDIList();

            ProtectFields(MainForm.isPowerUser);
        }

        private void ProtectFields(bool status)
        {
            btnDelete.Enabled = status;
            btnEdit.Enabled = status;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Edit questionnaire
            editQuestionnaire();
        }

        public void editQuestionnaire()
        {
            QuestionnaireDetailsForm qdForm = new QuestionnaireDetailsForm();

            var rslt = DialogResult.Cancel;

            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    //Edit VIQ questionnaire

                    qdForm.templateGuid = MainForm.StrToGuid(dgvVIQ.CurrentRow.Cells["TEMPLATE_GUID"].Value.ToString());

                    rslt = qdForm.ShowDialog();

                    if (rslt == DialogResult.OK)
                    {
                        if (!_templateChanged)
                            _templateChanged = qdForm.templateChanged;

                        int curRow = dgvVIQ.CurrentCell.RowIndex;
                        int curCol = dgvVIQ.CurrentCell.ColumnIndex;
                        int vRow = dgvVIQ.FirstDisplayedScrollingRowIndex;

                        updateVIQList();

                        dgvVIQ.FirstDisplayedScrollingRowIndex = vRow;
                        dgvVIQ.CurrentCell = dgvVIQ[curCol, curRow];
                    }
                    break;
                case 1:
                    //Edit OVIQ questionnaire

                    qdForm.templateGuid = MainForm.StrToGuid(dgvOVIQ.CurrentRow.Cells["TEMPLATE_GUID"].Value.ToString());

                    rslt = qdForm.ShowDialog();

                    if (rslt == DialogResult.OK)
                    {
                        if (!_templateChanged)
                            _templateChanged = qdForm.templateChanged;

                        int curRow = dgvOVIQ.CurrentCell.RowIndex;
                        int curCol = dgvOVIQ.CurrentCell.ColumnIndex;
                        int vRow = dgvOVIQ.FirstDisplayedScrollingRowIndex;

                        updateOviqList();

                        if (dgvOVIQ.Rows.Count > 0)
                        {

                            dgvOVIQ.FirstDisplayedScrollingRowIndex = vRow;
                            dgvOVIQ.CurrentCell = dgvOVIQ[curCol, curRow];
                        }
                    }
                    break;
                case 2:
                    qdForm.templateGuid = MainForm.StrToGuid(dgvPSC.CurrentRow.Cells["TEMPLATE_GUID"].Value.ToString());

                    rslt = qdForm.ShowDialog();

                    if (rslt == DialogResult.OK)
                    {
                        if (!_templateChanged)
                            _templateChanged = qdForm.templateChanged;

                        int curRow = dgvPSC.CurrentCell.RowIndex;
                        int curCol = dgvPSC.CurrentCell.ColumnIndex;
                        int vRow = dgvPSC.FirstDisplayedScrollingRowIndex;

                        UpdatePSCList();

                        if (dgvPSC.Rows.Count > 0)
                        {

                            dgvPSC.FirstDisplayedScrollingRowIndex = vRow;
                            dgvPSC.CurrentCell = dgvPSC[curCol, curRow];
                        }
                    }

                    break;
                case 3:
                    qdForm.templateGuid = MainForm.StrToGuid(dgvCDI.CurrentRow.Cells["TEMPLATE_GUID"].Value.ToString());

                    rslt = qdForm.ShowDialog();

                    if (rslt == DialogResult.OK)
                    {
                        if (!_templateChanged)
                            _templateChanged = qdForm.templateChanged;

                        int curRow = dgvCDI.CurrentCell.RowIndex;
                        int curCol = dgvCDI.CurrentCell.ColumnIndex;
                        int vRow = dgvCDI.FirstDisplayedScrollingRowIndex;

                        UpdateCDIList();

                        if (dgvCDI.Rows.Count > 0)
                        {

                            dgvCDI.FirstDisplayedScrollingRowIndex = vRow;
                            dgvCDI.CurrentCell = dgvCDI[curCol, curRow];
                        }
                    }

                    break;

            }

        }

        public string StrToSQLStr(string Text)
        {
            return Text.Replace("'", "''");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            showQuestionnaire();

        }

        private void showQuestionnaire()
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    //View VIQ questionnaire
                    QuestionnaireViewForm qvf0 = new QuestionnaireViewForm(
                        false, dgvVIQ.CurrentRow.Cells["TEMPLATE_GUID"].Value.ToString(),
                        dgvVIQ.CurrentRow.Cells["VERSION"].Value.ToString(),
                        dgvVIQ.CurrentRow.Cells["TYPE_CODE"].Value.ToString());

                    qvf0.ShowDialog();
                    break;
                case 1:
                    //View OVIQ questionnaire
                    QuestionnaireViewForm qvf4 = new QuestionnaireViewForm(
                        false, dgvOVIQ.CurrentRow.Cells["TEMPLATE_GUID"].Value.ToString(),
                        dgvOVIQ.CurrentRow.Cells["VERSION"].Value.ToString(),
                        dgvOVIQ.CurrentRow.Cells["TYPE_CODE"].Value.ToString());

                    qvf4.ShowDialog();
                    break;
                case 2:
                    //View PSC questionnaire
                    QuestionnaireViewForm form = new QuestionnaireViewForm(
                        false, dgvPSC.CurrentRow.Cells["TEMPLATE_GUID"].Value.ToString(),
                        dgvPSC.CurrentRow.Cells["VERSION"].Value.ToString(),
                        dgvPSC.CurrentRow.Cells["TYPE_CODE"].Value.ToString());

                    form.ShowDialog();
                    break;
                case 3:
                    //View CDI questionnaire
                    QuestionnaireViewForm formCDI = new QuestionnaireViewForm(
                        false, dgvCDI.CurrentRow.Cells["TEMPLATE_GUID"].Value.ToString(),
                        dgvCDI.CurrentRow.Cells["VERSION"].Value.ToString(),
                        dgvCDI.CurrentRow.Cells["TYPE_CODE"].Value.ToString());

                    formCDI.ShowDialog();
                    break;

            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string msg = "";
            var rslt=DialogResult.Cancel;

            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    {
                        msg = "You are going to delete the following VIQ questionnaire: \n\n" +
                            "Version : " + dgvVIQ.CurrentRow.Cells["VERSION"].Value.ToString() + "\n" +
                            "Title : " + dgvVIQ.CurrentRow.Cells["TITLE"].Value.ToString() + "\n" +
                            "Type : " + dgvVIQ.CurrentRow.Cells["TYPE_CODE"].Value.ToString() + "\n\n" +
                            "Whould you like to proceed?\n\n" +
                            "Notes: just questionnqire information will be deleted. Reports will be unchanged.";

                        rslt = System.Windows.Forms.MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (rslt == DialogResult.Yes)
                        {
                            OleDbCommand cmd = new OleDbCommand("", connection);

                            OleDbTransaction transaction = connection.BeginTransaction();

                            cmd.Transaction = transaction;

                            try
                            {
                                cmd.CommandText =
                                    "delete from TEMPLATE_QUESTIONS \n" +
                                    "where TEMPLATE_GUID={" + dgvVIQ.CurrentRow.Cells["TEMPLATE_GUID"].Value.ToString() + "}";

                                if (MainForm.cmdExecute(cmd)<0)
                                {
                                    MessageBox.Show("Failed to delete TEMPLATE_QUESTIONS record", "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    transaction.Rollback();
                                    return;
                                }

                                cmd.CommandText =
                                    "delete from TEMPLATES \n" +
                                    "where TEMPLATE_GUID=" + MainForm.GuidToStr(MainForm.StrToGuid(dgvVIQ.CurrentRow.Cells["TEMPLATE_GUID"].Value.ToString()));

                                if (MainForm.cmdExecute(cmd)<0)
                                {
                                    MessageBox.Show("Failed to delete TEMPLATES record", "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    transaction.Rollback();
                                    return;
                                }

                                transaction.Commit();

                                _templateChanged = true;

                                updateVIQList();
                            }
                            catch (Exception E)
                            {
                                transaction.Rollback();

                                MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                    break;
                case 1:
                    msg = "You are going to delete the following OVIQ questionnaire: \n\n" +
                        "Version : " + dgvOVIQ.CurrentRow.Cells["VERSION"].Value.ToString() + "\n" +
                        "Title : " + dgvOVIQ.CurrentRow.Cells["TITLE"].Value.ToString() + "\n" +
                        "Type : " + dgvOVIQ.CurrentRow.Cells["TYPE_CODE"].Value.ToString() + "\n\n" +
                        "Whould you like to proceed?\n\n"+
                        "Notes: just questionnqire information will be deleted. Reports will be unchanged.";

                    rslt = System.Windows.Forms.MessageBox.Show(msg,"Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                    if (rslt==DialogResult.Yes)
                    {
                        OleDbCommand cmd=new OleDbCommand("",connection);

                        OleDbTransaction transaction = connection.BeginTransaction();

                        cmd.Transaction = transaction;

                        try
                        {
                            cmd.CommandText =
                                "delete from TEMPLATE_QUESTIONS \n" +
                                "where TEMPLATE_GUID={" + dgvOVIQ.CurrentRow.Cells["TEMPLATE_GUID"].Value.ToString() + "}";

                            if (MainForm.cmdExecute(cmd)<0)
                            {
                                MessageBox.Show("Failed to delete TEMPLATE_QUESTIONS record", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                                transaction.Rollback();
                                return;
                            }

                            cmd.CommandText =
                                "delete from TEMPLATES \n" +
                                "where TEMPLATE_GUID={" + dgvOVIQ.CurrentRow.Cells["TEMPLATE_GUID"].Value.ToString() + "}";

                            if (MainForm.cmdExecute(cmd)<0)
                            {
                                MessageBox.Show("Failed to delete TEMPLATES record", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                                transaction.Rollback();
                                return;
                            }

                            transaction.Commit();

                            _templateChanged = true;

                            updateOviqList();
                        }
                        catch (Exception E)
                        {
                            transaction.Rollback();

                            MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    break;
                case 2:
                    msg = "You are going to delete the following PSC questionnaire: \n\n" +
                        "Version : " + dgvPSC.CurrentRow.Cells["VERSION"].Value.ToString() + "\n" +
                        "Title : " + dgvPSC.CurrentRow.Cells["TITLE"].Value.ToString() + "\n" +
                        "Type : " + dgvPSC.CurrentRow.Cells["TYPE_CODE"].Value.ToString() + "\n\n" +
                        "Whould you like to proceed?\n\n"+
                        "Notes: just questionnqire information will be deleted. Reports will be unchanged.";

                    rslt = System.Windows.Forms.MessageBox.Show(msg,"Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                    if (rslt==DialogResult.Yes)
                    {
                        OleDbCommand cmd=new OleDbCommand("",connection);

                        OleDbTransaction transaction = connection.BeginTransaction();

                        cmd.Transaction = transaction;

                        try
                        {
                            cmd.CommandText =
                                "delete from TEMPLATE_QUESTIONS \n" +
                                "where TEMPLATE_GUID={" + dgvPSC.CurrentRow.Cells["TEMPLATE_GUID"].Value.ToString() + "}";

                            if (MainForm.cmdExecute(cmd)<0)
                            {
                                MessageBox.Show("Failed to delete TEMPLATE_QUESTIONS record", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                                transaction.Rollback();
                                return;
                            }

                            cmd.CommandText =
                                "delete from TEMPLATES \n" +
                                "where TEMPLATE_GUID={" + dgvPSC.CurrentRow.Cells["TEMPLATE_GUID"].Value.ToString() + "}";

                            if (MainForm.cmdExecute(cmd)<0)
                            {
                                MessageBox.Show("Failed to delete TEMPLATES record", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                                transaction.Rollback();
                                return;
                            }

                            transaction.Commit();

                            _templateChanged = true;

                            UpdatePSCList();
                        }
                        catch (Exception E)
                        {
                            transaction.Rollback();

                            MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    break;
                case 3:
                    msg = "You are going to delete the following CDI questionnaire: \n\n" +
                        "Version : " + dgvCDI.CurrentRow.Cells["VERSION"].Value.ToString() + "\n" +
                        "Title : " + dgvCDI.CurrentRow.Cells["TITLE"].Value.ToString() + "\n" +
                        "Type : " + dgvCDI.CurrentRow.Cells["TYPE_CODE"].Value.ToString() + "\n\n" +
                        "Whould you like to proceed?\n\n" +
                        "Notes: just questionnqire information will be deleted. Reports will be unchanged.";

                    rslt = System.Windows.Forms.MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (rslt == DialogResult.Yes)
                    {
                        OleDbCommand cmd = new OleDbCommand("", connection);

                        OleDbTransaction transaction = connection.BeginTransaction();

                        cmd.Transaction = transaction;

                        try
                        {
                            cmd.CommandText =
                                "delete from TEMPLATE_QUESTIONS \n" +
                                "where TEMPLATE_GUID={" + dgvCDI.CurrentRow.Cells["TEMPLATE_GUID"].Value.ToString() + "}";

                            if (MainForm.cmdExecute(cmd) < 0)
                            {
                                MessageBox.Show("Failed to delete TEMPLATE_QUESTIONS record", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                                transaction.Rollback();
                                return;
                            }

                            cmd.CommandText =
                                "delete from TEMPLATES \n" +
                                "where TEMPLATE_GUID={" + dgvCDI.CurrentRow.Cells["TEMPLATE_GUID"].Value.ToString() + "}";

                            if (MainForm.cmdExecute(cmd) < 0)
                            {
                                MessageBox.Show("Failed to delete TEMPLATES record", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                                transaction.Rollback();
                                return;
                            }

                            transaction.Commit();

                            _templateChanged = true;

                            UpdateCDIList();
                        }
                        catch (Exception E)
                        {
                            transaction.Rollback();

                            MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    break;
            }

        }

        private void updateVIQList()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);
            cmd.CommandText =
                "select VERSION, TITLE, TYPE_CODE, TEMPLATE_GUID, PUBLISHED \n" +
                "from TEMPLATES \n" +
                "where TEMPLATE_TYPE='VIQ' \n"+
                "order by VERSION DESC, TYPE_CODE";

            OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("QLIST"))
                DS.Tables["QLIST"].Clear();

            adpt.Fill(DS, "QLIST");

            dgvVIQ.DataSource = DS;
            dgvVIQ.AutoGenerateColumns = true;
            dgvVIQ.DataMember = "QLIST";

            dgvVIQ.Columns["VERSION"].HeaderText = "Version";
            dgvVIQ.Columns["VERSION"].FillWeight = 50;

            dgvVIQ.Columns["TITLE"].HeaderText = "Title";
            dgvVIQ.Columns["TITLE"].FillWeight = 70;

            dgvVIQ.Columns["TYPE_CODE"].HeaderText = "Type code";
            dgvVIQ.Columns["TYPE_CODE"].FillWeight = 30;

            dgvVIQ.Columns["PUBLISHED"].HeaderText = "Published";
            dgvVIQ.Columns["PUBLISHED"].FillWeight = 30;

            dgvVIQ.Columns["TEMPLATE_GUID"].HeaderText = "GUID";
            dgvVIQ.Columns["TEMPLATE_GUID"].FillWeight = 30;
            //dataGridView1.Columns["TEMPLATE_GUID"].Visible = false;
        }

        private void dgvVIQ_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            editQuestionnaire();
        }

        private void updateOviqList()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select VERSION, TITLE, TYPE_CODE, TEMPLATE_GUID, PUBLISHED \n" +
                "from TEMPLATES \n" +
                "where TEMPLATE_TYPE='OVIQ' \n"+
                "order by VERSION DESC, TYPE_CODE";

            OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("OVIQLIST"))
                DS.Tables["OVIQLIST"].Clear();

            adpt.Fill(DS, "OVIQLIST");

            dgvOVIQ.DataSource = DS;
            dgvOVIQ.AutoGenerateColumns = true;
            dgvOVIQ.DataMember = "OVIQLIST";

            dgvOVIQ.Columns["VERSION"].HeaderText = "Version";
            dgvOVIQ.Columns["VERSION"].FillWeight = 50;

            dgvOVIQ.Columns["TITLE"].HeaderText = "Title";
            dgvOVIQ.Columns["TITLE"].FillWeight = 70;

            dgvOVIQ.Columns["TYPE_CODE"].HeaderText = "Type code";
            dgvOVIQ.Columns["TYPE_CODE"].FillWeight = 30;

            dgvOVIQ.Columns["PUBLISHED"].HeaderText = "Published";
            dgvOVIQ.Columns["PUBLISHED"].FillWeight = 30;

            dgvOVIQ.Columns["TEMPLATE_GUID"].HeaderText = "GUID";
            dgvOVIQ.Columns["TEMPLATE_GUID"].FillWeight = 30;
        }

        private void dgvOVIQ_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            editQuestionnaire();
        }

        private void QuestionnairiesForm_Load(object sender, EventArgs e)
        {

        }

        private void UpdatePSCList()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select VERSION, TITLE, TYPE_CODE, TEMPLATE_GUID, PUBLISHED \n" +
                "from TEMPLATES \n" +
                "where TEMPLATE_TYPE='PSC' \n" +
                "order by VERSION DESC, TYPE_CODE";

            OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("PSC_LIST"))
                DS.Tables["PSC_LIST"].Clear();

            adpt.Fill(DS, "PSC_LIST");

            dgvPSC.DataSource = DS;
            dgvPSC.AutoGenerateColumns = true;
            dgvPSC.DataMember = "PSC_LIST";

            dgvPSC.Columns["VERSION"].HeaderText = "Version";
            dgvPSC.Columns["VERSION"].FillWeight = 50;

            dgvPSC.Columns["TITLE"].HeaderText = "Title";
            dgvPSC.Columns["TITLE"].FillWeight = 70;

            dgvPSC.Columns["TYPE_CODE"].HeaderText = "Type code";
            dgvPSC.Columns["TYPE_CODE"].FillWeight = 30;

            dgvPSC.Columns["PUBLISHED"].HeaderText = "Published";
            dgvPSC.Columns["PUBLISHED"].FillWeight = 30;

            dgvPSC.Columns["TEMPLATE_GUID"].HeaderText = "GUID";
            dgvPSC.Columns["TEMPLATE_GUID"].FillWeight = 30;
        }

        private void UpdateCDIList()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText =
                "select VERSION, TITLE, TYPE_CODE, TEMPLATE_GUID, PUBLISHED \n" +
                "from TEMPLATES \n" +
                "where TEMPLATE_TYPE='CDI' \n" +
                "order by VERSION DESC, TYPE_CODE";

            OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);

            if (DS.Tables.Contains("CDI_LIST"))
                DS.Tables["CDI_LIST"].Clear();

            adpt.Fill(DS, "CDI_LIST");

            dgvCDI.DataSource = DS;
            dgvCDI.AutoGenerateColumns = true;
            dgvCDI.DataMember = "CDI_LIST";

            dgvCDI.Columns["VERSION"].HeaderText = "Version";
            dgvCDI.Columns["VERSION"].FillWeight = 50;

            dgvCDI.Columns["TITLE"].HeaderText = "Title";
            dgvCDI.Columns["TITLE"].FillWeight = 70;

            dgvCDI.Columns["TYPE_CODE"].HeaderText = "Type code";
            dgvCDI.Columns["TYPE_CODE"].FillWeight = 30;

            dgvCDI.Columns["PUBLISHED"].HeaderText = "Published";
            dgvCDI.Columns["PUBLISHED"].FillWeight = 30;

            dgvCDI.Columns["TEMPLATE_GUID"].HeaderText = "GUID";
            dgvCDI.Columns["TEMPLATE_GUID"].FillWeight = 30;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

        }

        private void dgvPSC_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            editQuestionnaire();
        }

        private void dgvCDI_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            editQuestionnaire();
        }
    }
}
