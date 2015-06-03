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
    public partial class AutoCorrectTextDlg : Form
    {
        private List<MAUTOCORRECT> autoCorrectList;

        public AutoCorrectTextDlg()
        {
            InitializeComponent();
        }

        private void AutoCorrectTextDlg_Load(object sender, EventArgs e)
        {
            langComboBox.DataSource = Program.db.Languages_GetDataNonChinese();
            langComboBox.SelectedValue = Program.lbuSettings.LangID;
        }

        private void AutoCorrect()
        {
            textBox2.Text = Program.AutoCorrect(textBox1.Text, autoCorrectList);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            AutoCorrect();
        }

        private void langComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (langComboBox.SelectedValue == null) return;
            var langID = (int)langComboBox.SelectedValue;
            autoCorrectList = Program.db.AutoCorrect_GetDataByLang(langID);
            AutoCorrect();
        }

        private void copyTextButton_Click(object sender, EventArgs e)
        {
            textBox2.Copy();
        }
    }
}
