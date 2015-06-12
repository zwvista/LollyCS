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
        public List<string> PhrasesTranslations
        {
            get
            {
                var pts =
                    (from line in phrasesTranslationsTextBox.Lines
                     let pt = line.Trim()
                     where pt != ""
                     select pt).ToList();
                if (pts.Count % 2 != 0)
                    pts.Add("");
                return pts;
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
