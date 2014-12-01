using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LollyBase;

namespace Lolly
{
    public class DictWebBrowser : WebBrowser
    {
        private DictLangConfig config;
        private List<UIDictItem> dictItems;
        private bool emptyTrans;
        private bool automationDone;

        public DictWebBrowser(DictLangConfig config)
        {
            this.config = config;
        }

        public void SetDict(List<UIDictItem> items)
        {
            dictItems = items;
        }

        public void SetDict(UIDictItem item)
        {
            dictItems = new List<UIDictItem> {item};
        }

        public void UpdateLiveHtml(string word, string dict, string translation)
        {
            DocumentText = DocumentText.Replace(Program.GetLiveHtml(word, dict),
                translation);
        }

        private MDICTALL FindDict(string dict)
        {
            return config.dictAllList.SingleOrDefault(r => r.DICTNAME == dict);
        }

        private string GetTranslation(MDICTALL dictRow, string word)
        {
            var wordRow = DictEntity.GetDataByWordDictTable(word, dictRow.DICTTABLE);
            return wordRow == null ? "" : wordRow.TRANSLATION;
        }

        public void UpdateHtml(string word, List<MAUTOCORRECT> autoCorrectList)
        {
            MDICTALL dictRow = null;

            Func<string, string> GetTemplatedHtml = str =>
                string.Format(dictRow.TEMPLATE, word, Program.appDataFolderInHtml + "css/", str);

            Func<string> GetTranslationHtml = () =>
            {
                var translation = GetTranslation(dictRow, word);
                emptyTrans = emptyTrans && translation == "";
                return GetTemplatedHtml(translation);
            };

            Func<string> GetDictURLForword = () =>
                Program.GetDictURLForWord(word, dictRow, autoCorrectList);

            Func<string> GetLingoesHtml = () => Program.lingoes.Search(word, config.dictsLingoes);

            Func<string> GetLingoesAllHtml = () => Program.lingoes.Search(word);

            Func<string> GetFrhelperHtml = () => GetTemplatedHtml(Program.frhelper.Search(word, dictRow.TRANSFORM_WIN));

            Func<string, string> GetLiveHtml = dict =>
            {
                var f = Parent;
                while (f as MDIForm == null)
                    f = f.Parent;
                ((MDIForm)f).ExtractTranslation(new[] { word }, new[] { dict }, true, this);
                return GetTemplatedHtml(Program.GetLiveHtml(word, dict));
            };

            emptyTrans = true;
            if (word == "")
                DocumentText = "";
            else if (dictItems.Count == 1)
            {
                var item = dictItems.First();
                dictRow = FindDict(item.Name);
                if (item.Type == DictNames.ONLINE && item.Name != DictNames.FRHELPER ||
                    item.Type == DictNames.CONJUGATOR ||
                    item.Type == DictNames.WEB)
                {
                    automationDone = false;
                    Navigate(GetDictURLForword());
                }
                else
                    DocumentText =
                        item.Type == DictNames.LOCAL || item.Type == DictNames.OFFLINE ? GetTranslationHtml() :
                        item.Type == DictNames.LIVE ? GetLiveHtml(item.Name) :
                        item.Type == DictNames.ONLINE && item.Name == DictNames.FRHELPER ? GetFrhelperHtml() :
                        item.Name == DictNames.LINGOES ? GetLingoesHtml() :
                        item.Name == DictNames.LINGOESALL ? GetLingoesAllHtml() :
                        "";
            }
            else
            {
                Func<string, string> GetIFrameOfflineText = str => string.Format(
                    "<iframe frameborder='0' style='width:100%; display:block' onload='setFrameContent(this, \"{0}\");'></iframe>\n",
                    str.Replace("'", "&#39;").Replace("\"", "\\\"").Replace("\r\n", "\\r\\n").Replace("\n", "\\n"));

                Func<string, string> GetIFrameOnlineText = url => string.Format(
                    "<iframe frameborder='1' style='width:100%; height:500px; display:block' src='{0}'></iframe>\n",
                    url);

                var sb = new StringBuilder("<html><head><META content=\"IE=10.0000\" http-equiv=\"X-UA-Compatible\"></head><body>\n");
                sb.AppendFormat("<script type=\"text/javascript\">\n{0}\n</script>\n", Program.js);

                foreach (var item in dictItems)
                {
                    dictRow = FindDict(item.Name);
                    if (item.Name == DictNames.LINGOES)
                        sb.Append(GetIFrameOfflineText(GetLingoesHtml()));
                    else if (item.Name == DictNames.LINGOESALL)
                        sb.Append(GetIFrameOfflineText(GetLingoesAllHtml()));
                    else if (item.Type == DictNames.LOCAL ||
                        item.Type == DictNames.OFFLINE)
                        sb.Append(GetIFrameOfflineText(GetTranslationHtml()));
                    else if (item.Type == DictNames.LIVE)
                        sb.Append(GetIFrameOfflineText(GetLiveHtml(item.Name)));
                    else if (item.Name == DictNames.FRHELPER)
                        sb.Append(GetIFrameOfflineText(GetFrhelperHtml()));
                    else
                        sb.Append(GetIFrameOnlineText(GetDictURLForword()));
                }

                sb.Append("</body></html>\n");
                DocumentText = sb.ToString();
            };
        }

        public bool CanDeleteTranslation()
        {
            return dictItems.Any(i => i.Type == DictNames.OFFLINE);
        }

        public bool CanEditTranslation()
        {
            return dictItems.Count == 1 && dictItems[0].Type == DictNames.OFFLINE;
        }

        public bool CanExtractAndOverriteTranslation()
        {
            return dictItems.Any(i => i.Type == DictNames.OFFLINE) ||
                dictItems.Count == 1 && dictItems[0].Type == DictNames.ONLINE;
        }

        public bool CanExtractAndAppendTranslation()
        {
            return dictItems.Count == 1 && dictItems[0].Type == DictNames.ONLINE;
        }

        public bool DoDeleteTranslation(string word)
        {
            var items = dictItems.Where(i => i.Type == DictNames.OFFLINE).ToList();
            bool isPlural = items.Count > 1;
            var sb = new StringBuilder();
            sb.AppendFormat("The translation{0} of the word \"{1}\" in the following dictionar{2}\n\n",
                isPlural ? "s" : "", word, isPlural ? "ies" : "y");
            foreach (var item in items)
                sb.AppendFormat("\t{0}\n", item.Name);
            sb.Append("\nwill be DELETED. Are you sure?");
            if (MessageBox.Show(sb.ToString(), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return false;
            foreach (var item in items)
            {
                var dictRow = FindDict(item.Name);
                DictEntity.Update(ExtensionClass.NOTRANSLATION, word, dictRow.DICTTABLE);
            }
            return true;
        }

        public bool DoEditTranslation(string word)
        {
            var dictRow = FindDict(dictItems[0].Name);
            var translation = GetTranslation(dictRow, word);

            var dlg = new EditTransDlg();
            dlg.translationTextBox.Text = translation;
            if (dlg.ShowDialog() != DialogResult.OK) return false;

            DictEntity.Update(dlg.translationTextBox.Text, word, dictRow.DICTTABLE);
            return true;
        }

        public bool DoExtractTranslation(string word, bool overriteDB)
        {
            if (dictItems.Count == 1 && dictItems[0].Type == DictNames.ONLINE)
            {
                var dictName = dictItems[0].Name;
                var dictRow = FindDict(dictName);
                var msg = string.Format("The translation from the url \"{0}\" will be EXTRACTED and {1} " +
                    "the translation of the word \"{2}\" in the dictionary \"{3}\". Are you sure?",
                    Url.AbsoluteUri,
                    overriteDB ? "used to REPLACE" : "APPENDED to",
                    word, dictName);
                if (MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return false;
                var wordRow = Program.OpenDictTable(word, dictRow.DICTTABLE);
                Program.UpdateDictTable(this, wordRow, dictRow, !overriteDB);
            }
            else
            {
                var items = dictItems.Where(i => i.Type == DictNames.OFFLINE).ToList();
                bool isPlural = items.Count > 1;
                var sb = new StringBuilder();
                if (!emptyTrans)
                {
                    sb.AppendFormat("The translation{0} of the word \"{1}\" in the following dictionar{2}\n\n",
                        isPlural ? "s" : "", word, isPlural ? "ies" : "y");
                    foreach (var item in items)
                        sb.AppendFormat("\t{0}\n", item.Name);
                    sb.Append("\nwill be DELETED and EXTRACTED from the web again. Are you sure?");
                    if (MessageBox.Show(sb.ToString(), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        return false;
                }
                ((MDIForm)FindForm().Parent.Parent).ExtractTranslation(new[]{word},
                    dictItems.Select(i => i.Name).ToArray(), true);
            }
            return true;
        }

        public void DoWebAutomation(string word)
        {
            if (dictItems.Count != 1) return;
            var dictType = dictItems[0].Type;
            var dictRow = FindDict(dictItems[0].Name);
            if ((dictType == DictNames.ONLINE || dictType == DictNames.WEB) &&
                dictRow.AUTOMATION != null && !automationDone)
            {
                this.DoWebAutomation(dictRow.AUTOMATION, word);
                automationDone = true;
            }
        }
    };
}
