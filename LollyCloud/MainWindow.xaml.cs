using Dragablz;
using LollyShared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public static SettingsViewModel vmSettings = new SettingsViewModel();
        ActionTabViewModal vmActionTabs;
        public ActionInterTabClient ActionInterTabClient { get; } = new ActionInterTabClient();

        public MainWindow()
        {
            InitializeComponent();
            // https://stackoverflow.com/questions/3145511/how-to-set-the-default-font-for-a-wpf-application
            Style = (Style)FindResource(typeof(Window));
            // https://stackoverflow.com/questions/43528152/how-to-close-tab-with-a-close-button-in-wpf
            // Initialize viewModel
            vmActionTabs = new ActionTabViewModal();
            // Bind the xaml TabControl to view model tabs
            tcMain.ItemsSource = vmActionTabs.Tabs;
            tcMain.DataContext = this;
            Init();
        }

        async void Init() => await vmSettings.GetData();

        async void miSettings_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SettingsDlg();
            dlg.Owner = this;
            dlg.ShowDialog();
            await vmSettings.GetData();
            if (dlg.Result == SettingsDlgResult.ApplyToNone) return;
            vmActionTabs.Tabs.ForEach((t, i) =>
            {
                if (dlg.Result == SettingsDlgResult.ApplyToAll || i == tcMain.SelectedIndex)
                    (t.Content as ILollySettings)?.OnSettingsChanged();
            });
        }

        void AddTab<TUserControl>(string header) where TUserControl : UserControl, new()
        {
            var i = vmActionTabs.Tabs.ToList().FindIndex(o => o.Content is TUserControl);
            if (i == -1)
            {
                vmActionTabs.Tabs.Add(new ActionTabItem { Header = header, Content = new TUserControl() });
                tcMain.SelectedIndex = tcMain.Items.Count - 1;
            }
            else
                tcMain.SelectedIndex = i;
        }

        void miSearch_Click(object sender, RoutedEventArgs e) => AddTab<WordsSearchControl>("Search");
        public void SearchWord(string word)
        {
            miSearch_Click(this, null);
            var c = (WordsSearchControl)vmActionTabs.Tabs[tcMain.SelectedIndex].Content;
            c.SearchWord(word);
        }
        void miWordsUnit_Click(object sender, RoutedEventArgs e) => AddTab<WordsUnitControl>("Words in Unit");
        void miPhrasesUnit_Click(object sender, RoutedEventArgs e) => AddTab<PhrasesUnitControl>("Phrases in Unit");
        void miWordsReview_Click(object sender, RoutedEventArgs e) => AddTab<WordsReviewControl>("Words Review");
        void miPhrasesReview_Click(object sender, RoutedEventArgs e) => AddTab<PhrasesReviewControl>("Phrases Review");
        void miWordsLang_Click(object sender, RoutedEventArgs e) => AddTab<WordsLangControl>("Words in Language");
        void miPhrasesLang_Click(object sender, RoutedEventArgs e) => AddTab<PhrasesLangControl>("Phrases in Language");
        void miWordsTextbook_Click(object sender, RoutedEventArgs e) => AddTab<WordsTextbookControl>("Words in Textbook");
        void miPhrasesTextbook_Click(object sender, RoutedEventArgs e) => AddTab<PhrasesTextbookControl>("Phrases in Textbook");
        void miPatterns_Click(object sender, RoutedEventArgs e) => AddTab<PatternsControl>("Patterns in Language");
        void miBlog_Click(object sender, RoutedEventArgs e) => AddTab<BlogControl>("Blog");
        void miReadNumber_Click(object sender, RoutedEventArgs e) => AddTab<ReadNumberControl>("Read Number");

        void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // This event will be thrown when on a close image clicked
            vmActionTabs.Tabs.RemoveAt(tcMain.SelectedIndex);
        }
    }
    // This class will be the Tab in the TabControl
    public class ActionTabItem
    {
        // This will be the text in the tab control
        public string Header { get; set; }
        // This will be the content of the tab control It is a UserControl whits you need to create manually
        public UserControl Content { get; set; }
    }
    /// view model for the TabControl To bind on
    public class ActionTabViewModal
    {
        // These Are the tabs that will be bound to the TabControl 
        public ObservableCollection<ActionTabItem> Tabs { get; set; }

        public ActionTabViewModal()
        {
            Tabs = new ObservableCollection<ActionTabItem>();
        }
    }
    // https://github.com/ButchersBoy/Dragablz/issues/13
    public class ActionInterTabClient : DefaultInterTabClient
    {
        public override TabEmptiedResponse TabEmptiedHandler(TabablzControl tabControl, Window window)
        {
            return TabEmptiedResponse.DoNothing;
        }
    }
}
