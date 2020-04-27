using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// PhrasesSelectLangDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class PhrasesSelectUnitDlg : Window
    {
        public PhrasesUnitViewModel vm;
        public SettingsViewModel vmSettings => vm.vmSettings;
        UnitPhraseDataStore unitPhraseDS = new UnitPhraseDataStore();
        public PhrasesSelectUnitDlg()
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
            var checkedItems = dgPhrases.SelectedItems.Cast<MUnitPhrase>();
            foreach (var o in vm.PhraseItems)
                o.IsChecked = n == 0 ? true : n == 1 ? false :
                    !checkedItems.Contains(o) ? o.IsChecked :
                    n == 2;
        }

        async void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        void btnCancel_Click(object sender, RoutedEventArgs e) => Close();
    }
}
