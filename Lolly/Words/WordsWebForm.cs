﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using LollyBase;

namespace Lolly
{
    public partial class WordsWebForm : WordsBaseForm
    {
        private string[] dictsLingoes;
        private Dictionary<string, DictInfo[]> dictsCustom;
        private string[] dictsOffline;
        
        private BoolRef webBrowser1_Enter_done = new BoolRef(true);

        private List<DictWebBrowser> dwbList = new List<DictWebBrowser>();
        private DictWebBrowser currentDWB;

        protected int filterScope = 0;
        protected string filter = "";
        protected string[] dictTablesOffline;
        protected List<MAUTOCORRECT> autoCorrectList;
        protected List<MDICTALL> dictAllList;

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
        }

        private void WordsBaseForm_Shown(object sender, EventArgs e)
        {
            if (DesignMode == false)
                UpdatelbuSettings();
        }

        private void EnableToolStripButtons()
        {
            deleteTranslationToolStripButton.Enabled = currentWord != "" &&
                (currentDWB.dictImage == DictImage.Offline || currentDWB.dictName == DictNames.DEFAULT);
            editTranslationtoolStripButton.Enabled = currentWord != "" && currentDWB.dictImage == DictImage.Offline;
            extractOverriteToolStripButton.Enabled = currentWord != "" &&
                (currentDWB.dictImage == DictImage.Offline || currentDWB.dictImage == DictImage.Online || currentDWB.dictName == DictNames.DEFAULT);
            extractAppendToolStripButton.Enabled = currentWord != "" && currentDWB.dictImage == DictImage.Online;
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

        protected override void AddDict(string dictName, int imageIndex)
        {
            base.AddDict(dictName, imageIndex);

            var dwb = new DictWebBrowser();
            ((Control)dwb).Enter += webBrowser1_Enter;
            dwb.DocumentCompleted += webBrowser1_DocumentCompleted;
            dwb.PreviewKeyDown += webBrowser1_PreviewKeyDown;
            dwb.Dock = DockStyle.Fill;
            dwb.ScriptErrorsSuppressed = true;
            splitContainer1.Panel2.Controls.Add(dwb);
            dwbList.Add(dwb);

            dwb.dictName = dictName;
            dwb.FindDict(dictAllList, dictName);
            var dictImage = (DictImage)imageIndex;
            if (dictImage >= DictImage.Offline && dictImage < DictImage.Offline + 9)
                dictImage = DictImage.Offline;
            else if (dictImage >= DictImage.Online && dictImage < DictImage.Online + 9)
                dictImage = DictImage.Online;
            else if (dictImage >= DictImage.Live && dictImage < DictImage.Live + 9)
                dictImage = DictImage.Live;
            dwb.dictImage = dictImage;

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
            dictToolStripDropDownButton.DropDownItems.Clear();
            dictsToolStrip.Items.Clear();
            dictsToolStrip.Tag = -1;
            var dictGroups = new List<string>();

            Action<DictImage, string, string[]> AddDictGroups = (dt, dictGroup, dictNames) =>
            {
                if (!dictNames.Any()) return;

                var item = new ToolStripMenuItem(dictGroup);
                int index = ~dictGroups.BinarySearch(dictGroup);
                dictGroups.Insert(index, dictGroup);
                dictToolStripDropDownButton.DropDownItems.Insert(index, item);

                int imageIndex = (int)dt;
                foreach (var dictName in dictNames)
                {
                    var item2 = (ToolStripMenuItem)item.DropDownItems.Add(dictName, imageList1.Images[imageIndex], (sender, e) =>
                    {
                        var item3 = sender as ToolStripMenuItem;
                        if (item3.Text == DictNames.DEFAULT) return;
                        if (item3.CheckState == CheckState.Checked)
                            AddDict(item3.Text, (int)item3.Tag);
                        else
                            RemoveDict(item3.Text, (int)item3.Tag);
                    });
                    item2.Tag = imageIndex;
                    item2.CheckOnClick = true;
                    if (dt == DictImage.Offline || dt == DictImage.Online || dt == DictImage.Live)
                        imageIndex++;
                }
            };

            var elemDicts = Program.GetConfigDicts(lbuSettings.LangID);

            // custom
            var elems = elemDicts.Elements("custom");
            var dictNamesCustom = elems.Select(elem => (string)elem.Attribute("name")).ToArray();
            AddDictGroups(DictImage.Custom, "Custom", dictNamesCustom);
            dictsCustom = elems.Select(elem => new
                {
                    Key = (string)elem.Attribute("name"),
                    Value = elem.Elements("dict").Select(elem2 => new DictInfo(elem2)).ToArray()
                }).ToDictionary(elem => elem.Key, elem => elem.Value);

            // group
            var groupDictInfo = Program.config.Element("dictInfo").Elements("group");
            var dictsGroup = elemDicts.Elements("group");
            var dictsGroupWithInfo =
                from info in groupDictInfo
                join dict in dictsGroup on (string)info equals (string)dict
                select new
                {
                    Name = (string)dict,
                    DictType = (string)info.Attribute("dictType"),
                    ImageIndex = (DictImage)Enum.Parse(typeof(DictImage), (string)dict)
                };
            foreach (var group in dictsGroupWithInfo)
            {
                dictAllList = 
                    group.Name == DictNames.WEB ? DictAll.GetDataByLangWeb(lbuSettings.LangID) :
                    DictAll.GetDataByLangDictType(lbuSettings.LangID, group.DictType);
                var dictNames = (from row in dictAllList select row.DICTNAME).ToArray();
                if (group.Name == DictNames.OFFLINE)
                {
                    dictsOffline = (from row in dictAllList select row.DICTNAME).ToArray();
                    dictTablesOffline = (from row in dictAllList select row.DICTTABLE).ToArray();
                }
                AddDictGroups(group.ImageIndex, group.Name, dictNames);
            }

            // lingoes + special
            dictsLingoes = elemDicts.Elements("lingoes").Select(elem => (string)elem).ToArray();
            var dictsSpecial = elemDicts.Elements("special").Select(elem => (string)elem).ToList();
            if (dictsLingoes.Any())
                dictsSpecial.Add("Lingoes");
            AddDictGroups(DictImage.Special, "Special", dictsSpecial.OrderBy(s => s).ToArray());

            dictAllList = DictAll.GetDataByLang(lbuSettings.LangID);
            AddDict(DictNames.DEFAULT, (int)DictImage.Custom);
            SelectDict(0);
        }

        private void UpdateHtml(DictWebBrowser dwb)
        {
            dwb.UpdateHtml(currentWord, autoCorrectList, dictAllList, dictsLingoes, dictsOffline, dictsCustom);
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
            Action DeleteTranslation = () =>
                DictEntity.Update(ExtensionClass.NOTRANS, currentWord, currentDWB.dictRow.DICTTABLE);

            if (currentDWB.dictName == DictNames.DEFAULT)
            {
                var msg = string.Format("ALL translations of the word \"{0}\" are about to be DELETED. Are you sure?", currentWord);
                if (MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    foreach (var dictA in dictsOffline)
                    {
                        currentDWB.FindDict(dictAllList, dictA);
                        DeleteTranslation();
                    }
            }
            else
            {
                var msg = string.Format("The translation of the word \"{0}\" in the dictionary \"{1}\"" +
                    "is about to be DELETED. Are you sure?", currentWord, currentDWB.dictName);
                if (MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    DeleteTranslation();
            }
        }

        private void extractToolStripButton_Click(object sender, EventArgs e)
        {
            var msg = "";
            if (currentDWB.dictName == DictNames.DEFAULT)
            {
                if (!currentDWB.emptyTrans)
                    msg = string.Format("ALL of the translations of the word \"{0}\" " +
                        "are about to be DELETED and EXTRACTED from the web again. Are you sure?", currentWord);
                if (msg == "" || MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    ((MDIForm)Parent.Parent).ExtractTranslation(new[] { currentWord }, dictsOffline, true);
            }
            else
            {
                if (currentDWB.dictImage == DictImage.Online)
                    msg = string.Format("The translation from the url \"{0}\" is about to be EXTRACTED and {1} " +
                        "the translation of the word \"{2}\" in the dictionary \"{3}\". Are you sure?",
                        currentDWB.Url.AbsoluteUri,
                        sender == extractOverriteToolStripButton ? "used to REPLACE" : "APPENDED to",
                        currentWord, currentDWB.dictName);
                else if (!currentDWB.emptyTrans)
                    msg = string.Format("The translation of the word \"{0}\" in the dictionary \"{1}\" " +
                        "is about to be DELETED and EXTRACTED from the web again. Are you sure?",
                        currentWord, currentDWB.dictName);
                if (msg == "" || MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    if (currentDWB.dictImage == DictImage.Online)
                    {
                        currentDWB.wordRow = Program.OpenDictTable(currentWord, currentDWB.dictRow.DICTTABLE);
                        Program.UpdateDictTable(currentDWB, currentDWB.wordRow, currentDWB.dictRow, sender == extractAppendToolStripButton);
                    }
                    else
                        ((MDIForm)Parent.Parent).ExtractTranslation(new[] { currentWord }, new[] { currentDWB.dictName }, true);
            }
        }

        private void editTranslationtoolStripButton_Click(object sender, EventArgs e)
        {
            var dlg = new EditTransDlg();
            dlg.translationTextBox.Text = currentDWB.wordRow.TRANSLATION;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DictEntity.Update(dlg.translationTextBox.Text, currentWord, currentDWB.dictRow.DICTTABLE);
                UpdateHtml();
            }
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
            var dwb = (DictWebBrowser)sender;
            if (dwb.ReadyState == WebBrowserReadyState.Complete &&
                (dwb.dictImage == DictImage.Online || dwb.dictImage == DictImage.Web) &&
                dwb.dictRow.AUTOMATION != null && !dwb.automationDone)
            {
                dwb.DoWebAutomation(dwb.dictRow.AUTOMATION, currentWord);
                dwb.automationDone = true;
            }
        }

        #region ILangBookUnits Members

        public override void UpdatelbuSettings()
        {
            currentDWB = null;
            foreach (var dwb in dwbList)
                dwb.Dispose();
            dwbList.Clear();
            //dictsLingoes = null;
            //dictsCustom = null;
            //dictsOffline = null;
            //dictTablesOffline = null;

            base.UpdatelbuSettings();
        }

        #endregion
    }
}
