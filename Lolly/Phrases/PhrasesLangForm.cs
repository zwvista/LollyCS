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
                filterScope == 0 ? Program.db.PhrasesLang_GetDataByLangPhrase(lbuSettings.LangID, filter) :
                Program.db.PhrasesLang_GetDataByLangTranslation(lbuSettings.LangID, filter)
            );
            bindingSource1.DataSource = phrasesList;
            autoCorrectList = Program.db.AutoCorrect_GetDataByLang(lbuSettings.LangID);
        }

        public override void UpdatelbuSettings()
        {
            base.UpdatelbuSettings();
            Text = string.Format("Phrases ({0})", lbuSettings.LangDesc);
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.ColumnIndex != 0) return;
            //bool ascending = dataGridView1.SortedColumn.Index != 0 ||
            //    dataGridView1.SortOrder == SortOrder.Descending;
            //bindingSource1.Sort = ascending ? "BOOKNAME,UNIT, ORD" : "BOOKNAME DESC, UNIT DESC, ORD DESC";
        }
    }
}
