using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LollyBase;
using mshtml;
using System.Text.RegularExpressions;
using System.IO;

namespace Lolly
{
    public partial class TestDlg : Form
    {
        private Regex reg = new Regex(@"表現文型辞典\[駆け込み寺\](\d+)\[(.+?)\]");
        private string[] sent_patterns = new string[488];
        private int page = 0;

        public TestDlg()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            page = 0;
            Navigate();
        }

        private void Navigate()
        {
            var url = string.Format("http://2ch.in/forum-22-{0}.html", ++page);
            webBrowser1.Navigate(url);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.ReadyState != WebBrowserReadyState.Complete) return;
            var doc = webBrowser1.GetHTMLDoc();
            var all = (from IHTMLElement i in doc.all 
                      where i is HTMLAnchorElement
                      let elem = i as HTMLAnchorElement
                      let text = elem.innerText
                      where text != null && text.StartsWith("表現文型辞典")
                      let m = reg.Match(text)
                      let num = m.Groups[1].Value
                      let pattern = m.Groups[2].Value
                      select new { num,
                          tag = string.Format(@"<a href=""{0}"">{1} {2}</a>",
                                elem.href, num, pattern)
                      });
            foreach (var v in all)
                sent_patterns[int.Parse(v.num) - 1] = v.tag;
            if (page < 25)
                Navigate();
            else
                File.WriteAllLines(Program.appDataFolder + @"blog\SentPatterns.txt", sent_patterns);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var db = new Entities())
            {
                foreach (var item in db.SWEBEXTRACT)
                {
                }
                db.SaveChanges();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }
    }
}
