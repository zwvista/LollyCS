﻿using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class SettingsViewModel : ReactiveObject
    {
        USMappingDataStore USMappingDS = new();
        UserSettingDataStore UserSettingDS = new();
        LanguageDataStore LanguageDS = new();
        DictionaryDataStore DictionaryDS = new();
        TextbookDataStore TextbookDS = new();
        AutoCorrectDataStore AutoCorrectDS = new();
        VoiceDataStore VoiceDS = new();
        CodeDataStore CodeDS = new();
        UnitBlogPostDataStore unitBlogDS = new();

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
        public List<MLanguage> LanguagesAll { get; set; }
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
        public List<MSelectItem> OnlineTextbookFilters { get; set; } = new List<MSelectItem>();

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
        public MSelectItem USUNITFROMItem
        {
            get => Units.SingleOrDefault(o => o.Value == USUNITFROM);
            set { if (value != null) USUNITFROM = value.Value; }
        }
        public MSelectItem USPARTFROMItem
        {
            get => Parts.SingleOrDefault(o => o.Value == USPARTFROM);
            set { if (value != null) USPARTFROM = value.Value; }
        }
        public MSelectItem USUNITTOItem
        {
            get => Units.SingleOrDefault(o => o.Value == USUNITTO);
            set { if (value != null) USUNITTO = value.Value; }
        }
        public MSelectItem USPARTTOItem
        {
            get => Parts.SingleOrDefault(o => o.Value == USPARTTO);
            set { if (value != null) USPARTTO = value.Value; }
        }

        public static List<MSelectItem> ToTypes { get; } = new List<MSelectItem>
        {
            new MSelectItem(0, "Unit"),
            new MSelectItem(1, "Part"),
            new MSelectItem(2, "To"),
        };
        [Reactive]
        public UnitPartToType ToType { get; set; } = UnitPartToType.To;
        public MSelectItem ToTypeItem
        {
            get => ToTypes.SingleOrDefault(o => o.Value == (int)ToType);
            set { if (value != null) ToType = (UnitPartToType)Enum.ToObject(typeof(UnitPartToType), value.Value); }
        }
        public bool ToTypeMovable => ToType != UnitPartToType.To;
        [Reactive]
        public bool UnitToEnabled { get; set; }
        [Reactive]
        public bool PartToEnabled { get; set; }
        [Reactive]
        public bool PreviousEnabled { get; set; }
        [Reactive]
        public bool NextEnabled { get; set; }
        [Reactive]
        public string PreviousText { get; set; }
        [Reactive]
        public string NextText { get; set; }
        [Reactive]
        public bool PartFromEnabled { get; set; }

        public List<MAutoCorrect> AutoCorrects { get; set; }
        public List<MCode> DictTypeCodes { get; set; }
        public List<MCode> ReadNumberCodes { get; set; }
        public static List<string> ScopeWordFilters { get; } = new List<string> { "Word", "Note" };
        public static List<string> ScopePhraseFilters { get; } = new List<string> { "Phrase", "Translation" };
        public static List<string> ScopePatternFilters { get; } = new List<string> { "Pattern", "Tags" };
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
                var newVal = v.ID;
                var dirty = USLANG != newVal;
                USLANG = newVal;
                INFO_USTEXTBOOK = GetUSInfo(MUSMapping.NAME_USTEXTBOOK);
                INFO_USDICTREFERENCE = GetUSInfo(MUSMapping.NAME_USDICTREFERENCE);
                INFO_USDICTNOTE = GetUSInfo(MUSMapping.NAME_USDICTNOTE);
                INFO_USDICTSREFERENCE = GetUSInfo(MUSMapping.NAME_USDICTSREFERENCE);
                INFO_USDICTTRANSLATION = GetUSInfo(MUSMapping.NAME_USDICTTRANSLATION);
                INFO_USVOICE = GetUSInfo(MUSMapping.NAME_USWINDOWSVOICE);
                var res1 = DictionaryDS.GetDictsReferenceByLang(USLANG);
                var res2 = DictionaryDS.GetDictsNoteByLang(USLANG);
                var res3 = DictionaryDS.GetDictsTranslationByLang(USLANG);
                var res4 = TextbookDS.GetDataByLang(USLANG);
                var res5 = AutoCorrectDS.GetDataByLang(USLANG);
                var res6 = VoiceDS.GetDataByLang(USLANG).ToObservable();
                DictsReference = await res1;
                DictsNote = await res2;
                DictsTranslation = await res3;
                Textbooks = await res4;
                AutoCorrects = await res5;
                Voices = await res6;
                SelectedDictReference = DictsReference.FirstOrDefault(o => o.DICTID.ToString() == USDICTREFERENCE);
                SelectedDictNote = DictsNote.FirstOrDefault(o => o.DICTID == USDICTNOTE) ?? DictsNote.FirstOrDefault();
                SelectedDictsReference = USDICTSREFERENCE.Split(',').SelectMany(d => DictsReference.Where(o => o.DICTID.ToString() == d)).ToList();
                SelectedDictTranslation = DictsTranslation.FirstOrDefault(o => o.DICTID == USDICTTRANSLATION) ?? DictsTranslation.FirstOrDefault();
                SelectedTextbook = Textbooks.FirstOrDefault(o => o.ID == USTEXTBOOK);
                TextbookFilters = Textbooks.Select(o => new MSelectItem(o.ID, o.TEXTBOOKNAME))
                    .StartWith(new MSelectItem(0, "All Textbooks")).ToList();
                OnlineTextbookFilters = Textbooks.Where(o => o.ONLINE).Select(o => new MSelectItem(o.ID, o.TEXTBOOKNAME))
                    .StartWith(new MSelectItem(0, "All Textbooks")).ToList();
                SelectedVoice = Voices.FirstOrDefault(o => o.ID == USDICTTRANSLATION) ?? Voices.FirstOrDefault();
                if (dirty)
                    await UserSettingDS.Update(INFO_USLANG, USLANG);
            });
            this.WhenAnyValue(x => x.SelectedVoice).Where(v => v != null).Subscribe(async v =>
            {
                var newVal = v.ID;
                var dirty = USVOICE != newVal;
                USVOICE = newVal;
                if (dirty)
                    await UserSettingDS.Update(INFO_USVOICE, USVOICE);
            });
            this.WhenAnyValue(x => x.SelectedDictReference).Where(v => v != null).Subscribe(async v =>
            {
                var newVal = v.DICTID.ToString();
                var dirty = USDICTREFERENCE != newVal;
                USDICTREFERENCE = newVal;
                if (dirty)
                    await UserSettingDS.Update(INFO_USDICTREFERENCE, USDICTREFERENCE);
            });
            this.WhenAnyValue(x => x.SelectedDictsReference).Where(v => v != null)
                .Subscribe(v => USDICTSREFERENCE = string.Join(",", v.Select(v2 => v2.DICTID.ToString())));
            this.WhenAnyValue(x => x.SelectedDictNote).Where(v => v != null).Subscribe(async v =>
            {
                var newVal = v.DICTID;
                var dirty = USDICTNOTE != newVal;
                USDICTNOTE = newVal;
                if (dirty)
                    await UserSettingDS.Update(INFO_USDICTNOTE, USDICTNOTE);
            });
            this.WhenAnyValue(x => x.SelectedDictTranslation).Where(v => v != null).Subscribe(async v =>
            {
                var newVal = v.DICTID;
                var dirty = USDICTTRANSLATION != newVal;
                USDICTTRANSLATION = newVal;
                if (dirty)
                    await UserSettingDS.Update(INFO_USDICTTRANSLATION, USDICTTRANSLATION);
            });
            this.WhenAnyValue(x => x.SelectedTextbook).Where(v => v != null).Subscribe(async v =>
            {
                var newVal = v.ID;
                var dirty = USTEXTBOOK != newVal;
                USTEXTBOOK = newVal;
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
                this.RaisePropertyChanged(nameof(USUNITFROMItem));
                this.RaisePropertyChanged(nameof(USPARTFROMItem));
                this.RaisePropertyChanged(nameof(USUNITTOItem));
                this.RaisePropertyChanged(nameof(USPARTTOItem));
                this.RaisePropertyChanged(nameof(ToTypeItem));
                if (dirty)
                    await UserSettingDS.Update(INFO_USTEXTBOOK, USTEXTBOOK);
            });
            this.WhenAnyValue(x => x.ToType).Where(_ => Units != null).Subscribe(async v =>
            {
                var b = v == UnitPartToType.To;
                UnitToEnabled = b;
                PartToEnabled = b && !IsSinglePart;
                PreviousEnabled = !b;
                NextEnabled = !b;
                var b2 = v != UnitPartToType.Unit;
                var t = !b2 ? "Unit" : "Part";
                PreviousText = "Previous " + t;
                NextText = "Next " + t;
                PartFromEnabled = b2 && !IsSinglePart;
                if (v == UnitPartToType.Unit)
                    await DoUpdateSingleUnit();
                else if (v == UnitPartToType.Part)
                    await DoUpdateUnitPartTo();
            });
            this.WhenAnyValue(x => x.USUNITFROM).Skip(1).Subscribe(async v =>
            {
                await DoUpdateUnitFrom(v);
                if (ToType == UnitPartToType.Unit)
                    await DoUpdateSingleUnit();
                else if (ToType == UnitPartToType.Part || IsInvalidUnitPart)
                    await DoUpdateUnitPartTo();
            });
            this.WhenAnyValue(x => x.USPARTFROM).Skip(1).Subscribe(async v =>
            {
                await DoUpdatePartFrom(v);
                if (ToType == UnitPartToType.Part || IsInvalidUnitPart)
                    await DoUpdateUnitPartTo();
            });
            this.WhenAnyValue(x => x.USUNITTO).Skip(1).Subscribe(async v =>
            {
                await DoUpdateUnitTo(v);
                if (IsInvalidUnitPart)
                    await DoUpdateUnitPartFrom();
            });
            this.WhenAnyValue(x => x.USPARTTO).Skip(1).Subscribe(async v =>
            {
                await DoUpdatePartTo(v);
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
            var res1 = LanguageDS.GetData();
            var res2 = USMappingDS.GetData();
            var res3 = UserSettingDS.GetDataByUser();
            var res4 = CodeDS.GetDictCodes();
            LanguagesAll = await res1;
            Languages = LanguagesAll.Where(o => o.ID != 0).ToList();
            USMappings = await res2;
            UserSettings = await res3;
            DictTypeCodes = await res4;
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
        async Task DoUpdateUnitFrom(int v)
        {
            if (USUNITFROM == v) return;
            await UserSettingDS.Update(INFO_USUNITFROM, USUNITFROM = v);
        }
        async Task DoUpdatePartFrom(int v)
        {
            if (USPARTFROM == v) return;
            await UserSettingDS.Update(INFO_USPARTFROM, USPARTFROM = v);
        }
        async Task DoUpdateUnitTo(int v)
        {
            if (USUNITTO == v) return;
            await UserSettingDS.Update(INFO_USUNITTO, USUNITTO = v);
        }
        async Task DoUpdatePartTo(int v)
        {
            if (USPARTTO == v) return;
            await UserSettingDS.Update(INFO_USPARTTO, USPARTTO = v);
        }
        public async Task UpdateReadNumberId() => await UserSettingDS.Update(INFO_USREADNUMBER, USREADNUMBER);

        public static readonly string ZeroNote = "O";
        public async Task<string> GetNote(string word)
        {
            if (SelectedDictNote == null) return "";
            var url = SelectedDictNote.UrlString(word, AutoCorrects);
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
        public async Task<string> GetBlogContent(int unit)
        {
            var item = await unitBlogDS.GetDataByTextbook(SelectedTextbook.ID, unit);
            return item.CONTENT ?? "";
        }
        public async Task<string> GetBlogContent() =>
            await GetBlogContent(USUNITTOItem.Value);
        public async Task SaveBlogContent(string content) =>
            await unitBlogDS.Update(SelectedTextbook.ID, USUNITTOItem.Value, content);
    }
}
