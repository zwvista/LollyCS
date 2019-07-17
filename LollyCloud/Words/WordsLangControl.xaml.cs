using LollyShared;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// WordsLangControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsLangControl : WordsBaseControl
    {
        public WordsLangViewModel vm { get; set; }
        public override DataGrid dgWordsBase => dgWords;
        public override MWordInterface ItemForRow(int row) => vm.Items[row];
        public override SettingsViewModel vmSettings => vm.vmSettings;
        public override WebBrowser wbDictBase => wbDict;
        public override ToolBar ToolBarDictBase => ToolBarDict;

        public WordsLangControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        void dgWords_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dlg = new WordsLangDetailDlg();
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            dlg.Owner = Window.GetWindow(this);
            dlg.itemOriginal = (sender as DataGridRow).Item as MLangWord;
            dlg.vm = vm;
            dlg.ShowDialog();
        }

        void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new WordsLangDetailDlg();
            dlg.Owner = Window.GetWindow(this);
            dlg.itemOriginal = vm.NewLangWord();
            dlg.vm = vm;
            dlg.ShowDialog();
            vm.Items.Add(dlg.itemOriginal);
        }

        async void dgWords_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var item = vm.Items[e.Row.GetIndex()];
                await vm.Update(item);
            }
        }

        public async override Task LevelChanged(int row)
        {
            var item = vm.Items[row];
            await vmSettings.UpdateLevel(item.ID, item.LEVEL);
        }

        public override async Task OnSettingsChanged()
        {
            vm = await WordsLangViewModel.CreateAsync(MainWindow.vmSettings, needCopy: true);
            dgWords.ItemsSource = vm.Items;
            await base.OnSettingsChanged();
        }

        async void miDelete_Click(object sender, RoutedEventArgs e)
        {
            var row = dgWords.SelectedIndex;
            if (row == -1) return;
            var item = vm.Items[row];
            await vm.Delete(item.ID);
        }

        async void tbNewWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return || string.IsNullOrEmpty(vm.NewWord)) return;
            var item = vm.NewLangWord();
            item.WORD = vmSettings.AutoCorrectInput(vm.NewWord);
            vm.NewWord = "";
            item.ID = await vm.Create(item);
            vm.Items.Add(item);
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
