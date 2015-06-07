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
    public partial class MDIForm : Form
    {
        private MLANGUAGE langRow;
        private MBOOK bookRow;

        public MDIForm()
        {
            InitializeComponent();
        }

        private void changeLBLSettings()
        {
            langRow = LollyDB.Languages_GetDataByLang(Program.lbuSettings.LangID);
            Program.lbuSettings.LangName = langRow.CHNNAME;
            Program.lbuSettings.BookID = (int)langRow.CURBOOKID;
            bookRow = LollyDB.Books_GetDataByBook(Program.lbuSettings.BookID);
            Program.lbuSettings.BookName = bookRow.BOOKNAME;
            Program.lbuSettings.UnitFrom = bookRow.UNITFROM;
            Program.lbuSettings.PartFrom = bookRow.PARTFROM;
            Program.lbuSettings.UnitTo = bookRow.UNITTO;
            Program.lbuSettings.PartTo = bookRow.PARTTO;
        }

        private void MDIForm_Load(object sender, EventArgs e)
        {
            changeLBLSettings();
            Program.InitVoices();
        }

        private void selectUnitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new SelectUnitsDlg(true);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                changeLBLSettings();
                if (dlg.ActiveIncluded && ActiveMdiChild is ILangBookUnits)
                    ((ILangBookUnits)ActiveMdiChild).UpdatelbuSettings();
            }
        }

        private void NewChildForm(Form childForm)
        {
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void wordsUnitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //new Finder<WordsUnitsForm>(this).NextChild();
            NextChildForm("WordsUnitsForm");
        }

        private void wordsLanguageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //new Finder<WordsLangForm>(this).NextChild();
            NextChildForm("WordsLangForm");
        }

        private void wordsAtWillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //new Finder<WordsAtWillForm>(this).NextChild();
            NextChildForm("WordsAtWillForm");
        }

        private void wordsBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //new Finder<WordsBooksForm>(this).NextChild();
            NextChildForm("WordsBooksForm");
        }

        private void phrasesUnitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //new Finder<PhrasesUnitsForm>(this).NextChild();
            NextChildForm("PhrasesUnitsForm");
        }

        private void phrasesLanguageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //new Finder<PhrasesLangForm>(this).NextChild();
            NextChildForm("PhrasesLangForm");
        }

        private void wordsUnitsEBWinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //new Finder<WordsUnitsEBForm>(this).NextChild();
            NextChildForm("WordsUnitsEBForm");
        }

        private void wordsAtWillEBWinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //new Finder<WordsAtWillEBForm>(this).NextChild();
            NextChildForm("WordsAtWillEBForm");
        }

        private void autoCorrectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewChildForm(new AuxAutoCorrectForm());
        }

        private void booksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewChildForm(new AuxBooksForm());
        }

        private void dictionariesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewChildForm(new AuxDictionariesForm());
        }

        private void pictureBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewChildForm(new AuxPicBooksForm());
        }

        private void webExtractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewChildForm(new AuxWebExtractForm());
        }

        private void webTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewChildForm(new AuxWebTextForm());
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void webBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewChildForm(new WebBrowserForm());
        }

        private void extractWebDictToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new ExtractWebDictOptionsDlg();
            if (dlg.ShowDialog() == DialogResult.OK)
                ExtractTranslation(dlg.SelectedWords, dlg.SelectedDicts, dlg.OverwriteDB);
        }

        public void ExtractTranslation(string[] words, string[] dicts, bool overwriteDB, DictWebBrowser dwb = null, string ifrId = "")
        {
            foreach(var dict in dicts)
                NewChildForm(new ExtractWebDictForm(words, dict, overwriteDB, dwb, ifrId));
            if (dwb != null)
                dwb.FindForm().Activate();
        }

        private void blogPostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewChildForm(new BlogPostForm());
        }

        private void autoCorrectTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new AutoCorrectTextDlg();
            dlg.ShowDialog();
        }

        private void text2PostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new Text2PostDlg();
            dlg.ShowDialog();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewChildForm(new TestDlg());
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
                childForm.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutDlg().ShowDialog();
        }

        private void readNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new ReadNumberDlg();
            dlg.ShowDialog();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new OptionsDlg();
            if (dlg.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void speakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Speak(Program.lbuSettings.LangID, Clipboard.GetText());
        }

        private void stopSpeakingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Speak(0, "");
            Program.Speak(1, "");
            Program.Speak(Program.lbuSettings.LangID, "");
        }

        private void speakChineseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Speak(0, Clipboard.GetText());
        }

        private void speakEnglishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Speak(1, Clipboard.GetText());
        }

        private void wordsUnitsNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewChildForm(new WordsUnitsForm());
        }

        private void wordsLanguageNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewChildForm(new WordsLangForm());
        }

        private void wordsAtWillNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewChildForm(new WordsAtWillForm());
        }

        private void wordsBooksNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewChildForm(new WordsBooksForm());
        }

        private void phrasesUnitsNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewChildForm(new PhrasesUnitsForm());
        }

        private void phrasesLanguageNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewChildForm(new PhrasesLangForm());
        }

        private void wordsUnitsEBWinNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewChildForm(new WordsUnitsEBForm());
        }

        private void wordsAtWillEBWinNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewChildForm(new WordsAtWillEBForm());
        }

        private void NewChildForm(Type t)
        {
            var f = Activator.CreateInstance(t) as Form;
            NewChildForm(f);
        }

        private void NextChildForm(string formClassName)
        {
            var t = Type.GetType("Lolly." + formClassName);
            var fs = MdiChildren.ToList();
            if (fs.Count == 0)
            {
                NewChildForm(t);
                return;
            }

            var index = fs.IndexOf(ActiveMdiChild) + 1;
            fs = fs.Skip(index).Concat(fs.Take(index)).ToList();

            var f = fs.FirstOrDefault(f_ => f_.GetType() == t);
            if (f == null)
                NewChildForm(t);
            else
                f.Focus();
        }

        class Finder<T> where T : Form, new()
        {
            MDIForm mdi;

            public Finder(MDIForm mdi)
            {
                this.mdi = mdi;
            }

            private void NewChild()
            {
                mdi.NewChildForm(new T());
            }

            public void NextChild()
            {
                var fs = mdi.MdiChildren.ToList();
                if (fs.Count == 0)
                {
                    NewChild();
                    return;
                }

                var index = fs.IndexOf(mdi.ActiveMdiChild) + 1;
                fs = fs.Skip(index).Concat(fs.Take(index)).ToList();

                var f = fs.FirstOrDefault(f_ => f_ is T);
                if (f == null)
                    NewChild();
                else
                    f.Focus();
            }
        }

        private void currentWindowNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null) return;
            var t = ActiveMdiChild.GetType();
            switch (t.ToString())
            {
                case "Lolly.ExtractWebDictForm":
                    return;
            }
            NewChildForm(t);
        }
    }
}
