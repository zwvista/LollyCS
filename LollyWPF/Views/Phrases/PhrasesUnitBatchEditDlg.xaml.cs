using LollyCommon;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LollyWPF
{
    /// <summary>
    /// PhrasesUnitBatchEditDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class PhrasesUnitBatchEditDlg : Window
    {
        public PhrasesUnitBatchEditViewModel vmBatch;
        public PhrasesUnitBatchEditDlg(Window owner, PhrasesUnitViewModelWPF vm)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            Owner = owner;
            DataContext = vmBatch = new PhrasesUnitBatchEditViewModel(vm);
        }

        void btnCheckItems_Click(object sender, RoutedEventArgs e)
        {
            int n = int.Parse((string)((Button)sender).Tag);
            var checkedItems = dgWords.SelectedItems.Cast<MUnitPhrase>().ToList();
            vmBatch.CheckItems(n, checkedItems);
        }
    }
}
