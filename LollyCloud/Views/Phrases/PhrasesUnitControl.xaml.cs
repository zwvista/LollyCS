using ReactiveUI;
using System;
using System.Linq;
using System.Reactive.Linq;
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
    public partial class PhrasesUnitControl : PhrasesBaseControl
    {
        public PhrasesUnitViewModel vm { get; set; }
        public override SettingsViewModel vmSettings => vm.vmSettings;
        public override DataGrid dgPhrasesBase => dgPhrases;
        EmbeddedReviewViewModel vmReview = new EmbeddedReviewViewModel();

        public PhrasesUnitControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }
        public void btnRefresh_Click(object sender, RoutedEventArgs e) => vm.Reload();

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        void dgPhrases_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            var dlg = new PhrasesUnitDetailDlg(Window.GetWindow(this), (MUnitPhrase)((DataGridRow)sender).Item, vm);
            dlg.ShowDialog();
        }

        void btnBatch_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PhrasesUnitBatchDlg(Window.GetWindow(this), vm);
            dlg.ShowDialog();
        }

        void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PhrasesUnitDetailDlg(Window.GetWindow(this), vm.NewUnitPhrase(), vm);
            if (dlg.ShowDialog() == true)
                vm.PhraseItems.Add(dlg.Item);
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
            vm.Reload();
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

        void OnEndEdit(object sender, DataGridCellEditEndingEventArgs e)
        {
            vm.IsEditing = false;
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var item = (MUnitPhrase)e.Row.DataContext;
                var el = (TextBox)e.EditingElement;
                if (((Binding)((DataGridBoundColumn)e.Column).Binding).Path.Path == "PHRASE")
                    el.Text = vm.vmSettings.AutoCorrectInput(el.Text);
                if (el.Text != originalText)
                    Observable.Timer(TimeSpan.FromMilliseconds(100)).ObserveOn(RxApp.MainThreadScheduler).Subscribe(async _ =>
                    {
                        await vm.Update(item);
                        dgPhrases.CancelEdit(DataGridEditingUnit.Row);
                    });
            }
        }
        void btnReview_Click(object sender, RoutedEventArgs e)
        {
            if (vmReview.IsRunning)
                vmReview.Stop();
            else
            {
                var dlg = new ReviewOptionsDlg(Window.GetWindow(this), vmReview.Options);
                if (dlg.ShowDialog() == true)
                {
                    var items = vm.PhraseItems.ToList();
                    if (vmReview.Options.Shuffled)
                        items.Shuffle();
                    var ids = items.Select(o => o.ID).ToList();
                    vmReview.Start(ids, id =>
                    {
                        dgPhrases.SelectedItem = vm.PhraseItems.FirstOrDefault(o => o.ID == id);
                    });
                }
            }
        }

    }
}
