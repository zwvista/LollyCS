﻿using System.Threading.Tasks;
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
            var dlg = new PatternsDetailDlg();
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            dlg.Owner = Window.GetWindow(this);
            dlg.Item = (sender as DataGridRow).Item as MPattern;
            dlg.vm = vm;
            dlg.ShowDialog();
        }

        void btnAddPattern_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PatternsDetailDlg();
            dlg.Owner = Window.GetWindow(this);
            dlg.Item = vm.NewPattern();
            dlg.vm = vm;
            if (dlg.ShowDialog() == true)
                vm.PatternItems.Add(dlg.Item);
        }

        void btnAddWebPage_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PatternsWebPageDlg();
            dlg.Owner = Window.GetWindow(this);
            dlg.Item = vm.NewPatternWebPage(selectedPatternID, selectedPattern);
            dlg.vm = vm;
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
            }
        }
        void btnRefresh_Click(object sender, RoutedEventArgs e) => vm.Reload();

        void OnBeginEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var o = e.EditingEventArgs.Source;
            var o2 = (TextBlock)((o as DataGridCell)?.Content ?? o);
            originalText = o2.Text;
        }

        async void OnEndEdit(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var item = vm.PatternItems[e.Row.GetIndex()];
                var text = ((TextBox)e.EditingElement).Text;
                if (((Binding)((DataGridTextColumn)e.Column).Binding).Path.Path == "PATTERN")
                    text = item.PATTERN = vm.vmSettings.AutoCorrectInput(text);
                if (text != originalText)
                    await vm.Update(item);
                dgPatterns.CancelEdit(DataGridEditingUnit.Row);
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
            var dlg = new PatternsWebPageDlg();
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            dlg.Owner = Window.GetWindow(this);
            dlg.Item = (sender as DataGridRow).Item as MPatternWebPage;
            dlg.vm = vm;
            dlg.ShowDialog();
        }

        void dgWebPages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var row = dgPatterns.SelectedIndex;
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