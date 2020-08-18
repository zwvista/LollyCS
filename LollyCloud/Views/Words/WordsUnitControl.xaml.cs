﻿using Dragablz;
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
        public WordsUnitViewModel vm { get; set; }
        protected override string NewWord => vm.NewWord;
        public override DataGrid dgWordsBase => dgWords;
        public override SettingsViewModel vmSettings => vm.vmSettings;
        public override ToolBar ToolBarDictBase => ToolBarDict;
        public override TabablzControl tcDictsBase => tcDicts;
        EmbeddedReviewViewModel vmReview = new EmbeddedReviewViewModel();

        public WordsUnitControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        void dgWords_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dgWords.CancelEdit();
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
            vm.Reload();
        }

        async void tbNewWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return || string.IsNullOrEmpty(vm.NewWord)) return;
            var item = vm.NewUnitWord();
            item.WORD = vmSettings.AutoCorrectInput(vm.NewWord);
            vm.NewWord = "";
            await vm.Create(item);
            vm.Add(item);
            dgWords.SelectedItem = vm.WordItems.Last();
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
            vmReview.Stop();
            var dlg = new ReviewOptionsDlg(Window.GetWindow(this), vmReview.Options);
            if (dlg.ShowDialog() == true)
            {
                var ids = vm.WordItems.Where(o => !vmReview.Options.Levelge0only || o.LEVEL >= 0).Select(o => o.ID).ToList();
                vmReview.Start(ids, id =>
                {
                    dgWords.SelectedItem = vm.WordItems.FirstOrDefault(o => o.ID == id);
                });
            }
        }
        public override async Task SearchPhrases() =>
            await vm.SearchPhrases(selectedWordID);

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
                var item = (MUnitWord)e.Row.DataContext;
                var el = (TextBox)e.EditingElement;
                if (((Binding)((DataGridBoundColumn)e.Column).Binding).Path.Path == "WORD")
                    el.Text = vm.vmSettings.AutoCorrectInput(el.Text);
                if (el.Text != originalText)
                    Observable.Timer(TimeSpan.FromMilliseconds(100), RxApp.MainThreadScheduler).Subscribe(async _ =>
                    {
                        await vm.Update(item);
                        dgWords.CancelEdit();
                    });
            }
        }

    }
}
