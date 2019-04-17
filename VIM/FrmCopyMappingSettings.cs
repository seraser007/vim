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
    public partial class FrmCopyMappingSettings : Form
    {
        public string qVersion
        {
            set { qv = value; }
        }


        public string templateGUID
        {
            get;
            set;
        }

        public string excludeChapters = "";

        public string defaultMapping = "";

        public string defaultFormCaption = "";

        public string defaultMappingLable = "";

        public bool copyMapping = true;

        public string masterSlaveGUID;

        private OleDbConnection connection;
        private DataSet DS;
        private string qv;
        private List<string> masterSlaveList= new List<string>();
        
        public FrmCopyMappingSettings()
        {
            this.Icon = MainForm.mainIcon;
            this.Font = MainForm.mainFont;

            InitializeComponent();

            connection = MainForm.connection;
            DS = MainForm.DS;
        }

        private void FillMappingList()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText=
                "select DISTINCT \n"+
                "TEMP1.TEMPLATE_GUID as MASTER_GUID, \n"+
                "TEMP1.TITLE as MASTER_TITLE, \n"+
                "TEMP1.VERSION as MASTER_VERSION, \n"+
                "TEMP2.TEMPLATE_GUID as SLAVE_GUID, \n"+
                "TEMP2.TITLE as SLAVE_TITLE, \n"+
                "TEMP2.VERSION as SLAVE_VERSION \n"+
                "from \n"+
                "(((QUESTIONS_MAPPING left join TEMPLATE_QUESTIONS as TQ1 \n"+
                "on QUESTIONS_MAPPING.QUESTION_MASTER_GUID=TQ1.QUESTION_GUID) \n"+
                "left join TEMPLATES as TEMP1 \n"+
                "on TQ1.TEMPLATE_GUID=TEMP1.TEMPLATE_GUID) \n"+
                "left join TEMPLATE_QUESTIONS as TQ2 \n"+
                "on QUESTIONS_MAPPING.QUESTION_SLAVE_GUID=TQ2.QUESTION_GUID) \n"+
                "left join TEMPLATES as TEMP2 \n"+
                "on TQ2.TEMPLATE_GUID=TEMP2.TEMPLATE_GUID \n"+
                "where \n" +
                "TEMP1.VERSION='"+qv+"' \n"+
                "order by TEMP1.TITLE DESC";

            OleDbDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string s = reader["MASTER_TITLE"].ToString() + " (" + reader["MASTER_VERSION"].ToString() + ") <-> " +
                        reader["SLAVE_TITLE"].ToString() + " (" + reader["SLAVE_VERSION"].ToString()+")";

                    cbMapping.Items.Add(s);

                    s = reader["MASTER_GUID"].ToString() + ";" + reader["SLAVE_GUID"].ToString();

                    masterSlaveList.Add(s);
                }
            }

            reader.Close();
        }

        private void FillChapterList()
        {
            OleDbCommand cmd = new OleDbCommand("", connection);

            cmd.CommandText=
                "SELECT \n"+
                "TEMPLATE_QUESTIONS.TEMPLATE_GUID, \n"+
                "CInt(CHAPTER_NUMBER) as CHAPTER_NUMBER, \n"+
                "CHAPTER_NAME \n"+
                "FROM  \n"+
                "[TEMPLATE_QUESTIONS] inner join TEMPLATES \n"+
                "on TEMPLATE_QUESTIONS.TEMPLATE_GUID=TEMPLATES.TEMPLATE_GUID \n"+
                "where \n"+
                "TEMPLATES.TEMPLATE_GUID='"+templateGUID+"' \n"+
                "group by TEMPLATE_QUESTIONS.TEMPLATE_GUID, CInt(CHAPTER_NUMBER), CHAPTER_NAME";

            chlbChapters.Items.Clear();

            OleDbDataReader reader=cmd.ExecuteReader();

            List<string> listExcludeChapters = MainForm.ParseStringToList(excludeChapters, ",");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int chapterNumber=Convert.ToInt32(reader["CHAPTER_NUMBER"].ToString());
                    string chapter=reader["CHAPTER_NUMBER"].ToString()+". "+
                        reader["CHAPTER_NAME"].ToString();

                    if (listExcludeChapters.IndexOf(chapterNumber.ToString())>=0)
                        chlbChapters.Items.Add(chapter, false);
                    else
                        chlbChapters.Items.Add(chapter, true);

                }
            }

            reader.Close();
        }

        private void FrmCopyMappingSettings_Load(object sender, EventArgs e)
        {
            if (defaultFormCaption.Length > 0)
                Text = defaultFormCaption;
            
            if (copyMapping)
                FillMappingList();
            else
            {
                cbMapping.DropDownStyle = ComboBoxStyle.Simple;
            }

            if (defaultMapping.Length > 0)
                cbMapping.Text = defaultMapping;

            if (defaultMappingLable.Length > 0)
                lblCopyMapping.Text = defaultMappingLable;

            FillChapterList();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            excludeChapters = "";

            for (int i=0;i<chlbChapters.Items.Count;i++)
            {
                if (chlbChapters.GetItemCheckState(i)==CheckState.Unchecked)
                {
                    if (excludeChapters.Length == 0)
                        excludeChapters = Convert.ToString(i + 1);
                    else
                        excludeChapters = excludeChapters + "," + Convert.ToString(i + 1);
                }
            }

            if (cbMapping.SelectedIndex>=0)
                masterSlaveGUID = masterSlaveList[cbMapping.SelectedIndex];
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i=0; i<chlbChapters.Items.Count; i++)
            {
                chlbChapters.SetItemChecked(i, true);
            }
        }

        private void btnResetAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chlbChapters.Items.Count; i++)
            {
                chlbChapters.SetItemChecked(i, false);
            }
        }
    }
}
