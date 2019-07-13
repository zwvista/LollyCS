﻿using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;

namespace LollyShared
{
    public class PhrasesLangViewModel : LollyViewModel
    {
        public SettingsViewModel vmSettings;
        LangPhraseDataStore langPhraseDS = new LangPhraseDataStore();

        public ObservableCollection<MLangPhrase> Items { get; set; }

        public static async Task<PhrasesLangViewModel> CreateAsync(SettingsViewModel vmSettings, bool needCopy)
        {
            var o = new PhrasesLangViewModel();
            o.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            o.Items = new ObservableCollection<MLangPhrase>(await o.langPhraseDS.GetDataByLang(vmSettings.SelectedTextbook.LANGID));
            return o;
        }

        public async Task Update(MLangPhrase item) => await langPhraseDS.Update(item);
        public async Task<int> Create(MLangPhrase item) => await langPhraseDS.Create(item);
        public async Task Delete(int id) => await langPhraseDS.Delete(id);

        public MLangPhrase NewLangPhrase() =>
            new MLangPhrase
            {
                LANGID = vmSettings.SelectedLang.ID,
            };
    }
}
