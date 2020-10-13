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

namespace LollyCloud
{
    /// <summary>
    /// PhrasesLangControl.xaml の相互作用ロジック
    /// </summary>
    public partial class PhrasesLangControl : PhrasesBaseControl
    {
        public PhrasesLangViewModel vm { get; set; }
        public override SettingsViewModel vmSettings => vm.vmSettings;
        public override ToolBar ToolBarDictBase => ToolBarDict;
        public override TabablzControl tcDictsBase => tcDicts;
        public override DataGrid dgPhrasesBase => dgPhrases;
        public override DataGrid dgWordsBase => dgWords;

        public PhrasesLangControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        void dgPhrases_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dgPhrases.CancelEdit();
            var item = (MLangPhrase)((DataGridRow)sender).Item;
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            var dlg = new PhrasesLangDetailDlg(Window.GetWindow(this), vm, item);
            dlg.ShowDialog();
        }

        void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PhrasesLangDetailDlg(Window.GetWindow(this), vm, vm.NewLangPhrase());
            dlg.ShowDialog();
        }
        public void btnRefresh_Click(object sender, RoutedEventArgs e) => vm.Reload();

        void OnBeginEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var o = e.EditingEventArgs.Source;
            var o2 = (TextBlock)((o as DataGridCell)?.Content ?? o);
            originalText = o2.Text;
        }

        void OnEndEdit(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var item = (MLangPhrase)e.Row.DataContext;
                var el = (TextBox)e.EditingElement;
                if (((Binding)((DataGridBoundColumn)e.Column).Binding).Path.Path == "PHRASE")
                    el.Text = vm.vmSettings.AutoCorrectInput(el.Text);
                if (el.Text != originalText)
                    Observable.Timer(TimeSpan.FromMilliseconds(100), RxApp.MainThreadScheduler).Subscribe(async _ =>
                    {
                        await vm.Update(item);
                        dgPhrases.CancelEdit();
                    });
            }
        }

        public override async Task OnSettingsChanged()
        {
            vm = new PhrasesLangViewModel(MainWindow.vmSettings, needCopy: true);
            DataContext = vm;
            await base.OnSettingsChanged();
        }

        async void miDelete_Click(object sender, RoutedEventArgs e)
        {
            var row = dgPhrases.SelectedIndex;
            if (row == -1) return;
            var item = vm.PhraseItems[row];
            await vm.Delete(item);
            vm.Reload();
        }
        public override async Task SearchWords() =>
            await vm.SearchWords(selectedPhraseID);
    }
}
