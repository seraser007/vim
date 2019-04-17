using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Collections;
using System.Windows.Forms;


namespace ComboBoxExt
{
    [Description("Combo box with extended suggest ability")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(ComboBoxExt), "SuggestComboBox.bmp")]

    public partial class ComboBoxExt : ComboBox
    {
            #region fields and properties
            
            public enum searchRules {cbsStartsWith, cbsContains, cbsEndsWith};
            public enum suggestBorders { sbsNone, sbsSizable, sbsSingle };


            private readonly ListBox _suggLb = new ListBox { Visible = false, TabStop = false };
            private readonly BindingList<string> _suggBindingList = new BindingList<string>();
            //private int SearchRule = 0;
            protected searchRules searchRule = searchRules.cbsContains;
            protected suggestBorders suggestBorder = suggestBorders.sbsNone;

            private readonly Form _suggFrm = new Form();
            private Timer _suggTimer = new Timer();

            private int MaxHeight = 106;
            private int _suggHeight = 106;

            //private Expression<Func<ObjectCollection, IEnumerable<string>>> _propertySelector;
            //private Func<ObjectCollection, IEnumerable<string>> _propertySelectorCompiled;
            //private Expression<Func<string, string, bool>> _filterRule;
            //private Func<string, bool> _filterRuleCompiled;
            //private Expression<Func<string, string>> _suggestListOrderRule;
            //private Func<string, string> _suggestListOrderRuleCompiled;

            public int SuggestBoxHeight
            {
                get { return _suggHeight; }
                set { if (value > 0) _suggHeight = value; }
            }

            
            [Description ("Filterring rules")]

            public searchRules SearchRule
            {
                get {return searchRule;}
                set {searchRule = value;}
            }
            
            public suggestBorders SuggestBorderStyle
            {
                get { return suggestBorder; }
                set { suggestBorder = value; }
            }
          

            //SearchRule = searchRules.cbsContains;

            /// <summary>
            /// If the item-type of the ComboBox is not string,
            /// you can set here which property should be used
            /// </summary>
            //public Expression<Func<ObjectCollection, IEnumerable<string>>> PropertySelector
            //{
            //    get { return _propertySelector; }
            //    set
            //    {
            //        if (value == null) return;
            //        _propertySelector = value;
            //        _propertySelectorCompiled = value.Compile();
            //    }
            //}

            ///<summary>
            /// Lambda-Expression to determine the suggested items
            /// (as Expression here because simple lamda (func) is not serializable)
            /// <para>default: case-insensitive contains search</para>
            /// <para>1st string: list item</para>
            /// <para>2nd string: typed text</para>
            ///</summary>
            //public Expression<Func<string, string, bool>> FilterRule
            //{
            //    get { return _filterRule; }
            //    set
            //    {
            //        if (value == null) return;
            //        _filterRule = value;
            //        _filterRuleCompiled = item => value.Compile()(item, Text);
            //    }
            //}

            ///<summary>
            /// Lambda-Expression to order the suggested items
            /// (as Expression here because simple lamda (func) is not serializable)
            /// <para>default: alphabetic ordering</para>
            ///</summary>
            //public Expression<Func<string, string>> SuggestListOrderRule
            //{
            //    get { return _suggestListOrderRule; }
            //    set
            //    {
            //        if (value == null) return;
            //        _suggestListOrderRule = value;
            //        _suggestListOrderRuleCompiled = value.Compile();
            //    }
            //}

            #endregion

            /// <summary>
            /// ctor
            /// </summary>
            public ComboBoxExt()
            {
                // set the standard rules:
                //_filterRuleCompiled = s => s.ToLower().Contains(Text.Trim().ToLower());
                //_suggestListOrderRuleCompiled = s => s;
                //_propertySelectorCompiled = collection => collection.Cast<string>();
                MaxHeight = SuggestBoxHeight;

                _suggLb.DataSource = _suggBindingList;
                _suggLb.Click += SuggLbOnClick;

                _suggFrm.Controls.Add(_suggLb);
                _suggFrm.Controls.SetChildIndex(_suggLb, 0);

                _suggFrm.ShowInTaskbar = false;
                _suggFrm.TopMost = true;
                _suggFrm.Visible = false;
                _suggFrm.ControlBox = false;
                _suggFrm.Text = "";
                _suggFrm.Font = Font;
                //_suggFrm.BackColor = Color.Aqua;
                _suggFrm.StartPosition = FormStartPosition.Manual;
                //_suggFrm.SizeGripStyle = SizeGripStyle.Show;
                //_suggFrm.Padding.Bottom = 15;

                _suggLb.Dock = DockStyle.Fill;
                //_suggLb.TabIndex = 0;

                ParentChanged += OnParentChanged;
                _suggTimer.Tick += new EventHandler(_suggTimer_Tick);

            }

            /// <summary>
            /// the magic happens here ;-)
            /// </summary>
            /// <param name="e"></param>
            protected override void OnTextChanged(EventArgs e)
            {
                base.OnTextChanged(e);

                if (!Focused)
                {
                    HideSuggest();
                    return;
                }

                if (Text.Length == 0)
                {
                    HideSuggest();
                    return;
                }

                SetNewPosition();

                //Save text and cursor position
                string txt = Text;
                int pos = SelectionStart;

                //Switch off update
                BeginUpdate();
                
                //Close list
                DroppedDown = false;
                
                //Restore text and cursor position
                Text = txt;
                SelectionStart = pos;
                

                _suggBindingList.Clear();
                _suggBindingList.RaiseListChangedEvents = false;

                /*
                _propertySelectorCompiled(Items)
                     .Where(_filterRuleCompiled)
                     .OrderBy(_suggestListOrderRuleCompiled)
                     .ToList()
                     .ForEach(_suggBindingList.Add);
               */
                
                AutoCompleteStringCollection col = ComboBoxAutoCompleteList();

                for (int i=0; i<col.Count; i++)
                {
                    _suggBindingList.Add(col[i].ToString());
                }

                int minHeight = ItemHeight;

                int boxHeight = ItemHeight * (col.Count+1);


                if (boxHeight < MaxHeight)
                {
                    if (boxHeight > minHeight)
                        _suggHeight = boxHeight;
                    else
                        _suggHeight = minHeight;
                }
                else
                    _suggHeight = MaxHeight;
                

                _suggBindingList.RaiseListChangedEvents = true;
                
                _suggBindingList.ResetBindings();

                bool _suggVisible = (_suggBindingList.Count > 0);
                

                if (_suggVisible)
                {
                    ShowSuggest();
                    this.Focus();
                    SelectionStart = pos;
                }

                if (_suggBindingList.Count == 1 &&
                            _suggBindingList[0].Length == Text.Trim().Length)
                {
                    Text = _suggBindingList[0].ToString();
                    Select(0, Text.Length);
                    HideSuggest();
                }

                //Switch on update
                EndUpdate();

            }
            
            private void ShowSuggest()
            {
                _suggFrm.Height = _suggHeight;
                
                SetNewPosition();
                SetBorgedStyle();

                //_suggFrm.Visible = true;
                _suggFrm.Show();

                _suggLb.Visible = true;
                _suggLb.Update();
                _suggLb.Dock = DockStyle.Fill;

                _suggFrm.BringToFront();

                _suggFrm.Height = _suggLb.Height;

                _suggTimer.Interval = 10;
                _suggTimer.Enabled = true;
            }

            private void HideSuggest()
            {
                _suggLb.Visible = false;
                _suggFrm.Visible = false;
                _suggTimer.Enabled = false;
            }

            private void SetBorgedStyle()
            {
                switch (suggestBorder)
                {
                    case suggestBorders.sbsNone:
                        _suggFrm.FormBorderStyle = FormBorderStyle.None;
                        _suggLb.IntegralHeight = true;
                        break;
                    case suggestBorders.sbsSingle:
                        _suggFrm.FormBorderStyle = FormBorderStyle.FixedSingle;
                        _suggLb.IntegralHeight = true;
                        break;
                    case suggestBorders.sbsSizable:
                        _suggFrm.FormBorderStyle = FormBorderStyle.Sizable;
                        //_suggFrm.SizeGripStyle = SizeGripStyle.Show;
                        _suggLb.IntegralHeight = false;
                        break;
                }
            }

            private AutoCompleteStringCollection ComboBoxAutoCompleteList ()
            {
                AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
                ComboBoxExt ComboBoxSource = this;

                coll.Clear();

                if (this.DataSource == null)
                {
                    for (int i = 0; i < ComboBoxSource.Items.Count; i++)
                    {
                        switch (SearchRule)
                        {
                            case searchRules.cbsStartsWith:
                                if (ComboBoxSource.Items[i].ToString().StartsWith(ComboBoxSource.Text, StringComparison.OrdinalIgnoreCase))
                                    coll.Add(ComboBoxSource.Items[i].ToString());
                                break;
                            case searchRules.cbsContains:
                                if (ComboBoxSource.Items[i].ToString().ToUpper().Contains(ComboBoxSource.Text.ToUpper()))
                                {
                                    coll.Add(ComboBoxSource.Items[i].ToString());
                                }
                                break;
                            case searchRules.cbsEndsWith:
                                if (ComboBoxSource.Items[i].ToString().EndsWith(ComboBoxSource.Text, StringComparison.OrdinalIgnoreCase))
                                {
                                    coll.Add(ComboBoxSource.Items[i].ToString());
                                }
                                break;
                        }
                    }
                }
                else
                {
                    DataTable dt=new DataTable();

                    try
                    {
                        BindingSource bs = (BindingSource) ComboBoxSource.DataSource;
                        dt=((DataRowView)bs.Current).DataView.Table;
                        //dt = (DataTable) bs.DataSource;
                    }
                    catch 
                    {
                        //System.Windows.Forms.MessageBox.Show(E.Message);

                        try
                        {
                            dt = (DataTable)this.DataSource;
                        }
                        catch
                        {
                            //System.Windows.Forms.MessageBox.Show(E1.Message);
                            return coll;
                        }
                    }

                    int index=dt.Columns[this.DisplayMember].Ordinal;
                    
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    foreach (DataRow row in dt.Rows)
                    {
                        
                        switch (SearchRule)
                        {
                            case searchRules.cbsStartsWith:
                                if (row.ItemArray[1].ToString().StartsWith(ComboBoxSource.Text, StringComparison.OrdinalIgnoreCase))
                                    coll.Add(row.ItemArray[1].ToString());
                                break;
                            case searchRules.cbsContains:
                                if (row.ItemArray[index].ToString().ToUpper().Contains(ComboBoxSource.Text.ToUpper()))
                                {
                                    coll.Add(row.ItemArray[index].ToString());
                                }
                                break;
                            case searchRules.cbsEndsWith:
                                if (row.ItemArray[1].ToString().EndsWith(ComboBoxSource.Text, StringComparison.OrdinalIgnoreCase))
                                {
                                    coll.Add(row.ItemArray[1].ToString());
                                }
                                break;
                        }
                    }
                }

                return coll;
            }

            /// <summary>
            /// suggest-ListBox is added to parent control
            /// (in ctor parent isn't already assigned)
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void OnParentChanged(object sender, EventArgs e)
            {

                /*
                _suggLb.Top = screenPoint.X + Height - 0;
                _suggLb.Left = screenPoint.Y + 0;
                _suggLb.Width = Width;
                _suggLb.Font = this.Font;
                */

                SetNewPosition();
            }
            
        
            private void SetNewPosition()
            {
                Point screenPoint = this.PointToScreen(Point.Empty);

                //_suggFrm.Width = Width;
                //_suggFrm.Height = this.SuggestBoxHeight;

                int scrHeight = Screen.PrimaryScreen.WorkingArea.Height;

                if ((scrHeight - screenPoint.Y - Height - _suggFrm.Height) < 1)
                {
                    _suggFrm.Left = screenPoint.X;
                    _suggFrm.Top = screenPoint.Y - _suggFrm.Height - 1;
                }
                else
                {
                    _suggFrm.Left = screenPoint.X;
                    _suggFrm.Top = screenPoint.Y + Height + 1;
                }

            }

            protected override void OnLostFocus(EventArgs e)
            {
                // _suggLb can only getting focused by clicking (because TabStop is off)
                // --> click-eventhandler 'SuggLbOnClick' is called
                
                if ((!_suggLb.Focused) && (!this.Focused) && (!_suggFrm.Focused))
                    HideSuggBox();

                base.OnLostFocus(e);
            }

            protected override void OnLocationChanged(EventArgs e)
            {
                base.OnLocationChanged(e);
                
                //_suggLb.Top = Top + Height - 3;
                //_suggLb.Left = Left + 3;
                SetNewPosition();
            }


            protected override void OnSizeChanged(EventArgs e)
            {
                base.OnSizeChanged(e);
                //_suggLb.Width = Width;

                _suggFrm.Width = Width;
            }

            private void SuggLbOnClick(object sender, EventArgs eventArgs)
            {
                Text = _suggLb.Text;
                Focus();
            }

            private void HideSuggBox()
            {
                _suggLb.Visible = false;
                _suggFrm.Visible = false;
            }

            protected override void OnDropDown(EventArgs e)
            {
                HideSuggBox();
                base.OnDropDown(e);
            }

            private void _suggTimer_Tick(object sender, System.EventArgs e)
            {
                if (_suggFrm.Visible)
                    SetNewPosition();
            }

            #region keystroke events

            /// <summary>
            /// if the suggest-ListBox is visible some keystrokes
            /// should behave in a custom way
            /// </summary>
            /// <param name="e"></param>
            protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
            {
                //if (!_suggLb.Visible)
                if (!_suggFrm.Visible)
                {
                    base.OnPreviewKeyDown(e);
                    return;
                }

                switch (e.KeyCode)
                {
                    case Keys.Down:
                        if (_suggLb.SelectedIndex < _suggBindingList.Count - 1)
                            _suggLb.SelectedIndex++;
                        return;
                    case Keys.Up:
                        if (_suggLb.SelectedIndex > 0)
                            _suggLb.SelectedIndex--;
                        return;
                    case Keys.Enter:
                        Text = _suggLb.Text;
                        Select(0, Text.Length);

                        _suggLb.Visible = false;
                        _suggFrm.Visible = false;
                        
                        return;
                    case Keys.Escape:
                        HideSuggBox();
                        return;
                }

                base.OnPreviewKeyDown(e);
            }

            //private static readonly Keys[] KeysToHandle = new[] { Keys.Down, Keys.Up, Keys.Enter, Keys.Escape };
            
            protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
            {
                // the keysstrokes of our interest should not be processed be base class:
                if (_suggLb.Visible && (keyData.Equals(Keys.Down) || keyData.Equals(Keys.Up) || keyData.Equals(Keys.Enter) || keyData.Equals(Keys.Escape)))
                    return true;
                return base.ProcessCmdKey(ref msg, keyData);
            }

            #endregion

        }
    }


