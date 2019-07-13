using LollyShared;
using MSHTML;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace LollyCloud
{
    /// <summary>
    /// WordsUnitControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsUnitControl : WordsBaseControl, ILollySettings
    {
        public WordsUnitViewModel vm { get; set; }
        public override DataGrid dgWordsBase { get => dgWords; }
        public override MWordInterface ItemForRow(int row) { return vm.Items[row]; }
        public override SettingsViewModel vmSettings { get => vm.vmSettings; }
        public override WebBrowser wbDictBase { get => wbDict; }

        public WordsUnitControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        void dgWords_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dlg = new WordsUnitDetailDlg();
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            dlg.Owner = Window.GetWindow(this);
            dlg.itemOriginal = (sender as DataGridRow).Item as MUnitWord;
            dlg.vm = vm;
            dlg.ShowDialog();
        }

        void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new WordsUnitDetailDlg();
            dlg.Owner = Window.GetWindow(this);
            dlg.itemOriginal = vm.NewUnitWord();
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

        async void btnRefresh_Click(object sender, RoutedEventArgs e) => await OnSettingsChanged();

        public async Task OnSettingsChanged()
        {
            vm = await WordsUnitViewModel.CreateAsync(MainWindow.vmSettings, true);
            selectedDictItemIndex = vmSettings.SelectedDictItemIndex;
            dgWords.ItemsSource = vm.Items;
            ToolBar1.Items.Clear();
            for (int i = 0; i < vmSettings.DictItems.Count; i++)
            {
                var b = new RadioButton
                {
                    Content = vmSettings.DictItems[i].DICTNAME,
                    GroupName = "DICT",
                    Tag = i,
                };
                b.Click += SearchDict;
                ToolBar1.Items.Add(b);
                if (i == selectedDictItemIndex)
                    b.IsChecked = true;
            }
        }

        async void miDelete_Click(object sender, RoutedEventArgs e)
        {
            var row = dgWords.SelectedIndex;
            if (row == -1) return;
            var item = vm.Items[row];
            await vm.Delete(item);
        }

        async void tbNewWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return || string.IsNullOrEmpty(vm.NewWord)) return;
            var item = vm.NewUnitWord();
            item.WORD = vmSettings.AutoCorrectInput(vm.NewWord);
            vm.NewWord = "";
            item.ID = await vm.Create(item);
            vm.Items.Add(item);
        }

        async Task ChangeLevel(int delta)
        {
            var row = dgWords.SelectedIndex;
            if (row == -1) return;
            var item = vm.Items[row];
            var newLevel = item.LEVEL + delta;
            if (newLevel != 0 && !vmSettings.USLEVELCOLORS.ContainsKey(newLevel)) return;
            item.LEVEL = newLevel;
            await vmSettings.UpdateLevel(item.WORDID, item.LEVEL);
        }

        async void dgWords_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Alt && (e.SystemKey == Key.Up || e.SystemKey == Key.Down))
            {
                await ChangeLevel(e.SystemKey == Key.Up ? 1 : -1);
                e.Handled = true;
            }
        }

        async void btnToggleToType_Click(object sender, RoutedEventArgs e)
        {
            var row = dgWords.SelectedIndex;
            var part = row == -1 ? vmSettings.Parts[0].Value : vm.Items[row].PART;
            await vmSettings.ToggleToType(part);
            btnRefresh_Click(sender, e);
        }
        async void btnPreviousUnitPart_Click(object sender, RoutedEventArgs e)
        {
            await vmSettings.PreviousUnitPart();
            btnRefresh_Click(sender, e);
        }
        async void btnNextUnitPart_Click(object sender, RoutedEventArgs e)
        {
            await vmSettings.NextUnitPart();
            btnRefresh_Click(sender, e);
        }
    }
}
