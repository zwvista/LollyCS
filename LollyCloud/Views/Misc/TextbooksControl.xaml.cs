using Hardcodet.Wpf.Util;
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
        public TextbooksControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        void dgTextbooks_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
        }

        async void dgTextbooks_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
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
