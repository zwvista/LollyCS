using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// PhrasesUnitControl.xaml の相互作用ロジック
    /// </summary>
    public partial class PhrasesTextbookControl : PhrasesBaseControl
    {
        public PhrasesUnitViewModel vm { get; set; }
        public override SettingsViewModel vmSettings => vm.vmSettings;
        public override DataGrid dgPhrasesBase => dgPhrases;
        public override MPhraseInterface ItemForRow(int row) => vm.PhraseItems[row];

        public PhrasesTextbookControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        void dgPhrases_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dlg = new PhrasesTextbookDetailDlg();
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            dlg.Owner = Window.GetWindow(this);
            dlg.Item = (sender as DataGridRow).Item as MUnitPhrase;
            dlg.vm = vm;
            dlg.ShowDialog();
        }

        void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PhrasesTextbookDetailDlg();
            dlg.Owner = Window.GetWindow(this);
            dlg.Item = vm.NewUnitPhrase();
            dlg.vm = vm;
            dlg.ShowDialog();
            vm.Add(dlg.Item);
        }
        public void btnRefresh_Click(object sender, RoutedEventArgs e) => vm.Reload();

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
                var item = e.Row.DataContext as MUnitPhrase;
                var text = ((TextBox)e.EditingElement).Text;
                if (((Binding)((DataGridTextColumn)e.Column).Binding).Path.Path == "PHRASE")
                    text = item.PHRASE = vm.vmSettings.AutoCorrectInput(text);
                if (text != originalText)
                    await vm.Update(item);
                dgPhrases.CancelEdit(DataGridEditingUnit.Row);
            }
        }

        public override async Task OnSettingsChanged()
        {
            vm = new PhrasesUnitViewModel(MainWindow.vmSettings, inTextbook: false, needCopy: true);
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
    }
}
