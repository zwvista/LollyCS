﻿using Dragablz;
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
    /// WordsLangControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsLangControl : WordsBaseControl
    {
        public WordsLangViewModel vm { get; set; }
        public override WordsPhrasesBaseViewModel vmWP => vm;
        public override SettingsViewModel vmSettings => vm.vmSettings;
        public override ToolBar ToolBarDictBase => ToolBarDict;
        public override TabablzControl tcDictsBase => tcDicts;
        MLangWord SelectedWordItem => (MLangWord)vm.SelectedWordItem;

        public WordsLangControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        void dgWords_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dgWords.CancelEdit();
            miEdit_Click(sender, null);
        }

        void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new WordsLangDetailDlg(Window.GetWindow(this), vm, vm.NewLangWord());
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
                var item = (MLangWord)e.Row.DataContext;
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

        public override async Task OnSettingsChanged()
        {
            DataContext = vm = new WordsLangViewModel(MainWindow.vmSettings, needCopy: true);
            tcDicts.DataContext = this;
            await base.OnSettingsChanged();
        }
        void miEdit_Click(object sender, RoutedEventArgs e)
        {
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            var dlg = new WordsLangDetailDlg(Window.GetWindow(this), vm, SelectedWordItem);
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
            var item = vm.NewLangWord();
            item.WORD = vmSettings.AutoCorrectInput(vm.NewWord);
            vm.NewWord = "";
            await vm.Create(item);
            vm.WordItems.Add(item);
        }
        public override async Task SearchPhrases() =>
            await vm.SearchPhrases(vm.SelectedWordID);
    }
}
