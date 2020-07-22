﻿using ReactiveUI;
using System.Threading.Tasks;

namespace LollyCloud
{
    public class PatternsWebPageViewModel : ReactiveObject
    {
        MPatternWebPage item;
        PatternsViewModel vm;
        public MPatternWebPageEdit ItemEdit = new MPatternWebPageEdit();

        public PatternsWebPageViewModel(MPatternWebPage item, PatternsViewModel vm)
        {
            this.item = item;
            this.vm = vm;
            item.CopyProperties(ItemEdit);
        }
        public async Task OnOK()
        {
            ItemEdit.CopyProperties(item);
            item.PATTERN = vm.vmSettings.AutoCorrectInput(item.PATTERN);
            if (item.WEBPAGEID == 0)
                item.WEBPAGEID = await vm.CreateWebPage(item);
            else
                await vm.UpdateWebPage(item);
            if (item.ID == 0)
                item.ID = await vm.CreatePatternWebPage(item);
            else
                await vm.UpdatePatternWebPage(item);
        }
    }
}