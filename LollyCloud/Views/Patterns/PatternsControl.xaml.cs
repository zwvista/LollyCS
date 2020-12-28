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
    /// PatternsControl.xaml の相互作用ロジック
    /// </summary>
    public partial class PatternsControl : UserControl, ILollySettings
    {
        public PatternsViewModelWPF vm { get; set; }
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
            dgPatterns.CancelEdit();
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            var dlg = new PatternsDetailDlg(Window.GetWindow(this), vm, (MPattern)((DataGridRow)sender).Item);
            dlg.ShowDialog();
        }

        void btnAddPattern_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PatternsDetailDlg(Window.GetWindow(this), vm, vm.NewPattern());
            dlg.ShowDialog();
        }

        void btnAddWebPage_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PatternsWebPageEditDlg(Window.GetWindow(this), vm, vm.NewPatternWebPage());
            dlg.ShowDialog();
        }

        async void dgPatterns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgPatterns.SelectedItem != null)
            {
                await vm.GetWebPages();
                if (vm.WebPageItems.Any())
                    dgWebPages.SelectedIndex = 0;
            }
        }
        void btnRefresh_Click(object sender, RoutedEventArgs e) => vm.Reload();

        void OnBeginEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var item = e.Row.Item;
            var propertyName = ((Binding)((DataGridBoundColumn)e.Column).Binding).Path.Path;
            originalText = (string)item.GetType().GetProperty(propertyName).GetValue(item);
        }

        async void OnEndEdit(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction != DataGridEditAction.Commit) return;
            var item = e.Row.Item;
            var propertyName = ((Binding)((DataGridBoundColumn)e.Column).Binding).Path.Path;
            var el = (TextBox)e.EditingElement;
            if (sender == dgPatterns && propertyName == "PATTERN")
                el.Text = vmSettings.AutoCorrectInput(el.Text);
            if (el.Text == originalText) return;
            item.GetType().GetProperty(propertyName).SetValue(item, el.Text);
            if (sender == dgPatterns)
                await vm.Update((MPattern)item);
            else
                await vm.UpdatePatternWebPage((MPatternWebPage)item);
            ((DataGrid)sender).CancelEdit();
        }

        public async Task OnSettingsChanged()
        {
            DataContext = vm = new PatternsViewModelWPF(MainWindow.vmSettings, needCopy: true);
        }

        async void miDelete_Click(object sender, RoutedEventArgs e)
        {
            var row = dgPatterns.SelectedIndex;
            if (row == -1) return;
            var item = vm.PatternItems[row];
            await vm.Delete(item.ID);
            vm.Reload();
        }
        void miCopy_Click(object sender, RoutedEventArgs e) => Clipboard.SetText(vm.SelectedPattern);

        void miGoogle_Click(object sender, RoutedEventArgs e) => vm.SelectedPattern.Google();

        void miMerge_Click(object sender, RoutedEventArgs e)
        {
            var lst = dgPatterns.SelectedItems.Cast<MPattern>().ToList();
            var dlg = new PatternsMergeDlg(Window.GetWindow(this), lst);
            dlg.ShowDialog();
        }

        void miSplit_Click(object sender, RoutedEventArgs e)
        {
            var item = (MPattern)dgPatterns.SelectedItem;
            var dlg = new PatternsSplitDlg(Window.GetWindow(this), item);
            dlg.ShowDialog();
        }

        void dgWebPages_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dgWebPages.CancelEdit();
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            var dlg = new PatternsWebPageEditDlg(Window.GetWindow(this), vm, (MPatternWebPage)((DataGridRow)sender).Item);
            dlg.ShowDialog();
        }

        void dgWebPages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (MPatternWebPage)dgWebPages.SelectedItem;
            if (item == null) return;
            wbWebPage.Load(item.URL);
        }
    }
}
