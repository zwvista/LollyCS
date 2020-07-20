using Dragablz;
using Hardcodet.Wpf.Util;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// WordsUnitControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsUnitControl : WordsBaseControl
    {
        public WordsUnitViewModel vm { get; set; }
        protected override string NewWord => vm.NewWord;
        public override DataGrid dgWordsBase => dgWords;
        public override MWordInterface ItemForRow(int row) => vm.WordItems[row];
        public override SettingsViewModel vmSettings => vm.vmSettings;
        public override ToolBar ToolBarDictBase => ToolBarDict;
        public override TabablzControl tcDictsBase => tcDicts;
        public MReviewOptions Options { get; set; } = new MReviewOptions();

        public WordsUnitControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        void dgWords_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            var dlg = new WordsUnitDetailDlg(Window.GetWindow(this), (MUnitWord)((DataGridRow)sender).Item, vm);
            dlg.ShowDialog();
        }

        void btnBatch_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new WordsUnitBatchDlg(Window.GetWindow(this), vm);
            dlg.ShowDialog();
        }

        void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new WordsUnitDetailDlg(Window.GetWindow(this), vm.NewUnitWord(), vm);
            if (dlg.ShowDialog() == true)
                vm.Add(dlg.Item);
        }
        public void btnRefresh_Click(object sender, RoutedEventArgs e) => vm.Reload();

        public override async Task OnSettingsChanged()
        {
            vm = new WordsUnitViewModel(MainWindow.vmSettings, inTextbook: true, needCopy: true);
            DataContext = vm;
            tcDicts.DataContext = this;
            await base.OnSettingsChanged();
        }

        async void miDelete_Click(object sender, RoutedEventArgs e)
        {
            var row = dgWords.SelectedIndex;
            if (row == -1) return;
            var item = vm.WordItems[row];
            await vm.Delete(item);
        }

        async void tbNewWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return || string.IsNullOrEmpty(vm.NewWord)) return;
            var item = vm.NewUnitWord();
            item.WORD = vmSettings.AutoCorrectInput(vm.NewWord);
            vm.NewWord = "";
            item.ID = await vm.Create(item);
            vm.Add(item);
            dgWords.SelectedItem = vm.WordItems.Last();
        }

        public async override Task LevelChanged(int row)
        {
            var item = vm.WordItems[row];
            await vmSettings.UpdateLevel(item.WORDID, item.LEVEL);
        }

        async void btnToggleToType_Click(object sender, RoutedEventArgs e)
        {
            var row = dgWords.SelectedIndex;
            var part = row == -1 ? vmSettings.Parts[0].Value : vm.WordItems[row].PART;
            await vmSettings.ToggleToType(part);
            vm.Reload();
        }
        async void btnPreviousUnitPart_Click(object sender, RoutedEventArgs e)
        {
            await vmSettings.PreviousUnitPart();
            vm.Reload();
        }
        async void btnNextUnitPart_Click(object sender, RoutedEventArgs e)
        {
            await vmSettings.NextUnitPart();
            vm.Reload();
        }
        async void miGetNote_Click(object sender, RoutedEventArgs e)
        {
            var row = dgWords.SelectedIndex;
            if (row == -1) return;
            await vm.GetNote(row);
        }
        async void miClearNote_Click(object sender, RoutedEventArgs e)
        {
            var row = dgWords.SelectedIndex;
            if (row == -1) return;
            await vm.ClearNote(row);
        }
        async void btnGetNotes_Click(object sender, RoutedEventArgs e) =>
            await vm.GetNotes(_ => { });
        async void btnClearNotes_Click(object sender, RoutedEventArgs e) =>
            await vm.ClearNotes(_ => { });
        void btnReview_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new ReviewOptionsDlg();
            dlg.Owner = UIHelpers.TryFindParent<Window>(this);
            dlg.optionsOriginal = Options;
            if (dlg.ShowDialog() == true)
            {
            }
        }
        public override async Task SearchPhrases() =>
            await vm.SearchPhrases(selectedWordID);

        void OnBeginEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            vm.IsEditing = true;
            var o = e.EditingEventArgs.Source;
            var o2 = (TextBlock)((o as DataGridCell)?.Content ?? o);
            originalText = o2.Text;
        }
        async void OnEndEdit(object sender, DataGridCellEditEndingEventArgs e)
        {
            vm.IsEditing = false;
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var item = e.Row.DataContext as MUnitWord;
                var text = ((TextBox)e.EditingElement).Text;
                if (((Binding)((DataGridTextColumn)e.Column).Binding).Path.Path == "WORD")
                    text = item.WORD = vm.vmSettings.AutoCorrectInput(text);
                if (text != originalText)
                    await vm.Update(item);
                dgWords.CancelEdit(DataGridEditingUnit.Row);
            }
        }

    }
}
