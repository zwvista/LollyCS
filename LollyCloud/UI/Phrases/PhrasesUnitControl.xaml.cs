using Hardcodet.Wpf.Util;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// PhrasesUnitControl.xaml の相互作用ロジック
    /// </summary>
    public partial class PhrasesUnitControl : PhrasesBaseControl
    {
        public PhrasesUnitViewModel vm { get; set; }
        public override SettingsViewModel vmSettings => vm.vmSettings;
        public override DataGrid dgPhrasesBase => dgPhrases;
        public override MPhraseInterface ItemForRow(int row) => vm.PhraseItems[row];

        public PhrasesUnitControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }
        public void btnRefresh_Click(object sender, RoutedEventArgs e) => vm.Reload();

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        void dgPhrases_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dlg = new PhrasesUnitDetailDlg();
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            dlg.Owner = Window.GetWindow(this);
            dlg.itemOriginal = (sender as DataGridRow).Item as MUnitPhrase;
            dlg.vm = vm;
            dlg.ShowDialog();
        }

        void btnBatch_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PhrasesUnitBatchDlg();
            dlg.Owner = Window.GetWindow(this);
            dlg.vmBatch.vm = vm;
            dlg.ShowDialog();
        }

        void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PhrasesUnitDetailDlg();
            dlg.Owner = Window.GetWindow(this);
            dlg.itemOriginal = vm.NewUnitPhrase();
            dlg.vm = vm;
            if (dlg.ShowDialog() == true)
                vm.PhraseItems.Add(dlg.itemOriginal);
        }

        async void dgPhrases_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var item = vm.PhraseItems[e.Row.GetIndex()];
                await vm.Update(item);
            }
        }

        public override async Task OnSettingsChanged()
        {
            vm = new PhrasesUnitViewModel(MainWindow.vmSettings, inTextbook: true, needCopy: true);
            DataContext = vm;
            await base.OnSettingsChanged();
        }

        async void miDelete_Click(object sender, RoutedEventArgs e)
        {
            var row = dgPhrases.SelectedIndex;
            if (row == -1) return;
            var item = vm.PhraseItems[row];
            await vm.Delete(item);
        }

        async void btnToggleToType_Click(object sender, RoutedEventArgs e)
        {
            var row = dgPhrases.SelectedIndex;
            var part = row == -1 ? vmSettings.Parts[0].Value : vm.PhraseItems[row].PART;
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
                var text = ((TextBox)e.EditingElement).Text;
                if (text != originalText)
                {
                    var item = vm.PhraseItems[e.Row.GetIndex()];
                    await vm.Update(item);
                }
                dgPhrases.CancelEdit(DataGridEditingUnit.Row);
            }
        }

    }
}
