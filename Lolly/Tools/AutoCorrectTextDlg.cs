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
        private IEnumerable<MAUTOCORRECT> autoCorrectList;

        public AutoCorrectTextDlg()
        {
            InitializeComponent();
            textBox1.KeyDown += Program.textBoxSelectAll_KeyDown;
            textBox2.KeyDown += Program.textBoxSelectAll_KeyDown;
        }

        private void AutoCorrectTextDlg_Load(object sender, EventArgs e)
        {
            langComboBox.DataSource = Languages.GetData();
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
            autoCorrectList = LollyBase.AutoCorrect.GetDataByLang(langID);
            AutoCorrect();
        }

        private void copyTextButton_Click(object sender, EventArgs e)
        {
            textBox2.Copy();
        }
    }
}
