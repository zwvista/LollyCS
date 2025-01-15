using ReactiveUI;
using ReactiveUI.Validation.Extensions;

namespace LollyCommon
{
    public class PatternsDetailViewModel : ReactiveObject
    {
        public MPatternEdit ItemEdit = new();

        public PatternsDetailViewModel(PatternsViewModel vm, MPattern item)
        {
            item.CopyProperties(ItemEdit);
            ItemEdit.Save = ReactiveCommand.CreateFromTask(async () =>
            {
                ItemEdit.CopyProperties(item);
                item.PATTERN = vm.vmSettings.AutoCorrectInput(item.PATTERN);
                if (item.ID == 0)
                    await vm.Create(item);
                else
                    await vm.Update(item);
            }, ItemEdit.IsValid());
        }
    }
}
