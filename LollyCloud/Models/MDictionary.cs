﻿using ReactiveUI;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;

namespace LollyShared
{
    public class MDictionary : ReactiveObject
    {
        int _ID;
        [JsonProperty]
        public int ID
        {
            get => _ID;
            set => this.RaiseAndSetIfChanged(ref _ID, value);
        }
        int _DICTID;
        [JsonProperty]
        public int DICTID
        {
            get => _DICTID;
            set => this.RaiseAndSetIfChanged(ref _DICTID, value);
        }
        int _LANGIDFROM;
        [JsonProperty]
        public int LANGIDFROM
        {
            get => _LANGIDFROM;
            set => this.RaiseAndSetIfChanged(ref _LANGIDFROM, value);
        }
        string _DICTTYPENAME;
        [JsonProperty]
        public string DICTTYPENAME
        {
            get => _DICTTYPENAME;
            set => this.RaiseAndSetIfChanged(ref _DICTTYPENAME, value);
        }
        string _DICTNAME;
        [JsonProperty]
        public string DICTNAME
        {
            get => _DICTNAME;
            set => this.RaiseAndSetIfChanged(ref _DICTNAME, value);
        }
        string _URL;
        [JsonProperty]
        public string URL
        {
            get => _URL;
            set => this.RaiseAndSetIfChanged(ref _URL, value);
        }
        string _CHCONV;
        [JsonProperty]
        public string CHCONV
        {
            get => _CHCONV;
            set => this.RaiseAndSetIfChanged(ref _CHCONV, value);
        }
        string _TRANSFORM;
        [JsonProperty("TRANSFORM_WIN")]
        public string TRANSFORM
        {
            get => _TRANSFORM;
            set => this.RaiseAndSetIfChanged(ref _TRANSFORM, value);
        }
        int? _WAIT;
        [JsonProperty]
        public int? WAIT
        {
            get => _WAIT;
            set => this.RaiseAndSetIfChanged(ref _WAIT, value);
        }
        string _TEMPLATE;
        [JsonProperty]
        public string TEMPLATE
        {
            get => _TEMPLATE;
            set => this.RaiseAndSetIfChanged(ref _TEMPLATE, value);
        }
        string _TEMPLATE2;
        [JsonProperty]
        public string TEMPLATE2
        {
            get => _TEMPLATE2;
            set => this.RaiseAndSetIfChanged(ref _TEMPLATE2, value);
        }

        public string UrlString(string word, List<MAutoCorrect> lstAutoCorrects)
        {
            var word2 = CHCONV == "BASIC" ? MAutoCorrect.AutoCorrect(word, lstAutoCorrects, o => o.EXTENDED, o => o.BASIC) : word;
            var url = URL.Replace("{0}", HttpUtility.UrlEncode(word2));
            return url;
        }
    }

    public class MDictsMean
    {
        public List<MDictMean> VDICTSMEAN { get; set; }
    }
    public class MDictMean : MDictionary
    {
        public string HtmlString(string html, string word, bool useTemplate2 = false)
        {
            var template = useTemplate2 && !string.IsNullOrEmpty(TEMPLATE2) ? TEMPLATE2 : TEMPLATE;
            return CommonApi.ExtractTextFromHtml(html, TRANSFORM, template, (text, template2) =>
                string.Format(template2, word, CommonApi.CssFolder, text));
        }
    }

    public class MDictItem : ReactiveObject
    {
        string _DICTID;
        public string DICTID
        {
            get => _DICTID;
            set => this.RaiseAndSetIfChanged(ref _DICTID, value);
        }
        string _DICTNAME;
        public string DICTNAME
        {
            get => _DICTNAME;
            set => this.RaiseAndSetIfChanged(ref _DICTNAME, value);
        }
        public MDictItem(string id, string name)
        {
            DICTID = id;
            DICTNAME = name;
        }
        public string[] DictIDs => DICTID.Split(',');
    }

    public class MDictsNote
    {
        public List<MDictNote> VDICTSNOTE { get; set; }
    }
    public class MDictNote : MDictionary{ }
}
