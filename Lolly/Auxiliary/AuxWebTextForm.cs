﻿using System;
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
    public partial class AuxWebTextForm : Form
    {
        private string currentSite = "";
        private string deletedSite = "";
        protected List<MWEBTEXT> auxList;

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
            auxList = WebText.GetData();
            bindingSource1.DataSource = auxList;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            dataGridView1.MoveToAddNew();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            var msg = string.Format("The webtext item \"{0}\" is about to be DELETED. Are you sure?", currentSite);
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
                var msg = string.Format("The webtext item \"{0}\" is about to be updated. Are you sure?", item);
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

            WebText.Delete(deletedSite);
            deletedSite = "";
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            currentSite = auxList[e.RowIndex].SITENAME;
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (!bindingSource1.ListRowChanged) return;

            var row = auxList[e.RowIndex];
            if (currentSite == null)
                WebText.Insert(row);
            else
                WebText.Update(row, currentSite);
        }
    }
}
