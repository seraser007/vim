using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;

//Custom message box with check box fnd long text scroll
//Project based on the following code:
//FlexibleMessageBox – A flexible replacement for the .NET MessageBox
//https://www.codeproject.com/Articles/601900/FlexibleMessageBox-A-flexible-replacement-for-the
//and
//MsgBox - A Better Winforms MessageBox in C#
//http://tutorialgenius.blogspot.ru/2016/04/msgbox-better-winforms-messagebox-in-c.html
//Main feathers
//- Check box with custom text
//- Text box for srolling of the long text
//- Default button selection
//- Message font selection
//- Message text color

namespace VIM
{
    public class MsgBoxExt
    {
        // Enhanced return object
        private static DialogResultExt result = null;

        #region Show() OVERLOADS
        /// <summary>
        /// Shows the extended message box.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="text">The text.</param>
        /// <returns>The dialog result.</returns>
        public static DialogResultExt Show(IWin32Window owner, string text)
        {
            return MsgBoxExtForm.Show(null, text);
        }

        /// <summary>
        /// Shows the extended message box.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <returns>The dialog result.</returns>
        public static DialogResultExt Show(IWin32Window owner, string text, string caption)
        {
            return MsgBoxExtForm.Show(owner, text, caption);
        }

        /// <summary>
        /// Shows the extended message box.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <returns>The dialog result.</returns>
        public static DialogResultExt Show(string text, string caption)
        {
            return MsgBoxExtForm.Show(null, text, caption);
        }

        /// <summary>
        /// Shows the extended message box.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <returns>The dialog result.</returns>
        public static DialogResultExt Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons)
        {
            return MsgBoxExtForm.Show(owner, text, caption, buttons);
        }

        /// <summary>
        /// Shows the extended message box.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <returns>The dialog result.</returns>
        public static DialogResultExt Show(string text, string caption, MessageBoxButtons buttons)
        {
            return MsgBoxExtForm.Show(null, text, caption, buttons);
        }

        /// <summary>
        /// Shows the extended message box.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <returns>The dialog result.</returns>
        public static DialogResultExt Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MsgBoxExtForm.Show(owner, text, caption, buttons, icon);
        }

        /// <summary>
        /// Shows the extended message box.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <returns>The dialog result.</returns>
        public static DialogResultExt Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MsgBoxExtForm.Show(null, text, caption, buttons, icon);
        }

        /// <summary>
        /// Shows the extended message box.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="defButton">The default button.</param>
        /// <returns>The dialog result.</returns>
        public static DialogResultExt Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MsgBoxExtDefaultButton defButton)
        {
            return MsgBoxExtForm.Show(owner, text, caption, buttons, icon, defButton);
        }

        /// <summary>
        /// Shows the extended message box.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="defButton">The default button.</param>
        /// <returns>The dialog result.</returns>
        public static DialogResultExt Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MsgBoxExtDefaultButton defButton=MsgBoxExtDefaultButton.Unknown)
        {
            return MsgBoxExtForm.Show(null, text, caption, buttons, icon, defButton);
        }

        /// <summary>
        /// Shows the extended message box.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="defButton">The default button.</param>
        /// <param name="showCheckBox">The show check box flag.</param>
        /// <param name="checkBoxText">The check box text.</param>
        /// <returns>The dialog result.</returns>
        public static DialogResultExt Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MsgBoxExtDefaultButton defButton=MsgBoxExtDefaultButton.Unknown, bool showCheckBox = false, string checkBoxText = "")
        {
            return MsgBoxExtForm.Show(owner, text, caption, buttons, icon, defButton, showCheckBox, checkBoxText);
        }

        /// <summary>
        /// Shows the extended message box.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="defButton">The default button.</param>
        /// <param name="showCheckBox">The show check box flag.</param>
        /// <param name="checkBoxText">The check box text.</param>
        /// <returns>The dialog result.</returns>
        public static DialogResultExt Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MsgBoxExtDefaultButton defButton=MsgBoxExtDefaultButton.Unknown, bool showCheckBox = false, string checkBoxText = "")
        {
            return MsgBoxExtForm.Show(null, text, caption, buttons, icon, defButton, showCheckBox, checkBoxText);
        }

        /// <summary>
        /// Shows the extended message box.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="defButton">The default button.</param>
        /// <param name="showCheckBox">The show check box flag.</param>
        /// <param name="checkBoxText">The check box text.</param>
        /// <param name="messageFont">The message font.</param>
        /// <returns>The dialog result.</returns>
        public static DialogResultExt Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MsgBoxExtDefaultButton defButton, bool showCheckBox = false, string checkBoxText = "", Font messageFont = null)
        {
            return MsgBoxExtForm.Show(owner, text, caption, buttons, icon, defButton, showCheckBox, checkBoxText, messageFont);
        }

        /// <summary>
        /// Shows the extended message box.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="defButton">The default button.</param>
        /// <param name="showCheckBox">The show check box flag.</param>
        /// <param name="checkBoxText">The check box text.</param>
        /// <param name="messageFont">The message font.</param>
        /// <returns>The dialog result.</returns>
        public static DialogResultExt Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MsgBoxExtDefaultButton defButton, bool showCheckBox = false, string checkBoxText = "", Font messageFont = null)
        {
            return MsgBoxExtForm.Show(null, text, caption, buttons, icon, defButton, showCheckBox, checkBoxText, messageFont);
        }

        /// <summary>
        /// Shows the extended message box.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="defaultButton">The default button.</param>
        /// <param name="showCheckBox">The show check box flag.</param>
        /// <param name="checkBoxText">The check box text.</param>
        /// <param name="messageFont">The message font.</param>
        /// <param name="messageForeColor">The message font color.</param>
        /// <returns>The dialog result.</returns>
        public static DialogResultExt Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MsgBoxExtDefaultButton defButton, bool showCheckBox = false, string checkBoxText = "", Font messageFont = null, Color? messageForeColor = null)
        {
            return MsgBoxExtForm.Show(owner, text, caption, buttons, icon, defButton, showCheckBox, checkBoxText, messageFont, messageForeColor);
        }

        /// <summary>
        /// Shows the extended message box.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="defaultButton">The default button.</param>
        /// <param name="showCheckBox">The show check box flag.</param>
        /// <param name="checkBoxText">The check box text.</param>
        /// <param name="messageFont">The message font.</param>
        /// <param name="messageForeColor">The message font color.</param>
        /// <returns>The dialog result.</returns>
        public static DialogResultExt Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MsgBoxExtDefaultButton defButton, bool showCheckBox = false, string checkBoxText = "", Font messageFont = null, Color? messageForeColor = null)
        {
            return MsgBoxExtForm.Show(null, text, caption, buttons, icon, defButton, showCheckBox, checkBoxText, messageFont, messageForeColor);
        }

        /// <summary>
        /// Shows the extended message box.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="defaultButton">The default button.</param>
        /// <param name="showCheckBox">The show check box flag.</param>
        /// <param name="checkBoxText">The check box text.</param>
        /// <param name="messageFont">The message font.</param>
        /// <param name="messageForeColor">The message font color.</param>
        /// <param name="messageBackColor">The message back color.</param>
        /// <returns>The dialog result.</returns>
        public static DialogResultExt Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MsgBoxExtDefaultButton defButton, bool showCheckBox = false, string checkBoxText = "", Font messageFont = null, Color? messageForeColor = null, Color? messageBackColor=null)
        {
            return MsgBoxExtForm.Show(null, text, caption, buttons, icon, defButton, showCheckBox, checkBoxText, messageFont, messageForeColor, messageBackColor);
        }

        #endregion

        class MsgBoxExtForm: Form
        { 
            private Panel pnlButtons;
            private CheckBox checkBox;
            private TextBox textBox;
            private PictureBox picIcon;
            private Panel pnlIcon;
            private Panel pnlMain;
            private Button defaultButton;
            private static int defButtonIndex = -1;

            private MsgBoxExtForm()
            {
                InitializeComponent();
            }

            public static DialogResultExt Show(IWin32Window owner, string text = "", string caption = "", 
                MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None, 
                MsgBoxExtDefaultButton defButton=MsgBoxExtDefaultButton.Unknown, bool showCheckBox = false, 
                string checkBoxText = "", Font messageFont = null, Color? messageForeColor = null,
                Color? messageBackColor = null)
            {
                // Setup return value and defaults each time as it's static - Cancel is default incase user x's the form
                result = new DialogResultExt { Result = System.Windows.Forms.DialogResult.Cancel, CheckBoxState = false };

                var msgBoxExtForm = new MsgBoxExtForm();
                msgBoxExtForm.ShowInTaskbar = false;
                msgBoxExtForm.ShowIcon = false;

                // Set window size defaults
                msgBoxExtForm.SetWindowDefaults();
            
                // Set window title
                msgBoxExtForm.Text = caption;


                if (messageBackColor != null)
                {
                    msgBoxExtForm.pnlMain.BackColor = (Color)messageBackColor;
                    msgBoxExtForm.pnlIcon.BackColor = (Color)messageBackColor;
                    msgBoxExtForm.textBox.BackColor = (Color)messageBackColor;
                }

                // Set the message text and styles
                msgBoxExtForm.textBox.Font = (messageFont == null) ? SystemFonts.DialogFont : messageFont;
                msgBoxExtForm.textBox.ForeColor = (!messageForeColor.HasValue) ? SystemColors.WindowText : messageForeColor.Value;
                msgBoxExtForm.textBox.Text = text.Replace("\n","\r\n"); // Set text last as it depends on the font size etc

                // Toggle Display again checkbox
                msgBoxExtForm.checkBox.Visible = showCheckBox;
                msgBoxExtForm.checkBox.Text = checkBoxText;

                // Set Icons [32x32]
                msgBoxExtForm.SetIcons(icon);

                defButtonIndex = (int)defButton;

                // Set buttons and adjust form width based on button count
                msgBoxExtForm.SetButtons(buttons);

                return new DialogResultExt { Result = msgBoxExtForm.ShowDialog(owner), CheckBoxState = msgBoxExtForm.checkBox.Checked};
            }


            private void InitializeComponent()
            {
                this.pnlButtons = new System.Windows.Forms.Panel();
                this.checkBox = new System.Windows.Forms.CheckBox();
                this.textBox = new System.Windows.Forms.TextBox();
                this.picIcon = new System.Windows.Forms.PictureBox();
                this.pnlIcon = new System.Windows.Forms.Panel();
                this.pnlMain = new System.Windows.Forms.Panel();
                ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
                this.pnlIcon.SuspendLayout();
                this.pnlMain.SuspendLayout();
                this.SuspendLayout();
                // 
                // pnlButtons
                // 
                this.pnlButtons.BackColor = System.Drawing.SystemColors.Control;
                this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
                this.pnlButtons.Location = new System.Drawing.Point(0, 89);
                this.pnlButtons.Name = "pnlButtons";
                this.pnlButtons.Size = new System.Drawing.Size(262, 48);
                this.pnlButtons.TabIndex = 2;
                // 
                // chkDoNotDisplayAgain
                // 
                this.checkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
                this.checkBox.AutoSize = true;
                this.checkBox.Location = new System.Drawing.Point(9, 69);
                this.checkBox.Name = "checkBox";
                this.checkBox.Size = new System.Drawing.Size(186, 17);
                this.checkBox.TabIndex = 3;
                this.checkBox.Text = "Do not display this message again";
                this.checkBox.UseVisualStyleBackColor = true;
                this.checkBox.Visible = false;
                // 
                // txtText
                // 
                this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
                this.textBox.BackColor = System.Drawing.Color.White;
                this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
                this.textBox.Location = new System.Drawing.Point(9, 24);
                this.textBox.MaximumSize = new System.Drawing.Size(403, 645);
                this.textBox.MaxLength = 999999999;
                this.textBox.MinimumSize = new System.Drawing.Size(100, 20);
                this.textBox.Multiline = true;
                this.textBox.Name = "textBox";
                this.textBox.ReadOnly = true;
                this.textBox.Size = new System.Drawing.Size(184, 20);
                this.textBox.TabIndex = 4;
                this.textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
                this.textBox.WordWrap = true;
                // 
                // picIcon
                // 
                this.picIcon.Location = new System.Drawing.Point(21, 22);
                this.picIcon.Name = "picIcon";
                this.picIcon.Size = new System.Drawing.Size(32, 32);
                this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
                this.picIcon.TabIndex = 5;
                this.picIcon.TabStop = false;
                // 
                // pnlIcon
                // 
                this.pnlIcon.Controls.Add(this.picIcon);
                this.pnlIcon.Dock = System.Windows.Forms.DockStyle.Left;
                this.pnlIcon.Location = new System.Drawing.Point(0, 0);
                this.pnlIcon.Name = "pnlIcon";
                this.pnlIcon.Size = new System.Drawing.Size(57, 89);
                this.pnlIcon.TabIndex = 6;
                // 
                // pnlMain
                // 
                this.pnlMain.Controls.Add(this.textBox);
                this.pnlMain.Controls.Add(this.checkBox);
                this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
                this.pnlMain.Location = new System.Drawing.Point(57, 0);
                this.pnlMain.Name = "pnlMain";
                this.pnlMain.Size = new System.Drawing.Size(205, 89);
                this.pnlMain.TabIndex = 7;
                // 
                // MsgBox
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.BackColor = System.Drawing.Color.White;
                this.ClientSize = new System.Drawing.Size(262, 137);
                this.Controls.Add(this.pnlMain);
                this.Controls.Add(this.pnlIcon);
                this.Controls.Add(this.pnlButtons);
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                this.MaximizeBox = false;
                this.MaximumSize = new System.Drawing.Size(497, 800);
                this.MinimizeBox = false;
                this.MinimumSize = new System.Drawing.Size(278, 175);
                this.Name = "MsgBoxExt";
                this.Shown += new System.EventHandler(this.MsgBoxExtForm_Shown); 
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
                this.pnlIcon.ResumeLayout(false);
                this.pnlIcon.PerformLayout();
                this.pnlMain.ResumeLayout(false);
                this.pnlMain.PerformLayout();
                this.ResumeLayout(false);

            }

            private void SetWindowDefaults()
            {
                // Make the maximum size of the form the same height as the screen
                this.MaximumSize = new Size(this.MaximumSize.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height); // Use primary display
                textBox.MaximumSize = new Size(textBox.MaximumSize.Width, this.MaximumSize.Height - 300);
            }

            private void SetIcons(MessageBoxIcon icon)
            {
                // Set icon based on requested icon

                if (icon == MessageBoxIcon.Asterisk) { picIcon.Image = SystemIcons.Asterisk.ToBitmap(); this.Icon = SystemIcons.Asterisk; }
                else if (icon == MessageBoxIcon.Error) { picIcon.Image = SystemIcons.Error.ToBitmap(); this.Icon = SystemIcons.Error; }
                else if (icon == MessageBoxIcon.Exclamation) { picIcon.Image = SystemIcons.Exclamation.ToBitmap(); this.Icon = SystemIcons.Exclamation; }
                else if (icon == MessageBoxIcon.Hand) { picIcon.Image = SystemIcons.Hand.ToBitmap(); this.Icon = SystemIcons.Hand; }
                else if (icon == MessageBoxIcon.Information) { picIcon.Image = SystemIcons.Information.ToBitmap(); this.Icon = SystemIcons.Information; }
                else if (icon == MessageBoxIcon.Question) { picIcon.Image = SystemIcons.Question.ToBitmap(); this.Icon = SystemIcons.Question; }
                else if (icon == MessageBoxIcon.Stop) { picIcon.Image = SystemIcons.Error.ToBitmap(); this.Icon = SystemIcons.Error; }
                else if (icon == MessageBoxIcon.Warning) { picIcon.Image = SystemIcons.Warning.ToBitmap(); this.Icon = SystemIcons.Warning; }
                else pnlIcon.Hide(); // Hide the whole icon panel so the text moves left with the docking if no icon
            }

            private void SetButtons(MessageBoxButtons buttons)
            {
                // Configure default form width for 3 buttons only. 2 buttons fit fine on the form at it's smallest size
                if ((buttons == MessageBoxButtons.AbortRetryIgnore || buttons == MessageBoxButtons.YesNoCancel) && this.Width < 349) 
                    this.Width = 349;


                // Configure button defaults
                Point firstButtonLocation = new Point(this.Width - 114, 13); // First button location based on form width. Buttons start from right, 10px apart.
                Size buttonSize = new Size(86, 24);

                // Init buttons
                List<Button> btnButtons = new List<Button>();

                for (int x = 0; x < 3; x++)
                {
                    Button btnButton = new Button();
                    btnButton.Name = "btn" + (x+1).ToString();
                    btnButton.Size = buttonSize;
                    btnButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                    btnButton.Location = firstButtonLocation;
                    firstButtonLocation.X = firstButtonLocation.X - (10 + buttonSize.Width); // Buttons start from right, 10px apart.
                    btnButton.Click += btnButton_Click;
                    btnButton.TabIndex = x;
                    btnButton.Visible = false;
                    pnlButtons.Controls.Add(btnButton);
                    btnButtons.Add(btnButton);
                }

                // Configure buttons
                switch (buttons)
                {
                    case MessageBoxButtons.OK:
                        btnButtons[0].Text = GetWindowsStrings.Load("user32.dll",800,"OK"); 
                        btnButtons[0].Visible = true;
                        btnButtons[0].DialogResult = DialogResult.OK;

                        defaultButton = btnButtons[0];
                        
                        break;
                    case MessageBoxButtons.OKCancel:
                        btnButtons[0].Text = GetWindowsStrings.Load("user32.dll", 801, "Cancel"); 
                        btnButtons[0].Visible = true;
                        btnButtons[0].DialogResult = DialogResult.Cancel;

                        btnButtons[1].Text = GetWindowsStrings.Load("user32.dll", 800, "OK"); 
                        btnButtons[1].Visible = true;
                        btnButtons[1].DialogResult = DialogResult.OK;

                        if ((defButtonIndex > 0) && (defButtonIndex<3))
                            defaultButton = btnButtons[2-defButtonIndex];
                        else
                            defaultButton = btnButtons[1];

                        break;
                    case MessageBoxButtons.YesNo:
                        btnButtons[0].Text = GetWindowsStrings.Load("user32.dll", 806, "No"); 
                        btnButtons[0].Visible = true;
                        btnButtons[0].DialogResult = DialogResult.No;

                        btnButtons[1].Text = GetWindowsStrings.Load("user32.dll", 805, "Yes");
                        btnButtons[1].Visible = true;
                        btnButtons[1].DialogResult = DialogResult.Yes;

                        if ((defButtonIndex > 0) && (defButtonIndex<3))
                            defaultButton = btnButtons[2-defButtonIndex];
                        else
                            defaultButton = btnButtons[1];

                        break;
                    case MessageBoxButtons.RetryCancel:
                        btnButtons[0].Text = GetWindowsStrings.Load("user32.dll", 801, "Cancel");
                        btnButtons[0].Visible = true;
                        btnButtons[0].DialogResult = DialogResult.Cancel;

                        btnButtons[1].Text = GetWindowsStrings.Load("user32.dll", 803, "Retry"); 
                        btnButtons[1].Visible = true;
                        btnButtons[1].DialogResult = DialogResult.Retry;

                        if ((defButtonIndex > 0) && (defButtonIndex<3))
                            defaultButton = btnButtons[2-defButtonIndex];
                        else
                            defaultButton = btnButtons[1];

                        break;
                    case MessageBoxButtons.AbortRetryIgnore:
                        btnButtons[0].Text = GetWindowsStrings.Load("user32.dll", 804, "Ignore");
                        btnButtons[0].Visible = true;
                        btnButtons[0].DialogResult = DialogResult.Ignore;

                        btnButtons[1].Text = GetWindowsStrings.Load("user32.dll", 803, "Retry");
                        btnButtons[1].Visible = true;
                        btnButtons[1].DialogResult = DialogResult.Retry;

                        btnButtons[2].Text = GetWindowsStrings.Load("user32.dll", 802, "Abort");
                        btnButtons[2].Visible = true;
                        btnButtons[2].DialogResult = DialogResult.Abort;
                    
                        if (defButtonIndex > 0)
                            defaultButton = btnButtons[3-defButtonIndex];
                        else
                            defaultButton = btnButtons[1];

                        break;
                    case MessageBoxButtons.YesNoCancel:
                        btnButtons[0].Text = GetWindowsStrings.Load("user32.dll", 801, "Cancel");
                        btnButtons[0].DialogResult = DialogResult.Cancel;
                        btnButtons[0].Visible = true;

                        btnButtons[1].Text = GetWindowsStrings.Load("user32.dll", 806, "No");
                        btnButtons[1].Visible = true;
                        btnButtons[1].DialogResult = DialogResult.No;

                        btnButtons[2].Text = GetWindowsStrings.Load("user32.dll", 805, "Yes");
                        btnButtons[2].Visible = true;
                        btnButtons[2].DialogResult = DialogResult.Yes;

                        if (defButtonIndex > 0)
                            defaultButton = btnButtons[3-defButtonIndex];
                        else
                            defaultButton = btnButtons[2];
                        
                        break;
                }

            }

            private void btnButton_Click(object sender, EventArgs e)
            {
                // Perform actions depending on button pressed
                Button btn = sender as Button;

                result.Result = btn.DialogResult;

                // Store extra settings
                result.CheckBoxState = checkBox.Checked;

                this.Close();
            }


            // Configure form size and message size which depends on the incoming message size
            private void textBox_TextChanged(object sender, EventArgs e)
            {
                // Set correct width for textbox and form. Base this off incoming message length.
                const int paddingWidth = 0; // Optional: Amount of padding height to add
                int borderTotalWidth = textBox.Width - this.textBox.ClientSize.Width; // Get border horizontal thickness
                int textToFormWidthDifference = this.MaximumSize.Width - textBox.MaximumSize.Width; // Get width difference between the text message and the form
                Size textSize = TextRenderer.MeasureText(textBox.Text, textBox.Font);
                textBox.Width = textSize.Width + paddingWidth + borderTotalWidth; // As long as we have set MinimumSize and MaximumSize widths for txtText and the form, then we don't need to worry about safe guards
                this.Width = textBox.Width + textToFormWidthDifference + pnlIcon.Width; // Set new form width

                // Set correct height for textbox and form. Base this off incoming message length.
                const int paddingHeight = 8; // Optional: Amount of padding height to add
                int borderTotalHeight = textBox.Height - this.textBox.ClientSize.Height; // Get border vertical thickness
                int textToFormHeightDifference = this.MinimumSize.Height - textBox.MinimumSize.Height; // Get height difference between the text message and the form
                int numLines = textBox.GetLineFromCharIndex(textBox.TextLength) + 1; // Get number of lines (first line is 0)
                textBox.Height = textBox.Font.Height * numLines + paddingHeight + borderTotalHeight; // Set height (height of one line * number of lines + spacing)
                this.Height = textBox.Height + textToFormHeightDifference; // Set new form height

                // If text field has reached it's maximum height, add scrollbars
                if (textBox.Height == textBox.MaximumSize.Height) textBox.ScrollBars = ScrollBars.Vertical;
            }

            private void MsgBoxExtForm_Shown(object sender, EventArgs e)
            {
                defaultButton.Focus();
            }
        }

    }

    /// <summary>
    /// For handling the enhanced return value
    /// </summary>
    public class DialogResultExt
    {
        public DialogResult Result { get; set; }
        public bool CheckBoxState { get; set; }
    }

    // Summary:
    //     Specifies constants defining the default button on a MsgBoxExt form.
    public enum MsgBoxExtDefaultButton
    {
        //
        // Summary:
        //     The default button is unknown.
        Unknown = 0,
        // Summary:
        //     The first button on the message box is the default button.
        Button1 = 1,
        //
        // Summary:
        //     The second button on the message box is the default button.
        Button2 = 2,
        //
        // Summary:
        //     The third button on the message box is the default button.
        Button3 = 3,
    }

}
