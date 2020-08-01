using ReactiveUI;
using ReactiveUI.Validation.Extensions;

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
            ItemEdit.Save = ReactiveCommand.CreateFromTask(async () =>
            {
                ItemEdit.CopyProperties(item);
                if (item.ID == 0)
                    item.ID = await vm.Create(item);
                else
                    await vm.Update(item);
            }, ItemEdit.IsValid());
        }
    }
}
