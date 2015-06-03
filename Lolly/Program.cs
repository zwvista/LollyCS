﻿using System;
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

        public int UnitPartFrom => UnitFrom * 10 + PartFrom;
        public int UnitPartTo => UnitTo * 10 + PartTo;
        public string BookUnitsDesc => UnitPartFrom == UnitPartTo ?
            $"{BookName} {UnitFrom}:{PartFrom}" :
            $"{BookName} {UnitFrom}:{PartFrom} -- {UnitTo}:{PartTo}";
        public string LangDesc => $"Language: {LangName}";
    }

    interface ILangBookUnits
    {
        void UpdatelbuSettings();
    }

    public class ReorderObject
    {
        public int ID { get; set; }
        public int ORD { get; set; } = 0;
        public string ITEM { get; set; }
        public ReorderObject(int id, string item)
        {
            ID = id;
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
        public const string FRHELPER = "Frhelper";
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

    enum DictWebBrowserStatus
    {
        Navigating,
        Automating,
        Ready
    };

    static class Program
    {
        public static LollyDB db = new LollyDB();
        public static LangBookUnitSettings lbuSettings;
        public static string appDataFolder;
        public static string appDataFolderInHtml;
        public static string js;
        public static string appLogFolder;
        public static DictConfig config;
        public static string lingoesClassName;
        public static string lingoesWindowName;
        public static Lingoes lingoes = new Lingoes();
        public static Frhelper frhelper = new Frhelper();
        public static Options options = new Options();
        private static SpeechSynthesizer synth = new SpeechSynthesizer();
        private static string[] voiceNames;

        private static string AutoCorrect(string text, List<MAUTOCORRECT> autoCorrectList,
            Func<MAUTOCORRECT, string> colFunc1, Func<MAUTOCORRECT, string> colFunc2)
        {
            var str = text;
            foreach (var row in autoCorrectList)
                str = str.Replace(colFunc1(row), colFunc2(row));
            return str;
        }

        public static string AutoCorrect(string text, List<MAUTOCORRECT> autoCorrectList)
        {
            return AutoCorrect(text, autoCorrectList, row => row.INPUT, row => row.EXTENDED);
        }

        public static string GetDictURLForWord(string word, MDICTALL dictRow, List<MAUTOCORRECT> autoCorrectList)
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
            return $"<p style=\"color: #0000FF; font-weight: bold\">Extracting translation of the word \"{word}\" from the web dictionary \"{dict}\"</p>";
        }

        public static MDICTENTITY OpenDictTable(string word, string dictTable)
        {
            var wordRow = db.DictEntity_GetDataByWordDictTable(word, dictTable);
            if (wordRow == null)
            {
                db.DictEntity_Insert(word, dictTable);
                wordRow = db.DictEntity_GetDataByWordDictTable(word, dictTable);
            }
            return wordRow;
        }

        public static void UpdateDictTable(WebBrowser wb, MDICTENTITY wordRow, MDICTALL dictRow, bool append)
        {
            var text = wb.ExtractFromWeb(dictRow, append ? "" : ExtensionClass.NOTRANSLATION);
            if (append)
                text = wordRow.TRANSLATION + text;
            wordRow.TRANSLATION = text;
            db.DictEntity_Update(text, wordRow.WORD, dictRow.DICTTABLE);
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
            voiceNames = (from row in db.Languages_GetData() select row.VOICE).ToArray();
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

        public static void AddPrompt(PromptBuilder pb, int nLangID, string text)
        {
            pb.StartVoice(voiceNames[nLangID]);
            pb.AppendText(text);
            pb.EndVoice();
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
            config = new DictConfig(appDataFolder + "Lolly.config");

            lingoesClassName = Properties.Settings.Default.LingoesClassName;
            lingoesWindowName = Properties.Settings.Default.LingoesWindowName;
            lingoes.FindLingoes();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MDIForm());
        }
    }
}
