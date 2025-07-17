using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace LollyCommon
{
    public class LangBlogPostsContentViewModel : ReactiveObject
    {
        public List<MLangBlogPost> LangBlogPostContents { get; }
        [Reactive]
        public int SelectedLangBlogPostIndex { get; set; }

        public LangBlogPostsContentViewModel(List<MLangBlogPost> langBlogPosts, int index)
        {
            LangBlogPostContents = langBlogPosts;
            SelectedLangBlogPostIndex = index;
        }

        public void Next(int delta) =>
            SelectedLangBlogPostIndex = (SelectedLangBlogPostIndex + delta + LangBlogPostContents.Count) % LangBlogPostContents.Count;
    }
}
