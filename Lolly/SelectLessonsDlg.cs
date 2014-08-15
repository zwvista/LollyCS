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
    public partial class SelectLessonsDlg : Form
    {
        private int selectedLangID;
        private int selectedBookID;
        public bool ActiveIncluded
        {
            get
            {
                return activeIncludedCheckBox.Checked;
            }
        }

        private IEnumerable<MLANGUAGE> languageList;
        private IEnumerable<MBOOK> bookList;

        public SelectLessonsDlg(bool activeIncluded)
        {
            InitializeComponent();
            activeIncludedCheckBox.Checked = activeIncluded;
        }

        private void SelectLessonsForm_Load(object sender, EventArgs e)
        {
            languageList = Languages.GetData();
            langComboBox.DataSource = languageList;
            langComboBox.SelectedValue = Program.lblSettings.LangID;
        }

        private void langComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (langComboBox.SelectedValue == null) return;
            selectedLangID = (int)langComboBox.SelectedValue;
            var row = languageList.Single(r => r.LANGID == selectedLangID);
            bookList = Books.GetDataByLang(selectedLangID);
            bookComboBox.DataSource = bookList;
            bookComboBox.SelectedValue = row.CURBOOKID;
        }

        private void bookComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bookComboBox.SelectedValue == null) return;
            selectedBookID = (int)bookComboBox.SelectedValue;
            var row = bookList.Single(r => r.BOOKID == selectedBookID);
            lessonInAllFromLabel.Text = lessonInAllToLabel.Text = string.Format("({0} in all)", row.NUMLESSONS);
            lessonFromNumericUpDown.Maximum = row.NUMLESSONS;
            lessonToNumericUpDown.Maximum = row.NUMLESSONS;
            lessonFromNumericUpDown.Value = row.LESSONFROM;
            lessonToNumericUpDown.Value = row.LESSONTO;
            var parts = row.PARTS.Split(' ');
            partFromComboBox.Items.Clear();
            partFromComboBox.Items.AddRange(parts);
            partToComboBox.Items.Clear();
            partToComboBox.Items.AddRange(parts);
            partFromComboBox.SelectedIndex = row.PARTFROM - 1;
            partToComboBox.SelectedIndex = row.PARTTO - 1;
            toCheckBox.Checked = row.LESSONFROM != row.LESSONTO || row.PARTFROM != row.PARTTO;
            toCheckBox_CheckedChanged(null, null);
        }

        private void lessonFromNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!toCheckBox.Checked || lessonToNumericUpDown.Value < lessonFromNumericUpDown.Value)
                lessonToNumericUpDown.Value = lessonFromNumericUpDown.Value;
        }

        private void lessonToNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (lessonFromNumericUpDown.Value > lessonToNumericUpDown.Value)
                lessonFromNumericUpDown.Value = lessonToNumericUpDown.Value;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Program.SetLangID(selectedLangID);
            Languages.UpdateBook(selectedBookID, selectedLangID);
            Books.UpdateLesson((int)lessonFromNumericUpDown.Value, partFromComboBox.SelectedIndex + 1,
                (int)lessonToNumericUpDown.Value, partToComboBox.SelectedIndex + 1, selectedBookID);
        }

        private void toCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            lessonToNumericUpDown.Enabled = toCheckBox.Checked;
            lessonInAllToLabel.Enabled = toCheckBox.Checked;
            partToComboBox.Enabled = toCheckBox.Checked;
            if(!toCheckBox.Checked)
            {
                lessonToNumericUpDown.Value = lessonFromNumericUpDown.Value;
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
