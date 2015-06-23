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
    public partial class ExtractWebDictOptionsDlg : Form
    {
        public long LangID { get; private set; }
        public string[] SelectedWords { get; private set; }
        public string[] SelectedDicts { get; private set; }
        public bool OverwriteDB => overwriteCheckBox.Checked;

        public ExtractWebDictOptionsDlg()
        {
            InitializeComponent();
            wordDataGridView.AutoGenerateColumns = false;
            dictDataGridView.AutoGenerateColumns = false;
        }

        private void ExtractWebDictOptionsForm_Load(object sender, EventArgs e)
        {
            LangID = Program.lbuSettings.LangID;
            bookUnitsRadioButton.Text = Program.lbuSettings.BookUnitsDesc;
            langRadioButton.Text = Program.lbuSettings.LangDesc;
            bookUnitsRadioButton.Checked = true;
            dictDataGridView.DataSource = LollyDB.DictAll_GetDataByLangExact(Program.lbuSettings.LangID);
            CheckDataGridView(dictDataGridView, true, false);
        }

        private void unitsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            wordDataGridView.DataSource = LollyDB.WordsLangOrBook_GetDataByBookUnitParts(Program.lbuSettings.BookID,
                Program.lbuSettings.UnitPartFrom, Program.lbuSettings.UnitPartTo);
            CheckDataGridView(wordDataGridView, true, false);
        }

        private void langRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            wordDataGridView.DataSource = LollyDB.WordsLangOrBook_GetDataByLang(Program.lbuSettings.LangID);
            CheckDataGridView(wordDataGridView, true, false);
        }

        private void CheckDataGridView(DataGridView dgv, bool toCheck, bool needSelect)
        {
            foreach (DataGridViewRow row in dgv.Rows)
                if (!needSelect || row.Selected)
                    row.Cells[0].Value = toCheck;
        }

        private void checkLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var tag = int.Parse(((LinkLabel)sender).Tag.ToString());
            var dgv = tag < 2 ? wordDataGridView : dictDataGridView;
            var toCheck = tag % 2 == 0;
            CheckDataGridView(dgv, toCheck, false);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Func<DataGridView, string[]> GetAllChecked = dgv => (
                from DataGridViewRow row in dgv.Rows
                let v = row.Cells[0].Value
                where v != null && (bool)v
                select row.Cells[1].Value.ToString()
            ).ToArray();

            SelectedWords = GetAllChecked(wordDataGridView);
            SelectedDicts = GetAllChecked(dictDataGridView);
            if (SelectedWords.Length == 0 || SelectedDicts.Length == 0)
                DialogResult = DialogResult.Cancel;
        }
    }
}
