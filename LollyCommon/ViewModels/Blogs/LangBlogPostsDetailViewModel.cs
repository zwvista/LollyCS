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
        LangBlogGPDataStore gpDS = new();

        public LangBlogPostsDetailViewModel(MLangBlogPost itemPost, LangBlogViewModel vm, MLangBlogGroup? itemGroup = null)
        {
            bool isNew = itemPost.ID == 0;
            itemPost.CopyProperties(ItemEdit);
            LANGNAME = vm.vmSettings.SelectedLang.LANGNAME;
            ItemEdit.Save = ReactiveCommand.CreateFromTask(async () =>
            {
                ItemEdit.CopyProperties(itemPost);
                if (isNew)
                {
                    var itemGP = new MLangBlogGP
                    {
                        GROUPID = itemGroup!.ID,
                        POSTID = await vm.CreatePost(itemPost),
                    };
                    await gpDS.Create(itemGP);
                }
                else
                    await vm.UpdatePost(itemPost);
            });
        }
    }
}
