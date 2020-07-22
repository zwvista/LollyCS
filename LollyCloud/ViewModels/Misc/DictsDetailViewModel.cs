using ReactiveUI;
using System.Threading.Tasks;

namespace LollyCloud
{
    public class DictsDetailViewModel : ReactiveObject
    {
        MTextbook item;
        TextbooksViewModel vm;
        public MTextbookEdit ItemEdit = new MTextbookEdit();
        public string LANGNAME { get; private set; }

        public DictsDetailViewModel(MTextbook item, TextbooksViewModel vm)
        {
            this.item = item;
            this.vm = vm;
            item.CopyProperties(ItemEdit);
            LANGNAME = vm.vmSettings.SelectedLang.LANGNAME;
        }
        public async Task OnOK()
        {
            ItemEdit.CopyProperties(item);
            if (item.ID == 0)
                item.ID = await vm.Create(item);
            else
                await vm.Update(item);
        }
    }
}
