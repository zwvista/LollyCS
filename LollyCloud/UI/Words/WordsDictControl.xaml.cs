using LollyShared;
using MSHTML;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace LollyCloud
{
    /// <summary>
    /// WordsDictControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsDictControl : UserControl
    {
        public DictWebBrowserStatus dictStatus = DictWebBrowserStatus.Ready;
        public MDictReference Dict;
        public string Word = "";
        public SettingsViewModel vmSettings;
        public WordsDictControl()
        {
            InitializeComponent();
        }
        public async void SearchWord(string word)
        {
            Word = word;
            dictStatus = DictWebBrowserStatus.Ready;
            var url = Dict.UrlString(word, vmSettings.AutoCorrects.ToList());
            if (Dict.DICTTYPENAME == "OFFLINE")
            {
                wbDict.Navigate("about:blank");
                var html = await vmSettings.client.GetStringAsync(url);
                var str = Dict.HtmlString(html, word);
                wbDict.NavigateToString(str);
            }
            else
            {
                wbDict.Navigate(url);
                if (Dict.AUTOMATION != null)
                    dictStatus = DictWebBrowserStatus.Automating;
                else if (Dict.DICTTYPENAME == "OFFLINE-ONLINE")
                    dictStatus = DictWebBrowserStatus.Navigating;
            }
        }
        public void wbDict_Navigated(object sender, NavigationEventArgs e) => wbDict.SetSilent(true);

        public void wbDict_LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.Uri == null) return;
            tbURL.Text = e.Uri.AbsoluteUri;
            if (dictStatus == DictWebBrowserStatus.Ready) return;
            switch (dictStatus)
            {
                case DictWebBrowserStatus.Automating:
                    var s = Dict.AUTOMATION.Replace("{0}", Word);
                    // https://stackoverflow.com/questions/7998996/how-to-inject-javascript-in-webbrowser-control
                    wbDict.InvokeScript("execScript", new[] { s, "JavaScript" });
                    dictStatus = DictWebBrowserStatus.Ready;
                    if (Dict.DICTTYPENAME == "OFFLINE-ONLINE")
                        dictStatus = DictWebBrowserStatus.Navigating;
                    break;
                case DictWebBrowserStatus.Navigating:
                    var doc = (HTMLDocument)wbDict.Document;
                    var html = doc.documentElement.outerHTML;
                    var str = Dict.HtmlString(html, Word, useTransformWin: true);
                    dictStatus = DictWebBrowserStatus.Ready;
                    wbDict.NavigateToString(str);
                    break;
            }
        }
        public void btnOpenURL_Click(object sender, RoutedEventArgs e) => Process.Start(tbURL.Text);
    }
}
