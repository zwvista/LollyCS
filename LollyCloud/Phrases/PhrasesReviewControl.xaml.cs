using Hardcodet.Wpf.Util;
using LollyShared;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// PhrasesReviewControl.xaml の相互作用ロジック
    /// </summary>
    public partial class PhrasesReviewControl : UserControl, ILollySettings
    {
        public PhrasesReviewViewModel vm { get; set; }

        public PhrasesReviewControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        public async Task OnSettingsChanged()
        {
            vm = new PhrasesReviewViewModel(MainWindow.vmSettings, needCopy: true);
        }
        void btnNewTest_Click(object sender, RoutedEventArgs e)
        {

        }

        void chkSpeak_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
