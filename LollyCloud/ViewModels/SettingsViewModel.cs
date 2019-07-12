using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Globalization;
using System.Net.Http;

namespace LollyShared
{
    public class SettingsViewModel : LollyViewModel
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

        public List<MUSMapping> USMappings { get; set; }
        public List<MUserSetting> UserSettings { get; set; }
        private string GetUSValue(MUserSettingInfo info)
        {
            var o = UserSettings?.FirstOrDefault(v => v.ID == info.USERSETTINGID);
            return o?.GetType().GetProperty($"VALUE{info.VALUEID}").GetValue(o) as string;
        }
        private void SetUSValue(MUserSettingInfo info, string value, string name)
        {
            var o = UserSettings?.FirstOrDefault(v => v.ID == info.USERSETTINGID);
            o?.GetType().GetProperty($"VALUE{info.VALUEID}").SetValue(o, value);
            this.RaisePropertyChanged(name);
        }
        private MUserSettingInfo INFO_USLANGID = new MUserSettingInfo();
        public int USLANGID
        {
            get => int.Parse(GetUSValue(INFO_USLANGID));
            set => SetUSValue(INFO_USLANGID, value.ToString(), nameof(USLANGID));
        }
        private MUserSettingInfo INFO_USROWSPERPAGEOPTIONS = new MUserSettingInfo();
        public List<int> USROWSPERPAGEOPTIONS =>
            GetUSValue(INFO_USROWSPERPAGEOPTIONS).Split(',').Select(s => int.Parse(s)).ToList();
        private MUserSettingInfo INFO_USROWSPERPAGE = new MUserSettingInfo();
        public int USROWSPERPAGE => int.Parse(GetUSValue(INFO_USROWSPERPAGE));
        private MUserSettingInfo INFO_USLEVELCOLORS = new MUserSettingInfo();
        public Dictionary<int, List<string>> USLEVELCOLORS;
        private MUserSettingInfo INFO_USSCANINTERVAL = new MUserSettingInfo();
        public int USSCANINTERVAL
        {
            get => int.Parse(GetUSValue(INFO_USSCANINTERVAL));
            set => SetUSValue(INFO_USSCANINTERVAL, value.ToString(), nameof(USSCANINTERVAL));
        }
        private MUserSettingInfo INFO_USREVIEWINTERVAL = new MUserSettingInfo();
        public int USREVIEWINTERVAL
        {
            get => int.Parse(GetUSValue(INFO_USREVIEWINTERVAL));
            set => SetUSValue(INFO_USREVIEWINTERVAL, value.ToString(), nameof(USREVIEWINTERVAL));
        }
        private MUserSettingInfo INFO_USTEXTBOOKID = new MUserSettingInfo();
        public int USTEXTBOOKID
        {
            get => int.Parse(GetUSValue(INFO_USTEXTBOOKID));
            set => SetUSValue(INFO_USTEXTBOOKID, value.ToString(), nameof(USTEXTBOOKID));
        }
        private MUserSettingInfo INFO_USDICTITEM = new MUserSettingInfo();
        public string USDICTITEM
        {
            get => GetUSValue(INFO_USDICTITEM);
            set => SetUSValue(INFO_USDICTITEM, value, nameof(USDICTITEM));
        }
        private MUserSettingInfo INFO_USDICTNOTEID = new MUserSettingInfo();
        public int USDICTNOTEID
        {
            get => int.TryParse(GetUSValue(INFO_USDICTNOTEID), out var v) ? v : 0;
            set => SetUSValue(INFO_USDICTNOTEID, value.ToString(), nameof(USDICTNOTEID));
        }
        private MUserSettingInfo INFO_USDICTITEMS = new MUserSettingInfo();
        public string USDICTITEMS
        {
            get => GetUSValue(INFO_USDICTITEMS) ?? "0";
            set => SetUSValue(INFO_USDICTITEMS, value, nameof(USDICTITEMS));
        }
        private MUserSettingInfo INFO_USDICTTRANSLATIONID = new MUserSettingInfo();
        public int USDICTTRANSLATIONID
        {
            get => int.TryParse(GetUSValue(INFO_USDICTTRANSLATIONID), out var v) ? v : 0;
            set => SetUSValue(INFO_USDICTTRANSLATIONID, value.ToString(), nameof(USDICTTRANSLATIONID));
        }
        private MUserSettingInfo INFO_USVOICEID = new MUserSettingInfo();
        public int USVOICEID
        {
            get => int.TryParse(GetUSValue(INFO_USVOICEID), out var v) ? v : 0;
            set => SetUSValue(INFO_USVOICEID, value.ToString(), nameof(USVOICEID));
        }
        private MUserSettingInfo INFO_USUNITFROM = new MUserSettingInfo();
        public int USUNITFROM
        {
            get => int.TryParse(GetUSValue(INFO_USUNITFROM), out var v) ? v : 0;
            set => SetUSValue(INFO_USUNITFROM, value.ToString(), nameof(USUNITFROM));
        }
        private MUserSettingInfo INFO_USPARTFROM = new MUserSettingInfo();
        public int USPARTFROM
        {
            get => int.TryParse(GetUSValue(INFO_USPARTFROM), out var v) ? v : 0;
            set => SetUSValue(INFO_USPARTFROM, value.ToString(), nameof(USPARTFROM));
        }
        private MUserSettingInfo INFO_USUNITTO = new MUserSettingInfo();
        public int USUNITTO
        {
            get => int.TryParse(GetUSValue(INFO_USUNITTO), out var v) ? v : 0;
            set => SetUSValue(INFO_USUNITTO, value.ToString(), nameof(USUNITTO));
        }
        private MUserSettingInfo INFO_USPARTTO = new MUserSettingInfo();
        public int USPARTTO
        {
            get => int.TryParse(GetUSValue(INFO_USPARTTO), out var v) ? v : 0;
            set => SetUSValue(INFO_USPARTTO, value.ToString(), nameof(USPARTTO));
        }
        public int USUNITPARTFROM => USUNITFROM * 10 + USPARTFROM;
        public int USUNITPARTTO => USUNITTO * 10 + USPARTTO;
        public bool IsSingleUnitPart => USUNITPARTFROM == USUNITPARTTO;
        public bool IsInvalidUnitPart => USUNITPARTFROM > USUNITPARTTO;

        List<MLanguage> _Languages;
        public List<MLanguage> Languages
        {
            get => _Languages;
            set => this.RaiseAndSetIfChanged(ref _Languages, value);
        }
        MLanguage _SelectedLang;
        public MLanguage SelectedLang
        {
            get => _SelectedLang;
            set => this.RaiseAndSetIfChanged(ref _SelectedLang, value);
        }
        public int SelectedLangIndex => Languages.IndexOf(_SelectedLang);

        List<MVoice> _Voices;
        public List<MVoice> Voices
        {
            get => _Voices;
            set => this.RaiseAndSetIfChanged(ref _Voices, value);
        }
        MVoice _SelectedVoice;
        public MVoice SelectedVoice
        {
            get => _SelectedVoice;
            set => this.RaiseAndSetIfChanged(ref _SelectedVoice, value);
        }
        public int SelectedVoiceIndex => _Voices.IndexOf(_SelectedVoice);

        public List<MDictReference> DictsReference;
        List<MDictItem> _DictItems;
        public List<MDictItem> DictItems
        {
            get => _DictItems;
            set => this.RaiseAndSetIfChanged(ref _DictItems, value);
        }
        MDictItem _SelectedDictItem;
        public MDictItem SelectedDictItem {
            get => _SelectedDictItem;
            set {
                if (value == null) return;
                this.RaiseAndSetIfChanged(ref _SelectedDictItem, value);
                USDICTITEM = value.DICTID;
            }
        }
        public int SelectedDictItemIndex => DictItems.IndexOf(_SelectedDictItem);

        List<MDictNote> _DictsNote;
        public List<MDictNote> DictsNote
        {
            get => _DictsNote;
            set => this.RaiseAndSetIfChanged(ref _DictsNote, value);
        }
        MDictNote _SelectedDictNote = new MDictNote();
        public MDictNote SelectedDictNote {
            get => _SelectedDictNote;
            set {
                this.RaiseAndSetIfChanged(ref _SelectedDictNote, value);
                USDICTNOTEID = value?.ID ?? 0;
            }
        }
        public int SelectedDictNoteIndex => DictsNote.IndexOf(_SelectedDictNote);

        List<MDictTranslation> _DictsTranslation;
        public List<MDictTranslation> DictsTranslation
        {
            get => _DictsTranslation;
            set => this.RaiseAndSetIfChanged(ref _DictsTranslation, value);
        }
        MDictTranslation _SelectedDictTranslation = new MDictTranslation();
        public MDictTranslation SelectedDictTranslation
        {
            get => _SelectedDictTranslation;
            set
            {
                this.RaiseAndSetIfChanged(ref _SelectedDictTranslation, value);
                USDICTTRANSLATIONID = value?.ID ?? 0;
            }
        }
        public int SelectedDictTranslationIndex => DictsTranslation.IndexOf(_SelectedDictTranslation);

        List<MTextbook> _Textbooks;
        public List<MTextbook> Textbooks
        {
            get => _Textbooks;
            set => this.RaiseAndSetIfChanged(ref _Textbooks, value);
        }
        MTextbook _SelectedTextbook;
        public MTextbook SelectedTextbook {
            get => _SelectedTextbook;
            set {
                if (value == null || value == _SelectedTextbook) return;
                this.RaiseAndSetIfChanged(ref _SelectedTextbook, value);
                USTEXTBOOKID = value.ID;
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
            }
        }
        public int SelectedTextbookIndex => Textbooks.IndexOf(_SelectedTextbook);

        public List<MSelectItem> Units => SelectedTextbook?.Units;
        public int UnitCount => Units?.Count ?? 0;
        public string UnitsInAll => $"({UnitCount} in all)";
        public List<MSelectItem> Parts => SelectedTextbook?.Parts;
        public int PartCount => Parts?.Count ?? 0;
        public bool IsSingleUnit => USUNITFROM == USUNITTO && USPARTFROM == 1 && USPARTTO == PartCount;
        public bool IsSinglePart => PartCount == 1;

        public List<MSelectItem> ToTypes { get; set; } = new List<MSelectItem>
        {
            new MSelectItem(0, "Unit"),
            new MSelectItem(1, "Part"),
            new MSelectItem(2, "To"),
        };
        UnitPartToType _ToType = UnitPartToType.Unit;
        public UnitPartToType ToType
        {
            get => _ToType;
            set
            {
                this.RaiseAndSetIfChanged(ref _ToType, value);
                OnUpdateToType?.Invoke(this, null);
            }
        }

        public List<MAutoCorrect> AutoCorrects { get; set; }

        public HttpClient client = new HttpClient();

        private MUserSettingInfo GetUSInfo(string name) {
            var o = USMappings.First(v => v.NAME == name);
            var entityid = o.ENTITYID != -1 ? o.ENTITYID :
                o.LEVEL == 1 ? SelectedLang.ID :
                o.LEVEL == 2 ? SelectedTextbook.ID :
                0;
            var o2 = UserSettings.First(v => v.KIND == o.KIND && v.ENTITYID == entityid);
            return new MUserSettingInfo { USERSETTINGID = o2.ID, VALUEID = o.VALUEID };
        }

        public async Task GetData() {
            Languages = await GetData(async () => await LanguageDS.GetData());
            USMappings = await GetData(async () => await USMappingDS.GetData());
            UserSettings = await GetData(async () => await UserSettingDS.GetDataByUser(CommonApi.UserId));
            INFO_USLANGID = GetUSInfo(MUSMapping.NAME_USLANGID);
            INFO_USROWSPERPAGEOPTIONS = GetUSInfo(MUSMapping.NAME_USROWSPERPAGEOPTIONS);
            INFO_USROWSPERPAGE = GetUSInfo(MUSMapping.NAME_USROWSPERPAGE);
            INFO_USLEVELCOLORS = GetUSInfo(MUSMapping.NAME_USLEVELCOLORS);
            INFO_USSCANINTERVAL = GetUSInfo(MUSMapping.NAME_USSCANINTERVAL);
            INFO_USREVIEWINTERVAL = GetUSInfo(MUSMapping.NAME_USREVIEWINTERVAL);
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
            var lstDicts = USDICTITEMS.Split(new[] { "\r\n" }, StringSplitOptions.None);
            DictsReference = await GetData(async () => await DictReferenceDS.GetDataByLang(USLANGID));
            DictsNote = await GetData(async () => await DictNoteDS.GetDataByLang(USLANGID));
            DictsTranslation = await GetData(async () => await DictTranslationDS.GetDataByLang(USLANGID));
            Textbooks = await GetData(async () => await TextbookDS.GetDataByLang(USLANGID));
            AutoCorrects = await GetData(async () => await AutoCorrectDS.GetDataByLang(USLANGID));
            Voices = await GetData(async () => await VoiceDS.GetDataByLang(USLANGID));
            var i = 0;
            DictItems = lstDicts.SelectMany(d => d == "0" ?
                DictsReference.Select(o => new MDictItem(o.DICTID.ToString(), o.DICTNAME)) :
                new List<MDictItem> { new MDictItem(d, $"Custom{++i}") }
            ).ToList();
            SelectedDictItem = DictItems.FirstOrDefault(o => o.DICTID == USDICTITEM);
            SelectedDictNote = DictsNote.FirstOrDefault(o => o.ID == USDICTNOTEID) ?? DictsNote.FirstOrDefault();
            SelectedDictTranslation = DictsTranslation.FirstOrDefault(o => o.ID == USDICTTRANSLATIONID) ?? DictsTranslation.FirstOrDefault();
            SelectedTextbook = Textbooks.FirstOrDefault(o => o.ID == USTEXTBOOKID);
            SelectedVoice = Voices.FirstOrDefault(o => o.ID == USDICTTRANSLATIONID) ?? Voices.FirstOrDefault();
            if (isinit)
                OnUpdateLang?.Invoke(this, null);
            else
                await UpdateLang();
        }

        public string DictHtml(string word, List<string> dictids)
        {
            var s = "<html><body>\n";
            foreach (var (dictid, i) in dictids.Select((dict, i) => (dict, i)))
            {
                var item = DictsReference.First(o => o.DICTID.ToString() == dictid);
                var ifrId = $"ifr{i + 1}";
                var url = item.UrlString(word, AutoCorrects.ToList());
                s += $"<iframe id='{ifrId}' frameborder='1' style='width:100%; height:500px; display:block' src='{url}'></iframe>\n";
            }
            s += "</body></html>\n";
            return s;
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
            await DoUpdateUnitFrom(USUNITFROM);
            if (ToType == UnitPartToType.Unit)
                await DoUpdateSingleUnit();
            else if (ToType == UnitPartToType.Part || IsInvalidUnitPart)
                await DoUpdateUnitPartTo();
        }
        public async Task UpdatePartFrom()
        {
            await DoUpdatePartFrom(USPARTFROM);
            if (ToType == UnitPartToType.Part || IsInvalidUnitPart)
                await DoUpdateUnitPartTo();
        }
        public async Task UpdateUnitTo()
        {
            await DoUpdateUnitFrom(USUNITTO);
            if (IsInvalidUnitPart)
                await DoUpdateUnitPartFrom();
        }
        public async Task UpdatePartTo()
        {
            await DoUpdatePartTo(USPARTTO);
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
        private async Task DoUpdateUnitPartFrom()
        {
            await DoUpdateUnitFrom(USUNITTO);
            await DoUpdatePartFrom(USPARTTO);
        }
        private async Task DoUpdateUnitPartTo()
        {
            await DoUpdateUnitTo(USUNITFROM);
            await DoUpdatePartTo(USPARTFROM);
        }
        private async Task DoUpdateSingleUnit()
        {
            await DoUpdateUnitTo(USUNITFROM);
            await DoUpdatePartFrom(1);
            await DoUpdatePartTo(PartCount);
        }
        private async Task DoUpdateUnitFrom(int v)
        {
            if (USUNITFROM == v) return;
            await UserSettingDS.Update(INFO_USUNITFROM, USUNITFROM = v);
            OnUpdateUnitFrom?.Invoke(this, null);
        }
        private async Task DoUpdatePartFrom(int v)
        {
            if (USPARTFROM == v) return;
            await UserSettingDS.Update(INFO_USPARTFROM, USPARTFROM = v);
            OnUpdatePartFrom?.Invoke(this, null);
        }
        private async Task DoUpdateUnitTo(int v)
        {
            if (USUNITTO == v) return;
            await UserSettingDS.Update(INFO_USUNITTO, USUNITTO = v);
            OnUpdateUnitTo?.Invoke(this, null);
        }
        private async Task DoUpdatePartTo(int v)
        {
            if (USPARTTO == v) return;
            await UserSettingDS.Update(INFO_USPARTTO, USPARTTO = v);
            OnUpdatePartTo?.Invoke(this, null);
        }
        public async Task UpdateLevel(int wordid, int level) => await WordFamiDS.Update(wordid, level);
    }
}
