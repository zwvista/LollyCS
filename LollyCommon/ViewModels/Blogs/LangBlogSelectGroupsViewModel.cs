using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Threading.Tasks;

namespace LollyCommon
{
    public class LangBlogSelectGroupsViewModel : ReactiveObject
    {
        SettingsViewModel vmSettings;
        LangBlogGroupDataStore groupDS = new LangBlogGroupDataStore();
        LangBlogGPDataStore gpDS = new LangBlogGPDataStore();
        public MLangBlogPost Item { get; }
        [Reactive]
        public ObservableCollection<MLangBlogGroup> GroupsAvailable { get; set; }
        [Reactive]
        public ObservableCollection<MLangBlogGroup> GroupsSelected { get; set; }
        List<MLangBlogGroup> GroupsSelectedOriginal;
        public ReactiveCommand<Unit, Unit> Save { get; }
        public LangBlogSelectGroupsViewModel(SettingsViewModel vmSettings, MLangBlogPost item)
        {
            this.vmSettings = vmSettings;
            Item = item;
            groupDS.GetDataByLang(vmSettings.SelectedLang.ID).ToObservable().Subscribe(lst =>
            {
                GroupsAvailable = new ObservableCollection<MLangBlogGroup>(lst);
                AdjustGroupsAvailable();
            });
            groupDS.GetDataByLangPost(vmSettings.SelectedLang.ID, item.ID).ToObservable().Subscribe(lst =>
            {
                GroupsSelected = new ObservableCollection<MLangBlogGroup>(GroupsSelectedOriginal = lst);
                AdjustGroupsAvailable();
            });
            Save = ReactiveCommand.CreateFromTask(async () =>
            {
                var lstRemove = GroupsSelectedOriginal.Where(o => !GroupsSelected.Any(o2 => o.ID == o2.ID)).ToList();
                var lstAdd = GroupsSelected.Where(o => !GroupsSelectedOriginal.Any(o2 => o.ID == o2.ID)).ToList();
                foreach (var o in lstRemove)
                    await gpDS.Delete(o.GPID);
                foreach (var o in lstAdd)
                    await gpDS.Create(new MLangBlogGP
                    {
                        POSTID = Item.ID,
                        GROUPID = o.ID,
                    });
            });
        }
        void AdjustGroupsAvailable()
        {
            if (GroupsAvailable == null || GroupsSelected == null) return;
            GroupsAvailable = new ObservableCollection<MLangBlogGroup>(
                GroupsAvailable.Where(o => !GroupsSelected.Any(o2 => o.ID == o2.ID)));
        }
    }
}
