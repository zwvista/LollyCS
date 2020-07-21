using CefSharp;
using CefSharp.Wpf;
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
        // https://stackoverflow.com/questions/43528152/how-to-close-tab-with-a-close-button-in-wpf
        // These are the tabs that will be bound to the TabControl 
        public ObservableCollection<ActionTabItem> Tabs { get; } = new ObservableCollection<ActionTabItem>();
        public ActionInterTabClient ActionInterTabClient { get; } = new ActionInterTabClient();

        public static RoutedCommand ShowSettingsCommand = new RoutedCommand();
        public static RoutedCommand WordsUnitCommand = new RoutedCommand();
        public static RoutedCommand PhrasesUnitCommand = new RoutedCommand();

        public MainWindow()
        {
            InitializeComponent();
            Cef.Initialize(new CefSettings());
            // https://stackoverflow.com/questions/3145511/how-to-set-the-default-font-for-a-wpf-application
            DataContext = this;
            Init();
        }

        async void Init() => await vmSettings.GetData();

        async void ShowSettingsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var dlg = new SettingsDlg();
            dlg.Owner = this;
            dlg.ShowDialog();
            await vmSettings.GetData();
            if (dlg.Result == SettingsDlgResult.ApplyToNone) return;
            Tabs.ForEach((t, i) =>
            {
                if (dlg.Result == SettingsDlgResult.ApplyToAll || i == tcMain.SelectedIndex)
                    (t.Content as ILollySettings)?.OnSettingsChanged();
            });
        }

        void AddTab<TUserControl>(string header) where TUserControl : UserControl, new()
        {
            var i = Tabs.ToList().FindIndex(o => o.Content is TUserControl);
            if (i == -1)
            {
                Tabs.Add(new ActionTabItem { Header = header, Content = new TUserControl() });
                tcMain.SelectedIndex = tcMain.Items.Count - 1;
            }
            else
                tcMain.SelectedIndex = i;
        }

        void miSearch_Click(object sender, RoutedEventArgs e) => AddTab<WordsSearchControl>("Search");
        public void SearchWord(string word)
        {
            miSearch_Click(this, null);
            var c = (WordsSearchControl)Tabs[tcMain.SelectedIndex].Content;
            c.SearchWord(word);
        }
        void WordsUnitCommand_Executed(object sender, ExecutedRoutedEventArgs e) => AddTab<WordsUnitControl>("Words in Unit");
        void PhrasesUnitCommand_Executed(object sender, ExecutedRoutedEventArgs e) => AddTab<PhrasesUnitControl>("Phrases in Unit");
        void miWordsReview_Click(object sender, RoutedEventArgs e) => AddTab<WordsReviewControl>("Words Review");
        void miPhrasesReview_Click(object sender, RoutedEventArgs e) => AddTab<PhrasesReviewControl>("Phrases Review");
        void miWordsLang_Click(object sender, RoutedEventArgs e) => AddTab<WordsLangControl>("Words in Language");
        void miPhrasesLang_Click(object sender, RoutedEventArgs e) => AddTab<PhrasesLangControl>("Phrases in Language");
        void miWordsTextbook_Click(object sender, RoutedEventArgs e) => AddTab<WordsTextbookControl>("Words in Textbook");
        void miPhrasesTextbook_Click(object sender, RoutedEventArgs e) => AddTab<PhrasesTextbookControl>("Phrases in Textbook");
        void miPatterns_Click(object sender, RoutedEventArgs e) => AddTab<PatternsControl>("Patterns in Language");
        void miBlog_Click(object sender, RoutedEventArgs e) => AddTab<BlogControl>("Blog");
        void miReadNumber_Click(object sender, RoutedEventArgs e) => AddTab<ReadNumberControl>("Read Number");
        void miTextbooks_Click(object sender, RoutedEventArgs e) => AddTab<TextbooksControl>("Textbooks");
        void miTest_Click(object sender, RoutedEventArgs e) => AddTab<TestControl>("Test");

    }
}
