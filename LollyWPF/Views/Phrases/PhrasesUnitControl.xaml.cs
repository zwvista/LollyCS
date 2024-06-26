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
    /// PhrasesUnitControl.xaml の相互作用ロジック
    /// </summary>
    public partial class PhrasesUnitControl : PhrasesBaseControl
    {
        PhrasesUnitViewModelWPF vm = null!;
        protected override PhrasesBaseViewModel vmPhrases => vm;
        public override SettingsViewModel vmSettings => vm.vmSettings;
        protected override ToolBar ToolBarDictBase => ToolBarDict;
        protected override TabablzControl tcDictsBase => tcDicts;
        MUnitPhrase SelectedPhraseItem => (MUnitPhrase)vm.SelectedPhraseItem;
        protected override DataGrid dgWordsBase => dgWords;

        public PhrasesUnitControl()
        {
            InitializeComponent();
            _ = OnSettingsChanged();
        }
        public void btnRefresh_Click(object sender, RoutedEventArgs e) => vm.Reload();

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        void dgPhrases_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dgPhrases.CancelEdit();
            if (Keyboard.IsKeyDown(Key.LeftAlt))
                miAssociateWords_Click(sender, null);
            else
                miEditPhrase_Click(sender, null);
        }

        void btnBatchAdd_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PhrasesUnitBatchAddDlg(Window.GetWindow(this), vm);
            dlg.ShowDialog();
        }
        void btnBatchEdit_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PhrasesUnitBatchEditDlg(Window.GetWindow(this), vm);
            dlg.ShowDialog();
        }

        public void AddNewUnitPhrase(int wordid)
        {
            var item = vm.NewUnitPhrase();
            var dlg = new PhrasesUnitDetailDlg(Window.GetWindow(this), vm, item, wordid);
            dlg.ShowDialog();
        }

        void btnAdd_Click(object sender, RoutedEventArgs e) =>
            AddNewUnitPhrase(0);

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
            await base.OnSettingsChanged();
        }
        void miEditPhrase_Click(object sender, RoutedEventArgs? e)
        {
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            var dlg = new PhrasesUnitDetailDlg(Window.GetWindow(this), vm, SelectedPhraseItem, 0);
            dlg.ShowDialog();
        }
        void miNewWord_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new WordsAssociateDlg(Window.GetWindow(this), vmSettings, vm.SelectedPhraseID, vm.SelectedPhrase);
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
    }
}
