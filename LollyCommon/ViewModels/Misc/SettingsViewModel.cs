using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Reflection;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class SettingsViewModel : ReactiveObject
    {
        USMappingDataStore USMappingDS = new USMappingDataStore();
        UserSettingDataStore UserSettingDS = new UserSettingDataStore();
        LanguageDataStore LanguageDS = new LanguageDataStore();
        DictionaryDataStore DictionaryDS = new DictionaryDataStore();
        TextbookDataStore TextbookDS = new TextbookDataStore();
        AutoCorrectDataStore AutoCorrectDS = new AutoCorrectDataStore();
        VoiceDataStore VoiceDS = new VoiceDataStore();
        CodeDataStore CodeDS = new CodeDataStore();

        public SettingsViewModel ShallowCopy() => (SettingsViewModel)this.MemberwiseClone();

        public List<MUSMapping> USMappings { get; set; }
        public List<MUserSetting> UserSettings { get; set; }
        string GetUSValue(MUserSettingInfo info)
        {
            var o = UserSettings?.FirstOrDefault(v => v.ID == info.USERSETTINGID);
            switch (info.VALUEID)
            {
                case 1: return o?.VALUE1;
                case 2: return o?.VALUE2;
                case 3: return o?.VALUE3;
                case 4: return o?.VALUE4;
                default: return null;
            }
        }
        void SetUSValue(MUserSettingInfo info, string value, string name)
        {
            var o = UserSettings?.FirstOrDefault(v => v.ID == info.USERSETTINGID);
            if (o == null) return;
            switch (info.VALUEID)
            {
                case 1: o.VALUE1 = value; break;
                case 2: o.VALUE2 = value; break;
                case 3: o.VALUE3 = value; break;
                case 4: o.VALUE4 = value; break;
            }
            this.RaisePropertyChanged(name);
        }
        MUserSettingInfo INFO_USLANG = new MUserSettingInfo();
        public int USLANG
        {
            get => int.Parse(GetUSValue(INFO_USLANG));
            set => SetUSValue(INFO_USLANG, value.ToString(), nameof(USLANG));
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
        MUserSettingInfo INFO_USREADNUMBER = new MUserSettingInfo();
        public int USREADNUMBER
        {
            get => int.Parse(GetUSValue(INFO_USREADNUMBER));
            set => SetUSValue(INFO_USREADNUMBER, value.ToString(), nameof(USREADNUMBER));
        }
        MUserSettingInfo INFO_USTEXTBOOK = new MUserSettingInfo();
        public int USTEXTBOOK
        {
            get => int.Parse(GetUSValue(INFO_USTEXTBOOK));
            set => SetUSValue(INFO_USTEXTBOOK, value.ToString(), nameof(USTEXTBOOK));
        }
        MUserSettingInfo INFO_USDICTREFERENCE = new MUserSettingInfo();
        public string USDICTREFERENCE
        {
            get => GetUSValue(INFO_USDICTREFERENCE);
            set => SetUSValue(INFO_USDICTREFERENCE, value, nameof(USDICTREFERENCE));
        }
        MUserSettingInfo INFO_USDICTNOTE = new MUserSettingInfo();
        public int USDICTNOTE
        {
            get => int.TryParse(GetUSValue(INFO_USDICTNOTE), out var v) ? v : 0;
            set => SetUSValue(INFO_USDICTNOTE, value.ToString(), nameof(USDICTNOTE));
        }
        MUserSettingInfo INFO_USDICTSREFERENCE = new MUserSettingInfo();
        public string USDICTSREFERENCE
        {
            get => GetUSValue(INFO_USDICTSREFERENCE) ?? "";
            set => SetUSValue(INFO_USDICTSREFERENCE, value, nameof(USDICTSREFERENCE));
        }
        MUserSettingInfo INFO_USDICTTRANSLATION = new MUserSettingInfo();
        public int USDICTTRANSLATION
        {
            get => int.TryParse(GetUSValue(INFO_USDICTTRANSLATION), out var v) ? v : 0;
            set => SetUSValue(INFO_USDICTTRANSLATION, value.ToString(), nameof(USDICTTRANSLATION));
        }
        MUserSettingInfo INFO_USVOICE = new MUserSettingInfo();
        public int USVOICE
        {
            get => int.TryParse(GetUSValue(INFO_USVOICE), out var v) ? v : 0;
            set => SetUSValue(INFO_USVOICE, value.ToString(), nameof(USVOICE));
        }
        MUserSettingInfo INFO_USUNITFROM = new MUserSettingInfo();
        public int USUNITFROM
        {
            get => int.TryParse(GetUSValue(INFO_USUNITFROM), out var v) ? v : 0;
            set => SetUSValue(INFO_USUNITFROM, value.ToString(), nameof(USUNITFROM));
        }
        public string USUNITFROMSTR => SelectedTextbook.UNITSTR(USUNITFROM);
        MUserSettingInfo INFO_USPARTFROM = new MUserSettingInfo();
        public int USPARTFROM
        {
            get => int.TryParse(GetUSValue(INFO_USPARTFROM), out var v) ? v : 0;
            set => SetUSValue(INFO_USPARTFROM, value.ToString(), nameof(USPARTFROM));
        }
        public string USPARTFROMSTR => SelectedTextbook.PARTSTR(USPARTFROM);
        MUserSettingInfo INFO_USUNITTO = new MUserSettingInfo();
        public int USUNITTO
        {
            get => int.TryParse(GetUSValue(INFO_USUNITTO), out var v) ? v : 0;
            set => SetUSValue(INFO_USUNITTO, value.ToString(), nameof(USUNITTO));
        }
        public string USUNITTOSTR => SelectedTextbook.UNITSTR(USUNITTO);
        MUserSettingInfo INFO_USPARTTO = new MUserSettingInfo();
        public int USPARTTO
        {
            get => int.TryParse(GetUSValue(INFO_USPARTTO), out var v) ? v : 0;
            set => SetUSValue(INFO_USPARTTO, value.ToString(), nameof(USPARTTO));
        }
        public string USPARTTOSTR => SelectedTextbook.PARTSTR(USPARTTO);
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

        [Reactive]
        public List<MDictionary> DictsReference { get; set; }
        [Reactive]
        public MDictionary SelectedDictReference { get; set; }
        [Reactive]
        public List<MDictionary> SelectedDictsReference { get; set; }

        [Reactive]
        public List<MDictionary> DictsNote { get; set; }
        [Reactive]
        public MDictionary SelectedDictNote { get; set; }

        [Reactive]
        public List<MDictionary> DictsTranslation { get; set; }
        [Reactive]
        public MDictionary SelectedDictTranslation { get; set; }
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
        public string LANGINFO => SelectedLang.LANGNAME;
        public string TEXTBOOKINFO => $"{LANGINFO}/{SelectedTextbook.TEXTBOOKNAME}";
        public string UNITINFO => $"{TEXTBOOKINFO}/{USUNITFROMSTR} {USPARTFROMSTR} ~ {USUNITTOSTR} {USPARTTOSTR}";

        public static List<MSelectItem> ToTypes { get; set; } = new List<MSelectItem>
        {
            new MSelectItem(0, "Unit"),
            new MSelectItem(1, "Part"),
            new MSelectItem(2, "To"),
        };
        [Reactive]
        public UnitPartToType ToType { get; set; } = UnitPartToType.To;
        public bool ToTypeIsMovable => ToType != UnitPartToType.To;
        [Reactive]
        public bool UnitToIsEnabled { get; set; }
        [Reactive]
        public bool PartToIsEnabled { get; set; }
        [Reactive]
        public bool PreviousIsEnabled { get; set; }
        [Reactive]
        public bool NextIsEnabled { get; set; }
        [Reactive]
        public string PreviousText { get; set; }
        [Reactive]
        public string NextText { get; set; }
        [Reactive]
        public bool PartFromIsEnabled { get; set; }

        public List<MAutoCorrect> AutoCorrects { get; set; }
        public List<MCode> DictCodes { get; set; }
        public List<MCode> ReadNumberCodes { get; set; }
        public static List<string> ScopeWordFilters { get; } = new List<string> { "Word", "Note" };
        public static List<string> ScopePhraseFilters { get; } = new List<string> { "Phrase", "Translation" };
        public static List<string> ScopePatternFilters { get; } = new List<string> { "Pattern", "Note", "Tags" };
        public static List<MSelectItem> ReviewModes { get; set; } = new List<MSelectItem>
        {
            new MSelectItem(0, "Review(Auto)"),
            new MSelectItem(1, "Review(Manual)"),
            new MSelectItem(2, "Test"),
            new MSelectItem(3, "Textbook"),
        };

        public HttpClient client = new HttpClient();

        public SettingsViewModel()
        {
            // https://stackoverflow.com/questions/35685063/httpclient-getasync-method-403-error
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "http://developer.github.com/v3/#user-agent-required");

            this.WhenAnyValue(x => x.SelectedLang).Where(v => v != null).Subscribe(async v =>
            {
                var isinit = USLANG == v.ID;
                USLANG = v.ID;
                INFO_USTEXTBOOK = GetUSInfo(MUSMapping.NAME_USTEXTBOOK);
                INFO_USDICTREFERENCE = GetUSInfo(MUSMapping.NAME_USDICTREFERENCE);
                INFO_USDICTNOTE = GetUSInfo(MUSMapping.NAME_USDICTNOTE);
                INFO_USDICTSREFERENCE = GetUSInfo(MUSMapping.NAME_USDICTSREFERENCE);
                INFO_USDICTTRANSLATION = GetUSInfo(MUSMapping.NAME_USDICTTRANSLATION);
                INFO_USVOICE = GetUSInfo(MUSMapping.NAME_USWINDOWSVOICE);
                var t = await DictionaryDS.GetDictsReferenceByLang(USLANG).ToObservable().Zip(DictionaryDS.GetDictsNoteByLang(USLANG).ToObservable(),
                    DictionaryDS.GetDictsTranslationByLang(USLANG).ToObservable(), TextbookDS.GetDataByLang(USLANG).ToObservable(),
                    AutoCorrectDS.GetDataByLang(USLANG).ToObservable(), VoiceDS.GetDataByLang(USLANG).ToObservable(), (a, b, c, d, e, f) => (a, b, c, d, e, f)).ToTask();
                DictsReference = t.a;
                DictsNote = t.b;
                DictsTranslation = t.c;
                Textbooks = t.d;
                AutoCorrects = t.e;
                Voices = t.f;
                SelectedDictReference = DictsReference.FirstOrDefault(o => o.DICTID.ToString() == USDICTREFERENCE);
                SelectedDictNote = DictsNote.FirstOrDefault(o => o.DICTID == USDICTNOTE) ?? DictsNote.FirstOrDefault();
                SelectedDictsReference = USDICTSREFERENCE.Split(',').SelectMany(d => DictsReference.Where(o => o.DICTID.ToString() == d)).ToList();
                SelectedDictTranslation = DictsTranslation.FirstOrDefault(o => o.DICTID == USDICTTRANSLATION) ?? DictsTranslation.FirstOrDefault();
                SelectedTextbook = Textbooks.FirstOrDefault(o => o.ID == USTEXTBOOK);
                TextbookFilters = Textbooks.Select(o => new MSelectItem(o.ID, o.TEXTBOOKNAME))
                    .StartWith(new MSelectItem(0, "All Textbooks")).ToList();
                SelectedVoice = Voices.FirstOrDefault(o => o.ID == USDICTTRANSLATION) ?? Voices.FirstOrDefault();
                if (!isinit)
                    await UserSettingDS.Update(INFO_USLANG, USLANG);
            });
            this.WhenAnyValue(x => x.SelectedVoice).Skip(1).Subscribe(async v =>
            {
                USVOICE = v?.ID ?? 0;
                await UserSettingDS.Update(INFO_USVOICE, USVOICE);
            });
            this.WhenAnyValue(x => x.SelectedDictReference).Where(v => v != null).Subscribe(async v =>
            {
                USDICTREFERENCE = v.DICTID.ToString();
                await UserSettingDS.Update(INFO_USDICTREFERENCE, USDICTREFERENCE);
            });
            this.WhenAnyValue(x => x.SelectedDictsReference).Where(v => v != null)
                .Subscribe(v => USDICTSREFERENCE = string.Join(",", v.Select(v2 => v2.DICTID.ToString())));
            this.WhenAnyValue(x => x.SelectedDictNote).Where(v => v != null).Subscribe(async v =>
            {
                USDICTNOTE = v?.DICTID ?? 0;
                await UserSettingDS.Update(INFO_USDICTNOTE, USDICTNOTE);
            });
            this.WhenAnyValue(x => x.SelectedDictTranslation).Where(v => v != null).Subscribe(async v =>
            {
                USDICTTRANSLATION = v?.DICTID ?? 0;
                await UserSettingDS.Update(INFO_USDICTTRANSLATION, USDICTTRANSLATION);
            });
            this.WhenAnyValue(x => x.SelectedTextbook).Where(v => v != null).Subscribe(async v =>
            {
                USTEXTBOOK = v.ID;
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
                await UserSettingDS.Update(INFO_USTEXTBOOK, USTEXTBOOK);
            });
            this.WhenAnyValue(x => x.ToType).Where(_ => Units != null).Subscribe(async v =>
            {
                var b = v == UnitPartToType.To;
                UnitToIsEnabled = b;
                PartToIsEnabled = b && !IsSinglePart;
                PreviousIsEnabled = !b;
                NextIsEnabled = !b;
                var b2 = v != UnitPartToType.Unit;
                var t = !b2 ? "Unit" : "Part";
                PreviousText = "Previous " + t;
                NextText = "Next " + t;
                PartFromIsEnabled = b2 && !IsSinglePart;
                if (v == UnitPartToType.Unit)
                    await DoUpdateSingleUnit();
                else if (ToType == UnitPartToType.Part)
                    await DoUpdateUnitPartTo();
            });
            this.WhenAnyValue(x => x.USUNITFROM).Skip(1).Subscribe(async v =>
            {
                await DoUpdateUnitFrom(v, check: false);
                if (ToType == UnitPartToType.Unit)
                    await DoUpdateSingleUnit();
                else if (ToType == UnitPartToType.Part || IsInvalidUnitPart)
                    await DoUpdateUnitPartTo();
            });
            this.WhenAnyValue(x => x.USPARTFROM).Skip(1).Subscribe(async v =>
            {
                await DoUpdatePartFrom(v, check: false);
                if (ToType == UnitPartToType.Part || IsInvalidUnitPart)
                    await DoUpdateUnitPartTo();
            });
            this.WhenAnyValue(x => x.USUNITTO).Skip(1).Subscribe(async v =>
            {
                await DoUpdateUnitTo(v, check: false);
                if (IsInvalidUnitPart)
                    await DoUpdateUnitPartFrom();
            });
            this.WhenAnyValue(x => x.USPARTTO).Skip(1).Subscribe(async v =>
            {
                await DoUpdatePartTo(v, check: false);
                if (IsInvalidUnitPart)
                    await DoUpdateUnitPartFrom();
            });
        }

        MUserSettingInfo GetUSInfo(string name)
        {
            var o = USMappings.First(v => v.NAME == name);
            var entityid = o.ENTITYID != -1 ? o.ENTITYID :
                o.LEVEL == 1 ? SelectedLang.ID :
                o.LEVEL == 2 ? SelectedTextbook.ID :
                0;
            var o2 = UserSettings.First(v => v.KIND == o.KIND && v.ENTITYID == entityid);
            return new MUserSettingInfo { USERSETTINGID = o2.ID, VALUEID = o.VALUEID };
        }

        public async Task GetData()
        {
            var t = await LanguageDS.GetData().ToObservable().Zip(USMappingDS.GetData().ToObservable(),
                UserSettingDS.GetDataByUser(CommonApi.UserId).ToObservable(), CodeDS.GetDictCodes().ToObservable(), (a, b, c, d) => (a, b, c, d)).ToTask();
            Languages = t.a;
            USMappings = t.b;
            UserSettings = t.c;
            DictCodes = t.d;
            ReadNumberCodes = await CodeDS.GetReadNumberCodes();
            INFO_USLANG = GetUSInfo(MUSMapping.NAME_USLANG);
            INFO_USROWSPERPAGEOPTIONS = GetUSInfo(MUSMapping.NAME_USROWSPERPAGEOPTIONS);
            INFO_USROWSPERPAGE = GetUSInfo(MUSMapping.NAME_USROWSPERPAGE);
            INFO_USLEVELCOLORS = GetUSInfo(MUSMapping.NAME_USLEVELCOLORS);
            INFO_USSCANINTERVAL = GetUSInfo(MUSMapping.NAME_USSCANINTERVAL);
            INFO_USREVIEWINTERVAL = GetUSInfo(MUSMapping.NAME_USREVIEWINTERVAL);
            INFO_USREADNUMBER = GetUSInfo(MUSMapping.NAME_USREADNUMBER);
            USLEVELCOLORS = GetUSValue(INFO_USLEVELCOLORS).Split(new[] { "\r\n" }, StringSplitOptions.None)
                .Select(s => s.Split(',')).ToDictionary(v => int.Parse(v[0]), v2 => new List<string> { v2[1], v2[2] });
            SelectedLang = Languages.FirstOrDefault(o => o.ID == USLANG);
        }

        public async Task UpdateDictsReference(List<MDictionary> lst)
        {
            SelectedDictsReference = lst;
            this.RaisePropertyChanged(nameof(SelectedDictsReference));
            await UserSettingDS.Update(INFO_USDICTSREFERENCE, USDICTSREFERENCE);
        }
        public string AutoCorrectInput(string text) => AutoCorrectDS.AutoCorrect(text, AutoCorrects, o => o.INPUT, o => o.EXTENDED);
        public async Task ToggleToType(int part)
        {
            if (ToType == UnitPartToType.Unit)
            {
                ToType = UnitPartToType.Part;
                await Task.WhenAll(DoUpdatePartFrom(part), DoUpdateUnitPartTo());
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
                    await Task.WhenAll(DoUpdateUnitFrom(USUNITFROM - 1), DoUpdateUnitTo(USUNITFROM));
            }
            else if (USPARTFROM > 1)
                await Task.WhenAll(DoUpdatePartFrom(USPARTFROM - 1), DoUpdateUnitPartTo());
            else if (USUNITFROM > 1)
                await Task.WhenAll(DoUpdateUnitFrom(USUNITFROM - 1), DoUpdatePartFrom(PartCount), DoUpdateUnitPartTo());
        }
        public async Task NextUnitPart()
        {
            if (ToType == UnitPartToType.Unit)
            {
                if (USUNITFROM < UnitCount)
                    await Task.WhenAll(DoUpdateUnitFrom(USUNITFROM + 1), DoUpdateUnitTo(USUNITFROM));
            }
            else if (USPARTFROM < PartCount)
                await Task.WhenAll(DoUpdatePartFrom(USPARTFROM + 1), DoUpdateUnitPartTo());
            else if (USUNITFROM < UnitCount)
                await Task.WhenAll(DoUpdateUnitFrom(USUNITFROM + 1), DoUpdatePartFrom(1), DoUpdateUnitPartTo());
        }
        async Task DoUpdateUnitPartFrom() =>
            await Task.WhenAll(DoUpdateUnitFrom(USUNITTO), DoUpdatePartFrom(USPARTTO));
        async Task DoUpdateUnitPartTo() =>
            await Task.WhenAll(DoUpdateUnitTo(USUNITFROM), DoUpdatePartTo(USPARTFROM));
        async Task DoUpdateSingleUnit() =>
            await Task.WhenAll(DoUpdateUnitTo(USUNITFROM), DoUpdatePartFrom(1), DoUpdatePartTo(PartCount));
        async Task DoUpdateUnitFrom(int v, bool check = true)
        {
            if (check && USUNITFROM == v) return;
            await UserSettingDS.Update(INFO_USUNITFROM, USUNITFROM = v);
        }
        async Task DoUpdatePartFrom(int v, bool check = true)
        {
            if (check && USPARTFROM == v) return;
            await UserSettingDS.Update(INFO_USPARTFROM, USPARTFROM = v);
        }
        async Task DoUpdateUnitTo(int v, bool check = true)
        {
            if (check && USUNITTO == v) return;
            await UserSettingDS.Update(INFO_USUNITTO, USUNITTO = v);
        }
        async Task DoUpdatePartTo(int v, bool check = true)
        {
            if (check && USPARTTO == v) return;
            await UserSettingDS.Update(INFO_USPARTTO, USPARTTO = v);
        }
        public async Task UpdateReadNumberId() => await UserSettingDS.Update(INFO_USREADNUMBER, USREADNUMBER);

        public static readonly string ZeroNote = "O";
        public async Task<string> GetNote(string word)
        {
            if (SelectedDictNote == null) return "";
            var url = SelectedDictNote.UrlString(word, AutoCorrects.ToList());
            var html = await client.GetStringAsync(url);
            return HtmlTransformService.ExtractTextFromHtml(html, SelectedDictNote.TRANSFORM, "", (text, _) => text);
        }

        public async Task GetNotes(int wordCount, Func<int, bool> isNoteEmpty, Func<int, Task> getOne)
        {
            if (SelectedDictNote == null) return;
            for (int i = 0; ;)
            {
                await Task.Delay((int)SelectedDictNote.WAIT);
                while (i < wordCount && !isNoteEmpty(i)) i++;
                if (i > wordCount)
                    break;
                if (i < wordCount)
                    await getOne(i);
                i++;
            }
        }
        public async Task ClearNotes(int wordCount, Func<int, bool> isNoteEmpty, Func<int, Task> getOne)
        {
            if (SelectedDictNote == null) return;
            for (int i = 0; i < wordCount;)
            {
                while (i < wordCount && !isNoteEmpty(i)) i++;
                if (i < wordCount)
                    await getOne(i);
                i++;
            }
        }
    }
}
