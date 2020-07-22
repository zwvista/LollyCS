using Hardcodet.Wpf.Util;
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
        }

    }
}
