using ReactiveUI;
using ReactiveUI.Validation.Extensions;

namespace LollyCloud
{
    public class PhrasesUnitDetailViewModel : ReactiveObject
    {
        MUnitPhrase item;
        PhrasesUnitViewModel vm;
        public MUnitPhraseEdit ItemEdit = new MUnitPhraseEdit();
        public SinglePhraseViewModel vmSinglePhrase;

        public PhrasesUnitDetailViewModel(MUnitPhrase item, PhrasesUnitViewModel vm)
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
                    await vm.Create(item);
                else
                    await vm.Update(item);
            }, ItemEdit.IsValid());
        }
    }
}
