using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;
using Microsoft.Exchange.WebServices.Data;
using System.Data.OleDb;

namespace VIM
{
    public partial class FrmMail : Form
    {
        public string msgToAddress
        {
            set { tbeToAddress.Text = value; }
        }

        public string msgCcAddress
        {
            set { tbCopy.Text = value; }
        }

        public string msgBccAddress
        {
            set { tbBlindCopy.Text = value; }
        }

        public string msgSubject
        {
            set { tbSubject.Text = value; }
        }
        
        public string msgText
        {
            set { tbMessage.Text = value; }
        }


        private struct MailSMTP
        {
            public string SenderName;
            public string EmailAddress;
            public string SMTPServer;
            public int Port;
            public string UserName;
            public string ConnectionSecurity;
            public string AuthenticationMathod;
            public string Password;
        }

        private string ServerType = "";

        private struct MailExchange
        {
            public string User;
            public string Password;
            public string ServerURL;
            public string Domain;
            public string Email; 
        }

        private MailSMTP mailSMTP = new MailSMTP();
        private MailExchange mailExchange = new MailExchange();
        private List<string> attachments=new List<string>();

        List<string> emails = new List<string>();

        public FrmMail()
        {
            this.Font = MainForm.mainFont;
            this.Icon = MainForm.mainIcon;
            
            InitializeComponent();
            
            FillAddresses();

            ServerType = GetServerType();

            if (ServerType == "SMTP")
            {
                GetSMTP();

                if (mailSMTP.SenderName.Length == 0)
                    tbFrom.Text = mailSMTP.EmailAddress;
                else
                {
                    if (mailSMTP.EmailAddress.Length == 0)
                        tbFrom.Clear();
                    else
                        tbFrom.Text = mailSMTP.SenderName + "<" + mailSMTP.EmailAddress + ">";
                }
            }
            else
            {
                GetExchange();

                if (mailExchange.Email.Length == 0)
                    tbFrom.Text = mailExchange.User;
                else
                    tbFrom.Text = mailExchange.Email;
            }

            GetSelfcopy();
        }

        public void AddAttachment(string fileName)
        {
            attachments.Add(fileName);

            ListViewItem item=new ListViewItem(fileName);
            item.Text=Path.GetFileName(fileName);
            lvAttachments.Items.Add(fileName);
        }

        private void GetExchange()
        {
            string fileName = Path.Combine(MainForm.serviceFolder, "Settings_" + MainForm.userName + ".ini");
            IniFile iniFile = new IniFile(fileName);

            string section = "Exchange";

            mailExchange.User = iniFile.ReadString(section, "User", "");
            
            string s = iniFile.ReadString(section, "Password", "");

            if (s.Length == 0)
                mailExchange.Password = s;
            else
            {
                StringEncryption enc = new StringEncryption();
                mailExchange.Password = enc.Decrypt(s);
            }

            mailExchange.ServerURL = iniFile.ReadString(section, "ServerURL", "");
            mailExchange.Domain = iniFile.ReadString(section, "Domain", "");
            mailExchange.Email = iniFile.ReadString(section, "Email", "");
        }

        private void GetSMTP()
        {
            string fileName = Path.Combine(MainForm.serviceFolder, "Settings_" + MainForm.userName + ".ini");
            IniFile iniFile = new IniFile(fileName);

            string section = "SMTP";

            mailSMTP.SenderName = iniFile.ReadString(section, "Sender", "");
            mailSMTP.EmailAddress = iniFile.ReadString(section, "Address", "");
            mailSMTP.SMTPServer = iniFile.ReadString(section, "Server", "");
            mailSMTP.Port = iniFile.ReadInteger(section, "Port", 25);
            mailSMTP.UserName = iniFile.ReadString(section, "User", "");
            mailSMTP.ConnectionSecurity = iniFile.ReadString(section, "Security", "None");
            mailSMTP.AuthenticationMathod = iniFile.ReadString(section, "Authentication", "No authentication");

            string s = iniFile.ReadString(section, "Password", "");

            if (s.Length == 0)
                mailSMTP.Password = s;
            else
            {
                StringEncryption enc = new StringEncryption();
                mailSMTP.Password = enc.Decrypt(s);
            }
        }

        private void SaveExchange()
        {
            string fileName = Path.Combine(MainForm.serviceFolder, "Settings_" + MainForm.userName + ".ini");
            IniFile iniFile = new IniFile(fileName);

            string section = "Exchange";

            iniFile.Write(section, "User", mailExchange.User);

            if (mailExchange.Password.Length == 0)
                iniFile.Write(section, "Password", "");
            else
            {
                StringEncryption enc = new StringEncryption();

                iniFile.Write(section, "Password", enc.Encrypt(mailExchange.Password));
            }

            iniFile.Write(section, "ServerURL", mailExchange.ServerURL);
            iniFile.Write(section, "Domain", mailExchange.Domain);
            iniFile.Write(section, "Email", mailExchange.Email);
        }

        private void SaveSMTP()
        {
            string fileName = Path.Combine(MainForm.serviceFolder, "Settings_" + MainForm.userName + ".ini");
            IniFile iniFile = new IniFile(fileName);

            string section = "SMTP";

            iniFile.Write(section, "Sender", mailSMTP.SenderName);
            iniFile.Write(section, "Address", mailSMTP.EmailAddress);
            iniFile.Write(section, "Server", mailSMTP.SMTPServer);
            iniFile.Write(section, "Port", mailSMTP.Port);
            iniFile.Write(section, "User", mailSMTP.UserName);
            iniFile.Write(section, "Security", mailSMTP.ConnectionSecurity);
            iniFile.Write(section, "Authentication", mailSMTP.AuthenticationMathod);

            if (mailSMTP.Password.Length == 0)
                iniFile.Write(section, "Password", "");
            else
            {
                StringEncryption enc = new StringEncryption();

                iniFile.Write(section, "Password", enc.Encrypt(mailSMTP.Password));
            }
        }

        private void SaveServerType()
        {
            string fileName = Path.Combine(MainForm.serviceFolder, "Settings_" + MainForm.userName + ".ini");
            IniFile iniFile = new IniFile(fileName);

            string section = "Mail client";

            iniFile.Write(section, "ServerType", ServerType);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            FrmSMTPSettings form = new FrmSMTPSettings();

            form.serverType = ServerType;

            
            form.sender = mailSMTP.SenderName;
            form.address = mailSMTP.EmailAddress;
            form.server = mailSMTP.SMTPServer;
            form.port = mailSMTP.Port;
            form.user = mailSMTP.UserName;
            form.security = mailSMTP.ConnectionSecurity;
            form.authentication = mailSMTP.AuthenticationMathod;
            form.password = mailSMTP.Password;

            form.exUser = mailExchange.User;
            form.exPassword = mailExchange.Password;
            form.exServer = mailExchange.ServerURL;
            form.exDomain = mailExchange.Domain;
            form.exEmail = mailExchange.Email;

            if (form.ShowDialog()==DialogResult.OK)
            {
                ServerType = form.serverType;

                SaveServerType();

                if (ServerType == "SMTP")
                {
                    bool needSave = false;
                    bool fromChanged = false;

                    if (mailSMTP.SenderName != form.sender)
                    {
                        mailSMTP.SenderName = form.sender;
                        needSave = true;
                        fromChanged = true;
                    }

                    if (mailSMTP.EmailAddress != form.address)
                    {
                        mailSMTP.EmailAddress = form.address;
                        needSave = true;
                        fromChanged = true;
                    }

                    if (mailSMTP.SMTPServer != form.server)
                    {
                        mailSMTP.SMTPServer = form.server;
                        needSave = true;
                    }

                    if (mailSMTP.Port != form.port)
                    {
                        mailSMTP.Port = form.port;
                        needSave = true;
                    }

                    if (mailSMTP.UserName != form.user)
                    {
                        mailSMTP.UserName = form.user;
                        needSave = true;
                    }

                    if (mailSMTP.ConnectionSecurity != form.security)
                    {
                        mailSMTP.ConnectionSecurity = form.security;
                        needSave = true;
                    }


                    if (mailSMTP.AuthenticationMathod != form.authentication)
                    {
                        mailSMTP.AuthenticationMathod = form.authentication;
                        needSave = true;
                    }

                    if (mailSMTP.Password != form.password)
                    {
                        mailSMTP.Password = form.password;
                        needSave = true;
                    }

                    if (needSave)
                        SaveSMTP();

                    if (fromChanged)
                    {
                        if (mailSMTP.SenderName.Length == 0)
                            tbFrom.Text = mailSMTP.EmailAddress;
                        else
                        {
                            if (mailSMTP.EmailAddress.Length == 0)
                                tbFrom.Clear();
                            else
                                tbFrom.Text = mailSMTP.SenderName + "<" + mailSMTP.EmailAddress + ">";
                        }
                    }
                }
                else
                {
                    mailExchange.User = form.exUser;
                    mailExchange.Password = form.exPassword;
                    mailExchange.ServerURL = form.exServer;
                    mailExchange.Domain = form.exDomain;
                    mailExchange.Email = form.exEmail;

                    SaveExchange();

                    if (mailExchange.Email.Length == 0)
                        tbFrom.Text = form.exUser;
                    else
                        tbFrom.Text = mailExchange.Email;
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //Send message
            if (tbeToAddress.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please provide address of recipient", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (tbFrom.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please provide address of sender", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ServerType == "SMTP")
            {
                if (mailSMTP.EmailAddress.Length == 0 || mailSMTP.SMTPServer.Length == 0 || mailSMTP.Port == 0)
                {
                    MessageBox.Show("Please provide correct setting for SMTP server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(tbFrom.Text);

                    List<string> toList = MainForm.ParseStringToList(tbeToAddress.Text, ",;");

                    for (int i = 0; i < toList.Count; i++)
                    {
                        mail.To.Add(new MailAddress(toList[i]));
                    }

                    if (tbCopy.Text.Trim().Length > 0)
                    {
                        List<string> ccList = MainForm.ParseStringToList(tbCopy.Text.Trim(), ",;");

                        for (int i = 0; i < ccList.Count; i++)
                        {
                            mail.CC.Add(new MailAddress(ccList[i]));
                        }
                    }

                    if (tbBlindCopy.Text.Trim().Length > 0)
                    {
                        List<string> bccList = MainForm.ParseStringToList(tbBlindCopy.Text.Trim(), ",;");

                        for (int i = 0; i < bccList.Count; i++)
                        {
                            mail.Bcc.Add(new MailAddress(bccList[i]));
                        }
                    }

                    if (chbSelfCopy.Checked)
                        mail.Bcc.Add(new MailAddress(tbFrom.Text));

                    mail.Subject = tbSubject.Text;
                    mail.Body = tbMessage.Text;
                    mail.IsBodyHtml = false;

                    if (lvAttachments.Items.Count > 0)
                    {
                        for (int i = 0; i < lvAttachments.Items.Count; i++)
                        {
                            mail.Attachments.Add(new System.Net.Mail.Attachment(lvAttachments.Items[i].Text));
                        }
                    }

                    SmtpClient client = new SmtpClient();
                    client.Host = mailSMTP.SMTPServer;
                    client.Port = mailSMTP.Port;

                    if (mailSMTP.ConnectionSecurity == "SSL/TLS" || mailSMTP.ConnectionSecurity == "STARTTLS")
                        client.EnableSsl = true;

                    if (mailSMTP.AuthenticationMathod == "Normal password")
                    {
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential(mailSMTP.UserName, mailSMTP.Password);
                    }
                    else
                    {
                        client.UseDefaultCredentials = true;
                    }

                    client.DeliveryMethod = SmtpDeliveryMethod.Network;

                    this.Cursor = Cursors.WaitCursor;

                    client.Send(mail);

                    this.Cursor = Cursors.Default;

                    mail.Dispose();

                    MessageBox.Show("Message was sent successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception E)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Mail.Send: " + E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                if (mailExchange.User.Length==0 || (mailExchange.ServerURL.Length==0 && mailExchange.Email.Length==0))
                {
                    MessageBox.Show("Please provide correct setting for Exchange server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);

                    if (mailExchange.Domain.Length > 0)
                        service.Credentials = new WebCredentials(mailExchange.User, mailExchange.Password, mailExchange.Domain);
                    else
                        service.Credentials = new WebCredentials(mailExchange.User, mailExchange.Password);


                    if (mailExchange.ServerURL.Length > 0)
                    {
                        service.Url = new System.Uri(mailExchange.ServerURL);
                    }
                    else
                    {
                        service.AutodiscoverUrl(mailExchange.Email, RedirectionUrlValidationCallback);
                    }

                    EmailMessage email = new EmailMessage(service);

                    List<string> toList = MainForm.ParseStringToList(tbeToAddress.Text, ",;");

                    for (int i = 0; i < toList.Count; i++)
                    {
                        email.ToRecipients.Add(new EmailAddress(toList[i]));
                    }

                    if (tbCopy.Text.Trim().Length > 0)
                    {
                        List<string> ccList = MainForm.ParseStringToList(tbCopy.Text.Trim(), ",;");

                        for (int i = 0; i < ccList.Count; i++)
                        {
                            email.CcRecipients.Add(new EmailAddress(ccList[i]));
                        }
                    }

                    if (tbBlindCopy.Text.Trim().Length > 0)
                    {
                        List<string> bccList = MainForm.ParseStringToList(tbBlindCopy.Text.Trim(), ",;");

                        for (int i = 0; i < bccList.Count; i++)
                        {
                            email.BccRecipients.Add(new EmailAddress(bccList[i]));
                        }
                    }

                    email.Subject = tbSubject.Text;
                    
                    email.Body = new MessageBody(tbMessage.Text);
                    email.Body.BodyType = BodyType.Text;

                    if (lvAttachments.Items.Count > 0)
                    {
                        for (int i = 0; i < lvAttachments.Items.Count; i++)
                        {
                            email.Attachments.AddFileAttachment(lvAttachments.Items[i].Text);
                        }
                    }

                    email.Send();

                    this.Cursor = Cursors.Default;

                    MessageBox.Show("Message to \"" + tbeToAddress.Text + "\" was sent successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    SaveSelfcopy();
                }
                catch (Exception E)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnAttachment_Click(object sender, EventArgs e)
        {
            AddNewAttachment();
        }

        private void AddNewAttachment()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (String file in openFileDialog1.FileNames)
                {
                    bool fileExists = false;

                    for (int i = 0; i < lvAttachments.Items.Count; i++)
                    {
                        if (file == lvAttachments.Items[i].Text)
                        {
                            fileExists = true;
                            break;
                        }
                    }

                    if (!fileExists)
                    {
                        lvAttachments.Items.Add(file);
                    }
                }

            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewAttachment();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Delete attachment

            if (lvAttachments.SelectedItems.Count>0)
            {
                for (int i = lvAttachments.SelectedItems.Count-1; i >=0; i--)
                {
                    lvAttachments.SelectedItems[i].Remove();
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open file
            if (lvAttachments.SelectedItems.Count > 0)
            {
                string fileName = lvAttachments.SelectedItems[0].Text;

                if (File.Exists(fileName))
                {
                    Process.Start(fileName);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("File \"" + fileName + "\" was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FrmMail_Load(object sender, EventArgs e)
        {
            //Restore form settings
            // Upgrade?
            if (Properties.Settings.Default.FrmMailSize.Width == 0)
                Properties.Settings.Default.Upgrade();

            if (Properties.Settings.Default.FrmMailSize.Width == 0 || Properties.Settings.Default.FrmMailSize.Height == 0)
            {
                // first start
                // optional: add default values
            }
            else
            {
                this.WindowState = Properties.Settings.Default.FrmMailState;

                // we don't want a minimized window at startup
                if (this.WindowState == FormWindowState.Minimized) this.WindowState = FormWindowState.Normal;

                this.Location = Properties.Settings.Default.FrmMailLocation;
                this.Size = Properties.Settings.Default.FrmMailSize;
            }

        }

        private void FrmMail_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Store form settings
            Properties.Settings.Default.FrmMailState = this.WindowState;

            if (this.WindowState == FormWindowState.Normal)
            {
                // save location and size if the state is normal
                Properties.Settings.Default.FrmMailLocation = this.Location;
                Properties.Settings.Default.FrmMailSize = this.Size;
            }
            else
            {
                // save the RestoreBounds if the form is minimized or maximized!
                Properties.Settings.Default.FrmMailLocation = this.RestoreBounds.Location;
                Properties.Settings.Default.FrmMailSize = this.RestoreBounds.Size;
            }

            // don't forget to save the settings
            Properties.Settings.Default.Save();

        }

        private string GetServerType()
        {
            string fileName = Path.Combine(MainForm.serviceFolder, "Settings_" + MainForm.userName + ".ini");
            IniFile iniFile = new IniFile(fileName);

            string section = "Mail client";

            string s = iniFile.ReadString(section, "ServerType", "SMTP");

            return s;
        }

        private static bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            // The default for the validation callback is to reject the URL.
            bool result = false;

            Uri redirectionUri = new Uri(redirectionUrl);

            // Validate the contents of the redirection URL. In this simple validation
            // callback, the redirection URL is considered valid if it is using HTTPS
            // to encrypt the authentication credentials. 
            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }
            return result;
        }

        private void SaveSelfcopy()
        {
            string fileName = Path.Combine(MainForm.serviceFolder, "Settings_" + MainForm.userName + ".ini");
            IniFile iniFile = new IniFile(fileName);

            string section = "Mail client";

            iniFile.Write(section, "CopyToMyself", chbSelfCopy.Checked);
        }

        private void GetSelfcopy()
        {
            string fileName = Path.Combine(MainForm.serviceFolder, "Settings_" + MainForm.userName + ".ini");
            IniFile iniFile = new IniFile(fileName);

            string section = "Mail client";

            chbSelfCopy.Checked = iniFile.ReadBoolean(section, "CopyToMyself", false);
        }

        private void FillAddresses()
        {
                        
            OleDbCommand cmd = new OleDbCommand("", MainForm.connection);

             cmd.CommandText =
                "select VESSEL_NAME, VESSEL_EMAIL \n" +
                "from VESSELS \n" +
                "where \n" +
                "not IsNull(VESSEL_EMAIL) \n" +
                "and Len(VESSEL_EMAIL)>0 \n" +
                "and HIDDEN=FALSE \n" +
                "order by VESSEL_NAME";

             OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);

             if (MainForm.DS.Tables.Contains("VESSELS_EMAIL"))
                 MainForm.DS.Tables["VESSELS_EMAIL"].Clear();

             adpt.Fill(MainForm.DS, "VESSELS_EMAIL");

             DataTable vslsEmail = MainForm.DS.Tables["VESSELS_EMAIL"];

             DataRow[] rows = vslsEmail.Select();

             emails.Clear();

            foreach (DataRow row in rows)
             {
                 emails.Add(row["VESSEL_NAME"].ToString() + "<" + row["VESSEL_EMAIL"].ToString()+">");
             }
        }

        private void tbeToAddress_BeforeDisplayingAutoComplete(object sender, TextBoxEx.TextBoxExAutoCompleteEventArgs e)
        {

            List<string> vsbl = new List<string>();

            for (int i=0; i<emails.Count; i++)
            {
                string em = emails[i].ToUpper();

                if (em.Contains(tbeToAddress.Text.ToUpper()))
                {
                    vsbl.Add(emails[i]);
                }
            }

            e.AutoCompleteList = vsbl;
            e.SelectedIndex = 0;
        }

        private void tbeToAddress_Resize(object sender, EventArgs e)
        {
        }


    }
}
