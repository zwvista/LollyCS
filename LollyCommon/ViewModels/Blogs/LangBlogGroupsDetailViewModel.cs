﻿using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LollyCommon
{
    public class LangBlogGroupsDetailViewModel : ReactiveObject
    {
        MLangBlogGroup item;
        LangBlogViewModel vm;
        public MLangBlogGroupEdit ItemEdit = new MLangBlogGroupEdit();
        public string LANGNAME { get; private set; }

        public LangBlogGroupsDetailViewModel(MLangBlogGroup item, LangBlogViewModel vm)
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
                    await vm.CreateGroup(item);
                else
                    await vm.UpdateGroup(item);
            });
        }
    }
}
