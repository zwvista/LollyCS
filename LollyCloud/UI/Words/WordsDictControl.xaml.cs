using LollyShared;
using MSHTML;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LollyCloud
{
    /// <summary>
    /// WordsDictControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsDictControl : UserControl
    {
        public DictWebBrowserStatus dictStatus = DictWebBrowserStatus.Ready;
        public int selectedDictItemIndex;
        public string selectedWord = "";
        public SettingsViewModel vmSettings;
        public WordsDictControl()
        {
            InitializeComponent();
        }
        public async void SearchWord(string word)
        {
            dictStatus = DictWebBrowserStatus.Ready;
            var item = vmSettings.DictItems[selectedDictItemIndex];
            var item2 = vmSettings.DictsReference.First(o => o.DICTNAME == item.DICTNAME);
            var url = item2.UrlString(word, vmSettings.AutoCorrects.ToList());
            if (item2.DICTTYPENAME == "OFFLINE")
            {
                wbDict.Navigate("about:blank");
                var html = await vmSettings.client.GetStringAsync(url);
                var str = item2.HtmlString(html, word);
                wbDict.NavigateToString(str);
            }
            else
            {
                wbDict.Navigate(url);
                if (item2.AUTOMATION != null)
                    dictStatus = DictWebBrowserStatus.Automating;
                else if (item2.DICTTYPENAME == "OFFLINE-ONLINE")
                    dictStatus = DictWebBrowserStatus.Navigating;
            }
        }
        public void wbDict_Navigated(object sender, NavigationEventArgs e) => wbDict.SetSilent(true);

        public void wbDict_LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.Uri == null) return;
            tbURL.Text = e.Uri.AbsoluteUri;
            if (dictStatus == DictWebBrowserStatus.Ready) return;
            var item = vmSettings.DictItems[selectedDictItemIndex];
            var item2 = vmSettings.DictsReference.FirstOrDefault(o => o.DICTNAME == item.DICTNAME);
            switch (dictStatus)
            {
                case DictWebBrowserStatus.Automating:
                    var s = item2.AUTOMATION.Replace("{0}", selectedWord);
                    // https://stackoverflow.com/questions/7998996/how-to-inject-javascript-in-webbrowser-control
                    wbDict.InvokeScript("execScript", new[] { s, "JavaScript" });
                    dictStatus = DictWebBrowserStatus.Ready;
                    if (item2.DICTTYPENAME == "OFFLINE-ONLINE")
                        dictStatus = DictWebBrowserStatus.Navigating;
                    break;
                case DictWebBrowserStatus.Navigating:
                    var doc = (HTMLDocument)wbDict.Document;
                    var html = doc.documentElement.outerHTML;
                    var str = item2.HtmlString(html, selectedWord, useTransformWin: true);
                    dictStatus = DictWebBrowserStatus.Ready;
                    wbDict.NavigateToString(str);
                    break;
            }
        }
        public void btnOpenURL_Click(object sender, RoutedEventArgs e) => Process.Start(tbURL.Text);
    }
}
