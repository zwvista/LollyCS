using LollyCommon;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LollyCloud
{
    public class PhrasesBaseControl : WordsPhrasesBaseControl
    {
        public void dgPhrases_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.Speak(vmSettings, vmWP.SelectedPhrase);
            SearchWords();
        }
        public void miCopy_Click(object sender, RoutedEventArgs e) => Clipboard.SetText(vmWP.SelectedPhrase);

        public void miGoogle_Click(object sender, RoutedEventArgs e) => vmWP.SelectedPhrase.Google();
        public virtual async Task SearchWords() { }
    }
}
