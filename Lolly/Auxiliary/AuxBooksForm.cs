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
    public partial class AuxBooksForm : Form, ILangBookUnits
    {
        private long currentBookID = 0;
        private long deletedBookID = 0;
        private LangBookUnitSettings lbuSettings;
        protected List<MBOOK> auxList;

        public AuxBooksForm()
        {
            InitializeComponent();
        }

        private void BooksForm_Load(object sender, EventArgs e)
        {
            UpdatelbuSettings();
        }

        private void FillTable()
        {
            auxList = LollyDB.Books_GetDataByLang(lbuSettings.LangID);
            bindingSource1.DataSource = auxList;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            dataGridView1.MoveToAddNew();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            var row = auxList[bindingSource1.Position];
            var bookName = row.BOOKNAME;
            var msg = $"The book \"{bookName}\" is about to be DELETED. Are you sure?";
            if (MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                deletedBookID = row.BOOKID;
                bindingSource1.RemoveCurrent();
            }
        }

        private void refreshToolStripButton_Click(object sender, EventArgs e)
        {
            FillTable();
        }

        #region ILangBookUnits Members

        public void UpdatelbuSettings()
        {
            lbuSettings = Program.lbuSettings;
            Text = $"Books ({lbuSettings.LangDesc})";
            FillTable();
        }

        #endregion

        private void bindingSource1_ListItemDeleted(object sender, ListChangedEventArgs e)
        {
            if (deletedBookID == 0) return;

            LollyDB.Books_Delete(deletedBookID);
            deletedBookID = 0;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            currentBookID = auxList[e.RowIndex].BOOKID;
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (!bindingSource1.ListRowChanged) return;

            var row = auxList[e.RowIndex];
            if (row.LANGID == 0)
            {
                row.LANGID = lbuSettings.LangID;
                LollyDB.Books_Insert(row);
            }
            else
                LollyDB.Books_Update(row, currentBookID);
        }
    }
}
