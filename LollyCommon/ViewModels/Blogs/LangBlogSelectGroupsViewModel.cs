using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace LollyCommon
{
    public class LangBlogSelectGroupsViewModel : ReactiveObject
    {
        LangBlogGroupDataStore groupDS = new();
        LangBlogGPDataStore gpDS = new();
        public MLangBlogPost Item { get; }
        [Reactive]
        public ObservableCollection<MLangBlogGroup> GroupsAvailable { get; set; }
        [Reactive]
        public ObservableCollection<MLangBlogGroup> GroupsSelected { get; set; }
        List<MLangBlogGroup> GroupsSelectedOriginal;
        public ReactiveCommand<Unit, Unit> Save { get; }
        public LangBlogSelectGroupsViewModel(MLangBlogPost item)
        {
            Item = item;
            groupDS.GetDataByLang(item.LANGID).ToObservable()
                .Zip(groupDS.GetDataByLangPost(item.LANGID, item.ID).ToObservable())
                .Subscribe(result =>
            {
                var (lst1, lst2) = result;
                GroupsSelected = new ObservableCollection<MLangBlogGroup>(GroupsSelectedOriginal = lst2);
                GroupsAvailable = new ObservableCollection<MLangBlogGroup>(
                    lst1.Where(o => !lst2.Any(o2 => o.ID == o2.ID)));
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
    }
}
