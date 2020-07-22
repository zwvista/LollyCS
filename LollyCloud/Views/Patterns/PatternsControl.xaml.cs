﻿using System;
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
    /// PatternsControl.xaml の相互作用ロジック
    /// </summary>
    public partial class PatternsControl : UserControl, ILollySettings
    {
        public PatternsViewModel vm { get; set; }
        public string selectedPattern = "";
        public int selectedPatternID = 0;
        public string originalText = "";
        public SettingsViewModel vmSettings => vm.vmSettings;

        public PatternsControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        void dgPatterns_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            var dlg = new PatternsDetailDlg(Window.GetWindow(this), (MPattern)((DataGridRow)sender).Item, vm);
            dlg.ShowDialog();
        }

        void btnAddPattern_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PatternsDetailDlg(Window.GetWindow(this), vm.NewPattern(), vm);
            if (dlg.ShowDialog() == true)
                vm.PatternItems.Add(dlg.Item);
        }

        void btnAddWebPage_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PatternsWebPageDlg(Window.GetWindow(this), vm.NewPatternWebPage(selectedPatternID, selectedPattern), vm);
            if (dlg.ShowDialog() == true)
                vm.WebPageItems.Add(dlg.Item);
        }

        async void dgPatterns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var row = dgPatterns.SelectedIndex;
            if (row == -1)
            {
                selectedPattern = "";
                selectedPatternID = 0;
            }
            else
            {
                var o = vm.PatternItems[row];
                selectedPattern = o.PATTERN;
                selectedPatternID = o.ID;
                await vm.GetWebPages(selectedPatternID);
                await SearchPhrase();
                if (vm.WebPageItems.Any())
                    dgWebPages.SelectedIndex = 0;
            }
        }
        void btnRefresh_Click(object sender, RoutedEventArgs e) => vm.Reload();

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
                var item = (MPattern)e.Row.DataContext;
                var el = (TextBox)e.EditingElement;
                if (((Binding)((DataGridBoundColumn)e.Column).Binding).Path.Path == "PATTERN")
                    el.Text = vm.vmSettings.AutoCorrectInput(el.Text);
                if (el.Text != originalText)
                    Observable.Timer(TimeSpan.FromMilliseconds(100)).Subscribe(async _ =>
                    {
                        await vm.Update(item);
                        dgPatterns.CancelEdit(DataGridEditingUnit.Row);
                    });
            }
        }

        public async Task OnSettingsChanged()
        {
            vm = new PatternsViewModel(MainWindow.vmSettings, needCopy: true);
            DataContext = vm;
        }

        async void miDelete_Click(object sender, RoutedEventArgs e)
        {
            var row = dgPatterns.SelectedIndex;
            if (row == -1) return;
            var item = vm.PatternItems[row];
            await vm.Delete(item.ID);
        }
        void miCopy_Click(object sender, RoutedEventArgs e) => Clipboard.SetText(selectedPattern);

        void miGoogle_Click(object sender, RoutedEventArgs e) => CommonApi.GoogleString(selectedPattern);

        void dgWebPages_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            var dlg = new PatternsWebPageDlg(Window.GetWindow(this), (MPatternWebPage)((DataGridRow)sender).Item, vm);
            dlg.ShowDialog();
        }

        void dgWebPages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var row = dgWebPages.SelectedIndex;
            if (row == -1) return;
            var item = vm.WebPageItems[row];
            wbWebPage.Load(item.URL);
        }
        async Task SearchPhrase() =>
            await vm.SearchPhrases(selectedPatternID);

        void dgPhrases_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var row = dgPhrases.SelectedIndex;
            if (row == -1) return;
            var item = vm.PhraseItems[row];
            App.Speak(vmSettings, item.PHRASE);
        }
    }
}