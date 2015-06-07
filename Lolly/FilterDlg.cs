using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LollyShared;

namespace Lolly
{
    public partial class FilterDlg : Form
    {
        public string Filter => filterComboBox.Text;
        public int FilterScope => filterScopeComboBox.SelectedIndex;
        public bool MatchWholeWord => matchWholeWordsCheckBox.Checked;
        private List<MAUTOCORRECT> autoCorrectList;

        public FilterDlg(List<MAUTOCORRECT> autoCorrectList)
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
