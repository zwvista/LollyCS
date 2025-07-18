using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace LollyCommon
{
    public class LangBlogPostsContentViewModel : ReactiveObject
    {
        [Reactive]
        public LangBlogGroupsViewModel vmGroups { get; set; }
        public List<MLangBlogPost> PostItems { get; }
        [Reactive]
        public int SelectedPostIndex { get; set; }

        public LangBlogPostsContentViewModel(LangBlogGroupsViewModel vmGroups, List<MLangBlogPost> postItems, int index)
        {
            this.vmGroups = vmGroups;
            PostItems = postItems;
            SelectedPostIndex = index;
            this.WhenAnyValue(x => x.SelectedPostIndex)
                .Subscribe(v => vmGroups.SelectedPostItem = PostItems[v]);
        }

        public void Next(int delta) =>
            SelectedPostIndex = (SelectedPostIndex + delta + PostItems.Count) % PostItems.Count;
    }
}
