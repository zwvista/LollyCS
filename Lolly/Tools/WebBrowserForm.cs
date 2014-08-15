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
    public partial class WebBrowserForm : Form
    {
        private ComboBoxAutoCompleteHelper addressHelper;

        public WebBrowserForm()
        {
            InitializeComponent();
            addressHelper = new ComboBoxAutoCompleteHelper(addressToolStripComboBox.ComboBox);
        }

        private void goToolStripButton_Click(object sender, EventArgs e)
        {
            addressHelper.AddTextToList();
            webBrowser1.Navigate(addressToolStripComboBox.Text);
        }

        private void addressToolStripComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                goToolStripButton.PerformClick();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                addressToolStripComboBox.Text = openFileDialog1.FileName;
                goToolStripButton.PerformClick();
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            //var row = dataSet2.WEBEXTRACT[0];
            //var doc = (mshtml.IHTMLDocument2)webBrowser1.Document.DomDocument;
            //var text = doc.body.parentElement.outerHTML;
            //text = Program.ExtractFromWeb(text, row.TEXTFROM, row.INCLUDEFROM,
            //    row.TEXTTO, row.INCLUDETO, row.TEXTWEB, row.TEXTRES);
            //text = string.Format(row.NEWHEAD.Replace("%s", "{0}"), doc.title) + text + row.NEWFOOT;

            //saveFileDialog1.InitialDirectory = row.FOLDER;
            //if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            //{

            //}
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //if (webBrowser1.ReadyState != WebBrowserReadyState.Complete) return;
            //webextractTableAdapter.FillByURL(dataSet2.WEBEXTRACT, webBrowser1.Url.AbsoluteUri);
            //saveToolStripButton.Enabled = dataSet2.WEBEXTRACT.Count > 0;
        }
    }
}
