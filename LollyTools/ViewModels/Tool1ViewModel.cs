using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LollyShared;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;

namespace LollyTools.ViewModels
{
    class Tool1ViewModel : BindableBase
    {
        public ICommand AllTranslationsFrhelperCommand { get; private set; }
        public ICommand OneTranslationFrhelperCommand { get; private set; }
        public ICommand SyncDictWordsWithLangCommand { get; private set; }

        private string _Word;
        public string Word
        {
            get { return _Word; }
            set { SetProperty(ref _Word, value); }
        }
        public Tool1ViewModel()
        {
            AllTranslationsFrhelperCommand = new DelegateCommand(OnAllTranslationsFrhelper);
            OneTranslationFrhelperCommand = new DelegateCommand(OnOneTranslationFrhelper);
            SyncDictWordsWithLangCommand = new DelegateCommand(OnSyncDictWordsWithLang);
            Word = "passer";
        }

        private void OnAllTranslationsFrhelper()
        {
            var obj = new Frhelper();
            var dictRow = LollyDB.DictAll_GetDataByLangDict(3, "Frhelper");
            using (var db = new LollyEntities())
            {
                var sql = @"
                    SELECT   *
                    FROM      [@FR-CH FRHELPER]
                ";
                var lst = db.Database.SqlQuery<MDICTENTITY>(sql).ToList();
                foreach (var r in lst)
                {
                    var reg = new Regex("<DIV id=dic_banner>(.+)<DIV class=dic_banner_RightText>Dictionnaire</DIV></DIV>");
                    var wordInDict = "";
                    if (r.TRANSLATION != null)
                    {
                        var match = reg.Match(r.TRANSLATION);
                        if (match.Groups.Count == 2)
                            wordInDict = match.Groups[1].Value.Trim();
                    }
                    if (string.Equals(wordInDict, r.WORD, StringComparison.InvariantCultureIgnoreCase)) continue;
                    //var html = obj.Search(r.WORD, dictRow.TRANSFORM_WIN);
                    //LollyDB.DictEntity_Update(html, r.WORD, "@FR-CH FRHELPER");
                    Debug.Print("{0} <> {1}", r.WORD, wordInDict);
                }
            }
        }

        private void OnOneTranslationFrhelper()
        {
            //var obj = new Frhelper();
            //var dictRow = LollyDB.DictAll_GetDataByLangDict(3, "Frhelper");
            //var html = obj.Search(Word, dictRow.TRANSFORM_WIN);

            var obj = new Lingoes();
            obj.FindLingoes();
            var lst = obj.GetWordList();
        }

        private void OnSyncDictWordsWithLang()
        {
            int langid = 3;
            var sqlDel = @"
                DELETE FROM [{0}]
                WHERE WORD IN (
                SELECT WORD FROM [{0}]
                EXCEPT
                SELECT WORD FROM WORDSLANG
                WHERE LANGID = {1})
            ";
            var sqlNewWords = @"
                SELECT WORD FROM WORDSLANG
                WHERE LANGID = {1}
                EXCEPT
                SELECT WORD FROM [{0}]
            ";
            using (var db = new LollyEntities())
            {
                var dictRows = (
                    from r in db.SDICTALL
                    where r.LANGID == langid
                    where r.DICTTYPENAME == "OFFLINE-ONLINE"
                    orderby r.SEQNUM
                    select r
                ).ToList();
                foreach (var r in dictRows)
                {
                    db.Database.ExecuteSqlCommand(string.Format(sqlDel, r.DICTTABLE, langid));
                    var newWords = db.Database.SqlQuery<string>(string.Format(sqlNewWords, r.DICTTABLE, langid)).ToList();
                    foreach (var w in newWords)
                        LollyDB.DictEntity_Insert(w, r.DICTTABLE);
                }
            }
        }
    }
}
