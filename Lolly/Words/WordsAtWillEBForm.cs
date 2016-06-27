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
    public partial class WordsAtWillEBForm : WordsEBForm
    {
        private BindingList<MWORDATWILL> wordsList;
        private BindingListView<MWORDATWILL> wordsView;

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
            bindingSource1.DataSource = wordsView = new BindingListView<MWORDATWILL>(wordsList);
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
                        orderby row.SEQNUM
                        select new ReorderObject(row.SEQNUM, row.WORD)).ToArray();
            var dlg = new ReorderDlg(objs);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                foreach (var obj in objs)
                {
                    var row = wordsList.SingleOrDefault(r => r.ID == obj.ID);
                    row.SEQNUM = obj.SEQNUM;
                }
                foreach (var row in wordsList)
                    row.ID = row.SEQNUM;
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

        private void bindingSource1_ListItemAdded(object sender, ListChangedEventArgs e)
        {
            if (wordsList.Count < wordsView.Count) return;

            var row = wordsList.Last();
            if (row.ID == 0)
            {
                if (row.SEQNUM == 0)
                    row.SEQNUM = wordsList.Count;
                row.ID = row.SEQNUM;
            }
        }
    }
}
