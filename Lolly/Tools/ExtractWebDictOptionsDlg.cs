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
    public partial class ExtractWebDictOptionsDlg : Form
    {
        public int LangID { get; private set; }
        public string[] SelectedWords { get; private set; }
        public string[] SelectedDicts { get; private set; }
        public bool OverwriteDB
        {
            get
            {
                return overwriteCheckBox.Checked;
            }
        }

        public ExtractWebDictOptionsDlg()
        {
            InitializeComponent();
            wordDataGridView.AutoGenerateColumns = false;
            dictDataGridView.AutoGenerateColumns = false;
        }

        private void ExtractWebDictOptionsForm_Load(object sender, EventArgs e)
        {
            LangID = Program.lblSettings.LangID;
            bookLessonsRadioButton.Text = Program.lblSettings.BookLessonsDesc;
            langRadioButton.Text = Program.lblSettings.LangDesc;
            bookLessonsRadioButton.Checked = true;
            dictDataGridView.DataSource = DictAll.GetDataByLangExact(Program.lblSettings.LangID);
            checkAllDictsButton.PerformClick();
        }

        private void lessonsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            wordDataGridView.DataSource = WordsLangOrBook.GetDataByBookLessonParts(Program.lblSettings.BookID,
                Program.lblSettings.LessonPartFrom, Program.lblSettings.LessonPartTo);
            checkAllWordsButton.PerformClick();
        }

        private void langRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            wordDataGridView.DataSource = WordsLangOrBook.GetDataByLang(Program.lblSettings.LangID);
            checkAllWordsButton.PerformClick();
        }

        private string[] GetAllChecked(DataGridView gdv)
        {
            return (from DataGridViewRow row in gdv.Rows
                    let v = row.Cells[0].Value
                    where v != null && (bool)v
                    select row.Cells[1].Value.ToString()).ToArray();
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            var tag = int.Parse(((Button)sender).Tag.ToString());
            var gdv = tag < 4 ? wordDataGridView : dictDataGridView;
            var toCheck = tag % 2 == 0;
            var needSelect = tag % 4 > 1;
            foreach (DataGridViewRow row in gdv.Rows)
                if (!needSelect || row.Selected)
                    row.Cells[0].Value = toCheck;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            SelectedWords = GetAllChecked(wordDataGridView);
            SelectedDicts = GetAllChecked(dictDataGridView);
            if (SelectedWords.Length == 0 || SelectedDicts.Length == 0)
                this.DialogResult = DialogResult.Cancel;
        }
    }
}
