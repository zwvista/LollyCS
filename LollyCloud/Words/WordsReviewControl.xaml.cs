using Hardcodet.Wpf.Util;
using LollyShared;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// WordsReviewControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsReviewControl : UserControl, ILollySettings
    {
        public WordsReviewViewModel vm { get; set; }

        public WordsReviewControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        public async Task OnSettingsChanged()
        {
            vm = await WordsReviewViewModel.CreateAsync(MainWindow.vmSettings, needCopy: true);
        }

        void btnNewTest_Click(object sender, RoutedEventArgs e)
        {

        }

        void chkSpeak_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
