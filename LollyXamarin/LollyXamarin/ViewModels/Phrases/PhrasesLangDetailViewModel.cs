using ReactiveUI;
using ReactiveUI.Validation.Extensions;

namespace LollyCloud
{
    public class PhrasesLangDetailViewModel : ReactiveObject
    {
        public MLangPhraseEdit ItemEdit = new MLangPhraseEdit();
        public SinglePhraseViewModel vmSinglePhrase;

        public PhrasesLangDetailViewModel(PhrasesLangViewModel vm, int index)
        {
            var item = index == -1 ? vm.NewLangPhrase() : vm.PhraseItems[index];
            item.CopyProperties(ItemEdit);
            vmSinglePhrase = new SinglePhraseViewModel(item.PHRASE, vm.vmSettings);
            ItemEdit.Save = ReactiveCommand.CreateFromTask(async () =>
            {
                ItemEdit.CopyProperties(item);
                item.PHRASE = vm.vmSettings.AutoCorrectInput(item.PHRASE);
                if (item.ID == 0)
                {
                    await vm.Create(item);
                    vm.PhraseItems.Add(item);
                }
                else
                    await vm.Update(item);
            }, ItemEdit.IsValid());
        }
    }
}
