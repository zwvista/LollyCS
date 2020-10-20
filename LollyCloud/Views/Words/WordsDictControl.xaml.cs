using CefSharp;
using LollyCommon;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LollyCloud
{
    /// <summary>
    /// WordsDictControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsDictControl : UserControl
    {
        public DictWebBrowserStatus dictStatus = DictWebBrowserStatus.Ready;
        public MDictionary Dict;
        public string Word = "";
        public SettingsViewModel vmSettings;
        public string Url;
        public WordsDictControl()
        {
            InitializeComponent();
        }
        public async Task SearchDict(string word)
        {
            Word = word;
            dictStatus = DictWebBrowserStatus.Ready;
            Url = Dict.UrlString(word, vmSettings.AutoCorrects.ToList());
            await Load();
        }
        async Task Load()
        {
            if (!wbDict.IsBrowserInitialized || string.IsNullOrEmpty(Url)) return;
            if (Dict.DICTTYPENAME == "OFFLINE")
            {
                wbDict.Load("about:blank");
                var html = await vmSettings.client.GetStringAsync(Url);
                var str = Dict.HtmlString(html, Word);
                wbDict.LoadLargeHtml(str);
            }
            else
            {
                wbDict.Load(Url);
                if (Dict.AUTOMATION != null)
                    dictStatus = DictWebBrowserStatus.Automating;
                else if (Dict.DICTTYPENAME == "OFFLINE-ONLINE")
                    dictStatus = DictWebBrowserStatus.Navigating;
            }
        }
        async void wbDict_IsBrowserInitializedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
                await Load();
        }
        async void wbDict_LoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {
            if (args.IsLoading) return;
            var frame = args.Browser.MainFrame;
            Dispatcher.Invoke(() => {
                var s = frame.Url;
                if (!s.StartsWith("data"))
                    tbURL.Text = s;
            });
            if (dictStatus == DictWebBrowserStatus.Ready) return;
            switch (dictStatus)
            {
                case DictWebBrowserStatus.Automating:
                    var s = Dict.AUTOMATION.Replace("{0}", Word);
                    frame.ExecuteJavaScriptAsync(s);
                    dictStatus = DictWebBrowserStatus.Ready;
                    if (Dict.DICTTYPENAME == "OFFLINE-ONLINE")
                        dictStatus = DictWebBrowserStatus.Navigating;
                    break;
                case DictWebBrowserStatus.Navigating:
                    var html = await frame.GetSourceAsync();
                    var str = Dict.HtmlString(html, Word);
                    dictStatus = DictWebBrowserStatus.Ready;
                    wbDict.LoadLargeHtml(str);
                    break;
            }
        }
        public void btnOpenURL_Click(object sender, RoutedEventArgs e) => Process.Start(tbURL.Text);
    }
}
