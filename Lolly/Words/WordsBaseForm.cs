using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LollyBase;

namespace Lolly
{
    public partial class WordsBaseForm : Form, ILangBookUnits
    {
        protected string currentWord = "";
        protected string currentNote = "";
        protected LangBookUnitSettings lbuSettings;
        protected DataGridView dataGridView;
        protected Dictionary<string, int> wordLevelDic = new Dictionary<string, int>();
        protected DictLangConfig config;

        public WordsBaseForm()
        {
            InitializeComponent();
            timerNavigate.Enabled = timerNavigateToolStripButton.Checked;
        }

        private void WordsBaseForm_Load(object sender, EventArgs e)
        {
            if (dataGridView != null)
            {
                dataGridView.RowEnter += dataGridView_RowEnter;
                dataGridView.CellFormatting += dataGridView1_CellFormatting;
            }

            navigateForwardToolStripMenuItem.PerformClick();
            dictsToolStrip.ImageList = imageList1;
        }

        protected virtual void OnDictSelected(int index)
        {
        }

        protected void SelectDict(int index)
        {
            var oldIndex = (int)dictsToolStrip.Tag;
            if (oldIndex != -1)
                ((ToolStripButton)dictsToolStrip.Items[oldIndex]).Checked = false;

            dictsToolStrip.Tag = index;
            ((ToolStripButton)dictsToolStrip.Items[index]).Checked = true;

            OnDictSelected(index);
        }

        protected void SelectNextDict(bool forward, int numDicts)
        {
            if (numDicts == 0) return;
            //int index = (int)dictToolStripDropDownButton.Tag + (forward ? 1 : -1);
            //index = (index + numDicts) % numDicts;
            //SelectDict(index);
        }

        protected virtual void AddDict(string dictName, int imageIndex)
        {
            var item = dictsToolStrip.Items.Add(dictName, imageList1.Images[imageIndex],
                (sender, e) => SelectDict(dictsToolStrip.Items.IndexOf((ToolStripButton)sender)));
            item.ImageIndex = imageIndex;
        }

        protected virtual int RemoveDict(string dictName, int imageIndex)
        {
            var items = dictsToolStrip.Items.Cast<ToolStripButton>().ToArray();
            int i = Array.FindIndex(items, item => item.Text == dictName && item.ImageIndex == imageIndex);
            if (i != -1)
            {
                if ((int)dictsToolStrip.Tag == i)
                    SelectDict(i - 1);
                dictsToolStrip.Items.RemoveAt(i);
            }
            return i;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            dataGridView.MoveToAddNew();
        }

        protected virtual void OnAddWord(string word)
        {
            dataGridView.MoveToAddNew();
            dataGridView.BeginEdit(false);
            dataGridView.NotifyCurrentCellDirty(true);
            dataGridView.CurrentRow.Cells["wordColumn"].Value = word;
            dataGridView.EndEdit();
        }

        protected virtual void OnAddComplete()
        {
        }

        private void multiAddToolStripButton_Click(object sender, EventArgs e)
        {
            var dlg = new NewWordsDlg();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                dlg.Words.ForEach(OnAddWord);
                dataGridView.MoveToAddNew();
                OnAddComplete();
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            var msg = string.Format("The word \"{0}\" is about to be DELETED. Are you sure?", currentWord);
            if (MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                OnDeleteWord();
        }

        protected virtual void OnDeleteWord()
        {
        }

        protected virtual void OnRowEnter()
        {
        }

        private void dataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.CurrentRow != null && dataGridView.CurrentRow.Index == e.RowIndex)
                return;
            var isNewRowIndex = e.RowIndex == dataGridView.NewRowIndex;
            bindingNavigatorAddNewItem.Enabled = bindingNavigatorDeleteItem.Enabled = !isNewRowIndex;
            var cellValue = dataGridView.Rows[e.RowIndex].Cells["wordColumn"].Value;
            currentWord = isNewRowIndex || cellValue == null ? "" : cellValue.ToString();
            if (dataGridView.Columns["noteColumn"] == null)
                currentNote = "";
            else
            {
                cellValue = dataGridView.Rows[e.RowIndex].Cells["noteColumn"].Value;
                currentNote = isNewRowIndex || cellValue == null ? "" : cellValue.ToString();
            }
            if (keepSpeakToolStripButton.Checked)
                speakToolStripButton.PerformClick();
            OnRowEnter();
        }

        protected virtual void FillTable()
        {
        }

        protected virtual void FillDicts()
        {
        }

        private void SelectWord(int index)
        {
            var count = dataGridView.Rows.Count;
            if (count == 0) return;
            dataGridView.CurrentCell = dataGridView.Rows[(index + count) % count].Cells[dataGridView.CurrentCell.ColumnIndex];
        }

        private void timerNavigate_Tick(object sender, EventArgs e)
        {
            //if (online) return;
            //SelectDict(dictsToolStripComboBox.SelectedIndex + 1);
            //if (dictsToolStripComboBox.SelectedIndex == 0)
            //    SelectWord(dataGridView.CurrentRow.Index + 1);
            navigateToolStripSplitButton.PerformButtonClick();
        }

        private void timerNavigateToolStripButton_Click(object sender, EventArgs e)
        {
            timerNavigateToolStripButton.Checked = !timerNavigateToolStripButton.Checked;
            timerNavigate.Enabled = timerNavigateToolStripButton.Checked;
        }

        private void speakToolStripButton_Click(object sender, EventArgs e)
        {
            Program.Speak(lbuSettings.LangID, currentWord);
        }

        private void keepSpeakToolStripButton_Click(object sender, EventArgs e)
        {
            if (keepSpeakToolStripButton.Checked)
                speakToolStripButton.PerformClick();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == Win32.WM_KEYDOWN || msg.Msg == Win32.WM_SYSKEYDOWN)
            {
                switch (keyData)
                {
                    case Keys.Control | Keys.Shift | Keys.R:
                        refreshToolStripButton.TryPerformClick();
                        return true;
                    case Keys.Control | Keys.Shift | Keys.A:
                        multiAddToolStripButton.TryPerformClick();
                        return true;
                    case Keys.Control | Keys.Shift | Keys.I:
                        reindexToolStripButton.TryPerformClick();
                        return true;
                    case Keys.Alt | Keys.Up:
                        naviagetBackwardToolStripMenuItem.PerformClick();
                        return true;
                    case Keys.Alt | Keys.Down:
                        navigateForwardToolStripMenuItem.PerformClick();
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #region ILangBookUnits Members

        public virtual void UpdatelbuSettings()
        {
            lbuSettings = Program.lbuSettings;
            config = Program.config.GetDictLangConfig(lbuSettings.LangID);
            bool canSpeak = Program.CanSpeak(lbuSettings.LangID);
            speakToolStripButton.Enabled = keepSpeakToolStripButton.Enabled = canSpeak;
            FillTable();
            FillDicts();
        }

        #endregion

        private void navigateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            navigateToolStripSplitButton.Image = ((ToolStripMenuItem)sender).Image;
            navigateToolStripSplitButton.PerformButtonClick();
        }

        private void navigateToolStripSplitButton_Click(object sender, EventArgs e)
        {
            var pos = int.Parse(bindingNavigatorPositionItem.Text);
            var count = int.Parse(bindingNavigatorCountItem.Text.Substring(2));
            if (count == 0) return;
            var index =
                navigateToolStripSplitButton.Image == navigateForwardToolStripMenuItem.Image ?
                    pos % count :
                navigateToolStripSplitButton.Image == naviagetBackwardToolStripMenuItem.Image ?
                    (pos - 2 + count) % count :
                new Random().Next(count);
            dataGridView.CurrentCell = dataGridView.Rows[index].Cells[dataGridView.CurrentCell.ColumnIndex];
        }

        protected virtual void OnWordEntered(string word)
        {
            OnAddWord(word);
            dataGridView.MoveToAddNew();
            OnAddComplete();
        }

        private void wordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var word = wordTextBox.Text.Trim();
                if (word != "")
                    OnWordEntered(word);
                wordTextBox.Clear();
            }
        }

        private void wordTextBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            wordTextBox.SelectAll();
        }

        private int GetWordLevel(string w)
        {
            if (!wordLevelDic.ContainsKey(w))
            {
                var level = WordsLang.GetWordLevel(lbuSettings.LangID, w) ?? 0;
                wordLevelDic.Add(w, level);
            }
            return wordLevelDic[w];
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var o = dataGridView.Rows[e.RowIndex].Cells["wordColumn"].Value;
            if (o == null) return;
            var level = GetWordLevel(o.ToString());
            e.CellStyle.BackColor =
                level == 3 ? Program.options.WordLevelP3BackColor :
                level == 2 ? Program.options.WordLevelP2BackColor :
                level == 1 ? Program.options.WordLevelP1BackColor :
                level == -1 ? Program.options.WordLevelN1BackColor :
                level == -2 ? Program.options.WordLevelN2BackColor :
                level == -3 ? Program.options.WordLevelN3BackColor :
                e.CellStyle.BackColor;
            e.CellStyle.ForeColor =
                level == 0 ? e.CellStyle.ForeColor :
                Color.White;
        }

        private void levelChangeToolStripButton_Click(object sender, EventArgs e)
        {
            var ris = (from DataGridViewCell cell in dataGridView.SelectedCells
                     orderby cell.RowIndex
                     select cell.RowIndex).Distinct().ToList();

            foreach (var i in ris)
            {
                var o = dataGridView.Rows[i].Cells["wordColumn"].Value;
                if (o == null) return;
                var w = o.ToString();
                var level = GetWordLevel(w);
                var delta = sender == levelUpToolStripButton ? 1 : -1;
                var newLevel = Math.Max(-3, Math.Min(3, level + delta));
                if (level != newLevel)
                {
                    WordsLang.UpdateWordLevel(newLevel, lbuSettings.LangID, w);
                    wordLevelDic[w] = newLevel;
                }
            }
            dataGridView.Refresh();
        }

        private void refreshToolStripButton_Click(object sender, EventArgs e)
        {
            wordLevelDic.Clear();
        }

    }
}
