using LollyCommon;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// WordsSelectLangDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsSelectLangDlg : Window
    {
        public WordsUnitViewModelWPF vm;
        public SettingsViewModel vmSettings => vm.vmSettings;
        UnitWordDataStore unitWordDS = new UnitWordDataStore();
        public WordsSelectLangDlg()
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var o in vm.WordItems)
                o.IsChecked = false;
            //DataContext = vmBatch;
        }

        void btnCheckItems_Click(object sender, RoutedEventArgs e)
        {
            int n = int.Parse((string)((Button)sender).Tag);
            var checkedItems = dgWords.SelectedItems.Cast<MUnitWord>();
            foreach (var o in vm.WordItems)
                o.IsChecked = n == 0 ? true : n == 1 ? false :
                    !checkedItems.Contains(o) ? o.IsChecked :
                    n == 2;
        }
    }
}
