using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;

namespace LollyShared
{
    public class SettingsViewModel : LollyViewModel
    {
        UserSettingDataStore UserSettingDS = new UserSettingDataStore();
        LanguageDataStore LanguageDS = new LanguageDataStore();
        DictOnlineDataStore DictOnlineDS = new DictOnlineDataStore();
        DictNoteDataStore DictNoteDS = new DictNoteDataStore();
        TextbookDataStore TextbookDS = new TextbookDataStore();

        public ObservableCollection<MUserSetting> UserSettings { get; set; }
        int SelectedUSUserIndex { get; set; }
        MUserSetting SelectedUSUser => UserSettings[SelectedUSUserIndex];
        public int USLANGID {
            get => int.Parse(SelectedUSUser.VALUE1);
            set => SelectedUSUser.VALUE1 = value.ToString();
        }
        int SelectedUSLangIndex { get; set; }
        MUserSetting SelectedUSLang => UserSettings[SelectedUSLangIndex];
        public int USTEXTBOOKID
        {
            get => int.Parse(SelectedUSLang.VALUE1);
            set => SelectedUSLang.VALUE1 = value.ToString();
        }
        public int USDICTONLINEID
        {
            get => int.Parse(SelectedUSLang.VALUE2);
            set => SelectedUSLang.VALUE2 = value.ToString();
        }
        public int USDICTNOTEID
        {
            get => int.Parse(SelectedUSLang.VALUE3);
            set => SelectedUSLang.VALUE3 = value.ToString();
        }
        int SelectedUSTextbookIndex { get; set; }
        MUserSetting SelectedUSTextbook => UserSettings[SelectedUSTextbookIndex];
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
        int SelectedLangIndex { get; set; }
        MLanguage SelectedLang => Languages[SelectedLangIndex];

        public ObservableCollection<MDictOnline> DictsOnline { get; set; }
        int selectedDictOnlineIndex = 0;
        public int SelectedDictOnlineIndex {
            get => selectedDictOnlineIndex;
            set {
                selectedDictOnlineIndex = value;
                USDICTONLINEID = SelectedDictOnline.ID;
            }
        }
        public MDictOnline SelectedDictOnline => DictsOnline[SelectedDictOnlineIndex];

        public ObservableCollection<MDictNote> DictsNote { get; set; }
        int selectedDictNoteIndex = 0;
        public int SelectedDictNoteIndex {
            get => selectedDictNoteIndex;
            set {
                selectedDictNoteIndex = value;
                USDICTNOTEID = SelectedDictNote?.ID ?? 0;
            }
        }
        MDictNote SelectedDictNote => DictsNote.Count == 0 ? null : DictsNote[SelectedDictNoteIndex];

        public ObservableCollection<MTextbook> Textbooks { get; set; }
        int selectedTextbookIndex = 0;
        public int SelectedTextbookIndex {
            get => selectedTextbookIndex;
            set {
                selectedTextbookIndex = value;
                SetSelectedTextbookIndex();
            }
        }
        public MTextbook SelectedTextbook => Textbooks[SelectedTextbookIndex];

        public ObservableCollection<string> Units { get; set; }
        public ObservableCollection<string> Parts { get; set; }
        public ObservableCollection<> AutoCorrects { get; set; }

        public int UserId = 1;

        public async Task GetData() {
            Languages = await GetData(async () => await LanguageDS.GetData());
            UserSettings = await GetData(async () => await UserSettingDS.GetDataByUser(UserId));
            SelectedUSUserIndex = UserSettings.ToList().FindIndex(o => o.KIND == 1);
            await SetSelectedLangIndex(Languages.ToList().FindIndex(o => o.ID == USLANGID));
        }

        public async Task SetSelectedLangIndex(int langindex) {
            SelectedLangIndex = langindex;
            USLANGID = SelectedLang.ID;
            SelectedUSLangIndex = UserSettings.ToList().FindIndex(o => o.KIND == 2 && o.ENTITYID == USLANGID);
            DictsOnline = await GetData(async () => await DictOnlineDS.GetDataByLang(USLANGID));
            SelectedDictOnlineIndex = DictsOnline.ToList().FindIndex(o => o.ID == USDICTONLINEID);
            DictsNote = await GetData(async () => await DictNoteDS.GetDataByLang(USLANGID));
            SelectedDictNoteIndex = DictsNote.ToList().FindIndex(o => o.ID == USDICTNOTEID);
            Textbooks = await GetData(async () => await TextbookDS.GetDataByLang(USLANGID));
            SelectedTextbookIndex = Textbooks.ToList().FindIndex(o => o.ID == USTEXTBOOKID);
        }

        void SetSelectedTextbookIndex() {
            USTEXTBOOKID = SelectedTextbook.ID;
            SelectedUSTextbookIndex = UserSettings.ToList().FindIndex(o => o.KIND == 3 && o.ENTITYID == USTEXTBOOKID);
            Units = new ObservableCollection<string>(Enumerable.Range(1, SelectedTextbook.UNITS).Select(i => i.ToString()));
            Parts = new ObservableCollection<string>(SelectedTextbook.PARTS.Split(' '));
        }

        public async Task<bool> UpdateLang() => await UserSettingDS.UpdateLang(SelectedUSUser.ID, USLANGID);
        public async Task<bool> UpdateTextbook() => await UserSettingDS.UpdateTextbook(SelectedUSLang.ID, USTEXTBOOKID);
        public async Task<bool> UpdateDictOnline() => await UserSettingDS.UpdateDictOnline(SelectedUSLang.ID, USDICTONLINEID);
        public async Task<bool> UpdateDictNote() => await UserSettingDS.UpdateDictNote(SelectedUSLang.ID, USDICTNOTEID);
        public async Task<bool> UpdateUnitFrom() => await UserSettingDS.UpdateUnitFrom(SelectedUSTextbook.ID, USUNITFROM);
        public async Task<bool> UpdatePartFrom() => await UserSettingDS.UpdatePartFrom(SelectedUSTextbook.ID, USPARTFROM);
        public async Task<bool> UpdateUnitTo() => await UserSettingDS.UpdateUnitTo(SelectedUSTextbook.ID, USUNITTO);
        public async Task<bool> UpdatePartTo() => await UserSettingDS.UpdatePartTo(SelectedUSTextbook.ID, USPARTTO);
    }
}
