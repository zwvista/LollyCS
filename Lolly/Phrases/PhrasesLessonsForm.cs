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
    public partial class PhrasesLessonsForm : PhrasesBaseForm
    {
        private int deletedID = 0;
        private BindingList<MPHRASELESSON> phrasesList;

        public PhrasesLessonsForm()
        {
            InitializeComponent();
            dataGridView = dataGridView1;
            bindingNavigator1.BindingSource = bindingSource1;
            bindingNavigator1.Items.Remove(setFilterToolStripButton);
            bindingNavigator1.Items.Remove(removeFilterToolStripButton);
            bindingNavigator1.Items.Remove(filtertoolStripLabel);
            bindingNavigatorAddNewItem.Click += bindingNavigatorAddNewItem_Click;
            bindingNavigatorDeleteItem.Click += bindingNavigatorDeleteItem_Click;
            reindexToolStripButton.Click += reindexToolStripButton_Click;
        }

        private void PhrasesForm_Load(object sender, EventArgs e)
        {
            UpdatelblSettings();
        }

        protected override void FillTable()
        {
            phrasesList = new BindingList<MPHRASELESSON>(
                PhrasesLessons.GetDataByBookLessonParts(lblSettings.BookID,
                lblSettings.LessonPartFrom, lblSettings.LessonPartTo));
            bindingSource1.DataSource = phrasesList;
            autoCorrectList = AutoCorrect.GetDataByLang(lblSettings.LangID);
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            dataGridView1.MoveToAddNew();
        }

        protected override void OnAddPhrase(string phrase, string translation)
        {
            dataGridView.MoveToAddNew();
            dataGridView.BeginEdit(false);
            dataGridView.NotifyCurrentCellDirty(true);
            dataGridView.CurrentRow.Cells["phraseColumn"].Value = phrase;
            dataGridView.CurrentRow.Cells["translationColumn"].Value = translation;
            dataGridView.EndEdit();
            dataGridView.MoveToAddNew();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            var msg = string.Format("The phrase \"{0}\" is about to be DELETED. Are you sure?", phrase);
            if (MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                deletedID = phrasesList[bindingSource1.Position].ID;
                bindingSource1.RemoveCurrent();
            }
        }

        public override void UpdatelblSettings()
        {
            base.UpdatelblSettings();
            Text = string.Format("Phrases ({0})", lblSettings.BookLessonsDesc);
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.ColumnIndex != 0) return;
            //bool ascending = dataGridView1.SortedColumn.Index != 0 ||
            //    dataGridView1.SortOrder == SortOrder.Descending;
            //bindingSource1.Sort = ascending ? "LESSON, PART, INDEX" : "LESSON DESC, PART, INDEX DESC";
        }

        private void reindexToolStripButton_Click(object sender, EventArgs e)
        {
            var objs = (from row in phrasesList
                        where row.ID != 0
                        orderby row.INDEX
                        select new ReindexObject(row.ID, row.PHRASE)).ToArray();
            var dlg = new ReindexDlg(objs);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                foreach (var obj in objs)
                    PhrasesLessons.UpdateIndex(obj.INDEX, obj.ID);
                refreshToolStripButton.PerformClick();
            }
        }

        private void bindingSource1_ListItemDeleted(object sender, ListChangedEventArgs e)
        {
            if (deletedID == 0) return;

            PhrasesLessons.Delete(deletedID);
            deletedID = 0;
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (!bindingSource1.ListRowChanged) return;

            var row = phrasesList[e.RowIndex];
            if (row.ID == 0)
            {
                row.BOOKID = lblSettings.BookID;
                if (row.LESSON == 0)
                    row.LESSON = lblSettings.LessonTo;
                if (row.PART == 0)
                    row.PART = lblSettings.PartTo;
                if (row.INDEX == 0)
                    row.INDEX = e.RowIndex + 1;
                row.PHRASE = Program.AutoCorrect(row.PHRASE, autoCorrectList);
                row.TRANSLATION = row.TRANSLATION;
                row.ID = PhrasesLessons.Insert(row);
                dataGridView1.Refresh();
            }
            else
            {
                row.PHRASE = Program.AutoCorrect(row.PHRASE, autoCorrectList);
                PhrasesLessons.Update(row);
            }
        }
    }
}
