using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LollyShared;
using Equin.ApplicationFramework;

namespace Lolly
{
    public partial class AuxWebTextForm : Form
    {
        private string currentSite = "";
        private string deletedSite = "";
        protected BindingList<MWEBTEXT> auxList;
        protected BindingListView<MWEBTEXT> auxView;

        public AuxWebTextForm()
        {
            InitializeComponent();
        }

        private void AuxWebTextForm_Load(object sender, EventArgs e)
        {
            FillTable();
        }

        private void FillTable()
        {
            auxList = new BindingList<MWEBTEXT>(LollyDB.WebText_GetData());
            bindingSource1.DataSource = auxView = new BindingListView<MWEBTEXT>(auxList);
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            dataGridView1.MoveToAddNew();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            var msg = $"The webtext item \"{currentSite}\" is about to be DELETED. Are you sure?";
            if (MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                deletedSite = currentSite;
                bindingSource1.RemoveCurrent();
            }
        }

        private void refreshToolStripButton_Click(object sender, EventArgs e)
        {
            FillTable();
        }

        private void dataGridView1_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridView1.IsCurrentRowDirty)
            {
                var item = auxList[e.RowIndex].SITENAME;
                var msg = $"The webtext item \"{item}\" is about to be updated. Are you sure?";
                if (MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    dataGridView1.CancelEdit();
                    e.Cancel = true;
                }
            }
        }

        private void bindingSource1_ListItemDeleted(object sender, ListChangedEventArgs e)
        {
            if (deletedSite == "") return;

            LollyDB.WebText_Delete(deletedSite);
            deletedSite = "";
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            currentSite = auxList[e.RowIndex].SITENAME;
        }

        private void bindingSource1_ListItemAdded(object sender, ListChangedEventArgs e)
        {
            if (auxList.Count < auxView.Count) return;

            var row = auxList.Last();
            if (currentSite == null)
                LollyDB.WebText_Insert(row);
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (!bindingSource1.ListRowChanged) return;

            var row = auxView[e.RowIndex].Object;
            LollyDB.WebText_Update(row, currentSite);
        }
    }
}
