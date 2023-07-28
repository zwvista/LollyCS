using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LollyCommon
{
    public class LangBlogPostsDetailViewModel : ReactiveObject
    {
        MLangBlogPost item;
        LangBlogViewModel vm;
        public MLangBlogPostEdit ItemEdit = new MLangBlogPostEdit();
        public string LANGNAME { get; private set; }

        public LangBlogPostsDetailViewModel(MLangBlogPost item, LangBlogViewModel vm)
        {
            bool isNew = item.ID == 0;
            this.item = item;
            this.vm = vm;
            item.CopyProperties(ItemEdit);
            LANGNAME = vm.vmSettings.SelectedLang.LANGNAME;
            ItemEdit.Save = ReactiveCommand.CreateFromTask(async () =>
            {
                ItemEdit.CopyProperties(item);
                if (isNew)
                    await vm.CreatePost(item);
                else
                    await vm.UpdatePost(item);
            });
        }
    }
}
