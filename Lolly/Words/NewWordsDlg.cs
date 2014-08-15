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
    public partial class NewWordsDlg : Form
    {
        public List<string> Words
        {
            get
            {
                return
                    (from line in wordsTextBox.Lines
                     let w = line.Trim()
                     where w != ""
                     select w).ToList();
            }
        }

        public NewWordsDlg()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if(wordsTextBox.Text == "")
                DialogResult =  DialogResult.Cancel;
        }
    }
}
