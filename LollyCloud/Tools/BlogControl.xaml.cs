using LollyShared;
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
        public BlogViewModel vm { get; set; }

        public BlogControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        public async Task OnSettingsChanged() =>
            vm = new BlogViewModel(MainWindow.vmSettings, true);

        void btnHtmlToMarked_Click(object sender, RoutedEventArgs e) =>
            tbMarked.Text = vm.HtmlToMarked(tbHtml.Text);
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
        void btnAddExplanation_Click(object sender, RoutedEventArgs e) =>
            ReplaceSelection(_ => vm.explanation);
        void btnMarkedToHtml_Click(object sender, RoutedEventArgs e)
        {
            tbHtml.Text = vm.MarkedToHtml(tbMarked.Text);
            var str = vm.GetHtml(tbHtml.Text);
            wbBlog.NavigateToString(str);
            Clipboard.SetDataObject(tbHtml.Text);
        }
        void btnPatternToHtml_Click(object sender, RoutedEventArgs e)
        {
            var url = vm.GetPatternUrl(tbPatternNo.Text);
            wbBlog.Navigate(url);
        }
        void WbBlog_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e) =>
            wbBlog.SetSilent(true);
        void btnCopyPatternMarkDown_Click(object sender, RoutedEventArgs e)
        {
            var text = vm.GetPatternMarkDown(tbPatternText.Text);
            Clipboard.SetDataObject(text);
        }
        async void btnAddNotes_Click(object sender, RoutedEventArgs e) =>
            await vm.AddNotes(tbMarked.Text, s => tbMarked.Text = s);
    }
}
