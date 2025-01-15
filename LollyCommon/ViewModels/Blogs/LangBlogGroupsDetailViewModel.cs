using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LollyCommon
{
    public class LangBlogGroupsDetailViewModel : ReactiveObject
    {
        public MLangBlogGroupEdit ItemEdit = new();
        public string LANGNAME { get; private set; }

        public LangBlogGroupsDetailViewModel(MLangBlogGroup item, LangBlogViewModel vm)
        {
            bool isNew = item.ID == 0;
            item.CopyProperties(ItemEdit);
            LANGNAME = vm.vmSettings.SelectedLang.LANGNAME;
            ItemEdit.Save = ReactiveCommand.CreateFromTask(async () =>
            {
                ItemEdit.CopyProperties(item);
                if (isNew)
                    await vm.CreateGroup(item);
                else
                    await vm.UpdateGroup(item);
            });
        }
    }
}
