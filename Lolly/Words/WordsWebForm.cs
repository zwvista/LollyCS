using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DllLolly;
using LollyBase;

namespace Lolly
{
    public partial class WordsWebForm : WordsBaseForm
    {
        private BoolRef webBrowser1_Enter_done = new BoolRef(true);

        private List<DictWebBrowser> dwbList = new List<DictWebBrowser>();
        private DictWebBrowser currentDWB;
        private List<UIDict> uiDicts = new List<UIDict>();

        protected int filterScope = 0;
        protected string filter = "";
        protected List<MAUTOCORRECT> autoCorrectList;


        public WordsWebForm()
        {
            InitializeComponent();
            //filterHelper = new ComboBoxAutoCompleteHelper(filterToolStripComboBox.ComboBox);
            refreshToolStripButton.Click += refreshToolStripButton_Click;
            deleteTranslationToolStripButton.Click += deleteTranslationToolStripButton_Click;
            extractAppendToolStripButton.Click += extractToolStripButton_Click;
            extractOverriteToolStripButton.Click += extractToolStripButton_Click;
            editTranslationtoolStripButton.Click += editTranslationtoolStripButton_Click;
            setFilterToolStripButton.Click += setFilterToolStripButton_Click;
            removeFilterToolStripButton.Click += removeFilterToolStripButton_Click;
            dictsToolStripButton.Click += dictsToolStripButton_Click;
        }

        private void WordsWebForm_Shown(object sender, EventArgs e)
        {
            if (DesignMode == false)
                UpdatelbuSettings();
        }

        private void EnableToolStripButtons()
        {
            bool b = currentWord != "";
            deleteTranslationToolStripButton.Enabled = b && currentDWB.CanDeleteTranslation();
            editTranslationtoolStripButton.Enabled = b && currentDWB.CanEditTranslation();
            extractOverriteToolStripButton.Enabled = b && currentDWB.CanExtractAndOverriteTranslation();
            extractAppendToolStripButton.Enabled = b && currentDWB.CanExtractAndAppendTranslation();
        }

        private void webBrowser1_Enter(object sender, EventArgs e)
        {
            //if (!webBrowser1_Enter_done.Value) return;
            //using (new BoolScope(webBrowser1_Enter_done))
            //{
            //    bindingNavigator1.Focus();
            //    ((WebBrowser)sender).Focus();
            //}
        }

        private void webBrowser1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //if (!e.Alt) return;
            //switch (e.KeyCode)
            //{
            //    case Keys.Up:
            //        break;
            //    case Keys.Down:
            //        break;
            //    case Keys.Left:
            //        SelectNextDict(false, e.Control);
            //        break;
            //    case Keys.Right:
            //        SelectNextDict(true, e.Control);
            //        break;
            //}
        }

        protected override void OnDictSelected(int index)
        {
            if (currentDWB != null)
                currentDWB.Visible = false;
            currentDWB = dwbList[index];
            currentDWB.Visible = true;
            EnableToolStripButtons();
        }

        private void SelectNextDict(bool forward, bool all)
        {
            //SelectNextDict(forward, all ? dictsToolStrip.Items.Count : offsetOnline);
        }

        protected override void AddDict(UIDict dict)
        {
            base.AddDict(dict);

            var dwb = new DictWebBrowser(config);
            ((Control)dwb).Enter += webBrowser1_Enter;
            dwb.DocumentCompleted += webBrowser1_DocumentCompleted;
            dwb.PreviewKeyDown += webBrowser1_PreviewKeyDown;
            dwb.Dock = DockStyle.Fill;
            dwb.ScriptErrorsSuppressed = true;
            splitContainer1.Panel2.Controls.Add(dwb);
            dwbList.Add(dwb);

            if (dict is UIDictItem)
                dwb.SetDict(dict as UIDictItem);
            else
            {
                var items = (dict as UIDictCollection).Items;
                if (dict is UIDictPile)
                    dwb.SetDict(items);
                else
                {
                    dwb.SetDict(items.First());
                    var cnt = dictsToolStrip.Items.Count;
                    var btn = dictsToolStrip.Items[cnt - 1] as ToolStripSplitButtonCheckable;
                    EventHandler f = (sender, e) =>
                    {
                        var menu = sender as ToolStripMenuItem;
                        var n = btn.DropDownItems.IndexOf(menu);
                        btn.Text = menu.Text;
                        btn.Image = menu.Image;
                        dwb.SetDict((dict as UIDictCollection).Items[n]);
                        UpdateHtml(dwb);
                    };
                    foreach (ToolStripMenuItem menu in btn.DropDownItems)
                        menu.Click += f;
                }
            }

            UpdateHtml(dwb);
        }

        protected override int RemoveDict(string dictName, int imageIndex)
        {
            int i = base.RemoveDict(dictName, imageIndex);
            var dwb = dwbList[i];
            dwb.Dispose();
            dwbList.RemoveAt(i);
            return i;
        }

        protected override void FillDicts()
        {
            currentDWB = null;
            foreach (var dwb in dwbList)
                dwb.Dispose();
            dwbList.Clear();
            dictsToolStrip.Items.Clear();
            dictsToolStrip.Tag = -1;
            if(!uiDicts.Any())
                uiDicts.Add(config.dictsCustom[DictNames.DEFAULT]);
            foreach (var dict in uiDicts)
                AddDict(dict);
            foreach (var dwb in dwbList)
                dwb.Visible = false;
            SelectDict(0);
        }

        private void UpdateHtml(DictWebBrowser dwb)
        {
            dwb.UpdateHtml(currentWord, autoCorrectList);
        }

        private void UpdateHtml()
        {
            foreach (var dwb in dwbList)
                UpdateHtml(dwb);
        }

        protected override void OnRowEnter()
        {
            if (currentDWB == null) return;
            EnableToolStripButtons();
            UpdateHtml();
        }

        private void SelectWord(int index)
        {
            var count = dataGridView.Rows.Count;
            if (count == 0) return;
            dataGridView.CurrentCell = dataGridView.Rows[(index + count) % count].Cells[dataGridView.CurrentCell.ColumnIndex];
        }

        private void refreshToolStripButton_Click(object sender, EventArgs e)
        {
            DoUpdateFilter();
            FillTable();
            //filterHelper.AddTextToList();
        }

        private void deleteTranslationToolStripButton_Click(object sender, EventArgs e)
        {
            if(currentDWB.DoDeleteTranslation(currentWord))
                UpdateHtml(currentDWB);
        }

        private void dictsToolStripButton_Click(object sender, EventArgs e)
        {
            var dlg = new ConfigDictDlg(config, uiDicts);
            if (dlg.ShowDialog() != DialogResult.OK) return;
            uiDicts = dlg.uiDicts;
            FillDicts();
        }

        private void extractToolStripButton_Click(object sender, EventArgs e)
        {
            if (currentDWB.DoExtractTranslation(currentWord, sender == extractOverriteToolStripButton))
                UpdateHtml(currentDWB);
        }

        private void editTranslationtoolStripButton_Click(object sender, EventArgs e)
        {
            if (currentDWB.DoEditTranslation(currentWord))
                UpdateHtml(currentDWB);
        }

        private void DoUpdateFilter()
        {
            if(filtertoolStripLabel.Owner != null)
                filtertoolStripLabel.Text =
                    filter == "" ? "No Filter" : string.Format("Filter: \"{0}\"", filter);
        }

        private void setFilterToolStripButton_Click(object sender, EventArgs e)
        {
            var dlg = new FilterDlg(autoCorrectList);
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                filter = dlg.Filter;
                filterScope = dlg.FilterScope;
                refreshToolStripButton.PerformClick();
            }
        }

        private void removeFilterToolStripButton_Click(object sender, EventArgs e)
        {
            filter = "";
            filterScope = 0;
            refreshToolStripButton.PerformClick();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == Win32.WM_KEYDOWN || msg.Msg == Win32.WM_SYSKEYDOWN)
            {
                switch (keyData)
                {
                    case Keys.Control | Keys.Shift | Keys.O:
                        extractOverriteToolStripButton.TryPerformClick();
                        return true;
                    case Keys.Control | Keys.Shift | Keys.D:
                        deleteTranslationToolStripButton.TryPerformClick();
                        return true;
                    case Keys.Control | Keys.Shift | Keys.F:
                        setFilterToolStripButton.TryPerformClick();
                        return true;
                    case Keys.Control | Keys.Shift | Keys.V:
                        removeFilterToolStripButton.TryPerformClick();
                        return true;
                    case Keys.Alt | Keys.Left:
                        //if(!webBrowser1.Focused)
                        //    SelectNextDict(false, false);
                        return true;
                    case Keys.Alt | Keys.Right:
                        //if (!webBrowser1.Focused)
                        //    SelectNextDict(true, false);
                        return true;
                    case Keys.Control | Keys.Alt | Keys.Left:
                        SelectNextDict(false, true);
                        return true;
                    case Keys.Control | Keys.Alt | Keys.Right:
                        SelectNextDict(true, true);
                        return true;
                    case Keys.Alt | Keys.D0:
                        SelectDict(0);
                        return true;
                    case Keys.Alt | Keys.D1:
                    case Keys.Alt | Keys.D2:
                    case Keys.Alt | Keys.D3:
                    case Keys.Alt | Keys.D4:
                    case Keys.Alt | Keys.D5:
                    case Keys.Alt | Keys.D6:
                    case Keys.Alt | Keys.D7:
                    case Keys.Alt | Keys.D8:
                    case Keys.Alt | Keys.D9:
                        //if (keyData - (Keys.Alt | Keys.D1) < offsetConjugator - offsetOffline)
                        //    SelectDict(keyData - (Keys.Alt | Keys.D1) + offsetOffline);
                        return true;
                    case Keys.Control | Keys.Alt | Keys.D0:
                        //SelectDict(offsetConjugator);
                        return true;
                    case Keys.Control | Keys.Alt | Keys.D1:
                    case Keys.Control | Keys.Alt | Keys.D2:
                    case Keys.Control | Keys.Alt | Keys.D3:
                    case Keys.Control | Keys.Alt | Keys.D4:
                    case Keys.Control | Keys.Alt | Keys.D5:
                    case Keys.Control | Keys.Alt | Keys.D6:
                    case Keys.Control | Keys.Alt | Keys.D7:
                    case Keys.Control | Keys.Alt | Keys.D8:
                    case Keys.Control | Keys.Alt | Keys.D9:
                        //if (keyData - (Keys.Control | Keys.Alt | Keys.D1) < offsetConjugator - offsetOffline)
                        //    SelectDict(keyData - (Keys.Control | Keys.Alt | Keys.D1) + offsetOnline);
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //var dwb = (DictWebBrowser)sender;
            //if (dwb.ReadyState == WebBrowserReadyState.Complete &&
            //    (dwb.dictImage == DictImage.Online || dwb.dictImage == DictImage.Web) &&
            //    dwb.dictRow.AUTOMATION != null && !dwb.automationDone)
            //{
            //    dwb.DoWebAutomation(dwb.dictRow.AUTOMATION, currentWord);
            //    dwb.automationDone = true;
            //}
        }

        #region ILangBookUnits Members

        public override void UpdatelbuSettings()
        {
            uiDicts.Clear();
            base.UpdatelbuSettings();
        }

        #endregion
    }
}
