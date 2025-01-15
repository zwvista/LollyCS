using ReactiveUI;
using ReactiveUI.Validation.Extensions;

namespace LollyCommon
{
    public class DictsDetailViewModel : ReactiveObject
    {
        public DictsViewModel vm;
        public MDictionaryEdit ItemEdit = new();
        public string LANGNAME { get; private set; }

        public DictsDetailViewModel(MDictionary item, DictsViewModel vm)
        {
            this.vm = vm;
            item.CopyProperties(ItemEdit);
            LANGNAME = vm.vmSettings.SelectedLang.LANGNAME;
            ItemEdit.Save = ReactiveCommand.CreateFromTask(async () =>
            {
                ItemEdit.CopyProperties(item);
                if (item.ID == 0)
                    await vm.Create(item);
                else
                    await vm.Update(item);
            }, ItemEdit.IsValid());
        }
    }
}
