using ReactiveUI;
using ReactiveUI.Validation.Extensions;

namespace LollyCommon
{
    public partial class PhrasesLangDetailViewModel : ReactiveObject
    {
        public MLangPhraseEdit ItemEdit = new();
        public SinglePhraseViewModel vmSinglePhrase;

        public PhrasesLangDetailViewModel(PhrasesLangViewModel vm, MLangPhrase item)
        {
            item.CopyProperties(ItemEdit);
            vmSinglePhrase = new SinglePhraseViewModel(item.PHRASE, vm.vmSettings);
            ItemEdit.Save = ReactiveCommand.CreateFromTask(async () =>
            {
                ItemEdit.CopyProperties(item);
                item.PHRASE = vm.vmSettings.AutoCorrectInput(item.PHRASE);
                if (item.ID == 0)
                    await vm.Create(item);
                else
                    await vm.Update(item);
            }, ItemEdit.IsValid());
        }
    }
}
