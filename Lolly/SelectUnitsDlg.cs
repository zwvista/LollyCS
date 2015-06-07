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
    public partial class SelectUnitsDlg : Form
    {
        private long selectedLangID;
        private long selectedBookID;
        public bool ActiveIncluded => activeIncludedCheckBox.Checked;

        private List<MLANGUAGE> languageList;
        private List<MBOOK> bookList;

        public SelectUnitsDlg(bool activeIncluded)
        {
            InitializeComponent();
            activeIncludedCheckBox.Checked = activeIncluded;
        }

        private void SelectUnitsDlg_Load(object sender, EventArgs e)
        {
            languageList = LollyDB.Languages_GetDataNonChinese();
            langComboBox.DataSource = languageList;
            langComboBox.SelectedValue = Program.lbuSettings.LangID;
        }

        private void langComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (langComboBox.SelectedValue == null) return;
            selectedLangID = (long)langComboBox.SelectedValue;
            var row = languageList.Single(r => r.LANGID == selectedLangID);
            bookList = LollyDB.Books_GetDataByLang(selectedLangID);
            bookComboBox.DataSource = bookList;
            bookComboBox.SelectedValue = row.CURBOOKID;
        }

        private void bookComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bookComboBox.SelectedValue == null) return;
            selectedBookID = (long)bookComboBox.SelectedValue;
            var row = bookList.Single(r => r.BOOKID == selectedBookID);
            // Controls for Units
            unitsInAllFromLabel.Text = unitsInAllToLabel.Text = $"({row.UNITSINBOOK} in all)";
            unitFromNumericUpDown.Maximum = unitToNumericUpDown.Maximum = row.UNITSINBOOK;
            unitFromNumericUpDown.Value = row.UNITFROM;
            unitToNumericUpDown.Value = row.UNITTO;
            // Controls for Parts
            var parts = row.PARTS.Split(' ');
            partFromComboBox.Items.Clear();
            partFromComboBox.Items.AddRange(parts);
            partToComboBox.Items.Clear();
            partToComboBox.Items.AddRange(parts);
            partFromComboBox.SelectedIndex = (int)row.PARTFROM - 1;
            partToComboBox.SelectedIndex = (int)row.PARTTO - 1;
            // toCheckBox
            toCheckBox.Checked = row.UNITFROM != row.UNITTO || row.PARTFROM != row.PARTTO;
            toCheckBox_CheckedChanged(null, null);
        }

        private void unitFromNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!toCheckBox.Checked || unitToNumericUpDown.Value < unitFromNumericUpDown.Value)
                unitToNumericUpDown.Value = unitFromNumericUpDown.Value;
        }

        private void unitToNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (unitFromNumericUpDown.Value > unitToNumericUpDown.Value)
                unitFromNumericUpDown.Value = unitToNumericUpDown.Value;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Program.SetLangID(selectedLangID);
            LollyDB.Languages_UpdateBook(selectedBookID, selectedLangID);
            LollyDB.Books_UpdateUnit((int)unitFromNumericUpDown.Value, partFromComboBox.SelectedIndex + 1,
                (int)unitToNumericUpDown.Value, partToComboBox.SelectedIndex + 1, selectedBookID);
        }

        private void toCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            unitToNumericUpDown.Enabled = toCheckBox.Checked;
            unitsInAllToLabel.Enabled = toCheckBox.Checked;
            partToComboBox.Enabled = toCheckBox.Checked;
            if(!toCheckBox.Checked)
            {
                unitToNumericUpDown.Value = unitFromNumericUpDown.Value;
                partToComboBox.SelectedIndex = partFromComboBox.SelectedIndex;
            }
        }

        private void partFromComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!toCheckBox.Checked)
                partToComboBox.SelectedIndex = partFromComboBox.SelectedIndex;
        }
    }
}
