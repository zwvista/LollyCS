using LollyCommon;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
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
            DataContext = this;
            CommonApi.UserId = ConfigurationManager.AppSettings["userid"];
            if (string.IsNullOrEmpty(CommonApi.UserId))
                miLogout_Click(null, null);
            Task.Run(() => vmSettings.GetData()).Wait();
        }

        void ShowSettingsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var dlg = new SettingsDlg();
            dlg.Owner = this;
            dlg.ShowDialog();
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
        public void SearchNewWord(string word)
        {
            int i = tcMain.SelectedIndex;
            miSearch_Click(this, null);
            var c = (WordsSearchControl)Tabs[tcMain.SelectedIndex].Content;
            c.SearchNewWord(word);
            tcMain.SelectedIndex = i;
        }
        public void AddNewUnitPhrase(int wordid)
        {
            int i = tcMain.SelectedIndex;
            PhrasesUnitCommand_Executed(this, null);
            var c = (PhrasesUnitControl)Tabs[tcMain.SelectedIndex].Content;
            c.AddNewUnitPhrase(wordid);
            tcMain.SelectedIndex = i;
        }
        public void AddNewUnitWord(int phraseid)
        {
            int i = tcMain.SelectedIndex;
            WordsUnitCommand_Executed(this, null);
            var c = (WordsUnitControl)Tabs[tcMain.SelectedIndex].Content;
            c.AddNewUnitWord(phraseid);
            tcMain.SelectedIndex = i;
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
        void miUnitBlogPost_Click(object sender, RoutedEventArgs e) => AddBlogPostEditTab("Unit Blog Post", null);
        void miLangBlogGroups_Click(object sender, RoutedEventArgs e) => AddTab<LangBlogGroupsControl>("Language Blog Groups");
        void miLangBlogPosts_Click(object sender, RoutedEventArgs e) => AddTab<LangBlogPostsControl>("Language Blog Posts");
        void miReadNumber_Click(object sender, RoutedEventArgs e) => AddTab<ReadNumberControl>("Read Number");
        void miTextbooks_Click(object sender, RoutedEventArgs e) => AddTab<TextbooksControl>("Textbooks");
        void miWebTextbooks_Click(object sender, RoutedEventArgs e) => AddTab<WebTextbooksControl>("WebTextbooks");
        void miDictionaries_Click(object sender, RoutedEventArgs e) => AddTab<DictsControl>("Dictionaries");
        void miPatternCrawlers_Click(object sender, RoutedEventArgs e) => AddTab<PatternCrawlersControl>("Pattern Crawlers");
        void miTest_Click(object sender, RoutedEventArgs e) => AddTab<TestControl>("Test");
        void miLogout_Click(object sender, RoutedEventArgs e)
        {
            CommonApi.UserId = "";
            App.SaveUserId();
            Tabs.Clear();
            var dlg = new LoginDlg();
            dlg.ShowDialog();
        }
        void miExit_Click(object sender, RoutedEventArgs e) => Environment.Exit(0);
        public void AddBlogPostEditTab(string header, MLangBlogPostContent itemPost)
        {
            Tabs.Add(new ActionTabItem { Header = header, Content = new BlogPostEditControl(itemPost) });
            tcMain.SelectedIndex = tcMain.Items.Count - 1;
        }
    }
}
