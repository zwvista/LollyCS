using ReactiveUI;
using System;
using System.Linq;

namespace LollyCommon
{
    public class PhrasesUnitBatchAddViewModel : ReactiveObject
    {
        public MUnitPhraseEdit ItemEdit = new MUnitPhraseEdit();
        public PhrasesUnitBatchAddViewModel(PhrasesUnitViewModel vm)
        {
            var item = vm.NewUnitPhrase();
            item.CopyProperties(ItemEdit);
            ItemEdit.Save = ReactiveCommand.CreateFromTask(async () =>
            {
                ItemEdit.CopyProperties(item);
                var phrases = ItemEdit.PHRASES.Split('\n').Select(s => s.Trim()).ToList();
                for (int i = 0; i < phrases.Count; i += 2)
                {
                    item.PHRASE = vm.vmSettings.AutoCorrectInput(phrases[i]);
                    item.TRANSLATION = phrases[i + 1];
                    await vm.Create(item);
                    item.SEQNUM++;
                }
            });
        }

        public static implicit operator PhrasesUnitBatchAddViewModel(WordsUnitBatchAddViewModel v)
        {
            throw new NotImplementedException();
        }
    }
}
