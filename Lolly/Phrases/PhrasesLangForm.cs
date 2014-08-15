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
            bindingNavigator1.Items.Remove(reindexToolStripButton);
        }

        private void PhrasesLangForm_Load(object sender, EventArgs e)
        {
            UpdatelblSettings();
        }

        protected override void FillTable()
        {
            phrasesList = new BindingList<MPHRASELANG>(
                filterScope == 0 ? PhrasesLang.GetDataByLangPhrase(lblSettings.LangID, filter) :
                PhrasesLang.GetDataByLangTranslation(lblSettings.LangID, filter)
            );
            bindingSource1.DataSource = phrasesList;
            autoCorrectList = AutoCorrect.GetDataByLang(lblSettings.LangID);
        }

        public override void UpdatelblSettings()
        {
            base.UpdatelblSettings();
            Text = string.Format("Phrases ({0})", lblSettings.LangDesc);
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.ColumnIndex != 0) return;
            //bool ascending = dataGridView1.SortedColumn.Index != 0 ||
            //    dataGridView1.SortOrder == SortOrder.Descending;
            //bindingSource1.Sort = ascending ? "BOOKNAME,LESSON, INDEX" : "BOOKNAME DESC, LESSON DESC, INDEX DESC";
        }
    }
}
