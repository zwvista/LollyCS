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
    public partial class AuxBooksForm : Form, ILangBookLessons
    {
        private int currentBookID = 0;
        private int deletedBookID = 0;
        private LangBookLessonSettings lblSettings;
        protected List<MBOOK> auxList;

        public AuxBooksForm()
        {
            InitializeComponent();
        }

        private void BooksForm_Load(object sender, EventArgs e)
        {
            UpdatelblSettings();
        }

        private void FillTable()
        {
            auxList = Books.GetDataByLang(lblSettings.LangID);
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
            var msg = string.Format("The book \"{0}\" is about to be DELETED. Are you sure?", bookName);
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

        #region ILangBookLessons Members

        public void UpdatelblSettings()
        {
            lblSettings = Program.lblSettings;
            Text = string.Format("Books ({0})", lblSettings.LangDesc);
            FillTable();
        }

        #endregion

        private void bindingSource1_ListItemDeleted(object sender, ListChangedEventArgs e)
        {
            if (deletedBookID == 0) return;

            Books.Delete(deletedBookID);
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
                row.LANGID = lblSettings.LangID;
                Books.Insert(row);
            }
            else
                Books.Update(row, currentBookID);
        }
    }
}
