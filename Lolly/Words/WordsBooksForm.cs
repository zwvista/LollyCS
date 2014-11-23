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
    public partial class WordsBooksForm : WordsWebForm, ILangBookUnits
    {
        private string deletedWord = "";
        private BindingList<MWORDBOOK> wordsList;

        public WordsBooksForm()
        {
            InitializeComponent();
            dataGridView = dataGridView1;
            bindingNavigator1.Items.Remove(bindingNavigatorAddNewItem);
            bindingNavigator1.Items.Remove(multiAddToolStripButton);
            bindingNavigator1.Items.Remove(reindexToolStripButton);
            bindingNavigator1.BindingSource = bindingSource1;
        }

        protected override void FillTable()
        {
            wordsList = new BindingList<MWORDBOOK>(
                filterScope == 0 ? WordsBooks.GetDataByLangWord(lbuSettings.LangID, filter) :
                WordsBooks.GetDataByLangTranslationDictTables(lbuSettings.LangID, filter, config.dictTablesOffline)
            );
            bindingSource1.DataSource = wordsList;
            autoCorrectList = AutoCorrect.GetDataByLang(lbuSettings.LangID);
        }

        protected override void OnDeleteWord()
        {
            deletedWord = currentWord;
            bindingSource1.RemoveCurrent();
        }

        public override void UpdatelbuSettings()
        {
            base.UpdatelbuSettings();
            Text = string.Format("Words (All Books on Learning {0})", lbuSettings.LangDesc);
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.ColumnIndex != 0) return;
            //bool ascending = dataGridView1.SortedColumn.Index != 0 ||
            //    dataGridView1.SortOrder == SortOrder.Descending;
            //bindingSource1.Sort = ascending ? "BOOKNAME,UNIT, ORD" : "BOOKNAME DESC, UNIT DESC, ORD DESC";
        }

        private void bindingSource1_ListItemDeleted(object sender, ListChangedEventArgs e)
        {
            //UpdateTable();
            //var count = queriesTableAdapter.WORDSBOOKGetWordCount(lbuSettings.LangID, deletedWord).Value;
            //if (count == 0)
            //{
            //    var msg = string.Format("The word \"{0}\" is about to be DELETED from the language \"{1}\". Are you sure?",
            //        deletedWord, lbuSettings.LangName);
            //    if (MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //        wORDSTRANSTableAdapter.Delete(lbuSettings.LangID, deletedWord);
            //}
        }
    }
}
