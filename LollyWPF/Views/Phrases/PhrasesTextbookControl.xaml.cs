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
    public partial class PhrasesTextbookControl : PhrasesBaseControl
    {
        PhrasesUnitViewModelWPF vm = null!;
        protected override PhrasesBaseViewModel vmPhrases => vm;
        public override SettingsViewModel vmSettings => vm.vmSettings;
        protected override ToolBar ToolBarDictBase => ToolBarDict;
        protected override TabablzControl tcDictsBase => tcDicts;
        MUnitPhrase SelectedPhraseItem => (MUnitPhrase)vm.SelectedPhraseItem;
        protected override DataGrid dgWordsBase => dgWords;

        public PhrasesTextbookControl()
        {
            InitializeComponent();
            _ = OnSettingsChanged();
        }

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        void dgPhrases_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dgPhrases.CancelEdit();
            if (Keyboard.IsKeyDown(Key.LeftAlt))
                miAssociateWords_Click(sender, null);
            else
                miEditPhrase_Click(sender, null);
        }
        public void btnRefresh_Click(object sender, RoutedEventArgs e) => vm.Reload();

        void OnEndEditPhrase(object sender, DataGridCellEditEndingEventArgs e) =>
            OnEndEdit(sender, e, "PHRASE", async item => await vm.Update((MUnitPhrase)item));

        public override async Task OnSettingsChanged()
        {
            DataContext = vm = new PhrasesUnitViewModelWPF(MainWindow.vmSettings, inTextbook: false, needCopy: true);
            await base.OnSettingsChanged();
        }
        void miEditPhrase_Click(object sender, RoutedEventArgs? e)
        {
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            var dlg = new PhrasesTextbookDetailDlg(Window.GetWindow(this), vm, SelectedPhraseItem);
            dlg.ShowDialog();
        }

        async void miDelete_Click(object sender, RoutedEventArgs e)
        {
            await vm.Delete(SelectedPhraseItem);
            vm.Reload();
        }
    }
}
