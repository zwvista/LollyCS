using GongSolutions.Wpf.DragDrop;
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
    public class WordsUnitDetailViewModel : ReactiveObject
    {
        MUnitWord item;
        WordsUnitViewModel vm;
        public MUnitWordEdit ItemEdit = new MUnitWordEdit();
        public SingleWordViewModel vmSingleWord;

        public WordsUnitDetailViewModel(MUnitWord item, WordsUnitViewModel vm)
        {
            this.item = item;
            this.vm = vm;
            item.CopyProperties(ItemEdit);
            vmSingleWord = new SingleWordViewModel(item.WORD, vm.vmSettings);
        }
        public async Task OnOK()
        {
            ItemEdit.CopyProperties(item);
            item.WORD = vm.vmSettings.AutoCorrectInput(item.WORD);
            if (item.ID == 0)
                item.ID = await vm.Create(item);
            else
                await vm.Update(item);
        }
    }
}
