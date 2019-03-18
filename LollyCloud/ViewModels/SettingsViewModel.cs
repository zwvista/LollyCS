﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using System.Globalization;
using System.Net.Http;

namespace LollyShared
{
    public class SettingsViewModel : LollyViewModel
    {
        UserSettingDataStore UserSettingDS = new UserSettingDataStore();
        LanguageDataStore LanguageDS = new LanguageDataStore();
        DictMeanDataStore DictMeanDS = new DictMeanDataStore();
        DictNoteDataStore DictNoteDS = new DictNoteDataStore();
        TextbookDataStore TextbookDS = new TextbookDataStore();
        AutoCorrectDataStore AutoCorrectDS = new AutoCorrectDataStore();

        public ObservableCollection<MUserSetting> UserSettings { get; set; }
        MUserSetting SelectedUSUser0;
        MUserSetting SelectedUSUser1;
        public int USLANGID {
            get => int.Parse(SelectedUSUser0.VALUE1);
            set => SelectedUSUser0.VALUE1 = value.ToString();
        }
        public List<int> USROWSPERPAGEOPTIONS =>
            SelectedUSUser0.VALUE2.Split(',').Select(s => int.Parse(s)).ToList();
        public int USROWSPERPAGE => int.Parse(SelectedUSUser0.VALUE3);
        public Dictionary<int, List<int>> USLEVELCOLORS;
        public int USREADINTERVAL => int.Parse(SelectedUSUser1.VALUE1);
        MUserSetting SelectedUSLang2;
        public int USTEXTBOOKID
        {
            get => int.Parse(SelectedUSLang2.VALUE1);
            set => SelectedUSLang2.VALUE1 = value.ToString();
        }
        public string USDICTITEM
        {
            get => SelectedUSLang2.VALUE2;
            set => SelectedUSLang2.VALUE2 = value;
        }
        public int USDICTNOTEID
        {
            get => int.TryParse(SelectedUSLang2.VALUE3, out var v) ? v : 0;
            set => SelectedUSLang2.VALUE3 = value.ToString();
        }
        public string USDICTITEMS
        {
            get => SelectedUSLang2.VALUE4 ?? "0";
            set => SelectedUSLang2.VALUE4 = value;
        }
        MUserSetting SelectedUSLang3;
        public int USVOICEID
        {
            get => int.TryParse(SelectedUSLang3.VALUE1, out var v) ? v : 0;
            set => SelectedUSLang2.VALUE1 = value.ToString();
        }
        MUserSetting SelectedUSTextbook;
        public int USUNITFROM
        {
            get => int.Parse(SelectedUSTextbook.VALUE1);
            set => SelectedUSTextbook.VALUE1 = value.ToString();
        }
        public int USPARTFROM
        {
            get => int.Parse(SelectedUSTextbook.VALUE2);
            set => SelectedUSTextbook.VALUE2 = value.ToString();
        }
        public int USUNITTO
        {
            get => int.Parse(SelectedUSTextbook.VALUE3);
            set => SelectedUSTextbook.VALUE3 = value.ToString();
        }
        public int USPARTTO
        {
            get => int.Parse(SelectedUSTextbook.VALUE4);
            set => SelectedUSTextbook.VALUE4 = value.ToString();
        }
        public int USUNITPARTFROM => USUNITFROM * 10 + USPARTFROM;
        public int USUNITPARTTO => USUNITTO * 10 + USPARTTO;
        public bool IsSingleUnitPart => USUNITPARTFROM == USUNITPARTTO;
        public bool IsInvalidUnitPart => USUNITPARTFROM > USUNITPARTTO;

        public ObservableCollection<MLanguage> Languages { get; set; }
        public MLanguage SelectedLang;
        public int SelectedLangIndex => Languages.IndexOf(SelectedLang);

        public ObservableCollection<MDictMean> DictsMean;
        public ObservableCollection<MDictItem> DictItems;
        MDictItem selectedDictItem;
        public MDictItem SelectedDictItem {
            get => selectedDictItem;
            set {
                selectedDictItem = value;
                USDICTITEM = selectedDictItem.DICTID;
            }
        }
        public int SelectedDictItemIndex => DictItems.IndexOf(SelectedDictItem);

        public ObservableCollection<MDictNote> DictsNote { get; set; }
        MDictNote selectedDictNote = new MDictNote();
        public MDictNote SelectedDictNote {
            get => selectedDictNote;
            set {
                selectedDictNote = value;
                USDICTNOTEID = selectedDictNote?.ID ?? 0;
            }
        }
        public int SelectedDictNoteIndex => DictsNote.IndexOf(SelectedDictNote);

        public ObservableCollection<MTextbook> Textbooks { get; set; }
        MTextbook selectedTextbook;
        public MTextbook SelectedTextbook {
            get => selectedTextbook;
            set {
                selectedTextbook = value;
                USTEXTBOOKID = SelectedTextbook.ID;
                SelectedUSTextbook = UserSettings.FirstOrDefault(o => o.KIND == 11 && o.ENTITYID == USTEXTBOOKID);
            }
        }
        public int SelectedTextbookIndex => Textbooks.IndexOf(SelectedTextbook);

        public ObservableCollection<MSelectItem> lstUnits;
        public int UnitCount => lstUnits.Count;
        public ObservableCollection<MSelectItem> lstParts;
        public int PartCount => lstParts.Count;
        public bool IsSinglePart => PartCount == 1;

        public ObservableCollection<MAutoCorrect> AutoCorrects { get; set; }

        public HttpClient client = new HttpClient();

        public async Task GetData() {
            Languages = await GetData(async () => await LanguageDS.GetData());
            UserSettings = await GetData(async () => await UserSettingDS.GetDataByUser(CommonApi.UserId));
            SelectedUSUser0 = UserSettings.FirstOrDefault(o => o.KIND == 1 && o.ENTITYID == 0);
            SelectedUSUser1 = UserSettings.FirstOrDefault(o => o.KIND == 1 && o.ENTITYID == 1);
            var lst = SelectedUSUser0.VALUE4.Split(new [] { "\r\n" }, StringSplitOptions.None).Select(s => s.Split(',')).ToList();
            USLEVELCOLORS = new Dictionary<int, List<int>>();
            foreach (var v in lst)
                USLEVELCOLORS[int.Parse(v[0])] = new List<int> { int.Parse(v[1], NumberStyles.HexNumber), int.Parse(v[2], NumberStyles.HexNumber) };
            await SetSelectedLangIndex(Languages.FirstOrDefault(o => o.ID == USLANGID));
        }

        public async Task SetSelectedLangIndex(MLanguage lang) {
            SelectedLang = lang;
            USLANGID = SelectedLang.ID;
            SelectedUSLang2 = UserSettings.FirstOrDefault(o => o.KIND == 2 && o.ENTITYID == USLANGID);
            SelectedUSLang3 = UserSettings.FirstOrDefault(o => o.KIND == 3 && o.ENTITYID == USLANGID);
            var lstDicts = USDICTITEMS.Split(new[] { "\r\n" }, StringSplitOptions.None);
            DictsMean = await GetData(async () => await DictMeanDS.GetDataByLang(USLANGID));
            DictsNote = await GetData(async () => await DictNoteDS.GetDataByLang(USLANGID));
            Textbooks = await GetData(async () => await TextbookDS.GetDataByLang(USLANGID));
            AutoCorrects = await GetData(async () => await AutoCorrectDS.GetDataByLang(USLANGID));
            var i = 0;
            DictItems = new ObservableCollection<MDictItem>(lstDicts.SelectMany(d => d == "0" ?
                DictsMean.Select(o => new MDictItem(o.DICTID.ToString(), o.DICTNAME)) :
                new List<MDictItem> { new MDictItem(d, $"Custom{++i}") }
            ));
            SelectedDictItem = DictItems.FirstOrDefault(o => o.DICTID == USDICTITEM);
            SelectedDictNote = DictsNote.FirstOrDefault(o => o.ID == USDICTNOTEID) ?? DictsNote.FirstOrDefault();
            SelectedTextbook = Textbooks.FirstOrDefault(o => o.ID == USTEXTBOOKID);
        }

        public string DictHtml(string word, List<string> dictids)
        {
            var s = "<html><body>\n";
            foreach (var (dictid, i) in dictids.Select((dict, i) => (dict, i)))
            {
                var item = DictsMean.First(o => o.DICTID.ToString() == dictid);
                var ifrId = $"ifr{i + 1}";
                var url = item.UrlString(word, AutoCorrects.ToList());
                s += $"<iframe id='{ifrId}' frameborder='1' style='width:100%; height:500px; display:block' src='{url}'></iframe>\n";
            }
            s += "</body></html>\n";
            return s;
        }

        public async Task<bool> UpdateLang() => await UserSettingDS.UpdateLang(SelectedUSUser0.ID, USLANGID);
        public async Task<bool> UpdateTextbook() => await UserSettingDS.UpdateTextbook(SelectedUSLang2.ID, USTEXTBOOKID);
        public async Task<bool> UpdateDictItem() => await UserSettingDS.UpdateDictItem(SelectedUSLang2.ID, USDICTITEM);
        public async Task<bool> UpdateDictNote() => await UserSettingDS.UpdateDictNote(SelectedUSLang2.ID, USDICTNOTEID);
        public async Task<bool> UpdateUnitFrom() => await UserSettingDS.UpdateUnitFrom(SelectedUSTextbook.ID, USUNITFROM);
        public async Task<bool> UpdatePartFrom() => await UserSettingDS.UpdatePartFrom(SelectedUSTextbook.ID, USPARTFROM);
        public async Task<bool> UpdateUnitTo() => await UserSettingDS.UpdateUnitTo(SelectedUSTextbook.ID, USUNITTO);
        public async Task<bool> UpdatePartTo() => await UserSettingDS.UpdatePartTo(SelectedUSTextbook.ID, USPARTTO);
    }
}
