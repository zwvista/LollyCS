using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LollyCommon
{
    public class LangBlogsDetailViewModel : ReactiveObject
    {
        MLangBlog item;
        LangBlogsViewModel vm;
        public MLangBlogEdit ItemEdit = new MLangBlogEdit();
        public string LANGNAME { get; private set; }

        public LangBlogsDetailViewModel(MLangBlog item, LangBlogsViewModel vm)
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
                    await vm.CreateBlog(item);
                else
                    await vm.UpdateBlog(item);
            });
        }
    }
}
