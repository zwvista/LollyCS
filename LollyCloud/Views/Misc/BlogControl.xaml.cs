using CefSharp;
using ReactiveUI;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// BlogControl.xaml の相互作用ロジック
    /// </summary>
    public partial class BlogControl : UserControl, ILollySettings
    {
        BlogViewModel vm;

        public BlogControl()
        {
            InitializeComponent();
            // Disable image loading
            // wbBlog.BrowserSettings.ImageLoading = CefState.Disabled;
            OnSettingsChanged();
        }

        public async Task OnSettingsChanged()
        {
            vm = new BlogViewModel(MainWindow.vmSettings, true);
            DataContext = vm;
        }

        void ReplaceSelection(Func<string, string> f) =>
            tbMarked.SelectedText = f(tbMarked.SelectedText);
        void btnAddTagB_Click(object sender, RoutedEventArgs e) =>
            ReplaceSelection(vm.AddTagB);
        void btnAddTagI_Click(object sender, RoutedEventArgs e) =>
            ReplaceSelection(vm.AddTagI);
        void btnRemoveTagBI_Click(object sender, RoutedEventArgs e) =>
            ReplaceSelection(vm.RemoveTagBI);
        void btnExchangeTagBI_Click(object sender, RoutedEventArgs e) =>
            ReplaceSelection(vm.ExchangeTagBI);
        void btnAddExplanation_Click(object sender, RoutedEventArgs e)
        {
            var text = Clipboard.GetText();
            tbMarked.SelectedText = vm.GetExplanation(text);
            var w = (MainWindow)Window.GetWindow(this);
            w.SearchWord(text);
        }
        void btnMarkedToHtml_Click(object sender, RoutedEventArgs e)
        {
            var str = vm.MarkedToHtml();
            wbBlog.LoadHtml(str);
            Clipboard.SetDataObject(vm.HtmlText);
        }
        void btnPatternToHtml_Click(object sender, RoutedEventArgs e) =>
            wbBlog.Load(vm.PatternUrl);
        void btnCopyPatternMarkDown_Click(object sender, RoutedEventArgs e) =>
            Clipboard.SetDataObject(vm.PatternMarkDown);
    }
}
