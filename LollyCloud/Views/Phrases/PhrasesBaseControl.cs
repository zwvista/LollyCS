using LollyCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LollyCloud
{
    public class PhrasesBaseControl : WordsPhraseBaseControl
    {
        protected string selectedPhrase = "";
        protected int selectedPhraseID = 0;
        public virtual DataGrid dgPhrasesBase => null;

        public void dgPhrases_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (MPhraseInterface)dgPhrasesBase.SelectedItem;
            if (item == null)
            {
                selectedPhrase = "";
                selectedPhraseID = 0;
            }
            else
            {
                selectedPhrase = item.PHRASE;
                selectedPhraseID = item.PHRASEID;
            }
            App.Speak(vmSettings, selectedPhrase);
            SearchWords();
        }

        public void miCopy_Click(object sender, RoutedEventArgs e) => Clipboard.SetText(selectedPhrase);

        public void miGoogle_Click(object sender, RoutedEventArgs e) => selectedPhrase.Google();
        public virtual async Task SearchWords() { }
    }
}
