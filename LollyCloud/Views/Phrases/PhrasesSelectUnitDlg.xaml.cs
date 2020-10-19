using LollyCommon;
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
        public PhrasesSelectUnitViewModel vm;
        public PhrasesSelectUnitDlg(Window owner, SettingsViewModel vmSettings, int wordid, string textFilter)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            Owner = owner;
            DataContext = vm = new PhrasesSelectUnitViewModel(vmSettings, wordid, textFilter);
        }

        void btnCheckItems_Click(object sender, RoutedEventArgs e)
        {
            int n = int.Parse((string)((Button)sender).Tag);
            var checkedItems = dgPhrases.SelectedItems.Cast<MUnitPhrase>().ToList();
            vm.CheckItems(n, checkedItems);
        }
    }
}
