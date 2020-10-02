using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LollyCloud
{
    /// <summary>
    /// WordsUnitBatchDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsUnitBatchDlg : Window
    {
        WordsUnitBatchViewModel vmBatch;
        public WordsUnitBatchDlg(Window owner, WordsUnitViewModelWPF vm)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            Owner = owner;
            DataContext = vmBatch = new WordsUnitBatchViewModel(vm);
        }

        void btnCheckItems_Click(object sender, RoutedEventArgs e)
        {
            int n = int.Parse((string)((Button)sender).Tag);
            var selectedItems = dgWords.SelectedItems.Cast<MUnitWord>().ToList();
            vmBatch.CheckItems(n, selectedItems);
        }
    }
}
