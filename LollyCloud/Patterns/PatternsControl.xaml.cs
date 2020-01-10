using LollyShared;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// PatternsControl.xaml の相互作用ロジック
    /// </summary>
    public partial class PatternsControl : UserControl, ILollySettings
    {
        public PatternsViewModel vm { get; set; }
        public string selectedPattern = "";
        public int selectedPatternID = 0;
        public string originalText = "";
        public SettingsViewModel vmSettings => vm.vmSettings;

        public PatternsControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        void dgPatterns_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dlg = new PatternsDetailDlg();
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            dlg.Owner = Window.GetWindow(this);
            dlg.itemOriginal = (sender as DataGridRow).Item as MPattern;
            dlg.vm = vm;
            dlg.ShowDialog();
        }

        void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PatternsDetailDlg();
            dlg.Owner = Window.GetWindow(this);
            dlg.itemOriginal = vm.NewPattern();
            dlg.vm = vm;
            dlg.ShowDialog();
            vm.PatternItems.Add(dlg.itemOriginal);
        }

        public async void dgPatterns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var row = dgPatterns.SelectedIndex;
            if (row == -1)
            {
                selectedPattern = "";
                selectedPatternID = 0;
            }
            else
            {
                var o = vm.PatternItems[row];
                selectedPattern = o.PATTERN;
                selectedPatternID = o.ID;
                await vm.GetWebPages(selectedPatternID);
                await SearchPhrase();
            }
        }
        public async void btnRefresh_Click(object sender, RoutedEventArgs e) => await OnSettingsChanged();

        void OnBeginEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            originalText = ((TextBlock)e.EditingEventArgs.Source).Text;
        }

        async void OnEndEdit(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var text = ((TextBox)e.EditingElement).Text;
                if (text != originalText)
                {
                    var item = vm.PatternItems[e.Row.GetIndex()];
                    await vm.Update(item);
                }
                dgPatterns.CancelEdit(DataGridEditingUnit.Row);
            }
        }

        void tbTextFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return) return;
            if (string.IsNullOrEmpty(vm.TextFilter))
                vm.ScopeFilter = SettingsViewModel.ScopePatternFilters[0];
            else if (vm.ScopeFilter == SettingsViewModel.ScopePatternFilters[0])
                vm.ScopeFilter = SettingsViewModel.ScopePatternFilters[1];
            vm.ApplyFilters();
        }

        void cbScopeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            vm.ApplyFilters();

        public async Task OnSettingsChanged()
        {
            vm = await PatternsViewModel.CreateAsync(MainWindow.vmSettings, needCopy: true);
            DataContext = this;
        }

        async void miDelete_Click(object sender, RoutedEventArgs e)
        {
            var row = dgPatterns.SelectedIndex;
            if (row == -1) return;
            var item = vm.PatternItems[row];
            await vm.Delete(item.ID);
        }
        public void miCopy_Click(object sender, RoutedEventArgs e) => Clipboard.SetText(selectedPattern);

        public void miGoogle_Click(object sender, RoutedEventArgs e) => CommonApi.GoogleString(selectedPattern);

        void dgWebPages_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dlg = new PatternsDetailDlg();
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            dlg.Owner = Window.GetWindow(this);
            dlg.itemOriginal = (sender as DataGridRow).Item as MPattern;
            dlg.vm = vm;
            dlg.ShowDialog();
        }

        public void dgWebPages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var row = dgPatterns.SelectedIndex;
            if (row == -1) return;
            selectedPattern = vm.PatternItems[row].PATTERN;
        }
        async Task SearchPhrase() =>
            await vm.SearchPhrases(selectedPatternID);
    }
}
