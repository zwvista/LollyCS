using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LollyCloud
{
    /// <summary>
    /// PhrasesUnitBatchDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class PhrasesUnitBatchDlg : Window
    {
        public PhrasesUnitBatchViewModel vmBatch;
        public PhrasesUnitBatchDlg(Window owner, PhrasesUnitViewModelWPF vm)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            Owner = owner;
            DataContext = vmBatch = new PhrasesUnitBatchViewModel(vm);
        }

        void btnCheckItems_Click(object sender, RoutedEventArgs e)
        {
            int n = int.Parse((string)((Button)sender).Tag);
            var checkedItems = dgWords.SelectedItems.Cast<MUnitPhrase>().ToList();
            vmBatch.CheckItems(n, checkedItems);
        }
    }
}
