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
    public partial class FilterDlg : Form
    {
        public string Filter
        {
            get
            {
                return filterComboBox.Text;
            }
        }
        public int FilterScope
        {
            get
            {
                return filterScopeComboBox.SelectedIndex;
            }
        }
        private IEnumerable<MAUTOCORRECT> autoCorrectList;

        public FilterDlg(IEnumerable<MAUTOCORRECT> autoCorrectList)
        {
            this.autoCorrectList = autoCorrectList;
            InitializeComponent();
            filterScopeComboBox.SelectedIndex = 0;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            filterComboBox.Text = Program.AutoCorrect(filterComboBox.Text, autoCorrectList);
        }
    }
}
