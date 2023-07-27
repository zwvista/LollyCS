using CefSharp;
using LollyCommon;
using ReactiveUI;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// BlogPostEditControl.xaml の相互作用ロジック
    /// </summary>
    public partial class BlogPostEditControl : UserControl, ILollySettings
    {
        BlogPostEditViewModel vm;
        MLangBlogPostContent itemBlog;

        public BlogPostEditControl(MLangBlogPostContent itemBlog)
        {
            InitializeComponent();
            // Disable image loading
            // wbBlog.BrowserSettings.ImageLoading = CefState.Disabled;
            this.itemBlog = itemBlog;
            OnSettingsChanged();
        }

        public async Task OnSettingsChanged()
        {
            DataContext = vm = new BlogPostEditViewModel(MainWindow.vmSettings, true, itemBlog);
            tbMarked.Text = await vm.LoadBlog();
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
            w.SearchNewWord(text);
        }
        void btnMarkedToHtml_Click(object sender, RoutedEventArgs e)
        {
            var str = vm.MarkedToHtml();
            wbBlog.LoadLargeHtml(str);
            Clipboard.SetDataObject(vm.HtmlText);
        }
        void btnPatternToHtml_Click(object sender, RoutedEventArgs e) =>
            wbBlog.Load(vm.PatternUrl);
        async void btnSave_Click(object sender, RoutedEventArgs e) =>
            await vm.SaveBlog(tbMarked.Text);
    }
}
