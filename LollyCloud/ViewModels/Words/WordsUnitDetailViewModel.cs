using ReactiveUI;
using ReactiveUI.Validation.Extensions;

namespace LollyCloud
{
    public class WordsUnitDetailViewModel : ReactiveObject
    {
        public MUnitWordEdit ItemEdit = new MUnitWordEdit();
        public SingleWordViewModel vmSingleWord;

        public WordsUnitDetailViewModel(WordsUnitViewModel vm, int index)
        {
            var item = index == -1 ? vm.NewUnitWord() : vm.WordItems[index];
            item.CopyProperties(ItemEdit);
            vmSingleWord = new SingleWordViewModel(item.WORD, vm.vmSettings);
            ItemEdit.Save = ReactiveCommand.CreateFromTask(async () =>
            {
                ItemEdit.CopyProperties(item);
                item.WORD = vm.vmSettings.AutoCorrectInput(item.WORD);
                if (item.ID == 0)
                {
                    var o = await vm.Create(item);
                    vm.Add(o);
                }
                else
                {
                    var o = await vm.Update(item);
                    vm.Replace(index, o);
                }
            }, ItemEdit.IsValid());
        }
    }
}
