using LollyShared;
using MSHTML;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace LollyCloud
{
    public class WordsBaseControl : UserControl, ILollySettings
    {
        protected DictWebBrowserStatus dictStatus = DictWebBrowserStatus.Ready;
        protected int selectedDictItemIndex;
        protected string selectedWord = "";
        protected int selectedWordID = 0;
        protected string originalText = "";
        public virtual DataGrid dgWordsBase => null;
        public virtual MWordInterface ItemForRow(int row) => null;
        public virtual SettingsViewModel vmSettings => null;
        public virtual WebBrowser wbDictBase => null;
        public virtual ToolBar ToolBarDictBase => null;
        public virtual TextBox tbURLBase => null;

        public virtual void dgWords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchDict(null, null);
            SearchPhrases();
            App.Speak(vmSettings, selectedWord);
        }

        public void SearchDict(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton)
                selectedDictItemIndex = (int)(sender as RadioButton).Tag;
            var row = dgWordsBase.SelectedIndex;
            if (row == -1)
            {
                selectedWord = "";
                selectedWordID = 0;
            }
            else
            {
                var item = ItemForRow(row);
                selectedWord = item.WORD;
                selectedWordID = item.WORDID;
                SearchWord(selectedWord);
            }
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
            if (e.Uri == null) return;
            tbURLBase.Text = e.Uri.AbsoluteUri;
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

        public async Task ChangeLevel(int delta)
        {
            var row = dgWordsBase.SelectedIndex;
            if (row == -1) return;
            var item = ItemForRow(row);
            var newLevel = item.LEVEL + delta;
            if (newLevel != 0 && !vmSettings.USLEVELCOLORS.ContainsKey(newLevel)) return;
            item.LEVEL = newLevel;
            await LevelChanged(row);
        }

        public virtual async Task LevelChanged(int row) { }

        public virtual async void dgWords_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Alt && (e.SystemKey == Key.Up || e.SystemKey == Key.Down))
            {
                await ChangeLevel(e.SystemKey == Key.Up ? 1 : -1);
                e.Handled = true;
            }
        }

        public async void btnRefresh_Click(object sender, RoutedEventArgs e) => await OnSettingsChanged();

        public void btnOpenURL_Click(object sender, RoutedEventArgs e) => Process.Start(tbURLBase.Text);

        public virtual async Task OnSettingsChanged()
        {
            selectedDictItemIndex = vmSettings.SelectedDictItemIndex;
            ToolBarDictBase.Items.Clear();
            for (int i = 0; i < vmSettings.DictItems.Count; i++)
            {
                var b = new RadioButton
                {
                    Content = vmSettings.DictItems[i].DICTNAME,
                    GroupName = "DICT",
                    Tag = i,
                };
                b.Click += SearchDict;
                ToolBarDictBase.Items.Add(b);
                if (i == selectedDictItemIndex)
                    b.IsChecked = true;
            }
        }

        public virtual async Task SearchPhrases() { }
    }
}
