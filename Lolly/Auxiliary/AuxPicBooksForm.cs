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
    public partial class AuxPicBooksForm : Form, ILangBookLessons
    {
        private string currentBook = "";
        private string deletedBook = "";
        private LangBookLessonSettings lblSettings;
        protected List<MPICBOOK> auxList;

        public AuxPicBooksForm()
        {
            InitializeComponent();
        }

        private void AuxPictureBooksForm_Load(object sender, EventArgs e)
        {
            UpdatelblSettings();
        }

        private void FillTable()
        {
            auxList = PicBooks.GetDataByLang(lblSettings.LangID);
            bindingSource1.DataSource = auxList;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            dataGridView1.MoveToAddNew();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            var row = auxList[bindingSource1.Position];
            var item = row.BOOKNAME;
            var msg = string.Format("The picbooks item \"{0}\" is about to be DELETED. Are you sure?", item);
            if (MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                bindingSource1.RemoveCurrent();
        }

        private void refreshToolStripButton_Click(object sender, EventArgs e)
        {
            FillTable();
        }

        #region ILangBookLessons Members

        public void UpdatelblSettings()
        {
            lblSettings = Program.lblSettings;
            Text = string.Format("Picture Books ({0})", lblSettings.LangDesc);
            FillTable();
        }

        #endregion

        private void bindingSource1_ListItemDeleted(object sender, ListChangedEventArgs e)
        {
            if (deletedBook == "") return;

            PicBooks.Delete(deletedBook);
            deletedBook = "";
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            currentBook = auxList[e.RowIndex].BOOKNAME;
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (!bindingSource1.ListRowChanged) return;

            var row = auxList[e.RowIndex];
            if (row.LANGID == 0)
            {
                row.LANGID = lblSettings.LangID;
                PicBooks.Insert(row);
            }
            else
                PicBooks.Update(row, currentBook);
        }
    }
}
