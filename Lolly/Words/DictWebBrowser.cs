using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using LollyShared;

namespace Lolly
{
    public class DictWebBrowser : WebBrowser
    {
        private DictLangConfig config;
        private List<UIDictItem> dictItems;
        private bool emptyTrans;
        private bool automationDone;

        private Regex regBody = new Regex("(?:<(?:body|BODY).*?>)((?:.*\r?\n)*?.*?)(?:</body>|</BODY>)");

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

        private MDICTALL FindDict(string dict)
        {
            return config.dictAllList.SingleOrDefault(r => r.DICTNAME == dict);
        }

        private string GetTemplatedHtml(MDICTALL dictRow, string word, string str)
        {
            return string.Format(dictRow.TEMPLATE, word, Program.appDataFolderInHtml + "css/", str);
        }

        public void UpdateLiveHtml(string ifrId, string word, string dict, string translation)
        {
            var text = Program.GetLiveHtml(word, dict);
            if (ifrId == "")
                DocumentText = DocumentText.Replace(text, translation);
            else
            {
                var dictRow = FindDict(dict);
                var ifrContent = GetTemplatedHtml(dictRow, word, translation);
                var bodyContent = regBody.Match(ifrContent).Groups[1].Value;
                object[] args = { ifrId, bodyContent };
                Document.InvokeScript("updateLiveHtml", args);
            }
        }

        private string GetTranslation(MDICTALL dictRow, string word)
        {
            var wordRow = LollyDB.DictEntity_GetDataByWordDictTable(word, dictRow.DICTTABLE);
            return wordRow?.TRANSLATION ?? "";
        }

        public void UpdateHtml(string word, List<MAUTOCORRECT> autoCorrectList)
        {
            MDICTALL dictRow = null;

            Func<string> GetTranslationHtml = () =>
            {
                var translation = GetTranslation(dictRow, word);
                emptyTrans = emptyTrans && translation == "";
                return GetTemplatedHtml(dictRow, word, translation);
            };

            Func<string> GetDictURLForword = () =>
                Program.GetDictURLForWord(word, dictRow, autoCorrectList);

            Func<string> GetLingoesHtml = () => Program.lingoes.Search(word, config.dictsLingoes);

            Func<string> GetLingoesAllHtml = () => Program.lingoes.Search(word);

            Func<string> GetFrhelperHtml = () => GetTemplatedHtml(dictRow, word, Program.frhelper.Search(word, dictRow.TRANSFORM_WIN));

            Func<string, string, string> GetLiveHtml = (dict, ifrId) =>
            {
                var f = Parent;
                while (f as MDIForm == null)
                    f = f.Parent;
                ((MDIForm)f).ExtractTranslation(new[] { word }, new[] { dict }, true, this, ifrId);
                return GetTemplatedHtml(dictRow, word, Program.GetLiveHtml(word, dict));
            };

            emptyTrans = true;
            if (word == "")
                DocumentText = "";
            else if (dictItems.Count == 1)
            {
                var item = dictItems.First();
                dictRow = FindDict(item.Name);
                if (item.Type == DictNames.ONLINE && item.Name != DictNames.FRHELPER ||
                    item.Type == DictNames.CONJUGATOR || item.Type == DictNames.WEB)
                {
                    automationDone = false;
                    Navigate(GetDictURLForword());
                }
                else
                    DocumentText =
                        item.Type == DictNames.LOCAL || item.Type == DictNames.OFFLINE ? GetTranslationHtml() :
                        item.Type == DictNames.LIVE ? GetLiveHtml(item.Name, "") :
                        item.Type == DictNames.ONLINE && item.Name == DictNames.FRHELPER ? GetFrhelperHtml() :
                        item.Name == DictNames.LINGOES ? GetLingoesHtml() :
                        item.Name == DictNames.LINGOESALL ? GetLingoesAllHtml() :
                        "";
            }
            else
            {
                var sb = new StringBuilder("<html><head><META content=\"IE=10.0000\" http-equiv=\"X-UA-Compatible\"></head><body>\n");
                sb.AppendFormat("<script type=\"text/javascript\">\n{0}\n</script>\n", Program.js);

                for (int i = 0; i < dictItems.Count; i++)
                {
                    var item = dictItems[i];
                    var ifrId = $"ifr{i}";

                    Func<string, string> GetIFrameOfflineText = str =>
                    {
                        var str2 = str.Replace("'", "&#39;").Replace("\"", "\\\"").Replace("\r\n", "\\r\\n").Replace("\n", "\\n");
                        return $"<iframe id='{ifrId}' frameborder='0' style='width:100%; display:block' onload='setFrameContent(this, \"{str2}\");'></iframe>\n";
                    };

                    Func<string, string> GetIFrameOnlineText = url => 
                        $"<iframe id='{ifrId}' frameborder='1' style='width:100%; height:500px; display:block' src='{url}'></iframe>\n";

                    dictRow = FindDict(item.Name);
                    if (item.Name == DictNames.LINGOES)
                        sb.Append(GetIFrameOfflineText(GetLingoesHtml()));
                    else if (item.Name == DictNames.LINGOESALL)
                        sb.Append(GetIFrameOfflineText(GetLingoesAllHtml()));
                    else if (item.Type == DictNames.LOCAL || item.Type == DictNames.OFFLINE)
                        sb.Append(GetIFrameOfflineText(GetTranslationHtml()));
                    else if (item.Type == DictNames.LIVE)
                        sb.Append(GetIFrameOfflineText(GetLiveHtml(item.Name, ifrId)));
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

        public bool CanExtractAndOverwriteTranslation()
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
                LollyDB.DictEntity_Update(ExtensionClass.NOTRANSLATION, word, dictRow.DICTTABLE);
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

            LollyDB.DictEntity_Update(dlg.translationTextBox.Text, word, dictRow.DICTTABLE);
            return true;
        }

        public bool DoExtractTranslation(string word, bool overwriteDB)
        {
            if (dictItems.Count == 1 && dictItems[0].Type == DictNames.ONLINE)
            {
                var dictName = dictItems[0].Name;
                var dictRow = FindDict(dictName);
                var msg = $"The translation from the url \"{Url.AbsoluteUri}\" will be EXTRACTED and " +
                    (overwriteDB ? "used to REPLACE" : "APPENDED to") +
                    $" the translation of the word \"{word}\" in the dictionary \"{dictName}\". Are you sure?";
                if (MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return false;
                var wordRow = Program.OpenDictTable(word, dictRow.DICTTABLE);
                Program.UpdateDictTable(this, wordRow, dictRow, !overwriteDB);
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
