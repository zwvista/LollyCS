using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LollyCloud
{
    public class PhrasesBaseControl : UserControl, ILollySettings
    {
        public string selectedPhrase = "";
        protected string originalText = "";
        public virtual SettingsViewModel vmSettings => null;
        public virtual DataGrid dgPhrasesBase => null;

        public void dgPhrases_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (MPhraseInterface)dgPhrasesBase.SelectedItem;
            if (item == null) return;
            App.Speak(vmSettings, selectedPhrase);
        }

        public virtual async Task OnSettingsChanged()
        {
        }

        public void miCopy_Click(object sender, RoutedEventArgs e) => Clipboard.SetText(selectedPhrase);

        public void miGoogle_Click(object sender, RoutedEventArgs e) => CommonApi.GoogleString(selectedPhrase);
    }
}
