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
    public partial class AuxAutoCorrectForm : Form, ILangBookUnits
    {
        private long deletedID = 0;
        private LangBookUnitSettings lbuSettings;
        private BindingList<MAUTOCORRECT> auxList;
        private BindingListView<MAUTOCORRECT> auxView;

        public AuxAutoCorrectForm()
        {
            InitializeComponent();
        }

        private void AutoCorrectForm_Load(object sender, EventArgs e)
        {
            UpdatelbuSettings();
        }

        private void FillTable()
        {
            auxList = new BindingList<MAUTOCORRECT>(LollyDB.AutoCorrect_GetDataByLang(lbuSettings.LangID));
            bindingSource1.DataSource = auxView = new BindingListView<MAUTOCORRECT>(auxList);
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            dataGridView1.MoveToAddNew();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            var row = auxList[bindingSource1.Position];
            var item = row.EXTENDED;
            var msg = $"The autocorrect item \"{item}\" is about to be DELETED. Are you sure?";
            if (MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                deletedID = row.ID;
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
            Text = $"AutoCorrect ({lbuSettings.LangDesc})";
            FillTable();
        }

        #endregion

        private void reorderToolStripButton_Click(object sender, EventArgs e)
        {
            var objs = (from row in auxList
                        where row.ID != 0
                        orderby row.ORD
                        select new ReorderObject(row.ID, row.EXTENDED)).ToArray();
            var dlg = new ReorderDlg(objs);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                foreach (var obj in objs)
                    LollyDB.AutoCorrect_UpdateOrd(obj.ORD, obj.ID);
                refreshToolStripButton.PerformClick();
            }
        }

        private void bindingSource1_ListItemDeleted(object sender, ListChangedEventArgs e)
        {
            if (deletedID == 0) return;

            LollyDB.AutoCorrect_Delete(deletedID);
            deletedID = 0;
        }

        private void bindingSource1_ListItemAdded(object sender, ListChangedEventArgs e)
        {
            if (auxList.Count < auxView.Count) return;

            var row = auxList.Last();
            if (row.ID == 0)
            {
                row.LANGID = lbuSettings.LangID;
                if (row.ORD == 0)
                    row.ORD = auxList.Count;
                row.ID = LollyDB.AutoCorrect_Insert(row);
                dataGridView1.Refresh();
            }
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (!bindingSource1.ListRowChanged) return;

            var row = auxView[e.RowIndex].Object;
            LollyDB.AutoCorrect_Update(row);
        }
    }
}
