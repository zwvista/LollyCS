using ReactiveUI;
using ReactiveUI.Validation.Extensions;

namespace LollyCommon
{
    public class WordsUnitDetailViewModel : ReactiveObject
    {
        public MUnitWordEdit ItemEdit = new MUnitWordEdit();
        public SingleWordViewModel vmSingleWord;

        public WordsUnitDetailViewModel(WordsUnitViewModel vm, MUnitWord item)
        {
            item.CopyProperties(ItemEdit);
            vmSingleWord = new SingleWordViewModel(item.WORD, vm.vmSettings);
            ItemEdit.Save = ReactiveCommand.CreateFromTask(async () =>
            {
                ItemEdit.CopyProperties(item);
                item.WORD = vm.vmSettings.AutoCorrectInput(item.WORD);
                if (item.ID == 0)
                    await vm.Create(item);
                else
                    await vm.Update(item);
            }, ItemEdit.IsValid());
        }
    }
}
