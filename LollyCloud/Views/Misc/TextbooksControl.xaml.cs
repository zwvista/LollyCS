using Hardcodet.Wpf.Util;
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
    /// TextbooksControl.xaml の相互作用ロジック
    /// </summary>
    public partial class TextbooksControl : UserControl, ILollySettings
    {
        public TextbooksViewModel vm { get; set; }
        string originalText = "";
        public TextbooksControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        public async Task OnSettingsChanged()
        {
            vm = new TextbooksViewModel(MainWindow.vmSettings, needCopy: true);
            DataContext = vm;
        }

        void dgTextbooks_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dlg = new TextbooksDetailDlg(Window.GetWindow(this), (MTextbook)((DataGridRow)sender).Item, vm);
            dlg.ShowDialog();
        }

        void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new TextbooksDetailDlg(Window.GetWindow(this), vm.NewTextbook(), vm);
            if (dlg.ShowDialog() == true)
                vm.Add(dlg.Item);
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
                var item = (MTextbook)e.Row.DataContext;
                var el = (TextBox)e.EditingElement;
                if (el.Text != originalText)
                    Observable.Timer(TimeSpan.FromMilliseconds(100)).ObserveOn(RxApp.MainThreadScheduler).Subscribe(async _ =>
                    {
                        await vm.Update(item);
                        dgTextbooks.CancelEdit(DataGridEditingUnit.Row);
                    });
            }
        }

    }
}
