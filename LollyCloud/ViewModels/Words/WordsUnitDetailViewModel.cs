using ReactiveUI;
using System.Threading.Tasks;

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
