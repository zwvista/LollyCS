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
        public WordsUnitBatchViewModel vmBatch = new WordsUnitBatchViewModel();
        public SettingsViewModel vmSettings => vmBatch.vm.vmSettings;
        UnitWordDataStore unitWordDS = new UnitWordDataStore();
        WordFamiDataStore wordFamiDS = new WordFamiDataStore();
        public WordsUnitBatchDlg()
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var o in vmBatch.vm.WordItems)
                o.IsChecked = false;
            DataContext = vmBatch;
        }

        void btnCheckItems_Click(object sender, RoutedEventArgs e)
        {
            int n = int.Parse((string)((Button)sender).Tag);
            var checkedItems = dgWords.SelectedItems.Cast<MUnitWord>();
            foreach (var o in vmBatch.vm.WordItems)
                o.IsChecked = n == 0 ? true : n == 1 ? false :
                    !checkedItems.Contains(o) ? o.IsChecked :
                    n == 2;
        }

        async void btnOK_Click(object sender, RoutedEventArgs e)
        {
            foreach (var o in vmBatch.vm.WordItems)
            {
                if (vmBatch.IsUnitChecked || vmBatch.IsPartChecked || vmBatch.IsSeqNumChecked)
                {
                    if (vmBatch.IsUnitChecked) o.UNIT = vmBatch.UNIT;
                    if (vmBatch.IsPartChecked) o.PART = vmBatch.PART;
                    if (vmBatch.IsSeqNumChecked) o.SEQNUM += vmBatch.SEQNUM;
                    await unitWordDS.Update(o);
                }
                if (vmBatch.IsLevelChecked && (!vmBatch.IsLevel0OnlyChecked || o.LEVEL == 0))
                    await wordFamiDS.Update(o.WORDID, vmBatch.LEVEL);
            }
            DialogResult = true;
        }
    }
}
