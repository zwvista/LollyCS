using LollyCommon;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// PhrasesLinkDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class PhrasesLinkDlg : Window
    {
        public PhrasesLinkViewModel vm;
        public PhrasesLinkDlg(Window owner, SettingsViewModel vmSettings, int wordid, string textFilter)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            Owner = owner;
            vm = new PhrasesLinkViewModel(vmSettings, wordid, textFilter);
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = vm;
        }

        void btnCheckItems_Click(object sender, RoutedEventArgs e)
        {
            int n = int.Parse((string)((Button)sender).Tag);
            var checkedItems = dgPhrases.SelectedItems.Cast<MLangPhrase>().ToList();
            vm.CheckItems(n, checkedItems);
        }
    }
}
