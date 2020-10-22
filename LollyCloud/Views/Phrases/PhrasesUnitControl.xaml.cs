using Dragablz;
using LollyCommon;
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
        public PhrasesUnitViewModelWPF vm { get; set; }
        protected override WordsPhrasesBaseViewModel vmWP => vm;
        public override SettingsViewModel vmSettings => vm.vmSettings;
        protected override ToolBar ToolBarDictBase => ToolBarDict;
        protected override TabablzControl tcDictsBase => tcDicts;
        EmbeddedReviewViewModel vmReview = new EmbeddedReviewViewModel();
        MUnitPhrase SelectedPhraseItem => (MUnitPhrase)vm.SelectedPhraseItem;

        public PhrasesUnitControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }
        public void btnRefresh_Click(object sender, RoutedEventArgs e) => vm.Reload();

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        void dgPhrases_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dgPhrases.CancelEdit();
            if (Keyboard.IsKeyDown(Key.LeftAlt))
                miSelectWord_Click(sender, null);
            else
                miEditPhrase_Click(sender, null);
        }

        void btnBatch_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PhrasesUnitBatchDlg(Window.GetWindow(this), vm);
            dlg.ShowDialog();
        }

        void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PhrasesUnitDetailDlg(Window.GetWindow(this), vm, vm.NewUnitPhrase());
            dlg.ShowDialog();
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
            DataContext = vm = new PhrasesUnitViewModelWPF(MainWindow.vmSettings, inTextbook: true, needCopy: true);
            tcDicts.DataContext = this;
            await base.OnSettingsChanged();
        }
        void miEditPhrase_Click(object sender, RoutedEventArgs e)
        {
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            var dlg = new PhrasesUnitDetailDlg(Window.GetWindow(this), vm, SelectedPhraseItem);
            dlg.ShowDialog();
        }
        void miSelectWord_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new WordsSelectUnitDlg(Window.GetWindow(this), vmSettings, vmWP.SelectedPhraseID, vmWP.SelectedPhrase);
            dlg.ShowDialog();
        }

        async void miDelete_Click(object sender, RoutedEventArgs e)
        {
            await vm.Delete(SelectedPhraseItem);
            vm.Reload();
        }

        async void btnToggleToType_Click(object sender, RoutedEventArgs e)
        {
            var part = SelectedPhraseItem == null ? vmSettings.Parts[0].Value : SelectedPhraseItem.PART;
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

        void OnEndEditPhrase(object sender, DataGridCellEditEndingEventArgs e) =>
            OnEndEdit(sender, e, "PHRASE", async item => await vm.Update((MUnitPhrase)item));
        void btnReview_Click(object sender, RoutedEventArgs e)
        {
            vmReview.Stop();
            var dlg = new ReviewOptionsDlg(Window.GetWindow(this), vmReview.Options);
            if (dlg.ShowDialog() == true)
            {
                var ids = vm.PhraseItems.Select(o => o.ID).ToList();
                vmReview.Start(ids, id =>
                {
                    dgPhrases.SelectedItem = vm.PhraseItems.FirstOrDefault(o => o.ID == id);
                });
            }
        }
        public override async Task GetWords()
        {
            await vm.GetWords(vmWP.SelectedPhraseID);
            if (vm.WordItems.Any())
                dgWords.SelectedIndex = 0;
        }
    }
}
