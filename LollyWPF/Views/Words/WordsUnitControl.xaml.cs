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

namespace LollyWPF
{
    /// <summary>
    /// WordsUnitControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsUnitControl : WordsBaseControl
    {
        WordsUnitViewModelWPF vm = null!;
        protected override WordsBaseViewModel vmWords => vm;
        public override SettingsViewModel vmSettings => vm.vmSettings;
        protected override ToolBar ToolBarDictBase => ToolBarDict;
        protected override TabablzControl tcDictsBase => tcDicts;
        protected override DataGrid dgPhrasesBase => dgPhrases;
        MUnitWord SelectedWordItem => (MUnitWord)vm.SelectedWordItem;

        public WordsUnitControl()
        {
            InitializeComponent();
            _ = OnSettingsChanged();
        }

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        void dgWords_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dgWords.CancelEdit();
            // https://stackoverflow.com/questions/14178800/how-can-i-check-if-ctrl-alt-are-pressed-on-left-mouse-click-in-c
            if (Keyboard.IsKeyDown(Key.LeftAlt))
                miAssociatePhrases_Click(sender, null);
            else
                miEditWord_Click(sender, null);
        }

        void btnBatchAdd_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new WordsUnitBatchAddDlg(Window.GetWindow(this), vm);
            dlg.ShowDialog();
        }
        void btnBatchEdit_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new WordsUnitBatchEditDlg(Window.GetWindow(this), vm);
            dlg.ShowDialog();
        }

        public void AddNewUnitWord(int phraseid)
        {
            var item = vm.NewUnitWord();
            var dlg = new WordsUnitDetailDlg(Window.GetWindow(this), vm, item, phraseid);
            dlg.ShowDialog();
        }
        void btnAdd_Click(object sender, RoutedEventArgs e) =>
            AddNewUnitWord(0);
        public void btnRefresh_Click(object sender, RoutedEventArgs e) => vm.Reload();

        public override async Task OnSettingsChanged()
        {
            DataContext = vm = new WordsUnitViewModelWPF(MainWindow.vmSettings, inTextbook: true, needCopy: true);
            await base.OnSettingsChanged();
        }
        void miEditWord_Click(object sender, RoutedEventArgs? e)
        {
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            var dlg = new WordsUnitDetailDlg(Window.GetWindow(this), vm, SelectedWordItem, 0);
            dlg.ShowDialog();
        }
        void miNewPhrase_Click(object sender, RoutedEventArgs e)
        {
            var w = (MainWindow)Window.GetWindow(this);
            w.AddNewUnitPhrase(vm.SelectedWordID);
        }

        async void miDelete_Click(object sender, RoutedEventArgs e)
        {
            await vm.Delete(SelectedWordItem);
            vm.Reload();
        }

        async void tbNewWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return || string.IsNullOrEmpty(vm.NewWord)) return;
            await vm.AddNewWord();
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
        async void miGetNote_Click(object sender, RoutedEventArgs e) =>
            await vm.GetNote(SelectedWordItem);
        async void miClearNote_Click(object sender, RoutedEventArgs e) =>
            await vm.ClearNote(SelectedWordItem);
        async void btnGetNotes_Click(object sender, RoutedEventArgs e) =>
            await vm.GetNotes(_ => { });
        async void btnClearNotes_Click(object sender, RoutedEventArgs e) =>
            await vm.ClearNotes(_ => { });
        void OnEndEditWord(object sender, DataGridCellEditEndingEventArgs e) =>
            OnEndEdit(sender, e, "WORD", async item => await vm.Update((MUnitWord)item));
    }
}
