using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using LollyBase;

namespace Lolly
{
    [ComVisible(true)]
    public partial class ExtractWebDictForm : Form, IOleClientSite
    {
        private LangBookLessonSettings lblSettings;
        private string[] words;
        private bool overwriteDB;
        private int wordIndex = 0;
        private string word = "";
        private MDICTALL dictRow;
        private MDICTENTITY wordRow;
        private DictWebBrowserStatus dictStatus = DictWebBrowserStatus.Navigating;
        private DictWebBrowser dwb;
        private IEnumerable<MAUTOCORRECT> autoCorrectList;

        [DispId(-5512)]
        public virtual int IDispatch_Invoke_Handler()
        {
            return (int)(BrowserOptions.DLCTL_SILENT |
                BrowserOptions.DLCTL_NO_SCRIPTS |
                BrowserOptions.DLCTL_NO_JAVA |
                BrowserOptions.DLCTL_NO_RUNACTIVEXCTLS | BrowserOptions.DLCTL_NO_DLACTIVEXCTLS |
                BrowserOptions.DLCTL_NO_CLIENTPULL);
            //return 0;
        }

        #region IOleClientSite Members

        public int SaveObject()
        {
            return 0;
        }

        public int GetMoniker(int dwAssign, int dwWhichMoniker, out object moniker)
        {
            moniker = this;
            return 0;
        }

        public int GetContainer(out object container)
        {
            container = this;
            return 0;
        }

        public int ShowObject()
        {
            return 0;
        }

        public int OnShowWindow(int fShow)
        {
            return 0;
        }

        public int RequestNewObjectLayout()
        {
            return 0;
        }

        #endregion

        public ExtractWebDictForm(string[] words, string dict, bool overwriteDB, DictWebBrowser dwb = null)
        {
            InitializeComponent();
            this.lblSettings = Program.lblSettings;
            this.words = words;
            this.overwriteDB = overwriteDB;
            this.dwb = dwb;

            dictToolStripTextBox.Text = dict;
            dictRow = DictAll.GetDataByLangDict(lblSettings.LangID, dict);
        }

        private void ExtractWebDictForm_Shown(object sender, EventArgs e)
        {
            autoCorrectList = AutoCorrect.GetDataByLang(lblSettings.LangID);
            langToolStripTextBox.Text = lblSettings.LangName;
            IOleObject obj = (IOleObject)webBrowser1.ActiveXInstance;
            obj.SetClientSite(this);
            ////notify browser of change
            //IOleControl obj2 = (IOleControl)webBrowser1.ActiveXInstance;
            //obj2.OnAmbientPropertyChange(-5512);
            GetNextWord();
            while (!NeedSearchDict())
                if (!GetNextSearch())
                    return;
            SearchDictForWord();
        }

        private void GetNextWord()
        {
            word = words[wordIndex];
            wordRow = Program.OpenDictTable(word, dictRow.DICTTABLE);
        }

        private bool NeedSearchDict()
        {
            return overwriteDB || string.IsNullOrEmpty(wordRow.TRANSLATION);
        }

        private bool GetNextSearch()
        {
            if (++wordIndex == words.Length)
            {
                Close();
                return false;
            }
            else
            {
                GetNextWord();
                return true;
            }
        }

        private void SearchDictForWord()
        {
            wordToolStripLabel.Text = string.Format("Word: {0} / {1}", wordIndex + 1, words.Length);
            wordToolStripTextBox.Text = word;
            dictStatus = DictWebBrowserStatus.Navigating;
            if (dictRow.DICTNAME == "Frhelper")
                webBrowser1.DocumentText = Program.frhelper.Search(word);
            else
            {
                var url = Program.GetDictURLForWord(word, dictRow, autoCorrectList);
                webBrowser1.Navigate(url);
            }
        }

        private void ExtractWebDict()
        {
            if (dwb == null)
                Program.UpdateDictTable(webBrowser1, wordRow, dictRow, false);
            else
                dwb.UpdateLiveHtml(word, dictRow.DICTNAME, webBrowser1.ExtractFromWeb(dictRow, ExtensionClass.NOTRANS));
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.ReadyState != WebBrowserReadyState.Complete ||
                dictStatus == DictWebBrowserStatus.Ready) return;

            var automation = dictRow.AUTOMATION;

            dictStatus = dictStatus == DictWebBrowserStatus.Navigating && !string.IsNullOrEmpty(automation) &&
                webBrowser1.DoWebAutomation(automation, word) &&
                !dictRow.AUTOJUMP ? DictWebBrowserStatus.Automating :
                DictWebBrowserStatus.Ready;

            if (dictStatus == DictWebBrowserStatus.Ready)
            {
                timerDocumentCompleted.Enabled = true;
                timerDocumentCompleted.Interval = (int)dictRow.WAIT;
            }
        }

        private void timerDocumentCompleted_Tick(object sender, EventArgs e)
        {
            timerDocumentCompleted.Enabled = false;
            ExtractWebDict();
            do
                if (!GetNextSearch())
                    return;
            while (!NeedSearchDict());
            SearchDictForWord();
        }

    }
}
