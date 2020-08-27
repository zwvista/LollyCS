using ReactiveUI;
using ReactiveUI.Validation.Extensions;

namespace LollyCloud
{
    public class WordsLangDetailViewModel : ReactiveObject
    {
        public MLangWordEdit ItemEdit = new MLangWordEdit();
        public SingleWordViewModel vmSingleWord;

        public WordsLangDetailViewModel(WordsLangViewModel vm, int index = -1)
        {
            var item = index == -1 ? vm.NewLangWord() : vm.WordItems[index];
            item.CopyProperties(ItemEdit);
            vmSingleWord = new SingleWordViewModel(item.WORD, vm.vmSettings);
            ItemEdit.Save = ReactiveCommand.CreateFromTask(async () =>
            {
                ItemEdit.CopyProperties(item);
                item.WORD = vm.vmSettings.AutoCorrectInput(item.WORD);
                if (item.ID == 0)
                {
                    await vm.Create(item);
                    vm.WordItems.Add(item);
                }
                else
                    await vm.Update(item);
            }, ItemEdit.IsValid());
        }
    }
}
