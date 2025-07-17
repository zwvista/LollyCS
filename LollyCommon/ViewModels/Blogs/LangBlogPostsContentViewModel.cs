using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace LollyCommon
{
    public class LangBlogPostsContentViewModel : ReactiveObject
    {
        public List<MLangBlogPostContent> LangBlogPostContents { get; }
        [Reactive]
        public int SelectedLangBlogPostContentIndex { get; set; }

        public LangBlogPostsContentViewModel(List<MLangBlogPostContent> langBlogPostContents, int index)
        {
            LangBlogPostContents = langBlogPostContents;
            SelectedLangBlogPostContentIndex = index;
        }

        public void Next(int delta) =>
            SelectedLangBlogPostContentIndex = (SelectedLangBlogPostContentIndex + delta + LangBlogPostContents.Count) % LangBlogPostContents.Count;
    }
}
