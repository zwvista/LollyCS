using CefSharp;
using LollyCommon;
using ReactiveUI;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LollyWPF
{
    /// <summary>
    /// BlogPostEditControl.xaml の相互作用ロジック
    /// </summary>
    public partial class BlogPostEditControl : UserControl, ILollySettings
    {
        BlogPostEditViewModel vm = null!;
        MLangBlogPostContent itemPost;

        public BlogPostEditControl(MLangBlogPostContent itemPost)
        {
            InitializeComponent();
            // Disable image loading
            // wbPost.BrowserSettings.ImageLoading = CefState.Disabled;
            this.itemPost = itemPost;
            _ = OnSettingsChanged();
        }

        public async Task OnSettingsChanged()
        {
            DataContext = vm = new BlogPostEditViewModel(MainWindow.vmSettings, true, itemPost);
            tbMarked.Text = await vm.LoadBlog();
            MarkedToHtml();
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
            MarkedToHtml();
            Clipboard.SetDataObject(vm.HtmlText);
        }
        void MarkedToHtml()
        {
            var str = vm.MarkedToHtml();
            wbPost.LoadHtml(str);
        }
        void btnPatternToHtml_Click(object sender, RoutedEventArgs e) =>
            wbPost.Load(vm.PatternUrl);
        async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            await vm.SaveBlog(tbMarked.Text);
            btnMarkedToHtml_Click(sender, e);
        }
    }
}
