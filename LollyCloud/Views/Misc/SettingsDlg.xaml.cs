using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LollyCloud
{
    public enum SettingsDlgResult { ApplyToAll, ApplyToCurrent, ApplyToNone };
    /// <summary>
    /// SettingsDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingsDlg : Window
    {
        SettingsViewModel vm = new SettingsViewModel();
        public SettingsDlgResult Result { get; private set; }

        public SettingsDlg()
        {
            InitializeComponent();
            DataContext = vm;
            // https://stackoverflow.com/questions/339620/how-do-i-remove-minimize-and-maximize-from-a-resizable-window-in-wpf
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            Init();
        }

        async Task Init() => await vm.GetData();

        async void cbUnitFrom_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            await vm.UpdateUnitFrom();

        async void cbPartFrom_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            await vm.UpdatePartFrom();

        async void cbUnitTo_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            await vm.UpdateUnitTo();

        async void cbPartTo_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            await vm.UpdatePartTo();

        async void btnPrevious_Click(object sender, RoutedEventArgs e) =>
            await vm.PreviousUnitPart();

        async void btnNext_Click(object sender, RoutedEventArgs e) =>
            await vm.NextUnitPart();

        void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Result = (SettingsDlgResult)(sender as Button).Tag;
            Close();
        }

        void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SettingsDictsDlg();
            dlg.Owner = this;
            dlg.vmSettings = vm;
            dlg.ShowDialog();
        }
    }
}
