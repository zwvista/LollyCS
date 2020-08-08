using ReactiveUI;
using ReactiveUI.Validation.Extensions;

namespace LollyCloud
{
    public class WordsLangDetailViewModel : ReactiveObject
    {
        MLangWord item;
        WordsLangViewModel vm;
        public MLangWordEdit ItemEdit = new MLangWordEdit();
        public SingleWordViewModel vmSingleWord;

        public WordsLangDetailViewModel(MLangWord item, WordsLangViewModel vm)
        {
            this.item = item;
            this.vm = vm;
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
