using ReactiveUI;
using ReactiveUI.Validation.Extensions;

namespace LollyCommon
{
    public partial class PhrasesUnitDetailViewModel : ReactiveObject
    {
        public MUnitPhraseEdit ItemEdit = new();
        public SinglePhraseViewModel vmSinglePhrase;
        WordPhraseDataStore wordPhraseDS = new();

        public PhrasesUnitDetailViewModel(PhrasesUnitViewModel vm, MUnitPhrase item, int wordid)
        {
            item.CopyProperties(ItemEdit);
            vmSinglePhrase = new SinglePhraseViewModel(item.PHRASE, vm.vmSettings);
            ItemEdit.Save = ReactiveCommand.CreateFromTask(async () =>
            {
                ItemEdit.CopyProperties(item);
                item.PHRASE = vm.vmSettings.AutoCorrectInput(item.PHRASE);
                if (item.ID != 0)
                    await vm.Update(item);
                else
                {
                    await vm.Create(item);
                    if (wordid != 0)
                        await wordPhraseDS.Associate(wordid, item.PHRASEID);
                }
            }, ItemEdit.IsValid());
        }
    }
}
