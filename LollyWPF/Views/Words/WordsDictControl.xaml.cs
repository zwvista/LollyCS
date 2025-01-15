using CefSharp;
using LollyCommon;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LollyWPF
{
    /// <summary>
    /// WordsDictControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsDictControl : UserControl, IOnlineDict
    {
        public DictWebBrowserStatus dictStatus = DictWebBrowserStatus.Ready;
        public SettingsViewModel vmSettings;
        public string Url => vmDict.Url;
        public OnlineDictViewModel vmDict { get; set; }
        public WordsDictControl(SettingsViewModel vmSettings, MDictionary Dict)
        {
            InitializeComponent();
            this.vmSettings = vmSettings;
            vmDict = new OnlineDictViewModel(vmSettings, this);
            vmDict.Dict = Dict;
        }
        public async Task SearchDict(string word)
        {
            vmDict.Word = word;
            await Load();
        }
        async Task Load()
        {
            if (!wbDict.IsBrowserInitialized) return;
            await vmDict.SearchDict();
        }
        async void wbDict_IsBrowserInitializedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
                await Load();
        }
        void wbDict_LoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {
            if (args.IsLoading) return;
            Dispatcher.Invoke(async () => {
                var s = args.Browser.MainFrame.Url;
                if (!s.StartsWith("data"))
                    tbURL.Text = s;
                await vmDict.OnNavigationFinished();
            });
        }
        public void btnOpenURL_Click(object sender, RoutedEventArgs e) => Process.Start(tbURL.Text);

        public void LoadURL(string url) =>
            wbDict.Load(url);

        public void LoadHtml(string html) =>
            wbDict.LoadHtml(html);

        public async Task EvaluateScriptAsync(string javascript) =>
            await wbDict.WebBrowser.GetMainFrame().EvaluateScriptAsync(javascript);

        public async Task<string> GetSourceAsync() =>
            await wbDict.WebBrowser.GetMainFrame().GetSourceAsync();
    }
}
