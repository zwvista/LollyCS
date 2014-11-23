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
        private Dictionary<int, DictLangConfig> lang2Config = new Dictionary<int, DictLangConfig>();

        public DictConfig(string uri)
        {
            var config = XDocument.Load(uri).Element("configuration");
            var elemGroupDictInfo = config.Element("dictInfo").Elements("group").ToList();
            foreach (var elemLang in config.Elements("language"))
            {
                int langID = (int) elemLang.Attribute("id");
                lang2Config[langID] = new DictLangConfig(elemLang, elemGroupDictInfo, langID);
            }
        }

        public DictLangConfig GetDictLangConfig(int langID)
        {
            return lang2Config[langID];
        }
    }

    public class DictInfo
    {
        public string Name;
        public string Type;
        public DictImage ImageIndex;
    }

    public class DictLangConfig
    {
        public List<KeyValuePair<string, string>> replacement;
        public List<string> dictsEBWin;
        public string[] dictsLingoes;
        public Dictionary<string, List<DictInfo>> dictsCustom;
        public string[] dictsOffline;
        public string[] dictTablesOffline;
        public List<MDICTALL> dictAllList;
        public SortedDictionary<string, List<DictInfo>> dictGroups = new SortedDictionary<string, List<DictInfo>>();

        public DictLangConfig(XElement elemLang, List<XElement> elemGroupDictInfo, int langID)
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
                if (!dictNames.Any()) return;

                var infos = new List<DictInfo>();
                dictGroups[groupName] = infos;

                int imageIndex = (int)dt;
                foreach (var dictName in dictNames)
                {
                    infos.Add(new DictInfo
                    {
                        Name = dictName,
                        Type = groupName,
                        ImageIndex = (DictImage)imageIndex
                    });
                    if (dt == DictImage.Offline || dt == DictImage.Online || dt == DictImage.Live)
                        imageIndex++;
                }
            };

            // custom
            var elems = elemDicts.Elements("custom");
            var dictNamesCustom = elems.Select(elem => (string)elem.Attribute("name")).ToArray();
            AddDictGroups(DictImage.Custom, "Custom", dictNamesCustom);
            dictsCustom = elems.Select(elem => new
            {
                Key = (string)elem.Attribute("name"),
                Value = elem.Elements("dict").Select(elem2 => new DictInfo
                {
                    Name = (string)elem2,
                    Type = (string)elem2.Attribute("type"),
                    ImageIndex = DictImage.Custom
                }).ToList()
            }).ToDictionary(elem => elem.Key, elem => elem.Value);

            // group
            var elemDictsGroup = elemDicts.Elements("group");
            var dictNormalGroups =
                from info in elemGroupDictInfo
                join dict in elemDictsGroup on (string)info equals (string)dict
                select new DictInfo
                {
                    Name = (string)dict,
                    Type = (string)info.Attribute("dictType"),
                    ImageIndex = (DictImage)Enum.Parse(typeof(DictImage), (string)dict)
                };
            foreach (var group in dictNormalGroups)
            {
                dictAllList =
                    group.Name == DictNames.WEB ? DictAll.GetDataByLangWeb(langID) :
                    DictAll.GetDataByLangDictType(langID, group.Type);
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

            dictAllList = DictAll.GetDataByLang(langID);
        }
    }
}
