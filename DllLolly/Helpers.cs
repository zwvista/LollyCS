using System;
using System.Windows.Forms;

namespace Lolly
{
    public class TextBoxAutoSelectHelper
    {
        private bool selectAll = false;
        private TextBox textBox;

        public TextBoxAutoSelectHelper(TextBox tb)
        {
            textBox = tb;
            textBox.Click += textBox_Click;
            textBox.Enter += textBox_Enter;
        }

        private void textBox_Click(object sender, EventArgs e)
        {
            if (selectAll)
            {
                textBox.SelectAll();
                selectAll = false;
            }
        }

        private void textBox_Enter(object sender, EventArgs e)
        {
            selectAll = true;
            textBox.SelectAll();
        }
    }

    public class ComboBoxAutoCompleteHelper
    {
        private ComboBox comboBox;

        public ComboBoxAutoCompleteHelper(ComboBox cb)
        {
            comboBox = cb;
            comboBox.KeyPress += comboBox_KeyPress;
        }

        private void comboBox_KeyPress(Object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            this.AutoComplete(sender as ComboBox, e, false);
        }

        private void AutoComplete(ComboBox cb, System.Windows.Forms.KeyPressEventArgs e, bool blnLimitToList)
        {
            string strFindStr = "";

            if (e.KeyChar == (char)8)
            {
                if (cb.SelectionStart <= 1)
                {
                    cb.Text = "";
                    return;
                }

                if (cb.SelectionLength == 0)
                    strFindStr = cb.Text.Substring(0, cb.Text.Length - 1);
                else
                    strFindStr = cb.Text.Substring(0, cb.SelectionStart - 1);
            }
            else
            {
                if (cb.SelectionLength == 0)
                    strFindStr = cb.Text + e.KeyChar;
                else
                    strFindStr = cb.Text.Substring(0, cb.SelectionStart) + e.KeyChar;
            }

            int intIdx = -1;

            // Search the string in the ComboBox list.

            intIdx = cb.FindString(strFindStr);

            if (intIdx != -1)
            {
                cb.SelectedText = "";
                cb.SelectedIndex = intIdx;
                cb.SelectionStart = strFindStr.Length;
                cb.SelectionLength = cb.Text.Length;
                e.Handled = true;
            }
            else
            {
                e.Handled = blnLimitToList;
            }

        }

        public void AddTextToList()
        {
            var text = comboBox.Text;
            if (text != "" && !comboBox.Items.Contains(text))
                comboBox.Items.Add(text);
        }
    }
}
