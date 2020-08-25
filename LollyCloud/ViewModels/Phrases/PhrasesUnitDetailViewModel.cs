using ReactiveUI;
using ReactiveUI.Validation.Extensions;

namespace LollyCloud
{
    public class PhrasesUnitDetailViewModel : ReactiveObject
    {
        public MUnitPhraseEdit ItemEdit = new MUnitPhraseEdit();
        public SinglePhraseViewModel vmSinglePhrase;

        public PhrasesUnitDetailViewModel(PhrasesUnitViewModel vm, int index)
        {
            var item = index == -1 ? vm.NewUnitPhrase() : vm.PhraseItems[index];
            item.CopyProperties(ItemEdit);
            vmSinglePhrase = new SinglePhraseViewModel(item.PHRASE, vm.vmSettings);
            ItemEdit.Save = ReactiveCommand.CreateFromTask(async () =>
            {
                ItemEdit.CopyProperties(item);
                item.PHRASE = vm.vmSettings.AutoCorrectInput(item.PHRASE);
                if (item.ID == 0)
                {
                    await vm.Create(item);
                    vm.Add(item);
                }
                else
                {
                    await vm.Update(item);
                    vm.Replace(index, item);
                }
            }, ItemEdit.IsValid());
        }
    }
}
