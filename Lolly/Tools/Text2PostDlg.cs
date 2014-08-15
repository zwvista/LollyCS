using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Web;

namespace Lolly
{
    public partial class Text2PostDlg : Form
    {
        private string fmtPost, fmtParagraph1, fmtParagraph2;
        private Regex regBlankLine = new Regex(@"(?:^|\r\n)(?:\s*(?:$|\r\n))+");
        public Text2PostDlg()
        {
            InitializeComponent();

            textBox1.KeyDown += Program.textBoxSelectAll_KeyDown;
            textBox2.KeyDown += Program.textBoxSelectAll_KeyDown;
            paragraphEndComboBox.SelectedIndex = 1;

            var blogFolder = Program.appDataFolder + "blog\\";
            fmtPost = File.ReadAllText(blogFolder + "PostFormat.txt");
            fmtParagraph1 = File.ReadAllText(blogFolder + "ParagraphFormat1.txt");
            fmtParagraph2 = File.ReadAllText(blogFolder + "ParagraphFormat2.txt");
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            var post = paragraphEndComboBox.SelectedIndex == 0 ?

                from line in textBox1.Lines
                let line2 = HttpUtility.HtmlEncode(line.Trim())
                where line2 != ""
                select string.Format(fmtParagraph1, line2) :

                from g in regBlankLine.Split(HttpUtility.HtmlEncode(textBox1.Text))
                where g != ""
                let lines = g.Split(new[]{Environment.NewLine}, StringSplitOptions.None)
                let n = lines.Length
                select (n > 1 ? string.Join("", 
                    from line in lines.Take(n - 1)
                    select string.Format(fmtParagraph2, line.Trim()) + Environment.NewLine) :
                    "") + string.Format(fmtParagraph1, lines[n - 1].Trim());

            textBox2.Text = string.Format(fmtPost, string.Join(Environment.NewLine, post));
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                File.WriteAllText(saveFileDialog1.FileName, textBox2.Text);
        }
    }
}
