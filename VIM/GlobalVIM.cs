using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Security.Principal;
using System.Drawing;
using System.IO;
using System.Security.AccessControl;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;

namespace VIM
{
    public static class GlobalVIM
    {
        public static string appName = "";
        public static string appType = "";
        public static string iniPersonalFile = "";
        public static string iniCommonFile = "";

        public static string workFolder;

        public static OleDbConnection gblConnection = new OleDbConnection();
        public static DataSet gblDS = new DataSet();

        public static bool anyUserMayChangeFont = false;
        public static bool oneFontForPowerUsers = true;

        public static Guid instanceGuid = Guid.NewGuid();
        public static Guid zeroGuid = new Guid();

        public static string betaString = "";
        public static WindowsIdentity user;
        public static List<string> MAPIClients = new List<string>();

        public static string userName = "";
        public static string appTempFolderName = "";
        public static string userTempFolder = "";
        public static string shortAppTempFolderName = "";
        public static string programID = "";
        public static bool isPowerUser = false;
        public static string appRegKey = "SOFTWARE\\NBK Software\\VIQ Manager";

        public static string serviceFolder = "";

        public static string templatesType = "";

        public static string appDataFolder = "";

        public static Font mainFont;
        public static Icon mainIcon;

        public static void AddDirectorySecurity(string folderName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(folderName);

            // Get a DirectorySecurity object that represents the 
            // current security settings.
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings. 
            dSecurity.AddAccessRule(new FileSystemAccessRule(Account,
                                                            Rights,
                                                            ControlType));

            // Set the new access settings.
            dInfo.SetAccessControl(dSecurity);

        }

        public static void RemoveDirectorySecurity(string FileName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(FileName);

            // Get a DirectorySecurity object that represents the 
            // current security settings.
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings. 
            dSecurity.RemoveAccessRule(new FileSystemAccessRule(Account,
                                                            Rights,
                                                            ControlType));

            // Set the new access settings.
            dInfo.SetAccessControl(dSecurity);

        }

        public static Guid GetVesselGuid(string vesselName, string vesselIMO)
        {
            OleDbCommand cmd = new OleDbCommand("", gblConnection);

            cmd.CommandText =
                "select VESSEL_GUID \n" +
                "from VESSELS \n" +
                "where \n" +
                "VESSEL_IMO='" + StrToSQLStr(vesselIMO) + "' \n" +
                "and UCASE(VESSEL_NAME)='" + StrToSQLStr(vesselName.ToUpper()) + "'";

            object id = cmd.ExecuteScalar();

            if (id != null)
                return MainForm.StrToGuid(id.ToString());
            else
                return zeroGuid;
        }

        public static string GetVesselNameForID(Guid vesselGuid)
        {
            if (vesselGuid == null)
                return "";

            OleDbCommand cmd = new OleDbCommand("", gblConnection);

            cmd.CommandText =
                "select VESSEL_NAME \n" +
                "from VESSELS \n" +
                "where \n" +
                "VESSEL_GUID=" + MainForm.GuidToStr(vesselGuid);

            object vesselName = cmd.ExecuteScalar();

            if (vesselName == null)
                return "";
            else
                return vesselName.ToString();
        }

        public static string GetVesselIMOForID(Guid vesselGuid)
        {
            if (vesselGuid == null)
                return "";

            OleDbCommand cmd = new OleDbCommand("", gblConnection);

            cmd.CommandText =
                "select VESSEL_IMO \n" +
                "from VESSELS \n" +
                "where \n" +
                "VESSEL_GUID=" + GuidToStr(vesselGuid);

            object vesselIMO = cmd.ExecuteScalar();

            if (vesselIMO == null)
                return "";
            else
                return vesselIMO.ToString();
        }

        public static string StrToSQLStr(string text, bool trim = false)
        {
            string s = "";

            if (text != null)
                s = text.Replace("'", "''");

            if (trim)
                return s.Trim();
            else
                return s;
        }

        public static Boolean tableExists(string tableName)
        {
            DataTable Tables = gblConnection.GetSchema("Tables");
            DataRow[] tableRows;

            tableRows = Tables.Select();


            foreach (DataRow row in Tables.Rows)
            {
                if (tableName.CompareTo(Convert.ToString(row.ItemArray[2])) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static string DateTimeToQueryStr(DateTime inDateTime)
        {

            string s;
            string Year;
            string Month;
            string Day;
            string Hour;
            string Min;
            string Sec;

            DateTime AMinimumDate = DateTime.Parse("1900-01-01");

            if (inDateTime > DateTimePicker.MinimumDateTime)
            {
                Year = Convert.ToString(inDateTime.Year);
                Month = Convert.ToString(inDateTime.Month);
                Day = Convert.ToString(inDateTime.Day);
                Hour = Convert.ToString(inDateTime.Hour);
                Min = Convert.ToString(inDateTime.Minute);
                Sec = Convert.ToString(inDateTime.Second);

                s = "DateSerial(" + Year + "," + Month + "," + Day + ")+TimeSerial(" + Hour + "," + Min + "," + Sec + ")";

                return s;
            }
            else
            {
                return "Null";
            }
        }

        public static int cmdExecute(OleDbCommand cmd)
        {
            int rslt = -1;

            if (gblConnection.State == ConnectionState.Closed)
                return rslt;

            try
            {
                rslt = cmd.ExecuteNonQuery();
                return rslt;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                MemoForm form = new MemoForm(cmd.CommandText);

                form.ShowDialog();

                return -1;
            }
        }

        public static bool IsNumericString(string aString)
        {
            if (aString.Length == 0)
                return false;

            string numbers = "1234567890";

            for (int i = 0; i < aString.Length; i++)
            {
                if (!numbers.Contains(aString[i].ToString()))
                    return false;
            }

            return true;
        }

        public static string GetOptionValue(int Tag)
        {
            OleDbCommand cmd = new OleDbCommand("", gblConnection);

            cmd.CommandText =
                "select STR_VALUE \n" +
                "from OPTIONS \n" +
                "where TAG=" + Tag.ToString();

            return (string)cmd.ExecuteScalar();
        }

        public static void SetOptionValue(int Tag, string Value)
        {
            OleDbCommand cmd = new OleDbCommand("", gblConnection);

            cmd.CommandText =
                "select Count(TAG) as TagCount \n" +
                "from Options \n" +
                "where TAG=" + Tag.ToString();

            int count = (int)cmd.ExecuteScalar();

            if (count == 0)
            {
                cmd.CommandText =
                    "insert into OPTIONS (TAG,STR_VALUE) \n" +
                    "values(" + Tag.ToString() + ",'" +
                    StrToSQLStr(Value) + "')";
                cmd.ExecuteNonQuery();
            }
            else
            {
                cmd.CommandText =
                    "update OPTIONS set \n" +
                    "STR_VALUE='" + StrToSQLStr(Value) + "' \n" +
                    "where TAG=" + Tag.ToString();
                cmd.ExecuteNonQuery();
            }
        }

        public static bool tempTableCreate(string tableName, string script)
        {
            //Check table exists

            if (tableExists(tableName))
                if (!tempTableDrop(tableName))
                    return false;

            OleDbCommand cmd = new OleDbCommand(script, gblConnection);

            try
            {
                if (cmdExecute(cmd) >= 0)
                {
                    tempTableAddLogRecord(tableName);

                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }

        }

        public static bool tempTableDrop(string tableName)
        {
            OleDbCommand cmd = new OleDbCommand("", gblConnection);
            cmd.CommandText =
                "drop table [" + tableName + "]";

            try
            {
                if (cmdExecute(cmd) >= 0)
                {
                    tempTableDeleteLogRecord(tableName);
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool tempTableAddLogRecord(string tableName)
        {
            OleDbCommand cmd = new OleDbCommand("", gblConnection);

            if (!tableExists("TEMP_TABLES"))
            {
                cmd.CommandText =
                    "create table TEMP_TABLES (ID counter Primary key,\n" +
                    "TABLE_NAME varchar(255), CREATED_ON DateTime, PROGRAM_ID varchar(50))";

                if (cmdExecute(cmd) < 0)
                    return false;
            }

            cmd.CommandText =
                "insert into TEMP_TABLES(TABLE_NAME,CREATED_ON,PROGRAM_ID) \n" +
                "values ('" + tableName + "'," +
                DateTimeToQueryStr(DateTime.Now) + ",'" +
                programID + "')";

            return cmdExecute(cmd) >= 0;

        }

        public static bool tempTableDeleteLogRecord(string tableName)
        {
            if (!tableExists("TEMP_TABLES"))
                return true;

            OleDbCommand cmd = new OleDbCommand("", gblConnection);

            cmd.CommandText =
                "delete from TEMP_TABLES \n" +
                "where TABLE_NAME='" + tableName + "' \n" +
                "and PROGRAM_ID='" + programID + "'";

            return cmdExecute(cmd) >= 0;
        }

        public static bool tempTablesClear()
        {
            if (!tableExists("TEMP_TABLES"))
                return true;

            OleDbCommand cmd = new OleDbCommand("", gblConnection);

            cmd.CommandText =
                "select * from TEMP_TABLES \n" +
                "where PROGRAM_ID='" + programID + "' \n" +
                "or CREATED_ON<" + DateTimeToQueryStr(DateTime.Now.AddDays(-3));

            DataSet tempDS = new DataSet();
            OleDbDataAdapter tempAdapter = new OleDbDataAdapter(cmd);

            try
            {
                tempAdapter.Fill(tempDS, "TEMP_TABLES");

                DataTable tempTables = tempDS.Tables["TEMP_TABLES"];

                foreach (DataRow row in tempTables.Rows)
                {
                    string tableName = row["TABLE_NAME"].ToString();

                    if (tableExists(tableName))
                    {
                        tempTableDrop(tableName);
                    }
                    else
                    {
                        tempTableDeleteLogRecord(tableName);
                    }
                }

                return true;
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
                return false;
            }


        }

        public static void LocateGridRecord(string searchString, string searchField, int colIndex, DataGridView dgv)
        {
            //Added on 30.09.2016 in version 2.0.15.1
            //Modified on 10.10.2016 in version 2.0.15.1
            //  Locate first visible column if colIndex<0

            String searchValue = searchString;
            int rowIndex = -1;

            int ind = 0;

            if (colIndex < 0)
            {
                //Locate first visible column
                int colInd = 0;

                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    if (dgv.Columns[i].Visible)
                    {
                        colInd = i;
                        break;
                    }
                }

                ind = colInd;
            }
            else
                ind = colIndex;

            if (!dgv.Columns.Contains(searchField)) return;

            foreach (DataGridViewRow dgRow in dgv.Rows)
            {
                if (dgRow.Cells[searchField].Value.ToString().Equals(searchValue))
                {
                    rowIndex = dgRow.Index;
                    break;
                }
            }

            if (rowIndex >= 0)
            {
                try
                {
                    dgv.CurrentCell = dgv[ind, rowIndex];
                }
                catch
                {

                }
            }
        }

        public static void LocateAdvGridRecord(string searchString, string searchField, int colIndex, ADGV.AdvancedDataGridView dgv)
        {
            //Added on 30.09.2016 in version 2.0.15.1
            //Modified on 10.10.2016 in version 2.0.15.1
            //  Locate first visible column if colIndex<0

            String searchValue = searchString;
            int rowIndex = -1;

            int ind = 0;

            if (colIndex < 0)
            {
                //Locate first visible column
                int colInd = 0;

                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    if (dgv.Columns[i].Visible)
                    {
                        colInd = i;
                        break;
                    }
                }

                ind = colInd;
            }
            else
                ind = colIndex;

            if (!dgv.Columns.Contains(searchField)) return;

            foreach (DataGridViewRow dgRow in dgv.Rows)
            {
                if (dgRow.Cells[searchField].Value.ToString().Equals(searchValue))
                {
                    rowIndex = dgRow.Index;
                    break;
                }
            }

            if (rowIndex >= 0)
            {
                try
                {
                    dgv.CurrentCell = dgv[ind, rowIndex];
                }
                catch
                {

                }
            }
        }

        public static string BoolToIntStr(bool value)
        {
            if (value)
                return "1";
            else
                return "0";
        }

        public static string TextToLine(string text)
        {
            string line = text;

            line = line.Replace("\t", "%tb");
            line = line.Replace("\n", "%nl");
            line = line.Replace("\r", "%lf");

            return line;
        }

        public static string LineToText(string line)
        {
            string text = line;

            text = text.Replace("%tb", "\t");
            text = text.Replace("%nl", "\n");
            text = text.Replace("%lf", "\r");

            return text;
        }

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        public static object ReadRegValue(string key)
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(appRegKey);

            object regValue = null;

            try
            {
                regValue = regKey.GetValue(key);
            }
            catch
            {
                //do nothing
            }

            return regValue;
        }

        public static bool WriteRegValue(string key, string name, object value)
        {
            string activeKey = appRegKey;

            if (key.Trim().Length > 0)
                activeKey = activeKey + "\\" + key;

            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(activeKey);

            if (regKey == null)
                regKey = Registry.CurrentUser.CreateSubKey(activeKey);

            try
            {
                regKey.SetValue(name, value);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void SetAppDataFolder()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

            if (!Directory.Exists(appData))
            {
                try
                {
                    Directory.CreateDirectory("C:\\ProgramData");
                    appData = "C:\\ProgramData";
                }
                catch
                {
                    appData = "C:\\";
                }
            }

            string curAppData = Path.Combine(appData, appName);

            if (!Directory.Exists(curAppData))
            {
                Directory.CreateDirectory(curAppData);
            }

            appDataFolder = curAppData;
        }

        public static int ExecuteCommand(string command)
        {
            int ExitCode;
            ProcessStartInfo ProcessInfo;
            Process process;

            ProcessInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.UseShellExecute = false;
            //ProcessInfo.WorkingDirectory = Application.StartupPath + "\\txtmanipulator";
            // *** Redirect the output ***
            ProcessInfo.RedirectStandardError = true;
            ProcessInfo.RedirectStandardOutput = true;

            process = Process.Start(ProcessInfo);
            process.WaitForExit();

            // *** Read the streams ***
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            ExitCode = process.ExitCode;

            process.Close();

            return ExitCode;
        }

        public static void GetMAPIClients()
        {
            MAPIClients.Clear();
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Clients\\Mail");

            string[] clients = regKey.GetSubKeyNames();

            for (int i = 0; i < clients.GetLength(0); i++)
            {
                MAPIClients.Insert(i, clients[i]);
            }
        }

        public static int GetMailClientID()
        {
            IniFile iniFile = new IniFile(iniPersonalFile);

            string section = "Mail client";

            return iniFile.ReadInteger(section, "MailClientID", 0);
        }

        public static void SetMailClientID(int mailClientID)
        {
            IniFile iniFile = new IniFile(iniPersonalFile);

            string section = "Mail client";

            int id = 0;

            id = mailClientID;

            iniFile.Write(section, "MailClientID", id);
        }

        public static string StrToIniStr(string text)
        {
            string _text = text;

            _text = _text.Replace("\n", "&nl");
            _text = _text.Replace("\r", "&lf");
            _text = _text.Replace("\t", "&tb");
            _text = _text.Replace("[", "&bo");
            _text = _text.Replace("]", "&bc");
            _text = _text.Replace("=", "&eq");

            return _text;
        }

        public static string IniStrToStr(string text)
        {
            string _text = text;

            _text = _text.Replace("&nl", "\n");
            _text = _text.Replace("&lf", "\r");
            _text = _text.Replace("&tb", "\t");
            _text = _text.Replace("&bo", "[");
            _text = _text.Replace("&bc", "]");
            _text = _text.Replace("&eq", "=");

            if (_text.Contains("\n") && !_text.Contains("\r"))
                _text = _text.Replace("\n", "\r\n");

            return _text;
        }

        public static string GetApplicationType()
        {
            //Read settings from ini file
            IniFile iniFile = new IniFile(iniCommonFile);

            //Use global section for bulletin settings
            string section = "General";

            string typeString = iniFile.ReadString(section, "Type", "");

            return typeString;
        }

        public static void SetApplicationType(string type)
        {
            //Write settings to ini file

            IniFile iniFile = new IniFile(iniCommonFile);

            //Use global section for bulletin settings
            string section = "General";

            iniFile.Write(section, "Type", type);
        }

        public static List<string> ParseStringToList(string inString, string delimiters)
        {
            List<string> list = new List<string>();
            string s = "";

            for (int i = 0; i < inString.Length; i++)
            {
                if (delimiters.Contains(inString[i].ToString()))
                {
                    list.Add(s.Trim());
                    s = "";
                }
                else
                {
                    s = s + inString[i].ToString();
                }
            }

            list.Add(s.Trim());

            return list;
        }

        public static string ReadIniValue(string iniFileName, string iniSection, string iniName, string defValue = "")
        {
            //Read settings from ini file
            IniFile iniFile = new IniFile(iniFileName);

            //Use global section for bulletin settings
            string section = iniSection;

            string iniString = iniFile.ReadString(section, iniName, defValue);

            return iniString;

        }

        public static bool WriteIniValue(string iniFileName, string iniSection, string iniName, string iniValue)
        {
            //Write settings to ini file

            IniFile iniFile = new IniFile(iniFileName);

            //Use global section for bulletin settings
            string section = iniSection;

            return iniFile.Write(section, iniName, iniValue);

        }

        public static bool StrToBool(string anyString)
        {
            return anyString.StartsWith("True", StringComparison.OrdinalIgnoreCase);
        }

        public static string BoolToStr(bool anyValue)
        {
            if (anyValue)
                return "True";
            else
                return "False";
        }

        public static void UpdateVesselGuid4CrewOnBoard()
        {
            OleDbCommand cmd = new OleDbCommand("", gblConnection);

            cmd.CommandText =
                        "update CREW_ON_BOARD left join VESSELS \n" +
                        "on CREW_ON_BOARD.VESSEL_NAME=VESSELS.VESSEL_NAME \n" +
                        "set CREW_ON_BOARD.VESSEL_GUID=VESSELS.VESSEL_GUID";

            MainForm.cmdExecute(cmd);
        }

        public static string GetFleetEmail(Guid vesselGuid)
        {
            string fleetEmail = "";

            if (vesselGuid == null || vesselGuid == zeroGuid)
                return fleetEmail;


            OleDbCommand cmd = new OleDbCommand("", gblConnection);

            cmd.CommandText =
                "select top 1 FLEET \n" +
                "from VESSELS \n" +
                "where \n" +
                "VESSEL_GUID=" + GuidToStr(vesselGuid);

            string fleet = (string)cmd.ExecuteScalar();

            if (fleet.Length > 0)
            {

                cmd.CommandText =
                    "select FLEET_EMAIL \n" +
                    "from FLEET_EMAILS \n" +
                    "where FLEET_ID='" + MainForm.StrToSQLStr(fleet) + "' \n" +
                    "order by FLEET_EMAIL";

                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (fleetEmail.Length == 0)
                            fleetEmail = reader["FLEET_EMAIL"].ToString();
                        else
                            fleetEmail = fleetEmail + "; " + reader["FLEET_EMAIL"].ToString();
                    }
                }

                reader.Close();

            }

            return fleetEmail;
        }

        public static string FormatGuidString(string guidStr)
        {
            if (guidStr.Length == 0)
                return "Null";

            if (!guidStr.StartsWith("{"))
                return "{" + guidStr.ToUpper() + "}";
            else
                return guidStr.ToUpper();
        }

        public static string GuidToStr(Guid guid, bool UseNull = false)
        {
            Guid zGuid = zeroGuid;

            if (guid != null)
            {
                zGuid = guid;
            }

            string str = zGuid.ToString();

            if (!str.StartsWith("{"))
                str = "{" + str + "}";

            if (zGuid == zeroGuid && UseNull)
                str = "Null";

            return str;
        }

        public static bool ClearTable(string tableName, string where = "")
        {
            OleDbCommand cmd = new OleDbCommand("", gblConnection);

            string _where = "";

            if (where.Length > 0)
            {
                if (where.StartsWith("where"))
                    _where = where;
                else
                    _where = "where " + where;
            }

            cmd.CommandText =
                "delete from [" + tableName + "] \n" +
                where;

            return cmdExecute(cmd) >= 0;
        }

        public static string BuildInspectorScoring(Guid inspectorGuid)
        {
            string tempTableName = "$SCORING_" + MainForm.programID;

            tempTableName = tempTableName.Replace("{", "");
            tempTableName = tempTableName.Replace("}", "");
            tempTableName = tempTableName.Replace("-", "");

            string script =
                "create table [" + tempTableName + "] \n" +
                "(ID counter Primary key, CHAPTER varchar(50), OBS_COUNT varchar(10))";

            if (!tempTableCreate(tempTableName, script))
                return "";

            OleDbCommand cmd = new OleDbCommand("", gblConnection);

            for (int i = 1; i < 14; i++)
            {
                cmd.CommandText =
                    "select iif(IsNull(Count(QUESTION_NUMBER)),0,Count(QUESTION_NUMBER)) AS QCOUNT \n" +
                    "from REPORT_ITEMS inner join  \n" +
                    "(select REPORT_CODE as RC \n" +
                    "from REPORTS \n" +
                    "where INSPECTOR_GUID=" + MainForm.GuidToStr(inspectorGuid) + ") as INSP_REPORTS \n" +
                    "on REPORT_ITEMS.REPORT_CODE=INSP_REPORTS.RC \n" +
                    "where IIF(IsNull(QUESTION_NUMBER) or Len(QUESTION_NUMBER)=0" +
                    ",False,LEFT(QUESTION_NUMBER,INSTR(QUESTION_NUMBER,'.')-1)='" + i.ToString() + "')  \n" +
                    "and LEN(OBSERVATION)>0";

                int obs = (int)cmd.ExecuteScalar();

                string count = "0";

                if (obs > 0)
                    count = obs.ToString();


                cmd.CommandText =
                    "insert into [" + tempTableName + "] (CHAPTER,OBS_COUNT) \n" +
                    "values('" + i.ToString() + "','" + count + "')";
                MainForm.cmdExecute(cmd);
            }

            return tempTableName;
        }

        public static Guid StrToGuid(string strGuid)
        {
            Guid gValue = zeroGuid;

            if (strGuid != null)
            {
                try
                {
                    gValue = Guid.Parse(strGuid);
                }
                catch
                {
                    //do nothing
                }
            }

            return gValue;
        }

        public static string GetPositionName(Guid positionGuid)
        {
            OleDbCommand cmd = new OleDbCommand("", gblConnection);

            cmd.CommandText =
                "select TOP 1 POSITION_NAME \n" +
                "from CREW_POSITIONS \n" +
                "where CREW_POSITION_GUID";

            object pn = cmd.ExecuteScalar();

            if (pn != null)
                return pn.ToString();
            else
                return "";
        }

        public static Guid GetPositionGuid(string positionName)
        {
            OleDbCommand cmd = new OleDbCommand("", gblConnection);

            cmd.CommandText =
                "select TOP 1 CREW_POSITION_GUID \n" +
                "from CREW_POSITIONS \n" +
                "where POSITION_NAME='" + StrToIniStr(positionName) + "'";

            object pg = cmd.ExecuteScalar();

            if (pg != null)
            {
                return StrToGuid(pg.ToString());
            }
            else
                return zeroGuid;
        }

        public static int GetReportsCount()
        {
            OleDbCommand cmd = new OleDbCommand("", gblConnection);

            cmd.CommandText =
                "select Count(REPORT_CODE) as RecCount \n" +
                "from REPORTS";

            int recs = (int)cmd.ExecuteScalar();

            return recs;
        }

        public static Guid GetCrewGuid(string crewName, Guid positionGuid)
        {
            if (crewName.Trim().Length == 0)
                return MainForm.zeroGuid;

            if (positionGuid == MainForm.zeroGuid)
                return MainForm.zeroGuid;

            OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

            cmd.CommandText =
                "select Count(CREW_GUID) as ReCount \n" +
                "from CREW \n" +
                "where \n" +
                "CREW_NAME='" + MainForm.StrToSQLStr(crewName) + "' \n" +
                "and CREW_POSITION_GUID=" + MainForm.GuidToStr(positionGuid);

            int recCount = (int)cmd.ExecuteScalar();

            switch (recCount)
            {
                case 0:
                    return SaveNewCrew(crewName, positionGuid);
                case 1:
                    cmd.CommandText =
                        "select CREW_GUID \n" +
                        "from CREW \n" +
                        "where \n" +
                        "CREW_NAME='" + MainForm.StrToSQLStr(crewName) + "' \n" +
                        "and CREW_POSITION_GUID=" + MainForm.GuidToStr(positionGuid);

                    return (Guid)cmd.ExecuteScalar();
                default:
                    FormSelectCrewmemberFromList form = new FormSelectCrewmemberFromList();
                    form.crewmemberName = crewName;
                    form.positionGuid = positionGuid;

                    if (form.ShowDialog() == DialogResult.OK)
                        return form.crewGuid;
                    else
                        return MainForm.zeroGuid;
            }
        }

        public static Guid SaveNewCrew(string crewName, Guid positionGuid)
        {
            if (crewName.Trim().Length == 0)
                return MainForm.zeroGuid;

            OleDbCommand cmd = new OleDbCommand("", gblConnection);

            Guid crewGuid = Guid.NewGuid();

            cmd.CommandText =
                "insert into CREW (CREW_GUID,CREW_NAME,CREW_POSITION_GUID)\n" +
                "values(" + MainForm.GuidToStr(crewGuid) + ",'" +
                StrToSQLStr(crewName) + "'," +
                MainForm.GuidToStr(positionGuid) + ")";

            if (MainForm.cmdExecute(cmd) < 0)
                return MainForm.zeroGuid;
            else
                return crewGuid;

        }

        public static Guid GetTeplateTypeGuid(string TemplateTypeStr)
        {
            OleDbCommand cmd = new OleDbCommand("", gblConnection);

            cmd.CommandText =
                "select TEMPLATE_TYPE_GUID \n" +
                "from TEMPLATE_TYPES \n" +
                "where TEMPLATE_TYPE='" + StrToSQLStr(TemplateTypeStr) + "'";

            object rslt = cmd.ExecuteScalar();

            if (rslt != null)
                return (Guid)rslt;
            else
                return zeroGuid;
        }

        public static string StrToSingleLine(string anyString)
        {
            string rsltString = anyString.Trim();

            rsltString = rsltString.Replace("\r", " ");
            rsltString = rsltString.Replace("\n", " ");

            rsltString = rsltString.Trim();

            int strLen = 0;

            while (strLen != rsltString.Length)
            {
                strLen = rsltString.Length;
                rsltString = rsltString.Replace("  ", " ");
            }

            return rsltString;
        }

    }
}
