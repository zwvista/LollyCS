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
    public partial class AuxPicBooksForm : Form, ILangBookUnits
    {
        private string currentBook = "";
        private string deletedBook = "";
        private LangBookUnitSettings lbuSettings;
        protected List<MPICBOOK> auxList;

        public AuxPicBooksForm()
        {
            InitializeComponent();
        }

        private void AuxPictureBooksForm_Load(object sender, EventArgs e)
        {
            UpdatelbuSettings();
        }

        private void FillTable()
        {
            auxList = Program.db.PicBooks_GetDataByLang(lbuSettings.LangID);
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
            var msg = $"The picbooks item \"{item}\" is about to be DELETED. Are you sure?";
            if (MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                bindingSource1.RemoveCurrent();
        }

        private void refreshToolStripButton_Click(object sender, EventArgs e)
        {
            FillTable();
        }

        #region ILangBookUnits Members

        public void UpdatelbuSettings()
        {
            lbuSettings = Program.lbuSettings;
            Text = $"Picture Books ({lbuSettings.LangDesc})";
            FillTable();
        }

        #endregion

        private void bindingSource1_ListItemDeleted(object sender, ListChangedEventArgs e)
        {
            if (deletedBook == "") return;

            Program.db.PicBooks_Delete(deletedBook);
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
                row.LANGID = lbuSettings.LangID;
                Program.db.PicBooks_Insert(row);
            }
            else
                Program.db.PicBooks_Update(row, currentBook);
        }
    }
}
