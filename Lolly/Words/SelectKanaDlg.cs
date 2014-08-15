using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lolly
{
    public partial class SelectKanaDlg : Form
    {
        public string SelectedKana
        {
            get
            {
                return kanasComboBox.Text;
            }
        }

        public SelectKanaDlg(string word, string[] kanas)
        {
            InitializeComponent();
            wordLabel.Text = word;
            kanasComboBox.Items.AddRange(kanas);
        }
    }
}
