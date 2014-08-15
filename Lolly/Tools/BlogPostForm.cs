using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using LollyBase;

namespace Lolly
{
    public partial class BlogPostForm : Form
    {
        private List<string> patterns;
        private string xmlFileName;

        public BlogPostForm()
        {
            InitializeComponent();
        }

        private void BlogPostForm_Load(object sender, EventArgs e)
        {
            patterns = File.ReadAllLines(Program.appDataFolder + @"blog\SentPatterns.txt").ToList();
            patterns.RemoveAll(s => s == "");

            var reg = new Regex("<a.+?>(.+?)</a>");
            var patternNames = new string[]{"No Sentence Pattern"}
                .Concat(from p in patterns select reg.Match(p).Groups[1].Value);
            patternNamesToolStripComboBox.Items.AddRange(patternNames.ToArray());
            patternNamesToolStripComboBox.SelectedIndex = 0;
        }

        private void newWordToolStripButton_Click(object sender, EventArgs e)
        {
            var xml = @"<line>
		        <original>（）：</original><definition></definition><translation></translation>
		        </line>";
            NewNote(xml, lineOnlyToolStripButton.Checked);
        }

        private void newPatternToolStripButton_Click(object sender, EventArgs e)
        {
            var index = (int)patternNamesToolStripComboBox.SelectedIndex;
            var xml = string.Format(
                @"<note><line>
		        <original>～：</original><definition></definition><translation></translation>
		        </line>{0}</note>",
                index == 0 ? "" : string.Format("<line>{0}</line>", patterns[index - 1]));
            NewNote(xml);
            patternNamesToolStripComboBox.SelectedIndex = 0;
        }

        private void NewNote(string note, bool noNoteTag = true)
        {
            if (!noNoteTag)
                note = string.Format("<note>{0}</note>", note);

            Clipboard.SetText(note);
            Win32.SetForegroundWindow(applicationControl1.AppHandle);
            Win32.keybd_event((byte)Keys.ControlKey, Win32.MapVirtualKey((byte)Keys.ControlKey, 0), 0, 0);
            Win32.keybd_event((byte)'V', Win32.MapVirtualKey((byte)'V', 0), 0, 0);
            Win32.keybd_event((byte)'V', Win32.MapVirtualKey((byte)'V', 0), (byte)Keys.Up, 0);
            Win32.keybd_event((byte)Keys.ControlKey, Win32.MapVirtualKey((byte)Keys.ControlKey, 0), (byte)Keys.Up, 0);

            lineOnlyToolStripButton.Checked = false;
        }

        private void originalToolStripButton_Click(object sender, EventArgs e)
        {
            NewNote("<original>（）：</original>");
        }

        private void definitionToolStripButton_Click(object sender, EventArgs e)
        {
            NewNote("<definition></definition>");
        }

        private void translationToolStripButton_Click(object sender, EventArgs e)
        {
            NewNote("<translation></translation>");
        }

        private void sourceViewToolStripButton_Click(object sender, EventArgs e)
        {
            var isSourceView = sourceViewToolStripButton.Checked;
            lineOnlyToolStripButton.Enabled = !isSourceView;
            newWordToolStripButton.Enabled = !isSourceView;
            newPatternToolStripButton.Enabled = !isSourceView;
            patternNamesToolStripComboBox.Enabled = !isSourceView;
            originalToolStripButton.Enabled = !isSourceView;
            definitionToolStripButton.Enabled = !isSourceView;
            translationToolStripButton.Enabled = !isSourceView;
            applicationControl1.Visible = !isSourceView;

            b_ToolStripButton.Enabled = isSourceView;
            i_ToolStripButton.Enabled = isSourceView;
            sourceTextBox.Visible = isSourceView;

            if (isSourceView)
            {
                Win32.ShowWindow(applicationControl1.AppHandle, Win32.WindowShowStyle.Hide);
                xmlFileName = Win32.GetText(applicationControl1.AppHandle)
                    .Substring("XML-Notepad - ".Length).TrimEnd('*');
                if (xmlFileName != "")
                    sourceTextBox.Text = File.ReadAllText(xmlFileName);
            }
            else
            {
                if (xmlFileName != "")
                    File.WriteAllText(xmlFileName, sourceTextBox.Text);
                Win32.ShowWindow(applicationControl1.AppHandle, Win32.WindowShowStyle.Show);
            }
        }

        private void bi_ToolStripButton_Click(object sender, EventArgs e)
        {
            var str = new Regex("<.+?>").Replace(sourceTextBox.SelectedText, "");
            sourceTextBox.SelectedText = string.Format(
                sender == b_ToolStripButton ? "<b>{0}</b>" : "<i>{0}</i>",
                str);
        }
    }
}
