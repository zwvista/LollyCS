using Hardcodet.Wpf.Util;
using LollyCommon;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// DictsControl.xaml の相互作用ロジック
    /// </summary>
    public partial class DictsControl : UserControl, ILollySettings
    {
        public DictsViewModel vm { get; set; }
        public DictsControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        public async Task OnSettingsChanged()
        {
            vm = new DictsViewModel(MainWindow.vmSettings, needCopy: true);
            DataContext = vm;
        }

        void dgDicts_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dgDicts.CancelEdit();
            var dlg = new DictsDetailDlg(Window.GetWindow(this), (MDictionary)((DataGridRow)sender).Item, vm);
            dlg.ShowDialog();
        }

        void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new DictsDetailDlg(Window.GetWindow(this), vm.NewDictionary(), vm);
            if (dlg.ShowDialog() == true)
                vm.Add(dlg.Item);
        }
        public void btnRefresh_Click(object sender, RoutedEventArgs e) => vm.Reload();

    }
}
