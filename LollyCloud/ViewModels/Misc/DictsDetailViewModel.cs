using ReactiveUI;
using System.Threading.Tasks;

namespace LollyCloud
{
    public class DictsDetailViewModel : ReactiveObject
    {
        MDictionary item;
        DictsViewModel vm;
        public MDictionaryEdit ItemEdit = new MDictionaryEdit();
        public string LANGNAME { get; private set; }

        public DictsDetailViewModel(MDictionary item, DictsViewModel vm)
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
