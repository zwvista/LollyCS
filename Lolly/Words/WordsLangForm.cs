﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LollyShared;
using Equin.ApplicationFramework;

namespace Lolly
{
    public partial class WordsLangForm : WordsWebForm, ILangBookUnits
    {
        private string deletedWord = "";
        private BindingList<MWORDLANG> wordsList;
        private BindingListView<MWORDLANG> wordsView;
        
        public WordsLangForm()
        {
            InitializeComponent();
            dataGridView = dataGridView1;
            bindingNavigator1.Items.Remove(multiAddToolStripButton);
            bindingNavigator1.Items.Remove(reorderToolStripButton);
            bindingNavigator1.BindingSource = bindingSource1;
        }

        protected override void FillTable()
        {
            wordsList = new BindingList<MWORDLANG>(
                filterScope == 0 ? LollyDB.WordsLang_GetDataByLangWord(lbuSettings.LangID, filter) :
                LollyDB.WordsLang_GetDataByLangTranslationDictTables(lbuSettings.LangID, filter, config.dictTablesOffline)
            );
            bindingSource1.DataSource = wordsView = new BindingListView<MWORDLANG>(wordsList);
            autoCorrectList = LollyDB.AutoCorrect_GetDataByLang(lbuSettings.LangID);
        }

        protected override void OnDeleteWord()
        {
            deletedWord = currentWord;
            bindingSource1.RemoveCurrent();
        }

        public override void UpdatelbuSettings()
        {
            base.UpdatelbuSettings();
            Text = $"Words ({lbuSettings.LangDesc})";
        }

        private void bindingSource1_ListItemDeleted(object sender, ListChangedEventArgs e)
        {
            if (deletedWord == "") return;

            LollyDB.WordsLang_Delete(lbuSettings.LangID, deletedWord);
            deletedWord = "";
        }

        private void bindingSource1_ListItemAdded(object sender, ListChangedEventArgs e)
        {
            if (wordsList.Count < wordsView.Count) return;

            var row = wordsList.Last();
            if (row.LANGID == 0)
            {
                row.LANGID = lbuSettings.LangID;
                row.WORD = Program.AutoCorrect(row.WORD, autoCorrectList);
                LollyDB.WordsLang_Insert(row.LANGID, row.WORD);
                dataGridView.Refresh();
            }
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (!bindingSource1.ListRowChanged) return;

            var row = wordsList[e.RowIndex];
            row.WORD = Program.AutoCorrect(row.WORD, autoCorrectList);
            LollyDB.WordsLang_Update(row.WORD, row.LANGID, currentWord);
        }
    }
}
