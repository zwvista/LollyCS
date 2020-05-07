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
        public DictsControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        void dgDicts_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
        }

        async void dgDicts_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
        }

        public async Task OnSettingsChanged()
        {
        }

        void OnBeginEdit(object sender, DataGridBeginningEditEventArgs e)
        {
        }

        async void OnEndEdit(object sender, DataGridCellEditEndingEventArgs e)
        {
        }

    }
}
