using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class PatternsWebPagesViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        public MPattern SelectedPatternItem { get; set; }
        PatternWebPageDataStore patternWebPageDS = new PatternWebPageDataStore();
        WebPageDataStore webPageDS = new WebPageDataStore();
        public ObservableCollection<MPatternWebPage> WebPageItems { get; set; }
        public bool IsBusy { get; set; } = true;
        public ReactiveCommand<Unit, Unit> ReloadCommand { get; set; }
        public PatternsWebPagesViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            ReloadCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                IsBusy = true;
                WebPageItems = new ObservableCollection<MPatternWebPage>(SelectedPatternItem == null ? new List<MPatternWebPage>() : await patternWebPageDS.GetDataByPattern(SelectedPatternItem.ID));
                this.RaisePropertyChanged(nameof(WebPageItems));
                IsBusy = false;
            });
            GetWebPages();
        }
        public void GetWebPages() => ReloadCommand.Execute().Subscribe();
        public async Task UpdatePatternWebPage(MPatternWebPage item) =>
            await patternWebPageDS.Update(item);
        public async Task CreatePatternWebPage(MPatternWebPage item)
        {
            item.ID = await patternWebPageDS.Create(item);
            WebPageItems.Add(item);
        }
        public async Task DeletePatternWebPage(int id) =>
            await patternWebPageDS.Delete(id);
        public async Task UpdateWebPage(MPatternWebPage item) =>
            await webPageDS.Update(item);
        public async Task CreateWebPage(MPatternWebPage item) =>
            item.WEBPAGEID = await webPageDS.Create(item);
        public async Task DeleteWebPage(int id) =>
            await webPageDS.Delete(id);
        public MPatternWebPage NewPatternWebPage() =>
            new MPatternWebPage
            {
                PATTERNID = SelectedPatternItem.ID,
                PATTERN = SelectedPatternItem.PATTERN,
                SEQNUM = WebPageItems.Select(o => o.SEQNUM).StartWith(0).Max() + 1
            };

        public async Task Reindex(Action<int> complete)
        {
            for (int i = 1; i <= WebPageItems.Count; i++)
            {
                var item = WebPageItems[i - 1];
                if (item.SEQNUM == i) continue;
                item.SEQNUM = i;
                await patternWebPageDS.UpdateSeqNum(item.ID, item.SEQNUM);
                complete(i - 1);
            }
        }
    }
}
