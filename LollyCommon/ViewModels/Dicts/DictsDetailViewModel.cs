using ReactiveUI;
using ReactiveUI.Validation.Extensions;

namespace LollyCommon
{
    public class DictsDetailViewModel : ReactiveObject
    {
        public DictsViewModel vm;
        public MDictionaryEdit ItemEdit = new MDictionaryEdit();
        public string LANGNAME { get; private set; }

        public DictsDetailViewModel(MDictionary item, DictsViewModel vm)
        {
            this.vm = vm;
            item.CopyProperties(ItemEdit);
            LANGNAME = vm.vmSettings.SelectedLang.LANGNAME;
            ItemEdit.Save = ReactiveCommand.CreateFromTask(async () =>
            {
                ItemEdit.CopyProperties(item);
                var itemDict = new MDictionaryDict();
                var itemSite = new MDictionarySite();
                ItemEdit.CopyProperties(itemDict);
                ItemEdit.CopyProperties(itemSite);
                if (itemDict.ID == 0)
                    await vm.CreateDict(itemDict);
                else
                    await vm.UpdateDict(itemDict);
                if (itemSite.SITEID != 0)
                    await vm.UpdateSite(itemSite);
                else if (!string.IsNullOrEmpty(itemSite.TRANSFORM))
                    await vm.CreateSite(itemSite);
            }, ItemEdit.IsValid());
        }
    }
}
