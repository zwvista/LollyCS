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
    public partial class WordsUnitsForm : WordsWebForm
    {
        private int deletedID = 0;
        private string deletedWord = "";
        private BindingList<MWORDUNIT> wordsList;

        public WordsUnitsForm()
        {
            InitializeComponent();
            dataGridView = dataGridView1;
            bindingNavigator1.BindingSource = bindingSource1;

            bindingNavigator1.Items.Remove(filterToolStripSeparator);
            bindingNavigator1.Items.Remove(setFilterToolStripButton);
            bindingNavigator1.Items.Remove(removeFilterToolStripButton);
            bindingNavigator1.Items.Remove(filtertoolStripLabel);

            bindingNavigator1.Items.Remove(lookupToolStripSeparator);
            bindingNavigator1.Items.Remove(lookupByKanjiToolStripButton);
            bindingNavigator1.Items.Remove(lookupByKanaFilterToolStripButton);
            bindingNavigator1.Items.Remove(findKanaToolStripButton);
            bindingNavigator1.Items.Remove(copyKanjiKanaToolStripButton);

            reorderToolStripButton.Click += reorderToolStripButton_Click;
        }

        protected override void FillTable()
        {
            wordsList = new BindingList<MWORDUNIT>(Program.db.WordsUnits_GetDataByBookUnitParts(lbuSettings.BookID,
                lbuSettings.UnitPartFrom, lbuSettings.UnitPartTo));
            bindingSource1.DataSource = wordsList;
            autoCorrectList = Program.db.AutoCorrect_GetDataByLang(lbuSettings.LangID);
        }

        private void InsertWordIfNeeded(string word)
        {
            var count = Program.db.WordsLang_GetWordCount(lbuSettings.LangID, word);
            if (count == 0)
                Program.db.WordsLang_Insert(lbuSettings.LangID, word);
        }

        private void DeleteWordIfNeeded(string word)
        {
            var count = Program.db.WordsBooks_GetWordCount(lbuSettings.LangID, word);
            if (count == 0)
            {
                var msg = string.Format("The word \"{0}\" is about to be DELETED from the language \"{1}\". Are you sure?",
                    word, lbuSettings.LangName);
                if (MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Program.db.WordsLang_Delete(lbuSettings.LangID, word);
            }
        }

        protected override void OnDeleteWord()
        {
            deletedID = wordsList[bindingSource1.Position].ID;
            deletedWord = currentWord;
            bindingSource1.RemoveCurrent();
        }

        public override void UpdatelbuSettings()
        {
            base.UpdatelbuSettings();
            Text = string.Format("Words ({0})", lbuSettings.BookUnitsDesc);
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.ColumnIndex != 0) return;
            //bool ascending = dataGridView1.SortedColumn.Index != 0 ||
            //    dataGridView1.SortOrder == SortOrder.Descending;
            //bindingSource1.Sort = ascending ? "UNIT, PART, ORD" : "UNIT DESC, PART, ORD DESC";
        }

        private void reorderToolStripButton_Click(object sender, EventArgs e)
        {
            var objs = (from row in wordsList
                        where row.ID != 0
                        orderby row.ORD
                        select new ReindexObject(row.ID, row.WORD)).ToArray();
            var dlg = new ReorderDlg(objs);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                foreach (var obj in objs)
                    Program.db.WordsUnits_UpdateOrd(obj.ORD, obj.ID);
                refreshToolStripButton.PerformClick();
            }
        }

        private void bindingSource1_ListItemDeleted(object sender, ListChangedEventArgs e)
        {
            if (deletedID == 0) return;

            Program.db.WordsUnits_Delete(deletedID);
            DeleteWordIfNeeded(deletedWord);

            deletedID = 0;
            deletedWord = "";
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (!bindingSource1.ListRowChanged) return;

            var row = wordsList[e.RowIndex];
            if (row.ID == 0)
            {
                row.BOOKID = lbuSettings.BookID;
                if (row.UNIT == 0)
                    row.UNIT = lbuSettings.UnitTo;
                if (row.PART == 0)
                    row.PART = lbuSettings.PartTo;
                if (row.ORD == 0)
                    row.ORD = e.RowIndex + 1;
                row.WORD = Program.AutoCorrect(row.WORD, autoCorrectList);
                row.ID = Program.db.WordsUnits_Insert(row);
                dataGridView1.Refresh();

                InsertWordIfNeeded(row.WORD);
            }
            else
            {
                row.WORD = Program.AutoCorrect(row.WORD, autoCorrectList);
                Program.db.WordsUnits_Update(row);
                if (currentWord != row.WORD)
                {
                    DeleteWordIfNeeded(currentWord);
                    InsertWordIfNeeded(row.WORD);
                }
            }
        }
    }

}
