using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Speech.Synthesis;
using Microsoft.Win32;
using System.Reflection;
using LollyBase;

namespace Lolly
{
    public struct LangBookUnitSettings
    {
        public int LangID { get; set; }
        public int BookID { get; set; }
        public int UnitFrom { get; set; }
        public int PartFrom { get; set; }
        public int UnitTo { get; set; }
        public int PartTo { get; set; }
        public string LangName { get; set; }
        public string BookName { get; set; }

        public int UnitPartFrom
        {
            get
            {
                return UnitFrom * 10 + PartFrom;
            }
        }

        public int UnitPartTo
        {
            get
            {
                return UnitTo * 10 + PartTo;
            }
        }

        public string BookUnitsDesc
        {
            get
            {
                return UnitPartFrom == UnitPartTo ? 
                    string.Format("{0} {1}:{2}", BookName, UnitFrom, PartFrom) :
                    string.Format("{0} {1}:{2} -- {3}:{4}", BookName, UnitFrom, PartFrom, UnitTo, PartTo);
            }
        }

        public string LangDesc
        {
            get
            {
                return string.Format("Language: {0}", LangName);
            }
        }
    }

    interface ILangBookUnits
    {
        void UpdatelbuSettings();
    }

    public class ReindexObject
    {
        public int ID {get; set;}
        public int ORD { get; set; }
        public string ITEM { get; set; }
        public ReindexObject(int id, string item)
        {
            ID = id;
            ORD = 0;
            ITEM = item;
        }
    }

    public enum DictImage
    {
        OfflineAll,
        Offline,
        OnlineAll = Offline + 9,
        Online,
        LiveAll = Online + 9,
        Live,
        Custom = Live + 9,
        Local,
        Special,
        Conjugator,
        Web,
    };

    public static class DictNames
    {
        public const string CONJUGATOR = "Conjugator";
        public const string DEFAULT = "DEFAULT";
        public const string LINGOES = "Lingoes";
        public const string LINGOESALL = "LINGOES";
        public const string LIVE = "Live";
        public const string LIVEALL = "LIVE";
        public const string LOCAL = "Local";
        public const string OFFLINE = "Offline";
        public const string OFFLINEALL = "OFFLINE";
        public const string ONLINE = "Online";
        public const string ONLINEALL = "ONLINE";
        public const string WEB = "Web";
    };

    public class DictInfo
    {
        public string type;
        public string name;
        public DictInfo(XElement elem)
        {
            type = (string)elem.Attribute("type");
            name = (string)elem;
        }
        public DictInfo(string type, string name)
        {
            this.type = type;
            this.name = name;
        }
    }

    enum DictWebBrowserStatus
    {
        Navigating,
        Automating,
        Ready
    };

    static class Program
    {
        public static LangBookUnitSettings lbuSettings;
        public static string appDataFolder;
        public static string appDataFolderInHtml;
        public static string js;
        public static string appLogFolder;
        public static XElement config;
        public static string lingoesClassName;
        public static string lingoesWindowName;
        public static Lingoes lingoes = new Lingoes();
        public static Frhelper frhelper = new Frhelper();
        public static Options options = new Options();
        private static SpeechSynthesizer synth = new SpeechSynthesizer();
        private static string[] voiceNames;

        private static string AutoCorrect(string text, IEnumerable<MAUTOCORRECT> autoCorrectList,
            Func<MAUTOCORRECT, string> colFunc1, Func<MAUTOCORRECT, string> colFunc2)
        {
            var str = text;
            foreach (var row in autoCorrectList)
                str = str.Replace(colFunc1(row), colFunc2(row));
            return str;
        }

        public static string AutoCorrect(string text, IEnumerable<MAUTOCORRECT> autoCorrectList)
        {
            return AutoCorrect(text, autoCorrectList, row => row.INPUT, row => row.EXTENDED);
        }

        public static string GetDictURLForWord(string word, MDICTALL dictRow, IEnumerable<MAUTOCORRECT> autoCorrectList)
        {
            var chconv = dictRow.CHCONV;
            var word2 =
                chconv == "UTF-8" ? HttpUtility.UrlEncode(word) :
                chconv == "BASIC" ? AutoCorrect(word, autoCorrectList, row => row.EXTENDED, row => row.BASIC) :
                chconv == "INPUT" ? AutoCorrect(word, autoCorrectList, row => row.EXTENDED, row => row.INPUT) :
                chconv == "Korean" ? HttpUtility.UrlEncode(word, Encoding.GetEncoding(949)) :
                chconv == "Western European" ? HttpUtility.UrlEncode(word, Encoding.GetEncoding(28591)) :
                chconv == "Russian" ? HttpUtility.UrlEncode(word, Encoding.GetEncoding(1251)) :
                word;
            var url = string.Format(dictRow.URL, word2);
            return url;
        }

        public static string GetLiveHtml(string word, string dict)
        {
            return string.Format("<p style=\"color: #0000FF; font-weight: bold\">Extracting translation of the word \"{0}\" from the web dictionary \"{1}\"</p>",
                word, dict);
        }

        public static MDICTENTITY OpenDictTable(string word, string dictTable)
        {
            var wordRow = DictEntity.GetDataByWordDictTable(word, dictTable);
            if (wordRow == null)
            {
                DictEntity.Insert(word, dictTable);
                wordRow = DictEntity.GetDataByWordDictTable(word, dictTable);
            }
            return wordRow;
        }

        public static void UpdateDictTable(WebBrowser wb, MDICTENTITY wordRow, MDICTALL dictRow, bool append)
        {
            var text = wb.ExtractFromWeb(dictRow, append ? "" : ExtensionClass.NOTRANSLATION);
            if (append)
                text = wordRow.TRANSLATION + text;
            wordRow.TRANSLATION = text;
            DictEntity.Update(text, wordRow.WORD, dictRow.DICTTABLE);
        }

        public static void SetLangID(int newLangID)
        {
            lbuSettings.LangID = newLangID;
            Properties.Settings.Default.LangID = newLangID;
            Properties.Settings.Default.Save();
        }

        public static void InitVoices()
        {
            var voices = new SpeechSynthesizer().GetInstalledVoices();
            voiceNames = (from row in Languages.GetData() select row.VOICE).ToArray();
        }

        public static void Speak(int nLangID, string text)
        {
            synth.SpeakAsyncCancelAll();
            synth.SelectVoice(voiceNames[nLangID]);
            synth.SpeakAsync(text);
        }

        public static void Speak(PromptBuilder pb)
        {
            synth.SpeakAsyncCancelAll();
            synth.SpeakAsync(pb);
        }

        public static bool CanSpeak(int nLangID)
        {
            return voiceNames[nLangID] != "";
        }

        public static void AddPropmt(PromptBuilder pb, int nLangID, string text)
        {
            pb.StartVoice(voiceNames[nLangID]);
            pb.AppendText(text);
            pb.EndVoice();
        }

        public static void textBoxSelectAll_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A) 
            { 
                (sender as TextBox).SelectAll();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        public static XElement GetConfig(int langID)
        {
            return Program.config.Elements("language")
                .Where(lang => (int)lang.Attribute("id") == langID)
                .FirstOrDefault();
        }

        public static XElement GetConfigDicts(int langID)
        {
            return GetConfig(langID).Element("dictionaries");
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            lbuSettings.LangID = Properties.Settings.Default.LangID;
            appDataFolder = Properties.Settings.Default.AppDataFolder + "\\";
            appLogFolder = appDataFolder + "Log\\";
            js = System.IO.File.ReadAllText(appDataFolder + "Lolly.js");
            appDataFolderInHtml = appDataFolder.Replace('\\', '/');
            config = XDocument.Load(appDataFolder + "Lolly.config").Element("configuration");

            lingoesClassName = Properties.Settings.Default.LingoesClassName;
            lingoesWindowName = Properties.Settings.Default.LingoesWindowName;
            lingoes.FindLingoes();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MDIForm());
        }
    }
}
