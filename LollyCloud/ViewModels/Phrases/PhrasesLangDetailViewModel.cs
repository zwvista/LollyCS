﻿using GongSolutions.Wpf.DragDrop;
using GongSolutions.Wpf.DragDrop.Utilities;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using System.Windows;

namespace LollyCloud
{
    public class PhrasesLangDetailViewModel : ReactiveObject
    {
        MLangPhrase item;
        PhrasesLangViewModel vm;
        public MLangPhraseEdit ItemEdit = new MLangPhraseEdit();
        public SinglePhraseViewModel vmSinglePhrase;

        public PhrasesLangDetailViewModel(MLangPhrase item, PhrasesLangViewModel vm)
        {
            this.item = item;
            this.vm = vm;
            item.CopyProperties(ItemEdit);
            vmSinglePhrase = new SinglePhraseViewModel(item.PHRASE, vm.vmSettings);
        }
        public async Task OnOK()
        {
            ItemEdit.CopyProperties(item);
            item.PHRASE = vm.vmSettings.AutoCorrectInput(item.PHRASE);
            if (item.ID == 0)
                item.ID = await vm.Create(item);
            else
                await vm.Update(item);
        }
    }
}
