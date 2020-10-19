using LollyCommon;
using ReactiveUI.Fody.Helpers;
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
        WordsSelectUnitViewModel vm;
        public WordsSelectUnitDlg(Window owner, SettingsViewModel vmSettings, int phraseid, string textFilter)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            Owner = owner;
            vm = new WordsSelectUnitViewModel(vmSettings, phraseid, textFilter);
        }

        void btnCheckItems_Click(object sender, RoutedEventArgs e)
        {
            int n = int.Parse((string)((Button)sender).Tag);
            var selectedItems = dgWords.SelectedItems.Cast<MUnitWord>().ToList();
            vm.CheckItems(n, selectedItems);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = vm;
        }
    }
}
