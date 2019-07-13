using LollyShared;
using MSHTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace LollyCloud
{
    public class WordsBaseControl: UserControl
    {
        protected DictWebBrowserStatus dictStatus = DictWebBrowserStatus.Ready;
        protected int selectedDictItemIndex;
        protected string selectedWord = "";
        public virtual DataGrid dgWordsBase { get => null; }
        public virtual MWordInterface ItemForRow(int row) { return null; }
        public virtual SettingsViewModel vmSettings { get => null; }
        public virtual WebBrowser wbDictBase { get => null; }
        public void dgWords_SelectionChanged(object sender, SelectionChangedEventArgs e) => SearchDict(null, null);

        public void SearchDict(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton)
                selectedDictItemIndex = (int)(sender as RadioButton).Tag;
            var row = dgWordsBase.SelectedIndex;
            if (row == -1) return;
            selectedWord = ItemForRow(row).WORD;
            SearchWord(selectedWord);
        }

        public async void SearchWord(string word)
        {
            dictStatus = DictWebBrowserStatus.Ready;
            var item = vmSettings.DictItems[selectedDictItemIndex];
            if (item.DICTNAME.StartsWith("Custom"))
            {
                var str = vmSettings.DictHtml(word, item.DictIDs.ToList());
                wbDictBase.NavigateToString(str);
            }
            else
            {
                var item2 = vmSettings.DictsReference.First(o => o.DICTNAME == item.DICTNAME);
                var url = item2.UrlString(word, vmSettings.AutoCorrects.ToList());
                if (item2.DICTTYPENAME == "OFFLINE")
                {
                    wbDictBase.Navigate("about:blank");
                    var html = await vmSettings.client.GetStringAsync(url);
                    var str = item2.HtmlString(html, word);
                    wbDictBase.NavigateToString(str);
                }
                else
                {
                    wbDictBase.Navigate(url);
                    if (item2.AUTOMATION != null)
                        dictStatus = DictWebBrowserStatus.Automating;
                    else if (item2.DICTTYPENAME == "OFFLINE-ONLINE")
                        dictStatus = DictWebBrowserStatus.Navigating;
                }
            }
        }
        public void wbDict_Navigated(object sender, NavigationEventArgs e) => wbDictBase.SetSilent(true);

        public void wbDict_LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (dictStatus == DictWebBrowserStatus.Ready) return;
            var item = vmSettings.DictItems[selectedDictItemIndex];
            var item2 = vmSettings.DictsReference.FirstOrDefault(o => o.DICTNAME == item.DICTNAME);
            switch (dictStatus)
            {
                case DictWebBrowserStatus.Automating:
                    var s = item2.AUTOMATION.Replace("{0}", selectedWord);
                    // https://stackoverflow.com/questions/7998996/how-to-inject-javascript-in-webbrowser-control
                    wbDictBase.InvokeScript("execScript", new[] { s, "JavaScript" });
                    dictStatus = DictWebBrowserStatus.Ready;
                    if (item2.DICTTYPENAME == "OFFLINE-ONLINE")
                        dictStatus = DictWebBrowserStatus.Navigating;
                    break;
                case DictWebBrowserStatus.Navigating:
                    var doc = (HTMLDocument)wbDictBase.Document;
                    var html = doc.documentElement.outerHTML;
                    var str = item2.HtmlString(html, selectedWord, useTransformWin: true);
                    dictStatus = DictWebBrowserStatus.Ready;
                    wbDictBase.NavigateToString(str);
                    break;
            }
        }

        public void miCopy_Click(object sender, RoutedEventArgs e) => Clipboard.SetText(selectedWord);

        public void miGoogle_Click(object sender, RoutedEventArgs e) => CommonApi.GoogleString(selectedWord);
    }
}
