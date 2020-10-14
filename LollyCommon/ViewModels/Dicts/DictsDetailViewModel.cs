using ReactiveUI;
using ReactiveUI.Validation.Extensions;

namespace LollyCommon
{
    public class DictsDetailViewModel : ReactiveObject
    {
        MDictionary item;
        public DictsViewModel vm;
        public MDictionaryEdit ItemEdit = new MDictionaryEdit();
        public string LANGNAME { get; private set; }

        public DictsDetailViewModel(MDictionary item, DictsViewModel vm)
        {
            this.item = item;
            this.vm = vm;
            item.CopyProperties(ItemEdit);
            LANGNAME = vm.vmSettings.SelectedLang.LANGNAME;
            ItemEdit.Save = ReactiveCommand.CreateFromTask(async () =>
            {
                ItemEdit.CopyProperties(item);
                //if (item.ID == 0)
                //    await vm.CreateDict(item);
                //else
                //    await vm.UpdateDict(item);
                //if (item.ID == 0)
                //    await vm.CreateSite(item);
                //else
                //    await vm.UpdateSite(item);
            }, ItemEdit.IsValid());
        }
    }
}
