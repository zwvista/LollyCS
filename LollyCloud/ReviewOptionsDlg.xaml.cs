using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using LollyShared;

namespace LollyCloud
{
    /// <summary>
    /// ReviewOptionsDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class ReviewOptionsDlg : Window
    {
        public MReviewOptions optionsOriginal;
        MReviewOptions options = new MReviewOptions();
        public ReviewOptionsDlg()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            optionsOriginal.CopyProperties(options);
            DataContext = options;
        }

        async void btnOK_Click(object sender, RoutedEventArgs e)
        {
            options.CopyProperties(optionsOriginal);
            Close();
        }

        void btnCancel_Click(object sender, RoutedEventArgs e) => Close();
    }
}
