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
    public partial class PhrasesUnitsForm : PhrasesBaseForm
    {
        private int deletedID = 0;
        private BindingList<MPHRASEUNIT> phrasesList;

        public PhrasesUnitsForm()
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
            UpdatelbuSettings();
        }

        protected override void FillTable()
        {
            phrasesList = new BindingList<MPHRASEUNIT>(
                PhrasesUnits.GetDataByBookUnitParts(lbuSettings.BookID,
                lbuSettings.UnitPartFrom, lbuSettings.UnitPartTo));
            bindingSource1.DataSource = phrasesList;
            autoCorrectList = AutoCorrect.GetDataByLang(lbuSettings.LangID);
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

        public override void UpdatelbuSettings()
        {
            base.UpdatelbuSettings();
            Text = string.Format("Phrases ({0})", lbuSettings.BookUnitsDesc);
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.ColumnIndex != 0) return;
            //bool ascending = dataGridView1.SortedColumn.Index != 0 ||
            //    dataGridView1.SortOrder == SortOrder.Descending;
            //bindingSource1.Sort = ascending ? "UNIT, PART, INDEX" : "UNIT DESC, PART, INDEX DESC";
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
                    PhrasesUnits.UpdateIndex(obj.INDEX, obj.ID);
                refreshToolStripButton.PerformClick();
            }
        }

        private void bindingSource1_ListItemDeleted(object sender, ListChangedEventArgs e)
        {
            if (deletedID == 0) return;

            PhrasesUnits.Delete(deletedID);
            deletedID = 0;
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (!bindingSource1.ListRowChanged) return;

            var row = phrasesList[e.RowIndex];
            if (row.ID == 0)
            {
                row.BOOKID = lbuSettings.BookID;
                if (row.UNIT == 0)
                    row.UNIT = lbuSettings.UnitTo;
                if (row.PART == 0)
                    row.PART = lbuSettings.PartTo;
                if (row.INDEX == 0)
                    row.INDEX = e.RowIndex + 1;
                row.PHRASE = Program.AutoCorrect(row.PHRASE, autoCorrectList);
                row.TRANSLATION = row.TRANSLATION;
                row.ID = PhrasesUnits.Insert(row);
                dataGridView1.Refresh();
            }
            else
            {
                row.PHRASE = Program.AutoCorrect(row.PHRASE, autoCorrectList);
                PhrasesUnits.Update(row);
            }
        }
    }
}
