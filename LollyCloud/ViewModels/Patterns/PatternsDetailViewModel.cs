﻿using ReactiveUI;
using ReactiveUI.Validation.Extensions;

namespace LollyCloud
{
    public class PatternsDetailViewModel : ReactiveObject
    {
        MPattern item;
        PatternsViewModel vm;
        public MPatternEdit ItemEdit = new MPatternEdit();

        public PatternsDetailViewModel(MPattern item, PatternsViewModel vm)
        {
            this.item = item;
            this.vm = vm;
            item.CopyProperties(ItemEdit);
            ItemEdit.Save = ReactiveCommand.CreateFromTask(async () =>
            {
                ItemEdit.CopyProperties(item);
                item.PATTERN = vm.vmSettings.AutoCorrectInput(item.PATTERN);
                if (item.ID == 0)
                    item.ID = await vm.Create(item);
                else
                    await vm.Update(item);
            }, ItemEdit.IsValid());
        }
    }
}
