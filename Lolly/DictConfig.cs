using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LollyBase;

namespace Lolly
{
    public class DictConfig
    {
        private Dictionary<long, DictLangConfig> lang2Config = new Dictionary<long, DictLangConfig>();

        public DictConfig(string uri)
        {
            var config = XDocument.Load(uri).Element("configuration");
            var elemGroupDictInfo = config.Element("dictInfo").Elements("group").ToList();
            foreach (var elemLang in config.Elements("language"))
            {
                long langID = (long) elemLang.Attribute("id");
                lang2Config[langID] = new DictLangConfig(elemLang, elemGroupDictInfo, langID);
            }
        }

        public DictLangConfig GetDictLangConfig(long langID)
        {
            return lang2Config[langID];
        }
    }

    public class DictLangConfig
    {
        public List<KeyValuePair<string, string>> replacement;
        public List<string> dictsEBWin;
        public string[] dictsLingoes;
        public Dictionary<string, UIDict> dictsCustom;
        public string[] dictsOffline;
        public string[] dictTablesOffline;
        public List<MDICTALL> dictAllList;
        public SortedDictionary<string, List<UIDictItem>> dictGroups = new SortedDictionary<string, List<UIDictItem>>();
        private Dictionary<string, UIDictItem> dictItems = new Dictionary<string, UIDictItem>();

        public DictLangConfig(XElement elemLang, List<XElement> elemGroupDictInfo, long langID)
        {
            // replacement
            replacement = elemLang.Elements("phraseReplace")
                .Select(elem => new KeyValuePair<string, string>(
                    (string)elem.Attribute("before"),
                    (string)elem.Attribute("after")
                )).ToList();

            // ebwin
            dictsEBWin = elemLang.Elements("ebwin").Select(elem => (string)elem).ToList();

            var elemDicts = elemLang.Element("dictionaries");
            if (elemDicts == null) return;

            Action<DictImage, string, string[]> AddDictGroups = (dt, groupName, dictNames) =>
            {
                if (dictNames.IsEmpty()) return;

                var items = new List<UIDictItem>();
                dictGroups[groupName] = items;

                int imageIndex = (int)dt;
                foreach (var dictName in dictNames)
                {
                    var item = new UIDictItem
                    {
                        Name = dictName,
                        Type = groupName,
                        ImageIndex = (DictImage)imageIndex
                    };
                    items.Add(item);
                    dictItems[groupName + dictName] = item;
                    if (dt == DictImage.Offline || dt == DictImage.Online || dt == DictImage.Live)
                        imageIndex++;
                }
            };

            // group
            var elemDictsGroup = elemDicts.Elements("group");
            var dictNormalGroups =
                from info in elemGroupDictInfo
                join dict in elemDictsGroup on (string)info equals (string)dict
                select new UIDictItem
                {
                    Name = (string)dict,
                    Type = (string)info.Attribute("dictType"),
                    ImageIndex = (DictImage)Enum.Parse(typeof(DictImage), (string)dict)
                };
            foreach (var group in dictNormalGroups)
            {
                dictAllList =
                    group.Name == DictNames.WEB ? LollyDB.DictAll_GetDataByLangWeb(langID) :
                    LollyDB.DictAll_GetDataByLangDictType(langID, group.Type);
                var dictNames = (from row in dictAllList select row.DICTNAME).ToArray();
                if (group.Name == DictNames.OFFLINE)
                {
                    dictsOffline = (from row in dictAllList select row.DICTNAME).ToArray();
                    dictTablesOffline = (from row in dictAllList select row.DICTTABLE).ToArray();
                }
                AddDictGroups(group.ImageIndex, group.Name, dictNames);
            }

            // lingoes + special
            dictsLingoes = elemDicts.Elements("lingoes").Select(elem => (string)elem).ToArray();
            var dictsSpecial = elemDicts.Elements("special").Select(elem => (string)elem).ToList();
            if (dictsLingoes.Any())
                dictsSpecial.Add("Lingoes");
            AddDictGroups(DictImage.Special, "Special", dictsSpecial.OrderBy(s => s).ToArray());

            // custom
            var elems = elemDicts.Elements("custom");
            var dictNamesCustom = elems.Select(elem => (string)elem.Attribute("name")).ToArray();
            AddDictGroups(DictImage.Custom, "Custom", dictNamesCustom);
            dictsCustom = elems.Select(elem => new
            {
                Name = (string)elem.Attribute("name"),
                Type = (string)elem.Attribute("type"),
                Items = elem.Elements("dict").Select(elem2 => new UIDictItem
                {
                    Name = (string)elem2,
                    Type = (string)elem2.Attribute("type"),
                    ImageIndex = DictImage.Custom
                }).SelectMany(i =>
                    i.Name == DictNames.OFFLINEALL ? dictGroups[DictNames.OFFLINE] :
                    i.Name == DictNames.ONLINEALL ? dictGroups[DictNames.ONLINE] :
                    i.Name == DictNames.LIVEALL ? dictGroups[DictNames.LIVE] :
                    new List<UIDictItem>{ dictItems[i.Type + i.Name] }
                ).ToList()
            }).Select(elem => new
            {
                Name = elem.Name,
                Dict = elem.Type == "Pile" ? (UIDict)new UIDictPile
                {
                    Name = elem.Name,
                    Items = elem.Items
                } : (UIDict)new UIDictSwitch
                {
                    Name = elem.Name,
                    Items = elem.Items
                }
            }).ToDictionary(elem => elem.Name, elem => elem.Dict);

            dictAllList = LollyDB.DictAll_GetDataByLang(langID);
        }
    }
}
