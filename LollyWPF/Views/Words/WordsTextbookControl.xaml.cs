using Dragablz;
using LollyCommon;
using ReactiveUI;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace LollyWPF
{
    /// <summary>
    /// WordsTextbookControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsTextbookControl : WordsBaseControl
    {
        WordsUnitViewModelWPF vm;
        protected override WordsBaseViewModel vmWords => vm;
        public override SettingsViewModel vmSettings => vm.vmSettings;
        protected override ToolBar ToolBarDictBase => ToolBarDict;
        protected override TabablzControl tcDictsBase => tcDicts;
        protected override DataGrid dgPhrasesBase => dgPhrases;
        MUnitWord SelectedWordItem => (MUnitWord)vm.SelectedWordItem;

        public WordsTextbookControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }
        public void btnRefresh_Click(object sender, RoutedEventArgs e) => vm.Reload();

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        void dgWords_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dgWords.CancelEdit();
            miEditWord_Click(sender, null);
        }

        void OnEndEditWord(object sender, DataGridCellEditEndingEventArgs e) =>
            OnEndEdit(sender, e, "WORD", async item => await vm.Update((MUnitWord)item));

        public override async Task OnSettingsChanged()
        {
            DataContext = vm = new WordsUnitViewModelWPF(MainWindow.vmSettings, inTextbook: false, needCopy: true);
            await base.OnSettingsChanged();
        }
        void miEditWord_Click(object sender, RoutedEventArgs e)
        {
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            var dlg = new WordsTextbookDetailDlg(Window.GetWindow(this), vm, SelectedWordItem);
            dlg.ShowDialog();
        }

        async void miDelete_Click(object sender, RoutedEventArgs e)
        {
            await vm.Delete(SelectedWordItem);
            vm.Reload();
        }
        async void miRetrieveNote_Click(object sender, RoutedEventArgs e) =>
            await vm.RetrieveNote(SelectedWordItem);
        async void miClearNote_Click(object sender, RoutedEventArgs e) =>
            await vm.ClearNote(SelectedWordItem);
    }
}
