using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LollyCommon
{
    public class LangBlogPostsDetailViewModel : ReactiveObject
    {
        public MLangBlogPostEdit ItemEdit = new();
        public string LANGNAME { get; private set; }
        MLangBlogGroup? ItemGroup { get; }
        LangBlogGPDataStore gpDS = new();

        public LangBlogPostsDetailViewModel(MLangBlogPost item, LangBlogViewModel vm, MLangBlogGroup? itemGroup = null)
        {
            ItemGroup = itemGroup;
            bool isNew = item.ID == 0;
            item.CopyProperties(ItemEdit);
            LANGNAME = vm.vmSettings.SelectedLang.LANGNAME;
            ItemEdit.Save = ReactiveCommand.CreateFromTask(async () =>
            {
                ItemEdit.CopyProperties(item);
                if (isNew)
                {
                    var itemGP = new MLangBlogGP
                    {
                        GROUPID = itemGroup!.ID,
                        POSTID = await vm.CreatePost(item),
                    };
                    await gpDS.Create(itemGP);
                }
                else
                    await vm.UpdatePost(item);
            });
            ItemGroup = itemGroup;
        }
    }
}
