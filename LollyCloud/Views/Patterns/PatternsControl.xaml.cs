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
            var dlg = new PatternsWebPageDlg(Window.GetWindow(this), vm, vm.NewPatternWebPage(selectedPatternID, selectedPattern));
            dlg.ShowDialog();
        }

        async void dgPatterns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var o = (MPattern)dgPatterns.SelectedItem;
            if (o == null)
            {
                selectedPattern = "";
                selectedPatternID = 0;
            }
            else
            {
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
                if (sender == dgPatterns)
                {
                    var item = (MPattern)e.Row.DataContext;
                    var el = (TextBox)e.EditingElement;
                    if (((Binding)((DataGridBoundColumn)e.Column).Binding).Path.Path == "PATTERN")
                        el.Text = vm.vmSettings.AutoCorrectInput(el.Text);
                    if (el.Text != originalText)
                        Observable.Timer(TimeSpan.FromMilliseconds(100), RxApp.MainThreadScheduler).Subscribe(async _ =>
                        {
                            await vm.Update(item);
                            dgPatterns.CancelEdit();
                        });
                }
                else if (sender == dgWebPages)
                {
                    var item = (MPatternWebPage)e.Row.DataContext;
                    var el = (TextBox)e.EditingElement;
                    if (el.Text != originalText)
                        Observable.Timer(TimeSpan.FromMilliseconds(100), RxApp.MainThreadScheduler).Subscribe(async _ =>
                        {
                            await vm.UpdatePatternWebPage(item);
                            dgWebPages.CancelEdit();
                        });
                }
            }
        }

        public async Task OnSettingsChanged()
        {
            vm = new PatternsViewModelWPF(MainWindow.vmSettings, needCopy: true);
            DataContext = vm;
        }

        async void miDelete_Click(object sender, RoutedEventArgs e)
        {
            var row = dgPatterns.SelectedIndex;
            if (row == -1) return;
            var item = vm.PatternItems[row];
            await vm.Delete(item.ID);
            vm.Reload();
        }
        void miCopy_Click(object sender, RoutedEventArgs e) => Clipboard.SetText(selectedPattern);

        void miGoogle_Click(object sender, RoutedEventArgs e) => selectedPattern.Google();

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
            var dlg = new PatternsWebPageDlg(Window.GetWindow(this), vm, (MPatternWebPage)((DataGridRow)sender).Item);
            dlg.ShowDialog();
        }

        void dgWebPages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (MPatternWebPage)dgWebPages.SelectedItem;
            if (item == null) return;
            wbWebPage.Load(item.URL);
        }
        async Task SearchPhrase() =>
            await vm.SearchPhrases(selectedPatternID);

        void dgPhrases_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (MPatternPhrase)dgPhrases.SelectedItem;
            if (item == null) return;
            App.Speak(vmSettings, item.PHRASE);
        }
    }
}
