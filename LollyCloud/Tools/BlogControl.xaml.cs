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
        public WordsUnitViewModel vm { get; set; }

        public BlogControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        public async Task OnSettingsChanged()
        {

        }
    }
}
