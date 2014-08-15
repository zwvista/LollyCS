﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Speech.Synthesis;
using LollyBase;

namespace Lolly
{
    public partial class PhrasesBaseForm : Form, ILangBookLessons
    {
        protected string phrase = "";
        protected string translation = "";
        protected LangBookLessonSettings lblSettings;
        protected DataGridView dataGridView;
        protected int filterScope = 0;
        protected string filter = "";
        protected IEnumerable<MAUTOCORRECT> autoCorrectList;

        private IEnumerable<KeyValuePair<string, string>> replacement;
        private IEnumerable<KeyValuePair<string, string>> replacementChn;

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
                    filter == "" ? "No Filter" : string.Format("Filter: \"{0}\"", filter);
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
                    var pattern = string.Format(@"(\W|^)({0})(\W|$)", kv.Key);
                    var evaluator = string.Format("$1{0}$3", kv.Value);
                    phrase2 = new Regex(pattern).Replace(phrase2, evaluator);
                }
                Program.AddPropmt(pb, lblSettings.LangID, phrase2);
            }
            if (speakTranslation)
            {
                var translation2 = translation;
                foreach (var kv in replacementChn)
                    translation2 = translation2.Replace(kv.Key, kv.Value);
                Program.AddPropmt(pb, 0, translation2);
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

        #region ILangBookLessons Members

        public virtual void UpdatelblSettings()
        {
            lblSettings = Program.lblSettings;

            bool canSpeak = Program.CanSpeak(lblSettings.LangID);
            speakToolStripButton.Enabled = keepSpeakToolStripButton.Enabled = canSpeak;

            Func<int, IEnumerable<KeyValuePair<string, string>>> GetReplacement = langID =>
                Program.GetConfig(langID).Elements("phraseReplace")
                .Select(elem => new KeyValuePair<string, string>(
                    (string)elem.Attribute("before"),
                    (string)elem.Attribute("after")
                ));

            replacement = GetReplacement(lblSettings.LangID);
            replacementChn = GetReplacement(0);

            FillTable();
        }

        #endregion
    }
}