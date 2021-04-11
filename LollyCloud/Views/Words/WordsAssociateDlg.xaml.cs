using LollyCommon;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// WordsAssociateDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsAssociateDlg : Window
    {
        public WordsAssociateViewModel vm;
        public WordsAssociateDlg(Window owner, SettingsViewModel vmSettings, int phraseid, string textFilter)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            Owner = owner;
            vm = new WordsAssociateViewModel(vmSettings, phraseid, textFilter);
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = vm;
        }

        void btnCheckItems_Click(object sender, RoutedEventArgs e)
        {
            int n = int.Parse((string)((Button)sender).Tag);
            var checkedItems = dgWords.SelectedItems.Cast<MLangWord>().ToList();
            vm.CheckItems(n, checkedItems);
        }
    }
}
