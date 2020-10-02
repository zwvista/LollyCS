using ReactiveUI;
using ReactiveUI.Validation.Extensions;

namespace LollyCommon
{
    public class WordsLangDetailViewModel : ReactiveObject
    {
        public MLangWordEdit ItemEdit = new MLangWordEdit();
        public SingleWordViewModel vmSingleWord;

        public WordsLangDetailViewModel(WordsLangViewModel vm, MLangWord item)
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
