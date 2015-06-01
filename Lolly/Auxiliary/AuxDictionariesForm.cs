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
    public partial class AuxDictionariesForm : Form, ILangBookUnits
    {
        private string currentDict = "";
        private string deletedDict = "";
        private LangBookUnitSettings lbuSettings;
        private List<MDICTIONARY> auxList;

        public AuxDictionariesForm()
        {
            InitializeComponent();
        }

        private void AuxDictionariesForm_Load(object sender, EventArgs e)
        {
            UpdatelbuSettings();
        }

        private void FillTable()
        {
            mLANGUAGEBindingSource.DataSource = Program.db.Languages_GetData();
            mDICTTYPEBindingSource.DataSource = Program.db.DictTypes_GetData();
            auxList = Program.db.Dictionaries_GetDataByLang(lbuSettings.LangID);
            bindingSource1.DataSource = auxList;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            dataGridView1.MoveToAddNew();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            var row = auxList[bindingSource1.Position];
            var item = row.DICTNAME;
            var msg = string.Format("The dictionaries item \"{0}\" is about to be DELETED. Are you sure?", item);
            if (MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                deletedDict = currentDict;
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
                var row = auxList[e.RowIndex];
                var item = row.DICTNAME;
                var msg = string.Format("The dictionaries item \"{0}\" is about to be updated. Are you sure?", item);
                if (MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    dataGridView1.CancelEdit();
                    e.Cancel = true;
                }
            }
        }

        #region ILangBookUnits Members

        public void UpdatelbuSettings()
        {
            lbuSettings = Program.lbuSettings;
            Text = string.Format("Dictionaries ({0})", lbuSettings.LangDesc);
            FillTable();
        }

        #endregion

        private void bindingSource1_ListItemDeleted(object sender, ListChangedEventArgs e)
        {
            if (deletedDict == "") return;

            Program.db.Dictionaries_Delete(lbuSettings.LangID, deletedDict);
            deletedDict = "";
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            currentDict = auxList[e.RowIndex].DICTNAME;
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (!bindingSource1.ListRowChanged) return;

            var row = auxList[e.RowIndex];
            if (row.LANGID == 0)
            {
                row.LANGID = lbuSettings.LangID;
                Program.db.Dictionaries_Insert(row);
            }
            else
                Program.db.Dictionaries_Update(row, currentDict);
        }
    }
}
