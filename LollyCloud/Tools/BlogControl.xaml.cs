using LollyShared;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// BlogControl.xaml の相互作用ロジック
    /// </summary>
    public partial class BlogControl : UserControl, ILollySettings
    {
        public BlogViewModel vm { get; set; }

        public BlogControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        public async Task OnSettingsChanged()
        {
            vm = new BlogViewModel(MainWindow.vmSettings, true);
        }

        private void btnHtmlToMarked_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddTagB_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddTagI_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRemoveTagBI_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExchangeTagBI_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddExplanation_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
