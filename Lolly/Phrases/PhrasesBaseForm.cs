using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Speech.Synthesis;
using LollyShared;

namespace Lolly
{
    public partial class PhrasesBaseForm : Form, ILangBookUnits
    {
        protected string phrase = "";
        protected string translation = "";
        protected LangBookUnitSettings lbuSettings;
        protected DataGridView dataGridView;
        protected int filterScope = 0;
        protected string filter = "";
        protected List<MAUTOCORRECT> autoCorrectList;

        private List<KeyValuePair<string, string>> replacement;
        private List<KeyValuePair<string, string>> replacementChn;

        public PhrasesBaseForm()
        {
            InitializeComponent();
        }

        private void PhrasesBaseForm_Load(object sender, EventArgs e)
        {
            if (dataGridView != null)
                dataGridView.RowEnter += dataGridView_RowEnter;
            navigateForwardToolStripMenuItem.PerformClick();
        }

        protected virtual void OnAddPhrase(string phrase, string translation)
        {

        }

        private void multiAddToolStripButton_Click(object sender, EventArgs e)
        {
            var dlg = new NewPhrasesDlg();
            if (dlg.ShowDialog() != DialogResult.OK) return;
            var pts = dlg.PhrasesTranslations;
            for (int i = 0; i < pts.Length; i += 2)
                OnAddPhrase(pts[i], pts[i + 1]);
            bindingNavigatorAddNewItem.PerformClick();
        }

        protected virtual void FillTable()
        {
        }

        private void refreshToolStripButton_Click(object sender, EventArgs e)
        {
            DoUpdateFilter();
            FillTable();
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
                    case Keys.Control | Keys.Shift | Keys.F:
                        setFilterToolStripButton.TryPerformClick();
                        return true;
                    case Keys.Control | Keys.Shift | Keys.V:
                        removeFilterToolStripButton.TryPerformClick();
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void DoUpdateFilter()
        {
            if (filtertoolStripLabel.Owner != null)
                filtertoolStripLabel.Text =
                    filter == "" ? "No Filter" : $"Filter: \"{filter}\"";
        }

        private void setFilterToolStripButton_Click(object sender, EventArgs e)
        {
            var dlg = new FilterDlg(autoCorrectList);
            if (dlg.ShowDialog() == DialogResult.OK)
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

        private void dataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.CurrentRow != null && dataGridView.CurrentRow.Index == e.RowIndex)
                return;
            var isNewRowIndex = e.RowIndex == dataGridView.NewRowIndex;
            bindingNavigatorAddNewItem.Enabled = bindingNavigatorDeleteItem.Enabled = !isNewRowIndex;
            var cellValue = dataGridView.Rows[e.RowIndex].Cells["phraseColumn"].Value;
            phrase = isNewRowIndex || cellValue == null ? "" : cellValue.ToString();
            cellValue = dataGridView.Rows[e.RowIndex].Cells["translationColumn"].Value;
            translation = isNewRowIndex || cellValue == null ? "" : cellValue.ToString();
            SpeakPhraseTrans(keepSpeakToolStripButton.Checked, keepSpeakTranslationToolStripButton.Checked);
        }

        private void speakToolStripButton_Click(object sender, EventArgs e)
        {
            SpeakPhraseTrans(true, false);
        }

        private void speakTranslationToolStripButton_Click(object sender, EventArgs e)
        {
            SpeakPhraseTrans(false, true);
        }

        private void keepSpeakToolStripButton_Click(object sender, EventArgs e)
        {
            if (keepSpeakToolStripButton.Checked)
                speakToolStripButton.PerformClick();
        }

        private void keepSpeakTranslationToolStripButton_Click(object sender, EventArgs e)
        {
            if (keepSpeakTranslationToolStripButton.Checked)
                speakTranslationToolStripButton.PerformClick();
        }

        private void SpeakPhraseTrans(bool speakPhrase, bool speakTranslation)
        {
            if (!speakPhrase && !speakTranslation || phrase == "" && translation == "")
                return;

            var pb = new PromptBuilder();
            if (speakPhrase)
            {
                var phrase2 = phrase;
                foreach(var kv in replacement)
                {
                    var pattern = $@"(\W|^)({kv.Key})(\W|$)";
                    var evaluator = $"$1{kv.Value}$3";
                    phrase2 = new Regex(pattern).Replace(phrase2, evaluator);
                }
                Program.AddPrompt(pb, lbuSettings.LangID, phrase2);
            }
            if (speakTranslation)
            {
                var translation2 = translation;
                foreach (var kv in replacementChn)
                    translation2 = translation2.Replace(kv.Key, kv.Value);
                Program.AddPrompt(pb, 0, translation2);
            }

            Program.Speak(pb);
        }

        private void navigateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            navigateToolStripSplitButton.Image = item.Image;
        }

        private void navigateToolStripSplitButton_Click(object sender, EventArgs e)
        {
            var pos = int.Parse(bindingNavigatorPositionItem.Text);
            var count = int.Parse(bindingNavigatorCountItem.Text.Substring(2));
            var index =
                navigateToolStripSplitButton.Image == navigateForwardToolStripMenuItem.Image ?
                    pos % count :
                navigateToolStripSplitButton.Image == naviagetBackwardToolStripMenuItem.Image ?
                    (pos - 2 + count) % count :
                new Random().Next(count);
            dataGridView.CurrentCell = dataGridView.Rows[index].Cells[dataGridView.CurrentCell.ColumnIndex];
        }

        private void timerNavigateToolStripButton_Click(object sender, EventArgs e)
        {
            timerNavigateToolStripButton.Checked = !timerNavigateToolStripButton.Checked;
            timerNavigate.Enabled = timerNavigateToolStripButton.Checked;
        }

        private void timerNavigate_Tick(object sender, EventArgs e)
        {
            navigateToolStripSplitButton.PerformButtonClick();
        }

        #region ILangBookUnits Members

        public virtual void UpdatelbuSettings()
        {
            lbuSettings = Program.lbuSettings;

            bool canSpeak = Program.CanSpeak(lbuSettings.LangID);
            speakToolStripButton.Enabled = keepSpeakToolStripButton.Enabled = canSpeak;

            Func<long, List<KeyValuePair<string, string>>> GetReplacement = langID =>
                Program.config.GetDictLangConfig(langID).replacement;

            replacement = GetReplacement(lbuSettings.LangID);
            replacementChn = GetReplacement(0);

            FillTable();
        }

        #endregion
    }
}