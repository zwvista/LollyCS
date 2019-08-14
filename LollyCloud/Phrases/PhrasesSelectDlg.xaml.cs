using LollyShared;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// PhrasesSelectDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class PhrasesSelectDlg : Window
    {
        public PhrasesUnitViewModel vm = new PhrasesUnitViewModel();
        public SettingsViewModel vmSettings => vm.vmSettings;
        UnitPhraseDataStore unitPhraseDS = new UnitPhraseDataStore();
        public PhrasesSelectDlg()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var o in vm.PhraseItems)
                o.IsChecked = false;
            //DataContext = vmBatch;
        }

        void btnCheckItems_Click(object sender, RoutedEventArgs e)
        {
            int n = int.Parse((string)((Button)sender).Tag);
            var checkedItems = dgWords.SelectedItems.Cast<MUnitPhrase>();
            foreach (var o in vm.PhraseItems)
                o.IsChecked = n == 0 ? true : n == 1 ? false :
                    !checkedItems.Contains(o) ? o.IsChecked :
                    n == 2;
        }

        async void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        void cbScopeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            vm.ApplyFilters();

        void cbTextbookFilter_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            vm.ApplyFilters();

        void tbTextFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return) return;
            if (string.IsNullOrEmpty(vm.TextFilter))
                vm.ScopeFilter = SettingsViewModel.ScopePhraseFilters[0];
            else if (vm.ScopeFilter == SettingsViewModel.ScopePhraseFilters[0])
                vm.ScopeFilter = SettingsViewModel.ScopePhraseFilters[1];
            vm.ApplyFilters();
        }

        void btnCancel_Click(object sender, RoutedEventArgs e) => Close();
    }
}
