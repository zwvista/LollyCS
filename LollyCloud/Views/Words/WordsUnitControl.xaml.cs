﻿using Dragablz;
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
    /// WordsUnitControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsUnitControl : WordsBaseControl
    {
        WordsUnitViewModelWPF vm;
        protected override WordsBaseViewModel vmWords => vm;
        public override SettingsViewModel vmSettings => vm.vmSettings;
        protected override ToolBar ToolBarDictBase => ToolBarDict;
        protected override TabablzControl tcDictsBase => tcDicts;
        protected override DataGrid dgPhrasesBase => dgPhrases;
        EmbeddedReviewViewModel vmReview = new EmbeddedReviewViewModel();
        MUnitWord SelectedWordItem => (MUnitWord)vm.SelectedWordItem;

        public WordsUnitControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        void dgWords_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dgWords.CancelEdit();
            // https://stackoverflow.com/questions/14178800/how-can-i-check-if-ctrl-alt-are-pressed-on-left-mouse-click-in-c
            if (Keyboard.IsKeyDown(Key.LeftAlt))
                miSelectPhrase_Click(sender, null);
            else
                miEditWord_Click(sender, null);
        }

        void btnBatch_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new WordsUnitBatchDlg(Window.GetWindow(this), vm);
            dlg.ShowDialog();
        }

        void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new WordsUnitDetailDlg(Window.GetWindow(this), vm, vm.NewUnitWord());
            dlg.ShowDialog();
        }
        public void btnRefresh_Click(object sender, RoutedEventArgs e) => vm.Reload();

        public override async Task OnSettingsChanged()
        {
            DataContext = vm = new WordsUnitViewModelWPF(MainWindow.vmSettings, inTextbook: true, needCopy: true);
            tcDicts.DataContext = this;
            await base.OnSettingsChanged();
        }
        void miEditWord_Click(object sender, RoutedEventArgs e)
        {
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            var dlg = new WordsUnitDetailDlg(Window.GetWindow(this), vm, SelectedWordItem);
            dlg.ShowDialog();
        }
        void miNewPhrase_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PhrasesSelectUnitDlg(Window.GetWindow(this), vmSettings, vm.SelectedWordID, vm.SelectedWord);
            dlg.ShowDialog();
        }
        void miLinkPhrases_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PhrasesSelectUnitDlg(Window.GetWindow(this), vmSettings, vm.SelectedWordID, vm.SelectedWord);
            dlg.ShowDialog();
        }

        async void miDelete_Click(object sender, RoutedEventArgs e)
        {
            await vm.Delete(SelectedWordItem);
            vm.Reload();
        }

        async void tbNewWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return || string.IsNullOrEmpty(vm.NewWord)) return;
            var item = vm.NewUnitWord();
            item.WORD = vmSettings.AutoCorrectInput(vm.NewWord);
            vm.NewWord = "";
            await vm.Create(item);
            dgWords.SelectedItem = vm.WordItems.Last();
        }

        async void btnToggleToType_Click(object sender, RoutedEventArgs e)
        {
            var part = SelectedWordItem == null ? vmSettings.Parts[0].Value : SelectedWordItem.PART;
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
        async void miRetrieveNote_Click(object sender, RoutedEventArgs e) =>
            await vm.RetrieveNote(SelectedWordItem);
        async void miClearNote_Click(object sender, RoutedEventArgs e) =>
            await vm.ClearNote(SelectedWordItem);
        async void btnRetrieveNotes_Click(object sender, RoutedEventArgs e) =>
            await vm.RetrieveNotes(_ => { });
        async void btnClearNotes_Click(object sender, RoutedEventArgs e) =>
            await vm.ClearNotes(_ => { });
        void btnReview_Click(object sender, RoutedEventArgs e)
        {
            vmReview.Stop();
            var dlg = new ReviewOptionsDlg(Window.GetWindow(this), vmReview.Options);
            if (dlg.ShowDialog() == true)
            {
                var ids = vm.WordItems.Select(o => o.ID).ToList();
                vmReview.Start(ids, id =>
                {
                    dgWords.SelectedItem = vm.WordItems.FirstOrDefault(o => o.ID == id);
                });
            }
        }
        void OnEndEditWord(object sender, DataGridCellEditEndingEventArgs e) =>
            OnEndEdit(sender, e, "WORD", async item => await vm.Update((MUnitWord)item));
    }
}
