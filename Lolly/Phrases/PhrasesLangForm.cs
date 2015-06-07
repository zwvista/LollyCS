using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LollyShared;

namespace Lolly
{
    public partial class PhrasesLangForm : PhrasesBaseForm
    {
        private BindingList<MPHRASELANG> phrasesList;

        public PhrasesLangForm()
        {
            InitializeComponent();
            dataGridView = dataGridView1;
            bindingNavigator1.BindingSource = bindingSource1;
            bindingNavigator1.Items.Remove(bindingNavigatorAddNewItem);
            bindingNavigator1.Items.Remove(multiAddToolStripButton);
            bindingNavigator1.Items.Remove(addDeleteToolStripSeparator);
            bindingNavigator1.Items.Remove(reorderToolStripButton);
        }

        private void PhrasesLangForm_Load(object sender, EventArgs e)
        {
            UpdatelbuSettings();
        }

        protected override void FillTable()
        {
            phrasesList = new BindingList<MPHRASELANG>(
                filterScope == 0 ? LollyDB.PhrasesLang_GetDataByLangPhrase(lbuSettings.LangID, filter, matchWholeWords) :
                LollyDB.PhrasesLang_GetDataByLangTranslation(lbuSettings.LangID, filter)
            );
            bindingSource1.DataSource = phrasesList;
            autoCorrectList = LollyDB.AutoCorrect_GetDataByLang(lbuSettings.LangID);
        }

        public override void UpdatelbuSettings()
        {
            base.UpdatelbuSettings();
            Text = $"Phrases ({lbuSettings.LangDesc})";
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.ColumnIndex != 0) return;
            //bool ascending = dataGridView1.SortedColumn.Index != 0 ||
            //    dataGridView1.SortOrder == SortOrder.Descending;
            //bindingSource1.Sort = ascending ? "BOOKNAME,UNIT, ORD" : "BOOKNAME DESC, UNIT DESC, ORD DESC";
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (!bindingSource1.ListRowChanged) return;

            var row = phrasesList[e.RowIndex];
            row.PHRASE = Program.AutoCorrect(row.PHRASE, autoCorrectList);
            LollyDB.PhrasesUnits_Update(row);
        }

        private void dataGridView1_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridView1.IsCurrentRowDirty)
            {
                var row = phrasesList[e.RowIndex];
                var msg = $"The phrase \"{row.PHRASE}\" is about to be updated. Are you sure?";
                if (MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    LollyDB.PhrasesUnits_Get(row);
                    dataGridView1.CancelEdit();
                    bindingSource1.ListRowChanged = false;
                    //e.Cancel = true;
                }
            }
        }
    }
}
