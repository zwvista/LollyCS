using ReactiveUI;
using System.Reactive;

namespace LollyCloud
{
    public class TextbooksDetailViewModel : ReactiveObject
    {
        MTextbook item;
        TextbooksViewModel vm;
        public MTextbookEdit ItemEdit = new MTextbookEdit();
        public string LANGNAME { get; private set; }
        public ReactiveCommand<Unit, Unit> Save { get; }

        public TextbooksDetailViewModel(MTextbook item, TextbooksViewModel vm)
        {
            this.item = item;
            this.vm = vm;
            item.CopyProperties(ItemEdit);
            LANGNAME = vm.vmSettings.SelectedLang.LANGNAME;
            Save = ReactiveCommand.CreateFromTask(async () =>
            {
                ItemEdit.CopyProperties(item);
                if (item.ID == 0)
                    item.ID = await vm.Create(item);
                else
                    await vm.Update(item);
            });
        }
    }
}
