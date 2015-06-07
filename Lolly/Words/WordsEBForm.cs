using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LollyShared;

namespace Lolly
{
    public partial class WordsEBForm : WordsBaseForm
    {
        protected EBWin ebwin;
        private string currentDict;

        public WordsEBForm()
        {
            InitializeComponent();
            bindingNavigator1.Items.Remove(translationToolStripSeparator);
            bindingNavigator1.Items.Remove(extractOverriteToolStripButton);
            bindingNavigator1.Items.Remove(extractAppendToolStripButton);
            bindingNavigator1.Items.Remove(editTranslationtoolStripButton);
            bindingNavigator1.Items.Remove(deleteTranslationToolStripButton);
            findKanaToolStripButton.Click += findKanaToolStripButton_Click;
            copyKanjiKanaToolStripButton.Click += copyKanjiKanaToolStripButton_Click;
        }

        private void WordsEBForm_Shown(object sender, EventArgs e)
        {
            if (DesignMode == false)
            {
                ebwin = new EBWin(applicationControl1.AppHandle);
                UpdatelbuSettings();
                Win32.SetForegroundWindow(this.Handle);
            }
        }

        protected override void OnDictSelected(int index)
        {
            currentDict = ((ToolStripButton)dictsToolStrip.Items[index]).Text;
            ebwin.ChooseDict(currentDict);
            UpdateEBView();
        }

        private void SelectNextDict(bool forward)
        {
            SelectNextDict(forward, dictsToolStrip.Items.Count);
        }

        protected override void FillDicts()
        {
            dictsToolStrip.Items.Clear();
            dictsToolStrip.Tag = -1;

            if (config.dictsEBWin.Any())
            {
                foreach (var dictName in config.dictsEBWin)
                    AddDict(new UIDictItem
                    {
                        Name = dictName,
                        ImageIndex = DictImage.Offline
                    });
                SelectDict(0);
            }
        }

        private void UpdateEBView()
        {
            if (currentWord != "")
            {
                ebwin.LookUp(currentWord);
                ebwin.SelectEntry(currentNote);
            }
        }

        protected override void OnRowEnter()
        {
            copyKanjiKanaToolStripButton.Enabled = currentWord != "";
            if (currentDict != "")
                UpdateEBView();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == Win32.WM_KEYDOWN || msg.Msg == Win32.WM_SYSKEYDOWN)
            {
                switch (keyData)
                {
                    case Keys.Control | Keys.Shift | Keys.F:
                        findKanaToolStripButton.TryPerformClick();
                        return true;
                    case Keys.Control | Keys.Shift | Keys.H:
                        copyKanjiKanaToolStripButton.TryPerformClick();
                        return true;
                    case Keys.Alt | Keys.Left:
                        SelectNextDict(false);
                        return true;
                    case Keys.Alt | Keys.Right:
                        SelectNextDict(true);
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
                        //if (keyData - (Keys.Alt | Keys.D1) < dictToolStripDropDownButton.DropDownItems.Count)
                        //    SelectDict(keyData - (Keys.Alt | Keys.D1));
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnAddComplete()
        {
            findKanaToolStripButton.PerformClick();
            dataGridView.MoveToAddNew();
        }

        protected virtual void OnFindKanas()
        {
        }

        private void findKanaToolStripButton_Click(object sender, EventArgs e)
        {
            if (dataGridView.RowCount < 2) return;
            ebwin.ChooseDict("三省堂　スーパー大辞林");
            OnFindKanas();
            bindingNavigatorAddNewItem.PerformClick();
        }

        private void copyKanjiKanaToolStripButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(
                currentWord != currentNote ? $"{currentWord}（{currentNote}）：" : $"{currentWord}：");
        }
    }

}
