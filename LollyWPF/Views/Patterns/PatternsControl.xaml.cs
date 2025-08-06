using LollyCommon;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace LollyWPF
{
    /// <summary>
    /// PatternsControl.xaml の相互作用ロジック
    /// </summary>
    public partial class PatternsControl : UserControl, ILollySettings
    {
        public PatternsViewModel vm { get; set; } = null!;
        public string originalText = "";
        public SettingsViewModel vmSettings => vm.vmSettings;

        public PatternsControl()
        {
            InitializeComponent();
            _ = OnSettingsChanged();
        }

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        void dgPatterns_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dgPatterns.CancelEdit();
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            var dlg = new PatternsDetailDlg(Window.GetWindow(this), vm, (MPattern)((DataGridRow)sender).Item);
            dlg.ShowDialog();
        }

        void btnAddPattern_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PatternsDetailDlg(Window.GetWindow(this), vm, vm.NewPattern());
            dlg.ShowDialog();
        }

        void dgPatterns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            wbWebPage.Load(vm.SelectedPatternItem.URL);
        }
        void btnRefresh_Click(object sender, RoutedEventArgs e) => vm.Reload();

        void OnBeginEdit(object sender, DataGridBeginningEditEventArgs e) =>
            originalText = DataGridHelper.OnBeginEditCell(e);

        void OnEndEdit(object sender, DataGridCellEditEndingEventArgs e) =>
            DataGridHelper.OnEndEditCell(sender, e, originalText, vmSettings, "PATTERN", async item =>
            {
                await vm.Update((MPattern)item);
            });

        public async Task OnSettingsChanged()
        {
            DataContext = vm = new PatternsViewModel(MainWindow.vmSettings, needCopy: true);
        }

        async void miDelete_Click(object sender, RoutedEventArgs e)
        {
            var row = dgPatterns.SelectedIndex;
            if (row == -1) return;
            var item = vm.PatternItems[row];
            await vm.Delete(item.ID);
            vm.Reload();
        }
        void miCopy_Click(object sender, RoutedEventArgs e) => Clipboard.SetText(vm.SelectedPattern);

        void miGoogle_Click(object sender, RoutedEventArgs e) => vm.SelectedPattern.Google();

    }
}
