﻿using LollyShared;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LollyCloud
{
    /// <summary>
    /// PhrasesSelectDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class PhrasesSelectDlg : Window
    {
        public PhrasesUnitBatchViewModel vmBatch = new PhrasesUnitBatchViewModel();
        public SettingsViewModel vmSettings => vmBatch.vm.vmSettings;
        UnitPhraseDataStore unitPhraseDS = new UnitPhraseDataStore();
        public PhrasesSelectDlg()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var o in vmBatch.vm.Items)
                o.IsChecked = false;
            DataContext = vmBatch;
        }

        void btnCheckItems_Click(object sender, RoutedEventArgs e)
        {
            int n = int.Parse((string)((Button)sender).Tag);
            var checkedItems = dgWords.SelectedItems.Cast<MUnitPhrase>();
            foreach (var o in vmBatch.vm.Items)
                o.IsChecked = n == 0 ? true : n == 1 ? false :
                    !checkedItems.Contains(o) ? o.IsChecked :
                    n == 2;
        }

        async void btnOK_Click(object sender, RoutedEventArgs e)
        {
            foreach (var o in vmBatch.vm.Items)
                if (vmBatch.IsUnitChecked || vmBatch.IsPartChecked || vmBatch.IsSeqNumChecked)
                {
                    if (vmBatch.IsUnitChecked) o.UNIT = vmBatch.UNIT;
                    if (vmBatch.IsPartChecked) o.PART = vmBatch.PART;
                    if (vmBatch.IsSeqNumChecked) o.SEQNUM += vmBatch.SEQNUM;
                    await unitPhraseDS.Update(o);
                }
            Close();
        }

        void btnCancel_Click(object sender, RoutedEventArgs e) => Close();
    }
}
