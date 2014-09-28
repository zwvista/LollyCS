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
    public partial class AuxAutoCorrectForm : Form, ILangBookUnits
    {
        private int deletedID = 0;
        private LangBookUnitSettings lbuSettings;
        protected BindingList<MAUTOCORRECT> auxList;

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
            auxList = new BindingList<MAUTOCORRECT>(AutoCorrect.GetDataByLang(lbuSettings.LangID));
            bindingSource1.DataSource = auxList;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            dataGridView1.MoveToAddNew();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            var row = auxList[bindingSource1.Position];
            var item = row.EXTENDED;
            var msg = string.Format("The autocorrect item \"{0}\" is about to be DELETED. Are you sure?", item);
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
            Text = string.Format("AutoCorrect ({0})", lbuSettings.LangDesc);
            FillTable();
        }

        #endregion

        private void reindexToolStripButton_Click(object sender, EventArgs e)
        {
            var objs = (from row in auxList
                        where row.ID != 0
                        orderby row.INDEX
                        select new ReindexObject(row.ID, row.EXTENDED)).ToArray();
            var dlg = new ReindexDlg(objs);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                foreach (var obj in objs)
                    AutoCorrect.UpdateIndex(obj.INDEX, obj.ID);
                refreshToolStripButton.PerformClick();
            }
        }

        private void bindingSource1_ListItemDeleted(object sender, ListChangedEventArgs e)
        {
            if (deletedID == 0) return;

            AutoCorrect.Delete(deletedID);
            deletedID = 0;
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (!bindingSource1.ListRowChanged) return;

            var row = auxList[e.RowIndex];
            if (row.ID == 0)
            {
                row.LANGID = lbuSettings.LangID;
                if (row.INDEX == 0)
                    row.INDEX = e.RowIndex + 1;
                row.ID = AutoCorrect.Insert(row);
                dataGridView1.Refresh();
            }
            else
                AutoCorrect.Update(row);
        }
    }
}
