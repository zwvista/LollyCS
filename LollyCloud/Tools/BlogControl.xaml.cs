using LollyShared;
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
    public partial class BlogControl : ReactiveUserControl<BlogViewModel>, ILollySettings
    {
        public BlogControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        public async Task OnSettingsChanged()
        {
            ViewModel = new BlogViewModel(MainWindow.vmSettings, true);
            DataContext = ViewModel;
        }

        void ReplaceSelection(ReactiveCommand<string, string> cmd) =>
            cmd.Execute(tbMarked.SelectedText).Subscribe(str => tbMarked.SelectedText = str);
        void btnAddTagB_Click(object sender, RoutedEventArgs e) =>
            ReplaceSelection(ViewModel.AddTagBCommand);
        void btnAddTagI_Click(object sender, RoutedEventArgs e) =>
            ReplaceSelection(ViewModel.AddTagICommand);
        void btnRemoveTagBI_Click(object sender, RoutedEventArgs e) =>
            ReplaceSelection(ViewModel.RemoveTagBICommand);
        void btnExchangeTagBI_Click(object sender, RoutedEventArgs e) =>
            ReplaceSelection(ViewModel.ExchangeTagBICommand);
        void btnAddExplanation_Click(object sender, RoutedEventArgs e)
        {
            var text = Clipboard.GetText();
            ViewModel.GetExplanationCommand.Execute(text).Subscribe(str =>
            {
                tbMarked.SelectedText = str;
                var w = (MainWindow)Window.GetWindow(this);
                w.SearchWord(text);
            });
        }
        void btnMarkedToHtml_Click(object sender, RoutedEventArgs e) =>
            ViewModel.MarkedToHtmlCommand.Execute().Subscribe(str =>
            {
                wbBlog.NavigateToString(str);
                Clipboard.SetDataObject(ViewModel.HtmlText);
            });
        void btnPatternToHtml_Click(object sender, RoutedEventArgs e) =>
            wbBlog.Navigate(ViewModel.PatternUrl);
        void WbBlog_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e) =>
            wbBlog.SetSilent(true);
        void btnCopyPatternMarkDown_Click(object sender, RoutedEventArgs e) =>
            Clipboard.SetDataObject(ViewModel.PatternMarkDown);
    }
}
