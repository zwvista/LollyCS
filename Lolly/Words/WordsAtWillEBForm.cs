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
    public partial class WordsAtWillEBForm : Lolly.WordsEBForm
    {
        private BindingList<MWORDATWILL> wordsList;

        public WordsAtWillEBForm()
        {
            InitializeComponent();
            dataGridView = dataGridView1;
            reorderToolStripButton.Click += reorderToolStripButton_Click;
            refreshToolStripButton.Click += refreshToolStripButton_Click;
        }

        protected override void FillTable()
        {
            wordsList = new BindingList<MWORDATWILL>(new List<MWORDATWILL>());
            bindingSource1.DataSource = wordsList;
        }

        protected override void OnDeleteWord()
        {
            bindingSource1.RemoveCurrent();
        }

        public override void UpdatelbuSettings()
        {
            base.UpdatelbuSettings();
            Text = $"Words At Will(EBWin) ({lbuSettings.LangName})";
        }

        private void reorderToolStripButton_Click(object sender, EventArgs e)
        {
            var objs = (from row in wordsList
                        where row.ID != 0
                        orderby row.ORD
                        select new ReorderObject(row.ORD, row.WORD)).ToArray();
            var dlg = new ReorderDlg(objs);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                foreach (var obj in objs)
                {
                    var row = wordsList.SingleOrDefault(r => r.ID == obj.ID);
                    row.ORD = obj.ORD;
                }
                foreach (var row in wordsList)
                    row.ID = row.ORD;
            }
        }

        protected override void OnFindKanas()
        {
            foreach (var row in wordsList)
                if (string.IsNullOrEmpty(row.NOTE))
                    row.NOTE = ebwin.FindKana(row.WORD);
            dataGridView1.Refresh();
        }

        private void refreshToolStripButton_Click(object sender, EventArgs e)
        {
            wordsList.Clear();
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (!bindingSource1.ListRowChanged) return;

            var row = wordsList[e.RowIndex];
            if (row.ID == 0)
            {
                if (row.ORD == 0)
                    row.ORD = e.RowIndex + 1;
                row.ID = row.ORD;
            }
        }
    }
}
