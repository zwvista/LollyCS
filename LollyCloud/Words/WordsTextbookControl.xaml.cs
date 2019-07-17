using LollyShared;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// WordsTextbookControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsTextbookControl : WordsBaseControl
    {
        public WordsUnitViewModel vm { get; set; }
        public override DataGrid dgWordsBase => dgWords;
        public override MWordInterface ItemForRow(int row) => vm.Items[row];
        public override SettingsViewModel vmSettings => vm.vmSettings;
        public override WebBrowser wbDictBase => wbDict;
        public override ToolBar ToolBarDictBase => ToolBarDict;

        public WordsTextbookControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        void dgWords_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dlg = new WordsTextbookDetailDlg();
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            dlg.Owner = Window.GetWindow(this);
            dlg.itemOriginal = (sender as DataGridRow).Item as MUnitWord;
            dlg.vm = vm;
            dlg.ShowDialog();
        }

        async void dgWords_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var item = vm.Items[e.Row.GetIndex()];
                await vm.Update(item);
            }
        }

        public override async Task LevelChanged(int row)
        {
            var item = vm.Items[row];
            await vmSettings.UpdateLevel(item.WORDID, item.LEVEL);
        }

        public override async Task OnSettingsChanged()
        {
            vm = await WordsUnitViewModel.CreateAsync(MainWindow.vmSettings, inTextbook: false, needCopy: true);
            selectedDictItemIndex = vmSettings.SelectedDictItemIndex;
            await base.OnSettingsChanged();
        }

        async void miDelete_Click(object sender, RoutedEventArgs e)
        {
            var row = dgWords.SelectedIndex;
            if (row == -1) return;
            var item = vm.Items[row];
            await vm.Delete(item);
        }

        private void CbScopeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            vm.ApplyFilters();

        private void ChkLevelge0only_Click(object sender, RoutedEventArgs e) =>
            vm.ApplyFilters();

        void tbTextFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return) return;
            if (string.IsNullOrEmpty(vm.TextFilter))
                vm.ScopeFilter = SettingsViewModel.ScopeWordFilters[0];
            else if (vm.ScopeFilter == SettingsViewModel.ScopeWordFilters[0])
                vm.ScopeFilter = SettingsViewModel.ScopeWordFilters[1];
            vm.ApplyFilters();
        }
    }
}
