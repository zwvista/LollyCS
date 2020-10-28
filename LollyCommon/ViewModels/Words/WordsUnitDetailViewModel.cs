using ReactiveUI;
using ReactiveUI.Validation.Extensions;

namespace LollyCommon
{
    public class WordsUnitDetailViewModel : ReactiveObject
    {
        public MUnitWordEdit ItemEdit = new MUnitWordEdit();
        public SingleWordViewModel vmSingleWord;
        WordPhraseDataStore wordPhraseDS = new WordPhraseDataStore();

        public WordsUnitDetailViewModel(WordsUnitViewModel vm, MUnitWord item, int phraseid)
        {
            item.CopyProperties(ItemEdit);
            vmSingleWord = new SingleWordViewModel(item.WORD, vm.vmSettings);
            ItemEdit.Save = ReactiveCommand.CreateFromTask(async () =>
            {
                ItemEdit.CopyProperties(item);
                item.WORD = vm.vmSettings.AutoCorrectInput(item.WORD);
                if (item.ID != 0)
                    await vm.Update(item);
                else
                {
                    await vm.Create(item);
                    if (phraseid != 0)
                        await wordPhraseDS.Link(item.WORDID, phraseid);
                }
            }, ItemEdit.IsValid());
        }
    }
}
