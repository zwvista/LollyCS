using ReactiveUI;
using System.Reactive;

namespace LollyCommon
{
    public partial class TextbooksDetailViewModel : ReactiveObject
    {
        MTextbook item;
        TextbooksViewModel vm;
        public MTextbookEdit ItemEdit = new();
        public string LANGNAME { get; private set; }

        public TextbooksDetailViewModel(MTextbook item, TextbooksViewModel vm)
        {
            bool isNew = item.ID == 0;
            this.item = item;
            this.vm = vm;
            item.CopyProperties(ItemEdit);
            LANGNAME = vm.vmSettings.SelectedLang.LANGNAME;
            ItemEdit.Save = ReactiveCommand.CreateFromTask(async () =>
            {
                ItemEdit.CopyProperties(item);
                if (isNew)
                    await vm.Create(item);
                else
                    await vm.Update(item);
            });
        }
    }
}
