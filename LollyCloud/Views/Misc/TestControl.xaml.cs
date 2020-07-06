using Hardcodet.Wpf.Util;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// TestControl.xaml の相互作用ロジック
    /// </summary>
    public partial class TestControl : UserControl, ILollySettings
    {
        public TestControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }


        public async Task OnSettingsChanged()
        {
        }

        void btnTest_Click(object sender, RoutedEventArgs e)
        {
        }

    }
}
