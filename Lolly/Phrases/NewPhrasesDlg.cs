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
    public partial class NewPhrasesDlg : Form
    {
        public string[] PhrasesTranslations
        {
            get
            {
                return
                    (from line in phrasesTranslationsTextBox.Lines
                    let pt = line.Trim()
                    where pt != ""
                    select pt).ToArray();
            }
        }

        public NewPhrasesDlg()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if(phrasesTranslationsTextBox.Text == "")
                DialogResult =  DialogResult.Cancel;
        }
    }
}
