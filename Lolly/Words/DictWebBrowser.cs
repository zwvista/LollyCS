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
        public string dictName;
        public DictImage dictImage;
        public MDICTALL dictRow;
        public MDICTENTITY wordRow;
        public bool emptyTrans;
        public bool automationDone;

        public void UpdateLiveHtml(string word, string dict, string translation)
        {
            DocumentText = DocumentText.Replace(Program.GetLiveHtml(word, dict),
                translation);
        }

        public void FindDict(List<MDICTALL> dictAllList, string dict)
        {
            dictRow = dictAllList.SingleOrDefault(r => r.DICTNAME == dict);
        }

        public void UpdateHtml(string word, List<MAUTOCORRECT> autoCorrectList, DictLangConfig config)
        {
            Func<string, string> GetTemplatedHtml = str =>
                string.Format(dictRow.TEMPLATE, word, Program.appDataFolderInHtml + "css/", str);

            Func<string> GetTranslationHtml = () =>
            {
                wordRow = DictEntity.GetDataByWordDictTable(word, dictRow.DICTTABLE);
                string translation = wordRow == null ? "" : wordRow.TRANSLATION;
                emptyTrans = emptyTrans && translation == "";
                return GetTemplatedHtml(translation);
            };

            Func<string> GetDictURLForword = () =>
                Program.GetDictURLForWord(word, dictRow, autoCorrectList);

            Func<bool, string> GetLingoesHtml = all =>
                all ? Program.lingoes.Search(word) :
                    Program.lingoes.Search(word, config.dictsLingoes);

            Func<string, string> GetLiveHtml = dict =>
            {
                var f = Parent;
                while (f as MDIForm == null)
                    f = f.Parent;
                ((MDIForm)f).ExtractTranslation(new[] { word }, new[] { dict }, true, this);
                return GetTemplatedHtml(Program.GetLiveHtml(word, dict));
            };

            Func<List<DictInfo>, string> GetCustomHtml = dictInfos =>
            {
                Func<string, string> GetIFrameOfflineText = str => string.Format(
                    "<iframe frameborder='0' style='width:100%; display:block' onload='setFrameContent(this, \"{0}\");'></iframe>\n",
                    str.Replace("'", "&#39;").Replace("\"", "\\\"").Replace("\r\n", "\\r\\n").Replace("\n", "\\n"));

                Func<string, string> GetIFrameOnlineText = str => string.Format(
                    "<iframe frameborder='1' style='width:100%; height:500px; display:block' src='{0}'></iframe>\n",
                    str);

                var sb = new StringBuilder("<html><head><META content=\"IE=10.0000\" http-equiv=\"X-UA-Compatible\"></head><body>\n");

                Action<string> AddOfflineDictText = dict =>
                {
                    FindDict(config.dictAllList, dict);
                    sb.Append(GetIFrameOfflineText(GetTranslationHtml()));
                };

                Action<string> AddOnlineDictText = dict =>
                {
                    FindDict(config.dictAllList, dict);
                    if (dict == "Frhelper")
                        sb.Append(GetIFrameOfflineText(GetLiveHtml(dict)));
                    else
                        sb.Append(GetIFrameOnlineText(GetDictURLForword()));
                };

                foreach (var info in dictInfos)
                {
                    var dictA = info.Name;
                    switch (dictA)
                    {
                        case DictNames.LINGOES:
                        case DictNames.LINGOESALL:
                            sb.Append(GetIFrameOfflineText(GetLingoesHtml(dictA == DictNames.LINGOESALL)));
                            break;
                        case DictNames.OFFLINEALL:
                            foreach (var dictB in config.dictsOffline)
                                AddOfflineDictText(dictB);
                            break;
                        case DictNames.ONLINEALL:
                            foreach (var dictB in config.dictsOffline)
                                AddOnlineDictText(dictB);
                            break;
                        case DictNames.LIVEALL:
                            foreach (var dictB in config.dictsOffline)
                                AddOfflineDictText(GetLiveHtml(dictB));
                            break;
                        default:
                            switch (info.Type)
                            {
                                case DictNames.LOCAL:
                                case DictNames.OFFLINE:
                                    AddOfflineDictText(dictA);
                                    break;
                                case DictNames.CONJUGATOR:
                                case DictNames.ONLINE:
                                case DictNames.WEB:
                                    AddOnlineDictText(dictA);
                                    break;
                                case DictNames.LIVE:
                                    AddOfflineDictText(GetLiveHtml(dictA));
                                    break;
                            }
                            break;
                    }
                }

                sb.AppendFormat("<script type=\"text/javascript\">\n{0}\n</script>", Program.js);
                sb.Append("</body></html>\n");
                return sb.ToString();
            };

            emptyTrans = true;
            if (word == "")
                DocumentText = "";
            else if (dictImage == DictImage.Online && dictName != "Frhelper" || dictImage == DictImage.Conjugator || dictImage == DictImage.Web)
            {
                automationDone = false;
                Navigate(GetDictURLForword());
            }
            else
                DocumentText =
                    dictImage == DictImage.Local || dictImage == DictImage.Offline ? GetTranslationHtml() :
                    dictImage == DictImage.Live || dictImage == DictImage.Online && dictName == "Frhelper" ? GetLiveHtml(dictName) :
                    dictImage == DictImage.Custom ? GetCustomHtml(config.dictsCustom[dictName]) :
                    dictName == DictNames.OFFLINEALL || dictName == DictNames.ONLINEALL ||dictName == DictNames.LIVEALL ?
                        GetCustomHtml(new List<DictInfo>{ new DictInfo
                        {
                            Name = dictName,
                            Type = "Group",
                        }}) :
                    dictName == DictNames.LINGOES || dictName == DictNames.LINGOESALL ? GetLingoesHtml(dictName == DictNames.LINGOESALL) :
                    "";
        }
    };
}
