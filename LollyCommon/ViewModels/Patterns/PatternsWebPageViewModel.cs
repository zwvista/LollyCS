using ReactiveUI;
using ReactiveUI.Validation.Extensions;

namespace LollyCloud
{
    public class PatternsWebPageViewModel : ReactiveObject
    {
        public MPatternWebPageEdit ItemEdit = new MPatternWebPageEdit();

        public PatternsWebPageViewModel(PatternsViewModel vm, MPatternWebPage item)
        {
            item.CopyProperties(ItemEdit);
            ItemEdit.Save = ReactiveCommand.CreateFromTask(async () =>
            {
                ItemEdit.CopyProperties(item);
                item.PATTERN = vm.vmSettings.AutoCorrectInput(item.PATTERN);
                if (item.WEBPAGEID == 0)
                    await vm.CreateWebPage(item);
                else
                    await vm.UpdateWebPage(item);
                if (item.ID == 0)
                    await vm.CreatePatternWebPage(item);
                else
                    await vm.UpdatePatternWebPage(item);
            }, ItemEdit.IsValid());
        }
    }
}
