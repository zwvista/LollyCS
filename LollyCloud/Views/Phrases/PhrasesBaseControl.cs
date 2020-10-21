using LollyCommon;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LollyCloud
{
    public class PhrasesBaseControl : WordsPhrasesBaseControl
    {
        protected WordsLangViewModel vmWordsLang;
        public override async Task OnSettingsChanged()
        {
            await base.OnSettingsChanged();
            vmWordsLang = new WordsLangViewModel(vmSettings, false);
        }
        public void OnEndEditWord(object sender, DataGridCellEditEndingEventArgs e) =>
            OnEndEdit(sender, e, "WORD", async item => await vmWordsLang.Update((MLangWord)item));
    }
}
