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
    public partial class WordsLangForm : WordsWebForm, ILangBookLessons
    {
        private string deletedWord = "";
        private BindingList<MWORDLANG> wordsList;
        
        public WordsLangForm()
        {
            InitializeComponent();
            dataGridView = dataGridView1;
            bindingNavigator1.Items.Remove(multiAddToolStripButton);
            bindingNavigator1.Items.Remove(reindexToolStripButton);
            bindingNavigator1.BindingSource = bindingSource1;
        }

        protected override void FillTable()
        {
            wordsList = new BindingList<MWORDLANG>(
                filterScope == 0 ? WordsLang.GetDataByLangWord(lblSettings.LangID, filter) :
                WordsLang.GetDataByLangTranslationDictTables(lblSettings.LangID, filter, dictTablesOffline)
            );
            bindingSource1.DataSource = wordsList;
            autoCorrectList = AutoCorrect.GetDataByLang(lblSettings.LangID);
        }

        protected override void OnDeleteWord()
        {
            deletedWord = currentWord;
            bindingSource1.RemoveCurrent();
        }

        public override void UpdatelblSettings()
        {
            base.UpdatelblSettings();
            Text = string.Format("Words ({0})", lblSettings.LangDesc);
        }

        private void bindingSource1_ListItemDeleted(object sender, ListChangedEventArgs e)
        {
            if (deletedWord == "") return;

            WordsLang.Delete(lblSettings.LangID, deletedWord);
            deletedWord = "";
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (!bindingSource1.ListRowChanged) return;

            var row = wordsList[e.RowIndex];
            if (row.LANGID == 0)
            {
                row.LANGID = lblSettings.LangID;
                row.WORD = Program.AutoCorrect(row.WORD, autoCorrectList);
                WordsLang.Insert(row.LANGID, row.WORD);
            }
            else
            {
                row.WORD = Program.AutoCorrect(row.WORD, autoCorrectList);
                WordsLang.Update(row.WORD, row.LANGID, currentWord);
            }
        }
    }
}
