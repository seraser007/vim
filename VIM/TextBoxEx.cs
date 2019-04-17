using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace VIM
{
class TextBoxEx: TextBox
    {
        public List<string> test = new List<string>();

        public int Tabs = 0;

        private int mSelStart;

        private int mSelLength;

        private List<string> AutoCompleteListEx = new List<string>();

        private ListBox LBoxEx = new ListBox();

        private Form myForm = new Form();

        private Form myParentForm;

        private bool SuspendFocus = false;

        private TextBoxExAutoCompleteEventArgs Args;

        private Timer HideTimer = new Timer();

        private Timer FocusTimer = new Timer();

        private bool myShowAutoCompleteOnFocus;

        private System.Windows.Forms.FormBorderStyle myAutoCompleteFormBorder = FormBorderStyle.None;

        private bool myOnEnterSelect;

        private int LastItem;

        private SelectOptions mySelectionMethods = (SelectOptions.OnDoubleClick | SelectOptions.OnEnterPress);

        private bool mySelectTextAfterItemSelect = true;

        private int Cnt = 0;

        private Size boxSize = new Size(200, 100);

        private bool autoBoxSize = false;

        private int boxMinWidth = 50;

        private int boxMinHeight = 20;

        private bool txtChanged = false;
        private bool getSelection = false;

        public bool SelectTextAfterItemSelect
        {
            get
            {
                return mySelectTextAfterItemSelect;
            }
            set
            {
                mySelectTextAfterItemSelect = value;
            }
        }

        public bool AutoCompleteBoxAutoSize
        {
            get { return autoBoxSize; }
            set { autoBoxSize = value; }
        }

        public Size AutoCompleteBoxSize
        {
            get { return boxSize; }
            set { 
                    boxSize = value;

                    if (boxSize.Width < boxMinWidth)
                        boxSize.Width = boxMinWidth;

                    if (boxSize.Height < boxMinHeight)
                        boxSize.Height = boxMinHeight;
                }
        }

        public int AutoCompleteBoxMinHeight
        {
            get { return boxMinHeight; }
            set { boxMinHeight = value; }
        }

        public int AutoCompleteBoxMinWidth
        {
            get { return boxMinWidth; }
            set { boxMinWidth = value; }
        }
        //[System.ComponentModel.Browsable(false)]

        public SelectOptions SelectionMethods
        {
            get
            {
                return mySelectionMethods;
            }
            set
            {
                mySelectionMethods = value;
            }
        }

        public bool OnEnterSelect
        {
            get
            {
                return myOnEnterSelect;
            }
            set
            {
                myOnEnterSelect = value;
            }
        }

        public System.Windows.Forms.FormBorderStyle AutoCompleteFormBorder
        {
            get
            {
                return myAutoCompleteFormBorder;
            }
            set
            {
                myAutoCompleteFormBorder = value;
            }
        }

        public bool ShowAutoCompleteOnFocus
        {
            get
            {
                return myShowAutoCompleteOnFocus;
            }
            set
            {
                myShowAutoCompleteOnFocus = value;
            }
        }

        public ListBox Lbox
        {
            get
            {
                return LBoxEx;
            }
        }

        public List<string> AutoCompleteList { get; set; }

        public event EventHandler<TextBoxExAutoCompleteEventArgs> BeforeDisplayingAutoComplete;

        public event EventHandler<TextBoxExItemSelectedEventArgs> ItemSelected;

        public enum SelectOptions
        {
            None = 0,
            OnEnterPress = 1,
            OnSingleClick = 2,
            OnDoubleClick = 4,
            OnTabPress = 8,
            OnRightArrow = 16,
            OnEnterSingleClick = 3,
            OnEnterSingleDoubleClicks = 7,
            OnEnterDoubleClick = 5,
            OnEnterTab = 9,
        }

        public class TextBoxExAutoCompleteEventArgs : EventArgs
        {

            private bool myCancel;

            private int mySelectedIndex;

            public int SelectedIndex
            {
                get
                {
                    return mySelectedIndex;
                }
                set
                {
                    mySelectedIndex = value;
                }
            }

            public bool Cancel
            {
                get
                {
                    return myCancel;
                }
                set
                {
                    myCancel = value;
                }
            }

            public List<string> AutoCompleteList { get; set; }
        }

        public override string SelectedText
        {
            get
            {
                return base.SelectedText;
            }
            set
            {
                base.SelectedText = value;
            }
        }

        public override int SelectionLength
        {
            get
            {
                return base.SelectionLength;
            }
            set
            {
                base.SelectionLength = value;
            }
        }

        public TextBoxEx()
        {
            HideTimer.Tick += new EventHandler(HideTimer_Tick);
            FocusTimer.Tick += new EventHandler(FocusTimer_Tick);

            LBoxEx.Click += new EventHandler(myLbox_Click);
            LBoxEx.DoubleClick += new EventHandler(myLbox_DoubleClick);
            LBoxEx.GotFocus += new EventHandler(myLbox_GotFocus);
            LBoxEx.KeyDown += new KeyEventHandler(myLbox_KeyDown);

            LBoxEx.KeyUp += new KeyEventHandler(myLbox_KeyUp);
            LBoxEx.LostFocus += new EventHandler(myLbox_LostFocus);
            LBoxEx.MouseClick += new MouseEventHandler(myLbox_MouseClick);
            LBoxEx.MouseDoubleClick += new MouseEventHandler(myLbox_MouseDoubleClick);
            LBoxEx.MouseDown += new MouseEventHandler(myLbox_MouseDown);


            this.GotFocus += new EventHandler(TextBoxEx_GotFocus);
            this.KeyDown += new KeyEventHandler(TextBoxEx_KeyDown);
            this.Leave += new EventHandler(TextBoxEx_Leave);
            this.LostFocus += new EventHandler(TextBoxEx_LostFocus);
            this.Move += new EventHandler(TextBoxEx_Move);
            this.ParentChanged += new EventHandler(TextBoxEx_ParentChanged);
            this.TextChanged += new EventHandler(TextBoxEx_TextChanged);
        }

        private void TextBoxEx_TextChanged(object sender, EventArgs e)
        {
            if (getSelection)
            {
                txtChanged = false;
                getSelection = false;
            }
            else
                txtChanged = true;
        }
 
        override protected void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
        {
            base.OnKeyUp(e);

            string s = e.KeyData.ToString();

            if (txtChanged && this.TextLength>0)
                /*
                (s.Length == 1 || 
                s == "Space" || 
                (this.Text.Length > 0 && 
                e.KeyCode!=Keys.Left && 
                e.KeyCode!=Keys.Right && 
                e.KeyCode!=Keys.Up && 
                e.KeyCode!=Keys.Down &&
                e.KeyCode!=Keys.Enter
                && e.KeyCode!=Keys.Escape
                && e.KeyCode!=Keys.Home
                && e.KeyCode!=Keys.End))
                */
            {
                
                this.ShowAutoComplete();
                txtChanged = false;
            }
            else if (this.Text.Length == 0 
                    && e.KeyCode != Keys.Left 
                    && e.KeyCode != Keys.Right 
                    && e.KeyCode != Keys.Up 
                    && e.KeyCode != Keys.Down
                    && e.KeyCode != Keys.Home
                    && e.KeyCode != Keys.End)
                DoHideAuto();

        }

        private void ShowOnChar(string C)
        {


            if (IsPrintChar(C))
            {
                this.ShowAutoComplete();
            }
        }

        private bool IsPrintChar(int C)
        {


            return IsPrintChar(((char)(C)));
        }

        private bool IsPrintChar(byte C)
        {


            return IsPrintChar(((char)(C)));
        }

        private bool IsPrintChar(char C)
        {


            return IsPrintChar(C.ToString());
        }

        private bool IsPrintChar(string C)
        {

            if (System.Text.RegularExpressions.Regex.IsMatch(C, "[^\\t\\n\\r\\f\\v]"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void TextBoxEx_GotFocus(object sender, System.EventArgs e)
        {

            if ((!this.SuspendFocus
                        && (this.myShowAutoCompleteOnFocus
                        && (this.myForm.Visible == false))))
            {
                this.ShowAutoComplete();
            }

        }

        private void TextBoxEx_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (!myForm.Visible)
                return;

            if (e.KeyCode == Keys.Escape)
            {
                DoHideAuto();
                return;
            }

            if (!SelectItem(e.KeyCode, false, false))
            {
                if ((e.KeyCode == Keys.Up))
                {
                    if ((LBoxEx.SelectedIndex > 0))
                    {
                        MoveLBox((LBoxEx.SelectedIndex - 1));
                    }
                }
                else if ((e.KeyCode == Keys.Down))
                {
                    MoveLBox((LBoxEx.SelectedIndex + 1));
                }
            }

        }

        new void SelectAll()
        {
        }

        private void MoveLBox(int Index)
        {

            try
            {
                if (Index > (LBoxEx.Items.Count - 1))
                {
                    Index = (LBoxEx.Items.Count - 1);
                }

                LBoxEx.SelectedIndex = Index;

                
            }
            catch
            {
            }

        }

        private void TextBoxEx_Leave(object sender, System.EventArgs e)
        {

            DoHide(sender, e);

        }

        private void TextBoxEx_LostFocus(object sender, System.EventArgs e)
        {

            if (!HideTimer.Enabled)
            {
                HideTimer.Interval = 500;
                HideTimer.Enabled = true;
            }
            //DoHide(sender, e);

            //MessageBox.Show("Text box lost focus");

        }

        private void TextBoxEx_Move(object sender, System.EventArgs e)
        {

            MoveDrop();

        }

        private void TextBoxEx_ParentChanged(object sender, System.EventArgs e)
        {

            if (myParentForm != null) myParentForm.Deactivate -= new EventHandler(myParentForm_Deactivate);
            myParentForm = GetParentForm(this);
            if (myParentForm != null) myParentForm.Deactivate += new EventHandler(myParentForm_Deactivate);
        }

        private void HideTimer_Tick(object sender, System.EventArgs e)
        {

            MoveDrop();
            
            if (!this.Focused)
                DoHide(sender, e);
            //MessageBox.Show("Hide");
            //HideTimer.Stop();

            Cnt++;
            if ((Cnt > 300))
            {
                if (!AppHasFocus(""))
                {
                    DoHideAuto();
                }
                Cnt = 0;
            }

        }

        private void myLbox_Click(object sender, System.EventArgs e)
        {
        }

        private void myLbox_DoubleClick(object sender, System.EventArgs e)
        {
        }

        private bool SelectItem(Keys Key, bool SingleClick)
        {
            return SelectItem(Key, SingleClick, false);
        }

        private bool SelectItem(Keys Key)
        {
            return SelectItem(Key, false, false);
        }

        private bool SelectItem(Keys Key, bool SingleClick, bool DoubleClick)
        {

            // Warning!!! Optional parameters not supported
            // Warning!!! Optional parameters not supported
            // Warning!!! Optional parameters not supported

            bool DoSelect = true;
            SelectOptions Meth = SelectOptions.None;
            LastItem = -1;

            if (((this.mySelectionMethods & SelectOptions.OnEnterPress) > 0) && (Key == Keys.Enter))
            {
                Meth = SelectOptions.OnEnterPress;
            }
            else if (((this.mySelectionMethods & SelectOptions.OnRightArrow) > 0) && Key == Keys.Right)
            {
                Meth = SelectOptions.OnRightArrow;
            }
            else if (((this.mySelectionMethods & SelectOptions.OnTabPress) > 0) && Key == Keys.Tab)
            {
                Meth = SelectOptions.OnTabPress;
            }
            else if (((this.mySelectionMethods & SelectOptions.OnSingleClick) > 0) && SingleClick)
            {
                Meth = SelectOptions.OnEnterPress;
            }
            else if (((this.mySelectionMethods & SelectOptions.OnDoubleClick) > 0) && DoubleClick)
            {
                Meth = SelectOptions.OnEnterPress;
            }
            else
            {
                DoSelect = false;
            }

            LastItem = LBoxEx.SelectedIndex;
            if (DoSelect)
            {
                DoSelectItem(Meth);
            }

            return DoSelect;
        }
        public class TextBoxExItemSelectedEventArgs : EventArgs
        {

            private int myIndex;

            private SelectOptions myMethod;

            private string myItemText;

            public TextBoxExItemSelectedEventArgs()
            {
            }

            public TextBoxExItemSelectedEventArgs(int Index, SelectOptions Method, string ItemText)
            {
                myIndex = Index;
                myMethod = Method;
                myItemText = ItemText;
            }

            public string ItemText
            {
                get
                {
                    return myItemText;
                }
                set
                {
                    myItemText = value;
                }
            }

            public SelectOptions Method
            {
                get
                {
                    return myMethod;
                }
                set
                {
                    myMethod = value;
                }
            }

            public int Index
            {
                get
                {
                    return myIndex;
                }
                set
                {
                    myIndex = value;
                }
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern int GetWindowThreadProcessId(IntPtr hWnd, ref int ProcessID);

        private bool AppHasFocus(string ExeNameWithoutExtension)
        {
            bool Out = false;
            // Warning!!! Optional parameters not supported
            int PID = 0;

            if ((ExeNameWithoutExtension == ""))
            {
                ExeNameWithoutExtension = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            }

            IntPtr activeHandle = GetForegroundWindow();
            GetWindowThreadProcessId(activeHandle, ref PID);
            
            if ((PID > 0))
            {
                // For Each p As Process In Process.GetProcessesByName(ExeNameWithoutExtension)
                if ((PID == System.Diagnostics.Process.GetCurrentProcess().Id))
                {
                    Out = true;
                }
                //  Next
            }

            return Out;
        }

        private void SaveSelects()
        {
            this.mSelStart = this.SelectionStart;
            this.mSelLength = this.SelectionLength;
        }

        private void LoadSelects()
        {
            this.SelectionStart = this.mSelStart;
            this.SelectionLength = this.mSelLength;
        }

        private void ShowAutoComplete()
        {

            Args = new TextBoxExAutoCompleteEventArgs();
            // With...
            Args.Cancel = false;
            Args.AutoCompleteList = this.AutoCompleteListEx;
            
            if ((LBoxEx.SelectedIndex == -1))
            {
                Args.SelectedIndex = 0;
            }
            else
            {
                Args.SelectedIndex = LBoxEx.SelectedIndex;
            }

            if (BeforeDisplayingAutoComplete != null) 
                BeforeDisplayingAutoComplete(this, Args);
            
            this.AutoCompleteListEx = Args.AutoCompleteList;
            // If Me.myAutoCompleteList IsNot Nothing AndAlso Me.myAutoCompleteList.Count - 1 < Args.SelectedIndex Then
            //   Args.SelectedIndex = Me.myAutoCompleteList.Count - 1
            // End If
            
            if ((!Args.Cancel && (Args.AutoCompleteList != null) && Args.AutoCompleteList.Count > 0))
            {
                DoShowAuto();
            }
            else
            {
                DoHideAuto();
            }

        }

        private void DoShowAuto()
        {
            //Show auto complete box

            SaveSelects();

            LBoxEx.BeginUpdate();

            try
            {
                LBoxEx.Items.Clear();
                LBoxEx.Items.AddRange(this.AutoCompleteListEx.ToArray());
                LBoxEx.Sorted = true;
                this.MoveLBox(Args.SelectedIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            LBoxEx.EndUpdate();
            myParentForm = GetParentForm(this);
            
            if (myParentForm != null)
            {
                LBoxEx.Name = ("mmmlbox" + DateTime.Now.Millisecond);
                
                if ((myForm.Visible == false))
                {
                    myForm.Font = this.Font;
                    LBoxEx.Font = this.Font;
                    LBoxEx.Visible = true;
                    
                    myForm.Visible = false;
                    myForm.ControlBox = false;
                    myForm.Text = "";


                    LBoxEx.Size = GetFormSize();

                    if (!myForm.Controls.Contains(LBoxEx))
                    {
                        myForm.Controls.Add(LBoxEx);
                    }

                    myForm.FormBorderStyle = FormBorderStyle.None;
                    myForm.ShowInTaskbar = false;
                    // With...
                    //LBoxEx.Dock = DockStyle.Fill;
                    LBoxEx.Location = new Point(0, 0);
                    LBoxEx.SelectionMode = SelectionMode.One;

                    //myForm.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                    //myForm.AutoSize = true;
                    myForm.Size = LBoxEx.Size;

                    // Frm.Controls.Add(myLbox)
                    SuspendFocus = true;
                    myForm.TopMost = true;
                    myForm.FormBorderStyle = this.myAutoCompleteFormBorder;
                    myForm.BringToFront();
                    MoveDrop();

                    myForm.Visible = true;
                    myForm.Show();
                    myForm.Refresh();

                    MoveDrop();
                    //HideTimer.Interval = 100;
                    this.Focus();
                    SuspendFocus = false;
                    //HideTimer.Enabled = true;
                    LoadSelects();
                }
                else
                {
                    LBoxEx.Size = GetFormSize();
                    myForm.Size = LBoxEx.Size;
                    MoveDrop();
                }
            }

        }

        private Size GetFormSize()
        {
            Size fSize=new Size();

            if (autoBoxSize)
            {
                int maxLen = 0;
                int maxHeight = 0;

                for (int i = 0; i < LBoxEx.Items.Count; i++)
                {
                    Size txtSize = TextRenderer.MeasureText(LBoxEx.Items[i].ToString(), this.Font);

                    if (maxLen < txtSize.Width)
                        maxLen = txtSize.Width;

                    if (maxHeight < txtSize.Height)
                        maxHeight = txtSize.Height;
                }

                fSize.Width = maxLen + 5;

                if (LBoxEx.Items.Count < 10)
                    fSize.Height = maxHeight * (LBoxEx.Items.Count + 1);
                else
                    fSize.Height = 10 * LBoxEx.Items.Count;
            }
            else
            {
                fSize.Width = boxSize.Width;
                fSize.Height = boxSize.Height;
            }

            if (fSize.Width < boxMinWidth)
                fSize.Width = boxMinWidth;

            if (fSize.Height < boxMinHeight)
                fSize.Height = boxMinHeight;

            if (fSize.Width > Screen.GetWorkingArea(this).Width - 100)
                fSize.Width = Screen.GetWorkingArea(this).Width - 110;

            if (fSize.Height > Screen.GetWorkingArea(this).Height / 2)
                fSize.Height = Screen.GetWorkingArea(this).Height / 2;

            return fSize;
        }

        void MoveDrop()
        {

            Point Pnt = new Point(this.Left, (this.Top
                            + (this.Height + 2)));
            Point ScreenPnt = this.PointToScreen(new Point(-2, this.Height));
            // Dim FrmPnt As Point = Frm.PointToClient(ScreenPnt)
            if (myForm != null)
            {
                if (ScreenPnt.X + this.myForm.Width > Screen.GetWorkingArea(this).Width)
                    ScreenPnt.X = Screen.GetWorkingArea(this).Width - this.myForm.Width - 10;

                if (ScreenPnt.Y + this.myForm.Height > Screen.GetWorkingArea(this).Height)
                    ScreenPnt.Y = ScreenPnt.Y - this.Height - this.myForm.Height;
                
                myForm.Location = ScreenPnt;
                // myForm.BringToFront()
                // myForm.Focus()
                // myLbox.Focus()
                // Me.Focus()
            }

        }

        void DoHide(object sender, EventArgs e)
        {

            HideAuto();

        }

        private void DFocus(int Delay)
        {

            // Warning!!! Optional parameters not supported
            FocusTimer.Interval = Delay;
            FocusTimer.Start();

        }

        private void DFocus()
        {
            DFocus(10);
        }

        private void DoHideAuto()
        {

            myForm.Hide();
            HideTimer.Enabled = false;
            FocusTimer.Enabled = false;

        }

        private void HideAuto()
        {

            if ((myForm.Visible && HasLostFocus()))
            {
                DoHideAuto();
            }

        }

        private bool HasLostFocus()
        {

            bool Out = false;
            if (this.myForm == null || myForm.ActiveControl != this.LBoxEx)
            {
                Out = true;
            }
            if (this.myParentForm == null || this.myParentForm.ActiveControl != this)
            {
                Out = true;
            }

            return Out;
        }

        private Form GetParentForm(Control InCon)
        {

            Control TopCon = FindTopParent(InCon);
            Form Out = null;
            if ((TopCon is Form))
            {
                Out = ((Form)(TopCon));
            }

            return Out;
        }

        private Control FindTopParent(Control InCon)
        {

            Control Out;
            if ((InCon.Parent == null))
            {
                Out = InCon;
            }
            else
            {
                Out = FindTopParent(InCon.Parent);
            }

            return Out;
        }

        private void DoSelectItem(SelectOptions Method)
        {

            if (((this.LBoxEx.Items.Count > 0)
                        && (this.LBoxEx.SelectedIndex > -1)))
            {
                string Value = this.LBoxEx.SelectedItem.ToString();
                string Orig = this.Text;

                getSelection = true;
                this.Text = Value;

                if (mySelectTextAfterItemSelect)
                {
                    try
                    {
                        this.SelectionStart = Orig.Length;
                        this.SelectionLength = (Value.Length - Orig.Length);
                    }
                    catch 
                    {
                    }
                }
                else
                {
                    // Me.SelectionStart = Me.Text.Length
                    // Me.SelectionLength = 0
                }

                TextBoxExItemSelectedEventArgs a;
                a = new TextBoxExItemSelectedEventArgs();
                a.Index = this.LBoxEx.SelectedIndex;
                a.Method = Method;
                a.ItemText = Value;

                if (ItemSelected != null) 
                    ItemSelected(this, a);

                //ItemSelected(this, new clsItemSelectedEventArgs(this.myLbox.SelectedIndex, Method, Value));
                this.DoHideAuto();
            }

        }

        private void myLbox_GotFocus(object sender, System.EventArgs e)
        {

            DFocus();

        }

        private void myLbox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {

            SelectItem(e.KeyCode);

        }

        private void ProcessKeyEvents(KeyEventArgs e)
        {


            if ((e.KeyCode >= Keys.A) && (e.KeyCode <= Keys.Z))
                base.OnKeyUp(e);


            //Keys.Back;
            //Keys.Enter;
            //Keys.Left;
            //Keys.Right;
            //Keys.Up;
            //Keys.Down;
            //(Keys.NumPad0 & (e.KeyCode <= Keys.NumPad9));
            //(Keys.D0 & (e.KeyCode <= Keys.D9));


        }

        private void myLbox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (IsPrintChar(e.KeyChar))
            {
                // Me.OnKeyPress(e)
                // Call MoveDrop()
            }

        }

        private void myLbox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (IsPrintChar(e.KeyValue))
            {
                // Me.OnKeyUp(e)
                // Call MoveDrop()
            }

        }

        private void myLbox_LostFocus(object sender, System.EventArgs e)
        {

            //DoHide(sender, e);

        }

        private void myLbox_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            // If e.Button <> Windows.Forms.MouseButtons.None Then
            SelectItem(Keys.None, true);
            // End If

        }

        private void myLbox_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            // If e.Button <> Windows.Forms.MouseButtons.None Then
            SelectItem(Keys.None, false, true);
            // End If

        }

        private void myForm_Deactivate(object sender, System.EventArgs e)
        {


        }

        private void myParentForm_Deactivate(object sender, System.EventArgs e)
        {


        }

        private void FocusTimer_Tick(object sender, System.EventArgs e)
        {

            this.Focus();

        }

        private void myLbox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            myLbox_MouseClick(sender, e);
        }
    
    }
}
