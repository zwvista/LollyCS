using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Globalization;
using System.Net.Http;
using ReactiveUI.Fody.Helpers;
using System.Reflection;

namespace LollyShared
{
    public class SettingsViewModel : ReactiveObject
    {
        USMappingDataStore USMappingDS = new USMappingDataStore();
        UserSettingDataStore UserSettingDS = new UserSettingDataStore();
        LanguageDataStore LanguageDS = new LanguageDataStore();
        DictReferenceDataStore DictReferenceDS = new DictReferenceDataStore();
        DictNoteDataStore DictNoteDS = new DictNoteDataStore();
        DictTranslationDataStore DictTranslationDS = new DictTranslationDataStore();
        TextbookDataStore TextbookDS = new TextbookDataStore();
        AutoCorrectDataStore AutoCorrectDS = new AutoCorrectDataStore();
        WordFamiDataStore WordFamiDS = new WordFamiDataStore();
        VoiceDataStore VoiceDS = new VoiceDataStore();
        CodeDataStore CodeDS = new CodeDataStore();

        public event EventHandler OnGetData;
        public event EventHandler OnUpdateLang;
        public event EventHandler OnUpdateDictItem;
        public event EventHandler OnUpdateDictNote;
        public event EventHandler OnUpdateDictTranslation;
        public event EventHandler OnUpdateTextbook;
        public event EventHandler OnUpdateUnitFrom;
        public event EventHandler OnUpdatePartFrom;
        public event EventHandler OnUpdateUnitTo;
        public event EventHandler OnUpdatePartTo;
        public event EventHandler OnUpdateVoice;
        public event EventHandler OnUpdateToType;

        public SettingsViewModel ShallowCopy() => (SettingsViewModel)this.MemberwiseClone();

        public List<MUSMapping> USMappings { get; set; }
        public List<MUserSetting> UserSettings { get; set; }
        (MUserSetting, PropertyInfo) GetUS(MUserSettingInfo info)
        {
            var o = UserSettings?.FirstOrDefault(v => v.ID == info.USERSETTINGID);
            var pi = o?.GetType().GetProperty($"VALUE{info.VALUEID}");
            return (o, pi);
        }
        string GetUSValue(MUserSettingInfo info)
        {
            var (o, pi) = GetUS(info);
            return pi?.GetValue(o) as string;
        }
        void SetUSValue(MUserSettingInfo info, string value, string name)
        {
            var (o, pi) = GetUS(info);
            pi?.SetValue(o, value);
            this.RaisePropertyChanged(name);
        }
        MUserSettingInfo INFO_USLANGID = new MUserSettingInfo();
        public int USLANGID
        {
            get => int.Parse(GetUSValue(INFO_USLANGID));
            set => SetUSValue(INFO_USLANGID, value.ToString(), nameof(USLANGID));
        }
        MUserSettingInfo INFO_USROWSPERPAGEOPTIONS = new MUserSettingInfo();
        public List<int> USROWSPERPAGEOPTIONS =>
            GetUSValue(INFO_USROWSPERPAGEOPTIONS).Split(',').Select(s => int.Parse(s)).ToList();
        MUserSettingInfo INFO_USROWSPERPAGE = new MUserSettingInfo();
        public int USROWSPERPAGE => int.Parse(GetUSValue(INFO_USROWSPERPAGE));
        MUserSettingInfo INFO_USLEVELCOLORS = new MUserSettingInfo();
        public Dictionary<int, List<string>> USLEVELCOLORS;
        MUserSettingInfo INFO_USSCANINTERVAL = new MUserSettingInfo();
        public int USSCANINTERVAL
        {
            get => int.Parse(GetUSValue(INFO_USSCANINTERVAL));
            set => SetUSValue(INFO_USSCANINTERVAL, value.ToString(), nameof(USSCANINTERVAL));
        }
        MUserSettingInfo INFO_USREVIEWINTERVAL = new MUserSettingInfo();
        public int USREVIEWINTERVAL
        {
            get => int.Parse(GetUSValue(INFO_USREVIEWINTERVAL));
            set => SetUSValue(INFO_USREVIEWINTERVAL, value.ToString(), nameof(USREVIEWINTERVAL));
        }
        MUserSettingInfo INFO_USREADNUMBERID = new MUserSettingInfo();
        public int USREADNUMBERID
        {
            get => int.Parse(GetUSValue(INFO_USREADNUMBERID));
            set => SetUSValue(INFO_USREADNUMBERID, value.ToString(), nameof(USREADNUMBERID));
        }
        MUserSettingInfo INFO_USTEXTBOOKID = new MUserSettingInfo();
        public int USTEXTBOOKID
        {
            get => int.Parse(GetUSValue(INFO_USTEXTBOOKID));
            set => SetUSValue(INFO_USTEXTBOOKID, value.ToString(), nameof(USTEXTBOOKID));
        }
        MUserSettingInfo INFO_USDICTITEM = new MUserSettingInfo();
        public string USDICTITEM
        {
            get => GetUSValue(INFO_USDICTITEM);
            set => SetUSValue(INFO_USDICTITEM, value, nameof(USDICTITEM));
        }
        MUserSettingInfo INFO_USDICTNOTEID = new MUserSettingInfo();
        public int USDICTNOTEID
        {
            get => int.TryParse(GetUSValue(INFO_USDICTNOTEID), out var v) ? v : 0;
            set => SetUSValue(INFO_USDICTNOTEID, value.ToString(), nameof(USDICTNOTEID));
        }
        MUserSettingInfo INFO_USDICTITEMS = new MUserSettingInfo();
        public string USDICTITEMS
        {
            get => GetUSValue(INFO_USDICTITEMS);
            set => SetUSValue(INFO_USDICTITEMS, value, nameof(USDICTITEMS));
        }
        MUserSettingInfo INFO_USDICTTRANSLATIONID = new MUserSettingInfo();
        public int USDICTTRANSLATIONID
        {
            get => int.TryParse(GetUSValue(INFO_USDICTTRANSLATIONID), out var v) ? v : 0;
            set => SetUSValue(INFO_USDICTTRANSLATIONID, value.ToString(), nameof(USDICTTRANSLATIONID));
        }
        MUserSettingInfo INFO_USVOICEID = new MUserSettingInfo();
        public int USVOICEID
        {
            get => int.TryParse(GetUSValue(INFO_USVOICEID), out var v) ? v : 0;
            set => SetUSValue(INFO_USVOICEID, value.ToString(), nameof(USVOICEID));
        }
        MUserSettingInfo INFO_USUNITFROM = new MUserSettingInfo();
        public int USUNITFROM
        {
            get => int.TryParse(GetUSValue(INFO_USUNITFROM), out var v) ? v : 0;
            set => SetUSValue(INFO_USUNITFROM, value.ToString(), nameof(USUNITFROM));
        }
        MUserSettingInfo INFO_USPARTFROM = new MUserSettingInfo();
        public int USPARTFROM
        {
            get => int.TryParse(GetUSValue(INFO_USPARTFROM), out var v) ? v : 0;
            set => SetUSValue(INFO_USPARTFROM, value.ToString(), nameof(USPARTFROM));
        }
        MUserSettingInfo INFO_USUNITTO = new MUserSettingInfo();
        public int USUNITTO
        {
            get => int.TryParse(GetUSValue(INFO_USUNITTO), out var v) ? v : 0;
            set => SetUSValue(INFO_USUNITTO, value.ToString(), nameof(USUNITTO));
        }
        MUserSettingInfo INFO_USPARTTO = new MUserSettingInfo();
        public int USPARTTO
        {
            get => int.TryParse(GetUSValue(INFO_USPARTTO), out var v) ? v : 0;
            set => SetUSValue(INFO_USPARTTO, value.ToString(), nameof(USPARTTO));
        }
        public int USUNITPARTFROM => USUNITFROM * 10 + USPARTFROM;
        public int USUNITPARTTO => USUNITTO * 10 + USPARTTO;
        public bool IsSingleUnitPart => USUNITPARTFROM == USUNITPARTTO;
        public bool IsInvalidUnitPart => USUNITPARTFROM > USUNITPARTTO;

        [Reactive]
        public List<MLanguage> Languages { get; set; }
        [Reactive]
        public MLanguage SelectedLang { get; set; }

        [Reactive]
        public List<MVoice> Voices { get; set; }
        [Reactive]
        public MVoice SelectedVoice { get; set; }

        public List<MDictReference> DictsReference;
        [Reactive]
        public List<MDictItem> DictItems { get; set; }
        [Reactive]
        public MDictItem SelectedDictItem { get; set; }
        [Reactive]
        public List<MDictItem> SelectedDictItems { get; set; }

        [Reactive]
        public List<MDictNote> DictsNote { get; set; }
        [Reactive]
        public MDictNote SelectedDictNote { get; set; }

        [Reactive]
        public List<MDictTranslation> DictsTranslation { get; set; }
        [Reactive]
        public MDictTranslation SelectedDictTranslation { get; set; }
        public bool HasDictTranslation => SelectedDictTranslation != null;

        [Reactive]
        public List<MTextbook> Textbooks { get; set; }
        [Reactive]
        public MTextbook SelectedTextbook { get; set; }
        public List<MSelectItem> TextbookFilters { get; set; } = new List<MSelectItem>();

        public List<MSelectItem> Units => SelectedTextbook?.Units;
        public int UnitCount => Units?.Count ?? 0;
        public string UnitsInAll => $"({UnitCount} in all)";
        public List<MSelectItem> Parts => SelectedTextbook?.Parts;
        public int PartCount => Parts?.Count ?? 0;
        public bool IsSingleUnit => USUNITFROM == USUNITTO && USPARTFROM == 1 && USPARTTO == PartCount;
        public bool IsSinglePart => PartCount == 1;

        public static List<MSelectItem> ToTypes { get; set; } = new List<MSelectItem>
        {
            new MSelectItem(0, "Unit"),
            new MSelectItem(1, "Part"),
            new MSelectItem(2, "To"),
        };
        [Reactive]
        public UnitPartToType ToType { get; set; } = UnitPartToType.To;

        public List<MAutoCorrect> AutoCorrects { get; set; }
        public List<MCode> DictCodes { get; set; }
        public List<MCode> ReadNumberCodes { get; set; }
        public static List<string> ScopeWordFilters { get; } = new List<string> { "Word", "Note" };
        public static List<string> ScopePhraseFilters { get; } = new List<string> { "Phrase", "Translation" };
        public static List<string> ScopePatternFilters { get; } = new List<string> { "Pattern", "Note" };
        public static List<MSelectItem> ReviewModes { get; set; } = new List<MSelectItem>
        {
            new MSelectItem(0, "Review(Auto)"),
            new MSelectItem(1, "Test"),
            new MSelectItem(2, "Review(Manual)"),
        };

        public HttpClient client = new HttpClient();

        public SettingsViewModel()
        {
            this.WhenAnyValue(x => x.SelectedDictItem, v => USDICTITEM = v.DICTID);
            this.WhenAnyValue(x => x.SelectedDictNote, v => USDICTNOTEID = v?.ID ?? 0);
            this.WhenAnyValue(x => x.SelectedDictTranslation, v => USDICTTRANSLATIONID = v?.ID ?? 0);
            this.WhenAnyValue(x => x.SelectedTextbook).Subscribe(v =>
            {
                if (v == null) return;
                USTEXTBOOKID = v.ID;
                INFO_USUNITFROM = GetUSInfo(MUSMapping.NAME_USUNITFROM);
                INFO_USPARTFROM = GetUSInfo(MUSMapping.NAME_USPARTFROM);
                INFO_USUNITTO = GetUSInfo(MUSMapping.NAME_USUNITTO);
                INFO_USPARTTO = GetUSInfo(MUSMapping.NAME_USPARTTO);
                ToType = IsSingleUnit ? UnitPartToType.Unit : IsSingleUnitPart ? UnitPartToType.Part : UnitPartToType.To;
                this.RaisePropertyChanged(nameof(Units));
                this.RaisePropertyChanged(nameof(UnitsInAll));
                this.RaisePropertyChanged(nameof(Parts));
                this.RaisePropertyChanged(nameof(USUNITFROM));
                this.RaisePropertyChanged(nameof(USPARTFROM));
                this.RaisePropertyChanged(nameof(USUNITTO));
                this.RaisePropertyChanged(nameof(USPARTTO));
            });
            this.WhenAnyValue(x => x.ToType).Subscribe(_ => OnUpdateToType?.Invoke(this, null));
        }

        MUserSettingInfo GetUSInfo(string name) {
            var o = USMappings.First(v => v.NAME == name);
            var entityid = o.ENTITYID != -1 ? o.ENTITYID :
                o.LEVEL == 1 ? SelectedLang.ID :
                o.LEVEL == 2 ? SelectedTextbook.ID :
                0;
            var o2 = UserSettings.First(v => v.KIND == o.KIND && v.ENTITYID == entityid);
            return new MUserSettingInfo { USERSETTINGID = o2.ID, VALUEID = o.VALUEID };
        }

        public async Task GetData() {
            Languages = await LanguageDS.GetData();
            USMappings = await USMappingDS.GetData();
            UserSettings = await UserSettingDS.GetDataByUser(CommonApi.UserId);
            DictCodes = await CodeDS.GetDictCodes();
            ReadNumberCodes = await CodeDS.GetReadNumberCodes();
            INFO_USLANGID = GetUSInfo(MUSMapping.NAME_USLANGID);
            INFO_USROWSPERPAGEOPTIONS = GetUSInfo(MUSMapping.NAME_USROWSPERPAGEOPTIONS);
            INFO_USROWSPERPAGE = GetUSInfo(MUSMapping.NAME_USROWSPERPAGE);
            INFO_USLEVELCOLORS = GetUSInfo(MUSMapping.NAME_USLEVELCOLORS);
            INFO_USSCANINTERVAL = GetUSInfo(MUSMapping.NAME_USSCANINTERVAL);
            INFO_USREVIEWINTERVAL = GetUSInfo(MUSMapping.NAME_USREVIEWINTERVAL);
            INFO_USREADNUMBERID = GetUSInfo(MUSMapping.NAME_USREADNUMBERID);
            USLEVELCOLORS = GetUSValue(INFO_USLEVELCOLORS).Split(new[] { "\r\n" }, StringSplitOptions.None)
                .Select(s => s.Split(',')).ToDictionary(v => int.Parse(v[0]), v2 => new List<string> { v2[1], v2[2] });
            OnGetData?.Invoke(this, null);
            await SetSelectedLang(Languages.FirstOrDefault(o => o.ID == USLANGID));
        }

        public async Task SetSelectedLang(MLanguage lang) {
            var isinit = USLANGID == lang.ID;
            USLANGID = lang.ID;
            SelectedLang = lang;
            INFO_USTEXTBOOKID = GetUSInfo(MUSMapping.NAME_USTEXTBOOKID);
            INFO_USDICTITEM = GetUSInfo(MUSMapping.NAME_USDICTITEM);
            INFO_USDICTNOTEID = GetUSInfo(MUSMapping.NAME_USDICTNOTEID);
            INFO_USDICTITEMS = GetUSInfo(MUSMapping.NAME_USDICTITEMS);
            INFO_USDICTTRANSLATIONID = GetUSInfo(MUSMapping.NAME_USDICTTRANSLATIONID);
            INFO_USVOICEID = GetUSInfo(MUSMapping.NAME_USWINDOWSVOICEID);
            DictsReference = await DictReferenceDS.GetDataByLang(USLANGID);
            DictsNote = await DictNoteDS.GetDataByLang(USLANGID);
            DictsTranslation = await DictTranslationDS.GetDataByLang(USLANGID);
            Textbooks = await TextbookDS.GetDataByLang(USLANGID);
            AutoCorrects = await AutoCorrectDS.GetDataByLang(USLANGID);
            Voices = await VoiceDS.GetDataByLang(USLANGID);
            DictItems = DictsReference.Select(o => new MDictItem(o.DICTID.ToString(), o.DICTNAME)).ToList();
            SelectedDictItem = DictItems.FirstOrDefault(o => o.DICTID == USDICTITEM);
            SelectedDictNote = DictsNote.FirstOrDefault(o => o.ID == USDICTNOTEID) ?? DictsNote.FirstOrDefault();
            var dictitems = USDICTITEM.Split(',').ToList();
            SelectedDictItems = DictItems.Where(o => dictitems.Contains(o.DICTID)).ToList();
            SelectedDictTranslation = DictsTranslation.FirstOrDefault(o => o.ID == USDICTTRANSLATIONID) ?? DictsTranslation.FirstOrDefault();
            SelectedTextbook = Textbooks.FirstOrDefault(o => o.ID == USTEXTBOOKID);
            TextbookFilters = Textbooks.Select(o => new MSelectItem(o.ID, o.TEXTBOOKNAME))
                .StartWith(new MSelectItem(0, "All Textbooks")).ToList();
            SelectedVoice = Voices.FirstOrDefault(o => o.ID == USDICTTRANSLATIONID) ?? Voices.FirstOrDefault();
            if (isinit)
                OnUpdateLang?.Invoke(this, null);
            else
                await UpdateLang();
        }

        public async Task UpdateLang()
        {
            await UserSettingDS.Update(INFO_USLANGID, USLANGID);
            OnUpdateLang?.Invoke(this, null);
        }
        public async Task UpdateTextbook()
        {
            await UserSettingDS.Update(INFO_USTEXTBOOKID, USTEXTBOOKID);
            OnUpdateTextbook?.Invoke(this, null);
        }
        public async Task UpdateDictItem()
        {
            await UserSettingDS.Update(INFO_USDICTITEM, USDICTITEM);
            OnUpdateDictItem?.Invoke(this, null);
        }
        public async Task UpdateDictNote()
        {
            await UserSettingDS.Update(INFO_USDICTNOTEID, USDICTNOTEID);
            OnUpdateDictNote?.Invoke(this, null);
        }
        public async Task UpdateDictTranslation()
        {
            await UserSettingDS.Update(INFO_USDICTTRANSLATIONID, USDICTTRANSLATIONID);
            OnUpdateDictTranslation?.Invoke(this, null);
        }
        public async Task UpdateVoice()
        {
            await UserSettingDS.Update(INFO_USVOICEID, USVOICEID);
            OnUpdateVoice?.Invoke(this, null);
        }
        public string AutoCorrectInput(string text) => AutoCorrectDS.AutoCorrect(text, AutoCorrects, o => o.INPUT, o => o.EXTENDED);
        public async Task UpdateUnitFrom()
        {
            await DoUpdateUnitFrom(USUNITFROM, check: false);
            if (ToType == UnitPartToType.Unit)
                await DoUpdateSingleUnit();
            else if (ToType == UnitPartToType.Part || IsInvalidUnitPart)
                await DoUpdateUnitPartTo();
        }
        public async Task UpdatePartFrom()
        {
            await DoUpdatePartFrom(USPARTFROM, check: false);
            if (ToType == UnitPartToType.Part || IsInvalidUnitPart)
                await DoUpdateUnitPartTo();
        }
        public async Task UpdateUnitTo()
        {
            await DoUpdateUnitTo(USUNITTO, check: false);
            if (IsInvalidUnitPart)
                await DoUpdateUnitPartFrom();
        }
        public async Task UpdatePartTo()
        {
            await DoUpdatePartTo(USPARTTO, check: false);
            if (IsInvalidUnitPart)
                await DoUpdateUnitPartFrom();
        }
        public async Task UpdateToType()
        {
            if (ToType == UnitPartToType.Unit)
                await DoUpdateSingleUnit();
            else if (ToType == UnitPartToType.Part)
                await DoUpdateUnitPartTo();
        }
        public async Task ToggleToType(int part)
        {
            if (ToType == UnitPartToType.Unit)
            {
                ToType = UnitPartToType.Part;
                await DoUpdatePartFrom(part);
                await DoUpdateUnitPartTo();
            }
            else if (ToType == UnitPartToType.Part)
            {
                ToType = UnitPartToType.Unit;
                await DoUpdateSingleUnit();
            }
        }
        public async Task PreviousUnitPart()
        {
            if (ToType == UnitPartToType.Unit)
            {
                if (USUNITFROM > 1)
                {
                    await DoUpdateUnitFrom(USUNITFROM - 1);
                    await DoUpdateUnitTo(USUNITFROM);
                }
            }
            else if (USPARTFROM > 1)
            {
                await DoUpdatePartFrom(USPARTFROM - 1);
                await DoUpdateUnitPartTo();
            }
            else if (USUNITFROM > 1)
            {
                await DoUpdateUnitFrom(USUNITFROM - 1);
                await DoUpdatePartFrom(PartCount);
                await DoUpdateUnitPartTo();
            }
        }
        public async Task NextUnitPart()
        {
            if (ToType == UnitPartToType.Unit)
            {
                if (USUNITFROM < UnitCount)
                {
                    await DoUpdateUnitFrom(USUNITFROM + 1);
                    await DoUpdateUnitTo(USUNITFROM);
                }
            }
            else if (USPARTFROM < PartCount)
            {
                await DoUpdatePartFrom(USPARTFROM + 1);
                await DoUpdateUnitPartTo();
            }
            else if (USUNITFROM < UnitCount)
            {
                await DoUpdateUnitFrom(USUNITFROM + 1);
                await DoUpdatePartFrom(1);
                await DoUpdateUnitPartTo();
            }
        }
        async Task DoUpdateUnitPartFrom()
        {
            await DoUpdateUnitFrom(USUNITTO);
            await DoUpdatePartFrom(USPARTTO);
        }
        async Task DoUpdateUnitPartTo()
        {
            await DoUpdateUnitTo(USUNITFROM);
            await DoUpdatePartTo(USPARTFROM);
        }
        async Task DoUpdateSingleUnit()
        {
            await DoUpdateUnitTo(USUNITFROM);
            await DoUpdatePartFrom(1);
            await DoUpdatePartTo(PartCount);
        }
        async Task DoUpdateUnitFrom(int v, bool check = true)
        {
            if (check && USUNITFROM == v) return;
            await UserSettingDS.Update(INFO_USUNITFROM, USUNITFROM = v);
            OnUpdateUnitFrom?.Invoke(this, null);
        }
        async Task DoUpdatePartFrom(int v, bool check = true)
        {
            if (check && USPARTFROM == v) return;
            await UserSettingDS.Update(INFO_USPARTFROM, USPARTFROM = v);
            OnUpdatePartFrom?.Invoke(this, null);
        }
        async Task DoUpdateUnitTo(int v, bool check = true)
        {
            if (check && USUNITTO == v) return;
            await UserSettingDS.Update(INFO_USUNITTO, USUNITTO = v);
            OnUpdateUnitTo?.Invoke(this, null);
        }
        async Task DoUpdatePartTo(int v, bool check = true)
        {
            if (check && USPARTTO == v) return;
            await UserSettingDS.Update(INFO_USPARTTO, USPARTTO = v);
            OnUpdatePartTo?.Invoke(this, null);
        }
        public async Task UpdateLevel(int wordid, int level) => await WordFamiDS.Update(wordid, level);
        public async Task UpdateReadNumberId() => await UserSettingDS.Update(INFO_USREADNUMBERID, USREADNUMBERID);
    }
}
