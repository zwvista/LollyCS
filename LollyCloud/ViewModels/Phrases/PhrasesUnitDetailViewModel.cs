using ReactiveUI;
using System.Threading.Tasks;

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
        }
        public async Task OnOK()
        {
            ItemEdit.CopyProperties(item);
            item.PHRASE = vm.vmSettings.AutoCorrectInput(item.PHRASE);
            if (item.ID == 0)
                item.ID = await vm.Create(item);
            else
                await vm.Update(item);
        }
    }
}
