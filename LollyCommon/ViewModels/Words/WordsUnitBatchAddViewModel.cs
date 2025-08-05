using ReactiveUI;
using System.Linq;

namespace LollyCommon
{
    public partial class WordsUnitBatchAddViewModel : ReactiveObject
    {
        public MUnitWordEdit ItemEdit = new();
        public WordsUnitBatchAddViewModel(WordsUnitViewModel vm)
        {
            var item = vm.NewUnitWord();
            item.CopyProperties(ItemEdit);
            ItemEdit.Save = ReactiveCommand.CreateFromTask(async () =>
            {
                ItemEdit.CopyProperties(item);
                var words = ItemEdit.WORDS.Split('\n').Select(s => s.Trim()).ToList();
                foreach (var s in words)
                {
                    item.WORD = vm.vmSettings.AutoCorrectInput(s);
                    await vm.Create(item);
                    item.SEQNUM++;
                }
            });
        }
    }
}
