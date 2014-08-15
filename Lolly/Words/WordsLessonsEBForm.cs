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
    public partial class WordsLessonsEBForm : Lolly.WordsEBForm
    {
        private int deletedID = 0;
        private string deletedWord = "";
        private BindingList<MWORDLESSON> wordsList;

        public WordsLessonsEBForm()
        {
            InitializeComponent();
            dataGridView = dataGridView1;
            bindingNavigator1.BindingSource = bindingSource1;
            refreshToolStripButton.Click += refreshToolStripButton_Click;
            reindexToolStripButton.Click += reindexToolStripButton_Click;
        }

        protected override void FillTable()
        {
            wordsList = new BindingList<MWORDLESSON>(WordsLessons.GetDataByBookLessonParts(lblSettings.BookID,
                lblSettings.LessonPartFrom, lblSettings.LessonPartTo));
            bindingSource1.DataSource = wordsList;
        }

        private void InsertWordIfNeeded(string word)
        {
            var count = WordsLang.GetWordCount(lblSettings.LangID, word);
            if (count == 0)
                WordsLang.Insert(lblSettings.LangID, word);
        }

        private void DeleteWordIfNeeded(string word)
        {
            var count = WordsBooks.GetWordCount(lblSettings.LangID, word);
            if (count == 0)
            {
                var msg = string.Format("The word \"{0}\" is about to be DELETED from the language \"{1}\". Are you sure?",
                    word, lblSettings.LangName);
                if (MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    WordsLang.Delete(lblSettings.LangID, word);
            }
        }

        protected override void OnDeleteWord()
        {
            deletedID = wordsList[bindingSource1.Position].ID;
            deletedWord = currentWord;
            bindingSource1.RemoveCurrent();
        }

        public override void UpdatelblSettings()
        {
            base.UpdatelblSettings();
            Text = string.Format("Words(EBWin) ({0})", lblSettings.BookLessonsDesc);
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void refreshToolStripButton_Click(object sender, EventArgs e)
        {
            FillTable();
        }

        private void reindexToolStripButton_Click(object sender, EventArgs e)
        {
            var objs = (from row in wordsList
                        where row.ID != 0
                        orderby row.INDEX
                        select new ReindexObject(row.ID, row.WORD)).ToArray();
            var dlg = new ReindexDlg(objs);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                foreach (var obj in objs)
                    WordsLessons.UpdateIndex(obj.INDEX, obj.ID);
                refreshToolStripButton.PerformClick();
            }
        }

        protected override void OnFindKanas()
        {
            foreach (var row in wordsList)
            {
                if (string.IsNullOrEmpty(row.NOTE))
                    row.NOTE = ebwin.FindKana(row.WORD);
                WordsLessons.UpdateNote(row.NOTE, row.ID);
            }
            dataGridView1.Refresh();
        }

        private void bindingSource1_ListItemDeleted(object sender, ListChangedEventArgs e)
        {
            if (deletedID == 0) return;

            WordsLessons.Delete(deletedID);
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
                row.BOOKID = lblSettings.BookID;
                if (row.LESSON == 0)
                    row.LESSON = lblSettings.LessonTo;
                if (row.PART == 0)
                    row.PART = lblSettings.PartTo;
                if (row.INDEX == 0)
                    row.INDEX = e.RowIndex + 1;
                row.ID = WordsLessons.Insert(row);
                dataGridView1.Refresh();

                InsertWordIfNeeded(row.WORD);
            }
            else
            {
                WordsLessons.Update(row);
                if (currentWord != row.WORD)
                {
                    DeleteWordIfNeeded(currentWord);
                    InsertWordIfNeeded(row.WORD);
                }
            }
        }
    }
}
