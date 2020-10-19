using LollyCommon;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LollyCloud
{
    /// <summary>
    /// WordsSelectUnitDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsSelectUnitDlg : Window
    {
        public WordsSelectViewModel vm;
        public WordsSelectUnitDlg(Window owner, SettingsViewModel vmSettings, int phraseid, string textFilter)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            Owner = owner;
            DataContext = vm = new WordsSelectViewModel(vmSettings, phraseid, textFilter);
        }

        void btnCheckItems_Click(object sender, RoutedEventArgs e)
        {
            int n = int.Parse((string)((Button)sender).Tag);
            var selectedItems = dgWords.SelectedItems.Cast<MUnitWord>().ToList();
            vm.CheckItems(n, selectedItems);
        }
    }
}
