using ReactiveUI;
using ReactiveUI.Validation.Extensions;

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
            ItemEdit.Save = ReactiveCommand.CreateFromTask(async () =>
            {
                ItemEdit.CopyProperties(item);
                item.PHRASE = vm.vmSettings.AutoCorrectInput(item.PHRASE);
                if (item.ID == 0)
                    item.ID = await vm.Create(item);
                else
                    await vm.Update(item);
            }, ItemEdit.IsValid());
        }
    }
}
