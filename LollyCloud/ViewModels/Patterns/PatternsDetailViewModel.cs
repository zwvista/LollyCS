using ReactiveUI;
using System.Threading.Tasks;

namespace LollyCloud
{
    public class PatternsDetailViewModel : ReactiveObject
    {
        MPattern item;
        PatternsViewModel vm;
        public MPatternEdit ItemEdit = new MPatternEdit();

        public PatternsDetailViewModel(MPattern item, PatternsViewModel vm)
        {
            this.item = item;
            this.vm = vm;
            item.CopyProperties(ItemEdit);
        }
        public async Task OnOK()
        {
            ItemEdit.CopyProperties(item);
            item.PATTERN = vm.vmSettings.AutoCorrectInput(item.PATTERN);
            if (item.ID == 0)
                item.ID = await vm.Create(item);
            else
                await vm.Update(item);
        }
    }
}
