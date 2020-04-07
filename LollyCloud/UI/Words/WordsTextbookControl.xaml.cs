using Dragablz;
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
        protected override string NewWord => vm.NewWord;
        public override DataGrid dgWordsBase => dgWords;
        public override MWordInterface ItemForRow(int row) => vm.WordItems[row];
        public override SettingsViewModel vmSettings => vm.vmSettings;
        public override ToolBar ToolBarDictBase => ToolBarDict;
        public override TabablzControl tcDictsBase => tcDicts;

        public WordsTextbookControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }
        public void btnRefresh_Click(object sender, RoutedEventArgs e) => vm.Reload();

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

        void OnBeginEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var o = e.EditingEventArgs.Source;
            var o2 = (TextBlock)((o as DataGridCell)?.Content ?? o);
            originalText = o2.Text;
        }

        async void OnEndEdit(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var text = ((TextBox)e.EditingElement).Text;
                if (text != originalText)
                {
                    var item = vm.WordItems[e.Row.GetIndex()];
                    await vm.Update(item);
                }
                dgWords.CancelEdit(DataGridEditingUnit.Row);
            }
        }

        public override async Task LevelChanged(int row)
        {
            var item = vm.WordItems[row];
            await vmSettings.UpdateLevel(item.WORDID, item.LEVEL);
        }

        public override async Task OnSettingsChanged()
        {
            vm = new WordsUnitViewModel(MainWindow.vmSettings, inTextbook: false, needCopy: true);
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
    }
}
