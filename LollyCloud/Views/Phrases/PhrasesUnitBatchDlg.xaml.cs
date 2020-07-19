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
        public PhrasesUnitBatchViewModel vmBatch = new PhrasesUnitBatchViewModel();
        public SettingsViewModel vmSettings => vmBatch.vm.vmSettings;
        UnitPhraseDataStore unitPhraseDS = new UnitPhraseDataStore();
        public PhrasesUnitBatchDlg()
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var o in vmBatch.vm.PhraseItems)
                o.IsChecked = false;
            DataContext = vmBatch;
        }

        void btnCheckItems_Click(object sender, RoutedEventArgs e)
        {
            int n = int.Parse((string)((Button)sender).Tag);
            var checkedItems = dgWords.SelectedItems.Cast<MUnitPhrase>();
            foreach (var o in vmBatch.vm.PhraseItems)
                o.IsChecked = n == 0 ? true : n == 1 ? false :
                    !checkedItems.Contains(o) ? o.IsChecked :
                    n == 2;
        }

        async void btnOK_Click(object sender, RoutedEventArgs e)
        {
            foreach (var o in vmBatch.vm.PhraseItems)
                if (vmBatch.IsUnitChecked || vmBatch.IsPartChecked || vmBatch.IsSeqNumChecked)
                {
                    if (vmBatch.IsUnitChecked) o.UNIT = vmBatch.UNIT;
                    if (vmBatch.IsPartChecked) o.PART = vmBatch.PART;
                    if (vmBatch.IsSeqNumChecked) o.SEQNUM += vmBatch.SEQNUM;
                    await unitPhraseDS.Update(o);
                }
            DialogResult = true;
        }
    }
}
